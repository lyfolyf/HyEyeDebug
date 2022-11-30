
namespace HalconSDK.Engine.UI
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
            this.nudPointY = new System.Windows.Forms.NumericUpDown();
            this.nudPointX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tvwRoiPointsList = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudPointY
            // 
            this.nudPointY.DecimalPlaces = 3;
            this.nudPointY.Font = new System.Drawing.Font("宋体", 11F);
            this.nudPointY.Location = new System.Drawing.Point(58, 92);
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
            this.nudPointY.Size = new System.Drawing.Size(132, 24);
            this.nudPointY.TabIndex = 14;
            this.nudPointY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPointY.ValueChanged += new System.EventHandler(this.nudPointY_ValueChanged);
            // 
            // nudPointX
            // 
            this.nudPointX.DecimalPlaces = 3;
            this.nudPointX.Font = new System.Drawing.Font("宋体", 11F);
            this.nudPointX.Location = new System.Drawing.Point(58, 31);
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
            this.nudPointX.Size = new System.Drawing.Size(132, 24);
            this.nudPointX.TabIndex = 15;
            this.nudPointX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPointX.ValueChanged += new System.EventHandler(this.nudPointX_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(14, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Y：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "X：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudPointX);
            this.groupBox1.Controls.Add(this.nudPointY);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox1.Location = new System.Drawing.Point(148, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 147);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "点集列表：";
            // 
            // tvwRoiPointsList
            // 
            this.tvwRoiPointsList.Font = new System.Drawing.Font("宋体", 12F);
            this.tvwRoiPointsList.ItemHeight = 20;
            this.tvwRoiPointsList.Location = new System.Drawing.Point(3, 37);
            this.tvwRoiPointsList.Name = "tvwRoiPointsList";
            this.tvwRoiPointsList.Size = new System.Drawing.Size(139, 289);
            this.tvwRoiPointsList.TabIndex = 18;
            this.tvwRoiPointsList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwRoiPointsList_NodeMouseClick);
            // 
            // HyPolygonInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tvwRoiPointsList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.NumericUpDown nudPointY;
        private System.Windows.Forms.NumericUpDown nudPointX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvwRoiPointsList;
    }
}
