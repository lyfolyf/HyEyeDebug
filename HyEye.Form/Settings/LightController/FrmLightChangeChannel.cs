using HyEye.API.Repository;
using HyEye.Models.VO;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmLightChangeChannel : Form
    {
        readonly ILightControllerRepository controllerRepo;

        public int Data { get; private set; }

        string controllerName;
        int oldChannelIndex;

        public FrmLightChangeChannel(ILightControllerRepository controllerRepo,
            string controllerName,
            ChannelLightInfoVO[] channelLights,
            int oldChannelIndex)
        {
            InitializeComponent();

            this.controllerRepo = controllerRepo;
            this.controllerName = controllerName;
            this.oldChannelIndex = oldChannelIndex;

            foreach (var channel in channelLights)
            {
                if (channel.ChannelIndex != oldChannelIndex)
                {
                    cmbLightChannel.Items.Add(channel);
                }
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (!cmbLightChannel.CheckSelected("请选择通道")) return;

            int newChannelIndex = (cmbLightChannel.SelectedItem as ChannelLightInfoVO).ChannelIndex;

            controllerRepo.ExchangeChannel(controllerName, oldChannelIndex, newChannelIndex);

            Data = newChannelIndex;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
