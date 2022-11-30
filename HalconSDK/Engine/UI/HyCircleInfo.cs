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


    public partial class HyCircleInfo : UserControl
    {


        private HyCircle srcCircle;
        private HalconEngineUI_2 HalconEngineViewer;

        public HyCircleInfo()
        {
            InitializeComponent();
            nudCenterX.ValueChanged -= nudCenterX_ValueChanged;
            nudCenterY.ValueChanged -= nudCenterY_ValueChanged;
            nudRadius.ValueChanged -= nudRadius_ValueChanged;
        }

        public HyCircleInfo(HalconEngineUI_2 HalconEngineViewer, HyCircle InputHyCircle) 
        {
            srcCircle = InputHyCircle;
            this.HalconEngineViewer = HalconEngineViewer;
            InitializeComponent();
            DisplayROIdata(srcCircle);
        }

        private void nudCenterX_ValueChanged(object sender, EventArgs e)
        {
            srcCircle.Center = new PointF((float)nudCenterX.Value, srcCircle.Center.Y);
            HalconEngineViewer.EditSrcRoidataToDisplay(srcCircle);
        }

        private void nudCenterY_ValueChanged(object sender, EventArgs e)
        {
            srcCircle.Center = new PointF(srcCircle.Center.X, (float)nudCenterY.Value);
            HalconEngineViewer.EditSrcRoidataToDisplay(srcCircle);
        }

        private void nudRadius_ValueChanged(object sender, EventArgs e)
        {
            srcCircle.Radius = (float)nudRadius.Value;
            HalconEngineViewer.EditSrcRoidataToDisplay(srcCircle);
        }

        public void DisplayROIdata(HyCircle InputHyCircle)
        {
            srcCircle = InputHyCircle;
            nudCenterX.Value = (decimal)srcCircle.Center.X;
            nudCenterY.Value = (decimal)srcCircle.Center.Y;
            nudRadius.Value = (decimal)srcCircle.Radius;
        }

    }
}
