
namespace HyEye.AI
{
    partial class HyAIDetEngineUI
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
            this.btnDetSelectFile = new System.Windows.Forms.Button();
            this.txtDetCfgFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDetInferenceDraw = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDetMaxDetections = new System.Windows.Forms.TextBox();
            this.txtDetEngineBatch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDetInputMaxW = new System.Windows.Forms.TextBox();
            this.txtDetScoreThreshold = new System.Windows.Forms.TextBox();
            this.txtDetInputMaxH = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDetOptInputSizeH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDetOptInputSizeW = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(5, 30, 5, 30);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1191, 181);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnDetSelectFile, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDetCfgFilePath, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1185, 33);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnDetSelectFile
            // 
            this.btnDetSelectFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDetSelectFile.Location = new System.Drawing.Point(950, 3);
            this.btnDetSelectFile.Name = "btnDetSelectFile";
            this.btnDetSelectFile.Size = new System.Drawing.Size(232, 27);
            this.btnDetSelectFile.TabIndex = 2;
            this.btnDetSelectFile.Text = "选择...";
            this.btnDetSelectFile.UseVisualStyleBackColor = true;
            this.btnDetSelectFile.Click += new System.EventHandler(this.btnDetSelectFile_Click);
            // 
            // txtDetCfgFilePath
            // 
            this.txtDetCfgFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetCfgFilePath.Location = new System.Drawing.Point(358, 3);
            this.txtDetCfgFilePath.Name = "txtDetCfgFilePath";
            this.txtDetCfgFilePath.ReadOnly = true;
            this.txtDetCfgFilePath.Size = new System.Drawing.Size(586, 25);
            this.txtDetCfgFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(121, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "引擎文件路径:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cboDetInferenceDraw, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDetMaxDetections, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtDetEngineBatch, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label7, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtDetInputMaxW, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtDetScoreThreshold, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDetInputMaxH, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtDetOptInputSizeH, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtDetOptInputSizeW, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 42);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1185, 136);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "结果图渲染:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDetInferenceDraw
            // 
            this.cboDetInferenceDraw.AutoSize = true;
            this.cboDetInferenceDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDetInferenceDraw.Location = new System.Drawing.Point(358, 3);
            this.cboDetInferenceDraw.Name = "cboDetInferenceDraw";
            this.cboDetInferenceDraw.Size = new System.Drawing.Size(231, 28);
            this.cboDetInferenceDraw.TabIndex = 10;
            this.cboDetInferenceDraw.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(595, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(349, 34);
            this.label10.TabIndex = 9;
            this.label10.Text = "得分阈值：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetMaxDetections
            // 
            this.txtDetMaxDetections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetMaxDetections.Location = new System.Drawing.Point(950, 105);
            this.txtDetMaxDetections.Name = "txtDetMaxDetections";
            this.txtDetMaxDetections.Size = new System.Drawing.Size(232, 25);
            this.txtDetMaxDetections.TabIndex = 15;
            this.txtDetMaxDetections.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetMaxDetections_KeyPress);
            // 
            // txtDetEngineBatch
            // 
            this.txtDetEngineBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetEngineBatch.Location = new System.Drawing.Point(358, 105);
            this.txtDetEngineBatch.Name = "txtDetEngineBatch";
            this.txtDetEngineBatch.Size = new System.Drawing.Size(231, 25);
            this.txtDetEngineBatch.TabIndex = 12;
            this.txtDetEngineBatch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetEngineBatch_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(595, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(349, 34);
            this.label7.TabIndex = 6;
            this.label7.Text = "最大检测目标数量:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetInputMaxW
            // 
            this.txtDetInputMaxW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetInputMaxW.Location = new System.Drawing.Point(950, 71);
            this.txtDetInputMaxW.Name = "txtDetInputMaxW";
            this.txtDetInputMaxW.Size = new System.Drawing.Size(232, 25);
            this.txtDetInputMaxW.TabIndex = 17;
            this.txtDetInputMaxW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetInputMaxW_KeyPress);
            // 
            // txtDetScoreThreshold
            // 
            this.txtDetScoreThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetScoreThreshold.Location = new System.Drawing.Point(950, 3);
            this.txtDetScoreThreshold.Name = "txtDetScoreThreshold";
            this.txtDetScoreThreshold.Size = new System.Drawing.Size(232, 25);
            this.txtDetScoreThreshold.TabIndex = 18;
            this.txtDetScoreThreshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetScoreThreshold_KeyPress);
            // 
            // txtDetInputMaxH
            // 
            this.txtDetInputMaxH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetInputMaxH.Location = new System.Drawing.Point(358, 71);
            this.txtDetInputMaxH.Name = "txtDetInputMaxH";
            this.txtDetInputMaxH.Size = new System.Drawing.Size(231, 25);
            this.txtDetInputMaxH.TabIndex = 16;
            this.txtDetInputMaxH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetInputMaxH_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(349, 34);
            this.label4.TabIndex = 3;
            this.label4.Text = "引擎批数量:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(349, 34);
            this.label5.TabIndex = 4;
            this.label5.Text = "输入图像优化高度:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(595, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(349, 34);
            this.label9.TabIndex = 8;
            this.label9.Text = "输入图像最大宽度:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetOptInputSizeH
            // 
            this.txtDetOptInputSizeH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetOptInputSizeH.Location = new System.Drawing.Point(358, 37);
            this.txtDetOptInputSizeH.Name = "txtDetOptInputSizeH";
            this.txtDetOptInputSizeH.Size = new System.Drawing.Size(231, 25);
            this.txtDetOptInputSizeH.TabIndex = 13;
            this.txtDetOptInputSizeH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetOptInputSizeH_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(595, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(349, 34);
            this.label6.TabIndex = 5;
            this.label6.Text = "输入图像优化宽度:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetOptInputSizeW
            // 
            this.txtDetOptInputSizeW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetOptInputSizeW.Location = new System.Drawing.Point(950, 37);
            this.txtDetOptInputSizeW.Name = "txtDetOptInputSizeW";
            this.txtDetOptInputSizeW.Size = new System.Drawing.Size(232, 25);
            this.txtDetOptInputSizeW.TabIndex = 14;
            this.txtDetOptInputSizeW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetOptInputSizeW_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(349, 34);
            this.label8.TabIndex = 7;
            this.label8.Text = "输入图像最大高度:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.hyTerminalCollInput, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.hyTerminalCollOutput, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 190);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1191, 431);
            this.tableLayoutPanel5.TabIndex = 23;
            // 
            // hyTerminalCollInput
            // 
            this.hyTerminalCollInput.DefaultNameHeader = null;
            this.hyTerminalCollInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTerminalCollInput.Location = new System.Drawing.Point(3, 3);
            this.hyTerminalCollInput.Name = "hyTerminalCollInput";
            this.hyTerminalCollInput.Size = new System.Drawing.Size(589, 425);
            this.hyTerminalCollInput.TabIndex = 3;
            this.hyTerminalCollInput.Tag = "输入图像";
            this.hyTerminalCollInput.Text = "输入图像";
            // 
            // hyTerminalCollOutput
            // 
            this.hyTerminalCollOutput.DefaultNameHeader = null;
            this.hyTerminalCollOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTerminalCollOutput.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hyTerminalCollOutput.Location = new System.Drawing.Point(598, 4);
            this.hyTerminalCollOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hyTerminalCollOutput.Name = "hyTerminalCollOutput";
            this.hyTerminalCollOutput.Size = new System.Drawing.Size(590, 423);
            this.hyTerminalCollOutput.TabIndex = 4;
            this.hyTerminalCollOutput.Tag = "";
            this.hyTerminalCollOutput.Text = "输出图像";
            // 
            // HyAIDetEngineUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Name = "HyAIDetEngineUI";
            this.Size = new System.Drawing.Size(1197, 624);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnDetSelectFile;
        private System.Windows.Forms.TextBox txtDetCfgFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cboDetInferenceDraw;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDetMaxDetections;
        private System.Windows.Forms.TextBox txtDetEngineBatch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDetInputMaxW;
        private System.Windows.Forms.TextBox txtDetScoreThreshold;
        private System.Windows.Forms.TextBox txtDetInputMaxH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDetOptInputSizeH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDetOptInputSizeW;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTerminalCollInput;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTerminalCollOutput;
    }
}
