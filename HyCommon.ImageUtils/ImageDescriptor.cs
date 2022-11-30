#region Copyright and author
/*
Copyright ?Lead 3C Dec.25,2021.
Author Louis
*/
#endregion

namespace HyCommon.ImageUtils
{
    public class ImageDescriptor
    {
        public ImageDataFormat Format { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BufferSize { get; private set; }

        public bool TopDown { get; private set; }

        public ImageDescriptor(ImageDataFormat format, int width, int height, int bufferSize, bool topDown = true)
        {
            this.Format = format;
            this.Width = width;
            this.Height = height;
            this.BufferSize = bufferSize;
            this.TopDown = topDown;
        }
    }
}
