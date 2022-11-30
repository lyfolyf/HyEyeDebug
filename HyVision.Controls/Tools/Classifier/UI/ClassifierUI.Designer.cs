
namespace HyVision.Tools.Classifier.UI
{
    partial class ClassifierUI
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hyTerminalCollInputData = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdvanceRuleOut = new System.Windows.Forms.Button();
            this.btnAddAdvanceRule = new System.Windows.Forms.Button();
            this.btnDeleteAdvanceRule = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvAdvanceRule = new System.Windows.Forms.DataGridView();
            this.ColumnAdvanceRuleSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnAdvanceRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvOutputRule = new System.Windows.Forms.DataGridView();
            this.ColumnOutputName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOutputRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRuleDirectOut = new System.Windows.Forms.Button();
            this.btnRuleOr = new System.Windows.Forms.Button();
            this.btnRuleAnd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvOriginalRule = new System.Windows.Forms.DataGridView();
            this.ColumnOriginalRuleSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnOriginalRuleInputName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnOriginalRuleOperator = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnOriginalRuleThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddOriginalRule = new System.Windows.Forms.Button();
            this.btnDeleteOriginalRule = new System.Windows.Forms.Button();
            this.btnDeleteOutputRule = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanceRule)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputRule)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginalRule)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hyTerminalCollInputData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1400, 651);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(423, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(974, 65);
            this.label2.TabIndex = 1;
            this.label2.Text = "判定规则";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入的缺陷数据";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hyTerminalCollInputData
            // 
            this.hyTerminalCollInputData.DefaultNameHeader = null;
            this.hyTerminalCollInputData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTerminalCollInputData.Location = new System.Drawing.Point(3, 68);
            this.hyTerminalCollInputData.Name = "hyTerminalCollInputData";
            this.hyTerminalCollInputData.Size = new System.Drawing.Size(414, 580);
            this.hyTerminalCollInputData.TabIndex = 2;
            this.hyTerminalCollInputData.Text = "缺陷数据";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(423, 68);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(974, 580);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(968, 516);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(534, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(431, 510);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.btnAdvanceRuleOut, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnAddAdvanceRule, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnDeleteAdvanceRule, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 232);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(425, 45);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // btnAdvanceRuleOut
            // 
            this.btnAdvanceRuleOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdvanceRuleOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdvanceRuleOut.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdvanceRuleOut.Location = new System.Drawing.Point(285, 3);
            this.btnAdvanceRuleOut.Name = "btnAdvanceRuleOut";
            this.btnAdvanceRuleOut.Size = new System.Drawing.Size(137, 39);
            this.btnAdvanceRuleOut.TabIndex = 1;
            this.btnAdvanceRuleOut.Text = "把高级规则添加到输出";
            this.btnAdvanceRuleOut.UseVisualStyleBackColor = true;
            this.btnAdvanceRuleOut.Click += new System.EventHandler(this.btnAdvanceRuleOut_Click);
            // 
            // btnAddAdvanceRule
            // 
            this.btnAddAdvanceRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddAdvanceRule.Location = new System.Drawing.Point(3, 3);
            this.btnAddAdvanceRule.Name = "btnAddAdvanceRule";
            this.btnAddAdvanceRule.Size = new System.Drawing.Size(135, 39);
            this.btnAddAdvanceRule.TabIndex = 2;
            this.btnAddAdvanceRule.Text = "添加高级规则";
            this.btnAddAdvanceRule.UseVisualStyleBackColor = true;
            this.btnAddAdvanceRule.Click += new System.EventHandler(this.btnAddAdvanceRule_Click);
            // 
            // btnDeleteAdvanceRule
            // 
            this.btnDeleteAdvanceRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteAdvanceRule.Location = new System.Drawing.Point(144, 3);
            this.btnDeleteAdvanceRule.Name = "btnDeleteAdvanceRule";
            this.btnDeleteAdvanceRule.Size = new System.Drawing.Size(135, 39);
            this.btnDeleteAdvanceRule.TabIndex = 3;
            this.btnDeleteAdvanceRule.Text = "删除选定的高级规则";
            this.btnDeleteAdvanceRule.UseVisualStyleBackColor = true;
            this.btnDeleteAdvanceRule.Click += new System.EventHandler(this.btnDeleteAdvanceRule_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvAdvanceRule);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 223);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "高级规则";
            // 
            // dgvAdvanceRule
            // 
            this.dgvAdvanceRule.AllowUserToAddRows = false;
            this.dgvAdvanceRule.AllowUserToDeleteRows = false;
            this.dgvAdvanceRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdvanceRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAdvanceRuleSelect,
            this.ColumnAdvanceRule});
            this.dgvAdvanceRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdvanceRule.Location = new System.Drawing.Point(3, 26);
            this.dgvAdvanceRule.MultiSelect = false;
            this.dgvAdvanceRule.Name = "dgvAdvanceRule";
            this.dgvAdvanceRule.RowHeadersVisible = false;
            this.dgvAdvanceRule.RowHeadersWidth = 51;
            this.dgvAdvanceRule.RowTemplate.Height = 27;
            this.dgvAdvanceRule.Size = new System.Drawing.Size(419, 194);
            this.dgvAdvanceRule.TabIndex = 1;
            // 
            // ColumnAdvanceRuleSelect
            // 
            this.ColumnAdvanceRuleSelect.HeaderText = "选中";
            this.ColumnAdvanceRuleSelect.MinimumWidth = 6;
            this.ColumnAdvanceRuleSelect.Name = "ColumnAdvanceRuleSelect";
            this.ColumnAdvanceRuleSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnAdvanceRuleSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnAdvanceRuleSelect.Width = 80;
            // 
            // ColumnAdvanceRule
            // 
            this.ColumnAdvanceRule.HeaderText = "规则";
            this.ColumnAdvanceRule.MinimumWidth = 6;
            this.ColumnAdvanceRule.Name = "ColumnAdvanceRule";
            this.ColumnAdvanceRule.Width = 250;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvOutputRule);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(3, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(425, 224);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "最终执行的输出规则";
            // 
            // dgvOutputRule
            // 
            this.dgvOutputRule.AllowUserToAddRows = false;
            this.dgvOutputRule.AllowUserToDeleteRows = false;
            this.dgvOutputRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutputRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOutputName,
            this.ColumnOutputRule,
            this.ColumnResult});
            this.dgvOutputRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutputRule.Location = new System.Drawing.Point(3, 26);
            this.dgvOutputRule.Name = "dgvOutputRule";
            this.dgvOutputRule.RowHeadersVisible = false;
            this.dgvOutputRule.RowHeadersWidth = 51;
            this.dgvOutputRule.RowTemplate.Height = 27;
            this.dgvOutputRule.Size = new System.Drawing.Size(419, 195);
            this.dgvOutputRule.TabIndex = 2;
            // 
            // ColumnOutputName
            // 
            this.ColumnOutputName.HeaderText = "名称";
            this.ColumnOutputName.MinimumWidth = 6;
            this.ColumnOutputName.Name = "ColumnOutputName";
            this.ColumnOutputName.Width = 125;
            // 
            // ColumnOutputRule
            // 
            this.ColumnOutputRule.HeaderText = "规则";
            this.ColumnOutputRule.MinimumWidth = 6;
            this.ColumnOutputRule.Name = "ColumnOutputRule";
            this.ColumnOutputRule.Width = 200;
            // 
            // ColumnResult
            // 
            this.ColumnResult.HeaderText = "判定结果";
            this.ColumnResult.MinimumWidth = 6;
            this.ColumnResult.Name = "ColumnResult";
            this.ColumnResult.Width = 125;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.btnRuleDirectOut, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.btnRuleOr, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnRuleAnd, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(438, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(90, 510);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // btnRuleDirectOut
            // 
            this.btnRuleDirectOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRuleDirectOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRuleDirectOut.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRuleDirectOut.Location = new System.Drawing.Point(3, 309);
            this.btnRuleDirectOut.Name = "btnRuleDirectOut";
            this.btnRuleDirectOut.Size = new System.Drawing.Size(84, 96);
            this.btnRuleDirectOut.TabIndex = 3;
            this.btnRuleDirectOut.Text = "把初始规则添加到输出";
            this.btnRuleDirectOut.UseVisualStyleBackColor = true;
            this.btnRuleDirectOut.Click += new System.EventHandler(this.btnRuleDirectOut_Click);
            // 
            // btnRuleOr
            // 
            this.btnRuleOr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRuleOr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRuleOr.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRuleOr.Location = new System.Drawing.Point(3, 105);
            this.btnRuleOr.Name = "btnRuleOr";
            this.btnRuleOr.Size = new System.Drawing.Size(84, 96);
            this.btnRuleOr.TabIndex = 1;
            this.btnRuleOr.Text = "初始规则合并到高级规则 (或者)";
            this.btnRuleOr.UseVisualStyleBackColor = true;
            this.btnRuleOr.Click += new System.EventHandler(this.btnRuleOr_Click);
            // 
            // btnRuleAnd
            // 
            this.btnRuleAnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRuleAnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRuleAnd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRuleAnd.Location = new System.Drawing.Point(3, 3);
            this.btnRuleAnd.Name = "btnRuleAnd";
            this.btnRuleAnd.Size = new System.Drawing.Size(84, 96);
            this.btnRuleAnd.TabIndex = 0;
            this.btnRuleAnd.Text = "初始规则合并到高级规则 (并且)";
            this.btnRuleAnd.UseVisualStyleBackColor = true;
            this.btnRuleAnd.Click += new System.EventHandler(this.btnRuleAnd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvOriginalRule);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 510);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "初始规则";
            // 
            // dgvOriginalRule
            // 
            this.dgvOriginalRule.AllowUserToAddRows = false;
            this.dgvOriginalRule.AllowUserToDeleteRows = false;
            this.dgvOriginalRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOriginalRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOriginalRuleSelect,
            this.ColumnOriginalRuleInputName,
            this.ColumnOriginalRuleOperator,
            this.ColumnOriginalRuleThreshold});
            this.dgvOriginalRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOriginalRule.Location = new System.Drawing.Point(3, 26);
            this.dgvOriginalRule.MultiSelect = false;
            this.dgvOriginalRule.Name = "dgvOriginalRule";
            this.dgvOriginalRule.RowHeadersVisible = false;
            this.dgvOriginalRule.RowHeadersWidth = 51;
            this.dgvOriginalRule.RowTemplate.Height = 27;
            this.dgvOriginalRule.Size = new System.Drawing.Size(423, 481);
            this.dgvOriginalRule.TabIndex = 2;
            this.dgvOriginalRule.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOriginalRule_CellContentClick);
            // 
            // ColumnOriginalRuleSelect
            // 
            this.ColumnOriginalRuleSelect.HeaderText = "选中";
            this.ColumnOriginalRuleSelect.MinimumWidth = 6;
            this.ColumnOriginalRuleSelect.Name = "ColumnOriginalRuleSelect";
            this.ColumnOriginalRuleSelect.Width = 80;
            // 
            // ColumnOriginalRuleInputName
            // 
            this.ColumnOriginalRuleInputName.HeaderText = "输入名称";
            this.ColumnOriginalRuleInputName.MinimumWidth = 6;
            this.ColumnOriginalRuleInputName.Name = "ColumnOriginalRuleInputName";
            this.ColumnOriginalRuleInputName.Width = 125;
            // 
            // ColumnOriginalRuleOperator
            // 
            this.ColumnOriginalRuleOperator.HeaderText = "运算操作符";
            this.ColumnOriginalRuleOperator.Items.AddRange(new object[] {
            ">",
            "<",
            "=",
            ">=",
            "<="});
            this.ColumnOriginalRuleOperator.MinimumWidth = 6;
            this.ColumnOriginalRuleOperator.Name = "ColumnOriginalRuleOperator";
            this.ColumnOriginalRuleOperator.Width = 125;
            // 
            // ColumnOriginalRuleThreshold
            // 
            this.ColumnOriginalRuleThreshold.HeaderText = "阈值";
            this.ColumnOriginalRuleThreshold.MinimumWidth = 6;
            this.ColumnOriginalRuleThreshold.Name = "ColumnOriginalRuleThreshold";
            this.ColumnOriginalRuleThreshold.Width = 125;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 5;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.Controls.Add(this.btnAddOriginalRule, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnDeleteOriginalRule, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnDeleteOutputRule, 4, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 525);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(968, 52);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // btnAddOriginalRule
            // 
            this.btnAddOriginalRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddOriginalRule.Location = new System.Drawing.Point(3, 3);
            this.btnAddOriginalRule.Name = "btnAddOriginalRule";
            this.btnAddOriginalRule.Size = new System.Drawing.Size(187, 46);
            this.btnAddOriginalRule.TabIndex = 0;
            this.btnAddOriginalRule.Text = "添加初始规则";
            this.btnAddOriginalRule.UseVisualStyleBackColor = true;
            this.btnAddOriginalRule.Click += new System.EventHandler(this.btnAddOriginalRule_Click);
            // 
            // btnDeleteOriginalRule
            // 
            this.btnDeleteOriginalRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteOriginalRule.Location = new System.Drawing.Point(196, 3);
            this.btnDeleteOriginalRule.Name = "btnDeleteOriginalRule";
            this.btnDeleteOriginalRule.Size = new System.Drawing.Size(187, 46);
            this.btnDeleteOriginalRule.TabIndex = 1;
            this.btnDeleteOriginalRule.Text = "删除选定的初始规则";
            this.btnDeleteOriginalRule.UseVisualStyleBackColor = true;
            this.btnDeleteOriginalRule.Click += new System.EventHandler(this.btnDeleteOriginalRule_Click);
            // 
            // btnDeleteOutputRule
            // 
            this.btnDeleteOutputRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteOutputRule.Location = new System.Drawing.Point(775, 3);
            this.btnDeleteOutputRule.Name = "btnDeleteOutputRule";
            this.btnDeleteOutputRule.Size = new System.Drawing.Size(190, 46);
            this.btnDeleteOutputRule.TabIndex = 2;
            this.btnDeleteOutputRule.Text = "删除选中的输出规则";
            this.btnDeleteOutputRule.UseVisualStyleBackColor = true;
            this.btnDeleteOutputRule.Click += new System.EventHandler(this.btnDeleteOutputRule_Click);
            // 
            // ClassifierUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ClassifierUI";
            this.Size = new System.Drawing.Size(1400, 700);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanceRule)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputRule)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginalRule)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private TerminalCollection.HyTerminalCollectionEdit hyTerminalCollInputData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnAdvanceRuleOut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvAdvanceRule;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvOutputRule;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnRuleDirectOut;
        private System.Windows.Forms.Button btnRuleOr;
        private System.Windows.Forms.Button btnRuleAnd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOriginalRule;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btnAddOriginalRule;
        private System.Windows.Forms.Button btnDeleteOriginalRule;
        private System.Windows.Forms.Button btnDeleteOutputRule;
        private System.Windows.Forms.Button btnAddAdvanceRule;
        private System.Windows.Forms.Button btnDeleteAdvanceRule;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnAdvanceRuleSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAdvanceRule;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnOriginalRuleSelect;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnOriginalRuleInputName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnOriginalRuleOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOriginalRuleThreshold;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOutputName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOutputRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResult;
    }
}
