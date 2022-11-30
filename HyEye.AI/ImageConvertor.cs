using System.Drawing;
using System.Drawing.Imaging;
using HyCommon.ImageUtils;

namespace HyEye.AI
{
    public class ImageConvertor
    {
        /// <summary>
        /// 将Bitmap传给底层的HyWrapper.Image对象
        /// </summary>
        /// <param name="bitmap">Bitmap图像</param>
        /// <returns>传给底层算法的HyWrapper.Image对象</returns>
        public static unsafe HyWrapper.Image GetHyImage(Bitmap bitmap)
        {
            ImageDescriptor descriptor = null;
            byte[] buffer = ReadBufferFromBitmap(bitmap, ref descriptor);
            return GetHyImage(buffer, descriptor);
        }

        /// <summary>
        /// 根据图像的数据和图像的描述创建传给底层的Image对象
        /// </summary>
        /// <param name="buffer">图像的数据Buffer</param>
        /// <param name="descriptor">图像的长，宽，格式等信息描述</param>
        /// <returns>传给底层算法的HyWrapper.Image对象</returns>
        private static unsafe HyWrapper.Image GetHyImage(byte[] buffer, ImageDescriptor descriptor)
        {
            int width = descriptor.Width;
            int height = descriptor.Height;
            PixelFormat pixelFormat = ImageFormatHelper.GetPixelFormat(descriptor.Format);
            HyWrapper.Image image = null;

            switch (descriptor.Format)
            {
                case ImageDataFormat.Y800:
                    fixed (byte* dataPtr = buffer)
                    {
                        image = new HyWrapper.Image(width, height, 1, "GRAY", width, 8, dataPtr, true);
                    }
                    break;
                case ImageDataFormat.RGB24:
                    fixed (byte* dataPtr = buffer)
                    {
                        image = new HyWrapper.Image(width, height, 3, "RGB", width * 3, 8, dataPtr, true);
                    }
                    break;
                case ImageDataFormat.BGR24:
                    fixed (byte* dataPtr = buffer)
                    {
                        image = new HyWrapper.Image(width, height, 3, "BGR", width * 3, 8, dataPtr, true);
                    }
                    break;
                case ImageDataFormat.RGBA32:
                    fixed (byte* dataPtr = buffer)
                    {
                        image = new HyWrapper.Image(width, height, 4, "RGBA", width * 4, 8, dataPtr, true);
                    }
                    break;
                case ImageDataFormat.BGRA32:
                    fixed (byte* dataPtr = buffer)
                    {
                        image = new HyWrapper.Image(width, height, 4, "BGRA", width * 4, 8, dataPtr, true);
                    }

                    /*Bitmap bitmap = new Bitmap(width, height, pixelFormat);
                    BitmapHelper.GetBitmap(buffer, descriptor, ref bitmap);
                    byte[] bmpBuffer = BitmapHelper.Bitmap2Bytes(bitmap);
                    fixed (byte* dataPtr = bmpBuffer)
                    {
                        image = new HyWrapper.Image(dataPtr, true);
                    }*/
                    break;

            }

            return image;
        }

        /// <summary>
        /// 将获HyWrapper.Image转换为Bitmap
        /// </summary>
        /// <param name="image">HyWrapper.Image 对象， 其中存放的是没有Padding的来自cvMat的数据Buffer</param>
        /// <returns>Bitmap图像</returns>
        public static unsafe Bitmap GetBitmap(HyWrapper.Image image)
        {
            Bitmap bitmap = null;

            int width = image.Width();
            int height = image.Height();
            int channels = image.Channels();

            int offset = (width / 8 + 1) * 8 - width;
            switch (channels)
            {
                case 1:
                    bitmap = new Bitmap(width + offset, height, PixelFormat.Format8bppIndexed);
                    break;
                case 3:
                    bitmap = new Bitmap(width + offset, height, PixelFormat.Format24bppRgb);
                    break;
                case 4:
                    bitmap = new Bitmap(width + offset, height, PixelFormat.Format32bppArgb);
                    break;
                default:
                    bitmap = new Bitmap(width + offset, height, PixelFormat.Format24bppRgb);
                    break;
            }

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width + offset, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int srcStep = width * channels;
            int dstStride = bmpData.Stride;

            byte* dst = (byte*)bmpData.Scan0;
            byte* src = image.Data();
            for (int h = 0; h < height; h++)
            {
                NativeMethods.memcpy(dst, src, srcStep);
                src += srcStep;
                dst += dstStride;
            }

            bitmap.UnlockBits(bmpData);

            if(channels == 1)
            {
                ColorPalette palette;
                using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
                {
                    palette = bmp.Palette;
                }
                for (int i = 0; i < 256; i++)
                {
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                }

                bitmap.Palette = palette;
            }

            return bitmap;
        }

        /// <summary>
        /// 获取一个Bitmap图片的图像数据和图像描述
        /// </summary>
        /// <param name="imageFullPath">图片路径，包含图片名称</param>
        /// <param name="descriptor">图像的长，宽，格式等信息描述</param>
        /// <returns>图像的数据</returns>
        public static byte[] ReadBufferFromImagePath(string imageFullPath, ref ImageDescriptor descriptor)
        {
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(imageFullPath);
            ImageDataFormat format = ImageFormatHelper.GetImageDataFormat(bitmap.PixelFormat);
            int bufferSize = ImageFormatHelper.ComputeBufferSize(bitmap.Width, bitmap.Height, format);
            descriptor = new ImageDescriptor(format, bitmap.Width, bitmap.Height, bufferSize);
            byte[] buffer = new byte[bufferSize];
            int dstStride = bufferSize / bitmap.Height;
            BitmapHelper.FillBufferFromBitmap(buffer, dstStride, bitmap, new Point(0, 0));
            return buffer;
        }

        /// <summary>
        /// 获取一个Bitmap图片的图像数据和图像描述
        /// </summary>
        /// <param name="bitmap">Bitmap图片</param>
        /// <param name="descriptor">图像的长，宽，格式等信息描述</param>
        /// <returns>图像的数据</returns>
        private static byte[] ReadBufferFromBitmap(Bitmap bitmap, ref ImageDescriptor descriptor)
        {
            ImageDataFormat format = ImageFormatHelper.GetImageDataFormat(bitmap.PixelFormat);
            int bufferSize = ImageFormatHelper.ComputeBufferSize(bitmap.Width, bitmap.Height, format);
            descriptor = new ImageDescriptor(format, bitmap.Width, bitmap.Height, bufferSize);
            byte[] buffer = new byte[bufferSize];
            int dstStride = bufferSize / bitmap.Height;
            BitmapHelper.FillBufferFromBitmap(buffer, dstStride, bitmap, new Point(0, 0));
            return buffer;
        }

        /// <summary>
        /// 从byte类型指针获取byte数组
        /// </summary>
        /// <param name="bytePtr">byte* 指针</param>
        /// <param name="copyLength">拷贝长度</param>
        /// <returns>byte数组</returns>
        private static unsafe byte[] ReadBufferFromBytePointer(byte* bytePtr, int copyLength)
        {
            byte[] buffer = new byte[copyLength];
            fixed (byte* dst = buffer)
            {
                byte* src = bytePtr;
                NativeMethods.memcpy(dst, src, copyLength);
            }

            return buffer;
        }
    }
}
