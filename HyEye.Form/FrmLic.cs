using HyCommon.License;
using System;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmLic : Form
    {
        HyLicense license;

        public int leftTime { get; set; }

        public FrmLic()
        {
            InitializeComponent();
            license = new HyLicense();
        }

        private void FrmLic_Load(object sender, EventArgs e)
        {
            txtMCode.Text = LicenseUtils.GetCustomMachineCode();
        }

        private void btnLic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMCode.Text))
            {
                MessageBox.Show("设备码不能为空");
                return;
            }
            if (string.IsNullOrEmpty(txtLic.Text))
            {
                MessageBox.Show("激活码不能为空");
                return;
            }

            LicenseInfo licInfo = new LicenseInfo();

            try
            {
                licInfo = license.DesLic(txtLic.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("激活异常：" + ex.Message);
                return;
            }

            if (licInfo.ProgramName != "HyInspect")
            {
                MessageBox.Show("激活失败，该激活码不是当前程序激活码");
                return;
            }

            licInfo.LastUseTime = DateTime.Now;
            if (licInfo.CustomRole == LicenseType.Permanent)
                licInfo.CustomMachineCode = txtMCode.Text;

            license.ActiveHyEye(licInfo);
            LicenseState current = license.GetLicenseState();
            leftTime = license.GetLeftTime();

            switch (current)
            {
                case LicenseState.Valid:
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;
                case LicenseState.Invalid:
                    MessageBox.Show("激活无效");
                    break;
                case LicenseState.Expired:
                    MessageBox.Show("激活码已过期");
                    break;
                case LicenseState.Damaged:
                    MessageBox.Show("激活文件损坏");
                    break;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            LicenseInfo licInfo = new LicenseInfo();
            licInfo.ProgramName = "HyInspect";
            licInfo.RegisterTime = DateTime.Now;
            licInfo.CustomRole = LicenseType.LongTime30;
            licInfo.LastUseTime = DateTime.Now;
            license.ActiveHyEye(licInfo);
            leftTime = license.GetLeftTime();
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
