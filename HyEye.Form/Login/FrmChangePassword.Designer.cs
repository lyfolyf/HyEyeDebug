namespace HyEye.WForm.Login
{
    partial class FrmChangePassword
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
            this.btnOK = new System.Windows.Forms.Button();
            this.tbNewPassWordAgain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNewPassWord = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbOldPassWord = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(140, 217);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 35);
            this.btnOK.TabIndex = 60;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnPWChange_Click);
            // 
            // tbNewPassWordAgain
            // 
            this.tbNewPassWordAgain.Location = new System.Drawing.Point(140, 155);
            this.tbNewPassWordAgain.Margin = new System.Windows.Forms.Padding(4);
            this.tbNewPassWordAgain.Name = "tbNewPassWordAgain";
            this.tbNewPassWordAgain.PasswordChar = '*';
            this.tbNewPassWordAgain.Size = new System.Drawing.Size(150, 25);
            this.tbNewPassWordAgain.TabIndex = 59;
            this.tbNewPassWordAgain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNewPassWordAgain_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(40, 160);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 64;
            this.label5.Text = "确认新密码：";
            // 
            // tbNewPassWord
            // 
            this.tbNewPassWord.Location = new System.Drawing.Point(140, 95);
            this.tbNewPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.tbNewPassWord.Name = "tbNewPassWord";
            this.tbNewPassWord.PasswordChar = '*';
            this.tbNewPassWord.Size = new System.Drawing.Size(150, 25);
            this.tbNewPassWord.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(40, 100);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 63;
            this.label6.Text = "输入新密码：";
            // 
            // tbOldPassWord
            // 
            this.tbOldPassWord.Location = new System.Drawing.Point(140, 35);
            this.tbOldPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.tbOldPassWord.Name = "tbOldPassWord";
            this.tbOldPassWord.PasswordChar = '*';
            this.tbOldPassWord.Size = new System.Drawing.Size(150, 25);
            this.tbOldPassWord.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(40, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 62;
            this.label7.Text = "输入旧密码：";
            // 
            // pbOK
            // 
            this.pbOK.BackColor = System.Drawing.Color.Transparent;
            this.pbOK.BackgroundImage = global::HyEye.WForm.Properties.Resources.确定;
            this.pbOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOK.Location = new System.Drawing.Point(3, 3);
            this.pbOK.Name = "pbOK";
            this.pbOK.Size = new System.Drawing.Size(29, 29);
            this.pbOK.TabIndex = 65;
            this.pbOK.TabStop = false;
            // 
            // FrmChangePassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(337, 290);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbNewPassWordAgain);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbNewPassWord);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbOldPassWord);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.TextBox tbNewPassWordAgain;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox tbNewPassWord;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox tbOldPassWord;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbOK;
    }
}