namespace HyEye.WForm.Settings
{
    partial class FrmLightName
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
            this.tbLightName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.btnCancel.SuspendLayout();
            this.btnOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLightName
            // 
            this.tbLightName.Location = new System.Drawing.Point(125, 37);
            this.tbLightName.MaxLength = 30;
            this.tbLightName.Name = "tbLightName";
            this.tbLightName.Size = new System.Drawing.Size(200, 25);
            this.tbLightName.TabIndex = 1;
            this.tbLightName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLightName_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(40, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "???????????????";
            // 
            // btnCancel
            // 
            this.btnCancel.Controls.Add(this.pbCancel);
            this.btnCancel.Location = new System.Drawing.Point(235, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "??????";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Controls.Add(this.pbOK);
            this.btnOK.Location = new System.Drawing.Point(125, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 35);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "??????";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.BackColor = System.Drawing.Color.Transparent;
            this.pbCancel.BackgroundImage = global::HyEye.WForm.Properties.Resources.??????;
            this.pbCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCancel.Location = new System.Drawing.Point(3, 3);
            this.pbCancel.Name = "pbCancel";
            this.pbCancel.Size = new System.Drawing.Size(29, 29);
            this.pbCancel.TabIndex = 32;
            this.pbCancel.TabStop = false;
            // 
            // pbOK
            // 
            this.pbOK.BackColor = System.Drawing.Color.Transparent;
            this.pbOK.BackgroundImage = global::HyEye.WForm.Properties.Resources.??????;
            this.pbOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOK.Location = new System.Drawing.Point(3, 3);
            this.pbOK.Name = "pbOK";
            this.pbOK.Size = new System.Drawing.Size(29, 29);
            this.pbOK.TabIndex = 31;
            this.pbOK.TabStop = false;
            // 
            // FrmLightName
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(366, 153);
            this.Controls.Add(this.tbLightName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("??????", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLightName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "????????????";
            this.Load += new System.EventHandler(this.FrmLightName_Load);
            this.btnCancel.ResumeLayout(false);
            this.btnOK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLightName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbCancel;
        private System.Windows.Forms.PictureBox pbOK;
    }
}