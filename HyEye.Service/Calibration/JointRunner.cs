using CameraSDK.Models;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.Services
{
    public class JointRunner
    {
        public event GetSrcImageHandle GetSrcImage;

        public event Action<string, string, object> ShowRecord;

        public IJointComponent JointComponent { get; }

        public string CalibrationName { get; private set; }

        readonly IHandeyeComponent handeyeComponent;
        readonly ICalibrationRepository calibRepo;
        readonly ISimulationRepository simulationRepo;
        readonly IGLog log;

        public JointRunner(
            string calibName,
            IJointComponent jointComponent,
            IHandeyeComponent handeyeComponent,
            ICalibrationRepository calibRepo,
            ISimulationRepository simulationRepo,
            IGLog log)
        {
            CalibrationName = calibName;
            this.handeyeComponent = handeyeComponent;
            this.JointComponent = jointComponent;
            this.calibRepo = calibRepo;
            this.simulationRepo = simulationRepo;
            this.log = log;
        }

        Dictionary<string, IAcquireImage> acqImgs = new Dictionary<string, IAcquireImage>();

        public LinkedDictionary<string, object> Run(string taskName, string acqName)
        {
            IAcquireImage acqImage = GetAcqImg(taskName);
            acqImage.Start(taskName, true);

            acqImage.BeginAcqImage(0, 1);
            (int errCode, CameraImage cameraImage) = GetImage(acqImage, taskName, acqName);
            acqImage.Close();

            if (errCode != ErrorCodeConst.OK)
            {
                return null;
            }

            int errorCode;
            LinkedDictionary<string, object> outputs;

            CalibrationInfoVO calib = calibRepo.GetCalibration(CalibrationName);
            if (calib.JointInfo.Master.TaskName == taskName && calib.JointInfo.Master.AcqImageName == acqName)
            {
                object img = handeyeComponent.RunNP(cameraImage.Bitmap, cameraImage.IsGrey);
                (errorCode, outputs) = JointComponent.Run(taskName, acqName, img, cameraImage.IsGrey);
            }
            else
            {
                (errorCode, outputs) = JointComponent.Run(taskName, acqName, cameraImage.Bitmap, cameraImage.IsGrey);
            }

            if (outputs.ContainsKey("Record"))
            {
                ShowRecord?.Invoke(taskName, acqName, outputs["Record"]);
                outputs.Remove("outputs");
            }

            if (errorCode != ErrorCodeConst.OK)
            {
                return null;
            }

            return outputs;
        }

        public void Calibration(string taskName, string acqName, List<(double X1, double Y1, double X2, double Y2)> points)
        {
            JointComponent.Calibration(taskName, acqName, points);
        }

        IAcquireImage GetAcqImg(string taskName)
        {
            if (acqImgs.ContainsKey(taskName))
                return acqImgs[taskName];
            else
            {
                IAcquireImage acqImg = Autofac.AutoFacContainer.Resolve<IAcquireImage>();
                acqImgs.Add(taskName, acqImg);

                return acqImg;
            }
        }

        (int errorCode, CameraImage cameraImage) GetImage(IAcquireImage acqImage, string taskName, string acqName)
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
                acqImage.Trigger(null);

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
                    TaskName = taskName,
                    AcqOrCalibName = acqName,
                    CalibName = CalibrationName,
                    AcqOrCalibIndex = 1
                });
            }
            else
            {
                // ##
                log.Error(new CalibServerLogMessage(CalibrationName, $"{taskName}-{acqName}", A_AcqImage, R_Fail, ""));
            }

            return (errorCode, cameraImage);
        }

        public Task<IReplyCommand> RunCommand(long cmdIndex, CalibCommand cmd)
        {
            return null;
        }
    }
}
