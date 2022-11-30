

namespace HyVision.Tools.ImageDisplay
{
    partial class HyImageDisplayControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //private DispalyPanel HyDisplayPanel;
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
            this.tsBottomTool = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiImportImg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportImg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDisplayOriImg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteROI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBandingShowContent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsTopTool = new System.Windows.Forms.ToolStrip();
            this.tsbClearImage = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadImage = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbRoiType = new System.Windows.Forms.ToolStripComboBox();
            this.tscbROIColor = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNewHyROI = new System.Windows.Forms.ToolStripButton();
            this.HyDisplayPanel = new HyVision.Tools.ImageDisplay.DispalyPanel();
            this.tsBottomTool.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tsTopTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsBottomTool
            // 
            this.tsBottomTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsBottomTool.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsBottomTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.tsBottomTool.Location = new System.Drawing.Point(0, 480);
            this.tsBottomTool.Name = "tsBottomTool";
            this.tsBottomTool.Size = new System.Drawing.Size(589, 25);
            this.tsBottomTool.TabIndex = 4;
            this.tsBottomTool.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(351, 22);
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
            this.toolStripSeparator4,
            this.tsmiDeleteROI,
            this.tsmBandingShowContent});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 126);
            // 
            // tsmiImportImg
            // 
            this.tsmiImportImg.Name = "tsmiImportImg";
            this.tsmiImportImg.Size = new System.Drawing.Size(148, 22);
            this.tsmiImportImg.Text = "导入图片";
            this.tsmiImportImg.Click += new System.EventHandler(this.tsmiImportImg_Click);
            // 
            // tsmiExportImg
            // 
            this.tsmiExportImg.Name = "tsmiExportImg";
            this.tsmiExportImg.Size = new System.Drawing.Size(148, 22);
            this.tsmiExportImg.Text = "导出图片";
            this.tsmiExportImg.Click += new System.EventHandler(this.tsmiExportImg_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmiDisplayOriImg
            // 
            this.tsmiDisplayOriImg.Name = "tsmiDisplayOriImg";
            this.tsmiDisplayOriImg.Size = new System.Drawing.Size(148, 22);
            this.tsmiDisplayOriImg.Text = "显示原图";
            this.tsmiDisplayOriImg.Click += new System.EventHandler(this.tsmiDisplayOriImg_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmiDeleteROI
            // 
            this.tsmiDeleteROI.Name = "tsmiDeleteROI";
            this.tsmiDeleteROI.Size = new System.Drawing.Size(148, 22);
            this.tsmiDeleteROI.Text = "删除选中ROI";
            this.tsmiDeleteROI.Click += new System.EventHandler(this.tsmiDeleteROI_Click);
            // 
            // tsmBandingShowContent
            // 
            this.tsmBandingShowContent.Name = "tsmBandingShowContent";
            this.tsmBandingShowContent.Size = new System.Drawing.Size(148, 22);
            this.tsmBandingShowContent.Text = "绑定显示内容";
            this.tsmBandingShowContent.Click += new System.EventHandler(this.tsmBandingShowContent_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 40);
            // 
            // tsTopTool
            // 
            this.tsTopTool.AutoSize = false;
            this.tsTopTool.BackColor = System.Drawing.Color.Aquamarine;
            this.tsTopTool.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsTopTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClearImage,
            this.toolStripSeparator1,
            this.tsbLoadImage,
            this.tsbSaveImage,
            this.toolStripSeparator5,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.toolStripSeparator2,
            this.tscbRoiType,
            this.tscbROIColor,
            this.tsbNewHyROI});
            this.tsTopTool.Location = new System.Drawing.Point(0, 0);
            this.tsTopTool.Name = "tsTopTool";
            this.tsTopTool.Size = new System.Drawing.Size(589, 32);
            this.tsTopTool.TabIndex = 3;
            this.tsTopTool.Text = "toolStrip1";
            // 
            // tsbClearImage
            // 
            this.tsbClearImage.AutoSize = false;
            this.tsbClearImage.BackColor = System.Drawing.Color.Aquamarine;
            this.tsbClearImage.Checked = true;
            this.tsbClearImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbClearImage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tsbClearImage.ImageTransparentColor = System.Drawing.Color.Lime;
            this.tsbClearImage.Name = "tsbClearImage";
            this.tsbClearImage.Size = new System.Drawing.Size(40, 37);
            this.tsbClearImage.Text = "清除";
            this.tsbClearImage.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbClearImage.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbClearImage.ToolTipText = "清除图片";
            this.tsbClearImage.Click += new System.EventHandler(this.tsbClearImage_Click);
            // 
            // tsbLoadImage
            // 
            this.tsbLoadImage.Checked = true;
            this.tsbLoadImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbLoadImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLoadImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadImage.Image")));
            this.tsbLoadImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadImage.Name = "tsbLoadImage";
            this.tsbLoadImage.Size = new System.Drawing.Size(60, 29);
            this.tsbLoadImage.Text = "加载图片";
            this.tsbLoadImage.ToolTipText = "加载图片";
            this.tsbLoadImage.Click += new System.EventHandler(this.tsbLoadImage_Click);
            // 
            // tsbSaveImage
            // 
            this.tsbSaveImage.Checked = true;
            this.tsbSaveImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSaveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSaveImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveImage.Image")));
            this.tsbSaveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveImage.Name = "tsbSaveImage";
            this.tsbSaveImage.Size = new System.Drawing.Size(60, 29);
            this.tsbSaveImage.Text = "保存图片";
            this.tsbSaveImage.ToolTipText = "保存图片";
            this.tsbSaveImage.Click += new System.EventHandler(this.tsbSaveImage_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(12, 40);
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.Checked = true;
            this.tsbZoomIn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomIn.Image")));
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(36, 29);
            this.tsbZoomIn.Text = "放大";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.Checked = true;
            this.tsbZoomOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomOut.Image")));
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(36, 29);
            this.tsbZoomOut.Text = "缩小";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 40);
            // 
            // tscbRoiType
            // 
            this.tscbRoiType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbRoiType.Name = "tscbRoiType";
            this.tscbRoiType.Size = new System.Drawing.Size(92, 32);
            // 
            // tscbROIColor
            // 
            this.tscbROIColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbROIColor.Name = "tscbROIColor";
            this.tscbROIColor.Size = new System.Drawing.Size(92, 32);
            // 
            // tsbNewHyROI
            // 
            this.tsbNewHyROI.Checked = true;
            this.tsbNewHyROI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbNewHyROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNewHyROI.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewHyROI.Image")));
            this.tsbNewHyROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewHyROI.Name = "tsbNewHyROI";
            this.tsbNewHyROI.Size = new System.Drawing.Size(58, 29);
            this.tsbNewHyROI.Text = "新增ROI";
            this.tsbNewHyROI.ToolTipText = "测试按钮";
            this.tsbNewHyROI.Click += new System.EventHandler(this.tsbNewHyROI_Click);
            // 
            // HyDisplayPanel
            // 
            this.HyDisplayPanel.BackColor = System.Drawing.Color.Black;
            this.HyDisplayPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.HyDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HyDisplayPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.HyDisplayPanel.Location = new System.Drawing.Point(0, 32);
            this.HyDisplayPanel.Margin = new System.Windows.Forms.Padding(2);
            this.HyDisplayPanel.Name = "HyDisplayPanel";
            this.HyDisplayPanel.Size = new System.Drawing.Size(589, 448);
            this.HyDisplayPanel.TabIndex = 5;
            // 
            // HyImageDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HyDisplayPanel);
            this.Controls.Add(this.tsBottomTool);
            this.Controls.Add(this.tsTopTool);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HyImageDisplayControl";
            this.Size = new System.Drawing.Size(589, 505);
            this.Load += new System.EventHandler(this.HyImageDisplayControl_Load);
            this.tsBottomTool.ResumeLayout(false);
            this.tsBottomTool.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tsTopTool.ResumeLayout(false);
            this.tsTopTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsBottomTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportImg;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportImg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayOriImg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip tsTopTool;
        private System.Windows.Forms.ToolStripButton tsbClearImage;
        private System.Windows.Forms.ToolStripButton tsbLoadImage;
        private System.Windows.Forms.ToolStripButton tsbSaveImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox tscbRoiType;
        private System.Windows.Forms.ToolStripButton tsbNewHyROI;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteROI;
        private System.Windows.Forms.ToolStripComboBox tscbROIColor;
        private System.Windows.Forms.ToolStripMenuItem tsmBandingShowContent;
        public DispalyPanel HyDisplayPanel;
    }
}
