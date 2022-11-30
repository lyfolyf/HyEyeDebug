namespace HyEye.WForm
{
    partial class FormLog
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbLogLevel = new System.Windows.Forms.GTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLogLevel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLogLevel
            // 
            this.tbLogLevel.Controls.Add(this.tabPage1);
            this.tbLogLevel.Controls.Add(this.tabPage2);
            this.tbLogLevel.Controls.Add(this.tabPage3);
            this.tbLogLevel.Controls.Add(this.tabPage4);
            this.tbLogLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbLogLevel.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbLogLevel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tbLogLevel.ItemSize = new System.Drawing.Size(150, 40);
            this.tbLogLevel.Location = new System.Drawing.Point(0, 0);
            this.tbLogLevel.Name = "tbLogLevel";
            this.tbLogLevel.SelectedIndex = 0;
            this.tbLogLevel.Size = new System.Drawing.Size(1050, 40);
            this.tbLogLevel.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbLogLevel.TabIndex = 0;
            this.tbLogLevel.SelectedIndexChanged += new System.EventHandler(this.LogLevelDisplayedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1050, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全部";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.ForeColor = System.Drawing.Color.LightGreen;
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(450, 230);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.ForeColor = System.Drawing.Color.Yellow;
            this.tabPage3.Location = new System.Drawing.Point(0, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(450, 230);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "警告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.ForeColor = System.Drawing.Color.Red;
            this.tabPage4.Location = new System.Drawing.Point(0, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(450, 230);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "错误";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rtbLogs
            // 
            this.rtbLogs.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogs.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbLogs.Location = new System.Drawing.Point(0, 40);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.Size = new System.Drawing.Size(1050, 650);
            this.rtbLogs.TabIndex = 2;
            this.rtbLogs.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(108, 24);
            this.tsmiClear.Text = "清空";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // FormLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1050, 690);
            this.Controls.Add(this.rtbLogs);
            this.Controls.Add(this.tbLogLevel);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HideOnClose = true;
            this.Name = "FormLog";
            this.Text = "日志";
            this.tbLogLevel.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GTabControl tbLogLevel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
    }
}

