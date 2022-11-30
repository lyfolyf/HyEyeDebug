
namespace HyEye.WForm.Settings
{
    partial class FrmCameraSDKLog
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
            this.cmbCameraBrand = new System.Windows.Forms.ComboBox();
            this.btnCopyLog = new System.Windows.Forms.Button();
            this.rdoOpen = new System.Windows.Forms.RadioButton();
            this.rdoClose = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机品牌：";
            // 
            // cmbCameraBrand
            // 
            this.cmbCameraBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameraBrand.FormattingEnabled = true;
            this.cmbCameraBrand.Location = new System.Drawing.Point(127, 21);
            this.cmbCameraBrand.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCameraBrand.Name = "cmbCameraBrand";
            this.cmbCameraBrand.Size = new System.Drawing.Size(92, 20);
            this.cmbCameraBrand.TabIndex = 1;
            // 
            // btnCopyLog
            // 
            this.btnCopyLog.Location = new System.Drawing.Point(93, 184);
            this.btnCopyLog.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyLog.Name = "btnCopyLog";
            this.btnCopyLog.Size = new System.Drawing.Size(91, 37);
            this.btnCopyLog.TabIndex = 3;
            this.btnCopyLog.Text = "拷贝日志";
            this.btnCopyLog.UseVisualStyleBackColor = true;
            this.btnCopyLog.Click += new System.EventHandler(this.btnCopyLog_Click);
            // 
            // rdoOpen
            // 
            this.rdoOpen.AutoSize = true;
            this.rdoOpen.Location = new System.Drawing.Point(43, 116);
            this.rdoOpen.Name = "rdoOpen";
            this.rdoOpen.Size = new System.Drawing.Size(89, 16);
            this.rdoOpen.TabIndex = 4;
            this.rdoOpen.TabStop = true;
            this.rdoOpen.Text = "开启SDK日志";
            this.rdoOpen.UseVisualStyleBackColor = true;
            this.rdoOpen.Click += new System.EventHandler(this.rdoOpen_Click);
            // 
            // rdoClose
            // 
            this.rdoClose.AutoSize = true;
            this.rdoClose.Location = new System.Drawing.Point(170, 116);
            this.rdoClose.Name = "rdoClose";
            this.rdoClose.Size = new System.Drawing.Size(89, 16);
            this.rdoClose.TabIndex = 5;
            this.rdoClose.TabStop = true;
            this.rdoClose.Text = "关闭SDK日志";
            this.rdoClose.UseVisualStyleBackColor = true;
            this.rdoClose.Click += new System.EventHandler(this.rdoClose_Click);
            // 
            // FrmCameraSDKLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 264);
            this.Controls.Add(this.rdoClose);
            this.Controls.Add(this.rdoOpen);
            this.Controls.Add(this.btnCopyLog);
            this.Controls.Add(this.cmbCameraBrand);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmCameraSDKLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相机 SDK 日志";
            this.Load += new System.EventHandler(this.FrmCameraSDKLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCameraBrand;
        private System.Windows.Forms.Button btnCopyLog;
        private System.Windows.Forms.RadioButton rdoOpen;
        private System.Windows.Forms.RadioButton rdoClose;
    }
}