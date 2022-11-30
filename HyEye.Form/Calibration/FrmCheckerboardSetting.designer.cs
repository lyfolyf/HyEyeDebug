namespace HyEye.WForm.Calibration
{
    partial class FrmCheckerboardSetting
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
            this.btnAutoCalibration = new System.Windows.Forms.Button();
            this.pbAutoCalibration = new System.Windows.Forms.PictureBox();
            this.btnOpticalSetting = new System.Windows.Forms.Button();
            this.pbOpticalSetting = new System.Windows.Forms.PictureBox();
            this.pnlVisionControl = new System.Windows.Forms.Panel();
            this.btnAcqImage = new System.Windows.Forms.Button();
            this.pbAcqImage = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnVerify = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.pbStop = new System.Windows.Forms.PictureBox();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.btnAutoCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAutoCalibration)).BeginInit();
            this.btnOpticalSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpticalSetting)).BeginInit();
            this.btnAcqImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAcqImage)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.btnVerify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.btnStop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStop)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(1050, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存标定";
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
            this.pbSave.TabIndex = 27;
            this.pbSave.TabStop = false;
            // 
            // btnAutoCalibration
            // 
            this.btnAutoCalibration.Controls.Add(this.pbAutoCalibration);
            this.btnAutoCalibration.Location = new System.Drawing.Point(370, 15);
            this.btnAutoCalibration.Margin = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.btnAutoCalibration.Name = "btnAutoCalibration";
            this.btnAutoCalibration.Size = new System.Drawing.Size(150, 35);
            this.btnAutoCalibration.TabIndex = 3;
            this.btnAutoCalibration.Text = "自动标定";
            this.btnAutoCalibration.UseVisualStyleBackColor = true;
            this.btnAutoCalibration.Click += new System.EventHandler(this.btnAutoCalibration_Click);
            // 
            // pbAutoCalibration
            // 
            this.pbAutoCalibration.BackColor = System.Drawing.Color.Transparent;
            this.pbAutoCalibration.BackgroundImage = global::HyEye.WForm.Properties.Resources.开始2;
            this.pbAutoCalibration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAutoCalibration.Location = new System.Drawing.Point(3, 3);
            this.pbAutoCalibration.Name = "pbAutoCalibration";
            this.pbAutoCalibration.Size = new System.Drawing.Size(29, 29);
            this.pbAutoCalibration.TabIndex = 28;
            this.pbAutoCalibration.TabStop = false;
            // 
            // btnOpticalSetting
            // 
            this.btnOpticalSetting.Controls.Add(this.pbOpticalSetting);
            this.btnOpticalSetting.Location = new System.Drawing.Point(195, 15);
            this.btnOpticalSetting.Margin = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.btnOpticalSetting.Name = "btnOpticalSetting";
            this.btnOpticalSetting.Size = new System.Drawing.Size(150, 35);
            this.btnOpticalSetting.TabIndex = 2;
            this.btnOpticalSetting.Text = "光学设置";
            this.btnOpticalSetting.UseVisualStyleBackColor = true;
            this.btnOpticalSetting.Click += new System.EventHandler(this.btnOpticalSetting_Click);
            // 
            // pbOpticalSetting
            // 
            this.pbOpticalSetting.BackColor = System.Drawing.Color.Transparent;
            this.pbOpticalSetting.BackgroundImage = global::HyEye.WForm.Properties.Resources.光学设置;
            this.pbOpticalSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOpticalSetting.Location = new System.Drawing.Point(3, 3);
            this.pbOpticalSetting.Name = "pbOpticalSetting";
            this.pbOpticalSetting.Size = new System.Drawing.Size(29, 29);
            this.pbOpticalSetting.TabIndex = 27;
            this.pbOpticalSetting.TabStop = false;
            // 
            // pnlVisionControl
            // 
            this.pnlVisionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVisionControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlVisionControl.Location = new System.Drawing.Point(0, 65);
            this.pnlVisionControl.Name = "pnlVisionControl";
            this.pnlVisionControl.Size = new System.Drawing.Size(1232, 638);
            this.pnlVisionControl.TabIndex = 18;
            // 
            // btnAcqImage
            // 
            this.btnAcqImage.Controls.Add(this.pbAcqImage);
            this.btnAcqImage.Location = new System.Drawing.Point(25, 15);
            this.btnAcqImage.Margin = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.btnAcqImage.Name = "btnAcqImage";
            this.btnAcqImage.Size = new System.Drawing.Size(150, 35);
            this.btnAcqImage.TabIndex = 1;
            this.btnAcqImage.Text = "取像";
            this.btnAcqImage.UseVisualStyleBackColor = true;
            this.btnAcqImage.Click += new System.EventHandler(this.btnAcqImage_Click);
            // 
            // pbAcqImage
            // 
            this.pbAcqImage.BackColor = System.Drawing.Color.Transparent;
            this.pbAcqImage.BackgroundImage = global::HyEye.WForm.Properties.Resources.单次拍照;
            this.pbAcqImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAcqImage.Location = new System.Drawing.Point(3, 3);
            this.pbAcqImage.Name = "pbAcqImage";
            this.pbAcqImage.Size = new System.Drawing.Size(29, 29);
            this.pbAcqImage.TabIndex = 27;
            this.pbAcqImage.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnVerify);
            this.pnlTop.Controls.Add(this.btnStop);
            this.pnlTop.Controls.Add(this.btnSave);
            this.pnlTop.Controls.Add(this.btnOpticalSetting);
            this.pnlTop.Controls.Add(this.btnAcqImage);
            this.pnlTop.Controls.Add(this.btnAutoCalibration);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1232, 65);
            this.pnlTop.TabIndex = 19;
            // 
            // btnVerify
            // 
            this.btnVerify.Controls.Add(this.pictureBox1);
            this.btnVerify.Location = new System.Drawing.Point(710, 15);
            this.btnVerify.Margin = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(150, 35);
            this.btnVerify.TabIndex = 6;
            this.btnVerify.Text = "标定验证";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::HyEye.WForm.Properties.Resources.保存;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 29);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // btnStop
            // 
            this.btnStop.Controls.Add(this.pbStop);
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(540, 15);
            this.btnStop.Margin = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(150, 35);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pbStop
            // 
            this.pbStop.BackColor = System.Drawing.Color.Transparent;
            this.pbStop.BackgroundImage = global::HyEye.WForm.Properties.Resources.停止1;
            this.pbStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbStop.Location = new System.Drawing.Point(3, 3);
            this.pbStop.Name = "pbStop";
            this.pbStop.Size = new System.Drawing.Size(29, 29);
            this.pbStop.TabIndex = 29;
            this.pbStop.TabStop = false;
            // 
            // FrmCheckerboardSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1232, 703);
            this.Controls.Add(this.pnlVisionControl);
            this.Controls.Add(this.pnlTop);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmCheckerboardSetting";
            this.Text = "FrmCheckerboardSetting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCheckerboardSetting_FormClosed);
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.btnAutoCalibration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAutoCalibration)).EndInit();
            this.btnOpticalSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOpticalSetting)).EndInit();
            this.btnAcqImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAcqImage)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.btnVerify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.btnStop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpticalSetting;
        private System.Windows.Forms.Panel pnlVisionControl;
        private System.Windows.Forms.Button btnAutoCalibration;
        private System.Windows.Forms.Button btnAcqImage;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pbOpticalSetting;
        private System.Windows.Forms.PictureBox pbAcqImage;
        private System.Windows.Forms.PictureBox pbStop;
        private System.Windows.Forms.PictureBox pbAutoCalibration;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}