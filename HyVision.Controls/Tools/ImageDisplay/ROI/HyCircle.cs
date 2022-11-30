using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;




namespace HyVision.Tools.ImageDisplay
{
    [Serializable]
    public class HyCircle : BaseHyROI
    {
        public HyCircle()
        {
            RoiType = RoiType.Circle;
        }



        private float _Radius;
        public float Radius
        {
            get { return _Radius; }
            set
            {
                _Radius = value;
                CalculatePointPosition();
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



        private float OriginalRadius;
        private PointF OriginalCenter = new PointF(), MouseDownPoint = new PointF();
        private int ret;
        private PointF LeftTopCorner = new PointF();
        private PointF[] ShapePoints = new PointF[4];

        /* ShapePoints各点示意图
         *          0
         *    
         *   
         * 3                  1
         *    
         *      
         *          2
         */


        public override void Display(Graphics Canvas)
        {
            if (Visible == true)
            {
                if (IsFill == true)
                {
                    SolidBrush solidBrush = new SolidBrush(Color);
                    Canvas.FillEllipse(solidBrush, LeftTopCorner.X, LeftTopCorner.Y, Radius * 2, Radius * 2);              
                }
                else
                {
                    Canvas.DrawEllipse(new Pen(Color, LineWidth), LeftTopCorner.X, LeftTopCorner.Y, Radius * 2, Radius * 2);
                }
                DisplayShape(Canvas);
                //Canvas.DrawString(Index.ToString(), new Font("Arial", 10), Brushes.Blue, LeftTopCorner.X, LeftTopCorner.Y);
            }
        }

        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            //float dx = ImgEndPoint.X - ImgStartPoint.X;
            //float dy = ImgEndPoint.Y - ImgStartPoint.Y;
            //Radius = (float)Math.Sqrt(dx * dx + dy * dy);

            Vector2 pt1 = new Vector2(ImgStartPoint.X, ImgStartPoint.Y);
            Vector2 pt2 = new Vector2(ImgEndPoint.X, ImgEndPoint.Y);
            Center = new PointF(ImgStartPoint.X, ImgStartPoint.Y);
            Radius = Vector2.Distance(pt1, pt2);
        }

        public override Cursor GetMouseType(PointF ImgPoint)
        {
            int ret = IsInsideHyROI(ImgPoint);

            if (ret == 0)
            {
                return Cursors.SizeAll;
            }
            else if (ret >= 1 && ret <= 4)
            {
                return Cursors.Cross;
            }
            else
            {
                return Cursors.Arrow;
            }
        }

        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                OriginalRadius = Radius;
                OriginalCenter = Center;

                MouseDownPoint = ImgStartPoint;
                ret = IsInsideHyROI(ImgStartPoint);
            }

            float dx = ImgEndPoint.X - MouseDownPoint.X;
            float dy = ImgEndPoint.Y - MouseDownPoint.Y;
            switch (ret)
            {
                case 0:
                    {
                        //Center = new PointF(OriginalCenter.X + dx, OriginalCenter.Y + dy);
                        //LeftTopCorner = new PointF(Center.X - Radius, Center.Y - Radius);
                        //CalculateShapePosition();
                        break;
                    }

                case 1:
                    {
                        Radius = Math.Abs(OriginalRadius - dy);
                        CalculatePointPosition();
                        break;
                    }

                case 2:
                    {
                        Radius = Math.Abs(OriginalRadius + dx);
                        CalculatePointPosition();
                        break;
                    }

                case 3:
                    {
                        Radius = Math.Abs(OriginalRadius + dy);
                        CalculatePointPosition();
                        break;
                    }

                case 4:
                    {
                        Radius = Math.Abs(OriginalRadius - dx);
                        CalculatePointPosition();
                        break;
                    }
            }
        }

        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                OriginalRadius = Radius;
                OriginalCenter = Center;
                MouseDownPoint = ImgStartPoint;              
            }

            float dx = ImgEndPoint.X - MouseDownPoint.X;
            float dy = ImgEndPoint.Y - MouseDownPoint.Y;

            Center = new PointF(OriginalCenter.X + dx, OriginalCenter.Y + dy);
            CalculatePointPosition();
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
            }
        }

        /// <summary>
        /// 不在区域内返回值 -1,  0代表在ROI区域内 , 1-4 代表在对应的小矩形内
        /// </summary>
        /// <param name="ImgPoint">要检测的输入图像点</param>
        /// <returns></returns>
        public override int IsInsideHyROI(PointF ImgPoint)
        {
            RectangleF HyEllipse = new RectangleF(LeftTopCorner, new SizeF(Radius * 2, Radius * 2));
            GraphicsPath GraPhEllipse = new GraphicsPath(FillMode.Winding);
            GraPhEllipse.AddEllipse(HyEllipse);

            Region HyRegion = new Region(GraPhEllipse);
            Region ComplementRegion = new Region();
            ComplementRegion.MakeEmpty();

            for (int i = 0; i < ShapePoints.Length; i++)
            {
                RectangleF ShapeRect = new RectangleF(ShapePoints[i], new SizeF(ShapeWidth, ShapeWidth));
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

        public override void CalculatePointPosition()
        {
            LeftTopCorner = new PointF(Center.X - Radius, Center.Y - Radius);

            ShapePoints[0] = new PointF(Center.X - ShapeWidth / 2, Center.Y - Radius - ShapeWidth / 2);
            ShapePoints[1] = new PointF(Center.X + Radius - ShapeWidth / 2, Center.Y - ShapeWidth / 2);
            ShapePoints[2] = new PointF(Center.X - ShapeWidth / 2, Center.Y + Radius - ShapeWidth / 2);
            ShapePoints[3] = new PointF(Center.X - Radius - ShapeWidth / 2, Center.Y - ShapeWidth / 2);
        }


    }
}
