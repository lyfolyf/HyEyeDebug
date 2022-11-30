using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class DisplayLayoutConfig
    {
        public int RowCount { get; set; } = 2;

        public int ColumnCount { get; set; } = 2;

        public bool AutoSize { get; set; } = true;

        public int Width { get; set; } = 400;

        public int Height { get; set; } = 300;

        public int MinSideLength { get; set; } = 50;

        public DisplayType Type { get; set; } = DisplayType.Hy;

        /// <summary>
        /// 显示结果图
        /// </summary>
        public bool ShowRetImage { get; set; } = true;

        public List<DisplayLayoutInfo> DisplayLayouts { get; set; } = new List<DisplayLayoutInfo>();
    }
}
