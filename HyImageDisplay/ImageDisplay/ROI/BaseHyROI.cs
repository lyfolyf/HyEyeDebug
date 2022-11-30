using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;



namespace HyVision.Tools.ImageDisplay
{

    [Serializable]
    [XmlInclude(typeof(HyCircle))]
    [XmlInclude(typeof(HyEllipse))]
    [XmlInclude(typeof(HyRectangle2))]
    [XmlInclude(typeof(HyPolygon))]
    [XmlInclude(typeof(HySector))]
    public abstract class BaseHyROI
    {

        public int Index { get; set; }

        public ROIType RoiType { get;  set; } //protected 不能XML序列化

        public Color Color { get; set; } = Color.Red;

        public float LineWidth { get; set; } = 0.1f;

        public bool IsSelected { get; set; } = false;

        public bool IsFill { get; set; } = false;

        public bool Visible { get; set; } = true;

        public object Tag { get; set; }

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


    public enum ROIType
    {
        Circle = 1,          //圆形

        Ellipse = 2,         //椭圆形

        //Rectangle1 =3,       //不带方向矩形

        Rectangle2 = 4,       //带方向矩形

        Sector = 5,          //扇形

        Polygon = 6,         //多边形

        //Irregular = 7,       //不规则图形

        DefectRegion = 8,       //区域缺陷

        DefectXLD = 9,         //亚像素缺陷


    }

}
