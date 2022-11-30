using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.ComponentModel;

using GL.Kit.Log;
using HyVision.Models;

namespace HyVision.Tools.ImageSave
{
    // public partial class ImageSaveToolUI : UserControl
    public partial class ImageSaveToolUI : BaseHyUserToolEdit<ImageSaveTool>
    {
        ImageSaveTool imageSaveTool;
        private LogPublisher log;

        public ImageSaveToolUI()
        {
            InitializeComponent();
            log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (imageSaveTool == null)
                    imageSaveTool = new ImageSaveTool();

                imageSaveTool.Width = (int)nudWidth.Value;
                imageSaveTool.Height = (int)nudHeight.Value;
                imageSaveTool.Quality = (int)nudQuality.Value;

                if (!string.IsNullOrEmpty(tbxSaveImageAddress.Text) && Directory.Exists(tbxSaveImageAddress.Text))
                    imageSaveTool.SaveImageFolder = tbxSaveImageAddress.Text;
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
        public override ImageSaveTool Subject
        {
            get { return imageSaveTool; }
            set
            {
                if (!object.Equals(imageSaveTool, value))
                {
                    imageSaveTool = value;
                    hyTCEInput.Subject = imageSaveTool.Inputs;

                    nudWidth.Value = imageSaveTool.Width;
                    nudHeight.Value = imageSaveTool.Height;
                    nudQuality.Value = imageSaveTool.Quality;
                    tbxSaveImageAddress.Text = imageSaveTool.SaveImageFolder;

                    // 自动添加输入参数
                    foreach ((string name, Type type) in ConstField.GetProperties())
                    {
                        HyTerminal input = new HyTerminal(name, type);
                        input.GUID = Guid.NewGuid().ToString("N");
                        if (!imageSaveTool.Inputs.Contains(name))
                            imageSaveTool.Inputs.Add(input);
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
    }
}
