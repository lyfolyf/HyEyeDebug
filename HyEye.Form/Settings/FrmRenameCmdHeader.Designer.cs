
namespace HyEye.WForm.Settings
{
    partial class FrmRenameCmdHeader
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
            this.tbTaskName = new System.Windows.Forms.TextBox();
            this.tbCmdHeader = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务名称：";
            // 
            // tbTaskName
            // 
            this.tbTaskName.Enabled = false;
            this.tbTaskName.Location = new System.Drawing.Point(136, 35);
            this.tbTaskName.Name = "tbTaskName";
            this.tbTaskName.Size = new System.Drawing.Size(140, 25);
            this.tbTaskName.TabIndex = 1;
            // 
            // tbCmdHeader
            // 
            this.tbCmdHeader.Location = new System.Drawing.Point(136, 95);
            this.tbCmdHeader.MaxLength = 4;
            this.tbCmdHeader.Name = "tbCmdHeader";
            this.tbCmdHeader.Size = new System.Drawing.Size(140, 25);
            this.tbCmdHeader.TabIndex = 1;
            this.tbCmdHeader.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCmdHeader_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "指令头：";
            // 
            // btnOK
            // 
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.Location = new System.Drawing.Point(136, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 35);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pbOK
            // 
            this.pbOK.BackColor = System.Drawing.Color.Transparent;
            this.pbOK.BackgroundImage = global::HyEye.WForm.Properties.Resources.确定;
            this.pbOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOK.Location = new System.Drawing.Point(3, 3);
            this.pbOK.Name = "pbOK";
            this.pbOK.Size = new System.Drawing.Size(29, 29);
            this.pbOK.TabIndex = 31;
            this.pbOK.TabStop = false;
            // 
            // FrmRenameCmdHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 228);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbCmdHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTaskName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmRenameCmdHeader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重命名指令头";
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTaskName;
        private System.Windows.Forms.TextBox tbCmdHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbOK;
    }
}