//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace HyDisplayWindow.ROI
//{
//    public class HyPoints : BaseHyROI
//    {

//        public HyPoints()
//        {
//            //RoiType = RoiType.Points;
//        }

//        public List<PointF> RoiPoints { get; set; } = new List<PointF>();

//        private PointF[] OriginalRoiPoints;
//        private PointF MouseDownPoint, SpecialPoint = new PointF(-111.111f, -111.111f);
//        int ret;





//        public override void Display(Graphics Canvas)
//        {
//            SolidBrush solidBrush1 = new SolidBrush(Color);
//            foreach (PointF pt in RoiPoints)
//            {
//                RectangleF rect = new RectangleF(pt.X, pt.Y, 1, 1);

//                Canvas.FillRectangle(solidBrush1, rect);
//            }

//            DisplayShape(Canvas);
//        }

//        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
//        {
//            if (ImgStartPoint == SpecialPoint)
//            {
//                return;
//            }

//            int PtX = (int)Math.Floor(ImgStartPoint.X);
//            int PtY = (int)Math.Floor(ImgStartPoint.Y);

//            if (!RoiPoints.Contains(new PointF(PtX, PtY)))
//            {
//                RoiPoints.Add(new PointF(PtX, PtY));
//            }
//        }

//        public override Cursor GetMouseType(PointF ImgPoint)
//        {
//            int ret = IsInsideHyROI(ImgPoint);

//            if (ret == 0)
//            {
//                return Cursors.SizeAll;
//            }
//            else if (ret > 0)
//            {
//                return Cursors.Cross;
//            }
//            else
//            {
//                return Cursors.Default;
//            }
//        }

//        public override int IsInsideHyROI(PointF ImgPoint)
//        {
//            for (int i = 0; i < RoiPoints.Count; i++)
//            {
//                RectangleF ShapeRect = new RectangleF(RoiPoints[i].X - ShapeWidth / 2,
//                  RoiPoints[i].Y - ShapeWidth / 2, ShapeWidth, ShapeWidth);

//                if (ShapeRect.Contains(ImgPoint) == true)
//                {
//                    if (i == 1)
//                    {
//                        return 0;
//                    }
//                    return i + 1;
//                }
//            }

//            return -1;
//        }

//        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
//        {
//            if (MouseDownPoint != ImgStartPoint)
//            {
//                MouseDownPoint = ImgStartPoint;
//                OriginalRoiPoints = RoiPoints.ToArray();
//            }

//            float dx = ImgEndPoint.X - MouseDownPoint.X;
//            float dy = ImgEndPoint.Y - MouseDownPoint.Y;

//            for (int i = 0; i < RoiPoints.Count; i++)
//            {
//                RoiPoints[i] = new PointF(OriginalRoiPoints[i].X + dx, OriginalRoiPoints[i].Y + dy);
//            }
//        }

//        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
//        {
//            if (MouseDownPoint != ImgStartPoint)
//            {
//                MouseDownPoint = ImgStartPoint;
//                OriginalRoiPoints = new PointF[RoiPoints.Count];

//                Array.Copy(RoiPoints.ToArray(), OriginalRoiPoints, RoiPoints.Count);
//                ret = IsInsideHyROI(ImgStartPoint);
//            }

//            float dx = ImgEndPoint.X - MouseDownPoint.X;
//            float dy = ImgEndPoint.Y - MouseDownPoint.Y;
//            if (ret > 0)
//            {
//                RoiPoints[ret - 1] = new PointF(OriginalRoiPoints[ret - 1].X + dx, OriginalRoiPoints[ret - 1].Y + dy);
//            }
//        }

//        public void DisplayShape(Graphics Canvas)
//        {
//            if (IsSelected == true)
//            {
//                Pen ShapePen = new Pen(Color.Yellow, LineWidth);

//                foreach (PointF pt in RoiPoints)
//                {
//                    Canvas.DrawRectangle(ShapePen, pt.X - ShapeWidth / 2, pt.Y - ShapeWidth / 2, ShapeWidth, ShapeWidth);
//                }
//            }
//        }

//    }
//}
