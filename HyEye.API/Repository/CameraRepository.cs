using AutoMapper;
using CameraSDK;
using CameraSDK.Models;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public delegate void RenameCameraHandle(string sn, string name);

    public interface ICameraRepository
    {
        event RenameCameraHandle AfterCameraRename;
        event Action<List<CameraInfoVO>> AfterSetCameraInfo;

        bool ConfigChanged { get; }

        ReadOnlyCollection<CameraBrand> GetCameraBrand();

        void SetCameraBrand(List<CameraBrand> cameraBrands);

        CameraInfoVO GetCameraInfo(string sn);

        List<CameraInfoVO> GetCameras();

        List<CameraInfoVO> GetCamerasWithVirtual();

        void SetCameraInfo(List<CameraInfoVO> cameras);

        /// <summary>
        /// 验证相机列表中的相机 SN 是否唯一
        /// <para>因为存在这种情况：同时勾选大华和海康的相机，同一个相机会搜出来 2 个</para>
        /// </summary>
        bool CheckSingle();

        /// <summary>
        /// 对于不同品牌重复搜索的结果，就要删除
        /// </summary>
        void DeleteCameraInfo(CameraInfoVO camera);

        bool CameraNameExists(string name);

        bool IsVirtualCamera(string sn);

        string CtiPath { get; set; }

        void RenameCamera(string sn, string name);

        void SetSoftTrigger(string sn, bool softTrigger);

        void SetImageCacheCount(string sn, int count);

        void SetIP(string sn, string ip, string subnetMask, string defaultGateway);

        ICamera GetCamera(string sn);

        List<CameraParamInfoListVO> GetParamInfoList();

        List<CameraParamInfoVO> GetParamInfos(string cameraSN);

        void Save();
    }

    public class CameraRepository : ICameraRepository
    {
        public event RenameCameraHandle AfterCameraRename;
        public event Action<List<CameraInfoVO>> AfterSetCameraInfo;

        readonly CameraConfig config = ApiConfig.CameraConfig;

        readonly ICameraFactoryCollection cameraFactory;
        readonly IMapper mapper;
        readonly IGLog log;

        public bool ConfigChanged { get; private set; } = false;

        readonly CameraInfoVO virtualCamera = new CameraInfoVO
        {
            SN = string.Empty,
            UserDefinedName = "虚拟相机"
        };

        public CameraRepository(ICameraFactoryCollection cameraFactory, IMapper mapper, IGLog log)
        {
            this.cameraFactory = cameraFactory;
            this.mapper = mapper;
            this.log = log;

            if (config.CameraInfos == null)
                config.CameraInfos = new List<CameraInfo>();

            cameraFactory.SetParamInfos(config.ParamInfoList);
            cameraFactory.SetCtiPath(config.CtiPath);
        }

        public ReadOnlyCollection<CameraBrand> GetCameraBrand()
        {
            return config.CameraBrands?.AsReadOnly();
        }

        public void SetCameraBrand(List<CameraBrand> cameraBrands)
        {
            config.CameraBrands = new List<CameraBrand>(cameraBrands);
            ConfigChanged = true;
        }

        public CameraInfoVO GetCameraInfo(string sn)
        {
            if (sn == string.Empty)
                return virtualCamera;

            CameraInfo camera = config.CameraInfos.FirstOrDefault(a => a.SN == sn);

            return mapper.Map<CameraInfoVO>(camera);
        }

        public List<CameraInfoVO> GetCameras()
        {
            return mapper.Map<List<CameraInfoVO>>(config.CameraInfos);
        }

        public List<CameraInfoVO> GetCamerasWithVirtual()
        {
            List<CameraInfoVO> cameras = new List<CameraInfoVO>
            {
                virtualCamera
            };

            cameras.AddRange(mapper.Map<List<CameraInfoVO>>(config.CameraInfos));

            return cameras;
        }

        public void SetCameraInfo(List<CameraInfoVO> cameraVOs)
        {
            List<CameraInfo> cameras = mapper.Map<List<CameraInfo>>(cameraVOs);

            config.CameraInfos = cameras;
            ConfigChanged = true;

            if (CheckSingle())
            {
                AfterSetCameraInfo?.Invoke(cameraVOs);
            }
        }

        public bool CheckSingle()
        {
            return config.CameraInfos.Select(a => a.SN).Distinct().Count() == config.CameraInfos.Count;
        }

        public void DeleteCameraInfo(CameraInfoVO camera)
        {
            config.CameraInfos.Remove(a => a.Brand == camera.Brand && a.SN == camera.SN);

            if (CheckSingle())
            {
                List<CameraInfoVO> cameraVOs = mapper.Map<List<CameraInfoVO>>(config.CameraInfos);

                AfterSetCameraInfo?.Invoke(cameraVOs);
            }
        }

        public bool CameraNameExists(string name)
        {
            if (config.CameraInfos == null) return false;

            return config.CameraInfos.Any(a => a.UserDefinedName == name);
        }

        public bool IsVirtualCamera(string sn)
        {
            return sn == string.Empty;
        }

        public string CtiPath
        {
            get { return config.CtiPath; }
            set { config.CtiPath = value; }
        }

        public void RenameCamera(string sn, string name)
        {
            CameraInfo camera = config.CameraInfos.FirstOrDefault(a => a.SN == sn);

            if (camera != null)
            {
                camera.UserDefinedName = name;
                ConfigChanged = true;

                AfterCameraRename?.Invoke(sn, name);
            }
            else
            {
                log.Error(new CameraApiLogMessage(camera.ToString(), A_Rename, R_Fail, "未找到相机配置"));
            }
        }

        public void SetSoftTrigger(string sn, bool softTrigger)
        {
            CameraInfo camera = config.CameraInfos.FirstOrDefault(a => a.SN == sn);

            if (camera != null)
            {
                camera.SoftTrigger = softTrigger;
                ConfigChanged = true;

                log.Info(new CameraApiLogMessage(camera.ToString(), A_SetParams, R_Success, $"触发模式：{(softTrigger ? "软触发" : "硬触发")}"));
            }
            else
            {
                log.Error(new CameraApiLogMessage(camera.ToString(), A_SetParams, R_Fail, "设置触发模式失败，未找到相机配置"));
            }
        }

        public void SetImageCacheCount(string sn, int count)
        {
            if (count > 10 || count < 0) throw new IndexOutOfRangeException("缓存节点数量范围在 1- 10 之间");

            CameraInfo cameraInfo = config.CameraInfos.FirstOrDefault(a => a.SN == sn);
            if (cameraInfo != null)
            {
                cameraInfo.ImageCacheCount = count;
                ConfigChanged = true;

                log.Info(new CameraApiLogMessage(cameraInfo.ToString(), A_SetParams, R_Success, $"图像缓存数量：{count}"));
            }
            else
            {
                log.Error(new CameraApiLogMessage(cameraInfo.ToString(), A_SetParams, R_Fail, "设置图像缓存数量失败，未找到相机配置"));
            }
        }

        public void SetIP(string sn, string ip, string subnetMask, string defaultGateway)
        {
            CameraInfo camera = config.CameraInfos.FirstOrDefault(a => a.SN == sn);

            if (camera != null)
            {
                camera.IP = ip;
                camera.SubnetMask = subnetMask;
                camera.DefaultGateway = defaultGateway;
                ConfigChanged = true;
            }
            else
            {
                log.Error(new CameraApiLogMessage(camera.ToString(), A_SetParams, R_Fail, "设置 IP 失败，未找到相机配置"));
            }
        }

        public ICamera GetCamera(string sn)
        {
            if (IsVirtualCamera(sn))
            {
                log.Error(new CameraApiLogMessage("虚拟相机", A_GetCamera, R_Fail, "当前为虚拟相机"));
                throw new ApiException("获取相机失败，当前为虚拟相机");
            }

            CameraInfo cameraInfo = config.CameraInfos.FirstOrDefault(a => a.SN == sn);
            if (cameraInfo == null)
            {
                log.Error(new CameraApiLogMessage("虚拟相机", A_GetCamera, R_Fail, "未找到相机配置"));
                throw new ApiException("获取相机失败，未找到相机配置");
            }

            ComCameraInfo comCameraInfo = mapper.Map<ComCameraInfo>(cameraInfo);

            return cameraFactory.GetCamera(comCameraInfo);
        }

        public List<CameraParamInfoListVO> GetParamInfoList()
        {
            return mapper.Map<List<CameraParamInfoListVO>>(config.ParamInfoList);
        }

        public List<CameraParamInfoVO> GetParamInfos(string cameraSN)
        {
            if (IsVirtualCamera(cameraSN)) return null;

            CameraInfo cameraInfo = config.CameraInfos.FirstOrDefault(a => a.SN == cameraSN);
            if (cameraInfo == null) return null;

            ComCameraInfo comCameraInfo = mapper.Map<ComCameraInfo>(cameraInfo);

            ICamera camera = cameraFactory.GetCamera(comCameraInfo);
            if (camera == null) return null;

            CameraParamInfo[] paramInfos = camera.GetParamInfos();

            return mapper.Map<List<CameraParamInfoVO>>(paramInfos);
        }

        public void Save()
        {
            if (ConfigChanged)
            {
                ApiConfig.Save(ApiConfig.CameraConfig);
                ConfigChanged = false;

                log.Info(new ApiLogMessage("相机设置", null, A_Save, R_Success));
            }
        }

    }
}
