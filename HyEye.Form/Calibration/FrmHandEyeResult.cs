using HyEye.API.Repository;
using HyEye.Models.VO;
using System.Windows.Forms;
using VisionSDK;

namespace HyEye.WForm.Calibration
{
    public partial class FrmHandEyeResult : Form
    {
        readonly IHandeyeComponent handeyeComponent;

        public FrmHandEyeResult(string calibName, IHandeyeComponent handeyeComponent, ICalibrationRepository calibrationRepo)
        {
            InitializeComponent();

            Text = $"{calibName} - 标定结果";

            this.handeyeComponent = handeyeComponent;

            tabPage1.Controls.Add(handeyeComponent.NPointToNPointControl);

            CalibrationInfoVO calibInfo = calibrationRepo.GetCalibration(calibName);
            if (calibInfo.HandEyeInfo.EnabledFitCircle)
            {
                tabPage2.Controls.Add(handeyeComponent.FitCircleControl);
            }
            else
            {
                tabControl1.TabPages.RemoveAt(1);
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            handeyeComponent.Save();
        }
    }
}
