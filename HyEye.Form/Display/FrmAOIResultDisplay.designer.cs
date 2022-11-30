
using HyEye.WForm.Display;

namespace HyEye.WForm
{
    partial class FrmAOIResultDisplay
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
            System.Windows.Forms.BindingSource defectInfoBindingSource;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAOIResultDisplay));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.hyDpDefect = new HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple();
            this.lblDefectData = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblDefectImage = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblDefectLight = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblDefectCamera = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblDefectBlock = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblDefectArea = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblDefectType = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblDefectSurface = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPreview = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucAreaSelect1 = new HyEye.WForm.Display.UcAreaSelect();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucAreaSelect2 = new HyEye.WForm.Display.UcAreaSelect();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbStandImg = new System.Windows.Forms.GroupBox();
            this.hyDpModel = new HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbCurrentImg = new System.Windows.Forms.GroupBox();
            this.hyDpCurrent = new HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTotalArea = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblDefectCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblProductModel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblProductSurface = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProductSn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDefects = new System.Windows.Forms.DataGridView();
            this.surfaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defectTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Camera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Light = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLblSurfaceConfig = new System.Windows.Forms.LinkLabel();
            this.label32 = new System.Windows.Forms.Label();
            defectInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(defectInfoBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPreview.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbStandImg.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbCurrentImg.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefects)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // defectInfoBindingSource
            // 
            defectInfoBindingSource.AllowNew = true;
            defectInfoBindingSource.DataSource = typeof(HyEye.WForm.Display.DefectInfo);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.54279F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.45721F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1240, 856);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(620, 503);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(310, 327);
            this.panel6.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.hyDpDefect);
            this.groupBox3.Controls.Add(this.lblDefectData);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.lblDefectImage);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.lblDefectLight);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.lblDefectCamera);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.lblDefectBlock);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.lblDefectArea);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.lblDefectType);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.lblDefectSurface);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(310, 327);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "详细";
            // 
            // hyDpDefect
            // 
            this.hyDpDefect.AllowOperation = true;
            this.hyDpDefect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hyDpDefect.AutoFit = true;
            this.hyDpDefect.BackColor = System.Drawing.Color.Transparent;
            this.hyDpDefect.Location = new System.Drawing.Point(151, 16);
            this.hyDpDefect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hyDpDefect.Name = "hyDpDefect";
            this.hyDpDefect.SelecedtIndex = -1;
            this.hyDpDefect.ShowCoorMsg = false;
            this.hyDpDefect.Size = new System.Drawing.Size(153, 128);
            this.hyDpDefect.TabIndex = 27;
            // 
            // lblDefectData
            // 
            this.lblDefectData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDefectData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectData.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectData.Location = new System.Drawing.Point(84, 278);
            this.lblDefectData.Name = "lblDefectData";
            this.lblDefectData.Size = new System.Drawing.Size(220, 41);
            this.lblDefectData.TabIndex = 26;
            this.lblDefectData.Text = "BlobCOunt";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(7, 278);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(68, 17);
            this.label31.TabIndex = 25;
            this.label31.Text = "算法数据：";
            // 
            // lblDefectImage
            // 
            this.lblDefectImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDefectImage.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectImage.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectImage.Location = new System.Drawing.Point(65, 224);
            this.lblDefectImage.Name = "lblDefectImage";
            this.lblDefectImage.Size = new System.Drawing.Size(239, 50);
            this.lblDefectImage.TabIndex = 24;
            this.lblDefectImage.Text = "J413.RSH0000151370800.CosmeticAOI.20210930.154354.LCM.Coaxial.1_4";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.Location = new System.Drawing.Point(7, 223);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(44, 17);
            this.label29.TabIndex = 23;
            this.label29.Text = "图片：";
            // 
            // lblDefectLight
            // 
            this.lblDefectLight.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectLight.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectLight.Location = new System.Drawing.Point(56, 114);
            this.lblDefectLight.Name = "lblDefectLight";
            this.lblDefectLight.Size = new System.Drawing.Size(76, 30);
            this.lblDefectLight.TabIndex = 22;
            this.lblDefectLight.Text = "Coaxial";
            this.lblDefectLight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.Location = new System.Drawing.Point(6, 122);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(44, 17);
            this.label27.TabIndex = 21;
            this.label27.Text = "灯光：";
            // 
            // lblDefectCamera
            // 
            this.lblDefectCamera.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectCamera.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectCamera.Location = new System.Drawing.Point(54, 84);
            this.lblDefectCamera.Name = "lblDefectCamera";
            this.lblDefectCamera.Size = new System.Drawing.Size(65, 30);
            this.lblDefectCamera.TabIndex = 20;
            this.lblDefectCamera.Text = "Camera 1";
            this.lblDefectCamera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(6, 90);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 17);
            this.label25.TabIndex = 19;
            this.label25.Text = "相机：";
            // 
            // lblDefectBlock
            // 
            this.lblDefectBlock.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectBlock.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectBlock.Location = new System.Drawing.Point(84, 54);
            this.lblDefectBlock.Name = "lblDefectBlock";
            this.lblDefectBlock.Size = new System.Drawing.Size(61, 30);
            this.lblDefectBlock.TabIndex = 18;
            this.lblDefectBlock.Text = "5";
            this.lblDefectBlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(7, 61);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(68, 17);
            this.label23.TabIndex = 17;
            this.label23.Text = "检测区域：";
            // 
            // lblDefectArea
            // 
            this.lblDefectArea.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectArea.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectArea.Location = new System.Drawing.Point(84, 177);
            this.lblDefectArea.Name = "lblDefectArea";
            this.lblDefectArea.Size = new System.Drawing.Size(89, 30);
            this.lblDefectArea.TabIndex = 16;
            this.lblDefectArea.Text = "1268.58 mm²";
            this.lblDefectArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(7, 185);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 17);
            this.label21.TabIndex = 15;
            this.label21.Text = "缺陷面积：";
            // 
            // lblDefectType
            // 
            this.lblDefectType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectType.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectType.Location = new System.Drawing.Point(84, 148);
            this.lblDefectType.Name = "lblDefectType";
            this.lblDefectType.Size = new System.Drawing.Size(89, 30);
            this.lblDefectType.TabIndex = 14;
            this.lblDefectType.Text = "Dent Shiny";
            this.lblDefectType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(7, 154);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 17);
            this.label19.TabIndex = 13;
            this.label19.Text = "缺陷类型：";
            // 
            // lblDefectSurface
            // 
            this.lblDefectSurface.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectSurface.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectSurface.Location = new System.Drawing.Point(70, 23);
            this.lblDefectSurface.Name = "lblDefectSurface";
            this.lblDefectSurface.Size = new System.Drawing.Size(75, 30);
            this.lblDefectSurface.TabIndex = 12;
            this.lblDefectSurface.Text = "Mandrel";
            this.lblDefectSurface.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(7, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 17);
            this.label17.TabIndex = 11;
            this.label17.Text = "检测面：";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 503);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(310, 327);
            this.panel4.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabPreview);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(310, 327);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "全览";
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.tabPage1);
            this.tabPreview.Controls.Add(this.tabPage2);
            this.tabPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPreview.Location = new System.Drawing.Point(3, 20);
            this.tabPreview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.SelectedIndex = 0;
            this.tabPreview.Size = new System.Drawing.Size(304, 303);
            this.tabPreview.TabIndex = 1;
            this.tabPreview.SelectedIndexChanged += new System.EventHandler(this.tabPreview_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DimGray;
            this.tabPage1.Controls.Add(this.ucAreaSelect1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(296, 273);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "LCM";
            // 
            // ucAreaSelect1
            // 
            this.ucAreaSelect1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucAreaSelect1.BackgroundImage")));
            this.ucAreaSelect1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucAreaSelect1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucAreaSelect1.ForeColor = System.Drawing.Color.SpringGreen;
            this.ucAreaSelect1.HCount = 3;
            this.ucAreaSelect1.HighlightColor = System.Drawing.Color.Yellow;
            this.ucAreaSelect1.HighlightTransparency = ((byte)(100));
            this.ucAreaSelect1.LineColor = System.Drawing.Color.Yellow;
            this.ucAreaSelect1.Location = new System.Drawing.Point(3, 25);
            this.ucAreaSelect1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucAreaSelect1.Name = "ucAreaSelect1";
            this.ucAreaSelect1.Size = new System.Drawing.Size(277, 224);
            this.ucAreaSelect1.TabIndex = 1;
            this.ucAreaSelect1.VCount = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.DimGray;
            this.tabPage2.Controls.Add(this.ucAreaSelect2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(296, 273);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mandrel";
            // 
            // ucAreaSelect2
            // 
            this.ucAreaSelect2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucAreaSelect2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucAreaSelect2.ForeColor = System.Drawing.Color.SpringGreen;
            this.ucAreaSelect2.HCount = 1;
            this.ucAreaSelect2.HighlightColor = System.Drawing.Color.Yellow;
            this.ucAreaSelect2.HighlightTransparency = ((byte)(100));
            this.ucAreaSelect2.LineColor = System.Drawing.Color.Yellow;
            this.ucAreaSelect2.Location = new System.Drawing.Point(5, 24);
            this.ucAreaSelect2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucAreaSelect2.Name = "ucAreaSelect2";
            this.ucAreaSelect2.Size = new System.Drawing.Size(285, 204);
            this.ucAreaSelect2.TabIndex = 2;
            this.ucAreaSelect2.VCount = 4;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.gbStandImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 503);
            this.panel1.TabIndex = 0;
            // 
            // gbStandImg
            // 
            this.gbStandImg.Controls.Add(this.hyDpModel);
            this.gbStandImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStandImg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbStandImg.Location = new System.Drawing.Point(0, 0);
            this.gbStandImg.Margin = new System.Windows.Forms.Padding(0);
            this.gbStandImg.Name = "gbStandImg";
            this.gbStandImg.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbStandImg.Size = new System.Drawing.Size(620, 503);
            this.gbStandImg.TabIndex = 0;
            this.gbStandImg.TabStop = false;
            this.gbStandImg.Text = "标准影像";
            // 
            // hyDpModel
            // 
            this.hyDpModel.AllowOperation = false;
            this.hyDpModel.AutoFit = true;
            this.hyDpModel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.hyDpModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyDpModel.Location = new System.Drawing.Point(3, 20);
            this.hyDpModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hyDpModel.Name = "hyDpModel";
            this.hyDpModel.SelecedtIndex = -1;
            this.hyDpModel.ShowCoorMsg = false;
            this.hyDpModel.Size = new System.Drawing.Size(614, 479);
            this.hyDpModel.TabIndex = 0;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.gbCurrentImg);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(620, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(620, 503);
            this.panel3.TabIndex = 2;
            // 
            // gbCurrentImg
            // 
            this.gbCurrentImg.Controls.Add(this.hyDpCurrent);
            this.gbCurrentImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCurrentImg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbCurrentImg.Location = new System.Drawing.Point(0, 0);
            this.gbCurrentImg.Margin = new System.Windows.Forms.Padding(0);
            this.gbCurrentImg.Name = "gbCurrentImg";
            this.gbCurrentImg.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbCurrentImg.Size = new System.Drawing.Size(620, 503);
            this.gbCurrentImg.TabIndex = 1;
            this.gbCurrentImg.TabStop = false;
            this.gbCurrentImg.Text = "当前影像";
            // 
            // hyDpCurrent
            // 
            this.hyDpCurrent.AllowOperation = true;
            this.hyDpCurrent.AutoFit = true;
            this.hyDpCurrent.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.hyDpCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hyDpCurrent.Location = new System.Drawing.Point(3, 20);
            this.hyDpCurrent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hyDpCurrent.Name = "hyDpCurrent";
            this.hyDpCurrent.SelecedtIndex = -1;
            this.hyDpCurrent.ShowCoorMsg = false;
            this.hyDpCurrent.Size = new System.Drawing.Size(614, 479);
            this.hyDpCurrent.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(930, 503);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(310, 327);
            this.panel8.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTotalArea);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.lblDefectCount);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lblProductModel);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.lblProductSurface);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.lblDate);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.lblStation);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.lblProductSn);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(310, 327);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "判定结果";
            // 
            // lblTotalArea
            // 
            this.lblTotalArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalArea.AutoSize = true;
            this.lblTotalArea.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalArea.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalArea.Location = new System.Drawing.Point(80, 238);
            this.lblTotalArea.Name = "lblTotalArea";
            this.lblTotalArea.Size = new System.Drawing.Size(49, 20);
            this.lblTotalArea.TabIndex = 14;
            this.lblTotalArea.Text = "15868";
            this.lblTotalArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(9, 238);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 20);
            this.label15.TabIndex = 13;
            this.label15.Text = "总面积：";
            // 
            // lblDefectCount
            // 
            this.lblDefectCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDefectCount.AutoSize = true;
            this.lblDefectCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefectCount.ForeColor = System.Drawing.Color.Blue;
            this.lblDefectCount.Location = new System.Drawing.Point(87, 208);
            this.lblDefectCount.Name = "lblDefectCount";
            this.lblDefectCount.Size = new System.Drawing.Size(17, 20);
            this.lblDefectCount.TabIndex = 12;
            this.lblDefectCount.Text = "3";
            this.lblDefectCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(9, 209);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 20);
            this.label13.TabIndex = 11;
            this.label13.Text = "缺陷数量：";
            // 
            // lblProductModel
            // 
            this.lblProductModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductModel.AutoSize = true;
            this.lblProductModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductModel.ForeColor = System.Drawing.Color.Blue;
            this.lblProductModel.Location = new System.Drawing.Point(64, 122);
            this.lblProductModel.Name = "lblProductModel";
            this.lblProductModel.Size = new System.Drawing.Size(39, 20);
            this.lblProductModel.TabIndex = 10;
            this.lblProductModel.Text = "J413";
            this.lblProductModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(9, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "型号：";
            // 
            // lblProductSurface
            // 
            this.lblProductSurface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductSurface.AutoSize = true;
            this.lblProductSurface.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductSurface.ForeColor = System.Drawing.Color.Blue;
            this.lblProductSurface.Location = new System.Drawing.Point(64, 178);
            this.lblProductSurface.Name = "lblProductSurface";
            this.lblProductSurface.Size = new System.Drawing.Size(39, 20);
            this.lblProductSurface.TabIndex = 8;
            this.lblProductSurface.Text = "LCM";
            this.lblProductSurface.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(9, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "区域：";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate.ForeColor = System.Drawing.Color.Blue;
            this.lblDate.Location = new System.Drawing.Point(67, 298);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(170, 20);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "2021/10/29 15:42:18.158";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(9, 296);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "日期：";
            // 
            // lblStation
            // 
            this.lblStation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStation.AutoSize = true;
            this.lblStation.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStation.ForeColor = System.Drawing.Color.Blue;
            this.lblStation.Location = new System.Drawing.Point(63, 268);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(81, 20);
            this.lblStation.TabIndex = 4;
            this.lblStation.Text = "STATION 2";
            this.lblStation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(9, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "工站：";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.LimeGreen;
            this.label3.Location = new System.Drawing.Point(7, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 88);
            this.label3.TabIndex = 2;
            this.label3.Text = "PASS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductSn
            // 
            this.lblProductSn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductSn.AutoSize = true;
            this.lblProductSn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductSn.ForeColor = System.Drawing.Color.Blue;
            this.lblProductSn.Location = new System.Drawing.Point(53, 148);
            this.lblProductSn.Name = "lblProductSn";
            this.lblProductSn.Size = new System.Drawing.Size(109, 20);
            this.lblProductSn.TabIndex = 1;
            this.lblProductSn.Text = "HY5WFW67M6";
            this.lblProductSn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "SN：";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(310, 503);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(310, 327);
            this.panel7.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDefects);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(310, 327);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "列表";
            // 
            // dgvDefects
            // 
            this.dgvDefects.AllowUserToAddRows = false;
            this.dgvDefects.AllowUserToDeleteRows = false;
            this.dgvDefects.AllowUserToResizeColumns = false;
            this.dgvDefects.AllowUserToResizeRows = false;
            this.dgvDefects.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDefects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDefects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.surfaceDataGridViewTextBoxColumn,
            this.blockDataGridViewTextBoxColumn,
            this.defectTypeDataGridViewTextBoxColumn,
            this.areaDataGridViewTextBoxColumn,
            this.Camera,
            this.Light});
            this.dgvDefects.DataSource = defectInfoBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDefects.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDefects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDefects.Location = new System.Drawing.Point(3, 20);
            this.dgvDefects.Name = "dgvDefects";
            this.dgvDefects.ReadOnly = true;
            this.dgvDefects.RowHeadersVisible = false;
            this.dgvDefects.RowTemplate.Height = 23;
            this.dgvDefects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDefects.Size = new System.Drawing.Size(304, 303);
            this.dgvDefects.TabIndex = 1;
            this.dgvDefects.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefects_CellClick);
            // 
            // surfaceDataGridViewTextBoxColumn
            // 
            this.surfaceDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.surfaceDataGridViewTextBoxColumn.DataPropertyName = "Surface";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.surfaceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.surfaceDataGridViewTextBoxColumn.HeaderText = "检测面";
            this.surfaceDataGridViewTextBoxColumn.Name = "surfaceDataGridViewTextBoxColumn";
            this.surfaceDataGridViewTextBoxColumn.ReadOnly = true;
            this.surfaceDataGridViewTextBoxColumn.Width = 69;
            // 
            // blockDataGridViewTextBoxColumn
            // 
            this.blockDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.blockDataGridViewTextBoxColumn.DataPropertyName = "Block";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.blockDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.blockDataGridViewTextBoxColumn.HeaderText = "区块";
            this.blockDataGridViewTextBoxColumn.Name = "blockDataGridViewTextBoxColumn";
            this.blockDataGridViewTextBoxColumn.ReadOnly = true;
            this.blockDataGridViewTextBoxColumn.Width = 57;
            // 
            // defectTypeDataGridViewTextBoxColumn
            // 
            this.defectTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.defectTypeDataGridViewTextBoxColumn.DataPropertyName = "DefectType";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.defectTypeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.defectTypeDataGridViewTextBoxColumn.HeaderText = "缺陷类型";
            this.defectTypeDataGridViewTextBoxColumn.Name = "defectTypeDataGridViewTextBoxColumn";
            this.defectTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.defectTypeDataGridViewTextBoxColumn.Width = 81;
            // 
            // areaDataGridViewTextBoxColumn
            // 
            this.areaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.areaDataGridViewTextBoxColumn.DataPropertyName = "Area";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.areaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.areaDataGridViewTextBoxColumn.HeaderText = "面积";
            this.areaDataGridViewTextBoxColumn.Name = "areaDataGridViewTextBoxColumn";
            this.areaDataGridViewTextBoxColumn.ReadOnly = true;
            this.areaDataGridViewTextBoxColumn.Width = 57;
            // 
            // Camera
            // 
            this.Camera.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Camera.DataPropertyName = "Camera";
            this.Camera.HeaderText = "相机";
            this.Camera.Name = "Camera";
            this.Camera.ReadOnly = true;
            this.Camera.Width = 57;
            // 
            // Light
            // 
            this.Light.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Light.DataPropertyName = "Light";
            this.Light.HeaderText = "灯光";
            this.Light.Name = "Light";
            this.Light.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 4);
            this.panel2.Controls.Add(this.linkLblSurfaceConfig);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 830);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1240, 26);
            this.panel2.TabIndex = 8;
            // 
            // linkLblSurfaceConfig
            // 
            this.linkLblSurfaceConfig.AutoSize = true;
            this.linkLblSurfaceConfig.Location = new System.Drawing.Point(54, 4);
            this.linkLblSurfaceConfig.Name = "linkLblSurfaceConfig";
            this.linkLblSurfaceConfig.Size = new System.Drawing.Size(44, 17);
            this.linkLblSurfaceConfig.TabIndex = 3;
            this.linkLblSurfaceConfig.TabStop = true;
            this.linkLblSurfaceConfig.Text = "配置面";
            this.linkLblSurfaceConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblSurfaceConfig_LinkClicked);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(4, 4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(44, 17);
            this.label32.TabIndex = 2;
            this.label32.Text = "设置：";
            // 
            // FrmAOIResultDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 856);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmAOIResultDisplay";
            this.Text = "检测结果显示";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAOIResultDisplay_FormClosing);
            this.Load += new System.EventHandler(this.FrmAOIResultDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(defectInfoBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPreview.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbStandImg.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbCurrentImg.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefects)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox gbStandImg;
        private HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple hyDpModel;
        private System.Windows.Forms.GroupBox gbCurrentImg;
        private HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple hyDpCurrent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProductSn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProductModel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblProductSurface;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalArea;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblDefectCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabControl tabPreview;
        private System.Windows.Forms.TabPage tabPage1;
        private UcAreaSelect ucAreaSelect1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDefectArea;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblDefectType;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblDefectSurface;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblDefectBlock;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblDefectLight;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblDefectCamera;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblDefectData;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblDefectImage;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panel2;
        private UcAreaSelect ucAreaSelect2;
        private System.Windows.Forms.LinkLabel linkLblSurfaceConfig;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.DataGridView dgvDefects;
        private System.Windows.Forms.DataGridViewTextBoxColumn surfaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defectTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Camera;
        private System.Windows.Forms.DataGridViewTextBoxColumn Light;
        private HyVision.Tools.ImageDisplay.HyImageDisplayControlSimple hyDpDefect;
    }
}