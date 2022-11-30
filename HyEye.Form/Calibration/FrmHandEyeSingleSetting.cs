using Autofac;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using HyEye.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;
using WeifenLuo.WinFormsUI.Docking;
using IImageService = HyEye.Services.IImageService;

namespace HyEye.WForm.Calibration
{
    public partial class FrmHandEyeSingleSetting : DockContentEx
    {
        public OpenOpticalHandler OpenOptical;

        readonly ITaskRepository taskRepo;
        readonly ICalibrationRepository calibRepo;
        readonly ICommandService commandService;
        readonly ICommunicationService commService;
        readonly ICalibrationService calibService;
        readonly IImageService imageService;
        readonly IGLog log;

        readonly CalibrationComponentSet calibComponentSet;

        string taskName;
        string acqName;
        string calibName;
        HandeyeSingleRunner runner;

        public FrmHandEyeSingleSetting(
            ITaskRepository taskRepo,
            ICalibrationRepository calibRepo,
            ICommunicationService commService,
            ICalibrationService calibService,
            ICommandService commandService,
            IImageService imageService,
            IGLog log,
            CalibrationComponentSet calibComponentSet,
            IPermission permission)
        {
            InitializeComponent();

            dgvData.Columns[0].Frozen = true;
            dgvData.SetSortMode(DataGridViewColumnSortMode.NotSortable);

            this.taskRepo = taskRepo;
            this.calibRepo = calibRepo;
            this.commService = commService;
            this.calibService = calibService;
            this.commandService = commandService;
            this.imageService = imageService;
            this.log = log;
            this.calibComponentSet = calibComponentSet;

            permission.SetPermission(this);

            this.taskRepo.TaskRename += TaskRepo_TaskRename;
            this.taskRepo.CalibRename += TaskRepo_CalibRename;
            this.taskRepo.CalibDelete += TaskRepo_CalibDelete;
        }

        #region Calibration 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            if (taskName == oldName)
            {
                taskName = newName;

                Text = Utils.GetCalibrationFormName(taskName, calibName, Models.CalibrationType.HandEye);
            }
        }

        private void TaskRepo_CalibRename(string taskName, Models.CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            if (this.taskName == taskName && calibName == oldCalibName)
            {
                calibName = newCalibName;

                Text = Utils.GetCalibrationFormName(taskName, calibName, Models.CalibrationType.HandEye);
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

        public void Init(string taskName, string acqName, string calibName)
        {
            this.taskName = taskName;
            this.acqName = acqName;
            this.calibName = calibName;

            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);

            runner = calibService.CreateHandeyeSingleRunner(calibInfo);
            runner.AfterGetPose += Runner_AfterGetPose;
            runner.ReceiveCmdS += Runner_ReceiveCmdS;

            pnlVisionControl.Controls.Add(runner.DisplayImageComponent.DisplayedControl);
            ckbEnabledCheckerboard.Checked = calibInfo.HandEyeSingleInfo.EnabledCheckerboard;

            InitDataGridView(calibInfo.HandEyeSingleInfo);
        }

        void InitDataGridView(HandEyeSingleInfoVO handEyeInfo)
        {
            dgvData.Rows.Clear();

            for (int i = 0, h = 9; i < h; i++)
            {
                dgvData.Rows.Add(i, string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }

        #region Runner 事件

        private void Runner_AfterGetPose(string taskName, string calibName, List<HandeyeSinglePoint> points, object graphic)
        {
            dgvData.AsyncAction((c) =>
            {
                for (int i = 0; i < points.Count; i++)
                {
                    dgvData.Rows[i].SetValues(i, points[i].X1, points[i].Y1, points[i].X2, points[i].Y2);
                }
            });
        }

        private void Runner_ReceiveCmdS()
        {
            dgvData.AsyncAction(c =>
            {
                CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
                InitDataGridView(calibInfo.HandEyeSingleInfo);
            });
        }

        #endregion

        #region 设置

        private void ckbEnabledCheckerboard_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEnabledCheckerboard.Checked)
            {
                TaskInfoVO taskInfo = taskRepo.GetTaskByName(taskName);
                AcquireImageInfoVO acqInfo = taskInfo.CameraAcquireImage.AcquireImages.First(a => a.Name == acqName);

                if (acqInfo.CheckerboardName == null)
                {
                    MessageBoxUtils.ShowWarn("该拍照中没有添加 Checkerboard 标定，无法启用 Checkerboard 畸变矫正");
                    ckbEnabledCheckerboard.Checked = false;
                    return;
                }

                runner.CheckerboardComponent = calibComponentSet.GetCheckerboardComponent(acqInfo.CheckerboardName);
            }
            else
            {
                runner.CheckerboardComponent = null;
            }
        }

        // 模板设置
        private void btnSetPattern_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmHandEyeSinglePattern>(
                new NamedParameter("calibName", calibName),
                new NamedParameter("runner", runner)
                ).ShowDialog();
        }

        // 光学设置
        private void btnOpticalSetting_Click(object sender, EventArgs e)
        {
            OpenOptical?.Invoke(taskName, calibName);
        }

        #endregion

        #region 自动标定

        bool autoCalib = false;

        void closeAutoCalibration()
        {
            if (autoCalib)
            {
                autoCalib = false;

                runner.Close();
            }
        }

        private void btnAutoCalibration_Click(object sender, EventArgs e)
        {
            if (!commService.Check()) return;
            if (!runner.Check()) return;

            try
            {
                SetEnabledAuto(true);

                autoCalib = true;

                commandService.Init();

                if (!imageService.Running)
                    imageService.Start();

                if (!commService.Running)
                    commService.Start();

                commService.Init();

                runner.AutoStart();

                if (!commService.Connected && commService.CommunicationInfo.ConnectionMethod == GL.Kit.Net.ConnectionMethod.Client)
                {
                    btnAutoCalibration.Enabled = true;
                }
            }
            catch
            {
                closeAutoCalibration();

                SetEnabledAuto(false);

                throw;
            }
        }

        private void btnAutoStop_Click(object sender, EventArgs e)
        {
            closeAutoCalibration();

            //calibService.HandEyeReset(calibName);
            runner.AutoStop();
            runner.Reset();

            SetEnabledAuto(false);
        }

        #endregion

        #region 手动标定

        // 获取模板坐标
        private void btnGetImagePoint_Click(object sender, EventArgs e)
        {
            SetEnableManual(true);

            btnGetImagePoint.Enabled = false;

            if (!imageService.Running)
                imageService.Start();
            runner.Start();

            runner.GetPose();

            btnGetImagePoint.Enabled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 自动标定在标定失败时，可进行手动标定
            // closeAutoCalibration();

            runner.Reset();

            if (!autoCalib)
                SetEnableManual(false);
            btnGetImagePoint.Enabled = true;

            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
            InitDataGridView(calibInfo.HandEyeSingleInfo);
        }

        // 手动标定
        private void btnManualCalibration_Click(object sender, EventArgs e)
        {
            List<HandeyeSinglePoint> points = new List<HandeyeSinglePoint>();

            for (int i = 0; i < 9; i++)
            {
                DataGridViewRow row = dgvData.Rows[i];

                HandeyeSinglePoint p = new HandeyeSinglePoint();
                p.X1 = Convert.ToDouble(row.Cells[1].Value);
                p.Y1 = Convert.ToDouble(row.Cells[2].Value);
                p.X2 = Convert.ToDouble(row.Cells[3].Value);
                p.Y2 = Convert.ToDouble(row.Cells[4].Value);
                p.Disable = row.ReadOnly;

                points.Add(p);
            }

            runner.ManualCalibrate(points);

            btnGetImagePoint.Enabled = true;
        }

        void SetEnabledAuto(bool begin)
        {
            gbSetting.Enabled = !begin;

            btnGetImagePoint.Enabled = !begin;

            btnAutoCalibration.Enabled = !begin;
            btnAutoStop.Enabled = begin;
        }

        // 手动时候
        void SetEnableManual(bool begin)
        {
            gbSetting.Enabled = !begin;

            btnAutoCalibration.Enabled = !begin;
            btnAutoStop.Enabled = false;
        }

        #endregion

        // 查看结果
        private void btnShowResult_Click(object sender, EventArgs e)
        {
            //AutoFacContainer.Resolve<FrmHandEyeResult>(
            //    new NamedParameter("calibName", calibName),
            //    new NamedParameter("handeyeComponent", runner.HandeyeSingleComponent)
            //    ).ShowDialog();
        }

        // 标定验证
        private void btnVerify_Click(object sender, EventArgs e)
        {
            //AutoFacContainer.Resolve<FrmHandeyeVerify>(
            //    new NamedParameter("taskName", taskName),
            //    new NamedParameter("calibName", calibName),
            //    new NamedParameter("handeyeComponent", runner.HandeyeSingleComponent)
            //    ).ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HandEyeSingleInfoVO handeyeSingleInfo = calibRepo.GetCalibration(calibName).HandEyeSingleInfo;
            if (handeyeSingleInfo.EnabledCheckerboard != ckbEnabledCheckerboard.Checked)
            {
                handeyeSingleInfo.EnabledCheckerboard = ckbEnabledCheckerboard.Checked;
                calibRepo.SetHandEyePatternMode(calibName, ckbEnabledCheckerboard.Checked);
            }
            calibRepo.Save();
        }

        DataGridViewCellStyle DisableRowsStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(255, 128, 128)
        };

        DataGridViewCellStyle EnableRowsStyle = new DataGridViewCellStyle
        {
            BackColor = SystemColors.Window
        };

        private void tsmiDisable_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentCell == null) return;

            dgvData.CurrentRow.ReadOnly = true;
            dgvData.CurrentRow.DefaultCellStyle = DisableRowsStyle;
        }

        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentCell == null) return;

            dgvData.CurrentRow.ReadOnly = false;
            dgvData.CurrentRow.DefaultCellStyle = EnableRowsStyle;
        }

        private void tsmiAutoSetValue_Click(object sender, EventArgs e)
        {
            dgvData.Rows[0].SetValues(0, -2, -2, -2, -2);
            dgvData.Rows[1].SetValues(1, 0, -2, 0, -2);
            dgvData.Rows[2].SetValues(2, 2, -2, 2, -2);

            dgvData.Rows[3].SetValues(3, 2, 0, 2, 0);
            dgvData.Rows[4].SetValues(4, 0, 0, 0, 0);
            dgvData.Rows[5].SetValues(5, -2, 0, -2, 0);

            dgvData.Rows[6].SetValues(6, -2, 2, -2, 2);
            dgvData.Rows[7].SetValues(7, 0, 2, 0, 2);
            dgvData.Rows[8].SetValues(8, 2, 2, 2, 2);
        }

        private void FrmHandEyeSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            pnlVisionControl.Controls.Clear();

            taskRepo.TaskRename -= TaskRepo_TaskRename;
            taskRepo.CalibRename -= TaskRepo_CalibRename;
            taskRepo.CalibDelete -= TaskRepo_CalibDelete;
            runner.AfterGetPose -= Runner_AfterGetPose;
            runner.ReceiveCmdS -= Runner_ReceiveCmdS;

            runner.Close();
        }
    }
}
