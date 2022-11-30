using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Windows.Forms;

namespace HyEye.WForm.Calibration
{
    public partial class FrmHandEyeParamSetting : Form
    {
        readonly ICalibrationRepository calibRepo;
        readonly string calibName;

        public FrmHandEyeParamSetting(string calibName, ICalibrationRepository calibRepo)
        {
            InitializeComponent();

            Text = $"{calibName} - 参数设置";

            this.calibName = calibName;
            this.calibRepo = calibRepo;
        }

        HandEyeInfoVO handEyeInfo;

        private void FrmHandEyeParamSetting_Load(object sender, EventArgs e)
        {
            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);

            handEyeInfo = calibInfo.HandEyeInfo;

            XPointNum.Value = handEyeInfo.XPointNum;
            YPointNum.Value = handEyeInfo.YPointNum;
            APointNum.Value = handEyeInfo.APointNum;
            XStep.Value = (decimal)handEyeInfo.XStep;
            YStep.Value = (decimal)handEyeInfo.YStep;
            AStep.Value = (decimal)handEyeInfo.AStep;

            ckbEnabledFitCircle.Checked = handEyeInfo.EnabledFitCircle;

            label1.Enabled = ckbEnabledFitCircle.Checked;
            rbtnMultiPointAngleFit.Enabled = ckbEnabledFitCircle.Checked;
            rbtnMultiPointFit.Enabled = ckbEnabledFitCircle.Checked;

            switch (handEyeInfo.FitCircleType)
            {
                case 1:
                    rbtnMultiPointFit.Checked = true;
                    break;
                case 2:
                    rbtnMultiPointAngleFit.Checked = true;
                    break;
                default:
                    rbtnMultiPointFit.Checked = true;
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            handEyeInfo.XPointNum = (int)XPointNum.Value;
            handEyeInfo.YPointNum = (int)YPointNum.Value;
            handEyeInfo.APointNum = (int)APointNum.Value;
            handEyeInfo.XStep = (double)XStep.Value;
            handEyeInfo.YStep = (double)YStep.Value;
            handEyeInfo.AStep = (double)AStep.Value;

            handEyeInfo.EnabledFitCircle = ckbEnabledFitCircle.Checked;

            if (rbtnMultiPointFit.Checked)
                handEyeInfo.FitCircleType = 1;
            if (rbtnMultiPointAngleFit.Checked)
                handEyeInfo.FitCircleType = 2;

            calibRepo.SetHandEyeParams(calibName, handEyeInfo);
            //calibRepo.Save();

            DialogResult = DialogResult.OK;
        }

        private void ckbEnabledFitCircle_CheckedChanged(object sender, EventArgs e)
        {
            label5.Enabled = ckbEnabledFitCircle.Checked;
            label6.Enabled = ckbEnabledFitCircle.Checked;
            APointNum.Enabled = ckbEnabledFitCircle.Checked;
            AStep.Enabled = ckbEnabledFitCircle.Checked;

            label1.Enabled = ckbEnabledFitCircle.Checked;
            rbtnMultiPointAngleFit.Enabled = ckbEnabledFitCircle.Checked;
            rbtnMultiPointFit.Enabled = ckbEnabledFitCircle.Checked;
        }

        private void rbtnMultiPointAngleFit_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMultiPointAngleFit.Checked)
                APointNum.Minimum = 2;
            else
                APointNum.Minimum = 3;
        }

    }
}
