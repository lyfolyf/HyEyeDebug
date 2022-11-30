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
    public partial class HyCircleInfo : UserControl
    {

        private Panel pl;
        private HyCircle hyCircle;


        public HyCircleInfo() : this(null)
        {
           
        }

        public HyCircleInfo(Panel DisplayPanel)
        {
            pl = DisplayPanel;
            InitializeComponent();
        }

        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            hyCircle.Center = new PointF((float)nudCenterX.Value, hyCircle.Center.Y);
            pl.Invalidate();
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            hyCircle.Center = new PointF(hyCircle.Center.X, (float)nudCenterY.Value);
            pl.Invalidate();
        }

        private void nudRadius_ValueChanged(object sender, EventArgs e)
        {
            hyCircle.Radius = (float)nudRadius.Value;
            pl.Invalidate();
        }

        public void DisplayROIdata(BaseHyROI InputHyCircle)
        {
            hyCircle = (HyCircle)InputHyCircle;
            nudCenterX.Value = (decimal)hyCircle.Center.X;
            nudCenterY.Value = (decimal)hyCircle.Center.Y;
            nudRadius.Value = (decimal)hyCircle.Radius;
        }

    }
}
