using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionSDK;
using HyDisplayWindow;
using HyRoiManager;
using HyRoiManager.ROI;




namespace HyVision.SDK
{
    public class HyVisionViewerComponent : IDisplayImageComponent
    {
        public event EventHandler ShowSelectOutput;

        public Control DisplayedControl => ImageViewer;

        public bool Visible
        {
            get { return ImageViewer.Visible; }
            set
            {
                ImageViewer.Visible = value;
            }
        }


        protected HyImageViewer ImageViewer;
        protected HyRoiController RoiManager;



        public HyVisionViewerComponent()
        {
            ImageViewer = new HyImageViewer();
            RoiManager = new HyRoiController(ImageViewer);

            ImageViewer.MouseDoubleClick += ImageViewer_MouseDoubleClick;
        }

        private void ImageViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowSelectOutput?.Invoke(this, e);
        }

        public void ClearGraphic()
        {
            RoiManager.HyROIs.Clear();
            RoiManager.ClearRoiImageRoiData();
        }

        public void ClearImage()
        {
            ImageViewer.ClearImage();
        }

        public Bitmap CreateContentBitmap()
        {
            return RoiManager.GetRoiImage();
        }

        public void Dispose()
        {
            ImageViewer.ClearImage();
            RoiManager.ClearImage();
        }

        public void ShowGraphic(object graphicCollection)
        {
            try
            {
                if (graphicCollection.GetType() == typeof(RoiData))
                {
                    RoiData Roidata = graphicCollection as RoiData;
                    RoiManager.DisplayRoiData(Roidata, false);
                    ImageViewer.Invalidate();
                }
                else if (graphicCollection.GetType() == typeof(BaseHyROI))
                {
                    // 保留
                }
                else if (graphicCollection.GetType() == typeof(List<BaseHyROI>))
                {
                    List<BaseHyROI> LstRois = graphicCollection as List<BaseHyROI>;
                    RoiManager.DisplayHyRoi(LstRois, false);
                    ImageViewer.Invalidate();
                }
            }
            catch (Exception e)
            {

            }
        }

        public void ShowGraphicAsync(object graphicCollection)
        {
            ShowGraphic(graphicCollection);
        }

        public void ShowImage(object image, bool clearGraphic = true, bool fit = true)
        {
            if (clearGraphic == true)
            {
                ClearGraphic();
                //RoiManager.ClearRoiImageRoiData();
            }
            ImageViewer.DisplayImage(image as Bitmap, fit);
        }
    }

    public class HyVisionViewerTaskImageComponent : HyVisionViewerComponent, IDisplayTaskImageComponent
    {
        public event EventHandler ClearAllImage;

        public string TaskName { get; set; }

        public string AcqOrCalibName { get; set; }



        public HyVisionViewerTaskImageComponent()
        {
            ToolStripMenuItem tsmiClearAllImage = new ToolStripMenuItem("清空所有图片");
            tsmiClearAllImage.Click += TsmiClearAllImage_Click;

            ImageViewer.RightClickMenu.Items.Add(tsmiClearAllImage);
        }

        private void TsmiClearAllImage_Click(object sender, EventArgs e)
        {
            ClearAllImage?.Invoke(sender, EventArgs.Empty);
        }

    }
}
