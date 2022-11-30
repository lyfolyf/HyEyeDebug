using HyVision.Tools.ImageDisplay;
using System;
using System.Drawing;
using System.Windows.Forms;
using VisionSDK;

namespace HyVision.SDK
{
    public class HyVisionDisplayComponent : IDisplayImageComponent
    {
        public event EventHandler DoubleClick;
        public event EventHandler ShowSelectOutput;

        public Control DisplayedControl => display;

        protected readonly HyImageDisplayControl display;

        public bool Visible
        {
            get => display.Visible;
            set => display.Visible = value;
        }

        public HyVisionDisplayComponent()
        {
            display = new HyImageDisplayControl
            {
                Dock = DockStyle.Fill,
                TopToolVisible = false
            };

            display.Controls["HyDisplayPanel"].DoubleClick += HyVisionDisplayComponent_DoubleClick;
        }

        private void HyVisionDisplayComponent_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick?.Invoke(this, e);
        }

        public void ShowImage(object image, bool clearGraphic = true, bool fit = true)
        {
            if (image == null) return;

            Bitmap bitmap = image as Bitmap;

            display.DisplayImage(bitmap);

            bitmap.Dispose();
        }

        public void ClearImage()
        {
            display.ClearImage();
        }

        public Bitmap CreateContentBitmap()
        {
            return null;
        }

        public void ShowGraphic(object graphicCollection)
        {

        }

        public void ShowGraphicAsync(object graphicCollection)
        {

        }

        public void ClearGraphic()
        {

        }

        public void Dispose()
        {
            display.Dispose();
        }
    }

    public class HyVisionDisplayTaskImageComponent : HyVisionDisplayComponent, IDisplayTaskImageComponent
    {
        public string TaskName { get; set; }

        public string AcqOrCalibName { get; set; }

        public event EventHandler ClearAllImage;

        public HyVisionDisplayTaskImageComponent()
        {
            ToolStripMenuItem tsmiClearAllImage = new ToolStripMenuItem("清空所有图片");
            tsmiClearAllImage.Click += TsmiClearAllImage_Click;

            //display.Controls["HyDisplayPanel"].ContextMenuStrip.Items.Add(tsmiClearAllImage);
        }

        private void TsmiClearAllImage_Click(object sender, EventArgs e)
        {
            ClearAllImage?.Invoke(sender, EventArgs.Empty);
        }
    }
}
