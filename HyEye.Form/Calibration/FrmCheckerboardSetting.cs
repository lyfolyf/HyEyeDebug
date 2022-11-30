using Autofac;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using HyEye.Services;
using System;
using System.Windows.Forms;
using VisionSDK;
using WeifenLuo.WinFormsUI.Docking;
using IImageService = HyEye.Services.IImageService;

namespace HyEye.WForm.Calibration
{
    public partial class FrmCheckerboardSetting : DockContentEx
    {
        public OpenOpticalHandler OpenOptical;

        readonly ITaskRepository taskRepo;
        readonly ICalibrationRepository calibRepo;
        readonly ICommunicationService commService;
        readonly ICommandService commandService;
        readonly ICalibrationService calibService;
        readonly IImageService imageService;
        readonly IGLog log;

        string taskName;
        string calibName;
        CheckerboardRunner runner;

        public FrmCheckerboardSetting(
            ITaskRepository taskRepo,
            ICalibrationRepository calibRepo,
            IGLog log,
            ICommunicationService commService,
            ICalibrationService calibService,
            ICommandService commandService,
            IImageService imageService)
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.calibRepo = calibRepo;
            this.log = log;
            this.commService = commService;
            this.calibService = calibService;
            this.commandService = commandService;
            this.imageService = imageService;

            this.taskRepo.TaskRename += TaskRepo_TaskRename;
            this.taskRepo.CalibRename += TaskRepo_CalibRename;
            this.taskRepo.CalibDelete += TaskRepo_CalibDelete;
        }

        #region 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            if (taskName == oldName)
            {
                taskName = newName;

                Text = Utils.GetCalibrationFormName(taskName, calibName, Models.CalibrationType.Checkerboard);
            }
        }

        private void TaskRepo_CalibRename(string taskName, Models.CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            if (this.taskName == taskName && oldCalibName == calibName)
            {
                calibName = newCalibName;

                Text = Utils.GetCalibrationFormName(taskName, calibName, Models.CalibrationType.Checkerboard);
            }
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, Models.CalibrationType calibType, string calibName)
        {
            if (this.taskName == taskName && this.calibName == calibName)
            {
                Close();
            }
        }

        #endregion

        public void Init(string taskName, string calibName)
        {
            this.taskName = taskName;
            this.calibName = calibName;

            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
            runner = calibService.CreateCheckerboardRunner(calibInfo);

            pnlVisionControl.Controls.Add(runner.CheckerboardComponent.DisplayedControl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            runner.CheckerboardComponent.Save();
        }

        private void btnOpticalSetting_Click(object sender, EventArgs e)
        {
            OpenOptical?.Invoke(taskName, calibName);
        }

        private void btnAcqImage_Click(object sender, EventArgs e)
        {
            runner.AcqImage();
        }

        // 自动标定
        private void btnAutoCalibration_Click(object sender, EventArgs e)
        {
            if (!commService.Check()) return;
            if (!runner.Check()) return;

            commandService.Init();

            if (!imageService.Running)
                imageService.Start();
            if (!commService.Running)
                commService.Start();

            commService.Init();

            try
            {
                SetEnabled(false);

                runner.Start();
            }
            catch
            {
                runner.Close();

                SetEnabled(true);

                throw;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            runner.Close();

            SetEnabled(true);
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmCheckerboardVerify>(
                new NamedParameter("taskName", taskName),
                new NamedParameter("calibName", calibName),
                new TypedParameter(typeof(ICheckerboardComponent), runner.CheckerboardComponent)
                ).ShowDialog();
        }

        void SetEnabled(bool enabled)
        {
            pnlTop.AsyncAction((c) =>
            {
                btnAcqImage.Enabled = enabled;
                btnOpticalSetting.Enabled = enabled;
                btnAutoCalibration.Enabled = enabled;
                btnStop.Enabled = !enabled;
                btnSave.Enabled = enabled;
            });
        }

        private void FrmCheckerboardSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            pnlVisionControl.Controls.Clear();

            taskRepo.TaskRename -= TaskRepo_TaskRename;
            taskRepo.CalibRename -= TaskRepo_CalibRename;
            taskRepo.CalibDelete -= TaskRepo_CalibDelete;

            runner.Close();
        }
    }
}
