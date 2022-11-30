
namespace HalconSDK.Engine.UI
{
    partial class HyCircleInfo
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
            this.nudRadius = new System.Windows.Forms.NumericUpDown();
            this.nudCenterY = new System.Windows.Forms.NumericUpDown();
            this.nudCenterX = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterX)).BeginInit();
            this.SuspendLayout();
            // 
            // nudRadius
            // 
            this.nudRadius.DecimalPlaces = 3;
            this.nudRadius.Font = new System.Drawing.Font("宋体", 11F);
            this.nudRadius.Location = new System.Drawing.Point(95, 114);
            this.nudRadius.Margin = new System.Windows.Forms.Padding(2);
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
            this.nudRadius.Size = new System.Drawing.Size(142, 24);
            this.nudRadius.TabIndex = 5;
            this.nudRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRadius.ValueChanged += new System.EventHandler(this.nudRadius_ValueChanged);
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
            this.nudCenterY.TabIndex = 6;
            this.nudCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCenterY.ValueChanged += new System.EventHandler(this.nudCenterY_ValueChanged);
            // 
            // nudCenterX
            // 
            this.nudCenterX.DecimalPlaces = 3;
            this.nudCenterX.Font = new System.Drawing.Font("宋体", 11F);
            this.nudCenterX.Location = new System.Drawing.Point(95, 16);
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
            this.nudCenterX.TabIndex = 7;
            this.nudCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCenterX.ValueChanged += new System.EventHandler(this.nudCenterX_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(14, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Radius：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "CenterY：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "CenterX：";
            // 
            // HyCircleInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.nudRadius);
            this.Controls.Add(this.nudCenterY);
            this.Controls.Add(this.nudCenterX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HyCircleInfo";
            this.Size = new System.Drawing.Size(263, 160);
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCenterX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudRadius;
        private System.Windows.Forms.NumericUpDown nudCenterY;
        private System.Windows.Forms.NumericUpDown nudCenterX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
