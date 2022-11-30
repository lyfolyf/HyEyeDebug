#region Copyright and Author
/*
Copyright ?LeadTech May.13,2022.
Author zhangjunyang
*/
#endregion

using Autofac;
using HyEye.Services;
using System.Threading;

namespace HyEye.WForm
{
    /// <summary>
    /// 系统准备
    /// </summary>
    public class SystemPrepare
    {
        static FrmLoading frmLoading;
        public static void Run()
        {
            frmLoading = new FrmLoading();
            frmLoading.Show();

            Loading();
        }

        public static void Run(string type, int leftTime)
        {
            frmLoading = new FrmLoading(type, leftTime);
            frmLoading.Show();

            Loading();
        }

        private static void Loading()
        {
            frmLoading.Refresh("加载显示窗体...", 5);
            ConfigRegistration.RegisterAutoFac();

            frmLoading.Refresh("加载显示窗体...", 10);
            ComponentInitialization components = AutoFacContainer.Resolve<ComponentInitialization>();
            components.DisplayControlInit();

            frmLoading.Refresh("加载任务 VPP...", 25);
            components.ToolBlockInit();

            frmLoading.Refresh("加载标定 VPP...", 70);
            components.CalibrationInit();

            frmLoading.Refresh("相机初始化...", 90);
            Thread.Sleep(100);
            components.CameraInit();

            new ServerFactory().InitServices();

            frmLoading.Refresh("加载日志控件...", 95);
            ConfigRegistration.RegisterLog();
            frmLoading.Refresh("完成", 100);
        }
    }
}
