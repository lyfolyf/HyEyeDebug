using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace HyRoiManager.ROI
{
    // 此类型只为记录不规则ROI的数据用，不做其他用
    public class HyDaub : BaseHyROI
    {
        public HyDaub()
        {

        }

        public HyDaub(RoiData roiData) : this()
        {
            DaubData = roiData;
        }


        public override RoiType RoiType => RoiType.Daub;

        public RoiData DaubData { get; set; } = new RoiData();



        public override void Display(Graphics Canvas)
        {

        }

        public override void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {

        }

        public override void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {

        }

        public override void Move(PointF ImgStartPoint, PointF ImgEndPoint)
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




    }
}
