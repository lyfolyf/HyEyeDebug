
namespace HyEye.WForm
{
    partial class FrmLic
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
            this.txtMCode = new System.Windows.Forms.TextBox();
            this.txtLic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnLic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前设备码：";
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // txtMCode
            // 
            this.txtMCode.Location = new System.Drawing.Point(34, 30);
            this.txtMCode.Name = "txtMCode";
            this.txtMCode.ReadOnly = true;
            this.txtMCode.Size = new System.Drawing.Size(476, 21);
            this.txtMCode.TabIndex = 1;
            // 
            // txtLic
            // 
            this.txtLic.Location = new System.Drawing.Point(34, 75);
            this.txtLic.Multiline = true;
            this.txtLic.Name = "txtLic";
            this.txtLic.Size = new System.Drawing.Size(476, 125);
            this.txtLic.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "激活码：";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(15, 207);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "暂无授权";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnLic
            // 
            this.btnLic.Location = new System.Drawing.Point(435, 207);
            this.btnLic.Name = "btnLic";
            this.btnLic.Size = new System.Drawing.Size(75, 23);
            this.btnLic.TabIndex = 3;
            this.btnLic.Text = "激活";
            this.btnLic.UseVisualStyleBackColor = true;
            this.btnLic.Click += new System.EventHandler(this.btnLic_Click);
            // 
            // FrmLic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 245);
            this.Controls.Add(this.btnLic);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtLic);
            this.Controls.Add(this.txtMCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HyInspect激活";
            this.Load += new System.EventHandler(this.FrmLic_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMCode;
        private System.Windows.Forms.TextBox txtLic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnLic;
    }
}