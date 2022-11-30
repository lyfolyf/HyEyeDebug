
namespace HyVision.Tools
{
    partial class BaseHyUserToolEdit<T>
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnRun = new System.Windows.Forms.ToolStripButton();
            this.tsBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbtnSaveWithImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnSaveWithoutImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnToolbox = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblRunTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblErrMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnRun,
            this.tsBtnOpen,
            this.toolStripSplitButton1,
            this.tsbtnToolbox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnRun
            // 
            this.tsBtnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRun.Enabled = false;
            this.tsBtnRun.Image = global::HyVision.Properties.Resources.运行;
            this.tsBtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRun.Name = "tsBtnRun";
            this.tsBtnRun.Size = new System.Drawing.Size(29, 24);
            this.tsBtnRun.Text = "运行";
            this.tsBtnRun.Click += new System.EventHandler(this.tsBtnRun_Click);
            // 
            // tsBtnOpen
            // 
            this.tsBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOpen.Image = global::HyVision.Properties.Resources.打开;
            this.tsBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOpen.Name = "tsBtnOpen";
            this.tsBtnOpen.Size = new System.Drawing.Size(29, 24);
            this.tsBtnOpen.Text = "打开";
            this.tsBtnOpen.Click += new System.EventHandler(this.tsBtnOpen_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSaveWithImage,
            this.tsbtnSaveWithoutImage});
            this.toolStripSplitButton1.Image = global::HyVision.Properties.Resources.保存;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(39, 24);
            this.toolStripSplitButton1.Text = "保存";
            // 
            // tsbtnSaveWithImage
            // 
            this.tsbtnSaveWithImage.Name = "tsbtnSaveWithImage";
            this.tsbtnSaveWithImage.Size = new System.Drawing.Size(227, 26);
            this.tsbtnSaveWithImage.Text = "保存完整工具";
            this.tsbtnSaveWithImage.Click += new System.EventHandler(this.tsbtnSaveWithImage_Click);
            // 
            // tsbtnSaveWithoutImage
            // 
            this.tsbtnSaveWithoutImage.Name = "tsbtnSaveWithoutImage";
            this.tsbtnSaveWithoutImage.Size = new System.Drawing.Size(227, 26);
            this.tsbtnSaveWithoutImage.Text = "保存不带图像的工具";
            this.tsbtnSaveWithoutImage.Click += new System.EventHandler(this.tsbtnSaveWithoutImage_Click);
            // 
            // tsbtnToolbox
            // 
            this.tsbtnToolbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnToolbox.Image = global::HyVision.Properties.Resources.工具箱;
            this.tsbtnToolbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnToolbox.Name = "tsbtnToolbox";
            this.tsbtnToolbox.Size = new System.Drawing.Size(29, 24);
            this.tsbtnToolbox.Text = "工具箱";
            this.tsbtnToolbox.Click += new System.EventHandler(this.tsbtnToolbox_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblRunTime,
            this.tslblErrMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblRunTime
            // 
            this.tslblRunTime.Name = "tslblRunTime";
            this.tslblRunTime.Size = new System.Drawing.Size(167, 20);
            this.tslblRunTime.Text = "toolStripStatusLabel1";
            // 
            // tslblErrMsg
            // 
            this.tslblErrMsg.ForeColor = System.Drawing.Color.Red;
            this.tslblErrMsg.Name = "tslblErrMsg";
            this.tslblErrMsg.Size = new System.Drawing.Size(167, 20);
            this.tslblErrMsg.Text = "toolStripStatusLabel2";
            // 
            // BaseHyUserToolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BaseHyUserToolEdit";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.BaseHyUserToolEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnRun;
        private System.Windows.Forms.ToolStripButton tsBtnOpen;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem tsbtnSaveWithImage;
        private System.Windows.Forms.ToolStripMenuItem tsbtnSaveWithoutImage;
        private System.Windows.Forms.ToolStripButton tsbtnToolbox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        protected System.Windows.Forms.ToolStripStatusLabel tslblRunTime;
        protected System.Windows.Forms.ToolStripStatusLabel tslblErrMsg;
    }
}
