using CameraSDK;
using CameraSDK.Models;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using LightControllerSDK;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    // 在图像缓存状态下，如果复用相机，常规条件无法排除取像是否是本次任务的
    // 所以只能做开关来判断
    // 要求复用相机情况下，复用相机的任务一定不能并发，否则必出错

    public interface IAcquireImage
    {
        event EventHandler Confirm;

        bool CheckCamera(string taskName);

        bool CheckCamera(TaskInfoVO taskInfo);

        void Start(string taskName, bool initScheduler = false);

        void Start(TaskInfoVO taskInfo, bool initScheduler = false);

        void Close();

        /// <summary>
        /// 开始取像
        /// </summary>
        /// <param name="cmdIndex">指令索引</param>
        /// <param name="count">取像次数</param>
        void BeginAcqImage(long cmdIndex, int count);

        /// <summary>
        /// 结束取像
        /// </summary>
        void EndAcqImage();

        /// <summary>
        /// 暂停
        /// <para>仅限相机取像</para>
        /// </summary>
        void Suspend();

        /// <summary>
        /// 恢复
        /// </summary>
        void Resume();

        /// <summary>
        /// 从相机取像 update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
        /// </summary>
        int GetImageFromCamera(out CameraImage cameraImage);

        /// <summary>
        /// 相机采图前的触发设置，包含软触发和硬触发
        /// add by LuoDian @ 20220111 为了提升效率，需要把触发和获取图像数据分开，触发完成即可给上位机返回响应信号
        /// </summary>
        /// <returns></returns>
        int Trigger(OpticsInfoVO opticsInfo, bool isSetLightController = true);

        /// <summary>
        /// add by LuoDian @ 20211118 当多个相机共用同一个光源的时候，需要在执行Task指令的时候判断下共有哪些相机共用这一个光源
        /// </summary>
        /// <param name="lightControllerHandles"></param>
        /// <returns></returns>
        int SetLightController(List<LightControllerHandle> lightControllerHandles);

        /// <summary>
        /// add by LuoDian @ 20211118 当多个相机共用同一个光源的时候，这里需要添加一个抽象接口，供TaskRunner那里使用
        /// </summary>
        /// <param name="opticsInfo"></param>
        /// <returns></returns>
        AcquireImageParams GetAcqParams(OpticsInfoVO opticsInfo);

        /// <summary>
        /// add by LuoDian @ 20211119 当多个相机共用同一个光源的时候，在所有相机拍完照之后，需要在TaskRunner中关闭光源
        /// </summary>
        /// <param name="lightControllerHandles"></param>
        /// <returns></returns>
        int CloseLightController(List<LightControllerHandle> lightControllerHandles);

        /// <summary>
        /// 从本地图片取像
        /// </summary>
        int GetImageFromLocal(string name, out CameraImage cameraImage);

        /// <summary>
        /// add by LuoDian @ 20210824 用于离线批量跑任务
        /// </summary>
        /// <param name="name">产品名称</param>
        /// <param name="cameraImage"></param>
        /// <returns></returns>
        int GetImageFromLocalForBatchRunOffline(string name, out CameraImage cameraImage);

        /// <summary>
        /// 重置
        /// <para>如果正在取像，则强制停止，返回超时</para>
        /// <para>清空调度队列中的指令</para>
        /// <para>结束当前轮取像</para>
        /// <para>手眼标定中，使用本地取像时，重置图片索引</para>
        /// </summary>
        void Reset();
    }

    public class AcquireImage : IAcquireImage
    {
        readonly IControllerCollection controllerFactory;
        readonly ITaskRepository taskRepo;
        readonly ICameraRepository cameraRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ICommandRepository commandRepo;
        readonly IRunTimeRepository runTimeRepo;
        readonly ISimulationRepository simulationRepo;
        readonly IGLog log;
        readonly AcqScheduler scheduler;

        public event EventHandler Confirm;

        public AcquireImage(
            IControllerCollection controllerFactory,
            ITaskRepository taskRepo,
            ICameraRepository cameraRepo,
            IOpticsRepository opticsRepo,
            ICommandRepository commandRepo,
            IRunTimeRepository runTimeRepo,
            ISimulationRepository simulationRepo,
            AcqScheduler scheduler,
            IGLog log)
        {
            this.controllerFactory = controllerFactory;
            this.taskRepo = taskRepo;
            this.cameraRepo = cameraRepo;
            this.opticsRepo = opticsRepo;
            this.commandRepo = commandRepo;
            this.runTimeRepo = runTimeRepo;
            this.simulationRepo = simulationRepo;
            this.scheduler = scheduler;
            this.log = log;
        }

        #region

        internal TaskInfoVO taskInfo;
        AcquireImageFromCamera acqImageFromCamera;
        AcquireImageFromLocal acqImageFromLocal;

        public bool CheckCamera(string taskName)
        {
            if (!simulationRepo.Enabled)
            {
                TaskInfoVO taskInfo = taskRepo.GetTaskByName(taskName);

                return CheckCamera(taskInfo);
            }

            return true;
        }

        public bool CheckCamera(TaskInfoVO taskInfo)
        {
            if (!simulationRepo.Enabled)
            {
                if (taskInfo.CameraAcquireImage == null)
                {
                    log.Error(new AcqImageLogMessage(taskInfo.Name, A_Check, R_Fail, "任务未添加相机"));
                    return false;
                }

                if (cameraRepo.IsVirtualCamera(taskInfo.CameraAcquireImage.CameraSN))
                {
                    if (MessageBoxUtils.ShowQuestion("虚拟相机无法在线取像，是否切换到离线模式？") == DialogResult.Yes)
                    {
                        simulationRepo.Enabled = true;
                        return true;
                    }
                    else
                    {
                        log.Error(new AcqImageLogMessage(taskInfo.Name, A_Check, R_Fail, "虚拟相机无法在线取像"));
                        return false;
                    }
                }

                CameraInfoVO cameraInfo = cameraRepo.GetCameraInfo(taskInfo.CameraAcquireImage.CameraSN);
                if (cameraInfo == null)
                {
                    log.Error(new AcqImageLogMessage(taskInfo.Name, A_Check, R_Fail, "任务未设置相机"));
                    return false;
                }

                ICamera camera = cameraRepo.GetCamera(taskInfo.CameraAcquireImage.CameraSN);
                if (camera == null)
                {
                    log.Error(new AcqImageLogMessage(taskInfo.Name, A_Check, R_Fail, "未找到相机"));
                    return false;
                }

                if (camera.IsOpen)
                {
                    // Modified by louis on January 25, 2022, solve the problem that the camera is not turned off due to abnormal code
                    camera.Close();
                }
            }

            return true;
        }

        public void Start(string taskName, bool initScheduler = false)
        {
            TaskInfoVO taskInfo = taskRepo.GetTaskByName(taskName);

            Start(taskInfo, initScheduler);
        }

        public void Start(TaskInfoVO taskInfo, bool initScheduler = false)
        {
            this.taskInfo = taskInfo;

            if (simulationRepo.Enabled)
            {
                if (acqImageFromLocal == null)
                    acqImageFromLocal = new AcquireImageFromLocal(simulationRepo, log);
            }
            else
            {
                if (cameraRepo.IsVirtualCamera(taskInfo.CameraAcquireImage.CameraSN))
                {
                    if (MessageBoxUtils.ShowQuestion("虚拟相机无法在线取像，是否切换到离线模式？") == DialogResult.Yes)
                    {
                        simulationRepo.Enabled = true;
                        if (acqImageFromLocal == null)
                            acqImageFromLocal = new AcquireImageFromLocal(simulationRepo, log);

                        return;
                    }
                    else
                    {
                        // 这里不想改了，直接抛错吧
                        throw new IgnoreException();
                    }
                }

                if (acqImageFromCamera == null)
                {
                    acqImageFromCamera = new AcquireImageFromCamera(taskInfo, controllerFactory, cameraRepo, opticsRepo, commandRepo, runTimeRepo, log);
                    acqImageFromCamera.Completed += AcqImageFromCamera_Completed;
                    acqImageFromCamera.Confirm += AcqImageFromCamera_Confirm;
                }
                else
                {
                    acqImageFromCamera.ResetTaskInfo(taskInfo);
                }
                acqImageFromCamera.Open();
            }

            if (initScheduler)
            {
                scheduler.InitOneTask(taskInfo);
            }
        }

        private void AcqImageFromCamera_Confirm(object sender, EventArgs e)
        {
            Confirm?.Invoke(this, e);
        }

        private void AcqImageFromCamera_Completed()
        {
            scheduler.Completed(taskInfo.CameraAcquireImage.CameraSN, this);
        }

        public void Close()
        {
            if (!simulationRepo.Enabled)
            {
                if (acqImageFromCamera != null)
                    acqImageFromCamera.Close();
            }
        }

        AutoResetEvent auto = new AutoResetEvent(false);
        AutoResetEvent auto1 = new AutoResetEvent(false);
        bool go = false;

        public void BeginAcqImage(long cmdIndex, int count)
        {
            if (!simulationRepo.Enabled)
            {
                bool idle = scheduler.Request(taskInfo.CameraAcquireImage.CameraSN, this);
                if (!idle)
                {
                    auto.WaitOne();
                }

                acqImageFromCamera?.BeginAcqImage(cmdIndex, count);

                if (go)
                {
                    go = false;
                    auto1.Set();
                }
            }
        }

        public void EndAcqImage()
        {
            if (!simulationRepo.Enabled)
                acqImageFromCamera?.EndAcqImage();
        }

        public void Suspend()
        {
            if (!simulationRepo.Enabled)
            {
                acqImageFromCamera.Suspend();
            }
        }

        public void Resume()
        {
            if (!simulationRepo.Enabled)
            {
                acqImageFromCamera.Resume();
            }
        }

        public void Reset()
        {
            if (simulationRepo.Enabled)
                acqImageFromLocal?.Reset();
            else
            {
                acqImageFromCamera?.Reset();
                scheduler?.Reset();
            }
        }

        internal void Go()
        {
            go = true;
            auto.Set();

            // 等待 BeginAcqImage 执行完成，确保取像事件已加载
            auto1.WaitOne();
        }

        public int GetImageFromCamera(out CameraImage cameraImage)
        {
            if (acqImageFromCamera == null)
            {
                cameraImage = null;
                return ErrorCodeConst.AcqImageError;
            }

            return acqImageFromCamera.GetImage(out cameraImage);
        }

        public int GetImageFromLocal(string name, out CameraImage cameraImage)
        {
            if (acqImageFromLocal == null)
            {
                cameraImage = null;
                return ErrorCodeConst.AcqImageError;
            }

            Bitmap bitmap = acqImageFromLocal.GetImage(taskInfo.Name, name);

            if (bitmap != null)
            {
                cameraImage = new CameraImage
                {
                    Bitmap = bitmap,
                    IsGrey = bitmap.PixelFormat == PixelFormat.Format8bppIndexed,
                    Timestamp = DateTime.Now
                };
                return ErrorCodeConst.OK;
            }
            else
            {
                cameraImage = null;
                return ErrorCodeConst.AcqImageError;
            }
        }

        /// <summary>
        /// add by LuoDian @ 20210824 用于离线批量跑任务
        /// </summary>
        /// <param name="name">产品名称</param>
        /// <param name="cameraImage"></param>
        /// <returns></returns>
        public int GetImageFromLocalForBatchRunOffline(string name, out CameraImage cameraImage)
        {
            if (acqImageFromLocal == null)
            {
                cameraImage = null;
                return ErrorCodeConst.AcqImageError;
            }

            Bitmap bitmap = acqImageFromLocal.GetImage($@"{taskInfo.Name}\Batch Run Offline", name);

            if (bitmap != null)
            {
                cameraImage = new CameraImage
                {
                    Bitmap = bitmap,
                    IsGrey = bitmap.PixelFormat == PixelFormat.Format8bppIndexed,
                };
                return ErrorCodeConst.OK;
            }
            else
            {
                cameraImage = null;
                return ErrorCodeConst.AcqImageError;
            }
        }

        //add by LuoDian @ 20211118 当多个相机共用同一个光源的时候，需要在执行Task指令的时候判断下共有哪些相机共用这一个光源
        public int SetLightController(List<LightControllerHandle> lightControllerHandles)
        {
            if (acqImageFromCamera == null)
                return ErrorCodeConst.LightOperationError;
            return acqImageFromCamera.SetLightController(lightControllerHandles);
        }

        //add by LuoDian @ 20211118 当多个相机共用同一个光源的时候，这里需要添加一个接口，供TaskRunner那里使用
        public AcquireImageParams GetAcqParams(OpticsInfoVO opticsInfo)
        {
            if (acqImageFromCamera == null)
                return null;
            return acqImageFromCamera.GetAcqParams(opticsInfo);
        }

        //add by LuoDian @ 20211119 当多个相机共用同一个光源的时候，在所有相机拍完照之后，需要在TaskRunner中关闭光源
        public int CloseLightController(List<LightControllerHandle> lightControllerHandles)
        {
            if (acqImageFromCamera == null)
                return ErrorCodeConst.LightOperationError;
            return acqImageFromCamera.CloseLightController(lightControllerHandles);
        }

        /// <summary>
        /// 相机采图前的触发设置，包含软触发和硬触发
        /// add by LuoDian @ 20220111 为了提升效率，把触发和获取图像数据分开，所以在这里添加一个触发接口
        /// </summary>
        /// <returns></returns>
        public int Trigger(OpticsInfoVO opticsInfo, bool isSetLightController = true)
        {

            if (acqImageFromCamera == null)
                return ErrorCodeConst.SetTriggerError;
            return acqImageFromCamera.Trigger(opticsInfo, isSetLightController);
        }

        #endregion

        /// <summary>
        /// 相机取像
        /// </summary>
        class AcquireImageFromCamera
        {
            public event Action Completed;
            public event EventHandler Confirm;

            readonly ICameraRepository cameraRepo;
            readonly ICommandRepository commandRepo;
            readonly IRunTimeRepository runTimeRepo;
            readonly IGLog log;

            CameraInfoVO cameraInfo;
            ICamera camera;
            readonly List<ILightController> lightControllers;

            readonly ManualResetEventSlim slim = new ManualResetEventSlim(false);

            // 取像超时计时器
            System.Timers.Timer timer;

            //update by Luodian @ 20220124 为了提升效率，低层相机出图的时候不采用事件回调出图，改为直接把图像放入这个队列中，所以要把这个队列挪到BaseCamera中，让这个类和低层相机类都能访问
            // 图像缓存区
            //readonly ConcurrentQueue<CameraImage> imgQueue;
            //add by LuoDian @ 20220124 添加一个标识位，用于在系统退出的时候，退出队列中获取图像数据的循环
            bool isExit = false;

            TaskInfoVO taskInfo;

            public AcquireImageFromCamera(
                TaskInfoVO taskInfo,
                IControllerCollection controllers,
                ICameraRepository cameraRepo,
                IOpticsRepository opticsRepo,
                ICommandRepository commandRepo,
                IRunTimeRepository runTimeRepo,
                IGLog log)
            {
                this.cameraRepo = cameraRepo;
                this.commandRepo = commandRepo;
                this.runTimeRepo = runTimeRepo;
                this.log = log;

                this.taskInfo = taskInfo;

                cameraInfo = cameraRepo.GetCameraInfo(taskInfo.CameraAcquireImage.CameraSN);

                camera = cameraRepo.GetCamera(taskInfo.CameraAcquireImage.CameraSN);

                // 不能在这里加，相机复用的时候会报错
                //if (camera.IsOpen)
                //    throw new ServiceException("相机已打开");

                opticsRepo.OpticsInfoChanged += OpticsRepo_OpticsInfoChanged;

                //delete by Luodian @ 20220124 为了提升效率，低层相机出图的时候不采用事件回调出图，改为直接把图像放入这个队列中，所以要把这个队列挪到BaseCamera中，让这个类和低层相机类都能访问
                //imgQueue = new ConcurrentQueue<CameraImage>();

                timer = new System.Timers.Timer();
                //delete by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，不需要再超时丢弃图像，所以这里不需要对超时的情况做处理
                //timer.Elapsed += Timer_Elapsed;

                //add by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，不需要在BeginAcqImage接口中绑定事件，挪到了这里进行绑定，
                //delete by Luodian @ 20220124 为了提升效率，低层相机出图的时候不采用事件回调出图，改为直接把图像放入这个队列中，所以要把这个队列挪到BaseCamera中，让这个类和低层相机类都能访问
                //camera.ImageReceived += Camera_ReceiveImage;

                lightControllers = controllers.GetControllers();

                //add by LuoDian @ 20220113 在程序初始化的时候，初始化光源控制器，节省后面运行时打开光源的时间
                foreach(ILightController lightController in lightControllers)
                {
                    if (!lightController.Connected)
                        lightController.Connect();
                }
            }

            //add by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，不需要在EndAcqImage接口中解绑事件，挪到了这里进行解绑
            ~AcquireImageFromCamera()
            {
                //delete by Luodian @ 20220124 为了提升效率，低层相机出图的时候不采用事件回调出图，改为直接把图像放入这个队列中，所以要把这个队列挪到BaseCamera中，让这个类和低层相机类都能访问
                //if (camera != null)
                //    camera.ImageReceived -= Camera_ReceiveImage;

                //add by LuoDian @ 20220124 添加一个标识位，用于在系统退出的时候，退出队列中获取图像数据的循环
                isExit = true;
            }

            public void ResetTaskInfo(TaskInfoVO taskInfo)
            {
                if (this.taskInfo.CameraAcquireImage.CameraSN != taskInfo.CameraAcquireImage.CameraSN)
                {
                    cameraInfo = cameraRepo.GetCameraInfo(taskInfo.CameraAcquireImage.CameraSN);

                    camera = cameraRepo.GetCamera(taskInfo.CameraAcquireImage.CameraSN);
                }

                this.taskInfo = taskInfo;
            }

            private void OpticsRepo_OpticsInfoChanged(OpticsInfoVO opticsInfo)
            {
                string opticsName = opticsInfo?.OpticsName();
                AcquireImageParams @params = CreateAcqParams(opticsInfo);
                parameters[opticsName] = @params;
            }

            public void Open()
            {
                camera.Open();
                camera.ImageCacheCount = cameraInfo.ImageCacheCount;
                camera.Start(TriggerMode.Trigger, cameraInfo.TriggerSource);
            }

            public void Close()
            {
                if (camera != null)
                    camera.Close();

                parameters.Clear();

                EndAcqImage();
            }

            long cmdIndex;  // 指令索引
            int count;
            int curIndex;

            bool timeout = false;   // 取像超时标志
            bool completed = true;  // 取像完成标志

            public void BeginAcqImage(long cmdIndex, int count)
            {
                this.cmdIndex = cmdIndex;
                this.count = count;
                curIndex = 0;

                #region delete by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，不需要再超时丢弃图像，所以这里不需要对超时的情况做处理, 图像接收数据的事情挪到前面初始化的时候来绑定
                //ClearQueue();

                //camera.ImageReceived += Camera_ReceiveImage;
                ////log.Info(new AcqImageLogMessage(taskInfo.Name, A_AcqImage, R_Start, $"指令[{cmdIndex}]加载取像事件，共 {count} 次取像"));

                //completed = false;
                //timeout = false;

                //// 暂停状态，不开启超时计时器
                //if (runTimeRepo.AcquireImageTimeout > 0 && state == 0)
                //{
                //    timer.Interval = runTimeRepo.AcquireImageTimeout;
                //    timer.Start();
                //}
                #endregion
            }

            #region update by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，不需要再动态绑定事件Camera_ReceiveImage，所以这里也不需要再解绑
            public void EndAcqImage()
            {
                //if (!completed)
                //{
                //    completed = true;

                //    timer.Stop();

                //    camera.ImageReceived -= Camera_ReceiveImage;
                //    log.Debug(new AcqImageLogMessage(taskInfo.Name, A_AcqImage, R_End, $"指令[{cmdIndex}]卸载取像事件"));

                //    Completed?.Invoke();
                //}
            }
            #endregion

            #region 暂停、继续、复位

            /// <summary>
            /// 0 正常，1 暂停
            /// </summary>
            int state = 0;

            /// <summary>
            /// 暂停，挂起
            /// <para>将停止超时计时器</para>
            /// </summary>
            public void Suspend()
            {
                if (state == 1) return;

                state = 1;

                // 虽然指令是一条条执行的，这个方法执行完成之前，不会有新的指令进来
                // 但是，调度中可能有排队指令，完全有可能当中进来

                if (timer.Enabled)
                {
                    timer.Enabled = false;
                }
            }

            /// <summary>
            /// 恢复，继续
            /// <para>如果取像未完成，将开启超时计时器，并且计时器是重新计时的</para>
            /// </summary>
            public void Resume()
            {
                if (state == 0) return;

                state = 0;

                if (!completed)
                {
                    timer.Enabled = true;
                }
            }

            /// <summary>
            /// 复位
            /// </summary>
            public void Reset()
            {
                if (timer.Enabled)
                {
                    timer.Enabled = false;
                    timeout = true;
                }

                slim.Set();

                EndAcqImage();
            }

            #endregion

            readonly object sync = new object();

            private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                lock (sync)
                {
                    if (timer.Enabled)
                    {
                        timer.Stop();

                        timeout = true;

                        curIndex++;

                        log.Error(new AcqImageLogMessage(taskInfo.Name, A_AcqImage, R_Fail, $"指令[{cmdIndex}] 第 {curIndex} 次取像超时，请检查相机是否能正常打开！"));

                        EndAcqImage();

                        slim.Set();
                    }
                }
            }

            /// <summary>
            /// 获取相机输出图像数据的回调
            /// delete by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，把超时丢弃图像数据的逻辑也一并删除了，并且队列的对象也挪到了BaseCamera中，所以这部分逻辑留着也没用，删掉了
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            //void Camera_ReceiveImage(object sender, CameraImageEventArgs e)
            //{
            //    #region delete by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，把超时丢弃图像数据的逻辑也一并删除了，所以这部分逻辑留着也没用，删掉了
            //    //lock (sync)
            //    //{
            //    //    // 超时之后，卸载取像事件之前出图，是有可能进这里的
            //    //    // 这里最好是加锁
            //    //    if (timeout)
            //    //    {
            //    //        log.Warn(new AcqImageLogMessage(taskInfo.Name, A_AcqImage, R_Fail, "取像已超时，图像丢弃"));
            //    //        return;
            //    //    }

            //    //    timer.Stop();

            //    //    //add by LuoDian @ 20211119 在这里先做深拷贝，拷贝完再传出去
            //    //    //Bitmap image = e.CameraImage.Bitmap.DeepClone();
            //    //    //CameraImage cameraImage = new CameraImage();
            //    //    //cameraImage.Bitmap = image;
            //    //    //cameraImage.AcqImgTime = e.CameraImage.AcqImgTime;
            //    //    //cameraImage.FrameNum = e.CameraImage.FrameNum;
            //    //    //cameraImage.IsGrey = e.CameraImage.IsGrey;
            //    //    //cameraImage.Timestamp = e.CameraImage.Timestamp;
            //    //    //imgQueue.Enqueue(cameraImage);
            //    //}

            //    //curIndex++;

            //    //if (curIndex == count)
            //    //{
            //    //    //log.Info(new AcqImageLogMessage(taskInfo.Name, A_AcqImage, R_End, $"指令[{cmdIndex}] 第 {curIndex} 次取像，FrameNum = {e.CameraImage.FrameNum}，取像结束"));

            //    //    EndAcqImage();
            //    //}
            //    //else
            //    //{
            //    //    log.Info(new AcqImageLogMessage(taskInfo.Name, A_AcqImage, R_Success, $"指令[{cmdIndex}] 第 {curIndex} 次取像，FrameNum = {e.CameraImage.FrameNum}"));

            //    //    if (state == 0 && runTimeRepo.AcquireImageTimeout > 0)
            //    //        timer.Start();
            //    //}
            //    #endregion

            //    //delete by LuoDian @ 20211119 挪到前面去做深拷贝，这里就删除掉了
            //    imgQueue.Enqueue(e.CameraImage);

            //    slim.Set();
            //}

            readonly Dictionary<string, AcquireImageParams> parameters = new Dictionary<string, AcquireImageParams>();

            #region  update by LuoDian @ 20220111 为了提升效率，把触发和获取图像数据分开，所以这个获取图像数据的接口，不再调用触发设置的逻辑
            //update by LuoDian @ 20211118 为了适应当多个相机共用同一个光源的情况，添加一个状态位判断是否需要在这里切换光源
            //public int GetImage(OpticsInfoVO opticsInfo, bool isSetLightController, out CameraImage cameraImage)
            //{
            //    AcquireImageParams @params = GetAcqParams(opticsInfo);

            //    Stopwatch grabImageWatch = new Stopwatch();
            //    if (cameraInfo.SoftTrigger)
            //    {
            //        int errCode;

            //        if (@params != null)
            //        {
            //            //grabImageWatch.Start();
            //            errCode = SetParams(@params, isSetLightController);
            //            //grabImageWatch.Stop();
            //            //log.Info($"相机参数已设置完成！耗时：{grabImageWatch.ElapsedMilliseconds}");
            //            if (errCode != ErrorCodeConst.OK)
            //            {
            //                cameraImage = null;
            //                return errCode;
            //            }
            //        }

            //        //grabImageWatch.Reset();
            //        grabImageWatch.Start();
            //        errCode = SoftTrigger();
            //        //grabImageWatch.Stop();
            //        //log.Info($"相机已触发完成！耗时：{grabImageWatch.ElapsedMilliseconds}");
            //        if (errCode != ErrorCodeConst.OK)
            //        {
            //            cameraImage = null;
            //            return ErrorCodeConst.AcqImageError;
            //        }
            //    }
            //    else
            //    {
            //        // 硬触发控制曝光，加在这里是最简单的，但是这里离接收到指令的地方有点远，需要的时间可能会长一点
            //        // 硬触发相机参数是提前设置好的，不变的
            //        if (@params != null)
            //        {
            //            int errCode = SetCameraParams(@params.CameraParams);
            //            if (errCode != ErrorCodeConst.OK)
            //            {
            //                cameraImage = null;
            //                return ErrorCodeConst.AcqImageError;
            //            }
            //        }

            //        // 若多次拍照的指令同时发送，则只触发一次
            //        if (curIndex == 0 && commandRepo.EnableHandCmd)
            //        {
            //            Confirm?.Invoke(this, EventArgs.Empty);
            //        }
            //    }

            //    // 取像后是存到这个缓存里的
            //    if (imgQueue.IsEmpty)
            //    {
            //        // 一组指令中，一次超时，后面全部记超时
            //        if (timeout)
            //        {
            //            cameraImage = null;
            //            return ErrorCodeConst.AcqImageError;
            //        }
            //        else
            //        {
            //            slim.Reset();
            //            slim.Wait();
            //        }
            //    }

            //    //grabImageWatch.Reset();
            //    //grabImageWatch.Start();
            //    if (imgQueue.TryDequeue(out cameraImage))
            //    {
            //        //if (@params?.LightControllerHandles != null)
            //        //{
            //        //    CloseLightController(@params.LightControllerHandles);
            //        //}
            //        grabImageWatch.Stop();
            //        log.Info($"任务[{opticsInfo.TaskName}]的拍照[{opticsInfo.AcquireImageName}]已完成！耗时：{grabImageWatch.ElapsedMilliseconds}");
            //        return ErrorCodeConst.OK;
            //    }
            //    else
            //    {
            //        cameraImage = null;
            //        return ErrorCodeConst.AcqImageError;
            //    }
            //}
            public int GetImage(out CameraImage cameraImage)
            {
                //// 取像后是存到这个缓存里的
                //if (imgQueue.IsEmpty)
                //{
                //    //update by LuoDian @ 20220113 在把触发跟获取图像数据分开之后，不需要再超时丢弃图像，所以这里不需要对超时的情况做处理
                //    // 一组指令中，一次超时，后面全部记超时
                //    //if (timeout)
                //    //{
                //    //    cameraImage = null;
                //    //    return ErrorCodeConst.AcqImageError;
                //    //}
                //    //else
                //    //{
                //    //    slim.Reset();
                //    //    slim.Wait();
                //    //}
                //    slim.Reset();
                //    slim.Wait();

                //    //add by LuoDian @ 20220113 添加一个GC，用于图像数据的释放
                //    GC.Collect();
                //}

                //grabImageWatch.Reset();
                //grabImageWatch.Start();
                cameraImage = camera.imgQueue.Take();
                //if (camera.imgQueue.TryDequeue(out cameraImage))
                {
                    //if (@params?.LightControllerHandles != null)
                    //{
                    //    CloseLightController(@params.LightControllerHandles);
                    //}

                    log.Info($"【CT统计】当前相机 {camera.CameraInfo.UserDefinedName} 中还有 {camera.imgQueue.Count} 张图！当前这张图从触发到获取完图像数据的耗时：{(DateTime.Now - cameraImage.AcqImgTime).TotalMilliseconds}");

                    return ErrorCodeConst.OK;
                }
                //else
                //{
                //    cameraImage = null;
                //    return ErrorCodeConst.AcqImageError;
                //}
            }
            #endregion

            //update by LuoDian @ 20211118 为了适应当多个相机共用同一个光源的情况, 这里需要把默认访问级别修改成public，供TaskRunner那里使用
            public AcquireImageParams GetAcqParams(OpticsInfoVO opticsInfo)
            {
                string opticsName = opticsInfo?.OpticsName();
                if (opticsName == null) return null;

                //update by LuoDian @ 20211224 添加子料号之后，需要每次都要重新从OpticsInfoVO中获取相机参数
                //if (parameters.ContainsKey(opticsName))
                //{
                //    return parameters[opticsName];
                //}
                //else
                //{
                //    AcquireImageParams @params = CreateAcqParams(opticsInfo);
                //    parameters[opticsName] = @params;

                //    return @params;
                //}
                AcquireImageParams @params = CreateAcqParams(opticsInfo);
                return @params;
            }

            AcquireImageParams CreateAcqParams(OpticsInfoVO opticsInfo)
            {
                AcquireImageParams @params = new AcquireImageParams
                {
                    CameraParams = opticsInfo.CameraParams
                };

                if (opticsInfo.LightControllerValues != null)
                {
                    @params.LightControllerHandles = new List<LightControllerHandle>();

                    foreach (LightControllerValueInfoVO controllerValue in opticsInfo.LightControllerValues)
                    {
                        LightControllerHandle lightControllerHandle = new LightControllerHandle
                        {
                            LightController = lightControllers.First(a => a.Name == controllerValue.LightControllerName),
                            ChannelValues = controllerValue.ChannelValues
                        };

                        @params.LightControllerHandles.Add(lightControllerHandle);
                    }
                }

                return @params;
            }

            //update by LuoDian @ 20211118 为了适应当多个相机共用同一个光源的情况，添加一个状态位判断是否需要在这里切换光源
            int SetParams(AcquireImageParams @params, bool isSetLightController)
            {
                // 硬触发相机参数是提前设置好的，不变的
                int errCode = SetCameraParams(@params.CameraParams);
                if (errCode != ErrorCodeConst.OK)
                {
                    return ErrorCodeConst.AcqImageError;
                }

                if(isSetLightController)
                {
                    // 硬触发必定是上位机控制光源
                    errCode = SetLightController(@params.LightControllerHandles);
                    if (errCode != ErrorCodeConst.OK)
                    {
                        return ErrorCodeConst.AcqImageError;
                    }
                }

                return errCode;
            }

            int SetCameraParams(CameraParams? cameraParams)
            {
                if (cameraParams.HasValue)
                {
                    if (camera.SetParams(cameraParams.Value))
                        return ErrorCodeConst.OK;
                    else
                        return ErrorCodeConst.AcqImageError;
                }
                return ErrorCodeConst.OK;
            }

            //update by LuoDian @ 20211118 当多个相机共用同一个光源的时候，需要在执行Task指令的时候判断下共有哪些相机共用这一个光源
            //这里就需要修改访问级别, 从默认级别修改为public
            public int SetLightController(List<LightControllerHandle> lightControllerHandles)
            {
                try
                {
                    if (lightControllerHandles != null && lightControllerHandles.Count > 0)
                    {
                        //CountdownEvent lightControlCount = new CountdownEvent(lightControllerHandles.Count);
                        foreach (LightControllerHandle controllerValue in lightControllerHandles)
                        {
                            //Task.Run(() => {
                            //    controllerValue.Open();
                            //    lightControlCount.Signal();
                            //});
                            controllerValue.Open();
                        }
                        //lightControlCount.Wait();
                    }

                    return ErrorCodeConst.OK;
                }
                catch(Exception ex)
                {
                    return ErrorCodeConst.LightOperationError;
                }
            }

            //update by LuoDian @ 20211119 当多个相机共用同一个光源的时候，需要在TaskRunner中判断所有相机拍完后，关闭光源
            public int CloseLightController(List<LightControllerHandle> lightControllerHandles)
            {
                try
                {
                    if (lightControllerHandles != null && lightControllerHandles.Count > 0)
                    {
                        //CountdownEvent lightControlCount = new CountdownEvent(lightControllerHandles.Count);
                        foreach (LightControllerHandle controllerValue in lightControllerHandles)
                        {
                            //Task.Run(() => {
                            //    controllerValue.Close();
                            //    lightControlCount.Signal();
                            //});
                            controllerValue.Close();
                        }
                        //lightControlCount.Wait();
                    }

                    return ErrorCodeConst.OK;
                }
                catch(Exception ex)
                {
                    return ErrorCodeConst.OK;
                }
            }

            public int Trigger(OpticsInfoVO opticsInfo, bool isSetLightController)
            {
                try
                {
                    AcquireImageParams @params = GetAcqParams(opticsInfo);

                    if (cameraInfo.SoftTrigger)
                    {
                        int errCode = ErrorCodeConst.OK;

                        if (@params != null)
                        {
                            errCode = SetParams(@params, isSetLightController);
                            if (errCode != ErrorCodeConst.OK)
                            {
                                return ErrorCodeConst.SetTriggerError;
                            }
                        }

                        try
                        {
                            camera.SoftTrigger();
                        }
                        catch
                        {
                            return ErrorCodeConst.SoftTriggerError;
                        }
                        finally
                        {
                            //add by LuoDian @ 20220121 软触发完成后，需要关闭光源
                            if (@params != null && isSetLightController)
                            {
                                // 硬触发必定是上位机控制光源
                                errCode = CloseLightController(@params.LightControllerHandles);
                            }
                        }

                        if (errCode != ErrorCodeConst.OK)
                        {
                            return errCode;
                        }
                    }
                    else
                    {
                        // 硬触发控制曝光，加在这里是最简单的，但是这里离接收到指令的地方有点远，需要的时间可能会长一点
                        // 硬触发相机参数是提前设置好的，不变的
                        if (@params != null)
                        {
                            int errCode = SetCameraParams(@params.CameraParams);
                            if (errCode != ErrorCodeConst.OK)
                            {
                                return ErrorCodeConst.SetTriggerError;
                            }
                        }

                        // 若多次拍照的指令同时发送，则只触发一次
                        if (curIndex == 0 && commandRepo.EnableHandCmd)
                        {
                            Confirm?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    return ErrorCodeConst.OK;
                }
                catch
                {
                    return ErrorCodeConst.SetTriggerError;
                }
            }
        }

        class AcquireImageFromLocal
        {
            readonly ISimulationRepository simulationRepo;
            readonly IGLog log;

            public AcquireImageFromLocal(ISimulationRepository simulationRepo, IGLog log)
            {
                this.simulationRepo = simulationRepo;
                this.log = log;
            }

            int currIndex = 0;

            string GetImagePath(string taskName, string acqImageName)
            {
                string directory = $@"{simulationRepo.GetLocalImageDirectory()}\{taskName}\{acqImageName}";
                if (!Directory.Exists(directory))
                    return null;

                string[] filenames = Directory.GetFiles(directory);
                if (filenames.Length == 0)
                    return null;

                if (currIndex >= filenames.Length)
                    currIndex = 0;

                return filenames[currIndex++];
            }

            public Bitmap GetImage(string taskName, string acqImageName)
            {
                string filename = GetImagePath(taskName, acqImageName);

                if (filename == null)
                    return null;
                else
                {
                    Image img = Image.FromFile(filename, false);
                    return (Bitmap)img;
                }
            }

            public void Reset()
            {
                currIndex = 0;
            }

            string GetImagePath(string taskName, string calibName, int calibIndex)
            {
                string directory = $@"{simulationRepo.GetLocalImageDirectory()}\{taskName}\{calibName}";
                string[] filenames = Directory.GetFiles(directory);
                if (filenames.Length < calibIndex)
                    return null;

                return filenames[calibIndex - 1];
            }

            public Bitmap GetImage(string taskName, string calibName, int calibIndex)
            {
                string filename = GetImagePath(taskName, calibName, calibIndex);

                if (filename == null)
                    return null;
                else
                    return new Bitmap(filename);
            }
        }
    }

    //add by LuoDian @ 20211118 为了适应当多个相机共用同一个光源的情况, 这里需要把默认访问级别修改成public，供TaskRunner那里使用
    public class AcquireImageParams
    {
        public CameraParams CameraParams { get; set; }

        public List<LightControllerHandle> LightControllerHandles { get; set; }
    }
}
