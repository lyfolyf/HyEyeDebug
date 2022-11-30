using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface ICommandRepository
    {
        /// <summary>
        /// 发送指令格式
        /// <para>True: Key1 = Value1,Key2 = Value2,Key3 = Value3</para>
        /// <para>False: Value1,Value2,Value3...</para>
        /// </summary>
        bool SendCmdFormat { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        int DecimalPlaces { get; set; }

        /// <summary>
        /// 启用指令索引
        /// </summary>
        bool EnableCmdIndex { get; set; }

        /// <summary>
        /// 启用握手指令
        /// <para>在收到 A 指令或 AR 指令后，立刻回复一条指令</para>
        /// </summary>
        bool EnableHandCmd { get; set; }

        /// <summary>
        /// 启用指令索引对齐
        /// <para>仅在 EnableCmdIndex = true 时有效</para>
        /// <para>当为 true 时，R 指令和 A 指令的指令索引必须相等，否则 R 指令获取不到结果</para>
        /// <para>此属性为解决结果错位问题</para>
        /// </summary>
        bool IndexAlign { get; }

        List<ReceiveCommandInfoVO> GetTaskRecvCommandInfos();
        void ResetTaskRecvCommands(List<ReceiveCommandInfoVO> commandInfos);

        List<SendCommandInfoVO> GetTaskSendCommandInfos();
        void ResetTaskSendCommands(List<SendCommandInfoVO> commandInfos);

        List<ReceiveCommandInfoVO> GetCalibRecvCommandInfos();
        void ResetCalibRecvCommands(List<ReceiveCommandInfoVO> commandInfos);

        List<SendCommandInfoVO> GetCalibSendCommandInfos();
        void ResetCalibSendCommands(List<SendCommandInfoVO> commandInfos);

        SendCommandInfoVO GetCalibSendCommand(string calibName);

        void RenameTaskCmdHeader(string taskName, string cmdHeader);

        void RenameCalibCmdHeader(string calibName, string cmdHeader);

        void Save();
    }

    public class CommandRepository : ICommandRepository
    {
        readonly ITaskRepository taskRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        List<ReceiveCommandInfo> taskRecvCommands;
        List<SendCommandInfo> taskSendCommands;
        List<ReceiveCommandInfo> calibRecvCommands;
        List<SendCommandInfo> calibSendCommands;

        static readonly Type DoubleType = typeof(double);

        public CommandRepository(
            ITaskRepository taskRepo,
            IMapper mapper,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.mapper = mapper;
            this.log = log;

            taskRecvCommands = ApiConfig.CommandConfig.TaskReceiveCommands;
            taskSendCommands = ApiConfig.CommandConfig.TaskSendCommands;
            calibRecvCommands = ApiConfig.CommandConfig.CalibrationReceiveCommands;
            calibSendCommands = ApiConfig.CommandConfig.CalibrationSendCommands;

            // 指令关联的是拍照和标定，和任务其实无关
            this.taskRepo.TaskRename += TaskRepo_TaskRename;
            this.taskRepo.TaskMoveUp += TaskRepo_TaskMove;
            this.taskRepo.TaskMoveDown += TaskRepo_TaskMove;

            this.taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            this.taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            this.taskRepo.AcqImageRename += TaskRepo_AcqImageRename;
            this.taskRepo.AcqImageMoveUp += TaskRepo_AcqImageMove;
            this.taskRepo.AcqImageMoveDown += TaskRepo_AcqImageMove;

            this.taskRepo.CalibAdd += TaskRepo_CalibAdd;
            this.taskRepo.CalibDelete += TaskRepo_CalibDelete;
            this.taskRepo.CalibRename += TaskRepo_CalibRename;

            this.taskRepo.AfterSave += TaskRepo_TaskSave;

            LoadTaskCommands();

            LoadCalibCommands();
        }

        void LoadTaskCommands()
        {
            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            if (taskRecvCommands.Count == 0)
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
            taskRecvCommands.Clear();
            taskSendCommands.Clear();

            maxIndexT = 1;

            foreach (TaskInfoVO task in tasks.Where(t => t.CameraAcquireImage != null))
            {
                foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                {
                    TaskRepo_AcqImageAdd(task.Name, acqImage.Name);
                }
            }
        }

        // 按任务树排序
        void SortTaskCommands(List<TaskInfoVO> tasks)
        {
            taskRecvCommands = taskRecvCommands
                .Join(tasks, c => c.TaskName, t => t.Name, (c, t) => new { Task = t, Command = c })
                .OrderBy(a => a.Task.Order)
                .ThenBy(a => a.Command.Index)
                .Select(a => a.Command).ToList();

            ApiConfig.CommandConfig.TaskReceiveCommands = taskRecvCommands;

            taskSendCommands = taskSendCommands
                .Join(tasks, c => c.TaskName, t => t.Name, (c, t) => new { Task = t, Command = c })
                .OrderBy(a => a.Task.Order)
                .ThenBy(a => a.Command.Index)
                .Select(a => a.Command).ToList();

            ApiConfig.CommandConfig.TaskSendCommands = taskSendCommands;
        }

        void LoadCalibCommands()
        {
            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            if (calibRecvCommands.Count == 0)
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
            calibRecvCommands.Clear();
            calibSendCommands.Clear();

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

                    if (!string.IsNullOrEmpty(acqImage.HandEyeSingleName))
                        TaskRepo_CalibAdd(task.Name, acqImage.Name, CalibrationType.HandEyeSingle, acqImage.HandEyeSingleName);
                }
            }
        }

        void GetMaxIndexT()
        {
            var indexs = taskRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^T(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexT = indexs.Max() + 1;
            }
        }

        void GetMaxIndexCB()
        {
            var indexs = calibRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^CB(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexCB = indexs.Max() + 1;
            }
        }

        void GetMaxIndexHE()
        {
            var indexs = calibRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^HE(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexHE = indexs.Max() + 1;
            }
        }

        void GetMaxIndexJN()
        {
            var indexs = calibRecvCommands.Select(cmd => Regex.Match(cmd.CommandHeader, @"^JN(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
            {
                maxIndexJN = indexs.Max() + 1;
            }
        }

        void SortCalibCommands(List<TaskInfoVO> tasks)
        {
            List<ReceiveCommandInfo> cbRecvCmds = new List<ReceiveCommandInfo>(calibRecvCommands.Count);
            List<SendCommandInfo> cbSendCmds = new List<SendCommandInfo>(calibSendCommands.Count);

            List<ReceiveCommandInfo> heRecvCmds = new List<ReceiveCommandInfo>(calibRecvCommands.Count);
            List<SendCommandInfo> heSendCmds = new List<SendCommandInfo>(calibSendCommands.Count);

            foreach (TaskInfoVO task in tasks)
            {
                if (task.CameraAcquireImage == null) continue;

                foreach (AcquireImageInfoVO acq in task.CameraAcquireImage.AcquireImages)
                {
                    if (acq.CheckerboardName != null)
                    {
                        ReceiveCommandInfo recvCmd = calibRecvCommands
                            .FirstOrDefault(a => a.TaskName == task.Name && a.Name == acq.CheckerboardName);
                        cbRecvCmds.Add(recvCmd);

                        SendCommandInfo sendCmd = calibSendCommands
                            .FirstOrDefault(a => a.TaskName == task.Name && a.Name == acq.CheckerboardName);
                        cbSendCmds.Add(sendCmd);
                    }

                    if (acq.HandEyeNames != null && acq.HandEyeNames.Count > 0)
                    {
                        foreach (string hename in acq.HandEyeNames)
                        {
                            ReceiveCommandInfo recvCmd = calibRecvCommands
                                .FirstOrDefault(a => a.TaskName == task.Name && a.Name == hename);
                            heRecvCmds.Add(recvCmd);

                            SendCommandInfo sendCmd = calibSendCommands
                                .FirstOrDefault(a => a.TaskName == task.Name && a.Name == hename);
                            heSendCmds.Add(sendCmd);
                        }
                    }

                    if (acq.HandEyeSingleName != null)
                    {
                        ReceiveCommandInfo recvCmd = calibRecvCommands
                            .FirstOrDefault(a => a.TaskName == task.Name && a.Name == acq.HandEyeSingleName);
                        heRecvCmds.Add(recvCmd);

                        SendCommandInfo sendCmd = calibSendCommands
                            .FirstOrDefault(a => a.TaskName == task.Name && a.Name == acq.HandEyeSingleName);
                        heSendCmds.Add(sendCmd);
                    }
                }
            }

            calibRecvCommands.Clear();
            calibSendCommands.Clear();

            calibRecvCommands.AddRange(cbRecvCmds);
            calibRecvCommands.AddRange(heRecvCmds);

            calibSendCommands.AddRange(cbSendCmds);
            calibSendCommands.AddRange(heSendCmds);

            //calibrationRecvCommands = calibrationRecvCommands.Join(tasks, c => c.TaskName, t => t.Name, (c, t) => (c, t))
            //    .Where(ct => ct.t.CameraAcquireImage != null)
            //    .Select(ct => (ct.c, ct.t, ct.t.CameraAcquireImage.AcquireImages.First(a => a.CheckerboardName == ct.c.Name || a.HandEyeNames.Contains(ct.c.Name))))
            //    .Join(taskRecvCommands, cta => cta.Item3.Name, r => r.Name, (cta, r) => (cta, r))
            //    .OrderBy(a => a.cta.t.Order)
            //    .ThenBy(a => a.r.Index)
            //    .Select(a => a.cta.c).ToList();

            //calibrationSendCommands = calibrationSendCommands.Join(tasks, c => c.TaskName, t => t.Name, (c, t) => (c, t))
            //    .Select(ct => (ct.c, ct.t, ct.t.CameraAcquireImage.AcquireImages.First(a => a.CheckerboardName == ct.c.Name || a.HandEyeNames.Contains(ct.c.Name))))
            //    .Join(taskRecvCommands, cta => cta.Item3.Name, r => r.Name, (cta, r) => (cta, r))
            //    .OrderBy(a => a.cta.t.Order)
            //    .ThenBy(a => a.r.Index)
            //    .Select(a => a.cta.c).ToList();
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

        int maxIndexT = 1;
        int maxIndexCB = 1;
        int maxIndexHE = 1;
        int maxIndexJN = 1;

        bool changedToken = false;
        bool sortedToken = true;

        #region Task 事件

        private void TaskRepo_TaskRename(string oldTaskName, string newTaskName)
        {
            foreach (ReceiveCommandInfo cmd in taskRecvCommands)
            {
                if (cmd.TaskName == oldTaskName)
                    cmd.TaskName = newTaskName;
            }

            foreach (SendCommandInfo cmd in taskSendCommands)
            {
                if (cmd.TaskName == oldTaskName)
                    cmd.TaskName = newTaskName;
            }

            foreach (ReceiveCommandInfo cmd in calibRecvCommands)
            {
                if (cmd.TaskName == oldTaskName)
                    cmd.TaskName = newTaskName;
            }

            foreach (SendCommandInfo cmd in calibSendCommands)
            {
                if (cmd.TaskName == oldTaskName)
                    cmd.TaskName = newTaskName;
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
            ReceiveCommandInfo recvCmd = new ReceiveCommandInfo
            {
                TaskName = taskName,
                Name = acqImageName,
                Fields = new List<CommandFieldInfo>()
            };

            ReceiveCommandInfo first = taskRecvCommands.FirstOrDefault(c => c.TaskName == taskName);
            if (first == null)
            {
                recvCmd.CommandHeader = $"T{maxIndexT++}";
                recvCmd.Index = 1;
            }
            else
            {
                recvCmd.CommandHeader = first.CommandHeader;
                recvCmd.Index = taskRecvCommands.Where(c => c.TaskName == taskName).Max(c => c.Index) + 1;
            }
            taskRecvCommands.Add(recvCmd);

            SendCommandInfo sendCmd = new SendCommandInfo
            {
                TaskName = recvCmd.TaskName,
                Name = recvCmd.Name,
                CommandHeader = recvCmd.CommandHeader,
                Index = recvCmd.Index
            };
            taskSendCommands.Add(sendCmd);

            changedToken = true;
            sortedToken = false;
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            taskRecvCommands.Remove(c => c.TaskName == taskName && c.Name == acqImageName);
            taskSendCommands.Remove(c => c.TaskName == taskName && c.Name == acqImageName);

            changedToken = true;
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            ReceiveCommandInfo recvCmd = taskRecvCommands.FirstOrDefault(a => a.TaskName == taskName && a.Name == oldAcqImageName);
            if (recvCmd != null)
            {
                recvCmd.Name = newAcqImageName;
            }

            SendCommandInfo sendCmd = taskSendCommands.FirstOrDefault(a => a.TaskName == taskName && a.Name == oldAcqImageName);
            if (sendCmd != null)
            {
                sendCmd.Name = newAcqImageName;
            }

            changedToken = true;
        }

        private void TaskRepo_AcqImageMove(string taskName, string acqImageName1, string acqImageName2)
        {
            ReceiveCommandInfo recv1 = taskRecvCommands.FirstOrDefault(a => a.Name == acqImageName1);
            ReceiveCommandInfo recv2 = taskRecvCommands.FirstOrDefault(a => a.Name == acqImageName2);
            int i = recv1.Index;
            recv1.Index = recv2.Index;
            recv2.Index = i;

            SendCommandInfo send1 = taskSendCommands.FirstOrDefault(a => a.Name == acqImageName1);
            SendCommandInfo send2 = taskSendCommands.FirstOrDefault(a => a.Name == acqImageName2);
            i = send1.Index;
            send1.Index = send2.Index;
            send2.Index = i;

            changedToken = true;
            sortedToken = false;
        }

        private string ToCommandHeader(CalibrationType calibType)
        {
            if (calibType == CalibrationType.Checkerboard)
                return $"CB{maxIndexCB++}";
            else if (calibType == CalibrationType.HandEye || calibType == CalibrationType.HandEyeSingle)
                return $"HE{maxIndexHE++}";
            else
                return $"JN{maxIndexJN++}";
        }

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            ReceiveCommandInfo recvCmd = new ReceiveCommandInfo
            {
                TaskName = taskName,
                Name = calibName,
                Index = 1,
                CalibrationType = calibType,
                CommandHeader = ToCommandHeader(calibType),
                Fields = new List<CommandFieldInfo>()
            };
            calibRecvCommands.Add(recvCmd);

            // 手眼标定必须有 X, Y 参数，否则 Runner 中是会报错的
            if (calibType == CalibrationType.HandEye)
            {
                recvCmd.Fields.Add(new CommandFieldInfo { Name = "X", DataType = DoubleType, Use = CommandFieldUse.None });
                recvCmd.Fields.Add(new CommandFieldInfo { Name = "Y", DataType = DoubleType, Use = CommandFieldUse.None });
                recvCmd.Fields.Add(new CommandFieldInfo { Name = "A", DataType = DoubleType, Use = CommandFieldUse.None });
            }
            else if (calibType == CalibrationType.HandEyeSingle)
            {
                for (int i = 1; i <= 9; i++)
                {
                    recvCmd.Fields.Add(new CommandFieldInfo { Name = "X" + i.ToString(), DataType = DoubleType, Use = CommandFieldUse.None });
                    recvCmd.Fields.Add(new CommandFieldInfo { Name = "Y" + i.ToString(), DataType = DoubleType, Use = CommandFieldUse.None });
                }
            }

            SendCommandInfo sendCmd = new SendCommandInfo
            {
                TaskName = recvCmd.TaskName,
                Name = recvCmd.Name,
                Index = recvCmd.Index,
                CommandHeader = recvCmd.CommandHeader
            };
            calibSendCommands.Add(sendCmd);

            changedToken = true;
            sortedToken = false;
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            calibRecvCommands.RemoveAll(a => a.Name == calibName);
            calibSendCommands.RemoveAll(a => a.Name == calibName);

            changedToken = true;
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            ReceiveCommandInfo recvCmd = calibRecvCommands.FirstOrDefault(a => a.TaskName == taskName && a.Name == oldCalibName);
            if (recvCmd != null)
            {
                recvCmd.Name = newCalibName;
            }

            SendCommandInfo sendCmd = calibSendCommands.FirstOrDefault(a => a.TaskName == taskName && a.Name == oldCalibName);
            if (sendCmd != null)
            {
                sendCmd.Name = newCalibName;
            }

            changedToken = true;
        }

        private void TaskRepo_TaskSave()
        {
            Save();
        }

        #endregion

        public bool SendCmdFormat
        {
            get { return ApiConfig.CommandConfig.SendCmdFormat; }
            set
            {
                if (ApiConfig.CommandConfig.SendCmdFormat != value)
                {
                    ApiConfig.CommandConfig.SendCmdFormat = value;
                    changedToken = true;
                }
            }
        }

        public int DecimalPlaces
        {
            get { return ApiConfig.CommandConfig.DecimalPlaces; }
            set
            {
                if (ApiConfig.CommandConfig.DecimalPlaces != value)
                {
                    ApiConfig.CommandConfig.DecimalPlaces = value;
                    changedToken = true;
                }
            }
        }

        public bool EnableHandCmd
        {
            get { return ApiConfig.CommandConfig.EnableHandCmd; }
            set
            {
                if (ApiConfig.CommandConfig.EnableHandCmd != value)
                {
                    ApiConfig.CommandConfig.EnableHandCmd = value;
                    changedToken = true;
                }
            }
        }

        public bool EnableCmdIndex
        {
            get { return ApiConfig.CommandConfig.EnableCmdIndex; }
            set
            {
                if (ApiConfig.CommandConfig.EnableCmdIndex != value)
                {
                    ApiConfig.CommandConfig.EnableCmdIndex = value;
                    changedToken = true;
                }
            }
        }

        public bool IndexAlign
        {
            get { return ApiConfig.CommandConfig.EnableCmdIndex; }
            //set
            //{
            //    if (ApiConfig.CommandConfig.IndexAlign != value)
            //    {
            //        ApiConfig.CommandConfig.IndexAlign = value;
            //        changedToken = true;
            //    }
            //}
        }

        #region

        public List<ReceiveCommandInfoVO> GetTaskRecvCommandInfos()
        {
            Sort();

            return mapper.Map<List<ReceiveCommandInfoVO>>(taskRecvCommands);
        }

        public List<SendCommandInfoVO> GetTaskSendCommandInfos()
        {
            Sort();

            return mapper.Map<List<SendCommandInfoVO>>(taskSendCommands);
        }

        public List<ReceiveCommandInfoVO> GetCalibRecvCommandInfos()
        {
            Sort();

            return mapper.Map<List<ReceiveCommandInfoVO>>(calibRecvCommands).ToList();
        }

        public List<SendCommandInfoVO> GetCalibSendCommandInfos()
        {
            Sort();

            return mapper.Map<List<SendCommandInfoVO>>(calibSendCommands).ToList();
        }

        public void ResetTaskRecvCommands(List<ReceiveCommandInfoVO> commandInfos)
        {
            ApiConfig.CommandConfig.TaskReceiveCommands = mapper.Map<List<ReceiveCommandInfo>>(commandInfos);
            taskRecvCommands = ApiConfig.CommandConfig.TaskReceiveCommands;

            changedToken = true;
        }

        public void ResetTaskSendCommands(List<SendCommandInfoVO> commandInfos)
        {
            ApiConfig.CommandConfig.TaskSendCommands = mapper.Map<List<SendCommandInfo>>(commandInfos);
            taskSendCommands = ApiConfig.CommandConfig.TaskSendCommands;

            changedToken = true;
        }

        public void ResetCalibRecvCommands(List<ReceiveCommandInfoVO> commandInfos)
        {
            ApiConfig.CommandConfig.CalibrationReceiveCommands = mapper.Map<List<ReceiveCommandInfo>>(commandInfos);
            calibRecvCommands = ApiConfig.CommandConfig.CalibrationReceiveCommands;

            changedToken = true;
        }

        public void ResetCalibSendCommands(List<SendCommandInfoVO> commandInfos)
        {
            ApiConfig.CommandConfig.CalibrationSendCommands = mapper.Map<List<SendCommandInfo>>(commandInfos);
            calibSendCommands = ApiConfig.CommandConfig.CalibrationSendCommands;

            changedToken = true;
        }

        public SendCommandInfoVO GetCalibSendCommand(string calibName)
        {
            SendCommandInfo sendCommand = calibSendCommands.FirstOrDefault(a => a.Name == calibName);
            return mapper.Map<SendCommandInfoVO>(sendCommand);
        }

        #endregion

        public void RenameTaskCmdHeader(string taskName, string cmdHeader)
        {
            string oldHeader = taskRecvCommands.FirstOrDefault(a=>a.TaskName == taskName)?.CommandHeader;
            if (oldHeader == null)
            {
                log.Error(new ApiLogMessage(taskName, "指令头", A_Rename, R_Fail, "未找到任务"));
                throw new ApiException("重命指令头失败，未找到任务");
            }

            if (oldHeader == cmdHeader) return;

            if (taskRecvCommands.Any(a => a.CommandHeader == cmdHeader))
            {
                log.Error(new ApiLogMessage(taskName, "指令头", A_Rename, R_Fail, "指令头已存在"));
                throw new ApiException("重命指令头失败，指令头已存在");
            }

            Match match = Regex.Match(cmdHeader, @"^T(\d+)$");
            if (!match.Success)
            {
                log.Error(new ApiLogMessage(taskName, "指令头", A_Rename, R_Fail, "指令头无效"));
                throw new ApiException("重命指令头失败，指令头无效");
            }

            taskRecvCommands.ForEach(cmd =>
            {
                if (cmd.TaskName == taskName)
                    cmd.CommandHeader = cmdHeader;
            });

            taskSendCommands.ForEach(cmd =>
            {
                if (cmd.TaskName == taskName)
                    cmd.CommandHeader = cmdHeader;
            });

            int idx = int.Parse(match.Groups[1].Value);
            if (idx >= maxIndexT)
                maxIndexT = idx + 1;
        }

        public void RenameCalibCmdHeader(string calibName, string cmdHeader)
        {
            ReceiveCommandInfo first = calibRecvCommands.FirstOrDefault(a=>a.Name == calibName);
            if (first == null)
            {
                log.Error(new ApiLogMessage(calibName, "指令头", A_Rename, R_Fail, "未找到标定"));
                throw new ApiException("重命指令头失败，未找到标定");
            }

            if (first.CommandHeader == cmdHeader) return;

            if (calibRecvCommands.Any(a => a.CommandHeader == cmdHeader))
            {
                log.Error(new ApiLogMessage(calibName, "指令头", A_Rename, R_Fail, "指令头已存在"));
                throw new ApiException("重命指令头失败，指令头已存在");
            }

            string s = first.CalibrationType == CalibrationType.Checkerboard ? "CB" : "HE";

            Match match = Regex.Match(cmdHeader, $@"^{s}(\d+)$");
            if (!match.Success)
            {
                log.Error(new ApiLogMessage(calibName, "指令头", A_Rename, R_Fail, "指令头无效"));
                throw new ApiException("重命指令头失败，指令头无效");
            }

            calibRecvCommands.ForEach(cmd =>
            {
                if (cmd.Name == calibName)
                    cmd.CommandHeader = cmdHeader;
            });

            calibSendCommands.ForEach(cmd =>
            {
                if (cmd.Name == calibName)
                    cmd.CommandHeader = cmdHeader;
            });

            int idx = int.Parse(match.Groups[1].Value);
            if (first.CalibrationType == CalibrationType.Checkerboard)
            {
                if (idx >= maxIndexCB)
                    maxIndexCB = idx + 1;
            }
            else if (first.CalibrationType == CalibrationType.HandEye)
            {
                if (idx >= maxIndexHE)
                    maxIndexHE = idx + 1;
            }
        }

        public void Save()
        {
            if (changedToken)
            {
                Sort();

                ApiConfig.Save(ApiConfig.CommandConfig);

                changedToken = false;

                log.Info(new ApiLogMessage("指令设置", null, A_Save, R_Success));
            }
        }
    }
}
