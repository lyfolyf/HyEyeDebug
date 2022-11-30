using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VisionSDK
{
    public interface ICalibVerifyComponent : IDisposable
    {
        Control DisplayedControl { get; }

        Dictionary<string, double?> Run(object img);

        void SetInputImage(Bitmap bmp, bool isGrey);

        void Save();
    }
}
