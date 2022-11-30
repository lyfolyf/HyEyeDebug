using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;



namespace HyVision.Tools.ImageDisplay
{
    [Serializable]
    public class HyRectangle2 : BaseHyROI
    {
        public HyRectangle2()
        {
            RoiType = ROIType.Rectangle2;

        }

        private PointF _Center;
        public PointF Center
        {
            get { return _Center; }
            set
            {
                _Center = value;
                CalculatePointPosition();
            }
        }

        /// <summary>
        /// 是角度，不是弧度，范围-180 - 180；
        /// </summary>
        public float Angle { get; set; }

        private float _Width;
        public float Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
                CalculatePointPosition();
            }
        }

        private float _Height;
        public float Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                CalculatePointPosition();
            }
        }



        private int ret;
        private float OriginalWidth, OriginalHeight;
        private PointF LeftTopCorner, MouseDownPoint, OriginalCenter;
        private PointF MinPoint = new PointF(1000000, 1000000);
        private PointF[] ShapePoints = new PointF[8];  //记录的是小矩形左上角的点
        private PointF[] ArrowPoints = new PointF[4];

        //ShapePoints各点示意图
        /*      0------1------2
         *      |             |
         *      3             4
         *      |             |
         *      5------6------7
         */


        public override void Display(Graphics Canvas)
        {
            if (Visible == true)
            {
                Canvas.TranslateTransform(Center.X, Center.Y);
                Canvas.RotateTransform(Angle);
                Canvas.TranslateTransform(-Center.X, -Center.Y);

                if (IsFill == true)
                {
                    SolidBrush solidBrush = new SolidBrush(Color);
                    Canvas.FillRectangle(solidBrush, LeftTopCorner.X, LeftTopCorner.Y, Width, Height);
                }
                else
                {
                    Canvas.DrawRectangle(new Pen(Color, LineWidth), LeftTopCorner.X, LeftTopCorner.Y, Width, Height);
                }

                DisplayShape(Canvas);
                Canvas.TranslateTransform(Center.X, Center.Y);
                Canvas.RotateTransform(-Angle);
                Canvas.TranslateTransform(-Center.X, -Center.Y);
            }
        }

        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                OriginalWidth = Width;
                OriginalHeight = Height;
                OriginalCenter = Center;

                MouseDownPoint = ImgStartPoint;
                ret = IsInsideHyROI(MouseDownPoint);
            }

            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { MouseDownPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);
            Mtx.Dispose();
            float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            switch (ret)
            {
                case 1:
                    {
                        Width = Math.Abs(OriginalWidth - dx * 2);
                        Height = Math.Abs(OriginalHeight - dy * 2);
                        //Center = new PointF(OriginalCenter.X + dx, OriginalCenter.Y + dy);
                        CalculatePointPosition();
                        break;
                    }

                case 2:
                    {
                        Width = OriginalWidth;
                        Height = Math.Abs(OriginalHeight - dy * 2);
                        CalculatePointPosition();
                        break;
                    }

                case 3:
                    {
                        Width = Math.Abs(OriginalWidth + dx * 2);
                        Height = Math.Abs(OriginalHeight - dy * 2);
                        CalculatePointPosition();
                        break;
                    }

                case 4:
                    {
                        Width = Math.Abs(OriginalWidth - dx * 2);
                        Height = OriginalHeight;
                        CalculatePointPosition();
                        break;
                    }

                case 5:
                    {
                        Width = Math.Abs(OriginalWidth + dx * 2);
                        Height = OriginalHeight;
                        CalculatePointPosition();
                        break;
                    }

                case 6:
                    {
                        Width = Math.Abs(OriginalWidth - dx * 2);
                        Height = Math.Abs(OriginalHeight + dy * 2);
                        CalculatePointPosition();
                        break;
                    }

                case 7:
                    {
                        Width = OriginalWidth;
                        Height = Math.Abs(OriginalHeight + dy * 2);
                        CalculatePointPosition();
                        break;
                    }

                case 8:
                    {
                        Width = Math.Abs(OriginalWidth + dx * 2);
                        Height = Math.Abs(OriginalHeight + dy * 2);
                        CalculatePointPosition();
                        break;
                    }

                case 9:
                    {
                        float dxx = ImgEndPoint.X - Center.X;
                        float dyy = ImgEndPoint.Y - Center.Y;
                        Angle = (float)(Math.Atan2(dyy, dxx) * 180 / Math.PI);
                        break;
                    }
            }
        }

        #region 原始代码，可以删
        private void Stretching(int ShapePointIndex, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[1] { ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);


            float dx = DtnPoint[0].X - (ShapePoints[ShapePointIndex].X + ShapeWidth / 2);
            float dy = DtnPoint[0].Y - (ShapePoints[ShapePointIndex].Y + ShapeWidth / 2);


            Width += dx * 2;
            Height -= dy * 2;


            if (Width < 0)
            {
                Width = Math.Abs(Width);
            }
            if (Height < 0)
            {
                Height = Math.Abs(Height);
            }

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching1(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            //VirtualWidth -= dx * 2;


            //Width = Math.Abs(VirtualWidth);

            Width = OriginalWidth - dx * 2;
            Width = Math.Abs(Width);

            Height = Math.Abs(OriginalHeight - dy * 2);

            //Width -= dx * 2;
            //Height -= dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();


        }

        private void Stretching2(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            //float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            //Width -= dx * 2;
            Height -= dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching3(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            Width += dx * 2;
            Height -= dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching4(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            float dx = DtnPoint[1].X - DtnPoint[0].X;
            //float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            Width -= dx * 2;
            //Height -= dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching5(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            float dx = DtnPoint[1].X - DtnPoint[0].X;
            //float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            Width += dx * 2;
            //Height -= dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching6(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            Width -= dx * 2;
            Height += dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching7(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            //float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            //Width -= dx * 2;
            Height += dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        private void Stretching8(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] DtnPoint = new PointF[2] { ImgStartPoint, ImgEndPoint };
            Mtx.TransformPoints(DtnPoint);

            float dx = DtnPoint[1].X - DtnPoint[0].X;
            float dy = DtnPoint[1].Y - DtnPoint[0].Y;

            Width += dx * 2;
            Height += dy * 2;

            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        #endregion
        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            float StartPx = ImgStartPoint.X, StartPy = ImgStartPoint.Y;
            float EndPx = ImgEndPoint.X, EndPy = ImgEndPoint.Y;

            if (ImgEndPoint.X <= ImgStartPoint.X)
            {
                StartPx = ImgEndPoint.X;
                EndPx = StartPx + 0.1f;

                if (MinPoint.X > ImgEndPoint.X)
                {
                    MinPoint.X = ImgEndPoint.X;
                }
            }
            if (ImgEndPoint.Y <= ImgStartPoint.Y)
            {
                StartPy = ImgEndPoint.Y;
                EndPy = StartPy + 0.1f;

                if (MinPoint.Y > ImgEndPoint.Y)
                {
                    MinPoint.Y = ImgEndPoint.Y;
                }
            }

            if (StartPx > MinPoint.X)
            {
                StartPx = MinPoint.X;
            }
            if (StartPy > MinPoint.Y)
            {
                StartPy = MinPoint.Y;
            }

            Width = EndPx - StartPx;
            Height = EndPy - StartPy;
            Center = new PointF(StartPx + Width / 2f, StartPy + Height / 2f);
            CalculatePointPosition();
        }

        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                OriginalCenter = Center;
                MouseDownPoint = ImgStartPoint;
            }

            float dx = ImgEndPoint.X - MouseDownPoint.X;
            float dy = ImgEndPoint.Y - MouseDownPoint.Y;

            Center = new PointF(OriginalCenter.X + dx, OriginalCenter.Y + dy);
            //LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);
            CalculatePointPosition();
        }

        public override Cursor GetMouseType(PointF ImgPoint)
        {
            int ret = IsInsideHyROI(ImgPoint);

            switch (ret)
            {
                case 0:
                    return Cursors.SizeAll;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return Cursors.Cross;

                case 9:
                    return Cursors.UpArrow;

                default:
                    return Cursors.Default;
            }



        }

        /// <summary>
        /// 判断输入点是否在ROI上。 -1 ：不在ROI上，  0 ：在ROI上，  1-8：在对应编号的小矩形上，  9：在箭头上
        /// </summary>
        /// <param name="ImgPoint">要检测的图像点</param>
        /// <returns></returns>
        public override int IsInsideHyROI(PointF ImgPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] InputPoint = new PointF[1] { ImgPoint };
            Mtx.TransformPoints(InputPoint);
            Mtx.Dispose();

            RectangleF HyRoi = new RectangleF(LeftTopCorner, new SizeF(Width, Height));
            Region HyRegion = new Region(HyRoi);
            Region ComplementRegion = new Region();
            ComplementRegion.MakeEmpty();

            for (int i = 0; i < ShapePoints.Length; i++)
            {
                RectangleF ShapeRect = new RectangleF(ShapePoints[i], new SizeF(ShapeWidth, ShapeWidth));
                ComplementRegion.Union(ShapeRect);

                if (ShapeRect.Contains(InputPoint[0]) == true)
                {
                    return i + 1;
                }
            }

            RectangleF rect = new RectangleF(ArrowPoints[2], new SizeF(ShapeWidth, ShapeWidth));
            if (rect.Contains(InputPoint[0]))
            {
                return 9;
            }

            ComplementRegion.Complement(HyRegion);
            if (ComplementRegion.IsVisible(InputPoint[0]))
            {
                return 0;
            }

            return -1;
        }

        private void DisplayShape(Graphics Canvas)
        {
            if (IsSelected == true)
            {
                Pen ShapePen = new Pen(Color.Yellow, LineWidth);

                foreach (PointF pt in ShapePoints)
                {
                    Canvas.DrawRectangle(ShapePen, pt.X, pt.Y, ShapeWidth, ShapeWidth);
                }

                Canvas.DrawLine(ShapePen, ArrowPoints[0], ArrowPoints[1]);
                Canvas.FillPolygon(Brushes.Yellow, new PointF[3] { ArrowPoints[1], ArrowPoints[2], ArrowPoints[3] });
            }
        }

        public override void CalculatePointPosition()
        {
            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);

            ShapePoints[0] = new PointF(LeftTopCorner.X - ShapeWidth / 2, LeftTopCorner.Y - ShapeWidth / 2);
            ShapePoints[1] = new PointF(LeftTopCorner.X + Width / 2 - ShapeWidth / 2, LeftTopCorner.Y - ShapeWidth / 2);
            ShapePoints[2] = new PointF(LeftTopCorner.X + Width - ShapeWidth / 2, LeftTopCorner.Y - ShapeWidth / 2);
            ShapePoints[3] = new PointF(LeftTopCorner.X - ShapeWidth / 2, LeftTopCorner.Y + Height / 2 - ShapeWidth / 2);
            ShapePoints[4] = new PointF(LeftTopCorner.X + Width - ShapeWidth / 2, LeftTopCorner.Y + Height / 2 - ShapeWidth / 2);
            ShapePoints[5] = new PointF(LeftTopCorner.X - ShapeWidth / 2, LeftTopCorner.Y + Height - ShapeWidth / 2);
            ShapePoints[6] = new PointF(LeftTopCorner.X + Width / 2 - ShapeWidth / 2, LeftTopCorner.Y + Height - ShapeWidth / 2);
            ShapePoints[7] = new PointF(LeftTopCorner.X + Width - ShapeWidth / 2, LeftTopCorner.Y + Height - ShapeWidth / 2);

            ArrowPoints[0] = new PointF(LeftTopCorner.X + Width - 1.5f * ShapeWidth, LeftTopCorner.Y + Height / 2);
            ArrowPoints[1] = new PointF(LeftTopCorner.X + Width + 3.5f * ShapeWidth, LeftTopCorner.Y + Height / 2);
            ArrowPoints[2] = new PointF(ArrowPoints[1].X - ShapeWidth, ArrowPoints[1].Y - ShapeWidth / 2);
            ArrowPoints[3] = new PointF(ArrowPoints[1].X - ShapeWidth, ArrowPoints[1].Y + ShapeWidth / 2);
        }


    }
}
