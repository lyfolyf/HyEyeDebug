namespace HyEye.Models
{
    public struct ChannelValue
    {
        /// <summary>
        /// 通道序号
        /// </summary>
        public int ChannelIndex { get; set; }

        /// <summary>
        /// 亮度
        /// </summary>
        public int Lightness { get; set; }

        /// <summary>
        /// true: 常亮
        /// </summary>
        public bool LightState { get; set; }
    }
}
