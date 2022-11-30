namespace HyEye.Models
{
    public class DisplayLayoutInfo
    {
        public string TaskName { get; set; }

        public string AcquireImageName { get; set; }

        /// <summary>
        /// -1 表示不显示
        /// </summary>
        public int Index { get; set; }
    }
}
