using LightControllerSDK.Models;
using System;

namespace HyEye.Models
{
    /// <summary>
    /// 光源控制器
    /// </summary>
    [Serializable]
    public class LightControllerInfo : ComLightControllerInfo
    {
        ChannelLightInfo[] channels;

        public ChannelLightInfo[] Channels
        {
            get
            {
                if (channels == null)
                {
                    channels = new ChannelLightInfo[ChannelCount];
                    for (int i = 0; i < ChannelCount; i++)
                    {
                        channels[i] = new ChannelLightInfo();
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
    public class ChannelLightInfo
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
