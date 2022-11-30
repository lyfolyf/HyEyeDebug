using Autofac;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using VisionSDK;

namespace VisionFactory
{
    public class CalibrationComponentSet
    {
        readonly ToolBlockComponentSet toolBlocks;
        readonly IGLog log;
        readonly ICalibrationRepository calibRepo;

        readonly object _sync = new object();
        readonly Dictionary<string, IHandeyeComponent> handeyeComponents = new Dictionary<string, IHandeyeComponent>();
        readonly Dictionary<string, IHandeyeSingleComponent> handeyeSingleComponents = new Dictionary<string, IHandeyeSingleComponent>();
        readonly Dictionary<string, ICheckerboardComponent> checkerboardComponents = new Dictionary<string, ICheckerboardComponent>();
        readonly Dictionary<string, IJointComponent> jointComponents = new Dictionary<string, IJointComponent>();

        public CalibrationComponentSet(
            ToolBlockComponentSet toolBlocks,
            ITaskRepository taskRepo,
            ICalibrationRepository calibRepo,
            IGLog log)
        {
            this.toolBlocks = toolBlocks;

            this.calibRepo = calibRepo;
            this.log = log;

            taskRepo.CalibAdd += TaskRepo_CalibAdd;
            taskRepo.CalibDelete += TaskRepo_CalibDelete;
            taskRepo.CalibRename += TaskRepo_CalibRename;

            taskRepo.CalibQuote += TaskRepo_CalibQuote;
            taskRepo.CancelCalibQuote += TaskRepo_CancelCalibQuote;
        }

        #region Calibration 事件

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
            IToolBlockComponent toolBlock = toolBlocks.GetComponent(taskName);

            if (calibInfo.CalibrationType == CalibrationType.Checkerboard)
            {
                if (checkerboardComponents.ContainsKey(calibInfo.Name))
                {
                    log.Error($"添加标定组件失败，[{calibInfo.Name}]已存在");
                    return;
                }

                ICheckerboardComponent checkerboardComponent = AutoFacContainer.Resolve<ICheckerboardComponent>(
                    new NamedParameter("calibInfo", calibInfo),
                    new NamedParameter("toolBlock",toolBlock));

                checkerboardComponents.Add(calibInfo.Name, checkerboardComponent);
            }
            else if (calibInfo.CalibrationType == CalibrationType.HandEye)
            {
                if (handeyeComponents.ContainsKey(calibInfo.Name))
                {
                    log.Error($"添加标定组件失败，[{calibInfo.Name}]已存在");
                    return;
                }

                IHandeyeComponent handeyeComponent = AutoFacContainer.Resolve<IHandeyeComponent>(
                    new NamedParameter("calibInfo", calibInfo),
                    new NamedParameter("toolBlock",toolBlock));

                handeyeComponents.Add(calibInfo.Name, handeyeComponent);
            }
            else if (calibInfo.CalibrationType == CalibrationType.HandEyeSingle)
            {
                IHandeyeSingleComponent component = AutoFacContainer.Resolve<IHandeyeSingleComponent>(
                    new NamedParameter("calibInfo", calibInfo),
                    new NamedParameter("toolBlock",toolBlock));
                handeyeSingleComponents.Add(calibInfo.Name, component);
            }
            else if (calibInfo.CalibrationType == CalibrationType.Joint)
            {
                if (jointComponents.ContainsKey(calibInfo.Name))
                {
                    log.Error($"添加标定组件失败，[{calibInfo.Name}]已存在");
                    return;
                }

                Dictionary<string, IToolBlockComponent> toolBlockDict = new Dictionary<string, IToolBlockComponent>();

                foreach (TaskAcqImageVO acq in calibInfo.JointInfo.Slaves)
                {
                    if (!toolBlockDict.ContainsKey(acq.TaskName))
                        toolBlockDict.Add(acq.TaskName, toolBlocks.GetComponent(acq.TaskName));
                }

                IJointComponent jointComponent = AutoFacContainer.Resolve<IJointComponent>(
                        new NamedParameter("calibInfo", calibInfo),
                        new NamedParameter("toolBlocks",toolBlockDict));

                jointComponents.Add(calibInfo.Name, jointComponent);
            }
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            if (handeyeComponents.ContainsKey(calibName))
            {
                handeyeComponents[calibName].Dispose();

                handeyeComponents.Remove(calibName);
            }
            else if (checkerboardComponents.ContainsKey(calibName))
            {
                checkerboardComponents[calibName].Dispose();

                checkerboardComponents.Remove(calibName);
            }
            else if (jointComponents.ContainsKey(calibName))
            {
                //jointComponents[calibName].Dispose();

                jointComponents.Remove(calibName);
            }
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            if (calibType == CalibrationType.Checkerboard)
            {
                ICheckerboardComponent checkerboardComponent = checkerboardComponents[oldCalibName];
                checkerboardComponents.Remove(oldCalibName);
                checkerboardComponents.Add(newCalibName, checkerboardComponent);

                checkerboardComponent.RenameCalibration(newCalibName);
            }
            else if (calibType == CalibrationType.HandEye)
            {
                IHandeyeComponent handeyeComponent = handeyeComponents[oldCalibName];
                handeyeComponents.Remove(oldCalibName);
                handeyeComponents.Add(newCalibName, handeyeComponent);

                handeyeComponent.RenameCalibration(newCalibName);
            }
        }

        private void TaskRepo_CalibQuote(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            IJointComponent cmp = jointComponents[calibName];

            IToolBlockComponent toolBlock = toolBlocks.GetComponent(taskName);

            cmp.AddQuote(taskName, acqImageName, toolBlock);
        }

        private void TaskRepo_CancelCalibQuote(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            IToolBlockComponent toolBlock = toolBlocks.GetComponent(taskName);

            toolBlock.RemoveTool($"{calibName}_{acqImageName}");
        }

        #endregion

        public void Init()
        {
            List<CalibrationInfoVO> calibInfos = calibRepo.GetCalibrations();

            foreach (CalibrationInfoVO calibInfo in calibInfos)
            {
                IToolBlockComponent toolBlock = toolBlocks.GetComponent(calibInfo.TaskName);

                if (calibInfo.CalibrationType == CalibrationType.Checkerboard)
                {
                    ICheckerboardComponent checkerboardComponent = AutoFacContainer.Resolve<ICheckerboardComponent>(
                        new NamedParameter("calibInfo", calibInfo),
                        new NamedParameter("toolBlock",toolBlock));
                    checkerboardComponents.Add(calibInfo.Name, checkerboardComponent);
                }
                else if (calibInfo.CalibrationType == CalibrationType.HandEye)
                {
                    IHandeyeComponent handeyeComponent = AutoFacContainer.Resolve<IHandeyeComponent>(
                        new NamedParameter("calibInfo", calibInfo),
                        new NamedParameter("toolBlock",toolBlock));

                    handeyeComponents.Add(calibInfo.Name, handeyeComponent);
                }
                else if (calibInfo.CalibrationType == CalibrationType.HandEyeSingle)
                {
                    IHandeyeSingleComponent handeyeSingleComponent = AutoFacContainer.Resolve<IHandeyeSingleComponent>(
                        new NamedParameter("calibInfo", calibInfo),
                        new NamedParameter("toolBlock",toolBlock));

                    handeyeSingleComponents.Add(calibInfo.Name, handeyeSingleComponent);
                }
                else if (calibInfo.CalibrationType == CalibrationType.Joint)
                {
                    Dictionary<string, IToolBlockComponent> toolBlockDict = new Dictionary<string, IToolBlockComponent>();

                    foreach (TaskAcqImageVO acq in calibInfo.JointInfo.Slaves)
                    {
                        if (!toolBlockDict.ContainsKey(acq.TaskName))
                            toolBlockDict.Add(acq.TaskName, toolBlocks.GetComponent(acq.TaskName));
                    }

                    IJointComponent jointComponent = AutoFacContainer.Resolve<IJointComponent>(
                        new NamedParameter("calibInfo", calibInfo),
                        new NamedParameter("toolBlocks",toolBlockDict));

                    jointComponents.Add(calibInfo.Name, jointComponent);
                }
            }
        }

        public IHandeyeComponent GetHandeyeComponent(string calibName)
        {
            lock (_sync)
            {
                if (handeyeComponents.ContainsKey(calibName))
                    return handeyeComponents[calibName];
                else
                {
                    log.Error($"未找到标定[{calibName}]对应的组件");
                    throw new Exception($"未找到标定[{calibName}]对应的组件");
                }
            }
        }

        public IHandeyeSingleComponent GetHandeyeSingleComponent(string calibName)
        {
            lock (_sync)
            {
                if (handeyeSingleComponents.ContainsKey(calibName))
                    return handeyeSingleComponents[calibName];
                else
                {
                    log.Error($"未找到标定[{calibName}]对应的组件");
                    throw new Exception($"未找到标定[{calibName}]对应的组件");
                }
            }
        }

        public ICheckerboardComponent GetCheckerboardComponent(string calibName)
        {
            lock (_sync)
            {
                if (checkerboardComponents.ContainsKey(calibName))
                    return checkerboardComponents[calibName];
                else
                {
                    log.Error($"未找到标定[{calibName}]对应的组件");
                    throw new Exception($"未找到标定[{calibName}]对应的组件");
                }
            }
        }

        public IJointComponent GetJointComponent(string calibName)
        {
            lock (_sync)
            {
                if (jointComponents.ContainsKey(calibName))
                    return jointComponents[calibName];
                else
                {
                    log.Error($"未找到标定[{calibName}]对应的组件");
                    throw new Exception($"未找到标定[{calibName}]对应的组件");
                }
            }
        }

        public void Save(string calibName)
        {
            lock (_sync)
            {
                IHandeyeComponent calibrationComponent = handeyeComponents[calibName];

                calibrationComponent.Save();
            }
        }

        public void SaveAll()
        {
            lock (_sync)
            {
                foreach (IHandeyeComponent calibrationComponent in handeyeComponents.Values)
                {
                    calibrationComponent.Save();
                }
            }
        }
    }
}
