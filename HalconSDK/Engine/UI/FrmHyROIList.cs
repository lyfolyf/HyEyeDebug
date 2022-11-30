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
    public partial class FrmHyROIList : Form
    {

        private List<BaseHyROI> HyROIs;
        public FrmHyROIList(List<BaseHyROI> HyROIs)
        {
            this.HyROIs = HyROIs;
            InitializeComponent();
        }

        private void FrmHyROIList_Load(object sender, EventArgs e)
        {
            lbROIList.Items.Clear();
            foreach (BaseHyROI roi in HyROIs)
            {
                lbROIList.Items.Add($"ROI{roi.Index}");
            }

            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }


    }
}
