namespace HyEye.WForm
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserManager = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCameraSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLightSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommandSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVisionParamsSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCameraSDKLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiState = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCameraState = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTaskSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpticalSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRunTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblConnectionState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblPSR = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblLayout = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblMT = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOnLine = new System.Windows.Forms.Button();
            this.btnMaterial = new System.Windows.Forms.Button();
            this.btnDisplaySetting = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.dockPanelMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUser,
            this.tsmiSettings,
            this.tsmiState,
            this.tsmiTaskSetting,
            this.tsmiOpticalSetting,
            this.tsmiCalibration,
            this.tsmiRunTasks});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1284, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiUser
            // 
            this.tsmiUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLogin,
            this.tsmiUserManager,
            this.tsmiChangePassword,
            this.tsmiExit});
            this.tsmiUser.Image = global::HyEye.WForm.Properties.Resources.用户;
            this.tsmiUser.Name = "tsmiUser";
            this.tsmiUser.Size = new System.Drawing.Size(73, 26);
            this.tsmiUser.Text = "用户";
            // 
            // tsmiLogin
            // 
            this.tsmiLogin.Name = "tsmiLogin";
            this.tsmiLogin.Size = new System.Drawing.Size(152, 26);
            this.tsmiLogin.Text = "用户登录";
            this.tsmiLogin.Click += new System.EventHandler(this.tsmiLogin_Click);
            // 
            // tsmiUserManager
            // 
            this.tsmiUserManager.Name = "tsmiUserManager";
            this.tsmiUserManager.Size = new System.Drawing.Size(152, 26);
            this.tsmiUserManager.Text = "用户管理";
            this.tsmiUserManager.Click += new System.EventHandler(this.tsmiUserManager_Click);
            // 
            // tsmiChangePassword
            // 
            this.tsmiChangePassword.Name = "tsmiChangePassword";
            this.tsmiChangePassword.Size = new System.Drawing.Size(152, 26);
            this.tsmiChangePassword.Text = "修改密码";
            this.tsmiChangePassword.Click += new System.EventHandler(this.tsmiChangePassword_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(152, 26);
            this.tsmiExit.Text = "用户退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCameraSetting,
            this.tsmiLightSetting,
            this.tsmiCommSetting,
            this.tsmiCommandSetting,
            this.tsmiImageSetting,
            this.tsmiSystemSetting,
            this.tsmiVisionParamsSetting,
            this.tsmiCameraSDKLog});
            this.tsmiSettings.Image = global::HyEye.WForm.Properties.Resources.系统设置;
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(73, 26);
            this.tsmiSettings.Text = "设置";
            // 
            // tsmiCameraSetting
            // 
            this.tsmiCameraSetting.Name = "tsmiCameraSetting";
            this.tsmiCameraSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiCameraSetting.Text = "相机设置";
            this.tsmiCameraSetting.Click += new System.EventHandler(this.tsmiCameraSetting_Click);
            // 
            // tsmiLightSetting
            // 
            this.tsmiLightSetting.Name = "tsmiLightSetting";
            this.tsmiLightSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiLightSetting.Text = "光控设置";
            this.tsmiLightSetting.Click += new System.EventHandler(this.tsmiLightSetting_Click);
            // 
            // tsmiCommSetting
            // 
            this.tsmiCommSetting.Name = "tsmiCommSetting";
            this.tsmiCommSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiCommSetting.Text = "通讯设置";
            this.tsmiCommSetting.Click += new System.EventHandler(this.tsmiCommSetting_Click);
            // 
            // tsmiCommandSetting
            // 
            this.tsmiCommandSetting.Name = "tsmiCommandSetting";
            this.tsmiCommandSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiCommandSetting.Text = "指令设置";
            this.tsmiCommandSetting.Click += new System.EventHandler(this.tsmiCommandSetting_Click);
            // 
            // tsmiImageSetting
            // 
            this.tsmiImageSetting.Name = "tsmiImageSetting";
            this.tsmiImageSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiImageSetting.Text = "图像设置";
            this.tsmiImageSetting.Click += new System.EventHandler(this.tsmiImageSetting_Click);
            // 
            // tsmiSystemSetting
            // 
            this.tsmiSystemSetting.Name = "tsmiSystemSetting";
            this.tsmiSystemSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiSystemSetting.Text = "系统设置";
            this.tsmiSystemSetting.Click += new System.EventHandler(this.tsmiSystemSetting_Click);
            // 
            // tsmiVisionParamsSetting
            // 
            this.tsmiVisionParamsSetting.Name = "tsmiVisionParamsSetting";
            this.tsmiVisionParamsSetting.Size = new System.Drawing.Size(190, 26);
            this.tsmiVisionParamsSetting.Text = "视觉参数设置";
            this.tsmiVisionParamsSetting.Click += new System.EventHandler(this.tsmiVisionParamsSetting_Click);
            // 
            // tsmiCameraSDKLog
            // 
            this.tsmiCameraSDKLog.Name = "tsmiCameraSDKLog";
            this.tsmiCameraSDKLog.Size = new System.Drawing.Size(190, 26);
            this.tsmiCameraSDKLog.Text = "相机 SDK 日志";
            this.tsmiCameraSDKLog.Click += new System.EventHandler(this.tsmiCameraSDKLog_Click);
            // 
            // tsmiState
            // 
            this.tsmiState.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCameraState,
            this.tsmiLog});
            this.tsmiState.Image = global::HyEye.WForm.Properties.Resources.状态查看;
            this.tsmiState.Name = "tsmiState";
            this.tsmiState.Size = new System.Drawing.Size(73, 26);
            this.tsmiState.Text = "状态";
            // 
            // tsmiCameraState
            // 
            this.tsmiCameraState.Name = "tsmiCameraState";
            this.tsmiCameraState.Size = new System.Drawing.Size(182, 26);
            this.tsmiCameraState.Text = "相机连接状态";
            this.tsmiCameraState.Click += new System.EventHandler(this.tsmiCameraState_Click);
            // 
            // tsmiLog
            // 
            this.tsmiLog.Name = "tsmiLog";
            this.tsmiLog.Size = new System.Drawing.Size(182, 26);
            this.tsmiLog.Text = "日志";
            this.tsmiLog.Click += new System.EventHandler(this.tsmiLog_Click);
            // 
            // tsmiTaskSetting
            // 
            this.tsmiTaskSetting.Image = global::HyEye.WForm.Properties.Resources.任务配置;
            this.tsmiTaskSetting.Name = "tsmiTaskSetting";
            this.tsmiTaskSetting.Size = new System.Drawing.Size(73, 26);
            this.tsmiTaskSetting.Text = "任务";
            this.tsmiTaskSetting.Click += new System.EventHandler(this.tsmiTaskSetting_Click);
            // 
            // tsmiOpticalSetting
            // 
            this.tsmiOpticalSetting.Image = global::HyEye.WForm.Properties.Resources.光学设置;
            this.tsmiOpticalSetting.Name = "tsmiOpticalSetting";
            this.tsmiOpticalSetting.Size = new System.Drawing.Size(103, 26);
            this.tsmiOpticalSetting.Text = "光学设置";
            // 
            // tsmiCalibration
            // 
            this.tsmiCalibration.Image = global::HyEye.WForm.Properties.Resources.手眼标定模板设置;
            this.tsmiCalibration.Name = "tsmiCalibration";
            this.tsmiCalibration.Size = new System.Drawing.Size(73, 26);
            this.tsmiCalibration.Text = "标定";
            this.tsmiCalibration.Click += new System.EventHandler(this.tsmiCalibration_Click);
            // 
            // tsmiRunTasks
            // 
            this.tsmiRunTasks.Image = global::HyEye.WForm.Properties.Resources.开始1;
            this.tsmiRunTasks.Name = "tsmiRunTasks";
            this.tsmiRunTasks.Size = new System.Drawing.Size(73, 26);
            this.tsmiRunTasks.Text = "运行";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblConnectionState,
            this.tslblPSR,
            this.tslblLayout,
            this.tslblMT,
            this.tslblTime});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 723);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1284, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblConnectionState
            // 
            this.tslblConnectionState.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tslblConnectionState.ForeColor = System.Drawing.Color.Red;
            this.tslblConnectionState.Image = global::HyEye.WForm.Properties.Resources.信号_未连接;
            this.tslblConnectionState.Name = "tslblConnectionState";
            this.tslblConnectionState.Size = new System.Drawing.Size(78, 20);
            this.tslblConnectionState.Text = " 未连接";
            // 
            // tslblPSR
            // 
            this.tslblPSR.Name = "tslblPSR";
            this.tslblPSR.Size = new System.Drawing.Size(0, 20);
            // 
            // tslblLayout
            // 
            this.tslblLayout.AutoSize = false;
            this.tslblLayout.Name = "tslblLayout";
            this.tslblLayout.Size = new System.Drawing.Size(100, 20);
            // 
            // tslblMT
            // 
            this.tslblMT.Name = "tslblMT";
            this.tslblMT.Size = new System.Drawing.Size(167, 20);
            this.tslblMT.Text = "toolStripStatusLabel1";
            // 
            // tslblTime
            // 
            this.tslblTime.Image = global::HyEye.WForm.Properties.Resources.时间;
            this.tslblTime.Name = "tslblTime";
            this.tslblTime.Size = new System.Drawing.Size(183, 20);
            this.tslblTime.Text = " 2021-01-01 00:00:00";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnStart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOnLine, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMaterial, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDisplaySetting, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnHelp, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAlarm, 6, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1284, 67);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.BackgroundImage = global::HyEye.WForm.Properties.Resources.开始2;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 61);
            this.btnStart.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnStart, "开始");
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::HyEye.WForm.Properties.Resources.停止2;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(103, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 61);
            this.btnStop.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnStop, "停止");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOnLine
            // 
            this.btnOnLine.BackgroundImage = global::HyEye.WForm.Properties.Resources.在线1;
            this.btnOnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOnLine.Location = new System.Drawing.Point(203, 3);
            this.btnOnLine.Name = "btnOnLine";
            this.btnOnLine.Size = new System.Drawing.Size(94, 61);
            this.btnOnLine.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnOnLine, "在线模式");
            this.btnOnLine.UseVisualStyleBackColor = true;
            this.btnOnLine.Click += new System.EventHandler(this.btnOnLine_Click);
            // 
            // btnMaterial
            // 
            this.btnMaterial.BackgroundImage = global::HyEye.WForm.Properties.Resources.物料;
            this.btnMaterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMaterial.Location = new System.Drawing.Point(403, 3);
            this.btnMaterial.Name = "btnMaterial";
            this.btnMaterial.Size = new System.Drawing.Size(94, 61);
            this.btnMaterial.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnMaterial, "料号管理");
            this.btnMaterial.UseVisualStyleBackColor = true;
            this.btnMaterial.Click += new System.EventHandler(this.btnMaterial_Click);
            // 
            // btnDisplaySetting
            // 
            this.btnDisplaySetting.BackgroundImage = global::HyEye.WForm.Properties.Resources.显示设置;
            this.btnDisplaySetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDisplaySetting.Location = new System.Drawing.Point(303, 3);
            this.btnDisplaySetting.Name = "btnDisplaySetting";
            this.btnDisplaySetting.Size = new System.Drawing.Size(94, 61);
            this.btnDisplaySetting.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnDisplaySetting, "布局");
            this.btnDisplaySetting.UseVisualStyleBackColor = true;
            this.btnDisplaySetting.Click += new System.EventHandler(this.btnDisplaySetting_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackgroundImage = global::HyEye.WForm.Properties.Resources.帮助;
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHelp.Location = new System.Drawing.Point(503, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(94, 61);
            this.btnHelp.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnHelp, "帮助");
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.BackgroundImage = global::HyEye.WForm.Properties.Resources.警报;
            this.btnAlarm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAlarm.Location = new System.Drawing.Point(603, 3);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(94, 61);
            this.btnAlarm.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnAlarm, "报警");
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Visible = false;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // dockPanelMain
            // 
            this.dockPanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanelMain.Location = new System.Drawing.Point(0, 97);
            this.dockPanelMain.Name = "dockPanelMain";
            this.dockPanelMain.Size = new System.Drawing.Size(1284, 626);
            this.dockPanelMain.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1284, 749);
            this.Controls.Add(this.dockPanelMain);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "HyInspect";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUser;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolStripStatusLabel tslblConnectionState;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Button btnOnLine;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserManager;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCameraSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiLightSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommandSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiState;
        private System.Windows.Forms.ToolStripMenuItem tsmiCameraState;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskSetting;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanelMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiRunTasks;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageSetting;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePassword;
        private System.Windows.Forms.Button btnMaterial;
        private System.Windows.Forms.Button btnDisplaySetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpticalSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiCalibration;
        private System.Windows.Forms.ToolStripMenuItem tsmiCameraSDKLog;
        private System.Windows.Forms.ToolStripStatusLabel tslblMT;
        private System.Windows.Forms.ToolStripStatusLabel tslblLayout;
        private System.Windows.Forms.ToolStripMenuItem tsmiVisionParamsSetting;
        private System.Windows.Forms.ToolStripStatusLabel tslblPSR;
    }
}