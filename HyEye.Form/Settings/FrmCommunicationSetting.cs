using GL.Kit.Net;
using GL.Kit.Net.Sockets;
using HyEye.API.Repository;
using HyEye.Models.VO;
using HyEye.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmCommunicationSetting : Form
    {
        readonly ICommunicationRepository communicationRepo;
        readonly ICommunicationService commService;

        public FrmCommunicationSetting(bool readOnly,
            ICommunicationRepository communicationRepo,
            ICommunicationService commService)
        {
            InitializeComponent();

            tbServerIP.AddEvent_KeyPress_InputNumberAndPoint();
            tbPort.AddEvent_KeyPress_InputNumber();

            this.communicationRepo = communicationRepo;
            this.commService = commService;

            if (commService.Running)
            {
                pnlTcp.Enabled = false;
                pnlBottom.Enabled = true;
            }
            else
            {
                pnlTcp.Enabled = true;
                pnlBottom.Enabled = false;
            }
        }

        private void FrmCommunicationSetting_Load(object sender, EventArgs e)
        {
            CommunicationInfoVO communicationInfo = communicationRepo.GetCommunicationInfo();
            if (communicationInfo == null)
            {
                communicationInfo = new CommunicationInfoVO
                {
                    CommProtocol = CommProtocol.TCP,
                    ConnectionMethod = ConnectionMethod.Server
                };
            }

            if (communicationInfo.CommProtocol == CommProtocol.TCP)
            {
                rdbtnTcp.Checked = true;

                if (communicationInfo.ConnectionMethod == ConnectionMethod.Server)
                    rdoServer.Checked = true;
                else
                    rdoClient.Checked = true;
            }
            else
            {
                rdbtnPLC.Checked = true;
            }

            tbServerIP.Text = communicationInfo.Network?.IP;
            tbPort.Text = (communicationInfo.Network?.Port)?.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbServerIP.CheckNotEmpty("请输入 IP")
                || !tbServerIP.CheckRegex(ComPattern.IpPattern, "IP 格式错误")
                || !tbPort.CheckNotEmpty("请输入端口号"))
            {
                return;
            }

            CommunicationInfoVO communicationInfo = new CommunicationInfoVO();
            if (rdbtnTcp.Checked)
            {
                communicationInfo.CommProtocol = CommProtocol.TCP;

                communicationInfo.ConnectionMethod = rdoServer.Checked ? ConnectionMethod.Server : ConnectionMethod.Client;
            }
            else
            {
                communicationInfo.CommProtocol = CommProtocol.PLC;
            }

            communicationInfo.Network = new NetworkInfo
            {
                IP = tbServerIP.Text,
                Port = tbPort.IntValue(),
            };

            communicationRepo.SetCommunicationInfo(communicationInfo);
            communicationRepo.Save();

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void rdbtnTcp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnTcp.Checked)
            {
                rdoServer.Visible = true;
                rdoClient.Visible = true;

                if (rdoServer.Checked)
                {
                    label1.Text = "设置IP：";
                    label2.Text = "监听端口：";
                }
                else
                {
                    label1.Text = "服务端IP：";
                    label2.Text = "端口号：";
                }
            }
        }

        private void rdbtnPLC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnPLC.Checked)
            {
                rdoServer.Visible = false;
                rdoClient.Visible = false;

                label1.Text = "PLC IP：";
                label2.Text = "PLC 端口：";
            }
        }

        private void rdoServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoServer.Checked)
            {
                label1.Text = "设置IP：";
                label2.Text = "监听端口：";
            }
        }

        private void rdoClient_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoClient.Checked)
            {
                label1.Text = "服务端IP：";
                label2.Text = "端口号：";
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            commService.Stop();
            pnlTcp.Enabled = true;
            btnDisConnect.Enabled = false;
        }

    }
}
