using LightControllerSDK.Models;

namespace HyEye.Models.VO
{
    /// <summary>
    /// 光源控制器
    /// </summary>
    public class LightControllerInfoVO : ComLightControllerInfo
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool Connected { get; set; }

        ChannelLightInfoVO[] channels;

        public ChannelLightInfoVO[] Channels
        {
            get
            {
                if (channels == null)
                {
                    channels = new ChannelLightInfoVO[ChannelCount];
                    for (int i = 0; i < ChannelCount; i++)
                    {
                        channels[i] = new ChannelLightInfoVO();
                        channels[i].ChannelIndex = i + 1;
                    }
                }
                return channels;
            }
            set { channels = value; }
        }
    }

    /// <summary>
    /// 通道和光源
    /// </summary>
    public class ChannelLightInfoVO
    {
        public int ChannelIndex { get; set; }

        public string LightName { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(LightName))
                return $"通道{ChannelIndex}";
            else
                return $"通道{ChannelIndex}({LightName})";
        }
    }

}
