using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Drawing.Imaging;
using System.Text;

namespace HyEye.Services
{
    /// <summary>
    /// 保存图像时传递给服务的参数
    /// </summary>
    public class ImageInfo : IDisposable
    {
        public HyImageInfo HyImage { get; set; }

        /// <summary>
        /// 是否标定
        /// </summary>
        public bool IsCalibration { get; set; }

        /// <summary>
        /// true：原图; false：结果图
        /// </summary>
        public bool IsSrc { get; set; }

        /// <summary>
        /// OK/NG
        /// </summary>
        public ImageFlag Flag { get; set; }

        public ImageSaveInfoVO SaveInfo { get; set; }

        ///// <summary>
        ///// 压缩系数
        ///// </summary>
        //public decimal CompressionFactor { get; set; }

        //public bool NamedBySN { get; set; }

        /// <summary>
        /// 获取图像保存时的文件名
        /// </summary>
        public string GetFilename(SubDireOfSaveImage subDire)
        {
            StringBuilder sb = new StringBuilder(64);
            sb.Append(HyImage.Timestamp.ToString("yyyyMMddHHmmssfff"));

            sb.Append("_FN").Append(HyImage.FrameNum);

            if (SaveInfo.NamedBySN)
            {
                if ((subDire & SubDireOfSaveImage.Task) == 0)
                    sb.Append("_").Append(HyImage.TaskName);
                if ((subDire & SubDireOfSaveImage.SN) == 0)
                    sb.Append("_").Append(HyImage.SN);
            }

            sb.Append("_").Append(HyImage.AcqOrCalibIndex.ToString());

            if ((subDire & SubDireOfSaveImage.Flag) == 0)
                sb.Append("_").Append(Flag.ToString());

            if ((subDire & SubDireOfSaveImage.Source) == 0)
                sb.Append("_").Append(IsSrc ? "src" : "ret");

            if (IsSrc)
                sb.Append(".Bmp");
            else
                sb.Append(".").Append(SaveInfo.StrResultImageFormat);

            return sb.ToString();
        }

        /// <summary>
        /// 获取图像保存目录
        /// </summary>
        public string GetDirectory(SubDireOfSaveImage subDire)
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("\\").Append(DateTime.Today.ToString("yyyy-MM-dd"));

            // 标定的子目录就是 任务\标定

            if (IsCalibration)
            {
                sb.Append("\\").Append(HyImage.TaskName);
                sb.Append("\\").Append(HyImage.AcqOrCalibName);
            }
            else
            {
                if (SaveInfo.NamedBySN)
                {
                    if ((subDire & SubDireOfSaveImage.Task) != 0)
                        sb.Append("\\").Append(HyImage.TaskName);
                    if ((subDire & SubDireOfSaveImage.SN) != 0)
                        sb.Append("\\").Append(HyImage.SN);
                }
                else
                {
                    sb.Append("\\").Append(HyImage.TaskName);
                }
            }

            if ((subDire & SubDireOfSaveImage.Flag) != 0)
                sb.Append("\\").Append(Flag.ToString());

            if ((subDire & SubDireOfSaveImage.Source) != 0)
                sb.Append("\\").Append(IsSrc ? "src" : "ret");

            return sb.ToString();
        }

        /// <summary>
        /// 获取图像大小
        /// </summary>
        public long ImageSize()
        {
            if (IsSrc)
                return HyImage.Bitmap.Width * HyImage.Bitmap.Height;
            else
            {
                if (SaveInfo.StrResultImageFormat == ImageFormat.Bmp.ToString())
                {
                    return HyImage.Bitmap.Width * HyImage.Bitmap.Height * 3;
                }
                else
                {
                    return 0;
                }
            }
        }

        // 实测数据：
        // 原图：           11.7M，存图平均耗时 62ms

        // 结果图存实际图像：生成结果图平均耗时：首次 275ms，第二次开始 630ms
        // 结果图 BMP 格式：35.1M，存图平均耗时 22ms
        // 结果图 PNG 格式：2.88M，存图平均耗时 180ms
        // 结果图 JPG 格式：289KB，存图平均耗时 50ms

        // 结果图存截图：生成结果图耗时：110ms
        // 结果图 BMP 格式：309 KB，存图平均耗时 1.25ms
        // 结果图 JPG 格式：9.58KB，存图平均耗时 1ms

        // 结果图压缩：
        // 压缩系数 0.3，结果图大小 3.16M，存图平均耗时 45ms

        public void Save(string root, SubDireOfSaveImage subDire)
        {
            if (HyImage.Bitmap == null) return;

            ImageFormat format = IsSrc ? ImageFormat.Bmp : SaveInfo.ResultImageFormat;

            string fullname = $@"{root}{GetDirectory(subDire)}\{GetFilename(subDire)}";

            HyImage.Bitmap.Save(fullname, format);
        }

        public void Dispose()
        {
            HyImage?.Dispose();
        }
    }
}
