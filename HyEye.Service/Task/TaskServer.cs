using Autofac;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using VisionFactory;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    public interface ITaskService : ICheckable
    {
        event EventHandler<SendCmdsEventArgs> SendCommands;

        event Action StateChanged;

        bool Running { get; }

        bool TaskRunnerStateRun { get; }

        void Start();

        void Close();

        void RunTask(string taskName);

        //add by LuoDian @ 20210824 用于离线批量跑任务
        void RunTaskForBatchRunOffline(string taskName);

        void RunAcqImage(string taskName, string acqImageName);

        IReplyCommand[] RunCommands(ReceiveCommand recvCmd);

        IReplyCommand RunProcess(ProcessCommand cmd);
    }

    public class TaskService : ITaskService
    {
        public event EventHandler<SendCmdsEventArgs> SendCommands;

        public event Action StateChanged;

        readonly ITaskRepository taskRepo;
        readonly IImageSaveRepository imageSaveRepo;
        readonly IDisplayLayoutRepository displayLayoutRepo;
        readonly INameMappingRepository nameMappingRepo;
        readonly IRecordShowRepository recordShowRepo;
        readonly ICommandRepository commandRepo;

        readonly ICommandService commandService;
        readonly IScriptService scriptService;
        readonly IImageService imageService;
        readonly IGLog log;

        //add by LuoDian @ 20211119 为了适应多个相机共用同一个光源的情况，需要在这里获取每次拍照的光源信息，来判断这个光源被哪几个相机控制
        readonly IOpticsRepository opticsRepo;
        //add by LuoDian @ 20211126 在这里添加了获取光源配置的逻辑之后，需要判断下是否是离线模式，在离线模式下不能获取光源配置
        readonly ISimulationRepository simulationRepo;

        readonly AcqScheduler acqScheduler;
        readonly ToolBlockComponentSet toolBlockComponentSet;
        readonly DisplayComponentSet displayComponentSet;

        List<ImageSaveInfoVO> saveInfos;

        //add by LuoDian @ 20211127 为了测试拍照时同步进行像素格式转换，为了防止报错，添加一个循环等待，这里指示系统退出的时候退出循环
        private bool isExit = false;


        // key: TaskName
        readonly Dictionary<string, TaskRunner> taskRunners = new Dictionary<string, TaskRunner>();

        public TaskService(
            ITaskRepository taskRepo,
            IImageSaveRepository imageSaveRepo,
            IDisplayLayoutRepository displayLayoutRepo,
            INameMappingRepository nameMappingRepo,
            ICommandService commandService,
            IScriptService scriptService,
            IImageService imageService,
            IRecordShowRepository recordShowRepo,
            ICommandRepository commandRepo,
            IGLog log,
            IOpticsRepository opticsRepo,
            ISimulationRepository simulationRepo,
            AcqScheduler acqScheduler,
            ToolBlockComponentSet toolBlockComponentSet,
            DisplayComponentSet displayComponentSet)
        {
            this.taskRepo = taskRepo;
            this.imageSaveRepo = imageSaveRepo;
            this.displayLayoutRepo = displayLayoutRepo;
            this.nameMappingRepo = nameMappingRepo;
            this.commandService = commandService;
            this.scriptService = scriptService;
            this.imageService = imageService;
            this.log = log;
            this.acqScheduler = acqScheduler;
            this.toolBlockComponentSet = toolBlockComponentSet;
            this.displayComponentSet = displayComponentSet;
            this.recordShowRepo = recordShowRepo;
            this.commandRepo = commandRepo;

            //add by LuoDian @ 20211119 为了适应多个相机共用同一个光源的情况，需要在这里获取每次拍照的光源信息，来判断这个光源被哪几个相机控制
            this.opticsRepo = opticsRepo;
            //add by LuoDian @ 20211126 在这里添加了获取光源配置的逻辑之后，需要判断下是否是离线模式，在离线模式下不能获取光源配置
            this.simulationRepo = simulationRepo;
        }

        //add by LuoDian @ 20211127 为了测试拍照时同步进行像素格式转换，为了防止报错，添加一个循环等待，这里指示系统退出的时候退出循环
        ~TaskService()
        {
            isExit = true;
        }

        public bool Check()
        {
            bool result = true;

            List<TaskInfoVO> taskInfos = taskRepo.GetTasks();
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                TaskRunner taskRunner;
                if (taskRunners.ContainsKey(taskInfo.Name))
                {
                    taskRunner = taskRunners[taskInfo.Name];
                }
                else
                {
                    taskRunner = CreateTaskRunner(taskInfo);
                    taskRunners.Add(taskInfo.Name, taskRunner);
                }

                result &= taskRunner.Check();
                //出现了一次false就不再往下检查了
                if (!result)
                    return result;
            }

            return result;
        }

        // 存图有 2 个问题
        // 1：OK 还是 NG，要 ToolBlock 运行完成才知道
        // 2：SN 要 ToolBlock 运行完成才知道
        // 所以原图只能缓存，等结果图出来再保存

        ConcurrentDictionary<string, HyImageInfo> srcImageCache = new ConcurrentDictionary<string, HyImageInfo>();

        // 获取到原图
        private void TaskRunner_GetSrcImage(HyImageInfo hyImage)
        {
            if (hyImage.Bitmap == null) return;

            DisplayLayoutInfoVO layout = displayLayoutRepo.GetLayout(hyImage.TaskName, hyImage.AcqOrCalibName);
            if (layout != null && layout.Index >= 0)
            {
                //不能开线程，可能导致结果图保存错误
                try
                {
                    Bitmap copy = (Bitmap)hyImage.Bitmap.Clone();
                    //Bitmap copy = hyImage.Bitmap;
                    // 每次拍照都对应一个 DisplayImageComponent，所以一个 Runner 要对应多个 DisplayImage
                    IDisplayTaskImageComponent displayComponent = displayComponentSet.GetDisplayControl(hyImage.TaskName, hyImage.AcqOrCalibName);
                    displayComponent.ShowImage(copy, false);
                }
                catch (Exception e)
                {
                    log.Error(new TaskServerLogMessage(hyImage.TaskName, hyImage.AcqOrCalibName, A_ShowImage, R_Fail, e.Message));
                }
            }

            string key = hyImage.TaskName + hyImage.AcqOrCalibName;
            srcImageCache[key] = hyImage;
        }

        // ToolBlock 运行完成，显示结果图
        private void ToolBlockComponent_Ran(object sender, ToolBlockRanEventArgs e)
        {
            ImageSaveInfoVO saveInfo = saveInfos?.FirstOrDefault(a => a.TaskName == e.TaskName);

            DisplayLayoutInfoVO layout = displayLayoutRepo.GetLayout(e.TaskName, e.AcqImageName);

            if (layout != null && layout.Index >= 0 && displayLayoutRepo.ShowRetImage)
            {
                IDisplayTaskImageComponent displayControl = displayComponentSet.GetDisplayControl(e.TaskName, e.AcqImageName);

                displayControl.ClearGraphic();
                foreach (var val in e.Outputs.Values)
                {
                    if (val != null)
                    {
                        displayControl.ShowGraphic(val);
                    }
                }
            }


            //RecordShowInfoVO recordShowInfo = recordShowRepo.GetRecordShow(e.TaskName);

            //if (//(saveInfo != null && saveInfo.ResultSaveMode != ImageSaveMode.None) ||
            //    (layout != null && layout.Index >= 0 && displayLayoutRepo.ShowRetImage))
            //{
            //    IDisplayTaskImageComponent displayControl = displayComponentSet.GetDisplayControl(e.TaskName, e.AcqImageName);

            //TimeSpan ts = FuncWatch.ElapsedTime(() =>
            //{
            //    IToolBlockComponent toolBlock = (IToolBlockComponent)sender;
            //    //默认显示0序号
            //    int showIndex = recordShowInfo == null ? 0 : recordShowInfo.RecordIndex;
            //    displayControl.ShowGraphic(toolBlock.CreateRecord(showIndex));
            //});
            //log.Debug(new TaskServerLogMessage(e.TaskName, e.AcqImageName, A_ShowGraphic, R_Success, null, ts.TotalMilliseconds));
            //}
        }

        /// <summary>
        /// 保存源图
        /// add by LuoDian @ 20220203 把保存源图和保存结果图分开，源图的保存在拍照完成的时候就可以做
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="acqImageName"></param>
        /// <param name="acqImageIndex"></param>
        /// <param name="outputs"></param>
        private void TaskRunner_SaveSrcImage(string taskName, string acqImageName, string sn)
        {
            ImageSaveInfoVO saveInfo = saveInfos?.FirstOrDefault(a => a.TaskName == taskName);
            if (saveInfo == null)
            {
                saveInfo = new ImageSaveInfoVO { TaskName = taskName };
            }

            // 如果同一条指令不间断狂发，这里是有可能报错的，并发的问题

            //add by LuoDian @ 20211127 为了测试拍照时同步进行像素格式转换，为了防止报错，添加一个循环等待
            while (true)
            {
                if (isExit)
                    return;

                if (srcImageCache.ContainsKey(taskName + acqImageName))
                    break;
                Thread.Sleep(5);
            }

            HyImageInfo srcHyImage = srcImageCache[taskName + acqImageName];

            srcHyImage.SN = sn;

            // 存原图
            if (saveInfo.NeedSave(true, ImageFlag.OK))
            {
                //update by LuoDian @ 20211104 需要先判断下Bitmap对象是否为null
                imageService?.SaveImage(srcHyImage, true, false, ImageFlag.OK);
            }
            else
            {
                //update by LuoDian @ 20211104 在释放前需要先判断下Bitmap对象是否为null
                srcHyImage?.Dispose();
            }

        }

        /// <summary>
        /// 保存结果图
        /// update by LuoDian @ 20220203 把保存源图和保存结果图分开后，这里只保存结果图，在保存结果图的时候，需要获取源图HyImageInfo对象的部分信息，所以在这里获取完HyImageInfo对象之后，再释放字典中的对象
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="acqImageName"></param>
        /// <param name="acqImageIndex"></param>
        /// <param name="outputs"></param>
        private void TaskRunner_CompletedSingleAcqImage(string taskName, string acqImageName, int acqImageIndex, LinkedDictionary<string, object> outputs)
        {
            ImageFlag flag;
            if (outputs.ContainsKey(InputOutputConst.Output_ErrorCode) && outputs[InputOutputConst.Output_ErrorCode] is int errcode && errcode == 0)
            {
                flag = ImageFlag.OK;
            }
            else
            {
                flag = ImageFlag.NG;
            }

            ImageSaveInfoVO saveInfo = saveInfos?.FirstOrDefault(a => a.TaskName == taskName);
            if (saveInfo == null)
            {
                saveInfo = new ImageSaveInfoVO { TaskName = taskName };
            }

            // 如果同一条指令不间断狂发，这里是有可能报错的，并发的问题

            //add by LuoDian @ 20211127 为了测试拍照时同步进行像素格式转换，为了防止报错，添加一个循环等待
            while (true)
            {
                if (isExit)
                    return;

                if (srcImageCache.ContainsKey(taskName + acqImageName))
                    break;
                Thread.Sleep(5);
            }
            HyImageInfo srcHyImage = srcImageCache[taskName + acqImageName];
            srcImageCache[taskName + acqImageName] = null;

            //存结果图
            Bitmap retBmp = null;
            if (saveInfo.NeedSave(false, flag))
            {
                IDisplayTaskImageComponent displayControl = displayComponentSet.GetDisplayControl(taskName, acqImageName);

                TimeSpan ts;
                (ts, retBmp) = FuncWatch.ElapsedTime(() =>
                {
                    return displayControl.CreateContentBitmap();
                });
                log.Debug(new TaskServerLogMessage(taskName, acqImageName, A_CreateRetImage, R_Success, null, ts.TotalMilliseconds));

                HyImageInfo hyImage = new HyImageInfo
                {
                    Bitmap = retBmp,
                    IsGrey = false,
                    FrameNum = srcHyImage.FrameNum,
                    Timestamp = srcHyImage.Timestamp,
                    CmdID = srcHyImage.CmdID,
                    TaskName = taskName,
                    AcqOrCalibName = acqImageName,
                    AcqOrCalibIndex = acqImageIndex,
                    SN = srcHyImage.SN
                };

                imageService.SaveImage(hyImage, false, false, flag);
            }
        }

        public bool Running { get; private set; } = false;

        public void Start()
        {
            acqScheduler.Init();
            scriptService.Init();
            commandService.Init();

            saveInfos = imageSaveRepo.GetSaveInfos();

            List<TaskInfoVO> taskInfos = taskRepo.GetTasks();
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                TaskRunner taskRunner;
                if (taskRunners.ContainsKey(taskInfo.Name))
                {
                    taskRunner = taskRunners[taskInfo.Name];
                }
                else
                {
                    taskRunner = CreateTaskRunner(taskInfo);
                    taskRunners.Add(taskInfo.Name, taskRunner);
                }
                taskRunner.Start();
            }

            Running = true;

            log.Info(new TaskServerLogMessage("任务服务", null, A_Start, R_Success));
        }

        TaskRunner CreateTaskRunner(TaskInfoVO taskInfo)
        {
            IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(taskInfo.Name);
            //不能用这个事件存图，这个事件在脚本之前执行，无法获得脚本里处理后的一些数据
            // Modified by Louis on Aug. 3 2022 暂时注释结果图显示
            // toolBlockComponent.Ran += ToolBlockComponent_Ran;

            TaskRunner taskRunner = AutoFacContainer.Resolve<TaskRunner>(
                new NamedParameter("taskName", taskInfo.Name),
                new NamedParameter("toolBlock", toolBlockComponent)
            );

            taskRunner.GetSrcImage += TaskRunner_GetSrcImage;
            taskRunner.CompletedSingleAcqImage += TaskRunner_CompletedSingleAcqImage;
            taskRunner.SendCommands += TaskRunner_SendCommands;

            //add by LuoDian @ 20220203 把保存源图和保存结果图分开，源图的保存在拍照完成的时候就可以做
            taskRunner.SaveSrcImage += TaskRunner_SaveSrcImage;

            return taskRunner;
        }

        private void TaskRunner_SendCommands(object sender, SendCmdsEventArgs e)
        {
            SendCommands?.Invoke(sender, e);
        }

        public bool TaskRunnerStateRun { get; private set; } = true;

        public void Close()
        {
            if (!Running) return;

            foreach (TaskRunner runner in taskRunners.Values)
            {
                //update by LuoDian @ 20211111 出现过TaskRunner的TaskInfo是null的情况，所以要加这个防呆
                if (runner != null && runner.TaskInfo != null && runner.TaskInfo.Name != null)
                {
                    IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(runner.TaskInfo.Name);
                    // Modified by Louis on Aug. 3 2022 暂时注释结果图显示
                    // toolBlockComponent.Ran -= ToolBlockComponent_Ran;
                }

                runner.Stop();
                runner.GetSrcImage -= TaskRunner_GetSrcImage;
                runner.CompletedSingleAcqImage -= TaskRunner_CompletedSingleAcqImage;

                //add by LuoDian @ 20220203 把保存源图和保存结果图分开，源图的保存在拍照完成的时候就可以做
                runner.SaveSrcImage -= TaskRunner_SaveSrcImage;
            }
            taskRunners.Clear();

            srcImageCache.Clear();

            scriptService.Clear();

            Running = false;

            log.Info(new TaskServerLogMessage("任务服务", null, A_Stop, R_Success));
        }

        public void RunTask(string taskName)
        {
            TaskRunner runner = GetTaskRunner(taskName);
            if (!runner.Check()) return;

            saveInfos = imageSaveRepo.GetSaveInfos();

            scriptService.Init();

            runner.Start(true);
            runner.RunTask();
            runner.Stop();
        }

        /// <summary>
        /// add by LuoDian @ 20210824 用于离线批量跑任务
        /// </summary>
        /// <param name="taskName"></param>
        public void RunTaskForBatchRunOffline(string taskName)
        {
            TaskRunner runner = GetTaskRunner(taskName);
            if (!runner.Check()) return;

            saveInfos = imageSaveRepo.GetSaveInfos();

            scriptService.Init();

            runner.Start(true);
            runner.BatchRunOffline();
            runner.Stop();
        }

        public void RunAcqImage(string taskName, string acqImageName)
        {
            TaskRunner runner = GetTaskRunner(taskName);
            if (!runner.Check()) return;

            saveInfos = imageSaveRepo.GetSaveInfos();

            scriptService.Init();

            runner.Start(true);
            runner.RunAcqImage(acqImageName);
            runner.Stop();
        }

        TaskRunner GetTaskRunner(string taskName)
        {
            TaskRunner runner;
            if (taskRunners.ContainsKey(taskName))
            {
                runner = taskRunners[taskName];
            }
            else
            {
                TaskInfoVO taskInfo = taskRepo.GetTaskByName(taskName);
                runner = CreateTaskRunner(taskInfo);
                taskRunners.Add(taskInfo.Name, runner);
            }

            return runner;
        }

        #region 指令运行

        public IReplyCommand[] RunCommands(ReceiveCommand recvCmd)
        {
            if (!Running)
            {
                log.Error(new TaskServerLogMessage("任务服务", null, A_RunCmd, R_Fail, "服务未启动"));
                return new IReplyCommand[] { ErrorCommand.Create(ErrorCodeConst.ServiceNotStarted) };
            }



            #region add by LuoDian @ 20211118 为了适应多个相机共用同一个光源的情况，这里需要找出每个相机的每次拍照与其它相机是否共用一个光源，是的话，要共用一个计数对象
            Dictionary<(string, int), CountdownEvent> dicCountdownEvent = new Dictionary<(string, int), CountdownEvent>();
            Dictionary<(string, int), CountdownEvent> dicOpenLightCountdownEvent = new Dictionary<(string, int), CountdownEvent>();

            if (!simulationRepo.Enabled)
            {
                foreach (TaskCommand curCommand in recvCmd.TaskCommands)
                {
                    if (dicCountdownEvent.ContainsKey((curCommand.TaskName, curCommand.AcqOrCalibIndex)))
                        continue;

                    //var @params = curCommand.FieldValues
                    //                        .Where(c => c.Use == CommandFieldUse.SaveImage)
                    //                        .Select(a => (a.Name, a.Value));

                    var @params = curCommand.FieldValues
                                           .Where(c => c.Use == CommandFieldUse.ToolBlock)
                                           .Select(a => (a.Name, a.Value));
                    string strSubName = "";
                    foreach (var (Name, Value) in @params)
                    {
                        if (Name.Equals("SubName") && Value != null)
                        {
                            strSubName = Value.ToString();
                            break;
                        }
                    }

                    OpticsInfoVO opticsInfo = opticsRepo.GetOptics(curCommand.TaskName, curCommand.AcqOrCalibName, null, strSubName);
                    if (opticsInfo == null)
                    {
                        log?.Error(ErrorCommand.Create(ErrorCodeConst.GetOpticsFail).ErrMsg);
                        return new IReplyCommand[] { ErrorCommand.Create(ErrorCodeConst.GetOpticsFail) };
                    }

                    var otherCommands = ((List<TaskCommand>)recvCmd.TaskCommands.FindAll(t => !t.TaskName.Equals(curCommand.TaskName) && t.AcqOrCalibIndex == curCommand.AcqOrCalibIndex));

                    if (otherCommands == null || ((List<TaskCommand>)otherCommands).Count < 1)
                    {
                        //这里添加没有其它相机或是其它相机没有当前拍照Index的逻辑，new一个计数对象放到字典里面去就行
                        dicCountdownEvent[(curCommand.TaskName, curCommand.AcqOrCalibIndex)] = new CountdownEvent(1);
                        dicOpenLightCountdownEvent[(curCommand.TaskName, curCommand.AcqOrCalibIndex)] = new CountdownEvent(1);
                        continue;
                    }

                    //这里不考虑多相机交叉共用光源的情况, 如果有交叉使用光源的，就认为所有相机共用同一个光源
                    List<(string, int)> lstOtherCommandUseSammerLight = new List<(string, int)>();
                    List<TaskCommand> lstOtherCommands = (List<TaskCommand>)otherCommands;
                    foreach (LightControllerValueInfoVO lightInfo in opticsInfo.LightControllerValues)
                    {
                        foreach (ChannelValue channel in lightInfo.ChannelValues)
                        {
                            foreach (TaskCommand command in lstOtherCommands)
                            {
                                OpticsInfoVO otherOpticsInfo = opticsRepo.GetOptics(command.TaskName, command.AcqOrCalibName, null, strSubName);
                                LightControllerValueInfoVO otherLightInfo = otherOpticsInfo.LightControllerValues.Find(t => t.LightControllerName.Equals(lightInfo.LightControllerName) && t.ChannelValues.FindAll(c => c.ChannelIndex == channel.ChannelIndex) != null && t.ChannelValues.FindAll(c => c.ChannelIndex == channel.ChannelIndex).Count > 0);
                                if (otherLightInfo != null && !lstOtherCommandUseSammerLight.Contains((command.TaskName, command.AcqOrCalibIndex)))
                                    lstOtherCommandUseSammerLight.Add((command.TaskName, command.AcqOrCalibIndex));
                            }
                        }
                    }

                    //确保几个相机共用同一个光源的时候，几个相机对象是共用同一个计数器
                    CountdownEvent curCountdownEvent = new CountdownEvent(1 + lstOtherCommandUseSammerLight.Count);
                    CountdownEvent curCountdownEvent2 = new CountdownEvent(1 + lstOtherCommandUseSammerLight.Count);
                    if (dicCountdownEvent.ContainsKey((curCommand.TaskName, curCommand.AcqOrCalibIndex)))
                    {
                        curCountdownEvent = dicCountdownEvent[(curCommand.TaskName, curCommand.AcqOrCalibIndex)];
                        curCountdownEvent2 = dicOpenLightCountdownEvent[(curCommand.TaskName, curCommand.AcqOrCalibIndex)];
                    }

                    foreach ((string, int) key in lstOtherCommandUseSammerLight)
                    {
                        if (dicCountdownEvent.ContainsKey(key) && dicCountdownEvent[key] != null)
                        {
                            curCountdownEvent = dicCountdownEvent[key];
                            curCountdownEvent2 = dicOpenLightCountdownEvent[key];
                            break;
                        }
                    }

                    //有可能其它相机还跟另外的相机共用其它光源，这时要把其它相机的计数标准数量添加进来
                    if (curCountdownEvent.InitialCount < lstOtherCommandUseSammerLight.Count + 1)
                    {
                        curCountdownEvent.AddCount((lstOtherCommandUseSammerLight.Count + 1) - curCountdownEvent.InitialCount);
                        curCountdownEvent2.AddCount((lstOtherCommandUseSammerLight.Count + 1) - curCountdownEvent2.InitialCount);
                    }

                    dicCountdownEvent[(curCommand.TaskName, curCommand.AcqOrCalibIndex)] = curCountdownEvent;
                    dicOpenLightCountdownEvent[(curCommand.TaskName, curCommand.AcqOrCalibIndex)] = curCountdownEvent2;
                }

                //foreach ((string, int) key in dicCountdownEvent.Keys)
                //{
                //    log?.Info($"任务[{key.Item1}]的拍照[{key.Item2}]共有 {dicCountdownEvent[key]?.InitialCount} 个相机共用同一个光源！");
                //}
            }
            #endregion



            var lookup = recvCmd.TaskCommands.ToLookup(k => k.TaskName);

            CountdownEvent countdown = new CountdownEvent(lookup.Count);
            ConcurrentQueue<IReplyCommand> replyCmdQueue = new ConcurrentQueue<IReplyCommand>();

            //add by LuoDian @ 20220112 用于统计所有任务执行完成的CT耗时
            Stopwatch allTaskExcuteWatch = new Stopwatch();
            allTaskExcuteWatch.Start();
            foreach (var cmdsGroup in lookup)
            {
                //update by LuoDian @ 20220113 当线程开销大的时候，线程池效率会比较低，改成原始的Thread
                TaskCommand[] commands = new TaskCommand[cmdsGroup.ToList().Count];
                cmdsGroup.ToList().CopyTo(commands);
                new System.Threading.Tasks.Task(() =>
                {
                    string taskName = commands[0].TaskName;

                    TaskRunner runner = taskRunners[taskName];
                    List<IReplyCommand> replyCmds = runner.RunCommands(recvCmd.Index, commands.ToList(), dicCountdownEvent, dicOpenLightCountdownEvent);

                    if (replyCmds != null)
                    {
                        foreach (IReplyCommand cmd in replyCmds)
                        {
                            replyCmdQueue.Enqueue(cmd);
                        }
                    }

                    countdown.Signal();
                }).Start();
                //ThreadPool.QueueUserWorkItem((objCmds =>
                //{
                //    List<TaskCommand> commands = (List<TaskCommand>)objCmds;
                //    string taskName = commands[0].TaskName;

                //    TaskRunner runner = taskRunners[taskName];
                //    List<IReplyCommand> replyCmds = runner.RunCommands(recvCmd.Index, commands, dicCountdownEvent, dicOpenLightCountdownEvent);

                //    if (replyCmds != null)
                //    {
                //        foreach (IReplyCommand cmd in replyCmds)
                //        {
                //            replyCmdQueue.Enqueue(cmd);
                //        }
                //    }

                //    countdown.Signal();
                //}), cmdsGroup.ToList());
            }
            countdown.Wait();
            countdown.Dispose();

            allTaskExcuteWatch.Stop();
            log?.Info($"【CT统计】本次收到的指令中所有共计 {lookup.Count} 个任务的指令已全部执行完成！耗时：{allTaskExcuteWatch.ElapsedMilliseconds}");

            return replyCmdQueue.ToArray();
        }

        #endregion

        #region 运行流程指令

        public IReplyCommand RunProcess(ProcessCommand cmd)
        {
            if (!Running)
            {
                log.Info(new TaskServerLogMessage("任务服务", null, A_RunCmd, R_Fail, "服务未启动"));
                return ErrorCommand.Create(ErrorCodeConst.ServiceNotStarted);
            }

            if (cmd.Type == ProcessCommandType.S)
                TaskRunnerStateRun = false;
            if (cmd.Type == ProcessCommandType.R || cmd.Type == ProcessCommandType.RE)
                TaskRunnerStateRun = true;

            StateChanged?.Invoke();

            foreach (TaskRunner runner in taskRunners.Values)
            {
                runner.RunProcessCommand(cmd);
            }

            return null;
        }

        #endregion
    }

}
