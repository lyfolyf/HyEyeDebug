using CameraSDK.Models;
using System.Collections.Generic;

namespace HyEye.Models
{
    public class OpticsInfo
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        // 取像、标定，二择其一
        // 联合标定一起用

        /// <summary>
        /// 取像名称
        /// </summary>
        public string AcquireImageName { get; set; }

        /// <summary>
        /// 标定名称
        /// </summary>
        public string CalibrationName { get; set; }

        /// <summary>
        /// 相机参数
        /// </summary>
        public CameraParams CameraParams { get; set; }

        /// <summary>
        /// 光源参数
        /// </summary>
        public List<LightControllerValueInfo> LightControllerValues { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(AcquireImageName))
                return $"[{TaskName}/{AcquireImageName}]";
            else
                return $"[{TaskName}]";
        }
    }

    public class LightControllerValueInfo
    {
        public string LightControllerName { get; set; }

        public List<ChannelValue> ChannelValues { get; set; } = new List<ChannelValue>();
    }

}
