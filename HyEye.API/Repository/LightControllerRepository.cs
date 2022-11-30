using AutoMapper;
using GL.Kit.Log;
using GL.Kit.Net;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public delegate void LightControllerChangedHandler(string controllerName, LightControllerInfoVO newControllerInfo);

    public delegate void NamedLightHandler(string controllerName, int channelIndex, string lightName);

    public delegate void ChangeChannelHandler(string controllerName, int srcChannelIndex, int destChannelIndex);

    public interface ILightControllerRepository
    {
        #region 事件

        event NameHandler ControllerDelete;

        event LightControllerChangedHandler ControllerChanged;

        event NamedLightHandler LightNamed;

        event ChangeChannelHandler ChannelExchanged;

        event Action AfterSave;

        #endregion

        bool Changed { get; }

        #region 光源控制器

        List<LightControllerInfoVO> GetControllerInfos();

        LightControllerInfoVO GetControllerInfo(string controllerName);

        void AddController(LightControllerInfoVO controllerInfo);

        LightControllerInfoVO UpdateController(string controllerName, LightControllerInfoVO controllerVO);

        bool DeleteController(string name);

        #endregion

        #region 光源

        void BindLight(string controllerName, int channelIndex, string lightName);

        void ExchangeChannel(string controllerName, int oldChannelIndex, int newChannelIndex);

        void DeleteLight(string controllerName, int channelIndex);

        #endregion

        void Save();
    }

    public class LightControllerRepository : ILightControllerRepository
    {
        readonly IGLog log;
        readonly IMapper mapper;

        readonly List<LightControllerInfo> controllerInfos = ApiConfig.LightControllerConfig.LightControllers;

        public event NameHandler ControllerDelete;
        public event LightControllerChangedHandler ControllerChanged;
        public event NamedLightHandler LightNamed;
        public event ChangeChannelHandler ChannelExchanged;
        public event Action AfterSave;

        public LightControllerRepository(IMapper mapper, IGLog log)
        {
            this.mapper = mapper;
            this.log = log;
        }

        bool changedToken = false;

        public bool Changed
        {
            get { return changedToken; }
        }

        #region 光源控制器

        public List<LightControllerInfoVO> GetControllerInfos()
        {
            return mapper.Map<List<LightControllerInfoVO>>(controllerInfos);
        }

        public LightControllerInfoVO GetControllerInfo(string controllerName)
        {
            LightControllerInfo controller = controllerInfos.FirstOrDefault(a => a.Name == controllerName);
            if (controller == null)
            {
                log.Error(new ApiLogMessage("光源控制器", controllerName, A_Get, R_Fail, "未找到光源控制器"));
                throw new ApiException($"未找到光源控制器[{controllerName}]");
            }

            return mapper.Map<LightControllerInfoVO>(controller);
        }

        public void AddController(LightControllerInfoVO controllerInfo)
        {
            if (controllerInfos.Any(a => a.Name == controllerInfo.Name))
            {
                log.Error(new ApiLogMessage("光源控制器", controllerInfo.Name, A_Add, R_Fail, "名称已存在"));
                throw new ApiException("光源控制器新增失败，名称已存在");
            }

            if (controllerInfo.CommProtocol == CommProtocol.COM)
            {
                if (controllerInfo.SerialInfo == null)
                {
                    log.Error(new ApiLogMessage("光源控制器", controllerInfo.Name, A_Add, R_Fail, "缺少串口信息"));
                    throw new ApiException("光源控制器新增失败，缺少串口信息");
                }

                if (controllerInfos.Any(a => a.SerialInfo != null && a.SerialInfo.PortName == controllerInfo.SerialInfo.PortName))
                {
                    log.Error(new ApiLogMessage("光源控制器", controllerInfo.Name, A_Add, R_Fail, "COM 口被占用"));
                    throw new ApiException("光源控制器新增失败，COM 口被占用");
                }
            }

            if (controllerInfo.CommProtocol == CommProtocol.TCP)
            {
                if (controllerInfo.NetworkInfo == null)
                {
                    log.Error(new ApiLogMessage("光源控制器", controllerInfo.Name, A_Add, R_Fail, "缺少网口信息"));
                    throw new ApiException("光源控制器新增失败，缺少网口信息");
                }

                if (controllerInfos.Any(a => a.NetworkInfo != null && a.NetworkInfo.IP == controllerInfo.NetworkInfo.IP
                                            && a.NetworkInfo.Port == controllerInfo.NetworkInfo.Port))
                {
                    log.Error(new ApiLogMessage("光源控制器", controllerInfo.Name, A_Add, R_Fail, "IP 端口被占用"));
                    throw new ApiException("光源控制器新增失败，IP 端口被占用");
                }
            }

            LightControllerInfo lightController = mapper.Map<LightControllerInfo>(controllerInfo);
            controllerInfos.Add(lightController);
            log.Info(new ApiLogMessage("光源控制器", controllerInfo.Name, A_Add, R_Success));

            changedToken = true;
        }

        public LightControllerInfoVO UpdateController(string controllerName, LightControllerInfoVO controllerInfo)
        {
            int index = controllerInfos.FindIndex(a => a.Name == controllerName);

            if (index == -1)
            {
                log.Error(new ApiLogMessage("光源控制器", controllerName, A_Update, R_Fail, "未找到光源控制器"));
                throw new ApiException("修改光源控制器失败，未找到光源控制器");
            }

            LightControllerInfo controllerInfoEntity = mapper.Map<LightControllerInfo>(controllerInfo);

            // 用拷贝保留原本设置的光源名称
            Array.Copy(controllerInfos[index].Channels, 0,
                controllerInfoEntity.Channels, 0,
                Math.Min(controllerInfos[index].ChannelCount, controllerInfoEntity.ChannelCount));

            controllerInfos[index] = controllerInfoEntity;

            log.Info(new ApiLogMessage("光源控制器", controllerName, A_Update, R_Success));

            changedToken = true;

            controllerInfo = mapper.Map<LightControllerInfoVO>(controllerInfoEntity);

            ControllerChanged?.Invoke(controllerName, controllerInfo);

            return controllerInfo;
        }

        public bool DeleteController(string controllerName)
        {
            LightControllerInfo lightController = controllerInfos.FirstOrDefault(a => a.Name == controllerName);

            if (lightController == null)
            {
                log.Error(new ApiLogMessage("光源控制器", controllerName, A_Delete, R_Fail, "未找到光源控制器"));
                return false;
            }
            else
            {
                controllerInfos.Remove(lightController);
                log.Info(new ApiLogMessage("光源控制器", controllerName, A_Delete, R_Success));

                changedToken = true;

                ControllerDelete?.Invoke(controllerName);

                return true;
            }
        }

        #endregion

        #region 光源

        public void BindLight(string controllerName, int channelIndex, string lightName)
        {
            LightControllerInfo lightController = controllerInfos.FirstOrDefault(a => a.Name == controllerName);
            if (lightController == null)
            {
                log.Error($"命名光源失败，未找到光源控制器[{controllerName}]");
                throw new ApiException($"未找到光源控制器[{controllerName}]");
            }

            if (channelIndex < 1 || channelIndex > lightController.ChannelCount)
            {
                log.Error($"命名光源失败，无效的通道索引[{channelIndex}]");
                throw new ApiException($"无效的通道索引[{channelIndex}]");
            }

            if (lightController.Channels.Any(a => a.LightName == lightName && a.ChannelIndex != channelIndex))
            {
                log.Error($"命名光源失败，光源名称[{lightName}]已存在");
                throw new ApiException($"光源名称[{lightName}]已存在");
            }

            ChannelLightInfo channelLight = lightController.Channels[channelIndex - 1];
            if (channelLight.LightName != lightName)
            {
                channelLight.LightName = lightName;
                log.Info($"光源控制器[{lightController.Name}]命名光源，[{channelLight}]");

                changedToken = true;

                LightNamed?.Invoke(controllerName, channelIndex, lightName);
            }
        }

        public void ExchangeChannel(string controllerName, int srcChannelIndex, int destChannelIndex)
        {
            LightControllerInfo lightController = controllerInfos.FirstOrDefault(a => a.Name == controllerName);
            if (lightController == null)
            {
                log.Error($"切换通道失败，未找到光源控制器[{controllerName}]");
                throw new ApiException($"未找到光源控制器[{controllerName}]");
            }

            if (srcChannelIndex < 1 || srcChannelIndex > lightController.ChannelCount)
            {
                log.Error($"切换通道失败，无效的通道索引[{srcChannelIndex}]");
                throw new ApiException($"无效的通道索引[{srcChannelIndex}]");
            }

            if (destChannelIndex < 1 || destChannelIndex > lightController.ChannelCount)
            {
                log.Error($"切换通道失败，无效的通道索引[{destChannelIndex}]");
                throw new ApiException($"无效的通道索引[{destChannelIndex}]");
            }

            ChannelLightInfo oldChannelLight = lightController.Channels[srcChannelIndex - 1];
            ChannelLightInfo newChannelLight = lightController.Channels[destChannelIndex - 1];

            lightController.Channels[srcChannelIndex - 1] = new ChannelLightInfo { ChannelIndex = srcChannelIndex, LightName = newChannelLight.LightName };
            lightController.Channels[destChannelIndex - 1] = new ChannelLightInfo { ChannelIndex = destChannelIndex, LightName = oldChannelLight.LightName };

            log.Info($"光源控制器[{lightController.Name}]，光源[{oldChannelLight.LightName}]切换至[通道{destChannelIndex}]");

            changedToken = true;

            ChannelExchanged?.Invoke(controllerName, srcChannelIndex, destChannelIndex);
        }

        public void DeleteLight(string controllerName, int channelIndex)
        {
            LightControllerInfo lightController = controllerInfos.FirstOrDefault(a => a.Name == controllerName);
            if (lightController == null)
            {
                log.Error($"未找到光源控制器[{controllerName}]");
                return;
            }

            if (channelIndex < 1 || channelIndex > lightController.ChannelCount)
            {
                log.Error($"删除光源失败，无效的通道索引[{channelIndex}]");
                throw new ApiException($"无效的通道索引[{channelIndex}]");
            }

            ChannelLightInfo channelLight = lightController.Channels[channelIndex - 1];
            log.Info($"光源控制器[{lightController.Name}]删除光源[{channelLight}]");
            channelLight.LightName = null;

            changedToken = true;
        }

        #endregion

        public void Save()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(ApiConfig.LightControllerConfig);

                log.Info(new ApiLogMessage("光控设置", null, A_Save, R_Success));

                AfterSave?.Invoke();
            }
        }
    }
}
