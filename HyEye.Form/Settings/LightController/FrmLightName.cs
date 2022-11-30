using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmLightName : Form
    {
        readonly ILightControllerRepository controllerRepo;

        string controllerName;
        ChannelLightInfoVO channelLight;

        public FrmLightName(ILightControllerRepository controllerRepo, string controllerName, ChannelLightInfoVO channelLight)
        {
            InitializeComponent();

            this.controllerRepo = controllerRepo;
            this.controllerName = controllerName;
            this.channelLight = channelLight;
        }

        private void FrmLightName_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(channelLight.LightName))
            {
                tbLightName.Text = channelLight.LightName;
                tbLightName.SelectAll();
            }
            tbLightName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbLightName.CheckNotWhiteSpace("请输入光源名称")) return;

            string lightname = tbLightName.Text.Trim();

            controllerRepo.BindLight(controllerName, channelLight.ChannelIndex, tbLightName.Text.Trim());

            channelLight.LightName = lightname;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tbLightName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
    }
}
