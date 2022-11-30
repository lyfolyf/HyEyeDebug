
namespace HalconSDK.Calib.CalibUI
{
    partial class FrmCamInnerParam
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
            this.GenCalibFiles_1 = new System.Windows.Forms.Button();
            this.btnOpenInnerImgPath_1 = new System.Windows.Forms.Button();
            this.text_InnerImgPath_1 = new System.Windows.Forms.TextBox();
            this.dgv_ShowMatrix = new System.Windows.Forms.DataGridView();
            this.HomMat3DObjInCamera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HomMat3Dscreen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Homtemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // GenCalibFiles_1
            // 
            this.GenCalibFiles_1.Location = new System.Drawing.Point(560, 30);
            this.GenCalibFiles_1.Margin = new System.Windows.Forms.Padding(4);
            this.GenCalibFiles_1.Name = "GenCalibFiles_1";
            this.GenCalibFiles_1.Size = new System.Drawing.Size(131, 40);
            this.GenCalibFiles_1.TabIndex = 5;
            this.GenCalibFiles_1.Text = "生成矩阵";
            this.GenCalibFiles_1.UseVisualStyleBackColor = true;
            this.GenCalibFiles_1.Click += new System.EventHandler(this.GenCalibFiles_1_Click);
            // 
            // btnOpenInnerImgPath_1
            // 
            this.btnOpenInnerImgPath_1.Location = new System.Drawing.Point(51, 30);
            this.btnOpenInnerImgPath_1.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenInnerImgPath_1.Name = "btnOpenInnerImgPath_1";
            this.btnOpenInnerImgPath_1.Size = new System.Drawing.Size(131, 40);
            this.btnOpenInnerImgPath_1.TabIndex = 4;
            this.btnOpenInnerImgPath_1.Text = "打开图像路径";
            this.btnOpenInnerImgPath_1.UseVisualStyleBackColor = true;
            this.btnOpenInnerImgPath_1.Click += new System.EventHandler(this.btnOpenInnerImgPath_1_Click);
            // 
            // text_InnerImgPath_1
            // 
            this.text_InnerImgPath_1.Location = new System.Drawing.Point(233, 30);
            this.text_InnerImgPath_1.Margin = new System.Windows.Forms.Padding(4);
            this.text_InnerImgPath_1.Multiline = true;
            this.text_InnerImgPath_1.Name = "text_InnerImgPath_1";
            this.text_InnerImgPath_1.Size = new System.Drawing.Size(256, 53);
            this.text_InnerImgPath_1.TabIndex = 3;
            // 
            // dgv_ShowMatrix
            // 
            this.dgv_ShowMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ShowMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HomMat3DObjInCamera,
            this.HomMat3Dscreen,
            this.Homtemp});
            this.dgv_ShowMatrix.Location = new System.Drawing.Point(106, 91);
            this.dgv_ShowMatrix.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_ShowMatrix.Name = "dgv_ShowMatrix";
            this.dgv_ShowMatrix.RowHeadersWidth = 51;
            this.dgv_ShowMatrix.RowTemplate.Height = 23;
            this.dgv_ShowMatrix.Size = new System.Drawing.Size(523, 395);
            this.dgv_ShowMatrix.TabIndex = 6;
            // 
            // HomMat3DObjInCamera
            // 
            this.HomMat3DObjInCamera.HeaderText = "HomMat3DObjInCamera";
            this.HomMat3DObjInCamera.MinimumWidth = 6;
            this.HomMat3DObjInCamera.Name = "HomMat3DObjInCamera";
            this.HomMat3DObjInCamera.Width = 150;
            // 
            // HomMat3Dscreen
            // 
            this.HomMat3Dscreen.HeaderText = "HomMat3Dscreen";
            this.HomMat3Dscreen.MinimumWidth = 6;
            this.HomMat3Dscreen.Name = "HomMat3Dscreen";
            this.HomMat3Dscreen.Width = 150;
            // 
            // Homtemp
            // 
            this.Homtemp.HeaderText = "Homtemp";
            this.Homtemp.MinimumWidth = 6;
            this.Homtemp.Name = "Homtemp";
            this.Homtemp.Width = 150;
            // 
            // FrmCamInnerParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(744, 501);
            this.Controls.Add(this.dgv_ShowMatrix);
            this.Controls.Add(this.GenCalibFiles_1);
            this.Controls.Add(this.btnOpenInnerImgPath_1);
            this.Controls.Add(this.text_InnerImgPath_1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCamInnerParam";
            this.Text = "FrmCamInnerParam";
            this.Load += new System.EventHandler(this.FrmCamInnerParam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowMatrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenCalibFiles_1;
        private System.Windows.Forms.Button btnOpenInnerImgPath_1;
        private System.Windows.Forms.TextBox text_InnerImgPath_1;
        private System.Windows.Forms.DataGridView dgv_ShowMatrix;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomMat3DObjInCamera;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomMat3Dscreen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Homtemp;
    }
}