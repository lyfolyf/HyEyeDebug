namespace HyEye.WForm.Settings
{
    partial class FrmOpticalSetting
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
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbLights = new System.Windows.Forms.GroupBox();
            this.pnlLights = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSetAll = new System.Windows.Forms.Button();
            this.btnConnectController = new System.Windows.Forms.Button();
            this.pbConnectController = new System.Windows.Forms.PictureBox();
            this.btnCloseController = new System.Windows.Forms.Button();
            this.pbCloseController = new System.Windows.Forms.PictureBox();
            this.btnSelectLight = new System.Windows.Forms.Button();
            this.pbSelectLight = new System.Windows.Forms.PictureBox();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.cmbPreampGain = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbGain = new System.Windows.Forms.TextBox();
            this.btnTriggerExec = new System.Windows.Forms.Button();
            this.pbTriggerExec = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnContinuous = new System.Windows.Forms.Button();
            this.pbContinuous = new System.Windows.Forms.PictureBox();
            this.btnCameraClose = new System.Windows.Forms.Button();
            this.pbCameraClose = new System.Windows.Forms.PictureBox();
            this.btnSetCameraParams = new System.Windows.Forms.Button();
            this.pbSetCameraParams = new System.Windows.Forms.PictureBox();
            this.tbExposure = new System.Windows.Forms.TextBox();
            this.btnCameraOpen = new System.Windows.Forms.Button();
            this.pbCameraOpen = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.gbLights.SuspendLayout();
            this.panel3.SuspendLayout();
            this.btnConnectController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectController)).BeginInit();
            this.btnCloseController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseController)).BeginInit();
            this.btnSelectLight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectLight)).BeginInit();
            this.gbCamera.SuspendLayout();
            this.btnTriggerExec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTriggerExec)).BeginInit();
            this.btnContinuous.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbContinuous)).BeginInit();
            this.btnCameraClose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraClose)).BeginInit();
            this.btnSetCameraParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSetCameraParams)).BeginInit();
            this.btnCameraOpen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraOpen)).BeginInit();
            this.panel2.SuspendLayout();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(445, 0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(787, 703);
            this.pnlDisplay.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbLights);
            this.panel1.Controls.Add(this.gbCamera);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.panel1.Size = new System.Drawing.Size(445, 703);
            this.panel1.TabIndex = 25;
            // 
            // gbLights
            // 
            this.gbLights.BackColor = System.Drawing.SystemColors.Control;
            this.gbLights.Controls.Add(this.pnlLights);
            this.gbLights.Controls.Add(this.panel3);
            this.gbLights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLights.Location = new System.Drawing.Point(20, 165);
            this.gbLights.Name = "gbLights";
            this.gbLights.Size = new System.Drawing.Size(405, 483);
            this.gbLights.TabIndex = 26;
            this.gbLights.TabStop = false;
            this.gbLights.Text = "光源设置";
            // 
            // pnlLights
            // 
            this.pnlLights.AutoScroll = true;
            this.pnlLights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLights.Location = new System.Drawing.Point(3, 71);
            this.pnlLights.Name = "pnlLights";
            this.pnlLights.Size = new System.Drawing.Size(399, 409);
            this.pnlLights.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSetAll);
            this.panel3.Controls.Add(this.btnConnectController);
            this.panel3.Controls.Add(this.btnCloseController);
            this.panel3.Controls.Add(this.btnSelectLight);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(399, 50);
            this.panel3.TabIndex = 0;
            // 
            // btnSetAll
            // 
            this.btnSetAll.Location = new System.Drawing.Point(215, 8);
            this.btnSetAll.Name = "btnSetAll";
            this.btnSetAll.Size = new System.Drawing.Size(71, 35);
            this.btnSetAll.TabIndex = 14;
            this.btnSetAll.Text = "设置所有";
            this.btnSetAll.UseVisualStyleBackColor = true;
            this.btnSetAll.Click += new System.EventHandler(this.btnSetAll_Click);
            // 
            // btnConnectController
            // 
            this.btnConnectController.Controls.Add(this.pbConnectController);
            this.btnConnectController.Location = new System.Drawing.Point(109, 8);
            this.btnConnectController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConnectController.Name = "btnConnectController";
            this.btnConnectController.Size = new System.Drawing.Size(100, 35);
            this.btnConnectController.TabIndex = 11;
            this.btnConnectController.Text = "连接控制器";
            this.btnConnectController.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConnectController.UseVisualStyleBackColor = true;
            this.btnConnectController.Click += new System.EventHandler(this.btnConnectController_Click);
            // 
            // pbConnectController
            // 
            this.pbConnectController.BackColor = System.Drawing.Color.Transparent;
            this.pbConnectController.BackgroundImage = global::HyEye.WForm.Properties.Resources.连接控制器;
            this.pbConnectController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbConnectController.Location = new System.Drawing.Point(3, 3);
            this.pbConnectController.Name = "pbConnectController";
            this.pbConnectController.Size = new System.Drawing.Size(29, 29);
            this.pbConnectController.TabIndex = 30;
            this.pbConnectController.TabStop = false;
            // 
            // btnCloseController
            // 
            this.btnCloseController.Controls.Add(this.pbCloseController);
            this.btnCloseController.Location = new System.Drawing.Point(292, 8);
            this.btnCloseController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCloseController.Name = "btnCloseController";
            this.btnCloseController.Size = new System.Drawing.Size(100, 35);
            this.btnCloseController.TabIndex = 12;
            this.btnCloseController.Text = "关闭控制器";
            this.btnCloseController.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCloseController.UseVisualStyleBackColor = true;
            this.btnCloseController.Click += new System.EventHandler(this.btnCloseController_Click);
            // 
            // pbCloseController
            // 
            this.pbCloseController.BackColor = System.Drawing.Color.Transparent;
            this.pbCloseController.BackgroundImage = global::HyEye.WForm.Properties.Resources.关闭控制器;
            this.pbCloseController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCloseController.Location = new System.Drawing.Point(3, 3);
            this.pbCloseController.Name = "pbCloseController";
            this.pbCloseController.Size = new System.Drawing.Size(29, 29);
            this.pbCloseController.TabIndex = 29;
            this.pbCloseController.TabStop = false;
            // 
            // btnSelectLight
            // 
            this.btnSelectLight.Controls.Add(this.pbSelectLight);
            this.btnSelectLight.Location = new System.Drawing.Point(3, 8);
            this.btnSelectLight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectLight.Name = "btnSelectLight";
            this.btnSelectLight.Size = new System.Drawing.Size(100, 35);
            this.btnSelectLight.TabIndex = 13;
            this.btnSelectLight.Text = "选择光源";
            this.btnSelectLight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectLight.UseVisualStyleBackColor = true;
            this.btnSelectLight.Click += new System.EventHandler(this.btnSelectLight_Click);
            // 
            // pbSelectLight
            // 
            this.pbSelectLight.BackColor = System.Drawing.Color.Transparent;
            this.pbSelectLight.BackgroundImage = global::HyEye.WForm.Properties.Resources.选择光源;
            this.pbSelectLight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSelectLight.Location = new System.Drawing.Point(3, 3);
            this.pbSelectLight.Name = "pbSelectLight";
            this.pbSelectLight.Size = new System.Drawing.Size(29, 29);
            this.pbSelectLight.TabIndex = 29;
            this.pbSelectLight.TabStop = false;
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.cmbPreampGain);
            this.gbCamera.Controls.Add(this.label1);
            this.gbCamera.Controls.Add(this.label2);
            this.gbCamera.Controls.Add(this.tbGain);
            this.gbCamera.Controls.Add(this.btnTriggerExec);
            this.gbCamera.Controls.Add(this.label3);
            this.gbCamera.Controls.Add(this.btnContinuous);
            this.gbCamera.Controls.Add(this.btnCameraClose);
            this.gbCamera.Controls.Add(this.btnSetCameraParams);
            this.gbCamera.Controls.Add(this.tbExposure);
            this.gbCamera.Controls.Add(this.btnCameraOpen);
            this.gbCamera.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCamera.Location = new System.Drawing.Point(20, 20);
            this.gbCamera.Margin = new System.Windows.Forms.Padding(4);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Padding = new System.Windows.Forms.Padding(4);
            this.gbCamera.Size = new System.Drawing.Size(405, 145);
            this.gbCamera.TabIndex = 19;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "相机设置";
            // 
            // cmbPreampGain
            // 
            this.cmbPreampGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreampGain.FormattingEnabled = true;
            this.cmbPreampGain.Location = new System.Drawing.Point(279, 83);
            this.cmbPreampGain.Name = "cmbPreampGain";
            this.cmbPreampGain.Size = new System.Drawing.Size(97, 23);
            this.cmbPreampGain.TabIndex = 54;
            this.cmbPreampGain.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "(us)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "曝光：";
            // 
            // tbGain
            // 
            this.tbGain.Location = new System.Drawing.Point(279, 83);
            this.tbGain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbGain.Name = "tbGain";
            this.tbGain.Size = new System.Drawing.Size(97, 25);
            this.tbGain.TabIndex = 8;
            // 
            // btnTriggerExec
            // 
            this.btnTriggerExec.Controls.Add(this.pbTriggerExec);
            this.btnTriggerExec.Enabled = false;
            this.btnTriggerExec.Location = new System.Drawing.Point(230, 55);
            this.btnTriggerExec.Margin = new System.Windows.Forms.Padding(4);
            this.btnTriggerExec.Name = "btnTriggerExec";
            this.btnTriggerExec.Size = new System.Drawing.Size(109, 23);
            this.btnTriggerExec.TabIndex = 10;
            this.btnTriggerExec.Text = "软触发一次";
            this.btnTriggerExec.UseVisualStyleBackColor = true;
            this.btnTriggerExec.Click += new System.EventHandler(this.btnTriggerExec_Click);
            // 
            // pbTriggerExec
            // 
            this.pbTriggerExec.BackColor = System.Drawing.Color.Transparent;
            this.pbTriggerExec.BackgroundImage = global::HyEye.WForm.Properties.Resources.单次拍照;
            this.pbTriggerExec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbTriggerExec.Location = new System.Drawing.Point(3, 3);
            this.pbTriggerExec.Name = "pbTriggerExec";
            this.pbTriggerExec.Size = new System.Drawing.Size(20, 20);
            this.pbTriggerExec.TabIndex = 30;
            this.pbTriggerExec.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "增益：";
            // 
            // btnContinuous
            // 
            this.btnContinuous.Controls.Add(this.pbContinuous);
            this.btnContinuous.Enabled = false;
            this.btnContinuous.Location = new System.Drawing.Point(21, 55);
            this.btnContinuous.Margin = new System.Windows.Forms.Padding(4);
            this.btnContinuous.Name = "btnContinuous";
            this.btnContinuous.Size = new System.Drawing.Size(109, 23);
            this.btnContinuous.TabIndex = 9;
            this.btnContinuous.Text = "实时";
            this.btnContinuous.UseVisualStyleBackColor = true;
            this.btnContinuous.Click += new System.EventHandler(this.btnContinuous_Click);
            // 
            // pbContinuous
            // 
            this.pbContinuous.BackColor = System.Drawing.Color.Transparent;
            this.pbContinuous.BackgroundImage = global::HyEye.WForm.Properties.Resources.实时取相;
            this.pbContinuous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbContinuous.Location = new System.Drawing.Point(3, 3);
            this.pbContinuous.Name = "pbContinuous";
            this.pbContinuous.Size = new System.Drawing.Size(20, 20);
            this.pbContinuous.TabIndex = 29;
            this.pbContinuous.TabStop = false;
            // 
            // btnCameraClose
            // 
            this.btnCameraClose.Controls.Add(this.pbCameraClose);
            this.btnCameraClose.Enabled = false;
            this.btnCameraClose.Location = new System.Drawing.Point(230, 25);
            this.btnCameraClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnCameraClose.Name = "btnCameraClose";
            this.btnCameraClose.Size = new System.Drawing.Size(109, 23);
            this.btnCameraClose.TabIndex = 2;
            this.btnCameraClose.Text = "关闭相机";
            this.btnCameraClose.UseVisualStyleBackColor = true;
            this.btnCameraClose.Click += new System.EventHandler(this.btnCameraClose_Click);
            // 
            // pbCameraClose
            // 
            this.pbCameraClose.BackColor = System.Drawing.Color.Transparent;
            this.pbCameraClose.BackgroundImage = global::HyEye.WForm.Properties.Resources.关闭相机;
            this.pbCameraClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCameraClose.Location = new System.Drawing.Point(3, 3);
            this.pbCameraClose.Name = "pbCameraClose";
            this.pbCameraClose.Size = new System.Drawing.Size(20, 20);
            this.pbCameraClose.TabIndex = 30;
            this.pbCameraClose.TabStop = false;
            // 
            // btnSetCameraParams
            // 
            this.btnSetCameraParams.Controls.Add(this.pbSetCameraParams);
            this.btnSetCameraParams.Enabled = false;
            this.btnSetCameraParams.Location = new System.Drawing.Point(230, 112);
            this.btnSetCameraParams.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetCameraParams.Name = "btnSetCameraParams";
            this.btnSetCameraParams.Size = new System.Drawing.Size(109, 23);
            this.btnSetCameraParams.TabIndex = 10;
            this.btnSetCameraParams.Text = "设置参数";
            this.btnSetCameraParams.UseVisualStyleBackColor = true;
            this.btnSetCameraParams.Click += new System.EventHandler(this.btnSetCameraParams_Click);
            // 
            // pbSetCameraParams
            // 
            this.pbSetCameraParams.BackColor = System.Drawing.Color.Transparent;
            this.pbSetCameraParams.BackgroundImage = global::HyEye.WForm.Properties.Resources.设置参数;
            this.pbSetCameraParams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSetCameraParams.Location = new System.Drawing.Point(3, 3);
            this.pbSetCameraParams.Name = "pbSetCameraParams";
            this.pbSetCameraParams.Size = new System.Drawing.Size(20, 20);
            this.pbSetCameraParams.TabIndex = 32;
            this.pbSetCameraParams.TabStop = false;
            // 
            // tbExposure
            // 
            this.tbExposure.Location = new System.Drawing.Point(70, 83);
            this.tbExposure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbExposure.Name = "tbExposure";
            this.tbExposure.Size = new System.Drawing.Size(97, 25);
            this.tbExposure.TabIndex = 7;
            // 
            // btnCameraOpen
            // 
            this.btnCameraOpen.Controls.Add(this.pbCameraOpen);
            this.btnCameraOpen.Location = new System.Drawing.Point(21, 25);
            this.btnCameraOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnCameraOpen.Name = "btnCameraOpen";
            this.btnCameraOpen.Size = new System.Drawing.Size(109, 23);
            this.btnCameraOpen.TabIndex = 1;
            this.btnCameraOpen.Text = "打开相机";
            this.btnCameraOpen.UseVisualStyleBackColor = true;
            this.btnCameraOpen.Click += new System.EventHandler(this.btnCameraOpen_Click);
            // 
            // pbCameraOpen
            // 
            this.pbCameraOpen.BackColor = System.Drawing.Color.Transparent;
            this.pbCameraOpen.BackgroundImage = global::HyEye.WForm.Properties.Resources.打开相机;
            this.pbCameraOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCameraOpen.Location = new System.Drawing.Point(3, 3);
            this.pbCameraOpen.Name = "pbCameraOpen";
            this.pbCameraOpen.Size = new System.Drawing.Size(20, 20);
            this.pbCameraOpen.TabIndex = 29;
            this.pbCameraOpen.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 648);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(405, 55);
            this.panel2.TabIndex = 29;
            // 
            // btnSave
            // 
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(10, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(385, 35);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "保存光学设置";
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
            this.pbSave.TabIndex = 30;
            this.pbSave.TabStop = false;
            // 
            // FrmOpticalSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1232, 703);
            this.Controls.Add(this.pnlDisplay);
            this.Controls.Add(this.panel1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmOpticalSetting";
            this.ShowIcon = false;
            this.Text = "光学设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOpticalSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmOpticalSetting_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmOpticalSetting_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.gbLights.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.btnConnectController.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectController)).EndInit();
            this.btnCloseController.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseController)).EndInit();
            this.btnSelectLight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectLight)).EndInit();
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
            this.btnTriggerExec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTriggerExec)).EndInit();
            this.btnContinuous.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbContinuous)).EndInit();
            this.btnCameraClose.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraClose)).EndInit();
            this.btnSetCameraParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSetCameraParams)).EndInit();
            this.btnCameraOpen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCameraOpen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbCamera;
        private System.Windows.Forms.Button btnCameraClose;
        private System.Windows.Forms.Button btnCameraOpen;
        private System.Windows.Forms.Button btnSetCameraParams;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbExposure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.Button btnTriggerExec;
        private System.Windows.Forms.Button btnContinuous;
        private System.Windows.Forms.GroupBox gbLights;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnConnectController;
        private System.Windows.Forms.Button btnCloseController;
        private System.Windows.Forms.Panel pnlLights;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Button btnSelectLight;
        private System.Windows.Forms.PictureBox pbSetCameraParams;
        private System.Windows.Forms.PictureBox pbTriggerExec;
        private System.Windows.Forms.PictureBox pbCameraClose;
        private System.Windows.Forms.PictureBox pbContinuous;
        private System.Windows.Forms.PictureBox pbCameraOpen;
        private System.Windows.Forms.PictureBox pbSelectLight;
        private System.Windows.Forms.PictureBox pbConnectController;
        private System.Windows.Forms.PictureBox pbCloseController;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPreampGain;
        private System.Windows.Forms.Button btnSetAll;
    }
}