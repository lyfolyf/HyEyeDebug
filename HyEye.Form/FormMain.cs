using Autofac;
using GL.Kit.Extension;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using HyEye.Services;
using HyEye.WForm.Calibration;
using HyEye.WForm.Display;
using HyEye.WForm.Login;
using HyEye.WForm.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VisionFactory;
using WeifenLuo.WinFormsUI.Docking;
using IImageService = HyEye.Services.IImageService;

namespace HyEye.WForm
{
    public partial class FormMain : Form
    {
        readonly ISimulationRepository simulationRepo;
        readonly ITaskRepository taskRepo;
        readonly ICommunicationRepository commRepo;
        readonly IBasicRepository basicRepo;
        readonly IUserRepository userRepo;
        readonly IMaterialRepository materialRepo;
        readonly IRecordShowRepository recordShowRepo;

        readonly ITaskService taskService;
        readonly ICommunicationService commService;
        readonly IImageService imageService;

        //add by LuoDian @ 20210923 用于添加一键运行功能时，生成执行TaskRunner的指令
        readonly ICommandService commandService;

        readonly ISystemRepository systemRepo;

        readonly LogPublisher log;

        readonly ToolBlockComponentSet toolBlockComponents;

        readonly bool hidden = false;

        //update by LuoDian @ 20210923 添加一个ICommandService对象，用于添加一键运行功能时，生成执行TaskRunner的指令
        public FormMain(
            ITaskRepository taskRepo,
            ISimulationRepository simulationRepo,
            ICommunicationRepository commRepo,
            IBasicRepository basicRepo,
            IUserRepository userRepo,
            IMaterialRepository materialRepo,
            IRecordShowRepository recordShowRepo,
            IPermission permission,
            ITaskService taskService,
            ICommunicationService commService,
            ICommandService commandService,
            IImageService imageService,
            ISystemRepository systemRepo,
            LogPublisher log,
            ToolBlockComponentSet toolBlockComponents)
        {
            InitializeComponent();

            Text = $"{Global.Name} [V{Global.Version}]";

            tslblTime.Alignment = ToolStripItemAlignment.Right;

            this.taskRepo = taskRepo;
            this.commRepo = commRepo;
            this.simulationRepo = simulationRepo;
            this.basicRepo = basicRepo;
            this.userRepo = userRepo;
            this.materialRepo = materialRepo;
            this.recordShowRepo = recordShowRepo;
            this.toolBlockComponents = toolBlockComponents;
            this.systemRepo = systemRepo;

            taskRepo.TaskAdd += TaskRepo_TaskAdd;
            taskRepo.TaskDelete += TaskRepo_TaskDelete;
            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            taskRepo.AcqImageRename += TaskRepo_AcqImageRename;
            taskRepo.CalibAdd += TaskRepo_CalibAdd;
            taskRepo.CalibDelete += TaskRepo_CalibDelete;
            taskRepo.CalibRename += TaskRepo_CalibRename;

            simulationRepo.EnabledChanged += SimulationRepo_EnabledChanged;

            userRepo.AfterLogin += UserRepo_AfterLogin;
            userRepo.AfterExit += UserRepo_AfterExit;

            this.taskService = taskService;
            this.commService = commService;
            this.imageService = imageService;

            //add by LuoDian @ 20210923 用于添加一键运行功能时，生成执行TaskRunner的指令
            this.commandService = commandService;

            this.log = log;

            taskService.StateChanged += taskService_StateChanged;
            commService.ConnectedChanged += CommService_ConnectedChanged;

            permission.SetPermission(this);

            //add by LuoDian @ 20220119 设置线程池的数量限制
            ThreadPool.SetMaxThreads(2500, 1500);
            ThreadPool.SetMinThreads(1000, 700);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            log.Info(new BaseGeneralLogMessage("HyInspect", "", "软件启动", "成功", "当前版本：" + Global.Version));

            tslblMT.Text = $"当前料号：[{materialRepo.CurrMaterial}]            当前用户：[{userRepo.CurrUser?.UserName}]            用户角色：[{userRepo.CurrUser?.Role.ToDescription()}]";

            tslblPSR.Text = "正常运行状态";
            tslblPSR.ForeColor = Color.Green;

            tslblLayout.Width = statusStrip1.Width / 2 - tslblConnectionState.Width - tslblMT.Width / 2;

            loginEnabled(userRepo.CurrUser != null);

            timer1.Start();

            ShowFrmDisplay();
            ShowLog();

            var taskInfos = taskRepo.GetTasks();

            LoadOpticalSettingMenu(taskInfos);
            LoadRunTaskMenu(taskInfos);
            LoadCalibMenu(taskInfos);

            if (basicRepo.AutoStart)
                Start();

            setDisable();

            if (systemRepo.AutoSaveConfig)
            {
                Thread thConfigSave = new Thread(ConfigAutoSave);
                thConfigSave.IsBackground = true;
                thConfigSave.Start();
            }
        }

        void ConfigAutoSave()
        {
            string ConfigPath = PathUtils.CurrentDirectory + "config\\";
            if (!Directory.Exists(systemRepo.ConfigSavePath))
                Directory.CreateDirectory(systemRepo.ConfigSavePath);

            DirectoryInfo bakDic = new DirectoryInfo(systemRepo.ConfigSavePath);
            List<DirectoryInfo> bakFiles = bakDic.GetDirectories().OrderByDescending(p => p.Name).ToList();

            string bakPath = systemRepo.ConfigSavePath + $"Config_{DateTime.Now.ToString("yyyyMMddHHmm")}\\";
            if (bakFiles.Count > 0)
            {
                DirectoryInfo dir = bakFiles[0];
                DateTime dtLast = DateTime.ParseExact(dir.Name.Split('_')[1], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                if (dtLast.AddDays(systemRepo.SaveType) <= DateTime.Now)
                {
                    DirectoryUtils.Copy(ConfigPath, bakPath);
                    log.Info(new BaseGeneralLogMessage("HyEye", "AutoSaveConfig", "配置自动保存", "成功", "保存地址：" + bakPath));
                }
            }
            else
            {
                DirectoryUtils.Copy(ConfigPath, bakPath);
                log.Info(new BaseGeneralLogMessage("HyEye", "AutoSaveConfig", "配置自动保存", "成功", "保存地址：" + bakPath));
            }
        }

        private void setDisable()
        {
            if (Global.LicType != "SW")
                return;

            btnOnLine.Enabled = false;
            btnDisplaySetting.Enabled = false;
            btnMaterial.Enabled = false;
            btnHelp.Enabled = false;
            btnAlarm.Enabled = false;

            tsmiUser.Enabled = false;
            tsmiSettings.Enabled = false;
            tsmiState.Enabled = false;
            tsmiTaskSetting.Enabled = false;
            tsmiOpticalSetting.Enabled = false;
            tsmiCalibration.Enabled = false;
            tsmiRunTasks.Enabled = false;
        }

        void loginEnabled(bool enabled)
        {
            tsmiLogin.Enabled = !enabled;
            tsmiUserManager.Enabled = enabled;
            tsmiChangePassword.Enabled = enabled;
            tsmiExit.Enabled = enabled;

            tsmiSettings.Enabled = enabled;
            tsmiState.Enabled = enabled;
            tsmiTaskSetting.Enabled = enabled;
            tsmiOpticalSetting.Enabled = enabled;
            tsmiCalibration.Enabled = enabled;
            tsmiRunTasks.Enabled = enabled;
        }

        #region 加载

        FrmDisplay frmDisplay;
        FormLog frmLog;

        void ShowFrmDisplay()
        {
            if (frmDisplay == null)
                frmDisplay = AutoFacContainer.Resolve<FrmDisplay>();

            frmDisplay.Show(dockPanelMain, DockState.Document);
            //add by LuoDian @ 20210719 测试添加一个显示图像的TAB页面
            //if (taskRepo != null && taskRepo.GetTasks() != null && taskRepo.GetTasks().Count > 0)
            //{
            //    string strTaskName = taskRepo.GetTasks()[0].Name;
            //    FrmOutputDisplay frmDisplay2 = AutoFacContainer.Resolve<FrmOutputDisplay>(new NamedParameter("rowCount", 1),
            //        new NamedParameter("colCount", 1), new NamedParameter("showTaskName", strTaskName));
            //    frmDisplay2.Text = $"{strTaskName}";
            //    frmDisplay2.Show(dockPanelMain, DockState.Document);
            //}
        }

        void ShowLog()
        {
            if (frmLog == null)
                frmLog = AutoFacContainer.Resolve<FormLog>();

            frmLog.Show(dockPanelMain, DockState.DockBottom);
        }

        void LoadRunTaskMenu(List<TaskInfoVO> taskInfos)
        {
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                ToolStripMenuItem tsmiRunTask = new ToolStripMenuItem { Name = taskInfo.Name, Text = taskInfo.Name };
                tsmiRunTask.Click += TsmiRunTask_Click;

                tsmiRunTasks.DropDownItems.Add(tsmiRunTask);

                if (taskInfo.CameraAcquireImage == null) continue;

                foreach (AcquireImageInfoVO acqImage in taskInfo.CameraAcquireImage.AcquireImages)
                {
                    ToolStripMenuItem tsmiRunAcqImage = new ToolStripMenuItem { Name = acqImage.Name, Text = acqImage.Name };
                    tsmiRunAcqImage.Click += TsmiRunAcqImage_Click;

                    tsmiRunTask.DropDownItems.Add(tsmiRunAcqImage);
                }
            }
        }

        void LoadOpticalSettingMenu(List<TaskInfoVO> taskInfos)
        {
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                ToolStripMenuItem tsmiTask = new ToolStripMenuItem { Name = taskInfo.Name, Text = taskInfo.Name };
                tsmiOpticalSetting.DropDownItems.Add(tsmiTask);

                if (taskInfo.CameraAcquireImage == null) continue;

                foreach (AcquireImageInfoVO acqImage in taskInfo.CameraAcquireImage.AcquireImages)
                {
                    ToolStripMenuItem tsmiAcqImage = new ToolStripMenuItem { Name = acqImage.Name, Text = acqImage.Name };
                    tsmiAcqImage.Click += TsmiAcqImageOpticalSetting_Click;
                    tsmiTask.DropDownItems.Add(tsmiAcqImage);

                    if (acqImage.CheckerboardName != null)
                    {
                        ToolStripMenuItem tsmiCheckerboard = new ToolStripMenuItem { Name = acqImage.CheckerboardName, Text = acqImage.CheckerboardName };
                        tsmiCheckerboard.Click += TsmiCalibOpticalSetting_Click;
                        tsmiAcqImage.DropDownItems.Add(tsmiCheckerboard);
                    }

                    if (acqImage != null && acqImage.HandEyeNames.Count > 0)
                    {
                        foreach (string handeyeName in acqImage.HandEyeNames)
                        {
                            ToolStripMenuItem tsmiHandEye = new ToolStripMenuItem { Name = handeyeName, Text = handeyeName };
                            tsmiHandEye.Click += TsmiCalibOpticalSetting_Click;
                            tsmiAcqImage.DropDownItems.Add(tsmiHandEye);
                        }
                    }
                }
            }
        }

        void LoadCalibMenu(List<TaskInfoVO> taskInfos)
        {
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                bool taskMenuVisible = false;

                ToolStripMenuItem tsmiTask = new ToolStripMenuItem { Name = taskInfo.Name, Text = taskInfo.Name };
                tsmiCalibration.DropDownItems.Add(tsmiTask);

                if (taskInfo.CameraAcquireImage != null)
                {
                    foreach (AcquireImageInfoVO acqImage in taskInfo.CameraAcquireImage.AcquireImages)
                    {
                        bool acqImageMenuVisible = acqImage.CheckerboardName != null || (acqImage.HandEyeNames != null && acqImage.HandEyeNames.Count > 0);
                        if (acqImageMenuVisible) taskMenuVisible = true;

                        ToolStripMenuItem tsmiAcqImage = new ToolStripMenuItem { Name = acqImage.Name, Text = acqImage.Name };
                        tsmiTask.DropDownItems.Add(tsmiAcqImage);

                        if (acqImage.CheckerboardName != null)
                        {
                            ToolStripMenuItem tsmiCheckerboard = new ToolStripMenuItem
                            {
                                Name = acqImage.CheckerboardName,
                                Text = acqImage.CheckerboardName,
                                Tag = CalibrationType.Checkerboard
                            };
                            tsmiCheckerboard.Click += TsmiCheckerboard_Click;
                            tsmiAcqImage.DropDownItems.Add(tsmiCheckerboard);
                        }

                        if (acqImage != null && acqImage.HandEyeNames.Count > 0)
                        {
                            foreach (string handeyeName in acqImage.HandEyeNames)
                            {
                                ToolStripMenuItem tsmiHandEye = new ToolStripMenuItem
                                {
                                    Name = handeyeName,
                                    Text = handeyeName,
                                    Tag = CalibrationType.HandEye
                                };
                                tsmiHandEye.Click += TsmiHandeye_Click;
                                tsmiAcqImage.DropDownItems.Add(tsmiHandEye);
                            }
                        }

                        if (hidden)
                            tsmiAcqImage.Visible = acqImageMenuVisible;
                    }
                }

                if (hidden)
                    tsmiTask.Visible = taskMenuVisible;
            }
        }

        #endregion

        #region Task 事件

        private void TaskRepo_TaskAdd(TaskInfoVO taskInfo)
        {
            // 运行任务
            ToolStripMenuItem tsmiRunTask = new ToolStripMenuItem { Name = taskInfo.Name, Text = taskInfo.Name };
            tsmiRunTask.Click += TsmiRunTask_Click;
            tsmiRunTasks.DropDownItems.Add(tsmiRunTask);

            // 光学设置
            ToolStripMenuItem tsmiTaskOptical = new ToolStripMenuItem { Name = taskInfo.Name, Text = taskInfo.Name };
            tsmiOpticalSetting.DropDownItems.Add(tsmiTaskOptical);

            // 标定
            ToolStripMenuItem tsmiTaskCalib = new ToolStripMenuItem { Name = taskInfo.Name, Text = taskInfo.Name };
            tsmiCalibration.DropDownItems.Add(tsmiTaskCalib);

            if (hidden)
                tsmiTaskCalib.Visible = false;
        }

        private void TaskRepo_TaskDelete(string name)
        {
            tsmiRunTasks.DropDownItems.RemoveByKey(name);

            tsmiOpticalSetting.DropDownItems.RemoveByKey(name);

            tsmiCalibration.DropDownItems.RemoveByKey(name);
        }

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            ToolStripItem tsmiRunTask = tsmiRunTasks.DropDownItems[oldName];
            tsmiRunTask.Name = newName;
            tsmiRunTask.Text = newName;

            ToolStripItem tsmiTaskOptical = tsmiOpticalSetting.DropDownItems[oldName];
            tsmiTaskOptical.Name = newName;
            tsmiTaskOptical.Text = newName;

            ToolStripItem tsmiTaskCalib = tsmiCalibration.DropDownItems[oldName];
            tsmiTaskCalib.Name = newName;
            tsmiTaskCalib.Text = newName;
        }

        private void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            // 运行任务
            ToolStripMenuItem tsmiRunAcquireImage = new ToolStripMenuItem { Name = acqImageName, Text = acqImageName };
            tsmiRunAcquireImage.Click += TsmiRunAcqImage_Click;
            ToolStripMenuItem tsmiRunTask = (ToolStripMenuItem)tsmiRunTasks.DropDownItems[taskName];
            tsmiRunTask.DropDownItems.Add(tsmiRunAcquireImage);

            // 光学设置
            ToolStripMenuItem tsmiAcqImageOptical = new ToolStripMenuItem { Name = acqImageName, Text = acqImageName };
            tsmiAcqImageOptical.Click += TsmiAcqImageOpticalSetting_Click;
            ToolStripMenuItem tsmiTaskOptical = (ToolStripMenuItem)tsmiOpticalSetting.DropDownItems[taskName];
            tsmiTaskOptical.DropDownItems.Add(tsmiAcqImageOptical);

            // 标定
            ToolStripMenuItem tsmiAcqImageCalib = new ToolStripMenuItem { Name = acqImageName, Text = acqImageName };
            ToolStripMenuItem tsmiTaskCalib = (ToolStripMenuItem)tsmiCalibration.DropDownItems[taskName];
            tsmiTaskCalib.DropDownItems.Add(tsmiAcqImageCalib);
            if (hidden)
                tsmiAcqImageCalib.Visible = false;
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            ((ToolStripMenuItem)tsmiRunTasks.DropDownItems[taskName]).DropDownItems.RemoveByKey(acqImageName);

            ((ToolStripMenuItem)tsmiOpticalSetting.DropDownItems[taskName]).DropDownItems.RemoveByKey(acqImageName);

            ((ToolStripMenuItem)tsmiCalibration.DropDownItems[taskName]).DropDownItems.RemoveByKey(acqImageName);
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            ToolStripItem tsmiRunAcqImage = ((ToolStripMenuItem)tsmiRunTasks.DropDownItems[taskName]).DropDownItems[oldAcqImageName];
            tsmiRunAcqImage.Name = newAcqImageName;
            tsmiRunAcqImage.Text = newAcqImageName;

            ToolStripItem tsmiAcqImageOptical = ((ToolStripMenuItem)tsmiOpticalSetting.DropDownItems[taskName]).DropDownItems[oldAcqImageName];
            tsmiAcqImageOptical.Name = newAcqImageName;
            tsmiAcqImageOptical.Text = newAcqImageName;

            ToolStripItem tsmiAcqImageCalib = ((ToolStripMenuItem)tsmiCalibration.DropDownItems[taskName]).DropDownItems[oldAcqImageName];
            tsmiAcqImageCalib.Name = newAcqImageName;
            tsmiAcqImageCalib.Text = newAcqImageName;
        }

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            if (calibType == CalibrationType.Joint) return;

            // 光学设置
            ToolStripMenuItem tsmiCalibOptical = new ToolStripMenuItem { Name = calibName, Text = calibName };
            tsmiCalibOptical.Click += TsmiCalibOpticalSetting_Click;
            ToolStripMenuItem tsmiAcqImageOptical = (ToolStripMenuItem)((ToolStripMenuItem)tsmiOpticalSetting.DropDownItems[taskName]).DropDownItems[acqImageName];
            tsmiAcqImageOptical.DropDownItems.Add(tsmiCalibOptical);

            // 标定
            ToolStripMenuItem tsmiTaskCalib = (ToolStripMenuItem)tsmiCalibration.DropDownItems[taskName];
            ToolStripMenuItem tsmiAcqImgCalib = (ToolStripMenuItem)tsmiTaskCalib.DropDownItems[acqImageName];
            ToolStripMenuItem tsmiCalib = new ToolStripMenuItem
            {
                Name = calibName,
                Text = calibName,
                Tag = calibType
            };
            if (calibType == CalibrationType.Checkerboard)
                tsmiCalib.Click += TsmiCheckerboard_Click;
            else if (calibType == CalibrationType.HandEye)
                tsmiCalib.Click += TsmiHandeye_Click;
            else if (calibType == CalibrationType.HandEyeSingle)
                tsmiCalib.Click += TsmiHandeyeSingle_Click;

            tsmiAcqImgCalib.DropDownItems.Add(tsmiCalib);

            tsmiTaskCalib.Visible = true;
            tsmiAcqImgCalib.Visible = true;
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            // 光学设置
            ((ToolStripMenuItem)((ToolStripMenuItem)tsmiOpticalSetting.DropDownItems[taskName]).DropDownItems[acqImageName]).DropDownItems.RemoveByKey(calibName);

            // 标定
            ToolStripMenuItem tsmiTaskCalib = (ToolStripMenuItem)tsmiCalibration.DropDownItems[taskName];
            ToolStripMenuItem tsmiAcqImgCalib = (ToolStripMenuItem)tsmiTaskCalib.DropDownItems[acqImageName];
            tsmiAcqImgCalib.DropDownItems.RemoveByKey(calibName);

            if (hidden && tsmiAcqImgCalib.DropDownItems.Count == 0)
            {
                tsmiAcqImgCalib.Visible = false;

                bool v = false;
                foreach (ToolStripMenuItem item in tsmiTaskCalib.DropDownItems)
                {
                    if (item.Visible)
                    {
                        v = true;
                        break;
                    }
                }
                tsmiTaskCalib.Visible = v;
            }
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            // 光学设置
            ToolStripMenuItem tsmiTaskOptical = (ToolStripMenuItem)tsmiOpticalSetting.DropDownItems[taskName];

            ToolStripMenuItem tsmiCalibOptical = null;
            foreach (ToolStripMenuItem tsmiAcqImageOptical in tsmiTaskOptical.DropDownItems)
            {
                if (tsmiAcqImageOptical.DropDownItems.ContainsKey(oldCalibName))
                {
                    tsmiCalibOptical = (ToolStripMenuItem)tsmiAcqImageOptical.DropDownItems[oldCalibName];
                    break;
                }
            }

            if (tsmiCalibOptical != null)
            {
                tsmiCalibOptical.Name = newCalibName;
                tsmiCalibOptical.Text = newCalibName;
            }

            // 标定
            ToolStripMenuItem tsmiTaskCalib = (ToolStripMenuItem)tsmiCalibration.DropDownItems[taskName];

            ToolStripMenuItem tsmiCalib = null;
            foreach (ToolStripMenuItem tsmiAcqImageCalib in tsmiTaskCalib.DropDownItems)
            {
                if (tsmiAcqImageCalib.DropDownItems.ContainsKey(oldCalibName))
                {
                    tsmiCalib = (ToolStripMenuItem)tsmiAcqImageCalib.DropDownItems[oldCalibName];
                    break;
                }
            }

            if (tsmiCalib != null)
            {
                tsmiCalib.Name = newCalibName;
                tsmiCalib.Text = newCalibName;
            }
        }

        #endregion

        #region Simulation 事件

        private void SimulationRepo_EnabledChanged()
        {
            if (simulationRepo.Enabled)
            {
                toolTip1.SetToolTip(btnOnLine, "离线模式");
                btnOnLine.BackgroundImage = Properties.Resources.离线1;
            }
            else
            {
                toolTip1.SetToolTip(btnOnLine, "在线模式");
                btnOnLine.BackgroundImage = Properties.Resources.在线1;
            }
        }

        #endregion

        DockState? logDockState;

        #region 用户权限

        private void tsmiLogin_Click(object sender, EventArgs e)
        {
            if (AutoFacContainer.Resolve<FrmUserLogin>().ShowDialog() == DialogResult.OK)
            {
                if (userRepo.IsDefaultPassword())
                {
                    AutoFacContainer.Resolve<FrmChangePassword>().ShowDialog();
                }
            }
        }

        private void tsmiUserManager_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmUserManager>().ShowDialog();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmChangePassword>().ShowDialog();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("确定要退出登录吗？") == DialogResult.Yes)
            {
                userRepo.Exit();
            }
        }

        void UserRepo_AfterLogin()
        {
            tslblMT.Text = $"当前料号：[{materialRepo.CurrMaterial}]            当前用户：[{userRepo.CurrUser.UserName}]            用户角色：[{userRepo.CurrUser.Role.ToDescription()}]";
            loginEnabled(true);
        }

        void UserRepo_AfterExit()
        {
            loginEnabled(false);
        }

        #endregion

        #region 系统设置

        private void tsmiCameraSetting_Click(object sender, EventArgs e)
        {
            FrmCameraSetting form = (FrmCameraSetting)FindSubForm("相机设置");
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmCameraSetting>();
            }
            form.Show(dockPanelMain, DockState.Document);
        }

        private void tsmiLightSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmLightControllerSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        private void tsmiCommSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmCommunicationSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        private void tsmiCommandSetting_Click(object sender, EventArgs e)
        {
            CommunicationInfoVO commInfo = commRepo.GetCommunicationInfo();
            if (commInfo == null)
            {
                MessageBox.Show("请先设置通讯");
                return;
            }
            if (commInfo.CommProtocol == GL.Kit.Net.CommProtocol.TCP)
                AutoFacContainer.Resolve<FrmCommandSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
            else
                AutoFacContainer.Resolve<Settings.PLCRegSetting.FrmPLCRegAgg>(new NamedParameter("readOnly", taskService.Running), new NamedParameter("closeSave", true)).ShowDialog();
        }

        private void tsmiImageSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmImageManager>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        private void tsmiSystemSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmSystemSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        private void tsmiVisionParamsSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<Vision.FrmSetParams>(new NamedParameter("readOnly", taskService.Running), new NamedParameter("closeSave", true)).ShowDialog();
        }

        private void tsmiCameraSDKLog_Click(object sender, EventArgs e)
        {
            //运行中也可以取SDK日志
            AutoFacContainer.Resolve<FrmCameraSDKLog>().ShowDialog();
        }

        #endregion

        #region 状态查看

        private void tsmiCameraState_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmCameraState>().ShowDialog();
        }

        private void tsmiLog_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FormLog>().Show(dockPanelMain, DockState.DockBottom);
        }

        #endregion

        #region 任务配置

        FormSettings formSettings;

        private void tsmiTaskSetting_Click(object sender, EventArgs e)
        {
            if (frmLog != null)
            {
                frmLog.Parent = null;

                logDockState = frmLog.DockState;
            }

            if (formSettings == null)
                formSettings = AutoFacContainer.Resolve<FormSettings>();

            formSettings.ShowDialog();

            if (frmLog != null)
                frmLog.Show(dockPanelMain, logDockState.Value);
        }

        #endregion

        #region 光学设置

        private void TsmiAcqImageOpticalSetting_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiAcqImageOpticalSetting = (ToolStripMenuItem)sender;

            string acqImageName = tsmiAcqImageOpticalSetting.Text;
            string taskName = tsmiAcqImageOpticalSetting.OwnerItem.Text;

            string text = Utils.GetOpticalFormName(taskName, acqImageName);

            FrmOpticalSetting form = (FrmOpticalSetting)FindSubForm(text);
            if (form == null)
            {
                FormSettings formSettings = AutoFacContainer.Resolve<FormSettings>();
                form = (FrmOpticalSetting)formSettings.FindSubForm(text);
            }
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                form.Text = text;
                form.LoadOptics(taskName, acqImageName, null);
            }

            form.Show(dockPanelMain, DockState.Document);
        }

        private void TsmiCalibOpticalSetting_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiAcqImageOpticalSetting = (ToolStripMenuItem)sender;

            string calibName = tsmiAcqImageOpticalSetting.Text;
            string taskName = tsmiAcqImageOpticalSetting.OwnerItem.OwnerItem.Text;

            OpenCalibrationOptical(taskName, calibName);
        }

        internal void OpenCalibrationOptical(string taskName, string calibName)
        {
            string text = Utils.GetOpticalFormName(taskName, calibName);

            FrmOpticalSetting form = (FrmOpticalSetting)FindSubForm(text);
            if (form == null)
            {
                FormSettings formSettings = AutoFacContainer.Resolve<FormSettings>();
                form = (FrmOpticalSetting)formSettings.FindSubForm(text);
            }
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                form.Text = text;
                form.LoadOptics(taskName, null, calibName);
            }

            form.Show(dockPanelMain, DockState.Document);
        }

        #endregion

        #region 标定

        FormCalibrations formCalibrations;

        private void tsmiCalibration_Click(object sender, EventArgs e)
        {
            //if (frmLog != null)
            //{
            //    frmLog.Parent = null;

            //    logDockState = frmLog.DockState;
            //}

            //if (formCalibrations == null)
            //    formCalibrations = AutoFacContainer.Resolve<FormCalibrations>();

            //formCalibrations.ShowDialog();

            //if (frmLog != null)
            //    frmLog.Show(dockPanelMain, logDockState.Value);
        }

        private void TsmiCheckerboard_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiCalib = (ToolStripMenuItem)sender;

            string taskName = tsmiCalib.OwnerItem.OwnerItem.Text;
            string acqName = tsmiCalib.OwnerItem.Text;
            string calibName = tsmiCalib.Text;
            CalibrationType calibType = (CalibrationType)tsmiCalib.Tag;

            string text = Utils.GetCalibrationFormName(taskName, calibName, calibType);

            FrmCheckerboardSetting form = (FrmCheckerboardSetting)FindSubForm(text);
            if (form == null)
            {
                FormSettings formSettings = AutoFacContainer.Resolve<FormSettings>();
                form = (FrmCheckerboardSetting)formSettings.FindSubForm(text);
            }
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmCheckerboardSetting>();
                form.Text = text;
                form.Init(taskName, calibName);
            }
            form.OpenOptical = OpenCalibrationOptical;

            form.Show(dockPanelMain, DockState.Document);
        }

        private void TsmiHandeye_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiCalib = (ToolStripMenuItem)sender;

            string taskName = tsmiCalib.OwnerItem.OwnerItem.Text;
            string acqName = tsmiCalib.OwnerItem.Text;
            string calibName = tsmiCalib.Text;
            CalibrationType calibType = (CalibrationType)tsmiCalib.Tag;

            string text = Utils.GetCalibrationFormName(taskName, calibName, calibType);

            FrmHandEyeSetting form = (FrmHandEyeSetting)FindSubForm(text);
            if (form == null)
            {
                FormSettings formSettings = AutoFacContainer.Resolve<FormSettings>();
                form = (FrmHandEyeSetting)formSettings.FindSubForm(text);
            }
            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmHandEyeSetting>();
                form.Text = text;
                form.Init(taskName, acqName, calibName);
            }
            form.OpenOptical = OpenCalibrationOptical;

            form.Show(dockPanelMain, DockState.Document);
        }

        private void TsmiHandeyeSingle_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 运行任务

        private void TsmiRunTask_Click(object sender, EventArgs e)
        {
            // 运行任务的时候，如果图像保存服务未启动，则会报错
            if (!imageService.Running)
                imageService.Start();

            string taskName = ((ToolStripMenuItem)sender).Text;

            taskService.RunTask(taskName);
        }

        private void TsmiRunAcqImage_Click(object sender, EventArgs e)
        {
            // 运行任务的时候，如果图像保存服务未启动，则会报错
            if (!imageService.Running)
                imageService.Start();

            ToolStripMenuItem tsmiRunAcqImage = (ToolStripMenuItem)sender;

            string acqImageName = tsmiRunAcqImage.Text;
            string taskName = tsmiRunAcqImage.OwnerItem.Text;

            taskService.RunAcqImage(taskName, acqImageName);
        }

        #endregion

        #region 服务启停

        private void CommService_ConnectedChanged()
        {
            statusStrip1.AsyncAction((s) =>
            {
                if (commService.Connected)
                {
                    tslblConnectionState.Text = " 已连接";
                    tslblConnectionState.ForeColor = Color.Green;
                    tslblConnectionState.Image = Properties.Resources.信号_已连接;
                }
                else
                {
                    tslblConnectionState.Text = " 未连接";
                    tslblConnectionState.ForeColor = Color.Red;
                    tslblConnectionState.Image = Properties.Resources.信号_未连接;
                }
            });
        }

        private void taskService_StateChanged()
        {
            if (taskService.TaskRunnerStateRun)
            {
                tslblPSR.Text = "正常运行状态";
                tslblPSR.ForeColor = Color.Green;
            }
            else
            {
                tslblPSR.Text = "上位机暂停状态";
                tslblPSR.ForeColor = Color.Red;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            taskService.Close();

            imageService.Stop();

            //commService.Stop();

            SetEnabled(true);
        }

        void Start()
        {
            if (!commService.Check()) return;

            if (!taskService.Check()) return;

            if (!toolBlockComponents.Check()) return;

            try
            {
                commService.Start();
                commService.Init();

                taskService.Start();

                imageService.Start();

                //RunTestLog();

                btnStop.Enabled = true;
                SetEnabled(false);
            }
            catch (ServiceException vex)
            {
                commService.Stop();

                taskService.Close();
                
                //update by LuoDian @ 20220117 为了人性化考虑，把弹窗改为log输出
                //MessageBoxUtils.ShowError(vex.Message);
                log?.Error(vex.Message);
            }
            catch (Exception ex)
            {
                commService.Stop();

                taskService.Close();

                //update by LuoDian @ 20220117 为了人性化考虑，把抛出异常改为log输出
                //throw;
                log?.Error(ex.Message);
            }
        }

        void SetEnabled(bool enabled)
        {
            btnStart.Enabled = enabled;
            btnStop.Enabled = !enabled;
            btnOnLine.Enabled = enabled;

            if (userRepo.CurrUser != null)
            {
                tsmiTaskSetting.Enabled = enabled;
                tsmiOpticalSetting.Enabled = enabled;
                tsmiCalibration.Enabled = enabled;
                tsmiRunTasks.Enabled = enabled;

                tsmiCameraSetting.Enabled = enabled;
            }

            if (enabled)
            {
                btnStart.BackgroundImage = Properties.Resources.开始2;
                btnStop.BackgroundImage = Properties.Resources.停止2;
            }
            else
            {
                btnStart.BackgroundImage = Properties.Resources.开始3;
                btnStop.BackgroundImage = Properties.Resources.停止1;
            }
        }

        #endregion

        #region 在线/离线

        private void btnOnLine_Click(object sender, EventArgs e)
        {
            simulationRepo.Enabled = !simulationRepo.Enabled;
        }

        #endregion

        #region 显示设置

        FrmDisplayLayoutSetting frmDisplayLayoutSetting;

        private void btnDisplaySetting_Click(object sender, EventArgs e)
        {
            if (frmDisplayLayoutSetting == null || frmDisplayLayoutSetting.IsDisposed)
                frmDisplayLayoutSetting = AutoFacContainer.Resolve<FrmDisplayLayoutSetting>();

            frmDisplayLayoutSetting.Show();
            frmDisplayLayoutSetting.BringToFront();
            if (frmDisplayLayoutSetting.WindowState == FormWindowState.Minimized)
                frmDisplayLayoutSetting.WindowState = FormWindowState.Normal;
        }

        #endregion

        #region 料号管理

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            FrmMaterial frm = AutoFacContainer.Resolve<FrmMaterial>();
            if (frm.ShowDialog() == DialogResult.Retry)
            {

            }
        }

        #endregion

        #region 告警

        bool alarm = false;

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            if (alarm)
            {
                alarm = false;
                btnAlarm.BackgroundImage = Properties.Resources.警报;
            }
            else
            {
                alarm = true;
                btnAlarm.BackgroundImage = Properties.Resources.警报1;
            }
        }

        #endregion

        #region 帮助

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string path = PathUtils.CurrentDirectory + "用户手册.pdf";

            if (File.Exists(path))
            {
                Process p = new Process
                {
                    StartInfo = new ProcessStartInfo(path)
                };
                p.Start();
            }
            else
            {
                MessageBoxUtils.ShowError("用户手册缺失");
            }
        }

        #endregion

        public Form FindSubForm(string text)
        {
            return MdiChildren.FirstOrDefault(f => f.Text == text);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (materialRepo.ForcedExit) return;

            if (MessageBoxUtils.ShowQuestion("确定要退出程序吗？") == DialogResult.No)
                e.Cancel = true;
            else
                //任务开启状态的时候，关闭主程序。会导致相机没有执行Close。（迈德威视相机需Close，否则再打开会报已占用）
                taskService?.Close();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.GetLogger("HyEye").Info(new BaseGeneralLogMessage("HyInspect", "", "软件关闭", "成功"));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslblTime.Text = DateTime.Now.ToString(" yyyy-MM-dd HH:mm:ss");
        }

    }
}
