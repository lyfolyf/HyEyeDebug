using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using System.IO;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface ISystemRepository : IBasicRepository, IRunTimeRepository
    {
        void Save();
    }

    public interface IBasicRepository
    {
        event SimulationHandle SimulationChanged;

        string SimulationPath { get; set; }

        /// <summary>
        /// 自动启动
        /// </summary>
        bool AutoStart { get; set; }

        /// <summary>
        /// 删除任务同时删除 VPP
        /// </summary>
        bool DeleteVPP { get; set; }

        /// <summary>
        /// VPP 保存模式
        /// <para>false：保存完整工具</para>
        /// <para>保存不带图像或结果的工具</para>
        /// </summary>
        bool VPPExcludeDataBindings { get; set; }

        bool AutoSaveConfig { get; set; }

        int SaveType { get; set; }

        string ConfigSavePath { get; set; }
    }

    public interface IRunTimeRepository
    {
        /// <summary>
        /// 拍照超时时间
        /// </summary>
        int AcquireImageTimeout { get; set; }

        /// <summary>
        /// R 指令超时时间
        /// </summary>
        int CmdRTimeout { get; set; }
    }

    public class SystemRepository : ISystemRepository
    {
        readonly IGLog log;

        readonly SystemConfig config;

        public SystemRepository(IGLog log)
        {
            this.log = log;

            config = ApiConfig.SystemConfig;
        }

        #region Basic

        public event SimulationHandle SimulationChanged;

        public bool AutoStart
        {
            get { return config.AutoStart; }
            set { config.AutoStart = value; }
        }

        public bool DeleteVPP
        {
            get { return config.DeleteVPP; }
            set { config.DeleteVPP = value; }
        }

        public bool VPPExcludeDataBindings
        {
            get { return config.VPPExcludeDataBindings; }
            set { config.VPPExcludeDataBindings = value; }
        }

        public string SimulationPath
        {
            get
            {
                if (config.Simulation == null)
                {
                    config.Simulation = "simulation\\" + ApiConfig.SetupConfig.StartMaterial;
                }
                return PathUtils.GetAbsolutePath(config.Simulation);
            }
            set
            {
                string path = value == null ?
                    "simulation\\" + ApiConfig.SetupConfig.StartMaterial : value.Replace(PathUtils.CurrentDirectory, "");

                if (config.Simulation != path)
                {
                    config.Simulation = path;
                    SimulationChanged?.Invoke(value);
                }
            }
        }

        #endregion

        #region ConfigSave

        public bool AutoSaveConfig
        {
            get { return config.AutoSaveConfig; }
            set { config.AutoSaveConfig = value; }
        }

        public int SaveType
        {
            get { return config.SaveType; }
            set { config.SaveType = value; }
        }

        public string ConfigSavePath
        {
            get
            {
                if (config.ConfigSavePath == null || string.IsNullOrEmpty(config.ConfigSavePath))
                {
                    config.ConfigSavePath = "configsave\\";
                }
                return PathUtils.GetAbsolutePath(config.ConfigSavePath);
            }
            set
            {
                string path = value == null ?
                    "configsave\\" : value.Replace(PathUtils.CurrentDirectory, "");

                if (config.ConfigSavePath != path)
                {
                    config.ConfigSavePath = path;
                }
            }
        }
        #endregion

        #region RunTime

        public int AcquireImageTimeout
        {
            get { return config.RunTime.AcquireImageTimeout; }
            set { config.RunTime.AcquireImageTimeout = value; }
        }

        public int CmdRTimeout
        {
            get { return config.RunTime.CmdRTimeout; }
            set { config.RunTime.CmdRTimeout = value; }
        }

        #endregion

        public void Save()
        {
            ApiConfig.Save(ApiConfig.SystemConfig);

            log.Info(new ApiLogMessage("系统设置", null, A_Save, R_Success));
        }
    }
}
