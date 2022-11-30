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
using WeifenLuo.WinFormsUI.Docking;
using IImageService = HyEye.Services.IImageService;

namespace HyEye.WForm.Calibration
{
    public partial class FrmHandEyeSetting : DockContentEx
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
        HandeyeRunner runner;

        public FrmHandEyeSetting(
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

            runner = calibService.CreateHandeyeRunner(calibInfo);
            runner.AfterGetPose += Runner_AfterGetPose;
            runner.ReceiveCmdS += Runner_ReceiveCmdS;

            pnlVisionControl.Controls.Add(runner.DisplayImageComponent.DisplayedControl);
            ckbEnabledCheckerboard.Checked = calibInfo.HandEyeInfo.EnabledCheckerboard;

            InitDataGridView(calibInfo.HandEyeInfo);
        }

        void InitDataGridView(HandEyeInfoVO handEyeInfo)
        {
            dgvData.Rows.Clear();

            for (int i = 0, h = handEyeInfo.TotalCount; i < h; i++)
            {
                dgvData.Rows.Add(i, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }

        #region Runner 事件

        private void Runner_AfterGetPose(string taskName, string calibName, int index, double x1, double y1, double a1, double? x2, double? y2, double? a2, object graphic)
        {
            curIndex = index;

            dgvData.AsyncAction((c) =>
            {
                dgvData.Rows[index - 1].SetValues(index - 1, x1, y1, a1, x2, y2, a2);
            });
        }

        private void Runner_ReceiveCmdS()
        {
            dgvData.AsyncAction(c =>
            {
                CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
                InitDataGridView(calibInfo.HandEyeInfo);
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

        private void btnSetParam_Click(object sender, EventArgs e)
        {
            FrmHandEyeParamSetting frm = new FrmHandEyeParamSetting(calibName, calibRepo);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);

                int total = calibInfo.HandEyeInfo.TotalCount;

                if (total > dgvData.Rows.Count)
                {
                    for (int i = dgvData.Rows.Count; i < total; i++)
                    {
                        dgvData.Rows.Add(i, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    }
                }
                else if (total < dgvData.Rows.Count)
                {
                    for (int i = dgvData.Rows.Count - 1; i == total; i--)
                    {
                        dgvData.Rows.RemoveAt(i);
                    }
                }
            }
        }

        // 模板设置
        private void btnSetPattern_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmHandEyePattern>(
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

        int curIndex = 0;
        int total;

        // 获取模板坐标
        private void btnGetImagePoint_Click(object sender, EventArgs e)
        {
            SetEnableManual(true);

            btnGetImagePoint.Enabled = false;

            if (curIndex == 0)
            {
                if (!imageService.Running)
                    imageService.Start();

                total = calibRepo.GetCalibration(calibName).HandEyeInfo.TotalCount;
                runner.Start();
            }

            if (runner.GetPose(++curIndex))
            {
                if (curIndex == total)
                {
                    btnGetImagePoint.Enabled = false;
                }
                else
                {
                    btnGetImagePoint.Enabled = true;
                }
            }
            else
            {
                btnGetImagePoint.Enabled = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 自动标定在标定失败时，可进行手动标定
            // closeAutoCalibration();

            curIndex = 0;
            runner.Reset();

            if (!autoCalib)
                SetEnableManual(false);
            btnGetImagePoint.Enabled = true;

            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
            InitDataGridView(calibInfo.HandEyeInfo);
        }

        // 手动标定
        private void btnManualCalibration_Click(object sender, EventArgs e)
        {
            if (curIndex < 3)
            {
                MessageBoxUtils.ShowError("坐标数据少于 3 条，无法标定");
                return;
            }

            List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> points = new List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)>();

            for (int i = 0; i < curIndex; i++)
            {
                DataGridViewRow row = dgvData.Rows[i];
                points.Add(
                    (Convert.ToDouble(row.Cells[1].Value),
                     Convert.ToDouble(row.Cells[2].Value),
                     Convert.ToDouble(row.Cells[3].Value),
                     Convert.ToDouble(row.Cells[4].Value),
                     Convert.ToDouble(row.Cells[5].Value),
                     Convert.ToDouble(row.Cells[6].Value),
                    row.ReadOnly));
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
            AutoFacContainer.Resolve<FrmHandEyeResult>(
                new NamedParameter("calibName", calibName),
                new NamedParameter("handeyeComponent", runner.HandeyeComponent)
                ).ShowDialog();
        }

        // 标定验证
        private void btnVerify_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmHandeyeVerify>(
                new NamedParameter("taskName", taskName),
                new NamedParameter("calibName", calibName),
                new NamedParameter("handeyeComponent", runner.HandeyeComponent)
                ).ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HandEyeInfoVO handeyeInfo = calibRepo.GetCalibration(calibName).HandEyeInfo;
            if (handeyeInfo.EnabledCheckerboard != ckbEnabledCheckerboard.Checked)
            {
                handeyeInfo.EnabledCheckerboard = ckbEnabledCheckerboard.Checked;
                calibRepo.SetHandEyeParams(calibName, handeyeInfo);
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
            HandEyeInfoVO handeyeInfo = calibRepo.GetCalibration(calibName).HandEyeInfo;

            int c = handeyeInfo.XPointNum * handeyeInfo.YPointNum;

            dgvData.Rows.Clear();
            int index = 0;
            foreach ((double x, double y, double a) in handeyeInfo.GetPoints())
            {
                if (index < c)
                {
                    dgvData.Rows.Add(index++, x, y, a, x, y, a);
                }
                else
                {
                    break;
                }
            }

            if (handeyeInfo.APointNum == 3)
            {
                dgvData.Rows.Add(index++, 0, -handeyeInfo.YStep, -90, 0, 0, -90);
                dgvData.Rows.Add(index++, handeyeInfo.XStep, 0, 0, 0, 0, 0);
                dgvData.Rows.Add(index++, 0, handeyeInfo.YStep, 90, 0, 0, 90);
            }
            if (handeyeInfo.APointNum == 2)
            {
                dgvData.Rows.Add(index++, handeyeInfo.XStep, 0, 0, 2, 0, 0);
                dgvData.Rows.Add(index++, 0, handeyeInfo.YStep, 90, 0, 2, 90);
            }
        }

        private void FrmHandEyeSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            pnlVisionControl.Controls.Clear();

            if (curIndex > 0)
                runner.Reset();

            taskRepo.TaskRename -= TaskRepo_TaskRename;
            taskRepo.CalibRename -= TaskRepo_CalibRename;
            taskRepo.CalibDelete -= TaskRepo_CalibDelete;
            runner.AfterGetPose -= Runner_AfterGetPose;
            runner.ReceiveCmdS -= Runner_ReceiveCmdS;

            runner.Close();
        }
    }
}
