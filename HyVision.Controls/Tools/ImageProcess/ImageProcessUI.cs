using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.ComponentModel;

using GL.Kit.Log;
using RestSharp;
using HyVision.Models;

namespace HyVision.Tools.ImageProcess
{
    //public partial class ImageProcessUI : UserControl
    public partial class ImageProcessUI : BaseHyUserToolEdit<ImageProcessTool>
    {
        private ImageProcessTool ImageProcess;
        private IGLog log;

        public ImageProcessUI()
        {
            InitializeComponent();
            if (log == null)
                log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (ImageProcess == null)
                    ImageProcess = new ImageProcessTool();

                ImageProcess.UseResize = cbxResize.Checked;
                ImageProcess.UseRotation = cbxRotation.Checked;
                ImageProcess.UseQuality = cbxQuality.Checked;
                ImageProcess.UseSaveImage = cbxSaveImage.Checked;
                ImageProcess.UseSaveNgImage = cbxSaveNgImage.Checked;
                ImageProcess.UseSendImage = cbxSendImage.Checked;
                ImageProcess.UseSendResult = cbxSendResult.Checked;
                ImageProcess.UseTestMode = cbxTestMode.Checked;
                ImageProcess.Width = (int)nudWidth.Value;
                ImageProcess.Height = (int)nudHeight.Value;
                ImageProcess.Quality = (int)nudQuality.Value;

                if (!string.IsNullOrEmpty(tbxSaveImageAddress.Text) && Directory.Exists(tbxSaveImageAddress.Text))
                    ImageProcess.SaveImageFolder = tbxSaveImageAddress.Text;
                if (!string.IsNullOrEmpty(tbxSaveNgImage.Text) && Directory.Exists(tbxSaveNgImage.Text))
                    ImageProcess.SaveNgImageFolder = tbxSaveNgImage.Text;
                if (!string.IsNullOrEmpty(tbxIP.Text) && IPAddress.TryParse(tbxIP.Text, out IPAddress ipAddress))
                    ImageProcess.IP = tbxIP.Text;
                if (!string.IsNullOrEmpty(tbxPort.Text) && int.TryParse(tbxPort.Text, out int iPort))
                    ImageProcess.Port = tbxPort.Text;

                if (!string.IsNullOrEmpty(tbxTcpIPAddress.Text) && IPAddress.TryParse(tbxTcpIPAddress.Text, out IPAddress tcpAddress))
                    ImageProcess.TcpIP = tbxTcpIPAddress.Text;
                if (!string.IsNullOrEmpty(tbxTcpPort.Text) && int.TryParse(tbxTcpPort.Text, out int tcpPort))
                    ImageProcess.TcpPort = tbxTcpPort.Text;
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }

        public override void Save()
        {
            try
            {
                UpdateDataToObject();
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageProcessTool Subject
        {
            get { return ImageProcess; }
            set
            {
                if (!object.Equals(ImageProcess, value))
                {
                    ImageProcess = value;
                    hyTCEInput.Subject = ImageProcess.Inputs;

                    cbxResize.Checked = ImageProcess.UseResize;
                    cbxRotation.Checked = ImageProcess.UseRotation;
                    cbxQuality.Checked = ImageProcess.UseQuality;
                    cbxSaveImage.Checked = ImageProcess.UseSaveImage;
                    cbxSaveNgImage.Checked = ImageProcess.UseSaveNgImage;
                    cbxSendImage.Checked = ImageProcess.UseSendImage;
                    cbxSendResult.Checked = ImageProcess.UseSendResult;
                    cbxTestMode.Checked = ImageProcess.UseTestMode;
                    nudWidth.Value = ImageProcess.Width;
                    nudHeight.Value = ImageProcess.Height;
                    nudQuality.Value = ImageProcess.Quality;
                    tbxIP.Text = ImageProcess.IP;
                    tbxPort.Text = ImageProcess.Port;
                    tbxSaveImageAddress.Text = ImageProcess.SaveImageFolder;
                    tbxSaveNgImage.Text = ImageProcess.SaveNgImageFolder;

                    tbxTcpIPAddress.Text = ImageProcess.TcpIP;
                    tbxTcpPort.Text = ImageProcess.TcpPort;

                    // 自动添加输入参数
                    foreach ((string name, Type type) in ConstField.GetProperties())
                    {
                        HyTerminal input = new HyTerminal(name, type);
                        input.GUID = Guid.NewGuid().ToString("N");
                        if (!ImageProcess.Inputs.Contains(name))
                            ImageProcess.Inputs.Add(input);
                    }
                }
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbxSaveImageAddress.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSelectNGFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbxSaveNgImage.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxIP.Text) || !IPAddress.TryParse(tbxIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("请输入正确的IP地址");
                return;
            }

            if (string.IsNullOrEmpty(tbxPort.Text) || !int.TryParse(tbxPort.Text, out int iPort))
            {
                MessageBox.Show("请输入正确的端口号");
                return;
            }

            RestClient client = new RestClient(string.Format("http://{0}:{1}/v1/inspect", ipAddress, iPort));

            try
            {
                var request = new RestRequest("", Method.Post);
                request.Timeout = -1;
                request.AddParameter("serialnumber", "ABCDEF1234");
                request.AddParameter("vendor", "LEAD");
                request.AddParameter("contents", "LCM-1-1-R-univieral");

                RestResponse response = client.Execute(request);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                    MessageBox.Show("连接成功");
                else
                    MessageBox.Show(string.Format("连接失败, 错误码：{0}", response?.StatusCode));
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message, ex);
            }
            finally
            {
                client.Dispose();
            }
        }

        private void cbxResize_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxResize.Checked)
            {
                lblWidth.Enabled = true;
                lblHeight.Enabled = true;
                nudWidth.Enabled = true;
                nudHeight.Enabled = true;
            }
            else
            {
                lblWidth.Enabled = false;
                lblHeight.Enabled = false;
                nudWidth.Enabled = false;
                nudHeight.Enabled = false;
            }

            ImageProcess.UseResize = cbxResize.Checked;
        }

        private void cbxQuality_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxQuality.Checked)
            {
                lblQuality.Enabled = true;
                nudQuality.Enabled = true;
            }
            else
            {
                lblQuality.Enabled = false;
                nudQuality.Enabled = false;
            }

            ImageProcess.UseQuality = cbxQuality.Checked;
        }

        private void cbxSaveImage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSaveImage.Checked)
            {
                tabImageSave.SelectedIndex = 0;
            }

            ImageProcess.UseSaveImage = cbxSaveImage.Checked;
        }

        private void cbxSaveNgImage_CheckedChanged(object sender, EventArgs e)
        {
            if(cbxSaveNgImage.Checked)
            {
                tabImageSave.SelectedIndex = 1;
                cbxSendImage.Checked = true;
            }

            ImageProcess.UseSaveNgImage = true;
        }

        private void cbxSendImage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSendImage.Checked)
            {
                tabSend.SelectedIndex = 0;
                tabSendImage.Enabled = true;
                nudQuality.Value = 75;
            }
            else
            {
                tabSendImage.Enabled = false;
                nudQuality.Value = 0;
            }

            cbxQuality.Checked = cbxSendImage.Checked;
            ImageProcess.UseSendImage = cbxSendImage.Checked;
        }

        private void cbxSendResult_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSendResult.Checked)
            {
                tabSend.SelectedIndex = 1;
                tabSendResult.Enabled = true;
                cbxSendImage.Checked = true;
            }
            else
            {
                tabSendResult.Enabled = false;
            }

            ImageProcess.UseSendResult = cbxSendResult.Checked;
        }

        private void cbxTestMode_CheckedChanged(object sender, EventArgs e)
        {
            ImageProcess.UseTestMode = cbxTestMode.Checked;
        }

        private void btnTcpConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxTcpIPAddress.Text) || !IPAddress.TryParse(tbxTcpIPAddress.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("不是有效的IP地址");
                return;
            }

            if (string.IsNullOrEmpty(tbxTcpPort.Text) || !int.TryParse(tbxTcpPort.Text, out int iPort))
            {
                MessageBox.Show("不是有效的端口");
                return;
            }

            TcpClient client = new TcpClient();

            try
            {
                client.Connect(ipAddress, iPort);
                if (client.Connected)
                    MessageBox.Show("连接成功");
                else
                    MessageBox.Show("连接失败");
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message, ex);
            }
            finally
            {
                if (client.Connected)
                    client.Close();
                client.Dispose();
            }
        }
    }
}
