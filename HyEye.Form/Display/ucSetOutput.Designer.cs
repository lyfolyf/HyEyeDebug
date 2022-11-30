
namespace HyEye.WForm
{
    partial class ucSetOutput
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
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.nudIndex = new System.Windows.Forms.NumericUpDown();
            this.rbIndex = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.gbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.nudIndex);
            this.gbMain.Controls.Add(this.rbIndex);
            this.gbMain.Controls.Add(this.rbAll);
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(416, 55);
            this.gbMain.TabIndex = 1;
            this.gbMain.TabStop = false;
            this.gbMain.Text = "任务";
            // 
            // nudIndex
            // 
            this.nudIndex.Location = new System.Drawing.Point(304, 23);
            this.nudIndex.Name = "nudIndex";
            this.nudIndex.Size = new System.Drawing.Size(42, 21);
            this.nudIndex.TabIndex = 4;
            this.nudIndex.ValueChanged += new System.EventHandler(this.nudIndex_ValueChanged);
            // 
            // rbIndex
            // 
            this.rbIndex.AutoSize = true;
            this.rbIndex.Location = new System.Drawing.Point(229, 25);
            this.rbIndex.Name = "rbIndex";
            this.rbIndex.Size = new System.Drawing.Size(71, 16);
            this.rbIndex.TabIndex = 0;
            this.rbIndex.TabStop = true;
            this.rbIndex.Text = "索引输出";
            this.rbIndex.UseVisualStyleBackColor = true;
            this.rbIndex.CheckedChanged += new System.EventHandler(this.rbIndex_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(85, 25);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(71, 16);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "全部输出";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // ucSetOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMain);
            this.Name = "ucSetOutput";
            this.Size = new System.Drawing.Size(416, 55);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.RadioButton rbIndex;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.NumericUpDown nudIndex;
    }
}
