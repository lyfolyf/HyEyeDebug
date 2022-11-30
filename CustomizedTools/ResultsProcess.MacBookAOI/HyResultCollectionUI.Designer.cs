
namespace ResultsProcess
{
    partial class HyResultCollectionUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tplInOut = new System.Windows.Forms.TableLayoutPanel();
            this.hyTCEInput = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.hyTCEOutput = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.tlpMain.SuspendLayout();
            this.tplInOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tplInOut, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(5, 30, 5, 30);
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpMain.Size = new System.Drawing.Size(1200, 600);
            this.tlpMain.TabIndex = 21;
            // 
            // tplInOut
            // 
            this.tplInOut.ColumnCount = 2;
            this.tplInOut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplInOut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplInOut.Controls.Add(this.hyTCEInput, 0, 0);
            this.tplInOut.Controls.Add(this.hyTCEOutput, 1, 0);
            this.tplInOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplInOut.Location = new System.Drawing.Point(8, 33);
            this.tplInOut.Name = "tplInOut";
            this.tplInOut.RowCount = 1;
            this.tplInOut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplInOut.Size = new System.Drawing.Size(1184, 534);
            this.tplInOut.TabIndex = 24;
            // 
            // hyTCEInput
            // 
            this.hyTCEInput.DefaultNameHeader = null;
            this.hyTCEInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTCEInput.Location = new System.Drawing.Point(3, 3);
            this.hyTCEInput.Name = "hyTCEInput";
            this.hyTCEInput.Size = new System.Drawing.Size(586, 528);
            this.hyTCEInput.TabIndex = 0;
            this.hyTCEInput.Text = "输入参数";
            // 
            // hyTCEOutput
            // 
            this.hyTCEOutput.DefaultNameHeader = null;
            this.hyTCEOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyTCEOutput.Location = new System.Drawing.Point(595, 3);
            this.hyTCEOutput.Name = "hyTCEOutput";
            this.hyTCEOutput.Size = new System.Drawing.Size(586, 528);
            this.hyTCEOutput.TabIndex = 1;
            this.hyTCEOutput.Text = "输出参数";
            // 
            // HyResultCollectionUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "HyResultCollectionUI";
            this.Size = new System.Drawing.Size(1200, 600);
            this.tlpMain.ResumeLayout(false);
            this.tplInOut.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tplInOut;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTCEInput;
        private HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit hyTCEOutput;
    }
}
