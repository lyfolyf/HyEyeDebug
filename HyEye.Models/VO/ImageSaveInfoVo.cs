using System.Drawing;
using System.Drawing.Imaging;

namespace HyEye.Models.VO
{
    /// <summary>
    /// 图片保存配置
    /// </summary>
    public class ImageSaveInfoVO
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 用 SN 命名图片
        /// </summary>
        public bool NamedBySN { get; set; }

        /// <summary>
        /// true:只有一次拍照有 SN
        /// <para>false:每次拍照都有 SN</para>
        /// </summary>
        public bool OnlyOneSN { get; set; }

        /// <summary>
        /// 有 SN 的拍照索引（OnlyOneSN = true 时有效）
        /// </summary>
        public int AcqIndexHasSN { get; set; }

        /// <summary>
        /// 原图保存模式
        /// </summary>
        public ImageSaveMode SrcSaveMode { get; set; }

        /// <summary>
        /// 原图压缩系数
        /// </summary>
        public decimal SrcCompressionFactor { get; set; }

        /// <summary>
        /// 结果图保存模式
        /// </summary>
        public ImageSaveMode ResultSaveMode { get; set; }

        /// <summary>
        /// 结果图压缩系数
        /// </summary>
        public decimal ResultCompressionFactor { get; set; }

        public ImageFormat ResultImageFormat { get; private set; }

        string strFormat;
        /// <summary>
        /// 结果图图片格式
        /// </summary>
        public string StrResultImageFormat
        {
            get { return strFormat; }
            set
            {
                strFormat = value;
                ResultImageFormat = ImageUtils.ToImageFormat(value);
            }
        }

        public bool NeedSave(bool isSrc, ImageFlag flag)
        {
            ImageSaveMode _flag = (ImageSaveMode)flag;

            if (isSrc)
            {
                if ((SrcSaveMode & _flag) == _flag)
                    return true;
            }
            else
            {
                if ((ResultSaveMode & _flag) == _flag)
                    return true;
            }

            return false;
        }
    }

    public class ImageDeleteInfoVO
    {
        public ImageDeleteMode DeleteMode { get; set; }

        public DefiniteTimeDeleteInfo DefiniteTimeDelete { get; set; }

        public CycleDeleteInfo CycleDelete { get; set; }
    }

}
