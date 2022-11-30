using Autofac;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections;
using System.Collections.Generic;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionFactory
{
    public class ToolBlockComponentSet : ICheckable, IEnumerable<IToolBlockComponent>
    {
        public event Action<IToolBlockComponent> ToolBlockComponentAdd;
        public event Action<IToolBlockComponent> ToolBlockComponentDelete;
        public event RemovedCalibHandle ToolBlockRemovedCalib;

        readonly ITaskRepository taskRepo;
        readonly IGLog log;

        readonly object _sync = new object();
        readonly Dictionary<string, IToolBlockComponent> toolBlockComponentDict = new Dictionary<string, IToolBlockComponent>();

        public ToolBlockComponentSet(ITaskRepository taskRepo,
            INameMappingRepository nameMappingRepo, // 先加载事件
            ICalibrationRepository calibRepo,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.log = log;

            taskRepo.TaskAdd += TaskRepo_TaskAdd;
            taskRepo.TaskDelete += TaskRepo_TaskDelete;
            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.AfterSave += TaskRepo_TaskSave;

            taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;

            taskRepo.CalibAdd += TaskRepo_CalibAdd;
            taskRepo.CalibDelete += TaskRepo_CalibDelete;
            taskRepo.CalibRename += TaskRepo_CalibRename;
        }

        public void Init()
        {
            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            foreach (TaskInfoVO task in tasks)
            {
                AddComponent(task);
            }
        }

        #region 事件

        #region 任务

        private void TaskRepo_TaskAdd(TaskInfoVO task)
        {
            if (toolBlockComponentDict.ContainsKey(task.Name))
            {
                return;
            }

            IToolBlockComponent toolBlockComponent = AddComponent(task);

            ToolBlockComponentAdd?.Invoke(toolBlockComponent);
        }

        IToolBlockComponent AddComponent(TaskInfoVO task)
        {
            IToolBlockComponent toolBlockComponent = AutoFacContainer.Resolve<IToolBlockComponent>(
                new NamedParameter("task", task));

            if (task.Type == TaskType.VP)
            {
                toolBlockComponent.ComponentRemovedCalib += ToolBlockComponent_RemovedCalib;
            }

            toolBlockComponentDict.Add(task.Name, toolBlockComponent);
            return toolBlockComponent;
        }

        private void ToolBlockComponent_RemovedCalib(string taskName, string calibName)
        {
            ToolBlockRemovedCalib?.Invoke(taskName, calibName);
        }

        private void TaskRepo_TaskDelete(string taskname)
        {
            if (!toolBlockComponentDict.ContainsKey(taskname))
            {
                log.Error($"删除视觉处理失败，未找到[{taskname}]");
                return;
            }

            ToolBlockComponentDelete?.Invoke(toolBlockComponentDict[taskname]);

            toolBlockComponentDict[taskname].Dispose();

            toolBlockComponentDict.Remove(taskname);
            log.Info($"任务[{taskname}]删除视觉处理");
        }

        private void TaskRepo_TaskRename(string oldname, string newname)
        {
            if (!toolBlockComponentDict.ContainsKey(oldname))
            {
                log.Error($"重命名视觉处理失败，未找到[{oldname}]");
                return;
            }

            if (toolBlockComponentDict.ContainsKey(newname))
            {
                log.Error($"重命名视觉处理失败，[{newname}]已存在");
                return;
            }

            IToolBlockComponent toolBlockComponent = toolBlockComponentDict[oldname];
            toolBlockComponent.RenameTaskName(newname);
            toolBlockComponentDict.Remove(oldname);
            toolBlockComponentDict.Add(newname, toolBlockComponent);
        }

        private void TaskRepo_TaskSave()
        {
            SaveAll();
        }

        #endregion

        #region 拍照

        private void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            lock (_sync)
            {
                IToolBlockComponent toolBlockComponent = toolBlockComponentDict[taskName];

                toolBlockComponent.AddAcqImage(acqImageName);
            }
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            lock (_sync)
            {
                IToolBlockComponent toolBlockComponent = toolBlockComponentDict[taskName];

                toolBlockComponent.RemoveAcqImage(acqImageName);
            }
        }

        #endregion

        #region 标定

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            lock (_sync)
            {
                if (calibType != CalibrationType.Joint)
                {
                    IToolBlockComponent toolBlockComponent = toolBlockComponentDict[taskName];
                    toolBlockComponent.AddCalibration(calibType, calibName);
                }
                else
                {
                    // 这里不需要加什么
                    //toolBlockComponent.AddCalibration(calibType, $"{calibName}_{acqImageName}");
                }
            }
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            lock (_sync)
            {
                IToolBlockComponent toolBlockComponent = toolBlockComponentDict[taskName];

                if (calibType == CalibrationType.Joint)
                {
                    toolBlockComponent.RemoveTool($"{calibName}_{acqImageName}");
                }
                else
                {
                    toolBlockComponent.RemoveTool(calibName);
                }
            }
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            lock (_sync)
            {
                IToolBlockComponent toolBlockComponent = toolBlockComponentDict[taskName];

                toolBlockComponent.RenameTool(oldCalibName, newCalibName);
            }
        }

        #endregion

        #endregion

        public IToolBlockComponent GetComponent(string taskname)
        {
            lock (_sync)
            {
                if (toolBlockComponentDict.ContainsKey(taskname))
                    return toolBlockComponentDict[taskname];
                else
                {
                    log.Error($"未找到任务[{taskname}]对应的 CogToolBlock");
                    throw new Exception($"未找到任务[{taskname}]对应的 CogToolBlock");
                }
            }
        }

        public bool Check()
        {
            bool result = true;
            foreach (IToolBlockComponent component in toolBlockComponentDict.Values)
            {
                if (!component.Check())
                {
                    result = false;
                }
            }

            return result;
        }

        public void Save(string taskName)
        {
            lock (_sync)
            {
                IToolBlockComponent toolBlockComponent = toolBlockComponentDict[taskName];

                toolBlockComponent.Save();
            }
        }

        public void SaveAll()
        {
            lock (_sync)
            {
                foreach (IToolBlockComponent toolBlockComponent in toolBlockComponentDict.Values)
                {
                    toolBlockComponent.Save();
                }
            }
        }

        public void ExpandAll()
        {
            foreach (IToolBlockComponent toolBlockComponent in toolBlockComponentDict.Values)
            {
                toolBlockComponent.ExpandAll();
            }
        }

        public void ExpandOne(string toolKey)
        {
            IToolBlockComponent toolBlockComponent;
            bool IsGetValue = toolBlockComponentDict.TryGetValue(toolKey, out toolBlockComponent);
            if (IsGetValue)
                toolBlockComponent.ExpandAll();
        }

        public IEnumerator<IToolBlockComponent> GetEnumerator()
        {
            return toolBlockComponentDict.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
