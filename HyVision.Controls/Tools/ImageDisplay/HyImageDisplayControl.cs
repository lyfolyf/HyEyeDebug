using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using VisionSDK;
using HyEye.API.Repository;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace HyVision.Tools.ImageDisplay
{
    public partial class HyImageDisplayControl : UserControl, IDisplayImageComponent
    {
        //add by LuoDian @ 20210804 用于ROI操作事件
        public UpdateROI AddROIEvent;
        public UpdateROI DeleteROIEvent;
        //add by LuoDian @ 20210722 打开选择显示结果的窗体
        public event EventHandler ShowSelectOutput;


        public HyImageDisplayControl()
        {
            InitializeComponent();

            InitializeEvent();
            InitializeContextMenuStrip();
            InitializeHyRoiSets();
        }


        private static bool _showEditROIForm = false;
        public bool ShowEditROIForm
        {
            get { return _showEditROIForm; }
            set
            {
                _showEditROIForm = value;
                if (_showEditROIForm == false)
                {
                    ShowFrmEditHyROI(_showEditROIForm);
                }
            }
        }

        private bool _topToolVisible = true;
        public bool TopToolVisible
        {
            get
            {
                return _topToolVisible;
            }
            set
            {
                _topToolVisible = value;
                tsTopTool.Visible = _topToolVisible;
            }
        }

        private bool _bottomToolVisible = true;
        public bool BottomToolVisible
        {
            get
            {
                return _bottomToolVisible;
            }
            set
            {
                _bottomToolVisible = value;
                tsBottomTool.Visible = _bottomToolVisible;
            }
        }


        public bool EditRoiEnable { get; set; } = true;




        private HyROIRepository hyROIRepository = new HyROIRepository();
        private static FrmEditHyROI frmEditHyROI = new FrmEditHyROI();
        private PointF ImageStartPoint, ImageEndPoint, SpecialPoint = new PointF(-111.111f, -111.111f);
        private Point MouseDownPos;
        private double OffsetX, OffsetY, ScaleX = 1, ScaleY = 1;
        private bool IsDrawingROI, IsDrawingPolygon, IsMoveingROI_1, IsMoveingROI_2, IsReDrawingROI;
        private bool ShowHyROI = true;




        private void HyImageDisplayControl_Load(object sender, EventArgs e)
        {
            //InitializeEvent();
            //InitializeContextMenuStrip();
            //InitializeHyRoiSets();

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
            List<BaseHyROI> HyROIs = hyROIRepository.GetHyROIs();

            FileStream fs = new FileStream("D:\\test.bin", FileMode.Create, FileAccess.ReadWrite);
            fs.Position = 0;
            //fs.Flush();

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, HyROIs);
            fs.Close();
        }

        public void DeSerializeHyROI(string Path)
        {
            FileStream fs = new FileStream("D:\\test.bin", FileMode.Open, FileAccess.ReadWrite);
            fs.Position = 0;

            BinaryFormatter formatter = new BinaryFormatter();
            List<BaseHyROI> Rois = (List<BaseHyROI>)formatter.Deserialize(fs);

            hyROIRepository.SetHyROIs(Rois);

        }

        private void Test1()
        {
            Rectangle rect = new Rectangle(0, 0, bpImage.Width, bpImage.Height);
            BitmapData bitmapData = bpImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Bitmap OutputImage = new Bitmap(bpImage.Width, bpImage.Height, 3 * bpImage.Width, PixelFormat.Format24bppRgb, bitmapData.Scan0);

            Bitmap bb = new Bitmap(OutputImage);
            bpImage.UnlockBits(bitmapData);
            Graphics graphics = Graphics.FromImage(bb);

            graphics.DrawRectangle(new Pen(Color.Red, 0.5f), 0, 0, 150, 100);
            //bb.Save(@"C:\Users\29092\Desktop\Test Images\123456789.bmp");
            DisplayImage(bb);

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

        private void InitializeHyRoiSets()
        {
            foreach (var item in typeof(Color).GetMembers())
            {
                if (item.MemberType == System.Reflection.MemberTypes.Property &&
                    Color.FromName(item.Name).IsKnownColor == true)
                {
                    tscbROIColor.Items.Add(item.Name);
                }
            }
            tscbROIColor.SelectedItem = ColorTranslator.ToHtml(Color.Red);



            tscbRoiType.Items.Clear();
            foreach (string Name in Enum.GetNames(typeof(RoiType)))
            {
                if (Name.Contains("Defect"))
                {
                    continue;
                }
                tscbRoiType.Items.Add(Name);
            }
            tscbRoiType.SelectedIndex = 0;
        }

        private void HyDisplayPanel_Paint(object sender, PaintEventArgs e)
        {
            if (bpImage != null)
            {
                Graphics Canvas = e.Graphics;
                Canvas.SmoothingMode = SmoothingMode.None;
                Canvas.InterpolationMode = InterpolationMode.NearestNeighbor;
                Canvas.PixelOffsetMode = PixelOffsetMode.Half;
                Canvas.TranslateTransform((float)OffsetX, (float)OffsetY);
                Canvas.ScaleTransform((float)ScaleX, (float)ScaleY);

                Canvas.DrawImage(bpImage, 0, 0, bpImage.Width, bpImage.Height);
                if (ShowHyROI == true)
                {
                    hyROIRepository.DisplayHyROI(Canvas);
                    if (InputDefect != null)
                    {
                        InputDefect.Display(Canvas);
                    }

                    foreach (BaseHyROI roi in InputDefects)
                    {
                        roi.Display(Canvas);
                    }
                }
            }
        }


        private void ShowFrmEditHyROI(bool Open = true)
        {
            if (Open == true)
            {
                if (ShowEditROIForm == true)
                {
                    if (frmEditHyROI.Visible == false)
                    {
                        frmEditHyROI.Show();
                        frmEditHyROI.TopMost = true;
                        frmEditHyROI.Location = new Point(Screen.GetWorkingArea(this).Width - frmEditHyROI.Width, 0);
                    }

                    frmEditHyROI.UpDateWindow(HyDisplayPanel, hyROIRepository);
                }
            }
            else
            {
                frmEditHyROI.Hide();
            }
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

            float ImgPointX = (float)((HyDisplayPanel.PointToClient(MouseDownPos).X - OffsetX) / ScaleX);
            float ImgPointY = (float)((HyDisplayPanel.PointToClient(MouseDownPos).Y - OffsetY) / ScaleY);
            ImageStartPoint = new PointF(ImgPointX, ImgPointY);

            if (e.Button == MouseButtons.Left)
            {
                int ret = SetMouseStyle(ImageStartPoint);

                if (EditRoiEnable == false)
                {
                    ret = -1;
                }

                if (ret == 0)
                {
                    IsMoveingROI_1 = true;
                }
                else if (ret == 1 || ret == 2)
                {
                    IsReDrawingROI = true;
                }

                if (IsDrawingROI == true)
                {
                    IsMoveingROI_1 = false;
                    IsReDrawingROI = false;

                    hyROIRepository.SetSelected(false);
                    RoiType RoiTpye = (RoiType)Enum.Parse(typeof(RoiType), tscbRoiType.Text);
                    hyROIRepository.AddNewHyROI(RoiTpye, ColorTranslator.FromHtml(tscbROIColor.Text));

                    ShowFrmEditHyROI(ShowEditROIForm);

                    //add by LuoDian @ 20210804 用于ROI操作事件
                    AddROIEvent?.Invoke(hyROIRepository.GetHyROIs().Count);

                    if (RoiTpye == RoiType.Polygon || RoiTpye == RoiType.Points)
                    {
                        IsDrawingPolygon = true;
                        IsDrawingROI = false;
                        HyDisplayPanel.ContextMenuStrip = null;
                    }
                }

                if (IsDrawingPolygon == true)
                {
                    hyROIRepository.Draw(ImageStartPoint, SpecialPoint);
                    ShowFrmEditHyROI(ShowEditROIForm);
                    HyDisplayPanel.Invalidate();
                }
            }
        }

        private void HyDisplayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsDrawingROI == false && IsReDrawingROI == false && IsMoveingROI_2 == false &&
                IsDrawingPolygon == false && e.Button != MouseButtons.Right)
            {
                hyROIRepository.SetSelected(ImageStartPoint);
                HyDisplayPanel.Invalidate();

                if (hyROIRepository.GetCurrentSelectedHyROI() != null)
                {
                    ShowFrmEditHyROI(ShowEditROIForm);
                }
            }


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

            if (e.Button == MouseButtons.Right && IsDrawingPolygon == true)
            {
                IsDrawingPolygon = false;
                hyROIRepository.Draw(new PointF(0, 1), new PointF(1, 0));
                ShowFrmEditHyROI(ShowEditROIForm);
                HyDisplayPanel.Invalidate();
                HyDisplayPanel.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void HyDisplayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (bpImage == null) return;

            Point CurrentMousePos = MousePosition;
            Point CurrentPanlePoint = HyDisplayPanel.PointToClient(CurrentMousePos);
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
                HyDisplayPanel.Invalidate();
                MouseDownPos = CurrentMousePos;
            }

            if (e.Button == MouseButtons.Left && EditRoiEnable == true)
            {
                if (IsDrawingROI == true)
                {
                    hyROIRepository.Draw(ImageStartPoint, ImageEndPoint);
                    HyDisplayPanel.Invalidate();
                    ShowFrmEditHyROI(ShowEditROIForm);
                }

                if (IsReDrawingROI == true)
                {
                    hyROIRepository.ReDraw(ImageStartPoint, ImageEndPoint);
                    HyDisplayPanel.Invalidate();
                    ShowFrmEditHyROI(ShowEditROIForm);
                }

                if (IsMoveingROI_1 == true)
                {
                    IsMoveingROI_2 = true;
                    hyROIRepository.MoveSelectedROI(ImageStartPoint, ImageEndPoint);
                    HyDisplayPanel.Invalidate();
                    ShowFrmEditHyROI(ShowEditROIForm);
                }
            }


            if (IsDrawingPolygon == true && EditRoiEnable == true)
            {
                hyROIRepository.Draw(SpecialPoint, ImageEndPoint);
                HyDisplayPanel.Invalidate();
            }

        }

        private void HyDisplayPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (bpImage == null) return;
            PointF CurrentPanlePoint = HyDisplayPanel.PointToClient(MousePosition);
            double AfterScalePx, AfterScalePy, ScaleRatio = 1.5;//缩放后的坐标和缩放倍数

            if (e.Delta > 0)
            {
                ScaleX *= ScaleRatio;
                ScaleY *= ScaleRatio;

                AfterScalePx = (CurrentPanlePoint.X - OffsetX) * ScaleRatio;
                AfterScalePy = (CurrentPanlePoint.Y - OffsetY) * ScaleRatio;

                hyROIRepository.SetShapeWidth(BaseHyROI.ShapeWidth / 1.1f);
            }
            else
            {
                ScaleX /= ScaleRatio;
                ScaleY /= ScaleRatio;

                AfterScalePx = (CurrentPanlePoint.X - OffsetX) / ScaleRatio;
                AfterScalePy = (CurrentPanlePoint.Y - OffsetY) / ScaleRatio;

                hyROIRepository.SetShapeWidth(BaseHyROI.ShapeWidth * 1.1f);
            }

            //新的偏移 = 原来偏移 + 当前点在缩放前后的偏移
            OffsetX = OffsetX + (CurrentPanlePoint.X - OffsetX) - AfterScalePx;
            OffsetY = OffsetY + (CurrentPanlePoint.Y - OffsetY) - AfterScalePy;

            HyDisplayPanel.Invalidate();
        }


        /// <summary>
        /// 在ROI区域内返回 0，在辅助区域内返回 1，在方向箭头上返回 2，都不在则返回-1
        /// </summary>
        /// <param name="ImgPoint"></param>
        /// <returns></returns>
        private int SetMouseStyle(PointF ImgPoint)
        {
            if (IsDrawingROI == true || IsReDrawingROI == true || IsDrawingPolygon == true) return -1;

            HyDisplayPanel.Cursor = hyROIRepository.GetMouseType(ImgPoint);

            if (HyDisplayPanel.Cursor == Cursors.SizeAll)
            {
                return 0;
            }
            else if (HyDisplayPanel.Cursor == Cursors.Cross)
            {
                return 1;
            }
            else if (HyDisplayPanel.Cursor == Cursors.UpArrow)
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
                toolStripLabel1.Text = $"[ X {Column}, Y {Row} ]     [ R{c.R}, G{c.G}, B{c.B} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
            else
            {
                toolStripLabel1.Text = $"[ X {Column}, Y {Row} ]     [ R{0} ,G{0} ,B{0} ]     [ZOOM : {(Math.Round(ScaleX * 100))}%]";
            }
        }

        #endregion



        #region 右键菜单事件
        private void tsmiImportImg_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void tsmiExportImg_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        public Bitmap DeepClone(Bitmap bmp)
        {
            PixelFormat pixelFormat = bmp.PixelFormat;

            Bitmap copy = new Bitmap(bmp.Width, bmp.Height, pixelFormat);
            if (pixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette cp = copy.Palette;
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                copy.Palette = cp;
            }

            BitmapData bmpData = null;
            BitmapData copyData = null;
            try
            {
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, pixelFormat);
                copyData = copy.LockBits(new Rectangle(0, 0, copy.Width, copy.Height), ImageLockMode.ReadWrite, pixelFormat);

                Kernel32Api.CopyMemory(copyData.Scan0, bmpData.Scan0, (uint)(bmpData.Stride * bmp.Height));
            }
            finally
            {
                bmp.UnlockBits(bmpData);
                copy.UnlockBits(copyData);
            }

            return copy;
        }

        public static class Kernel32Api
        {
            [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
            public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        }

        private void tsmiDisplayOriImg_Click(object sender, EventArgs e)
        {
            if (bpImage != null)
            {
                AutoSetImagePosition(true);
            }
        }

        private void tsmiDrawCircle_Click(object sender, EventArgs e)
        {
            IsDrawingROI = true;

        }

        private void tsmiDrawRectangle_Click(object sender, EventArgs e)
        {


        }

        private void tsmiDeleteROI_Click(object sender, EventArgs e)
        {
            //add by LuoDian @ 20210804 删除ROI的时候，先获取删除的是哪些ROI
            BaseHyROI[] arrDeleteROI = hyROIRepository.GetAllSelectedHyROI();

            hyROIRepository.DeleteHyROI();
            HyDisplayPanel.Invalidate();
            ShowFrmEditHyROI(ShowEditROIForm);

            //add by LuoDian @ 20210804 删除ROI的时候，同步把UI中的可选的ROI列表中删除对应ROI
            if (arrDeleteROI != null && arrDeleteROI.Length > 0)
            {
                foreach (BaseHyROI roi in arrDeleteROI)
                {
                    DeleteROIEvent?.Invoke(roi.Index);
                }
            }


        }

        #endregion


        #region 工具栏事件

        private void tsbClearImage_Click(object sender, EventArgs e)
        {
            bpImage = null;
            HyDisplayPanel.Invalidate();
        }

        private void tsbLoadImage_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void tsbSaveImage_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            Point PanleCentre = new Point(HyDisplayPanel.Width / 2, HyDisplayPanel.Height / 2);
            ScaleX *= 1.5;
            ScaleY *= 1.5;

            OffsetX = OffsetX + (PanleCentre.X - OffsetX) - (PanleCentre.X - OffsetX) * 1.5;
            OffsetY = OffsetY + (PanleCentre.Y - OffsetY) - (PanleCentre.Y - OffsetY) * 1.5;
            HyDisplayPanel.Invalidate();
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            Point PanleCentre = new Point(HyDisplayPanel.Width / 2, HyDisplayPanel.Height / 2);
            ScaleX /= 1.5;
            ScaleY /= 1.5;

            OffsetX = OffsetX + (PanleCentre.X - OffsetX) - (PanleCentre.X - OffsetX) / 1.5;
            OffsetY = OffsetY + (PanleCentre.Y - OffsetY) - (PanleCentre.Y - OffsetY) / 1.5;
            HyDisplayPanel.Invalidate();
        }

        private void tsbNewHyROI_Click(object sender, EventArgs e)
        {
            if (EditRoiEnable == true)
            {
                IsDrawingROI = true;
            }
        }

        #endregion


        private void AutoSetImagePosition(bool ShowInCenter = false)
        {
            if (ImgWidth != bpImage.Width || ImgHeight != bpImage.Height || ShowInCenter == true)
            {
                ImgWidth = bpImage.Width;
                ImgHeight = bpImage.Height;

                float RatioWidth = (float)HyDisplayPanel.Width / ImgWidth;
                float RatioHeight = (float)HyDisplayPanel.Height / ImgHeight;

                if (RatioWidth < RatioHeight)//缩放图片时，缩放相同倍数图片宽优先到达控件的宽度
                {

                    ScaleX = RatioWidth;
                    ScaleY = ScaleX;

                    OffsetX = 0;
                    OffsetY = (HyDisplayPanel.Height - ImgHeight * ScaleY) / 2;

                }
                else                          //缩放图片时，缩放相同倍数图片高优先到达控件的高度
                {
                    ScaleX = RatioHeight;
                    ScaleY = ScaleX;

                    OffsetX = (HyDisplayPanel.Width - ImgWidth * ScaleY) / 2;
                    OffsetY = 0;
                }
            }
            HyDisplayPanel.Invalidate();
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
                bpImage.Save(SaveImageDialog.FileName, ImageFormat.Bmp);
            }
        }


        #region 对外功能接口


        private Bitmap bpImage = null;
        private int ImgWidth, ImgHeight;



        public void DisplayImage(string FilePath)
        {
            bpImage = new Bitmap(FilePath);
            BaseHyROI.ShapeWidth = bpImage.Width / 100;
            AutoSetImagePosition();
        }

        public void DisplayImage(Bitmap DispalyImage)
        {
            bpImage = DeepClone(DispalyImage);
            BaseHyROI.ShapeWidth = bpImage.Width / 100;
            AutoSetImagePosition();
        }

        public Bitmap GetBitmap()
        {
            return bpImage;
        }

        public List<BaseHyROI> GetHyROIs()
        {
            return hyROIRepository.GetHyROIs();
        }

        public void SetHyROIs(List<BaseHyROI> HyRois)
        {
            hyROIRepository.SetHyROIs(HyRois);
        }

        public BaseHyROI GetHyROI(int ROIIndex)
        {
            return hyROIRepository.GetHyROI(ROIIndex);
        }

        public void SetSelected(int ROIIndex)
        {
            hyROIRepository.SetSelected(ROIIndex);
        }

        public void SetBackColor(Color color)
        {
            HyDisplayPanel.BackColor = color;
        }

        public HyROIRepository GetHyROIRepository()
        {
            return hyROIRepository;
        }

        public void DisplayControlInvalidate()
        {
            HyDisplayPanel.Invalidate();
        }

        //add by LuoDian @ 20210805 右键打开选择显示结果的窗体
        private void tsmBandingShowContent_Click(object sender, EventArgs e)
        {
            //add by LuoDian @ 20210722 打开选择显示结果的窗体
            ShowSelectOutput?.Invoke(this, e);
        }

        #endregion




        #region 实现接口

        private BaseHyROI InputDefect = null;
        private List<BaseHyROI> InputDefects = new List<BaseHyROI>();

        public Control DisplayedControl
        {
            get
            {
                return this;
            }
        }

        public void ShowImage(object image, bool clearGraphic = true, bool fit = true)
        {

            bpImage = new Bitmap((Bitmap)image);
            AutoSetImagePosition(fit);
            ShowHyROI = !clearGraphic;
        }

        public void ShowGraphic(object graphicCollection)
        {
            try
            {
                if (graphicCollection.GetType() == typeof(List<HyDefectXLD>))
                {
                    List<HyDefectXLD> lstROI = (List<HyDefectXLD>)graphicCollection;
                    ShowGraphic(lstROI);
                }
                else
                {
                    ShowHyROI = true;
                    InputDefect = (BaseHyROI)graphicCollection;
                    HyDisplayPanel.Invalidate();
                }
            }
            catch { }

        }

        public void ShowGraphic(List<HyDefectXLD> HyDefects)
        {
            try
            {
                ShowHyROI = true;
                if (HyDefects != null && HyDefects.Count > 0)
                {
                    InputDefects.AddRange(HyDefects);
                }

                HyDisplayPanel.Invalidate();
            }
            catch { }

        }

        public Bitmap CreateContentBitmap()
        {
            //在原图基础上画上ROI和缺陷ROI信息后的图片          出来是彩色图

            Rectangle rect = new Rectangle(0, 0, bpImage.Width, bpImage.Height);
            BitmapData bitmapData = bpImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Bitmap OutputImage = new Bitmap(bpImage.Width, bpImage.Height, 3 * bpImage.Width, PixelFormat.Format24bppRgb, bitmapData.Scan0);

            Bitmap bp = new Bitmap(OutputImage);
            bpImage.UnlockBits(bitmapData);

            Graphics graphics = Graphics.FromImage(bp);
            if (InputDefect != null)
            {
                InputDefect.Display(graphics);
            }

            return bp;
        }

        public void ShowGraphicAsync(object graphicCollection)
        {
            //异步显示   开辟一个线程显示Graphic信息

            try
            {
                ShowHyROI = true;
                InputDefect = (BaseHyROI)graphicCollection;
            }
            catch { }
        }

        public void ClearImage()
        {
            bpImage = null;
        }

        public void ClearGraphic()
        {
            InputDefect = null;
            InputDefects.Clear();
        }

        #endregion




    }


    //add by LuoDian @ 20210722 显示结果图像
    public class OutputDisplayTaskImageComponent : HyImageDisplayControl, IDisplayTaskImageComponent
    {
        public string TaskName { get; set; }

        public string AcqOrCalibName { get; set; }

        private List<string> lstGraphicNames;
        public List<string> GraphicNames
        {
            get
            {
                if (lstGraphicNames == null)
                    lstGraphicNames = new List<string>();
                return lstGraphicNames;
            }
            set
            {
                lstGraphicNames = value;
            }
        }

        public int Index;

        public bool IsBinding { get; set; }

        public event EventHandler ClearAllImage;
    }
}
