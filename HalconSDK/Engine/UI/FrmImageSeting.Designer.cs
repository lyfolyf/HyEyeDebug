
namespace HalconSDK.Engine.UI
{
    partial class FrmImageSeting
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
            this.btnOpenHalconFile = new System.Windows.Forms.Button();
            this.tbImagePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.hyImageDisplayControl1 = new HyVision.Tools.ImageDisplay.HyImageDisplayControl();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenHalconFile
            // 
            this.btnOpenHalconFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenHalconFile.Location = new System.Drawing.Point(792, 6);
            this.btnOpenHalconFile.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.btnOpenHalconFile.Name = "btnOpenHalconFile";
            this.btnOpenHalconFile.Size = new System.Drawing.Size(160, 48);
            this.btnOpenHalconFile.TabIndex = 12;
            this.btnOpenHalconFile.Text = "...";
            this.btnOpenHalconFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenHalconFile.UseVisualStyleBackColor = true;
            this.btnOpenHalconFile.Click += new System.EventHandler(this.btnOpenHalconFile_Click);
            // 
            // tbImagePath
            // 
            this.tbImagePath.BackColor = System.Drawing.Color.White;
            this.tbImagePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbImagePath.Location = new System.Drawing.Point(2, 12);
            this.tbImagePath.Margin = new System.Windows.Forms.Padding(2, 12, 2, 2);
            this.tbImagePath.Name = "tbImagePath";
            this.tbImagePath.ReadOnly = true;
            this.tbImagePath.Size = new System.Drawing.Size(786, 24);
            this.tbImagePath.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(958, 76);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择输入图片";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.84625F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.15375F));
            this.tableLayoutPanel1.Controls.Add(this.tbImagePath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOpenHalconFile, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(954, 55);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.hyImageDisplayControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(962, 650);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // hyImageDisplayControl1
            // 
            this.hyImageDisplayControl1.BottomToolVisible = true;
            this.tableLayoutPanel2.SetColumnSpan(this.hyImageDisplayControl1, 2);
            this.hyImageDisplayControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyImageDisplayControl1.Location = new System.Drawing.Point(2, 82);
            this.hyImageDisplayControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.hyImageDisplayControl1.Name = "hyImageDisplayControl1";
            this.hyImageDisplayControl1.ShowEditROIForm = false;
            this.hyImageDisplayControl1.Size = new System.Drawing.Size(958, 566);
            this.hyImageDisplayControl1.TabIndex = 0;
            this.hyImageDisplayControl1.TopToolVisible = true;
            // 
            // FrmImageSeting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 650);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmImageSeting";
            this.Text = "FrmImageSeting";
            this.Load += new System.EventHandler(this.FrmImageSeting_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HyVision.Tools.ImageDisplay.HyImageDisplayControl hyImageDisplayControl1;
        private System.Windows.Forms.Button btnOpenHalconFile;
        private System.Windows.Forms.TextBox tbImagePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}