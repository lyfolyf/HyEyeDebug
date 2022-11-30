
namespace HyEye.WForm.Calibration
{
    partial class FrmJoint
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAutoStop = new System.Windows.Forms.Button();
            this.pbAutoStop = new System.Windows.Forms.PictureBox();
            this.btnAutoCalibration = new System.Windows.Forms.Button();
            this.pbAutoCalibration = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.pbReset = new System.Windows.Forms.PictureBox();
            this.btnMasterCalib = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudPointCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCalibM = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRun = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAutoStop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAutoStop)).BeginInit();
            this.btnAutoCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAutoCalibration)).BeginInit();
            this.btnReset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbReset)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointCount)).BeginInit();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAutoStop
            // 
            this.btnAutoStop.Controls.Add(this.pbAutoStop);
            this.btnAutoStop.Enabled = false;
            this.btnAutoStop.Location = new System.Drawing.Point(356, 26);
            this.btnAutoStop.Name = "btnAutoStop";
            this.btnAutoStop.Size = new System.Drawing.Size(119, 35);
            this.btnAutoStop.TabIndex = 10;
            this.btnAutoStop.Text = "停止";
            this.btnAutoStop.UseVisualStyleBackColor = true;
            // 
            // pbAutoStop
            // 
            this.pbAutoStop.BackColor = System.Drawing.Color.Transparent;
            this.pbAutoStop.BackgroundImage = global::HyEye.WForm.Properties.Resources.停止1;
            this.pbAutoStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAutoStop.Location = new System.Drawing.Point(3, 3);
            this.pbAutoStop.Name = "pbAutoStop";
            this.pbAutoStop.Size = new System.Drawing.Size(29, 29);
            this.pbAutoStop.TabIndex = 31;
            this.pbAutoStop.TabStop = false;
            // 
            // btnAutoCalibration
            // 
            this.btnAutoCalibration.Controls.Add(this.pbAutoCalibration);
            this.btnAutoCalibration.Location = new System.Drawing.Point(206, 26);
            this.btnAutoCalibration.Name = "btnAutoCalibration";
            this.btnAutoCalibration.Size = new System.Drawing.Size(120, 35);
            this.btnAutoCalibration.TabIndex = 9;
            this.btnAutoCalibration.Text = "自动标定";
            this.btnAutoCalibration.UseVisualStyleBackColor = true;
            // 
            // pbAutoCalibration
            // 
            this.pbAutoCalibration.BackColor = System.Drawing.Color.Transparent;
            this.pbAutoCalibration.BackgroundImage = global::HyEye.WForm.Properties.Resources.开始2;
            this.pbAutoCalibration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAutoCalibration.Location = new System.Drawing.Point(3, 3);
            this.pbAutoCalibration.Name = "pbAutoCalibration";
            this.pbAutoCalibration.Size = new System.Drawing.Size(29, 29);
            this.pbAutoCalibration.TabIndex = 30;
            this.pbAutoCalibration.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Controls.Add(this.pbReset);
            this.btnReset.Location = new System.Drawing.Point(517, 26);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 35);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // pbReset
            // 
            this.pbReset.BackColor = System.Drawing.Color.Transparent;
            this.pbReset.BackgroundImage = global::HyEye.WForm.Properties.Resources.重置;
            this.pbReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbReset.Location = new System.Drawing.Point(3, 3);
            this.pbReset.Name = "pbReset";
            this.pbReset.Size = new System.Drawing.Size(29, 29);
            this.pbReset.TabIndex = 28;
            this.pbReset.TabStop = false;
            // 
            // btnMasterCalib
            // 
            this.btnMasterCalib.Location = new System.Drawing.Point(37, 26);
            this.btnMasterCalib.Name = "btnMasterCalib";
            this.btnMasterCalib.Size = new System.Drawing.Size(117, 35);
            this.btnMasterCalib.TabIndex = 2;
            this.btnMasterCalib.Text = "主标定";
            this.btnMasterCalib.UseVisualStyleBackColor = true;
            this.btnMasterCalib.Click += new System.EventHandler(this.btnMasterCalib_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudPointCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnMasterCalib);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnAutoStop);
            this.panel1.Controls.Add(this.btnAutoCalibration);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1346, 82);
            this.panel1.TabIndex = 1;
            // 
            // nudPointCount
            // 
            this.nudPointCount.Location = new System.Drawing.Point(809, 32);
            this.nudPointCount.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudPointCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudPointCount.Name = "nudPointCount";
            this.nudPointCount.Size = new System.Drawing.Size(80, 25);
            this.nudPointCount.TabIndex = 13;
            this.nudPointCount.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudPointCount.ValueChanged += new System.EventHandler(this.nudPointCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(710, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "仿射变换点数：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(1155, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 37);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbSave
            // 
            this.pbSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImage = global::HyEye.WForm.Properties.Resources.保存;
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSave.Location = new System.Drawing.Point(3, 3);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(29, 29);
            this.pbSave.TabIndex = 27;
            this.pbSave.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1346, 647);
            this.panel2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1346, 647);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1338, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图像显示";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1338, 618);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1338, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.btnCalibM);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.btnRun);
            this.splitContainer1.Size = new System.Drawing.Size(1338, 618);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnCalibM
            // 
            this.btnCalibM.Location = new System.Drawing.Point(20, 20);
            this.btnCalibM.Name = "btnCalibM";
            this.btnCalibM.Size = new System.Drawing.Size(100, 35);
            this.btnCalibM.TabIndex = 2;
            this.btnCalibM.Text = "光学设置";
            this.btnCalibM.UseVisualStyleBackColor = true;
            this.btnCalibM.Click += new System.EventHandler(this.btnCalibM_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(20, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(260, 340);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(150, 20);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 35);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "已校正 X";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "已校正 Y";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // FrmJoint
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1346, 729);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmJoint";
            this.Text = "FrmJoint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmJoint_FormClosing);
            this.btnAutoStop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAutoStop)).EndInit();
            this.btnAutoCalibration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAutoCalibration)).EndInit();
            this.btnReset.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbReset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointCount)).EndInit();
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnMasterCalib;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.PictureBox pbReset;
        private System.Windows.Forms.Button btnAutoStop;
        private System.Windows.Forms.PictureBox pbAutoStop;
        private System.Windows.Forms.Button btnAutoCalibration;
        private System.Windows.Forms.PictureBox pbAutoCalibration;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.Button btnCalibM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPointCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}