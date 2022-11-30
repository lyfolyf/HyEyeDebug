using CameraSDK.Models;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    class TaskRunner : ICheckable
    {
        public event GetSrcImageHandle GetSrcImage;

        public event CompletedSingleAcqImageHandle CompletedSingleAcqImage;

        //add by LuoDian @ 20220203 把保存源图和保存结果图分开，源图的保存在拍照完成的时候就可以做
        public event SaveSrcImageHandle SaveSrcImage;

        public event EventHandler<SendCmdsEventArgs> SendCommands;

        string taskName;

        public TaskInfoVO TaskInfo { get; private set; }

        readonly ITaskRepository taskRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ICommandRepository commandRepo;
        readonly IRunTimeRepository runTimeRepo;
        readonly ISimulationRepository simulationRepo;

        readonly IToolBlockComponent toolBlock;
        readonly IScriptService scriptService;
        readonly IAcquireImage acqImage;
        readonly IGLog log;
        // Added by louis on Mar. 08 2022 用于保存缺陷图模式下丢弃不需要的图像
        const string IMAGE_DISCARD = "Discard";
        const string IMAGE_DEFAULT_FOLDER = @"D:\Images";
        // const string IMAGE_DEFAULT_BAK_FOLDER = @"D:\ZHide";

        const string LOGO = "Logo";
        const string CONER = "Corner";
        const string OTHER = "Other";
        const string SEPARATOR = "_";
        Dictionary<string, string> timeSet = new Dictionary<string, string>();

        string saveImageFolder = IMAGE_DEFAULT_FOLDER;
        // string bakImageFolder = IMAGE_DEFAULT_BAK_FOLDER;

        //add by LuoDian @ 20211214 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        //add by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，后面一条新的指令过来的时候，有可能前面的指令还没有执行完成，由于是多线程程在运行采图跟ToolBlock，所以需要在采图和运行ToolBlock的地方添加一个线程锁，防止图像错乱
        private readonly object getImageLockObj = new object();
        private readonly object runToolBlockLockObj = new object();
        //add by LuoDian @ 20220113 添加一个字典，用于存储每张图像在显示、存图、运行ToolBlock这几个线程的完成状态
        private Dictionary<DateTime, bool[]> dicImageFinishStatus = new Dictionary<DateTime, bool[]>();

        public TaskRunner(
            string taskName,
            ITaskRepository taskRepo,
            IOpticsRepository opticsRepo,
            ICommandRepository commandRepo,
            IRunTimeRepository runTimeRepo,
            ISimulationRepository simulationRepo,
            IAcquireImage acqImage,
            IToolBlockComponent toolBlock,
            IScriptService scriptService,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            this.taskName = taskName;

            this.taskRepo = taskRepo;
            this.opticsRepo = opticsRepo;
            this.commandRepo = commandRepo;
            this.runTimeRepo = runTimeRepo;
            this.simulationRepo = simulationRepo;
            this.acqImage = acqImage;
            this.toolBlock = toolBlock;
            this.scriptService = scriptService;
            this.log = log;

            //add by LuoDian @ 20211214 用于子料号的快速切换
            this.materialRepo = materialRepo;

            e = new SendCommandEventClass(this, log);
            acqImage.Confirm += e.Confirm;
        }

        ~TaskRunner()
        {
            runningToken = false;
            //add by LuoDian @ 20220121 系统退出的时候，销毁获取图像数据的队列中遗留未处理的图像数据
            ResetQueue();
            //add by LuoDian @ 20220121 这个是用来传递任务执行结果输出的，不能放在ResetQueue()中一起销毁，只在系统退出的时候销毁
            dicOutputValQueue?.Clear();
        }

        SendCommandEventClass e;

        public bool Check()
        {
            return acqImage.CheckCamera(taskName);
        }

        public void RenameTaskName(string newTaskName)
        {
            taskName = newTaskName;

            if (TaskInfo != null)
                TaskInfo.Name = newTaskName;
        }

        bool runningToken = false;

        public void Start(bool initScheduler = false)
        {

            try
            {
                TaskInfo = taskRepo.GetTaskByName(taskName);

                runningToken = true;

                //add by LuoDian @ 20220121 把触发与获取图像数据分开后，图像后处理的流程放到队列+线程中处理，这里在拍照之前先提前启动线程
                StartImageProcess();

                acqImage.Start(TaskInfo, initScheduler);

                m_outputCache = new OutputCache(TaskInfo.CameraAcquireImage.AcquireImages.Count, commandRepo.IndexAlign, log);

                //add by LuoDian @ 20210826 记录产品名称，暂时把任务名称作为产品名称
                API.GlobalParams.ProductName = taskName;
                //add by LuoDian @ 20210830 记录开始运行的时间
                API.GlobalParams.BatchRunStartTime = DateTime.Now;

                toolBlock.StartSerial();
            }
            catch (Exception ex)
            {
                //update by LuoDian @ 20220117 初始化失败后，不应该再启动服务
                //log.Error(new TaskServerLogMessage(taskName, "", A_Start, R_Fail, ex.Message));
                runningToken = false;
                throw new Exception($"任务 {taskName} 启动失败！原因：{ex.Message}");
            }

            state = State.Running;

            errorOutput = null;
        }

        public void Stop()
        {
            if (runningToken)
            {
                if (TaskInfo == null)
                    return;

                try
                {
                    toolBlock.StopSerial();

                    acqImage.Close();
                }
                catch (Exception ex)
                {
                    log.Error(new TaskServerLogMessage(taskName, "", A_Stop, R_Fail, ex.Message));
                }

                errorOutput = null;

                runningToken = false;

                state = State.Idle;
            }
        }

        OutputCache m_outputCache;

        class OutputCache
        {
            // 用 index 表示拍照索引，省掉用 Dictionary
            readonly OutputList[] cache_List;

            class OutputList
            {
                public long CmdIndex;

                public LinkedDictionary<string, object> Output;
            }

            // key：cmdIndex
            readonly Dictionary<long, LinkedDictionary<string, object>[]> cache_Dict;

            readonly Queue<long> queue;

            readonly int acqCount;
            readonly bool indexAlign;

            readonly IGLog log;

            readonly object _sync = new object();

            public OutputCache(int acqCount, bool indexAlign, IGLog log)
            {
                this.acqCount = acqCount;
                this.indexAlign = indexAlign;
                this.log = log;

                if (indexAlign)
                {
                    cache_Dict = new Dictionary<long, LinkedDictionary<string, object>[]>(acqCount * 4);
                    queue = new Queue<long>(acqCount * 2);
                }
                else
                {
                    cache_List = new OutputList[acqCount];
                    for (int i = 0; i < acqCount; i++)
                    {
                        cache_List[i] = new OutputList();
                    }
                }
            }

            public void SetOutput(long cmdIndex, int acqIndex, LinkedDictionary<string, object> output)
            {
                lock (_sync)
                {
                    acqIndex--;

                    if (indexAlign)
                    {
                        LinkedDictionary<string, object>[] k;
                        if (cache_Dict.ContainsKey(cmdIndex))
                        {
                            k = cache_Dict[cmdIndex];
                        }
                        else
                        {
                            k = new LinkedDictionary<string, object>[acqCount];
                            cache_Dict.Add(cmdIndex, k);

                            queue.Enqueue(cmdIndex);
                            if (queue.Count == acqCount * 2)
                            {
                                for (int i = 0, h = acqCount; i < h; i++)
                                {
                                    long cmdIdx = queue.Dequeue();
                                    cache_Dict.Remove(cmdIdx);
                                }
                            }
                        }
                        k[acqIndex] = output;
                    }
                    else
                    {
                        if (cache_List[acqIndex].CmdIndex == cmdIndex && cache_List[acqIndex].Output == null)
                        {
                            return;
                        }

                        cache_List[acqIndex].CmdIndex = cmdIndex;
                        cache_List[acqIndex].Output = output;
                    }
                }
            }

            // 最初的做法，核心问题就是不让超时后产生的结果数据污染原始数据，因为 Get 的时候是无法判断是否是脏数据的
            // 那么核心就变成了超时后的数据不让其 Set，从而拒绝脏数据

            public void SetTimeout(long cmdIndex, int acqIndex)
            {
                lock (_sync)
                {
                    //SZ cache_List => NullReferenceException 
                    try
                    {
                        if (cache_List != null)
                        {
                            acqIndex--;

                            cache_List[acqIndex].CmdIndex = cmdIndex;
                            cache_List[acqIndex].Output = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(new TaskServerLogMessage("设置超时", $"指令{cmdIndex}", $"取像序号{acqIndex}", R_Fail, ex.Message));
                    }
                }
            }

            public LinkedDictionary<string, object> GetOutput(long cmdIndex, int acqIndex)
            {
                lock (_sync)
                {
                    acqIndex--;

                    if (indexAlign)
                    {
                        if (cache_Dict.ContainsKey(cmdIndex))
                        {
                            LinkedDictionary<string, object> result = cache_Dict[cmdIndex][acqIndex];
                            cache_Dict[cmdIndex][acqIndex] = null;
                            return result;
                        }
                        else
                            return null;
                    }
                    else
                    {
                        if (cache_List[acqIndex].CmdIndex == 0 || cache_List[acqIndex].Output == null) return null;

                        LinkedDictionary<string, object> result = cache_List[acqIndex].Output;
                        cache_List[acqIndex].CmdIndex = 0;
                        cache_List[acqIndex].Output = null;
                        return result;
                    }
                }
            }
        }

        // 理论上来说，每次拍照的 Output 的 Key 是可以不同的，因为脚本里可以做修改
        // 先不管
        LinkedDictionary<string, object> errorOutput = null;

        void createErrorOutput(LinkedDictionary<string, object> outputs)
        {
            if (errorOutput == null)
            {
                errorOutput = new LinkedDictionary<string, object>();

                foreach (string name in outputs.Keys)
                {
                    errorOutput.Add(name, "999");
                }
            }
        }

        LinkedDictionary<string, object> getErrorOutput(int acqIndex)
        {
            if (errorOutput == null)
            {
                LinkedDictionary<string, object> tbOutputs = toolBlock.GetErrorOutputs();

                if (tbOutputs == null)
                {
                    return null;
                }

                try
                {
                    errorOutput = scriptService.ModifyOutputs(TaskInfo.Name, acqIndex, tbOutputs);
                }
                catch (Exception ex)
                {
                    log.Error(new TaskServerLogMessage("获取ErrorOutput", "", $"取像序号{acqIndex}", R_Fail, ex.Message));
                }
            }

            return errorOutput;
        }

        #region 手动运行

        long ManualCmdIndex = 10000001;

        /// <summary>
        /// 运行任务
        /// </summary>
        public void RunTask()
        {
            log.Info(new TaskServerLogMessage(taskName, null, A_Run, R_Start));

            acqImage.BeginAcqImage(0, TaskInfo.CameraAcquireImage.AcquireImages.Count);

            foreach (AcquireImageInfoVO acqImage in TaskInfo.CameraAcquireImage.AcquireImages)
            {
                RunAcqImage2(ManualCmdIndex, acqImage.Name);
            }
            ManualCmdIndex++;

            log.Info(new TaskServerLogMessage(taskName, null, A_Run, R_End));
        }

        /// <summary>
        /// 运行拍照
        /// </summary>
        public bool RunAcqImage(string acqImageName)
        {
            // acqImage.BeginAcqImage(0, 1);

            return RunAcqImage2(ManualCmdIndex++, acqImageName);
        }

        private bool RunAcqImage2(long cmdIndex, string acqImageName)
        {
            (TimeSpan ts, bool success) = FuncWatch.ElapsedTime(() =>
            {
                log.Info(new TaskServerLogMessage(taskName, acqImageName, A_Run, R_Start));

                int acqImageIndex = taskRepo.GetAcqImageIndex(taskName, acqImageName);

                //add by LuoDian @ 20211118 为了适应多个相机共用同一个光源的情况，在这里单独控制光源
                OpticsInfoVO opticsInfo = opticsRepo.GetOptics(taskName, acqImageName, null, materialRepo.CurSubName);

                if (!simulationRepo.Enabled)
                {
                    int errCode = acqImage.Trigger(opticsInfo);
                    if (errCode != ErrorCodeConst.OK)
                    {
                        log?.Error($"任务[{taskName}]的拍照[{acqImageName}] 触发失败！错误码：{errCode}");
                        return false;
                    }
                    log?.Info($"任务[{taskName}]的拍照[{acqImageName}] 触发成功！");
                }

                (int errorCode, HyImageInfo hyImage, HyImageInfo hySaveImage, HyImageInfo toolBlockImage) = GetImage(cmdIndex, acqImageName, acqImageIndex, null, materialRepo.CurSubName);

                if (errorCode != ErrorCodeConst.OK)
                {
                    log?.Error($"任务[{taskName}]的拍照[{acqImageName}] 获取图像数据失败！错误码：{errorCode}");
                    return false;
                }
                log?.Info($"任务[{taskName}]的拍照[{acqImageName}] 获取图像数据成功！");

                //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
                toolBlock.RunSerial(hyImage, null, materialRepo.CurSubName, callback);

                #region add by LuoDian @ 20220114 显示的挪到这里
                //delete by LuoDian @ 20211119 已经在相机基类的回调函数那里进行了拷贝，这里不再拷贝
                //HyImageInfo copy = hyImage.Clone();

                // 这里绝对不能用线程
                GetSrcImage?.Invoke(hyImage);//屏蔽显示
                #endregion

                //add by LuoDian @ 20220114 释放图像
                //hyImage.Dispose();
                //hySaveImage.Dispose();
                //toolBlockImage.Dispose();

                return true;
            });

            log.Info(new TaskServerLogMessage(taskName, acqImageName, A_Run, R_End, null, ts.TotalMilliseconds));

            return success;

            void callback(string m_acqImageName, int m_acqImageIndex, int errorCode, LinkedDictionary<string, object> tbOutputs)
            {
                LinkedDictionary<string, object> outputs = null;
                try
                {
                    outputs = scriptService.ModifyOutputs(TaskInfo.Name, m_acqImageIndex, tbOutputs);

                    createErrorOutput(outputs);

                    log.Info(new TaskServerLogMessage(taskName, acqImageName, A_RunScript, R_Success, $"\"{string.Join(", ", outputs.Select(kv => $"{kv.Key} = {kv.Value}"))}\""));
                }
                catch (Exception e)
                {
                    log.Error(new TaskServerLogMessage(taskName, acqImageName, A_RunScript, R_Fail, e.Message));
                }

                if (outputs != null)
                {
                    Task.Run(() =>
                    {
                        CompletedSingleAcqImage?.Invoke(taskName, m_acqImageName, m_acqImageIndex, outputs);
                    });
                }
            }
        }

        /// <summary>
        /// add by LuoDian @ 20210824 用于离线批量跑任务
        /// </summary>
        /// <param name="cmdIndex"></param>
        /// <param name="acqImageName">当前拍照名称</param>
        /// <param name="productName">产品名称</param>
        /// <returns></returns>
        private bool RunAcqImageForBatchRunOffline(long cmdIndex, string acqImageName, string productName)
        {
            (TimeSpan ts, bool success) = FuncWatch.ElapsedTime(() =>
            {
                log.Info(new TaskServerLogMessage(taskName, $"{productName} 读图", A_Run, R_Start));

                int acqImageIndex = taskRepo.GetAcqImageIndex(taskName, acqImageName);

                (int errorCode, HyImageInfo hyImage) = GetImageForBatchRunOffline(cmdIndex, acqImageName, productName, acqImageIndex, null);

                if (errorCode != ErrorCodeConst.OK)
                {
                    return false;
                }

                toolBlock.RunSerial(hyImage, null, materialRepo.CurSubName, callback);

                return true;
            });

            log.Info(new TaskServerLogMessage(taskName, $"{productName} 读图", A_Run, R_End, null, ts.TotalMilliseconds));

            return success;

            void callback(string m_acqImageName, int m_acqImageIndex, int errorCode, LinkedDictionary<string, object> tbOutputs)
            {
                LinkedDictionary<string, object> outputs = null;
                try
                {
                    outputs = scriptService.ModifyOutputs(TaskInfo.Name, m_acqImageIndex, tbOutputs);

                    createErrorOutput(outputs);

                    log.Info(new TaskServerLogMessage(taskName, productName, A_RunScript, R_Success, $"\"{string.Join(", ", outputs.Select(kv => $"{kv.Key} = {kv.Value}"))}\""));
                }
                catch (Exception e)
                {
                    log.Error(new TaskServerLogMessage(taskName, productName, A_RunScript, R_Fail, e.Message));
                }
            }
        }

        /// <summary>
        /// add by LuoDian @ 20210824 用于离线批量跑任务
        /// </summary>
        public void BatchRunOffline()
        {
            log.Info(new TaskServerLogMessage(taskName, "离线批量读图", A_Run, R_Start));
            API.GlobalParams.BatchRunStartTime = DateTime.Now;

            string folderPath = $@"{simulationRepo.GetLocalImageDirectory()}\{TaskInfo.Name}\Batch Run Offline";
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            if (directoryInfos == null || directoryInfos.Length < 1)
            {
                log.Warn($"在指定的文件夹 {folderPath} 中未找到任何子文件夹！");
                log.Info(new TaskServerLogMessage(taskName, "离线批量读图", A_Run, R_End));
                return;
            }

            foreach (DirectoryInfo info in directoryInfos)
            {
                API.GlobalParams.ProductName = info.Name;
                string[] bmpFiles = Directory.GetFiles($@"{folderPath}\{info.Name}", "*.bmp");
                if (bmpFiles == null || bmpFiles.Length != TaskInfo.CameraAcquireImage.AcquireImages.Count)
                {
                    log.Warn($@"在指定的文件夹 {folderPath}\{info.Name} 中的BMP图像数量不合法！合法的数量为：{TaskInfo.CameraAcquireImage.AcquireImages.Count}");
                    log.Info(new TaskServerLogMessage(taskName, "离线批量读图", A_Run, R_End));
                    continue;
                }

                acqImage.BeginAcqImage(0, TaskInfo.CameraAcquireImage.AcquireImages.Count);

                foreach (AcquireImageInfoVO acqImage in TaskInfo.CameraAcquireImage.AcquireImages)
                {
                    RunAcqImageForBatchRunOffline(ManualCmdIndex, acqImage.Name, info.Name);
                }
                ManualCmdIndex++;
            }

            log.Info(new TaskServerLogMessage(taskName, "离线批量读图", A_Run, R_End));
        }

        #endregion

        #region 指令运行

        State state = State.Idle;
        // 用于收到暂停指令时，暂停线程
        ManualResetEventSlim slim = new ManualResetEventSlim(false);

        class SendCommandEventClass
        {
            TaskRunner runner;

            readonly IGLog log;

            public long CmdID;
            public List<TaskCommand> Cmds;

            public SendCommandEventClass(TaskRunner runner, IGLog log)
            {
                this.runner = runner;
                this.log = log;
            }

            public void Confirm(object sender, EventArgs e)
            {
                IReplyCommand[] replyCmds = Cmds.Select(c => new SendCommand
                {
                    CommandHeader = c.CommandHeader,
                    AcqOrCalibIndex = c.AcqOrCalibIndex,
                    Type = c.Type == CommandType.A ? CommandType.AC : CommandType.ARC,
                    ErrorCode = 0
                }).ToArray();

                runner.SendCommands?.Invoke(this, new SendCmdsEventArgs(CmdID, replyCmds));
            }
        }

        static Type _CommandType = typeof(CommandType);

        /// <summary>
        /// 执行 指令
        ///  update by LuoDian @ 20211118 为了适应多个相机共用同一个光源的情况，添加一个线程计数，用于同步
        /// </summary>
        /// <param name="cmds">必须是同一任务的指令</param>
        public List<IReplyCommand> RunCommands(long cmdId, List<TaskCommand> cmds, Dictionary<(string, int), CountdownEvent> dicCountdownEvent,
            Dictionary<(string, int), CountdownEvent> dicOpenLightCountdownEvent)
        {
            if (state == State.Suspend)
            {
                log.Error("上位机发送指令错误，在暂停状态仍发送任务指令。");
                return cmds.Select(cmd => (IReplyCommand)new SendCommand
                {
                    CommandHeader = cmd.CommandHeader,
                    AcqOrCalibIndex = cmd.AcqOrCalibIndex,
                    Type = cmd.Type,
                    ErrorCode = ErrorCodeConst.TaskSuspend
                }).ToList();
            }

            // 复位完成就应该置为 Running 状态
            // 这里等收到指令的时候置为 Running，处理简单
            // 这就要求调用中清掉缓存指令
            if (state == State.Reset)
            {
                state = State.Running;
            }

            // 这个方法已经是在线程中执行的了
            // 取像必然是串行的，只有运行 ToolBlock 才有串行和并发的区别
            // A指令，ToolBlock 可以选择串行或并行，但不管串行还是并行，都是另一个线程的事，不影响流程往下走
            // AR指令，目前 ToolBlock 也是异步执行，但是主线程依然会卡住等待 ToolBlock 结果返回再执行下一条指令
            // 由于取像中做了缓存，所以就算这里实际上还是串行，但效率还是提高的

            List<IReplyCommand> replyCommands = new List<IReplyCommand>(cmds.Count);

            int acqCount = cmds.Count(c => (c.Type & CommandType.A) == CommandType.A);

            //delete by Luodian @ 20220113 在把触发跟获取图像分开之后，已经把拍照超时，丢弃图像的逻辑给拿掉了，所以这里也拿掉；
            //if (acqCount > 0)
            //    acqImage.BeginAcqImage(cmdIndex, acqCount);

            //add by LuoDian @ 20211119 因为存图与运行ToolBlock是并行的，所以添加一个计数器，统计图像的数量，以便在运行完ToolBlock后释放计数器，以此来Dispose图像
            //CountdownEvent countEvent = new CountdownEvent(cmds.FindAll(c => c.Type == CommandType.A || c.Type == CommandType.AR).Count);

            Stopwatch cameraGrapTime = new Stopwatch();
            cameraGrapTime.Start();
            foreach (TaskCommand cmd in cmds)
            {
                if (state == State.Running)
                {
                    if (cmd.Type != CommandType.A && cmd.Type != CommandType.R && cmd.Type != CommandType.AR)
                    {
                        SendCommand sendCommand = new SendCommand
                        {
                            CommandHeader = cmd.CommandHeader,
                            AcqOrCalibIndex = cmd.AcqOrCalibIndex,
                            Type = cmd.Type,
                            ErrorCode = ErrorCodeConst.CommandTypeError
                        };
                        replyCommands.Add(sendCommand);
                        continue;
                    }

                    (TimeSpan ts, SendCommand replyCommand) = FuncWatch.ElapsedTime(() =>
                    {
                        if ((cmd.Type & CommandType.A) == CommandType.A)
                        {
                            e.CmdID = cmdId;
                            e.Cmds = cmds;

                            var @params = cmd.FieldValues
                                            .Where(c => c.Use == CommandFieldUse.ToolBlock)
                                            .Select(a => (a.Name, a.Value));

                            string strSubName = "default", strSN = "";
                            foreach (var (Name, Value) in @params)
                            {
                                if (Name.Equals("SN") && Value != null)
                                    strSN = Value.ToString();
                                else if (Name.Equals("SubName") && Value != null)
                                    strSubName = Value.ToString();
                            }

                            // Modified by louis on Mar. 18 这里目前只有缺陷信息
                            var @paramsSaveImage = cmd.FieldValues
                                                        .Where(c => c.Use == CommandFieldUse.SaveImage)
                                                        .Select(a => (a.Name, a.Value));

                            LinkedDictionary<string, object> snData = new LinkedDictionary<string, object>();
                            snData.Add("SN", strSN);

                            //add by LuoDian @ 20211119 这里需要根据计数的情况判断是否需要切换光源
                            int errCode = SetTrigger(cmdId, cmd, dicCountdownEvent, dicOpenLightCountdownEvent, strSubName);
                            if (errCode != ErrorCodeConst.OK)
                                return BuildSendCommand(errCode, cmd, snData);

                            if (errCode == ErrorCodeConst.OK)
                            {
                                //add by Luodian @ 20220111 把触发设置和获取图像数据分开之后，获取图像数据和运行ToolBlock放在一个单独的接口RunToolBlock中实现，这里先把这个接口的参数先准备好
                                string AcqOrCalibName = cmd.AcqOrCalibName;
                                int AcqOrCalibIndex = cmd.AcqOrCalibIndex;

                                // add by LuoDian @ 20220111 MAC BOOK AOI项目定制的存图逻辑z
                                // Modified by Louis on June 27 2022 存图迁移到ImageProcess工具中了
                                // SaveImageBeforeRunToolBlock(@params, paramsSaveImage, AcqOrCalibName);
                                //add by LuoDian @ 20220121 往队列中塞入运行ToolBlock的参数
                                if (dicParamsQueue != null && dicParamsQueue.ContainsKey(AcqOrCalibName) && dicParamsQueue[AcqOrCalibName] != null)
                                    dicParamsQueue[AcqOrCalibName].Enqueue(@params);
                                if (dicSubNameQueue != null && dicSubNameQueue.ContainsKey(AcqOrCalibName) && dicSubNameQueue[AcqOrCalibName] != null)
                                    dicSubNameQueue[AcqOrCalibName].Enqueue(strSubName);
                                if (dicCmdIdQueue != null && dicCmdIdQueue.ContainsKey(AcqOrCalibName) && dicCmdIdQueue[AcqOrCalibName] != null)
                                    dicCmdIdQueue[AcqOrCalibName].Enqueue(cmdId);

                                if (cmd.Type == CommandType.A)
                                {
                                    // 这里只是丢到队列里去，没做啥操作，所以必定是 OK
                                    //update by LuoDian @ 20211019 把SN也反馈回去
                                    return BuildSendCommand(ErrorCodeConst.OK, cmd, snData);
                                }
                                else if (cmd.Type == CommandType.AR)
                                {
                                    //update by Luodian @ 20220121 把触发设置和获取图像数据分开之后，ToolBlock的运行已经放到一个单独的线程中执行，通过队列来传递参数和结果
                                    LinkedDictionary<string, object> output = null;
                                    if (dicOutputValQueue != null && dicOutputValQueue.ContainsKey(AcqOrCalibName) && dicOutputValQueue[AcqOrCalibName] != null)
                                    {
                                        do
                                        {
                                            if (dicOutputValQueue[AcqOrCalibName].Count > 0)
                                            {
                                                output = dicOutputValQueue[AcqOrCalibName].Dequeue();
                                                break;
                                            }
                                        } while (runningToken);
                                    }

                                    return BuildSendCommand(errCode, cmd, output);
                                }
                                else
                                    return null;
                            }
                            else
                            {
                                return BuildSendCommand(errCode, cmd, cmd.Type == CommandType.A ? null : getErrorOutput(cmd.AcqOrCalibIndex));
                            }
                        }
                        else if (cmd.Type == CommandType.R)
                        {
                            int errorCode = ErrorCodeConst.OK;
                            LinkedDictionary<string, object> output = RunCmdR(cmdId, cmd.AcqOrCalibIndex, ref errorCode);

                            return BuildSendCommand(errorCode, cmd, output);
                        }
                        else
                            return null;
                    });

                    if (replyCommand.ErrorCode == ErrorCodeConst.OK)
                        log.Debug(new TaskServerLogMessage(taskName, cmd.AcqOrCalibName, A_RunCmd, R_Success, $"\"[{cmdId}]{cmd}\"", ts.TotalMilliseconds));
                    else
                        log.Error(new TaskServerLogMessage(taskName, cmd.AcqOrCalibName, A_RunCmd, R_Fail, $"\"[{cmdId}]{cmd}\"，{ErrorCodeConst.ErrorMessage(replyCommand.ErrorCode)}", ts.TotalMilliseconds));

                    if (replyCommand != null)
                        replyCommands.Add(replyCommand);

                    if (state == State.Suspend)
                    {
                        slim.Reset();
                        slim.Wait();
                    }

                    //add by LuoDian @ 20211119 等待所有共用同一个光源的相机的计数释放完成
                    if (!simulationRepo.Enabled)
                    {
                        dicCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].Wait();
                        //log?.Info($"与任务[{taskName}]的拍照[{cmd.AcqOrCalibIndex}]共用同一个光源的所有相机已完成拍照，计数已释放！");
                    }
                }
                else
                {
                    if ((cmd.Type & CommandType.A) == CommandType.A)
                        replyCommands.Add(BuildSendCommand(ErrorCodeConst.TaskCancel, cmd, null));
                    else
                        replyCommands.Add(BuildSendCommand(ErrorCodeConst.TaskCancel, cmd, errorOutput));
                }
            }
            cameraGrapTime.Stop();
            log?.Info($"【CT统计】指令ID [{cmdId}] 中任务[{taskName}]的共计 {cmds?.Count} 个指令已执行完成！耗时：{cameraGrapTime.ElapsedMilliseconds}");

            return replyCommands;
        }

        /// <summary>
        /// 在运行ToolBlock前保存图像
        /// add by LuoDian @ 20211012 在这里添加保存图像的逻辑，通过多线程异步执行来保存图像，这里保存完图像后，要把后面的保存图像逻辑干掉
        /// 产品型号、SN码、检测面名称、拍照的点位编号、光源名称 通过上位机指令传过来，日期时间即时生成，相机编号通过当前相机对象获取
        /// 是否要保存原图到指令文件夹，需要从ToolBlock参数中获取
        /// </summary>
        private void SaveImageBeforeRunToolBlock(IEnumerable<(string, object)> @params, IEnumerable<(string, object)> @paramsSaveImage, string acqImageName)
        {
            #region   add by LuoDian @ 20211012 在这里添加保存图像到客户指定文件夹的逻辑
            //add by LuoDian @ 20211012 在这里添加保存图像的逻辑，通过多线程异步执行来保存图像，这里保存完图像后，要把后面的保存图像逻辑干掉
            //产品型号、SN码、检测面名称、拍照的点位编号、光源名称 通过上位机指令传过来，日期时间即时生成，相机编号通过当前相机对象获取
            //是否要保存原图到指令文件夹，需要从ToolBlock参数中获取
            bool bSaveImage = false;
            string strProductType = "", strSN = "", strInspectPosName = "";
            int iGrabPosNo = -1;
            string strCameraNo = "", strLightName = "";
            string strSubName = "default";
            string strColor = "Color";
            string strDefectInfo = "";
            List<ParamInfo> inputParams = toolBlock.GetInputs();
            foreach (ParamInfo param in inputParams)
            {
                if (param.Name.Equals("SaveOriginalImage") && param.Value.ToString().ToUpper().Equals("YES"))
                    bSaveImage = true;
                else if (param.Name.Equals("CameraNo"))
                    strCameraNo = param.Value.ToString();
                else if (param.Name.Equals("SaveFolderPath"))
                    saveImageFolder = param.Value.ToString();
                //else if (param.Name.Equals("BakFolderPath"))
                //    bakImageFolder = param.Value.ToString();
            }

            foreach (var (Name, Value) in @params)
            {
                if (Name.Equals("ProductType") && Value != null)
                    strProductType = Value.ToString();
                else if (Name.Equals("SN") && Value != null)
                    strSN = Value.ToString();
                else if (Name.Equals("InspectPosName") && Value != null)
                    strInspectPosName = Value.ToString();
                else if (Name.Equals("SubName") && Value != null)
                    strSubName = Value.ToString();
                else if (Name.Equals("LightName") && Value != null)
                    strLightName = Value.ToString();
                else if (Name.Equals("GrabPosNo") && Value != null)
                    iGrabPosNo = int.Parse(Value.ToString());
            }

            foreach (var (Name, Value) in @paramsSaveImage)
            {
                if (Name.Equals("DefectInfo") && Value != null)
                    strDefectInfo = Value.ToString();
            }

            if (strSubName.Contains(API.GlobalParams.SubName_DeepBlue))
                strColor = "D";
            if (strSubName.Contains(API.GlobalParams.SubName_Bassalt))
                strColor = "B";
            else if (strSubName.Contains(API.GlobalParams.SubName_Gray))
                strColor = "R";
            else if (strSubName.Contains(API.GlobalParams.SubName_Gold))
                strColor = "G";
            else if (strSubName.Contains(API.GlobalParams.SubName_Silver))
                strColor = "S";
            #endregion

            if (string.IsNullOrEmpty(strDefectInfo))
            {
                log?.Warn($"指令中缺少缺陷信息：{strDefectInfo}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                AddImageNameToQueue(acqImageName, IMAGE_DISCARD);
                return;
            }

            List<string> lstDefectName = new List<string>();
            if (strDefectInfo.ToUpper().Equals("NO"))
                return;
            else if (strDefectInfo.ToUpper().Equals("YES"))
                lstDefectName.Add("All");
            else
            {
                //先用%把不同的缺陷先分割出来
                string[] arrDefectItem = strDefectInfo.Split("%".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrDefectItem != null && arrDefectItem.Length > 0)
                {
                    foreach (string strItem in arrDefectItem)
                    {
                        if (string.IsNullOrEmpty(strItem) || !strItem.Contains("#"))
                        {
                            log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                            break;
                        }
                        string[] arrDefectInfo = strItem.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (arrDefectInfo == null || arrDefectInfo.Length != 3 || string.IsNullOrEmpty(arrDefectInfo[0]) || string.IsNullOrEmpty(arrDefectInfo[1]) || string.IsNullOrEmpty(arrDefectInfo[2]))
                        {
                            log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                            break;
                        }
                        if ((arrDefectInfo[0].Equals("TC") || arrDefectInfo[0].Equals("LCM") || arrDefectInfo[0].Equals("DH") || arrDefectInfo[0].Equals("BC")) && arrDefectInfo[0].Equals(strInspectPosName))
                        {
                            if (!arrDefectInfo[1].Contains("_"))
                            {
                                log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                                break;
                            }
                            string[] arrCameraNoAndPosNo = arrDefectInfo[1].Split('_');
                            if (arrCameraNoAndPosNo == null || arrCameraNoAndPosNo.Length != 2 || string.IsNullOrEmpty(arrCameraNoAndPosNo[0]) || string.IsNullOrEmpty(arrCameraNoAndPosNo[1]))
                            {
                                log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                                break;
                            }
                            if (!int.TryParse(arrCameraNoAndPosNo[0], out int iCameraNo) ||
                                !int.TryParse(arrCameraNoAndPosNo[1], out int iPosNo))
                            {
                                log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                                break;
                            }
                            //判断是否是当前工站当前点位的缺陷，是的话就存图
                            if (strCameraNo.Equals(iCameraNo.ToString()) && iPosNo == iGrabPosNo)
                            {
                                lstDefectName.Add(arrDefectInfo[2]);
                            }
                        }
                        else if ((arrDefectInfo[0].Equals("Corner") || arrDefectInfo[0].Equals("Mandrel")) && arrDefectInfo[0].Equals(strInspectPosName))
                        {
                            if (!int.TryParse(arrDefectInfo[1], out int iPosNo) || iPosNo < 1 || iPosNo > 4)
                            {
                                log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                                break;
                            }

                            if (iPosNo == iGrabPosNo)
                            {
                                lstDefectName.Add(arrDefectInfo[2]);
                            }
                        }
                        else if (arrDefectInfo[0].Equals("Side") && arrDefectInfo[0].Equals(strInspectPosName))
                        {
                            if (!int.TryParse(arrDefectInfo[1], out int iPosNo) || iPosNo < 1 || iPosNo > 10)
                            {
                                log?.Warn($"指令中接收到非法的缺陷信息：{strItem}! 相机编号[{strCameraNo}的拍照[{acqImageName}]没有存图！");
                                break;
                            }

                            if (iPosNo == iGrabPosNo)
                            {
                                lstDefectName.Add(arrDefectInfo[2]);
                            }
                        }
                        else if (arrDefectInfo[0].Equals("Logo"))
                        {
                            if (arrDefectInfo[0].Equals(strInspectPosName))
                            {
                                lstDefectName.Add(arrDefectInfo[2]);
                            }
                        }
                    }
                }
            }

            // add by LuoDian @ 20211012 在这里添加保存图像到客户指定文件夹的逻辑
            if (bSaveImage && lstDefectName.Count > 0)
            {
                // 客户要求一个产品相同的面的同一个点位的时间戳一样
                switch (strInspectPosName)
                {
                    case LOGO:
                        // Logo 只有一个点位，所以只有一个时间戳
                        string logoKey = strSN + SEPARATOR + LOGO;
                        string logoTime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                        if (!timeSet.ContainsKey(logoKey))
                        {
                            // 移除Logo旧的时间戳，防止长时间运行这个时间集合越来越大
                            var obsoletedKeys = timeSet.Keys.Where(key => key.EndsWith(SEPARATOR + LOGO));
                            for (int i = 0; i < obsoletedKeys.Count(); i++)
                            {
                                timeSet.Remove(obsoletedKeys.ElementAt(i));
                            }

                            timeSet.Add(logoKey, logoTime);
                        }
                        break;
                    case CONER:
                        // Coner的点位信息按照光源来区分, 一个产品有多个点位
                        string conerPos = strLightName.Substring(strLightName.Length - 1);
                        string conerKey = conerPos + SEPARATOR + strSN + SEPARATOR + CONER;
                        string conerTime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                        if (!timeSet.ContainsKey(conerKey))
                        {
                            // 当新的sn到达时移除Coner旧的sn时间戳，防止长时间运行这个时间集合越来越大
                            var obsoletedKeys = timeSet.Keys.Where(key => key.EndsWith(SEPARATOR + CONER) && !key.Contains(strSN));
                            for (int i = 0; i < obsoletedKeys.Count(); i++)
                            {
                                timeSet.Remove(obsoletedKeys.ElementAt(i));
                            }

                            timeSet.Add(conerKey, conerTime);
                        }
                        break;
                    default:
                        // LCM, TC, DH, BC, Side 的点位信息时间戳处理
                        string otherPos = strCameraNo + SEPARATOR + iGrabPosNo;
                        string otherKey = otherPos + SEPARATOR + strSN + SEPARATOR + OTHER;
                        string otherTime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                        if (!timeSet.ContainsKey(otherKey))
                        {
                            // 当新的sn到达时移除其它面旧的sn时间戳，防止长时间运行这个时间集合越来越大
                            var obsoletedKeys = timeSet.Keys.Where(key => key.EndsWith(SEPARATOR + OTHER) && !key.Contains(strSN));
                            for (int i = 0; i < obsoletedKeys.Count(); i++)
                            {
                                timeSet.Remove(obsoletedKeys.ElementAt(i));
                            }

                            timeSet.Add(otherKey, otherTime);
                        }
                        break;
                }

                string strFilePath = "";
                foreach (string strDefectName in lstDefectName)
                {
                    if (string.IsNullOrEmpty(strDefectName))
                        continue;
                    string strTempPath = "";
                    string strFolderPath = $@"{saveImageFolder}\{strSN}\{strInspectPosName}_{strDefectName}";
                    if (strInspectPosName.Equals(LOGO) && strLightName.Equals("Coaxial"))
                    {
                        string logoKey = strSN + SEPARATOR + LOGO;
                        string logoTime = timeSet[logoKey];
                        strTempPath = $@"{strFolderPath}\{strProductType}.{strColor}.{strSN}.CosmeticAOI.{logoTime}.{strInspectPosName}.{strLightName}.jpg";
                    }
                    else if (strInspectPosName.Equals(CONER))
                    {
                        string conerPos = strLightName.Substring(strLightName.Length - 1);
                        string conerKey = conerPos + SEPARATOR + strSN + SEPARATOR + CONER;
                        string conerTime = timeSet[conerKey];
                        strTempPath = $@"{strFolderPath}\{strProductType}.{strColor}.{strSN}.CosmeticAOI.{conerTime}.{strInspectPosName}.{strLightName.Substring(0, strLightName.Length - 1)}.{conerPos}.jpg";
                    }
                    else
                    {
                        string otherPos = strCameraNo + SEPARATOR + iGrabPosNo;
                        string otherKey = otherPos + SEPARATOR + strSN + SEPARATOR + OTHER;
                        string otherTime = timeSet[otherKey];
                        strTempPath = $@"{strFolderPath}\{strProductType}.{strColor}.{strSN}.CosmeticAOI.{otherTime}.{strInspectPosName}.{strLightName}.{otherPos}.jpg";
                    }

                    strFilePath += $"{strTempPath}&*";
                }

                strFilePath = strFilePath.Substring(0, strFilePath.Length - 2);
                AddImageNameToQueue(acqImageName, strFilePath);
            }
            else
            {
                AddImageNameToQueue(acqImageName, IMAGE_DISCARD);
            }
        }

        /// <summary>
        /// 添加每个拍照每次执行的时候需要存图的路径或者丢弃标记
        /// </summary>
        /// <param name="acqImageName">任务中的拍照名称</param>
        /// <param name="filePathOrDiscard">图片保存的全路径或丢弃标识</param>
        private void AddImageNameToQueue(string acqImageName, string filePathOrDiscard)
        {
            if (dicImageNameQueue != null && dicImageNameQueue.ContainsKey(acqImageName) && dicImageNameQueue[acqImageName] != null)
                dicImageNameQueue[acqImageName].Enqueue(filePathOrDiscard);
        }

        /// <summary>
        /// 设置光源亮度、触发、关闭光源
        /// add by LuoDian @ 20220111 把设置光源亮度、触发、关闭光源的逻辑放到这里
        /// </summary>
        private int SetTrigger(long cmdId, TaskCommand cmd, Dictionary<(string, int), CountdownEvent> dicCountdownEvent,
            Dictionary<(string, int), CountdownEvent> dicOpenLightCountdownEvent, string strSubName)
        {
            int errCode = ErrorCodeConst.OK;
            AcquireImageParams acqImageParams = null;
            OpticsInfoVO opticsInfo = opticsRepo.GetOptics(taskName, cmd.AcqOrCalibName, null, strSubName);
            if (!simulationRepo.Enabled)
            {
                acqImageParams = acqImage.GetAcqParams(opticsInfo);
                if (dicOpenLightCountdownEvent.ContainsKey((taskName, cmd.AcqOrCalibIndex)))
                {
                    lock (API.GlobalParams.LightControlLockObj)
                    {
                        if (dicOpenLightCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].CurrentCount == dicOpenLightCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].InitialCount)
                        {
                            if (acqImageParams != null && acqImageParams.LightControllerHandles != null && acqImageParams.LightControllerHandles.Count > 0)
                            {
                                Stopwatch openLightWatch = new Stopwatch();
                                openLightWatch.Start();
                                errCode = acqImage.SetLightController(acqImageParams.LightControllerHandles);
                                //if (errCode != ErrorCodeConst.OK)
                                //    return errCode;
                                openLightWatch.Stop();
                                foreach (LightControllerValueInfoVO ligthInfo in opticsInfo.LightControllerValues)
                                {
                                    log?.Info($"【CT统计】指令ID [{cmdId}] 中任务[{taskName}]的拍照[{cmd.AcqOrCalibIndex}]已点亮光源[{ligthInfo.LightControllerName}]! 耗时：{openLightWatch.ElapsedMilliseconds}");
                                }
                            }
                        }
                        dicOpenLightCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].Signal();
                    }
                    dicOpenLightCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].Wait();
                }

                //update by LuoDian @ 20220111 为了提升软触发的效率，把触发设置和获取图像数据分开，这里先设置触发，再通过一个单独的线程来获取图像数据和运行ToolBlock
                Stopwatch grabImageWatch = new Stopwatch();
                grabImageWatch.Start();
                errCode = acqImage.Trigger(opticsInfo, false);
                //if (errCode != ErrorCodeConst.OK)
                //    return errCode;
                grabImageWatch.Stop();
                log?.Info($"【CT统计】指令ID [{cmdId}] 中任务 [{taskName}] 的拍照 [{cmd.AcqOrCalibIndex}] 触发已完成！耗时：{grabImageWatch.ElapsedMilliseconds}");


                //add by LuoDian @ 20211119 这里需要根据计数的情况判断是否需要切换光源
                lock (API.GlobalParams.CloseLightControlLockObj)
                {
                    //add by LuoDian @ 20211119 所有相机都拍完之后才能关闭光源
                    if (dicCountdownEvent.Count > 0 && dicCountdownEvent.ContainsKey((taskName, cmd.AcqOrCalibIndex)))
                    {
                        if (dicCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].CurrentCount == 1)
                        {
                            Stopwatch closeLightWatch = new Stopwatch();
                            closeLightWatch.Start();
                            errCode = acqImage.CloseLightController(acqImageParams.LightControllerHandles);
                            //if (errCode != ErrorCodeConst.OK)
                            //    return errCode;
                            closeLightWatch.Stop();
                            log?.Info($"【CT统计】指令ID [{cmdId}] 中与任务[{taskName}]的拍照[{cmd.AcqOrCalibIndex}]共用同一个光源的所有相机已拍完照！光源已关闭！耗时：{closeLightWatch.ElapsedMilliseconds}");
                        }

                        //add by LuoDian @ 20211119 为了判断是否需要切换光源，每个相机拍完一次照之后，计数一次
                        dicCountdownEvent[(taskName, cmd.AcqOrCalibIndex)].Signal();
                    }
                }
            }
            return errCode;
        }

        // R 指令缓存，有一个清空的问题，当 R 指令执行完毕的时候，就清空对应的缓存
        // 如果 R 指令超时，已经回复了 999，但这时 ToolBlock 运行完成，给缓存赋值，就会造成错误
        // 我们极端一点，如果某一轮的 ToolBlock 还没有运行完成，下一轮的指令就过来了
        // 下一轮的 A 指令先于上一轮的 R 指令完成，那么用比较指令索引的方式就行不通了
        //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
        void RunToolBlockByCmdA(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params, string subName)
        {
            //add by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，后面一条新的指令过来的时候，有可能前面的指令还没有执行完成，由于是多线程程在运行采图跟ToolBlock，所以需要在采图和运行ToolBlock的地方添加一个线程锁，防止图像错乱
            lock (runToolBlockLockObj)
            {
                toolBlock.RunSerial(hyImage, @params, subName, callback);
            }

            void callback(string acqName, int acqIndex, int errorCode, LinkedDictionary<string, object> outputs)
            {
                // ToolBlock 不管运行成功失败，都要运行 Func
                // ToolBlock 运行失败，也可能得到部分结果，一样要传给上位机
                try
                {
                    RunScrip_ModifyOutputs(hyImage.CmdID, acqName, acqIndex, ref errorCode, outputs);

                    //add by LuoDian @ 20220121 把图像后处理的逻辑修改成队列+独立线程处理之后，ToolBlock的运行已经放到单独的线程中执行，对于AR类型的指令，需要在这里把输出结果压入队列
                    dicOutputValQueue[acqName].Enqueue(outputs);

                    hyImage.Dispose();
                }
                catch (Exception ex)
                {
                    log.Error(new TaskServerLogMessage(hyImage.TaskName, hyImage.AcqOrCalibName, $"{A_RunCmd}：A", R_Fail, ex.Message));
                }
            }
        }

        //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
        (int errorCode, LinkedDictionary<string, object> output) RunToolBlockByCmdAR(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params, string subName)
        {
            int errCode = -1;
            LinkedDictionary<string, object> output = null;

            AutoResetEvent autoReset = new AutoResetEvent(false);

            //add by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，后面一条新的指令过来的时候，有可能前面的指令还没有执行完成，由于是多线程程在运行采图跟ToolBlock，所以需要在采图和运行ToolBlock的地方添加一个线程锁，防止图像错乱
            lock (runToolBlockLockObj)
            {
                toolBlock.RunSerial(hyImage, @params, subName, callback);
            }

            autoReset.WaitOne();
            autoReset.Dispose();

            return (errCode, output);

            void callback(string acqName, int acqIndex, int errorCode, LinkedDictionary<string, object> outputs)
            {
                // ToolBlock 不管运行成功失败，都要运行 Func
                // ToolBlock 运行失败，也可能得到部分结果，一样要传给上位机

                int errorCode1 = errorCode;

                try
                {
                    output = RunScrip_ModifyOutputs(hyImage.CmdID, acqName, acqIndex, ref errorCode1, outputs);
                    //add by LuoDian @ 20220120 释放图像
                    hyImage.Dispose();

                    errCode = errorCode == ErrorCodeConst.OK ? errorCode1 : errorCode;
                }
                catch (Exception ex)
                {
                    log.Error(new TaskServerLogMessage(hyImage.TaskName, hyImage.AcqOrCalibName, $"{A_RunCmd}：AR", R_Fail, ex.Message));
                }

                autoReset.Set();
            }
        }

        LinkedDictionary<string, object> RunCmdR(long cmdIndex, int acqImageIndex, ref int errorCode)
        {
            int count = runTimeRepo.CmdRTimeout / 10;

            int index = 0;

            LinkedDictionary<string, object> result;

            do
            {
                result = m_outputCache.GetOutput(cmdIndex, acqImageIndex);
                if (result != null)
                {
                    errorCode = ErrorCodeConst.OK;
                    break;
                }

                if (index == count)
                {
                    //SZ m_outputCache => NullReferenceException 
                    if (m_outputCache != null)
                    {
                        m_outputCache.SetTimeout(cmdIndex, acqImageIndex);

                        errorCode = ErrorCodeConst.CmdRTimeout;
                        result = getErrorOutput(acqImageIndex);
                        break;
                    }
                }

                index++;
                Thread.Sleep(10);
            } while (true);

            return result;
        }

        LinkedDictionary<string, object> RunScrip_ModifyOutputs(long cmdIndex, string acqImageName, int acqImageIndex, ref int errorCode, LinkedDictionary<string, object> tbOutputs)
        {
            if (errorCode == ErrorCodeConst.OK)
            {
                // 运行时有验证，这里就不需要验证是否有 ErrorCode 了
                if (!tbOutputs.ContainsKey(InputOutputConst.Output_ErrorCode) || tbOutputs[InputOutputConst.Output_ErrorCode] == null)
                    errorCode = ErrorCodeConst.NoneErrorCode;
                else
                {
                    errorCode = (int)tbOutputs[InputOutputConst.Output_ErrorCode];
                }
            }

            try
            {
                (TimeSpan ts, LinkedDictionary<string, object> funcOutputs) = FuncWatch.ElapsedTime(() =>
                {
                    return scriptService.ModifyOutputs(TaskInfo.Name, acqImageIndex, tbOutputs);
                });

                log.Debug(new TaskServerLogMessage(taskName, acqImageName, A_RunScript, R_Success, null, ts.TotalMilliseconds));

                Task.Run(() =>
                {
                    // 完成一次 ToolBlock 了，也就知道 OK/NG 了，可以存图了
                    CompletedSingleAcqImage?.Invoke(taskName, acqImageName, acqImageIndex, funcOutputs);
                });

                m_outputCache.SetOutput(cmdIndex, acqImageIndex, funcOutputs);

                createErrorOutput(funcOutputs);

                return funcOutputs;
            }
            catch (Exception e)
            {
                log.Error(new TaskServerLogMessage(taskName, acqImageName, A_RunScript, R_Fail, e.Message));

                if (errorCode == ErrorCodeConst.OK)
                {
                    errorCode = ErrorCodeConst.ScriptError;
                }

                m_outputCache.SetOutput(cmdIndex, acqImageIndex, errorOutput);

                // 函数运行失败，直接获取 errorOutput，如果 errorOutput 是 null，那就只能传 null，不能再次调用 Func
                return errorOutput;
            }
        }

        public IReplyCommand RunProcessCommand(ProcessCommand cmd)
        {
            // 任务和标定指令都是异步运行的
            // 指令排队执行可以保证，修改 state 的时候，不会并发

            // 暂停的时候，可能有多条指令正在运行，例如第一轮的指令卡了，但还没超时，第二轮的指令又来了，这时点了暂停
            // 更常见的是 A 指令和 R 指令同时在执行

            // A 指令和 AR 指令，都只会在取像里卡住，那么去暂停取像类里的超时计时器就可以了
            // 如果有条指令在运行，由于调度的存在，后面的指令会排队，所以只要卡住第一条指令就可以了
            // R 指令在本类里处理


            if (cmd.Type == ProcessCommandType.S)
            {
                state = State.Suspend;

                acqImage.Suspend();
            }
            else if (cmd.Type == ProcessCommandType.R)
            {
                state = State.Running;

                slim.Set();

                acqImage.Resume();
            }
            else if (cmd.Type == ProcessCommandType.RE)
            {
                if (state == State.Suspend)
                {
                    state = State.Reset;

                    slim.Set();

                    acqImage.Reset();
                }
            }

            return null;
        }

        SendCommand BuildSendCommand(int errCode, TaskCommand cmd, LinkedDictionary<string, object> outputs)
        {
            SendCommand sendCommand = new SendCommand
            {
                CommandHeader = cmd.CommandHeader,
                AcqOrCalibIndex = cmd.AcqOrCalibIndex,
                Type = cmd.Type,
                ErrorCode = errCode
            };

            if (outputs != null)
            {
                foreach (var kv in outputs)
                {
                    // #### 这里对 ErrorCode 做了过滤，做个标记
                    if (kv.Key == InputOutputConst.Output_ErrorCode) continue;

                    sendCommand.FieldValues.Add(new CommandFieldValue { Name = kv.Key, Value = kv.Value });
                }
            }

            return sendCommand;
        }

        //update by LuoDian @ 20211119 这里拷贝多一份图像，用于存图，显示和运行ToolBlock单独一份
        (int errorCode, HyImageInfo hyImage, HyImageInfo hySaveImage, HyImageInfo toolBlockImage) GetImage(long cmdIndex, string acqImageName, int acqImageIndex, string sn, string subName, double imageRotatoAngle = 0)
        {
            int errorCode;
            CameraImage cameraImage;
            HyImageInfo hyImage = null;

            //add by LuoDian @ 20211119 这里拷贝多一份图像，用于存图，显示和运行ToolBlock单独一份
            HyImageInfo hySaveImage = null, toolBlockImage = null;
            Stopwatch watch = new Stopwatch();

            if (simulationRepo.Enabled)
            {
                errorCode = acqImage.GetImageFromLocal(acqImageName, out cameraImage);
            }
            else
            {
                //add by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，后面一条新的指令过来的时候，有可能前面的指令还没有执行完成，由于是多线程程在运行采图跟ToolBlock，所以需要在采图和运行ToolBlock的地方添加一个线程锁，防止图像错乱
                lock (getImageLockObj)
                {
                    watch.Start();
                    //update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
                    errorCode = acqImage.GetImageFromCamera(out cameraImage);
                    watch.Stop();
                    log?.Info($"【优化CT】指令ID [{cmdIndex}] 中任务 [{taskName}] 的拍照 [{acqImageIndex}] 的在触发完之后，获取图像数据这个过程中的耗时：{watch.ElapsedMilliseconds}。这个耗时过长，说明触发完成后，在排队等待队列出图。队列出图的耗时主要在：等待前面一张图的队列出图线程锁释放、图像像素格式转换、深拷贝");
                }
            }

            //watch.Reset();
            //watch.Start();
            ////add by LuoDian @ 20211227 根据视觉参数设定的图像旋转角度，对图像进行旋转，不翻转
            //RotatoImage(cameraImage.Bitmap, acqImageName, imageRotatoAngle);
            //watch.Stop();
            //log?.Info($"【优化CT】指令ID [{cmdIndex}] 中任务 [{taskName}] 的拍照 [{acqImageIndex}] 的在获取图像数据之后，旋转图像的耗时：{watch.ElapsedMilliseconds}");
            //add by LuoDian @ 20220113 把图像的状态放到字典里面去
            dicImageFinishStatus[cameraImage.Timestamp] = new bool[] { false, false, false };


            watch.Reset();
            watch.Start();
            if (errorCode == ErrorCodeConst.OK)
            {
                hyImage = new HyImageInfo
                {
                    Bitmap = cameraImage.Bitmap,
                    IsGrey = cameraImage.IsGrey,
                    FrameNum = cameraImage.FrameNum,
                    Timestamp = cameraImage.Timestamp,
                    CmdID = cmdIndex,
                    TaskName = taskName,
                    AcqOrCalibName = acqImageName,
                    AcqOrCalibIndex = acqImageIndex,
                    SN = sn
                };

                //add by LuoDian @ 20211119 这里拷贝多一份图像，用于存图，显示和运行ToolBlock单独一份
                hySaveImage = new HyImageInfo
                {
                    Bitmap = (System.Drawing.Bitmap)cameraImage.Bitmap.Clone(),
                    IsGrey = cameraImage.IsGrey,
                    FrameNum = cameraImage.FrameNum,
                    Timestamp = cameraImage.Timestamp,
                    CmdID = cmdIndex,
                    TaskName = taskName,
                    AcqOrCalibName = acqImageName,
                    AcqOrCalibIndex = acqImageIndex,
                    SN = sn
                };

                toolBlockImage = new HyImageInfo
                {
                    Bitmap = (System.Drawing.Bitmap)cameraImage.Bitmap.Clone(),
                    IsGrey = cameraImage.IsGrey,
                    FrameNum = cameraImage.FrameNum,
                    Timestamp = cameraImage.Timestamp,
                    CmdID = cmdIndex,
                    TaskName = taskName,
                    AcqOrCalibName = acqImageName,
                    AcqOrCalibIndex = acqImageIndex,
                    SN = sn
                };
                watch.Stop();
                log?.Info($"【优化CT】指令ID [{cmdIndex}] 中任务 [{taskName}] 的拍照 [{acqImageIndex}] 的在获取图像数据之后，3次拷贝图像的耗时：{watch.ElapsedMilliseconds}");
            }
            else
            {
                log.Error(new TaskServerLogMessage(taskName, acqImageName, A_Run, R_Fail, $"取像失败，ErrorCode={errorCode}"));
            }

            return (errorCode, hyImage, hySaveImage, toolBlockImage);
        }

        /// <summary>
        /// add by LuoDian @ 20211227 根据视觉参数设定的图像旋转角度，对图像进行旋转，不翻转
        /// </summary>
        /// <param name="imageInfo"></param>
        /// <param name="imageRotatoAngle"></param>
        void RotatoImage(System.Drawing.Bitmap image, string acqName, double imageRotatoAngle)
        {
            switch (imageRotatoAngle)
            {
                case 0:
                    break;
                case 90:
                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                    break;
                case 180:
                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                    break;
                case 270:
                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                    break;
                default:
                    log.Error(new TaskServerLogMessage(taskName, acqName, A_Run, R_Fail, $"图像旋转失败！"));
                    break;
            }
        }

        /// <summary>
        /// add by LuoDian @ 20210824 用于离线批量跑任务
        /// </summary>
        /// <param name="cmdIndex"></param>
        /// <param name="productName"></param>
        /// <param name="acqImageIndex"></param>
        /// <param name="sn"></param>
        /// <returns></returns>
        (int errorCode, HyImageInfo hyImage) GetImageForBatchRunOffline(long cmdIndex, string acqImageName, string productName, int acqImageIndex, string sn)
        {
            int errorCode;
            CameraImage cameraImage;
            HyImageInfo hyImage = null;

            errorCode = acqImage.GetImageFromLocalForBatchRunOffline(productName, out cameraImage);

            if (errorCode == ErrorCodeConst.OK)
            {
                hyImage = new HyImageInfo
                {
                    Bitmap = cameraImage.Bitmap,
                    IsGrey = cameraImage.IsGrey,
                    FrameNum = cameraImage.FrameNum,
                    CmdID = cmdIndex,
                    TaskName = taskName,
                    AcqOrCalibName = acqImageName,
                    AcqOrCalibIndex = acqImageIndex,
                    SN = sn
                };

                HyImageInfo copy = hyImage.Clone();

                // 这里可以提升 CT
                Task.Run(() =>
                {
                    GetSrcImage?.Invoke(copy);
                });
            }
            else
            {
                log.Error(new TaskServerLogMessage(taskName, acqImageName, A_Run, R_Fail, "取像失败"));
            }

            return (errorCode, hyImage);
        }

        enum State
        {
            /// <summary>
            /// 空闲
            /// </summary>
            Idle,

            /// <summary>
            /// 运行中
            /// </summary>
            Running,

            /// <summary>
            /// 暂停
            /// </summary>
            Suspend,

            /// <summary>
            /// 复位
            /// </summary>
            Reset
        }
        #endregion

        #region add by LuoDian @ 20220121  图像后处理的逻辑控制，中心思路是同一个拍照Index，同一个线程内进行处理，不同的拍照Index，不同的线程处理
        Dictionary<string, Queue<HyImageInfo>> dicShowImageQueue = new Dictionary<string, Queue<HyImageInfo>>();
        Dictionary<string, Queue<HyImageInfo>> dicSaveImageQueue = new Dictionary<string, Queue<HyImageInfo>>();
        Dictionary<string, Queue<HyImageInfo>> dicRunToolBlockQueue = new Dictionary<string, Queue<HyImageInfo>>();

        Dictionary<string, Queue<string>> dicImageNameQueue = new Dictionary<string, Queue<string>>();
        Dictionary<string, Queue<IEnumerable<(string Name, object Value)>>> dicParamsQueue = new Dictionary<string, Queue<IEnumerable<(string Name, object Value)>>>();
        Dictionary<string, Queue<string>> dicSubNameQueue = new Dictionary<string, Queue<string>>();
        Dictionary<string, Queue<LinkedDictionary<string, object>>> dicOutputValQueue = new Dictionary<string, Queue<LinkedDictionary<string, object>>>();
        Dictionary<string, Queue<long>> dicCmdIdQueue = new Dictionary<string, Queue<long>>();

        public int StartImageProcess()
        {
            int errCode = ErrorCodeConst.OK;

            ResetQueue();

            //不同的拍照Index，分成不同的线程来等待触发完成
            foreach (AcquireImageInfoVO imageInfoVO in TaskInfo.CameraAcquireImage.AcquireImages)
            {
                // dicGetImageResetEvent.Add(imageInfoVO.Name, new BlockingCollection<int>());
                dicShowImageQueue.Add(imageInfoVO.Name, new Queue<HyImageInfo>());
                dicSaveImageQueue.Add(imageInfoVO.Name, new Queue<HyImageInfo>());
                dicRunToolBlockQueue.Add(imageInfoVO.Name, new Queue<HyImageInfo>());
                dicImageNameQueue.Add(imageInfoVO.Name, new Queue<string>());
                dicParamsQueue.Add(imageInfoVO.Name, new Queue<IEnumerable<(string Name, object Value)>>());
                dicSubNameQueue.Add(imageInfoVO.Name, new Queue<string>());
                dicOutputValQueue.Add(imageInfoVO.Name, new Queue<LinkedDictionary<string, object>>());
                dicCmdIdQueue.Add(imageInfoVO.Name, new Queue<long>());

                // 显示图像
                ShowImage(imageInfoVO);

                // 存图
                // Modified by Louis on June 27 2022 存图迁移到ImageProcess工具中了
                // SaveImage(imageInfoVO);

                // 运行ToolBlock
                RunToolBlock(imageInfoVO);
            }

            // Modified by Louis on Apr. 4 2022
            // 获取图像为了单个相机下每次拍照顺序获取，所以一个相机用一个线程将相机缓存的图像队列分发到 运行，显示，存图三个队列中
            GetImage();

            return errCode;
        }

        /// <summary>
        /// 不同的拍照，在各自的线程中获取图像数据
        /// </summary>
        /// <param name="imageInfoVO">当前拍照的图像设定</param>
        /// <returns></returns>
        public int GetImage()
        {

            new Thread(() =>
            {
                int errorCode;
                while (runningToken)
                {
                    bool needWaiting = true;
                    foreach (AcquireImageInfoVO imageInfoVO in TaskInfo.CameraAcquireImage.AcquireImages)
                    {
                        if (dicCmdIdQueue[imageInfoVO.Name].Count > 0)
                        {
                            needWaiting = false;
                            break;
                        }
                    }

                    if (needWaiting)
                    {
                        Thread.Sleep(5);
                        continue;
                    }

                    foreach (AcquireImageInfoVO imageInfoVO in TaskInfo.CameraAcquireImage.AcquireImages)
                    {
                        string strAcqName = imageInfoVO.Name;
                        try
                        {
                            Stopwatch watch = new Stopwatch();
                            if (dicCmdIdQueue[strAcqName].Count < 1)
                                continue;

                            CameraImage cameraImage;
                            watch.Reset();
                            watch.Start();
                            if (simulationRepo.Enabled)
                            {
                                errorCode = acqImage.GetImageFromLocal(strAcqName, out cameraImage);
                            }
                            else
                            {
                                lock (getImageLockObj)
                                {
                                    //update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
                                    errorCode = acqImage.GetImageFromCamera(out cameraImage);
                                }
                            }
                            watch.Stop();
                            //log?.Info($"【CT统计】任务 [{taskName}] 的拍照 [{strAcqName}] 的在触发完之后，获取图像数据这个过程中的耗时：{watch.ElapsedMilliseconds}。这个耗时过长，说明触发完成后，在排队等待队列出图。队列出图的耗时主要在：等待前面一张图的队列出图线程锁释放、图像像素格式转换、深拷贝");

                            if (errorCode != ErrorCodeConst.OK)
                            {
                                log?.Error($"任务 [{TaskInfo.Name}] 的拍照 [{strAcqName}] {ErrorCodeConst.ErrorMessage(errorCode)}");
                                runningToken = false;
                                continue;
                            }

                            //watch.Reset();
                            //watch.Start();
                            ////add by LuoDian @ 20211227 根据视觉参数设定的图像旋转角度，对图像进行旋转，不翻转
                            //RotatoImage(cameraImage.Bitmap, acqImageName, imageRotatoAngle);
                            //watch.Stop();
                            //log?.Info($"【优化CT】指令ID [{cmdIndex}] 中任务 [{taskName}] 的拍照 [{acqImageIndex}] 的在获取图像数据之后，旋转图像的耗时：{watch.ElapsedMilliseconds}");

                            int acqImageIndex = taskRepo.GetAcqImageIndex(taskName, strAcqName);
                            long cmdId = dicCmdIdQueue[strAcqName].Dequeue();

                            // Added by Louis on Mar. 31 2022
                            //if (!simulationRepo.Enabled)
                            //{
                            //    int frameIndex = taskRepo.GetFrameIndex(taskName, cameraImage.FrameNum);
                            //    if (acqImageIndex != frameIndex)
                            //    {
                            //        log.Error(new TaskServerLogMessage(taskName, strAcqName, A_Run, R_Fail, $"拍照序列号和相机图像序列号不对应，可能已丢图！"));
                            //    }
                            //}

                            watch.Reset();
                            watch.Start();
                            HyImageInfo hyImage = new HyImageInfo
                            {
                                Bitmap = cameraImage.Bitmap,
                                IsGrey = cameraImage.IsGrey,
                                FrameNum = cameraImage.FrameNum,
                                Timestamp = cameraImage.Timestamp,
                                TaskName = taskName,
                                AcqOrCalibName = strAcqName,
                                AcqOrCalibIndex = acqImageIndex,
                                CmdID = cmdId
                            };

                            // add by LuoDian @ 20211119 这里拷贝多一份图像，用于存图，显示和运行ToolBlock单独一份
                            // Modified by Louis on June 27 2022 存图迁移到ImageProcess工具中了
                            //HyImageInfo hySaveImage = new HyImageInfo
                            //{
                            //    Bitmap = (System.Drawing.Bitmap)cameraImage.Bitmap.Clone(),
                            //    IsGrey = cameraImage.IsGrey,
                            //    FrameNum = cameraImage.FrameNum,
                            //    Timestamp = cameraImage.Timestamp,
                            //    TaskName = taskName,
                            //    AcqOrCalibName = strAcqName,
                            //    AcqOrCalibIndex = acqImageIndex,
                            //    CmdID = cmdId
                            //};

                            HyImageInfo toolBlockImage = new HyImageInfo
                            {
                                Bitmap = (System.Drawing.Bitmap)cameraImage.Bitmap.Clone(),
                                IsGrey = cameraImage.IsGrey,
                                FrameNum = cameraImage.FrameNum,
                                Timestamp = cameraImage.Timestamp,
                                TaskName = taskName,
                                AcqOrCalibName = strAcqName,
                                AcqOrCalibIndex = acqImageIndex,
                                CmdID = cmdId
                            };
                            watch.Stop();
                            //log?.Info($"【优化CT】中任务 [{taskName}] 的拍照 [{strAcqName}] 的在获取图像数据之后，3次拷贝图像的耗时：{watch.ElapsedMilliseconds}");

                            //把三张图像丢到三个队列中，做不同的图像后处理
                            dicShowImageQueue[strAcqName].Enqueue(hyImage);
                            // Modified by Louis on June 27 2022 存图迁移到ImageProcess工具中了
                            // dicSaveImageQueue[strAcqName].Enqueue(hySaveImage);
                            dicRunToolBlockQueue[strAcqName].Enqueue(toolBlockImage);
                        }
                        catch (Exception ex)
                        {
                            log.Error(new TaskServerLogMessage(taskName, strAcqName, A_Run, R_Fail, $"取像失败，原因：{ex.Message}"));
                        }
                    }

                    Thread.Sleep(5);
                }
            })
            { IsBackground = true }.Start();
            return ErrorCodeConst.OK;
        }

        /// <summary>
        /// 不同的拍照，在各自的线程中显示图像
        /// </summary>
        /// <param name="imageInfoVO">当前拍照的图像设定</param>
        /// <returns></returns>
        public int ShowImage(AcquireImageInfoVO imageInfoVO)
        {
            new Thread(() =>
            {
                Stopwatch watch = new Stopwatch();
                while (runningToken)
                {
                    try
                    {
                        if (dicShowImageQueue[imageInfoVO.Name].Count < 1)
                        {
                            Thread.Sleep(5);
                            continue;
                        }

                        HyImageInfo showImage = dicShowImageQueue[imageInfoVO.Name].Dequeue();
                        GetSrcImage?.Invoke(showImage);//屏蔽显示
                        showImage.Dispose();
                    }
                    catch (Exception ex)
                    {
                        log.Error(new TaskServerLogMessage(taskName, imageInfoVO.Name, A_Run, R_Fail, $"显示图像失败，原因：{ex.Message}"));
                    }

                    Thread.Sleep(5);
                }
            })
            { IsBackground = true }.Start();
            return ErrorCodeConst.OK;
        }

        /// <summary>
        /// 不同的拍照，在各自的线程中存图
        /// </summary>
        /// <param name="imageInfoVO">当前拍照的图像设定</param>
        /// <returns></returns>
        public int SaveImage(AcquireImageInfoVO imageInfoVO)
        {
            //add by Luodian @ 20211129 存图时存成JGP，需要保留图像的完整信息，需要添加存图的参数，这里把参数声明放到循环外面，节省一点时间，理论上放外部最好
            ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(a => a.FormatID == ImageFormat.Jpeg.Guid);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, (long)75);
            encoderParameters.Param[0] = encoderParameter;

            new Thread(() =>
            {
                Stopwatch watch = new Stopwatch();
                while (runningToken)
                {
                    try
                    {
                        if (dicSaveImageQueue[imageInfoVO.Name].Count < 1)
                        {
                            Thread.Sleep(5);
                            continue;
                        }

                        HyImageInfo saveImage = dicSaveImageQueue[imageInfoVO.Name].Dequeue();

                        if (dicImageNameQueue[imageInfoVO.Name].Count < 1)
                        {
                            saveImage.Dispose();
                            Thread.Sleep(5);
                            continue;
                        }

                        string strFileNames = dicImageNameQueue[imageInfoVO.Name].Dequeue();
                        if (string.IsNullOrEmpty(strFileNames) || !strFileNames.Contains(@"\"))
                        {
                            saveImage.Dispose();
                            continue;
                        }

                        string[] arrFileName = strFileNames.Split("&*".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (arrFileName == null || arrFileName.Length < 1)
                        {
                            saveImage.Dispose();
                            continue;
                        }
                        foreach (string fileName in arrFileName)
                        {
                            if (string.IsNullOrEmpty(fileName) || !fileName.Contains(@"\"))
                            {
                                saveImage.Dispose();
                                continue;
                            }

                            #region 存图的方式
                            string strSaveFolderPath = fileName.Substring(0, fileName.LastIndexOf(@"\"));
                            if (!Directory.Exists(strSaveFolderPath))
                                Directory.CreateDirectory(strSaveFolderPath);

                            //log.Info($"存图开始：{strProductType}.{strSN}.CosmeticAOI.{strDateTime}.{strInspectPosName}.{strLightName}.{strCameraNo}_{strGrabPosNo}.jpg");
                            watch.Reset();
                            watch.Start();
                            //saveImage.Bitmap.Save(fileName, ImageFormat.Jpeg);
                            saveImage.Bitmap.Save(fileName, imageCodecInfo, encoderParameters);
                            watch.Stop();
                            log.Info($"【CT统计】任务 [{taskName}] 的拍照 [{imageInfoVO.Name}] 图像【{fileName.Substring(fileName.LastIndexOf(@"\") + 1, fileName.Length - (fileName.LastIndexOf(@"\") + 1))}】的存图完成！耗时：{watch.ElapsedMilliseconds}ms");
                            //log.Info($"存图结束：{strProductType}.{strSN}.CosmeticAOI.{strDateTime}.{strInspectPosName}.{strLightName}.{strCameraNo}_{strGrabPosNo}.jpg");
                            #endregion

                            #region 存图备份图，防止误删
                            //string bakFileName = fileName.Replace(saveImageFolder, bakImageFolder);
                            //string bakFolderPath = bakFileName.Substring(0, fileName.LastIndexOf(@"\"));
                            //if (!Directory.Exists(bakFolderPath))
                            //    Directory.CreateDirectory(bakFolderPath);
                            //watch.Reset();
                            //watch.Start();
                            //// saveImage.Bitmap.Save(bakFileName.Replace(".jpg", ".bmp"), ImageFormat.Bmp);
                            //saveImage.Bitmap.Save(bakFileName, imageCodecInfo, encoderParameters);
                            //watch.Stop();
                            //log.Info($"【CT统计】任务 [{taskName}] 的拍照 [{imageInfoVO.Name}] 图像【{fileName.Substring(fileName.LastIndexOf(@"\") + 1, fileName.Length - (fileName.LastIndexOf(@"\") + 1))}】的存备份图完成！耗时：{watch.ElapsedMilliseconds}ms");
                            #endregion
                        }

                        saveImage.Dispose();
                    }
                    catch (Exception ex)
                    {
                        log.Error(new TaskServerLogMessage(taskName, imageInfoVO.Name, A_Run, R_Fail, $"存图失败，原因：{ex.Message}"));
                    }

                    Thread.Sleep(5);
                }
            })
            { IsBackground = true }.Start();
            return ErrorCodeConst.OK;
        }

        /// <summary>
        /// 不同的拍照，在各自的线程中运行任务
        /// </summary>
        /// <param name="imageInfoVO">当前拍照的图像设定</param>
        /// <returns></returns>
        public int RunToolBlock(AcquireImageInfoVO imageInfoVO)
        {
            new Thread(() =>
            {
                Stopwatch watch = new Stopwatch();
                while (runningToken)
                {
                    try
                    {
                        if (dicRunToolBlockQueue[imageInfoVO.Name].Count < 1)
                        {
                            Thread.Sleep(5);
                            continue;
                        }

                        if (dicParamsQueue[imageInfoVO.Name].Count < 1)
                        {
                            Thread.Sleep(5);
                            continue;
                        }

                        if (dicSubNameQueue[imageInfoVO.Name].Count < 1)
                        {
                            Thread.Sleep(5);
                            continue;
                        }

                        HyImageInfo toolBlockImage = dicRunToolBlockQueue[imageInfoVO.Name].Dequeue();
                        IEnumerable<(string Name, object Value)> @Params = dicParamsQueue[imageInfoVO.Name].Dequeue();
                        string subName = dicSubNameQueue[imageInfoVO.Name].Dequeue();

                        //运行ToolBlock
                        watch.Reset();
                        watch.Start();
                        //因为不同的拍照，已经分开在不同的线程，不同的队列中执行任务了，这里只按钮指令A的方式执行，指令AR的控制指令A的回调中，把Output输出，然后Task任务运行中只需要等队列输出值即可
                        RunToolBlockByCmdA(toolBlockImage, @Params, subName);
                        watch.Stop();
                        log?.Info($"【CT统计】任务 [{taskName}] 的拍照 [{imageInfoVO.Name}] 的ToolBlock执行完成! 耗时：{watch.ElapsedMilliseconds}ms");
                    }
                    catch (Exception ex)
                    {
                        log.Error(new TaskServerLogMessage(taskName, imageInfoVO.Name, A_Run, R_Fail, $"运行ToolBlock失败，原因：{ex.Message}"));
                    }

                    Thread.Sleep(5);
                }
            })
            { IsBackground = true }.Start();
            return ErrorCodeConst.OK;
        }

        public int ResetQueue()
        {
            try
            {
                if (dicShowImageQueue.Count > 0)
                {
                    foreach (Queue<HyImageInfo> queue in dicShowImageQueue.Values)
                    {
                        while (queue.Count > 0)
                        {
                            HyImageInfo hyImage = queue.Dequeue();
                            hyImage.Dispose();
                        }
                        queue.Clear();
                    }
                    dicShowImageQueue.Clear();
                }

                if (dicSaveImageQueue.Count > 0)
                {
                    foreach (Queue<HyImageInfo> queue in dicSaveImageQueue.Values)
                    {
                        while (queue.Count > 0)
                        {
                            HyImageInfo hyImage = queue.Dequeue();
                            hyImage.Dispose();
                        }
                        queue.Clear();
                    }
                    dicSaveImageQueue.Clear();
                }

                if (dicRunToolBlockQueue.Count > 0)
                {
                    foreach (Queue<HyImageInfo> queue in dicRunToolBlockQueue.Values)
                    {
                        while (queue.Count > 0)
                        {
                            HyImageInfo hyImage = queue.Dequeue();
                            hyImage.Dispose();
                        }
                        queue.Clear();
                    }
                    dicRunToolBlockQueue.Clear();
                }

                dicImageNameQueue.Clear();
                dicParamsQueue.Clear();
                dicSubNameQueue.Clear();
                dicCmdIdQueue.Clear();
                dicOutputValQueue.Clear();
            }
            catch (Exception ex)
            {
                return ErrorCodeConst.TaskStartFail;
            }
            return ErrorCodeConst.OK;
        }
        #endregion
    }
}
