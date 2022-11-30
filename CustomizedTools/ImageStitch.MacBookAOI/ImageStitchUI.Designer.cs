namespace ImageProcess
{
    partial class ImageStitchUI
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
            this.tplTopUp = new System.Windows.Forms.TableLayoutPanel();
            this.gbResizeQuality = new System.Windows.Forms.GroupBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.gbSend = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.gbSaveFolder = new System.Windows.Forms.GroupBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbxSaveImageAddress = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tlpMain.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tplTopUp.SuspendLayout();
            this.gbResizeQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.gbSend.SuspendLayout();
            this.gbSaveFolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.hyTCEInput, 0, 1);
            this.tlpMain.Controls.Add(this.tlpTop, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 27);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(5, 35, 5, 30);
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.14815F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.85185F));
            this.tlpMain.Size = new System.Drawing.Size(800, 551);
            this.tlpMain.TabIndex = 6;
            // 
            // hyTCEInput
            // 
            this.hyTCEInput.DefaultNameHeader = null;
            this.hyTCEInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTCEInput.Location = new System.Drawing.Point(8, 272);
            this.hyTCEInput.MaterialSubName = null;
            this.hyTCEInput.Name = "hyTCEInput";
            this.hyTCEInput.Size = new System.Drawing.Size(784, 246);
            this.hyTCEInput.TabIndex = 0;
            this.hyTCEInput.Text = "输入图像";
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 1;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTop.Controls.Add(this.tplTopUp, 0, 0);
            this.tlpTop.Controls.Add(this.gbSaveFolder, 0, 1);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(8, 38);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 2;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.38461F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.61538F));
            this.tlpTop.Size = new System.Drawing.Size(784, 228);
            this.tlpTop.TabIndex = 1;
            // 
            // tplTopUp
            // 
            this.tplTopUp.ColumnCount = 2;
            this.tplTopUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tplTopUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tplTopUp.Controls.Add(this.gbResizeQuality, 0, 0);
            this.tplTopUp.Controls.Add(this.gbSend, 1, 0);
            this.tplTopUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplTopUp.Location = new System.Drawing.Point(3, 3);
            this.tplTopUp.Name = "tplTopUp";
            this.tplTopUp.RowCount = 1;
            this.tplTopUp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tplTopUp.Size = new System.Drawing.Size(778, 143);
            this.tplTopUp.TabIndex = 3;
            // 
            // gbResizeQuality
            // 
            this.gbResizeQuality.Controls.Add(this.lblHeight);
            this.gbResizeQuality.Controls.Add(this.lblWidth);
            this.gbResizeQuality.Controls.Add(this.nudHeight);
            this.gbResizeQuality.Controls.Add(this.nudWidth);
            this.gbResizeQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbResizeQuality.Location = new System.Drawing.Point(3, 3);
            this.gbResizeQuality.Name = "gbResizeQuality";
            this.gbResizeQuality.Size = new System.Drawing.Size(305, 137);
            this.gbResizeQuality.TabIndex = 5;
            this.gbResizeQuality.TabStop = false;
            this.gbResizeQuality.Text = "调整图像大小和质量";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(34, 103);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(67, 15);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "源图高度";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(34, 52);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(67, 15);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "源图宽度";
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
            // gbSend
            // 
            this.gbSend.Controls.Add(this.btnConnect);
            this.gbSend.Controls.Add(this.lblPort);
            this.gbSend.Controls.Add(this.lblIP);
            this.gbSend.Controls.Add(this.tbxPort);
            this.gbSend.Controls.Add(this.tbxIP);
            this.gbSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSend.Location = new System.Drawing.Point(314, 3);
            this.gbSend.Name = "gbSend";
            this.gbSend.Size = new System.Drawing.Size(461, 137);
            this.gbSend.TabIndex = 4;
            this.gbSend.TabStop = false;
            this.gbSend.Text = "发送";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(352, 85);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(84, 30);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "测试连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(44, 93);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(37, 15);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "端口";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(44, 43);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(53, 15);
            this.lblIP.TabIndex = 2;
            this.lblIP.Text = "IP地址";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(130, 90);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(166, 25);
            this.tbxPort.TabIndex = 6;
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(130, 40);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(166, 25);
            this.tbxIP.TabIndex = 5;
            // 
            // gbSaveFolder
            // 
            this.gbSaveFolder.Controls.Add(this.lblQuality);
            this.gbSaveFolder.Controls.Add(this.nudQuality);
            this.gbSaveFolder.Controls.Add(this.btnSelectFolder);
            this.gbSaveFolder.Controls.Add(this.tbxSaveImageAddress);
            this.gbSaveFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSaveFolder.Location = new System.Drawing.Point(3, 152);
            this.gbSaveFolder.Name = "gbSaveFolder";
            this.gbSaveFolder.Size = new System.Drawing.Size(778, 73);
            this.gbSaveFolder.TabIndex = 4;
            this.gbSaveFolder.TabStop = false;
            this.gbSaveFolder.Text = "存图";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(471, 35);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(67, 15);
            this.lblQuality.TabIndex = 7;
            this.lblQuality.Text = "存图质量";
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(564, 25);
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(120, 25);
            this.nudQuality.TabIndex = 6;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(352, 22);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(84, 30);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "选择路径";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // tbxSaveImageAddress
            // 
            this.tbxSaveImageAddress.Location = new System.Drawing.Point(47, 25);
            this.tbxSaveImageAddress.Name = "tbxSaveImageAddress";
            this.tbxSaveImageAddress.Size = new System.Drawing.Size(299, 25);
            this.tbxSaveImageAddress.TabIndex = 3;
            // 
            // ImageStitchUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ImageStitchUI";
            this.Controls.SetChildIndex(this.tlpMain, 0);
            this.tlpMain.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tplTopUp.ResumeLayout(false);
            this.gbResizeQuality.ResumeLayout(false);
            this.gbResizeQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.gbSend.ResumeLayout(false);
            this.gbSend.PerformLayout();
            this.gbSaveFolder.ResumeLayout(false);
            this.gbSaveFolder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.TableLayoutPanel tplTopUp;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTCEInput;
        private System.Windows.Forms.GroupBox gbResizeQuality;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.GroupBox gbSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.GroupBox gbSaveFolder;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbxSaveImageAddress;
    }
}
