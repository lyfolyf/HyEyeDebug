using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK.Calib
{
    [Serializable]
    public class ImageInfo
    {
        public HObject Image { get; set; }
        public long Index { get; set; }
    }

    public class MiddleImages
    {
        public HObject Mask { get; set; }
        public HObject GradX { get; set; }
        public HObject GradY { get; set; }
        public HObject CloudZ { get; set; }
    }

    public class MachineMaster
    {
        public static Clib_xml clibxml = new Clib_xml();
        public static List<ImageInfo> LstFoldImage_1 = new List<ImageInfo>();
    }
}
