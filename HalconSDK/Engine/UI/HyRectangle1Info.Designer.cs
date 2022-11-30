
namespace HalconSDK.Engine.UI
{
    partial class HyRectangle1Info
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudCenterX = new System.Windows.Forms.NumericUpDown();
            this.nudCenterY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(27, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Row1：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Column1：";
            // 
            // nudCenterX
            // 
            this.nudCenterX.DecimalPlaces = 3;
            this.nudCenterX.Font = new System.Drawing.Font("宋体", 11F);
            this.nudCenterX.Location = new System.Drawing.Point(95, 15);
            this.nudCenterX.Margin = new System.Windows.Forms.Padding(2);
            this.nudCenterX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCenterX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudCenterX.Name = "nudCenterX";
            this.nudCenterX.Size = new System.Drawing.Size(142, 24);
            this.nudCenterX.TabIndex = 1;
            this.nudCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCenterX.ValueChanged += new System.EventHandler(this.nudCenterX_ValueChanged);
            // 
            // nudCenterY
            // 
            this.nudCenterY.DecimalPlaces = 3;
            this.nudCenterY.Font = new System.Drawing.Font("宋体", 11F);
            this.nudCenterY.Location = new System.Drawing.Point(95, 65);
            this.nudCenterY.Margin = new System.Windows.Forms.Padding(2);
            this.nudCenterY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCenterY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudCenterY.Name = "nudCenterY";
            this.nudCenterY.Size = new System.Drawing.Size(142, 24);
            this.nudCenterY.TabIndex = 1;
            this.nudCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCenterY.ValueChanged += new System.EventHandler(this.nudCenterY_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(34, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Row2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(10, 170);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Column2:";
            // 
            // nudWidth
            // 
            this.nudWidth.DecimalPlaces = 3;
            this.nudWidth.Font = new System.Drawing.Font("宋体", 11F);
            this.nudWidth.Location = new System.Drawing.Point(95, 115);
            this.nudWidth.Margin = new System.Windows.Forms.Padding(2);
            this.nudWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(142, 24);
            this.nudWidth.TabIndex = 1;
            this.nudWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudHeight
            // 
            this.nudHeight.DecimalPlaces = 3;
            this.nudHeight.Font = new System.Drawing.Font("宋体", 11F);
            this.nudHeight.Location = new System.Drawing.Point(95, 165);
            this.nudHeight.Margin = new System.Windows.Forms.Padding(2);
            this.nudHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(142, 24);
            this.nudHeight.TabIndex = 1;
            this.nudHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
            // 
            // HyRectangle1Info
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.nudCenterY);
            this.Controls.Add(this.nudCenterX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HyRectangle1Info";
            this.Size = new System.Drawing.Size(263, 210);
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCenterX;
        private System.Windows.Forms.NumericUpDown nudCenterY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
    }
}
