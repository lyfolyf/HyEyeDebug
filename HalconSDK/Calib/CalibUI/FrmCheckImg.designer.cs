
namespace HalconSDK.Calib.CalibUI
{
    partial class FrmCheckImg
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
            this.txt_CheckImg_result = new System.Windows.Forms.TextBox();
            this.btnOpenInnerImgFold_1 = new System.Windows.Forms.Button();
            this.txt_CheckImgPath = new System.Windows.Forms.TextBox();
            this.btn_CheckImg = new System.Windows.Forms.Button();
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Singe_Img = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_CheckImg_result
            // 
            this.txt_CheckImg_result.Location = new System.Drawing.Point(25, 190);
            this.txt_CheckImg_result.Multiline = true;
            this.txt_CheckImg_result.Name = "txt_CheckImg_result";
            this.txt_CheckImg_result.Size = new System.Drawing.Size(166, 80);
            this.txt_CheckImg_result.TabIndex = 23;
            // 
            // btnOpenInnerImgFold_1
            // 
            this.btnOpenInnerImgFold_1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenInnerImgFold_1.Location = new System.Drawing.Point(59, 19);
            this.btnOpenInnerImgFold_1.Name = "btnOpenInnerImgFold_1";
            this.btnOpenInnerImgFold_1.Size = new System.Drawing.Size(98, 36);
            this.btnOpenInnerImgFold_1.TabIndex = 24;
            this.btnOpenInnerImgFold_1.Text = "打开Inner图像";
            this.btnOpenInnerImgFold_1.UseVisualStyleBackColor = true;
            this.btnOpenInnerImgFold_1.Click += new System.EventHandler(this.btnOpenInnerImgFold_1_Click);
            // 
            // txt_CheckImgPath
            // 
            this.txt_CheckImgPath.Location = new System.Drawing.Point(25, 62);
            this.txt_CheckImgPath.Multiline = true;
            this.txt_CheckImgPath.Name = "txt_CheckImgPath";
            this.txt_CheckImgPath.Size = new System.Drawing.Size(166, 58);
            this.txt_CheckImgPath.TabIndex = 25;
            // 
            // btn_CheckImg
            // 
            this.btn_CheckImg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CheckImg.Location = new System.Drawing.Point(59, 151);
            this.btn_CheckImg.Name = "btn_CheckImg";
            this.btn_CheckImg.Size = new System.Drawing.Size(98, 33);
            this.btn_CheckImg.TabIndex = 26;
            this.btn_CheckImg.Text = "检测图像";
            this.btn_CheckImg.UseVisualStyleBackColor = true;
            this.btn_CheckImg.Click += new System.EventHandler(this.btn_CheckImg_Click);
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Black;
            this.hWindowControl2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl2.Location = new System.Drawing.Point(214, 10);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(340, 380);
            this.hWindowControl2.TabIndex = 27;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(340, 380);
            this.hWindowControl2.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl2_HMouseWheel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(3, 318);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "单个物料序号:";
            // 
            // txt_Singe_Img
            // 
            this.txt_Singe_Img.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Singe_Img.Location = new System.Drawing.Point(86, 310);
            this.txt_Singe_Img.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Singe_Img.Name = "txt_Singe_Img";
            this.txt_Singe_Img.Size = new System.Drawing.Size(96, 28);
            this.txt_Singe_Img.TabIndex = 29;
            // 
            // FrmCheckImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(558, 401);
            this.Controls.Add(this.txt_Singe_Img);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hWindowControl2);
            this.Controls.Add(this.btn_CheckImg);
            this.Controls.Add(this.txt_CheckImgPath);
            this.Controls.Add(this.btnOpenInnerImgFold_1);
            this.Controls.Add(this.txt_CheckImg_result);
            this.Name = "FrmCheckImg";
            this.Text = "FrmCheckImg";
            this.Load += new System.EventHandler(this.FrmCheckImg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_CheckImg_result;
        private System.Windows.Forms.Button btnOpenInnerImgFold_1;
        private System.Windows.Forms.TextBox txt_CheckImgPath;
        private System.Windows.Forms.Button btn_CheckImg;
        private HalconDotNet.HWindowControl hWindowControl2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Singe_Img;
    }
}