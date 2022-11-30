namespace HyEye.WForm.Calibration
{
    partial class FrmHandEyeParamSetting
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbEnabledFitCircle = new System.Windows.Forms.CheckBox();
            this.AStep = new System.Windows.Forms.NumericUpDown();
            this.rbtnMultiPointFit = new System.Windows.Forms.RadioButton();
            this.rbtnMultiPointAngleFit = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.APointNum = new System.Windows.Forms.NumericUpDown();
            this.gbParams = new System.Windows.Forms.GroupBox();
            this.YStep = new System.Windows.Forms.NumericUpDown();
            this.XStep = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.YPointNum = new System.Windows.Forms.NumericUpDown();
            this.XPointNum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.APointNum)).BeginInit();
            this.gbParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPointNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPointNum)).BeginInit();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ckbEnabledFitCircle);
            this.groupBox2.Controls.Add(this.AStep);
            this.groupBox2.Controls.Add(this.rbtnMultiPointFit);
            this.groupBox2.Controls.Add(this.rbtnMultiPointAngleFit);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.APointNum);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(20, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 165);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "旋转中心";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(35, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "旋转点数：";
            // 
            // ckbEnabledFitCircle
            // 
            this.ckbEnabledFitCircle.AutoSize = true;
            this.ckbEnabledFitCircle.Checked = true;
            this.ckbEnabledFitCircle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEnabledFitCircle.Location = new System.Drawing.Point(38, 35);
            this.ckbEnabledFitCircle.Name = "ckbEnabledFitCircle";
            this.ckbEnabledFitCircle.Size = new System.Drawing.Size(59, 19);
            this.ckbEnabledFitCircle.TabIndex = 9;
            this.ckbEnabledFitCircle.Text = "启用";
            this.ckbEnabledFitCircle.UseVisualStyleBackColor = true;
            this.ckbEnabledFitCircle.CheckedChanged += new System.EventHandler(this.ckbEnabledFitCircle_CheckedChanged);
            // 
            // AStep
            // 
            this.AStep.DecimalPlaces = 2;
            this.AStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.AStep.Location = new System.Drawing.Point(352, 75);
            this.AStep.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.AStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.AStep.Name = "AStep";
            this.AStep.Size = new System.Drawing.Size(80, 25);
            this.AStep.TabIndex = 6;
            this.AStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AStep.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            // 
            // rbtnMultiPointFit
            // 
            this.rbtnMultiPointFit.AutoSize = true;
            this.rbtnMultiPointFit.Checked = true;
            this.rbtnMultiPointFit.Enabled = false;
            this.rbtnMultiPointFit.Location = new System.Drawing.Point(126, 124);
            this.rbtnMultiPointFit.Name = "rbtnMultiPointFit";
            this.rbtnMultiPointFit.Size = new System.Drawing.Size(88, 19);
            this.rbtnMultiPointFit.TabIndex = 8;
            this.rbtnMultiPointFit.TabStop = true;
            this.rbtnMultiPointFit.Text = "多点拟合";
            this.rbtnMultiPointFit.UseVisualStyleBackColor = true;
            // 
            // rbtnMultiPointAngleFit
            // 
            this.rbtnMultiPointAngleFit.AutoSize = true;
            this.rbtnMultiPointAngleFit.Enabled = false;
            this.rbtnMultiPointAngleFit.Location = new System.Drawing.Point(263, 123);
            this.rbtnMultiPointAngleFit.Name = "rbtnMultiPointAngleFit";
            this.rbtnMultiPointAngleFit.Size = new System.Drawing.Size(118, 19);
            this.rbtnMultiPointAngleFit.TabIndex = 7;
            this.rbtnMultiPointAngleFit.Text = "多点夹角拟合";
            this.rbtnMultiPointAngleFit.UseVisualStyleBackColor = true;
            this.rbtnMultiPointAngleFit.CheckedChanged += new System.EventHandler(this.rbtnMultiPointAngleFit_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "旋转点数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "旋转步长：";
            // 
            // APointNum
            // 
            this.APointNum.Location = new System.Drawing.Point(126, 75);
            this.APointNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.APointNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.APointNum.Name = "APointNum";
            this.APointNum.Size = new System.Drawing.Size(80, 25);
            this.APointNum.TabIndex = 3;
            this.APointNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.APointNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // gbParams
            // 
            this.gbParams.Controls.Add(this.YStep);
            this.gbParams.Controls.Add(this.XStep);
            this.gbParams.Controls.Add(this.label7);
            this.gbParams.Controls.Add(this.label8);
            this.gbParams.Controls.Add(this.YPointNum);
            this.gbParams.Controls.Add(this.XPointNum);
            this.gbParams.Controls.Add(this.label4);
            this.gbParams.Controls.Add(this.label3);
            this.gbParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbParams.Location = new System.Drawing.Point(20, 20);
            this.gbParams.Name = "gbParams";
            this.gbParams.Size = new System.Drawing.Size(450, 120);
            this.gbParams.TabIndex = 13;
            this.gbParams.TabStop = false;
            this.gbParams.Text = "仿射变换";
            // 
            // YStep
            // 
            this.YStep.DecimalPlaces = 2;
            this.YStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.YStep.Location = new System.Drawing.Point(352, 75);
            this.YStep.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.YStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.YStep.Name = "YStep";
            this.YStep.Size = new System.Drawing.Size(80, 25);
            this.YStep.TabIndex = 5;
            this.YStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // XStep
            // 
            this.XStep.DecimalPlaces = 2;
            this.XStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.XStep.Location = new System.Drawing.Point(352, 31);
            this.XStep.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.XStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.XStep.Name = "XStep";
            this.XStep.Size = new System.Drawing.Size(80, 25);
            this.XStep.TabIndex = 4;
            this.XStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(260, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "Y方向步长：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(260, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "X方向步长：";
            // 
            // YPointNum
            // 
            this.YPointNum.Enabled = false;
            this.YPointNum.Location = new System.Drawing.Point(126, 75);
            this.YPointNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.YPointNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.YPointNum.Name = "YPointNum";
            this.YPointNum.Size = new System.Drawing.Size(80, 25);
            this.YPointNum.TabIndex = 2;
            this.YPointNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YPointNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // XPointNum
            // 
            this.XPointNum.Enabled = false;
            this.XPointNum.Location = new System.Drawing.Point(126, 31);
            this.XPointNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.XPointNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.XPointNum.Name = "XPointNum";
            this.XPointNum.Size = new System.Drawing.Size(80, 25);
            this.XPointNum.TabIndex = 1;
            this.XPointNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XPointNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Y方向点数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "X方向点数：";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 20);
            this.panel1.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(20, 331);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(450, 35);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImage = global::HyEye.WForm.Properties.Resources.确定;
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSave.Location = new System.Drawing.Point(3, 3);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(29, 29);
            this.pbSave.TabIndex = 28;
            this.pbSave.TabStop = false;
            // 
            // FrmHandEyeParamSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 383);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHandEyeParamSetting";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHandEyeParamSetting";
            this.Load += new System.EventHandler(this.FrmHandEyeParamSetting_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.APointNum)).EndInit();
            this.gbParams.ResumeLayout(false);
            this.gbParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPointNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPointNum)).EndInit();
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnMultiPointFit;
        private System.Windows.Forms.RadioButton rbtnMultiPointAngleFit;
        private System.Windows.Forms.GroupBox gbParams;
        private System.Windows.Forms.NumericUpDown AStep;
        private System.Windows.Forms.NumericUpDown YStep;
        private System.Windows.Forms.NumericUpDown XStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown APointNum;
        private System.Windows.Forms.NumericUpDown YPointNum;
        private System.Windows.Forms.NumericUpDown XPointNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.CheckBox ckbEnabledFitCircle;
        private System.Windows.Forms.Label label1;
    }
}