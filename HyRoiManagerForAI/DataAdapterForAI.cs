using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HyWrapper;
using HyRoiManager;
using HyRoiManager.ROI;





namespace HyRoiManagerForAI
{
    public class DataAdapterForAI
    {

        public static BaseHyROI AiDefectToHyRoi(HyWrapper.Region AiDefect)
        {
            if (AiDefect is HyWrapper.Rect)
            {
                Rect rect = AiDefect as Rect;
                HyRectangle1 DstRoi = new HyRectangle1();
                DstRoi.Center = new PointF(rect.x - rect.width / 2, rect.y - rect.height / 2);
                DstRoi.Width = rect.width;
                DstRoi.Height = rect.height;

                return DstRoi;
            }
            else if (AiDefect is HyWrapper.Circle)
            {
                Circle circle = AiDefect as Circle;
                HyCircle DstRoi = new HyCircle();
                DstRoi.Center = new PointF(circle.center.x, circle.center.y);
                DstRoi.Radius = circle.radius;

                return DstRoi;
            }
            else if (AiDefect is HyWrapper.Contour)
            {
                Contour contour = new Contour();
                HyPolygon DstRoi = new HyPolygon();

                float MaxNumber = 10000;
                int Length = contour.vertices.Length;
                if (Length > MaxNumber)
                {
                    int Interval = 1;
                    double ratio = Length / MaxNumber;

                    if (ratio > 1.35 && ratio <= 2.5)
                    {
                        Interval = 2;
                    }
                    else
                    {
                        Interval = (int)Math.Round(ratio);
                    }
                    for (int i = 0; i < Length; i = i + Interval)
                    {
                        DstRoi.PolygonPoints.Add(new PointF(contour.vertices[i].x, contour.vertices[i].y));
                    }
                }
                else
                {
                    for (int i = 0; i < Length; i++)
                    {
                        DstRoi.PolygonPoints.Add(new PointF(contour.vertices[i].x, contour.vertices[i].y));
                    }
                }

                return DstRoi;
            }
            else if (AiDefect is HyWrapper.Polygon)
            {
                Polygon polygon = AiDefect as Polygon;
                HyPolygon DstRoi = new HyPolygon();

                foreach (HyWrapper.Point pt in polygon.vertices)
                {
                    DstRoi.PolygonPoints.Add(new PointF(pt.x, pt.y));
                }

                return DstRoi;
            }
            else if (AiDefect is HyWrapper.Points)
            {
                Points AIpoints = AiDefect as Points;
                HyDaub DstRoi = new HyDaub();

                float MaxNumber = 10000;
                int Length = AIpoints.points.Length;
                if (Length > MaxNumber)
                {
                    int Interval = 1;
                    double ratio = Length / MaxNumber;

                    if (ratio > 1.35 && ratio <= 2.5)
                    {
                        Interval = 2;
                    }
                    else
                    {
                        Interval = (int)Math.Round(ratio);
                    }
                    for (int i = 0; i < Length; i = i + Interval)
                    {

                        DstRoi.DaubData.RowIndex.Add(AIpoints.points[i].x);
                        DstRoi.DaubData.StartColumn.Add(AIpoints.points[i].x);
                        DstRoi.DaubData.StartColumn.Add(AIpoints.points[i].y);
                    }
                }
                else
                {
                    for (int i = 0; i < Length; i++)
                    {
                        DstRoi.DaubData.RowIndex.Add(AIpoints.points[i].x);
                        DstRoi.DaubData.StartColumn.Add(AIpoints.points[i].x);
                        DstRoi.DaubData.StartColumn.Add(AIpoints.points[i].y);
                    }
                }

                return DstRoi;
            }


            return null;

        }
    }

}
