using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.ComponentModel;

using GL.Kit.Log;
using HyVision.Tools;
using HyVision.Models;

namespace ImageProcess
{
    // public partial class ImageStitchUI : UserControl
    public partial class ImageStitchUI : BaseHyUserToolEdit<ImageStitchTool>
    {
        ImageStitchTool imageStitchTool;
        private LogPublisher log;

        public ImageStitchUI()
        {
            InitializeComponent();
            log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (imageStitchTool == null)
                    imageStitchTool = new ImageStitchTool();

                imageStitchTool.Width = (int)nudWidth.Value;
                imageStitchTool.Height = (int)nudHeight.Value;
                imageStitchTool.Quality = (int)nudQuality.Value;

                if (!string.IsNullOrEmpty(tbxSaveImageAddress.Text) && Directory.Exists(tbxSaveImageAddress.Text))
                    imageStitchTool.SaveImageFolder = tbxSaveImageAddress.Text;
                if (!string.IsNullOrEmpty(tbxIP.Text) && IPAddress.TryParse(tbxIP.Text, out IPAddress ipAddress))
                    imageStitchTool.IP = tbxIP.Text;
                if (!string.IsNullOrEmpty(tbxPort.Text) && int.TryParse(tbxPort.Text, out int iPort))
                    imageStitchTool.Port = tbxPort.Text;
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
        public override ImageStitchTool Subject
        {
            get { return imageStitchTool; }
            set
            {
                if (!object.Equals(imageStitchTool, value))
                {
                    imageStitchTool = value;
                    hyTCEInput.Subject = imageStitchTool.Inputs;

                    nudWidth.Value = imageStitchTool.Width;
                    nudHeight.Value = imageStitchTool.Height;
                    nudQuality.Value = imageStitchTool.Quality;
                    tbxIP.Text = imageStitchTool.IP;
                    tbxPort.Text = imageStitchTool.Port;
                    tbxSaveImageAddress.Text = imageStitchTool.SaveImageFolder;

                    // 自动添加输入参数
                    HyTerminal input = new HyTerminal(ConstField.CAMERA_NO, typeof(string));
                    input.GUID = Guid.NewGuid().ToString("N");
                    if (!imageStitchTool.Inputs.Contains(ConstField.CAMERA_NO))
                        imageStitchTool.Inputs.Add(input);
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxIP.Text) || !IPAddress.TryParse(tbxIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("不是有效的IP地址");
                return;
            }

            if (string.IsNullOrEmpty(tbxPort.Text) || !int.TryParse(tbxPort.Text, out int iPort))
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
