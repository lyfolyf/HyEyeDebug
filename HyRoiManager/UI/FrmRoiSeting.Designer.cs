
namespace HyRoiManager.UI
{
    partial class FrmRoiSeting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbCross = new System.Windows.Forms.CheckBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbPolygon = new System.Windows.Forms.RadioButton();
            this.rbEllipse = new System.Windows.Forms.RadioButton();
            this.rbCircle = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.rbDaub = new System.Windows.Forms.RadioButton();
            this.rbEraser = new System.Windows.Forms.RadioButton();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gbBrushSize = new System.Windows.Forms.GroupBox();
            this.nudBrushSize = new System.Windows.Forms.NumericUpDown();
            this.tbxImagePath = new System.Windows.Forms.TextBox();
            this.btnImagePath = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbBrushSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrushSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(4, 118);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 688);
            this.panel1.TabIndex = 0;
            // 
            // cbCross
            // 
            this.cbCross.AutoSize = true;
            this.cbCross.Font = new System.Drawing.Font("宋体", 12F);
            this.cbCross.Location = new System.Drawing.Point(1100, 769);
            this.cbCross.Margin = new System.Windows.Forms.Padding(4);
            this.cbCross.Name = "cbCross";
            this.cbCross.Size = new System.Drawing.Size(131, 24);
            this.cbCross.TabIndex = 2;
            this.cbCross.Text = "十字线显示";
            this.cbCross.UseVisualStyleBackColor = true;
            this.cbCross.CheckedChanged += new System.EventHandler(this.cbCross_CheckedChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 11.5F);
            this.btnConfirm.Location = new System.Drawing.Point(21, 41);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(133, 41);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 11.5F);
            this.btnCancel.Location = new System.Drawing.Point(21, 101);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 41);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(1068, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(172, 450);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能选项";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbPolygon);
            this.panel3.Controls.Add(this.rbEllipse);
            this.panel3.Controls.Add(this.rbCircle);
            this.panel3.Controls.Add(this.rbRectangle);
            this.panel3.Controls.Add(this.rbDaub);
            this.panel3.Controls.Add(this.rbEraser);
            this.panel3.Controls.Add(this.rbMove);
            this.panel3.Location = new System.Drawing.Point(4, 31);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(161, 406);
            this.panel3.TabIndex = 4;
            // 
            // rbPolygon
            // 
            this.rbPolygon.AutoSize = true;
            this.rbPolygon.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbPolygon.Location = new System.Drawing.Point(31, 351);
            this.rbPolygon.Margin = new System.Windows.Forms.Padding(4);
            this.rbPolygon.Name = "rbPolygon";
            this.rbPolygon.Size = new System.Drawing.Size(90, 24);
            this.rbPolygon.TabIndex = 0;
            this.rbPolygon.Text = "多边形";
            this.rbPolygon.UseVisualStyleBackColor = true;
            this.rbPolygon.CheckedChanged += new System.EventHandler(this.rbPolygon_CheckedChanged);
            // 
            // rbEllipse
            // 
            this.rbEllipse.AutoSize = true;
            this.rbEllipse.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbEllipse.Location = new System.Drawing.Point(33, 298);
            this.rbEllipse.Margin = new System.Windows.Forms.Padding(4);
            this.rbEllipse.Name = "rbEllipse";
            this.rbEllipse.Size = new System.Drawing.Size(90, 24);
            this.rbEllipse.TabIndex = 0;
            this.rbEllipse.Text = "椭圆形";
            this.rbEllipse.UseVisualStyleBackColor = true;
            this.rbEllipse.CheckedChanged += new System.EventHandler(this.rbEllipse_CheckedChanged);
            // 
            // rbCircle
            // 
            this.rbCircle.AutoSize = true;
            this.rbCircle.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbCircle.Location = new System.Drawing.Point(33, 244);
            this.rbCircle.Margin = new System.Windows.Forms.Padding(4);
            this.rbCircle.Name = "rbCircle";
            this.rbCircle.Size = new System.Drawing.Size(70, 24);
            this.rbCircle.TabIndex = 0;
            this.rbCircle.Text = "圆形";
            this.rbCircle.UseVisualStyleBackColor = true;
            this.rbCircle.CheckedChanged += new System.EventHandler(this.rbCircle_CheckedChanged);
            // 
            // rbRectangle
            // 
            this.rbRectangle.AutoSize = true;
            this.rbRectangle.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbRectangle.Location = new System.Drawing.Point(33, 190);
            this.rbRectangle.Margin = new System.Windows.Forms.Padding(4);
            this.rbRectangle.Name = "rbRectangle";
            this.rbRectangle.Size = new System.Drawing.Size(70, 24);
            this.rbRectangle.TabIndex = 0;
            this.rbRectangle.Text = "矩形";
            this.rbRectangle.UseVisualStyleBackColor = true;
            this.rbRectangle.CheckedChanged += new System.EventHandler(this.rbRectangle_CheckedChanged);
            // 
            // rbDaub
            // 
            this.rbDaub.AutoSize = true;
            this.rbDaub.BackColor = System.Drawing.Color.LightGreen;
            this.rbDaub.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbDaub.Location = new System.Drawing.Point(33, 136);
            this.rbDaub.Margin = new System.Windows.Forms.Padding(4);
            this.rbDaub.Name = "rbDaub";
            this.rbDaub.Size = new System.Drawing.Size(70, 24);
            this.rbDaub.TabIndex = 0;
            this.rbDaub.Text = "涂抹";
            this.rbDaub.UseVisualStyleBackColor = false;
            this.rbDaub.CheckedChanged += new System.EventHandler(this.rbDaub_CheckedChanged);
            // 
            // rbEraser
            // 
            this.rbEraser.AutoSize = true;
            this.rbEraser.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbEraser.Location = new System.Drawing.Point(33, 82);
            this.rbEraser.Margin = new System.Windows.Forms.Padding(4);
            this.rbEraser.Name = "rbEraser";
            this.rbEraser.Size = new System.Drawing.Size(90, 24);
            this.rbEraser.TabIndex = 0;
            this.rbEraser.Text = "橡皮擦";
            this.rbEraser.UseVisualStyleBackColor = true;
            this.rbEraser.CheckedChanged += new System.EventHandler(this.rbEraser_CheckedChanged);
            // 
            // rbMove
            // 
            this.rbMove.AutoSize = true;
            this.rbMove.Font = new System.Drawing.Font("宋体", 11.5F);
            this.rbMove.Location = new System.Drawing.Point(31, 29);
            this.rbMove.Margin = new System.Windows.Forms.Padding(4);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(70, 24);
            this.rbMove.TabIndex = 0;
            this.rbMove.Text = "移动";
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.CheckedChanged += new System.EventHandler(this.rbMove_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnConfirm);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(1068, 469);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(172, 161);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "确定/取消";
            // 
            // gbBrushSize
            // 
            this.gbBrushSize.Controls.Add(this.nudBrushSize);
            this.gbBrushSize.Font = new System.Drawing.Font("宋体", 11.5F);
            this.gbBrushSize.Location = new System.Drawing.Point(1072, 646);
            this.gbBrushSize.Margin = new System.Windows.Forms.Padding(4);
            this.gbBrushSize.Name = "gbBrushSize";
            this.gbBrushSize.Padding = new System.Windows.Forms.Padding(4);
            this.gbBrushSize.Size = new System.Drawing.Size(168, 102);
            this.gbBrushSize.TabIndex = 8;
            this.gbBrushSize.TabStop = false;
            this.gbBrushSize.Text = "橡皮擦大小";
            // 
            // nudBrushSize
            // 
            this.nudBrushSize.Location = new System.Drawing.Point(17, 41);
            this.nudBrushSize.Margin = new System.Windows.Forms.Padding(4);
            this.nudBrushSize.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudBrushSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBrushSize.Name = "nudBrushSize";
            this.nudBrushSize.Size = new System.Drawing.Size(117, 29);
            this.nudBrushSize.TabIndex = 0;
            this.nudBrushSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBrushSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBrushSize.ValueChanged += new System.EventHandler(this.nudBrushSize_ValueChanged);
            // 
            // tbxImagePath
            // 
            this.tbxImagePath.BackColor = System.Drawing.Color.White;
            this.tbxImagePath.Font = new System.Drawing.Font("宋体", 11.5F);
            this.tbxImagePath.Location = new System.Drawing.Point(28, 38);
            this.tbxImagePath.Margin = new System.Windows.Forms.Padding(2, 80, 2, 2);
            this.tbxImagePath.Name = "tbxImagePath";
            this.tbxImagePath.ReadOnly = true;
            this.tbxImagePath.Size = new System.Drawing.Size(837, 29);
            this.tbxImagePath.TabIndex = 16;
            // 
            // btnImagePath
            // 
            this.btnImagePath.Font = new System.Drawing.Font("宋体", 11.5F);
            this.btnImagePath.Location = new System.Drawing.Point(882, 39);
            this.btnImagePath.Margin = new System.Windows.Forms.Padding(15, 20, 2, 2);
            this.btnImagePath.Name = "btnImagePath";
            this.btnImagePath.Size = new System.Drawing.Size(151, 26);
            this.btnImagePath.TabIndex = 15;
            this.btnImagePath.Text = "....";
            this.btnImagePath.UseVisualStyleBackColor = true;
            this.btnImagePath.Click += new System.EventHandler(this.btnImagePath_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxImagePath);
            this.groupBox3.Controls.Add(this.btnImagePath);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 11.5F);
            this.groupBox3.Location = new System.Drawing.Point(4, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1056, 94);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择图片";
            // 
            // FrmRoiSeting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 809);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbBrushSize);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbCross);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRoiSeting";
            this.Text = "ROI设置";
            this.Load += new System.EventHandler(this.FrmRoiSeting_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gbBrushSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBrushSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbCross;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbBrushSize;
        private System.Windows.Forms.NumericUpDown nudBrushSize;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.RadioButton rbDaub;
        private System.Windows.Forms.RadioButton rbEraser;
        private System.Windows.Forms.RadioButton rbMove;
        private System.Windows.Forms.RadioButton rbPolygon;
        private System.Windows.Forms.RadioButton rbEllipse;
        private System.Windows.Forms.RadioButton rbCircle;
        private System.Windows.Forms.TextBox tbxImagePath;
        private System.Windows.Forms.Button btnImagePath;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}