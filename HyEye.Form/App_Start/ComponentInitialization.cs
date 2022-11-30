using Autofac;
using CameraSDK;
using HyEye.API.Repository;
using VisionFactory;

namespace HyEye.WForm
{
    /// <summary>
    /// 组件初始化
    /// </summary>
    class ComponentInitialization
    {
        readonly ICameraRepository cameraRepo;
        readonly ICameraFactoryCollection cameraFactory;

        public ComponentInitialization(ICameraRepository cameraRepo,
            ICameraFactoryCollection cameraFactory)
        {
            this.cameraRepo = cameraRepo;
            this.cameraFactory = cameraFactory;
        }

        public void CameraInit()
        {
            cameraFactory.SetCameraBrands(cameraRepo.GetCameraBrand());
        }

        public void ToolBlockInit()
        {
            ToolBlockComponentSet toolBlockComponentSet = AutoFacContainer.Resolve<ToolBlockComponentSet>();

            toolBlockComponentSet.Init();

            //add by LuoDian @ 20210722 For 显示结果图像   注意，这里的Display init必须要在ToolBlock Init之后，因为在初始化的时候需要用到ToolBlock
            OutputDisplayComponentSet outputDisplayControlSet = AutoFacContainer.Resolve<OutputDisplayComponentSet>();
            outputDisplayControlSet.Init();
        }

        public void CalibrationInit()
        {
            CalibrationComponentSet calibrationComponentSet = AutoFacContainer.Resolve<CalibrationComponentSet>();

            calibrationComponentSet.Init();
        }

        public void DisplayControlInit()
        {
            DisplayComponentSet displayControlSet = AutoFacContainer.Resolve<DisplayComponentSet>();
            displayControlSet.Init();
        }

    }
}
