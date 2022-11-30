using GL.Kit.Log;
using GL.Kit.Net;
using HyEye.API.Repository;
using HyEye.Models.VO;
using LightControllerSDK;
using LightControllerSDK.Models;
using System.Collections.Generic;

namespace HyEye.API
{
    public interface IControllerCollection
    {
        List<ILightController> GetControllers();

        ILightController GetController(ComLightControllerInfo controllerInfo);
    }

    // 注册成了单例
    public class ControllerCollection : IControllerCollection
    {
        // 这里还只能用控制器名称，序列号不是所有厂家都提供的
        readonly Dictionary<string, ILightController> controllers = new Dictionary<string, ILightController>();

        readonly ILightControllerRepository controllerRepo;
        readonly IGLog log;

        public ControllerCollection(
            ILightControllerRepository controllerRepo,
            IGLog log)
        {
            this.controllerRepo = controllerRepo;
            this.log = log;

            this.controllerRepo.ControllerChanged += ControllerRepo_ControllerChanged;
            this.controllerRepo.ControllerDelete += ControllerRepo_ControllerDelete;
        }

        #region Controller 事件

        private void ControllerRepo_ControllerChanged(string controllerName, LightControllerInfoVO newControllerInfo)
        {
            if (controllerName != newControllerInfo.Name)
            {
                if (controllers.ContainsKey(controllerName))
                {
                    ILightController temp = controllers[controllerName];
                    temp.Name = newControllerInfo.Name;

                    controllers.Remove(controllerName);
                    controllers.Add(newControllerInfo.Name, temp);
                }
            }

            if (controllers.ContainsKey(newControllerInfo.Name))
            {
                ILightController controller = controllers[newControllerInfo.Name];

                if (controller.Brand != newControllerInfo.Brand || controller.CommProtocol != newControllerInfo.CommProtocol)
                {
                    controller.Dispose();

                    controllers[newControllerInfo.Name] = ControllerCreater.Create(newControllerInfo, log);
                }
                else
                {
                    controller.ChannelCount = newControllerInfo.ChannelCount;
                    // 刷新配置
                    if (newControllerInfo.CommProtocol == CommProtocol.TCP)
                    {
                        ILightController_Tcp tcpController = (ILightController_Tcp)controller;
                        tcpController.NetworkInfo = newControllerInfo.NetworkInfo;
                    }
                    else if (newControllerInfo.CommProtocol == CommProtocol.COM)
                    {
                        ILightController_Com comController = (ILightController_Com)controller;
                        comController.SerialInfo = newControllerInfo.SerialInfo;
                    }
                    else if (newControllerInfo.CommProtocol == CommProtocol.UDP)
                    {
                        ILightController_Tcp udpController = (ILightController_Tcp)controller;
                        udpController.NetworkInfo = newControllerInfo.NetworkInfo;
                    }
                }
            }
        }

        private void ControllerRepo_ControllerDelete(string name)
        {
            if (!controllers.ContainsKey(name)) return;

            controllers[name].Disconnect();
            controllers.Remove(name);
        }

        #endregion

        public List<ILightController> GetControllers()
        {
            var controllerInfos = controllerRepo.GetControllerInfos();

            List<ILightController> controllers = new List<ILightController>();

            foreach (LightControllerInfoVO controllerInfo in controllerInfos)
            {
                controllers.Add(GetController(controllerInfo));
            }
            return controllers;
        }

        public ILightController GetController(ComLightControllerInfo controllerInfo)
        {
            if (controllers.ContainsKey(controllerInfo.Name))
            {
                return controllers[controllerInfo.Name];
            }
            else
            {
                ILightController controller = ControllerCreater.Create(controllerInfo, log);

                controllers.Add(controllerInfo.Name, controller);

                return controller;
            }
        }

    }
}
