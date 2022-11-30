using CameraSDK.Models;

namespace HyEye.Models.VO
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CameraInfoVO
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
        /// 连接方式（网口/U口）
        /// </summary>
        public ConnectionType ConnectionType { get; set; }

        /// <summary>
        /// true:软触发; false:硬触发
        /// </summary>
        public bool SoftTrigger { get; set; }

        /// <summary>
        /// 图像缓存节点数
        /// </summary>
        public int ImageCacheCount { get; set; }

        public CameraParams Params { get; set; }

        public TriggerSource TriggerSource
        {
            get { return SoftTrigger ? TriggerSource.Software : TriggerSource.Extern; }
        }

        public bool IsVirtualCamera()
        {
            return SN == string.Empty;
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
