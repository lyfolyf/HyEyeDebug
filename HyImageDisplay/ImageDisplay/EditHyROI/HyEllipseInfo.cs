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
    public partial class HyEllipseInfo : UserControl
    {

        private Panel pl;
        private HyEllipse hyEllipse;


        public HyEllipseInfo(Panel DisplayPanel)
        {
            pl = DisplayPanel;
            InitializeComponent();
        }





        public void DisplayROIdata(BaseHyROI InputHyEllipse)
        {
            hyEllipse = (HyEllipse)InputHyEllipse;
            nudCenterX.Value = (decimal)hyEllipse.Center.X;
            nudCenterY.Value = (decimal)hyEllipse.Center.Y;
            nudAngle.Value = (decimal)hyEllipse.Angle;
            nudWidth.Value = (decimal)hyEllipse.Width;
            nudHeight.Value = (decimal)hyEllipse.Height;
        }

        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            hyEllipse.Center = new PointF((float)nudCenterX.Value, hyEllipse.Center.Y);
            pl.Invalidate();
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            hyEllipse.Center = new PointF(hyEllipse.Center.X, (float)nudCenterY.Value);
            pl.Invalidate();
        }

        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            hyEllipse.Angle = (float)nudAngle.Value;
            pl.Invalidate();
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            hyEllipse.Width = (float)nudWidth.Value;
            pl.Invalidate();
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            hyEllipse.Height = (float)nudHeight.Value;
            pl.Invalidate();
        }

    
    }
}
