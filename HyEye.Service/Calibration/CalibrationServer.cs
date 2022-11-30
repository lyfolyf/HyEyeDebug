using Autofac;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using VisionFactory;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    public interface ICalibrationService
    {
        event EventHandler<SendCmdsEventArgs> SendCommands;

        CheckerboardRunner CreateCheckerboardRunner(CalibrationInfoVO calibInfo);

        HandeyeRunner CreateHandeyeRunner(CalibrationInfoVO calibInfo);

        HandeyeSingleRunner CreateHandeyeSingleRunner(CalibrationInfoVO calibInfo);

        JointRunner CreateJointRunner(CalibrationInfoVO calibInfo);

        /// <summary>
        /// 执行标定指令
        /// </summary>
        Task<IReplyCommand> RunCommands(ReceiveCommand recvCmd);
    }

    public class CalibrationService : ICalibrationService
    {
        public event EventHandler<SendCmdsEventArgs> SendCommands;

        readonly ITaskRepository taskRepo;
        readonly ICalibrationRepository calibRepo;
        readonly ICommandRepository commandRepo;
        readonly IImageService imageService;
        readonly IGLog log;
        readonly CalibrationComponentSet calibComponentSet;
        readonly HandEyeDisplayComponentSet handEyeDisplayComponentSet;
        readonly JointDisplayComponentSet jointDisplayComponentSet;

        readonly Dictionary<string, HandeyeRunner> handeyeRunners = new Dictionary<string, HandeyeRunner>();
        readonly Dictionary<string, HandeyeSingleRunner> handeyeSingleRunners = new Dictionary<string, HandeyeSingleRunner>();
        readonly Dictionary<string, CheckerboardRunner> checkerboardRunners = new Dictionary<string, CheckerboardRunner>();
        readonly Dictionary<string, JointRunner> jointRunners = new Dictionary<string, JointRunner>();

        public CalibrationService(
            ITaskRepository taskRepo,
            ICalibrationRepository calibRepo,
            ICommandRepository commandRepo,
            IImageService imageService,
            IGLog log,
            CalibrationComponentSet calibComponentSet,
            HandEyeDisplayComponentSet handEyeDisplayComponentSet,
            JointDisplayComponentSet jointDisplayComponentSet)
        {
            this.taskRepo = taskRepo;
            this.calibRepo = calibRepo;
            this.commandRepo = commandRepo;
            this.imageService = imageService;
            this.log = log;
            this.calibComponentSet = calibComponentSet;
            this.handEyeDisplayComponentSet = handEyeDisplayComponentSet;
            this.jointDisplayComponentSet = jointDisplayComponentSet;
        }

        #region 获取/创建标定

        public CheckerboardRunner CreateCheckerboardRunner(CalibrationInfoVO calibInfo)
        {
            CheckerboardRunner checkerboardRunner;

            if (checkerboardRunners.ContainsKey(calibInfo.Name))
            {
                checkerboardRunner = checkerboardRunners[calibInfo.Name];
            }
            else
            {
                ICheckerboardComponent component = calibComponentSet.GetCheckerboardComponent(calibInfo.Name);

                checkerboardRunner = AutoFacContainer.Resolve<CheckerboardRunner>(
                    new NamedParameter("taskName", calibInfo.TaskName),
                    new NamedParameter("calibName", calibInfo.Name),
                    new NamedParameter("checkerboardComponent", component)
                    );
                checkerboardRunner.GetSrcImage += CheckerboardRunner_GetSrcImage;

                checkerboardRunners.Add(calibInfo.Name, checkerboardRunner);
            }

            return checkerboardRunner;
        }

        public HandeyeRunner CreateHandeyeRunner(CalibrationInfoVO calibInfo)
        {
            HandeyeRunner handeyeRunner;
            if (handeyeRunners.ContainsKey(calibInfo.Name))
            {
                handeyeRunner = handeyeRunners[calibInfo.Name];
            }
            else
            {
                IHandeyeComponent handeyeComponent = calibComponentSet.GetHandeyeComponent(calibInfo.Name);
                IDisplayImageComponent displayImageComponent = handEyeDisplayComponentSet.GetDisplayControl(calibInfo.TaskName, calibInfo.Name);

                handeyeRunner = AutoFacContainer.Resolve<HandeyeRunner>(
                        new NamedParameter("taskName", calibInfo.TaskName),
                        new NamedParameter("calibName", calibInfo.Name),
                        new NamedParameter("handeyeComponent", handeyeComponent),
                        new NamedParameter("displayImageComponent", displayImageComponent));
                handeyeRunner.GetSrcImage += HandeyeRunner_GetSrcImage;
                handeyeRunner.GetAfterCheckerboardImage += HandeyeRunner_GetAfterCheckerboardImage;
                handeyeRunner.AfterGetPose += HandeyeRunner_AfterGetPose;

                handeyeRunners.Add(calibInfo.Name, handeyeRunner);
            }

            return handeyeRunner;
        }

        public HandeyeSingleRunner CreateHandeyeSingleRunner(CalibrationInfoVO calibInfo)
        {
            HandeyeSingleRunner handeyeSingleRunner;
            if (handeyeSingleRunners.ContainsKey(calibInfo.Name))
            {
                handeyeSingleRunner = handeyeSingleRunners[calibInfo.Name];
            }
            else
            {
                IHandeyeSingleComponent handeyeSingleComponent = calibComponentSet.GetHandeyeSingleComponent(calibInfo.Name);
                IDisplayImageComponent displayImageComponent = handEyeDisplayComponentSet.GetDisplayControl(calibInfo.TaskName, calibInfo.Name);

                handeyeSingleRunner = AutoFacContainer.Resolve<HandeyeSingleRunner>(
                        new NamedParameter("taskName", calibInfo.TaskName),
                        new NamedParameter("calibName", calibInfo.Name),
                        new NamedParameter("handeyeSingleComponent", handeyeSingleComponent),
                        new NamedParameter("displayImageComponent", displayImageComponent));
                handeyeSingleRunner.GetSrcImage += HandeyeRunner_GetSrcImage;
                handeyeSingleRunner.GetAfterCheckerboardImage += HandeyeRunner_GetAfterCheckerboardImage;
                handeyeSingleRunner.AfterGetPose += HandeyeSingleRunner_AfterGetPose;

                handeyeSingleRunners.Add(calibInfo.Name, handeyeSingleRunner);
            }

            return handeyeSingleRunner;
        }

        public JointRunner CreateJointRunner(CalibrationInfoVO calibInfo)
        {
            if (jointRunners.ContainsKey(calibInfo.Name))
            {
                return jointRunners[calibInfo.Name];
            }
            else
            {
                IJointComponent jointComponent = calibComponentSet.GetJointComponent(calibInfo.Name);

                string handEyeName = taskRepo.GetTaskByName(calibInfo.JointInfo.Master.TaskName)
                    .CameraAcquireImage.AcquireImages.First(a => a.JointName == calibInfo.Name).HandEyeNames[0];

                IHandeyeComponent handeyeComponent = calibComponentSet.GetHandeyeComponent(handEyeName);

                JointRunner jointRunner = AutoFacContainer.Resolve<JointRunner>(
                    new NamedParameter("calibName", calibInfo.Name),
                    new NamedParameter("jointComponent", jointComponent),
                    new NamedParameter("handeyeComponent", handeyeComponent));
                jointRunner.GetSrcImage += JointRunner_GetSrcImage;
                jointRunner.ShowRecord += JointRunner_ShowRecord;

                jointRunners.Add(calibInfo.Name, jointRunner);
                return jointRunner;
            }
        }

        #endregion

        #region Runner 事件

        private void CheckerboardRunner_GetSrcImage(HyImageInfo hyImage)
        {
            if (hyImage.Bitmap == null) return;

            imageService.SaveImage(hyImage.Clone(), true, true, ImageFlag.OK);
        }

        private void HandeyeRunner_GetSrcImage(HyImageInfo hyImage)
        {
            if (hyImage.Bitmap == null) return;

            Bitmap copy = (Bitmap)hyImage.Bitmap.Clone();
            IDisplayTaskImageComponent displayComponent = handEyeDisplayComponentSet.GetDisplayControl(hyImage.TaskName, hyImage.AcqOrCalibName);
            displayComponent.ShowImage(copy, false);

            imageService.SaveImage(hyImage.Clone(), true, true, ImageFlag.OK);
        }

        private void HandeyeRunner_GetAfterCheckerboardImage(string taskName, string acqOrCalibName, object img, bool isGrey)
        {
            if (img == null) return;

            IDisplayTaskImageComponent displayComponent = handEyeDisplayComponentSet.GetDisplayControl(taskName, acqOrCalibName);

            displayComponent.ShowImage(img, false);
        }

        private void HandeyeRunner_AfterGetPose(string taskName, string calibName, int index, double x1, double y1, double a1, double? x2, double? y2, double? a2, object graphic)
        {
            IDisplayTaskImageComponent displayControl = handEyeDisplayComponentSet.GetDisplayControl(taskName, calibName);

            displayControl.ShowGraphic(graphic);

            Bitmap resultBmp = displayControl.CreateContentBitmap();
            if (resultBmp != null)
            {
                imageService.SaveImage(new HyImageInfo
                {
                    Bitmap = resultBmp,
                    IsGrey = false,
                    FrameNum = 0,
                    TaskName = taskName,
                    AcqOrCalibName = calibName,
                    AcqOrCalibIndex = index,
                }, false, true, ImageFlag.OK);
            }
        }

        private void HandeyeSingleRunner_AfterGetPose(string taskName, string calibName, List<HandeyeSinglePoint> points, object graphic)
        {
            IDisplayTaskImageComponent displayControl = handEyeDisplayComponentSet.GetDisplayControl(taskName, calibName);

            displayControl.ShowGraphic(graphic);

            Bitmap resultBmp = displayControl.CreateContentBitmap();
            if (resultBmp != null)
            {
                imageService.SaveImage(new HyImageInfo
                {
                    Bitmap = resultBmp,
                    IsGrey = false,
                    FrameNum = 0,
                    TaskName = taskName,
                    AcqOrCalibName = calibName,
                    AcqOrCalibIndex = 1,
                }, false, true, ImageFlag.OK);
            }
        }

        private void JointRunner_GetSrcImage(HyImageInfo hyImage)
        {
            if (hyImage.Bitmap == null) return;

            Bitmap copy = (Bitmap)hyImage.Bitmap.Clone();
            IDisplayTaskImageComponent displayComponent = jointDisplayComponentSet.GetJointDisplayControl(hyImage.TaskName, hyImage.AcqOrCalibName);
            displayComponent.ShowImage(copy, false);

            imageService.SaveImage(hyImage.Clone(), true, true, ImageFlag.OK);
        }

        private void JointRunner_ShowRecord(string taskName, string acqName, object record)
        {
            IDisplayTaskImageComponent displayComponent = jointDisplayComponentSet.GetJointDisplayControl(taskName, acqName);
            displayComponent.ShowGraphic(record);
        }

        #endregion

        ///// <summary>
        ///// 结束自动标定，发送 E 指令
        ///// </summary>
        //public void EndAutoCalibration(string calibName)
        //{
        //    SendCommandInfoVO sendCommandInfo = commandRepo.GetCalibrationSendCommand(calibName);
        //    SendCommand cmd = new SendCommand
        //    {
        //        CommandHeader = sendCommandInfo.CommandHeader,
        //        AcquireImageIndex = 0,
        //        Type = CommandType.E,
        //        ErrorCode = ErrorCodeConst.ActiveStop
        //    };

        //    CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
        //    log.Info(new CalibLogMessage(calibInfo.TaskName, calibName, A_Calibration, R_End));

        //    SendCommands?.Invoke(new IReplyCommand[] { cmd });

        //    if (handeyeRunners.ContainsKey(calibName))
        //    {
        //        HandeyeRunner handeyeRunner = handeyeRunners[calibName];
        //        handeyeRunner.AutoStop();
        //        handeyeRunner.Close();
        //    }
        //}

        /// <summary>
        /// 执行标定指令
        /// </summary>
        public async Task<IReplyCommand> RunCommands(ReceiveCommand recvCmd)
        {
            CalibCommand cmd = recvCmd.CalibCommand;

            if (checkerboardRunners.ContainsKey(cmd.CalibName))
            {
                CheckerboardRunner checkerboardRunner = checkerboardRunners[cmd.CalibName];

                return await checkerboardRunner.RunCommand(recvCmd.Index, cmd);
            }
            else if (handeyeRunners.ContainsKey(cmd.CalibName))
            {
                HandeyeRunner handeyeRunner = handeyeRunners[cmd.CalibName];

                return await handeyeRunner.RunCommand(recvCmd.Index, cmd);
            }
            else if (handeyeSingleRunners.ContainsKey(cmd.CalibName))
            {
                HandeyeSingleRunner handeyeSingleRunner = handeyeSingleRunners[cmd.CalibName];

                return await handeyeSingleRunner.RunCommand(recvCmd.Index, cmd);
            }
            else
            {
                log?.Error(new CalibServerLogMessage(cmd.TaskName, cmd.CalibName, A_RunCmd, R_Fail, "标定未开启"));
                return new SendCommand(cmd.CommandHeader, cmd.CalibIndex, cmd.Type, ErrorCodeConst.CalibNotStarted);
            }
        }

    }
}
