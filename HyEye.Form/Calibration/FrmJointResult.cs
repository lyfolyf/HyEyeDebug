using System.Windows.Forms;

namespace HyEye.WForm.Calibration
{
    public partial class FrmJointResult : Form
    {
        public FrmJointResult(Control c)
        {
            InitializeComponent();

            Controls.Add(c);
        }

        private void FrmJointResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controls.RemoveAt(0);
        }
    }
}
