
namespace HalconSDK.Engine.UI
{
    partial class HyROIOperation
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
            this.tsmiDeleteROI1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gbIndexName = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbxCollection1 = new System.Windows.Forms.ListBox();
            this.lbxCollection2 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stmiDeleteROI2 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbResult = new System.Windows.Forms.Label();
            this.lbUnion2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbUnion1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.gbIndexName.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteROI1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiDeleteROI1
            // 
            this.tsmiDeleteROI1.Name = "tsmiDeleteROI1";
            this.tsmiDeleteROI1.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteROI1.Text = "删除选中";
            this.tsmiDeleteROI1.Click += new System.EventHandler(this.tsmiDeleteROI1_Click);
            // 
            // gbIndexName
            // 
            this.gbIndexName.Controls.Add(this.tableLayoutPanel1);
            this.gbIndexName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIndexName.Font = new System.Drawing.Font("宋体", 11F);
            this.gbIndexName.Location = new System.Drawing.Point(0, 0);
            this.gbIndexName.Margin = new System.Windows.Forms.Padding(2);
            this.gbIndexName.Name = "gbIndexName";
            this.gbIndexName.Padding = new System.Windows.Forms.Padding(2);
            this.gbIndexName.Size = new System.Drawing.Size(575, 130);
            this.gbIndexName.TabIndex = 16;
            this.gbIndexName.TabStop = false;
            this.gbIndexName.Text = "序号1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.91297F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.91297F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.91297F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.565195F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.69588F));
            this.tableLayoutPanel1.Controls.Add(this.lbxCollection1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbxCollection2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbUnion2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbUnion1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(571, 109);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // lbxCollection1
            // 
            this.lbxCollection1.ContextMenuStrip = this.contextMenuStrip1;
            this.lbxCollection1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbxCollection1.FormattingEnabled = true;
            this.lbxCollection1.ItemHeight = 15;
            this.lbxCollection1.Location = new System.Drawing.Point(4, 34);
            this.lbxCollection1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.lbxCollection1.Name = "lbxCollection1";
            this.lbxCollection1.Size = new System.Drawing.Size(128, 64);
            this.lbxCollection1.TabIndex = 1;
            // 
            // lbxCollection2
            // 
            this.lbxCollection2.ContextMenuStrip = this.contextMenuStrip2;
            this.lbxCollection2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbxCollection2.FormattingEnabled = true;
            this.lbxCollection2.ItemHeight = 15;
            this.lbxCollection2.Location = new System.Drawing.Point(276, 34);
            this.lbxCollection2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.lbxCollection2.Name = "lbxCollection2";
            this.lbxCollection2.Size = new System.Drawing.Size(128, 64);
            this.lbxCollection2.TabIndex = 1;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stmiDeleteROI2});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            this.contextMenuStrip2.Text = "删除选中";
            // 
            // stmiDeleteROI2
            // 
            this.stmiDeleteROI2.Name = "stmiDeleteROI2";
            this.stmiDeleteROI2.Size = new System.Drawing.Size(124, 22);
            this.stmiDeleteROI2.Text = "删除选中";
            this.stmiDeleteROI2.Click += new System.EventHandler(this.stmiDeleteROI2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(410, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(50, 105);
            this.panel1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 15F);
            this.label10.Location = new System.Drawing.Point(14, 44);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "=";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(464, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(105, 105);
            this.panel2.TabIndex = 1;
            // 
            // lbResult
            // 
            this.lbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbResult.BackColor = System.Drawing.Color.MistyRose;
            this.lbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbResult.Font = new System.Drawing.Font("宋体", 12F);
            this.lbResult.Location = new System.Drawing.Point(4, 41);
            this.lbResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(97, 26);
            this.lbResult.TabIndex = 8;
            this.lbResult.Text = "Result1";
            this.lbResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnion2
            // 
            this.lbUnion2.BackColor = System.Drawing.Color.LightGreen;
            this.lbUnion2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbUnion2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbUnion2.Font = new System.Drawing.Font("宋体", 10F);
            this.lbUnion2.Location = new System.Drawing.Point(276, 11);
            this.lbUnion2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUnion2.Name = "lbUnion2";
            this.lbUnion2.Size = new System.Drawing.Size(128, 21);
            this.lbUnion2.TabIndex = 7;
            this.lbUnion2.Text = "并集1";
            this.lbUnion2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbOperation);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(138, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 2);
            this.panel3.Size = new System.Drawing.Size(132, 105);
            this.panel3.TabIndex = 6;
            // 
            // cbOperation
            // 
            this.cbOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOperation.Font = new System.Drawing.Font("宋体", 12F);
            this.cbOperation.FormattingEnabled = true;
            this.cbOperation.Items.AddRange(new object[] {
            "交集",
            "并集",
            "差集"});
            this.cbOperation.Location = new System.Drawing.Point(10, 52);
            this.cbOperation.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.cbOperation.Name = "cbOperation";
            this.cbOperation.Size = new System.Drawing.Size(114, 24);
            this.cbOperation.TabIndex = 3;
            this.cbOperation.Text = "交集";
            this.cbOperation.SelectedIndexChanged += new System.EventHandler(this.cbOperation_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(10, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "集合操作";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnion1
            // 
            this.lbUnion1.BackColor = System.Drawing.Color.LightGreen;
            this.lbUnion1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbUnion1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbUnion1.Font = new System.Drawing.Font("宋体", 10F);
            this.lbUnion1.Location = new System.Drawing.Point(4, 11);
            this.lbUnion1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUnion1.Name = "lbUnion1";
            this.lbUnion1.Size = new System.Drawing.Size(128, 21);
            this.lbUnion1.TabIndex = 7;
            this.lbUnion1.Text = "并集1";
            this.lbUnion1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HyROIOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbIndexName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HyROIOperation";
            this.Size = new System.Drawing.Size(575, 130);
            this.Load += new System.EventHandler(this.HyROIOperation_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.gbIndexName.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteROI1;
        private System.Windows.Forms.GroupBox gbIndexName;
        private System.Windows.Forms.ListBox lbxCollection1;
        private System.Windows.Forms.ListBox lbxCollection2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbOperation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbUnion1;
        private System.Windows.Forms.Label lbUnion2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem stmiDeleteROI2;
    }
}
