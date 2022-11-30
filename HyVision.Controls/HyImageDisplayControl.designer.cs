namespace HyVision.Tools.ImageDisplay
{
    partial class HyImageDisplayControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HyImageDisplayControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.显示ROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiImportImg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportImg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDisplayOriImg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisplayDefectImg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDrawROI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrawCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrawRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrawPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.HyDisplayPanel = new HyInspect.Base.UIL.DispalyPanel();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Aquamarine;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripComboBox1,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(849, 36);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.Checked = true;
            this.toolStripButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 25);
            this.toolStripButton1.Text = "清除";
            this.toolStripButton1.ToolTipText = "清除图片";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(60, 52);
            this.toolStripButton2.Text = "放大";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.AutoSize = false;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(60, 52);
            this.toolStripButton3.Text = "缩小";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.AutoSize = false;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "线宽1",
            "线宽2",
            "线宽3"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(140, 28);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.显示ROIToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(39, 33);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 26);
            this.toolStripMenuItem1.Text = "Test1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 26);
            this.toolStripMenuItem2.Text = "Test2";
            // 
            // 显示ROIToolStripMenuItem
            // 
            this.显示ROIToolStripMenuItem.Name = "显示ROIToolStripMenuItem";
            this.显示ROIToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.显示ROIToolStripMenuItem.Text = "显示ROI";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 617);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(849, 26);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(434, 23);
            this.toolStripLabel1.Text = "[ X {0}, Y {0} ]     [ R{0}, G{0}, B{0} ]     [ZOOM : 100%]";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImportImg,
            this.tsmiExportImg,
            this.toolStripSeparator3,
            this.tsmiDisplayOriImg,
            this.tsmiDisplayDefectImg,
            this.toolStripSeparator4,
            this.tsmiDrawROI});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 136);
            // 
            // tsmiImportImg
            // 
            this.tsmiImportImg.Name = "tsmiImportImg";
            this.tsmiImportImg.Size = new System.Drawing.Size(153, 24);
            this.tsmiImportImg.Text = "导入图片";
            this.tsmiImportImg.Click += new System.EventHandler(this.tsmiImportImg_Click);
            // 
            // tsmiExportImg
            // 
            this.tsmiExportImg.Name = "tsmiExportImg";
            this.tsmiExportImg.Size = new System.Drawing.Size(153, 24);
            this.tsmiExportImg.Text = "导出图片";
            this.tsmiExportImg.Click += new System.EventHandler(this.tsmiExportImg_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmiDisplayOriImg
            // 
            this.tsmiDisplayOriImg.Name = "tsmiDisplayOriImg";
            this.tsmiDisplayOriImg.Size = new System.Drawing.Size(153, 24);
            this.tsmiDisplayOriImg.Text = "显示原图";
            this.tsmiDisplayOriImg.Click += new System.EventHandler(this.tsmiDisplayOriImg_Click);
            // 
            // tsmiDisplayDefectImg
            // 
            this.tsmiDisplayDefectImg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.tsmiDisplayDefectImg.Name = "tsmiDisplayDefectImg";
            this.tsmiDisplayDefectImg.Size = new System.Drawing.Size(153, 24);
            this.tsmiDisplayDefectImg.Text = "显示缺陷图";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(167, 26);
            this.toolStripMenuItem4.Text = "缺陷图1";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(167, 26);
            this.toolStripMenuItem5.Text = "缺陷图2";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(167, 26);
            this.toolStripMenuItem6.Text = "中间处理图";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmiDrawROI
            // 
            this.tsmiDrawROI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDrawCircle,
            this.tsmiDrawRectangle,
            this.tsmiDrawPolygon});
            this.tsmiDrawROI.Name = "tsmiDrawROI";
            this.tsmiDrawROI.Size = new System.Drawing.Size(153, 24);
            this.tsmiDrawROI.Text = "画ROI";
            // 
            // tsmiDrawCircle
            // 
            this.tsmiDrawCircle.Name = "tsmiDrawCircle";
            this.tsmiDrawCircle.Size = new System.Drawing.Size(137, 26);
            this.tsmiDrawCircle.Text = "圆形";
            this.tsmiDrawCircle.Click += new System.EventHandler(this.tsmiDrawCircle_Click);
            // 
            // tsmiDrawRectangle
            // 
            this.tsmiDrawRectangle.Name = "tsmiDrawRectangle";
            this.tsmiDrawRectangle.Size = new System.Drawing.Size(137, 26);
            this.tsmiDrawRectangle.Text = "矩形";
            this.tsmiDrawRectangle.Click += new System.EventHandler(this.tsmiDrawRectangle_Click);
            // 
            // tsmiDrawPolygon
            // 
            this.tsmiDrawPolygon.Name = "tsmiDrawPolygon";
            this.tsmiDrawPolygon.Size = new System.Drawing.Size(137, 26);
            this.tsmiDrawPolygon.Text = "多边形";
            this.tsmiDrawPolygon.Click += new System.EventHandler(this.tsmiDrawPolygon_Click);
            // 
            // HyDisplayPanel
            // 
            this.HyDisplayPanel.BackColor = System.Drawing.Color.Black;
            this.HyDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HyDisplayPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.HyDisplayPanel.Location = new System.Drawing.Point(0, 36);
            this.HyDisplayPanel.Name = "HyDisplayPanel";
            this.HyDisplayPanel.Size = new System.Drawing.Size(849, 581);
            this.HyDisplayPanel.TabIndex = 5;
            // 
            // HyImageDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HyDisplayPanel);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "HyImageDisplayControl";
            this.Size = new System.Drawing.Size(849, 643);
            this.Load += new System.EventHandler(this.HyImageDisplayControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportImg;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportImg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayOriImg;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayDefectImg;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrawROI;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrawCircle;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrawRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrawPolygon;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 显示ROIToolStripMenuItem;
        //private HyInspect.Base.UIL.DispalyPanel HyDisplayPanel;
        HyVision.Tools.ImageDisplay 
    }
}
