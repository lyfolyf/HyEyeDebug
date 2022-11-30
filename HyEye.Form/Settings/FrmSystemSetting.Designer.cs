namespace HyEye.WForm.Settings
{
    partial class FrmSystemSetting
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
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.gbBase = new System.Windows.Forms.GroupBox();
            this.rdbtnSaveExcludeDataBindings = new System.Windows.Forms.RadioButton();
            this.rdbtnSaveAll = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.tbSimulationPath = new System.Windows.Forms.TextBox();
            this.cbDeleteVPP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchSimulationPath = new System.Windows.Forms.Button();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbRunning = new System.Windows.Forms.GroupBox();
            this.nudCmdRTimeout = new System.Windows.Forms.NumericUpDown();
            this.nudAcqImageTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbDataSave = new System.Windows.Forms.GroupBox();
            this.rdbtnDataSaveModeLast = new System.Windows.Forms.RadioButton();
            this.rdbtnDataSaveModeAll = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.ckbDataSaveEnabled = new System.Windows.Forms.CheckBox();
            this.tbDataSavePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSearchDataSavePath = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.ckbEnableCmdIndex = new System.Windows.Forms.CheckBox();
            this.ckbEnableHandCmd = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nudDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.rdbOnlyValue = new System.Windows.Forms.RadioButton();
            this.rdbCmdKeyValue = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.cmbFileLogLevel = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConfigSavePath = new System.Windows.Forms.TextBox();
            this.btnSelectConfigSavePath = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.ckConfigSavePath = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtConfigSaveType = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.gbBase.SuspendLayout();
            this.gbRunning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCmdRTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcqImageTimeout)).BeginInit();
            this.gbDataSave.SuspendLayout();
            this.gbCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).BeginInit();
            this.gbLog.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(20, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(669, 35);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImage = global::HyEye.WForm.Properties.Resources.保存;
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSave.Location = new System.Drawing.Point(3, 3);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(29, 29);
            this.pbSave.TabIndex = 29;
            this.pbSave.TabStop = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 727);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBottom.Size = new System.Drawing.Size(709, 75);
            this.pnlBottom.TabIndex = 21;
            // 
            // gbBase
            // 
            this.gbBase.Controls.Add(this.rdbtnSaveExcludeDataBindings);
            this.gbBase.Controls.Add(this.rdbtnSaveAll);
            this.gbBase.Controls.Add(this.label15);
            this.gbBase.Controls.Add(this.tbSimulationPath);
            this.gbBase.Controls.Add(this.cbDeleteVPP);
            this.gbBase.Controls.Add(this.label3);
            this.gbBase.Controls.Add(this.btnSearchSimulationPath);
            this.gbBase.Controls.Add(this.cbAutoStart);
            this.gbBase.Controls.Add(this.label7);
            this.gbBase.Controls.Add(this.label2);
            this.gbBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbBase.Location = new System.Drawing.Point(20, 20);
            this.gbBase.Name = "gbBase";
            this.gbBase.Size = new System.Drawing.Size(648, 180);
            this.gbBase.TabIndex = 9;
            this.gbBase.TabStop = false;
            this.gbBase.Text = "基本设置";
            // 
            // rdbtnSaveExcludeDataBindings
            // 
            this.rdbtnSaveExcludeDataBindings.AutoSize = true;
            this.rdbtnSaveExcludeDataBindings.Location = new System.Drawing.Point(321, 88);
            this.rdbtnSaveExcludeDataBindings.Name = "rdbtnSaveExcludeDataBindings";
            this.rdbtnSaveExcludeDataBindings.Size = new System.Drawing.Size(208, 19);
            this.rdbtnSaveExcludeDataBindings.TabIndex = 4;
            this.rdbtnSaveExcludeDataBindings.TabStop = true;
            this.rdbtnSaveExcludeDataBindings.Text = "保存不带图像或结果的工具";
            this.rdbtnSaveExcludeDataBindings.UseVisualStyleBackColor = true;
            // 
            // rdbtnSaveAll
            // 
            this.rdbtnSaveAll.AutoSize = true;
            this.rdbtnSaveAll.Location = new System.Drawing.Point(155, 88);
            this.rdbtnSaveAll.Name = "rdbtnSaveAll";
            this.rdbtnSaveAll.Size = new System.Drawing.Size(118, 19);
            this.rdbtnSaveAll.TabIndex = 3;
            this.rdbtnSaveAll.TabStop = true;
            this.rdbtnSaveAll.Text = "保存完整工具";
            this.rdbtnSaveAll.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(38, 90);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 15);
            this.label15.TabIndex = 10;
            this.label15.Text = "VPP 保存模式：";
            // 
            // tbSimulationPath
            // 
            this.tbSimulationPath.Enabled = false;
            this.tbSimulationPath.Location = new System.Drawing.Point(155, 135);
            this.tbSimulationPath.Name = "tbSimulationPath";
            this.tbSimulationPath.Size = new System.Drawing.Size(435, 25);
            this.tbSimulationPath.TabIndex = 1;
            // 
            // cbDeleteVPP
            // 
            this.cbDeleteVPP.AutoSize = true;
            this.cbDeleteVPP.Location = new System.Drawing.Point(497, 40);
            this.cbDeleteVPP.Name = "cbDeleteVPP";
            this.cbDeleteVPP.Size = new System.Drawing.Size(18, 17);
            this.cbDeleteVPP.TabIndex = 2;
            this.cbDeleteVPP.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "模拟图片路径：";
            // 
            // btnSearchSimulationPath
            // 
            this.btnSearchSimulationPath.BackgroundImage = global::HyEye.WForm.Properties.Resources.打开文件夹;
            this.btnSearchSimulationPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchSimulationPath.Location = new System.Drawing.Point(590, 134);
            this.btnSearchSimulationPath.Name = "btnSearchSimulationPath";
            this.btnSearchSimulationPath.Size = new System.Drawing.Size(35, 26);
            this.btnSearchSimulationPath.TabIndex = 5;
            this.btnSearchSimulationPath.UseVisualStyleBackColor = true;
            this.btnSearchSimulationPath.Click += new System.EventHandler(this.btnSearchSimulationPath_Click);
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Location = new System.Drawing.Point(155, 40);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(18, 17);
            this.cbAutoStart.TabIndex = 1;
            this.cbAutoStart.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(318, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "删除任务同时删除 VPP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "自动启动：";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 20);
            this.panel1.TabIndex = 13;
            // 
            // gbRunning
            // 
            this.gbRunning.Controls.Add(this.nudCmdRTimeout);
            this.gbRunning.Controls.Add(this.nudAcqImageTimeout);
            this.gbRunning.Controls.Add(this.label1);
            this.gbRunning.Controls.Add(this.label5);
            this.gbRunning.Controls.Add(this.label4);
            this.gbRunning.Controls.Add(this.label6);
            this.gbRunning.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRunning.Location = new System.Drawing.Point(20, 220);
            this.gbRunning.Name = "gbRunning";
            this.gbRunning.Size = new System.Drawing.Size(648, 85);
            this.gbRunning.TabIndex = 12;
            this.gbRunning.TabStop = false;
            this.gbRunning.Text = "运行设置";
            // 
            // nudCmdRTimeout
            // 
            this.nudCmdRTimeout.Location = new System.Drawing.Point(497, 35);
            this.nudCmdRTimeout.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudCmdRTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudCmdRTimeout.Name = "nudCmdRTimeout";
            this.nudCmdRTimeout.Size = new System.Drawing.Size(80, 25);
            this.nudCmdRTimeout.TabIndex = 12;
            this.nudCmdRTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCmdRTimeout.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // nudAcqImageTimeout
            // 
            this.nudAcqImageTimeout.Location = new System.Drawing.Point(155, 35);
            this.nudAcqImageTimeout.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.nudAcqImageTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudAcqImageTimeout.Name = "nudAcqImageTimeout";
            this.nudAcqImageTimeout.Size = new System.Drawing.Size(80, 25);
            this.nudAcqImageTimeout.TabIndex = 11;
            this.nudAcqImageTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAcqImageTimeout.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "拍照超时时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(580, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "毫秒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "毫秒";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "R指令超时时间：";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 305);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(648, 20);
            this.panel2.TabIndex = 14;
            // 
            // gbDataSave
            // 
            this.gbDataSave.Controls.Add(this.rdbtnDataSaveModeLast);
            this.gbDataSave.Controls.Add(this.rdbtnDataSaveModeAll);
            this.gbDataSave.Controls.Add(this.label14);
            this.gbDataSave.Controls.Add(this.ckbDataSaveEnabled);
            this.gbDataSave.Controls.Add(this.tbDataSavePath);
            this.gbDataSave.Controls.Add(this.label8);
            this.gbDataSave.Controls.Add(this.label9);
            this.gbDataSave.Controls.Add(this.btnSearchDataSavePath);
            this.gbDataSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDataSave.Location = new System.Drawing.Point(20, 325);
            this.gbDataSave.Name = "gbDataSave";
            this.gbDataSave.Size = new System.Drawing.Size(648, 180);
            this.gbDataSave.TabIndex = 11;
            this.gbDataSave.TabStop = false;
            this.gbDataSave.Text = "数据保存";
            // 
            // rdbtnDataSaveModeLast
            // 
            this.rdbtnDataSaveModeLast.AutoSize = true;
            this.rdbtnDataSaveModeLast.Enabled = false;
            this.rdbtnDataSaveModeLast.Location = new System.Drawing.Point(375, 138);
            this.rdbtnDataSaveModeLast.Name = "rdbtnDataSaveModeLast";
            this.rdbtnDataSaveModeLast.Size = new System.Drawing.Size(193, 19);
            this.rdbtnDataSaveModeLast.TabIndex = 24;
            this.rdbtnDataSaveModeLast.TabStop = true;
            this.rdbtnDataSaveModeLast.Text = "仅保存最后一次拍照结果";
            this.rdbtnDataSaveModeLast.UseVisualStyleBackColor = true;
            // 
            // rdbtnDataSaveModeAll
            // 
            this.rdbtnDataSaveModeAll.AutoSize = true;
            this.rdbtnDataSaveModeAll.Enabled = false;
            this.rdbtnDataSaveModeAll.Location = new System.Drawing.Point(156, 138);
            this.rdbtnDataSaveModeAll.Name = "rdbtnDataSaveModeAll";
            this.rdbtnDataSaveModeAll.Size = new System.Drawing.Size(148, 19);
            this.rdbtnDataSaveModeAll.TabIndex = 23;
            this.rdbtnDataSaveModeAll.TabStop = true;
            this.rdbtnDataSaveModeAll.Text = "保存每次拍照结果";
            this.rdbtnDataSaveModeAll.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(68, 140);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 15);
            this.label14.TabIndex = 23;
            this.label14.Text = "保存模式：";
            // 
            // ckbDataSaveEnabled
            // 
            this.ckbDataSaveEnabled.AutoSize = true;
            this.ckbDataSaveEnabled.Location = new System.Drawing.Point(155, 40);
            this.ckbDataSaveEnabled.Name = "ckbDataSaveEnabled";
            this.ckbDataSaveEnabled.Size = new System.Drawing.Size(18, 17);
            this.ckbDataSaveEnabled.TabIndex = 21;
            this.ckbDataSaveEnabled.UseVisualStyleBackColor = true;
            this.ckbDataSaveEnabled.CheckedChanged += new System.EventHandler(this.ckbDataSaveEnabled_CheckedChanged);
            // 
            // tbDataSavePath
            // 
            this.tbDataSavePath.Enabled = false;
            this.tbDataSavePath.Location = new System.Drawing.Point(155, 85);
            this.tbDataSavePath.Name = "tbDataSavePath";
            this.tbDataSavePath.Size = new System.Drawing.Size(435, 25);
            this.tbDataSavePath.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "启用：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "保存路径：";
            // 
            // btnSearchDataSavePath
            // 
            this.btnSearchDataSavePath.BackgroundImage = global::HyEye.WForm.Properties.Resources.打开文件夹;
            this.btnSearchDataSavePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchDataSavePath.Enabled = false;
            this.btnSearchDataSavePath.Location = new System.Drawing.Point(590, 84);
            this.btnSearchDataSavePath.Name = "btnSearchDataSavePath";
            this.btnSearchDataSavePath.Size = new System.Drawing.Size(35, 26);
            this.btnSearchDataSavePath.TabIndex = 22;
            this.btnSearchDataSavePath.UseVisualStyleBackColor = true;
            this.btnSearchDataSavePath.Click += new System.EventHandler(this.btnSearchDataSavePath_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(20, 505);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(648, 20);
            this.panel3.TabIndex = 16;
            // 
            // gbCommand
            // 
            this.gbCommand.Controls.Add(this.ckbEnableCmdIndex);
            this.gbCommand.Controls.Add(this.ckbEnableHandCmd);
            this.gbCommand.Controls.Add(this.label13);
            this.gbCommand.Controls.Add(this.nudDecimalPlaces);
            this.gbCommand.Controls.Add(this.label11);
            this.gbCommand.Controls.Add(this.rdbOnlyValue);
            this.gbCommand.Controls.Add(this.rdbCmdKeyValue);
            this.gbCommand.Controls.Add(this.label10);
            this.gbCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCommand.Location = new System.Drawing.Point(20, 525);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Size = new System.Drawing.Size(648, 175);
            this.gbCommand.TabIndex = 17;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "指令设置";
            // 
            // ckbEnableCmdIndex
            // 
            this.ckbEnableCmdIndex.AutoSize = true;
            this.ckbEnableCmdIndex.Location = new System.Drawing.Point(39, 140);
            this.ckbEnableCmdIndex.Name = "ckbEnableCmdIndex";
            this.ckbEnableCmdIndex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbEnableCmdIndex.Size = new System.Drawing.Size(134, 19);
            this.ckbEnableCmdIndex.TabIndex = 36;
            this.ckbEnableCmdIndex.Text = "：启用指令索引";
            this.ckbEnableCmdIndex.UseVisualStyleBackColor = true;
            // 
            // ckbEnableHandCmd
            // 
            this.ckbEnableHandCmd.AutoSize = true;
            this.ckbEnableHandCmd.Location = new System.Drawing.Point(464, 144);
            this.ckbEnableHandCmd.Name = "ckbEnableHandCmd";
            this.ckbEnableHandCmd.Size = new System.Drawing.Size(18, 17);
            this.ckbEnableHandCmd.TabIndex = 34;
            this.ckbEnableHandCmd.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(318, 144);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 15);
            this.label13.TabIndex = 35;
            this.label13.Text = "启用取像握手指令：";
            // 
            // nudDecimalPlaces
            // 
            this.nudDecimalPlaces.Location = new System.Drawing.Point(155, 85);
            this.nudDecimalPlaces.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudDecimalPlaces.Name = "nudDecimalPlaces";
            this.nudDecimalPlaces.Size = new System.Drawing.Size(80, 25);
            this.nudDecimalPlaces.TabIndex = 33;
            this.nudDecimalPlaces.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDecimalPlaces.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(68, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 15);
            this.label11.TabIndex = 5;
            this.label11.Text = "小数位数：";
            // 
            // rdbOnlyValue
            // 
            this.rdbOnlyValue.AutoSize = true;
            this.rdbOnlyValue.Location = new System.Drawing.Point(321, 38);
            this.rdbOnlyValue.Name = "rdbOnlyValue";
            this.rdbOnlyValue.Size = new System.Drawing.Size(108, 19);
            this.rdbOnlyValue.TabIndex = 32;
            this.rdbOnlyValue.TabStop = true;
            this.rdbOnlyValue.Text = "Only Value";
            this.rdbOnlyValue.UseVisualStyleBackColor = true;
            // 
            // rdbCmdKeyValue
            // 
            this.rdbCmdKeyValue.AutoSize = true;
            this.rdbCmdKeyValue.Location = new System.Drawing.Point(155, 38);
            this.rdbCmdKeyValue.Name = "rdbCmdKeyValue";
            this.rdbCmdKeyValue.Size = new System.Drawing.Size(116, 19);
            this.rdbCmdKeyValue.TabIndex = 31;
            this.rdbCmdKeyValue.Text = "Key = Value";
            this.rdbCmdKeyValue.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "发送指令格式：";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(20, 700);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(648, 20);
            this.panel4.TabIndex = 18;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.cmbFileLogLevel);
            this.gbLog.Controls.Add(this.label12);
            this.gbLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLog.Location = new System.Drawing.Point(20, 720);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(648, 85);
            this.gbLog.TabIndex = 19;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "日志";
            // 
            // cmbFileLogLevel
            // 
            this.cmbFileLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileLogLevel.FormattingEnabled = true;
            this.cmbFileLogLevel.Location = new System.Drawing.Point(155, 35);
            this.cmbFileLogLevel.Name = "cmbFileLogLevel";
            this.cmbFileLogLevel.Size = new System.Drawing.Size(121, 23);
            this.cmbFileLogLevel.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 15);
            this.label12.TabIndex = 0;
            this.label12.Text = "文件日志等级：";
            // 
            // pnlTop
            // 
            this.pnlTop.AutoScroll = true;
            this.pnlTop.Controls.Add(this.groupBox1);
            this.pnlTop.Controls.Add(this.panel5);
            this.pnlTop.Controls.Add(this.gbLog);
            this.pnlTop.Controls.Add(this.panel4);
            this.pnlTop.Controls.Add(this.gbCommand);
            this.pnlTop.Controls.Add(this.panel3);
            this.pnlTop.Controls.Add(this.gbDataSave);
            this.pnlTop.Controls.Add(this.panel2);
            this.pnlTop.Controls.Add(this.gbRunning);
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.gbBase);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTop.Size = new System.Drawing.Size(709, 727);
            this.pnlTop.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtConfigSaveType);
            this.groupBox1.Controls.Add(this.txtConfigSavePath);
            this.groupBox1.Controls.Add(this.btnSelectConfigSavePath);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.ckConfigSavePath);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(20, 825);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 100);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置自动保存";
            // 
            // txtConfigSavePath
            // 
            this.txtConfigSavePath.Enabled = false;
            this.txtConfigSavePath.Location = new System.Drawing.Point(134, 59);
            this.txtConfigSavePath.Name = "txtConfigSavePath";
            this.txtConfigSavePath.Size = new System.Drawing.Size(435, 25);
            this.txtConfigSavePath.TabIndex = 23;
            // 
            // btnSelectConfigSavePath
            // 
            this.btnSelectConfigSavePath.BackgroundImage = global::HyEye.WForm.Properties.Resources.打开文件夹;
            this.btnSelectConfigSavePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectConfigSavePath.Enabled = false;
            this.btnSelectConfigSavePath.Location = new System.Drawing.Point(569, 58);
            this.btnSelectConfigSavePath.Name = "btnSelectConfigSavePath";
            this.btnSelectConfigSavePath.Size = new System.Drawing.Size(35, 26);
            this.btnSelectConfigSavePath.TabIndex = 24;
            this.btnSelectConfigSavePath.UseVisualStyleBackColor = true;
            this.btnSelectConfigSavePath.Click += new System.EventHandler(this.btnSelectConfigSavePath_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(42, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "保存路径：";
            // 
            // ckConfigSavePath
            // 
            this.ckConfigSavePath.AutoSize = true;
            this.ckConfigSavePath.Location = new System.Drawing.Point(40, 31);
            this.ckConfigSavePath.Name = "ckConfigSavePath";
            this.ckConfigSavePath.Size = new System.Drawing.Size(149, 19);
            this.ckConfigSavePath.TabIndex = 1;
            this.ckConfigSavePath.Text = "启用配置自动保存";
            this.ckConfigSavePath.UseVisualStyleBackColor = true;
            this.ckConfigSavePath.CheckedChanged += new System.EventHandler(this.ckConfigSavePath_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(20, 805);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(648, 20);
            this.panel5.TabIndex = 20;
            // 
            // txtConfigSaveType
            // 
            this.txtConfigSaveType.Enabled = false;
            this.txtConfigSaveType.Location = new System.Drawing.Point(406, 24);
            this.txtConfigSaveType.Name = "txtConfigSaveType";
            this.txtConfigSaveType.Size = new System.Drawing.Size(57, 25);
            this.txtConfigSaveType.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(318, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 15);
            this.label17.TabIndex = 26;
            this.label17.Text = "保存周期：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(469, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(52, 15);
            this.label18.TabIndex = 26;
            this.label18.Text = "（天）";
            // 
            // FrmSystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 802);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.FrmSystemSetting_Load);
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.gbBase.ResumeLayout(false);
            this.gbBase.PerformLayout();
            this.gbRunning.ResumeLayout(false);
            this.gbRunning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCmdRTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcqImageTimeout)).EndInit();
            this.gbDataSave.ResumeLayout(false);
            this.gbDataSave.PerformLayout();
            this.gbCommand.ResumeLayout(false);
            this.gbCommand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).EndInit();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.GroupBox gbBase;
        private System.Windows.Forms.TextBox tbSimulationPath;
        private System.Windows.Forms.CheckBox cbDeleteVPP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchSimulationPath;
        private System.Windows.Forms.CheckBox cbAutoStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbRunning;
        private System.Windows.Forms.NumericUpDown nudCmdRTimeout;
        private System.Windows.Forms.NumericUpDown nudAcqImageTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbDataSave;
        private System.Windows.Forms.CheckBox ckbDataSaveEnabled;
        private System.Windows.Forms.TextBox tbDataSavePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSearchDataSavePath;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gbCommand;
        private System.Windows.Forms.NumericUpDown nudDecimalPlaces;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rdbOnlyValue;
        private System.Windows.Forms.RadioButton rdbCmdKeyValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.ComboBox cmbFileLogLevel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox ckbEnableHandCmd;
        private System.Windows.Forms.CheckBox ckbEnableCmdIndex;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rdbtnDataSaveModeAll;
        private System.Windows.Forms.RadioButton rdbtnDataSaveModeLast;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rdbtnSaveExcludeDataBindings;
        private System.Windows.Forms.RadioButton rdbtnSaveAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox ckConfigSavePath;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtConfigSavePath;
        private System.Windows.Forms.Button btnSelectConfigSavePath;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtConfigSaveType;
        private System.Windows.Forms.Label label18;
    }
}