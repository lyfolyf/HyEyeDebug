#region Copyright and author
/*
Copyright ?Lead 3C Dec.25,2021.
Author Louis
*/
#endregion

using System;
using System.Drawing.Imaging;

namespace HyCommon.ImageUtils
{
    public enum ImageDataFormat
    {
        None,
        RGB24,
        BGR24,
        RGBA32,
        BGRA32,
        Y800,
        JPEG,
        BMP
    }

    public static class ImageFormatHelper
    {
        /// <summary>
        /// Returns the number of bytes taken by an image depending on its size and format.
        /// For color images, buffer size is always the full RGB24 size, even for compressed formats.
        /// </summary>
        public static int ComputeBufferSize(int width, int height, ImageDataFormat format)
        {
            switch (format)
            {
                case ImageDataFormat.BGR24:
                case ImageDataFormat.RGB24:
                    return width * height * 3; // FIXME: many image providers will align to 4 bytes.
                case ImageDataFormat.BGRA32:
                case ImageDataFormat.RGBA32:
                    return width * height * 4;
                case ImageDataFormat.JPEG:
                    return width * height * 3;
                case ImageDataFormat.Y800:
                    return width * height * 1;
                case ImageDataFormat.None:
                default:
                    return width * height * 3;
            }
        }

        /// <summary>
        /// Get ImageFormat from PixelFormat
        /// </summary>
        /// <param name="pixelFormat">the bitmap pixel format</param>
        /// <returns>Image data format</returns>
        public static ImageDataFormat GetImageDataFormat(PixelFormat pixelFormat)
        {
            ImageDataFormat fmt = ImageDataFormat.None;
            switch (pixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    fmt = ImageDataFormat.BGRA32;
                    break;
                case PixelFormat.Format24bppRgb:
                    fmt = ImageDataFormat.BGR24;
                    break;
                case PixelFormat.Format8bppIndexed:
                    fmt = ImageDataFormat.Y800;
                    break;
                default:
                    throw new NotSupportedException("Currently only support RGB32, RGB24 & Y800 Image format!");
            }

            return fmt;
        }

        public static PixelFormat GetPixelFormat(ImageDataFormat format)
        {
            PixelFormat pixelFormat = PixelFormat.Format24bppRgb;

            switch (format)
            {
                case ImageDataFormat.BGRA32:
                case ImageDataFormat.RGBA32:
                    pixelFormat = PixelFormat.Format32bppArgb;
                    break;
                case ImageDataFormat.BGR24:
                case ImageDataFormat.RGB24:
                case ImageDataFormat.JPEG:
                    pixelFormat = PixelFormat.Format24bppRgb;
                    break;
                case ImageDataFormat.Y800:
                    pixelFormat = PixelFormat.Format8bppIndexed;
                    break;
                default:
                    pixelFormat = PixelFormat.Format24bppRgb;
                    break;
            }

            return pixelFormat;
        }
    }
}
