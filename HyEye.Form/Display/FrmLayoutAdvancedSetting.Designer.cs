namespace HyEye.WForm
{
    partial class FrmLayoutAdvancedSetting
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
            this.rdoAutoSize = new System.Windows.Forms.RadioButton();
            this.rdoUserSize = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.pbOK = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width：";
            // 
            // rdoAutoSize
            // 
            this.rdoAutoSize.AutoSize = true;
            this.rdoAutoSize.Location = new System.Drawing.Point(40, 40);
            this.rdoAutoSize.Name = "rdoAutoSize";
            this.rdoAutoSize.Size = new System.Drawing.Size(73, 19);
            this.rdoAutoSize.TabIndex = 1;
            this.rdoAutoSize.Text = "自适应";
            this.rdoAutoSize.UseVisualStyleBackColor = true;
            this.rdoAutoSize.CheckedChanged += new System.EventHandler(this.rdoAutoSize_CheckedChanged);
            // 
            // rdoUserSize
            // 
            this.rdoUserSize.AutoSize = true;
            this.rdoUserSize.Location = new System.Drawing.Point(40, 90);
            this.rdoUserSize.Name = "rdoUserSize";
            this.rdoUserSize.Size = new System.Drawing.Size(73, 19);
            this.rdoUserSize.TabIndex = 2;
            this.rdoUserSize.Text = "自定义";
            this.rdoUserSize.UseVisualStyleBackColor = true;
            this.rdoUserSize.CheckedChanged += new System.EventHandler(this.rdoUserSize_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height：";
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(194, 89);
            this.nudWidth.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(60, 25);
            this.nudWidth.TabIndex = 4;
            this.nudWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudWidth.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(362, 89);
            this.nudHeight.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(60, 25);
            this.nudHeight.TabIndex = 5;
            this.nudHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(194, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(322, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 7;
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
            this.pbCancel.Parent = this.btnCancel;
            this.pbCancel.Size = new System.Drawing.Size(29, 29);
            this.pbCancel.TabIndex = 32;
            this.pbCancel.TabStop = false;
            // 
            // pbOK
            // 
            this.pbOK.BackColor = System.Drawing.Color.Transparent;
            this.pbOK.BackgroundImage = global::HyEye.WForm.Properties.Resources.确定;
            this.pbOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOK.Location = new System.Drawing.Point(3, 3);
            this.pbOK.Name = "pbOK";
            this.pbOK.Parent = this.btnOK;
            this.pbOK.Size = new System.Drawing.Size(29, 29);
            this.pbOK.TabIndex = 31;
            this.pbOK.TabStop = false;
            // 
            // FrmLayoutAdvancedSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(462, 201);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdoUserSize);
            this.Controls.Add(this.rdoAutoSize);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmLayoutAdvancedSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "高级设置";
            this.Load += new System.EventHandler(this.FrmLayoutAdvancedSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoAutoSize;
        private System.Windows.Forms.RadioButton rdoUserSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbCancel;
        private System.Windows.Forms.PictureBox pbOK;
    }
}