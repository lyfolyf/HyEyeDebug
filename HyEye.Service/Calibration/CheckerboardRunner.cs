using CameraSDK.Models;
using GL.Kit;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Threading.Tasks;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    public class CheckerboardRunner : ICheckable
    {
        public event GetSrcImageHandle GetSrcImage;

        public string TaskName { get; private set; }

        public string CalibrationName { get; private set; }

        TaskInfoVO taskInfo;
        OpticsInfoVO opticsInfo;

        readonly ITaskRepository taskRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ISimulationRepository simulationRepo;
        readonly IGLog log;
        readonly IAcquireImage acqImage;

        //add by LuoDian @ 20211214 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        public ICheckerboardComponent CheckerboardComponent { get; }

        public CheckerboardRunner(
            string taskName,
            string calibName,
            ICheckerboardComponent checkerboardComponent,
            ITaskRepository taskRepo,
            IOpticsRepository opticsRepo,
            ISimulationRepository simulationRepo,
            IAcquireImage acqImage,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            TaskName = taskName;
            CalibrationName = calibName;

            this.taskRepo = taskRepo;
            this.opticsRepo = opticsRepo;
            this.simulationRepo = simulationRepo;
            this.acqImage = acqImage;
            this.log = log;

            //add by LuoDian @ 20211214 用于子料号的快速切换
            this.materialRepo = materialRepo;

            this.CheckerboardComponent = checkerboardComponent;
        }

        public bool Check()
        {
            return acqImage.CheckCamera(TaskName);
        }

        public void Start()
        {
            taskInfo = taskRepo.GetTaskByName(TaskName);

            acqImage.Start(taskInfo, true);
        }

        public void Close()
        {
            acqImage.Close();
        }

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
        bool calibing = false;

        IReplyCommand RunCmdS(long cmdIndex, CalibCommand cmd)
        {
            if (!autoCalib)
            {
                autoCalib = true;

                acqImage.BeginAcqImage(cmdIndex, 1);
                log.Info(new CalibServerLogMessage(cmd.TaskName, cmd.CalibName, A_Calibration, R_Start));
            }

            return new SendCommand(cmd.CommandHeader, cmd.CalibIndex, cmd.Type, ErrorCodeConst.OK);
        }

        IReplyCommand RunCmdC(CalibCommand cmd)
        {
            if (!autoCalib)
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_RunCmd, R_Fail, "标定未开始或已结束，请先发送 S 指令"));

                return new SendCommand(cmd.CommandHeader, cmd.CalibIndex, cmd.Type, ErrorCodeConst.CalibNotStarted);
            }

            if (calibing)
            {
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_RunCmd, R_Fail, "标定中"));

                return new SendCommand(cmd.CommandHeader, cmd.CalibIndex, cmd.Type, ErrorCodeConst.Calibrating);
            }

            (int errCode, CameraImage cameraImage) = GetImage();

            CheckerboardComponent.SetInputImage(cameraImage.Bitmap, cameraImage.IsGrey);

            SendCommand sendCommand;

            if (errCode == ErrorCodeConst.OK)
            {
                try
                {
                    calibing = true;
                    CheckerboardComponent.Calibrate(cameraImage.Bitmap, cameraImage.IsGrey);

                    sendCommand = new SendCommand(cmd.CommandHeader, cmd.CalibIndex, CommandType.E, errCode);

                    log.Info(new CalibServerLogMessage(TaskName, CalibrationName, A_Calibration, R_Success));
                }
                catch (Exception e)
                {
                    log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_Calibration, R_Fail, e.Message));

                    sendCommand = new SendCommand(cmd.CommandHeader, cmd.CalibIndex, CommandType.E, ErrorCodeConst.CalibrateFail);
                }

                autoCalib = false;
                calibing = false;
            }
            else
            {
                sendCommand = new SendCommand(cmd.CommandHeader, cmd.CalibIndex, cmd.Type, errCode);
            }

            return sendCommand;
        }

        (int errorCode, CameraImage cameraImage) GetImage()
        {
            int errCode;
            CameraImage cameraImage;

            if (simulationRepo.Enabled)
                errCode = acqImage.GetImageFromLocal(CalibrationName, out cameraImage);
            else
            {
                opticsInfo = opticsRepo.GetOptics(TaskName, null, CalibrationName, materialRepo.CurSubName);

                //add by LuoDian @ 20220111 为了提升软触发的效率，把触发设置和获取图像数据分开，以便在触发完成后，立马返回信号响应，不必等图像数据输出
                errCode = acqImage.Trigger(opticsInfo);
                if (errCode != ErrorCodeConst.OK)
                {
                    log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_AcqImage, R_Fail, "触发设置失败"));
                    return (errCode, null);
                }

                //update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
                errCode = acqImage.GetImageFromCamera(out cameraImage);
            }

            if (errCode == ErrorCodeConst.OK)
            {
                GetSrcImage?.Invoke(new HyImageInfo
                {
                    Bitmap = cameraImage.Bitmap,
                    IsGrey = cameraImage.IsGrey,
                    FrameNum = cameraImage.FrameNum,
                    TaskName = TaskName,
                    AcqOrCalibName = CalibrationName,
                    AcqOrCalibIndex = 1
                });

            }
            else
            {
                // ##
                log.Error(new CalibServerLogMessage(TaskName, CalibrationName, A_AcqImage, R_Fail, ""));
            }

            return (errCode, cameraImage);
        }

        /// <summary>
        /// 手动取像
        /// </summary>
        public void AcqImage()
        {
            // CheckerBoard 标定一般需要黑白图像
            taskInfo = taskRepo.GetTaskByName(TaskName);

            if (acqImage.CheckCamera(taskInfo))
            {
                acqImage.Start(taskInfo, true);
                acqImage.BeginAcqImage(0, 1);

                (int errCode, CameraImage cameraImage) = GetImage();

                acqImage.Close();

                if (errCode == ErrorCodeConst.OK)
                    CheckerboardComponent.SetInputImage(cameraImage.Bitmap, cameraImage.IsGrey);
            }
        }

    }
}
