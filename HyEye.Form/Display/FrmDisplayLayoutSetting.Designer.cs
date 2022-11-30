namespace HyEye.WForm
{
    partial class FrmDisplayLayoutSetting
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudRowCount = new System.Windows.Forms.NumericUpDown();
            this.nudColumnCount = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tvAcquireImage = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSetOutPut = new System.Windows.Forms.Button();
            this.ckbShowRetImage = new System.Windows.Forms.CheckBox();
            this.btnAdvancedSetting = new System.Windows.Forms.Button();
            this.pbAdvancedSetting = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnCount)).BeginInit();
            this.panel1.SuspendLayout();
            this.btnAdvancedSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvancedSetting)).BeginInit();
            this.btnSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "拍照总次数：{0}  当前显示次数{1}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "行:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "列:";
            // 
            // nudRowCount
            // 
            this.nudRowCount.Location = new System.Drawing.Point(247, 21);
            this.nudRowCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRowCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRowCount.Name = "nudRowCount";
            this.nudRowCount.Size = new System.Drawing.Size(46, 25);
            this.nudRowCount.TabIndex = 4;
            this.nudRowCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // nudColumnCount
            // 
            this.nudColumnCount.Location = new System.Drawing.Point(328, 21);
            this.nudColumnCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudColumnCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumnCount.Name = "nudColumnCount";
            this.nudColumnCount.Size = new System.Drawing.Size(46, 25);
            this.nudColumnCount.TabIndex = 5;
            this.nudColumnCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // tvAcquireImage
            // 
            this.tvAcquireImage.CheckBoxes = true;
            this.tvAcquireImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.tvAcquireImage.Location = new System.Drawing.Point(998, 64);
            this.tvAcquireImage.Name = "tvAcquireImage";
            this.tvAcquireImage.Size = new System.Drawing.Size(214, 619);
            this.tvAcquireImage.TabIndex = 6;
            this.tvAcquireImage.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvAcquireImage_BeforeCheck);
            this.tvAcquireImage.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvAcquireImage_AfterCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSetOutPut);
            this.panel1.Controls.Add(this.ckbShowRetImage);
            this.panel1.Controls.Add(this.btnAdvancedSetting);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.nudRowCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.nudColumnCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1192, 64);
            this.panel1.TabIndex = 7;
            // 
            // btnSetOutPut
            // 
            this.btnSetOutPut.Location = new System.Drawing.Point(664, 14);
            this.btnSetOutPut.Name = "btnSetOutPut";
            this.btnSetOutPut.Size = new System.Drawing.Size(150, 35);
            this.btnSetOutPut.TabIndex = 10;
            this.btnSetOutPut.Text = "设置显示输出";
            this.btnSetOutPut.UseVisualStyleBackColor = true;
            this.btnSetOutPut.Click += new System.EventHandler(this.btnSetOutPut_Click);
            // 
            // ckbShowRetImage
            // 
            this.ckbShowRetImage.AutoSize = true;
            this.ckbShowRetImage.Location = new System.Drawing.Point(554, 23);
            this.ckbShowRetImage.Name = "ckbShowRetImage";
            this.ckbShowRetImage.Size = new System.Drawing.Size(104, 19);
            this.ckbShowRetImage.TabIndex = 9;
            this.ckbShowRetImage.Text = "显示结果图";
            this.ckbShowRetImage.UseVisualStyleBackColor = true;
            // 
            // btnAdvancedSetting
            // 
            this.btnAdvancedSetting.Controls.Add(this.pbAdvancedSetting);
            this.btnAdvancedSetting.Location = new System.Drawing.Point(389, 12);
            this.btnAdvancedSetting.Name = "btnAdvancedSetting";
            this.btnAdvancedSetting.Size = new System.Drawing.Size(150, 35);
            this.btnAdvancedSetting.TabIndex = 8;
            this.btnAdvancedSetting.Text = "高级设置";
            this.btnAdvancedSetting.UseVisualStyleBackColor = true;
            this.btnAdvancedSetting.Click += new System.EventHandler(this.btnAdvancedSetting_Click);
            // 
            // pbAdvancedSetting
            // 
            this.pbAdvancedSetting.BackColor = System.Drawing.Color.Transparent;
            this.pbAdvancedSetting.BackgroundImage = global::HyEye.WForm.Properties.Resources.系统设置;
            this.pbAdvancedSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAdvancedSetting.Location = new System.Drawing.Point(3, 3);
            this.pbAdvancedSetting.Name = "pbAdvancedSetting";
            this.pbAdvancedSetting.Size = new System.Drawing.Size(29, 29);
            this.pbAdvancedSetting.TabIndex = 30;
            this.pbAdvancedSetting.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Controls.Add(this.pbSave);
            this.btnSave.Location = new System.Drawing.Point(1030, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 35);
            this.btnSave.TabIndex = 7;
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
            this.pbSave.TabIndex = 29;
            this.pbSave.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 64);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 36, 0);
            this.panel2.Size = new System.Drawing.Size(978, 619);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.layoutPanel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(942, 619);
            this.panel3.TabIndex = 1;
            // 
            // layoutPanel
            // 
            this.layoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.layoutPanel.ColumnCount = 4;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 2;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.Size = new System.Drawing.Size(942, 619);
            this.layoutPanel.TabIndex = 0;
            // 
            // FrmDisplayLayoutSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1232, 703);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tvAcquireImage);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmDisplayLayoutSetting";
            this.Padding = new System.Windows.Forms.Padding(20, 0, 20, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "界面布局设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDisplayLayoutSetting_FormClosed);
            this.Load += new System.EventHandler(this.FrmMainSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnCount)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.btnAdvancedSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvancedSetting)).EndInit();
            this.btnSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRowCount;
        private System.Windows.Forms.NumericUpDown nudColumnCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.TreeView tvAcquireImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel layoutPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdvancedSetting;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbAdvancedSetting;
        private System.Windows.Forms.CheckBox ckbShowRetImage;
        private System.Windows.Forms.Button btnSetOutPut;
    }
}