using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;



namespace HyRoiManager.ROI
{
    [Serializable]
    [XmlInclude(typeof(HyCircle))]
    [XmlInclude(typeof(HyEllipse))]
    [XmlInclude(typeof(HyRectangle1))]
    [XmlInclude(typeof(HyDaub))]
    [XmlInclude(typeof(HyPolygon))]
    [XmlInclude(typeof(RoiData))]
    //[XmlInclude(typeof(HyRectangle2))]
    //[XmlInclude(typeof(HySector))]
    //[XmlInclude(typeof(HyPoints))]
    public abstract class BaseHyROI
    {
      
        public abstract RoiType RoiType { get; }

        public int Index { get; set; }

        public Color Color { get; set; } = Color.Red;

        public float LineWidth { get; set; } = 1f;

        public bool IsSelected { get; set; } = false;

        public bool IsFill { get; set; } = false;

        public bool Visible { get; set; } = true;

        public string Name { get; set; }

        public static float ShapeWidth { get; set; } = 15;




        public abstract Cursor GetMouseType(PointF ImgPoint);

        public abstract int IsInsideHyROI(PointF ImgPoint);

        public abstract void Display(Graphics Canvas);

        public abstract void Draw(PointF ImgStartPoint, PointF ImgEndPoint);

        public abstract void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint);

        public abstract void Move(PointF ImgStartPoint, PointF ImgEndPoint);

        public virtual void CalculatePointPosition()
        {

        }

    }


    public enum RoiType
    {
        Circle = 1,                   //圆形

        Ellipse = 2,                 //椭圆形

        Rectangle1 = 3,              //不带方向矩形

        //Rectangle2 = 4,            //带方向矩形

        //Sector = 5,                //扇形

        Polygon = 6,               //多边形

        Daub = 7,                 //涂抹类型  不规则图形

        //DefectRegion = 8,       //区域缺陷

        //DefectXLD = 9,         //亚像素缺陷

        //Points = 10,            //点集

        Erase = 11                //擦除类型

    }

}
