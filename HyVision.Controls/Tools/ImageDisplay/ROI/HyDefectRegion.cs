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
    public class HyDefectRegion : BaseHyROI
    {

        public HyDefectRegion()
        {
            RoiType = RoiType.DefectRegion;
        }

        public List<PointF> DefectPoints { get; set; } = new List<PointF>();




        public override void Display(Graphics Canvas)
        {
            //GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);

            //foreach (PointF pt in DefectPoints)
            //{
            //    RectangleF rect = new RectangleF(pt.X, pt.Y, 1, 1);

            //    graphicsPath.AddRectangle(rect);
            //}

            //SolidBrush solidBrush = new SolidBrush(Color);
            //Canvas.FillPath(solidBrush, graphicsPath);
            //solidBrush.Dispose();



            SolidBrush solidBrush1 = new SolidBrush(Color);
            foreach (PointF pt in DefectPoints)
            {
                RectangleF rect = new RectangleF(pt.X, pt.Y, 1, 1);

                Canvas.FillRectangle(solidBrush1, rect);
            }
            solidBrush1.Dispose();


        }

        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
           
        }

        public override Cursor GetMouseType(PointF ImgPoint)
        {
            return Cursors.Default;
        }

        public override int IsInsideHyROI(PointF ImgPoint)
        {
            return -1;
        }

        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
        {

        }

        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {

        }


    }
}
