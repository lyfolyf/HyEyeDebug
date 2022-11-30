using HyEye.API.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HyVision.Tools.ImageDisplay
{
    public partial class HyImageDisplayControlSimple : UserControl
    {
        public HyImageDisplayControlSimple()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
            Load += HyImageDisplayControl_Load;
            InitializeEvent();
        }

        List<BaseHyROI> hyROIs = new List<BaseHyROI>();
        private PointF ImageStartPoint, ImageEndPoint, SpecialPoint = new PointF(-111.111f, -111.111f);
        private Point MouseDownPos;
        private double OffsetX, OffsetY, ScaleX = 1, ScaleY = 1;
        private bool IsDrawingROI, IsDrawingPolygon, IsMoveingROI_1, IsMoveingROI_2, IsReDrawingROI;
        private bool ShowHyROI = true;
        string coordMsg = string.Empty;
        private bool showCoorMsg = false;
        private bool autoFit = true;
        private bool allowOperation = true;
        public int SelecedtIndex
        {
            get { return selecedtIndex; }
            set
            {
                if (value < 0 || value >= hyROIs.Count)
                {
                    selecedtIndex = -1;
                }
                selecedtIndex = value;
                Invalidate();
            }
        }
        private Bitmap bpImage = null;
        private int ImgWidth, ImgHeight;
        private int selecedtIndex = -1;

        public Bitmap Image
        {
            set
            {
                DisplayImage(value);
                GC.Collect();
            }
        }

        public bool AllowOperation
        {
            get { return allowOperation; }
            set
            {
                allowOperation = value;
            }
        }
        public bool ShowCoorMsg
        {
            get { return showCoorMsg; }
            set
            {
                showCoorMsg = value;
            }
        }
        public bool AutoFit
        {
            get { return autoFit; }
            set
            {
                autoFit = value;
            }
        }

        private void HyImageDisplayControl_Load(object sender, EventArgs e)
        {

        }

        public void BinarySerialization()
        {
            List<BaseHyROI> HyROIs = new List<BaseHyROI>();
            HyRectangle2 hyRectangle = new HyRectangle2() { Angle = 10, Width = 11, Height = 12, IsSelected = true, LineWidth = 13 };
            HyCircle hyCircle = new HyCircle() { LineWidth = 15, IsSelected = true, Radius = 21 };

            HyROIs.Add(hyRectangle);
            HyROIs.Add(hyCircle);
            HyROIs.Add(new HyEllipse());
            HyROIs.Add(new HyPolygon());


            FileStream fs = new FileStream("D:\\test.xml", FileMode.Create, FileAccess.ReadWrite);
            fs.Position = 0;
            fs.Flush();

            XmlSerializer xml = new XmlSerializer(typeof(List<BaseHyROI>), new XmlRootAttribute("Test"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xml.Serialize(fs, HyROIs, ns);


            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, HyROIs);



            fs.Position = 0;
            fs.Flush();
            List<BaseHyROI> aaa = (List<BaseHyROI>)formatter.Deserialize(fs);
            List<BaseHyROI> bbb = (List<BaseHyROI>)xml.Deserialize(fs);


            fs.Close();
        }

        public void SerializeHyROI(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite);
            fs.Position = 0;
            //fs.Flush();

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, this.hyROIs);
            fs.Close();
        }

        public void DeSerializeHyROI(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite);
            fs.Position = 0;

            BinaryFormatter formatter = new BinaryFormatter();
            this.hyROIs = (List<BaseHyROI>)formatter.Deserialize(fs);
            Invalidate();

        }
        private void InitializeEvent()
        {
            this.MouseDoubleClick += HyDisplayPanel_MouseDoubleClick;
            this.MouseDown += HyDisplayPanel_MouseDown;
            this.MouseUp += HyDisplayPanel_MouseUp;
            this.MouseMove += HyDisplayPanel_MouseMove;
            this.MouseWheel += HyDisplayPanel_MouseWheel;
            this.Resize += HyImageDisplayControlSimple_Resize;
        }


        #region 鼠标事件

        //add by LuoDian @ 20210722 双击打开选择显示结果的窗体
        public new event EventHandler DoubleClick;

        private void HyDisplayPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (bpImage != null)
            {
                AutoSetImagePosition(true);
            }

            //add by LuoDian @ 20210722 双击打开选择显示结果的窗体
            DoubleClick?.Invoke(this, e);
        }

        private void HyDisplayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            IsMoveingROI_2 = false;
            MouseDownPos = MousePosition;

            float ImgPointX = (float)((this.PointToClient(MouseDownPos).X - OffsetX) / ScaleX);
            float ImgPointY = (float)((this.PointToClient(MouseDownPos).Y - OffsetY) / ScaleY);
            ImageStartPoint = new PointF(ImgPointX, ImgPointY);

            if (e.Button == MouseButtons.Left)
            {
                int ret = SetMouseStyle(ImageStartPoint);

                if (ret == 0)
                {
                    IsMoveingROI_1 = true;
                }
                else if (ret == 1 || ret == 2)
                {
                    IsReDrawingROI = true;
                }

            }

        }

        private void HyDisplayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsDrawingROI == true)
            {
                IsDrawingROI = false;
            }

            if (IsReDrawingROI == true)
            {
                IsReDrawingROI = false;
            }

            if (IsMoveingROI_1 == true)
            {
                IsMoveingROI_1 = false;
            }
        }

        private void HyDisplayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (bpImage == null) return;
            if (!allowOperation) return;
            Point CurrentMousePos = MousePosition;
            Point CurrentPanlePoint = this.PointToClient(CurrentMousePos);
            ImageEndPoint = new PointF((float)((CurrentPanlePoint.X - OffsetX) / ScaleX),
                (float)((CurrentPanlePoint.Y - OffsetY) / ScaleY));
            SetMouseStyle(ImageEndPoint);
            DisplayCoordinate();


            //移动图片
            if (e.Button == MouseButtons.Left && IsDrawingROI == false && IsReDrawingROI == false &&
                IsMoveingROI_1 == false && IsDrawingPolygon == false)
            {
                OffsetX += CurrentMousePos.X - MouseDownPos.X;
                OffsetY += CurrentMousePos.Y - MouseDownPos.Y;
                this.Invalidate();
                MouseDownPos = CurrentMousePos;
            }

        }

        private void HyDisplayPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (bpImage == null) return;
            if (!allowOperation) return;
            PointF CurrentPanlePoint = this.PointToClient(MousePosition);
            double AfterScalePx, AfterScalePy, ScaleRatio = 1.5;//缩放后的坐标和缩放倍数

            if (e.Delta > 0)
            {
                ScaleX *= ScaleRatio;
                ScaleY *= ScaleRatio;

                AfterScalePx = (CurrentPanlePoint.X - OffsetX) * ScaleRatio;
                AfterScalePy = (CurrentPanlePoint.Y - OffsetY) * ScaleRatio;
                BaseHyROI.ShapeWidth /= 1.1f;
                foreach (BaseHyROI roi in hyROIs)
                {
                    roi.CalculatePointPosition();
                }
                //hyROIRepository.SetShapeWidth(BaseHyROI.ShapeWidth / 1.1f);
            }
            else
            {
                ScaleX /= ScaleRatio;
                ScaleY /= ScaleRatio;

                AfterScalePx = (CurrentPanlePoint.X - OffsetX) / ScaleRatio;
                AfterScalePy = (CurrentPanlePoint.Y - OffsetY) / ScaleRatio;

                BaseHyROI.ShapeWidth *= 1.1f;
                foreach (BaseHyROI roi in hyROIs)
                {
                    roi.CalculatePointPosition();
                }
                //hyROIRepository.SetShapeWidth(BaseHyROI.ShapeWidth * 1.1f);
            }

            //新的偏移 = 原来偏移 + 当前点在缩放前后的偏移
            OffsetX = OffsetX + (CurrentPanlePoint.X - OffsetX) - AfterScalePx;
            OffsetY = OffsetY + (CurrentPanlePoint.Y - OffsetY) - AfterScalePy;

            DisplayCoordinate();
            this.Invalidate();
        }
        private void HyImageDisplayControlSimple_Resize(object sender, EventArgs e)
        {
            if (bpImage == null) return;
            if (autoFit)
            {
                AutoSetImagePosition(true);
            }
            DisplayCoordinate();
            this.Invalidate();
        }

        /// <summary>
        /// 在ROI区域内返回 0，在辅助区域内返回 1，在方向箭头上返回 2，都不在则返回-1
        /// </summary>
        /// <param name="ImgPoint"></param>
        /// <returns></returns>
        private int SetMouseStyle(PointF ImgPoint)
        {
            if (IsDrawingROI == true || IsReDrawingROI == true || IsDrawingPolygon == true) return -1;

            this.Cursor = Cursors.Hand;// hyROIRepository.GetMouseType(ImgPoint);

            if (this.Cursor == Cursors.SizeAll)
            {
                return 0;
            }
            else if (this.Cursor == Cursors.Cross)
            {
                return 1;
            }
            else if (this.Cursor == Cursors.UpArrow)
            {
                return 2;
            }
            return -1;
        }

        private void DisplayCoordinate()
        {
            int Row = (int)Math.Floor(ImageEndPoint.Y);
            int Column = (int)Math.Floor(ImageEndPoint.X);
            if (Row >= 0 && Row <= bpImage.Height - 1 && Column >= 0 && Column <= bpImage.Width - 1)
            {
                Color c = bpImage.GetPixel(Column, Row);
                coordMsg = $"[ X {Column}, Y {Row} ]     [ R{c.R}, G{c.G}, B{c.B} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
            else
            {
                coordMsg = $"[ X {Column}, Y {Row} ]     [ R{0} ,G{0} ,B{0} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
            this.Invalidate();
        }

        #endregion


        private void AutoSetImagePosition(bool ShowInCenter = false)
        {
            if (ImgWidth != bpImage.Width || ImgHeight != bpImage.Height || ShowInCenter == true)
            {
                ImgWidth = bpImage.Width;
                ImgHeight = bpImage.Height;

                float RatioWidth = (float)this.Width / ImgWidth;
                float RatioHeight = (float)this.Height / ImgHeight;

                if (RatioWidth < RatioHeight)//缩放图片时，缩放相同倍数图片宽优先到达控件的宽度
                {

                    ScaleX = RatioWidth;
                    ScaleY = ScaleX;

                    OffsetX = 0;
                    OffsetY = (this.Height - ImgHeight * ScaleY) / 2;

                }
                else                          //缩放图片时，缩放相同倍数图片高优先到达控件的高度
                {
                    ScaleX = RatioHeight;
                    ScaleY = ScaleX;

                    OffsetX = (this.Width - ImgWidth * ScaleY) / 2;
                    OffsetY = 0;
                }
            }
            this.Invalidate();
        }

        private void LoadImage()
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.Title = "加载当前灰阶图片";
            Openfile.Multiselect = false;
            Openfile.Filter = "图片(*.jpg;*.png;*.gif;*.bmp;*.jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";

            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                DisplayImage(Openfile.FileName);
                AutoSetImagePosition(true);
            }
        }

        private void SaveImage()
        {
            SaveFileDialog SaveImageDialog = new SaveFileDialog();
            SaveImageDialog.Title = "保存图片";
            SaveImageDialog.Filter = "XML文件(*.bmp)|*.bmp";

            if (SaveImageDialog.ShowDialog() == DialogResult.OK && bpImage != null)
            {
                bpImage.Save(SaveImageDialog.FileName);
            }
        }


        #region 对外功能接口



        public void DisplayImage(string FilePath)
        {
            bpImage = new Bitmap(FilePath);
            BaseHyROI.ShapeWidth = bpImage.Width / 100;
            AutoSetImagePosition(autoFit);
        }

        public void DisplayImage(Bitmap DispalyImage)
        {
            bpImage = /*new Bitmap(*/DispalyImage/*)*/;
            BaseHyROI.ShapeWidth = bpImage.Width / 100;
            AutoSetImagePosition(autoFit);
        }

        public Bitmap GetBitmap()
        {
            return bpImage;
        }

        public List<BaseHyROI> GetHyROIs()
        {
            return this.hyROIs;//hyROIRepository.GetHyROIs();
        }

        public void SetHyROIs(List<BaseHyROI> HyRois)
        {
            this.hyROIs = HyRois;
            // hyROIRepository.SetHyROIs(HyRois);
        }

        public BaseHyROI GetHyROI(int ROIIndex)
        {
            if (ROIIndex < 0 || ROIIndex >= hyROIs.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return this.hyROIs[ROIIndex];// hyROIRepository.GetHyROI(ROIIndex);
        }

        public void SetSelected(int ROIIndex)
        {
            SelecedtIndex = ROIIndex;
        }

        #endregion




        #region 实现接口


        private void fitWindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoSetImagePosition(true);
            this.Invalidate();
        }

        private void saveImageToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog { Filter = @"位图|*.bmp|Jpg格式|*.jpg|所有文件|*.*" };
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                switch (saveFile.FilterIndex)
                {
                    case 1:
                        bpImage.Save(saveFile.FileName, ImageFormat.Bmp);
                        break;
                    case 2:
                        bpImage.Save(saveFile.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                    default:
                        bpImage.Save(saveFile.FileName);
                        break;
                }

            }
        }

        private void saveResultImageToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog { Filter = @"位图|*.bmp|Jpg格式|*.jpg|所有文件|*.*" };
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = CreateContentBitmap2();
                switch (saveFile.FilterIndex)
                {
                    case 1:
                        bitmap.Save(saveFile.FileName, ImageFormat.Bmp);
                        break;
                    case 2:
                        bitmap.Save(saveFile.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                    default:
                        bitmap.Save(saveFile.FileName);
                        break;
                }
            }
        }



        public void ShowImage(object image, bool clearGraphic = true, bool fit = true)
        {
            bpImage = new Bitmap((Bitmap)image);
            AutoSetImagePosition(fit);
            ShowHyROI = false;
        }

        public Bitmap CreateContentBitmap()
        {
            //在原图基础上画上ROI和缺陷ROI信息后的图片          出来是彩色图

            Rectangle rect = new Rectangle(0, 0, bpImage.Width, bpImage.Height);
            BitmapData bitmapData = bpImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Bitmap OutputImage = new Bitmap(bpImage.Width, bpImage.Height, 3 * bpImage.Width, PixelFormat.Format24bppRgb, bitmapData.Scan0);

            Bitmap bp = new Bitmap(OutputImage);
            bpImage.UnlockBits(bitmapData);

            Graphics g = Graphics.FromImage(bp);
            for (int i = 0; i < hyROIs.Count; i++)
            {
                var roi = hyROIs[i];
                roi.Display(g);
            }
            //hyROIRepository.DisplayHyROI(graphics);
            return bp;
        }
        public Bitmap CreateContentBitmap2()
        {
            Bitmap bp = new Bitmap(bpImage.Width, bpImage.Height);
            using (Graphics g = Graphics.FromImage(bp))
            {
                g.DrawImage(bpImage, new PointF(0, 0));
                for (int i = 0; i < hyROIs.Count; i++)
                {
                    var roi = hyROIs[i];
                    roi.Display(g);
                }
            }
            return bp;
        }


        public void ClearImage()
        {
            bpImage = null;
        }

        public void ClearGraphic()
        {
            hyROIs = new List<BaseHyROI>();
            Invalidate();
        }

        #endregion

        public void AddROI(BaseHyROI hyROI)
        {
            hyROIs.Add(hyROI);
            Invalidate();
        }
        public void Show(BaseHyROI hyROI)
        {
            if (hyROIs.Contains(hyROI))
            {
                SelecedtIndex = hyROIs.FindIndex(k => k == hyROI);
            }
            else
            {
                hyROIs.Add(hyROI);
                SelecedtIndex = hyROIs.Count - 1;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (bpImage != null)
            {
                if (ScaleX >= 5000 || ScaleY >= 5000 || ScaleX <= 1 / 500 || ScaleY <= 1 / 500)
                {
                    ScaleX = 1;
                    ScaleY = 1;
                    OffsetX = 0;
                    OffsetY = 0;
                }
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.TranslateTransform((float)OffsetX, (float)OffsetY);
                g.ScaleTransform((float)ScaleX, (float)ScaleY);

                g.DrawImage(bpImage, 0, 0, bpImage.Width, bpImage.Height);
                if (ShowHyROI == true)
                {
                    if (selecedtIndex == -1)
                    {
                        for (int i = 0; i < hyROIs.Count; i++)
                        {
                            var roi = hyROIs[i];
                            roi.Display(g);
                        }
                    }
                    else
                    {
                        var roi = hyROIs[selecedtIndex];
                        roi.Display(g);
                    }
                    //hyROIRepository.DisplayHyROI(g);
                }
                //PointF[] pts = new PointF[1] { new Point(0, 0) };
                //Matrix matrix = new Matrix();
                //matrix.Scale(1 / (float)ScaleX, 1 / (float)ScaleY);
                //matrix.Translate(-(float)OffsetX, -(float)OffsetY);
                //matrix.TransformPoints(pts);
                g.ScaleTransform(1 / (float)ScaleX, 1 / (float)ScaleY);
                g.TranslateTransform(-(float)OffsetX, -(float)OffsetY);
                if (showCoorMsg)
                    g.DrawString(coordMsg, this.Font, Brushes.Cyan, new Point(0, 0));
            }
        }
    }
}
