using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VisionSDK._VisionPro
{
    public class VisionProDisplayImageComponent : IDisplayImageComponent
    {
        static Color BackColor { get; } = Color.Gray;

        public event EventHandler ShowSelectOutput;

        protected readonly CogRecordDisplay display;

        public Control DisplayedControl
        {
            get
            {
                display.BackColor = BackColor;
                return display;
            }
        }

        public VisionProDisplayImageComponent()
        {
            display = new CogRecordDisplay
            {
                Dock = DockStyle.Fill
            };
            display.DoubleClick += CogDisplay_DoubleClick;

            display.ContextMenuStrip.Items.RemoveAt(4);
            display.ContextMenuStrip.Items.RemoveAt(4);
            display.ContextMenuStrip.Items.RemoveAt(4);
            display.ContextMenuStrip.Items.RemoveAt(4);
            display.ContextMenuStrip.Items.RemoveAt(4);
            display.ContextMenuStrip.Items.RemoveAt(4);
            display.ContextMenuStrip.Items.RemoveAt(15);
            display.ContextMenuStrip.Items.RemoveAt(15);

            ToolStripMenuItem tsmiClearImage = new ToolStripMenuItem("清空图片");
            tsmiClearImage.Click += TsmiClearImage_Click;
            display.ContextMenuStrip.Items.Add(tsmiClearImage);
        }

        private void TsmiClearImage_Click(object sender, EventArgs e)
        {
            display.Image = null;
        }

        private void CogDisplay_DoubleClick(object sender, EventArgs e)
        {
            ShowSelectOutput?.Invoke(this, e);
        }

        public bool Visible
        {
            get { return display.Visible; }
            set { display.Visible = value; }
        }

        public void ShowImage(object image, bool clearGraphic = true, bool fit = true)
        {
            if (image == null) return;

            if (clearGraphic)
            {
                display.InteractiveGraphics.Clear();
                display.StaticGraphics.Clear();
            }

            //DG Image => NullReferenceException

            //if (cogDisplay.Image is IDisposable dis)
            //{
            //    dis?.Dispose();
            //}

            if (image is ICogImage cogImage)
            {
                display.Image = cogImage;
            }
            else
            {
                Bitmap bitmap = image as Bitmap;

                display.Image = VisionProUtils.ToCogImage(bitmap);

                bitmap.Dispose();
            }
            if (fit)
                display.Fit();
        }

        public void ShowGraphicAsync(object graphicCollection)
        {
            if (graphicCollection == null) return;

            Action action = () =>
            {
                showGraphic(graphicCollection);
            };

            display.BeginInvoke(action);
        }

        public void ShowGraphic(object graphicCollection)
        {
            // 这里的异步，是因为 showGraphic 中调用了禁用重绘等方法
            Action action = () =>
            {
                showGraphic(graphicCollection);
            };

            IAsyncResult async = display.BeginInvoke(action);

            display.EndInvoke(async);
        }

        public Bitmap CreateContentBitmap()
        {
            Bitmap bitmap = null;
            try
            {
                // Custom 和 Image 图像的大小是一样的
                bitmap = (Bitmap)display.CreateContentBitmap(CogDisplayContentBitmapConstants.Image);
            }
            catch (COMException e) when (e.Message == "Unable to create the requested DIB section.")
            {

            }
            catch (Exception)
            {
                throw;
            }

            return bitmap;
        }

        void showGraphic(object graphicCollection)
        {
            if (graphicCollection == null) return;

            if (graphicCollection is ICogRecord record)
            {
                if (record.SubRecords.Count > 0)
                {
                    display.SetRedraw(0);

                    display.Record = record;
                    display.Fit();
                    display.BackColor = BackColor;

                    display.SetRedraw(1);
                    display.Refresh();
                }
            }
            else if (graphicCollection is CogCompositeShape shape)
            {
                display.StaticGraphics.Add(shape, "");
            }
            else if (graphicCollection is CogGraphicCollection collection)
            {
                foreach (ICogGraphic gc in collection)
                {
                    display.StaticGraphics.Add(gc, "");
                }
            }
        }

        public void ClearImage()
        {
            if (display != null)
            {
                Action action = () => display.Image = null;

                display.BeginInvoke(action);
            }
        }

        public void ClearGraphic()
        {
            display.InteractiveGraphics.Clear();
            display.StaticGraphics.Clear();
        }

        public void Dispose()
        {
            display?.Dispose();
        }
    }

    public class VisionProDisplayTaskImageComponent : VisionProDisplayImageComponent, IDisplayTaskImageComponent
    {
        public string TaskName { get; set; }

        public string AcqOrCalibName { get; set; }

        public event EventHandler ClearAllImage;

        public VisionProDisplayTaskImageComponent()
        {
            ToolStripMenuItem tsmiClearAllImage = new ToolStripMenuItem("清空所有图片");
            tsmiClearAllImage.Click += TsmiClearAllImage_Click; ;
            display.ContextMenuStrip.Items.Add(tsmiClearAllImage);
        }

        private void TsmiClearAllImage_Click(object sender, EventArgs e)
        {
            ClearAllImage?.Invoke(sender, EventArgs.Empty);
        }
    }
}
