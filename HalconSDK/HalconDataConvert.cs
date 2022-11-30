using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using HyVision;
using HyVision.Models;
using HyVision.Tools.ImageDisplay;

using HalconDotNet;

namespace HalconSDK
{
    public class HalconDataConvert
    {
        public HTuple ConvertToHTuple(HyTerminal data)
        {
            //如果输入参数的类型不是HTuple支持的可转换的类型，则抛出异常
            if (!HalconSupportDataTypes.HTupleSupportedDataType.Contains(data.ValueType))
                throw new HyVisionException($"The type of input param[{data.Name}] can not convert to HTuple! The type is: {data.ValueType}");

            HTuple hValue = new HTuple(data.Value);
            return hValue;
        }

        public HObject ConvertToHObject(HyTerminal terminalData)
        {
            //如果输入参数的类型不是HObject支持的可转换的类型，则抛出异常
            if (!HalconSupportDataTypes.HObjectSupportedDataType.Contains(terminalData.ValueType))
                throw new Exception($"The type of input param[{terminalData.Name}] can not convert to HObject! The type is: {terminalData.ValueType}");

            HObject hObject;

            if (terminalData.ValueType == typeof(HyImage))
            {
                HOperatorSet.GenEmptyObj(out hObject);
                HyImage data = (HyImage)terminalData.Value;
                if (data.ImageData != null && data.ImageDataType != null && !data.Equals(""))
                {
                    string strImageType = data.ImageDataType.Substring(data.ImageDataType.IndexOf('_') + 1);
                    HOperatorSet.GenImage1(out hObject, strImageType, data.Width, data.Height, data.ImageData);
                }
                else if (data.Image != null)
                {
                    Bitmap bitmapImage = data.Image;
                    string strImageDataType;
                    if (bitmapImage.PixelFormat == PixelFormat.Format8bppIndexed)
                        strImageDataType = "byte";
                    else if (bitmapImage.PixelFormat == PixelFormat.Format16bppGrayScale ||
                        bitmapImage.PixelFormat == PixelFormat.Format16bppRgb565)
                        strImageDataType = "uint2";
                    else if (bitmapImage.PixelFormat == PixelFormat.Format24bppRgb)      
                    {
                        //hObject = Bitmap2HImage_24_2(bitmapImage);
                        Bitmap24ToHObject(bitmapImage, out hObject);
                        return hObject;
                    }
                    else if (bitmapImage.PixelFormat == PixelFormat.Format32bppRgb ||
                                bitmapImage.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        hObject = Bitmap2HImage_24_2(bitmapImage);
                        return hObject;
                    }
                    else
                        throw new Exception($"The image type of input param[{terminalData.Name}] is invalid! The image type is: {bitmapImage.PixelFormat}");

                    hObject = Bitmap2HImage_8(bitmapImage, strImageDataType);
                    //HOperatorSet.GenImage1(out hObject, strImageDataType, data.Width, data.Height, data.Image.GetHbitmap());
                }
                else
                    throw new Exception($"The input param[{terminalData.Name}] did not have image data!");
            }
            else
                hObject = (HObject)terminalData.Value;

            return hObject;
        }

        public HImage ConvertToHImage(HyTerminal terminalData)
        {
            //如果输入参数的类型不是HImage支持的可转换的类型，则抛出异常
            if (!HalconSupportDataTypes.HImageSupportedDataType.Contains(terminalData.ValueType))
                throw new Exception($"The type of input param[{terminalData.Name}] can not convert to HImage! The type is: {terminalData.ValueType}");

            HImage hImage = null;
            //如果是一个图像文件的路径，则直接创建HImage对象，否则需要先转换成指针IntPtr
            if (terminalData.ValueType == typeof(string))
            {
                if (!File.Exists(terminalData.Value.ToString()))
                    throw new Exception($"The image file is not exist! File path: {terminalData.Value}");

                hImage = new HImage(terminalData.Value.ToString());
            }
            else
            {
                HObject hObject;

                if (terminalData.ValueType == typeof(HyImage))
                {
                    HOperatorSet.GenEmptyObj(out hObject);
                    HyImage data = (HyImage)terminalData.Value;
                    if (data.ImageData != null && data.ImageDataType != null && !data.Equals(""))
                        HOperatorSet.GenImage1(out hObject, data.ImageDataType, data.Width, data.Height, data.ImageData);
                    else if (data.Image != null)
                    {
                        Bitmap bitmapImage = data.Image;
                        string strImageDataType;
                        if (bitmapImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                            strImageDataType = "byte";
                        else if (bitmapImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale ||
                            bitmapImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565)
                            strImageDataType = "uint2";
                        else
                            throw new Exception($"The image type of input param[{terminalData.Name}] is invalid! The image type is: {bitmapImage.PixelFormat}");

                        HOperatorSet.GenImage1(out hObject, strImageDataType, data.Width, data.Height, data.Image.GetHbitmap());
                        hImage = (HImage)hObject;
                    }
                    else
                        throw new Exception($"The input param[{terminalData.Name}] did not have image data!");
                }
                else
                    hImage = (HImage)terminalData.Value;
            }
            return hImage;
        }

        public HRegion ConvertToHRegion(HyTerminal data)
        {
            //如果输入参数的类型不是HRegion支持的可转换的类型，则抛出异常
            if (!HalconSupportDataTypes.HRegionSupportedDataType.Contains(data.ValueType))
                throw new Exception($"The type of input param[{data.Name}] can not convert to HRegion! The type is: {data.ValueType}");

            var handle = GCHandle.Alloc(data.Value);
            IntPtr ptr = GCHandle.ToIntPtr(handle);

            //此处声明的HOject对象先不释放，如果释放的话，会对后面的使用造成影响，这样的话，要求Excute接口在使用完之后必须要把这个对象释放掉
            HRegion hObject = new HRegion(ptr);
            handle.Free();

            return hObject;
        }

        public HXLD ConvertToHXLD(HyTerminal data)
        {
            //如果输入参数的类型不是HXLD支持的可转换的类型，则抛出异常
            if (!HalconSupportDataTypes.HXLDSupportedDataType.Contains(data.ValueType))
                throw new Exception($"The type of input param[{data.Name}] can not convert to HXLD! The type is: {data.ValueType}");

            var handle = GCHandle.Alloc(data.Value);
            IntPtr ptr = GCHandle.ToIntPtr(handle);

            //此处声明的HOject对象先不释放，如果释放的话，会对后面的使用造成影响，这样的话，要求Excute接口在使用完之后必须要把这个对象释放掉
            HXLD hObject = new HXLD(ptr);
            handle.Free();

            return hObject;
        }

        /*
        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(int Destination, int add, int Length);
        /// <summary>
        /// HObject转8位Bitmap(单通道)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="res"></param>
        public void HObject2Bitmap8(HObject image, out Bitmap res)
        {
            BitmapData bitmapData = null;
            res = null;
            try
            {
                HTuple hpoint, type, width, height;

                const int Alpha = 255;
                int[] ptr = new int[2];
                HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);

                res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                ColorPalette pal = res.Palette;
                for (int i = 0; i <= 255; i++)
                {
                    pal.Entries[i] = Color.FromArgb(Alpha, i, i, i);
                }
                res.Palette = pal;
                Rectangle rect = new Rectangle(0, 0, width, height);
                bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                ptr[0] = bitmapData.Scan0.ToInt32();
                ptr[1] = hpoint.I;
                if (width % 4 == 0)
                    CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
                else
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        ptr[1] += width;
                        CopyMemory(ptr[0], ptr[1], width * PixelSize);
                        ptr[0] += bitmapData.Stride;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bitmapData != null)
                {
                    res?.UnlockBits(bitmapData);
                    res = null;
                }
            }
        }
        */

        [DllImport("ntdll.dll")]
        private static extern IntPtr memcpy(IntPtr dst, IntPtr src, int count);
        public Bitmap HObject2Bitmap8(HObject hoImage)
        {
            HTuple hpoint, type, width, height;
            HOperatorSet.GetImagePointer1(hoImage, out hpoint, out type, out width, out height);
            if (width.TupleLength() == 0)
            {
                return null;
            }
            Bitmap res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            ColorPalette pal = res.Palette;
            for (int i = 0; i <= 255; i++)
            {
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            res.Palette = pal;
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int length = bitmapData.Stride * height;

            if (width % 4 == 0)
            {
                memcpy(bitmapData.Scan0, hpoint.IP, length);
            }
            else
            {
                int offset = 0, offset1 = 0;
                for (int i = 0; i < height; i++)
                {
                    memcpy(bitmapData.Scan0 + offset1, hpoint.IP + offset, width);
                    offset += width;
                    offset1 += bitmapData.Stride;
                }
            }
            res.UnlockBits(bitmapData);
            //释放临时变量
            return res;
        }

        /// <summary>
        /// HObject转24位Bitmap
        /// </summary>
        /// <param name="image"></param>
        /// <param name="res"></param>
        public void HObject2Bitmap24(HObject image, out Bitmap res)
        {
            BitmapData bitmapData = null;
            res = null;
            try
            {
                HTuple hred, hgreen, hblue, type, width, height;
                HOperatorSet.GetImagePointer3(image, out hred, out hgreen, out hblue, out type, out width, out height);

                res = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                Rectangle rect = new Rectangle(0, 0, width, height);
                bitmapData = res.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int imglength = width * height;
                unsafe
                {
                    byte* bptr = (byte*)bitmapData.Scan0;
                    byte* r = ((byte*)hred.L);
                    byte* g = ((byte*)hgreen.L);
                    byte* b = ((byte*)hblue.L);
                    for (int i = 0; i < imglength; i++)
                    {
                        bptr[i * 3] = (b)[i];
                        bptr[i * 3 + 1] = (g)[i];
                        bptr[i * 3 + 2] = (r)[i];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bitmapData != null)
                {
                    res?.UnlockBits(bitmapData);
                    //res = null;
                }
            }
        }

        /// <summary>
        /// HObject转24位RGB图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="res"></param>
        public void HObject2RGB(HObject image, out Bitmap res)
        {
            try
            {
                HObject temp = image.Clone();
                //获取图像尺寸
                HOperatorSet.GetImageSize(temp, out HTuple width0, out HTuple height0);
                HOperatorSet.GetImageSize(temp, out width0, out height0);
                //创建交错格式图像
                HOperatorSet.InterleaveChannels(temp, out HObject InterImage, "rgb", 4 * width0, 0);
                //获取交错格式图像指针
                HOperatorSet.GetImagePointer1(InterImage, out HTuple Pointer, out HTuple type, out HTuple width, out HTuple height);
                IntPtr ptr = Pointer;
                //构建新Bitmap图像
                res = new Bitmap(width / 4, height, width, PixelFormat.Format24bppRgb, ptr);
            }
            catch (Exception ex)
            {
                res = null;
                throw ex;
            }
        }

        ///// <summary>
        ///// 32位Bitmap转HImage
        ///// </summary>
        ///// <param name="bImage"></param>
        ///// <returns></returns>
        //public HImage Bitmap2HImage_32(Bitmap bImage, string imageType)
        //{
        //    Bitmap bImage32 = null;
        //    BitmapData bmData = null;
        //    try
        //    {
        //        Rectangle rect;
        //        IntPtr pBitmap;
        //        IntPtr pPixels;
        //        HImage hImage = new HImage();
        //        rect = new Rectangle(0, 0, bImage.Width, bImage.Height);
        //        bImage32 = new Bitmap(bImage.Width, bImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bImage32);
        //        g.DrawImage(bImage, rect);
        //        g.Dispose();
        //        bmData = bImage32.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //        pBitmap = bmData.Scan0;
        //        pPixels = pBitmap;
        //        hImage.GenImageInterleaved(pPixels, "bgr", bImage.Width, bImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);

        //        return hImage;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (bmData != null)
        //            bImage32.UnlockBits(bmData);
        //    }
        //}

        /// <summary>
        /// 24位Bitmap转HImage
        /// </summary>
        /// <param name="bImage"></param>
        /// <returns></returns>
        public HImage Bitmap2HImage_24(Bitmap bImage)
        {
            Bitmap bImage24 = null;
            BitmapData bmData = null;
            try
            {
                Rectangle rect;
                IntPtr pBitmap;
                IntPtr pPixels;
                HImage hImage = new HImage();
                rect = new Rectangle(0, 0, bImage.Width, bImage.Height);
                bImage24 = new Bitmap(bImage.Width, bImage.Height, bImage.PixelFormat);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bImage24);
                g.DrawImage(bImage, rect);
                g.Dispose();
                bmData = bImage24.LockBits(rect, ImageLockMode.ReadOnly, bImage.PixelFormat);
                pBitmap = bmData.Scan0;
                pPixels = pBitmap;
                hImage.GenImageInterleaved(pPixels, "rgb", bImage.Width, bImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);

                return hImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmData != null)
                    bImage24.UnlockBits(bmData);
            }
        }

        /// <summary>
        /// Bitmap转HImage
        /// </summary>
        /// <param name="bImage"></param>
        /// <returns></returns>
        public HImage Bitmap2HImage_8(Bitmap bImage, string imageType)
        {
            BitmapData bmData = null;
            try
            {
                Rectangle rect;
                IntPtr pBitmap;
                IntPtr pPixels;
                var hImage = new HImage();
                rect = new Rectangle(0, 0, bImage.Width, bImage.Height);
                bmData = bImage.LockBits(rect, ImageLockMode.ReadOnly, bImage.PixelFormat);
                pBitmap = bmData.Scan0;
                pPixels = pBitmap;
                hImage.GenImage1(imageType, bImage.Width, bImage.Height, pPixels);
                //formathimage = hImage;
                return hImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmData != null)
                    bImage.UnlockBits(bmData);
            }
        }

        /// <summary>
        /// 24位Bitmap转HImage
        /// </summary>
        /// <param name="bImage"></param>
        /// <returns></returns>
        public HObject Bitmap2HImage_24_2(Bitmap bImage)
        {
            BitmapData bmData = null;
            try
            {
                int channels = 3;
                if (bImage.PixelFormat == PixelFormat.Format24bppRgb)
                    channels = 3;
                else if (bImage.PixelFormat == PixelFormat.Format32bppRgb || bImage.PixelFormat == PixelFormat.Format32bppArgb)
                    channels = 4;

                var hImage = new HObject();
                bmData = bImage.LockBits(new Rectangle(0, 0, bImage.Width, bImage.Height), ImageLockMode.ReadOnly, bImage.PixelFormat);//锁定BitMap 

                byte[] arrayR = new byte[bmData.Width * bmData.Height];//红色数组 
                byte[] arrayG = new byte[bmData.Width * bmData.Height];//绿色数组 
                byte[] arrayB = new byte[bmData.Width * bmData.Height];//蓝色数组 

                unsafe
                {
                    byte* pBmp = (byte*)bmData.Scan0;//BitMap的头指针 
                                                     //下面的循环分别提取出红绿蓝三色放入三个数组 
                    for (int R = 0; R < bmData.Height; R++)
                    {
                        for (int C = 0; C < bmData.Width; C++)
                        {
                            //因为内存BitMap的储存方式，行宽用Stride算，C*3是因为这是三通道，另外BitMap是按BGR储存的 
                            byte* pBase = pBmp + bmData.Stride * R + C * channels;
                            arrayR[R * bmData.Width + C] = *(pBase + 2);
                            arrayG[R * bmData.Width + C] = *(pBase + 1);
                            arrayB[R * bmData.Width + C] = *(pBase);
                        }
                    }

                    //得到三个数组的头指针，C#特色 
                    fixed (byte* pR = arrayR, pG = arrayG, pB = arrayB)
                    {
                        HOperatorSet.GenImage3(out hImage, "byte", bmData.Width, bmData.Height,
                                                                   new IntPtr(pR), new IntPtr(pG), new IntPtr(pB));
                        //如果这里报错，仔细看看前面有没有写错 
                    }
                }
                return hImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmData != null)
                    bImage.UnlockBits(bmData);
            }
        }

        #region Halcon 与 HyROI 互转，以及区域的交并差集操作

        public HObject HyRoiToHObject(BaseHyROI HyRoi)
        {
            HObject RetHObj;
            if (HyRoi.RoiType == RoiType.Circle)
            {
                HyCircle hyCircle = (HyCircle)HyRoi;

                HOperatorSet.GenCircle(out RetHObj, hyCircle.Center.Y, hyCircle.Center.X, hyCircle.Radius);
                return RetHObj;
            }

            if (HyRoi.RoiType == RoiType.Ellipse)
            {
                HyEllipse hyEllipse = (HyEllipse)HyRoi;

                double phi = -hyEllipse.Angle * Math.PI / 180;
                HOperatorSet.GenEllipse(out RetHObj, hyEllipse.Center.Y, hyEllipse.Center.X, phi,
                    hyEllipse.Width / 2, hyEllipse.Height / 2);

                return RetHObj;
            }

            if (HyRoi.RoiType == RoiType.Rectangle2)
            {
                HyRectangle2 hyRectangle = (HyRectangle2)HyRoi;

                double phi = -hyRectangle.Angle * Math.PI / 180;
                HOperatorSet.GenRectangle2(out RetHObj, hyRectangle.Center.Y, hyRectangle.Center.X, phi,
                    hyRectangle.Width / 2, hyRectangle.Height / 2);

                return RetHObj;
            }

            if (HyRoi.RoiType == RoiType.Polygon)
            {
                HyPolygon hyPolygon = (HyPolygon)HyRoi;
                HTuple row = new HTuple();
                HTuple column = new HTuple();

                for (int i = 0; i < hyPolygon.PolygonPoints.Count; i++)
                {
                    row[i] = hyPolygon.PolygonPoints[i].Y;
                    column[i] = hyPolygon.PolygonPoints[i].X;
                }

                HOperatorSet.GenRegionPolygon(out RetHObj, row, column);
                return RetHObj;
            }

            if (HyRoi.RoiType == RoiType.Sector)
            {
                HySector hySector = (HySector)HyRoi;
                double StartAngle = -hySector.StartAngle * Math.PI / 180;
                double EndAngle = -hySector.EndAngle * Math.PI / 180;

                HOperatorSet.GenEllipseSector(out RetHObj, hySector.Center.Y, hySector.Center.X, 0, hySector.Radius
                    , hySector.Radius, StartAngle, EndAngle);
                return RetHObj;
            }
            return default;
        }

        public HRegion HyRoiToHRegion(BaseHyROI HyRoi)
        {
            HRegion region = new HRegion();
            if (HyRoi.RoiType == RoiType.Circle)
            {
                HyCircle hyCircle = (HyCircle)HyRoi;
                region.GenCircle(hyCircle.Center.Y * 1d, hyCircle.Center.X, hyCircle.Radius);

                return region;
            }

            if (HyRoi.RoiType == RoiType.Ellipse)
            {
                HyEllipse hyEllipse = (HyEllipse)HyRoi;
                double phi = -hyEllipse.Angle * Math.PI / 180;
                region.GenEllipse(hyEllipse.Center.Y, hyEllipse.Center.X, phi, hyEllipse.Width / 2, hyEllipse.Height / 2);

                return region;
            }

            if (HyRoi.RoiType == RoiType.Rectangle2)
            {
                HyRectangle2 hyRectangle = (HyRectangle2)HyRoi;
                double phi = -hyRectangle.Angle * Math.PI / 180;
                region.GenRectangle2(hyRectangle.Center.Y, hyRectangle.Center.X, phi, hyRectangle.Width / 2, hyRectangle.Height / 2);

                return region;
            }

            if (HyRoi.RoiType == RoiType.Polygon)
            {
                HyPolygon hyPolygon = (HyPolygon)HyRoi;
                HTuple row = new HTuple();
                HTuple column = new HTuple();

                for (int i = 0; i < hyPolygon.PolygonPoints.Count; i++)
                {
                    row[i] = hyPolygon.PolygonPoints[i].Y;
                    column[i] = hyPolygon.PolygonPoints[i].X;
                }

                region.GenRegionPolygon(row, column);
                return region;
            }

            if (HyRoi.RoiType == RoiType.Sector)
            {
                HySector hySector = (HySector)HyRoi;
                double StartAngle = -hySector.StartAngle * Math.PI / 180;
                double EndAngle = -hySector.EndAngle * Math.PI / 180;

                region.GenEllipseSector(hySector.Center.Y, hySector.Center.X, 0, hySector.Radius
                    , hySector.Radius, StartAngle, EndAngle);

                return region;
            }
            return default;
        }

        public HyDefectRegion HRegionToHyDefectRegion(HRegion hRegion)
        {
            HyDefectRegion hyDefectsRegion = new HyDefectRegion();

            //update by LuoDian @ 20210903 处理数组的情况
            int count = hRegion.CountObj();
            for (int j = 1; j <= count; j++)
            {
                HRegion subRegion = hRegion.SelectObj(j);
                subRegion.GetRegionPoints(out HTuple rows, out HTuple columns);
                HOperatorSet.TupleLength(rows, out HTuple length);
                for (int i = 0; i < length; i++)
                {
                    hyDefectsRegion.DefectPoints.Add(new PointF(columns[i].F, rows[i].F));
                }
            }

            return hyDefectsRegion;
        }

        public HyDefectRegion HobjectToHyDefectRegion(HObject InputHobject)
        {
            HTuple rows = new HTuple(), columns = new HTuple();
            HyDefectRegion hyDefectsRegion = new HyDefectRegion();


            HOperatorSet.CountObj(InputHobject, out HTuple number);
            if (number > 1) number = 1;
            for (int i = 0; i < number; i++)
            {
                HOperatorSet.SelectObj(InputHobject, out HObject objectSelected, i + 1);
                HOperatorSet.GetRegionPoints(objectSelected, out HTuple Newrows, out HTuple Newcolumns);
                rows = rows.TupleConcat(Newrows);
                columns = columns.TupleConcat(Newcolumns);
            }

            //HOperatorSet.GetRegionPoints(InputHobject, out HTuple rows, out HTuple columns);
            HOperatorSet.TupleLength(rows, out HTuple length);

            for (int i = 0; i < length; i++)
            {
                hyDefectsRegion.DefectPoints.Add(new PointF(columns[i].F, rows[i].F));
            }

            return hyDefectsRegion;
        }

        public List<HyDefectXLD> HobjectTolstHyDefectXLD(HObject InputHobject)
        {
            HTuple rows = new HTuple(), columns = new HTuple();
            List<HyDefectXLD> hyDefectXLDs = new List<HyDefectXLD>();

            HOperatorSet.CountObj(InputHobject, out HTuple number);
            for (int i = 0; i < number; i++)
            {
                HOperatorSet.SelectObj(InputHobject, out HObject objectSelected, i + 1);
                HOperatorSet.GenContourRegionXld(objectSelected, out HObject contouts, "border");

                HOperatorSet.CountObj(contouts, out HTuple XLDcount);
                for (int j = 0; j < XLDcount; j++)
                {
                    HyDefectXLD xldDefect = new HyDefectXLD();
                    HOperatorSet.SelectObj(contouts, out HObject SelectXLD, j + 1);
                    HOperatorSet.GetContourXld(SelectXLD, out HTuple XLDrow, out HTuple XLDcolumn);

                    HOperatorSet.TupleLength(XLDrow, out HTuple RowLength);
                    for (int k = 0; k < RowLength; k += 3)
                    {
                        xldDefect.DefectXLDPoints.Add(new PointF(XLDcolumn[k].F, XLDrow[k].F));
                    }
                    hyDefectXLDs.Add(xldDefect);
                }
            }

            return hyDefectXLDs;
        }


        public HObject HyDefectRegionToHobject(HyDefectRegion InputHydefectRegion)
        {
            HTuple rows = new HTuple();
            HTuple columns = new HTuple();

            for (int i = 0; i < InputHydefectRegion.DefectPoints.Count; i++)
            {
                rows = rows.TupleConcat(InputHydefectRegion.DefectPoints[i].Y);
                columns = columns.TupleConcat(InputHydefectRegion.DefectPoints[i].X);
            }

            HOperatorSet.GenRegionPoints(out HObject region, rows, columns);

            return region;
        }


        //public HyDefectXLD HXLDContToHyDefectXLD(HXLDCont hXLDCont)
        //{
        //    float offsetx = 0.5f, offsety = 0.5f;

        //    HyDefectXLD hyDefectXLD = new HyDefectXLD();
        //    hXLDCont.GetContourXld(out HTuple rows, out HTuple columns);
        //    HOperatorSet.TupleLength(rows, out HTuple length);

        //    for (int i = 0; i < length; i++)
        //    {
        //        hyDefectXLD.DefectXLDPoints.Add(new PointF(columns[i].F + offsetx, rows[i].F + offsety));
        //    }

        //    return hyDefectXLD;
        //}


        public HyDefectXLD HXLDContToHyDefectXLD(HXLD InputHXLD)
        {
            float offsetx = 0.5f, offsety = 0.5f;

            HyDefectXLD hyDefectXLD = new HyDefectXLD();
            ((HXLDCont)InputHXLD).GetContourXld(out HTuple rows, out HTuple columns);
            HOperatorSet.TupleLength(rows, out HTuple length);

            for (int i = 0; i < length; i++)
            {
                hyDefectXLD.DefectXLDPoints.Add(new PointF(columns[i].F + offsetx, rows[i].F + offsety));
            }

            return hyDefectXLD;
        }

        public HyDefectXLD HXLDContToHyDefectXLD(HObject InputHObject)
        {
            float offsetx = 0.5f, offsety = 0.5f;

            HyDefectXLD hyDefectXLD = new HyDefectXLD();

            HTuple rows = new HTuple(), columns = new HTuple(), length;
            HXLDCont xld = new HXLDCont(InputHObject);
            HTuple xldCount = xld.CountObj();

            for (int i = 0; i < xldCount; i++)
            {
                xld.SelectObj(i + 1).GetContourXld(out HTuple Newrows, out HTuple Newcolumns);
                rows = rows.TupleConcat(Newrows);
                columns = columns.TupleConcat(Newcolumns);
            }

            HOperatorSet.TupleLength(rows, out length);

            for (int i = 0; i < length; i++)
            {
                hyDefectXLD.DefectXLDPoints.Add(new PointF(columns[i].F + offsetx, rows[i].F + offsety));
            }

            return hyDefectXLD;
        }

        public HObject Union1(List<BaseHyROI> HyROIs)
        {
            HOperatorSet.GenEmptyObj(out HObject UnionObject);

            foreach (BaseHyROI roi in HyROIs)
            {
                HObject Hobj = HyRoiToHObject(roi);
                HOperatorSet.Union2(UnionObject, Hobj, out UnionObject);
            }
            return UnionObject;
        }

        public HObject Union1(List<HObject> hObjects)
        {
            HOperatorSet.GenEmptyObj(out HObject UnionObject);

            foreach (HObject obj in hObjects)
            {
                HOperatorSet.Union2(UnionObject, obj, out UnionObject);
            }

            return UnionObject;
        }

        public HObject Union2(BaseHyROI HyROI1, BaseHyROI HyROI2)
        {
            HOperatorSet.Union2(HyRoiToHObject(HyROI1), HyRoiToHObject(HyROI2), out HObject RegionUnion);
            return RegionUnion;
        }

        public HObject Union2(HObject HObj1, HObject HObj2)
        {
            HOperatorSet.Union2(HObj1, HObj2, out HObject RegionUnion);
            return RegionUnion;
        }

        public HObject Difference(BaseHyROI HyROI1, BaseHyROI HyROI2)
        {
            HOperatorSet.Difference(HyRoiToHObject(HyROI1), HyRoiToHObject(HyROI2), out HObject RegionDifference);
            return RegionDifference;
        }

        public HObject Difference(HObject HObj1, HObject HObj2)
        {
            HOperatorSet.Difference(HObj1, HObj2, out HObject RegionDifference);
            return RegionDifference;
        }

        public HObject Intersection(BaseHyROI HyROI1, BaseHyROI HyROI2)
        {
            HOperatorSet.Intersection(HyRoiToHObject(HyROI1), HyRoiToHObject(HyROI2), out HObject RegionIntersection);
            return RegionIntersection;
        }

        public HObject Intersection(HObject HObj1, HObject HObj2)
        {
            HOperatorSet.Intersection(HObj1, HObj2, out HObject RegionIntersection);
            return RegionIntersection;
        }





        #endregion


        #region  3通道 byte图片（即24位图）与HObject 转换

        public bool Bitmap24ToHObject(Bitmap SrcImage, out HObject DstImage)
        {
            HOperatorSet.GenEmptyObj(out DstImage);
            try
            {
                BitmapData data = SrcImage.LockBits(new Rectangle(0, 0, SrcImage.Width, SrcImage.Height), ImageLockMode.ReadOnly, SrcImage.PixelFormat);
                HOperatorSet.GenImageInterleaved(out DstImage, data.Scan0, "bgr", SrcImage.Width, SrcImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
                SrcImage.UnlockBits(data);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool HObjectToBitmap24(HObject SrcImage, out Bitmap DstImage)
        {
            try
            {
                HOperatorSet.GetImageSize(SrcImage, out HTuple SrcWidth, out HTuple SrcHeight);

                int Num = 0;
                if ((3 * SrcWidth) % 4 == 0)
                {
                    Num = 3 * SrcWidth;
                }
                else
                {
                    Num = 3 * SrcWidth + (4 - (3 * SrcWidth) % 4);
                }

                HOperatorSet.InterleaveChannels(SrcImage, out HObject interleavedimage, "bgr", Num, 0);
                HOperatorSet.GetImagePointer1(interleavedimage, out HTuple pointer, out HTuple type, out HTuple ww, out HTuple hh);
                DstImage = new Bitmap(SrcWidth, SrcHeight, PixelFormat.Format24bppRgb);
                BitmapData DstData = DstImage.LockBits(new Rectangle(0, 0, SrcWidth, SrcHeight), ImageLockMode.ReadWrite, DstImage.PixelFormat);

                int length = Num * SrcHeight;
                byte[] ImgByte = new byte[length];
                Marshal.Copy(new IntPtr(pointer.L), ImgByte, 0, length);
                Marshal.Copy(ImgByte, 0, DstData.Scan0, ImgByte.Length);
                DstImage.UnlockBits(DstData);
                return true;
            }
            catch (Exception ex)
            {
                DstImage = null;
                return false;
            }
        }

        #endregion

    }
}
