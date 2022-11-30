using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyDisplayWindow;
using HyRoiManager.ROI;





namespace HyRoiManager
{
    public class HyRoiController : IDrawingRoi
    {

        public RoiType? CurrentRoiType
        {
            get { return currentRoiType; }
            set
            {
                currentRoiType = value;
                if (value == RoiType.Daub || value == RoiType.Erase)
                {
                    SetRoiSelected(false);
                    //ImageViewer.Invalidate();
                }
            }
        }

        public DrawingState DrawState { get; set; } = DrawingState.Nothing;

        public bool OnlyDisplayRoi
        {
            get => onlyDisplayRoi;
            set
            {
                if (onlyDisplayRoi != value)
                {
                    onlyDisplayRoi = value;
                    HyROIs.Clear();
                    if (onlyDisplayRoi == true)
                    {
                        paintingImage?.Dispose();
                        paintingImage = null;
                        CheckRoiImage();
                    }
                    else
                    {
                        roiImage?.Dispose();
                        roiImage = null;
                        CheckPaintingImage();
                    }
                }
            }
        }

        public int RoiOpacity
        {
            get
            {
                return roiOpacity;
            }
            set
            {
                if (value > 255)
                {
                    roiOpacity = 255;
                    return;
                }
                if (value < 0)
                {
                    roiOpacity = 0;
                    return;
                }
                roiOpacity = value;
            }
        }

        public Color RoiColor { get; set; } = Color.Red;

        public int BrushSize
        {
            get { return brushSize; }
            set
            {
                if (value > 300)
                {
                    brushSize = 300;
                    return;
                }
                if (value < 1)
                {
                    brushSize = 1;
                    return;
                }
                brushSize = value;
            }
        }




        private RoiType? currentRoiType;
        private bool onlyDisplayRoi = true;
        private int roiOpacity = 100, brushSize = 30;

        public List<BaseHyROI> HyROIs = new List<BaseHyROI>();
        private HyImageViewer imageViewer;
        private PointF imageStartPoint, mouseImgPoint, specialPoint = new PointF(-111.111f, -111.111f);
        private Bitmap paintingImage, roiImage;
        private Point mouseDownPoint;
        private int nextROIIndex, maxIndex;
        private ContextMenuStrip viewerMenu;
        private SolidBrush clearBrush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));


        #region 构造函数

        public HyRoiController()
        {
            imageViewer = new HyImageViewer();
        }

        public HyRoiController(HyImageViewer ImageViewer, bool OnlyDisplayRoi = true)
        {
            this.imageViewer = ImageViewer;
            this.imageViewer.BindingRoiManager(this);
            this.OnlyDisplayRoi = OnlyDisplayRoi;
        }

        #endregion


        #region  IDrawingRoi接口实现

        public void DisplayHyRoi(Graphics Canvas)
        {
            if (OnlyDisplayRoi == false)
            {
                if (paintingImage != null)
                {
                    Canvas.DrawImage(paintingImage, 0, 0, paintingImage.Width, paintingImage.Height);
                }

                if (CurrentRoiType == RoiType.Daub || CurrentRoiType == RoiType.Erase)
                {
                    DisplayDaubShape(Canvas, mouseImgPoint);
                }
            }
            else
            {
                if (roiImage != null)
                {
                    Canvas.DrawImage(roiImage, 0, 0, roiImage.Width, roiImage.Height);
                }
            }
            foreach (BaseHyROI roi in HyROIs)
            {
                roi.Display(Canvas);
            }
        }

        public void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (OnlyDisplayRoi == true) return;
            if (e.Button == MouseButtons.Left && DrawState == DrawingState.Drawing
                && CurrentRoiType == RoiType.Polygon)
            {
                //RoiController.DrawPolygon(new PointF(0, 1), new PointF(1, 0));
                //ContextMenuStrip = cmsRightButtonMenu;
                //DrawingPolygon = false;
                //Invalidate();
                imageViewer.ContextMenuStrip = viewerMenu;
                Draw(new PointF(0, 1), new PointF(1, 0));
                imageViewer.Invalidate();
                DrawState = DrawingState.Nothing;
            }
        }

        public void OnMouseDown(MouseEventArgs e, PointF ImagePoint)
        {
            if (OnlyDisplayRoi == true) return;
            if (e.Button == MouseButtons.Left && DrawState == DrawingState.Nothing)
            {
                mouseDownPoint.X = e.X;
                mouseDownPoint.Y = e.Y;
                if (CurrentRoiType == null)
                {
                    imageViewer.SetMovingEnable(true);
                    return;
                }

                Cursor MouseCursor = GetMouseType(ImagePoint);
                imageViewer.Cursor = MouseCursor;
                imageStartPoint = ImagePoint;

                if (MouseCursor == Cursors.Default)
                {
                    DrawState = DrawingState.NewRoi;
                }
                else if (MouseCursor == Cursors.SizeAll)
                {
                    DrawState = DrawingState.Move;
                }
                else if (MouseCursor == Cursors.Cross || MouseCursor == Cursors.UpArrow)
                {
                    DrawState = DrawingState.ReDraw;
                }
            }


            if (DrawState != DrawingState.Nothing && CurrentRoiType == RoiType.Daub || CurrentRoiType == RoiType.Erase)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Draw(ImagePoint, new PointF(ImagePoint.X, ImagePoint.Y));
                    imageViewer.Invalidate();
                }
            }


            if (DrawState == DrawingState.Drawing && CurrentRoiType == RoiType.Polygon)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Draw(ImagePoint, specialPoint);
                    imageViewer.Invalidate();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Draw(specialPoint, specialPoint);
                    imageViewer.Invalidate();
                }
            }



        }

        public void OnMouseMove(MouseEventArgs e, PointF ImagePoint)
        {
            if (OnlyDisplayRoi == true) return;
            mouseImgPoint = ImagePoint;

            if (DrawState == DrawingState.Nothing && CurrentRoiType != null)
            {
                imageViewer.Cursor = GetMouseType(ImagePoint);
            }

            if (e.Button == MouseButtons.Left)
            {
                if (DrawState == DrawingState.NewRoi)
                {
                    imageViewer.SetMovingEnable(false);
                    if (CurrentRoiType == RoiType.Erase || CurrentRoiType == RoiType.Daub)
                    {
                        DrawState = DrawingState.Drawing;
                    }
                    else
                    {
                        if (Math.Abs(mouseDownPoint.X - e.X) > 10 || Math.Abs(mouseDownPoint.Y - e.Y) > 10)
                        {
                            AddNewHyROI();
                            DrawState = DrawingState.Drawing;

                            if (CurrentRoiType == RoiType.Polygon)
                            {
                                viewerMenu = imageViewer.RightClickMenu;
                                imageViewer.ContextMenuStrip = null;
                                Draw(imageStartPoint, specialPoint);
                            }
                        }
                    }

                }

                if (DrawState == DrawingState.Drawing && CurrentRoiType != RoiType.Polygon)
                {
                    imageViewer.SetMovingEnable(false);
                    Draw(imageStartPoint, ImagePoint);
                    imageViewer.Invalidate();
                }
                else if (DrawState == DrawingState.ReDraw)
                {
                    imageViewer.SetMovingEnable(false);
                    ReDraw(imageStartPoint, ImagePoint);
                    imageViewer.Invalidate();
                }
                else if (DrawState == DrawingState.Move)
                {
                    imageViewer.SetMovingEnable(false);
                    Move(imageStartPoint, ImagePoint);
                    imageViewer.Invalidate();
                }

            }

            if (DrawState == DrawingState.Drawing && CurrentRoiType == RoiType.Polygon)
            {
                Draw(specialPoint, ImagePoint);
                imageViewer.Invalidate();
            }

            if (CurrentRoiType == RoiType.Daub || CurrentRoiType == RoiType.Erase)
            {
                imageViewer.Invalidate();
            }
        }

        public void OnMouseUp(MouseEventArgs e, PointF ImagePoint)
        {
            if (OnlyDisplayRoi == true) return;
            if (e.Button == MouseButtons.Left)
            {
                if (CurrentRoiType == RoiType.Polygon && DrawState == DrawingState.Drawing)
                {
                    return;
                }

                if (DrawState == DrawingState.NewRoi && CurrentRoiType != null
                    && CurrentRoiType != RoiType.Daub && CurrentRoiType != RoiType.Erase)
                {
                    DrawState = DrawingState.Nothing;
                    BaseHyROI selectedRoi = SetSelected(ImagePoint);
                    imageViewer.Invalidate();
                }

                if (DrawState != DrawingState.Nothing)
                {
                    DrawState = DrawingState.Nothing;
                }
            }
        }

        #endregion


        private Cursor GetMouseType(PointF ImgPoint)
        {
            List<BaseHyROI> SelectedHyROIs = HyROIs.FindAll(h => h.IsSelected == true);

            foreach (BaseHyROI Roi in SelectedHyROIs)
            {
                Cursor cursor = Roi.GetMouseType(ImgPoint);
                if (cursor != Cursors.Default)
                {
                    return cursor;
                }
            }

            return Cursors.Default;
        }

        private void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (CurrentRoiType == RoiType.Daub)
            {
                if (CheckPaintingImage() == true)
                {
                    Daub(ImgStartPoint, ImgEndPoint);
                    imageStartPoint = ImgEndPoint;
                }

            }
            else if (CurrentRoiType == RoiType.Erase)
            {
                if (CheckPaintingImage() == true)
                {
                    Erase(ImgStartPoint, ImgEndPoint);
                    imageStartPoint = ImgEndPoint;
                }
            }
            else
            {
                BaseHyROI SelectedRoi = HyROIs.FirstOrDefault(roi => roi.IsSelected == true);
                SelectedRoi?.Draw(ImgStartPoint, ImgEndPoint);
            }

            //if (SelectedRoi != null)
            //{
            //    if (CurrentRoiType == RoiType.Daub)
            //    {
            //        CheckPaintingImage();
            //        Daub(ImgStartPoint, ImgEndPoint);
            //        ImageStartPoint = ImgEndPoint;
            //    }
            //    else if (CurrentRoiType == RoiType.Erase)
            //    {
            //        CheckPaintingImage();
            //        Erase(ImgStartPoint, ImgEndPoint);
            //        ImageStartPoint = ImgEndPoint;
            //    }
            //    else
            //    {
            //        SelectedRoi.Draw(ImgStartPoint, ImgEndPoint);
            //    }
            //}
        }

        private void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            BaseHyROI SelectedRoi = HyROIs.FirstOrDefault(roi => roi.IsSelected == true);

            if (SelectedRoi != null)
            {
                SelectedRoi.ReDraw(ImgStartPoint, ImgEndPoint);
            }
        }

        private void Move(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            BaseHyROI SelectedRoi = HyROIs.FirstOrDefault(roi => roi.IsSelected == true);

            if (SelectedRoi != null)
            {
                SelectedRoi.Move(ImgStartPoint, ImgEndPoint);
            }
        }




        #region 对外公有函数

        public void BindingHyImageViewer(HyImageViewer ImageViewer)
        {
            this.imageViewer = ImageViewer;
            this.imageViewer.BindingRoiManager(this);
        }

        public void DisplayRoiData(RoiData SrcRoiData, bool ClearOld = true)
        {
            if (roiImage == null || roiImage.Width != SrcRoiData.ImageWidth || roiImage.Height != SrcRoiData.ImageHeight)
            {
                roiImage = new Bitmap(SrcRoiData.ImageWidth, SrcRoiData.ImageHeight, PixelFormat.Format8bppIndexed);
                ColorPalette palette = roiImage.Palette;
                for (int i = 0; i < 256; i++)
                {
                    palette.Entries[i] = Color.FromArgb(0, i, i, i);
                }
                palette.Entries[255] = Color.FromArgb(RoiOpacity, RoiColor);
                roiImage.Palette = palette;
            }

            if (ClearOld == true)
            {
                ClearRoiImageRoiData();
            }
            DrawRoiDataToRoiImage(roiImage, SrcRoiData);
        }

        public void DisplayHyRoi(List<BaseHyROI> HyRois, bool ClearOld = true)
        {
            if (CheckRoiImage() == false) return;
            if (ClearOld == true)
            {
                this.HyROIs.Clear();
                ClearRoiImageRoiData();
                this.HyROIs = HyRois;
            }
            else
            {
                HyROIs.AddRange(HyRois);
            }

            foreach (BaseHyROI roi in HyROIs)
            {
                if (roi.RoiType == RoiType.Daub)
                {
                    DrawRoiDataToRoiImage(roiImage, (roi as HyDaub).DaubData);
                }
            }
        }



        public Bitmap GetRoiImage()
        {
            if (CheckRoiImage() == true)
            {
                return roiImage;
            }
            else
            {
                return null;
            }
        }

        public RoiData GetRoiImageRoiData()
        {
            if (CheckRoiImage() == false) return null;

            BitmapData data = roiImage.LockBits(new Rectangle(0, 0, roiImage.Width, roiImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            bool mark = false;
            int count = 0;

            //RoiData roiData = new RoiData(data.Width, data.Height);
            RoiData roiData = new RoiData();
            roiData.ImageWidth = data.Width;
            roiData.ImageHeight = data.Height;

            unsafe
            {
                byte* pointer = (byte*)data.Scan0;

                for (int h = 0; h < data.Height; h++)
                {
                    for (int w = 0; w < data.Width; w++)
                    {
                        int a = pointer[0];
                        if (a != 0)
                        {
                            count += 1;
                            if (mark == false)
                            {
                                mark = true;
                                roiData.RowIndex.Add(h);
                                roiData.StartColumn.Add(w);
                            }

                            if (w == data.Width - 1)
                            {
                                roiData.EndColumn.Add(w);
                                count = 0;
                                mark = false;
                            }
                        }
                        else
                        {
                            if (count != 0)
                            {
                                roiData.EndColumn.Add(w - 1);
                                count = 0;
                                mark = false;
                            }
                        }
                        pointer += 1;
                    }
                }
            }
            roiImage.UnlockBits(data);
            return roiData;
        }

        /// <summary>
        /// 返回ROI中，规则的ROI直接返回，不规则ROI合成一个涂抹类型返回
        /// </summary>
        /// <returns></returns>
        public List<BaseHyROI> GetRoiImageHyRoi()
        {
            List<BaseHyROI> RetRoi = new List<BaseHyROI>();

            foreach (BaseHyROI roi in HyROIs)
            {
                //if (roi.GetType().Name != typeof(HyDaub).Name)
                if (roi.RoiType != RoiType.Daub)
                {
                    RetRoi.Add(roi);
                }
            }

            HyDaub hyDaub = new HyDaub() { Index = RetRoi.Count + 1 };
            hyDaub.DaubData = GetRoiImageRoiData();
            RetRoi.Add(hyDaub);
            return RetRoi;
        }

        public void ClearRoiImageRoiData()
        {
            if (roiImage == null) return;
            BitmapData data = roiImage.LockBits(new Rectangle(0, 0, roiImage.Width, roiImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            unsafe
            {
                byte* pointer = (byte*)data.Scan0;

                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        pointer[0] = 0;
                        pointer += 1;
                    }
                }
            }

            roiImage.UnlockBits(data);

        }




        public RoiData GetPaintingImageRoiData()
        {
            if (CheckPaintingImage() == false) return null;
            BitmapData data = paintingImage.LockBits(new Rectangle(0, 0, paintingImage.Width, paintingImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            bool mark = false;
            int count = 0;

            //RoiData roiData = new RoiData(data.Width, data.Height);
            RoiData roiData = new RoiData();
            roiData.ImageWidth = data.Width;
            roiData.ImageHeight = data.Height;

            unsafe
            {
                byte* pointer = (byte*)data.Scan0;
                int offset = data.Stride - data.Width * 4;

                for (int h = 0; h < data.Height; h++)
                {
                    for (int w = 0; w < data.Width; w++)
                    {
                        int a = pointer[3];
                        if (a != 0)
                        {
                            count += 1;
                            if (mark == false)
                            {
                                mark = true;
                                roiData.RowIndex.Add(h);
                                roiData.StartColumn.Add(w);
                            }

                            if (w == data.Width - 1)
                            {
                                roiData.EndColumn.Add(w);
                                count = 0;
                                mark = false;
                            }
                        }
                        else
                        {
                            if (count != 0)
                            {
                                roiData.EndColumn.Add(w - 1);
                                count = 0;
                                mark = false;
                            }
                        }
                        pointer += 4;
                    }
                }

                pointer += offset;
            }

            paintingImage.UnlockBits(data);

            return roiData;
        }

        public void SetPaintingImageRoiData(RoiData DisplayRoiData)
        {
            if (CheckPaintingImage(true) == false)
            {

                paintingImage = new Bitmap(DisplayRoiData.ImageWidth, DisplayRoiData.ImageHeight, PixelFormat.Format32bppArgb);
            }
            DrawHyRoiToPaintingImage(paintingImage, new HyDaub(DisplayRoiData));
        }

        public void LoadPaintingImageRoiData(RoiData DisplayRoiData)
        {
            int MaxWidth, MaxHeight;
            bool sign = imageViewer.GetImageSize(out int ImgWidth, out int ImgHeight);

            if (sign == true)
            {
                 MaxWidth = ImgWidth > DisplayRoiData.ImageWidth ? ImgWidth : DisplayRoiData.ImageWidth;
                 MaxHeight = ImgHeight > DisplayRoiData.ImageHeight ? ImgHeight : DisplayRoiData.ImageHeight;
            }
            else
            {
                MaxWidth = DisplayRoiData.ImageWidth;
                MaxHeight = DisplayRoiData.ImageHeight;  
            }

            paintingImage = new Bitmap(MaxWidth, MaxHeight, PixelFormat.Format32bppArgb);
            DrawRoiDataToPaintingImage(paintingImage, DisplayRoiData);
        }

        private List<BaseHyROI> GetPaintingImageHyRoi()
        {
            BaseHyROI DaubRoi = null;
            foreach (BaseHyROI roi in HyROIs)
            {
                if (roi.IsSelected == true)
                {
                    roi.IsSelected = false;
                    DrawHyRoiToPaintingImage(paintingImage, roi);
                }

                if (roi.RoiType == RoiType.Daub)
                {
                    DaubRoi = roi;
                }
            }

            if (DaubRoi == null)
            {
                DaubRoi = new HyDaub() { Index = HyROIs.Count + 1 };
                HyROIs.Add(DaubRoi);
            }

            (DaubRoi as HyDaub).DaubData = GetPaintingImageRoiData();
            return HyROIs;
        }

        private void SetPaintingImageHyRoi(List<BaseHyROI> HyRois)
        {
            if (CheckPaintingImage(true) == false)
            {
                BaseHyROI BaseRoi = HyRois.FirstOrDefault(roi => roi.RoiType == RoiType.Daub);

                if (BaseRoi != null)
                {
                    HyDaub DaubRoi = BaseRoi as HyDaub;
                    paintingImage = new Bitmap(DaubRoi.DaubData.ImageWidth, DaubRoi.DaubData.ImageHeight, PixelFormat.Format32bppArgb);
                }
                else
                {
                    return;
                }
            }

            HyROIs.Clear();
            HyROIs = HyRois;
            foreach (BaseHyROI roi in HyROIs)
            {
                DrawHyRoiToPaintingImage(paintingImage, roi);
            }


        }

        public void SetPaintingImageRoiData(Bitmap PaintingImage, RoiData roiData)
        {
            BitmapData data = PaintingImage.LockBits(new Rectangle(0, 0, PaintingImage.Width, PaintingImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Color color = RoiColor;

            unsafe
            {
                byte* pointer = (byte*)data.Scan0;
                int offset = data.Stride - data.Width * 4;

                for (int j = 0; j < roiData.RowIndex.Count; j++)
                {
                    int result = roiData.StartColumn[j] * 4 + roiData.RowIndex[j] * (PaintingImage.Width * 4 + offset);
                    int length = roiData.EndColumn[j] - roiData.StartColumn[j] + 1;

                    for (int m = 0; m < length; m++)
                    {

                        pointer[result] = color.B;
                        pointer[result + 1] = color.G;
                        pointer[result + 2] = color.R;
                        pointer[result + 3] = (byte)RoiOpacity;
                        result += 4;
                    }
                }
            }

            PaintingImage.UnlockBits(data);
        }

        public void SetRoiSelected(bool SelectedAll = true)
        {
            foreach (BaseHyROI Roi in HyROIs)
            {
                if (SelectedAll == true)
                {
                    Roi.Visible = true;
                    Roi.IsSelected = true;
                }
                else
                {
                    Roi.IsSelected = false;
                }
            }
        }


        public void ClearImage()
        {
            if (roiImage != null)
            {
                roiImage.Dispose();
                roiImage = null;
            }
            if (paintingImage != null)
            {
                paintingImage.Dispose();
                paintingImage = null;
            }
        }

        //public void SetPaintingImageHyRoi(List<BaseHyROI> PaintingImageRoi)
        //{
        //    HyROIs = PaintingImageRoi;
        //}


        private void DrawRoiDataToPaintingImage(Bitmap PaintingImage, RoiData roiData)
        {
            BitmapData data = PaintingImage.LockBits(new Rectangle(0, 0, PaintingImage.Width, PaintingImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Color color = RoiColor;

            unsafe
            {
                byte* pointer = (byte*)data.Scan0;
                int offset = data.Stride - data.Width * 4;

                for (int j = 0; j < roiData.RowIndex.Count; j++)
                {
                    int result = roiData.StartColumn[j] * 4 + roiData.RowIndex[j] * (PaintingImage.Width * 4 + offset);
                    int length = roiData.EndColumn[j] - roiData.StartColumn[j] + 1;

                    for (int m = 0; m < length; m++)
                    {

                        pointer[result] = color.B;
                        pointer[result + 1] = color.G;
                        pointer[result + 2] = color.R;
                        pointer[result + 3] = (byte)RoiOpacity;
                        result += 4;
                    }
                }
            }

            PaintingImage.UnlockBits(data);
        }

        private void DrawRoiDataToRoiImage(Bitmap RoiImage, RoiData SrcRoiData)
        {
            BitmapData data = RoiImage.LockBits(new Rectangle(0, 0, RoiImage.Width, RoiImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            unsafe
            {
                byte* pointer = (byte*)data.Scan0;

                for (int j = 0; j < SrcRoiData.RowIndex.Count; j++)
                {
                    int result = SrcRoiData.RowIndex[j] * data.Width + SrcRoiData.StartColumn[j];
                    int length = SrcRoiData.EndColumn[j] - SrcRoiData.StartColumn[j] + 1;

                    for (int m = 0; m < length; m++)
                    {
                        pointer[result] = 255;
                        result += 1;
                    }
                }
            }
            RoiImage.UnlockBits(data);
        }

        private void DrawHyRoiToPaintingImage(Bitmap PaintingImage, BaseHyROI InputRoi, bool ClearRoi = false)
        {
            if (CheckPaintingImage() == false) return;
            Graphics Canvas = Graphics.FromImage(PaintingImage);
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(RoiOpacity, RoiColor));
            float LeftTopX, LeftTopY;

            switch (InputRoi.RoiType)
            {
                case RoiType.Circle:
                    HyCircle hyCircle = InputRoi as HyCircle;
                    LeftTopX = hyCircle.Center.X - hyCircle.Radius;
                    LeftTopY = hyCircle.Center.Y - hyCircle.Radius;
                    Canvas.CompositingMode = CompositingMode.SourceCopy;
                    Canvas.FillEllipse(clearBrush, new RectangleF(LeftTopX, LeftTopY, hyCircle.Radius * 2, hyCircle.Radius * 2));
                    if (ClearRoi == false)
                    {
                        Canvas.CompositingMode = CompositingMode.SourceOver;
                        Canvas.FillEllipse(solidBrush, new RectangleF(LeftTopX, LeftTopY, hyCircle.Radius * 2, hyCircle.Radius * 2));
                    }
                    break;

                case RoiType.Ellipse:
                    HyEllipse hyEllipse = InputRoi as HyEllipse;
                    LeftTopX = hyEllipse.Center.X - hyEllipse.Width / 2;
                    LeftTopY = hyEllipse.Center.Y - hyEllipse.Height / 2;
                    Canvas.CompositingMode = CompositingMode.SourceCopy;
                    Canvas.FillEllipse(clearBrush, new RectangleF(LeftTopX, LeftTopY, hyEllipse.Width, hyEllipse.Height));
                    if (ClearRoi == false)
                    {
                        Canvas.CompositingMode = CompositingMode.SourceOver;
                        Canvas.FillEllipse(solidBrush, new RectangleF(LeftTopX, LeftTopY, hyEllipse.Width, hyEllipse.Height));
                    }
                    break;

                case RoiType.Rectangle1:
                    HyRectangle1 Rect1 = InputRoi as HyRectangle1;
                    LeftTopX = Rect1.Center.X - Rect1.Width / 2;
                    LeftTopY = Rect1.Center.Y - Rect1.Height / 2;
                    Canvas.CompositingMode = CompositingMode.SourceCopy;
                    Canvas.FillRectangle(clearBrush, new RectangleF(LeftTopX, LeftTopY, Rect1.Width, Rect1.Height));
                    if (ClearRoi == false)
                    {
                        Canvas.CompositingMode = CompositingMode.SourceOver;
                        Canvas.FillRectangle(solidBrush, new RectangleF(LeftTopX, LeftTopY, Rect1.Width, Rect1.Height));
                    }
                    break;

                case RoiType.Polygon:
                    HyPolygon hyPolygon = InputRoi as HyPolygon;
                    Canvas.CompositingMode = CompositingMode.SourceCopy;
                    Canvas.FillPolygon(clearBrush, hyPolygon.PolygonPoints.ToArray());
                    if (ClearRoi == false)
                    {
                        Canvas.CompositingMode = CompositingMode.SourceOver;
                        Canvas.FillPolygon(solidBrush, hyPolygon.PolygonPoints.ToArray());
                    }
                    break;

                case RoiType.Daub:
                    HyDaub hyDaub = InputRoi as HyDaub;
                    DrawRoiDataToPaintingImage(PaintingImage, hyDaub.DaubData);

                    break;
            }

            solidBrush.Dispose();
            Canvas.Dispose();
        }


        #endregion

        private int CalculateHyRoiIndex()
        {
            if (HyROIs.Count != maxIndex)
            {
                nextROIIndex = 1;
                foreach (BaseHyROI roi in HyROIs)
                {
                    BaseHyROI roi1 = HyROIs.Find(h => h.Index == nextROIIndex);

                    if (roi1 != null)
                    {
                        nextROIIndex += 1;
                        continue;
                    }
                    else
                    {
                        return nextROIIndex;
                    }
                }
            }
            else
            {
                maxIndex += 1;
                nextROIIndex = maxIndex;
            }
            return nextROIIndex;
        }

        private void AddNewHyROI()
        {
            //if (drawState == DrawingState.NewRoi && CurrentRoiType != null && CurrentRoiType != RoiType.Erase)
            {
                BaseHyROI selectedRoi = HyROIs.FirstOrDefault(roi => roi.IsSelected == true);
                if (selectedRoi != null)
                {
                    selectedRoi.IsSelected = false;
                    DrawHyRoiToPaintingImage(paintingImage, selectedRoi);
                }

                SetRoiSelected(false);

                Type type = Type.GetType("HyRoiManager.ROI." + "Hy" + CurrentRoiType.ToString());
                BaseHyROI NewHyROI = Activator.CreateInstance(type) as BaseHyROI;
                NewHyROI.Index = CalculateHyRoiIndex();
                NewHyROI.IsSelected = true;
                HyROIs.Insert(NewHyROI.Index - 1, NewHyROI);
            }
        }

        private bool CheckPaintingImage(bool Reset = false)
        {
            bool sign = imageViewer.GetImageSize(out int width, out int height);

            if (sign == true)
            {
                if (Reset == false && paintingImage != null && paintingImage.Width == width && paintingImage.Height == height)
                {
                    return true;
                }
                else
                {
                    paintingImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                    return true;
                }
            }

            return false;
        }

        private bool CheckRoiImage(bool Reset = false)
        {
            bool sign = imageViewer.GetImageSize(out int width, out int height);

            if (sign == true)
            {
                if (Reset == false && roiImage != null && roiImage.Width == width && roiImage.Height == height)
                {
                    return true;
                }
                else
                {
                    roiImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                    ColorPalette palette = roiImage.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(0, i, i, i);
                    }
                    palette.Entries[255] = Color.FromArgb(RoiOpacity, RoiColor);
                    roiImage.Palette = palette;
                    return true;
                }
            }

            return false;
        }

        private void Daub(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Graphics Canvas = Graphics.FromImage(paintingImage);
            Canvas.CompositingMode = CompositingMode.SourceCopy;

            Pen pen1 = new Pen(Color.FromArgb(RoiOpacity, RoiColor));
            pen1.Width = BrushSize;
            pen1.StartCap = LineCap.Round;
            pen1.EndCap = LineCap.Round;

            Pen pen2 = new Pen(Color.FromArgb(0, 0, 0, 0));
            pen2.Width = BrushSize;
            pen2.StartCap = LineCap.Round;
            pen2.EndCap = LineCap.Round;

            Canvas.DrawLine(pen2, ImgStartPoint, ImgEndPoint);
            Canvas.CompositingMode = CompositingMode.SourceOver;
            Canvas.DrawLine(pen1, ImgStartPoint, ImgEndPoint);

            pen1.Dispose();
            pen2.Dispose();
            Canvas.Dispose();
        }

        private void Erase(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Graphics Canvas = Graphics.FromImage(paintingImage);
            Canvas.CompositingMode = CompositingMode.SourceCopy;

            Pen pen2 = new Pen(Color.FromArgb(0, 0, 0, 0));
            pen2.Width = BrushSize;
            pen2.StartCap = LineCap.Round;
            pen2.EndCap = LineCap.Round;

            Canvas.DrawLine(pen2, ImgStartPoint, ImgEndPoint);
            Canvas.CompositingMode = CompositingMode.SourceOver;

            pen2.Dispose();
            Canvas.Dispose();
        }

        private void DisplayDaubShape(Graphics Canvas, PointF CurrentImgPoint)
        {
            Pen pen1 = new Pen(Color.White, 0.5f);
            Pen pen2 = new Pen(Color.Black, 0.5f);

            float width1 = BrushSize;
            float x1 = CurrentImgPoint.X - width1 / 2f;
            float y1 = CurrentImgPoint.Y - width1 / 2f;

            float width2 = BrushSize + 1f;
            float x2 = CurrentImgPoint.X - width2 / 2f;
            float y2 = CurrentImgPoint.Y - width2 / 2f;

            Canvas.DrawEllipse(pen1, new RectangleF(x1, y1, width1, width1));
            Canvas.DrawEllipse(pen2, new RectangleF(x2, y2, width2, width2));
        }

        private BaseHyROI SetSelected(PointF ImagePoint)
        {
            BaseHyROI CurrentSelectedRoi = HyROIs.FirstOrDefault(roi => roi.IsInsideHyROI(ImagePoint) >= 0);

            if (CurrentSelectedRoi != null)
            {
                CurrentSelectedRoi.IsSelected = true;
                DrawHyRoiToPaintingImage(paintingImage, CurrentSelectedRoi, true);

                foreach (BaseHyROI roi in HyROIs)
                {
                    if (roi != CurrentSelectedRoi)
                    {
                        roi.IsSelected = false;
                        DrawHyRoiToPaintingImage(paintingImage, roi);
                    }
                }
                return CurrentSelectedRoi;
            }
            else
            {
                BaseHyROI LastTimeSelectedRoi = HyROIs.FirstOrDefault(roi => roi.IsSelected == true);

                if (LastTimeSelectedRoi != null)
                {
                    LastTimeSelectedRoi.IsSelected = false;
                    DrawHyRoiToPaintingImage(paintingImage, LastTimeSelectedRoi);
                }

                return default;
            }



            //foreach (BaseHyROI Roi in HyROIs)
            //{
            //    if (Roi.IsInsideHyROI(ImagePoint) == 0)
            //    {
            //        SetRoiSelected(false);
            //        Roi.IsSelected = true;
            //        DrawHyRoiToPaintingImage(PaintingImage, Roi, true);

            //        foreach (BaseHyROI Roi1 in HyROIs)
            //        {
            //            if (Roi1.IsSelected == false)
            //            {
            //                DrawHyRoiToPaintingImage(PaintingImage, Roi1);
            //            }
            //        }
            //        return Roi;
            //    }
            //}

            //BaseHyROI SelectedHyROI = HyROIs.Find(h => h.IsSelected == true);
            //if (SelectedHyROI != null)
            //{
            //    SelectedHyROI.IsSelected = false;
            //    DrawHyRoiToPaintingImage(PaintingImage, SelectedHyROI);
            //}

            //return SelectedHyROI;
        }



        public enum DrawingState
        {
            NewRoi = 1,
            Drawing = 2,
            ReDraw = 3,
            Move = 4,
            Nothing = 5
        }

    }


    [Serializable]
    public class RoiData
    {

        //public RoiData()
        //{
        //    //ImageWidth = 1000;
        //    //ImageHeight = 1000;
        //}

        //public RoiData(int ImageWidth, int ImageHeight)
        //{
        //    this.ImageWidth = ImageWidth;
        //    this.ImageHeight = ImageHeight;
        //}

        public string Name { get; set; }

        public int ImageWidth { get; set; } = 1000;

        public int ImageHeight { get; set; } = 1000;

        public List<int> RowIndex { get; set; } = new List<int>();

        public List<int> StartColumn { get; set; } = new List<int>();

        public List<int> EndColumn { get; set; } = new List<int>();

        public void ClearData()
        {
            RowIndex.Clear();
            StartColumn.Clear();
            EndColumn.Clear();
        }
    }
}
