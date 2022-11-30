using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace HyDisplayWindow
{
    public class HyImageViewer : Control
    {


        private IDrawingRoi drawingRoi;
        private ToolStrip tsCoordinateDisplay;
        private Bitmap SourceImage;
        private Point MouseDownPoint;
        private float OffsetX, OffsetY, ScaleX = 1, ScaleY = 1;
        private int ImgWidth, ImgHeight;
        private bool MovingEnable = true;

        // private 导航图没写

        public ContextMenuStrip RightClickMenu { get; set; }

        public MessageManager MessageController { get; set; } = new MessageManager();

        public CrossCurve CrossController { get; set; }

      




        public HyImageViewer()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            //BackColor = SystemColors.ScrollBar;
            BackColor = Color.PaleTurquoise;
            InitializeControl();
         
        }

        private void InitializeControl()
        {
            CrossController = new CrossCurve(this);
            InitializeToolbar();
            InitializeRightButtonMenu();
        }

        private void InitializeToolbar()
        {
            tsCoordinateDisplay = new ToolStrip();
            tsCoordinateDisplay.Dock = DockStyle.Bottom;
            ToolStripItem toolStripItem = new ToolStripLabel()
            {
                Name = "lbImageCoord"
            };
            tsCoordinateDisplay.Items.Add(toolStripItem);
            Controls.Add(tsCoordinateDisplay);
        }

        private void InitializeRightButtonMenu()
        {
            RightClickMenu = new ContextMenuStrip();
            ToolStripMenuItem tsmiDisplayImage = new ToolStripMenuItem()
            {
                Text = "显示原图"
            };
            ToolStripMenuItem tsmiImportImage = new ToolStripMenuItem()
            {
                Text = "导入图片"
            };
            ToolStripMenuItem tsmiExportSrcImage = new ToolStripMenuItem()
            {
                Text = "导出原图"
            };
            ToolStripMenuItem tsmiExportDefectImage = new ToolStripMenuItem()
            {
                Text = "导出缺陷图"
            };

            tsmiDisplayImage.Click += new EventHandler((object o, EventArgs e) =>
            {
                SetImageInCenter(true);
            });

            tsmiImportImage.Click += new EventHandler((object o, EventArgs e) =>
            {
                OpenFileDialog Openfile = new OpenFileDialog();
                Openfile.Title = "导入图片";
                Openfile.Multiselect = false;
                Openfile.Filter = "图片(*.jpg;*.png;*.gif;*.bmp;*.jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";

                if (Openfile.ShowDialog() == DialogResult.OK)
                {
                    DisplayImage(Openfile.FileName);
                }
            });
            tsmiExportSrcImage.Click += new EventHandler((object o, EventArgs e) =>
            {
                SaveFileDialog SaveImageDialog = new SaveFileDialog();
                SaveImageDialog.Title = "导出原图";
                SaveImageDialog.Filter = $"图片(*.bmp)|*.bmp";

                if (SaveImageDialog.ShowDialog() == DialogResult.OK && SourceImage != null)
                {
                    SourceImage.Save(SaveImageDialog.FileName, SourceImage.RawFormat);
                }
            });
            tsmiExportDefectImage.Click += new EventHandler((object o, EventArgs e) =>
            {
                SaveFileDialog SaveImageDialog = new SaveFileDialog();
                SaveImageDialog.Title = "导出缺陷图";
                SaveImageDialog.Filter = $"图片(*.bmp)|*.bmp";

                //if (SaveImageDialog.ShowDialog() == DialogResult.OK && PaintingImage != null)
                //{
                //    PaintingImage.Save(SaveImageDialog.FileName, PaintingImage.RawFormat);
                //}
            });
            RightClickMenu.Items.Add(tsmiDisplayImage);
            ToolStripSeparator tss = new ToolStripSeparator();
            RightClickMenu.Items.Add(tss);
            RightClickMenu.Items.Add(tsmiImportImage);
            RightClickMenu.Items.Add(tsmiExportSrcImage);
            //cmsRightButtonMenu.Items.Add(tsmiExportDefectImage);
            ContextMenuStrip = RightClickMenu;
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            CrossController.LineX = Height / 2;
            CrossController.LineY = Width / 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            try
            {
                //if (SourceImage != null)
                {
                    Graphics Canvas = e.Graphics;
                    Canvas.SmoothingMode = SmoothingMode.None;
                    Canvas.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Canvas.PixelOffsetMode = PixelOffsetMode.Half;
                    Canvas.CompositingQuality = CompositingQuality.HighSpeed;

                    Canvas.TranslateTransform(OffsetX, OffsetY);
                    Canvas.ScaleTransform(ScaleX, ScaleY);
                    if (SourceImage != null)
                    {
                        Canvas.DrawImage(SourceImage, 0, 0, SourceImage.Width, SourceImage.Height);
                    }
                  

                    //if (WindowMode == DisplayMode.Display)
                    {
                        //if (RoiImage != null)
                        //{
                        //    Canvas.DrawImage(RoiImage, 0, 0, RoiImage.Width, RoiImage.Height);
                        //}
                    }
                    //else
                    //{
                    //    if (PaintingImage != null)
                    //    {
                    //        Canvas.DrawImage(PaintingImage, 0, 0, PaintingImage.Width, PaintingImage.Height);
                    //    }
                    //    RoiController.DisplayHyROI(Canvas, ImgPointF);
                    //}


                    //if (GDIroi != null)
                    //{
                    //    foreach (BaseHyROI roi in GDIroi)
                    //    {
                    //        roi.Display(Canvas);
                    //    }
                    //}


                    if (drawingRoi != null)
                    {
                        drawingRoi.DisplayHyRoi(Canvas);
                    }



                    MessageController.DisplayMessages(Canvas);

                    Canvas.ScaleTransform(1 / ScaleX, 1 / ScaleY);
                    Canvas.TranslateTransform(-OffsetX, -OffsetY);
                    CrossController.DrawCross(Canvas, Width, Height);
                }
            }
            catch (Exception exp)
            {

            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            drawingRoi?.OnMouseDoubleClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MouseDownPoint = new Point(e.X, e.Y);
            PointF ImagePoint = new PointF((e.X - OffsetX) / ScaleX, (e.Y - OffsetY) / ScaleY);
            drawingRoi?.OnMouseDown(e, ImagePoint);

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            PointF ImagePoint = new PointF((e.X - OffsetX) / ScaleX, (e.Y - OffsetY) / ScaleY);
            drawingRoi?.OnMouseUp(e, ImagePoint);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            
            //if (SourceImage == null) return;
            float AfterScalePx, AfterScalePy, ScaleRatio = 1.5f;
            if (e.Delta > 0)
            {
                ScaleX *= ScaleRatio;
                ScaleY *= ScaleRatio;

                AfterScalePx = (e.X - OffsetX) * ScaleRatio;
                AfterScalePy = (e.Y - OffsetY) * ScaleRatio;
            }
            else
            {
                ScaleX /= ScaleRatio;
                ScaleY /= ScaleRatio;

                AfterScalePx = (e.X - OffsetX) / ScaleRatio;
                AfterScalePy = (e.Y - OffsetY) / ScaleRatio;
            }

            OffsetX = OffsetX + (e.X - OffsetX) - AfterScalePx;
            OffsetY = OffsetY + (e.Y - OffsetY) - AfterScalePy;
            PointF ImagePoint = new PointF((e.X - OffsetX) / ScaleX, (e.Y - OffsetY) / ScaleY);
            DisplayCoordinate(ImagePoint);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //if (SourceImage == null) return;

      

            PointF ImagePoint = new PointF((e.X - OffsetX) / ScaleX, (e.Y - OffsetY) / ScaleY);
            drawingRoi?.OnMouseMove(e, ImagePoint);
            DisplayCoordinate(ImagePoint);


            if (e.Button == MouseButtons.Left && MovingEnable == true)
            {
                OffsetX += e.X - MouseDownPoint.X;
                OffsetY += e.Y - MouseDownPoint.Y;
                Invalidate();
                MouseDownPoint = new Point(e.X, e.Y);
            }

        }


        #region 公有方法

        public void BindingRoiManager(IDrawingRoi drawingRoi)
        {
            this.drawingRoi = drawingRoi;
        }

        public Bitmap GetDisplayImage()
        {
            if (SourceImage != null)
            {
                return CopyImage(SourceImage);
            }
            return default;
        }

        public bool GetImageSize(out int Width, out int Height)
        {
            Width = 0;
            Height = 0;
            if (SourceImage != null)
            {
                Width = SourceImage.Width;
                Height = SourceImage.Height;
                return true;
            }

            return false;
        }

        public void DisplayImage(string FilePath)
        {
            SourceImage = new Bitmap(FilePath);
            SetImageInCenter();
        }

        public void DisplayImage(Bitmap SrcImage, bool ShowInCenter = false)
        {
            if (SrcImage == null)
            {
                SourceImage.Dispose();
                SourceImage = null;
                return;
            }

            SourceImage = CopyImage(SrcImage);
            SetImageInCenter(ShowInCenter);
        }

        public void SetMovingEnable(bool enable)
        {
            if (MovingEnable != enable)
            {
                MovingEnable = enable;
            }
           
        }


        public void ClearImage()
        {
            if (SourceImage != null)
            {
                SourceImage.Dispose();
                SourceImage = null;
                Invalidate();
            }
        }

        //public ContextMenuStrip GetMenu()
        //{
        //    return cmsRightButtonMenu;
        //}

        #endregion

        private void DisplayCoordinate(PointF ImgPoint)
        {
            if (SourceImage == null) return;
            int Row = (int)Math.Floor(ImgPoint.Y);
            int Column = (int)Math.Floor(ImgPoint.X);
            if (Row >= 0 && Row <= SourceImage.Height - 1 && Column >= 0 && Column <= SourceImage.Width - 1)
            {
                Color c = SourceImage.GetPixel(Column, Row);
                tsCoordinateDisplay.Items["lbImageCoord"].Text = $"[ X {Column}, Y {Row} ]     [ R{c.R}, G{c.G}, B{c.B} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
            else
            {
                tsCoordinateDisplay.Items["lbImageCoord"].Text = $"[ X {Column}, Y {Row} ]     [ R{0} ,G{0} ,B{0} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
        }

        private void SetImageInCenter(bool ShowInCenter = false)
        {
            if (SourceImage == null) return;
            if (ImgWidth != SourceImage.Width || ImgHeight != SourceImage.Height || ShowInCenter == true)
            {
                ImgWidth = SourceImage.Width;
                ImgHeight = SourceImage.Height;

                int DisplayHeight = Height - tsCoordinateDisplay.Height;
                float RatioWidth = (float)this.Width / ImgWidth;
                float RatioHeight = (float)DisplayHeight / ImgHeight;

                if (RatioWidth < RatioHeight)
                {
                    ScaleX = RatioWidth;
                    ScaleY = ScaleX;

                    OffsetX = 0;
                    OffsetY = (DisplayHeight - ImgHeight * ScaleY) / 2;
                }
                else
                {
                    ScaleX = RatioHeight;
                    ScaleY = ScaleX;

                    OffsetX = (this.Width - ImgWidth * ScaleY) / 2;
                    OffsetY = 0;
                }
            }
            Invalidate();
        }

        private Bitmap CopyImage(Bitmap SrcImage)
        {
            Bitmap DstImage = new Bitmap(SrcImage.Width, SrcImage.Height, SrcImage.PixelFormat);
            BitmapData DstImageData = DstImage.LockBits(new Rectangle(0, 0, DstImage.Width, DstImage.Height), ImageLockMode.ReadWrite, DstImage.PixelFormat);
            BitmapData SrcImageData = SrcImage.LockBits(new Rectangle(0, 0, SrcImage.Width, SrcImage.Height), ImageLockMode.ReadWrite, SrcImage.PixelFormat);

            int length = DstImageData.Stride * DstImageData.Height;
            byte[] Imgdata = new byte[length];
            Marshal.Copy(SrcImageData.Scan0, Imgdata, 0, length);
            Marshal.Copy(Imgdata, 0, DstImageData.Scan0, length);

            SrcImage.UnlockBits(SrcImageData);
            DstImage.UnlockBits(DstImageData);
            if (SrcImage.PixelFormat == PixelFormat.Format1bppIndexed ||
                SrcImage.PixelFormat == PixelFormat.Format4bppIndexed ||
                 SrcImage.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                DstImage.Palette = SrcImage.Palette;
            }
            return DstImage;
        }


    }
}
