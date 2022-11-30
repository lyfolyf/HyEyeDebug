using AutoMapper;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using PlcSDK;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface IPlcRepository : ICheckable
    {
        PlcDeviceName StartReadDeviceName { get; set; }

        int ReadLength { get; set; }

        PlcDeviceName ReadFlagDeviceName { get; set; }

        PlcDeviceName WriteFlagDeviceName { get; set; }

        List<PlcCommandInfoVO> GetTaskRecvCommandInfos();
        void ResetTaskRecvCommands(List<PlcCommandInfoVO> commandInfos);

        List<PlcCommandInfoVO> GetTaskSendCommandInfos();
        void ResetTaskSendCommands(List<PlcCommandInfoVO> commandInfos);

        List<PlcCommandInfoVO> GetCalibRecvCommandInfos();
        void ResetCalibRecvCommands(List<PlcCommandInfoVO> commandInfos);

        List<PlcCommandInfoVO> GetCalibSendCommandInfos();
        void ResetCalibSendCommands(List<PlcCommandInfoVO> commandInfos);

        void Save();
    }

    public class PlcRepository : IPlcRepository
    {
        readonly ITaskRepository taskRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        readonly PlcSettings plcSettings;

        int maxIndexT = 1;
        int maxIndexCB = 1;
        int maxIndexHE = 1;
        int maxIndexJN = 1;

        bool changedToken = false;
        bool sortedToken = true;

        public PlcRepository(
            ITaskRepository taskRepo,
            IMapper mapper,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.mapper = mapper;
            this.log = log;

            plcSettings = ApiConfig.PlcSettings;

            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.TaskMoveUp += TaskRepo_TaskMove;
            taskRepo.TaskMoveDown += TaskRepo_TaskMove;

            taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            taskRepo.AcqImageRename += TaskRepo_AcqImageRename;
            taskRepo.AcqImageMoveUp += TaskRepo_AcqImageMove;
            taskRepo.AcqImageMoveDown += TaskRepo_AcqImageMove;

            taskRepo.CalibAdd += TaskRepo_CalibAdd;
            taskRepo.CalibDelete += TaskRepo_CalibDelete;
            taskRepo.CalibRename += TaskRepo_CalibRename;

            taskRepo.AfterSave += TaskRepo_AfterSave;

            LoadTaskCommands();

            LoadCalibCommands();
        }

        // 这里 CommandHeader 是多少并不重要，只是在日志中显示
        // 本质还是根据任务名称来做的关联

        void LoadTaskCommands()
        {
            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            if (plcSettings.TaskRecvCommands.Count == 0)
            {
                InitTaskCommands(tasks);
            }
            else
            {
                GetMaxIndexT();

                SortTaskCommands(tasks);
            }
        }

        void InitTaskCommands(List<TaskInfoVO> tasks)
        {
            plcSettings.TaskRecvCommands.Clear();
            plcSettings.TaskSendCommands.Clear();

            maxIndexT = 1;

            foreach (TaskInfoVO task in tasks.Where(t => t.CameraAcquireImage != null))
            {
                foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                {
                    TaskRepo_AcqImageAdd(task.Name, acqImage.Name);
                }
            }
        }

        void LoadCalibCommands()
        {
            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            if (plcSettings.CalibRecvCommands.Count == 0)
            {
                InitCalibCommands(tasks);
            }
            else
            {
                GetMaxIndexCB();
                GetMaxIndexHE();
                GetMaxIndexJN();

                SortCalibCommands(tasks);
            }
        }

        void InitCalibCommands(List<TaskInfoVO> tasks)
        {
            plcSettings.CalibRecvCommands.Clear();
            plcSettings.CalibSendCommands.Clear();

            maxIndexCB = 1;
            maxIndexHE = 1;
            maxIndexJN = 1;

            foreach (TaskInfoVO task in tasks.Where(t => t.CameraAcquireImage != null))
            {
                foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                {
                    if (!string.IsNullOrEmpty(acqImage.CheckerboardName))
                        TaskRepo_CalibAdd(task.Name, acqImage.Name, CalibrationType.Checkerboard, acqImage.CheckerboardName);

                    if (acqImage.HandEyeNames != null && acqImage.HandEyeNames.Count > 0)
                    {
                        foreach (string handeyeName in acqImage.HandEyeNames)
                            TaskRepo_CalibAdd(task.Name, acqImage.Name, CalibrationType.HandEye, handeyeName);
                    }
                }
            }
        }

        void GetMaxIndexT()
        {
            var indexs = plcSettings.TaskRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^T(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexT = indexs.Max() + 1;
            }
        }

        void GetMaxIndexCB()
        {
            var indexs = plcSettings.CalibRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^CB(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexCB = indexs.Max() + 1;
            }
        }

        void GetMaxIndexHE()
        {
            var indexs = plcSettings.CalibRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^HE(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexHE = indexs.Max() + 1;
            }
        }

        void GetMaxIndexJN()
        {
            var indexs = plcSettings.CalibRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^JN(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexJN = indexs.Max() + 1;
            }
        }

        void SortTaskCommands(List<TaskInfoVO> tasks)
        {
            plcSettings.TaskRecvCommands = plcSettings.TaskRecvCommands
                .Join(tasks, c => c.TaskName, t => t.Name, (c, t) => new { Task = t, Command = c })
                .OrderBy(a => a.Task.Order)
                .ThenBy(a => a.Command.AcqIndex)
                .Select(a => a.Command).ToList();

            plcSettings.TaskSendCommands = plcSettings.TaskSendCommands
                .Join(tasks, c => c.TaskName, t => t.Name, (c, t) => new { Task = t, Command = c })
                .OrderBy(a => a.Task.Order)
                .ThenBy(a => a.Command.AcqIndex)
                .Select(a => a.Command).ToList();
        }

        void SortCalibCommands(List<TaskInfoVO> tasks)
        {

        }

        void Sort()
        {
            if (!sortedToken)
            {
                List<TaskInfoVO> tasks = taskRepo.GetTasks();
                SortTaskCommands(tasks);
                SortCalibCommands(tasks);

                sortedToken = true;
            }
        }

        #region 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            foreach (var item in plcSettings.TaskRecvCommands.Where(p => p.TaskName == oldName))
            {
                item.TaskName = newName;
            }

            foreach (var item in plcSettings.TaskSendCommands.Where(p => p.TaskName == oldName))
            {
                item.TaskName = newName;
            }

            foreach (var item in plcSettings.CalibRecvCommands.Where(p => p.TaskName == oldName))
            {
                item.TaskName = newName;
            }

            foreach (var item in plcSettings.CalibSendCommands.Where(p => p.TaskName == oldName))
            {
                item.TaskName = newName;
            }

            changedToken = true;
        }

        private void TaskRepo_TaskMove(string taskName1, string taskName2)
        {
            changedToken = true;
            sortedToken = false;
        }

        private void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            PlcCommandInfo recvCmdInfo = new PlcCommandInfo
            {
                TaskName = taskName,
                AcqName = acqImageName,
                StartDeviceName = "D0",
                DeviceLength = 3
            };

            PlcCommandInfo first = plcSettings.TaskRecvCommands.FirstOrDefault(c => c.TaskName == taskName);
            if (first == null)
            {
                recvCmdInfo.CommandHeader = $"T{maxIndexT++}";
                recvCmdInfo.AcqIndex = 1;
            }
            else
            {
                recvCmdInfo.CommandHeader = first.CommandHeader;
                recvCmdInfo.AcqIndex = plcSettings.TaskRecvCommands.Where(c => c.TaskName == taskName).Max(c => c.AcqIndex) + 1;
            }

            plcSettings.TaskRecvCommands.Add(recvCmdInfo);

            PlcCommandInfo sendCmdInfo = new PlcCommandInfo
            {
                TaskName = taskName,
                CommandHeader = recvCmdInfo.CommandHeader,
                AcqName = acqImageName,
                AcqIndex = recvCmdInfo.AcqIndex,
                StartDeviceName = "D0",
                DeviceLength = 4,
                //Fields = DefaultSendFieldInfo
            };

            plcSettings.TaskSendCommands.Add(sendCmdInfo);

            changedToken = true;
            sortedToken = false;
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            PlcCommandInfo recvCmdInfo = plcSettings.TaskRecvCommands.FirstOrDefault(p => p.TaskName == taskName && p.AcqName == oldAcqImageName);
            if (recvCmdInfo != null)
            {
                recvCmdInfo.AcqName = newAcqImageName;
            }

            PlcCommandInfo sendCmdInfo = plcSettings.TaskSendCommands.FirstOrDefault(p => p.TaskName == taskName && p.AcqName == oldAcqImageName);
            if (sendCmdInfo != null)
            {
                sendCmdInfo.AcqName = newAcqImageName;
            }

            changedToken = true;
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            plcSettings.TaskRecvCommands.Remove(c => c.TaskName == taskName && c.AcqName == acqImageName);
            plcSettings.TaskSendCommands.Remove(c => c.TaskName == taskName && c.AcqName == acqImageName);

            changedToken = true;
        }

        private void TaskRepo_AcqImageMove(string taskName, string acqImageName1, string acqImageName2)
        {
            PlcCommandInfo recv1 = plcSettings.TaskRecvCommands.FirstOrDefault(a => a.AcqName == acqImageName1);
            PlcCommandInfo recv2 = plcSettings.TaskRecvCommands.FirstOrDefault(a => a.AcqName == acqImageName2);
            if (recv1 != null && recv2 != null)
            {
                int i = recv1.AcqIndex;
                recv1.AcqIndex = recv2.AcqIndex;
                recv2.AcqIndex = i;
            }

            PlcCommandInfo send1 = plcSettings.TaskSendCommands.FirstOrDefault(a => a.AcqName == acqImageName1);
            PlcCommandInfo send2 = plcSettings.TaskSendCommands.FirstOrDefault(a => a.AcqName == acqImageName2);
            if (send1 != null && send2 != null)
            {
                int i = send1.AcqIndex;
                send1.AcqIndex = send2.AcqIndex;
                send2.AcqIndex = i;
            }

            changedToken = true;
            sortedToken = false;
        }

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            PlcCommandInfo recvCmdInfo = new PlcCommandInfo
            {
                TaskName = taskName,
                AcqName = calibName,
                AcqIndex = 1,
                StartDeviceName = "D0",
                DeviceLength = 9
            };
            if (calibType == CalibrationType.Checkerboard)
                recvCmdInfo.CommandHeader = "CB" + (maxIndexCB++).ToString();
            else if (calibType == CalibrationType.HandEye)
                recvCmdInfo.CommandHeader = "HE" + (maxIndexHE++).ToString();
            else if (calibType == CalibrationType.Joint)
                recvCmdInfo.CommandHeader = "JN" + (maxIndexJN++).ToString();

            // 手眼标定必须有 X, Y 参数，否则 Runner 中是会报错的
            if (calibType == CalibrationType.HandEye)
            {
                recvCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "X",
                    DeviceName = recvCmdInfo.StartDeviceName + 3,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "X"
                });
                recvCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "Y",
                    DeviceName = recvCmdInfo.StartDeviceName + 5,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "Y"
                });
                recvCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "A",
                    DeviceName = recvCmdInfo.StartDeviceName + 7,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "A"
                });
            }
            else if (calibType == CalibrationType.HandEyeSingle)
            {
                for (int i = 1; i <= 9; i++)
                {
                    recvCmdInfo.Fields.Add(new PlcCommandFieldInfo
                    {
                        Name = "X" + i.ToString(),
                        DeviceName = recvCmdInfo.StartDeviceName + 3 + (i - 1) * 4,
                        ValueType = typeof(int),
                        DeviceLength = 2,
                        ValueRatio = 1,
                        Use = CommandFieldUse.None,
                        Description = "X" + i.ToString()
                    });
                    recvCmdInfo.Fields.Add(new PlcCommandFieldInfo
                    {
                        Name = "Y" + i.ToString(),
                        DeviceName = recvCmdInfo.StartDeviceName + 5 + (i - 1) * 4,
                        ValueType = typeof(int),
                        DeviceLength = 2,
                        ValueRatio = 1,
                        Use = CommandFieldUse.None,
                        Description = "Y" + i.ToString()
                    });
                }
            }

            plcSettings.CalibRecvCommands.Add(recvCmdInfo);

            PlcCommandInfo sendCmdInfo = new PlcCommandInfo
            {
                TaskName = taskName,
                CommandHeader = recvCmdInfo.CommandHeader,
                AcqName = calibName,
                AcqIndex = recvCmdInfo.AcqIndex,
                StartDeviceName = "D0",
                DeviceLength = 10,
            };

            if (calibType == CalibrationType.HandEye)
            {
                sendCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "X",
                    DeviceName = recvCmdInfo.StartDeviceName + 3,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "X"
                });
                sendCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "Y",
                    DeviceName = recvCmdInfo.StartDeviceName + 5,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "Y"
                });
                sendCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "A",
                    DeviceName = recvCmdInfo.StartDeviceName + 7,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "A"
                });
            }
            else if (calibType == CalibrationType.HandEyeSingle)
            {
                sendCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "X",
                    DeviceName = recvCmdInfo.StartDeviceName + 3,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "X"
                });
                sendCmdInfo.Fields.Add(new PlcCommandFieldInfo
                {
                    Name = "Y",
                    DeviceName = recvCmdInfo.StartDeviceName + 5,
                    ValueType = typeof(int),
                    DeviceLength = 2,
                    ValueRatio = 1,
                    Use = CommandFieldUse.None,
                    Description = "Y"
                });
            }

            plcSettings.CalibSendCommands.Add(sendCmdInfo);

            changedToken = true;
            sortedToken = false;
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            plcSettings.CalibRecvCommands.RemoveAll(a => a.AcqName == calibName);
            plcSettings.CalibSendCommands.RemoveAll(a => a.AcqName == calibName);

            changedToken = true;
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            PlcCommandInfo recvCmd = plcSettings.CalibRecvCommands.FirstOrDefault(a => a.TaskName == taskName && a.AcqName == oldCalibName);
            if (recvCmd != null)
            {
                recvCmd.AcqName = newCalibName;
            }

            PlcCommandInfo sendCmd = plcSettings.CalibSendCommands.FirstOrDefault(a => a.TaskName == taskName && a.AcqName == oldCalibName);
            if (sendCmd != null)
            {
                sendCmd.AcqName = newCalibName;
            }

            changedToken = true;
        }

        private void TaskRepo_AfterSave()
        {
            Save();
        }

        #endregion

        public bool Check()
        {
            if (string.IsNullOrWhiteSpace(StartReadDeviceName)) return false;
            if (string.IsNullOrWhiteSpace(ReadFlagDeviceName)) return false;
            if (string.IsNullOrWhiteSpace(WriteFlagDeviceName)) return false;
            if (ReadLength <= 0) return false;

            return true;
        }

        #region

        public PlcDeviceName StartReadDeviceName
        {
            get { return plcSettings.StartReadDeviceName; }
            set
            {
                if (plcSettings.StartReadDeviceName != value)
                {
                    plcSettings.StartReadDeviceName = value;
                    changedToken = true;
                }
            }
        }

        public int ReadLength
        {
            get { return plcSettings.ReadLength; }
            set
            {
                if (plcSettings.ReadLength != value)
                {
                    plcSettings.ReadLength = value;
                    changedToken = true;
                }
            }
        }

        public PlcDeviceName ReadFlagDeviceName
        {
            get { return plcSettings.ReadFlagDeviceName; }
            set
            {
                if (plcSettings.ReadFlagDeviceName != value)
                {
                    plcSettings.ReadFlagDeviceName = value;
                    changedToken = true;
                }
            }
        }

        public PlcDeviceName WriteFlagDeviceName
        {
            get { return plcSettings.WriteFlagDeviceName; }
            set
            {
                if (plcSettings.WriteFlagDeviceName != value)
                {
                    plcSettings.WriteFlagDeviceName = value;
                    changedToken = true;
                }
            }
        }

        public List<PlcCommandInfoVO> GetTaskRecvCommandInfos()
        {
            Sort();

            return mapper.Map<List<PlcCommandInfoVO>>(plcSettings.TaskRecvCommands);
        }

        public void ResetTaskRecvCommands(List<PlcCommandInfoVO> commandInfos)
        {
            plcSettings.TaskRecvCommands = mapper.Map<List<PlcCommandInfo>>(commandInfos);

            changedToken = true;
        }

        public List<PlcCommandInfoVO> GetTaskSendCommandInfos()
        {
            Sort();

            return mapper.Map<List<PlcCommandInfoVO>>(plcSettings.TaskSendCommands);
        }

        public void ResetTaskSendCommands(List<PlcCommandInfoVO> commandInfos)
        {
            plcSettings.TaskSendCommands = mapper.Map<List<PlcCommandInfo>>(commandInfos);

            changedToken = true;
        }

        public List<PlcCommandInfoVO> GetCalibRecvCommandInfos()
        {
            Sort();

            return mapper.Map<List<PlcCommandInfoVO>>(plcSettings.CalibRecvCommands);
        }

        public void ResetCalibRecvCommands(List<PlcCommandInfoVO> commandInfos)
        {
            plcSettings.CalibRecvCommands = mapper.Map<List<PlcCommandInfo>>(commandInfos);

            changedToken = true;
        }

        public List<PlcCommandInfoVO> GetCalibSendCommandInfos()
        {
            Sort();

            return mapper.Map<List<PlcCommandInfoVO>>(plcSettings.CalibSendCommands);
        }

        public void ResetCalibSendCommands(List<PlcCommandInfoVO> commandInfos)
        {
            plcSettings.CalibSendCommands = mapper.Map<List<PlcCommandInfo>>(commandInfos);

            changedToken = true;
        }

        #endregion

        public void Save()
        {
            if (changedToken)
            {
                Sort();

                ApiConfig.Save(plcSettings);

                changedToken = false;

                log.Info(new ApiLogMessage("PLC寄存器设置", null, A_Save, R_Success));
            }
        }

    }
}
