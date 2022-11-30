
namespace HyEye.WForm.Display
{
    partial class FrmSetAreaInfo
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
            this.numericRowStart = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericColSpan = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericRowSpan = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBlockInfo = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.numericColStart = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ucAreaSelect1 = new HyEye.WForm.Display.UcAreaSelect();
            this.btnCancel = new System.Windows.Forms.Button();
            this.linkLblSetModelImage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericColSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericColStart)).BeginInit();
            this.SuspendLayout();
            // 
            // numericRowStart
            // 
            this.numericRowStart.Location = new System.Drawing.Point(360, 10);
            this.numericRowStart.Name = "numericRowStart";
            this.numericRowStart.Size = new System.Drawing.Size(66, 21);
            this.numericRowStart.TabIndex = 67;
            this.numericRowStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRowStart.ValueChanged += new System.EventHandler(this.numericRowStart_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(298, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 66;
            this.label12.Text = "起始行";
            // 
            // numericColSpan
            // 
            this.numericColSpan.Location = new System.Drawing.Point(360, 97);
            this.numericColSpan.Name = "numericColSpan";
            this.numericColSpan.Size = new System.Drawing.Size(66, 21);
            this.numericColSpan.TabIndex = 73;
            this.numericColSpan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericColSpan.ValueChanged += new System.EventHandler(this.numericColSpan_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 72;
            this.label2.Text = "跨列";
            // 
            // numericRowSpan
            // 
            this.numericRowSpan.Location = new System.Drawing.Point(360, 68);
            this.numericRowSpan.Name = "numericRowSpan";
            this.numericRowSpan.Size = new System.Drawing.Size(66, 21);
            this.numericRowSpan.TabIndex = 71;
            this.numericRowSpan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRowSpan.ValueChanged += new System.EventHandler(this.numericRowSpan_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 70;
            this.label3.Text = "跨行";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 75;
            this.label1.Text = "块名称";
            // 
            // txtBlockInfo
            // 
            this.txtBlockInfo.Location = new System.Drawing.Point(298, 140);
            this.txtBlockInfo.Name = "txtBlockInfo";
            this.txtBlockInfo.Size = new System.Drawing.Size(127, 21);
            this.txtBlockInfo.TabIndex = 76;
            this.txtBlockInfo.TextChanged += new System.EventHandler(this.txtBlockInfo_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.Turquoise;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.Black;
            this.btnApply.Location = new System.Drawing.Point(369, 210);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(57, 30);
            this.btnApply.TabIndex = 78;
            this.btnApply.Text = "确认";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // numericColStart
            // 
            this.numericColStart.Location = new System.Drawing.Point(360, 39);
            this.numericColStart.Name = "numericColStart";
            this.numericColStart.Size = new System.Drawing.Size(66, 21);
            this.numericColStart.TabIndex = 80;
            this.numericColStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericColStart.ValueChanged += new System.EventHandler(this.numericColStart_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "起始列";
            // 
            // ucAreaSelect1
            // 
            this.ucAreaSelect1.BackColor = System.Drawing.Color.Gray;
            this.ucAreaSelect1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucAreaSelect1.ForeColor = System.Drawing.Color.Aqua;
            this.ucAreaSelect1.HCount = 3;
            this.ucAreaSelect1.HighlightColor = System.Drawing.Color.Yellow;
            this.ucAreaSelect1.HighlightTransparency = ((byte)(100));
            this.ucAreaSelect1.LineColor = System.Drawing.Color.Yellow;
            this.ucAreaSelect1.Location = new System.Drawing.Point(12, 10);
            this.ucAreaSelect1.Name = "ucAreaSelect1";
            this.ucAreaSelect1.Size = new System.Drawing.Size(280, 230);
            this.ucAreaSelect1.TabIndex = 77;
            this.ucAreaSelect1.VCount = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.DimGray;
            this.btnCancel.Location = new System.Drawing.Point(301, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 30);
            this.btnCancel.TabIndex = 81;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // linkLblSetModelImage
            // 
            this.linkLblSetModelImage.AutoSize = true;
            this.linkLblSetModelImage.Location = new System.Drawing.Point(299, 173);
            this.linkLblSetModelImage.Name = "linkLblSetModelImage";
            this.linkLblSetModelImage.Size = new System.Drawing.Size(101, 12);
            this.linkLblSetModelImage.TabIndex = 82;
            this.linkLblSetModelImage.TabStop = true;
            this.linkLblSetModelImage.Text = "点击设置模板图像";
            this.linkLblSetModelImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblSetModelImage_LinkClicked);
            // 
            // FrmSetAreaInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 252);
            this.Controls.Add(this.linkLblSetModelImage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.numericColStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.ucAreaSelect1);
            this.Controls.Add(this.txtBlockInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericColSpan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericRowSpan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericRowStart);
            this.Controls.Add(this.label12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSetAreaInfo";
            this.Text = "添加块信息";
            this.Load += new System.EventHandler(this.FrmSetAreaInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericRowStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericColSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRowSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericColStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericRowStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericColSpan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericRowSpan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBlockInfo;
        private UcAreaSelect ucAreaSelect1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.NumericUpDown numericColStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel linkLblSetModelImage;
    }
}