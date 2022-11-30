using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisionSDK
{
    public interface IDisplayImageComponent : IDisposable
    {
        event EventHandler ShowSelectOutput;

        Control DisplayedControl { get; }

        bool Visible { get; set; }

        void ShowImage(object image, bool clearGraphic = true, bool fit = true);

        void ShowGraphic(object graphicCollection);

        Bitmap CreateContentBitmap();

        void ShowGraphicAsync(object graphicCollection);

        void ClearImage();

        void ClearGraphic();
    }

    public interface IDisplayTaskImageComponent : IDisplayImageComponent
    {
        string TaskName { get; set; }

        string AcqOrCalibName { get; set; }

        event EventHandler ClearAllImage;
    }
}
