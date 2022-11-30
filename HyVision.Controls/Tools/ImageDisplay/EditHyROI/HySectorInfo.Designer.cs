
namespace HyVision.Tools.ImageDisplay
{
    partial class HySectorInfo
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
            this.nudEndAngle = new System.Windows.Forms.NumericUpDown();
            this.nudStartAngle = new System.Windows.Forms.NumericUpDown();
            this.nudRadius = new System.Windows.Forms.NumericUpDown();
            this.nudCenterY = new System.Windows.Forms.NumericUpDown();
            this.nudCenterX = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterX)).BeginInit();
            this.SuspendLayout();
            // 
            // nudEndAngle
            // 
            this.nudEndAngle.DecimalPlaces = 3;
            this.nudEndAngle.Font = new System.Drawing.Font("宋体", 11F);
            this.nudEndAngle.Location = new System.Drawing.Point(127, 264);
            this.nudEndAngle.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudEndAngle.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudEndAngle.Name = "nudEndAngle";
            this.nudEndAngle.Size = new System.Drawing.Size(189, 28);
            this.nudEndAngle.TabIndex = 7;
            this.nudEndAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudEndAngle.ValueChanged += new System.EventHandler(this.nudEndAngle_ValueChanged);
            // 
            // nudStartAngle
            // 
            this.nudStartAngle.DecimalPlaces = 3;
            this.nudStartAngle.Font = new System.Drawing.Font("宋体", 11F);
            this.nudStartAngle.Location = new System.Drawing.Point(127, 203);
            this.nudStartAngle.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudStartAngle.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudStartAngle.Name = "nudStartAngle";
            this.nudStartAngle.Size = new System.Drawing.Size(189, 28);
            this.nudStartAngle.TabIndex = 8;
            this.nudStartAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudStartAngle.ValueChanged += new System.EventHandler(this.nudStartAngle_ValueChanged);
            // 
            // nudRadius
            // 
            this.nudRadius.DecimalPlaces = 3;
            this.nudRadius.Font = new System.Drawing.Font("宋体", 11F);
            this.nudRadius.Location = new System.Drawing.Point(127, 142);
            this.nudRadius.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudRadius.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudRadius.Name = "nudRadius";
            this.nudRadius.Size = new System.Drawing.Size(189, 28);
            this.nudRadius.TabIndex = 9;
            this.nudRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRadius.ValueChanged += new System.EventHandler(this.nudRadius_ValueChanged);
            // 
            // nudCenterY
            // 
            this.nudCenterY.DecimalPlaces = 3;
            this.nudCenterY.Font = new System.Drawing.Font("宋体", 11F);
            this.nudCenterY.Location = new System.Drawing.Point(127, 81);
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
            this.nudCenterY.Size = new System.Drawing.Size(189, 28);
            this.nudCenterY.TabIndex = 10;
            this.nudCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCenterY.ValueChanged += new System.EventHandler(this.nudCenterY_ValueChanged);
            // 
            // nudCenterX
            // 
            this.nudCenterX.DecimalPlaces = 3;
            this.nudCenterX.Font = new System.Drawing.Font("宋体", 11F);
            this.nudCenterX.Location = new System.Drawing.Point(127, 20);
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
            this.nudCenterX.Size = new System.Drawing.Size(189, 28);
            this.nudCenterX.TabIndex = 11;
            this.nudCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCenterX.ValueChanged += new System.EventHandler(this.nudCenterX_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(3, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "EndAngle：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(-1, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "StartAngle：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(13, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Radius：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(8, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "CenterY：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "CenterX：";
            // 
            // HySectorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudEndAngle);
            this.Controls.Add(this.nudStartAngle);
            this.Controls.Add(this.nudRadius);
            this.Controls.Add(this.nudCenterY);
            this.Controls.Add(this.nudCenterX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "HySectorInfo";
            this.Size = new System.Drawing.Size(351, 329);
            ((System.ComponentModel.ISupportInitialize)(this.nudEndAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudEndAngle;
        private System.Windows.Forms.NumericUpDown nudStartAngle;
        private System.Windows.Forms.NumericUpDown nudRadius;
        private System.Windows.Forms.NumericUpDown nudCenterY;
        private System.Windows.Forms.NumericUpDown nudCenterX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
