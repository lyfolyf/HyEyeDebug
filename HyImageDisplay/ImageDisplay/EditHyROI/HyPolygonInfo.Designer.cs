
namespace HyVision.Tools.ImageDisplay
{
    partial class HyPolygonInfo
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
            this.lbPoints = new System.Windows.Forms.ListBox();
            this.nudPointY = new System.Windows.Forms.NumericUpDown();
            this.nudPointX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPoints
            // 
            this.lbPoints.Font = new System.Drawing.Font("宋体", 12F);
            this.lbPoints.FormattingEnabled = true;
            this.lbPoints.ItemHeight = 20;
            this.lbPoints.Items.AddRange(new object[] {
            "Point1",
            "Point2"});
            this.lbPoints.Location = new System.Drawing.Point(3, 43);
            this.lbPoints.Name = "lbPoints";
            this.lbPoints.Size = new System.Drawing.Size(111, 284);
            this.lbPoints.TabIndex = 0;
            this.lbPoints.SelectedIndexChanged += new System.EventHandler(this.lbPoints_SelectedIndexChanged);
            // 
            // nudPointY
            // 
            this.nudPointY.DecimalPlaces = 3;
            this.nudPointY.Font = new System.Drawing.Font("宋体", 11F);
            this.nudPointY.Location = new System.Drawing.Point(59, 92);
            this.nudPointY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPointY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudPointY.Name = "nudPointY";
            this.nudPointY.Size = new System.Drawing.Size(149, 28);
            this.nudPointY.TabIndex = 14;
            this.nudPointY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPointY.ValueChanged += new System.EventHandler(this.nudPointY_ValueChanged);
            // 
            // nudPointX
            // 
            this.nudPointX.DecimalPlaces = 3;
            this.nudPointX.Font = new System.Drawing.Font("宋体", 11F);
            this.nudPointX.Location = new System.Drawing.Point(59, 31);
            this.nudPointX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPointX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudPointX.Name = "nudPointX";
            this.nudPointX.Size = new System.Drawing.Size(149, 28);
            this.nudPointX.TabIndex = 15;
            this.nudPointX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPointX.ValueChanged += new System.EventHandler(this.nudPointX_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(11, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 12;
            this.label2.Text = "Y：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "X：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "点列表：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudPointX);
            this.groupBox1.Controls.Add(this.nudPointY);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox1.Location = new System.Drawing.Point(120, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 147);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标";
            // 
            // HyPolygonInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbPoints);
            this.Name = "HyPolygonInfo";
            this.Size = new System.Drawing.Size(351, 329);
            ((System.ComponentModel.ISupportInitialize)(this.nudPointY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbPoints;
        private System.Windows.Forms.NumericUpDown nudPointY;
        private System.Windows.Forms.NumericUpDown nudPointX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
