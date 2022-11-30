namespace HyEye.WForm.Calibration
{
    partial class FrmCheckerboardVerify
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
            this.pnlToolBlock = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAcqImage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlMeasure = new System.Windows.Forms.Panel();
            this.tbMeasureValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.nudMeasureTolerance = new System.Windows.Forms.NumericUpDown();
            this.ckbMeasure = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudMeasureTheoreticalValue = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlAspect = new System.Windows.Forms.Panel();
            this.tbAspectActualValue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nudAspectTolerance = new System.Windows.Forms.NumericUpDown();
            this.ckbAspect = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudAspectTheoreticalValue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlRMS = new System.Windows.Forms.Panel();
            this.tbRmsActualValue = new System.Windows.Forms.TextBox();
            this.nudRmsTolerance = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.ckbRMS = new System.Windows.Forms.CheckBox();
            this.nudRmsTheoreticalValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbAcqImage = new System.Windows.Forms.PictureBox();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.pbVerify = new System.Windows.Forms.PictureBox();
            this.btnSave.SuspendLayout();
            this.panel1.SuspendLayout();
            this.btnAcqImage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlMeasure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeasureTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeasureTheoreticalValue)).BeginInit();
            this.pnlAspect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAspectTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAspectTheoreticalValue)).BeginInit();
            this.pnlRMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRmsTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRmsTheoreticalValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAcqImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVerify)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolBlock
            // 
            this.pnlToolBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolBlock.Enabled = false;
            this.pnlToolBlock.Location = new System.Drawing.Point(0, 235);
            this.pnlToolBlock.Name = "pnlToolBlock";
            this.pnlToolBlock.Size = new System.Drawing.Size(1222, 558);
            this.pnlToolBlock.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(825, 102);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Controls.Add(this.pbVerify);
            this.btnVerify.Location = new System.Drawing.Point(825, 51);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(150, 35);
            this.btnVerify.TabIndex = 0;
            this.btnVerify.Text = "验证";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAcqImage);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnVerify);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20);
            this.panel1.Size = new System.Drawing.Size(1222, 235);
            this.panel1.TabIndex = 5;
            // 
            // btnAcqImage
            // 
            this.btnAcqImage.Controls.Add(this.pbAcqImage);
            this.btnAcqImage.Location = new System.Drawing.Point(825, 152);
            this.btnAcqImage.Margin = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.btnAcqImage.Name = "btnAcqImage";
            this.btnAcqImage.Size = new System.Drawing.Size(150, 35);
            this.btnAcqImage.TabIndex = 2;
            this.btnAcqImage.Text = "取像";
            this.btnAcqImage.UseVisualStyleBackColor = true;
            this.btnAcqImage.Click += new System.EventHandler(this.btnAcqImage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlMeasure);
            this.groupBox1.Controls.Add(this.pnlAspect);
            this.groupBox1.Controls.Add(this.pnlRMS);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(20, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "验证条件";
            // 
            // pnlMeasure
            // 
            this.pnlMeasure.Controls.Add(this.tbMeasureValue);
            this.pnlMeasure.Controls.Add(this.label11);
            this.pnlMeasure.Controls.Add(this.nudMeasureTolerance);
            this.pnlMeasure.Controls.Add(this.ckbMeasure);
            this.pnlMeasure.Controls.Add(this.label6);
            this.pnlMeasure.Controls.Add(this.label7);
            this.pnlMeasure.Controls.Add(this.nudMeasureTheoreticalValue);
            this.pnlMeasure.Controls.Add(this.label8);
            this.pnlMeasure.Location = new System.Drawing.Point(30, 125);
            this.pnlMeasure.Name = "pnlMeasure";
            this.pnlMeasure.Size = new System.Drawing.Size(650, 50);
            this.pnlMeasure.TabIndex = 0;
            // 
            // tbMeasureValue
            // 
            this.tbMeasureValue.Enabled = false;
            this.tbMeasureValue.Location = new System.Drawing.Point(550, 13);
            this.tbMeasureValue.Name = "tbMeasureValue";
            this.tbMeasureValue.Size = new System.Drawing.Size(70, 25);
            this.tbMeasureValue.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(480, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "实际值：";
            // 
            // nudMeasureTolerance
            // 
            this.nudMeasureTolerance.DecimalPlaces = 2;
            this.nudMeasureTolerance.Enabled = false;
            this.nudMeasureTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMeasureTolerance.Location = new System.Drawing.Point(380, 13);
            this.nudMeasureTolerance.Name = "nudMeasureTolerance";
            this.nudMeasureTolerance.Size = new System.Drawing.Size(60, 25);
            this.nudMeasureTolerance.TabIndex = 8;
            // 
            // ckbMeasure
            // 
            this.ckbMeasure.AutoSize = true;
            this.ckbMeasure.Location = new System.Drawing.Point(20, 15);
            this.ckbMeasure.Name = "ckbMeasure";
            this.ckbMeasure.Size = new System.Drawing.Size(104, 19);
            this.ckbMeasure.TabIndex = 2;
            this.ckbMeasure.Text = "测量标准品";
            this.ckbMeasure.UseVisualStyleBackColor = true;
            this.ckbMeasure.CheckedChanged += new System.EventHandler(this.ckbMeasure_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "理论值：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 22;
            this.label7.Text = "公差：";
            // 
            // nudMeasureTheoreticalValue
            // 
            this.nudMeasureTheoreticalValue.DecimalPlaces = 2;
            this.nudMeasureTheoreticalValue.Enabled = false;
            this.nudMeasureTheoreticalValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMeasureTheoreticalValue.Location = new System.Drawing.Point(210, 13);
            this.nudMeasureTheoreticalValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMeasureTheoreticalValue.Name = "nudMeasureTheoreticalValue";
            this.nudMeasureTheoreticalValue.Size = new System.Drawing.Size(60, 25);
            this.nudMeasureTheoreticalValue.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(357, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "±";
            // 
            // pnlAspect
            // 
            this.pnlAspect.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAspect.Controls.Add(this.tbAspectActualValue);
            this.pnlAspect.Controls.Add(this.label10);
            this.pnlAspect.Controls.Add(this.nudAspectTolerance);
            this.pnlAspect.Controls.Add(this.ckbAspect);
            this.pnlAspect.Controls.Add(this.label3);
            this.pnlAspect.Controls.Add(this.nudAspectTheoreticalValue);
            this.pnlAspect.Controls.Add(this.label5);
            this.pnlAspect.Controls.Add(this.label4);
            this.pnlAspect.Location = new System.Drawing.Point(30, 75);
            this.pnlAspect.Name = "pnlAspect";
            this.pnlAspect.Size = new System.Drawing.Size(650, 50);
            this.pnlAspect.TabIndex = 0;
            // 
            // tbAspectActualValue
            // 
            this.tbAspectActualValue.Enabled = false;
            this.tbAspectActualValue.Location = new System.Drawing.Point(550, 13);
            this.tbAspectActualValue.Name = "tbAspectActualValue";
            this.tbAspectActualValue.Size = new System.Drawing.Size(70, 25);
            this.tbAspectActualValue.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(480, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "实际值：";
            // 
            // nudAspectTolerance
            // 
            this.nudAspectTolerance.DecimalPlaces = 2;
            this.nudAspectTolerance.Enabled = false;
            this.nudAspectTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudAspectTolerance.Location = new System.Drawing.Point(380, 13);
            this.nudAspectTolerance.Name = "nudAspectTolerance";
            this.nudAspectTolerance.Size = new System.Drawing.Size(60, 25);
            this.nudAspectTolerance.TabIndex = 6;
            // 
            // ckbAspect
            // 
            this.ckbAspect.AutoSize = true;
            this.ckbAspect.Location = new System.Drawing.Point(20, 15);
            this.ckbAspect.Name = "ckbAspect";
            this.ckbAspect.Size = new System.Drawing.Size(74, 19);
            this.ckbAspect.TabIndex = 1;
            this.ckbAspect.Text = "纵横比";
            this.ckbAspect.UseVisualStyleBackColor = true;
            this.ckbAspect.CheckedChanged += new System.EventHandler(this.ckbAspect_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "±";
            // 
            // nudAspectTheoreticalValue
            // 
            this.nudAspectTheoreticalValue.DecimalPlaces = 2;
            this.nudAspectTheoreticalValue.Enabled = false;
            this.nudAspectTheoreticalValue.Location = new System.Drawing.Point(210, 13);
            this.nudAspectTheoreticalValue.Name = "nudAspectTheoreticalValue";
            this.nudAspectTheoreticalValue.Size = new System.Drawing.Size(60, 25);
            this.nudAspectTheoreticalValue.TabIndex = 5;
            this.nudAspectTheoreticalValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "理论值：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "公差：";
            // 
            // pnlRMS
            // 
            this.pnlRMS.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRMS.Controls.Add(this.tbRmsActualValue);
            this.pnlRMS.Controls.Add(this.nudRmsTolerance);
            this.pnlRMS.Controls.Add(this.label9);
            this.pnlRMS.Controls.Add(this.ckbRMS);
            this.pnlRMS.Controls.Add(this.nudRmsTheoreticalValue);
            this.pnlRMS.Controls.Add(this.label2);
            this.pnlRMS.Controls.Add(this.label1);
            this.pnlRMS.Location = new System.Drawing.Point(30, 25);
            this.pnlRMS.Name = "pnlRMS";
            this.pnlRMS.Size = new System.Drawing.Size(650, 50);
            this.pnlRMS.TabIndex = 0;
            // 
            // tbRmsActualValue
            // 
            this.tbRmsActualValue.Enabled = false;
            this.tbRmsActualValue.Location = new System.Drawing.Point(550, 13);
            this.tbRmsActualValue.Name = "tbRmsActualValue";
            this.tbRmsActualValue.Size = new System.Drawing.Size(70, 25);
            this.tbRmsActualValue.TabIndex = 16;
            // 
            // nudRmsTolerance
            // 
            this.nudRmsTolerance.DecimalPlaces = 2;
            this.nudRmsTolerance.Enabled = false;
            this.nudRmsTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudRmsTolerance.Location = new System.Drawing.Point(380, 13);
            this.nudRmsTolerance.Name = "nudRmsTolerance";
            this.nudRmsTolerance.Size = new System.Drawing.Size(60, 25);
            this.nudRmsTolerance.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(480, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "实际值：";
            // 
            // ckbRMS
            // 
            this.ckbRMS.AutoSize = true;
            this.ckbRMS.Location = new System.Drawing.Point(20, 15);
            this.ckbRMS.Name = "ckbRMS";
            this.ckbRMS.Size = new System.Drawing.Size(53, 19);
            this.ckbRMS.TabIndex = 0;
            this.ckbRMS.Text = "RMS";
            this.ckbRMS.UseVisualStyleBackColor = true;
            this.ckbRMS.CheckedChanged += new System.EventHandler(this.ckbRMS_CheckedChanged);
            // 
            // nudRmsTheoreticalValue
            // 
            this.nudRmsTheoreticalValue.DecimalPlaces = 2;
            this.nudRmsTheoreticalValue.Enabled = false;
            this.nudRmsTheoreticalValue.Location = new System.Drawing.Point(210, 13);
            this.nudRmsTheoreticalValue.Name = "nudRmsTheoreticalValue";
            this.nudRmsTheoreticalValue.Size = new System.Drawing.Size(60, 25);
            this.nudRmsTheoreticalValue.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "理论值：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "公差：";
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
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImage = global::HyEye.WForm.Properties.Resources.保存;
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSave.Location = new System.Drawing.Point(3, 3);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(29, 29);
            this.pbSave.TabIndex = 28;
            this.pbSave.TabStop = false;
            // 
            // pbVerify
            // 
            this.pbVerify.BackColor = System.Drawing.Color.Transparent;
            this.pbVerify.BackgroundImage = global::HyEye.WForm.Properties.Resources.验证;
            this.pbVerify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbVerify.Location = new System.Drawing.Point(3, 3);
            this.pbVerify.Name = "pbVerify";
            this.pbVerify.Size = new System.Drawing.Size(29, 29);
            this.pbVerify.TabIndex = 31;
            this.pbVerify.TabStop = false;
            // 
            // FrmCheckerboardVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 793);
            this.Controls.Add(this.pnlToolBlock);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCheckerboardVerify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCheckerboardVerify";
            this.Load += new System.EventHandler(this.FrmCheckerboardVerify_Load);
            this.btnSave.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.btnAcqImage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.pnlMeasure.ResumeLayout(false);
            this.pnlMeasure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeasureTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeasureTheoreticalValue)).EndInit();
            this.pnlAspect.ResumeLayout(false);
            this.pnlAspect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAspectTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAspectTheoreticalValue)).EndInit();
            this.pnlRMS.ResumeLayout(false);
            this.pnlRMS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRmsTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRmsTheoreticalValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAcqImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVerify)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolBlock;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbMeasure;
        private System.Windows.Forms.CheckBox ckbAspect;
        private System.Windows.Forms.CheckBox ckbRMS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudAspectTheoreticalValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRmsTheoreticalValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudMeasureTheoreticalValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlRMS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlAspect;
        private System.Windows.Forms.Panel pnlMeasure;
        private System.Windows.Forms.NumericUpDown nudMeasureTolerance;
        private System.Windows.Forms.NumericUpDown nudAspectTolerance;
        private System.Windows.Forms.NumericUpDown nudRmsTolerance;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRmsActualValue;
        private System.Windows.Forms.TextBox tbMeasureValue;
        private System.Windows.Forms.TextBox tbAspectActualValue;
        private System.Windows.Forms.Button btnAcqImage;
        private System.Windows.Forms.PictureBox pbAcqImage;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbVerify;
    }
}