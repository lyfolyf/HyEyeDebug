using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyVision.Tools.ImageDisplay
{

    [Serializable]
    public class HyPolygon : BaseHyROI
    {
        //gen_region_polygon
        public HyPolygon()
        {
            RoiType = RoiType.Polygon;
        }



        public List<PointF> PolygonPoints { get; set; } = new List<PointF>();


        private PointF[] OriginalPolygonPoints;
        private PointF MouseDownPoint, SpecialPoint = new PointF(-111.111f, -111.111f);
        private bool IsDrawing;
        private int ret;

        public override void Display(Graphics Canvas)
        {
            if (IsDrawing == true)
            {
                Canvas.DrawLines(new Pen(Color, LineWidth), PolygonPoints.ToArray());
                DisplayShape(Canvas);
            }
            else
            {
                if (PolygonPoints.Count > 2)
                {
                    GraphicsPath graphicsPath = new GraphicsPath();
                    graphicsPath.AddPolygon(PolygonPoints.ToArray());

                    if (IsFill == true)
                    {
                        SolidBrush solidBrush = new SolidBrush(Color);
                        Canvas.FillPath(solidBrush, graphicsPath);
                    }
                    else
                    {
                        Canvas.DrawPath(new Pen(Color, LineWidth), graphicsPath);
                        //Canvas.DrawPolygon(new Pen(Color, LineWidth), PolygonPoints.ToArray());
                    }
                    DisplayShape(Canvas);

                    //Canvas.DrawString(Index.ToString(), new Font("Arial", 10), Brushes.Blue, PolygonPoints[0].X, PolygonPoints[0].Y);
                }
            }
        }


        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            int Count = PolygonPoints.Count;

            if (ImgEndPoint == SpecialPoint)
            {
                if (Count == 0)
                {
                    PolygonPoints.Add(ImgStartPoint);
                    PolygonPoints.Add(ImgStartPoint);
                }
                else
                {
                    PolygonPoints[Count - 1] = ImgStartPoint;
                    PolygonPoints.Add(ImgStartPoint);
                }

                IsDrawing = true;
            }

            if (ImgStartPoint == SpecialPoint)
            {
                PolygonPoints[Count - 1] = ImgEndPoint;
            }

            if (ImgStartPoint.X == 0 && ImgStartPoint.Y == 1 &&
               ImgEndPoint.X == 1 && ImgEndPoint.Y == 0)
            {
                PolygonPoints.RemoveAt(Count - 1);
                IsDrawing = false;
            }
        }

        public override Cursor GetMouseType(PointF ImgPoint)
        {
            int ret = IsInsideHyROI(ImgPoint);

            if (ret == 0)
            {
                return Cursors.SizeAll;
            }
            else if (ret > 0)
            {
                return Cursors.Cross;
            }
            else
            {
                return Cursors.Default;
            }

        }

        public override int IsInsideHyROI(PointF ImgPoint)
        {
            GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
            graphicsPath.AddPolygon(PolygonPoints.ToArray());
            Region HyRegion = new Region(graphicsPath);
            Region ComplementRegion = new Region();
            ComplementRegion.MakeEmpty();

            for (int i = 0; i < PolygonPoints.Count; i++)
            {
                RectangleF ShapeRect = new RectangleF(PolygonPoints[i].X - ShapeWidth / 2,
                    PolygonPoints[i].Y - ShapeWidth / 2, ShapeWidth, ShapeWidth);
                ComplementRegion.Union(ShapeRect);

                if (ShapeRect.Contains(ImgPoint) == true)
                {
                    return i + 1;
                }
            }

            ComplementRegion.Complement(HyRegion);
            if (ComplementRegion.IsVisible(ImgPoint))
            {
                return 0;
            }
            return -1;
        }

        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                MouseDownPoint = ImgStartPoint;
                OriginalPolygonPoints = PolygonPoints.ToArray();
            }

            float dx = ImgEndPoint.X - MouseDownPoint.X;
            float dy = ImgEndPoint.Y - MouseDownPoint.Y;

            for (int i = 0; i < PolygonPoints.Count; i++)
            {
                PolygonPoints[i] = new PointF(OriginalPolygonPoints[i].X + dx, OriginalPolygonPoints[i].Y + dy);
            }
        }

        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                MouseDownPoint = ImgStartPoint;
                OriginalPolygonPoints = new PointF[PolygonPoints.Count];

                Array.Copy(PolygonPoints.ToArray(), OriginalPolygonPoints, PolygonPoints.Count);
                ret = IsInsideHyROI(ImgStartPoint);
            }

            float dx = ImgEndPoint.X - MouseDownPoint.X;
            float dy = ImgEndPoint.Y - MouseDownPoint.Y;
            if (ret > 0)
            {
                PolygonPoints[ret - 1] = new PointF(OriginalPolygonPoints[ret - 1].X + dx, OriginalPolygonPoints[ret - 1].Y + dy);
            }

        }


        public void DisplayShape(Graphics Canvas)
        {
            if (IsSelected == true)
            {
                Pen ShapePen = new Pen(Color.Yellow, LineWidth);

                foreach (PointF pt in PolygonPoints)
                {
                    Canvas.DrawRectangle(ShapePen, pt.X - ShapeWidth / 2, pt.Y - ShapeWidth / 2, ShapeWidth, ShapeWidth);
                }
            }
        }

        //public override void CalculatePointPosition()
        //{
        //    for (int i = 0; i < PolygonPoints.Count; i++)
        //    {
        //        //PolygonPoints[i] = new PointF()
        //    }
        //}
    }
}
