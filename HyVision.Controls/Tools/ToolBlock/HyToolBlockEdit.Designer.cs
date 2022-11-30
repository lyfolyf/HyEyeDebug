
namespace HyVision.Tools.ToolBlock
{
    partial class HyToolBlockEdit
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
            this.tsmiAddInt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDouble = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddString = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDatetime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddHyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeView1 = new HyVision.Controls.HyTreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.inputEdit = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.outputEdit = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbImages = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddInt,
            this.tsmiAddDouble,
            this.tsmiAddString,
            this.tsmiAddDatetime,
            this.tsmiAddHyImage,
            this.tsmiDelLink,
            this.tsmiDelete,
            this.toolStripSeparator1,
            this.tsmiMoveUp,
            this.tsmiMoveDown,
            this.tsmiRename});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(305, 278);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiAddInt
            // 
            this.tsmiAddInt.Name = "tsmiAddInt";
            this.tsmiAddInt.Size = new System.Drawing.Size(304, 24);
            this.tsmiAddInt.Text = "新增 System.Int";
            this.tsmiAddInt.Click += new System.EventHandler(this.tsmiAddInt_Click);
            // 
            // tsmiAddDouble
            // 
            this.tsmiAddDouble.Name = "tsmiAddDouble";
            this.tsmiAddDouble.Size = new System.Drawing.Size(304, 24);
            this.tsmiAddDouble.Text = "新增 System.Double";
            this.tsmiAddDouble.Click += new System.EventHandler(this.tsmiAddDouble_Click);
            // 
            // tsmiAddString
            // 
            this.tsmiAddString.Name = "tsmiAddString";
            this.tsmiAddString.Size = new System.Drawing.Size(304, 24);
            this.tsmiAddString.Text = "新增 System.String";
            this.tsmiAddString.Click += new System.EventHandler(this.tsmiAddString_Click);
            // 
            // tsmiAddDatetime
            // 
            this.tsmiAddDatetime.Name = "tsmiAddDatetime";
            this.tsmiAddDatetime.Size = new System.Drawing.Size(304, 24);
            this.tsmiAddDatetime.Text = "新增 System.Datetime";
            this.tsmiAddDatetime.Click += new System.EventHandler(this.tsmiAddDatetime_Click);
            // 
            // tsmiAddHyImage
            // 
            this.tsmiAddHyImage.Name = "tsmiAddHyImage";
            this.tsmiAddHyImage.Size = new System.Drawing.Size(304, 24);
            this.tsmiAddHyImage.Text = "新增 HyVision.Models.HyImage";
            this.tsmiAddHyImage.Click += new System.EventHandler(this.tsmiAddHyImage_Click);
            // 
            // tsmiDelLink
            // 
            this.tsmiDelLink.Name = "tsmiDelLink";
            this.tsmiDelLink.Size = new System.Drawing.Size(304, 24);
            this.tsmiDelLink.Text = "取消链接";
            this.tsmiDelLink.Click += new System.EventHandler(this.tsmiDelLink_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(304, 24);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(301, 6);
            // 
            // tsmiMoveUp
            // 
            this.tsmiMoveUp.Name = "tsmiMoveUp";
            this.tsmiMoveUp.Size = new System.Drawing.Size(304, 24);
            this.tsmiMoveUp.Text = "上移";
            this.tsmiMoveUp.Click += new System.EventHandler(this.tsmiMoveUp_Click);
            // 
            // tsmiMoveDown
            // 
            this.tsmiMoveDown.Name = "tsmiMoveDown";
            this.tsmiMoveDown.Size = new System.Drawing.Size(304, 24);
            this.tsmiMoveDown.Text = "下移";
            this.tsmiMoveDown.Click += new System.EventHandler(this.tsmiMoveDown_Click);
            // 
            // tsmiRename
            // 
            this.tsmiRename.Name = "tsmiRename";
            this.tsmiRename.Size = new System.Drawing.Size(304, 24);
            this.tsmiRename.Text = "重命名";
            this.tsmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.cmbImages);
            this.splitContainer1.Size = new System.Drawing.Size(855, 524);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(418, 524);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(410, 495);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工具";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedNode = null;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(404, 489);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(410, 495);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输入/输出";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.inputEdit);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.outputEdit);
            this.splitContainer2.Size = new System.Drawing.Size(410, 495);
            this.splitContainer2.SplitterDistance = 207;
            this.splitContainer2.TabIndex = 0;
            // 
            // inputEdit
            // 
            this.inputEdit.DefaultNameHeader = "Input";
            this.inputEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputEdit.Location = new System.Drawing.Point(0, 0);
            this.inputEdit.Name = "inputEdit";
            this.inputEdit.Size = new System.Drawing.Size(410, 207);
            this.inputEdit.TabIndex = 0;
            this.inputEdit.Text = "输入";
            // 
            // outputEdit
            // 
            this.outputEdit.DefaultNameHeader = "Output";
            this.outputEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputEdit.Location = new System.Drawing.Point(0, 0);
            this.outputEdit.Name = "outputEdit";
            this.outputEdit.Size = new System.Drawing.Size(410, 284);
            this.outputEdit.TabIndex = 1;
            this.outputEdit.Text = "输出";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(433, 501);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cmbImages
            // 
            this.cmbImages.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImages.FormattingEnabled = true;
            this.cmbImages.Location = new System.Drawing.Point(0, 0);
            this.cmbImages.Name = "cmbImages";
            this.cmbImages.Size = new System.Drawing.Size(433, 23);
            this.cmbImages.TabIndex = 1;
            this.cmbImages.SelectedIndexChanged += new System.EventHandler(this.cmbImages_SelectedIndexChanged);
            // 
            // HyToolBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "HyToolBlockEdit";
            this.Size = new System.Drawing.Size(855, 573);
            this.Load += new System.EventHandler(this.HyToolBlockEdit_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HyVision.Controls.HyTreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddInt;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDouble;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddString;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDatetime;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMoveUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiMoveDown;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ComboBox cmbImages;
        private TerminalCollection.HyTerminalCollectionEdit inputEdit;
        private TerminalCollection.HyTerminalCollectionEdit outputEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiRename;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddHyImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelLink;
    }
}
