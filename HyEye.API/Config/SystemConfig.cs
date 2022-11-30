namespace HyEye.API.Config
{
    public class SystemConfig
    {
        public class RunTimeConfig
        {
            /// <summary>
            /// 拍照超时时间
            /// </summary>
            public int AcquireImageTimeout { get; set; } = 3000;

            /// <summary>
            /// R 指令超时时间
            /// </summary>
            public int CmdRTimeout { get; set; } = 500;
        }

        /// <summary>
        /// 模拟图片路径
        /// </summary>
        public string Simulation { get; set; }

        /// <summary>
        /// 自动启动
        /// </summary>
        public bool AutoStart = false;

        /// <summary>
        /// 删除任务同时删除 VPP
        /// </summary>
        public bool DeleteVPP { get; set; } = false;

        /// <summary>
        /// VPP 保存模式
        /// <para>false：保存完整工具</para>
        /// <para>保存不带图像或结果的工具</para>
        /// </summary>
        public bool VPPExcludeDataBindings { get; set; } = false;

        public RunTimeConfig RunTime { get; set; } = new RunTimeConfig();

        /// <summary>
        /// 配置自动保存
        /// </summary>
        public bool AutoSaveConfig = false;

        /// <summary>
        /// 保存周期（）天
        /// </summary>
        public int SaveType = 1;

        /// <summary>
        /// 配置自动保存路径
        /// </summary>
        public string ConfigSavePath { get; set; }

    }
}
