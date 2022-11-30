using System.Drawing.Imaging;

namespace HyEye.Models
{
    public class ImageSaveInfo
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
        public ImageSaveMode SrcSaveMode { get; set; } = ImageSaveMode.None;

        /// <summary>
        /// 原图压缩系数
        /// </summary>
        public decimal SrcCompressionFactor { get; set; } = 1.0m;

        /// <summary>
        /// 结果图保存模式
        /// </summary>
        public ImageSaveMode ResultSaveMode { get; set; } = ImageSaveMode.None;

        /// <summary>
        /// 结果图图片格式
        /// </summary>
        public string StrResultImageFormat { get; set; } = ImageFormat.Bmp.ToString();

        /// <summary>
        /// 结果图压缩系数
        /// </summary>
        public decimal ResultCompressionFactor { get; set; } = 1.0m;
    }

    public class ImageDeleteInfo
    {
        public ImageDeleteMode DeleteMode { get; set; } = ImageDeleteMode.NoDelete;

        public DefiniteTimeDeleteInfo DefiniteTimeDelete { get; set; } =
            new DefiniteTimeDeleteInfo(12, 0, 13, 0, 7);

        public CycleDeleteInfo CycleDelete { get; set; }
            = new CycleDeleteInfo(60, ImageDeleteCondition.DiskFreeLess, 10, 1);
    }
}
