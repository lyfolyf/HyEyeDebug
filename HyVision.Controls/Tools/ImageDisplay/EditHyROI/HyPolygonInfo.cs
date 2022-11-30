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
    public partial class HyPolygonInfo : UserControl
    {
        private Panel pl;
        private HyPolygon hyPolygon;



        public HyPolygonInfo(Panel DisplayPanel)
        {
            pl = DisplayPanel;
            InitializeComponent();
        }

        private void nudPointX_ValueChanged(object sender, EventArgs e)
        {
            int PointIndex = lbPoints.SelectedIndex;
            if (PointIndex != -1)
            {
                hyPolygon.PolygonPoints[PointIndex] = new PointF((float)nudPointX.Value, hyPolygon.PolygonPoints[PointIndex].Y);
                pl.Invalidate();
            }
        }

        private void nudPointY_ValueChanged(object sender, EventArgs e)
        {
            int PointIndex = lbPoints.SelectedIndex;
            if (PointIndex != -1)
            {
                hyPolygon.PolygonPoints[PointIndex] = new PointF(hyPolygon.PolygonPoints[PointIndex].X, (float)nudPointY.Value);
                pl.Invalidate();
            }
        }

        public void DisplayROIdata(BaseHyROI InputHyPolgon)
        {
            hyPolygon = (HyPolygon)InputHyPolgon;

            lbPoints.Items.Clear();
            for (int i = 0; i < hyPolygon.PolygonPoints.Count; i++)
            {
                lbPoints.Items.Add($"Point{i + 1}");
            }
            if (lbPoints.Items.Count > 0)
            {
                lbPoints.SelectedIndex = 0;
                nudPointX.Value = (decimal)hyPolygon.PolygonPoints[0].X;
                nudPointY.Value = (decimal)hyPolygon.PolygonPoints[0].Y;
            }

        }

        private void lbPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PointIndex = lbPoints.SelectedIndex;
            if (PointIndex != -1)
            {
                nudPointX.Value = (decimal)hyPolygon.PolygonPoints[PointIndex].X;
                nudPointY.Value = (decimal)hyPolygon.PolygonPoints[PointIndex].Y;

            }

        }
    }
}
