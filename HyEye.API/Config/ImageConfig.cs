using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class ImageConfig
    {
        /// <summary>
        /// 低优先级
        /// </summary>
        public bool LowestPriority { get; set; }

        #region 存图扫描
        /// <summary>
        /// 扫描间隔时间
        /// 单位：分钟 默认：1分钟
        /// </summary>
        public int ScanTime { get; set; } = 1;

        /// <summary>
        /// 剩余空间大小
        /// 单位：GB 默认：10GB
        /// </summary>
        public int MinSpace { get; set; } = 10;

        /// <summary>
        /// 存图缓存限制张数
        /// 单位：张 默认：100张
        /// </summary>
        public int CacheNum { get; set; } = 100;
        #endregion

        public string Root { get; set; } = @"\Images";

        public bool SubDire_Datetime { get; set; } = true;

        public bool SubDire_Task { get; set; } = true;

        public bool SubDire_SN { get; set; } = false;

        /// <summary>
        /// OK/NG
        /// </summary>
        public bool SubDire_ImageFlag { get; set; } = true;

        /// <summary>
        /// 原图/结果图
        /// </summary>
        public bool SubDire_Source { get; set; } = false;

        public SubDireOfSaveImage SubDirectory { get; set; } = SubDireOfSaveImage.Date | SubDireOfSaveImage.Task | SubDireOfSaveImage.Flag;

        public bool SaveByOpenCV { get; set; } = false;

        public List<ImageSaveInfo> SaveInfos { get; set; }

        public ImageDeleteInfo DeleteInfo { get; set; } = new ImageDeleteInfo();
    }

}
