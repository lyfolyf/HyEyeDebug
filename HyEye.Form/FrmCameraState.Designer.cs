namespace HyEye.WForm
{
    partial class FrmCameraState
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCameraState = new System.Windows.Forms.DataGridView();
            this.colCameraName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCameraState = new System.Windows.Forms.DataGridViewImageColumn();
            this.colIsOpen = new System.Windows.Forms.DataGridViewImageColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCameraState)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCameraState
            // 
            this.dgvCameraState.AllowUserToAddRows = false;
            this.dgvCameraState.AllowUserToDeleteRows = false;
            this.dgvCameraState.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCameraState.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCameraState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCameraState.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCameraName,
            this.colCameraState,
            this.colIsOpen});
            this.dgvCameraState.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCameraState.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCameraState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCameraState.Location = new System.Drawing.Point(0, 0);
            this.dgvCameraState.Name = "dgvCameraState";
            this.dgvCameraState.ReadOnly = true;
            this.dgvCameraState.RowHeadersWidth = 35;
            this.dgvCameraState.RowTemplate.Height = 27;
            this.dgvCameraState.Size = new System.Drawing.Size(517, 450);
            this.dgvCameraState.TabIndex = 0;
            // 
            // colCameraName
            // 
            this.colCameraName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCameraName.HeaderText = "相机名称";
            this.colCameraName.MinimumWidth = 6;
            this.colCameraName.Name = "colCameraName";
            this.colCameraName.ReadOnly = true;
            // 
            // colCameraState
            // 
            this.colCameraState.HeaderText = "连接状态";
            this.colCameraState.MinimumWidth = 6;
            this.colCameraState.Name = "colCameraState";
            this.colCameraState.ReadOnly = true;
            this.colCameraState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCameraState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCameraState.Width = 125;
            // 
            // colIsOpen
            // 
            this.colIsOpen.HeaderText = "打开状态";
            this.colIsOpen.MinimumWidth = 6;
            this.colIsOpen.Name = "colIsOpen";
            this.colIsOpen.ReadOnly = true;
            this.colIsOpen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colIsOpen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colIsOpen.Width = 125;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(108, 24);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // FrmCameraState
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(517, 450);
            this.Controls.Add(this.dgvCameraState);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmCameraState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相机状态";
            this.Load += new System.EventHandler(this.FrmCameraState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCameraState)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCameraState;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCameraName;
        private System.Windows.Forms.DataGridViewImageColumn colCameraState;
        private System.Windows.Forms.DataGridViewImageColumn colIsOpen;
    }
}