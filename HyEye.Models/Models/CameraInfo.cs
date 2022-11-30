using CameraSDK.Models;

namespace HyEye.Models
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CameraInfo
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string UserDefinedName { get; set; }

        public string IP { get; set; }

        /// <summary>
        /// 子网掩码
        /// </summary>
        public string SubnetMask { get; set; }

        /// <summary>
        /// 默认网关
        /// </summary>
        public string DefaultGateway { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public CameraBrand Brand { get; set; }

        /// <summary>
        /// true:软触发; false:硬触发
        /// </summary>
        public bool SoftTrigger { get; set; }

        /// <summary>
        /// 图像缓存节点数
        /// </summary>
        public int ImageCacheCount { get; set; } = 1;

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (!(obj is CameraInfo task))
                return false;

            return SN == task.SN;
        }

        public override int GetHashCode()
        {
            return SN.GetHashCode();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(UserDefinedName))
                return $"{Brand}, {SN}";
            else
                return UserDefinedName;
        }
    }

}
