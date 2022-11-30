using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using System.IO;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface IDataRepository
    {
        bool Enabled { get; set; }

        string SavePath { get; set; }

        DataSaveMode SaveMode { get; set; }

        void Save();
    }

    public class DataRepository : IDataRepository
    {
        readonly IGLog log;

        readonly DataConfig config;

        public DataRepository(IGLog log)
        {
            this.log = log;

            config = ApiConfig.DataConfig;
        }

        bool changedToken = false;

        public bool Enabled
        {
            get { return config.Enabled; }
            set
            {
                if (config.Enabled != value)
                {
                    config.Enabled = value;

                    changedToken = true;
                }
            }
        }

        public string SavePath
        {
            get { return PathUtils.GetAbsolutePath(config.SavePath); }
            set
            {
                string path = value.Replace(PathUtils.CurrentDirectory, "");

                if (config.SavePath != path)
                {
                    config.SavePath = path;

                    changedToken = true;
                }
            }
        }

        public DataSaveMode SaveMode
        {
            get { return config.SaveMode; }
            set
            {
                if (config.SaveMode != value)
                {
                    config.SaveMode = value;

                    changedToken = true;
                }
            }
        }

        public void Save()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(ApiConfig.DataConfig);

                log.Info(new ApiLogMessage("数据设置", null, A_Save, R_Success));
            }
        }
    }
}
