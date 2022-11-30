using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmLightControllerSearch : Form
    {
        readonly ILightControllerRepository controllerRepo;

        List<LightControllerValueInfoVO> lightControllerValues;

        public FrmLightControllerSearch(
            ILightControllerRepository controllerRepo,
            List<LightControllerValueInfoVO> lightControllerValues)
        {
            InitializeComponent();

            this.controllerRepo = controllerRepo;

            this.lightControllerValues = lightControllerValues;

            tvLightController.HideCheckBox(node => node.Level == 0);
        }

        List<LightControllerInfoVO> lightControllers;

        private void FrmLightControllerSearch_Load(object sender, System.EventArgs e)
        {
            lightControllers = controllerRepo.GetControllerInfos();

            foreach (LightControllerInfoVO lightController in lightControllers)
            {
                TreeNode lightControllerNode = buildLightControllerNode(lightController);
                tvLightController.Nodes.Add(lightControllerNode);
            }

            tvLightController.ExpandAll();
        }

        TreeNode buildLightControllerNode(LightControllerInfoVO lightController)
        {
            TreeNode lightControllerNode = new TreeNode
            {
                Text = lightController.Name,
                Tag = lightController
            };

            LightControllerValueInfoVO lightControllerValue = lightControllerValues?.FirstOrDefault(a => a.LightControllerName == lightController.Name);
            if (lightControllerValue != null)
            {
                lightControllerNode.Checked = true;
            }

            foreach (ChannelLightInfoVO channelLight in lightController.Channels)
            {
                TreeNode channelNode = new TreeNode
                {
                    Text = channelLight.ToString(),
                    Tag = channelLight
                };

                if (lightControllerValue != null && lightControllerValue.ChannelValues.Any(a => a.ChannelIndex == channelLight.ChannelIndex))
                {
                    channelNode.Checked = true;
                }
                lightControllerNode.Nodes.Add(channelNode);
            }

            return lightControllerNode;
        }

        public List<LightControllerValueInfoVO> Data { get; private set; }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            List<LightControllerValueInfoVO> lightControllerValues = new List<LightControllerValueInfoVO>();

            foreach (TreeNode controllerNode in tvLightController.Nodes)
            {
                LightControllerValueInfoVO newControllerValue = new LightControllerValueInfoVO { LightControllerName = controllerNode.Text };

                LightControllerValueInfoVO oldControllerValue = lightControllerValues.FirstOrDefault(a => a.LightControllerName == newControllerValue.LightControllerName);

                foreach (TreeNode lightNode in controllerNode.Nodes)
                {
                    if (lightNode.Checked)
                    {
                        ChannelLightInfoVO lightInfo = lightNode.Tag as ChannelLightInfoVO;

                        ChannelValue? channelValue = oldControllerValue?.ChannelValues.FirstOrDefault(a => a.ChannelIndex == lightInfo.ChannelIndex);
                        if (channelValue != null)
                            newControllerValue.ChannelValues.Add(channelValue.Value);
                        else
                            newControllerValue.ChannelValues.Add(new ChannelValue { ChannelIndex = lightInfo.ChannelIndex });
                    }
                }

                if (newControllerValue.ChannelValues.Count > 0)
                    lightControllerValues.Add(newControllerValue);
            }

            Data = lightControllerValues;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
