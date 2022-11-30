using Autofac;
using CameraSDK;
using CameraSDK.HIK;
using CameraSDK.Models;
using HyEye.API;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using LightControllerSDK;
using LightControllerSDK.UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using HyDisplayWindow;


namespace HyEye.WForm.Settings
{
    public partial class FrmOpticalSetting : DockContentEx
    {
        readonly ITaskRepository taskRepo;
        readonly ICameraRepository cameraRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ILightControllerRepository controllerRepo;
        readonly IControllerCollection controllerFactory;

        //add by LuoDian @ 20211214 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        // 新版显示控件替换旧版    Heweile  2022/3/31
        private HyImageViewer displayControl;


        public FrmOpticalSetting(
            ITaskRepository taskRepo,
            ICameraRepository cameraRepo,
            IOpticsRepository opticsRepo,
            ILightControllerRepository controllerRepo,
            IMaterialRepository materialRepo,
            IControllerCollection controllerFactory)
        {
            InitializeComponent();

            tbExposure.AddEvent_KeyPress_InputNumberAndPoint();
            tbGain.AddEvent_KeyPress_InputNumberAndPoint();

            loadCameraParams();

            this.taskRepo = taskRepo;
            this.cameraRepo = cameraRepo;
            this.opticsRepo = opticsRepo;
            this.controllerRepo = controllerRepo;

            this.controllerFactory = controllerFactory;

            //add by LuoDian @ 20211214 用于子料号的快速切换
            this.materialRepo = materialRepo;

            this.taskRepo.TaskRename += TaskRepo_TaskRename;
            this.taskRepo.CameraChanged += TaskRepo_CameraChanged;
            this.taskRepo.AcqImageRename += TaskRepo_AcqImageRename;
            this.taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            this.taskRepo.CalibRename += TaskRepo_CalibRename;
            this.taskRepo.CalibDelete += TaskRepo_CalibDelete;

            this.controllerRepo.ControllerChanged += ControllerRepo_ControllerChanged;
            this.controllerRepo.ControllerDelete += ControllerRepo_ControllerDelete;
            this.controllerRepo.LightNamed += ControllerRepo_LightNamed;



            displayControl = new HyImageViewer();
            displayControl.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(displayControl);
        }

        #region Task 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            if (taskName == oldName)
            {
                taskName = newName;

                Text = Utils.GetOpticalFormName(taskName, acqImageName ?? calibName);
            }
        }

        private void TaskRepo_CameraChanged(string taskName, string cameraSN)
        {
            if (camera != null)
            {
                btnCameraClose.PerformClick();

                displayControl.ClearImage();
            }
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            if (this.taskName == taskName && acqImageName == oldAcqImageName)
            {
                acqImageName = newAcqImageName;

                Text = Utils.GetOpticalFormName(taskName, acqImageName);
            }
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            if (this.taskName == taskName && this.acqImageName == acqImageName)
            {
                Close();
            }
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            if (this.taskName == taskName && calibName == oldCalibName)
            {
                calibName = newCalibName;
                Text = Utils.GetCalibrationFormName(taskName, calibName, calibType);
            }
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            if (this.taskName == taskName && this.calibName == calibName)
            {
                Close();
            }
        }

        #endregion

        #region Controller 事件

        private void ControllerRepo_ControllerChanged(string controllerName, LightControllerInfoVO newControllerInfo)
        {
            int index = controllerInfos.FindIndex(a => a.Name == controllerName);

            if (index != -1)
            {
                controllerInfos[index] = newControllerInfo;
            }

            List<Control> removedControls = new List<Control>();

            foreach (Control c in pnlLights.Controls)
            {
				//add by WuBing 添加条纹同轴光源
                if (c.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting uc = c as UcRseeMatrixLightSetting;
                    if (uc.ControllerName != controllerName)
                        continue;

                    if (uc.Index > newControllerInfo.ChannelCount)
                    {
                        removedControls.Add(c);
                        continue;
                    }

                    uc.ControllerName = newControllerInfo.Name;
                    uc.Text = $"{newControllerInfo.Name}/{newControllerInfo.Channels[uc.Index - 1]}";
                }
                else
                {
                    UcLightSettings lightSetting = (UcLightSettings)c;

                    if (lightSetting.ControllerName != controllerName)
                        continue;

                    if (lightSetting.Index > newControllerInfo.ChannelCount)
                    {
                        removedControls.Add(c);
                        continue;
                    }

                    lightSetting.ControllerName = newControllerInfo.Name;
                    lightSetting.Text = $"{newControllerInfo.Name}/{newControllerInfo.Channels[lightSetting.Index - 1]}";
                }
            }

            foreach (Control c in removedControls)
            {
                pnlLights.Controls.Remove(c);
            }
        }

        private void ControllerRepo_ControllerDelete(string name)
        {
            controllerInfos.Remove(a => a.Name == name);

            List<Control> needRemoved = new List<Control>();

            foreach (Control c in pnlLights.Controls)
            {
                if (c.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting uc = c as UcRseeMatrixLightSetting;
                    if (uc.ControllerName == name)
                        needRemoved.Add(uc);
                }
                else
                {
                    UcLightSettings uc = (UcLightSettings)c;
                    if (uc.ControllerName == name)
                        needRemoved.Add(uc);
                }
            }

            foreach (Control uc in needRemoved)
            {
                pnlLights.Controls.Remove(uc);
            }
        }

        private void ControllerRepo_LightNamed(string controllerName, int channelIndex, string lightName)
        {
            LightControllerInfoVO controllerInfo = controllerInfos.FirstOrDefault(a => a.Name == controllerName);
            if (controllerInfo == null) return;
            if (controllerInfo.ChannelCount < channelIndex) return;

            ChannelLightInfoVO channelInfo = controllerInfo.Channels[channelIndex - 1];
            channelInfo.LightName = lightName;

            foreach (Control c in pnlLights.Controls)
            {
                if (c.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting uc = c as UcRseeMatrixLightSetting;
                    if (uc.ControllerName == controllerName && uc.Index == channelIndex)
                    {
                        uc.Text = $"{controllerName}/{channelInfo}";
                    }
                }
                else
                {
                    UcLightSettings uc = (UcLightSettings)c;
                    if (uc.ControllerName == controllerName && uc.Index == channelIndex)
                    {
                        uc.Text = $"{controllerName}/{channelInfo}";
                    }
                }
            }
        }

        #endregion

        private void FrmOpticalSetting_Load(object sender, EventArgs e)
        {

        }

        ICamera camera;
        OpticsInfoVO opticsInfo;

        string taskName;
        string acqImageName;
        string calibName;

        readonly List<LightControllerInfoVO> controllerInfos = new List<LightControllerInfoVO>();

        EnumCombobox ecmbPreampGain;

        #region 加载数据

        public void LoadOptics(string taskName, string acqName, string calibName)
        {
            this.taskName = taskName;
            this.acqImageName = acqName;
            this.calibName = calibName;

            opticsInfo = opticsRepo.GetOptics(taskName, acqName, calibName, materialRepo.CurSubName);
            if (opticsInfo != null)
            {
                LoadCameraParams(opticsInfo.CameraParams);
                LoadLights(opticsInfo.LightControllerValues);
            }
        }

        void loadCameraParams()
        {
            ecmbPreampGain = new EnumCombobox(cmbPreampGain, typeof(PreampGainEnum));
        }

        void LoadCameraParams(CameraParams cameraParams)
        {
            tbExposure.Text = cameraParams.ExposureTime?.ToString();
            tbGain.Text = cameraParams.Gain?.ToString();
            ecmbPreampGain.SelectedItem = cameraParams.PreampGain;
        }

        void LoadLights(List<LightControllerValueInfoVO> controllerValues)
        {
            CloseLights();

            pnlLights.Controls.Clear();
            controllerInfos.Clear();

            if (controllerValues != null)
            {
                foreach (LightControllerValueInfoVO controllerValue in controllerValues)
                {
                    LightControllerInfoVO lightControllerInfo = controllerRepo.GetControllerInfo(controllerValue.LightControllerName);

                    if (lightControllerInfo == null)
                    {
                        // 要删除
                        continue;
                    }

                    controllerInfos.Add(lightControllerInfo);
                    /*****************************2021/10/26 18848 MAC AOI 程控条纹光专用*********************************/
                    if (lightControllerInfo.Brand == LightControllerSDK.Models.LightControllerBrand.RseeMatrix)
                    {
                        ILightController controller = controllerFactory.GetController(lightControllerInfo);
                        bool[] strips = new bool[controllerValue.ChannelValues.Count];
                        var lightness = 0;
                        foreach (ChannelValue channelValue in controllerValue.ChannelValues)
                        {
                            strips[channelValue.ChannelIndex - 1] = Convert.ToBoolean(channelValue.Lightness);
                            lightness = channelValue.Lightness > lightness ? channelValue.Lightness : lightness;
                        }
                        UcRseeMatrixLightSetting lightSettings = new UcRseeMatrixLightSetting
                        {
                            Text = $"{controllerValue.LightControllerName}",
                            MaxValue = controller.MaxLightness,
                            Index = 1,
                            ControllerName = controllerValue.LightControllerName,
                            Dock = DockStyle.Top,
                            Enabled = false,
                            ShowLightState = true,
                            LightState = true,

                        };
                        lightSettings.Strips = strips;
                        lightSettings.SetInitialValue(lightness);
                        lightSettings.ValueChanged += setMatrixLightness;
                        pnlLights.Controls.Add(lightSettings);
                        //lightSettings.BringToFront();
                    }
                    else
                    {
                        foreach (ChannelValue channelValue in controllerValue.ChannelValues)
                        {
                            ILightController controller = controllerFactory.GetController(lightControllerInfo);

                            UcLightSettings lightSettings = new UcLightSettings
                            {
                                Text = $"{controllerValue.LightControllerName}/{lightControllerInfo.Channels[channelValue.ChannelIndex - 1]}",
                                MaxValue = controller.MaxLightness,
                                Index = channelValue.ChannelIndex,
                                ControllerName = controllerValue.LightControllerName,
                                Dock = DockStyle.Top,
                                Enabled = false,
                                ShowLightState = true,
                                LightState = channelValue.LightState
                            };
                            lightSettings.SetInitialValue(channelValue.Lightness);
                            lightSettings.ValueChanged += setLightness;

                            pnlLights.Controls.Add(lightSettings);
                            //lightSettings.BringToFront();
                        }
                    }
                    //foreach (ChannelValue channelValue in controllerValue.ChannelValues)
                    //{
                    //    ILightController controller = controllerFactory.GetController(lightControllerInfo);

                    //    UcLightSettings lightSettings = new UcLightSettings
                    //    {
                    //        Text = $"{controllerValue.LightControllerName}/{lightControllerInfo.Channels[channelValue.ChannelIndex - 1]}",
                    //        MaxValue = controller.MaxLightness,
                    //        Index = channelValue.ChannelIndex,
                    //        ControllerName = controllerValue.LightControllerName,
                    //        Dock = DockStyle.Top,
                    //        Enabled = false,
                    //        ShowLightState = true,
                    //        LightState = channelValue.LightState
                    //    };
                    //    lightSettings.SetInitialValue(channelValue.Lightness);
                    //    lightSettings.ValueChanged += setLightness;

                    //    pnlLights.Controls.Add(lightSettings);
                    //    lightSettings.BringToFront();
                    //}

                }
            }
        }

        /*****************************2021/10/26 18848 MAC AOI 程控条纹光专用*********************************/
        /// <summary>
        ///  调节程控条纹光光源亮度及条纹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setMatrixLightness(object sender, EventArgs e)
        {
            LightControllerSDK.UC.UcRseeMatrixLightSetting uc = sender as LightControllerSDK.UC.UcRseeMatrixLightSetting;
            LightControllerInfoVO controllerInfo = controllerRepo.GetControllerInfo(uc.ControllerName);

            ILightController controller = controllerFactory.GetController(controllerInfo);

            //controller.SetLightness(uc.Index, uc.Value);
            var lightness = new int[uc.Strips.Length];
            for (int i = 0; i < lightness.Length; i++)
            {
                lightness[i] = uc.Strips[i] == false ? Convert.ToInt32(uc.Strips[i]) : uc.Value;
            }
            int[] chs = ConvertToIntArray(uc.Strips);
            controller.SetMultiLightness(chs, lightness);
        }

        // 调节光源亮度
        void setLightness(object sender, EventArgs e)
        {
            UcLightSettings uc = sender as UcLightSettings;

            LightControllerInfoVO controllerInfo = controllerRepo.GetControllerInfo(uc.ControllerName);

            ILightController controller = controllerFactory.GetController(controllerInfo);
            controller.SetLightness(uc.Index, uc.Value);
        }

        #endregion

        void CloseCamera()
        {
            if (camera != null && camera.IsOpen && !btnCameraOpen.Enabled)
            {
                camera.Close();
                camera.ImageReceived -= Camera_ImageReceived;
                camera = null;

                if (btnContinuous.Text == "停止实时")
                    btnContinuous.Text = "实时";

                btnCameraOpen.Enabled = true;
                btnCameraClose.Enabled = false;

                btnContinuous.Enabled = false;
                btnTriggerExec.Enabled = false;

                btnSetCameraParams.Enabled = false;
            }
        }

        void CloseLights()
        {
            foreach (LightControllerInfoVO controllerInfo in controllerInfos)
            {
                ILightController controller = controllerFactory.GetController(controllerInfo);
                if (controller.Connected)
                    setControllerLightnessZero(controllerInfo.Name, controller);
                controller.Disconnect();

                controllerInfo.Connected = false;
            }

        }

        #region 光源

        int[] ConvertToIntArray(bool[] strips)
        {
            int[] chs = new int[strips.Length];
            for (int i = 0; i < strips.Length; i++)
            {
                chs[i] = Convert.ToInt32(strips[i]);
            }

            return chs;
        }

        // 设置光源调试器是否启用
        void setLightSettingsEnabled(string controllerName, bool enabled)
        {
            foreach (Control control in pnlLights.Controls)
            {
                if (control.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting ucr = control as UcRseeMatrixLightSetting;
                    if (ucr.ControllerName == controllerName)
                    {
                        ucr.Enabled = enabled;
                    }
                }
                else
                {
                    UcLightSettings uc = control as UcLightSettings;

                    if (uc.ControllerName == controllerName)
                    {
                        uc.Enabled = enabled;
                    }
                }
            }
        }

        // 设置光源控制器亮度
        void setControllerLightness(string controllerName, ILightController controller)
        {
            foreach (Control control in pnlLights.Controls)
            {
                if (control.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting uc = control as UcRseeMatrixLightSetting;
                    if (uc.ControllerName == controllerName)
                    {
                        //controller.SetLightness(uc.Index, uc.Value);
                        var lightness = new int[uc.Strips.Length];
                        for (int i = 0; i < lightness.Length; i++)
                        {
                            lightness[i] = uc.Strips[i] == false ? Convert.ToInt32(uc.Strips[i]) : uc.Value;
                        }

                        int[] chs = ConvertToIntArray(uc.Strips);
                        controller.SetMultiLightness(chs, lightness);
                    }
                }
                else
                {
                    UcLightSettings uc = control as UcLightSettings;

                    if (uc.ControllerName == controllerName)
                    {
                        controller.SetLightness(uc.Index, uc.Value);
                    }
                }
            }
        }

        void setControllerLightnessZero(string controllerName, ILightController controller)
        {
            foreach (Control control in pnlLights.Controls)
            {
                if (control.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting uc = control as UcRseeMatrixLightSetting;
                    if (uc.ControllerName == controllerName)
                    {
                        //controller.SetLightness(uc.Index, uc.Value);
                        var lightness = new int[uc.Strips.Length];
                        for (int i = 0; i < lightness.Length; i++)
                        {
                            lightness[i] = 0;
                        }
                        int[] chs = ConvertToIntArray(uc.Strips);
                        controller.SetMultiLightness(chs, lightness);
                    }
                }
                else
                {
                    UcLightSettings uc = control as UcLightSettings;

                    if (uc.ControllerName == controllerName)
                    {
                        controller.SetLightness(uc.Index, 0);
                    }
                }
            }
        }

        private void btnConnectController_Click(object sender, EventArgs e)
        {
            btnConnectController.Enabled = false;

            foreach (LightControllerInfoVO controllerInfo in controllerInfos)
            {
                if (controllerInfo.Connected)
                {
                    MessageBoxUtils.ShowWarn($"控制器[{controllerInfo.Name}]已打开，请先关闭");
                    return;
                }
            }

            try
            {
                foreach (LightControllerInfoVO controllerInfo in controllerInfos)
                {
                    ILightController controller = controllerFactory.GetController(controllerInfo);

                    controller.Connect();

                    controllerInfo.Connected = true;

                    setControllerLightness(controllerInfo.Name, controller);

                    setLightSettingsEnabled(controllerInfo.Name, true);
                }
            }
            catch (Exception ex)
            {
                btnConnectController.Enabled = true;
            }
        }

        private void btnCloseController_Click(object sender, EventArgs e)
        {
            CloseLights();

            foreach (LightControllerInfoVO controllerInfo in controllerInfos)
            {
                setLightSettingsEnabled(controllerInfo.Name, false);
            }

            btnConnectController.Enabled = true;
        }

        // 选择光源
        private void btnSelectLight_Click(object sender, EventArgs e)
        {
            List<LightControllerValueInfoVO> controllerValues = assignmentLightParams();

            FrmLightControllerSearch frm = AutoFacContainer.Resolve<FrmLightControllerSearch>(
                new TypedParameter(typeof(List<LightControllerValueInfoVO>), controllerValues));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadLights(frm.Data);
            }
        }

        #endregion

        #region 相机设置

        private void btnCameraOpen_Click(object sender, EventArgs e)
        {
            TaskInfoVO task = taskRepo.GetTaskByName(taskName);
            ResetParams(task.CameraAcquireImage.CameraSN);

            camera = cameraRepo.GetCamera(task.CameraAcquireImage.CameraSN);

            if (camera == null)
                throw new Exception("未找到相机");
            if (camera.IsOpen)
                throw new Exception("相机已打开");

            camera.ImageReceived += Camera_ImageReceived;

            camera.Open();

            if (opticsInfo == null)
            {
                getCameraParams();
            }
            else
            {
                setCameraParams();
            }

            camera.Start(TriggerMode.Trigger, TriggerSource.Software);

            btnCameraOpen.Enabled = false;
            btnCameraClose.Enabled = true;

            btnContinuous.Enabled = true;
            btnTriggerExec.Enabled = true;

            btnSetCameraParams.Enabled = true;
        }

        // 设置参数的显示
        void ResetParams(string cameraSN)
        {
            List<CameraParamInfoVO> paramInfos = cameraRepo.GetParamInfos(cameraSN);
            if (paramInfos == null) return;

            foreach (CameraParamInfoVO paramInfo in paramInfos)
            {
                switch (paramInfo.Name)
                {
                    case CameraParamList.C_Gain:
                        tbGain.Visible = paramInfo.Enabled;
                        break;
                    case CameraParamList.C_PreampGain:
                        cmbPreampGain.Visible = paramInfo.Enabled;
                        break;
                }
            }
        }

        private void Camera_ImageReceived(object sender, CameraImageEventArgs e)
        {
            displayControl.DisplayImage(e.CameraImage.Bitmap);
        }

        private void btnCameraClose_Click(object sender, EventArgs e)
        {
            CloseCamera();
        }

        private void btnContinuous_Click(object sender, EventArgs e)
        {
            if (btnContinuous.Text == "实时")
            {
                btnContinuous.Text = "停止实时";
                btnTriggerExec.Enabled = false;

                camera.SetTriggerMode(TriggerMode.Continuous);
            }
            else
            {
                btnContinuous.Text = "实时";
                btnTriggerExec.Enabled = true;

                camera.SetTriggerMode(TriggerMode.Trigger);
            }
        }

        private void btnTriggerExec_Click(object sender, EventArgs e)
        {
            camera.SoftTrigger();
        }

        void getCameraParams()
        {
            CameraParams param = camera.GetParams();

            tbExposure.Text = param.ExposureTime?.ToString();
            tbGain.Text = param.Gain?.ToString();
        }

        private void btnSetCameraParams_Click(object sender, EventArgs e)
        {
            setCameraParams();
        }

        void setCameraParams()
        {
            CameraParams @params = new CameraParams
            {
                ExposureTime = tbExposure.Text.Length > 0 ? float.Parse(tbExposure.Text) : (float?)null,
                Gain = tbGain.Text.Length > 0 ? float.Parse(tbGain.Text) : (float?)null,
                PreampGain = (PreampGainEnum?)ecmbPreampGain.SelectedItem
            };

            camera.SetParams(@params);
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!tbExposure.CheckNotEmpty("请输入曝光值"))
                return;

            if (opticsInfo == null)
            {
                opticsInfo = new OpticsInfoVO
                {
                    TaskName = taskName,
                    AcquireImageName = acqImageName,
                    CalibrationName = calibName
                };
            }

            opticsInfo.CameraParams = assignmentCameraParams();
            opticsInfo.LightControllerValues = assignmentLightParams();

            opticsRepo.AddOrUpdate(opticsInfo);
            opticsRepo.Save();
        }

        CameraParams assignmentCameraParams()
        {
            return new CameraParams
            {
                ExposureTime = float.Parse(tbExposure.Text),
                Gain = tbGain.Text.Length > 0 ? float.Parse(tbGain.Text) : (float?)null
            };
        }

        List<LightControllerValueInfoVO> assignmentLightParams()
        {
            List<LightControllerValueInfoVO> controllerValues = new List<LightControllerValueInfoVO>();

            foreach (Control control in pnlLights.Controls)
            {
                if (control.GetType() == typeof(LightControllerSDK.UC.UcRseeMatrixLightSetting))
                {
                    UcRseeMatrixLightSetting uc = control as UcRseeMatrixLightSetting;
                    LightControllerValueInfoVO controllerValue = controllerValues.FirstOrDefault(a => a.LightControllerName == uc.ControllerName);
                    if (controllerValue == null)
                    {
                        controllerValue = new LightControllerValueInfoVO { LightControllerName = uc.ControllerName };
                        controllerValues.Add(controllerValue);
                    }

                    for (int i = 0; i < uc.Strips.Length; i++)
                    {
                        var lightness = Convert.ToInt32(uc.Strips[i]) * uc.Value;
                        controllerValue.ChannelValues.Add(new ChannelValue { ChannelIndex = i + 1, Lightness = lightness, LightState = uc.LightState });
                    }

                }
                else
                {
                    UcLightSettings uc = (UcLightSettings)control;

                    LightControllerValueInfoVO controllerValue = controllerValues.FirstOrDefault(a => a.LightControllerName == uc.ControllerName);
                    if (controllerValue == null)
                    {
                        controllerValue = new LightControllerValueInfoVO { LightControllerName = uc.ControllerName };
                        controllerValues.Add(controllerValue);
                    }

                    controllerValue.ChannelValues.Add(new ChannelValue { ChannelIndex = uc.Index, Lightness = uc.Value, LightState = uc.LightState });
                }
            }

            return controllerValues;
        }

        private void FrmOpticalSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CloseLights();
            }
            catch
            {

            }
            CloseCamera();
        }

        private void FrmOpticalSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                if (btnCameraClose.Enabled)
                    CloseCamera();

                try
                {
                    CloseLights();
                }
                catch
                {

                }
            }
        }

        private void btnSetAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (LightControllerInfoVO controllerInfo in controllerInfos)
                {
                    ILightController controller = controllerFactory.GetController(controllerInfo);
                    setControllerLightness(controllerInfo.Name, controller);

                    //setLightSettingsEnabled(controllerInfo.Name, true);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
