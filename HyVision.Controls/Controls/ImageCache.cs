using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HyVision.Controls
{
    class ImageCache
    {
        public static Dictionary<string, Image> Imgs;

        static ImageCache()
        {
            // 要注意图片被加密的话就会读取失败

            string[] fns = Directory.GetFiles(HyVisionUtils.HyVisionToolImagePath);

            Imgs = new Dictionary<string, Image>(fns.Length * 2);

            foreach (string fn in fns)
            {
                try
                {
                    Image img = ImageUtils.LoadFromFile(fn);

                    Imgs.Add(Path.GetFileNameWithoutExtension(fn), img);
                }
                catch { }
            }
        }
    }
}
