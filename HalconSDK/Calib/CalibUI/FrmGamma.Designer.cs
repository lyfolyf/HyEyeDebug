
namespace HalconSDK.Calib.CalibUI
{
    partial class FrmGamma
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
            this.text_gamma_result = new System.Windows.Forms.TextBox();
            this.btnOpenGammaImgFold_1 = new System.Windows.Forms.Button();
            this.text_gammaPath_1 = new System.Windows.Forms.TextBox();
            this.Gamma校正 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_gamma_result
            // 
            this.text_gamma_result.Location = new System.Drawing.Point(311, 176);
            this.text_gamma_result.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_gamma_result.Multiline = true;
            this.text_gamma_result.Name = "text_gamma_result";
            this.text_gamma_result.Size = new System.Drawing.Size(275, 102);
            this.text_gamma_result.TabIndex = 23;
            // 
            // btnOpenGammaImgFold_1
            // 
            this.btnOpenGammaImgFold_1.Location = new System.Drawing.Point(121, 72);
            this.btnOpenGammaImgFold_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenGammaImgFold_1.Name = "btnOpenGammaImgFold_1";
            this.btnOpenGammaImgFold_1.Size = new System.Drawing.Size(149, 45);
            this.btnOpenGammaImgFold_1.TabIndex = 24;
            this.btnOpenGammaImgFold_1.Text = "打开gamma图像";
            this.btnOpenGammaImgFold_1.UseVisualStyleBackColor = true;
            this.btnOpenGammaImgFold_1.Click += new System.EventHandler(this.btnOpenGammaImgFold_1_Click);
            // 
            // text_gammaPath_1
            // 
            this.text_gammaPath_1.Location = new System.Drawing.Point(311, 72);
            this.text_gammaPath_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_gammaPath_1.Multiline = true;
            this.text_gammaPath_1.Name = "text_gammaPath_1";
            this.text_gammaPath_1.Size = new System.Drawing.Size(275, 44);
            this.text_gammaPath_1.TabIndex = 25;
            // 
            // Gamma校正
            // 
            this.Gamma校正.Location = new System.Drawing.Point(121, 189);
            this.Gamma校正.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Gamma校正.Name = "Gamma校正";
            this.Gamma校正.Size = new System.Drawing.Size(149, 41);
            this.Gamma校正.TabIndex = 26;
            this.Gamma校正.Text = "Gamma校正";
            this.Gamma校正.UseVisualStyleBackColor = true;
            this.Gamma校正.Click += new System.EventHandler(this.Gamma校正_Click);
            // 
            // FrmGamma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(744, 501);
            this.Controls.Add(this.Gamma校正);
            this.Controls.Add(this.text_gammaPath_1);
            this.Controls.Add(this.btnOpenGammaImgFold_1);
            this.Controls.Add(this.text_gamma_result);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmGamma";
            this.Text = "FrmGamma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox text_gamma_result;
        private System.Windows.Forms.Button btnOpenGammaImgFold_1;
        private System.Windows.Forms.TextBox text_gammaPath_1;
        private System.Windows.Forms.Button Gamma校正;
    }
}