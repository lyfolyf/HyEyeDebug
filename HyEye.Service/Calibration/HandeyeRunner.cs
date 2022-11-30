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
    public delegate void HandeyeRanHandle(string taskName, string calibName, int index, double x1, double y1, double a1, double? x2, double? y2, double? a2, object graphic);

    public class HandeyeRunner : ICheckable
    {
        /// <summary>
        /// 获取模板坐标完成后发生
        /// </summary>
        public event HandeyeRanHandle AfterGetPose;

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

        public IHandeyeComponent HandeyeComponent { get; }

        public ICheckerboardComponent CheckerboardComponent { get; set; }

        public IDisplayImageComponent DisplayImageComponent { get; }

        public string TaskName { get; private set; }

        public string CalibrationName { get; private set; }

        public HandeyeRunner(
            string taskName,
            string calibName,
            ITaskRepository taskRepo,
            ICalibrationRepository calibRepo,
            IOpticsRepository opticsRepo,
            ISimulationRepository simulationRepo,
            IHandeyeComponent handeyeComponent,
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

            this.HandeyeComponent = handeyeComponent;
            DisplayImageComponent = displayImageComponent;
        }

        (double X, double Y, double A) errPoint = (999, 999, 999);
        // 发送给上位机的走位信息
        (double X, double Y, double A)[] points;

        int totalCount;

        // datagridview 中的数据，自动标定时用，手动标定时用 ManualCalibrate 方法传入的参数
        List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> calibratedPoints;

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

            if (calibInfo.HandEyeInfo != null)
            {
                totalCount = calibInfo.HandEyeInfo.TotalCount;
                points = calibInfo.HandEyeInfo.GetPoints();
                calibratedPoints = new List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)>();

                acqImage.Start(taskInfo, true);

                started = true;
            }
            else
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_Start, R_Fail, "未配置标定参数"));
            }
        }

        public void Close()
        {
            if (started)
            {
                acqImage.Close();

                started = false;
                totalCount = 0;
                points = null;
                calibratedPoints = null;

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
        public bool GetPose(int index)
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

            if (index == 1)
            {
                acqImage.Reset();

                HandeyeComponent.Start();
            }
            else
            {
                acqImage.Start(taskInfo, true);
            }

            // 手动没有 S 指令，只能在第一次获取模板坐标中开启取像
            acqImage.BeginAcqImage(0, 1);

            (int errCode, CameraImage cameraImage) = GetImage(index);

            acqImage.Close();

            if (errCode != ErrorCodeConst.OK)
            {
                return false;
            }

            Dictionary<string, object> outputs;
            (errCode, outputs) = RunComponent(index, cameraImage.Bitmap, cameraImage.IsGrey);

            if (errCode == ErrorCodeConst.OK)
            {
                log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Success,
                    $"第[{index}]次坐标获取：X = {outputs["X"]}, Y = {outputs["Y"]}, A = {outputs["A"]}"));

                getPoint(index, outputs, null, false);

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
        public void ManualCalibrate(List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> points)
        {
            HandeyeComponent.Calibrate(points);
        }

        /// <summary>
        /// 重置标定
        /// </summary>
        public void Reset()
        {
            HandeyeComponent.Reset();
            DisplayImageComponent.ClearGraphic();
            DisplayImageComponent.ClearImage();
            acqImage.Reset();
            autoStart = false;

            if (calibratedPoints != null)
                calibratedPoints.Clear();
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
                acqImage.BeginAcqImage(cmdIndex, totalCount);

                ReceiveCmdS?.Invoke();

                autoStart = true;

                log.Info(new CalibServerLogMessage(cmd.TaskName, cmd.CalibName, A_Calibration, R_Start));

                int errCode = HandeyeComponent.Start();

                return buildSendCommand(errCode, cmd, points[0]);
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

            if (!HandeyeComponent.CheckIndex(cmd.CalibIndex) || cmd.CalibIndex > totalCount)
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_RunCmd, R_Fail, "标定索引错误"));

                return buildSendCommand(ErrorCodeConst.IndexError, cmd, errPoint);
            }

            (int errCode, CameraImage cameraImage) = GetImage(cmd.CalibIndex);
            if (errCode != ErrorCodeConst.OK)
            {
                return buildSendCommand(errCode, cmd, errPoint);
            }

            Dictionary<string, object> outputs;

            (errCode, outputs) = RunComponent(cmd.CalibIndex, cameraImage.Bitmap, cameraImage.IsGrey);

            if (errCode != ErrorCodeConst.OK)
            {
                // ##
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Fail, ""));

                return buildSendCommand(errCode, cmd, errPoint);
            }

            log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_GetPose, R_Success,
                    $"第[{cmd.CalibIndex}]次坐标获取：X = {outputs["X"]}, Y = {outputs["Y"]}, A = {outputs["A"]}"));

            getPoint(cmd.CalibIndex, outputs, cmd.FieldValues, true);

            if (cmd.CalibIndex != totalCount)
                return buildSendCommand(errCode, cmd, points[cmd.CalibIndex]);
            else
            {
                try
                {
                    HandeyeComponent.Calibrate(calibratedPoints);
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
        }

        #endregion

        (int errCode, Dictionary<string, object>) RunComponent(int index, Bitmap bitmap, bool isGrey)
        {
            if (CheckerboardComponent == null)
            {
                return HandeyeComponent.Run(index, bitmap, isGrey);
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

                    return HandeyeComponent.Run(index, img, isGrey);
                }
                catch (Exception)
                {
                    return (ErrorCodeConst.CheckerboardCalibrationError, null);
                }
            }
        }

        // fieldValues: 指令中发过来的数据，手动标定时为 null
        void getPoint(int index, Dictionary<string, object> outputs, List<CommandFieldValue> fieldValues, bool auto)
        {
            double x1 = outputs["X"] == null ? 999 : (double)outputs["X"];
            double y1 = outputs["Y"] == null ? 999 : (double)outputs["Y"];
            double a1 = outputs["A"] == null ? 999 : (double)outputs["A"];

            double? x2 = (double?)fieldValues?.FirstOrDefault(a => a.Name == "X")?.Value;
            double? y2 = (double?)fieldValues?.FirstOrDefault(a => a.Name == "Y")?.Value;
            double? a2 = (double?)fieldValues?.FirstOrDefault(a => a.Name == "A")?.Value;

            if (auto)
            {
                // 自动标定的时候，x2,y2 是必有的
                calibratedPoints.Add((x1, y1, a1, x2.Value, y2.Value, a2.Value, false));
            }

            AfterGetPose?.Invoke(TaskName, CalibrationName, index, x1, y1, a1, x2, y2, a2, outputs["Graphic"]);
        }

        SendCommand buildSendCommand(int errCode, CalibCommand cmd, (double X, double Y, double A) p)
        {
            SendCommand sendCommand = new SendCommand
            {
                CommandHeader = cmd.CommandHeader,
                AcqOrCalibIndex = cmd.CalibIndex,
                Type = cmd.Type,
                ErrorCode = errCode
            };

            sendCommand.FieldValues.Add(new CommandFieldValue { Name = "X", Value = p.X });
            sendCommand.FieldValues.Add(new CommandFieldValue { Name = "Y", Value = p.Y });
            sendCommand.FieldValues.Add(new CommandFieldValue { Name = "A", Value = p.A });

            return sendCommand;
        }

        (int errorCode, CameraImage cameraImage) GetImage(int calibrationIndex)
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
                if (calibrationIndex != -1)
                    GetSrcImage?.Invoke(new HyImageInfo
                    {
                        Bitmap = cameraImage.Bitmap,
                        IsGrey = cameraImage.IsGrey,
                        FrameNum = cameraImage.FrameNum,
                        TaskName = TaskName,
                        AcqOrCalibName = CalibrationName,
                        AcqOrCalibIndex = calibrationIndex,
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

            (int errorCode, CameraImage cameraImage) = GetImage(-1);

            acqImage.Close();

            if (errorCode == ErrorCodeConst.OK)
            {
                if (CheckerboardComponent == null)
                {
                    HandeyeComponent.SetPatternImage(cameraImage.Bitmap, cameraImage.IsGrey);
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
                        HandeyeComponent.SetPatternImage(img, cameraImage.IsGrey);
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
