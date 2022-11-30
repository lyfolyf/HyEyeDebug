namespace HyEye.WForm.Settings
{
    partial class FrmCommunicationSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.rdoServer = new System.Windows.Forms.RadioButton();
            this.rdoClient = new System.Windows.Forms.RadioButton();
            this.pnlTcp = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdbtnTcp = new System.Windows.Forms.RadioButton();
            this.rdbtnPLC = new System.Windows.Forms.RadioButton();
            this.gbTCP = new System.Windows.Forms.GroupBox();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.btnCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            this.pnlTcp.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.btnDisConnect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbTCP.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置 IP：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "监听端口：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(116, 71);
            this.tbServerIP.MaxLength = 15;
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(170, 25);
            this.tbServerIP.TabIndex = 3;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(116, 121);
            this.tbPort.MaxLength = 5;
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(170, 25);
            this.tbPort.TabIndex = 4;
            this.tbPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPort_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.Location = new System.Drawing.Point(54, 353);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 35);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定  ";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.pbOK.TabIndex = 27;
            this.pbOK.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Controls.Add(this.pbCancel);
            this.btnCancel.Location = new System.Drawing.Point(186, 353);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消  ";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.pbCancel.TabIndex = 28;
            this.pbCancel.TabStop = false;
            // 
            // rdoServer
            // 
            this.rdoServer.AutoSize = true;
            this.rdoServer.Checked = true;
            this.rdoServer.Location = new System.Drawing.Point(28, 28);
            this.rdoServer.Name = "rdoServer";
            this.rdoServer.Size = new System.Drawing.Size(128, 19);
            this.rdoServer.TabIndex = 1;
            this.rdoServer.TabStop = true;
            this.rdoServer.Text = "HyInspect是服务端";
            this.rdoServer.UseVisualStyleBackColor = true;
            this.rdoServer.CheckedChanged += new System.EventHandler(this.rdoServer_CheckedChanged);
            // 
            // rdoClient
            // 
            this.rdoClient.AutoSize = true;
            this.rdoClient.Location = new System.Drawing.Point(158, 28);
            this.rdoClient.Name = "rdoClient";
            this.rdoClient.Size = new System.Drawing.Size(128, 19);
            this.rdoClient.TabIndex = 2;
            this.rdoClient.Text = "HyInspect是客户端";
            this.rdoClient.UseVisualStyleBackColor = true;
            this.rdoClient.CheckedChanged += new System.EventHandler(this.rdoClient_CheckedChanged);
            // 
            // pnlTcp
            // 
            this.pnlTcp.Controls.Add(this.tbServerIP);
            this.pnlTcp.Controls.Add(this.rdoClient);
            this.pnlTcp.Controls.Add(this.rdoServer);
            this.pnlTcp.Controls.Add(this.tbPort);
            this.pnlTcp.Controls.Add(this.label2);
            this.pnlTcp.Controls.Add(this.label1);
            this.pnlTcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTcp.Location = new System.Drawing.Point(3, 21);
            this.pnlTcp.Name = "pnlTcp";
            this.pnlTcp.Size = new System.Drawing.Size(316, 183);
            this.pnlTcp.TabIndex = 8;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnDisConnect);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(3, 204);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(316, 55);
            this.pnlBottom.TabIndex = 10;
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Controls.Add(this.pictureBox1);
            this.btnDisConnect.Location = new System.Drawing.Point(39, 8);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(225, 35);
            this.btnDisConnect.TabIndex = 8;
            this.btnDisConnect.Text = "断开连接";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::HyEye.WForm.Properties.Resources.关闭控制器;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 29);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // rdbtnTcp
            // 
            this.rdbtnTcp.AutoSize = true;
            this.rdbtnTcp.Location = new System.Drawing.Point(43, 28);
            this.rdbtnTcp.Name = "rdbtnTcp";
            this.rdbtnTcp.Size = new System.Drawing.Size(90, 19);
            this.rdbtnTcp.TabIndex = 11;
            this.rdbtnTcp.TabStop = true;
            this.rdbtnTcp.Text = "TCP 通讯";
            this.rdbtnTcp.UseVisualStyleBackColor = true;
            this.rdbtnTcp.CheckedChanged += new System.EventHandler(this.rdbtnTcp_CheckedChanged);
            // 
            // rdbtnPLC
            // 
            this.rdbtnPLC.AutoSize = true;
            this.rdbtnPLC.Location = new System.Drawing.Point(173, 28);
            this.rdbtnPLC.Name = "rdbtnPLC";
            this.rdbtnPLC.Size = new System.Drawing.Size(90, 19);
            this.rdbtnPLC.TabIndex = 12;
            this.rdbtnPLC.TabStop = true;
            this.rdbtnPLC.Text = "PLC 通讯";
            this.rdbtnPLC.UseVisualStyleBackColor = true;
            this.rdbtnPLC.CheckedChanged += new System.EventHandler(this.rdbtnPLC_CheckedChanged);
            // 
            // gbTCP
            // 
            this.gbTCP.Controls.Add(this.pnlTcp);
            this.gbTCP.Controls.Add(this.pnlBottom);
            this.gbTCP.Location = new System.Drawing.Point(12, 68);
            this.gbTCP.Name = "gbTCP";
            this.gbTCP.Size = new System.Drawing.Size(322, 262);
            this.gbTCP.TabIndex = 13;
            this.gbTCP.TabStop = false;
            this.gbTCP.Text = "TCP 通讯";
            // 
            // FrmCommunicationSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(347, 412);
            this.Controls.Add(this.rdbtnPLC);
            this.Controls.Add(this.gbTCP);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rdbtnTcp);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCommunicationSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯设置";
            this.Load += new System.EventHandler(this.FrmCommunicationSetting_Load);
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.btnCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            this.pnlTcp.ResumeLayout(false);
            this.pnlTcp.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.btnDisConnect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbTCP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdoServer;
        private System.Windows.Forms.RadioButton rdoClient;
        private System.Windows.Forms.PictureBox pbOK;
        private System.Windows.Forms.PictureBox pbCancel;
        private System.Windows.Forms.Panel pnlTcp;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton rdbtnTcp;
        private System.Windows.Forms.RadioButton rdbtnPLC;
        private System.Windows.Forms.GroupBox gbTCP;
    }
}