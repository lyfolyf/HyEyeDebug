#region Copyright and author
/*
Copyright ?Lead 3C Dec.25,2021.
Author Louis
*/
#endregion

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace HyCommon.ImageUtils
{
    public class BitmapHelper
    {
        /// <summary>
        /// Get the Bitmap format from Frame format.
        /// </summary>
        public static unsafe void GetBitmap(byte[] buffer, ImageDescriptor descriptor, ref Bitmap bitmap)
        {
            bool topDown = descriptor.TopDown;
            Rectangle rect = new Rectangle(0, 0, descriptor.Width, descriptor.Height);
            switch (descriptor.Format)
            {
                case ImageDataFormat.RGBA32:
                    FillBitmapFromRGBA32(bitmap, rect, buffer, topDown);
                    break;
                case ImageDataFormat.BGRA32:
                    FillBitmapFromBGRA32(bitmap, rect, buffer, topDown);
                    break;
                case ImageDataFormat.RGB24:
                    FillBitmapFromRGB24(bitmap, rect, buffer, topDown);
                    break;
                case ImageDataFormat.BGR24:
                    FillBitmapFromBGR24(bitmap, rect, buffer, topDown);
                    break;
                case ImageDataFormat.Y800:
                    FillGrayFromY800(bitmap, rect, buffer, topDown);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public static byte[] Bitmap2Bytes(Bitmap bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Bmp);
                byte[] byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
        }

        /// <summary>
        /// Generate a new 32-bit image with BGRA32 buffer.
        /// </summary>
        /// <param name="bitmap">The 32-bit bitmap</param>
        /// <param name="rect">The rectangle of bitmap</param>
        /// <param name="buffer">The source BGRA32 buffer</param>
        public unsafe static void FillBitmapFromBGRA32(Bitmap bitmap, Rectangle rect, byte[] buffer, bool topDown = true)
        {
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int srcLineLength = rect.Width * 4;
            int dstStride = bmpData.Stride;

            fixed (byte* pBuffer = buffer)
            {
                byte* src = pBuffer;
                byte* dst = (byte*)bmpData.Scan0.ToPointer();

                if (!topDown)
                    dst = (byte*)bmpData.Scan0.ToPointer() + (dstStride * (rect.Height - 1));

                for (int i = 0; i < rect.Height; i++)
                {
                    NativeMethods.memcpy(dst, src, srcLineLength);
                    src += srcLineLength;
                    if (topDown)
                        dst += dstStride;
                    else
                        dst -= dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);
        }

        /// <summary>
        /// Generate a new 32-bit image with RGBA32 buffer.
        /// </summary>
        /// <param name="bitmap">The 32-bit bitmap</param>
        /// <param name="rect">The rectangle of bitmap</param>
        /// <param name="buffer">The source RGBA32 buffer</param>
        public unsafe static void FillBitmapFromRGBA32(Bitmap bitmap, Rectangle rect, byte[] buffer, bool topDown = true)
        {
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int dstStride = bmpData.Stride;
            int dstPadding = dstStride - (rect.Width * 4);

            fixed (byte* pBuffer = buffer)
            {
                byte* src = pBuffer;
                byte* dst = (byte*)bmpData.Scan0.ToPointer();

                if (!topDown)
                    dst = (byte*)bmpData.Scan0.ToPointer() + (dstStride * (rect.Height - 1));

                for (int i = 0; i < rect.Height; i++)
                {
                    for (int j = 0; j < rect.Width; j++)
                    {
                        dst[0] = src[2];
                        dst[1] = src[1];
                        dst[2] = src[0];
                        dst[3] = src[3];
                        src += 4;
                        dst += 4;
                    }

                    dst += dstPadding;
                    if (!topDown)
                        dst -= dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);
        }

        /// <summary>
        /// Copy the buffer into the bitmap line by line, with optional vertical flip.
        /// The buffer is assumed BGR24 and the Bitmap must already be allocated.
        /// FIXME: this probably doesn't work well with image size with row padding.
        /// </summary>
        public unsafe static void FillBitmapFromBGR24(Bitmap bitmap, Rectangle rect, byte[] buffer, bool topDown = true)
        {
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            int srcLineLength = rect.Width * 3;
            int dstStride = bmpData.Stride;

            fixed (byte* pBuffer = buffer)
            {
                byte* src = pBuffer;
                byte* dst = (byte*)bmpData.Scan0.ToPointer();

                if (!topDown)
                    dst = (byte*)bmpData.Scan0.ToPointer() + (dstStride * (rect.Height - 1));

                for (int i = 0; i < rect.Height; i++)
                {
                    NativeMethods.memcpy(dst, src, srcLineLength);
                    src += srcLineLength;
                    if (topDown)
                        dst += dstStride;
                    else
                        dst -= dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);
        }

        /// <summary>
        /// Copy an RGB24 buffer into an bitmap.
        /// The buffer is expected dense.
        /// </summary>
        public unsafe static void FillBitmapFromRGB24(Bitmap bitmap, Rectangle rect, byte[] buffer, bool topDown = true)
        {
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int dstStride = bmpData.Stride;
            int dstPadding = dstStride - (rect.Width * 3);

            fixed (byte* pBuffer = buffer)
            {
                byte* src = pBuffer;
                byte* dst = (byte*)bmpData.Scan0.ToPointer();

                if (!topDown)
                    dst = (byte*)bmpData.Scan0.ToPointer() + (dstStride * (rect.Height - 1));

                for (int i = 0; i < rect.Height; i++)
                {
                    for (int j = 0; j < rect.Width; j++)
                    {
                        dst[0] = src[2];
                        dst[1] = src[1];
                        dst[2] = src[0];
                        src += 3;
                        dst += 3;
                    }

                    dst += dstPadding;
                    if (!topDown)
                        dst -= dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);
        }


        /// <summary>
        /// Copy the buffer into the bitmap.
        /// The buffer is assumed Y800 with no padding and the Bitmap is RGB24 and already allocated.
        /// </summary>
        public unsafe static void FillBitmapFromY800(Bitmap bitmap, Rectangle rect, byte[] buffer, bool topDown = true)
        {
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int dstStride = bmpData.Stride;
            int dstPadding = dstStride - (rect.Width * 3);

            fixed (byte* pBuffer = buffer)
            {
                byte* src = pBuffer;
                byte* dst = (byte*)bmpData.Scan0.ToPointer();

                if(!topDown)
                    dst = (byte*)bmpData.Scan0.ToPointer() + (dstStride * (rect.Height - 1));

                for (int i = 0; i < rect.Height; i++)
                {
                    for (int j = 0; j < rect.Width; j++)
                    {
                        dst[0] = dst[1] = dst[2] = *src;
                        src++;
                        dst += 3;
                    }

                    dst += dstPadding;
                    if (!topDown)
                        dst -= dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);
        }

        /// <summary>
        /// Generate a new 8-bit gray image with gray array.
        /// </summary>
        /// <param name="bitmap">The 8-bit grayscale bitmap</param>
        /// <param name="rect">The rectangle of bitmap</param>
        /// <param name="buffer">The source gray buffer</param>
        public unsafe static void FillGrayFromY800(Bitmap bitmap, Rectangle rect, byte[] buffer, bool topDown = true)
        {
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int srcLineLength = bmpData.Width;
            int dstStride = bmpData.Stride;

            fixed (byte* pBuffer = buffer)
            {
                byte* src = pBuffer;
                byte* dst = (byte*)bmpData.Scan0.ToPointer();

                if (!topDown)
                    dst = (byte*)bmpData.Scan0.ToPointer() + (dstStride * (rect.Height - 1));

                for (int i = 0; i < rect.Height; i++)
                {
                    NativeMethods.memcpy(dst, src, srcLineLength);
                    src += srcLineLength;
                    if (topDown)
                        dst += dstStride;
                    else
                        dst -= dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);

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

        /// <summary>
        /// Copy the whole bitmap into a rectangle in the frame buffer.
        /// The source bitmap is expected to be smaller than destination.
        /// </summary>
        public unsafe static void FillBufferFromBitmap(byte[] buffer, int dstStride, Bitmap bitmap, Point location)
        {
            Rectangle bmpRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(bmpRectangle, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int srcStride = bmpData.Stride;

            fixed (byte* pBuffer = buffer)
            {
                byte* src = (byte*)bmpData.Scan0.ToPointer();
                byte* dst = pBuffer + ((location.Y * dstStride) + (location.X * 3));

                for (int i = 0; i < bmpRectangle.Height; i++)
                {
                    NativeMethods.memcpy(dst, src, srcStride);
                    src += srcStride;
                    dst += dstStride;
                }
            }

            bitmap.UnlockBits(bmpData);
        }
    }
}
