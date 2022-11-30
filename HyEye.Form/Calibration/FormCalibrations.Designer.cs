
namespace HyEye.WForm.Calibration
{
    partial class FormCalibrations
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvCalibration = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddCheckerboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddHandEye = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddJoint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectCamera = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvCalibration);
            this.splitContainer1.Size = new System.Drawing.Size(1039, 673);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvCalibration
            // 
            this.tvCalibration.ContextMenuStrip = this.contextMenuStrip1;
            this.tvCalibration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCalibration.Location = new System.Drawing.Point(0, 0);
            this.tvCalibration.Name = "tvCalibration";
            this.tvCalibration.Size = new System.Drawing.Size(345, 673);
            this.tvCalibration.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddCalibration,
            this.tsmiDeleteCalibration,
            this.tsmiSelectCamera});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 104);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiAddCalibration
            // 
            this.tsmiAddCalibration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddCheckerboard,
            this.tsmiAddHandEye,
            this.tsmiAddJoint});
            this.tsmiAddCalibration.Name = "tsmiAddCalibration";
            this.tsmiAddCalibration.Size = new System.Drawing.Size(138, 24);
            this.tsmiAddCalibration.Text = "新建标定";
            // 
            // tsmiAddCheckerboard
            // 
            this.tsmiAddCheckerboard.Name = "tsmiAddCheckerboard";
            this.tsmiAddCheckerboard.Size = new System.Drawing.Size(195, 26);
            this.tsmiAddCheckerboard.Text = "Checkerboard";
            this.tsmiAddCheckerboard.Click += new System.EventHandler(this.tsmiAddCheckerboard_Click);
            // 
            // tsmiAddHandEye
            // 
            this.tsmiAddHandEye.Name = "tsmiAddHandEye";
            this.tsmiAddHandEye.Size = new System.Drawing.Size(195, 26);
            this.tsmiAddHandEye.Text = "HandEye";
            this.tsmiAddHandEye.Click += new System.EventHandler(this.tsmiAddHandEye_Click);
            // 
            // tsmiAddJoint
            // 
            this.tsmiAddJoint.Name = "tsmiAddJoint";
            this.tsmiAddJoint.Size = new System.Drawing.Size(195, 26);
            this.tsmiAddJoint.Text = "联合标定";
            this.tsmiAddJoint.Click += new System.EventHandler(this.tsmiAddJoint_Click);
            // 
            // tsmiDeleteCalibration
            // 
            this.tsmiDeleteCalibration.Name = "tsmiDeleteCalibration";
            this.tsmiDeleteCalibration.Size = new System.Drawing.Size(138, 24);
            this.tsmiDeleteCalibration.Text = "删除标定";
            this.tsmiDeleteCalibration.Click += new System.EventHandler(this.tsmiDeleteCalibration_Click);
            // 
            // tsmiSelectCamera
            // 
            this.tsmiSelectCamera.Name = "tsmiSelectCamera";
            this.tsmiSelectCamera.Size = new System.Drawing.Size(138, 24);
            this.tsmiSelectCamera.Text = "选择相机";
            this.tsmiSelectCamera.Click += new System.EventHandler(this.tsmiSelectCamera_Click);
            // 
            // FrmCalibrations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 673);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmCalibrations";
            this.Text = "标定设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvCalibration;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectCamera;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCalibration;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCalibration;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCheckerboard;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddHandEye;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddJoint;
    }
}