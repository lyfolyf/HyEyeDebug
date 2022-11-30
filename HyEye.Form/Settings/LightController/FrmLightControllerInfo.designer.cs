namespace HyEye.WForm.Settings
{
    partial class FrmLightControllerInfo
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
            this.gbCOM = new System.Windows.Forms.GroupBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudChannelCount = new System.Windows.Forms.NumericUpDown();
            this.tbLightControllerName = new System.Windows.Forms.TextBox();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.cmbControllerBrand = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gbTCP = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbCOM.SuspendLayout();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelCount)).BeginInit();
            this.gbTCP.SuspendLayout();
            this.btnCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCOM
            // 
            this.gbCOM.Controls.Add(this.cmbStopBits);
            this.gbCOM.Controls.Add(this.label3);
            this.gbCOM.Controls.Add(this.cmbParity);
            this.gbCOM.Controls.Add(this.label4);
            this.gbCOM.Controls.Add(this.cmbDataBits);
            this.gbCOM.Controls.Add(this.label5);
            this.gbCOM.Controls.Add(this.cmbBaudRate);
            this.gbCOM.Controls.Add(this.label6);
            this.gbCOM.Controls.Add(this.cmbCom);
            this.gbCOM.Controls.Add(this.label7);
            this.gbCOM.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCOM.Location = new System.Drawing.Point(12, 240);
            this.gbCOM.Name = "gbCOM";
            this.gbCOM.Size = new System.Drawing.Size(299, 240);
            this.gbCOM.TabIndex = 3;
            this.gbCOM.TabStop = false;
            this.gbCOM.Text = "通讯参数";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(117, 192);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(134, 20);
            this.cmbStopBits.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "端口号：";
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(117, 152);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(134, 20);
            this.cmbParity.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "波特率：";
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Location = new System.Drawing.Point(117, 112);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(134, 20);
            this.cmbDataBits.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "数据位：";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(117, 72);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(134, 20);
            this.cmbBaudRate.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "校验位：";
            // 
            // cmbCom
            // 
            this.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(117, 32);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(134, 20);
            this.cmbCom.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "停止位：";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(116, 71);
            this.tbPort.MaxLength = 5;
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(134, 21);
            this.tbPort.TabIndex = 6;
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(116, 32);
            this.tbIP.MaxLength = 15;
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(134, 21);
            this.tbIP.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址：";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.Location = new System.Drawing.Point(34, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 35);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pbOK
            // 
            this.pbOK.BackColor = System.Drawing.Color.Transparent;
            this.pbOK.BackgroundImage = global::HyEye.WForm.Properties.Resources.确定;
            this.pbOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOK.Location = new System.Drawing.Point(3, 3);
            this.pbOK.Name = "pbOK";
            this.pbOK.Size = new System.Drawing.Size(29, 29);
            this.pbOK.TabIndex = 33;
            this.pbOK.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "设备品牌：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "协议类型：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "设备名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudChannelCount);
            this.groupBox1.Controls.Add(this.tbLightControllerName);
            this.groupBox1.Controls.Add(this.cmbProtocol);
            this.groupBox1.Controls.Add(this.cmbControllerBrand);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 200);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本参数";
            // 
            // nudChannelCount
            // 
            this.nudChannelCount.Location = new System.Drawing.Point(116, 152);
            this.nudChannelCount.Maximum = new decimal(new int[] {
            56,
            0,
            0,
            0});
            this.nudChannelCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChannelCount.Name = "nudChannelCount";
            this.nudChannelCount.Size = new System.Drawing.Size(133, 21);
            this.nudChannelCount.TabIndex = 4;
            this.nudChannelCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudChannelCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudChannelCount.ValueChanged += new System.EventHandler(this.nudChannelCount_ValueChanged);
            // 
            // tbLightControllerName
            // 
            this.tbLightControllerName.Location = new System.Drawing.Point(116, 112);
            this.tbLightControllerName.MaxLength = 30;
            this.tbLightControllerName.Name = "tbLightControllerName";
            this.tbLightControllerName.Size = new System.Drawing.Size(134, 21);
            this.tbLightControllerName.TabIndex = 3;
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Location = new System.Drawing.Point(116, 72);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(134, 20);
            this.cmbProtocol.TabIndex = 2;
            this.cmbProtocol.SelectedIndexChanged += new System.EventHandler(this.cmbProtocol_SelectedIndexChanged);
            // 
            // cmbControllerBrand
            // 
            this.cmbControllerBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControllerBrand.FormattingEnabled = true;
            this.cmbControllerBrand.Location = new System.Drawing.Point(116, 32);
            this.cmbControllerBrand.Name = "cmbControllerBrand";
            this.cmbControllerBrand.Size = new System.Drawing.Size(134, 20);
            this.cmbControllerBrand.TabIndex = 1;
            this.cmbControllerBrand.SelectedIndexChanged += new System.EventHandler(this.cmbLightControllerBrand_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "通道数量：";
            // 
            // gbTCP
            // 
            this.gbTCP.Controls.Add(this.tbPort);
            this.gbTCP.Controls.Add(this.tbIP);
            this.gbTCP.Controls.Add(this.label1);
            this.gbTCP.Controls.Add(this.label2);
            this.gbTCP.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTCP.Location = new System.Drawing.Point(12, 480);
            this.gbTCP.Name = "gbTCP";
            this.gbTCP.Size = new System.Drawing.Size(299, 120);
            this.gbTCP.TabIndex = 10;
            this.gbTCP.TabStop = false;
            this.gbTCP.Text = "通讯参数";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Controls.Add(this.pbCancel);
            this.btnCancel.Location = new System.Drawing.Point(159, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.BackColor = System.Drawing.Color.Transparent;
            this.pbCancel.BackgroundImage = global::HyEye.WForm.Properties.Resources.取消;
            this.pbCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCancel.Location = new System.Drawing.Point(3, 3);
            this.pbCancel.Name = "pbCancel";
            this.pbCancel.Size = new System.Drawing.Size(29, 29);
            this.pbCancel.TabIndex = 34;
            this.pbCancel.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(12, 621);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(299, 60);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(12, 220);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(299, 20);
            this.panel3.TabIndex = 15;
            // 
            // FrmLightControllerInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(323, 681);
            this.Controls.Add(this.gbTCP);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbCOM);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLightControllerInfo";
            this.Padding = new System.Windows.Forms.Padding(12, 20, 12, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "光源控制器";
            this.Load += new System.EventHandler(this.FrmLightControllerSetting_Load);
            this.gbCOM.ResumeLayout(false);
            this.gbCOM.PerformLayout();
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelCount)).EndInit();
            this.gbTCP.ResumeLayout(false);
            this.gbTCP.PerformLayout();
            this.btnCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbCOM;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLightControllerName;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.ComboBox cmbControllerBrand;
        private System.Windows.Forms.GroupBox gbTCP;
        private System.Windows.Forms.NumericUpDown nudChannelCount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pbOK;
        private System.Windows.Forms.PictureBox pbCancel;
    }
}