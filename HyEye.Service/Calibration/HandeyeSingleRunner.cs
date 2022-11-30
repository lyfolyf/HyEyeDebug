using CameraSDK.Models;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    public delegate void HandeyeSingleRanHandle(string taskName, string calibName,
        List<HandeyeSinglePoint> points, object graphic);

    public class HandeyeSingleRunner : ICheckable
    {
        /// <summary>
        /// 获取模板坐标完成后发生
        /// </summary>
        public event HandeyeSingleRanHandle AfterGetPose;

        public event Action ReceiveCmdS;

        public event GetSrcImageHandle GetSrcImage;

        public event GetAfterCheckerboardImageHandle GetAfterCheckerboardImage;

        TaskInfoVO taskInfo;
        CalibrationInfoVO calibInfo;
        OpticsInfoVO opticsInfo;

        readonly ITaskRepository taskRepo;
        readonly ICalibrationRepository calibRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ISimulationRepository simulationRepo;

        readonly IAcquireImage acqImage;
        readonly IGLog log;

        //add by LuoDian @ 20211214 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        public IHandeyeSingleComponent HandeyeSingleComponent { get; }

        public ICheckerboardComponent CheckerboardComponent { get; set; }

        public IDisplayImageComponent DisplayImageComponent { get; }

        public string TaskName { get; private set; }

        public string CalibrationName { get; private set; }

        public HandeyeSingleRunner(
            string taskName,
            string calibName,
            ITaskRepository taskRepo,
            ICalibrationRepository calibRepo,
            IOpticsRepository opticsRepo,
            ISimulationRepository simulationRepo,
            IHandeyeSingleComponent handeyeSingleComponent,
            IDisplayImageComponent displayImageComponent,
            IAcquireImage acqImage,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            TaskName = taskName;
            CalibrationName = calibName;

            this.taskRepo = taskRepo;
            this.calibRepo = calibRepo;
            this.simulationRepo = simulationRepo;
            this.opticsRepo = opticsRepo;
            this.acqImage = acqImage;
            this.log = log;

            //add by LuoDian @ 20211214 用于子料号的快速切换
            this.materialRepo = materialRepo;

            this.HandeyeSingleComponent = handeyeSingleComponent;
            DisplayImageComponent = displayImageComponent;
        }

        (double X, double Y) errPoint = (999, 999);

        bool started = false;

        public bool Check()
        {
            return acqImage.CheckCamera(TaskName);
        }

        public void Start()
        {
            taskInfo = taskRepo.GetTaskByName(TaskName);
            calibInfo = calibRepo.GetCalibration(CalibrationName);
            opticsInfo = opticsRepo.GetOptics(TaskName, null, CalibrationName, materialRepo.CurSubName);

            acqImage.Start(taskInfo, true);

            started = true;
        }

        public void Close()
        {
            if (started)
            {
                acqImage.Close();

                started = false;

                DisplayImageComponent.ClearGraphic();
                DisplayImageComponent.ClearImage();
            }
        }

        public void AutoStart()
        {
            Start();
            autoCalib = true;
            log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_AutoCalib, R_Start));
        }

        public void AutoStop()
        {
            autoCalib = false;
            autoStart = false;

            log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_AutoCalib, R_Stop));
        }

        /// <summary>
        /// 手动获取模板坐标
        /// </summary>
        public bool GetPose()
        {
            if (!started)
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Fail, "标定尚未启动"));
                throw new ServiceException("标定尚未启动");
            }

            if (autoCalib)
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Fail, "当前是自动标定模式"));
                throw new ServiceException("当前是自动标定模式");
            }

            HandeyeSingleComponent.Start();
            acqImage.Start(taskInfo, true);

            // 手动没有 S 指令，只能在第一次获取模板坐标中开启取像
            acqImage.BeginAcqImage(0, 1);

            (int errCode, CameraImage cameraImage) = GetImage();

            acqImage.Close();

            if (errCode != ErrorCodeConst.OK)
            {
                return false;
            }

            Dictionary<string, object> outputs;
            (errCode, outputs) = RunComponent(cameraImage.Bitmap, cameraImage.IsGrey);

            if (errCode == ErrorCodeConst.OK)
            {
                log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Success,
                    $"坐标获取：X1 = {outputs["X1"]}, Y1 = {outputs["Y1"]}, X2 = {outputs["X2"]}, Y2 = {outputs["Y2"]}, X3 = {outputs["X3"]}, Y3 = {outputs["Y3"]}, X4 = {outputs["X4"]}, Y4 = {outputs["Y4"]}, X5 = {outputs["X5"]}, Y5 = {outputs["Y5"]}, X6 = {outputs["X6"]}, Y6 = {outputs["Y6"]}, X7 = {outputs["X7"]}, Y7 = {outputs["Y7"]}, X8 = {outputs["X8"]}, Y8 = {outputs["Y8"]}, X9 = {outputs["X9"]}, Y9 = {outputs["Y9"]}"));

                getPoint(outputs, null, false);

                return true;
            }
            else
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Fail, ""));

                return false;
            }
        }

        /// <summary>
        /// 手动标定
        /// </summary>
        public void ManualCalibrate(List<HandeyeSinglePoint> points)
        {
            HandeyeSingleComponent.Calibrate(points);
        }

        /// <summary>
        /// 重置标定
        /// </summary>
        public void Reset()
        {
            DisplayImageComponent.ClearGraphic();
            DisplayImageComponent.ClearImage();
            acqImage.Reset();
            autoStart = false;
        }

        #region 运行指令

        public Task<IReplyCommand> RunCommand(long cmdIndex, CalibCommand cmd)
        {
            return Task.Run(() =>
            {
                if (cmd.Type == CommandType.S)
                {
                    return RunCmdS(cmdIndex, cmd);
                }
                else if (cmd.Type == CommandType.C)
                {
                    return RunCmdC(cmd);
                }

                return ErrorCommand.Create(ErrorCodeConst.CommandTypeError);
            });
        }

        bool autoCalib = false;
        bool autoStart = false;

        IReplyCommand RunCmdS(long cmdIndex, CalibCommand cmd)
        {
            if (autoCalib)
            {
                //if (!autoStart)
                //{
                //    acqImage.EndAcqImage();
                //    acqImage.BeginAcqImage(cmdIndex, totalCount);
                //}
                //else
                //{
                //    Reset();
                //}

                Reset();
                acqImage.BeginAcqImage(cmdIndex, 1);

                ReceiveCmdS?.Invoke();

                autoStart = true;

                log.Info(new CalibServerLogMessage(cmd.TaskName, cmd.CalibName, A_Calibration, R_Start));

                int errCode = HandeyeSingleComponent.Start();

                return buildSendCommand(errCode, cmd, null);
            }
            else
            {
                // ## 这里缺一个判断，手动模式开始了，才是这个错误，否则应该报自动标定未启动
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_RunCmd, R_Fail, "当前是手动标定模式"));

                return ErrorCommand.Create(ErrorCodeConst.CalibModeError);
            }
        }

        IReplyCommand RunCmdC(CalibCommand cmd)
        {
            if (!autoStart)
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_RunCmd, R_Fail, "标定未开始或已结束，请先发送 S 指令"));

                return buildSendCommand(ErrorCodeConst.CalibNotStarted, cmd, errPoint);
            }

            (int errCode, CameraImage cameraImage) = GetImage();
            if (errCode != ErrorCodeConst.OK)
            {
                return buildSendCommand(errCode, cmd, errPoint);
            }

            Dictionary<string, object> outputs;

            (errCode, outputs) = RunComponent(cameraImage.Bitmap, cameraImage.IsGrey);

            if (errCode != ErrorCodeConst.OK)
            {
                // ##
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Fail, ""));

                return buildSendCommand(errCode, cmd, errPoint);
            }

            log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Success,
                    $"坐标获取：X1 = {outputs["X1"]}, Y1 = {outputs["Y1"]}, X2 = {outputs["X2"]}, Y2 = {outputs["Y2"]}, X3 = {outputs["X3"]}, Y3 = {outputs["Y3"]}, X4 = {outputs["X4"]}, Y4 = {outputs["Y4"]}, X5 = {outputs["X5"]}, Y5 = {outputs["Y5"]}, X6 = {outputs["X6"]}, Y6 = {outputs["Y6"]}, X7 = {outputs["X7"]}, Y7 = {outputs["Y7"]}, X8 = {outputs["X8"]}, Y8 = {outputs["Y8"]}, X9 = {outputs["X9"]}, Y9 = {outputs["Y9"]}"));

            getPoint(outputs, cmd.FieldValues, true);

            try
            {
                HandeyeSingleComponent.Calibrate(points);
            }
            catch
            {
                errCode = ErrorCodeConst.CalibrateFail;
            }

            autoStart = false;

            return new SendCommand
            {
                CommandHeader = cmd.CommandHeader,
                AcqOrCalibIndex = cmd.CalibIndex,
                Type = CommandType.E,
                ErrorCode = errCode
            };
        }

        #endregion

        (int errCode, Dictionary<string, object>) RunComponent(Bitmap bitmap, bool isGrey)
        {
            if (CheckerboardComponent == null)
            {
                return HandeyeSingleComponent.Run(bitmap, isGrey);
            }
            else
            {
                try
                {
                    object img = CheckerboardComponent.Run(bitmap, isGrey);

                    if (img == null)
                    {
                        return (ErrorCodeConst.CheckerboardCalibrationError, null);
                    }

                    GetAfterCheckerboardImage?.Invoke(TaskName, CalibrationName, img, isGrey);

                    return HandeyeSingleComponent.Run(img, isGrey);
                }
                catch (Exception)
                {
                    return (ErrorCodeConst.CheckerboardCalibrationError, null);
                }
            }
        }

        List<HandeyeSinglePoint> points;

        // fieldValues: 指令中发过来的数据，手动标定时为 null
        void getPoint(Dictionary<string, object> outputs, List<CommandFieldValue> fieldValues, bool auto)
        {
            points = new List<HandeyeSinglePoint>(9);

            for (int i = 1; i <= 9; i++)
            {
                string xname = "X" + i.ToString();
                string yname = "Y" + i.ToString();

                HandeyeSinglePoint p = new HandeyeSinglePoint();
                p.X1 = outputs[xname] == null ? 999 : (double)outputs[xname];
                p.Y1 = outputs[yname] == null ? 999 : (double)outputs[yname];
                p.X2 = (double?)fieldValues?.FirstOrDefault(a => a.Name == xname)?.Value;
                p.Y2 = (double?)fieldValues?.FirstOrDefault(a => a.Name == yname)?.Value;
                p.Disable = false;

                points.Add(p);
            }

            AfterGetPose?.Invoke(TaskName, CalibrationName, points, outputs["Graphic"]);
        }

        SendCommand buildSendCommand(int errCode, CalibCommand cmd, (double X, double Y)? p)
        {
            SendCommand sendCommand = new SendCommand
            {
                CommandHeader = cmd.CommandHeader,
                AcqOrCalibIndex = cmd.CalibIndex,
                Type = cmd.Type,
                ErrorCode = errCode
            };

            if (p != null)
            {
                sendCommand.FieldValues.Add(new CommandFieldValue { Name = "X", Value = p.Value.X });
                sendCommand.FieldValues.Add(new CommandFieldValue { Name = "Y", Value = p.Value.Y });
            }

            return sendCommand;
        }

        (int errorCode, CameraImage cameraImage) GetImage()
        {
            int errorCode;
            CameraImage cameraImage;

            if (simulationRepo.Enabled)
            {
                errorCode = acqImage.GetImageFromLocal(CalibrationName, out cameraImage);
            }
            else
            {
                //add by LuoDian @ 20220111 为了提升软触发的效率，把触发设置和获取图像数据分开，以便在触发完成后，立马返回信号响应，不必等图像数据输出
                errorCode = acqImage.Trigger(opticsInfo);
                if (errorCode != ErrorCodeConst.OK)
                {
                    log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_AcqImage, R_Fail, "触发设置失败"));
                    return (errorCode, null);
                }

                //update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
                errorCode = acqImage.GetImageFromCamera(out cameraImage);
            }

            if (errorCode == ErrorCodeConst.OK)
            {
                GetSrcImage?.Invoke(new HyImageInfo
                {
                    Bitmap = cameraImage.Bitmap,
                    IsGrey = cameraImage.IsGrey,
                    FrameNum = cameraImage.FrameNum,
                    TaskName = TaskName,
                    AcqOrCalibName = CalibrationName,
                    AcqOrCalibIndex = 1,
                });
            }
            else
            {
                // ##
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_AcqImage, R_Fail, ""));
            }

            return (errorCode, cameraImage);
        }

        /// <summary>
        /// 手动取像
        /// </summary>
        public void AcqImage()
        {
            taskInfo = taskRepo.GetTaskByName(TaskName);

            if (!acqImage.CheckCamera(taskInfo)) return;

            acqImage.Start(taskInfo, true);
            acqImage.BeginAcqImage(0, 1);

            (int errorCode, CameraImage cameraImage) = GetImage();

            acqImage.Close();

            if (errorCode == ErrorCodeConst.OK)
            {
                if (CheckerboardComponent == null)
                {
                    HandeyeSingleComponent.SetPatternImage(cameraImage.Bitmap, cameraImage.IsGrey);
                }
                else
                {
                    object img = null;
                    try
                    {
                        img = CheckerboardComponent.Run(cameraImage.Bitmap, cameraImage.IsGrey);
                    }
                    catch (Exception)
                    {
                        log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_Distort, R_Fail, "Checkerboard 畸变校正失败"));
                    }

                    if (img != null)
                    {
                        HandeyeSingleComponent.SetPatternImage(img, cameraImage.IsGrey);
                    }
                    else
                    {
                        log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_Distort, R_Fail, "Checkerboard 畸变校正失败"));
                    }
                }

                cameraImage.Dispose();
            }
        }

    }
}
