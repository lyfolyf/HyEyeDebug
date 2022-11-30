using Autofac;
using CameraSDK.Models;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using HyEye.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisionSDK;

namespace HyEye.WForm.Calibration
{
    public partial class FrmCheckerboardVerify : Form
    {
        static readonly Color DefaultColor = SystemColors.Control;
        static readonly Color OkColor = Color.FromArgb(32, 255, 32);
        static readonly Color ErrorColor = Color.FromArgb(255, 32, 32);

        readonly string taskName;
        readonly string calibName;

        readonly ITaskRepository taskRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ICalibrationVerifyRepository calibVerifyRepo;
        readonly ISimulationRepository simulationRepo;

        readonly ICheckerboardComponent checkerboardComponent;
        readonly IAcquireImage acqImage;
        readonly IGLog log;

        //add by LuoDian @ 20211214 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        CheckerboardVerifyInfoVO verifyInfo;
        ICalibVerifyComponent component;

        public FrmCheckerboardVerify(
            string taskName,
            string calibName,
            ITaskRepository taskRepo,
            IOpticsRepository opticsRepo,
            ICalibrationVerifyRepository calibVerifyRepo,
            ISimulationRepository simulationRepo,
            ICheckerboardComponent checkerboardComponent,
            IAcquireImage acqImage,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            InitializeComponent();

            Text = calibName + " 标定验证";

            this.taskName = taskName;
            this.calibName = calibName;

            this.taskRepo = taskRepo;
            this.opticsRepo = opticsRepo;
            this.calibVerifyRepo = calibVerifyRepo;
            this.simulationRepo = simulationRepo;

            this.checkerboardComponent = checkerboardComponent;
            this.acqImage = acqImage;
            this.log = log;

            //add by LuoDian @ 20211214 用于子料号的快速切换
            this.materialRepo = materialRepo;
        }

        private void FrmCheckerboardVerify_Load(object sender, EventArgs e)
        {
            verifyInfo = calibVerifyRepo.GetVerifyInfo<CheckerboardVerifyInfoVO>(calibName);
            if (verifyInfo == null)
                verifyInfo = new CheckerboardVerifyInfoVO { CalibrationName = calibName };

            ckbRMS.Checked = verifyInfo.RmsEnabled;
            nudRmsTolerance.Value = (decimal)verifyInfo.RmsTolerance;

            ckbAspect.Checked = verifyInfo.AspectEnabled;
            nudAspectTolerance.Value = (decimal)verifyInfo.AspectTolerance;

            ckbMeasure.Checked = verifyInfo.MeasureEnabled;
            nudMeasureTolerance.Value = (decimal)verifyInfo.MeasureTolerance;
            nudMeasureTheoreticalValue.Value = (decimal)verifyInfo.MeasureTheoreticalValue;

            component = AutoFacContainer.Resolve<ICalibVerifyComponent>(
                new NamedParameter("taskName", taskName),
                new NamedParameter("calibName", calibName),
                new NamedParameter("calibType", CalibrationType.Checkerboard));
            pnlToolBlock.Controls.Add(component.DisplayedControl);
        }

        private void ckbRMS_CheckedChanged(object sender, EventArgs e)
        {
            nudRmsTolerance.Enabled = ckbRMS.Checked;
        }

        private void ckbAspect_CheckedChanged(object sender, EventArgs e)
        {
            nudAspectTolerance.Enabled = ckbAspect.Checked;
        }

        private void ckbMeasure_CheckedChanged(object sender, EventArgs e)
        {
            nudMeasureTheoreticalValue.Enabled = ckbMeasure.Checked;
            nudMeasureTolerance.Enabled = ckbMeasure.Checked;

            pnlToolBlock.Enabled = ckbMeasure.Checked;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            pnlRMS.BackColor = DefaultColor;
            pnlAspect.BackColor = DefaultColor;
            pnlMeasure.BackColor = DefaultColor;

            if (ckbRMS.Checked)
            {
                verifyRms();
            }

            if (ckbAspect.Checked)
            {
                verifyAspect();
            }

            if (ckbMeasure.Checked)
            {
                verifyMeasure();
            }
        }

        void verifyRms()
        {
            try
            {
                double rms = checkerboardComponent.GetRMS();
                tbRmsActualValue.Text = rms.ToString();

                if (rms > (double)nudRmsTolerance.Value)
                {
                    pnlRMS.BackColor = ErrorColor;
                    log.Error($"[{calibName}]标定验证失败，RMS = {rms} 超出公差允许范围");
                }
                else
                {
                    pnlRMS.BackColor = OkColor;
                }
            }
            catch
            {
                tbRmsActualValue.Text = string.Empty;
                pnlRMS.BackColor = ErrorColor;

                log.Error($"[{calibName}]标定验证失败，获取 RMS 失败");
            }
        }

        void verifyAspect()
        {
            try
            {
                double aspect = checkerboardComponent.GetAspect();
                tbAspectActualValue.Text = aspect.ToString();

                double tolerance = Math.Abs(aspect - verifyInfo.AspectTheoreticalValue);

                if (tolerance > (double)nudAspectTolerance.Value)
                {
                    pnlAspect.BackColor = ErrorColor;
                    log.Error($"[{calibName}]标定验证失败，纵横比 = {aspect} 超出公差允许范围");
                }
                else
                {
                    pnlAspect.BackColor = OkColor;
                }
            }
            catch
            {
                tbAspectActualValue.Text = string.Empty;
                pnlAspect.BackColor = ErrorColor;

                log.Error($"[{calibName}]获取纵横比失败");
            }
        }

        void verifyMeasure()
        {
            Dictionary<string, double?> outputs = component.Run(checkerboardComponent.OutputImage);
            double? distance = outputs[InputOutputConst.Output_Distance];

            tbMeasureValue.Text = distance?.ToString();

            if (distance.HasValue)
            {
                double tolerance = Math.Abs(distance.Value - verifyInfo.MeasureTheoreticalValue);

                if (tolerance > (double)nudMeasureTolerance.Value)
                {
                    pnlMeasure.BackColor = ErrorColor;
                    log.Error($"[{calibName}]标定验证失败，标准品距离 = {distance} 超出公差允许范围");
                }
                else
                {
                    pnlMeasure.BackColor = OkColor;
                }
            }
            else
            {
                pnlMeasure.BackColor = ErrorColor;
                log.Error($"[{calibName}]测量标准品没有返回结果");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            verifyInfo.RmsEnabled = ckbRMS.Checked;
            verifyInfo.RmsTolerance = (double)nudRmsTolerance.Value;

            verifyInfo.AspectEnabled = ckbAspect.Checked;
            verifyInfo.AspectTolerance = (double)nudAspectTolerance.Value;

            verifyInfo.MeasureEnabled = ckbMeasure.Checked;
            verifyInfo.MeasureTolerance = (double)nudMeasureTolerance.Value;
            verifyInfo.MeasureTheoreticalValue = (double)nudMeasureTheoreticalValue.Value;

            calibVerifyRepo.SetVerifyInfo(verifyInfo);
            calibVerifyRepo.Save();

            component.Save();
        }

        private void btnAcqImage_Click(object sender, EventArgs e)
        {
            // Modified by louis on January 25, 2022, solve the problem that the camera is not turned off due to abnormal code
            try
            {
                TaskInfoVO taskInfo = taskRepo.GetTaskByName(taskName);

                if (!acqImage.CheckCamera(taskInfo)) return;
                acqImage.Start(taskInfo);

                int errCode;
                CameraImage cameraImage;

	            if (simulationRepo.Enabled)
	            {
	                errCode = acqImage.GetImageFromLocal(calibName, out cameraImage);
	            }
	            else
	            {
	                OpticsInfoVO opticsInfo = opticsRepo.GetOptics(taskInfo.Name, null, calibName, materialRepo.CurSubName);

	                //add by LuoDian @ 20220111 为了提升软触发的效率，把触发设置和获取图像数据分开，以便在触发完成后，立马返回信号响应，不必等图像数据输出
	                errCode = acqImage.Trigger(opticsInfo);
	                if (errCode != ErrorCodeConst.OK)
	                {
	                    MessageBoxUtils.ShowError("触发设置失败");
	                    return;
	                }

	                //update by LuoDian 在把触发与获取图像数据分开之后，已经不需要拍照信息对象OpticsInfoVO了
	                errCode = acqImage.GetImageFromCamera(out cameraImage);
	            }

                if (errCode == ErrorCodeConst.OK)
                    component.SetInputImage(cameraImage.Bitmap, cameraImage.IsGrey);
                else
                    MessageBoxUtils.ShowError("取像失败");
            }
            catch (Exception ex)
            {
                log.Error("取像失败, 错误信息：{0}", ex);
            }
            finally
            {
                acqImage.Close();
            }

        }
    }
}
