namespace HyEye.WForm
{
    partial class FrmMaterial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaterial));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddSubMaterial = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnRenameMaterial = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDeleteMaterial = new System.Windows.Forms.Button();
            this.pbDeleteController = new System.Windows.Forms.PictureBox();
            this.btnAddMaterial = new System.Windows.Forms.Button();
            this.pbAddController = new System.Windows.Forms.PictureBox();
            this.dgvMaterials = new System.Windows.Forms.DataGridViewRowMerge();
            this.colMaterialIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaterialName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubMaterialName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnable = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colChange = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colBackup = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRenameMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteSubMaterial = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.btnAddSubMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.btnRenameMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.btnDeleteMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteController)).BeginInit();
            this.btnAddMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.btnDeleteSubMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteSubMaterial);
            this.panel1.Controls.Add(this.btnAddSubMaterial);
            this.panel1.Controls.Add(this.btnRenameMaterial);
            this.panel1.Controls.Add(this.btnDeleteMaterial);
            this.panel1.Controls.Add(this.btnAddMaterial);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnAddSubMaterial
            // 
            this.btnAddSubMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSubMaterial.Controls.Add(this.pictureBox2);
            this.btnAddSubMaterial.Location = new System.Drawing.Point(369, 12);
            this.btnAddSubMaterial.Name = "btnAddSubMaterial";
            this.btnAddSubMaterial.Size = new System.Drawing.Size(152, 35);
            this.btnAddSubMaterial.TabIndex = 4;
            this.btnAddSubMaterial.Text = "添加子料号";
            this.btnAddSubMaterial.UseVisualStyleBackColor = true;
            this.btnAddSubMaterial.Click += new System.EventHandler(this.btnAddSubMaterial_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::HyEye.WForm.Properties.Resources.新增;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 29);
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // btnRenameMaterial
            // 
            this.btnRenameMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenameMaterial.Controls.Add(this.pictureBox1);
            this.btnRenameMaterial.Location = new System.Drawing.Point(252, 12);
            this.btnRenameMaterial.Name = "btnRenameMaterial";
            this.btnRenameMaterial.Size = new System.Drawing.Size(100, 35);
            this.btnRenameMaterial.TabIndex = 3;
            this.btnRenameMaterial.Text = "重命名";
            this.btnRenameMaterial.UseVisualStyleBackColor = true;
            this.btnRenameMaterial.Click += new System.EventHandler(this.btnRenameMaterial_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::HyEye.WForm.Properties.Resources.重命名;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 29);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // btnDeleteMaterial
            // 
            this.btnDeleteMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteMaterial.Controls.Add(this.pbDeleteController);
            this.btnDeleteMaterial.Location = new System.Drawing.Point(132, 12);
            this.btnDeleteMaterial.Name = "btnDeleteMaterial";
            this.btnDeleteMaterial.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteMaterial.TabIndex = 2;
            this.btnDeleteMaterial.Text = "删除";
            this.btnDeleteMaterial.UseVisualStyleBackColor = true;
            this.btnDeleteMaterial.Click += new System.EventHandler(this.btnDeleteMaterial_Click);
            // 
            // pbDeleteController
            // 
            this.pbDeleteController.BackColor = System.Drawing.Color.Transparent;
            this.pbDeleteController.BackgroundImage = global::HyEye.WForm.Properties.Resources.删除;
            this.pbDeleteController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbDeleteController.Location = new System.Drawing.Point(3, 3);
            this.pbDeleteController.Name = "pbDeleteController";
            this.pbDeleteController.Size = new System.Drawing.Size(29, 29);
            this.pbDeleteController.TabIndex = 30;
            this.pbDeleteController.TabStop = false;
            // 
            // btnAddMaterial
            // 
            this.btnAddMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMaterial.Controls.Add(this.pbAddController);
            this.btnAddMaterial.Location = new System.Drawing.Point(12, 12);
            this.btnAddMaterial.Name = "btnAddMaterial";
            this.btnAddMaterial.Size = new System.Drawing.Size(100, 35);
            this.btnAddMaterial.TabIndex = 1;
            this.btnAddMaterial.Text = "添加";
            this.btnAddMaterial.UseVisualStyleBackColor = true;
            this.btnAddMaterial.Click += new System.EventHandler(this.btnAddMaterial_Click);
            // 
            // pbAddController
            // 
            this.pbAddController.BackColor = System.Drawing.Color.Transparent;
            this.pbAddController.BackgroundImage = global::HyEye.WForm.Properties.Resources.新增;
            this.pbAddController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAddController.Location = new System.Drawing.Point(3, 3);
            this.pbAddController.Name = "pbAddController";
            this.pbAddController.Size = new System.Drawing.Size(29, 29);
            this.pbAddController.TabIndex = 29;
            this.pbAddController.TabStop = false;
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaterialIndex,
            this.colMaterialName,
            this.colSubMaterialName,
            this.colEnable,
            this.colChange,
            this.colBackup,
            this.colDelete});
            this.dgvMaterials.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterials.Location = new System.Drawing.Point(0, 59);
            this.dgvMaterials.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvMaterials.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvMaterials.MergeColumnNames")));
            this.dgvMaterials.MultiSelect = false;
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.ReadOnly = true;
            this.dgvMaterials.RowHeadersWidth = 35;
            this.dgvMaterials.RowTemplate.Height = 23;
            this.dgvMaterials.Size = new System.Drawing.Size(702, 532);
            this.dgvMaterials.TabIndex = 1;
            this.dgvMaterials.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterials_CellContentClick);
            // 
            // colMaterialIndex
            // 
            this.colMaterialIndex.HeaderText = "料号索引";
            this.colMaterialIndex.MinimumWidth = 6;
            this.colMaterialIndex.Name = "colMaterialIndex";
            this.colMaterialIndex.ReadOnly = true;
            this.colMaterialIndex.Width = 80;
            // 
            // colMaterialName
            // 
            this.colMaterialName.HeaderText = "料号名称";
            this.colMaterialName.MinimumWidth = 6;
            this.colMaterialName.Name = "colMaterialName";
            this.colMaterialName.ReadOnly = true;
            this.colMaterialName.Width = 125;
            // 
            // colSubMaterialName
            // 
            this.colSubMaterialName.HeaderText = "子料号名称";
            this.colSubMaterialName.MinimumWidth = 6;
            this.colSubMaterialName.Name = "colSubMaterialName";
            this.colSubMaterialName.ReadOnly = true;
            this.colSubMaterialName.Width = 125;
            // 
            // colEnable
            // 
            this.colEnable.HeaderText = "启用";
            this.colEnable.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.colEnable.MinimumWidth = 6;
            this.colEnable.Name = "colEnable";
            this.colEnable.ReadOnly = true;
            this.colEnable.Text = "";
            this.colEnable.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.colEnable.Width = 80;
            // 
            // colChange
            // 
            this.colChange.HeaderText = "切换子料号";
            this.colChange.MinimumWidth = 6;
            this.colChange.Name = "colChange";
            this.colChange.ReadOnly = true;
            this.colChange.Width = 125;
            // 
            // colBackup
            // 
            this.colBackup.HeaderText = "备份";
            this.colBackup.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.colBackup.MinimumWidth = 6;
            this.colBackup.Name = "colBackup";
            this.colBackup.ReadOnly = true;
            this.colBackup.Text = "备份";
            this.colBackup.UseColumnTextForLinkValue = true;
            this.colBackup.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.colBackup.Width = 80;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "删除";
            this.colDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.colDelete.MinimumWidth = 6;
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Text = "删除";
            this.colDelete.UseColumnTextForLinkValue = true;
            this.colDelete.Visible = false;
            this.colDelete.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.colDelete.Width = 80;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRenameMaterial});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 28);
            // 
            // tsmiRenameMaterial
            // 
            this.tsmiRenameMaterial.Name = "tsmiRenameMaterial";
            this.tsmiRenameMaterial.Size = new System.Drawing.Size(123, 24);
            this.tsmiRenameMaterial.Text = "重命名";
            this.tsmiRenameMaterial.Click += new System.EventHandler(this.tsmiRenameMaterial_Click);
            // 
            // btnDeleteSubMaterial
            // 
            this.btnDeleteSubMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSubMaterial.Controls.Add(this.pictureBox3);
            this.btnDeleteSubMaterial.Location = new System.Drawing.Point(540, 12);
            this.btnDeleteSubMaterial.Name = "btnDeleteSubMaterial";
            this.btnDeleteSubMaterial.Size = new System.Drawing.Size(150, 35);
            this.btnDeleteSubMaterial.TabIndex = 5;
            this.btnDeleteSubMaterial.Text = "删除子料号";
            this.btnDeleteSubMaterial.UseVisualStyleBackColor = true;
            this.btnDeleteSubMaterial.Click += new System.EventHandler(this.btnDeleteSubMaterial_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::HyEye.WForm.Properties.Resources.删除;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 29);
            this.pictureBox3.TabIndex = 30;
            this.pictureBox3.TabStop = false;
            // 
            // FrmMaterial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(702, 591);
            this.Controls.Add(this.dgvMaterials);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "料号管理";
            this.Load += new System.EventHandler(this.FrmMaterial_Load);
            this.panel1.ResumeLayout(false);
            this.btnAddSubMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.btnRenameMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.btnDeleteMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteController)).EndInit();
            this.btnAddMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAddController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.btnDeleteSubMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewRowMerge dgvMaterials;
        private System.Windows.Forms.Button btnAddMaterial;
        private System.Windows.Forms.PictureBox pbAddController;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRenameMaterial;
        private System.Windows.Forms.Button btnDeleteMaterial;
        private System.Windows.Forms.PictureBox pbDeleteController;
        private System.Windows.Forms.Button btnRenameMaterial;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaterialIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaterialName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubMaterialName;
        private System.Windows.Forms.DataGridViewLinkColumn colEnable;
        private System.Windows.Forms.DataGridViewLinkColumn colChange;
        private System.Windows.Forms.DataGridViewLinkColumn colBackup;
        private System.Windows.Forms.DataGridViewLinkColumn colDelete;
        private System.Windows.Forms.Button btnAddSubMaterial;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnDeleteSubMaterial;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}