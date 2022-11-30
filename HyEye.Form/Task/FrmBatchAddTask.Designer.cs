namespace HyEye.WForm.Settings
{
    partial class FrmBatchAddTask
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
            this.label3 = new System.Windows.Forms.Label();
            this.nudTaskCount = new System.Windows.Forms.NumericUpDown();
            this.cmbCameras = new System.Windows.Forms.ComboBox();
            this.nudAcqCount = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.cmbVisionType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcqCount)).BeginInit();
            this.btnCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "选择相机：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "每个任务的拍照数量：";
            // 
            // nudTaskCount
            // 
            this.nudTaskCount.Location = new System.Drawing.Point(180, 35);
            this.nudTaskCount.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudTaskCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaskCount.Name = "nudTaskCount";
            this.nudTaskCount.Size = new System.Drawing.Size(120, 25);
            this.nudTaskCount.TabIndex = 1;
            this.nudTaskCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTaskCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cmbCameras
            // 
            this.cmbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameras.FormattingEnabled = true;
            this.cmbCameras.Location = new System.Drawing.Point(179, 136);
            this.cmbCameras.Name = "cmbCameras";
            this.cmbCameras.Size = new System.Drawing.Size(121, 23);
            this.cmbCameras.TabIndex = 3;
            // 
            // nudAcqCount
            // 
            this.nudAcqCount.Location = new System.Drawing.Point(180, 188);
            this.nudAcqCount.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudAcqCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAcqCount.Name = "nudAcqCount";
            this.nudAcqCount.Size = new System.Drawing.Size(120, 25);
            this.nudAcqCount.TabIndex = 4;
            this.nudAcqCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAcqCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.Controls.Add(this.pbCancel);
            this.btnCancel.Location = new System.Drawing.Point(210, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
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
            this.pbCancel.TabIndex = 32;
            this.pbCancel.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.Location = new System.Drawing.Point(98, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 35);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
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
            this.pbOK.TabIndex = 31;
            this.pbOK.TabStop = false;
            // 
            // cmbVisionType
            // 
            this.cmbVisionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVisionType.FormattingEnabled = true;
            this.cmbVisionType.Location = new System.Drawing.Point(179, 86);
            this.cmbVisionType.Name = "cmbVisionType";
            this.cmbVisionType.Size = new System.Drawing.Size(121, 23);
            this.cmbVisionType.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "视觉类型：";
            // 
            // FrmBatchAddTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 337);
            this.Controls.Add(this.cmbVisionType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudAcqCount);
            this.Controls.Add(this.cmbCameras);
            this.Controls.Add(this.nudTaskCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmBatchAddTask";
            this.Text = "批量新建任务";
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcqCount)).EndInit();
            this.btnCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudTaskCount;
        private System.Windows.Forms.ComboBox cmbCameras;
        private System.Windows.Forms.NumericUpDown nudAcqCount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbOK;
        private System.Windows.Forms.ComboBox cmbVisionType;
        private System.Windows.Forms.Label label4;
    }
}