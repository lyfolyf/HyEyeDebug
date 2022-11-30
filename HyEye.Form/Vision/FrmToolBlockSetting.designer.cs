namespace HyEye.WForm.Vision
{
    partial class FrmToolBlockSetting
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
            this.tabVision = new System.Windows.Forms.TabControl();
            this.btnSaveCurTask = new System.Windows.Forms.Button();
            this.pbSaveCurTask = new System.Windows.Forms.PictureBox();
            this.btnSaveAllTasks = new System.Windows.Forms.Button();
            this.pbSaveAllTasks = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResetParam = new System.Windows.Forms.Button();
            this.pbResetParam = new System.Windows.Forms.PictureBox();
            this.btnSetParams = new System.Windows.Forms.Button();
            this.pbSetParams = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTasks = new System.Windows.Forms.ComboBox();
            this.btnSaveCurTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSaveCurTask)).BeginInit();
            this.btnSaveAllTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSaveAllTasks)).BeginInit();
            this.panel1.SuspendLayout();
            this.btnResetParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResetParam)).BeginInit();
            this.btnSetParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSetParams)).BeginInit();
            this.SuspendLayout();
            // 
            // tabVision
            // 
            this.tabVision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabVision.Location = new System.Drawing.Point(0, 56);
            this.tabVision.Name = "tabVision";
            this.tabVision.SelectedIndex = 0;
            this.tabVision.Size = new System.Drawing.Size(1232, 647);
            this.tabVision.TabIndex = 0;
            // 
            // btnSaveCurTask
            // 
            this.btnSaveCurTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCurTask.Controls.Add(this.pbSaveCurTask);
            this.btnSaveCurTask.Location = new System.Drawing.Point(840, 12);
            this.btnSaveCurTask.Margin = new System.Windows.Forms.Padding(18, 3, 18, 3);
            this.btnSaveCurTask.Name = "btnSaveCurTask";
            this.btnSaveCurTask.Size = new System.Drawing.Size(175, 35);
            this.btnSaveCurTask.TabIndex = 4;
            this.btnSaveCurTask.Text = "保存当前视觉处理";
            this.btnSaveCurTask.UseVisualStyleBackColor = true;
            this.btnSaveCurTask.Click += new System.EventHandler(this.btnSaveCurTask_Click);
            // 
            // pbSaveCurTask
            // 
            this.pbSaveCurTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSaveCurTask.BackColor = System.Drawing.Color.Transparent;
            this.pbSaveCurTask.BackgroundImage = global::HyEye.WForm.Properties.Resources.保存;
            this.pbSaveCurTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSaveCurTask.Location = new System.Drawing.Point(3, 3);
            this.pbSaveCurTask.Name = "pbSaveCurTask";
            this.pbSaveCurTask.Size = new System.Drawing.Size(29, 29);
            this.pbSaveCurTask.TabIndex = 30;
            this.pbSaveCurTask.TabStop = false;
            // 
            // btnSaveAllTasks
            // 
            this.btnSaveAllTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAllTasks.Controls.Add(this.pbSaveAllTasks);
            this.btnSaveAllTasks.Location = new System.Drawing.Point(1030, 12);
            this.btnSaveAllTasks.Margin = new System.Windows.Forms.Padding(18, 3, 18, 3);
            this.btnSaveAllTasks.Name = "btnSaveAllTasks";
            this.btnSaveAllTasks.Size = new System.Drawing.Size(175, 35);
            this.btnSaveAllTasks.TabIndex = 5;
            this.btnSaveAllTasks.Text = "保存所有视觉处理";
            this.btnSaveAllTasks.UseVisualStyleBackColor = true;
            this.btnSaveAllTasks.Click += new System.EventHandler(this.btnSaveAllTasks_Click);
            // 
            // pbSaveAllTasks
            // 
            this.pbSaveAllTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSaveAllTasks.BackColor = System.Drawing.Color.Transparent;
            this.pbSaveAllTasks.BackgroundImage = global::HyEye.WForm.Properties.Resources.全部保存;
            this.pbSaveAllTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSaveAllTasks.Location = new System.Drawing.Point(3, 3);
            this.pbSaveAllTasks.Name = "pbSaveAllTasks";
            this.pbSaveAllTasks.Size = new System.Drawing.Size(29, 29);
            this.pbSaveAllTasks.TabIndex = 31;
            this.pbSaveAllTasks.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnResetParam);
            this.panel1.Controls.Add(this.btnSaveAllTasks);
            this.panel1.Controls.Add(this.btnSetParams);
            this.panel1.Controls.Add(this.btnSaveCurTask);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbTasks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1232, 56);
            this.panel1.TabIndex = 13;
            // 
            // btnResetParam
            // 
            this.btnResetParam.Controls.Add(this.pbResetParam);
            this.btnResetParam.Location = new System.Drawing.Point(510, 12);
            this.btnResetParam.Margin = new System.Windows.Forms.Padding(18, 3, 18, 3);
            this.btnResetParam.Name = "btnResetParam";
            this.btnResetParam.Size = new System.Drawing.Size(150, 35);
            this.btnResetParam.TabIndex = 3;
            this.btnResetParam.Text = "重置默认参数";
            this.btnResetParam.UseVisualStyleBackColor = true;
            this.btnResetParam.Click += new System.EventHandler(this.btnResetParam_Click);
            // 
            // pbResetParam
            // 
            this.pbResetParam.BackColor = System.Drawing.Color.Transparent;
            this.pbResetParam.BackgroundImage = global::HyEye.WForm.Properties.Resources.重置;
            this.pbResetParam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbResetParam.Location = new System.Drawing.Point(3, 3);
            this.pbResetParam.Name = "pbResetParam";
            this.pbResetParam.Size = new System.Drawing.Size(29, 29);
            this.pbResetParam.TabIndex = 33;
            this.pbResetParam.TabStop = false;
            // 
            // btnSetParams
            // 
            this.btnSetParams.Controls.Add(this.pbSetParams);
            this.btnSetParams.Location = new System.Drawing.Point(340, 12);
            this.btnSetParams.Margin = new System.Windows.Forms.Padding(18, 3, 18, 3);
            this.btnSetParams.Name = "btnSetParams";
            this.btnSetParams.Size = new System.Drawing.Size(150, 35);
            this.btnSetParams.TabIndex = 2;
            this.btnSetParams.Text = "设置参数";
            this.btnSetParams.UseVisualStyleBackColor = true;
            this.btnSetParams.Click += new System.EventHandler(this.btnSetParams_Click);
            // 
            // pbSetParams
            // 
            this.pbSetParams.BackColor = System.Drawing.Color.Transparent;
            this.pbSetParams.BackgroundImage = global::HyEye.WForm.Properties.Resources.系统设置;
            this.pbSetParams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSetParams.Location = new System.Drawing.Point(3, 3);
            this.pbSetParams.Name = "pbSetParams";
            this.pbSetParams.Size = new System.Drawing.Size(29, 29);
            this.pbSetParams.TabIndex = 29;
            this.pbSetParams.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "任务切换：";
            // 
            // cmbTasks
            // 
            this.cmbTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTasks.FormattingEnabled = true;
            this.cmbTasks.Location = new System.Drawing.Point(108, 17);
            this.cmbTasks.Name = "cmbTasks";
            this.cmbTasks.Size = new System.Drawing.Size(200, 23);
            this.cmbTasks.TabIndex = 1;
            this.cmbTasks.SelectedIndexChanged += new System.EventHandler(this.cmbTasks_SelectedIndexChanged);
            // 
            // FrmToolBlockSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1232, 703);
            this.Controls.Add(this.tabVision);
            this.Controls.Add(this.panel1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HideOnClose = true;
            this.Name = "FrmToolBlockSetting";
            this.ShowIcon = false;
            this.Text = "视觉处理";
            this.Load += new System.EventHandler(this.FrmVisionToolSetting_Load);
            this.btnSaveCurTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSaveCurTask)).EndInit();
            this.btnSaveAllTasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSaveAllTasks)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.btnResetParam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbResetParam)).EndInit();
            this.btnSetParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSetParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabVision;
        private System.Windows.Forms.Button btnSaveCurTask;
        private System.Windows.Forms.Button btnSaveAllTasks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSetParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTasks;
        private System.Windows.Forms.PictureBox pbSaveAllTasks;
        private System.Windows.Forms.PictureBox pbSaveCurTask;
        private System.Windows.Forms.PictureBox pbSetParams;
        private System.Windows.Forms.PictureBox pbResetParam;
        private System.Windows.Forms.Button btnResetParam;
    }
}