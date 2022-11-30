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
    public partial class HyRectangle1Info : UserControl
    {

        private HyRectangle1 srcRectangle1;
        private HalconEngineUI_2 HalconEngineViewer;

        public HyRectangle1Info(HalconEngineUI_2 HalconEngineViewer, HyRectangle1 InputHyRectangle1)
        {
            srcRectangle1 = InputHyRectangle1;
            this.HalconEngineViewer = HalconEngineViewer;
            InitializeComponent();
            DisplayROIdata(srcRectangle1);
        }

   

        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle1.Center = new PointF((float)nudCenterX.Value, srcRectangle1.Center.Y);
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle1);
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle1.Center = new PointF(srcRectangle1.Center.X, (float)nudCenterY.Value);
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle1);
        }


        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle1.Width = (float)nudWidth.Value;
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle1);
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            srcRectangle1.Height = (float)nudHeight.Value;
            HalconEngineViewer.EditSrcRoidataToDisplay(srcRectangle1);
        }

        public void DisplayROIdata(HyRectangle1 InputHyRectangle1)
        {
            srcRectangle1 = InputHyRectangle1;
            nudCenterX.Value = (decimal)srcRectangle1.Center.X;
            nudCenterY.Value = (decimal)srcRectangle1.Center.Y;
            nudWidth.Value = (decimal)srcRectangle1.Width;
            nudHeight.Value = (decimal)srcRectangle1.Height;
        }
    }
}
