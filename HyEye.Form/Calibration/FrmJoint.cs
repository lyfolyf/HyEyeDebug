using Autofac;
using HyEye.API.Repository;
using HyEye.Models.VO;
using HyEye.Services;
using HyEye.WForm.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm.Calibration
{
    public partial class FrmJoint : DockContentEx
    {
        readonly ITaskRepository taskRepo;
        readonly ICalibrationRepository calibrationRepo;
        readonly IUserRepository userRepo;
        readonly ICalibrationService calibService;
        readonly JointDisplayComponentSet jointDisplayComponents;
        readonly ToolBlockComponentSet toolBlockComponents;

        CalibrationInfoVO calib;
        JointRunner runner;

        FormSettings formSettings;

        List<CC> slaveControls = new List<CC>();

        public FrmJoint(ITaskRepository taskRepo,
            ICalibrationRepository calibrationRepo,
            IUserRepository userRepo,
            ICalibrationService calibService,
            JointDisplayComponentSet jointDisplayComponents,
            ToolBlockComponentSet toolBlockComponents)
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.calibrationRepo = calibrationRepo;
            this.userRepo = userRepo;
            this.calibService = calibService;
            this.jointDisplayComponents = jointDisplayComponents;
            this.toolBlockComponents = toolBlockComponents;

            formSettings = AutoFacContainer.Resolve<FormSettings>();

            taskRepo.CalibQuote += TaskRepo_CalibQuote;
            taskRepo.CancelCalibQuote += TaskRepo_CancelCalibQuote;
        }

        private void TaskRepo_CalibQuote(string taskName, string acqImageName, Models.CalibrationType calibType, string calibName)
        {
            if (calibName != calib.Name) return;

            calib = calibrationRepo.GetCalibration(calibName);

            TabPage p = new TabPage($"{taskName}-{acqImageName}");
            tabControl1.TabPages.Add(p);

            runner = calibService.CreateJointRunner(calib);

            JointControl c = runner.JointComponent.Controls.FirstOrDefault(a => a.TaskName == taskName && a.AcqName == acqImageName);

            IToolBlockComponent taskToolBlock = toolBlockComponents.GetComponent(taskName);

            slaveControls.Add(new CC(calib, p, c, runner, taskToolBlock, formSettings, userRepo));
        }

        private void TaskRepo_CancelCalibQuote(string taskName, string acqImageName, Models.CalibrationType calibType, string calibName)
        {
            if (calibName != calib.Name) return;

            TabPage p = tabControl1.TabPages.FirstOrDefault(a=>a.Text == $"{taskName}-{acqImageName}");
            if (p != null)
            {
                tabControl1.TabPages.Remove(p);
                p.Dispose();
            }

            calib = calibrationRepo.GetCalibration(calibName);
        }

        public bool Init(string calibName)
        {
            calib = calibrationRepo.GetCalibration(calibName);

            if (taskRepo.GetTaskByName(calib.JointInfo.Master.TaskName)
                    .CameraAcquireImage.AcquireImages.First(a => a.JointName == calib.Name).HandEyeNames.Count == 0)
            {
                MessageBoxUtils.ShowWarn("该拍照中缺少 HandEye 标定，请先添加一个 HandEye 标定");
                return false;
            }

            nudPointCount.Value = calib.JointInfo.PointCount;

            runner = calibService.CreateJointRunner(calib);

            List<JointControl> controls = runner.JointComponent.Controls;

            splitContainer1.Panel1.Controls.Add(controls[0].DisplayedControl);
            tabControl1.TabPages[1].Text = $"{controls[0].TaskName}-{controls[0].AcqName}";

            foreach (JointControl c in controls.Skip(1))
            {
                TabPage p = new TabPage($"{c.TaskName}-{c.AcqName}");
                tabControl1.TabPages.Add(p);

                IToolBlockComponent taskToolBlock = toolBlockComponents.GetComponent(c.TaskName);
                slaveControls.Add(new CC(calib, p, c, runner, taskToolBlock, formSettings, userRepo));
            }

            ShowDisplay();

            return true;
        }

        void ShowDisplay()
        {
            (int rowCount, int colCount) = GetRowCol(calib.JointInfo.Slaves.Count + 1);
            tableLayoutPanel1.RowCount = rowCount;
            tableLayoutPanel1.ColumnCount = colCount;

            int r = 0;
            int c = 0;

            GroupBox gbMaster = new GroupBox()
            {
                Text = $"{calib.JointInfo.Master.TaskName}-{calib.JointInfo.Master.AcqImageName}",
                Dock = DockStyle.Fill
            };
            tableLayoutPanel1.Controls.Add(gbMaster, c, r);

            IDisplayTaskImageComponent displayComponent = jointDisplayComponents.GetJointDisplayControl(calib.JointInfo.Master.TaskName, calib.JointInfo.Master.AcqImageName);
            gbMaster.Controls.Add(displayComponent.DisplayedControl);

            c++;
            if (c == colCount)
            {
                c = 0;
                r++;
            }

            foreach (TaskAcqImageVO acq in calib.JointInfo.Slaves)
            {
                GroupBox gbSlave = new GroupBox(){ Text = $"{acq.TaskName}-{acq.AcqImageName}", Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(gbSlave, c, r);

                IDisplayTaskImageComponent displayComponent1 = jointDisplayComponents.GetJointDisplayControl(acq.TaskName, acq.AcqImageName);
                gbSlave.Controls.Add(displayComponent1.DisplayedControl);

                c++;
                if (c == colCount)
                {
                    c = 0;
                    r++;
                }
            }
        }

        (int rowCount, int colCount) GetRowCol(int count)
        {
            if (count == 1) return (1, 1);

            if (count == 2) return (1, 2);

            if (count <= 4) return (2, 2);

            if (count <= 6) return (2, 3);

            return (3, 3);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            LinkedDictionary<string, object> outputs = runner.Run(calib.JointInfo.Master.TaskName, calib.JointInfo.Master.AcqImageName);

            if (outputs != null)
            {
                dataGridView1.Rows.Clear();
                for (int i = 1; i <= calib.JointInfo.PointCount; i++)
                {
                    dataGridView1.Rows.Add(i - 1, outputs["X" + i.ToString()], outputs["Y" + i.ToString()]);
                }

                foreach (CC slaveControl in slaveControls)
                {
                    slaveControl.SetPhysicsPoints(outputs);
                }
            }
        }

        class CC
        {
            TabPage p;
            JointControl c;
            CalibrationInfoVO calib;

            JointRunner runner;
            IToolBlockComponent taskToolBlock;
            FormSettings formSettings;
            IUserRepository userRepo;

            TaskAcqImageVO acqInfo;

            public CC(CalibrationInfoVO calib,
                TabPage p,
                JointControl c,
                JointRunner runner,
                IToolBlockComponent taskToolBlock,
                FormSettings formSettings,
                IUserRepository userRepo)
            {
                InitializeComponent();

                this.calib = calib;
                this.p = p;
                this.c = c;
                this.runner = runner;
                this.taskToolBlock = taskToolBlock;
                this.formSettings = formSettings;
                this.userRepo = userRepo;

                split.Panel1.Controls.Add(c.DisplayedControl);
                p.Controls.Add(split);

                acqInfo = calib.JointInfo.Slaves.First(a => a.AcqImageName == c.AcqName);

                XStep.Value = (decimal)acqInfo.Offset_X;
                YStep.Value = (decimal)acqInfo.Offset_Y;
            }

            #region 界面设计

            SplitContainer split;
            Label label1;
            Label label2;
            NumericUpDown YStep;
            NumericUpDown XStep;

            Button btnRun;
            Button btnCalib;
            Button btnShowNPoint;
            Button btnOptical;

            DataGridViewTextBoxColumn colSN;
            DataGridViewTextBoxColumn colX1;
            DataGridViewTextBoxColumn colY1;
            DataGridViewTextBoxColumn colX2;
            DataGridViewTextBoxColumn colY2;
            DataGridView dgv;

            ContextMenuStrip contextMenuStrip1;
            ToolStripMenuItem tsmiAutoSetValue;

            private void InitializeComponent()
            {
                split = new SplitContainer();
                label1 = new Label();
                label2 = new Label();
                btnRun = new Button();
                btnCalib = new Button();
                btnShowNPoint = new Button();
                btnOptical = new Button();
                colSN = new DataGridViewTextBoxColumn();
                colX1 = new DataGridViewTextBoxColumn();
                colY1 = new DataGridViewTextBoxColumn();
                colX2 = new DataGridViewTextBoxColumn();
                colY2 = new DataGridViewTextBoxColumn();
                dgv = new DataGridView();
                YStep = new NumericUpDown();
                XStep = new NumericUpDown();
                contextMenuStrip1 = new ContextMenuStrip();
                tsmiAutoSetValue = new ToolStripMenuItem();
                ((System.ComponentModel.ISupportInitialize)(YStep)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(XStep)).BeginInit();

                split.Dock = DockStyle.Fill;
                split.Orientation = Orientation.Vertical;
                split.Panel1MinSize = 400;
                split.Panel2.AutoScroll = true;
                split.Size = new Size(1338, 618);
                split.SplitterDistance = 800;

                label1.AutoSize = true;
                label1.Location = new Point(20, 20);
                label1.Size = new Size(98, 15);
                label1.Text = "移动距离 X：";

                XStep.DecimalPlaces = 3;
                XStep.Increment = 0.001m;
                XStep.Location = new Point(100, 16);
                XStep.Maximum = decimal.MaxValue;
                XStep.Minimum = decimal.MinValue;
                XStep.Size = new Size(80, 25);
                XStep.TextAlign = HorizontalAlignment.Right;
                XStep.Value = 0m;
                XStep.ValueChanged += XStep_ValueChanged;

                label2.AutoSize = true;
                label2.Location = new Point(260, 20);
                label2.Size = new Size(98, 15);
                label2.Text = "移动距离 Y：";

                YStep.DecimalPlaces = 3;
                YStep.Increment = 0.001m;
                YStep.Location = new Point(340, 16);
                YStep.Maximum = decimal.MaxValue;
                YStep.Minimum = decimal.MinValue;
                YStep.Size = new Size(80, 25);
                YStep.TextAlign = HorizontalAlignment.Right;
                YStep.Value = 0m;
                YStep.ValueChanged += YStep_ValueChanged;

                btnOptical.Location = new Point(20, 55);
                btnOptical.Size = new Size(100, 35);
                btnOptical.Text = "光学设置";
                btnOptical.UseVisualStyleBackColor = true;
                btnOptical.Click += BtnOptical_Click;

                btnRun.Location = new Point(140, 55);
                btnRun.Size = new Size(100, 35);
                btnRun.Text = "运行";
                btnRun.UseVisualStyleBackColor = true;
                btnRun.Click += BtnRun_Click;

                btnCalib.Location = new Point(260, 55);
                btnCalib.Size = new Size(100, 35);
                btnCalib.Text = "标定";
                btnCalib.UseVisualStyleBackColor = true;
                btnCalib.Click += BtnCalib_Click;

                btnShowNPoint.Location = new Point(380, 55);
                btnShowNPoint.Size = new Size(100, 35);
                btnShowNPoint.Text = "查看结果";
                btnShowNPoint.UseVisualStyleBackColor = true;
                btnShowNPoint.Click += BtnShowNPoint_Click;

                colSN.HeaderText = "序号";
                colSN.MinimumWidth = 20;
                colSN.Width = 60;

                colX1.HeaderText = "未校正 X";
                colX1.MinimumWidth = 20;
                colX1.Width = 90;

                colY1.HeaderText = "未校正 Y";
                colY1.MinimumWidth = 20;
                colY1.Width = 90;

                colX2.HeaderText = "已校正 X";
                colX2.MinimumWidth = 20;
                colX2.Width = 90;

                colY2.HeaderText = "已校正 Y";
                colY2.MinimumWidth = 20;
                colY2.Width = 90;

                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.BackgroundColor = SystemColors.ControlLightLight;
                dgv.Columns.AddRange(colSN, colX1, colY1, colX2, colY2);
                dgv.RowHeadersWidth = 25;
                dgv.Location = new Point(20, 105);
                dgv.Size = new Size(460, 300);
                dgv.ContextMenuStrip = contextMenuStrip1;

                contextMenuStrip1.ImageScalingSize = new Size(20, 20);
                contextMenuStrip1.Items.AddRange(new ToolStripItem[] { tsmiAutoSetValue });
                contextMenuStrip1.Name = "contextMenuStrip1";
                contextMenuStrip1.Size = new Size(139, 76);
                contextMenuStrip1.Opening += ContextMenuStrip1_Opening;

                tsmiAutoSetValue.Name = "tsmiAutoSetValue";
                tsmiAutoSetValue.Size = new Size(138, 24);
                tsmiAutoSetValue.Text = "自动填值";
                tsmiAutoSetValue.Click += TsmiAutoSetValue_Click;

                split.Panel2.Controls.AddRange(new Control[] { label1, label2, XStep, YStep, btnOptical, btnRun, btnCalib, btnShowNPoint, dgv });

                ((System.ComponentModel.ISupportInitialize)(YStep)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(XStep)).EndInit();
            }

            private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
            {
                UserVO current = userRepo.CurrUser;
                if (current == null || current.Role != Models.Role.Developer)
                {
                    e.Cancel = true;
                }
            }

            double[][] points = new double[9][]
            {
                new double[] { -2, -2 },
                new double[] {  0, -2 },
                new double[] {  2, -2 },
                new double[] {  2,  0 },
                new double[] {  0,  0 },
                new double[] { -2,  0 },
                new double[] { -2,  2 },
                new double[] {  0,  2 },
                new double[] {  2,  2 }
            };

            private void TsmiAutoSetValue_Click(object sender, EventArgs e)
            {
                dgv.Rows.Clear();

                for (int i = 0; i < calib.JointInfo.PointCount; i++)
                {
                    dgv.Rows.Add(i, points[i][0], points[i][1], points[i][0], points[i][1]);
                }
            }

            private void XStep_ValueChanged(object sender, EventArgs e)
            {
                double offset = (double)XStep.Value - acqInfo.Offset_X;

                acqInfo.Offset_X = (double)XStep.Value;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[3].Value != null)
                    {
                        row.Cells[3].Value = (double)row.Cells[3].Value + offset;
                    }
                }
            }

            private void YStep_ValueChanged(object sender, EventArgs e)
            {
                double offset = (double)YStep.Value - acqInfo.Offset_Y;

                acqInfo.Offset_Y = (double)YStep.Value;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        row.Cells[4].Value = (double)row.Cells[4].Value + offset;
                    }
                }
            }

            #endregion

            private void BtnOptical_Click(object sender, EventArgs e)
            {
                string text = $"{c.TaskName}/{c.AcqName}/{calib.Name}-光学设置";

                FrmOpticalSetting form = (FrmOpticalSetting)formSettings.FindSubForm(text);

                if (form == null)
                {
                    form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                    form.Text = text;
                    form.LoadOptics(c.TaskName, c.AcqName, calib.Name);
                }
                formSettings.ShowAloneForm(DockState.Document, form);
            }

            private void BtnRun_Click(object sender, EventArgs e)
            {
                if (dgv.Rows.Count == 0 || dgv.Rows[0].Cells[3].Value == null)
                {
                    MessageBoxUtils.ShowWarn("请先执行主标定");
                    return;
                }

                LinkedDictionary<string, object> outputs = runner.Run(c.TaskName, c.AcqName);
                if (outputs == null) return;

                for (int i = 1; i <= calib.JointInfo.PointCount; i++)
                {
                    string xname = "X" + i.ToString();
                    string yname = "Y" + i.ToString();

                    if (outputs.ContainsKey(xname) && outputs[xname] != null)
                        dgv.Rows[i - 1].Cells[1].Value = (double)outputs[xname];

                    if (outputs.ContainsKey(yname) && outputs[yname] != null)
                        dgv.Rows[i - 1].Cells[2].Value = (double)outputs[yname];
                }
            }

            bool Check()
            {
                if (dgv.Rows.Count < calib.JointInfo.PointCount)
                {
                    MessageBoxUtils.ShowWarn("数据不全，无法标定");
                    return false;
                }

                for (int i = 0; i < calib.JointInfo.PointCount; i++)
                {
                    if (dgv.Rows[i].Cells[1].Value == null
                        || dgv.Rows[i].Cells[2].Value == null
                        || dgv.Rows[i].Cells[3].Value == null
                        || dgv.Rows[i].Cells[4].Value == null)
                    {
                        MessageBoxUtils.ShowWarn("数据不全，无法标定");
                        return false;
                    }
                }

                return true;
            }

            private void BtnCalib_Click(object sender, EventArgs e)
            {
                if (!Check()) return;

                List<(double X1, double Y1, double X2, double Y2)> points
                    = new List<(double X1, double Y1, double X2, double Y2)>(dgv.Rows.Count);

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    points.Add(((double)row.Cells[1].Value, (double)row.Cells[2].Value, (double)row.Cells[3].Value, (double)row.Cells[4].Value));
                }

                try
                {
                    runner.Calibration(c.TaskName, c.AcqName, points);

                    MessageBoxUtils.ShowInfo("标定完成");
                }
                catch (Exception ex)
                {
                    MessageBoxUtils.ShowError("标定失败，" + ex.Message);
                }
            }

            private void BtnShowNPoint_Click(object sender, EventArgs e)
            {
                object tool = taskToolBlock.GetToolEdit($"{calib.Name}_{c.AcqName}");

                FrmJointResult f = new FrmJointResult(tool as Control);
                f.Text = p.Text;
                f.ShowDialog();
            }

            public void SetPhysicsPoints(LinkedDictionary<string, object> outputs)
            {
                dgv.Rows.Clear();

                double offsetX = (double)XStep.Value;
                double offsetY = (double)YStep.Value;

                for (int i = 1; i <= calib.JointInfo.PointCount; i++)
                {
                    dgv.Rows.Add(i - 1, null, null,
                        GetValue((double?)outputs["X" + i.ToString()], offsetX),
                        GetValue((double?)outputs["Y" + i.ToString()], offsetY));
                }
            }

            double? GetValue(double? value, double offset)
            {
                if (value.HasValue)
                {
                    return value.Value + offset;
                }

                return null;
            }
        }

        private void btnMasterCalib_Click(object sender, EventArgs e)
        {
            string taskName = calib.JointInfo.Master.TaskName;
            string acqName = calib.JointInfo.Master.AcqImageName;
            string handEyeName = taskRepo.GetTaskByName(taskName)
                .CameraAcquireImage.AcquireImages.First(a => a.JointName == calib.Name).HandEyeNames[0];

            string text = Utils.GetCalibrationFormName(taskName, handEyeName, Models.CalibrationType.HandEye);

            Form form = formSettings.FindSubForm(text);

            FrmHandEyeSetting frmHandEye;
            if (form == null)
            {
                frmHandEye = AutoFacContainer.Resolve<FrmHandEyeSetting>();
                frmHandEye.Init(taskName, acqName, handEyeName);
                frmHandEye.Text = text;
            }
            else
            {
                frmHandEye = (FrmHandEyeSetting)form;
            }
            frmHandEye.OpenOptical = OpenCalibrationOptical;
            formSettings.ShowAloneForm(DockState.Document, frmHandEye);
        }

        private void OpenCalibrationOptical(string taskName, string calibName)
        {
            string text = $"{taskName}/{calibName}-光学设置";

            FrmOpticalSetting form = (FrmOpticalSetting)formSettings.FindSubForm(text);

            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                form.LoadOptics(taskName, null, calibName);
            }
            formSettings.ShowAloneForm(DockState.Document, form);
        }

        private void FrmJoint_FormClosing(object sender, FormClosingEventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            runner?.JointComponent.Save();

            calibrationRepo.SetJointParams(calib.Name, (int)nudPointCount.Value, calib.JointInfo);
            calibrationRepo.Save();
        }

        private void btnCalibM_Click(object sender, EventArgs e)
        {
            string taskName = calib.JointInfo.Master.TaskName;
            string acqName = calib.JointInfo.Master.AcqImageName;

            string text = $"{taskName}/{acqName}/{calib.Name}-光学设置";

            FrmOpticalSetting form = (FrmOpticalSetting)formSettings.FindSubForm(text);

            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmOpticalSetting>();
                form.Text = text;
                form.LoadOptics(taskName, acqName, calib.Name);
            }
            formSettings.ShowAloneForm(DockState.Document, form);
        }

        private void nudPointCount_ValueChanged(object sender, EventArgs e)
        {
            calib.JointInfo.PointCount = (int)nudPointCount.Value;
        }
    }
}
