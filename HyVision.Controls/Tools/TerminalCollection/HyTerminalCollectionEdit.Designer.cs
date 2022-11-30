
namespace HyVision.Tools.TerminalCollection
{
    partial class HyTerminalCollectionEdit
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
            this.dgvTerminals = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripInput = new System.Windows.Forms.ToolStrip();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiAddInt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDouble = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddString = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDatetime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddHyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddBitmap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddHyROI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddHRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddXLD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListHyDefectXLD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListInt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListDouble = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListString = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListHyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListBitmap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddRoiData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsBtnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsBtnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.tstbText = new System.Windows.Forms.ToolStripTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsmiAddAIDefect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddListAIDefect = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerminals)).BeginInit();
            this.toolStripInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTerminals
            // 
            this.dgvTerminals.AllowUserToAddRows = false;
            this.dgvTerminals.AllowUserToDeleteRows = false;
            this.dgvTerminals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvTerminals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTerminals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colType,
            this.colValue,
            this.colDescription});
            this.dgvTerminals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTerminals.Location = new System.Drawing.Point(0, 29);
            this.dgvTerminals.MultiSelect = false;
            this.dgvTerminals.Name = "dgvTerminals";
            this.dgvTerminals.RowHeadersVisible = false;
            this.dgvTerminals.RowHeadersWidth = 20;
            this.dgvTerminals.RowTemplate.Height = 27;
            this.dgvTerminals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTerminals.Size = new System.Drawing.Size(629, 260);
            this.dgvTerminals.TabIndex = 3;
            this.dgvTerminals.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvTerminals_CellBeginEdit);
            // 
            // colName
            // 
            this.colName.HeaderText = "名称";
            this.colName.MaxInputLength = 32;
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.Width = 125;
            // 
            // colType
            // 
            this.colType.HeaderText = "类型";
            this.colType.MinimumWidth = 6;
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 125;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "值";
            this.colValue.MinimumWidth = 6;
            this.colValue.Name = "colValue";
            this.colValue.Width = 125;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.HeaderText = "描述";
            this.colDescription.MinimumWidth = 6;
            this.colDescription.Name = "colDescription";
            // 
            // toolStripInput
            // 
            this.toolStripInput.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripInput.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAdd,
            this.tsBtnDelete,
            this.tsBtnMoveUp,
            this.tsBtnMoveDown,
            this.tstbText});
            this.toolStripInput.Location = new System.Drawing.Point(0, 0);
            this.toolStripInput.Name = "toolStripInput";
            this.toolStripInput.Size = new System.Drawing.Size(629, 29);
            this.toolStripInput.TabIndex = 2;
            this.toolStripInput.Text = "toolStrip2";
            // 
            // tsBtnAdd
            // 
            this.tsBtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAdd.DropDownButtonWidth = 30;
            this.tsBtnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddInt,
            this.tsmiAddDouble,
            this.tsmiAddString,
            this.tsmiAddDatetime,
            this.tsmiAddHyImage,
            this.tsmiAddBitmap,
            this.tsmiAddHyROI,
            this.tsmiAddHRegion,
            this.tsmiAddXLD,
            this.tsmiAddListHyDefectXLD,
            this.tsmiAddListInt,
            this.tsmiAddListDouble,
            this.tsmiAddListString,
            this.tsmiAddListHyImage,
            this.tsmiAddListBitmap,
            this.tsmiAddRoiData,
            this.tsmiAddAIDefect,
            this.tsmiAddListAIDefect});
            this.tsBtnAdd.Image = global::HyVision.Properties.Resources.新增;
            this.tsBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAdd.Name = "tsBtnAdd";
            this.tsBtnAdd.Size = new System.Drawing.Size(55, 26);
            this.tsBtnAdd.Text = "toolStripSplitButton1";
            // 
            // tsmiAddInt
            // 
            this.tsmiAddInt.Name = "tsmiAddInt";
            this.tsmiAddInt.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddInt.Text = "新增 System.Int";
            this.tsmiAddInt.Click += new System.EventHandler(this.tsmiAddInt_Click);
            // 
            // tsmiAddDouble
            // 
            this.tsmiAddDouble.Name = "tsmiAddDouble";
            this.tsmiAddDouble.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddDouble.Text = "新增 System.Double";
            this.tsmiAddDouble.Click += new System.EventHandler(this.tsmiAddDouble_Click);
            // 
            // tsmiAddString
            // 
            this.tsmiAddString.Name = "tsmiAddString";
            this.tsmiAddString.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddString.Text = "新增 System.String";
            this.tsmiAddString.Click += new System.EventHandler(this.tsmiAddString_Click);
            // 
            // tsmiAddDatetime
            // 
            this.tsmiAddDatetime.Name = "tsmiAddDatetime";
            this.tsmiAddDatetime.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddDatetime.Text = "新增 System.Datetime";
            this.tsmiAddDatetime.Click += new System.EventHandler(this.tsmiAddDatetime_Click);
            // 
            // tsmiAddHyImage
            // 
            this.tsmiAddHyImage.Name = "tsmiAddHyImage";
            this.tsmiAddHyImage.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddHyImage.Text = "新增 HyImage";
            this.tsmiAddHyImage.Click += new System.EventHandler(this.tsmiAddHyImage_Click);
            // 
            // tsmiAddBitmap
            // 
            this.tsmiAddBitmap.Name = "tsmiAddBitmap";
            this.tsmiAddBitmap.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddBitmap.Text = "新增 Bitmap";
            this.tsmiAddBitmap.Click += new System.EventHandler(this.tsmiAddBitmap_Click);
            // 
            // tsmiAddHyROI
            // 
            this.tsmiAddHyROI.Name = "tsmiAddHyROI";
            this.tsmiAddHyROI.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddHyROI.Text = "新增 ROI";
            this.tsmiAddHyROI.Click += new System.EventHandler(this.tsmiAddHyROI_Click);
            // 
            // tsmiAddHRegion
            // 
            this.tsmiAddHRegion.Name = "tsmiAddHRegion";
            this.tsmiAddHRegion.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddHRegion.Text = "新增 HRegion";
            this.tsmiAddHRegion.Click += new System.EventHandler(this.tsmiAddHRegion_Click);
            // 
            // tsmiAddXLD
            // 
            this.tsmiAddXLD.Name = "tsmiAddXLD";
            this.tsmiAddXLD.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddXLD.Text = "新增 XLD";
            this.tsmiAddXLD.Click += new System.EventHandler(this.tsmiAddXLD_Click);
            // 
            // tsmiAddListHyDefectXLD
            // 
            this.tsmiAddListHyDefectXLD.Name = "tsmiAddListHyDefectXLD";
            this.tsmiAddListHyDefectXLD.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListHyDefectXLD.Text = "新增 List<HyDefectXLD>";
            this.tsmiAddListHyDefectXLD.Click += new System.EventHandler(this.tsmiAddListHyDefectXLD_Click);
            // 
            // tsmiAddListInt
            // 
            this.tsmiAddListInt.Name = "tsmiAddListInt";
            this.tsmiAddListInt.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListInt.Text = "新增 List<Int>";
            this.tsmiAddListInt.Click += new System.EventHandler(this.tsmiAddListInt_Click);
            // 
            // tsmiAddListDouble
            // 
            this.tsmiAddListDouble.Name = "tsmiAddListDouble";
            this.tsmiAddListDouble.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListDouble.Text = "新增 List<Double>";
            this.tsmiAddListDouble.Click += new System.EventHandler(this.tsmiAddListDouble_Click);
            // 
            // tsmiAddListString
            // 
            this.tsmiAddListString.Name = "tsmiAddListString";
            this.tsmiAddListString.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListString.Text = "新增 List<String>";
            this.tsmiAddListString.Click += new System.EventHandler(this.tsmiAddListString_Click);
            // 
            // tsmiAddListHyImage
            // 
            this.tsmiAddListHyImage.Name = "tsmiAddListHyImage";
            this.tsmiAddListHyImage.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListHyImage.Text = "新增 List<HyImage>";
            this.tsmiAddListHyImage.Click += new System.EventHandler(this.tsmiAddListHyImage_Click);
            // 
            // tsmiAddListBitmap
            // 
            this.tsmiAddListBitmap.Name = "tsmiAddListBitmap";
            this.tsmiAddListBitmap.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListBitmap.Text = "新增 List<Bitmap>";
            this.tsmiAddListBitmap.Click += new System.EventHandler(this.tsmiAddListBitmap_Click);
            // 
            // tsmiAddRoiData
            // 
            this.tsmiAddRoiData.Name = "tsmiAddRoiData";
            this.tsmiAddRoiData.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddRoiData.Text = "新增 RoiData";
            this.tsmiAddRoiData.Click += new System.EventHandler(this.tsmiAddRoiData_Click);
            // 
            // tsBtnDelete
            // 
            this.tsBtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDelete.Image = global::HyVision.Properties.Resources.删除筛选项;
            this.tsBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDelete.Name = "tsBtnDelete";
            this.tsBtnDelete.Size = new System.Drawing.Size(29, 26);
            this.tsBtnDelete.Text = "删除";
            this.tsBtnDelete.ToolTipText = "删除";
            this.tsBtnDelete.Click += new System.EventHandler(this.tsBtnDelete_Click);
            // 
            // tsBtnMoveUp
            // 
            this.tsBtnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnMoveUp.Image = global::HyVision.Properties.Resources.上_移;
            this.tsBtnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMoveUp.Name = "tsBtnMoveUp";
            this.tsBtnMoveUp.Size = new System.Drawing.Size(29, 26);
            this.tsBtnMoveUp.Text = "上移";
            this.tsBtnMoveUp.ToolTipText = "上移";
            this.tsBtnMoveUp.Click += new System.EventHandler(this.tsBtnMoveUp_Click);
            // 
            // tsBtnMoveDown
            // 
            this.tsBtnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnMoveDown.Image = global::HyVision.Properties.Resources.下_移;
            this.tsBtnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMoveDown.Name = "tsBtnMoveDown";
            this.tsBtnMoveDown.Size = new System.Drawing.Size(29, 26);
            this.tsBtnMoveDown.Text = "下移";
            this.tsBtnMoveDown.ToolTipText = "下移";
            this.tsBtnMoveDown.Click += new System.EventHandler(this.tsBtnMoveDown_Click);
            // 
            // tstbText
            // 
            this.tstbText.BackColor = System.Drawing.SystemColors.Control;
            this.tstbText.Enabled = false;
            this.tstbText.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.tstbText.Name = "tstbText";
            this.tstbText.ReadOnly = true;
            this.tstbText.Size = new System.Drawing.Size(100, 29);
            this.tstbText.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "名称";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "类型";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "值";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "描述";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // tsmiAddAIDefect
            // 
            this.tsmiAddAIDefect.Name = "tsmiAddAIDefect";
            this.tsmiAddAIDefect.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddAIDefect.Text = "新增 AI缺陷";
            this.tsmiAddAIDefect.Click += new System.EventHandler(this.tsmiAddAIDefect_Click);
            // 
            // tsmiAddListAIDefect
            // 
            this.tsmiAddListAIDefect.Name = "tsmiAddListAIDefect";
            this.tsmiAddListAIDefect.Size = new System.Drawing.Size(270, 26);
            this.tsmiAddListAIDefect.Text = "新增 AI缺陷集合";
            this.tsmiAddListAIDefect.Click += new System.EventHandler(this.tsmiAddListAIDefect_Click);
            // 
            // HyTerminalCollectionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTerminals);
            this.Controls.Add(this.toolStripInput);
            this.Name = "HyTerminalCollectionEdit";
            this.Size = new System.Drawing.Size(629, 289);
            this.Load += new System.EventHandler(this.HyTerminalCollectionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerminals)).EndInit();
            this.toolStripInput.ResumeLayout(false);
            this.toolStripInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTerminals;
        private System.Windows.Forms.ToolStrip toolStripInput;
        private System.Windows.Forms.ToolStripSplitButton tsBtnAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddInt;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDouble;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddString;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDatetime;
        private System.Windows.Forms.ToolStripButton tsBtnDelete;
        private System.Windows.Forms.ToolStripButton tsBtnMoveUp;
        private System.Windows.Forms.ToolStripButton tsBtnMoveDown;
        private System.Windows.Forms.ToolStripTextBox tstbText;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddHyImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddHyROI;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddHRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddXLD;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListHyDefectXLD;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddBitmap;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListInt;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListDouble;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListString;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListHyImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListBitmap;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRoiData;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddAIDefect;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddListAIDefect;
    }
}
