namespace HyEye.WForm.Settings
{
    partial class FrmLightControllerSetting
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
            this.btnSave = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddLightController = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdateLightController = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteLightController = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiBindLight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.tvLightController = new System.Windows.Forms.TreeViewDisableDoubleClick();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnDeleteController = new System.Windows.Forms.Button();
            this.btnAddController = new System.Windows.Forms.Button();
            this.pbDeleteController = new System.Windows.Forms.PictureBox();
            this.pbAddController = new System.Windows.Forms.PictureBox();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.btnSave.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.btnDeleteController.SuspendLayout();
            this.btnAddController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(236, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddLightController,
            this.tsmiUpdateLightController,
            this.tsmiDeleteLightController,
            this.toolStripSeparator1,
            this.tsmiBindLight,
            this.tsmiChangeChannel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 130);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiAddLightController
            // 
            this.tsmiAddLightController.Name = "tsmiAddLightController";
            this.tsmiAddLightController.Size = new System.Drawing.Size(183, 24);
            this.tsmiAddLightController.Text = "添加光源控制器";
            this.tsmiAddLightController.Click += new System.EventHandler(this.tsmiAddLightController_Click);
            // 
            // tsmiUpdateLightController
            // 
            this.tsmiUpdateLightController.Name = "tsmiUpdateLightController";
            this.tsmiUpdateLightController.Size = new System.Drawing.Size(183, 24);
            this.tsmiUpdateLightController.Text = "编辑光源控制器";
            this.tsmiUpdateLightController.Click += new System.EventHandler(this.tsmiUpdateLightController_Click);
            // 
            // tsmiDeleteLightController
            // 
            this.tsmiDeleteLightController.Name = "tsmiDeleteLightController";
            this.tsmiDeleteLightController.Size = new System.Drawing.Size(183, 24);
            this.tsmiDeleteLightController.Text = "删除光源控制器";
            this.tsmiDeleteLightController.Click += new System.EventHandler(this.tsmiDeleteLightController_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // tsmiBindLight
            // 
            this.tsmiBindLight.Name = "tsmiBindLight";
            this.tsmiBindLight.Size = new System.Drawing.Size(183, 24);
            this.tsmiBindLight.Text = "命名光源";
            this.tsmiBindLight.Click += new System.EventHandler(this.tsmiBindLight_Click);
            // 
            // tsmiChangeChannel
            // 
            this.tsmiChangeChannel.Name = "tsmiChangeChannel";
            this.tsmiChangeChannel.Size = new System.Drawing.Size(183, 24);
            this.tsmiChangeChannel.Text = "切换通道";
            this.tsmiChangeChannel.Click += new System.EventHandler(this.tsmiChangeChannel_Click);
            // 
            // tvLightController
            // 
            this.tvLightController.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvLightController.ContextMenuStrip = this.contextMenuStrip1;
            this.tvLightController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLightController.Location = new System.Drawing.Point(0, 57);
            this.tvLightController.Name = "tvLightController";
            this.tvLightController.Size = new System.Drawing.Size(492, 530);
            this.tvLightController.TabIndex = 26;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnDeleteController);
            this.pnlTop.Controls.Add(this.btnSave);
            this.pnlTop.Controls.Add(this.btnAddController);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(492, 57);
            this.pnlTop.TabIndex = 27;
            // 
            // btnDeleteController
            // 
            this.btnDeleteController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteController.Controls.Add(this.pbDeleteController);
            this.btnDeleteController.Location = new System.Drawing.Point(130, 12);
            this.btnDeleteController.Name = "btnDeleteController";
            this.btnDeleteController.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteController.TabIndex = 1;
            this.btnDeleteController.Text = "删除";
            this.btnDeleteController.UseVisualStyleBackColor = true;
            this.btnDeleteController.Click += new System.EventHandler(this.btnDeleteController_Click);
            // 
            // btnAddController
            // 
            this.btnAddController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddController.Controls.Add(this.pbAddController);
            this.btnAddController.Location = new System.Drawing.Point(24, 12);
            this.btnAddController.Name = "btnAddController";
            this.btnAddController.Size = new System.Drawing.Size(100, 35);
            this.btnAddController.TabIndex = 0;
            this.btnAddController.Text = "添加";
            this.btnAddController.UseVisualStyleBackColor = true;
            this.btnAddController.Click += new System.EventHandler(this.btnAddController_Click);
            // 
            // pbDeleteController
            // 
            this.pbDeleteController.BackColor = System.Drawing.Color.Transparent;
            this.pbDeleteController.BackgroundImage = global::HyEye.WForm.Properties.Resources.删除;
            this.pbDeleteController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbDeleteController.Location = new System.Drawing.Point(3, 3);
            this.pbDeleteController.Name = "pbDeleteController";
            this.pbDeleteController.Size = new System.Drawing.Size(29, 29);
            this.pbDeleteController.TabIndex = 30;
            this.pbDeleteController.TabStop = false;
            // 
            // pbAddController
            // 
            this.pbAddController.BackColor = System.Drawing.Color.Transparent;
            this.pbAddController.BackgroundImage = global::HyEye.WForm.Properties.Resources.新增;
            this.pbAddController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAddController.Location = new System.Drawing.Point(3, 3);
            this.pbAddController.Name = "pbAddController";
            this.pbAddController.Size = new System.Drawing.Size(29, 29);
            this.pbAddController.TabIndex = 29;
            this.pbAddController.TabStop = false;
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
            // FrmLightControllerSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(492, 587);
            this.Controls.Add(this.tvLightController);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLightControllerSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "光源控制器设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLightControllerSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmLightControllerSetting_Load);
            this.btnSave.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.btnDeleteController.ResumeLayout(false);
            this.btnAddController.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TreeViewDisableDoubleClick tvLightController;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddLightController;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateLightController;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteLightController;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiBindLight;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeChannel;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnAddController;
        private System.Windows.Forms.Button btnDeleteController;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbDeleteController;
        private System.Windows.Forms.PictureBox pbAddController;
    }
}