using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HyVision.Tools.ImageDisplay
{
    public partial class HyImageDisplayControl : UserControl
    {

        public HyImageDisplayControl()
        {
            InitializeComponent();
      
        }


        //private List<ROI> LocalROIs = new List<ROI>();




        private PointF MouseDownPos;
        private double OffsetX, OffsetY, ScaleX = 1, ScaleY = 1;
        private bool IsDrawing;



        private void HyImageDisplayControl_Load(object sender, EventArgs e)
        {
         
            InitializeEvent();
            InitializeContextMenuStrip();


        }

        private void InitializeEvent()
        {
            HyDisplayPanel.MouseDoubleClick += HyDisplayPanel_MouseDoubleClick;
            HyDisplayPanel.MouseDown += HyDisplayPanel_MouseDown;
            HyDisplayPanel.MouseUp += HyDisplayPanel_MouseUp;
            HyDisplayPanel.MouseMove += HyDisplayPanel_MouseMove;
            HyDisplayPanel.MouseWheel += HyDisplayPanel_MouseWheel;
            HyDisplayPanel.Paint += HyDisplayPanel_Paint;
        }

        private void InitializeContextMenuStrip()
        {
            HyDisplayPanel.ContextMenuStrip = contextMenuStrip1;

        }





        private void HyDisplayPanel_Paint(object sender, PaintEventArgs e)
        {
            if (bpImage != null)
            {
                Graphics Canvas = e.Graphics;
                Canvas.SmoothingMode = SmoothingMode.None;
                Canvas.InterpolationMode = InterpolationMode.NearestNeighbor;
                Canvas.TranslateTransform((float)OffsetX, (float)OffsetY);
                Canvas.ScaleTransform((float)ScaleX, (float)ScaleY);

                Canvas.DrawImage(bpImage, 0, 0, bpImage.Width, bpImage.Height);
            }
        }

        private void DisaplayROI(List<ROI> ROIs)
        {

        }

        #region ????????????
        private void HyDisplayPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (bpImage != null)
            {
                SetImagePosition(true);
            }
        }

        private void HyDisplayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownPos = MousePosition;
        }

        private void HyDisplayPanel_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void HyDisplayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (bpImage == null) return;
            Point CurrentMousePos = MousePosition;
            Point CurrentPanlePoint = HyDisplayPanel.PointToClient(CurrentMousePos);
            double ImagePx, ImagePy;

            //????????????
            //if (Gpath.IsVisible(new PointF((p1.X - dx) / sx - 0.5f, (p1.Y - dy) / sy - 0.5f)))
            //{
            //    this.Cursor = Cursors.Hand;
            //}
            //else
            //{
            //    this.Cursor = Cursors.Default;
            //}


            ImagePx = (CurrentPanlePoint.X - OffsetX) / ScaleX + 0.5;
            ImagePy = (CurrentPanlePoint.Y - OffsetY) / ScaleY + 0.5;

            int Row = (int)Math.Floor(ImagePy);
            int Column = (int)Math.Floor(ImagePx);
            if (Row >= 0 && Row <= bpImage.Height - 1 && Column >= 0 && Column <= bpImage.Width - 1)
            {
                Color c = bpImage.GetPixel(Column, Row);
                toolStripLabel1.Text = $"[ X {Column}, Y {Row} ]     [ R{c.R}, G{c.G}, B{c.B} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
            else
            {
                toolStripLabel1.Text = $"[ X {Column}, Y {Row} ]     [ R{0} ,G{0} ,B{0} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }

            if (e.Button == MouseButtons.Left && IsDrawing == false)
            {
                OffsetX += CurrentMousePos.X - MouseDownPos.X;
                OffsetY += CurrentMousePos.Y - MouseDownPos.Y;
                HyDisplayPanel.Invalidate();
                MouseDownPos = CurrentMousePos;
            }
        }

        private void HyDisplayPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (bpImage == null) return;
            PointF CurrentPanlePoint = HyDisplayPanel.PointToClient(MousePosition);
            double AfterScalePx, AfterScalePy, ScaleRatio = 1.5;//?????????????????????????????????

            if (e.Delta > 0)
            {
                ScaleX *= ScaleRatio;
                ScaleY *= ScaleRatio;

                AfterScalePx = (CurrentPanlePoint.X - OffsetX) * ScaleRatio;
                AfterScalePy = (CurrentPanlePoint.Y - OffsetY) * ScaleRatio;
            }
            else
            {
                ScaleX /= ScaleRatio;
                ScaleY /= ScaleRatio;

                AfterScalePx = (CurrentPanlePoint.X - OffsetX) / ScaleRatio;
                AfterScalePy = (CurrentPanlePoint.Y - OffsetY) / ScaleRatio;
            }

            //???????????? = ???????????? + ?????????????????????????????????
            OffsetX = OffsetX + (CurrentPanlePoint.X - OffsetX) - AfterScalePx;
            OffsetY = OffsetY + (CurrentPanlePoint.Y - OffsetY) - AfterScalePy;

            HyDisplayPanel.Invalidate();
        }

        #endregion



        #region ??????????????????
        private void tsmiImportImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.Title = "????????????????????????";
            Openfile.Multiselect = false;
            Openfile.Filter = "??????(*.jpg;*.png;*.gif;*.bmp;*.jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";

            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                DisplayImage(Openfile.FileName);
                SetImagePosition(true);
            }
        }

        private void tsmiExportImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveImageDialog = new SaveFileDialog();
            SaveImageDialog.Title = "????????????";
            SaveImageDialog.Filter = "XML??????(*.bmp)|*.bmp";

            if (SaveImageDialog.ShowDialog() == DialogResult.OK && bpImage != null)
            {
                bpImage.Save(SaveImageDialog.FileName);
            }
        }

        private void tsmiDisplayOriImg_Click(object sender, EventArgs e)
        {
            if (bpImage != null)
            {
                SetImagePosition(true);
            }
        }

        private void tsmiDrawCircle_Click(object sender, EventArgs e)
        {

        }

        private void tsmiDrawRectangle_Click(object sender, EventArgs e)
        {

        }

        private void tsmiDrawPolygon_Click(object sender, EventArgs e)
        {

        }

        #endregion



        private void SetImagePosition(bool ShowInCenter = false)
        {
            if (ImgWidth != bpImage.Width || ImgHeight != bpImage.Height || ShowInCenter == true)
            {
                ImgWidth = bpImage.Width;
                ImgHeight = bpImage.Height;

                float RatioWidth = (float)HyDisplayPanel.Width / ImgWidth;
                float RatioHeight = (float)HyDisplayPanel.Height / ImgHeight;

                if (RatioWidth < RatioHeight)//????????????????????????????????????????????????????????????????????????
                {

                    ScaleX = RatioWidth;
                    ScaleY = ScaleX;

                    OffsetX = 0;
                    OffsetY = (HyDisplayPanel.Height - ImgHeight * ScaleY) / 2;

                }
                else                          //????????????????????????????????????????????????????????????????????????
                {
                    ScaleX = RatioHeight;
                    ScaleY = ScaleX;

                    OffsetX = (HyDisplayPanel.Width - ImgWidth * ScaleY) / 2;
                    OffsetY = 0;
                }
            }
            HyDisplayPanel.Invalidate();
        }


        #region ??????????????????


        private Bitmap bpImage = null;
        private int ImgWidth, ImgHeight;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HyImageDisplayControl
            // 
            this.Name = "HyImageDisplayControl";
            this.Size = new System.Drawing.Size(348, 282);
            this.ResumeLayout(false);

        }

  



        public void DisplayImage(string FilePath)
        {
            bpImage = new Bitmap(FilePath);
            SetImagePosition();
        }

        public void DisplayImage(Bitmap DispalyImage)
        {
            bpImage = (Bitmap)DispalyImage.Clone();
            SetImagePosition();
        }


        public void Test()
        {
            float RatioWidth = 1, RatioHeight = 1;

            RatioWidth = (float)ImgWidth / HyDisplayPanel.Width;
            RatioHeight = (float)ImgHeight / HyDisplayPanel.Height;

            //ScaleX = 1d * HyDisplayPanel.Width / ImgWidth;
            //ScaleY = 1d * HyDisplayPanel.Width / ImgWidth;

            ScaleX = 1d * HyDisplayPanel.Height / ImgHeight;
            ScaleY = 1d * HyDisplayPanel.Height / ImgHeight;

            var a = this.Width + this.Height;
            HyDisplayPanel.Invalidate();
        }


        #endregion



    }
}
