
namespace HalconSDK.Calib.CalibUI
{
    partial class FrmMirror
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
            this.text_MirrorPathbox_1 = new System.Windows.Forms.TextBox();
            this.btnOpenMirrorPath_1 = new System.Windows.Forms.Button();
            this.btn_absphase = new System.Windows.Forms.Button();
            this.text_BackupBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_MirrorPathbox_1
            // 
            this.text_MirrorPathbox_1.Location = new System.Drawing.Point(232, 128);
            this.text_MirrorPathbox_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_MirrorPathbox_1.Multiline = true;
            this.text_MirrorPathbox_1.Name = "text_MirrorPathbox_1";
            this.text_MirrorPathbox_1.Size = new System.Drawing.Size(260, 78);
            this.text_MirrorPathbox_1.TabIndex = 0;
            // 
            // btnOpenMirrorPath_1
            // 
            this.btnOpenMirrorPath_1.Location = new System.Drawing.Point(67, 141);
            this.btnOpenMirrorPath_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenMirrorPath_1.Name = "btnOpenMirrorPath_1";
            this.btnOpenMirrorPath_1.Size = new System.Drawing.Size(125, 54);
            this.btnOpenMirrorPath_1.TabIndex = 1;
            this.btnOpenMirrorPath_1.Text = "打开镜像路径";
            this.btnOpenMirrorPath_1.UseVisualStyleBackColor = true;
            this.btnOpenMirrorPath_1.Click += new System.EventHandler(this.btnOpenMirrorPath_1_Click);
            // 
            // btn_absphase
            // 
            this.btn_absphase.Location = new System.Drawing.Point(556, 141);
            this.btn_absphase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_absphase.Name = "btn_absphase";
            this.btn_absphase.Size = new System.Drawing.Size(125, 54);
            this.btn_absphase.TabIndex = 2;
            this.btn_absphase.Text = "生成绝对相位图";
            this.btn_absphase.UseVisualStyleBackColor = true;
            this.btn_absphase.Click += new System.EventHandler(this.btn_absphase_Click);
            // 
            // text_BackupBox
            // 
            this.text_BackupBox.Location = new System.Drawing.Point(232, 279);
            this.text_BackupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_BackupBox.Multiline = true;
            this.text_BackupBox.Name = "text_BackupBox";
            this.text_BackupBox.Size = new System.Drawing.Size(260, 60);
            this.text_BackupBox.TabIndex = 30;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(71, 279);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 50);
            this.button4.TabIndex = 29;
            this.button4.Text = "选择备份XML路径";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(556, 279);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 50);
            this.button3.TabIndex = 28;
            this.button3.Text = "备份XML";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmMirror
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(744, 501);
            this.Controls.Add(this.text_BackupBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_absphase);
            this.Controls.Add(this.btnOpenMirrorPath_1);
            this.Controls.Add(this.text_MirrorPathbox_1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmMirror";
            this.Text = "FrmMirror";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_MirrorPathbox_1;
        private System.Windows.Forms.Button btnOpenMirrorPath_1;
        private System.Windows.Forms.Button btn_absphase;
        private System.Windows.Forms.TextBox text_BackupBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
    }
}