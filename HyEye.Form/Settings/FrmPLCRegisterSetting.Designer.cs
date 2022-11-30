
namespace HyEye.WForm.Settings
{
    partial class FrmPLCRegisterSetting
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
            this.dgvRecvCommands = new System.Windows.Forms.DataGridView();
            this.colTaskRecvTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskRecvAcqName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskRecvRegisterAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskRecvCmdLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskRecvRead = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colTaskRecvWrite = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecvCommands)).BeginInit();
            this.panel1.SuspendLayout();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRecvCommands
            // 
            this.dgvRecvCommands.AllowUserToAddRows = false;
            this.dgvRecvCommands.AllowUserToDeleteRows = false;
            this.dgvRecvCommands.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRecvCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecvCommands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTaskRecvTaskName,
            this.colTaskRecvAcqName,
            this.colTaskRecvRegisterAddr,
            this.colTaskRecvCmdLength,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.colTaskRecvRead,
            this.colTaskRecvWrite});
            this.dgvRecvCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecvCommands.Location = new System.Drawing.Point(3, 3);
            this.dgvRecvCommands.Name = "dgvRecvCommands";
            this.dgvRecvCommands.ReadOnly = true;
            this.dgvRecvCommands.RowHeadersWidth = 25;
            this.dgvRecvCommands.RowTemplate.Height = 27;
            this.dgvRecvCommands.Size = new System.Drawing.Size(1396, 562);
            this.dgvRecvCommands.TabIndex = 0;
            // 
            // colTaskRecvTaskName
            // 
            this.colTaskRecvTaskName.HeaderText = "任务名称";
            this.colTaskRecvTaskName.MinimumWidth = 6;
            this.colTaskRecvTaskName.Name = "colTaskRecvTaskName";
            this.colTaskRecvTaskName.ReadOnly = true;
            this.colTaskRecvTaskName.Width = 125;
            // 
            // colTaskRecvAcqName
            // 
            this.colTaskRecvAcqName.HeaderText = "拍照名称";
            this.colTaskRecvAcqName.MinimumWidth = 6;
            this.colTaskRecvAcqName.Name = "colTaskRecvAcqName";
            this.colTaskRecvAcqName.ReadOnly = true;
            this.colTaskRecvAcqName.Width = 125;
            // 
            // colTaskRecvRegisterAddr
            // 
            this.colTaskRecvRegisterAddr.HeaderText = "寄存器起始地址";
            this.colTaskRecvRegisterAddr.MinimumWidth = 6;
            this.colTaskRecvRegisterAddr.Name = "colTaskRecvRegisterAddr";
            this.colTaskRecvRegisterAddr.ReadOnly = true;
            this.colTaskRecvRegisterAddr.Width = 125;
            // 
            // colTaskRecvCmdLength
            // 
            this.colTaskRecvCmdLength.HeaderText = "指令长度";
            this.colTaskRecvCmdLength.MinimumWidth = 6;
            this.colTaskRecvCmdLength.Name = "colTaskRecvCmdLength";
            this.colTaskRecvCmdLength.ReadOnly = true;
            this.colTaskRecvCmdLength.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "寄存器值（任务）";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "寄存器值（拍照）";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "寄存器值（指令类型）";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 110;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "附加数据";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 200;
            // 
            // colTaskRecvRead
            // 
            this.colTaskRecvRead.HeaderText = "读取";
            this.colTaskRecvRead.MinimumWidth = 6;
            this.colTaskRecvRead.Name = "colTaskRecvRead";
            this.colTaskRecvRead.ReadOnly = true;
            this.colTaskRecvRead.Text = "读取";
            this.colTaskRecvRead.Width = 125;
            // 
            // colTaskRecvWrite
            // 
            this.colTaskRecvWrite.HeaderText = "写入";
            this.colTaskRecvWrite.MinimumWidth = 6;
            this.colTaskRecvWrite.Name = "colTaskRecvWrite";
            this.colTaskRecvWrite.ReadOnly = true;
            this.colTaskRecvWrite.Text = "写入";
            this.colTaskRecvWrite.Width = 125;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1410, 60);
            this.panel1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(528, 17);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 25);
            this.textBox2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "接收指令寄存器总长度：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(219, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "接收指令寄存器起始地址：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(1248, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSave.Location = new System.Drawing.Point(3, 3);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(29, 29);
            this.pbSave.TabIndex = 31;
            this.pbSave.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1410, 597);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvRecvCommands);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1402, 568);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "生产接收指令";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1402, 568);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "生产发送指令";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1402, 568);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "标定接收指令";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1402, 568);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "标定发送指令";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // FrmPLCRegisterSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 657);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPLCRegisterSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "指令设置";
            this.Load += new System.EventHandler(this.FrmRegisterConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecvCommands)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRecvCommands;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskRecvTaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskRecvAcqName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskRecvRegisterAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskRecvCmdLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn colTaskRecvRead;
        private System.Windows.Forms.DataGridViewButtonColumn colTaskRecvWrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}