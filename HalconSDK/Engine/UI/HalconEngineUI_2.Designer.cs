
namespace HalconSDK.Engine.UI
{
    partial class HalconEngineUI_2
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("01.1a");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("02.3a");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("03.5a");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("04.5b");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HalconEngineUI_2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("01.1a");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("02.3a");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("03.5a");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("04.5b");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("AppliedROI2Image_01");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("WavinessProcess_01");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("FirstLevel", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("SetupCoordAxis_02");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("SecondLevel", new System.Windows.Forms.TreeNode[] {
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("ThirdLevel");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Other");
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hyImageDisplayControl1 = new HyVision.Tools.ImageDisplay.HyImageDisplayControl();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnCreateROI = new System.Windows.Forms.Button();
            this.cbxMainAxis = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPixelRes = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gbxRoiInfo = new System.Windows.Forms.GroupBox();
            this.hyCircleInfo1 = new HyVision.Tools.ImageDisplay.HyCircleInfo();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tvwRoiList = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSaveData = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnReReadData = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxHalconFilePath = new System.Windows.Forms.TextBox();
            this.btnHalconFilePath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCsvFilePath = new System.Windows.Forms.TextBox();
            this.btnImagePath = new System.Windows.Forms.Button();
            this.tbxModelImagePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCSVPath = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvFunctionParam = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddFunc = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tvwSelectFuncList = new System.Windows.Forms.TreeView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tvwFuncList = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFold = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpread = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPixelRes)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.gbxRoiInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunctionParam)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1507, 715);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hyImageDisplayControl1);
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1499, 685);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "建立模板";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hyImageDisplayControl1
            // 
            this.hyImageDisplayControl1.BottomToolVisible = true;
            this.hyImageDisplayControl1.EditRoiEnable = true;
            this.hyImageDisplayControl1.Location = new System.Drawing.Point(716, 14);
            this.hyImageDisplayControl1.Margin = new System.Windows.Forms.Padding(2);
            this.hyImageDisplayControl1.Name = "hyImageDisplayControl1";
            this.hyImageDisplayControl1.ShowEditROIForm = false;
            this.hyImageDisplayControl1.Size = new System.Drawing.Size(778, 663);
            this.hyImageDisplayControl1.TabIndex = 0;
            this.hyImageDisplayControl1.TopToolVisible = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnCreateROI);
            this.groupBox8.Controls.Add(this.cbxMainAxis);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.nudPixelRes);
            this.groupBox8.Location = new System.Drawing.Point(8, 178);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(703, 88);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "设置参数";
            // 
            // btnCreateROI
            // 
            this.btnCreateROI.Location = new System.Drawing.Point(525, 38);
            this.btnCreateROI.Name = "btnCreateROI";
            this.btnCreateROI.Size = new System.Drawing.Size(146, 29);
            this.btnCreateROI.TabIndex = 8;
            this.btnCreateROI.Text = "生成ROI";
            this.btnCreateROI.UseVisualStyleBackColor = true;
            this.btnCreateROI.Click += new System.EventHandler(this.btnCreateROI_Click);
            // 
            // cbxMainAxis
            // 
            this.cbxMainAxis.FormattingEnabled = true;
            this.cbxMainAxis.Items.AddRange(new object[] {
            "x",
            "y"});
            this.cbxMainAxis.Location = new System.Drawing.Point(108, 40);
            this.cbxMainAxis.Name = "cbxMainAxis";
            this.cbxMainAxis.Size = new System.Drawing.Size(121, 24);
            this.cbxMainAxis.TabIndex = 4;
            this.cbxMainAxis.Text = "x";
            this.cbxMainAxis.SelectedIndexChanged += new System.EventHandler(this.cbxMainAxis_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "坐标主轴：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "分辨率：";
            // 
            // nudPixelRes
            // 
            this.nudPixelRes.DecimalPlaces = 3;
            this.nudPixelRes.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPixelRes.Location = new System.Drawing.Point(349, 39);
            this.nudPixelRes.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudPixelRes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPixelRes.Name = "nudPixelRes";
            this.nudPixelRes.Size = new System.Drawing.Size(120, 26);
            this.nudPixelRes.TabIndex = 6;
            this.nudPixelRes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPixelRes.Value = new decimal(new int[] {
            79,
            0,
            0,
            196608});
            this.nudPixelRes.ValueChanged += new System.EventHandler(this.nudPixelRes_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gbxRoiInfo);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(4, 284);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(707, 397);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "设置ROI坐标";
            // 
            // gbxRoiInfo
            // 
            this.gbxRoiInfo.Controls.Add(this.hyCircleInfo1);
            this.gbxRoiInfo.Location = new System.Drawing.Point(286, 26);
            this.gbxRoiInfo.Name = "gbxRoiInfo";
            this.gbxRoiInfo.Size = new System.Drawing.Size(363, 362);
            this.gbxRoiInfo.TabIndex = 13;
            this.gbxRoiInfo.TabStop = false;
            this.gbxRoiInfo.Text = "ROI坐标修改";
            // 
            // hyCircleInfo1
            // 
            this.hyCircleInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyCircleInfo1.Location = new System.Drawing.Point(3, 22);
            this.hyCircleInfo1.Margin = new System.Windows.Forms.Padding(2);
            this.hyCircleInfo1.Name = "hyCircleInfo1";
            this.hyCircleInfo1.Size = new System.Drawing.Size(357, 337);
            this.hyCircleInfo1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tvwRoiList);
            this.groupBox2.Controls.Add(this.btnSaveData);
            this.groupBox2.Controls.Add(this.btnDown);
            this.groupBox2.Controls.Add(this.btnReReadData);
            this.groupBox2.Controls.Add(this.btnUp);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(4, 26);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(265, 367);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ROI列表";
            // 
            // tvwRoiList
            // 
            this.tvwRoiList.ImageIndex = 2;
            this.tvwRoiList.ImageList = this.imageList1;
            this.tvwRoiList.ItemHeight = 20;
            this.tvwRoiList.Location = new System.Drawing.Point(13, 112);
            this.tvwRoiList.Name = "tvwRoiList";
            treeNode1.Name = "节点0";
            treeNode1.Text = "01.1a";
            treeNode2.Name = "节点1";
            treeNode2.Text = "02.3a";
            treeNode3.Name = "节点2";
            treeNode3.Text = "03.5a";
            treeNode4.Name = "节点3";
            treeNode4.Text = "04.5b";
            this.tvwRoiList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.tvwRoiList.SelectedImageIndex = 2;
            this.tvwRoiList.Size = new System.Drawing.Size(235, 250);
            this.tvwRoiList.TabIndex = 1;
            this.tvwRoiList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwRoiList_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.png");
            this.imageList1.Images.SetKeyName(1, "2.png");
            this.imageList1.Images.SetKeyName(2, "3.png");
            // 
            // btnSaveData
            // 
            this.btnSaveData.Font = new System.Drawing.Font("宋体", 11F);
            this.btnSaveData.Location = new System.Drawing.Point(147, 24);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(100, 35);
            this.btnSaveData.TabIndex = 0;
            this.btnSaveData.Text = "保存数据";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("宋体", 11F);
            this.btnDown.Location = new System.Drawing.Point(147, 71);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(100, 35);
            this.btnDown.TabIndex = 0;
            this.btnDown.Text = "下移";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnReReadData
            // 
            this.btnReReadData.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReReadData.Location = new System.Drawing.Point(16, 24);
            this.btnReReadData.Name = "btnReReadData";
            this.btnReReadData.Size = new System.Drawing.Size(100, 35);
            this.btnReReadData.TabIndex = 0;
            this.btnReReadData.Text = "重新读取";
            this.btnReReadData.UseVisualStyleBackColor = true;
            this.btnReReadData.Click += new System.EventHandler(this.btnReReadData_Click);
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("宋体", 11F);
            this.btnUp.Location = new System.Drawing.Point(16, 71);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(100, 35);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "上移";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxHalconFilePath);
            this.groupBox3.Controls.Add(this.btnHalconFilePath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbxCsvFilePath);
            this.groupBox3.Controls.Add(this.btnImagePath);
            this.groupBox3.Controls.Add(this.tbxModelImagePath);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnCSVPath);
            this.groupBox3.Location = new System.Drawing.Point(4, 5);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(707, 168);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "设置文件路径";
            // 
            // tbxHalconFilePath
            // 
            this.tbxHalconFilePath.BackColor = System.Drawing.Color.White;
            this.tbxHalconFilePath.Location = new System.Drawing.Point(142, 32);
            this.tbxHalconFilePath.Margin = new System.Windows.Forms.Padding(2, 80, 2, 2);
            this.tbxHalconFilePath.Name = "tbxHalconFilePath";
            this.tbxHalconFilePath.ReadOnly = true;
            this.tbxHalconFilePath.Size = new System.Drawing.Size(449, 26);
            this.tbxHalconFilePath.TabIndex = 13;
            // 
            // btnHalconFilePath
            // 
            this.btnHalconFilePath.Location = new System.Drawing.Point(610, 32);
            this.btnHalconFilePath.Margin = new System.Windows.Forms.Padding(15, 20, 2, 2);
            this.btnHalconFilePath.Name = "btnHalconFilePath";
            this.btnHalconFilePath.Size = new System.Drawing.Size(88, 26);
            this.btnHalconFilePath.TabIndex = 12;
            this.btnHalconFilePath.Text = "....";
            this.btnHalconFilePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHalconFilePath.UseVisualStyleBackColor = true;
            this.btnHalconFilePath.Click += new System.EventHandler(this.btnHalconFilePath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "模板图片路径：";
            // 
            // tbxCsvFilePath
            // 
            this.tbxCsvFilePath.BackColor = System.Drawing.Color.White;
            this.tbxCsvFilePath.Location = new System.Drawing.Point(142, 120);
            this.tbxCsvFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.tbxCsvFilePath.Name = "tbxCsvFilePath";
            this.tbxCsvFilePath.ReadOnly = true;
            this.tbxCsvFilePath.Size = new System.Drawing.Size(449, 26);
            this.tbxCsvFilePath.TabIndex = 2;
            // 
            // btnImagePath
            // 
            this.btnImagePath.Location = new System.Drawing.Point(610, 76);
            this.btnImagePath.Margin = new System.Windows.Forms.Padding(2);
            this.btnImagePath.Name = "btnImagePath";
            this.btnImagePath.Size = new System.Drawing.Size(88, 26);
            this.btnImagePath.TabIndex = 3;
            this.btnImagePath.Text = "....";
            this.btnImagePath.UseVisualStyleBackColor = true;
            this.btnImagePath.Click += new System.EventHandler(this.btnImagePath_Click);
            // 
            // tbxModelImagePath
            // 
            this.tbxModelImagePath.BackColor = System.Drawing.Color.White;
            this.tbxModelImagePath.Location = new System.Drawing.Point(142, 76);
            this.tbxModelImagePath.Margin = new System.Windows.Forms.Padding(2);
            this.tbxModelImagePath.Name = "tbxModelImagePath";
            this.tbxModelImagePath.ReadOnly = true;
            this.tbxModelImagePath.Size = new System.Drawing.Size(449, 26);
            this.tbxModelImagePath.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-2, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Halcon程序路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "ROI坐标路径：";
            // 
            // btnCSVPath
            // 
            this.btnCSVPath.Location = new System.Drawing.Point(610, 120);
            this.btnCSVPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnCSVPath.Name = "btnCSVPath";
            this.btnCSVPath.Size = new System.Drawing.Size(88, 26);
            this.btnCSVPath.TabIndex = 3;
            this.btnCSVPath.Text = "....";
            this.btnCSVPath.UseVisualStyleBackColor = true;
            this.btnCSVPath.Click += new System.EventHandler(this.btnCSVPath_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1499, 685);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置引擎参数";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvFunctionParam);
            this.groupBox5.Location = new System.Drawing.Point(657, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(840, 675);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "配置函数参数";
            // 
            // dgvFunctionParam
            // 
            this.dgvFunctionParam.AllowUserToAddRows = false;
            this.dgvFunctionParam.AllowUserToDeleteRows = false;
            this.dgvFunctionParam.AllowUserToResizeRows = false;
            this.dgvFunctionParam.BackgroundColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFunctionParam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFunctionParam.ColumnHeadersHeight = 30;
            this.dgvFunctionParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFunctionParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFunctionParam.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFunctionParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFunctionParam.Location = new System.Drawing.Point(3, 22);
            this.dgvFunctionParam.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFunctionParam.Name = "dgvFunctionParam";
            this.dgvFunctionParam.RowHeadersVisible = false;
            this.dgvFunctionParam.RowHeadersWidth = 51;
            this.dgvFunctionParam.RowTemplate.Height = 27;
            this.dgvFunctionParam.Size = new System.Drawing.Size(834, 650);
            this.dgvFunctionParam.TabIndex = 8;
            this.dgvFunctionParam.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFunctionParam_CellClick);
            this.dgvFunctionParam.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvFunctionParam_CellPainting);
            this.dgvFunctionParam.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFunctionParam_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 124;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "函数名";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "输入/输出";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 124;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "参数名";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 124;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "类型";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "值";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 124;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "开放连线";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAddFunc);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 675);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置函数";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(536, 541);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(98, 36);
            this.btnConfirm.TabIndex = 15;
            this.btnConfirm.Text = "= >";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Visible = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(536, 442);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(98, 36);
            this.btnImport.TabIndex = 15;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(536, 382);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(98, 36);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(536, 283);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 36);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(536, 223);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(98, 36);
            this.btnMoveDown.TabIndex = 15;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(536, 163);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(98, 36);
            this.btnMoveUp.TabIndex = 15;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(536, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(98, 36);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddFunc
            // 
            this.btnAddFunc.Location = new System.Drawing.Point(536, 43);
            this.btnAddFunc.Name = "btnAddFunc";
            this.btnAddFunc.Size = new System.Drawing.Size(98, 36);
            this.btnAddFunc.TabIndex = 15;
            this.btnAddFunc.Text = "添加";
            this.btnAddFunc.UseVisualStyleBackColor = true;
            this.btnAddFunc.Click += new System.EventHandler(this.btnAddFunc_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tvwSelectFuncList);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox7.Location = new System.Drawing.Point(267, 24);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(255, 648);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "已选函数列表";
            // 
            // tvwSelectFuncList
            // 
            this.tvwSelectFuncList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSelectFuncList.ImageIndex = 1;
            this.tvwSelectFuncList.ImageList = this.imageList1;
            this.tvwSelectFuncList.ItemHeight = 20;
            this.tvwSelectFuncList.Location = new System.Drawing.Point(2, 21);
            this.tvwSelectFuncList.Name = "tvwSelectFuncList";
            treeNode5.Name = "节点0";
            treeNode5.Text = "01.1a";
            treeNode6.Name = "节点1";
            treeNode6.Text = "02.3a";
            treeNode7.Name = "节点2";
            treeNode7.Text = "03.5a";
            treeNode8.Name = "节点3";
            treeNode8.Text = "04.5b";
            this.tvwSelectFuncList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.tvwSelectFuncList.SelectedImageIndex = 0;
            this.tvwSelectFuncList.Size = new System.Drawing.Size(251, 625);
            this.tvwSelectFuncList.TabIndex = 1;
            this.tvwSelectFuncList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwSelectFuncList_NodeMouseClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tvwFuncList);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox6.Location = new System.Drawing.Point(-1, 22);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(255, 648);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "可选函数总列表";
            // 
            // tvwFuncList
            // 
            this.tvwFuncList.ContextMenuStrip = this.contextMenuStrip1;
            this.tvwFuncList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwFuncList.ImageIndex = 0;
            this.tvwFuncList.ImageList = this.imageList1;
            this.tvwFuncList.ItemHeight = 20;
            this.tvwFuncList.Location = new System.Drawing.Point(2, 21);
            this.tvwFuncList.Name = "tvwFuncList";
            treeNode9.Name = "节点1";
            treeNode9.Text = "AppliedROI2Image_01";
            treeNode10.Name = "节点3";
            treeNode10.Text = "WavinessProcess_01";
            treeNode11.Name = "节点0";
            treeNode11.Text = "FirstLevel";
            treeNode12.Name = "节点4";
            treeNode12.Text = "SetupCoordAxis_02";
            treeNode13.Name = "节点1";
            treeNode13.Text = "SecondLevel";
            treeNode14.Name = "节点2";
            treeNode14.Text = "ThirdLevel";
            treeNode15.Name = "节点3";
            treeNode15.Text = "Other";
            this.tvwFuncList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode13,
            treeNode14,
            treeNode15});
            this.tvwFuncList.SelectedImageIndex = 0;
            this.tvwFuncList.Size = new System.Drawing.Size(251, 625);
            this.tvwFuncList.TabIndex = 1;
            this.tvwFuncList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwFuncList_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFold,
            this.tsmiSpread});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmiFold
            // 
            this.tsmiFold.Name = "tsmiFold";
            this.tsmiFold.Size = new System.Drawing.Size(100, 22);
            this.tsmiFold.Text = "折叠";
            this.tsmiFold.Click += new System.EventHandler(this.tsmiFold_Click);
            // 
            // tsmiSpread
            // 
            this.tsmiSpread.Name = "tsmiSpread";
            this.tsmiSpread.Size = new System.Drawing.Size(100, 22);
            this.tsmiSpread.Text = "展开";
            this.tsmiSpread.Click += new System.EventHandler(this.tsmiSpread_Click);
            // 
            // HalconEngineUI_2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tabControl1);
            this.Name = "HalconEngineUI_2";
            this.Size = new System.Drawing.Size(1507, 764);
            this.Load += new System.EventHandler(this.HalconEngineUI_2_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPixelRes)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.gbxRoiInfo.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunctionParam)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCSVPath;
        private System.Windows.Forms.TextBox tbxModelImagePath;
        private System.Windows.Forms.Button btnImagePath;
        private System.Windows.Forms.TextBox tbxCsvFilePath;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvFunctionParam;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbxRoiInfo;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Button btnReReadData;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TreeView tvwRoiList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudPixelRes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxMainAxis;
        private System.Windows.Forms.Button btnCreateROI;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TreeView tvwSelectFuncList;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TreeView tvwFuncList;
        private System.Windows.Forms.TextBox tbxHalconFilePath;
        private System.Windows.Forms.Button btnHalconFilePath;
        private System.Windows.Forms.Label label6;
        private HyVision.Tools.ImageDisplay.HyImageDisplayControl hyImageDisplayControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddFunc;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFold;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpread;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private HyVision.Tools.ImageDisplay.HyCircleInfo hyCircleInfo1;
    }
}
