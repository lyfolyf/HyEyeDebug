
namespace HalconSDK.Engine.UI
{
    partial class HalconEngineTool_UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HalconEngineTool_UI));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("01.1a");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("02.3a");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("03.5a");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("04.5b");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("AppliedROI2Image_01");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("WavinessProcess_01");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("FirstLevel", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("SetupCoordAxis_02");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("SecondLevel", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("ThirdLevel");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Other");
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
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
            this.cbxDebugModel = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxHalconFilePath = new System.Windows.Forms.TextBox();
            this.btnHalconFilePath = new System.Windows.Forms.Button();
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
            this.tvwFunctionList = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFold = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpread = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunctionParam)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.png");
            this.imageList1.Images.SetKeyName(1, "2.png");
            this.imageList1.Images.SetKeyName(2, "3.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2009, 894);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(2001, 860);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置引擎参数";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvFunctionParam);
            this.groupBox5.Location = new System.Drawing.Point(876, 6);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(1118, 844);
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
            this.dgvFunctionParam.Location = new System.Drawing.Point(4, 27);
            this.dgvFunctionParam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvFunctionParam.Name = "dgvFunctionParam";
            this.dgvFunctionParam.RowHeadersVisible = false;
            this.dgvFunctionParam.RowHeadersWidth = 51;
            this.dgvFunctionParam.RowTemplate.Height = 27;
            this.dgvFunctionParam.Size = new System.Drawing.Size(1110, 813);
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
            this.Column1.Width = 158;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "函数名";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 159;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "输入/输出";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 158;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "参数名";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 159;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "类型";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 158;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "值";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 159;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "开放连线";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 158;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDebugModel);
            this.groupBox1.Controls.Add(this.groupBox2);
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
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(864, 844);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置函数";
            // 
            // cbxDebugModel
            // 
            this.cbxDebugModel.AutoSize = true;
            this.cbxDebugModel.Location = new System.Drawing.Point(725, 801);
            this.cbxDebugModel.Name = "cbxDebugModel";
            this.cbxDebugModel.Size = new System.Drawing.Size(121, 24);
            this.cbxDebugModel.TabIndex = 20;
            this.cbxDebugModel.Text = "Debug模式";
            this.cbxDebugModel.UseVisualStyleBackColor = true;
            this.cbxDebugModel.CheckedChanged += new System.EventHandler(this.cbxDebugModel_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxHalconFilePath);
            this.groupBox2.Controls.Add(this.btnHalconFilePath);
            this.groupBox2.Location = new System.Drawing.Point(8, 31);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(838, 113);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hdpl文件路径";
            // 
            // tbxHalconFilePath
            // 
            this.tbxHalconFilePath.BackColor = System.Drawing.Color.White;
            this.tbxHalconFilePath.Location = new System.Drawing.Point(7, 42);
            this.tbxHalconFilePath.Margin = new System.Windows.Forms.Padding(3, 100, 3, 2);
            this.tbxHalconFilePath.Name = "tbxHalconFilePath";
            this.tbxHalconFilePath.ReadOnly = true;
            this.tbxHalconFilePath.Size = new System.Drawing.Size(649, 30);
            this.tbxHalconFilePath.TabIndex = 18;
            // 
            // btnHalconFilePath
            // 
            this.btnHalconFilePath.Location = new System.Drawing.Point(679, 40);
            this.btnHalconFilePath.Margin = new System.Windows.Forms.Padding(20, 25, 3, 2);
            this.btnHalconFilePath.Name = "btnHalconFilePath";
            this.btnHalconFilePath.Size = new System.Drawing.Size(117, 32);
            this.btnHalconFilePath.TabIndex = 17;
            this.btnHalconFilePath.Text = "....";
            this.btnHalconFilePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHalconFilePath.UseVisualStyleBackColor = true;
            this.btnHalconFilePath.Click += new System.EventHandler(this.btnHalconFilePath_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(715, 704);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(131, 45);
            this.btnConfirm.TabIndex = 15;
            this.btnConfirm.Text = "= >";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Visible = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(715, 631);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(131, 45);
            this.btnImport.TabIndex = 15;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(715, 559);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(131, 45);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(715, 487);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 45);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(715, 415);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(131, 45);
            this.btnMoveDown.TabIndex = 15;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(715, 343);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(131, 45);
            this.btnMoveUp.TabIndex = 15;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(715, 271);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(131, 45);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddFunc
            // 
            this.btnAddFunc.Location = new System.Drawing.Point(715, 199);
            this.btnAddFunc.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddFunc.Name = "btnAddFunc";
            this.btnAddFunc.Size = new System.Drawing.Size(131, 45);
            this.btnAddFunc.TabIndex = 15;
            this.btnAddFunc.Text = "添加";
            this.btnAddFunc.UseVisualStyleBackColor = true;
            this.btnAddFunc.Click += new System.EventHandler(this.btnAddFunc_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tvwSelectFuncList);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox7.Location = new System.Drawing.Point(356, 176);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox7.Size = new System.Drawing.Size(340, 664);
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
            this.tvwSelectFuncList.Location = new System.Drawing.Point(3, 25);
            this.tvwSelectFuncList.Margin = new System.Windows.Forms.Padding(4);
            this.tvwSelectFuncList.Name = "tvwSelectFuncList";
            treeNode1.Name = "节点0";
            treeNode1.Text = "01.1a";
            treeNode2.Name = "节点1";
            treeNode2.Text = "02.3a";
            treeNode3.Name = "节点2";
            treeNode3.Text = "03.5a";
            treeNode4.Name = "节点3";
            treeNode4.Text = "04.5b";
            this.tvwSelectFuncList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.tvwSelectFuncList.SelectedImageIndex = 0;
            this.tvwSelectFuncList.Size = new System.Drawing.Size(334, 637);
            this.tvwSelectFuncList.TabIndex = 1;
            this.tvwSelectFuncList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwSelectFuncList_NodeMouseDoubleClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tvwFunctionList);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox6.Location = new System.Drawing.Point(-1, 174);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Size = new System.Drawing.Size(340, 664);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "可选函数总列表";
            // 
            // tvwFunctionList
            // 
            this.tvwFunctionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwFunctionList.ImageIndex = 0;
            this.tvwFunctionList.ImageList = this.imageList1;
            this.tvwFunctionList.ItemHeight = 20;
            this.tvwFunctionList.Location = new System.Drawing.Point(3, 25);
            this.tvwFunctionList.Margin = new System.Windows.Forms.Padding(4);
            this.tvwFunctionList.Name = "tvwFunctionList";
            treeNode5.Name = "节点1";
            treeNode5.Text = "AppliedROI2Image_01";
            treeNode6.Name = "节点3";
            treeNode6.Text = "WavinessProcess_01";
            treeNode7.Name = "节点0";
            treeNode7.Text = "FirstLevel";
            treeNode8.Name = "节点4";
            treeNode8.Text = "SetupCoordAxis_02";
            treeNode9.Name = "节点1";
            treeNode9.Text = "SecondLevel";
            treeNode10.Name = "节点2";
            treeNode10.Text = "ThirdLevel";
            treeNode11.Name = "节点3";
            treeNode11.Text = "Other";
            this.tvwFunctionList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode9,
            treeNode10,
            treeNode11});
            this.tvwFunctionList.SelectedImageIndex = 0;
            this.tvwFunctionList.Size = new System.Drawing.Size(334, 637);
            this.tvwFunctionList.TabIndex = 1;
            this.tvwFunctionList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwFunctionList_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFold,
            this.tsmiSpread});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // tsmiFold
            // 
            this.tsmiFold.Name = "tsmiFold";
            this.tsmiFold.Size = new System.Drawing.Size(108, 24);
            this.tsmiFold.Text = "折叠";
            // 
            // tsmiSpread
            // 
            this.tsmiSpread.Name = "tsmiSpread";
            this.tsmiSpread.Size = new System.Drawing.Size(108, 24);
            this.tsmiSpread.Text = "展开";
            // 
            // HalconEngineTool_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HalconEngineTool_UI";
            this.Size = new System.Drawing.Size(2009, 894);
            this.Load += new System.EventHandler(this.HalconEngineTool_UI_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunctionParam)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvFunctionParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddFunc;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TreeView tvwSelectFuncList;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TreeView tvwFunctionList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxHalconFilePath;
        private System.Windows.Forms.Button btnHalconFilePath;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFold;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpread;
        private System.Windows.Forms.CheckBox cbxDebugModel;
    }
}
