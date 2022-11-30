
namespace HyVision.Tools.ImageSave
{
    partial class ImageSaveToolUI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.hyTCEInput = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.gbResizeQuality = new System.Windows.Forms.GroupBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.tplRight = new System.Windows.Forms.TableLayoutPanel();
            this.gbSaveFolder = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbxSaveImageAddress = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tlpMain.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.gbResizeQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.tplRight.SuspendLayout();
            this.gbSaveFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.hyTCEInput, 0, 1);
            this.tlpMain.Controls.Add(this.tlpTop, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(5, 35, 5, 30);
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.Size = new System.Drawing.Size(800, 600);
            this.tlpMain.TabIndex = 6;
            // 
            // hyTCEInput
            // 
            this.hyTCEInput.DefaultNameHeader = null;
            this.hyTCEInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTCEInput.Location = new System.Drawing.Point(8, 252);
            this.hyTCEInput.Name = "hyTCEInput";
            this.hyTCEInput.Size = new System.Drawing.Size(784, 315);
            this.hyTCEInput.TabIndex = 0;
            this.hyTCEInput.Text = "输入图像";
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpTop.Controls.Add(this.gbResizeQuality, 0, 0);
            this.tlpTop.Controls.Add(this.tplRight, 2, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(8, 38);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(784, 208);
            this.tlpTop.TabIndex = 1;
            // 
            // gbResizeQuality
            // 
            this.gbResizeQuality.Controls.Add(this.lblQuality);
            this.gbResizeQuality.Controls.Add(this.nudQuality);
            this.gbResizeQuality.Controls.Add(this.lblHeight);
            this.gbResizeQuality.Controls.Add(this.lblWidth);
            this.gbResizeQuality.Controls.Add(this.nudHeight);
            this.gbResizeQuality.Controls.Add(this.nudWidth);
            this.gbResizeQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbResizeQuality.Location = new System.Drawing.Point(3, 3);
            this.gbResizeQuality.Name = "gbResizeQuality";
            this.gbResizeQuality.Size = new System.Drawing.Size(307, 202);
            this.gbResizeQuality.TabIndex = 1;
            this.gbResizeQuality.TabStop = false;
            this.gbResizeQuality.Text = "调整图像大小和质量";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(34, 155);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(67, 15);
            this.lblQuality.TabIndex = 5;
            this.lblQuality.Text = "图像质量";
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(127, 145);
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(120, 25);
            this.nudQuality.TabIndex = 2;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(34, 103);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(67, 15);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "图像高度";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(34, 52);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(67, 15);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "图像宽度";
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(127, 94);
            this.nudHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(120, 25);
            this.nudHeight.TabIndex = 1;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(127, 50);
            this.nudWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(120, 25);
            this.nudWidth.TabIndex = 0;
            // 
            // tplRight
            // 
            this.tplRight.ColumnCount = 1;
            this.tplRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplRight.Controls.Add(this.gbSaveFolder, 0, 0);
            this.tplRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplRight.Location = new System.Drawing.Point(316, 3);
            this.tplRight.Name = "tplRight";
            this.tplRight.RowCount = 2;
            this.tplRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tplRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tplRight.Size = new System.Drawing.Size(465, 202);
            this.tplRight.TabIndex = 3;
            // 
            // gbSaveFolder
            // 
            this.gbSaveFolder.Controls.Add(this.btnSelectFolder);
            this.gbSaveFolder.Controls.Add(this.tbxSaveImageAddress);
            this.gbSaveFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSaveFolder.Location = new System.Drawing.Point(3, 3);
            this.gbSaveFolder.Name = "gbSaveFolder";
            this.gbSaveFolder.Size = new System.Drawing.Size(459, 64);
            this.gbSaveFolder.TabIndex = 2;
            this.gbSaveFolder.TabStop = false;
            this.gbSaveFolder.Text = "存图";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(352, 25);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(84, 30);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "选择路径";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // tbxSaveImageAddress
            // 
            this.tbxSaveImageAddress.Location = new System.Drawing.Point(47, 26);
            this.tbxSaveImageAddress.Name = "tbxSaveImageAddress";
            this.tbxSaveImageAddress.Size = new System.Drawing.Size(299, 25);
            this.tbxSaveImageAddress.TabIndex = 3;
            // 
            // ImageSaveToolUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ImageSaveToolUI";
            this.Size = new System.Drawing.Size(800, 600);
            this.tlpMain.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.gbResizeQuality.ResumeLayout(false);
            this.gbResizeQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.tplRight.ResumeLayout(false);
            this.gbSaveFolder.ResumeLayout(false);
            this.gbSaveFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private TerminalCollection.HyTerminalCollectionEdit hyTCEInput;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.GroupBox gbResizeQuality;
        private System.Windows.Forms.TableLayoutPanel tplRight;
        private System.Windows.Forms.GroupBox gbSaveFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbxSaveImageAddress;
    }
}
