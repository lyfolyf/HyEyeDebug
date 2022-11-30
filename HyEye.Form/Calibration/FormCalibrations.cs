using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HyEye.WForm.Calibration
{
    public partial class FormCalibrations : Form
    {
        public FormCalibrations()
        {
            InitializeComponent();
        }

        #region 新建标定

        private void tsmiAddCheckerboard_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAddHandEye_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAddJoint_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 删除标定

        private void tsmiDeleteCalibration_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void tsmiSelectCamera_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (tvCalibration.SelectedNode == null)
            {
                tsmiAddCalibration.Visible = true;
                tsmiDeleteCalibration.Visible = true;
                tsmiSelectCamera.Visible = false;
            }
            else if (tvCalibration.SelectedNode.Level == 0)
            {

            }
        }
    }
}
