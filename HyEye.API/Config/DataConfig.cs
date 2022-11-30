using System;

namespace HyEye.API.Config
{
    [Serializable]
    public class DataConfig
    {
        public bool Enabled { get; set; }

        public string SavePath { get; set; } = "data";

        public DataSaveMode SaveMode { get; set; }
    }

    public enum DataSaveMode
    {
        /// <summary>
        /// 每次拍照都保存
        /// </summary>
        All,

        /// <summary>
        /// 仅保存最后一次
        /// </summary>
        Last
    }
}
