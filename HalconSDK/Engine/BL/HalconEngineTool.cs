using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Threading;
using System.Linq;
using HalconDotNet;
using HalconSDK.Engine.UI;
using HyVision.Tools;
using HyVision.Models;
using HyVision.Tools.ImageDisplay;
using HyRoiManager;
using GL.Kit.Log;
using Autofac;




namespace HalconSDK.Engine.BL
{


    [Serializable]
    public class HalconEngineTool : BaseHyUserTool
    {

        public HalconEngineTool()
        {
            //Name = "Halcon引擎工具";
            HalconEngineManager = new HalconEngineManager();

        }


        public override Type ToolEditType => typeof(HalconEngineTool_UI);


        //add by LuoDian @ 20211102 添加日志输出
        private LogPublisher log;

        private HalconEngineManager HalconEngineManager;

        public string HalconFilePath { get; set; }

        public List<string> SelectFuncNames { get; set; }

        public int UISelectedIndex { get; set; }

        public List<ParameterInfo> Parameters { get; set; } = new List<ParameterInfo>();

        [XmlIgnore]
        public bool IsInitialization { get; set; }


        private bool _isDebugMode;
        public bool IsDebugMode
        {
            get => _isDebugMode;
            set
            {
                _isDebugMode = value;

                if (HalconEngineManager.IsDebugMode != _isDebugMode)
                {
                    HalconEngineManager.IsDebugMode = _isDebugMode;
                }
            }

        }

        public override object Clone(bool containsData)
        {
            return null;
        }

        protected override void Dispose(bool disposing)
        {

        }

        // update by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        protected override void Run2(string subName)
        {
            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            string Err = "";
            //1.初始化
            if (IsInitialization == false)
            {

                if (IsDebugMode == true)
                {
                    log.Warn($"*************** Halcon引擎进入Debug模式 ***************");
                }

                Err = HalconEngineManager.InitializeHalconEngine(HalconFilePath, SelectFuncNames);
                if (Err != "OK")
                {
                    OnException($"（Halcon引擎）初始化Halcon引擎出错，请查看Halcon文件路径是否配置正确！", new Exception($"初始化引擎失败!"));
                }
                Err = SetLocalParamsToHalconEngine();
                if (Err != "OK")
                {
                    OnException($"（Halcon引擎）给Halcon引擎传入表格参数时出错！", new Exception($"设置引擎参数出错!"));
                }

                Err = HalconEngineManager.RunInitFunction();
                if (Err != "OK")
                {
                    OnException($"（Halcon引擎）运行初始化函数出错！", new Exception($"运行初始化函数出错!"));
                }
                log.Info($"（Halcon引擎）初始化完成！");
                IsInitialization = true;
            }

            //2.设置halcon引擎输入参数
            SetInputsToHalconEngine();
            log.Info($"（Halcon引擎）设置输入参数完成！");
            //3.执行halcon引擎
            DateTime start = DateTime.Now;
            Err = RunHalconEngine();
            double diff = (DateTime.Now - start).TotalMilliseconds;
            log.InfoFormat("Halcon引擎执行耗时：{0}ms", diff);

            if (Err != "OK")
            {
                log.Error(Err);
            }
            log.Info($"（Halcon引擎）算法执行完成！");
            //4.将halcon引擎输入，转换到Outputs
            GetResultToOutputs();
            log.Info($"（Halcon引擎）获取结果并转换到Outputs完成！");
        }

        private string SetLocalParamsToHalconEngine()
        {
            try
            {
                Dictionary<string, string> NameMapping = new Dictionary<string, string>();

                for (int i = 0; i < Parameters.Count; i++)
                {
                    ParameterInfo ParamInfo = Parameters[i];
                    if (ParamInfo.InputOutputType == "输入" && ParamInfo.IsConnect == false)
                    {
                        string FuncParamName = $"{ParamInfo.FunctionName},{ParamInfo.ParamName}";

                        if (ParamInfo.DataType == HalconDataType.Image.ToString() || ParamInfo.DataType == HalconDataType.List_Image.ToString())
                        {
                            //保留

                        }
                        else if (ParamInfo.DataType == HalconDataType.XLD.ToString())
                        {
                            //保留
                        }
                        else if (ParamInfo.DataType == HalconDataType.Region.ToString())
                        {
                            // ROI参数需要集成新的图像显示控件进行设置获取

                            if (ParamInfo.Roidata == null)
                            {
                                continue;
                            }

                            if (ParamInfo.Roidata.ImageWidth > HalconEngineManager.SystemWidth)
                            {
                                HalconEngineManager.SystemWidth = (uint)ParamInfo.Roidata.ImageWidth;
                            }
                            if (ParamInfo.Roidata.ImageHeight > HalconEngineManager.SystemHeight)
                            {
                                HalconEngineManager.SystemHeight = (uint)ParamInfo.Roidata.ImageHeight;
                            }

                            HOperatorSet.GenEmptyObj(out HObject RoiRegion);
                            HOperatorSet.GenRegionRuns(out  RoiRegion, ParamInfo.Roidata.RowIndex.ToArray(), ParamInfo.Roidata.StartColumn.ToArray(),
                                ParamInfo.Roidata.EndColumn.ToArray());
                            HalconEngineManager.SetValue(FuncParamName, RoiRegion);

                        }
                        else if (ParamInfo.DataType == HalconDataType.内部输出.ToString())
                        {
                            NameMapping[FuncParamName] = ParamInfo.MappingInfo;
                        }
                        else if (ParamInfo.DataType == HalconDataType.Int.ToString() || ParamInfo.DataType == HalconDataType.List_Int.ToString())
                        {
                            HalconEngineManager.SetValue(FuncParamName, new HTuple(int.Parse(ParamInfo.Value)));
                        }
                        else if (ParamInfo.DataType == HalconDataType.Double.ToString() || ParamInfo.DataType == HalconDataType.List_Double.ToString())
                        {
                            HalconEngineManager.SetValue(FuncParamName, new HTuple(double.Parse(ParamInfo.Value)));

                        }
                        else if (ParamInfo.DataType == HalconDataType.Bool.ToString() || ParamInfo.DataType == HalconDataType.List_Bool.ToString())
                        {
                            HalconEngineManager.SetValue(FuncParamName, new HTuple(bool.Parse(ParamInfo.Value)));

                        }
                        else if (ParamInfo.DataType == HalconDataType.String.ToString() || ParamInfo.DataType == HalconDataType.List_String.ToString())
                        {
                            HalconEngineManager.SetValue(FuncParamName, new HTuple(ParamInfo.Value));
                        }
                    }
                }

                HalconEngineManager.SetNameMapping(NameMapping);
            }
            catch (Exception err)
            {
                return err.ToString();
            }

            return "OK";
        }

        private void SetInputsToHalconEngine()
        {
            HalconDataConvert dataConvert = new HalconDataConvert();

            //输入只做图像和数值类型的转换
            if (Inputs != null)
            {
                for (int i = 0; i < Inputs.Count; i++)
                {
                    //add by LuoDian @ 20220120 需要判断下输入数据是否为null，如果是的话，不做转换
                    if (Inputs[i] == null || Inputs[i].Value == null)
                    {
                        //if (Inputs[i] != null)
                        //{
                        //    log.Error($"输入参数[{Inputs[i].Name}]值为空！");
                        //}
                        //else
                        //{
                        //    log.Error($"第[{i+1}]个输入参数值为空！");
                        //}

                        continue;
                    }

                    if (!string.IsNullOrEmpty(Inputs[i].From))
                    {
                        if (Inputs[i].ValueType == typeof(HyImage))
                        {
                            int channels = 0;
                            HyImage hyImage = (HyImage)Inputs[i].Value;

                            if (hyImage?.Image?.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                                channels = 1;
                            else if (hyImage?.Image?.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale ||
                                hyImage?.Image?.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565)
                                channels = 2;
                            else if (hyImage?.Image?.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ||
                                        hyImage?.Image?.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppRgb ||
                                        hyImage?.Image?.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                                channels = 3;

                            log.InfoFormat("Bitmap是{0}通道", channels);

                            DateTime start = DateTime.Now;
                            HObject value = dataConvert.ConvertToHObject(Inputs[i]);
                            double diff = (DateTime.Now - start).TotalMilliseconds;
                            log.InfoFormat("Bitmap转化HObject耗时：{0}ms", diff);

                            //int Time1 = Environment.TickCount;
                            // dataConvert.Bitmap24ToHObject(((HyImage)Inputs[i].Value).Image, out HObject value);
                            //int Time2 = Environment.TickCount;
                            //log.Info($"Bitmap转化HObject耗时：{Time2 - Time1} ms");

                             HalconEngineManager.SetValue(Inputs[i].Description, value);
                        }
                        else if (Inputs[i].ValueType == typeof(HyDefectRegion))
                        {
                            //保留
                        }
                        else if (Inputs[i].ValueType == typeof(HyDefectXLD))
                        {
                            //保留
                        }
                        else if(Inputs[i].ValueType == typeof(HyRoiManager.RoiData))
                        {
                            HyRoiManager.RoiData roiData = Inputs[i].Value as HyRoiManager.RoiData;

                            if (roiData != null)
                            {
                                HOperatorSet.GenEmptyObj(out HObject RoiRegion);
                                HOperatorSet.GenRegionRuns(out RoiRegion, roiData.RowIndex.ToArray(), roiData.StartColumn.ToArray(),
                                    roiData.EndColumn.ToArray());
                                HalconEngineManager.SetValue(Inputs[i].Description, RoiRegion);
                            }

                        }
                        else if (Inputs[i].ValueType == typeof(List<HyImage>))
                        {
                            List<HyImage> Imgs = Inputs[i].Value as List<HyImage>;
                            HOperatorSet.GenEmptyObj(out HObject HalconImgs);
                            for (int k = 0; k < Imgs.Count; k++)
                            {
                                HyTerminal Img = new HyTerminal($"HyImage{k}", typeof(HyImage));
                                Img.Value = Imgs[k];
                                HObject HalconImg = dataConvert.ConvertToHObject(Img);
                                HOperatorSet.ConcatObj(HalconImgs, HalconImg, out HalconImgs);
                            }

                            HalconEngineManager.SetValue(Inputs[i].Description, HalconImgs);
                        }
                        else if (Inputs[i].ValueType == typeof(List<int>))
                        {
                            List<int> value = Inputs[i].Value as List<int>;
                            HTuple HalconValue = new HTuple();
                            for (int k = 0; k < value.Count; k++)
                            {
                                HOperatorSet.TupleConcat(HalconValue, new HTuple(value[i]), out HalconValue);
                            }

                            HalconEngineManager.SetValue(Inputs[i].Description, HalconValue);
                        }
                        else if (Inputs[i].ValueType == typeof(List<double>))
                        {
                            List<double> value = Inputs[i].Value as List<double>;
                            HTuple HalconValue = new HTuple();
                            for (int k = 0; k < value.Count; k++)
                            {
                                HOperatorSet.TupleConcat(HalconValue, new HTuple(value[i]), out HalconValue);
                            }

                            HalconEngineManager.SetValue(Inputs[i].Description, HalconValue);
                        }
                        else if (Inputs[i].ValueType == typeof(List<bool>))
                        {
                            List<bool> value = Inputs[i].Value as List<bool>;
                            HTuple HalconValue = new HTuple();
                            for (int k = 0; k < value.Count; k++)
                            {
                                HOperatorSet.TupleConcat(HalconValue, new HTuple(value[i]), out HalconValue);
                            }

                            HalconEngineManager.SetValue(Inputs[i].Description, HalconValue);
                        }
                        else if (Inputs[i].ValueType == typeof(List<string>))
                        {
                            List<string> value = Inputs[i].Value as List<string>;
                            HTuple HalconValue = new HTuple();
                            for (int k = 0; k < value.Count; k++)
                            {
                                HOperatorSet.TupleConcat(HalconValue, new HTuple(value[i]), out HalconValue);
                            }

                            HalconEngineManager.SetValue(Inputs[i].Description, HalconValue);
                        }
                        else if (Inputs[i].ValueType == typeof(int) || Inputs[i].ValueType == typeof(double)
                             || Inputs[i].ValueType == typeof(bool) || Inputs[i].ValueType == typeof(string))
                        {
                            HTuple value = dataConvert.ConvertToHTuple(Inputs[i]);
                            HalconEngineManager.SetValue(Inputs[i].Description, value);
                        }
                    }
                }
            }
        }

        private string RunHalconEngine()
        {
            return HalconEngineManager.RunFunctionsExceptInit();
        }

        private int GetResultToOutputs()
        {
            int ret = 0;
            if (Outputs != null)
            {
                try
                {
                    HalconDataConvert dataConvert = new HalconDataConvert();

                    for (int i = 0; i < Outputs.Count; i++)
                    {
                        if (Outputs[i].ValueType == typeof(HyImage))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object Img);

                            //HObject HalconImage = HalconEngineManager.GetIconicParams(Outputs[i].Description);
                            HObject HalconImage = Img as HObject;
                            HOperatorSet.CountChannels(Img as HObject, out HTuple chanels);

                            Bitmap ResultImg = null;
                            log.Info($"引擎结果输出 1 张图片,通道数为 {chanels}");
                            DateTime start = DateTime.Now;
                            if (chanels.L == 1)
                            {
                                ResultImg = dataConvert.HObject2Bitmap8(HalconImage);
                            }
                            else if (chanels.L == 3)
                            {
                                dataConvert.HObject2Bitmap24(HalconImage, out ResultImg);

                                //dataConvert.HObjectToBitmap24
                            }
                            double diff = (DateTime.Now - start).TotalMilliseconds;
                            log.InfoFormat("HObject转化为Bitmap耗时：{0}", diff);

                            if (ResultImg != null)
                            {
                                Outputs[i].Value = new HyImage(ResultImg);
                            }
                        }
                        else if (Outputs[i].ValueType == typeof(List<HyDefectXLD>))
                        {
                            //保留
                            //Outputs[i].Value = null;
                        }
                        else if (Outputs[i].ValueType == typeof(HyDefectRegion))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HRegion);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HRegion == null || (HRegion as HObject).CountObj() < 1)
                                continue;

                            HObject DefectRegion = HRegion as HObject;
                            Outputs[i].Value = dataConvert.HobjectTolstHyDefectXLD(DefectRegion);

                        }
                        else if (Outputs[i].ValueType == typeof(HyDefectXLD))
                        {
                            //保留
                            //Outputs[i].Value = null;
                        }
                        else if (Outputs[i].ValueType == typeof(HyRoiManager.RoiData))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object RoiRegion);
                            HObject Roi = RoiRegion as HObject;

                            if (Roi != null)
                            {
                                HOperatorSet.Union1(Roi, out HObject regionUnion);
                                HOperatorSet.CountObj(regionUnion, out HTuple number);
                                log.Info($"（Halcon引擎）结果转换到output，类型为RoiData的缺陷Union1后个数为：{number}");                              
                                if (number.L > 0)
                                {
                                    HOperatorSet.GetRegionRuns(regionUnion, out HTuple row, out HTuple columnBegin, out HTuple columnEnd);

                                    RoiData rd = new RoiData();
                                    rd.ImageWidth = (int)HalconEngineManager.SystemWidth;
                                    rd.ImageHeight = (int)HalconEngineManager.SystemHeight;
                                    rd.RowIndex = row.ToIArr().ToList();
                                    rd.StartColumn = columnBegin.ToIArr().ToList();
                                    rd.EndColumn = columnEnd.ToIArr().ToList();

                                    Outputs[i].Value = rd;
                                }
                            }
                        }
                        else if (Outputs[i].ValueType == typeof(int))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleInt);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleInt == null || (HtupleInt as HTuple).Length < 1)
                                continue;

                            Outputs[i].Value = (HtupleInt as HTuple).I;
                        }
                        else if (Outputs[i].ValueType == typeof(double))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleDouble);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleDouble == null || (HtupleDouble as HTuple).Length < 1)
                                continue;

                            Outputs[i].Value = (HtupleDouble as HTuple).D;
                        }
                        else if (Outputs[i].ValueType == typeof(string))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleString);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleString == null || (HtupleString as HTuple).Length < 1)
                            {
                                Outputs[i].Value = string.Empty;
                                continue;
                            }

                            Outputs[i].Value = (HtupleString as HTuple).S;
                        }
                        else if (Outputs[i].ValueType == typeof(bool))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleBool);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleBool == null || (HtupleBool as HTuple).Length < 1)
                                continue;

                            Outputs[i].Value = (HtupleBool as HTuple).I;
                        }
                        //add by LuoDian @ 20220118 添加一个 IntPtr 类型的输出数据
                        else if (Outputs[i].ValueType == typeof(IntPtr))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleByteArray);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleByteArray == null || (HtupleByteArray as HTuple).Length < 1)
                                continue;

                            Outputs[i].Value = (HtupleByteArray as HTuple).IP;
                        }
                        //add by LuoDian @ 20220302 添加一个 List<int> 类型的输出数据
                        else if (Outputs[i].ValueType == typeof(List<int>))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleInt);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleInt == null || (HtupleInt as HTuple).Length < 1)
                                continue;

                            if ((HtupleInt as HTuple).Length == 1)
                            {
                                List<int> lstOutput = new List<int>();
                                lstOutput.Add((HtupleInt as HTuple).I);
                                Outputs[i].Value = lstOutput;
                            }
                            else
                                Outputs[i].Value = (HtupleInt as HTuple).ToIArr().ToList();
                        }
                        //add by LuoDian @ 20220302 添加一个 List<double> 类型的输出数据
                        else if (Outputs[i].ValueType == typeof(List<double>))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleInt);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleInt == null || (HtupleInt as HTuple).Length < 1)
                                continue;

                            if ((HtupleInt as HTuple).Length == 1)
                            {
                                List<double> lstOutput = new List<double>();
                                lstOutput.Add((HtupleInt as HTuple).D);
                                Outputs[i].Value = lstOutput;
                            }
                            else
                                Outputs[i].Value = (HtupleInt as HTuple).DArr.ToList();
                        }
                        //add by LuoDian @ 20220302 添加一个 List<string> 类型的输出数据
                        else if (Outputs[i].ValueType == typeof(List<string>))
                        {
                            HalconEngineManager.GetValue(Outputs[i].Description, out object HtupleInt);

                            //add by LuoDian @ 20220218 要先判断输出是否有值
                            if (HtupleInt == null || (HtupleInt as HTuple).Length < 1)
                                continue;

                            if ((HtupleInt as HTuple).Length == 1)
                            {
                                List<string> lstOutput = new List<string>();
                                lstOutput.Add((HtupleInt as HTuple).S);
                                Outputs[i].Value = lstOutput;
                            }
                            else
                                Outputs[i].Value = (HtupleInt as HTuple).SArr.ToList();
                        }
                        else if (Outputs[i].ValueType == typeof(List<HyImage>))
                        {
                            List<HyImage> HyImgs = new List<HyImage>();
                            HalconEngineManager.GetValue(Outputs[i].Description, out object Img);
                            HObject HalconImages = Img as HObject;
                            HOperatorSet.CountObj(HalconImages, out HTuple number);
                            for (int k = 0; k < number; k++)
                            {
                                Bitmap ResultImg = null;
                                HOperatorSet.SelectObj(HalconImages, out HObject objectSelected, k + 1);
                                HOperatorSet.CountChannels(objectSelected, out HTuple chanels);
                                log.Info($"引擎结果输出 {number} 张图片: 第 {k + 1} 张的通道数Chanel = {chanels}");

                                if (chanels.L == 1)
                                {
                                    ResultImg = dataConvert.HObject2Bitmap8(objectSelected);
                                }
                                else if (chanels.L == 3)
                                {
                                    dataConvert.HObject2Bitmap24(objectSelected, out ResultImg);
                                }
                                else
                                {
                                    throw new NotImplementedException($"{chanels} 通道Halcon引擎还没有实现！");
                                }

                                if (ResultImg != null)
                                {
                                    HyImgs.Add(new HyImage(ResultImg));
                                }
                            }
                            Outputs[i].Value = HyImgs;
                        }

                    }

                }
                catch (Exception ex)
                {
                    OnException($"Halcon引擎运行结束后，取结果转化为Outputs过程中出错\r\n{ex}", new Exception($"Halcon结果转化到Outputs出错"));
                }
            }
            else
            {
                ret = -1;
            }
            return ret;
        }

        public void SetProcedurePath(string HdplFilePath)
        {
            HalconEngineManager.SetProcedurePath(HdplFilePath);
        }

        public string[] GetProcedureNames()
        {
            string Err = HalconEngineManager.GetProcedureNames(out string[] FuncNames);

            if (Err == "OK")
            {
                return FuncNames;
            }

            return null;
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            string Err = "";
            //1.初始化
            if (IsInitialization == false)
            {
                if (IsDebugMode == true)
                {
                    log.Warn($"*************** Halcon引擎进入Debug模式 ***************");
                }

                Err = HalconEngineManager.InitializeHalconEngine(HalconFilePath, SelectFuncNames);
                if (Err != "OK")
                {
                    OnException($"[Halcon引擎] 初始化Halcon引擎出错，请查看Halcon文件路径是否配置正确！", new Exception($"初始化引擎失败!"));
                }
                Err = SetLocalParamsToHalconEngine();
                if (Err != "OK")
                {
                    OnException($"[Halcon引擎] 给Halcon引擎传入表格参数时出错！", new Exception($"设置引擎参数出错!"));
                }

                Err = HalconEngineManager.RunInitFunction();
                if (Err != "OK")
                {
                    OnException($"[Halcon引擎] 运行初始化函数出错！", new Exception($"运行初始化函数出错!"));
                }
                log.Info($"[Halcon引擎] 初始化完成！");
                IsInitialization = true;
            }

            return IsInitialization;
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            IsInitialization = false;
        }
    }
}
