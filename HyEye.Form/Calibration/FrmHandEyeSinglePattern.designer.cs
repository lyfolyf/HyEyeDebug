namespace HyEye.WForm.Calibration
{
    partial class FrmHandEyeSinglePattern
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAcqImage = new System.Windows.Forms.Button();
            this.pbAcqImage = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbtnUsePMAlign = new System.Windows.Forms.RadioButton();
            this.rdbtnUseToolBlock = new System.Windows.Forms.RadioButton();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.panel1.SuspendLayout();
            this.btnAcqImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAcqImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(1050, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 37);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbSave
            // 
            this.pbSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImage = global::HyEye.WForm.Properties.Resources.保存;
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSave.Location = new System.Drawing.Point(3, 3);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(29, 29);
            this.pbSave.TabIndex = 27;
            this.pbSave.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbtnUseToolBlock);
            this.panel1.Controls.Add(this.rdbtnUsePMAlign);
            this.panel1.Controls.Add(this.btnAcqImage);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1232, 65);
            this.panel1.TabIndex = 3;
            // 
            // btnAcqImage
            // 
            this.btnAcqImage.Controls.Add(this.pbAcqImage);
            this.btnAcqImage.Location = new System.Drawing.Point(25, 15);
            this.btnAcqImage.Name = "btnAcqImage";
            this.btnAcqImage.Size = new System.Drawing.Size(150, 37);
            this.btnAcqImage.TabIndex = 2;
            this.btnAcqImage.Text = "取像";
            this.btnAcqImage.UseVisualStyleBackColor = true;
            this.btnAcqImage.Click += new System.EventHandler(this.btnAcqImage_Click);
            // 
            // pbAcqImage
            // 
            this.pbAcqImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAcqImage.BackColor = System.Drawing.Color.Transparent;
            this.pbAcqImage.BackgroundImage = global::HyEye.WForm.Properties.Resources.单次拍照;
            this.pbAcqImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAcqImage.Location = new System.Drawing.Point(3, 3);
            this.pbAcqImage.Name = "pbAcqImage";
            this.pbAcqImage.Size = new System.Drawing.Size(29, 29);
            this.pbAcqImage.TabIndex = 28;
            this.pbAcqImage.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1232, 638);
            this.panel2.TabIndex = 4;
            // 
            // rdbtnUsePMAlign
            // 
            this.rdbtnUsePMAlign.AutoSize = true;
            this.rdbtnUsePMAlign.Location = new System.Drawing.Point(258, 24);
            this.rdbtnUsePMAlign.Name = "rdbtnUsePMAlign";
            this.rdbtnUsePMAlign.Size = new System.Drawing.Size(122, 19);
            this.rdbtnUsePMAlign.TabIndex = 3;
            this.rdbtnUsePMAlign.TabStop = true;
            this.rdbtnUsePMAlign.Text = "使用 PMAlign";
            this.rdbtnUsePMAlign.UseVisualStyleBackColor = true;
            this.rdbtnUsePMAlign.CheckedChanged += new System.EventHandler(this.rdbtnUsePMAlign_CheckedChanged);
            // 
            // rdbtnUseToolBlock
            // 
            this.rdbtnUseToolBlock.AutoSize = true;
            this.rdbtnUseToolBlock.Location = new System.Drawing.Point(399, 24);
            this.rdbtnUseToolBlock.Name = "rdbtnUseToolBlock";
            this.rdbtnUseToolBlock.Size = new System.Drawing.Size(138, 19);
            this.rdbtnUseToolBlock.TabIndex = 4;
            this.rdbtnUseToolBlock.TabStop = true;
            this.rdbtnUseToolBlock.Text = "使用 ToolBlock";
            this.rdbtnUseToolBlock.UseVisualStyleBackColor = true;
            this.rdbtnUseToolBlock.CheckedChanged += new System.EventHandler(this.rdbtnUseToolBlock_CheckedChanged);
            // 
            // FrmHandEyePattern
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1232, 703);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Name = "FrmHandEyePattern";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHandEyeToolSetting";
            this.Load += new System.EventHandler(this.FrmHandEyePattern_Load);
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.btnAcqImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAcqImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAcqImage;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbAcqImage;
        private System.Windows.Forms.RadioButton rdbtnUseToolBlock;
        private System.Windows.Forms.RadioButton rdbtnUsePMAlign;
    }
}