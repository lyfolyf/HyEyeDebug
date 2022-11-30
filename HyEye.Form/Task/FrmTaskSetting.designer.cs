namespace HyEye.WForm.Settings
{
    partial class FrmTaskSetting
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.cmsTaskTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNewVisionProTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHyVisionTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBatchAddTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRenameTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRunTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddAcqImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRenameAcqImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteAcqImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpticalSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteOpticalSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRunAcqImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNewCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRenameCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQuoteJointCalib = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tvTasks = new System.Windows.Forms.TreeViewProcessCmdKey();
            this.tsmiBatchRunTaskOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.cmsTaskTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 561);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(267, 57);
            this.panel1.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(10, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(245, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存任务设置";
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
            this.pbSave.TabIndex = 29;
            this.pbSave.TabStop = false;
            // 
            // cmsTaskTree
            // 
            this.cmsTaskTree.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTaskTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewVisionProTask,
            this.tsmiHyVisionTask,
            this.tsmiBatchAddTask,
            this.tsmiRenameTask,
            this.tsmiDeleteTask,
            this.tsmiRunTask,
            this.tsmiBatchRunTaskOffline,
            this.toolStripSeparator1,
            this.tsmiAddCamera,
            this.toolStripSeparator2,
            this.tsmiAddAcqImage,
            this.tsmiRenameAcqImage,
            this.tsmiDeleteAcqImage,
            this.tsmiOpticalSetting,
            this.tsmiDeleteOpticalSetting,
            this.tsmiRunAcqImage,
            this.toolStripSeparator3,
            this.tsmiNewCalibration,
            this.tsmiRenameCalibration,
            this.tsmiDeleteCalibration,
            this.tsmiQuoteJointCalib,
            this.toolStripSeparator4,
            this.tsmiMoveUp,
            this.tsmiMoveDown,
            this.tsmiExpandAll,
            this.tsmiCollapseAll,
            this.toolStripSeparator5,
            this.tsmiRefresh});
            this.cmsTaskTree.Name = "contextMenuStrip1";
            this.cmsTaskTree.Size = new System.Drawing.Size(216, 590);
            this.cmsTaskTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTaskTree_Opening);
            // 
            // tsmiNewVisionProTask
            // 
            this.tsmiNewVisionProTask.Name = "tsmiNewVisionProTask";
            this.tsmiNewVisionProTask.Size = new System.Drawing.Size(215, 24);
            this.tsmiNewVisionProTask.Text = "新建 VisionPro 任务";
            this.tsmiNewVisionProTask.Click += new System.EventHandler(this.tsmiNewVisionProTask_Click);
            // 
            // tsmiHyVisionTask
            // 
            this.tsmiHyVisionTask.Name = "tsmiHyVisionTask";
            this.tsmiHyVisionTask.Size = new System.Drawing.Size(215, 24);
            this.tsmiHyVisionTask.Text = "新建 HyVision 任务";
            this.tsmiHyVisionTask.Click += new System.EventHandler(this.tsmiHyVisionTask_Click);
            // 
            // tsmiBatchAddTask
            // 
            this.tsmiBatchAddTask.Name = "tsmiBatchAddTask";
            this.tsmiBatchAddTask.Size = new System.Drawing.Size(215, 24);
            this.tsmiBatchAddTask.Text = "批量新建任务";
            this.tsmiBatchAddTask.Click += new System.EventHandler(this.tsmiBatchAddTask_Click);
            // 
            // tsmiRenameTask
            // 
            this.tsmiRenameTask.Name = "tsmiRenameTask";
            this.tsmiRenameTask.Size = new System.Drawing.Size(215, 24);
            this.tsmiRenameTask.Text = "重命名任务";
            this.tsmiRenameTask.Click += new System.EventHandler(this.tsmiRenameTask_Click);
            // 
            // tsmiDeleteTask
            // 
            this.tsmiDeleteTask.Name = "tsmiDeleteTask";
            this.tsmiDeleteTask.Size = new System.Drawing.Size(215, 24);
            this.tsmiDeleteTask.Text = "删除任务";
            this.tsmiDeleteTask.Click += new System.EventHandler(this.tsmiDeleteTask_Click);
            // 
            // tsmiRunTask
            // 
            this.tsmiRunTask.Name = "tsmiRunTask";
            this.tsmiRunTask.Size = new System.Drawing.Size(215, 24);
            this.tsmiRunTask.Text = "运行任务";
            this.tsmiRunTask.Click += new System.EventHandler(this.tsmiRunTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // tsmiAddCamera
            // 
            this.tsmiAddCamera.Name = "tsmiAddCamera";
            this.tsmiAddCamera.Size = new System.Drawing.Size(215, 24);
            this.tsmiAddCamera.Text = "选择相机";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // tsmiAddAcqImage
            // 
            this.tsmiAddAcqImage.Name = "tsmiAddAcqImage";
            this.tsmiAddAcqImage.Size = new System.Drawing.Size(215, 24);
            this.tsmiAddAcqImage.Text = "添加拍照";
            this.tsmiAddAcqImage.Click += new System.EventHandler(this.tsmiAddAcqImage_Click);
            // 
            // tsmiRenameAcqImage
            // 
            this.tsmiRenameAcqImage.Name = "tsmiRenameAcqImage";
            this.tsmiRenameAcqImage.Size = new System.Drawing.Size(215, 24);
            this.tsmiRenameAcqImage.Text = "重命名拍照";
            this.tsmiRenameAcqImage.Click += new System.EventHandler(this.tsmiRenameTakePhone_Click);
            // 
            // tsmiDeleteAcqImage
            // 
            this.tsmiDeleteAcqImage.Name = "tsmiDeleteAcqImage";
            this.tsmiDeleteAcqImage.Size = new System.Drawing.Size(215, 24);
            this.tsmiDeleteAcqImage.Text = "删除拍照";
            this.tsmiDeleteAcqImage.Click += new System.EventHandler(this.tsmiDeleteAcqImage_Click);
            // 
            // tsmiOpticalSetting
            // 
            this.tsmiOpticalSetting.Name = "tsmiOpticalSetting";
            this.tsmiOpticalSetting.Size = new System.Drawing.Size(215, 24);
            this.tsmiOpticalSetting.Text = "添加光学设置";
            this.tsmiOpticalSetting.Click += new System.EventHandler(this.tsmiOpticalSetting_Click);
            // 
            // tsmiDeleteOpticalSetting
            // 
            this.tsmiDeleteOpticalSetting.Name = "tsmiDeleteOpticalSetting";
            this.tsmiDeleteOpticalSetting.Size = new System.Drawing.Size(215, 24);
            this.tsmiDeleteOpticalSetting.Text = "删除光学设置";
            this.tsmiDeleteOpticalSetting.Click += new System.EventHandler(this.tsmiDeleteOpticalSetting_Click);
            // 
            // tsmiRunAcqImage
            // 
            this.tsmiRunAcqImage.Name = "tsmiRunAcqImage";
            this.tsmiRunAcqImage.Size = new System.Drawing.Size(215, 24);
            this.tsmiRunAcqImage.Text = "运行拍照";
            this.tsmiRunAcqImage.Click += new System.EventHandler(this.tsmiRunAcqImage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // tsmiNewCalibration
            // 
            this.tsmiNewCalibration.Name = "tsmiNewCalibration";
            this.tsmiNewCalibration.Size = new System.Drawing.Size(215, 24);
            this.tsmiNewCalibration.Text = "添加标定";
            this.tsmiNewCalibration.Click += new System.EventHandler(this.tsmiNewCalibration_Click);
            // 
            // tsmiRenameCalibration
            // 
            this.tsmiRenameCalibration.Name = "tsmiRenameCalibration";
            this.tsmiRenameCalibration.Size = new System.Drawing.Size(215, 24);
            this.tsmiRenameCalibration.Text = "重命名标定";
            this.tsmiRenameCalibration.Click += new System.EventHandler(this.tsmiRenameCalibration_Click);
            // 
            // tsmiDeleteCalibration
            // 
            this.tsmiDeleteCalibration.Name = "tsmiDeleteCalibration";
            this.tsmiDeleteCalibration.Size = new System.Drawing.Size(215, 24);
            this.tsmiDeleteCalibration.Text = "删除标定";
            this.tsmiDeleteCalibration.Click += new System.EventHandler(this.tsmiDeleteCalibration_Click);
            // 
            // tsmiQuoteJointCalib
            // 
            this.tsmiQuoteJointCalib.Name = "tsmiQuoteJointCalib";
            this.tsmiQuoteJointCalib.Size = new System.Drawing.Size(215, 24);
            this.tsmiQuoteJointCalib.Text = "引用联合标定";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            // 
            // tsmiMoveUp
            // 
            this.tsmiMoveUp.Name = "tsmiMoveUp";
            this.tsmiMoveUp.Size = new System.Drawing.Size(215, 24);
            this.tsmiMoveUp.Text = "上移";
            this.tsmiMoveUp.Click += new System.EventHandler(this.tsmiMoveUp_Click);
            // 
            // tsmiMoveDown
            // 
            this.tsmiMoveDown.Name = "tsmiMoveDown";
            this.tsmiMoveDown.Size = new System.Drawing.Size(215, 24);
            this.tsmiMoveDown.Text = "下移";
            this.tsmiMoveDown.Click += new System.EventHandler(this.tsmiMoveDown_Click);
            // 
            // tsmiExpandAll
            // 
            this.tsmiExpandAll.Name = "tsmiExpandAll";
            this.tsmiExpandAll.Size = new System.Drawing.Size(215, 24);
            this.tsmiExpandAll.Text = "全部展开";
            this.tsmiExpandAll.Click += new System.EventHandler(this.tsmiExpandAll_Click);
            // 
            // tsmiCollapseAll
            // 
            this.tsmiCollapseAll.Name = "tsmiCollapseAll";
            this.tsmiCollapseAll.Size = new System.Drawing.Size(215, 24);
            this.tsmiCollapseAll.Text = "全部收起";
            this.tsmiCollapseAll.Click += new System.EventHandler(this.tsmiCollapseAll_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(212, 6);
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(215, 24);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tvTasks
            // 
            this.tvTasks.ContextMenuStrip = this.cmsTaskTree;
            this.tvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTasks.HideSelection = false;
            this.tvTasks.Location = new System.Drawing.Point(0, 0);
            this.tvTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvTasks.Name = "tvTasks";
            this.tvTasks.ShowNodeToolTips = true;
            this.tvTasks.Size = new System.Drawing.Size(267, 561);
            this.tvTasks.TabIndex = 20;
            this.tvTasks.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvTasks_AfterLabelEdit);
            this.tvTasks.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTasks_AfterCheck);
            this.tvTasks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTasks_NodeMouseDoubleClick);
            this.tvTasks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvTasks_KeyPress);
            // 
            // tsmiBatchRunTaskOffline
            // 
            this.tsmiBatchRunTaskOffline.Name = "tsmiBatchRunTaskOffline";
            this.tsmiBatchRunTaskOffline.Size = new System.Drawing.Size(215, 24);
            this.tsmiBatchRunTaskOffline.Text = "离线批量运行任务";
            this.tsmiBatchRunTaskOffline.Click += new System.EventHandler(this.tsmiBatchRunTaskOffline_Click);
            // 
            // FrmTaskSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(267, 618);
            this.Controls.Add(this.tvTasks);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HideOnClose = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTaskSetting";
            this.Text = "任务设置";
            this.Load += new System.EventHandler(this.FrmTaskSetting_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmTaskSetting_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.cmsTaskTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip cmsTaskTree;
        private System.Windows.Forms.TreeViewProcessCmdKey tvTasks;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewVisionProTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiRenameTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCamera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddAcqImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteAcqImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiRenameAcqImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiMoveUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiMoveDown;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpticalSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiRunTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewCalibration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCalibration;
        private System.Windows.Forms.ToolStripMenuItem tsmiRenameCalibration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiRunAcqImage;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiBatchAddTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiHyVisionTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteOpticalSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiBatchRunTaskOffline;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuoteJointCalib;
    }
}