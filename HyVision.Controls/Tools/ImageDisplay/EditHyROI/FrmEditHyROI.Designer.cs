
namespace HyVision.Tools.ImageDisplay
{
    partial class FrmEditHyROI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbRoiIndex = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRoiType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbRoiColor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxFill = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxVisible = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudLineWidth = new System.Windows.Forms.NumericUpDown();
            this.cbxAllVisible = new System.Windows.Forms.CheckBox();
            this.cbxAllFill = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "序号：";
            // 
            // cbRoiIndex
            // 
            this.cbRoiIndex.Font = new System.Drawing.Font("宋体", 11F);
            this.cbRoiIndex.FormattingEnabled = true;
            this.cbRoiIndex.Location = new System.Drawing.Point(101, 43);
            this.cbRoiIndex.Name = "cbRoiIndex";
            this.cbRoiIndex.Size = new System.Drawing.Size(212, 23);
            this.cbRoiIndex.TabIndex = 1;
            this.cbRoiIndex.DropDown += new System.EventHandler(this.cbRoiIndex_DropDown);
            this.cbRoiIndex.SelectedIndexChanged += new System.EventHandler(this.cbRoiIndex_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(21, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型：";
            // 
            // tbRoiType
            // 
            this.tbRoiType.BackColor = System.Drawing.SystemColors.Window;
            this.tbRoiType.Font = new System.Drawing.Font("宋体", 11F);
            this.tbRoiType.Location = new System.Drawing.Point(101, 100);
            this.tbRoiType.Name = "tbRoiType";
            this.tbRoiType.ReadOnly = true;
            this.tbRoiType.Size = new System.Drawing.Size(212, 24);
            this.tbRoiType.TabIndex = 3;
            this.tbRoiType.TextChanged += new System.EventHandler(this.tbROIType_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(21, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "颜色：";
            // 
            // cbRoiColor
            // 
            this.cbRoiColor.Font = new System.Drawing.Font("宋体", 11F);
            this.cbRoiColor.FormattingEnabled = true;
            this.cbRoiColor.Location = new System.Drawing.Point(101, 159);
            this.cbRoiColor.Name = "cbRoiColor";
            this.cbRoiColor.Size = new System.Drawing.Size(212, 23);
            this.cbRoiColor.TabIndex = 1;
            this.cbRoiColor.DropDown += new System.EventHandler(this.cbRoiColor_DropDown);
            this.cbRoiColor.SelectedIndexChanged += new System.EventHandler(this.cbRoiColor_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(21, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "线宽：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(2, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "是否填充：";
            // 
            // cbxFill
            // 
            this.cbxFill.AutoSize = true;
            this.cbxFill.Font = new System.Drawing.Font("宋体", 11F);
            this.cbxFill.Location = new System.Drawing.Point(101, 273);
            this.cbxFill.Name = "cbxFill";
            this.cbxFill.Size = new System.Drawing.Size(86, 19);
            this.cbxFill.TabIndex = 4;
            this.cbxFill.Text = "当前填充";
            this.cbxFill.UseVisualStyleBackColor = true;
            this.cbxFill.CheckedChanged += new System.EventHandler(this.cbxFill_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(2, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "是否可见：";
            // 
            // cbxVisible
            // 
            this.cbxVisible.AutoSize = true;
            this.cbxVisible.Font = new System.Drawing.Font("宋体", 11F);
            this.cbxVisible.Location = new System.Drawing.Point(101, 327);
            this.cbxVisible.Name = "cbxVisible";
            this.cbxVisible.Size = new System.Drawing.Size(86, 19);
            this.cbxVisible.TabIndex = 4;
            this.cbxVisible.Text = "当前可见";
            this.cbxVisible.UseVisualStyleBackColor = true;
            this.cbxVisible.CheckedChanged += new System.EventHandler(this.cbxVisible_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudLineWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxAllVisible);
            this.groupBox1.Controls.Add(this.cbxVisible);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxAllFill);
            this.groupBox1.Controls.Add(this.cbxFill);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbRoiType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbRoiIndex);
            this.groupBox1.Controls.Add(this.cbRoiColor);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 378);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // nudLineWidth
            // 
            this.nudLineWidth.DecimalPlaces = 1;
            this.nudLineWidth.Font = new System.Drawing.Font("宋体", 11F);
            this.nudLineWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLineWidth.Location = new System.Drawing.Point(101, 213);
            this.nudLineWidth.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLineWidth.Name = "nudLineWidth";
            this.nudLineWidth.Size = new System.Drawing.Size(212, 24);
            this.nudLineWidth.TabIndex = 5;
            this.nudLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLineWidth.ValueChanged += new System.EventHandler(this.nudLineWidth_ValueChanged);
            // 
            // cbxAllVisible
            // 
            this.cbxAllVisible.AutoSize = true;
            this.cbxAllVisible.Checked = true;
            this.cbxAllVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAllVisible.Font = new System.Drawing.Font("宋体", 11F);
            this.cbxAllVisible.Location = new System.Drawing.Point(228, 327);
            this.cbxAllVisible.Name = "cbxAllVisible";
            this.cbxAllVisible.Size = new System.Drawing.Size(86, 19);
            this.cbxAllVisible.TabIndex = 4;
            this.cbxAllVisible.Text = "全部可见";
            this.cbxAllVisible.UseVisualStyleBackColor = true;
            this.cbxAllVisible.CheckedChanged += new System.EventHandler(this.cbxAllVisible_CheckedChanged);
            // 
            // cbxAllFill
            // 
            this.cbxAllFill.AutoSize = true;
            this.cbxAllFill.Font = new System.Drawing.Font("宋体", 11F);
            this.cbxAllFill.Location = new System.Drawing.Point(228, 271);
            this.cbxAllFill.Name = "cbxAllFill";
            this.cbxAllFill.Size = new System.Drawing.Size(86, 19);
            this.cbxAllFill.TabIndex = 4;
            this.cbxAllFill.Text = "全部填充";
            this.cbxAllFill.UseVisualStyleBackColor = true;
            this.cbxAllFill.CheckedChanged += new System.EventHandler(this.cbxAllFill_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(12, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 395);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "点位信息";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 370);
            this.panel1.TabIndex = 0;
            // 
            // FrmEditHyROI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(361, 803);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditHyROI";
            this.Text = "ROI编辑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEditHyROI_FormClosing);
            this.Load += new System.EventHandler(this.FrmEditHyROI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRoiIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRoiType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRoiColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbxFill;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbxVisible;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbxAllVisible;
        private System.Windows.Forms.CheckBox cbxAllFill;
        private System.Windows.Forms.NumericUpDown nudLineWidth;
    }
}