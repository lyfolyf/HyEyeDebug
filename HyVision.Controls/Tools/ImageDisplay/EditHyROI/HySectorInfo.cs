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
    public partial class HySectorInfo : UserControl
    {


        private Panel pl;
        private HySector hySector;

        public HySectorInfo(Panel DisplayPanel)
        {
            pl = DisplayPanel;
            InitializeComponent();
        }

        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            hySector.Center = new PointF((float)nudCenterX.Value, hySector.Center.Y);
            pl.Invalidate();
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            hySector.Center = new PointF(hySector.Center.X, (float)nudCenterY.Value);
            pl.Invalidate();
        }

        private void nudRadius_ValueChanged(object sender, EventArgs e)
        {
            hySector.Radius = (float)nudRadius.Value;
            pl.Invalidate();
        }

        private void nudStartAngle_ValueChanged(object sender, EventArgs e)
        {
            hySector.StartAngle = (float)nudStartAngle.Value;
            pl.Invalidate();
        }

        private void nudEndAngle_ValueChanged(object sender, EventArgs e)
        {
            hySector.EndAngle = (float)nudEndAngle.Value;
            pl.Invalidate();
        }

        public void DisplayROIdata(BaseHyROI InputHyROI)
        {
            hySector = (HySector)InputHyROI;
            nudCenterX.Value = (decimal)hySector.Center.X;
            nudCenterY.Value = (decimal)hySector.Center.Y;
            nudRadius.Value = (decimal)hySector.Radius;
            nudStartAngle.Value = (decimal)hySector.StartAngle;
            nudEndAngle.Value = (decimal)hySector.EndAngle;
        }
    }
}
