using GL.Kit.Net;
using GL.Kit.Net.Sockets;
using HyEye.API.Repository;
using HyEye.Models.VO;
using LightControllerSDK;
using LightControllerSDK.Models;
using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmLightControllerInfo : Form
    {
        readonly ILightControllerRepository controllerRepo;
        readonly LightControllerInfoVO showData;

        public LightControllerInfoVO Data { get; private set; }

        public FrmLightControllerInfo(ILightControllerRepository controllerRepo, LightControllerInfoVO controllerInfo)
        {
            InitializeComponent();

            this.controllerRepo = controllerRepo;
            this.showData = controllerInfo;

            tbIP.AddEvent_KeyPress_InputNumberAndPoint();
            tbPort.AddEvent_KeyPress_InputNumber();
        }

        private void FrmLightControllerSetting_Load(object sender, EventArgs e)
        {
            LoadLightControllerBrand();

            LoadCom();
            LoadBaudRate();
            LoadDataBits();
            LoadParity();
            LoadStopBits();

            if (showData != null)
            {
                cmbControllerBrand.SelectedItem = showData.Brand;
                cmbProtocol.SelectedItem = showData.CommProtocol;
                tbLightControllerName.Text = showData.Name;
                nudChannelCount.Value = showData.ChannelCount;

                if (showData.NetworkInfo != null)
                {
                    tbIP.Text = showData.NetworkInfo.IP;
                    tbPort.Text = showData.NetworkInfo.Port.ToString();
                }
                if (showData.SerialInfo != null)
                {
                    cmbCom.SelectedItem = showData.SerialInfo.PortName;
                    cmbBaudRate.SelectedItem = showData.SerialInfo.BaudRate;
                    cmbDataBits.SelectedItem = showData.SerialInfo.DataBits;
                    cmbParity.SelectedItem = showData.SerialInfo.Parity;
                    if (showData.SerialInfo.StopBits == StopBits.One)
                        cmbStopBits.SelectedIndex = 0;
                    else if (showData.SerialInfo.StopBits == StopBits.OnePointFive)
                        cmbStopBits.SelectedIndex = 1;
                    else if (showData.SerialInfo.StopBits == StopBits.Two)
                        cmbStopBits.SelectedIndex = 2;
                }
            }
            else
            {
                cmbControllerBrand.SelectedIndex = 0;

                cmbBaudRate.SelectedItem = 9600;
                cmbDataBits.SelectedItem = 8;
                cmbParity.SelectedItem = Parity.None;
                cmbStopBits.SelectedItem = 1;
            }
        }

        private void cmbLightControllerBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbControllerBrand.SelectedIndex == -1) return;

            LoadProtocol();

            cmbProtocol.SelectedIndex = 0;
        }

        #region 加载

        // 加载光源控制器品牌
        void LoadLightControllerBrand()
        {
            foreach (LightControllerBrand brand in Enum.GetValues(typeof(LightControllerBrand)))
            {
                cmbControllerBrand.Items.Add(brand);
            }
        }

        void LoadProtocol()
        {
            cmbProtocol.Items.Clear();
            foreach (CommProtocol protocol in ControllerCreater.GetBrandProtocol((LightControllerBrand)cmbControllerBrand.SelectedItem))
            {
                cmbProtocol.Items.Add(protocol);
            }
        }

        void LoadCom()
        {
            string[] serialPortName = System.IO.Ports.SerialPort.GetPortNames();

            foreach (string name in serialPortName)
            {
                cmbCom.Items.Add(name);
            }
        }

        void LoadBaudRate()
        {
            cmbBaudRate.Items.AddRange(new object[]
            {
                9600,
                19200,
                38400,
                115200
            });
        }

        void LoadDataBits()
        {
            cmbDataBits.Items.AddRange(new object[]
            {
                5,
                6,
                7,
                8
            });
        }

        void LoadParity()
        {
            cmbParity.BindEnum<Parity>();
        }

        void LoadStopBits()
        {
            cmbStopBits.Items.AddRange(new object[]
            {
                1,
                1.5,
                2
            });
        }

        #endregion

        private void cmbProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProtocol.SelectedIndex == -1) return;

            CommProtocol protocol = (CommProtocol)cmbProtocol.SelectedItem;

            if (protocol == CommProtocol.COM)
            {
                gbCOM.Visible = true;
                gbTCP.Visible = false;

                Height = gbCOM.Location.Y + gbCOM.Height + 100;
            }
            else if (protocol == CommProtocol.TCP || protocol == CommProtocol.UDP)
            {
                gbCOM.Visible = false;
                gbTCP.Visible = true;

                Height = gbTCP.Location.Y + gbTCP.Height + 100;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!check()) return;

            LightControllerInfoVO lightController = buildData();

            if (showData == null)
            {
                controllerRepo.AddController(lightController);

                Data = lightController;
            }
            else
            {
                Data = controllerRepo.UpdateController(showData.Name, lightController);
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        bool check()
        {
            if (!cmbControllerBrand.CheckSelected("请选择光源控制器品牌")
                || !tbLightControllerName.CheckNotWhiteSpace("请输入光源控制器名称"))
            {
                return false;
            }

            CommProtocol protocol = (CommProtocol)cmbProtocol.SelectedItem;
            if (protocol == CommProtocol.COM)
            {
                return cmbCom.CheckSelected("请选择串口");
            }
            else if (protocol == CommProtocol.TCP || protocol == CommProtocol.UDP)
            {
                return tbIP.CheckNotEmpty("请输入 IP 地址")
                    && tbIP.CheckRegex(ComPattern.IpPattern, "无效的 IP 地址")
                    && tbPort.CheckNotEmpty("请输入端口号")
                    && tbPort.CheckIntRange(0, 65565, "无效的端口号");
            }

            return true;
        }

        LightControllerInfoVO buildData()
        {
            LightControllerInfoVO lightController = new LightControllerInfoVO
            {
                Brand = (LightControllerBrand)cmbControllerBrand.SelectedItem,
                CommProtocol = (CommProtocol)cmbProtocol.SelectedItem,
                Name = tbLightControllerName.Text.Trim(),
                ChannelCount = (int)nudChannelCount.Value
            };

            if (lightController.CommProtocol == CommProtocol.COM)
            {
                lightController.SerialInfo = new SerialInfo
                {
                    PortName = cmbCom.Text,
                    BaudRate = int.Parse(cmbBaudRate.Text),
                    DataBits = int.Parse(cmbDataBits.Text),
                    Parity = (Parity)cmbParity.SelectedItem,
                    StopBits = getStopBits()
                };
            }
            else if (lightController.CommProtocol == CommProtocol.TCP || lightController.CommProtocol == CommProtocol.UDP)
            {
                lightController.NetworkInfo = new NetworkInfo
                {
                    IP = tbIP.Text,
                    Port = int.Parse(tbPort.Text)
                };
            }

            return lightController;
        }

        StopBits getStopBits()
        {
            if (cmbStopBits.SelectedIndex == 0)
                return StopBits.One;
            else if (cmbStopBits.SelectedIndex == 1)
                return StopBits.OnePointFive;
            else if (cmbStopBits.SelectedIndex == 2)
                return StopBits.Two;
            else
                return StopBits.None;
        }

        private void nudChannelCount_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
