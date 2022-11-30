
namespace HyEye.WForm.Display
{
    partial class FrmLoadImage
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRotate270 = new System.Windows.Forms.RadioButton();
            this.rbRotate180 = new System.Windows.Forms.RadioButton();
            this.rbRotate90 = new System.Windows.Forms.RadioButton();
            this.rbRotate0 = new System.Windows.Forms.RadioButton();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSurface = new System.Windows.Forms.ComboBox();
            this.hyImageDisplayControlSimple1 = new HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRotate270);
            this.groupBox1.Controls.Add(this.rbRotate180);
            this.groupBox1.Controls.Add(this.rbRotate90);
            this.groupBox1.Controls.Add(this.rbRotate0);
            this.groupBox1.Location = new System.Drawing.Point(348, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "旋转";
            // 
            // rbRotate270
            // 
            this.rbRotate270.AutoSize = true;
            this.rbRotate270.Location = new System.Drawing.Point(26, 84);
            this.rbRotate270.Name = "rbRotate270";
            this.rbRotate270.Size = new System.Drawing.Size(53, 16);
            this.rbRotate270.TabIndex = 3;
            this.rbRotate270.Text = "270°";
            this.rbRotate270.UseVisualStyleBackColor = true;
            this.rbRotate270.CheckedChanged += new System.EventHandler(this.rbRotate270_CheckedChanged);
            // 
            // rbRotate180
            // 
            this.rbRotate180.AutoSize = true;
            this.rbRotate180.Location = new System.Drawing.Point(26, 62);
            this.rbRotate180.Name = "rbRotate180";
            this.rbRotate180.Size = new System.Drawing.Size(53, 16);
            this.rbRotate180.TabIndex = 2;
            this.rbRotate180.Text = "180°";
            this.rbRotate180.UseVisualStyleBackColor = true;
            this.rbRotate180.CheckedChanged += new System.EventHandler(this.rbRotate180_CheckedChanged);
            // 
            // rbRotate90
            // 
            this.rbRotate90.AutoSize = true;
            this.rbRotate90.Location = new System.Drawing.Point(26, 40);
            this.rbRotate90.Name = "rbRotate90";
            this.rbRotate90.Size = new System.Drawing.Size(47, 16);
            this.rbRotate90.TabIndex = 1;
            this.rbRotate90.Text = "90°";
            this.rbRotate90.UseVisualStyleBackColor = true;
            this.rbRotate90.CheckedChanged += new System.EventHandler(this.rbRotate90_CheckedChanged);
            // 
            // rbRotate0
            // 
            this.rbRotate0.AutoSize = true;
            this.rbRotate0.Checked = true;
            this.rbRotate0.Location = new System.Drawing.Point(26, 18);
            this.rbRotate0.Name = "rbRotate0";
            this.rbRotate0.Size = new System.Drawing.Size(41, 16);
            this.rbRotate0.TabIndex = 0;
            this.rbRotate0.TabStop = true;
            this.rbRotate0.Text = "0°";
            this.rbRotate0.UseVisualStyleBackColor = true;
            this.rbRotate0.CheckedChanged += new System.EventHandler(this.rbRotate0_CheckedChanged);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.Gray;
            this.btnLoadImage.FlatAppearance.BorderSize = 0;
            this.btnLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadImage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadImage.Location = new System.Drawing.Point(342, 4);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(123, 30);
            this.btnLoadImage.TabIndex = 1;
            this.btnLoadImage.Text = "加载";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.DimGray;
            this.btnCancel.Location = new System.Drawing.Point(340, 214);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.Turquoise;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.Black;
            this.btnApply.Location = new System.Drawing.Point(408, 214);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(57, 30);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "确认";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPG图片|*.jpg|bmp位图|*.bmp|所有文件|*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(348, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "设置名称";
            this.label1.Visible = false;
            // 
            // cbSurface
            // 
            this.cbSurface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSurface.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSurface.FormattingEnabled = true;
            this.cbSurface.Location = new System.Drawing.Point(342, 170);
            this.cbSurface.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSurface.Name = "cbSurface";
            this.cbSurface.Size = new System.Drawing.Size(123, 25);
            this.cbSurface.TabIndex = 55;
            this.cbSurface.Visible = false;
            // 
            // hyImageDisplayControlSimple1
            // 
            this.hyImageDisplayControlSimple1.AllowOperation = true;
            this.hyImageDisplayControlSimple1.AutoFit = true;
            this.hyImageDisplayControlSimple1.BackColor = System.Drawing.Color.Gray;
            this.hyImageDisplayControlSimple1.Location = new System.Drawing.Point(3, 4);
            this.hyImageDisplayControlSimple1.Name = "hyImageDisplayControlSimple1";
            this.hyImageDisplayControlSimple1.SelecedtIndex = -1;
            this.hyImageDisplayControlSimple1.ShowCoorMsg = false;
            this.hyImageDisplayControlSimple1.Size = new System.Drawing.Size(331, 246);
            this.hyImageDisplayControlSimple1.TabIndex = 2;
            // 
            // FrmLoadImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 256);
            this.Controls.Add(this.cbSurface);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.hyImageDisplayControlSimple1);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLoadImage";
            this.Text = "加载图像";
            this.Load += new System.EventHandler(this.FrmLoadImage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRotate270;
        private System.Windows.Forms.RadioButton rbRotate180;
        private System.Windows.Forms.RadioButton rbRotate90;
        private System.Windows.Forms.RadioButton rbRotate0;
        private System.Windows.Forms.Button btnLoadImage;
        private HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple hyImageDisplayControlSimple1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSurface;
    }
}