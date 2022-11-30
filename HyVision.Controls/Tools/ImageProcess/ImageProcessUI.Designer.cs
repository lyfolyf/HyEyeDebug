
namespace HyVision.Tools.ImageProcess
{
    partial class ImageProcessUI
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
            this.tabImageSave = new System.Windows.Forms.TabControl();
            this.tabCompressSave = new System.Windows.Forms.TabPage();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbxSaveImageAddress = new System.Windows.Forms.TextBox();
            this.tabNgSave = new System.Windows.Forms.TabPage();
            this.btnNgSave = new System.Windows.Forms.Button();
            this.tbxSaveNgImage = new System.Windows.Forms.TextBox();
            this.tabSend = new System.Windows.Forms.TabControl();
            this.tabSendImage = new System.Windows.Forms.TabPage();
            this.cbxTestMode = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.tabSendResult = new System.Windows.Forms.TabPage();
            this.btnTcpConnect = new System.Windows.Forms.Button();
            this.lblTcpPort = new System.Windows.Forms.Label();
            this.lblTcpIP = new System.Windows.Forms.Label();
            this.tbxTcpPort = new System.Windows.Forms.TextBox();
            this.tbxTcpIPAddress = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxSaveNgImage = new System.Windows.Forms.CheckBox();
            this.cbxSendResult = new System.Windows.Forms.CheckBox();
            this.cbxResize = new System.Windows.Forms.CheckBox();
            this.cbxSendImage = new System.Windows.Forms.CheckBox();
            this.cbxSaveImage = new System.Windows.Forms.CheckBox();
            this.cbxRotation = new System.Windows.Forms.CheckBox();
            this.cbxQuality = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tlpMain.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.gbResizeQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.tplRight.SuspendLayout();
            this.tabImageSave.SuspendLayout();
            this.tabCompressSave.SuspendLayout();
            this.tabNgSave.SuspendLayout();
            this.tabSend.SuspendLayout();
            this.tabSendImage.SuspendLayout();
            this.tabSendResult.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.hyTCEInput, 0, 2);
            this.tlpMain.Controls.Add(this.tlpTop, 0, 1);
            this.tlpMain.Controls.Add(this.panel1, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 27);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(5, 35, 5, 30);
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.15888F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.93458F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(800, 551);
            this.tlpMain.TabIndex = 6;
            // 
            // hyTCEInput
            // 
            this.hyTCEInput.DefaultNameHeader = null;
            this.hyTCEInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTCEInput.Location = new System.Drawing.Point(8, 324);
            this.hyTCEInput.MaterialSubName = null;
            this.hyTCEInput.Name = "hyTCEInput";
            this.hyTCEInput.Size = new System.Drawing.Size(784, 194);
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
            this.tlpTop.Location = new System.Drawing.Point(8, 86);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tlpTop.Size = new System.Drawing.Size(784, 232);
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
            this.gbResizeQuality.Size = new System.Drawing.Size(307, 226);
            this.gbResizeQuality.TabIndex = 1;
            this.gbResizeQuality.TabStop = false;
            this.gbResizeQuality.Text = "调整图像大小和质量";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Enabled = false;
            this.lblQuality.Location = new System.Drawing.Point(34, 155);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(67, 15);
            this.lblQuality.TabIndex = 5;
            this.lblQuality.Text = "图像质量";
            // 
            // nudQuality
            // 
            this.nudQuality.Enabled = false;
            this.nudQuality.Location = new System.Drawing.Point(127, 145);
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(120, 25);
            this.nudQuality.TabIndex = 2;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Enabled = false;
            this.lblHeight.Location = new System.Drawing.Point(34, 103);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(67, 15);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "图像高度";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Enabled = false;
            this.lblWidth.Location = new System.Drawing.Point(34, 52);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(67, 15);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "图像宽度";
            // 
            // nudHeight
            // 
            this.nudHeight.Enabled = false;
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
            this.nudWidth.Enabled = false;
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
            this.tplRight.Controls.Add(this.tabImageSave, 0, 0);
            this.tplRight.Controls.Add(this.tabSend, 0, 1);
            this.tplRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplRight.Location = new System.Drawing.Point(316, 3);
            this.tplRight.Name = "tplRight";
            this.tplRight.RowCount = 2;
            this.tplRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tplRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tplRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplRight.Size = new System.Drawing.Size(465, 226);
            this.tplRight.TabIndex = 3;
            // 
            // tabImageSave
            // 
            this.tabImageSave.Controls.Add(this.tabCompressSave);
            this.tabImageSave.Controls.Add(this.tabNgSave);
            this.tabImageSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabImageSave.Location = new System.Drawing.Point(3, 3);
            this.tabImageSave.Name = "tabImageSave";
            this.tabImageSave.SelectedIndex = 0;
            this.tabImageSave.Size = new System.Drawing.Size(459, 73);
            this.tabImageSave.TabIndex = 10;
            // 
            // tabCompressSave
            // 
            this.tabCompressSave.BackColor = System.Drawing.SystemColors.Control;
            this.tabCompressSave.Controls.Add(this.btnSelectFolder);
            this.tabCompressSave.Controls.Add(this.tbxSaveImageAddress);
            this.tabCompressSave.Location = new System.Drawing.Point(4, 25);
            this.tabCompressSave.Name = "tabCompressSave";
            this.tabCompressSave.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompressSave.Size = new System.Drawing.Size(451, 44);
            this.tabCompressSave.TabIndex = 0;
            this.tabCompressSave.Text = "存压缩图";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(326, 9);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(84, 30);
            this.btnSelectFolder.TabIndex = 6;
            this.btnSelectFolder.Text = "选择路径";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // tbxSaveImageAddress
            // 
            this.tbxSaveImageAddress.Location = new System.Drawing.Point(21, 10);
            this.tbxSaveImageAddress.Name = "tbxSaveImageAddress";
            this.tbxSaveImageAddress.Size = new System.Drawing.Size(299, 25);
            this.tbxSaveImageAddress.TabIndex = 5;
            // 
            // tabNgSave
            // 
            this.tabNgSave.BackColor = System.Drawing.SystemColors.Control;
            this.tabNgSave.Controls.Add(this.btnNgSave);
            this.tabNgSave.Controls.Add(this.tbxSaveNgImage);
            this.tabNgSave.Location = new System.Drawing.Point(4, 25);
            this.tabNgSave.Name = "tabNgSave";
            this.tabNgSave.Padding = new System.Windows.Forms.Padding(3);
            this.tabNgSave.Size = new System.Drawing.Size(451, 44);
            this.tabNgSave.TabIndex = 1;
            this.tabNgSave.Text = "存NG原图";
            // 
            // btnNgSave
            // 
            this.btnNgSave.Location = new System.Drawing.Point(326, 9);
            this.btnNgSave.Name = "btnNgSave";
            this.btnNgSave.Size = new System.Drawing.Size(84, 30);
            this.btnNgSave.TabIndex = 6;
            this.btnNgSave.Text = "选择路径";
            this.btnNgSave.UseVisualStyleBackColor = true;
            this.btnNgSave.Click += new System.EventHandler(this.btnSelectNGFolder_Click);
            // 
            // tbxSaveNgImage
            // 
            this.tbxSaveNgImage.Location = new System.Drawing.Point(21, 10);
            this.tbxSaveNgImage.Name = "tbxSaveNgImage";
            this.tbxSaveNgImage.Size = new System.Drawing.Size(299, 25);
            this.tbxSaveNgImage.TabIndex = 5;
            // 
            // tabSend
            // 
            this.tabSend.Controls.Add(this.tabSendImage);
            this.tabSend.Controls.Add(this.tabSendResult);
            this.tabSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSend.Location = new System.Drawing.Point(3, 82);
            this.tabSend.Name = "tabSend";
            this.tabSend.SelectedIndex = 0;
            this.tabSend.Size = new System.Drawing.Size(459, 141);
            this.tabSend.TabIndex = 3;
            // 
            // tabSendImage
            // 
            this.tabSendImage.BackColor = System.Drawing.SystemColors.Control;
            this.tabSendImage.Controls.Add(this.cbxTestMode);
            this.tabSendImage.Controls.Add(this.btnConnect);
            this.tabSendImage.Controls.Add(this.lblPort);
            this.tabSendImage.Controls.Add(this.lblIP);
            this.tabSendImage.Controls.Add(this.tbxPort);
            this.tabSendImage.Controls.Add(this.tbxIP);
            this.tabSendImage.Location = new System.Drawing.Point(4, 25);
            this.tabSendImage.Name = "tabSendImage";
            this.tabSendImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabSendImage.Size = new System.Drawing.Size(451, 112);
            this.tabSendImage.TabIndex = 0;
            this.tabSendImage.Text = "发送图像";
            // 
            // cbxTestMode
            // 
            this.cbxTestMode.AutoSize = true;
            this.cbxTestMode.Location = new System.Drawing.Point(335, 32);
            this.cbxTestMode.Name = "cbxTestMode";
            this.cbxTestMode.Size = new System.Drawing.Size(89, 19);
            this.cbxTestMode.TabIndex = 14;
            this.cbxTestMode.Text = "测试模式";
            this.cbxTestMode.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(335, 74);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(84, 30);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "测试连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(27, 82);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(37, 15);
            this.lblPort.TabIndex = 10;
            this.lblPort.Text = "端口";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(27, 32);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(53, 15);
            this.lblIP.TabIndex = 9;
            this.lblIP.Text = "IP地址";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(113, 79);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(166, 25);
            this.tbxPort.TabIndex = 12;
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(113, 29);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(166, 25);
            this.tbxIP.TabIndex = 11;
            // 
            // tabSendResult
            // 
            this.tabSendResult.BackColor = System.Drawing.SystemColors.Control;
            this.tabSendResult.Controls.Add(this.btnTcpConnect);
            this.tabSendResult.Controls.Add(this.lblTcpPort);
            this.tabSendResult.Controls.Add(this.lblTcpIP);
            this.tabSendResult.Controls.Add(this.tbxTcpPort);
            this.tabSendResult.Controls.Add(this.tbxTcpIPAddress);
            this.tabSendResult.Location = new System.Drawing.Point(4, 25);
            this.tabSendResult.Name = "tabSendResult";
            this.tabSendResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabSendResult.Size = new System.Drawing.Size(451, 112);
            this.tabSendResult.TabIndex = 1;
            this.tabSendResult.Text = "发送结果";
            // 
            // btnTcpConnect
            // 
            this.btnTcpConnect.Location = new System.Drawing.Point(335, 74);
            this.btnTcpConnect.Name = "btnTcpConnect";
            this.btnTcpConnect.Size = new System.Drawing.Size(84, 30);
            this.btnTcpConnect.TabIndex = 13;
            this.btnTcpConnect.Text = "测试连接";
            this.btnTcpConnect.UseVisualStyleBackColor = true;
            this.btnTcpConnect.Click += new System.EventHandler(this.btnTcpConnect_Click);
            // 
            // lblTcpPort
            // 
            this.lblTcpPort.AutoSize = true;
            this.lblTcpPort.Location = new System.Drawing.Point(27, 82);
            this.lblTcpPort.Name = "lblTcpPort";
            this.lblTcpPort.Size = new System.Drawing.Size(37, 15);
            this.lblTcpPort.TabIndex = 10;
            this.lblTcpPort.Text = "端口";
            // 
            // lblTcpIP
            // 
            this.lblTcpIP.AutoSize = true;
            this.lblTcpIP.Location = new System.Drawing.Point(27, 32);
            this.lblTcpIP.Name = "lblTcpIP";
            this.lblTcpIP.Size = new System.Drawing.Size(53, 15);
            this.lblTcpIP.TabIndex = 9;
            this.lblTcpIP.Text = "IP地址";
            // 
            // tbxTcpPort
            // 
            this.tbxTcpPort.Location = new System.Drawing.Point(113, 79);
            this.tbxTcpPort.Name = "tbxTcpPort";
            this.tbxTcpPort.Size = new System.Drawing.Size(166, 25);
            this.tbxTcpPort.TabIndex = 12;
            // 
            // tbxTcpIPAddress
            // 
            this.tbxTcpIPAddress.Location = new System.Drawing.Point(113, 29);
            this.tbxTcpIPAddress.Name = "tbxTcpIPAddress";
            this.tbxTcpIPAddress.Size = new System.Drawing.Size(166, 25);
            this.tbxTcpIPAddress.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxSaveNgImage);
            this.panel1.Controls.Add(this.cbxSendResult);
            this.panel1.Controls.Add(this.cbxResize);
            this.panel1.Controls.Add(this.cbxSendImage);
            this.panel1.Controls.Add(this.cbxSaveImage);
            this.panel1.Controls.Add(this.cbxRotation);
            this.panel1.Controls.Add(this.cbxQuality);
            this.panel1.Location = new System.Drawing.Point(8, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 42);
            this.panel1.TabIndex = 2;
            // 
            // cbxSaveNgImage
            // 
            this.cbxSaveNgImage.AutoSize = true;
            this.cbxSaveNgImage.Location = new System.Drawing.Point(453, 14);
            this.cbxSaveNgImage.Name = "cbxSaveNgImage";
            this.cbxSaveNgImage.Size = new System.Drawing.Size(90, 19);
            this.cbxSaveNgImage.TabIndex = 6;
            this.cbxSaveNgImage.Text = "存NG原图";
            this.cbxSaveNgImage.UseVisualStyleBackColor = true;
            this.cbxSaveNgImage.CheckedChanged += new System.EventHandler(this.cbxSaveNgImage_CheckedChanged);
            // 
            // cbxSendResult
            // 
            this.cbxSendResult.AutoSize = true;
            this.cbxSendResult.Location = new System.Drawing.Point(672, 14);
            this.cbxSendResult.Name = "cbxSendResult";
            this.cbxSendResult.Size = new System.Drawing.Size(89, 19);
            this.cbxSendResult.TabIndex = 5;
            this.cbxSendResult.Text = "发送结果";
            this.cbxSendResult.UseVisualStyleBackColor = true;
            this.cbxSendResult.CheckedChanged += new System.EventHandler(this.cbxSendResult_CheckedChanged);
            // 
            // cbxResize
            // 
            this.cbxResize.AutoSize = true;
            this.cbxResize.Location = new System.Drawing.Point(17, 14);
            this.cbxResize.Name = "cbxResize";
            this.cbxResize.Size = new System.Drawing.Size(89, 19);
            this.cbxResize.TabIndex = 4;
            this.cbxResize.Text = "图像大小";
            this.cbxResize.UseVisualStyleBackColor = true;
            this.cbxResize.CheckedChanged += new System.EventHandler(this.cbxResize_CheckedChanged);
            // 
            // cbxSendImage
            // 
            this.cbxSendImage.AutoSize = true;
            this.cbxSendImage.Location = new System.Drawing.Point(563, 14);
            this.cbxSendImage.Name = "cbxSendImage";
            this.cbxSendImage.Size = new System.Drawing.Size(89, 19);
            this.cbxSendImage.TabIndex = 3;
            this.cbxSendImage.Text = "发送图像";
            this.cbxSendImage.UseVisualStyleBackColor = true;
            this.cbxSendImage.CheckedChanged += new System.EventHandler(this.cbxSendImage_CheckedChanged);
            // 
            // cbxSaveImage
            // 
            this.cbxSaveImage.AutoSize = true;
            this.cbxSaveImage.Location = new System.Drawing.Point(344, 14);
            this.cbxSaveImage.Name = "cbxSaveImage";
            this.cbxSaveImage.Size = new System.Drawing.Size(89, 19);
            this.cbxSaveImage.TabIndex = 2;
            this.cbxSaveImage.Text = "保存图像";
            this.cbxSaveImage.UseVisualStyleBackColor = true;
            this.cbxSaveImage.CheckedChanged += new System.EventHandler(this.cbxSaveImage_CheckedChanged);
            // 
            // cbxRotation
            // 
            this.cbxRotation.AutoSize = true;
            this.cbxRotation.Location = new System.Drawing.Point(126, 14);
            this.cbxRotation.Name = "cbxRotation";
            this.cbxRotation.Size = new System.Drawing.Size(89, 19);
            this.cbxRotation.TabIndex = 1;
            this.cbxRotation.Text = "图像旋转";
            this.cbxRotation.UseVisualStyleBackColor = true;
            // 
            // cbxQuality
            // 
            this.cbxQuality.AutoSize = true;
            this.cbxQuality.Location = new System.Drawing.Point(235, 14);
            this.cbxQuality.Name = "cbxQuality";
            this.cbxQuality.Size = new System.Drawing.Size(89, 19);
            this.cbxQuality.TabIndex = 0;
            this.cbxQuality.Text = "图像质量";
            this.cbxQuality.UseVisualStyleBackColor = true;
            this.cbxQuality.CheckedChanged += new System.EventHandler(this.cbxQuality_CheckedChanged);
            // 
            // ImageProcessUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ImageProcessUI";
            this.Controls.SetChildIndex(this.tlpMain, 0);
            this.tlpMain.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.gbResizeQuality.ResumeLayout(false);
            this.gbResizeQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.tplRight.ResumeLayout(false);
            this.tabImageSave.ResumeLayout(false);
            this.tabCompressSave.ResumeLayout(false);
            this.tabCompressSave.PerformLayout();
            this.tabNgSave.ResumeLayout(false);
            this.tabNgSave.PerformLayout();
            this.tabSend.ResumeLayout(false);
            this.tabSendImage.ResumeLayout(false);
            this.tabSendImage.PerformLayout();
            this.tabSendResult.ResumeLayout(false);
            this.tabSendResult.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private TerminalCollection.HyTerminalCollectionEdit hyTCEInput;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.GroupBox gbResizeQuality;
        private System.Windows.Forms.TableLayoutPanel tplRight;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbxRotation;
        private System.Windows.Forms.CheckBox cbxQuality;
        private System.Windows.Forms.CheckBox cbxSendImage;
        private System.Windows.Forms.CheckBox cbxSaveImage;
        private System.Windows.Forms.CheckBox cbxResize;
        private System.Windows.Forms.TabControl tabSend;
        private System.Windows.Forms.TabPage tabSendImage;
        private System.Windows.Forms.TabPage tabSendResult;
        private System.Windows.Forms.Button btnTcpConnect;
        private System.Windows.Forms.Label lblTcpPort;
        private System.Windows.Forms.Label lblTcpIP;
        private System.Windows.Forms.TextBox tbxTcpPort;
        private System.Windows.Forms.TextBox tbxTcpIPAddress;
        private System.Windows.Forms.CheckBox cbxSendResult;
        private System.Windows.Forms.CheckBox cbxTestMode;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.CheckBox cbxSaveNgImage;
        private System.Windows.Forms.TabControl tabImageSave;
        private System.Windows.Forms.TabPage tabCompressSave;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbxSaveImageAddress;
        private System.Windows.Forms.TabPage tabNgSave;
        private System.Windows.Forms.Button btnNgSave;
        private System.Windows.Forms.TextBox tbxSaveNgImage;
    }
}
