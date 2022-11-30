using HyEye.API.Repository;
using HyEye.Models.VO;
using HyEye.Services;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm.Calibration
{
    public partial class FrmHandEyeSinglePattern : DockContent
    {
        readonly HandeyeSingleRunner runner;
        readonly string calibName;
        readonly ICalibrationRepository calibRepo;

        public FrmHandEyeSinglePattern(string calibName,
            HandeyeSingleRunner runner,
            ICalibrationRepository calibRepo)
        {
            InitializeComponent();

            this.calibName = calibName;
            this.runner = runner;
            this.calibRepo = calibRepo;
        }

        private void FrmHandEyePattern_Load(object sender, EventArgs e)
        {
            Text = $"{calibName} - 模板设置";

            CalibrationInfoVO calibInfo = calibRepo.GetCalibration(calibName);
            if (calibInfo.HandEyeSingleInfo.PMAlignOrToolBlock)
            {
                rdbtnUsePMAlign.Checked = true;
            }
            else
            {
                rdbtnUseToolBlock.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            runner.HandeyeSingleComponent.SavePattern();

            calibRepo.Save();
        }

        private void btnAcqImage_Click(object sender, EventArgs e)
        {
            runner.AcqImage();
        }

        private void rdbtnUsePMAlign_CheckedChanged(object sender, EventArgs e)
        {
            calibRepo.SetHandEyePatternMode(calibName, true);

            panel2.Controls.Clear();
            panel2.Controls.Add(runner.HandeyeSingleComponent.PatternControl);
        }

        private void rdbtnUseToolBlock_CheckedChanged(object sender, EventArgs e)
        {
            calibRepo.SetHandEyePatternMode(calibName, false);

            panel2.Controls.Clear();
            panel2.Controls.Add(runner.HandeyeSingleComponent.PatternControl);
        }
    }
}
