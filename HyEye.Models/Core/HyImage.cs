using System;
using System.Drawing;

namespace HyEye.Models
{
    public class HyImageInfo : IDisposable
    {
        public Bitmap Bitmap { get; set; }

        public bool IsGrey { get; set; }

        /// <summary>
        /// 帧号
        /// </summary>
        public int FrameNum { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 图像对应的指令编号
        /// <para>手动取像为 0</para>
        /// </summary>
        public long CmdID { get; set; }

        public string TaskName { get; set; }

        public string AcqOrCalibName { get; set; }

        /// <summary>
        /// 在联合标定中，需要用到 AcqName 和 CalibName,所以这种合并写法就不合适了
        /// 目前这个属性仅在联合标定中使用，其他地方暂时未修改
        /// </summary>
        public string CalibName { get; set; }

        public int AcqOrCalibIndex { get; set; }

        public string SN { get; set; }

        public HyImageInfo Clone()
        {
            return new HyImageInfo
            {
                Bitmap = (Bitmap)Bitmap.Clone(),
                IsGrey = IsGrey,
                FrameNum = FrameNum,
                Timestamp = Timestamp,
                CmdID = CmdID,
                TaskName = TaskName,
                AcqOrCalibName = AcqOrCalibName,
                AcqOrCalibIndex = AcqOrCalibIndex,
                SN = SN
            };
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
        }

    }
}
