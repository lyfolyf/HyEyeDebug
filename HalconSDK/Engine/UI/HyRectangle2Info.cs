using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyVision.Tools.ImageDisplay;





namespace HalconSDK.Engine.UI
{
    public partial class HyRectangle2Info : UserControl
    {

        private HyRectangle2 srcRectangle2;
        private HalconEngineUI_2 HalconEngineViewer;

        public HyRectangle2Info(HalconEngineUI_2 HalconEngineViewer, HyRectangle2 InputHyRectangle2)
        {
            srcRectangle2 = InputHyRectangle2;
            this.HalconEngineViewer = HalconEngineViewer;
            InitializeComponent();
            DisplayROIdata(srcRectangle2);
        }


        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle2.Center = new PointF((float)nudCenterX.Value, srcRectangle2.Center.Y);
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle2);
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle2.Center = new PointF(srcRectangle2.Center.X, (float)nudCenterY.Value);
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle2);
        }

        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle2.Angle = (float)nudAngle.Value;
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle2);
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle2.Width = (float)nudWidth.Value;
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle2);
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle2.Height = (float)nudHeight.Value;
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle2);
        }

        public void DisplayROIdata(HyRectangle2 InputRect2)
        {
            srcRectangle2 = InputRect2;
            nudCenterX.Value = (decimal)srcRectangle2.Center.X;
            nudCenterY.Value = (decimal)srcRectangle2.Center.Y;
            nudAngle.Value = (decimal)srcRectangle2.Angle;
            nudWidth.Value = (decimal)srcRectangle2.Width;
            nudHeight.Value = (decimal)srcRectangle2.Height;
        }
    }
}
