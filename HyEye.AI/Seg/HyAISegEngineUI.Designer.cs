
namespace HyEye.AI
{
    partial class HyAISegEngineUI
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSegSelectFile = new System.Windows.Forms.Button();
            this.txtSegInferenceCfgPath = new System.Windows.Forms.TextBox();
            this.lblSegInferenceCfgPath = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSegInferenceDraw = new System.Windows.Forms.Label();
            this.cbxSegInferenceDraw = new System.Windows.Forms.CheckBox();
            this.lblSegIsPatch = new System.Windows.Forms.Label();
            this.cbxSegIsPatch = new System.Windows.Forms.CheckBox();
            this.lblSegBatchMax = new System.Windows.Forms.Label();
            this.txtSegBatchMax = new System.Windows.Forms.TextBox();
            this.lblSegOptBatch = new System.Windows.Forms.Label();
            this.txtSegOptBatch = new System.Windows.Forms.TextBox();
            this.lblSegBatchPatchSplit = new System.Windows.Forms.Label();
            this.txtSegBatchPatchSplit = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.hyTerminalCollInput = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.hyTerminalCollOutput = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(5, 30, 5, 30);
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1197, 624);
            this.tableLayoutPanel4.TabIndex = 20;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 33);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1181, 163);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnSegSelectFile, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtSegInferenceCfgPath, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSegInferenceCfgPath, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1175, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnSegSelectFile
            // 
            this.btnSegSelectFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSegSelectFile.Location = new System.Drawing.Point(942, 3);
            this.btnSegSelectFile.Name = "btnSegSelectFile";
            this.btnSegSelectFile.Size = new System.Drawing.Size(230, 23);
            this.btnSegSelectFile.TabIndex = 2;
            this.btnSegSelectFile.Text = "选择...";
            this.btnSegSelectFile.UseVisualStyleBackColor = true;
            this.btnSegSelectFile.Click += new System.EventHandler(this.btnSegSelectFile_Click);
            // 
            // txtSegInferenceCfgPath
            // 
            this.txtSegInferenceCfgPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegInferenceCfgPath.Location = new System.Drawing.Point(355, 3);
            this.txtSegInferenceCfgPath.Name = "txtSegInferenceCfgPath";
            this.txtSegInferenceCfgPath.ReadOnly = true;
            this.txtSegInferenceCfgPath.Size = new System.Drawing.Size(581, 25);
            this.txtSegInferenceCfgPath.TabIndex = 1;
            // 
            // lblSegInferenceCfgPath
            // 
            this.lblSegInferenceCfgPath.AutoSize = true;
            this.lblSegInferenceCfgPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegInferenceCfgPath.Location = new System.Drawing.Point(120, 0);
            this.lblSegInferenceCfgPath.Name = "lblSegInferenceCfgPath";
            this.lblSegInferenceCfgPath.Size = new System.Drawing.Size(229, 29);
            this.lblSegInferenceCfgPath.TabIndex = 0;
            this.lblSegInferenceCfgPath.Text = "引擎文件路径：";
            this.lblSegInferenceCfgPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.lblSegInferenceDraw, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbxSegInferenceDraw, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblSegIsPatch, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbxSegIsPatch, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblSegBatchMax, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtSegBatchMax, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblSegOptBatch, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtSegOptBatch, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblSegBatchPatchSplit, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtSegBatchPatchSplit, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1175, 122);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblSegInferenceDraw
            // 
            this.lblSegInferenceDraw.AutoSize = true;
            this.lblSegInferenceDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegInferenceDraw.Location = new System.Drawing.Point(3, 0);
            this.lblSegInferenceDraw.Name = "lblSegInferenceDraw";
            this.lblSegInferenceDraw.Size = new System.Drawing.Size(346, 30);
            this.lblSegInferenceDraw.TabIndex = 1;
            this.lblSegInferenceDraw.Text = "输出图像是否渲染:";
            this.lblSegInferenceDraw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxSegInferenceDraw
            // 
            this.cbxSegInferenceDraw.AutoSize = true;
            this.cbxSegInferenceDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxSegInferenceDraw.Location = new System.Drawing.Point(355, 3);
            this.cbxSegInferenceDraw.Name = "cbxSegInferenceDraw";
            this.cbxSegInferenceDraw.Size = new System.Drawing.Size(229, 24);
            this.cbxSegInferenceDraw.TabIndex = 10;
            this.cbxSegInferenceDraw.UseVisualStyleBackColor = true;
            // 
            // lblSegIsPatch
            // 
            this.lblSegIsPatch.AutoSize = true;
            this.lblSegIsPatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegIsPatch.Location = new System.Drawing.Point(590, 0);
            this.lblSegIsPatch.Name = "lblSegIsPatch";
            this.lblSegIsPatch.Size = new System.Drawing.Size(346, 30);
            this.lblSegIsPatch.TabIndex = 9;
            this.lblSegIsPatch.Text = "是否切片：";
            this.lblSegIsPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxSegIsPatch
            // 
            this.cbxSegIsPatch.AutoSize = true;
            this.cbxSegIsPatch.Checked = true;
            this.cbxSegIsPatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSegIsPatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxSegIsPatch.Enabled = false;
            this.cbxSegIsPatch.Location = new System.Drawing.Point(942, 3);
            this.cbxSegIsPatch.Name = "cbxSegIsPatch";
            this.cbxSegIsPatch.Size = new System.Drawing.Size(230, 24);
            this.cbxSegIsPatch.TabIndex = 18;
            this.cbxSegIsPatch.UseVisualStyleBackColor = true;
            // 
            // lblSegBatchMax
            // 
            this.lblSegBatchMax.AutoSize = true;
            this.lblSegBatchMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegBatchMax.Location = new System.Drawing.Point(3, 30);
            this.lblSegBatchMax.Name = "lblSegBatchMax";
            this.lblSegBatchMax.Size = new System.Drawing.Size(346, 30);
            this.lblSegBatchMax.TabIndex = 5;
            this.lblSegBatchMax.Text = "引擎张量最大维度:";
            this.lblSegBatchMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSegBatchMax
            // 
            this.txtSegBatchMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegBatchMax.Location = new System.Drawing.Point(355, 33);
            this.txtSegBatchMax.Name = "txtSegBatchMax";
            this.txtSegBatchMax.Size = new System.Drawing.Size(229, 25);
            this.txtSegBatchMax.TabIndex = 14;
            this.txtSegBatchMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegBatchMax_KeyPress);
            // 
            // lblSegOptBatch
            // 
            this.lblSegOptBatch.AutoSize = true;
            this.lblSegOptBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegOptBatch.Location = new System.Drawing.Point(590, 30);
            this.lblSegOptBatch.Name = "lblSegOptBatch";
            this.lblSegOptBatch.Size = new System.Drawing.Size(346, 30);
            this.lblSegOptBatch.TabIndex = 7;
            this.lblSegOptBatch.Text = "引擎张量优化维度:";
            this.lblSegOptBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSegOptBatch
            // 
            this.txtSegOptBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegOptBatch.Location = new System.Drawing.Point(942, 33);
            this.txtSegOptBatch.Name = "txtSegOptBatch";
            this.txtSegOptBatch.Size = new System.Drawing.Size(230, 25);
            this.txtSegOptBatch.TabIndex = 16;
            this.txtSegOptBatch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegOptBatch_KeyPress);
            // 
            // lblSegBatchPatchSplit
            // 
            this.lblSegBatchPatchSplit.AutoSize = true;
            this.lblSegBatchPatchSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegBatchPatchSplit.Location = new System.Drawing.Point(3, 60);
            this.lblSegBatchPatchSplit.Name = "lblSegBatchPatchSplit";
            this.lblSegBatchPatchSplit.Size = new System.Drawing.Size(346, 30);
            this.lblSegBatchPatchSplit.TabIndex = 8;
            this.lblSegBatchPatchSplit.Text = "引擎张量切片等分数量：";
            this.lblSegBatchPatchSplit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSegBatchPatchSplit
            // 
            this.txtSegBatchPatchSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegBatchPatchSplit.Location = new System.Drawing.Point(355, 63);
            this.txtSegBatchPatchSplit.Name = "txtSegBatchPatchSplit";
            this.txtSegBatchPatchSplit.Size = new System.Drawing.Size(229, 25);
            this.txtSegBatchPatchSplit.TabIndex = 17;
            this.txtSegBatchPatchSplit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegBatchPatchSplit_KeyPress);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.hyTerminalCollInput, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.hyTerminalCollOutput, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 202);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1181, 389);
            this.tableLayoutPanel5.TabIndex = 23;
            // 
            // hyTerminalCollInput
            // 
            this.hyTerminalCollInput.DefaultNameHeader = null;
            this.hyTerminalCollInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTerminalCollInput.Location = new System.Drawing.Point(3, 3);
            this.hyTerminalCollInput.Name = "hyTerminalCollInput";
            this.hyTerminalCollInput.Size = new System.Drawing.Size(584, 383);
            this.hyTerminalCollInput.TabIndex = 2;
            this.hyTerminalCollInput.Tag = "输入图像";
            this.hyTerminalCollInput.Text = "输入图像";
            // 
            // hyTerminalCollOutput
            // 
            this.hyTerminalCollOutput.DefaultNameHeader = null;
            this.hyTerminalCollOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTerminalCollOutput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hyTerminalCollOutput.Location = new System.Drawing.Point(593, 4);
            this.hyTerminalCollOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hyTerminalCollOutput.Name = "hyTerminalCollOutput";
            this.hyTerminalCollOutput.Size = new System.Drawing.Size(585, 381);
            this.hyTerminalCollOutput.TabIndex = 1;
            this.hyTerminalCollOutput.Tag = "";
            this.hyTerminalCollOutput.Text = "输出图像";
            // 
            // HyAISegEngineUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Name = "HyAISegEngineUI";
            this.Size = new System.Drawing.Size(1197, 624);
            this.Controls.SetChildIndex(this.tableLayoutPanel4, 0);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSegSelectFile;
        private System.Windows.Forms.TextBox txtSegInferenceCfgPath;
        private System.Windows.Forms.Label lblSegInferenceCfgPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblSegInferenceDraw;
        private System.Windows.Forms.CheckBox cbxSegInferenceDraw;
        private System.Windows.Forms.Label lblSegIsPatch;
        private System.Windows.Forms.TextBox txtSegBatchPatchSplit;
        private System.Windows.Forms.TextBox txtSegOptBatch;
        private System.Windows.Forms.Label lblSegBatchPatchSplit;
        private System.Windows.Forms.Label lblSegOptBatch;
        private System.Windows.Forms.CheckBox cbxSegIsPatch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTerminalCollInput;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTerminalCollOutput;
        private System.Windows.Forms.Label lblSegBatchMax;
        private System.Windows.Forms.TextBox txtSegBatchMax;
    }
}
