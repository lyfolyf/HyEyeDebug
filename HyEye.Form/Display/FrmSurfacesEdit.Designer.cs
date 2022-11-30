
namespace HyEye.WForm.Display
{
    partial class FrmSurfacesEdit
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
            this.cbSurfaces = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.numericUdHcount = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUdVcount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBlockEdit = new System.Windows.Forms.Button();
            this.btnSetPreviewImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUdHcount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUdVcount)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSurfaces
            // 
            this.cbSurfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSurfaces.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSurfaces.FormattingEnabled = true;
            this.cbSurfaces.Location = new System.Drawing.Point(189, 24);
            this.cbSurfaces.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSurfaces.Name = "cbSurfaces";
            this.cbSurfaces.Size = new System.Drawing.Size(123, 25);
            this.cbSurfaces.TabIndex = 57;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(163, 184);
            this.listBox1.TabIndex = 58;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Gray;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.Location = new System.Drawing.Point(318, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 25);
            this.btnAdd.TabIndex = 60;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.Silver;
            this.btnDel.FlatAppearance.BorderSize = 0;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.ForeColor = System.Drawing.Color.Red;
            this.btnDel.Location = new System.Drawing.Point(283, 175);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(85, 25);
            this.btnDel.TabIndex = 61;
            this.btnDel.Text = "删除选中项";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // numericUdHcount
            // 
            this.numericUdHcount.Location = new System.Drawing.Point(246, 56);
            this.numericUdHcount.Name = "numericUdHcount";
            this.numericUdHcount.Size = new System.Drawing.Size(66, 21);
            this.numericUdHcount.TabIndex = 63;
            this.numericUdHcount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(184, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 62;
            this.label12.Text = "相机数";
            // 
            // numericUdVcount
            // 
            this.numericUdVcount.Location = new System.Drawing.Point(246, 83);
            this.numericUdVcount.Name = "numericUdVcount";
            this.numericUdVcount.Size = new System.Drawing.Size(66, 21);
            this.numericUdVcount.TabIndex = 65;
            this.numericUdVcount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 64;
            this.label1.Text = "点位数";
            // 
            // btnBlockEdit
            // 
            this.btnBlockEdit.BackColor = System.Drawing.Color.Gray;
            this.btnBlockEdit.FlatAppearance.BorderSize = 0;
            this.btnBlockEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlockEdit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnBlockEdit.Location = new System.Drawing.Point(179, 175);
            this.btnBlockEdit.Name = "btnBlockEdit";
            this.btnBlockEdit.Size = new System.Drawing.Size(88, 25);
            this.btnBlockEdit.TabIndex = 66;
            this.btnBlockEdit.Text = "添加块";
            this.btnBlockEdit.UseVisualStyleBackColor = false;
            this.btnBlockEdit.Click += new System.EventHandler(this.btnBlockEdit_Click);
            // 
            // btnSetPreviewImage
            // 
            this.btnSetPreviewImage.BackColor = System.Drawing.Color.Gray;
            this.btnSetPreviewImage.FlatAppearance.BorderSize = 0;
            this.btnSetPreviewImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPreviewImage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSetPreviewImage.Location = new System.Drawing.Point(179, 144);
            this.btnSetPreviewImage.Name = "btnSetPreviewImage";
            this.btnSetPreviewImage.Size = new System.Drawing.Size(88, 25);
            this.btnSetPreviewImage.TabIndex = 68;
            this.btnSetPreviewImage.Text = "设置预览图像";
            this.btnSetPreviewImage.UseVisualStyleBackColor = false;
            this.btnSetPreviewImage.Click += new System.EventHandler(this.btnSetPreviewImage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(205, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 70;
            this.label3.Text = "设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(205, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 72;
            this.label4.Text = "新增";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(12, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 74;
            this.label2.Text = "现有项";
            // 
            // FrmSurfacesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 212);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSetPreviewImage);
            this.Controls.Add(this.btnBlockEdit);
            this.Controls.Add(this.numericUdVcount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUdHcount);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cbSurfaces);
            this.Name = "FrmSurfacesEdit";
            this.Text = "配置面";
            ((System.ComponentModel.ISupportInitialize)(this.numericUdHcount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUdVcount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSurfaces;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.NumericUpDown numericUdHcount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUdVcount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBlockEdit;
        private System.Windows.Forms.Button btnSetPreviewImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}