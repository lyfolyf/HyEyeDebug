using Autofac;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmLightControllerSetting : Form
    {
        readonly ILightControllerRepository controllerRepo;

        public FrmLightControllerSetting(bool readOnly, ILightControllerRepository controllerRepo)
        {
            InitializeComponent();

            pnlTop.Enabled = !readOnly;

            this.controllerRepo = controllerRepo;
        }

        List<LightControllerInfoVO> controllerInfos;

        private void FrmLightControllerSetting_Load(object sender, EventArgs e)
        {
            controllerInfos = controllerRepo.GetControllerInfos();

            foreach (LightControllerInfoVO lightController in controllerInfos)
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

            foreach (ChannelLightInfoVO channelLight in lightController.Channels)
            {
                TreeNode channelNode = new TreeNode
                {
                    Text = channelLight.ToString(),
                    Tag = channelLight
                };

                lightControllerNode.Nodes.Add(channelNode);
            }

            return lightControllerNode;
        }

        private void btnAddController_Click(object sender, EventArgs e)
        {
            AddController();
        }

        private void btnDeleteController_Click(object sender, EventArgs e)
        {
            DeleteController();
        }

        private void tsmiAddLightController_Click(object sender, EventArgs e)
        {
            AddController();
        }

        void AddController()
        {
            FrmLightControllerInfo frm
                = AutoFacContainer.Resolve<FrmLightControllerInfo>(new TypedParameter(typeof(LightControllerInfoVO), null));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LightControllerInfoVO lightController = frm.Data;

                controllerInfos.Add(lightController);

                TreeNode lightControllerNode = buildLightControllerNode(lightController);

                tvLightController.Nodes.Add(lightControllerNode);
                tvLightController.SelectedNode = lightControllerNode;

                lightControllerNode.Expand();
            }
        }

        void DeleteController()
        {
            if (tvLightController.SelectedNode == null) return;

            if (MessageBoxUtils.ShowQuestion("确定要删除该光源控制器吗？") == DialogResult.No) return;

            TreeNode node = tvLightController.SelectedNode.Level == 0 ? tvLightController.SelectedNode : tvLightController.SelectedNode.Parent;

            LightControllerInfoVO lightController = node.Tag as LightControllerInfoVO;

            controllerRepo.DeleteController(lightController.Name);

            controllerInfos.Remove(lightController);

            node.Remove();
        }

        private void tsmiUpdateLightController_Click(object sender, EventArgs e)
        {
            TreeNode node = tvLightController.SelectedNode.Level == 0 ? tvLightController.SelectedNode : tvLightController.SelectedNode.Parent;

            LightControllerInfoVO src = node.Tag as LightControllerInfoVO;

            FrmLightControllerInfo frm
                = AutoFacContainer.Resolve<FrmLightControllerInfo>(new TypedParameter(typeof(LightControllerInfoVO), src));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LightControllerInfoVO lightController = frm.Data;

                controllerInfos.Remove(src);
                controllerInfos.Add(lightController);

                int index = node.Index;
                node.Remove();

                TreeNode lightControllerNode = buildLightControllerNode(lightController);
                tvLightController.Nodes.Insert(index, lightControllerNode);
                tvLightController.SelectedNode = lightControllerNode;

                lightControllerNode.Expand();
            }
        }

        private void tsmiDeleteLightController_Click(object sender, EventArgs e)
        {
            DeleteController();
        }

        private void tsmiBindLight_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvLightController.SelectedNode;
            string controllerName = selNode.Parent.Text;
            ChannelLightInfoVO channelLight = selNode.Tag as ChannelLightInfoVO;

            FrmLightName frm = AutoFacContainer.Resolve<FrmLightName>(
                    new NamedParameter("controllerName", controllerName),
                    new NamedParameter("channelLight", channelLight));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selNode.Text = channelLight.ToString();
            }
        }

        private void tsmiRenameLight_Click(object sender, EventArgs e)
        {
            tsmiBindLight_Click(sender, e);
        }

        private void tsmiChangeChannel_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvLightController.SelectedNode;
            string controllerName = selNode.Parent.Text;

            ChannelLightInfoVO[] channelLights = (selNode.Parent.Tag as LightControllerInfoVO).Channels;

            int oldChannelIndex = (selNode.Tag as ChannelLightInfoVO).ChannelIndex;

            FrmLightChangeChannel frm = AutoFacContainer.Resolve<FrmLightChangeChannel>(
                    new NamedParameter("controllerName", controllerName),
                    new NamedParameter("channelLights", channelLights),
                    new NamedParameter("oldChannelIndex", oldChannelIndex));

            if (frm.ShowDialog() == DialogResult.OK)
            {
                TreeNode newNode = selNode.Parent.Nodes[frm.Data - 1];

                ChannelLightInfoVO oldLightInfo = selNode.Tag as ChannelLightInfoVO;
                ChannelLightInfoVO newLightInfo = newNode.Tag as ChannelLightInfoVO;

                string temp = oldLightInfo.LightName;
                oldLightInfo.LightName = newLightInfo.LightName;
                newLightInfo.LightName = temp;

                selNode.Text = oldLightInfo.ToString();
                newNode.Text = newLightInfo.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            controllerRepo.Save();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode selNode = tvLightController.SelectedNode;

            if (selNode == null)
            {
                SetLightControllerMenu(true, false);
                SetLightMenu(false, false);
            }
            else if (selNode.Level == 0)
            {
                SetLightControllerMenu(true, true);
                SetLightMenu(false, false);
            }
            else if (selNode.Level == 1)
            {
                SetLightControllerMenu(true, true);
                SetLightMenu(true, string.IsNullOrEmpty((selNode.Tag as ChannelLightInfoVO).LightName));
            }

            contextMenuStrip1.Enabled = pnlTop.Enabled;
        }

        void SetLightControllerMenu(bool visible, bool enabled)
        {
            tsmiAddLightController.Visible = visible;
            tsmiUpdateLightController.Visible = visible;
            tsmiDeleteLightController.Visible = visible;

            tsmiUpdateLightController.Enabled = enabled;
            tsmiDeleteLightController.Enabled = enabled;
        }

        void SetLightMenu(bool visible, bool enabled)
        {
            toolStripSeparator1.Visible = visible;
            tsmiBindLight.Visible = visible;
            tsmiChangeChannel.Visible = visible;

            tsmiChangeChannel.Enabled = !enabled;
        }

        private void FrmLightControllerSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (controllerRepo.Changed && MessageBoxUtils.ShowQuestion("光源控制器配置已更改，是否保存？") == DialogResult.Yes)
            {
                controllerRepo.Save();
            }
        }

    }
}
