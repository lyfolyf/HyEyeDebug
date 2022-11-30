using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

using HalconDotNet;
using HalconSDK.Engine.UI;
using HyVision.Tools;
using HyVision.Models;
using HyVision.Tools.ImageDisplay;
using GL.Kit.Log;
using Autofac;

namespace HalconSDK.Engine.BL
{


    [Serializable]
    public class HalconProgramEngineBL_2 : BaseHyUserTool
    {

        public override Type ToolEditType
        {
            get
            {
                return typeof(HalconEngineUI_2);
            }
        }

        //add by LuoDian @ 20211102 添加日志输出
        LogPublisher log;

        public string HalconFilePath { get; set; }

        public string ModelImagePath { get; set; }

        public string CsvFilePath { get; set; }

        public string MainAxis { get; set; } = "x";

        public float PixelRes { get; set; } = 0.079f;

        public List<string> SelectFuncNames { get; set; }

        public int UIIndex { get; set; }

        public List<ParameterInfo> Parameters { get; set; } = new List<ParameterInfo>();

        [XmlIgnore]
        public bool IsInitialization { get; set; }

        [XmlIgnore]
        public HDevProgram hDevProgram { get; set; }


        //字典的key格式为：函数名，参数名
        [XmlIgnore]
        public Dictionary<string, HObject> dnyIconicVal { get; set; } = new Dictionary<string, HObject>();

        [XmlIgnore]
        public Dictionary<string, HTuple> dnyCtrlVal { get; set; } = new Dictionary<string, HTuple>();

        [XmlIgnore]
        public Dictionary<string, string> dnyParamNameMapping { get; set; } = new Dictionary<string, string>();




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
            string folderPath = Directory.GetCurrentDirectory();
            
            if(log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            //1.初始化
            if (IsInitialization == false)
            {
                try
                {
                    //RunAppliedROI2Image_01();
                    InitializeHalconEngine();
                    InitializeDictionary();
                    IsInitialization = true;
                }
                catch
                {
                    OnException($"初始化Halcon引擎出错，请查看RunAppliedROI2Image_01参数配置是否有误！", new Exception($"建模失败!"));
                }
            }

            //2.输入转成Halcon类型
            InputsToDictionary();
            log.Info($"【算法引擎】输入数据转换完成！");
            //3.
            RunEachFunction();
            log.Info($"【算法引擎】算法执行完成！");
            //4.遍历Outputs,将结果值转化传出
            ResultToOutputs();
            log.Info($"【算法引擎】输出数据转换完成！");
            Directory.SetCurrentDirectory(folderPath);
        }

        //输入转换为Halcon类型，供运行时取值使用
        private void InputsToDictionary()
        {
            HalconDataConvert dataConvert = new HalconDataConvert();

            //输入只做图像和数值类型的转换
            if (Inputs != null)
            {
                for (int i = 0; i < Inputs.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Inputs[i].From))
                    {
                        //add by LuoDian @ 20220104 为了兼容Halcon引擎有多个输入图像，但是运行时只需要有其中某一个或某几个图像有数据的情况
                        if (Inputs[i] == null)
                            continue;

                        if (Inputs[i].ValueType == typeof(HyImage))
                        {
                            HObject value = dataConvert.ConvertToHObject(Inputs[i]);
                            dnyIconicVal[Inputs[i].Description] = value;
                        }
                        else if (Inputs[i].ValueType == typeof(HyDefectRegion))
                        {

                        }
                        else if (Inputs[i].ValueType == typeof(HyDefectXLD))
                        {

                        }
                        else
                        {
                            HTuple value = dataConvert.ConvertToHTuple(Inputs[i]);
                            dnyCtrlVal[Inputs[i].Description] = value;
                        }
                    }
                }
            }
        }

        //1.初始化dnyParamNameMapping  2.初始化dnyIconicVal 和 dnyCtrlVal的输入参数值（输出是在运行后取值）
        private void InitializeDictionary()
        {
            dnyParamNameMapping.Clear();
            for (int i = 0; i < Parameters.Count; i++)
            {
                ParameterInfo ParamInfo = Parameters[i];
                if (ParamInfo.InputOutputType == "输入" && ParamInfo.IsConnect == false)
                {
                    string FuncParamName = $"{ParamInfo.FunctionName},{ParamInfo.ParamName}";

                    if (ParamInfo.DataType == HalconDataType.Image.ToString())
                    {
                        //保留
                        HObject Img = new HObject();
                        HOperatorSet.GenEmptyObj(out Img);
                        dnyIconicVal[FuncParamName] = Img;
                    }
                    else if (ParamInfo.DataType == HalconDataType.XLD.ToString())
                    {
                        //保留
                    }
                    else if (ParamInfo.DataType == HalconDataType.Region.ToString())
                    {
                        HObject ROIs = new HObject();
                        ROIs.GenEmptyObj();
                        if (!string.IsNullOrEmpty(ParamInfo.SelectedRoiIndex))
                        {
                            string[] RoiIndexs = ParamInfo.SelectedRoiIndex.Split(',');

                            for (int j = 0; j < RoiIndexs.Length; j++)
                            {
                                ROIs = ROIs.ConcatObj(dnyIconicVal["AppliedROI2Image_01,ROIPixel"].SelectObj(int.Parse(RoiIndexs[j])));
                            }
                        }
                        dnyIconicVal[FuncParamName] = ROIs;

                    }
                    else if (ParamInfo.DataType == HalconDataType.内部输出.ToString())
                    {
                        dnyParamNameMapping[FuncParamName] = ParamInfo.MappingInfo;
                    }
                    else if (ParamInfo.DataType == HalconDataType.Int.ToString())
                    {
                        dnyCtrlVal[FuncParamName] = new HTuple(int.Parse(ParamInfo.Value));
                    }
                    else if (ParamInfo.DataType == HalconDataType.Double.ToString())
                    {
                        dnyCtrlVal[FuncParamName] = new HTuple(double.Parse(ParamInfo.Value));
                    }
                    else if (ParamInfo.DataType == HalconDataType.Bool.ToString())
                    {
                        dnyCtrlVal[FuncParamName] = new HTuple(bool.Parse(ParamInfo.Value));
                    }
                    else if (ParamInfo.DataType == HalconDataType.String.ToString())
                    {
                        dnyCtrlVal[FuncParamName] = new HTuple(ParamInfo.Value);
                    }

                }
            }
        }

        public int CreateModel()
        {
            int ReturnVal = 0;
            ReturnVal = InitializeHalconEngine();
            if (ReturnVal != 0)
            {
                return -1;
            }

            ReturnVal = CreateAffineMatrix();
            if (ReturnVal != 0)
            {
                return -2;
            }

            ReturnVal = ReadCsvFile(out string RoiInfo);
            if (ReturnVal != 0)
            {
                return -3;
            }

            ReturnVal = CreateAffineROI(RoiInfo);
            if (ReturnVal != 0)
            {
                return -4;
            }
            return 0;
        }

        public int RunAppliedROI2Image_01()
        {
            int ReturnVal = 0;
            try
            {
                hDevProgram = new HDevProgram(HalconFilePath);

                string FuncName = "AppliedROI2Image_01";
                HDevProcedure Procedure = new HDevProcedure(hDevProgram, FuncName);
                HDevProcedureCall ProcedureCall = Procedure.CreateCall();

                ParameterInfo PInfoImagePath = Parameters.Find(p =>
                               p.FunctionName == FuncName && p.ParamName == "ModelPath");
                ParameterInfo PInfoCSVPath = Parameters.Find(p =>
                               p.FunctionName == FuncName && p.ParamName == "ROICoordPath");
                ParameterInfo PInfoRes = Parameters.Find(p =>
                               p.FunctionName == FuncName && p.ParamName == "Res");

                ProcedureCall.SetInputCtrlParamTuple("ModelPath", PInfoImagePath.Value);
                ProcedureCall.SetInputCtrlParamTuple("ROICoordPath", PInfoCSVPath.Value);
                ProcedureCall.SetInputCtrlParamTuple("Res", double.Parse(PInfoRes.Value));
                ProcedureCall.Execute();
                dnyIconicVal["AppliedROI2Image_01,ROIPixel"] = ProcedureCall.GetOutputIconicParamObject("ROIPixel");
                dnyIconicVal["AppliedROI2Image_01,ModelImage"] = ProcedureCall.GetOutputIconicParamObject("ModelImage");
                dnyCtrlVal["AppliedROI2Image_01,ROIName"] = ProcedureCall.GetOutputCtrlParamTuple("ROIName");
            }
            catch (Exception ex)
            {
                OnException($"执行AppliedROI2Image_01过程中出错", new Exception($"建模失败!"));
                ReturnVal = -1;
            }

            return ReturnVal;
        }

        public int RunEachFunction()
        {
            string FuncParamName = "";
            try
            {
                for (int i = 0; i < SelectFuncNames.Count; i++)
                {
                    if (SelectFuncNames[i] == "AppliedROI2Image_01")
                    {
                        continue;
                    }
                    HDevProcedure Procedure = new HDevProcedure(hDevProgram, SelectFuncNames[i]);
                    HDevProcedureCall ProcedureCall = Procedure.CreateCall();
                    //1.赋值
                    HTuple InputIconicParms = Procedure.GetInputIconicParamNames();
                    for (int j = 0; j < InputIconicParms.Length; j++)
                    {
                        FuncParamName = $"{SelectFuncNames[i]},{InputIconicParms.TupleSelect(j).S}";
                        dnyParamNameMapping.TryGetValue(FuncParamName, out string NewName);
                        if (string.IsNullOrEmpty(NewName))
                        {
                            //add by LuoDian @ 20220104 为了兼容Halcon引擎有多个输入图像，但是运行时只需要有其中某一个或某几个图像有数据的情况
                            if (!dnyIconicVal.ContainsKey(FuncParamName) || dnyIconicVal[FuncParamName] == null)
                                continue;

                            ProcedureCall.SetInputIconicParamObject(InputIconicParms.TupleSelect(j).S, dnyIconicVal[FuncParamName]);
                        }
                        else
                        {
                            //add by LuoDian @ 20220104 为了兼容Halcon引擎有多个输入图像，但是运行时只需要有其中某一个或某几个图像有数据的情况
                            if (!dnyIconicVal.ContainsKey(NewName) || dnyIconicVal[NewName] == null)
                                continue;

                            ProcedureCall.SetInputIconicParamObject(InputIconicParms.TupleSelect(j).S, dnyIconicVal[NewName]);
                        }
                    }

                    HTuple InputCtrlParms = Procedure.GetInputCtrlParamNames();
                    for (int j = 0; j < InputCtrlParms.Length; j++)
                    {
                        FuncParamName = $"{SelectFuncNames[i]},{InputCtrlParms.TupleSelect(j).S}";
                        dnyParamNameMapping.TryGetValue(FuncParamName, out string NewName);
                        if (string.IsNullOrEmpty(NewName))
                        {
                            //add by LuoDian @ 20220104 为了兼容Halcon引擎有多个输入图像，但是运行时只需要有其中某一个或某几个图像有数据的情况
                            if (!dnyCtrlVal.ContainsKey(FuncParamName) || dnyCtrlVal[FuncParamName] == null)
                                continue;

                            ProcedureCall.SetInputCtrlParamTuple(InputCtrlParms.TupleSelect(j).S, dnyCtrlVal[FuncParamName]);
                        }
                        else
                        {
                            //add by LuoDian @ 20220104 为了兼容Halcon引擎有多个输入图像，但是运行时只需要有其中某一个或某几个图像有数据的情况
                            if (!dnyCtrlVal.ContainsKey(NewName) || dnyCtrlVal[NewName] == null)
                                continue;

                            ProcedureCall.SetInputCtrlParamTuple(InputCtrlParms.TupleSelect(j).S, dnyCtrlVal[NewName]);
                        }
                    }
                    //2.运行
                    ProcedureCall.Execute();

                    //3.取值
                    HTuple OutputIconicParms = Procedure.GetOutputIconicParamNames();
                    for (int k = 0; k < OutputIconicParms.Length; k++)
                    {
                        FuncParamName = $"{SelectFuncNames[i]},{OutputIconicParms.TupleSelect(k).S}";
                        dnyIconicVal[FuncParamName] = ProcedureCall.GetOutputIconicParamObject(OutputIconicParms.TupleSelect(k).S);
                    }

                    HTuple OutputCtrlParms = Procedure.GetOutputCtrlParamNames();
                    for (int k = 0; k < OutputCtrlParms.Length; k++)
                    {
                        FuncParamName = $"{SelectFuncNames[i]},{OutputCtrlParms.TupleSelect(k).S}";
                        dnyCtrlVal[FuncParamName] = ProcedureCall.GetOutputCtrlParamTuple(OutputCtrlParms.TupleSelect(k).S);
                    }
                }
            }
            catch (Exception ex)
            {
                OnException($"{ex}\n\r运行Halcon函数出错，错误信息 {FuncParamName}", new Exception($"运行Halcon函数出错！"));
                return -1;
            }

            return 0;
        }

        private int ResultToOutputs()
        {
            int ret = 0;
            if (Outputs != null)
            {
                try
                {
                    //Halcon输出的Hobject和Htuple可以是多个，只输出第一个
                    HalconDataConvert dataConvert = new HalconDataConvert();

                    for (int i = 0; i < Outputs.Count; i++)
                    {
                        if (Outputs[i].ValueType == typeof(HyImage))
                        {
                            //只支持8位灰度图
                            HObject HalconImage = dnyIconicVal[Outputs[i].Description];

                            Bitmap bp = null;
                            HOperatorSet.CountChannels(HalconImage, out HTuple chanels);
                            if (chanels.I == 1)
                            {
                                 bp = dataConvert.HObject2Bitmap8(HalconImage);
                            }
                            else if(chanels.I == 3)
                            {
                                dataConvert.HObject2Bitmap24(HalconImage, out  bp);
                            }

                            if (bp != null)
                            {
                                Outputs[i].Value = new HyImage(bp);
                            }
                           
                        }
                        else if (Outputs[i].ValueType == typeof(List<HyDefectXLD>))
                        {
                            HObject DefectRegion = dnyIconicVal[Outputs[i].Description];
                            Outputs[i].Value = dataConvert.HobjectTolstHyDefectXLD(DefectRegion);
                        }
                        else if (Outputs[i].ValueType == typeof(HyDefectRegion))
                        {
                            HObject DefectRegion = dnyIconicVal[Outputs[i].Description];
                            Outputs[i].Value = dataConvert.HobjectTolstHyDefectXLD(DefectRegion);
                        }
                        else if (Outputs[i].ValueType == typeof(HyDefectXLD))
                        {
                            HObject DefectXLD = dnyIconicVal[Outputs[i].Description];
                            Outputs[i].Value = dataConvert.HXLDContToHyDefectXLD(DefectXLD);
                        }
                        else if (Outputs[i].ValueType == typeof(int))
                        {
                            Outputs[i].Value = dnyCtrlVal[Outputs[i].Description].I;
                        }
                        else if (Outputs[i].ValueType == typeof(double))
                        {
                            Outputs[i].Value = dnyCtrlVal[Outputs[i].Description].D;
                        }
                        else if (Outputs[i].ValueType == typeof(string))
                        {
                            Outputs[i].Value = dnyCtrlVal[Outputs[i].Description].S;
                        }
                        else if (Outputs[i].ValueType == typeof(bool))
                        {
                            Outputs[i].Value = Convert.ToBoolean(dnyCtrlVal[Outputs[i].Description].I);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnException($"{ex}\r\n运行结束后，结果转化为Outputs过程出错", new Exception($"结果转化到Outputs出错"));
                }
            }
            else
            {
                ret = -1;
            }
            return ret;
        }

        public int InitializeHalconEngine()
        {
            try
            {
                hDevProgram = new HDevProgram(HalconFilePath);
            }
            catch (Exception ex)
            {
                OnException($"加载算法文件过程中出错！错误信息：{ex.Message}", new Exception($"加载算法文件失败!"));
                return -1;
            }
            return 0;

        }

        public string[] GetFuncNames()
        {
            HTuple FuncNames = null;
            if (hDevProgram != null)
            {
                FuncNames = hDevProgram.GetUsedProcedureNames();
            }

            return FuncNames == null ? null : FuncNames.ToSArr();
        }

        public int CreateAffineMatrix()
        {
            try
            {
                string FuncName = "SetupCoordAxis_02";
                HDevProcedure Procedure = new HDevProcedure(hDevProgram, FuncName);
                HDevProcedureCall ProcedureCall = Procedure.CreateCall();

                ProcedureCall.SetInputCtrlParamTuple("ModelPath", ModelImagePath);
                ProcedureCall.SetInputCtrlParamTuple("MainAxis", MainAxis);
                ProcedureCall.Execute();
                dnyCtrlVal["AffineHomMat"] = ProcedureCall.GetOutputCtrlParamTuple("AffineHomMat");
                dnyIconicVal["ModelImage"] = ProcedureCall.GetOutputIconicParamObject("ModelImage");
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        public int ReadCsvFile(out string strRoiInfo)
        {
            string OneLine = "";
            string[] OneLineSplit = null;
            int OneLineLength = 0;
            bool IsFirstLine = false;
            strRoiInfo = "";
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                FileStream fs = new FileStream(CsvFilePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                while ((OneLine = sr.ReadLine()) != null)
                {
                    if (IsFirstLine == false)
                    {
                        IsFirstLine = true;
                    }
                    else
                    {
                        OneLineSplit = OneLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        OneLineLength = OneLineSplit.Length;
                        //字符串格式 name1,circle,1,1,1#name2,circle,1,1,1#
                        switch (OneLineSplit[2])
                        {
                            case "Circle":
                                stringBuilder.Append($"{OneLineSplit[1]},{OneLineSplit[2]},{OneLineSplit[3]},{OneLineSplit[4]},{OneLineSplit[5]}#");
                                break;

                            case "Rectangle1":
                                stringBuilder.Append($"{OneLineSplit[1]},{OneLineSplit[2]},{OneLineSplit[3]},{OneLineSplit[4]},{OneLineSplit[5]},{OneLineSplit[6]}#");
                                break;

                            case "Rectagnle2":
                                stringBuilder.Append($"{OneLineSplit[1]},{OneLineSplit[2]},{OneLineSplit[3]},{OneLineSplit[4]},{OneLineSplit[5]},{OneLineSplit[6]},{OneLineSplit[7]}#");
                                break;

                            case "Polygon":
                            case "Points":
                                stringBuilder.Append($"{OneLineSplit[1]},{OneLineSplit[2]}");
                                for (int i = 3; i < OneLineLength - 1; i += 2)
                                {
                                    stringBuilder.Append($",{OneLineSplit[i]},{OneLineSplit[i + 1]}");
                                }
                                stringBuilder.Append("#");
                                break;
                        }
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                strRoiInfo = null;
                return -1;
            }
            strRoiInfo = stringBuilder.ToString();
            return 0;
        }

        public int CreateAffineROI(string strRoiInfo)
        {
            try
            {
                string FuncName = "GenROIPixel_02";
                HDevProcedure Procedure = new HDevProcedure(hDevProgram, FuncName);
                HDevProcedureCall ProcedureCall = Procedure.CreateCall();

                ProcedureCall.SetInputCtrlParamTuple("OriginalCoord", strRoiInfo);
                ProcedureCall.SetInputCtrlParamTuple("Res", MainAxis);
                ProcedureCall.SetInputCtrlParamTuple("AffineHomMat", dnyCtrlVal["AffineHomMat"]);
                ProcedureCall.Execute();
                dnyCtrlVal["ROIName"] = ProcedureCall.GetOutputCtrlParamTuple("ROIName");
                dnyIconicVal["ROIPixel"] = ProcedureCall.GetOutputIconicParamObject("ROIPixel");
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            //1.初始化
            if (IsInitialization == false)
            {
                try
                {
                    //RunAppliedROI2Image_01();
                    InitializeHalconEngine();
                    InitializeDictionary();
                    IsInitialization = true;
                }
                catch
                {
                    OnException($"初始化Halcon引擎出错，请查看RunAppliedROI2Image_01参数配置是否有误！", new Exception($"建模失败!"));
                    return false;
                }
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
