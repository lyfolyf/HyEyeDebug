//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;





//namespace HyDisplayWindow.ROI
//{

//    [Serializable]
//    public class HyDefectXLD : BaseHyROI
//    {
//        public HyDefectXLD()
//        {
//            //RoiType = RoiType.DefectXLD;
//        }



//        public List<PointF> DefectXLDPoints { get; set; } = new List<PointF>();



//        public override void Display(Graphics Canvas)
//        {
//            //GraphicsPath graphicsPath = new GraphicsPath();
//            ////graphicsPath.AddLines(DefectXLDPoints.ToArray());
//            //SolidBrush solidBrush = new SolidBrush(Color);
//            //Canvas.FillPath(solidBrush, graphicsPath);
//            //solidBrush.Dispose();

//            Canvas.DrawLines(new Pen(Color, LineWidth), DefectXLDPoints.ToArray());
//        }

//        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
//        {

//        }

//        public override Cursor GetMouseType(PointF ImgPoint)
//        {
//            return Cursors.Default;
//        }

//        public override int IsInsideHyROI(PointF ImgPoint)
//        {
//            return -1;
//        }

//        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
//        {

//        }

//        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
//        {

//        }
//    }
//}
