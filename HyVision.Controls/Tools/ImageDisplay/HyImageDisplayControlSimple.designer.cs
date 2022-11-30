
namespace HyVision.Tools.ImageDisplay
{
    partial class HyImageDisplayControlSimple
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fitWindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveImageToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResultImageToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitWindToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveImageToFileToolStripMenuItem,
            this.saveResultImageToFileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 98);
            // 
            // fitWindToolStripMenuItem
            // 
            this.fitWindToolStripMenuItem.Name = "fitWindToolStripMenuItem";
            this.fitWindToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fitWindToolStripMenuItem.Text = "适应图像";
            this.fitWindToolStripMenuItem.Click += new System.EventHandler(this.fitWindToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // saveImageToFileToolStripMenuItem
            // 
            this.saveImageToFileToolStripMenuItem.Name = "saveImageToFileToolStripMenuItem";
            this.saveImageToFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveImageToFileToolStripMenuItem.Text = "保存图像";
            this.saveImageToFileToolStripMenuItem.Click += new System.EventHandler(this.saveImageToFileToolStripMenuItem_Click);
            // 
            // saveResultImageToFileToolStripMenuItem
            // 
            this.saveResultImageToFileToolStripMenuItem.Name = "saveResultImageToFileToolStripMenuItem";
            this.saveResultImageToFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveResultImageToFileToolStripMenuItem.Text = "保存结果图像";
            this.saveResultImageToFileToolStripMenuItem.Click += new System.EventHandler(this.saveResultImageToFileToolStripMenuItem_Click);
            // 
            // HyImageDisplayControlSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "HyImageDisplayControlSimple";
            this.Size = new System.Drawing.Size(281, 198);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fitWindToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveResultImageToFileToolStripMenuItem;
    }
}
