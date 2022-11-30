
namespace HyVision.Tools.ThridLib
{
    partial class HyThridLibToolEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbDllName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbFuncName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbClassName = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.inputEdit = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.outputEdit = new HyVision.Tools.TerminalCollection.HyTerminalCollectionEdit();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "DLL 名称：";
            // 
            // tbDllName
            // 
            this.tbDllName.Location = new System.Drawing.Point(117, 35);
            this.tbDllName.Name = "tbDllName";
            this.tbDllName.Size = new System.Drawing.Size(300, 25);
            this.tbDllName.TabIndex = 2;
            this.tbDllName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDllName_KeyPress);
            this.tbDllName.Leave += new System.EventHandler(this.tbDllName_Leave);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 544);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbFuncName);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cmbClassName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbDllName);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(462, 515);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmbFuncName
            // 
            this.cmbFuncName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuncName.FormattingEnabled = true;
            this.cmbFuncName.Location = new System.Drawing.Point(117, 157);
            this.cmbFuncName.Name = "cmbFuncName";
            this.cmbFuncName.Size = new System.Drawing.Size(300, 23);
            this.cmbFuncName.TabIndex = 6;
            this.cmbFuncName.SelectedIndexChanged += new System.EventHandler(this.cmbFuncName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "方法：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "类：";
            // 
            // cmbClassName
            // 
            this.cmbClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassName.FormattingEnabled = true;
            this.cmbClassName.Location = new System.Drawing.Point(117, 97);
            this.cmbClassName.Name = "cmbClassName";
            this.cmbClassName.Size = new System.Drawing.Size(300, 23);
            this.cmbClassName.TabIndex = 3;
            this.cmbClassName.SelectedIndexChanged += new System.EventHandler(this.cmbClassName_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(462, 515);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输入/输出";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.inputEdit);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.outputEdit);
            this.splitContainer2.Size = new System.Drawing.Size(456, 509);
            this.splitContainer2.SplitterDistance = 248;
            this.splitContainer2.TabIndex = 0;
            // 
            // inputEdit
            // 
            this.inputEdit.DefaultNameHeader = "Input";
            this.inputEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputEdit.Location = new System.Drawing.Point(0, 0);
            this.inputEdit.Name = "inputEdit";
            this.inputEdit.Size = new System.Drawing.Size(456, 248);
            this.inputEdit.TabIndex = 0;
            this.inputEdit.Text = "输入";
            // 
            // outputEdit
            // 
            this.outputEdit.DefaultNameHeader = "Output";
            this.outputEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputEdit.Location = new System.Drawing.Point(0, 0);
            this.outputEdit.Name = "outputEdit";
            this.outputEdit.Size = new System.Drawing.Size(456, 257);
            this.outputEdit.TabIndex = 1;
            this.outputEdit.Text = "输出";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(926, 544);
            this.splitContainer1.SplitterDistance = 470;
            this.splitContainer1.TabIndex = 4;
            // 
            // HyThridLibToolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "HyThridLibToolEdit";
            this.Size = new System.Drawing.Size(926, 593);
            this.Load += new System.EventHandler(this.HyThridLibToolEdit_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDllName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbClassName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFuncName;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private TerminalCollection.HyTerminalCollectionEdit inputEdit;
        private TerminalCollection.HyTerminalCollectionEdit outputEdit;
    }
}
