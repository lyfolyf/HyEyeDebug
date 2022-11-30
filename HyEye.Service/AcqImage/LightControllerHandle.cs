using GL.Kit.Log;
using HyEye.Models;
using LightControllerSDK;
using System.Collections.Generic;
using System.Diagnostics;

namespace HyEye.Services
{
    // update by LuoDian @ 20211118 当多个相机共用同一个光源的时候，需要在执行Task指令的时候判断下共有哪些相机共用这一个光源
    // 这里就需要修改访问级别, 从默认级别修改为public
    public class LightControllerHandle
    {
        public ILightController LightController { get; set; }

        public List<ChannelValue> ChannelValues { get; set; } = new List<ChannelValue>();

        readonly IGLog log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

        public void Connect()
        {
            LightController.Connect();
        }

        public void Open()
        {
            if (!LightController.Connected)
            {
                LightController.Connect();
                //log.Info($"------------------------{LightController.Name} open light connect: {openLightWatch.ElapsedMilliseconds}");
            }

            //update by WuBing @ 20211112 条纹同轴不支持单通道设置亮度
            var len = ChannelValues.Count;

            int[] chs = new int[len];
            int[] lightnesses = new int[len];

            foreach (ChannelValue cv in ChannelValues)
            {
                //try
                //{
                //    LightController.SetLightness(cv.ChannelIndex, cv.Lightness);
                //}
                //catch (System.Exception ex)
                //{

                //}
                chs[cv.ChannelIndex - 1] = cv.ChannelIndex;
                lightnesses[cv.ChannelIndex - 1] = cv.Lightness;
            }
            
            LightController.SetMultiLightness(chs, lightnesses);
        }

        /// <summary>
        /// 设置光源亮度为 0
        /// </summary>
        public void Close()
        {
            //foreach (ChannelValue cv in ChannelValues)
            //{
            //    //LightController.SetLightness(cv.ChannelIndex, 0);
            //    if (!cv.LightState)
            //        LightController.OffChannel(cv.ChannelIndex);
            //}
            var len = ChannelValues.Count;
            int[] chs = new int[len];
            int[] lightnesses = new int[len];

            foreach (ChannelValue cv in ChannelValues)
            {
                //try
                //{
                //    LightController.SetLightness(cv.ChannelIndex, cv.Lightness);
                //}
                //catch (System.Exception ex)
                //{

                //}
                chs[cv.ChannelIndex - 1] = cv.ChannelIndex;
                lightnesses[cv.ChannelIndex - 1] = cv.Lightness;
            }
            
            LightController.OffMultiChannel(chs);
        }

        /// <summary>
        /// 断开光源控制器连接
        /// </summary>
        public void Disconnect()
        {
            LightController.Disconnect();
        }
    }
}
