using Autofac;
using GL.Kit.Log;
using HalconDotNet;
using HalconSDK.Engine.DA;
using HalconSDK.Engine.UI;
using HyVision;
using HyVision.Models;
using HyVision.Tools;
using HyVision.Tools.ImageDisplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK.Engine.BL
{
    [Serializable]
    public class HalconProcedureEngineBL : BaseHyUserTool
    {
        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        //用于连线Mapping时显示的Input的名称
        public const string DEFAULT_IMAGE_TYPE_NAME = "默认的图像数据类型";
        public const string DEFAULT_IMAGE_WIDTH_NAME = "默认的图像宽度Width";
        public const string DEFAULT_IMAGE_HEIGHT_NAME = "默认的图像高度Height";

        public override Type ToolEditType => typeof(HalconEngineUI);


        LogPublisher log = AutoFacContainer.Resolve<LogPublisher>();
        HDevEngine hengine;                                 //定义调用hdev程序的引擎       
        HDevProcedureCall hprocall;                         //定义hdev程序执行实例 

        protected override void Run2(string subName)
        {
            try
            {
                //log.Info($"开始执行[{Name}]");
                if (hengine == null)
                {
                    //log.Info($"Halcon引擎未初始化，开始初始化！");
                    if (!File.Exists(FilePath))
                        OnException($"未找到指定的 hdvp 文件！文件路径：{FilePath}", new HyVisionException($"未找到指定的 hdvp 文件！文件路径：{FilePath}"));

                    string strFileFolder = FilePath.Substring(0, FilePath.LastIndexOf('\\') + 1);
                    string strProcedureName = FilePath.Substring(FilePath.LastIndexOf('\\') + 1, FilePath.LastIndexOf('.') - FilePath.LastIndexOf('\\') - 1);

                    hengine = new HDevEngine();
                    hengine.SetProcedurePath(strFileFolder);
                    var Program = new HDevProcedure(strProcedureName);
                    hprocall = new HDevProcedureCall(Program);

                    //HDevProgram hDevProgram = new HDevProgram();
                    //HDevProcedure hDevProcedure = new HDevProcedure(hDevProgram, strProcedureName);
                    //hDevProcedure.GetInputCtrlParamNames();
                    //hDevProcedure.GetInputIconicParamNames();
                    //HDevProcedureCall hDevProcedureCall = new HDevProcedureCall(hDevProcedure);
                    //HTuple h = new HTuple();
                    //var a =  h.TupleSelect(0);
                    //HOperatorSet.TupleLength(h, out  b);

                    //log.Info($"Halcon引擎初始化完成！");
                }

                string strStartTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                HalconDataConvert dataConvert = new HalconDataConvert();

                //log.Info($"Halcon引擎开始写入输入参数！");
                //设定输入参数
                if (Inputs != null && Inputs.Count > 0)
                {
                    for (int i = 0; i < Inputs.Count; i++)
                    {
                        //log.Info($"Halcon引擎开始写入参数[{Inputs[i].Name}]！");
                        if (Inputs[i].GetType() != typeof(HyTerminal))
                            OnException($"参数 {Inputs[i].Name} 不是一个有效的 {typeof(HyTerminal)} 类型参数！当前类型：{Inputs[i].GetType()}", new HyVisionException($"参数 {Inputs[i].Name} 不是一个有效的 {typeof(HyTerminal)} 类型参数！当前类型：{Inputs[i].GetType()}"));

                        HyTerminal inputTerminal = (HyTerminal)Inputs[i];

                        if (inputTerminal.ConvertTargetType == null)
                            OnException($"未能获取到参数 {Inputs[i].Name} 的ConvertTargetType类型！", new HyVisionException($"未能获取到参数 {Inputs[i].Name} 的ConvertTargetType类型！"));

                        if (inputTerminal.Value == null)
                            OnException($"参数 {Inputs[i].Name} 没有值，请确认参数连线是否正常！", new HyVisionException($"参数 {Inputs[i].Name} 没有值，请确认参数连线是否正常！"));

                        //通过子类的ConvertTargetType属性来判断转换成哪一种数据类型
                        if (inputTerminal.ConvertTargetType.ToUpper().Equals("HTuple".ToUpper()))
                        {
                            HTuple value = dataConvert.ConvertToHTuple(inputTerminal);
                            hprocall.SetInputCtrlParamTuple(inputTerminal.Name, value);
                        }
                        else if (inputTerminal.ConvertTargetType.ToUpper().Equals("HObject".ToUpper()))
                        {
                            if (inputTerminal.Value.GetType() != typeof(HyImage))
                                OnException($"参数 {Inputs[i].Name} 的值类型不正确！当前类型为 {inputTerminal.Value.GetType()}，要求的类型为 {typeof(HyImage)}", new HyVisionException($"参数 {Inputs[i].Name} 的值类型不正确！当前类型为 {inputTerminal.Value.GetType()}，要求的类型为 {typeof(HyImage)}"));

                            HObject value = dataConvert.ConvertToHObject(inputTerminal);
                            hprocall.SetInputIconicParamObject(inputTerminal.Name, value);
                        }
                        //else if (inputTerminal.ConvertTargetType.ToUpper().Equals("HyImage".ToUpper()))
                        //{
                        //    HyImage hyImage = (HyImage)inputTerminal.Value;
                        //    if (hyImage == null || hyImage.Image == null)
                        //        OnException($"运行[{Name}]时发生异常", new HyVisionException($"参数 {Inputs[i].Name} 没有图像数据！"));

                        //    ImageDataStr data = new ImageDataStr();
                        //    if (hyImage.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                        //        data.Type = EHalconImageDataType.halcon_byte;
                        //    else if (hyImage.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale ||
                        //        hyImage.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565)
                        //        data.Type = EHalconImageDataType.halcon_uint2;
                        //    else
                        //        OnException($"运行[{Name}]时发生异常", new HyVisionException($"参数 {Inputs[i].Name} 的图像类型不合法！当前图像类型：{hyImage.Image.PixelFormat}"));
                        //    data.nWidth = hyImage.Width;
                        //    data.nHeight = hyImage.Height;
                        //    data.pData = hyImage.Image.GetHbitmap();

                        //    if (hyImage.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed ||
                        //        hyImage.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                        //        data.nChannel = 1;
                        //    else if (hyImage.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565)
                        //        data.nChannel = 3;
                        //    else
                        //        OnException($"运行[{Name}]时发生异常", new HyVisionException($"参数 {Inputs[i].Name} 的图像类型不合法！当前图像类型：{hyImage.Image.PixelFormat}"));

                        //    HyTerminal tempTermain = new HyTerminal(inputTerminal.Name, typeof(ImageDataStr));
                        //    tempTermain.Value = data;

                        //    HObject value = dataConvert.ConvertToHObject(tempTermain);
                        //    hprocall.SetInputIconicParamObject(tempTermain.Name, value);
                        //}
                        else if (inputTerminal.ConvertTargetType.ToUpper().Equals("HXLD".ToUpper()) ||
                            inputTerminal.ConvertTargetType.ToUpper().Equals("HRegion".ToUpper()))
                        {
                            HObject value = dataConvert.HyRoiToHObject((BaseHyROI)inputTerminal.Value);
                            hprocall.SetInputIconicParamObject(inputTerminal.Name, value);
                        }

                        //log.Info($"Halcon引擎输入参数[{Inputs[i].Name}]写入完成！");
                    }
                }
                //log.Info($"Halcon引擎所有输入参数写入完成！");

                //log.Info($"Halcon引擎开始执行算法！");
                hprocall.Execute();
                //log.Info($"Halcon引擎算法执行完成！");


                //log.Info($"Halcon引擎开始获取输出结果！");
                //获取输出结果
                if (Outputs != null && Outputs.Count > 0)
                {
                    for (int i = 0; i < Outputs.Count; i++)
                    {
                        //log.Info($"Halcon引擎开始获取结果[{Outputs[i].Name}]！");
                        if (Outputs[i].GetType() != typeof(HyTerminal))
                            OnException($"参数 {Outputs[i].Name} 不是一个有效的 {typeof(HyTerminal)} 类型参数！当前类型：{Outputs[i].GetType()}", new HyVisionException($"参数 {Outputs[i].Name} 不是一个有效的 {typeof(HyTerminal)} 类型参数！当前类型：{Outputs[i].GetType()}"));

                        HyTerminal outputTerminal = Outputs[i];

                        if (outputTerminal.ConvertTargetType == null)
                            OnException($"未能获取到参数 {Outputs[i].Name} 的ConvertTargetType类型！", new HyVisionException($"未能获取到参数 {Outputs[i].Name} 的ConvertTargetType类型！"));

                        if (outputTerminal.ConvertTargetType.ToUpper().Equals("HTuple".ToUpper()))
                        {
                            HTuple value = hprocall.GetOutputCtrlParamTuple(outputTerminal.Name);
                            if (value == null)
                                continue;
                            outputTerminal.Value = value?.ToString();
                        }
                        else if (outputTerminal.ConvertTargetType.ToUpper().Equals("HObject".ToUpper()))
                        {
                            HObject value = hprocall.GetOutputIconicParamObject(outputTerminal.Name);
                            if (value == null || !value.IsInitialized() || value.CountObj() < 1)
                                continue;
                            Bitmap bitMap = dataConvert.HObject2Bitmap8(value);
                            HyImage image = new HyImage(bitMap, true);
                            outputTerminal.Value = image;
                        }
                        else if (outputTerminal.ConvertTargetType.ToUpper().Equals("HImage".ToUpper()))
                        {
                            HImage value = hprocall.GetOutputIconicParamImage(outputTerminal.Name);
                            if (value == null || !value.IsInitialized() || value.CountObj() < 1)
                                continue;
                            Bitmap bitMap = dataConvert.HObject2Bitmap8(value);
                            HyImage image = new HyImage(bitMap, true);
                            outputTerminal.Value = image;
                        }
                        else if (outputTerminal.ConvertTargetType.ToUpper().Equals("HObject_24".ToUpper()))
                        {
                            HObject value = hprocall.GetOutputIconicParamObject(outputTerminal.Name);
                            if (value == null || !value.IsInitialized() || value.CountObj() < 1)
                                continue;
                            Bitmap bitMap;
                            dataConvert.HObject2Bitmap24(value, out bitMap);
                            HyImage image = new HyImage(bitMap, false);
                            outputTerminal.Value = image;
                        }
                        else if (outputTerminal.ConvertTargetType.ToUpper().Equals("HImage_24".ToUpper()))
                        {
                            HImage value = hprocall.GetOutputIconicParamImage(outputTerminal.Name);
                            if (value == null || !value.IsInitialized() || value.CountObj() < 1)
                                continue;
                            Bitmap bitMap;
                            dataConvert.HObject2Bitmap24(value, out bitMap);
                            HyImage image = new HyImage(bitMap, false);
                            outputTerminal.Value = image;
                        }
                        else if (outputTerminal.ConvertTargetType.ToUpper().Equals("HRegion".ToUpper()))
                        {
                            HRegion halconValue = hprocall.GetOutputIconicParamRegion(outputTerminal.Name);
                            if (halconValue == null || !halconValue.IsInitialized() || halconValue.CountObj() < 1)
                                continue;
                            HyDefectRegion hyRegion = dataConvert.HRegionToHyDefectRegion(halconValue);
                            outputTerminal.Value = hyRegion;
                        }
                        else if (outputTerminal.ConvertTargetType.ToUpper().Equals("HXLD".ToUpper()))
                        {
                            HXLD halconValue = hprocall.GetOutputIconicParamXld(outputTerminal.Name);
                            if (halconValue == null || !halconValue.IsInitialized() || halconValue.CountObj() < 1)
                                continue;
                            HyDefectXLD hyXLD = dataConvert.HXLDContToHyDefectXLD((HXLDCont)halconValue);
                            outputTerminal.Value = hyXLD;
                        }
                        //log.Info($"Halcon引擎的结果[{Outputs[i].Name}]获取完成！");
                    }

                    if (Outputs.Contains(DataReport.BL.HalconDataReportGeneraterBL.INPUT_NAME_ALGORITHM_PRO_START_TIME))
                    {
                        //log.Info($"Halcon引擎开始获取结果[{DataReport.BL.HalconDataReportGeneraterBL.INPUT_NAME_ALGORITHM_PRO_START_TIME}]！");
                        Outputs[DataReport.BL.HalconDataReportGeneraterBL.INPUT_NAME_ALGORITHM_PRO_START_TIME].Value = strStartTime;
                        //log.Info($"Halcon引擎的结果[{DataReport.BL.HalconDataReportGeneraterBL.INPUT_NAME_ALGORITHM_PRO_START_TIME}]获取完成！");
                    }
                }
                //log.Info($"Halcon引擎所有输出结果获取完毕！");
                //log.Info($"[{Name}]执行完成！");
            }
            catch (Exception ex)
            {
                OnException($"运行[{Name}]时发生异常: {ex.Message}", new HyVisionException(ex.Message));
            }

        }

        protected override void Dispose(bool disposing)
        {
            //对于HObject类型的输出数据进行释放
            for (int i = 0; i < Outputs.Count; i++)
            {
                if (Outputs[i].Value != null)
                {
                    if (Outputs[i].ValueType == typeof(HObject) ||
                        Outputs[i].ValueType == typeof(HImage) ||
                        Outputs[i].ValueType == typeof(HRegion) ||
                        Outputs[i].ValueType == typeof(HXLD))
                    {
                        ((HObject)Outputs[i].Value).Dispose();
                        Outputs[i].Value = null;
                    }
                }
            }
        }

        public override object Clone(bool containsImage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            
        }
    }
}
