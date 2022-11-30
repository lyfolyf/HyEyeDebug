namespace HalconSDK.Engine.UI
{
    partial class HalconEngineUI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvOutputItems = new System.Windows.Forms.DataGridView();
            this.OutputColumnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutputColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutputColumnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvInputItems = new System.Windows.Forms.DataGridView();
            this.InputColumnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputColumnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.InputColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddInputItems = new System.Windows.Forms.Button();
            this.btnCoverInputItems = new System.Windows.Forms.Button();
            this.btnClearInputItems = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddOutputItems = new System.Windows.Forms.Button();
            this.btnCoverOutputItems = new System.Windows.Forms.Button();
            this.btnClearOutputItems = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtOutputItems = new System.Windows.Forms.RichTextBox();
            this.txtInputItems = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.hyImageDisplayControl = new HyVision.Tools.ImageDisplay.HyImageDisplayControl();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lstROI = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetROI = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputItems)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 660);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.txtFilePath, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOpenFile, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(880, 62);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(2, 2);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(700, 25);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenFile.Location = new System.Drawing.Point(706, 2);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(172, 58);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "打开文件....";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.dgvOutputItems, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.dgvInputItems, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(2, 332);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(880, 326);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // dgvOutputItems
            // 
            this.dgvOutputItems.AllowUserToAddRows = false;
            this.dgvOutputItems.AllowUserToDeleteRows = false;
            this.dgvOutputItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutputItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OutputColumnIndex,
            this.OutputColumnName,
            this.OutputColumnType});
            this.dgvOutputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutputItems.Location = new System.Drawing.Point(442, 2);
            this.dgvOutputItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOutputItems.MultiSelect = false;
            this.dgvOutputItems.Name = "dgvOutputItems";
            this.dgvOutputItems.RowHeadersWidth = 51;
            this.dgvOutputItems.RowTemplate.Height = 27;
            this.dgvOutputItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOutputItems.Size = new System.Drawing.Size(436, 322);
            this.dgvOutputItems.TabIndex = 1;
            // 
            // OutputColumnIndex
            // 
            this.OutputColumnIndex.FillWeight = 50F;
            this.OutputColumnIndex.HeaderText = "序号";
            this.OutputColumnIndex.MinimumWidth = 6;
            this.OutputColumnIndex.Name = "OutputColumnIndex";
            this.OutputColumnIndex.ReadOnly = true;
            this.OutputColumnIndex.Width = 50;
            // 
            // OutputColumnName
            // 
            this.OutputColumnName.FillWeight = 120F;
            this.OutputColumnName.HeaderText = "名称";
            this.OutputColumnName.MinimumWidth = 6;
            this.OutputColumnName.Name = "OutputColumnName";
            this.OutputColumnName.Width = 120;
            // 
            // OutputColumnType
            // 
            this.OutputColumnType.HeaderText = "数据类型";
            this.OutputColumnType.Items.AddRange(new object[] {
            "HTuple",
            "HObject",
            "HImage",
            "HRegion",
            "HXLD",
            "HObject_24",
            "HImage_24",
            "string"});
            this.OutputColumnType.MinimumWidth = 6;
            this.OutputColumnType.Name = "OutputColumnType";
            this.OutputColumnType.Width = 125;
            // 
            // dgvInputItems
            // 
            this.dgvInputItems.AllowUserToAddRows = false;
            this.dgvInputItems.AllowUserToDeleteRows = false;
            this.dgvInputItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InputColumnIndex,
            this.InputColumnName,
            this.InputColumnType,
            this.InputColumnValue});
            this.dgvInputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInputItems.Location = new System.Drawing.Point(2, 2);
            this.dgvInputItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInputItems.MultiSelect = false;
            this.dgvInputItems.Name = "dgvInputItems";
            this.dgvInputItems.RowHeadersWidth = 51;
            this.dgvInputItems.RowTemplate.Height = 27;
            this.dgvInputItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvInputItems.Size = new System.Drawing.Size(436, 322);
            this.dgvInputItems.TabIndex = 0;
            this.dgvInputItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInputItems_CellClick);
            // 
            // InputColumnIndex
            // 
            this.InputColumnIndex.FillWeight = 50F;
            this.InputColumnIndex.HeaderText = "序号";
            this.InputColumnIndex.MinimumWidth = 6;
            this.InputColumnIndex.Name = "InputColumnIndex";
            this.InputColumnIndex.ReadOnly = true;
            this.InputColumnIndex.Width = 50;
            // 
            // InputColumnName
            // 
            this.InputColumnName.FillWeight = 120F;
            this.InputColumnName.HeaderText = "名称";
            this.InputColumnName.MinimumWidth = 6;
            this.InputColumnName.Name = "InputColumnName";
            this.InputColumnName.Width = 120;
            // 
            // InputColumnType
            // 
            this.InputColumnType.HeaderText = "数据类型";
            this.InputColumnType.Items.AddRange(new object[] {
            "HTuple",
            "HObject",
            "HImage",
            "HRegion",
            "HXLD"});
            this.InputColumnType.MinimumWidth = 6;
            this.InputColumnType.Name = "InputColumnType";
            this.InputColumnType.Width = 125;
            // 
            // InputColumnValue
            // 
            this.InputColumnValue.HeaderText = "设定值";
            this.InputColumnValue.MinimumWidth = 6;
            this.InputColumnValue.Name = "InputColumnValue";
            this.InputColumnValue.ReadOnly = true;
            this.InputColumnValue.Width = 125;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 266);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(880, 62);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.btnAddInputItems, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnCoverInputItems, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnClearInputItems, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(436, 58);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // btnAddInputItems
            // 
            this.btnAddInputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddInputItems.Location = new System.Drawing.Point(2, 2);
            this.btnAddInputItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddInputItems.Name = "btnAddInputItems";
            this.btnAddInputItems.Size = new System.Drawing.Size(141, 54);
            this.btnAddInputItems.TabIndex = 0;
            this.btnAddInputItems.Text = "添加";
            this.btnAddInputItems.UseVisualStyleBackColor = true;
            this.btnAddInputItems.Click += new System.EventHandler(this.btnInsertInputItems_Click);
            // 
            // btnCoverInputItems
            // 
            this.btnCoverInputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCoverInputItems.Location = new System.Drawing.Point(147, 2);
            this.btnCoverInputItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnCoverInputItems.Name = "btnCoverInputItems";
            this.btnCoverInputItems.Size = new System.Drawing.Size(141, 54);
            this.btnCoverInputItems.TabIndex = 1;
            this.btnCoverInputItems.Text = "覆盖";
            this.btnCoverInputItems.UseVisualStyleBackColor = true;
            this.btnCoverInputItems.Click += new System.EventHandler(this.btnCoverInputItems_Click);
            // 
            // btnClearInputItems
            // 
            this.btnClearInputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearInputItems.Location = new System.Drawing.Point(292, 2);
            this.btnClearInputItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearInputItems.Name = "btnClearInputItems";
            this.btnClearInputItems.Size = new System.Drawing.Size(142, 54);
            this.btnClearInputItems.TabIndex = 2;
            this.btnClearInputItems.Text = "删除指定输入";
            this.btnClearInputItems.UseVisualStyleBackColor = true;
            this.btnClearInputItems.Click += new System.EventHandler(this.btnClearInputItems_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.btnAddOutputItems, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnCoverOutputItems, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnClearOutputItems, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(442, 2);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(436, 58);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // btnAddOutputItems
            // 
            this.btnAddOutputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddOutputItems.Location = new System.Drawing.Point(2, 2);
            this.btnAddOutputItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddOutputItems.Name = "btnAddOutputItems";
            this.btnAddOutputItems.Size = new System.Drawing.Size(141, 54);
            this.btnAddOutputItems.TabIndex = 0;
            this.btnAddOutputItems.Text = "添加";
            this.btnAddOutputItems.UseVisualStyleBackColor = true;
            this.btnAddOutputItems.Click += new System.EventHandler(this.btnInsertOutputItems_Click);
            // 
            // btnCoverOutputItems
            // 
            this.btnCoverOutputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCoverOutputItems.Location = new System.Drawing.Point(147, 2);
            this.btnCoverOutputItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnCoverOutputItems.Name = "btnCoverOutputItems";
            this.btnCoverOutputItems.Size = new System.Drawing.Size(141, 54);
            this.btnCoverOutputItems.TabIndex = 1;
            this.btnCoverOutputItems.Text = "覆盖";
            this.btnCoverOutputItems.UseVisualStyleBackColor = true;
            this.btnCoverOutputItems.Click += new System.EventHandler(this.btnCoverOutputItems_Click);
            // 
            // btnClearOutputItems
            // 
            this.btnClearOutputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearOutputItems.Location = new System.Drawing.Point(292, 2);
            this.btnClearOutputItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearOutputItems.Name = "btnClearOutputItems";
            this.btnClearOutputItems.Size = new System.Drawing.Size(142, 54);
            this.btnClearOutputItems.TabIndex = 2;
            this.btnClearOutputItems.Text = "删除指定输出";
            this.btnClearOutputItems.UseVisualStyleBackColor = true;
            this.btnClearOutputItems.Click += new System.EventHandler(this.btnClearOutputItems_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.txtOutputItems, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtInputItems, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 68);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(880, 194);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // txtOutputItems
            // 
            this.txtOutputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputItems.Location = new System.Drawing.Point(442, 2);
            this.txtOutputItems.Margin = new System.Windows.Forms.Padding(2);
            this.txtOutputItems.Name = "txtOutputItems";
            this.txtOutputItems.Size = new System.Drawing.Size(436, 190);
            this.txtOutputItems.TabIndex = 0;
            this.txtOutputItems.Text = "";
            // 
            // txtInputItems
            // 
            this.txtInputItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputItems.Location = new System.Drawing.Point(2, 2);
            this.txtInputItems.Margin = new System.Windows.Forms.Padding(2);
            this.txtInputItems.Name = "txtInputItems";
            this.txtInputItems.Size = new System.Drawing.Size(436, 190);
            this.txtInputItems.TabIndex = 1;
            this.txtInputItems.Text = "";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1480, 664);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.hyImageDisplayControl, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(890, 2);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(588, 660);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // hyImageDisplayControl
            // 
            this.hyImageDisplayControl.BottomToolVisible = true;
            this.hyImageDisplayControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyImageDisplayControl.EditRoiEnable = true;
            this.hyImageDisplayControl.Location = new System.Drawing.Point(3, 2);
            this.hyImageDisplayControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hyImageDisplayControl.Name = "hyImageDisplayControl";
            this.hyImageDisplayControl.ShowEditROIForm = true;
            this.hyImageDisplayControl.Size = new System.Drawing.Size(582, 458);
            this.hyImageDisplayControl.TabIndex = 2;
            this.hyImageDisplayControl.TopToolVisible = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel10.Controls.Add(this.lstROI, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel11, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 465);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(582, 192);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // lstROI
            // 
            this.lstROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstROI.FormattingEnabled = true;
            this.lstROI.ItemHeight = 15;
            this.lstROI.Location = new System.Drawing.Point(3, 3);
            this.lstROI.Name = "lstROI";
            this.lstROI.Size = new System.Drawing.Size(459, 186);
            this.lstROI.TabIndex = 0;
            this.lstROI.SelectedIndexChanged += new System.EventHandler(this.lstROI_SelectedIndexChanged);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.btnSetROI, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(468, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(111, 186);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // btnSetROI
            // 
            this.btnSetROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetROI.Enabled = false;
            this.btnSetROI.Location = new System.Drawing.Point(3, 3);
            this.btnSetROI.Name = "btnSetROI";
            this.btnSetROI.Size = new System.Drawing.Size(105, 180);
            this.btnSetROI.TabIndex = 0;
            this.btnSetROI.Text = "设定ROI";
            this.btnSetROI.UseVisualStyleBackColor = true;
            this.btnSetROI.Click += new System.EventHandler(this.btnSetROI_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 120F;
            this.dataGridViewTextBoxColumn2.HeaderText = "名称";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 50F;
            this.dataGridViewTextBoxColumn3.HeaderText = "序号";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 120F;
            this.dataGridViewTextBoxColumn4.HeaderText = "名称";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "设定值";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // HalconEngineUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel8);
            this.Name = "HalconEngineUI";
            this.Size = new System.Drawing.Size(1480, 713);
            this.Controls.SetChildIndex(this.tableLayoutPanel8, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputItems)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.RichTextBox txtOutputItems;
        private System.Windows.Forms.RichTextBox txtInputItems;
        private System.Windows.Forms.Button btnAddInputItems;
        private System.Windows.Forms.Button btnCoverInputItems;
        private System.Windows.Forms.Button btnClearInputItems;
        private System.Windows.Forms.Button btnAddOutputItems;
        private System.Windows.Forms.Button btnCoverOutputItems;
        private System.Windows.Forms.Button btnClearOutputItems;
        private System.Windows.Forms.DataGridView dgvOutputItems;
        private System.Windows.Forms.DataGridView dgvInputItems;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ListBox lstROI;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Button btnSetROI;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputColumnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn InputColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutputColumnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutputColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn OutputColumnType;
        private HyVision.Tools.ImageDisplay.HyImageDisplayControl hyImageDisplayControl;
    }
}
