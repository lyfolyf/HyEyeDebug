//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;







//namespace HyDisplayWindow.ROI
//{

//    [Serializable]
//    public class HySector : BaseHyROI
//    {

//        public HySector()
//        {
//            //RoiType = RoiType.Sector;
//        }



//        private PointF _Center;
//        public PointF Center
//        {
//            get { return _Center; }
//            set
//            {
//                _Center = value;
//                CalculatePointPosition();
//            }
//        }

//        private float _StartAngle;
//        public float StartAngle
//        {
//            get { return _StartAngle; }
//            set
//            {
//                _StartAngle = value;

//                SweepAngle = EndAngle - StartAngle;
//                CenterAngle = StartAngle + (EndAngle - StartAngle) / 2;
//            }
//        }

//        private float _EndAngle = 1;
//        public float EndAngle
//        {
//            get { return _EndAngle; }
//            set
//            {
//                _EndAngle = value;

//                SweepAngle = _EndAngle - StartAngle;
//                CenterAngle = StartAngle + (EndAngle - StartAngle) / 2;
//            }
//        }

//        private float _Radius = 0.1f;
//        public float Radius
//        {
//            get { return _Radius; }
//            set
//            {
//                _Radius = value;
//                CalculatePointPosition();
//            }
//        }




//        private PointF LeftTopCorner, MouseDownPoint, OriginalCenter;
//        private PointF[] ShapePoints = new PointF[1];
//        private PointF[] ArrowPoints = new PointF[4];
//        private float OriginalCenterAngle, OriginalStartAngle, OriginalEndAngle, SweepAngle, CenterAngle;
//        private int ret;





//        public override void Display(Graphics Canvas)
//        {
//            if (Visible == true)
//            {
//                if (IsFill == true)
//                {
//                    SolidBrush solidBrush = new SolidBrush(Color);
//                    Canvas.FillPie(solidBrush, LeftTopCorner.X, LeftTopCorner.Y, Radius * 2,
//                        Radius * 2, StartAngle, SweepAngle);
//                }
//                else
//                {
//                    Canvas.DrawPie(new Pen(Color, LineWidth), LeftTopCorner.X, LeftTopCorner.Y,
//                      Radius * 2, Radius * 2, StartAngle, SweepAngle);
//                }

//                DisplayShape(Canvas);
//            }
//        }

//        private void DisplayShape(Graphics Canvas)
//        {
//            if (IsSelected == true)
//            {
//                float Angle;
//                Pen ShapePen = new Pen(Color.Yellow, LineWidth);

//                for (int i = 0; i < 3; i++)
//                {
//                    if (i == 0)
//                    {
//                        Angle = StartAngle;
//                    }
//                    else if (i == 1)
//                    {
//                        Angle = CenterAngle;
//                    }
//                    else
//                    {
//                        Angle = EndAngle;
//                    }

//                    Canvas.TranslateTransform(Center.X, Center.Y);
//                    Canvas.RotateTransform(Angle);
//                    Canvas.TranslateTransform(-Center.X, -Center.Y);

//                    Canvas.DrawRectangle(ShapePen, ShapePoints[0].X, ShapePoints[0].Y, ShapeWidth, ShapeWidth);
//                    if (i == 1)
//                    {
//                        Canvas.DrawLine(ShapePen, ArrowPoints[0], ArrowPoints[1]);
//                        Canvas.FillPolygon(Brushes.Yellow, new PointF[3] { ArrowPoints[1], ArrowPoints[2], ArrowPoints[3] });
//                    }

//                    Canvas.TranslateTransform(Center.X, Center.Y);
//                    Canvas.RotateTransform(-Angle);
//                    Canvas.TranslateTransform(-Center.X, -Center.Y);
//                }
//            }
//        }

//        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
//        {
//            Vector2 pt1 = new Vector2(ImgStartPoint.X, ImgStartPoint.Y);
//            Vector2 pt2 = new Vector2(ImgEndPoint.X, ImgEndPoint.Y);
//            Center = new PointF(ImgStartPoint.X, ImgStartPoint.Y);
//            Radius = Vector2.Distance(pt1, pt2);
//            if (Radius == 0)
//            {
//                Radius = 0.1f;
//            }

//            float dx = ImgEndPoint.X - Center.X;
//            float dy = ImgEndPoint.Y - Center.Y;

//            CenterAngle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
//            StartAngle = CenterAngle - 20;
//            SweepAngle = 40;
//            EndAngle = StartAngle + SweepAngle;
//            CalculatePointPosition();
//        }

//        public override Cursor GetMouseType(PointF ImgPoint)
//        {
//            int ret = IsInsideHyROI(ImgPoint);

//            if (ret == 0)
//            {
//                return Cursors.SizeAll;
//            }
//            else if (ret >= 1 && ret <= 3)
//            {
//                return Cursors.Cross;
//            }
//            else if (ret == 4)
//            {
//                return Cursors.UpArrow;
//            }

//            return Cursors.Default;
//        }

//        public override int IsInsideHyROI(PointF ImgPoint)
//        {
//            float Angle;
//            Matrix matrix = new Matrix();
//            PointF[] InputPoint;
//            RectangleF Rect = new RectangleF(ShapePoints[0].X, ShapePoints[0].Y, ShapeWidth, ShapeWidth);

//            for (int i = 0; i < 3; i++)
//            {
//                if (i == 0)
//                {
//                    Angle = StartAngle;
//                }
//                else if (i == 1)
//                {
//                    Angle = CenterAngle;
//                }
//                else
//                {
//                    Angle = EndAngle;
//                }
//                InputPoint = new PointF[1] { ImgPoint };
//                matrix.Reset();
//                matrix.RotateAt(-Angle, Center);
//                matrix.TransformPoints(InputPoint);

//                if (Rect.Contains(InputPoint[0]) == true)
//                {
//                    return i + 1;
//                }

//                if (i == 1)
//                {
//                    RectangleF rect1 = new RectangleF(ArrowPoints[2], new SizeF(ShapeWidth, ShapeWidth));
//                    if (rect1.Contains(InputPoint[0]))
//                    {
//                        return 4;
//                    }
//                }
//            }

//            GraphicsPath GraPhEllipse = new GraphicsPath(FillMode.Winding);
//            GraPhEllipse.AddPie(LeftTopCorner.X, LeftTopCorner.Y, Radius * 2, Radius * 2, StartAngle, SweepAngle);
//            if (GraPhEllipse.IsVisible(ImgPoint) == true)
//            {
//                return 0;
//            }

//            return -1;
//        }

//        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
//        {
//            if (MouseDownPoint != ImgStartPoint)
//            {
//                OriginalCenter = Center;
//                MouseDownPoint = ImgStartPoint;
//            }

//            float dx = ImgEndPoint.X - MouseDownPoint.X;
//            float dy = ImgEndPoint.Y - MouseDownPoint.Y;

//            Center = new PointF(OriginalCenter.X + dx, OriginalCenter.Y + dy);
//            CalculatePointPosition();
//        }

      
//        float OriginalSweepAngle;
//        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
//        {
//            if (MouseDownPoint != ImgStartPoint)
//            {
//                OriginalCenterAngle = CenterAngle;
//                OriginalStartAngle = StartAngle;
//                OriginalEndAngle = EndAngle;
//                OriginalSweepAngle = SweepAngle;
//                MouseDownPoint = ImgStartPoint;
//                ret = IsInsideHyROI(ImgStartPoint);
//            }

//            float dx = ImgEndPoint.X - Center.X;
//            float dy = ImgEndPoint.Y - Center.Y;
//            float CurrentAngle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);

//            switch (ret)
//            {
//                case 1:
//                    {
//                        float offsetAngle = CurrentAngle - OriginalStartAngle;
//                        StartAngle = CurrentAngle;
//                        SweepAngle = OriginalSweepAngle - offsetAngle;
//                        CenterAngle = StartAngle + SweepAngle / 2;
//                        //EndAngle = StartAngle + SweepAngle;

//                        break;
//                    }

//                case 2:
//                    {
//                        //Matrix matrix = new Matrix();
//                        //PointF[] InputPoint = new PointF[1] { ImgEndPoint };
//                        //matrix.RotateAt(CenterAngle, Center);
//                        //matrix.TransformPoints(InputPoint);


//                        Vector2 pt1 = new Vector2(Center.X, Center.Y);
//                        Vector2 pt2 = new Vector2(ImgEndPoint.X, ImgEndPoint.Y);
//                        Radius = Vector2.Distance(pt1, pt2);

//                        break;
//                    }

//                case 3:
//                    {
//                        //float CurrentAngle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
//                        EndAngle = CurrentAngle;
//                        SweepAngle = EndAngle - StartAngle;
//                        CenterAngle = StartAngle + SweepAngle / 2;

//                        break;
//                    }

//                case 4:
//                    {            
//                        CenterAngle = CurrentAngle;
//                        StartAngle = CenterAngle + OriginalSweepAngle/2;
//                        EndAngle = StartAngle - OriginalSweepAngle;
//                        break;
//                    }
//            }

//        }


//        public override void CalculatePointPosition()
//        {
//            LeftTopCorner = new PointF(Center.X - Radius, Center.Y - Radius);
//            ShapePoints[0] = new PointF(Center.X + Radius - ShapeWidth / 2, Center.Y - ShapeWidth / 2);

//            ArrowPoints[0] = new PointF(Center.X + Radius - 1.5f * ShapeWidth, Center.Y);
//            ArrowPoints[1] = new PointF(Center.X + Radius + 3.5f * ShapeWidth, Center.Y);
//            ArrowPoints[2] = new PointF(ArrowPoints[1].X - ShapeWidth, ArrowPoints[1].Y - ShapeWidth / 2);
//            ArrowPoints[3] = new PointF(ArrowPoints[1].X - ShapeWidth, ArrowPoints[1].Y + ShapeWidth / 2);
//        }

 
//    }
//}
