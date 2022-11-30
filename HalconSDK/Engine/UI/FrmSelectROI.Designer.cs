
namespace HalconSDK.Engine.UI
{
    partial class FrmSelectROI
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("01.1a");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("02.3a");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("03.5a");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("04.5b");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectROI));
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("01.1a");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("02.3a");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("03.5a");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("04.5b");
            this.tvwSelectedRoi = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tvwRoiList = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.hyImageDisplayControl1 = new HyVision.Tools.ImageDisplay.HyImageDisplayControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvwSelectedRoi
            // 
            this.tvwSelectedRoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSelectedRoi.ImageIndex = 2;
            this.tvwSelectedRoi.ImageList = this.imageList1;
            this.tvwSelectedRoi.ItemHeight = 20;
            this.tvwSelectedRoi.Location = new System.Drawing.Point(3, 22);
            this.tvwSelectedRoi.Name = "tvwSelectedRoi";
            treeNode1.Name = "节点0";
            treeNode1.Text = "01.1a";
            treeNode2.Name = "节点1";
            treeNode2.Text = "02.3a";
            treeNode3.Name = "节点2";
            treeNode3.Text = "03.5a";
            treeNode4.Name = "节点3";
            treeNode4.Text = "04.5b";
            this.tvwSelectedRoi.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.tvwSelectedRoi.SelectedImageIndex = 2;
            this.tvwSelectedRoi.Size = new System.Drawing.Size(229, 537);
            this.tvwSelectedRoi.TabIndex = 2;
            this.tvwSelectedRoi.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwSelectedRoi_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.png");
            this.imageList1.Images.SetKeyName(1, "2.png");
            this.imageList1.Images.SetKeyName(2, "3.png");
            // 
            // tvwRoiList
            // 
            this.tvwRoiList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwRoiList.ImageIndex = 2;
            this.tvwRoiList.ImageList = this.imageList1;
            this.tvwRoiList.ItemHeight = 20;
            this.tvwRoiList.Location = new System.Drawing.Point(3, 22);
            this.tvwRoiList.Name = "tvwRoiList";
            treeNode5.Name = "节点0";
            treeNode5.Text = "01.1a";
            treeNode6.Name = "节点1";
            treeNode6.Text = "02.3a";
            treeNode7.Name = "节点2";
            treeNode7.Text = "03.5a";
            treeNode8.Name = "节点3";
            treeNode8.Text = "04.5b";
            this.tvwRoiList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.tvwRoiList.SelectedImageIndex = 2;
            this.tvwRoiList.Size = new System.Drawing.Size(229, 540);
            this.tvwRoiList.TabIndex = 3;
            this.tvwRoiList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwRoiList_NodeMouseClick);
            this.tvwRoiList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwRoiList_NodeMouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvwRoiList);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(0, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 565);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ROI列表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tvwSelectedRoi);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(255, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 562);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已选ROI";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAdd.Location = new System.Drawing.Point(507, 134);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 45);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDelete.Location = new System.Drawing.Point(507, 219);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 45);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(621, 615);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择ROI";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSave.Location = new System.Drawing.Point(507, 314);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 45);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.hyImageDisplayControl1);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox4.Location = new System.Drawing.Point(630, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(748, 605);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ROI显示";
            // 
            // hyImageDisplayControl1
            // 
            this.hyImageDisplayControl1.BottomToolVisible = true;
            this.hyImageDisplayControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyImageDisplayControl1.EditRoiEnable = true;
            this.hyImageDisplayControl1.Location = new System.Drawing.Point(3, 22);
            this.hyImageDisplayControl1.Margin = new System.Windows.Forms.Padding(2);
            this.hyImageDisplayControl1.Name = "hyImageDisplayControl1";
            this.hyImageDisplayControl1.ShowEditROIForm = false;
            this.hyImageDisplayControl1.Size = new System.Drawing.Size(742, 580);
            this.hyImageDisplayControl1.TabIndex = 0;
            this.hyImageDisplayControl1.TopToolVisible = true;
            // 
            // FrmSelectROI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 620);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmSelectROI";
            this.Text = "选择输入ROI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSelectROI_FormClosing);
            this.Load += new System.EventHandler(this.FrmSelectROI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HyVision.Tools.ImageDisplay.HyImageDisplayControl hyImageDisplayControl1;
        private System.Windows.Forms.TreeView tvwSelectedRoi;
        private System.Windows.Forms.TreeView tvwRoiList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ImageList imageList1;
    }
}