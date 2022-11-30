namespace HalconSDK.DataReport.UI
{
    partial class HalconDataReportGeneraterUI
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
            this.cboGeneratMidImage = new System.Windows.Forms.CheckBox();
            this.cboGeneratFeatureMap = new System.Windows.Forms.CheckBox();
            this.cboGeneratScoreTxt = new System.Windows.Forms.CheckBox();
            this.cboGeneratExcel = new System.Windows.Forms.CheckBox();
            this.hyTerminalCollectionEditInput = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtGeneratReportFolderPath = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hyTerminalCollectionEditInput, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 501);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.cboGeneratMidImage, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboGeneratFeatureMap, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboGeneratScoreTxt, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboGeneratExcel, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(899, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cboGeneratMidImage
            // 
            this.cboGeneratMidImage.AutoSize = true;
            this.cboGeneratMidImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGeneratMidImage.Location = new System.Drawing.Point(3, 3);
            this.cboGeneratMidImage.Name = "cboGeneratMidImage";
            this.cboGeneratMidImage.Size = new System.Drawing.Size(218, 23);
            this.cboGeneratMidImage.TabIndex = 0;
            this.cboGeneratMidImage.Text = "生成中间图";
            this.cboGeneratMidImage.UseVisualStyleBackColor = true;
            this.cboGeneratMidImage.CheckedChanged += new System.EventHandler(this.cboGeneratMidImage_CheckedChanged);
            // 
            // cboGeneratFeatureMap
            // 
            this.cboGeneratFeatureMap.AutoSize = true;
            this.cboGeneratFeatureMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGeneratFeatureMap.Location = new System.Drawing.Point(227, 3);
            this.cboGeneratFeatureMap.Name = "cboGeneratFeatureMap";
            this.cboGeneratFeatureMap.Size = new System.Drawing.Size(218, 23);
            this.cboGeneratFeatureMap.TabIndex = 1;
            this.cboGeneratFeatureMap.Text = "生成特征图";
            this.cboGeneratFeatureMap.UseVisualStyleBackColor = true;
            this.cboGeneratFeatureMap.CheckedChanged += new System.EventHandler(this.cboGeneratFeatureMap_CheckedChanged);
            // 
            // cboGeneratScoreTxt
            // 
            this.cboGeneratScoreTxt.AutoSize = true;
            this.cboGeneratScoreTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGeneratScoreTxt.Location = new System.Drawing.Point(451, 3);
            this.cboGeneratScoreTxt.Name = "cboGeneratScoreTxt";
            this.cboGeneratScoreTxt.Size = new System.Drawing.Size(218, 23);
            this.cboGeneratScoreTxt.TabIndex = 2;
            this.cboGeneratScoreTxt.Text = "生成文本数据";
            this.cboGeneratScoreTxt.UseVisualStyleBackColor = true;
            this.cboGeneratScoreTxt.CheckedChanged += new System.EventHandler(this.cboGeneratScoreTxt_CheckedChanged);
            // 
            // cboGeneratExcel
            // 
            this.cboGeneratExcel.AutoSize = true;
            this.cboGeneratExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGeneratExcel.Location = new System.Drawing.Point(675, 3);
            this.cboGeneratExcel.Name = "cboGeneratExcel";
            this.cboGeneratExcel.Size = new System.Drawing.Size(221, 23);
            this.cboGeneratExcel.TabIndex = 3;
            this.cboGeneratExcel.Text = "生成表格数据";
            this.cboGeneratExcel.UseVisualStyleBackColor = true;
            this.cboGeneratExcel.CheckedChanged += new System.EventHandler(this.cboGeneratExcel_CheckedChanged);
            // 
            // hyTerminalCollectionEditInput
            // 
            this.hyTerminalCollectionEditInput.DefaultNameHeader = null;
            this.hyTerminalCollectionEditInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTerminalCollectionEditInput.Location = new System.Drawing.Point(3, 73);
            this.hyTerminalCollectionEditInput.Name = "hyTerminalCollectionEditInput";
            this.hyTerminalCollectionEditInput.Size = new System.Drawing.Size(899, 425);
            this.hyTerminalCollectionEditInput.TabIndex = 1;
            this.hyTerminalCollectionEditInput.Text = "输入数据";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Controls.Add(this.txtGeneratReportFolderPath, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnOpenFolder, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(899, 29);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // txtGeneratReportFolderPath
            // 
            this.txtGeneratReportFolderPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGeneratReportFolderPath.Location = new System.Drawing.Point(3, 3);
            this.txtGeneratReportFolderPath.Name = "txtGeneratReportFolderPath";
            this.txtGeneratReportFolderPath.ReadOnly = true;
            this.txtGeneratReportFolderPath.Size = new System.Drawing.Size(623, 25);
            this.txtGeneratReportFolderPath.TabIndex = 0;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenFolder.Location = new System.Drawing.Point(632, 3);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(264, 23);
            this.btnOpenFolder.TabIndex = 1;
            this.btnOpenFolder.Text = "选择结果输出的文件夹....";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // HalconDataReportGeneraterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HalconDataReportGeneraterUI";
            this.Size = new System.Drawing.Size(905, 550);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox cboGeneratMidImage;
        private System.Windows.Forms.CheckBox cboGeneratFeatureMap;
        private System.Windows.Forms.CheckBox cboGeneratScoreTxt;
        private System.Windows.Forms.CheckBox cboGeneratExcel;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTerminalCollectionEditInput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox txtGeneratReportFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnOpenFolder;
    }
}
