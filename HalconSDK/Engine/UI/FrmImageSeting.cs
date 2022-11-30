using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconSDK.Engine.BL;



namespace HalconSDK.Engine.UI
{
    public partial class FrmImageSeting : Form
    {
        private SettingsBL ImageSetting;


        public FrmImageSeting(SettingsBL ImageSetting)
        {
            this.ImageSetting = ImageSetting;
            InitializeComponent();
        }

        private void FrmImageSeting_Load(object sender, EventArgs e)
        {        
            if (!string.IsNullOrEmpty(ImageSetting.ImagePath))
            {
                tbImagePath.Text = ImageSetting.ImagePath;

                if (ImageSetting.InputImage != null && ImageSetting.InputImage.Image != null)
                {
                    hyImageDisplayControl1.DisplayImage(ImageSetting.InputImage.Image);
                }
            }
        }

        private void btnOpenHalconFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择图片";
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "图片(*.jpg;*.png;*.gif;*.bmp;*.jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bmp = new Bitmap(openFileDialog.FileName);

                    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    Bitmap OutputImage = bmp.Clone(rect, PixelFormat.Format8bppIndexed);

                    tbImagePath.Text = openFileDialog.FileName;
                    ImageSetting.ImagePath = openFileDialog.FileName;
                    hyImageDisplayControl1.DisplayImage(OutputImage);
                    ImageSetting.InputImage = new HyVision.Models.HyImage(OutputImage);
                }

                openFileDialog.Dispose();
            }
            catch
            {
                MessageBox.Show("导入图片有误，请重新导入！", "导入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public string GetImagePath()
        //{
        //    return ImagePath;
        //}

    }
}
