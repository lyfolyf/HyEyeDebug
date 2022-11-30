using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace HyVision.Tools.ImageDisplay
{
    public partial class HyRectangleInfo : UserControl
    {

        private HyRectangle2 hyRectangle2;
        private Panel pl;

        public HyRectangleInfo(Panel DisplayPanel)
        {
            pl = DisplayPanel;
            InitializeComponent();
        }

        private void HyRectangleInfo_Load(object sender, EventArgs e)
        {
           
        }


        public void DisplayROIdata(BaseHyROI InputRect2)
        {
            hyRectangle2 = (HyRectangle2)InputRect2;
            nudCenterX.Value = (decimal)hyRectangle2.Center.X;
            nudCenterY.Value = (decimal)hyRectangle2.Center.Y;
            nudAngle.Value = (decimal)hyRectangle2.Angle;
            nudWidth.Value = (decimal)hyRectangle2.Width;
            nudHeight.Value = (decimal)hyRectangle2.Height;
        }

        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            hyRectangle2.Center = new PointF((float)nudCenterX.Value, hyRectangle2.Center.Y);
            pl.Invalidate();
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            hyRectangle2.Center = new PointF(hyRectangle2.Center.X, (float)nudCenterY.Value);
            pl.Invalidate();
        }

        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            hyRectangle2.Angle = (float)nudAngle.Value;
            pl.Invalidate();
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            hyRectangle2.Width = (float)nudWidth.Value;
            pl.Invalidate();
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            hyRectangle2.Height = (float)nudHeight.Value;
            pl.Invalidate();
        }
    }
}
