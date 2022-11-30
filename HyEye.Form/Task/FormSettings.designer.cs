namespace HyEye.WForm
{
    partial class FormSettings
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dockPanelMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsbtnView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTaskSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVisionHandle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCameraSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLightControllerSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommunicationSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommandSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOnLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 677);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1228, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dockPanelMain
            // 
            this.dockPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanelMain.Location = new System.Drawing.Point(0, 28);
            this.dockPanelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dockPanelMain.Name = "dockPanelMain";
            this.dockPanelMain.Size = new System.Drawing.Size(1228, 649);
            this.dockPanelMain.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnView,
            this.tsbtnSettings,
            this.tsmiOnLine,
            this.tsmiProjectProcess});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1228, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsbtnView
            // 
            this.tsbtnView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTaskSetting,
            this.tsmiVisionHandle,
            this.tsmiLog});
            this.tsbtnView.Image = global::HyEye.WForm.Properties.Resources.视图;
            this.tsbtnView.Name = "tsbtnView";
            this.tsbtnView.Size = new System.Drawing.Size(73, 26);
            this.tsbtnView.Text = "视图";
            // 
            // tsmiTaskSetting
            // 
            this.tsmiTaskSetting.Name = "tsmiTaskSetting";
            this.tsmiTaskSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiTaskSetting.Text = "任务";
            this.tsmiTaskSetting.Click += new System.EventHandler(this.tsmiTaskSetting_Click);
            // 
            // tsmiVisionHandle
            // 
            this.tsmiVisionHandle.Name = "tsmiVisionHandle";
            this.tsmiVisionHandle.Size = new System.Drawing.Size(152, 26);
            this.tsmiVisionHandle.Text = "视觉处理";
            this.tsmiVisionHandle.Click += new System.EventHandler(this.tsmiVisionHandle_Click);
            // 
            // tsmiLog
            // 
            this.tsmiLog.Name = "tsmiLog";
            this.tsmiLog.Size = new System.Drawing.Size(152, 26);
            this.tsmiLog.Text = "日志";
            this.tsmiLog.Click += new System.EventHandler(this.tsmiLog_Click);
            // 
            // tsbtnSettings
            // 
            this.tsbtnSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCameraSetting,
            this.tsmiLightControllerSetting,
            this.tsmiCommunicationSetting,
            this.tsmiCommandSetting,
            this.tsmiImageSetting,
            this.tsmiSystemSetting});
            this.tsbtnSettings.Image = global::HyEye.WForm.Properties.Resources.系统设置;
            this.tsbtnSettings.Name = "tsbtnSettings";
            this.tsbtnSettings.Size = new System.Drawing.Size(73, 26);
            this.tsbtnSettings.Text = "设置";
            // 
            // tsmiCameraSetting
            // 
            this.tsmiCameraSetting.Name = "tsmiCameraSetting";
            this.tsmiCameraSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiCameraSetting.Text = "相机设置";
            this.tsmiCameraSetting.Click += new System.EventHandler(this.tsmiCameraSetting_Click);
            // 
            // tsmiLightControllerSetting
            // 
            this.tsmiLightControllerSetting.Name = "tsmiLightControllerSetting";
            this.tsmiLightControllerSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiLightControllerSetting.Text = "光控设置";
            this.tsmiLightControllerSetting.Click += new System.EventHandler(this.tsmiLightControllerSetting_Click);
            // 
            // tsmiCommunicationSetting
            // 
            this.tsmiCommunicationSetting.Name = "tsmiCommunicationSetting";
            this.tsmiCommunicationSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiCommunicationSetting.Text = "通讯设置";
            this.tsmiCommunicationSetting.Click += new System.EventHandler(this.tsmiCommunicationSetting_Click);
            // 
            // tsmiCommandSetting
            // 
            this.tsmiCommandSetting.Name = "tsmiCommandSetting";
            this.tsmiCommandSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiCommandSetting.Text = "指令设置";
            this.tsmiCommandSetting.Click += new System.EventHandler(this.tsmiCommandSetting_Click);
            // 
            // tsmiImageSetting
            // 
            this.tsmiImageSetting.Name = "tsmiImageSetting";
            this.tsmiImageSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiImageSetting.Text = "图像设置";
            this.tsmiImageSetting.Click += new System.EventHandler(this.tsmiImageSetting_Click);
            // 
            // tsmiSystemSetting
            // 
            this.tsmiSystemSetting.Name = "tsmiSystemSetting";
            this.tsmiSystemSetting.Size = new System.Drawing.Size(152, 26);
            this.tsmiSystemSetting.Text = "系统设置";
            this.tsmiSystemSetting.Click += new System.EventHandler(this.tsmiSystemSetting_Click);
            // 
            // tsmiOnLine
            // 
            this.tsmiOnLine.Image = global::HyEye.WForm.Properties.Resources.在线1;
            this.tsmiOnLine.Name = "tsmiOnLine";
            this.tsmiOnLine.Size = new System.Drawing.Size(103, 26);
            this.tsmiOnLine.Text = "在线模式";
            this.tsmiOnLine.Click += new System.EventHandler(this.tsmiOnLine_Click);
            // 
            // tsmiProjectProcess
            // 
            this.tsmiProjectProcess.Image = global::HyEye.WForm.Properties.Resources.流程;
            this.tsmiProjectProcess.Name = "tsmiProjectProcess";
            this.tsmiProjectProcess.Size = new System.Drawing.Size(73, 26);
            this.tsmiProjectProcess.Text = "流程";
            this.tsmiProjectProcess.Click += new System.EventHandler(this.tsmiProjectProcess_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1228, 699);
            this.Controls.Add(this.dockPanelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSettings";
            this.Text = "2D 视觉标准化软件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanelMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsbtnView;
        private System.Windows.Forms.ToolStripMenuItem tsbtnSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectProcess;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiVisionHandle;
        private System.Windows.Forms.ToolStripMenuItem tsmiLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiCameraSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiLightControllerSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommunicationSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommandSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiOnLine;
    }
}