
namespace HyEye.WForm
{
    partial class FrmSetOutput
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
            this.fMain = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.btnCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 404);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 62);
            this.panel1.TabIndex = 2;
            // 
            // fMain
            // 
            this.fMain.AutoScroll = true;
            this.fMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fMain.Location = new System.Drawing.Point(0, 0);
            this.fMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fMain.Name = "fMain";
            this.fMain.Size = new System.Drawing.Size(608, 404);
            this.fMain.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Controls.Add(this.pbCancel);
            this.btnCancel.Location = new System.Drawing.Point(340, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.BackColor = System.Drawing.Color.Transparent;
            this.pbCancel.BackgroundImage = global::HyEye.WForm.Properties.Resources.取消;
            this.pbCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCancel.Location = new System.Drawing.Point(3, 3);
            this.pbCancel.Name = "pbCancel";
            this.pbCancel.Size = new System.Drawing.Size(29, 29);
            this.pbCancel.TabIndex = 32;
            this.pbCancel.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.Location = new System.Drawing.Point(140, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 35);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnSave_Click);
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
            // FrmSetOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 466);
            this.Controls.Add(this.fMain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetOutput";
            this.Text = "设置显示输出";
            this.Load += new System.EventHandler(this.FrmSetOutput_Load);
            this.panel1.ResumeLayout(false);
            this.btnCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel fMain;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbOK;
    }
}