using CameraSDK.Models;
using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class OpticsInfoVO
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

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
        public CameraParams CameraParams { get; set; } = new CameraParams();

        /// <summary>
        /// 光源参数
        /// </summary>
        public List<LightControllerValueInfoVO> LightControllerValues { get; set; }

        public string OpticsName()
        {
            return $"{TaskName}/{CalibrationName ?? AcquireImageName}";
        }
    }

    public class LightControllerValueInfoVO
    {
        public string LightControllerName { get; set; }

        public List<ChannelValue> ChannelValues { get; set; } = new List<ChannelValue>();
    }

}
