using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace HyRoiManager.ROI
{

    [Serializable]
    public class HyEllipse : BaseHyROI
    {
        public HyEllipse()
        {
            
        }

        public override RoiType RoiType
        {
            get
            {
                return RoiType.Ellipse;
            }
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

        public float Angle { get; set; } = 0;




        private int ret;
        private float OriginalWidth, OriginalHeight;
        private PointF LeftTopCorner, MouseDownPoint, OriginalCenter;
        private PointF MinPoint = new PointF(1000000, 1000000);
        private PointF[] ShapePoints = new PointF[4];
        private PointF[] ArrowPoints = new PointF[4];





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
                    Canvas.FillEllipse(solidBrush, LeftTopCorner.X, LeftTopCorner.Y, Width, Height);
                }
                else
                {
                    Canvas.DrawEllipse(new Pen(Color, LineWidth), LeftTopCorner.X, LeftTopCorner.Y, Width, Height);
                }
                DisplayShape(Canvas);

              
                Canvas.TranslateTransform(Center.X, Center.Y);
                Canvas.RotateTransform(-Angle);
                Canvas.TranslateTransform(-Center.X, -Center.Y);
            }
        }



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
                    return Cursors.Cross;

                case 5:
                    //return Cursors.UpArrow;

                default:
                    return Cursors.Default;
            }
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
            CalculatePointPosition();
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
                        Width = OriginalWidth;
                        Height = Math.Abs(OriginalHeight - dy * 2);
                        CalculatePointPosition();
                        break;
                    }

                case 2:
                    {
                        Width = Math.Abs(OriginalWidth + dx * 2);
                        Height = OriginalHeight;
                        CalculatePointPosition();
                        break;
                    }

                case 3:
                    {
                        Width = OriginalWidth;
                        Height = Math.Abs(OriginalHeight + dy * 2);
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
                        float dxx = ImgEndPoint.X - Center.X;
                        float dyy = ImgEndPoint.Y - Center.Y;
                        Angle = (float)(Math.Atan2(dyy, dxx) * 180 / Math.PI);
                        break;
                    }
            }
        }

        public override int IsInsideHyROI(PointF ImgPoint)
        {
            Matrix Mtx = new Matrix();
            Mtx.RotateAt(-Angle, Center);
            PointF[] InputPoint = new PointF[1] { ImgPoint };
            Mtx.TransformPoints(InputPoint);
            Mtx.Dispose();

            RectangleF HyRoi = new RectangleF(LeftTopCorner, new SizeF(Width, Height));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(HyRoi);
            Region HyRegion = new Region(graphicsPath);
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
                return 5;
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

                //Canvas.DrawLine(ShapePen, ArrowPoints[0], ArrowPoints[1]);
                //Canvas.FillPolygon(Brushes.Yellow, new PointF[3] { ArrowPoints[1], ArrowPoints[2], ArrowPoints[3] });
            }
        }

        public override void CalculatePointPosition()
        {
            LeftTopCorner = new PointF(Center.X - Width / 2f, Center.Y - Height / 2);

            ShapePoints[0] = new PointF(Center.X - ShapeWidth / 2, Center.Y - Height / 2 - ShapeWidth / 2);
            ShapePoints[1] = new PointF(Center.X + Width / 2 - ShapeWidth / 2, Center.Y - ShapeWidth / 2);
            ShapePoints[2] = new PointF(Center.X - ShapeWidth / 2, Center.Y + Height / 2 - ShapeWidth / 2);
            ShapePoints[3] = new PointF(Center.X - Width / 2 - ShapeWidth / 2, Center.Y - ShapeWidth / 2);

            //ArrowPoints[0] = new PointF(LeftTopCorner.X + Width - 1.5f * ShapeWidth, LeftTopCorner.Y + Height / 2);
            //ArrowPoints[1] = new PointF(LeftTopCorner.X + Width + 3.5f * ShapeWidth, LeftTopCorner.Y + Height / 2);
            //ArrowPoints[2] = new PointF(ArrowPoints[1].X - ShapeWidth, ArrowPoints[1].Y - ShapeWidth / 2);
            //ArrowPoints[3] = new PointF(ArrowPoints[1].X - ShapeWidth, ArrowPoints[1].Y + ShapeWidth / 2);
        }


    }
}
