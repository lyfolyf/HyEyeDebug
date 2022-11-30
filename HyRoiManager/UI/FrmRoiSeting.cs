using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HyDisplayWindow;
using HyRoiManager.ROI;




namespace HyRoiManager.UI
{
    public partial class FrmRoiSeting : Form
    {

        private string ImagePath;
        private HyImageViewer ImageViewer;
        public HyRoiController RoiController;
        private RadioButton SelectedRadioButton;
        private List<BaseHyROI> HyRois = new List<BaseHyROI>();

        public bool Modify { get; set; } = false;


        public FrmRoiSeting()
        {
            InitializeComponent();

            ImageViewer = new HyImageViewer();
            ImageViewer.Dock = DockStyle.Fill;
            panel1.Controls.Add(ImageViewer);
            RoiController = new HyRoiController(ImageViewer, false);


           
        }


        public FrmRoiSeting(RoiData DisplayRoiData, Bitmap Img = null) : this()
        {
            if (Img != null)
            {
                ImageViewer.DisplayImage(Img);
            }
            RoiController.LoadPaintingImageRoiData(DisplayRoiData);
        }


        private void FrmRoiSeting_Load(object sender, EventArgs e)
        {

            nudBrushSize.Value = RoiController.BrushSize;
            SelectedRadioButton = rbDaub;
            rbDaub.Checked = true;
        }


        public void DisplayImage(string ImagePath)
        {
            if (!string.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
            {
                this.ImagePath = ImagePath.Clone().ToString();
                tbxImagePath.Text = ImagePath;
                ImageViewer.DisplayImage(ImagePath);
            }
        }

        public string GetImagePath()
        {
            return ImagePath;
        }


        private void btnImagePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择显示图片";
            openFileDialog.Filter = "图片(jpg;png;gif;bmp;jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog.FileName;
                tbxImagePath.Text = openFileDialog.FileName;
                ImageViewer.DisplayImage(openFileDialog.FileName);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cbCross_CheckedChanged(object sender, EventArgs e)
        {
            ImageViewer.CrossController.Visible = cbCross.Checked;
        }

        private void nudBrushSize_ValueChanged(object sender, EventArgs e)
        {  
            RoiController.BrushSize = (int)nudBrushSize.Value;
        }

        private void rbMove_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMove.Checked == true)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbMove;
                rbMove.BackColor = Color.LightGreen;
                //ImageViewer.Cursor = Cursors.Hand;
                nudBrushSize.Enabled = false;

                RoiController.SetRoiSelected(false);
                RoiController.DrawState = HyRoiController.DrawingState.Nothing;
                RoiController.CurrentRoiType = null;
                ImageViewer.SetMovingEnable(true);
                ImageViewer.Invalidate();
            }
        }

        private void rbEraser_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEraser.Checked)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbEraser;
                rbEraser.BackColor = Color.LightGreen;
                ImageViewer.Cursor = Cursors.Default;
                nudBrushSize.Enabled = true;
                RoiController.CurrentRoiType = RoiType.Erase;
            }

        }

        private void rbDaub_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaub.Checked)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbDaub;
                rbDaub.BackColor = Color.LightGreen;
                ImageViewer.Cursor = Cursors.Default;
                ImageViewer.SetMovingEnable(false);
                nudBrushSize.Enabled = true;
                RoiController.CurrentRoiType = RoiType.Daub;

            }

        }

        private void rbRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRectangle.Checked)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbRectangle;
                rbRectangle.BackColor = Color.LightGreen;
                ImageViewer.Cursor = Cursors.Default;
                nudBrushSize.Enabled = false;
                RoiController.CurrentRoiType = RoiType.Rectangle1;
            }

        }

        private void rbCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCircle.Checked)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbCircle;
                rbCircle.BackColor = Color.LightGreen;
                ImageViewer.Cursor = Cursors.Default;
                nudBrushSize.Enabled = false;
                RoiController.CurrentRoiType = RoiType.Circle;
            }
        }

        private void rbEllipse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEllipse.Checked)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbEllipse;
                rbEllipse.BackColor = Color.LightGreen;
                ImageViewer.Cursor = Cursors.Default;
                nudBrushSize.Enabled = false;
                RoiController.CurrentRoiType = RoiType.Ellipse;
            }
        }

        private void rbPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPolygon.Checked)
            {
                SelectedRadioButton.BackColor = Color.Transparent;
                SelectedRadioButton = rbPolygon;
                rbPolygon.BackColor = Color.LightGreen;
                ImageViewer.Cursor = Cursors.Default;
                nudBrushSize.Enabled = false;
                RoiController.CurrentRoiType = RoiType.Polygon;
            }


        }


    }
}
