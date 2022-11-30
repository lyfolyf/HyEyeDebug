using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using GL.Kit.Log;
using HyVision.Models;
using HyVision.Tools;
using Newtonsoft.Json;

namespace ResultsProcess
{
    public class HyResultCollectionBL : BaseHyUserTool
    {
        private LogPublisher log;
        private List<DefectObj> defectList;

        public override Type ToolEditType => typeof(HyResultCollectionUI);

        public override bool Initialize()
        {
            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            defectList = new List<DefectObj>();

            return true;
        }

        public override void Save()
        {
        }

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        protected override void Run2(string subName)
        {
            try
            {
                Process();
            }
            catch (Exception ex)
            {
                OnException($"数据收集模块 运行出错！错误信息：{ex.Message}", new Exception($"数据收集模块行失败!"));
            }
        }

        public void Process()
        {
            defectList.Clear();

            List<string> defectTypes = new List<string>();
            if (Inputs[ConstField.DEFECT_TYPE].Value != null)
            {
               defectTypes = Inputs[ConstField.DEFECT_TYPE].Value as List<string>;
            }

            List<string> defectLocations = new List<string>();
            if (Inputs[ConstField.DEFECT_LOCATION].Value != null)
            {
                defectLocations = Inputs[ConstField.DEFECT_LOCATION].Value as List<string>;
            }

            List<string> judgeMethods = new List<string>();
            if (Inputs[ConstField.ALGORITHM_JUDGE_METHOD].Value != null)
            {
                judgeMethods = Inputs[ConstField.ALGORITHM_JUDGE_METHOD].Value as List<string>;
            }

            List<HyImage> defectImages = new List<HyImage>();
            if (Inputs[ConstField.DEFECT_IMAGES].Value != null)
            {
                defectImages = Inputs[ConstField.DEFECT_IMAGES].Value as List<HyImage>;
            }

            //if (defectImages.Count != defectLocations.Count)
            //{
            //    OnException($"缺陷图片数量和缺陷坐标数量不匹配！", new Exception($"数据收集模块行失败!"));
            //}

            for (int index = 0; index < defectLocations.Count; index++)
            {
                string camNo = "";
                string fovLoc = "";
                string lightName = "";
                DefectObj defectObj = new DefectObj();
                if (defectTypes.Count > index)
                    defectObj.strDefectType = defectTypes[index];
                if (defectLocations.Count > index)
                    defectObj.strDefectLocation = defectLocations[index];
                if (judgeMethods.Count > index)
                    defectObj.strAlgorithmJudgeMethod = judgeMethods[index];
                if (defectImages.Count > index)
                    defectObj.bArrDetectImage = GetBytesFromHyImage(defectImages[index]);

                // defectImages[index].Image.Save($"{index}.bmp");

                for (int i = 0; i < Inputs.Count; i++)
                {
                    //if (Inputs[i] == null || Inputs[i].Value == null)
                    //{
                    //    if (Inputs[i] != null)
                    //    {
                    //        OnException($"输入参数[{Inputs[i].Name}]值为空！", new Exception($"数据收集 模块运行失败!"));
                    //    }
                    //    else
                    //    {
                    //        OnException($"第[{i + 1}]个输入参数值为空！", new Exception($"数据收集 模块运行失败!"));
                    //    }
                    //}

                    switch (Inputs[i].Name)
                    {
                        case ConstField.CAMERA_NO:
                            camNo = Inputs[i].Value as string;
                            break;
                        case ConstField.FOV_LOCATION:
                            fovLoc = Inputs[i].Value as string;
                            defectObj.strLocation = fovLoc;
                            break;
                        case ConstField.LIGHT_NAME:
                            lightName = Inputs[i].Value as string;
                            break;
                        case ConstField.PRODUCT_MODEL:
                            defectObj.strProductModel = Inputs[i].Value as string;
                            break;
                        case ConstField.PRODUCT_COLOR:
                            defectObj.strProductColor = Inputs[i].Value as string;
                            break;
                        case ConstField.PRODUCT_SN:
                            defectObj.strSN = Inputs[i].Value as string;
                            break;
                        case ConstField.AI_SCORE:
                            defectObj.dAIScore = Convert.ToDouble(Inputs[i].Value);
                            break;
                        case ConstField.AI_MODEL_VER:
                            defectObj.strAIModelVer = Inputs[i].Value as string;
                            break;
                        case ConstField.HALCON_SCRIPT_VER:
                            defectObj.strHalconScriptVer = Inputs[i].Value as string;
                            break;
                        case ConstField.DEFECT_TYPE:
                        case ConstField.DEFECT_LOCATION:
                        case ConstField.ALGORITHM_JUDGE_METHOD:
                        case ConstField.DEFECT_IMAGES:
                            break;
                        default:
                            log.Error($"输入项{Inputs[i].Name}没有实现！");
                            break;
                    }
                }

                defectObj.strPictureName = string.Format("{0}_{1}_{2}", camNo, fovLoc, lightName);
                defectObj.strTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                defectList.Add(defectObj);
            }

            // 要求UI中添加的输出的数量、类型、及对应的顺序，要跟算法中输出参数的类型及顺序保持一致
            for (int i = 0; i < Outputs.Count; i++)
            {
                if (Outputs[i] != null && Outputs[i]?.ValueType == typeof(string) && defectList.Count > 0)
                {
                    string jsonStr = JsonConvert.SerializeObject(defectList);
                    Outputs[i].Value = jsonStr;
                }
            }

            log.Info($"数据收集模块 执行成功！");
        }

        private byte[] GetBytesFromHyImage(HyImage hyImage)
        {
            byte[] buffer = null;
            if (hyImage.Image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                hyImage.Image.Save(memoryStream, ImageFormat.Bmp);
                buffer = memoryStream.GetBuffer();
            }

            return buffer;
        }
    }

}