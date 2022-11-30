using System;
using System.Drawing;
using System.Collections.Generic;

using Autofac;
using GL.Kit.Log;
using HyEye.Models;
using HyVision;
using HyVision.Models;
using HyVision.Tools;
using Newtonsoft.Json;
using HyWrapper;
using HyRoiManager.ROI;
using HyRoiManagerForAI;

namespace HyEye.AI
{
    [Serializable]
    public class HyAISegEngineBL : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(HyAISegEngineUI);


        #region 引擎Seg模块参数 及 接口
        // 第一步 在程序启动时，如果添加了AI算子，则需要在程序启动时将算子实列创建出来
        HyWrapper.ViAlgorithm segAlgorithm;
        HyWrapper.SegInferenceParameters segParams;

        private bool isSegInitialize = false;
        private HyWrapper.Results[] segOutputs;
        public const string OUTPUT_NAME_SEG_INFERENCE = "SegInferenceResult";

        private string segInferenceCfgPath;
        public string SegInferenceCfgPath { get => segInferenceCfgPath; set => segInferenceCfgPath = value; }

        private bool segInferenceDraw;
        public bool SegInferenceDraw { get => segInferenceDraw; set => segInferenceDraw = value; }

        private bool segIsPatch;
        public bool SegIsPatch { get => segIsPatch; set => segIsPatch = value; }

        private int segBatchMax;
        public int SegBatchMax { get => segBatchMax; set => segBatchMax = value; }

        private int segOptBatch;
        public int SegOptBatch { get => segOptBatch; set => segOptBatch = value; }

        private int segBatchPatchSplit;
        public int SegBatchPatchSplit { get => segBatchPatchSplit; set => segBatchPatchSplit = value; }


        //增加缺陷是否是粗略显示还是精细显示  Heweile  2022/4/18
        public bool RoughDisplay { get; set; } = true;

        public void SegInitialize()
        {
            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            if (segAlgorithm == null)
            {
                segAlgorithm = new HyWrapper.ViAlgorithm((int)HyWrapper.AlgorithmType.ALGORITHM_SEGINFERENCE);
            }

            if (segParams == null)
            {
                segParams = new HyWrapper.SegInferenceParameters();
            }

            // 第二步 根据用户的设定构造参数，并传给算子实列，这个步骤可以在程序启动或者相机启动但未开始执行任务前做
            segParams.segInferenceCfgPath = SegInferenceCfgPath + @"\";
            segParams.isPatch = SegIsPatch;
            segParams.inferenceDraw = SegInferenceDraw;
            segParams.batchMax = SegBatchMax;
            segParams.optBatch = SegOptBatch;
            segParams.batchPatchSplit = SegBatchPatchSplit;

            int resultCode = segAlgorithm.SetParameters(segParams);
            if (resultCode != (int)AIErrorType.SET_PARAMS_OK)
            {
                OnException($"错误码：{resultCode}: {HyAIError.GetErrMsg(resultCode)}", new HyVisionException($"AI 引擎的 SEG 模块设置参数失败！"));
            }

            // 第四步 初始化算子, 前面的参数传递给底层后，通过Initialize方法完成对底层算子的初始化，执行时间同第二、三步
            resultCode = segAlgorithm.Initialize();
            if (resultCode == (int)AIErrorType.ONNX_ENGINE_DEPLOY_OK)
            {
                isSegInitialize = true;
            }
            else
            {
                OnException($"错误码：{resultCode}: {HyAIError.GetErrMsg(resultCode)}", new HyVisionException($"AI 引擎的 SEG 模块初始化失败！"));
            }
        }

        public void SegProcess()
        {
            if (!isSegInitialize)
            {
                SegInitialize();
            }

            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            //在运行之前，先对上一次运行的结果进行释放
            if (segOutputs == null)
                segOutputs = new HyWrapper.Results[0];
            else
            {
                // 输出结果在使用完毕后通过如下方式释放
                foreach (HyWrapper.Results rst in segOutputs)
                {
                    rst?.outImage?.Dispose();
                    rst?.Dispose();
                }
            }

            List<HyWrapper.Image> images = new List<HyWrapper.Image>();
            List<HyWrapper.Region> regions = new List<HyWrapper.Region>();

            string regionKey = string.Empty;
            //把输入图像的数据转成 HyWrapper.Image
            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i] == null || Inputs[i].Value == null)
                {
                    if (Inputs[i] != null)
                    {
                        OnException($"输入参数[{Inputs[i].Name}]值为空！", new Exception($"AI引擎 SEG 模块运行失败!"));
                    }
                    else
                    {
                        OnException($"第[{i + 1}]个输入参数值为空！", new Exception($"AI引擎 SEG 模块运行失败!"));
                    }
                }

                if (Inputs[i].ValueType == typeof(HyImage))
                {
                    HyImage img = (HyImage)Inputs[i].Value;

                    if (img != null)
                    {
                        HyWrapper.Image aiImage = ImageConvertor.GetHyImage(img.Image);
                        images.Add(aiImage);
                        // Bitmap inputImg = ImageConvertor.GetBitmap(aiImage);
                        // inputImg.Save(Inputs[i].Name + "_input.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }
                else if (Inputs[i].ValueType == typeof(Bitmap))
                {
                    Bitmap img = (Bitmap)Inputs[i].Value;

                    if (img != null)
                    {
                        HyWrapper.Image aiImage = ImageConvertor.GetHyImage(img);
                        images.Add(aiImage);
                    }
                }

                if (Inputs[i].Name == HyAIConst.CAM_LOC)
                {
                    regionKey = Inputs[i].Value.ToString() + "_";
                }

                if (Inputs[i].Name == HyAIConst.FOV_LOC)
                {
                    regionKey += Inputs[i].Value.ToString();
                    if (regionKey != string.Empty)
                    {
                        regions.Add(GetRegion(regionKey));
                    }
                }
            }

            int resultCode = segAlgorithm.Process(images.ToArray(), regions.ToArray(), out segOutputs);
            if (resultCode == ErrorCodeConst.OK)
            {
                if (segOutputs == null)
                    OnException($"AI引擎 SEG 模块没有输出图像！", new Exception($"AI引擎 SEG 模块运行失败!"));

                if (segOutputs.Length != Outputs.Count)
                    OnException($"AI引擎 SEG 模块输出图像的数量与设定的输出数量不一致！当前输出图像的数量：{segOutputs.Length}", new Exception($"AI引擎 SEG 模块运行失败!"));

                //要求UI中添加的输出的数量、类型、及对应的顺序，要跟算法中输出参数的类型及顺序保持一致
                for (int i = 0; i < Outputs.Count; i++)
                {
                    if (Outputs[i]?.ValueType == typeof(HyImage) && segOutputs[i]?.outImage != null)
                    {
                        DateTime start = DateTime.Now;
                        Bitmap bitmap = ImageConvertor.GetBitmap(segOutputs[i]?.outImage);
                        double diff = (DateTime.Now - start).TotalMilliseconds;
                        log.InfoFormat("HyWrapper.Image转化Bitmap耗时：{0}ms", diff);
                        // bitmap.Save("mask.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Outputs[i].Value = new HyImage(bitmap);
                    }
                    else if (Outputs[i]?.ValueType == typeof(Bitmap))
                        Outputs[i].Value = ImageConvertor.GetBitmap(segOutputs[i]?.outImage);
                    else if (Outputs[i] != null && Outputs[i]?.ValueType == typeof(string) && segOutputs[i] != null && typeof(SegInferenceResultsProperties) == segOutputs[i].GetType())
                    {
                        string jsonStr = JsonConvert.SerializeObject(segOutputs[i]);
                        Outputs[i].Value = jsonStr;
                    }
                    //增加单个AI缺陷转为框架数据  2022/4/15 Heweile
                    else if (Outputs[i].ValueType == typeof(HyRoiManager.ROI.BaseHyROI))
                    {
                        // 保留
                    }
                    //增加AI缺陷集合转为框架数据  2022/4/15 Heweile
                    else if (Outputs[i].ValueType == typeof(List<HyRoiManager.ROI.BaseHyROI>))
                    {
                        SegInferenceImageResults SegResults = segOutputs[i] as SegInferenceImageResults;

                        if (SegResults != null)
                        {
                            if (RoughDisplay == true)
                            {
                                List<BaseHyROI> LstRois = new List<BaseHyROI>();

                                for (int k = 0; k < SegResults.ImageResults.Length; k++)
                                {
                                    BaseHyROI roi = DataAdapterForAI.AiDefectToHyRoi(SegResults.ImageResults[k].rectange);
                                    LstRois.Add(roi);
                                }
                                Outputs[i].Value = LstRois;
                            }
                            else
                            {
                                List<BaseHyROI> LstRois = new List<BaseHyROI>();

                                for (int k = 0; k < SegResults.ImageResults.Length; k++)
                                {
                                    BaseHyROI roi = DataAdapterForAI.AiDefectToHyRoi(SegResults.ImageResults[k].contour);
                                    LstRois.Add(roi);
                                }
                                Outputs[i].Value = LstRois;
                            }

                        }
                        else
                        {
                            log.Error($"AI引擎 SEG模块  获取AI缺陷结果时为null！");
                        }
                    }
                }

                log.Info($"AI引擎 SEG模块 执行成功！");
            }
            else
            {
                OnException($"错误码：{resultCode}: {HyAIError.GetErrMsg(resultCode)}", new Exception($"AI引擎 SEG 模块运行失败!"));
            }

            // 释放使用完毕的图像
            foreach (HyWrapper.Image image in images)
            {
                image.Dispose();
            }
        }
        #endregion

        ~HyAISegEngineBL()
        {
            //如果输出结果不为null，则对结果先进行释放
            if (segOutputs != null && segOutputs.Length > 0)
            {
                // 输出结果在使用完毕后通过如下方式释放
                foreach (HyWrapper.Results rst in segOutputs)
                {
                    rst?.Dispose();
                }
            }

            // 释放Seg模块算子实例
            segAlgorithm?.Dispose();
        }

        LogPublisher log;

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
                SegProcess();
            }
            catch (Exception ex)
            {
                OnException($"AI引擎 运行出错！错误信息：{ex.Message}", new Exception($"AI引擎运行失败!"));
            }
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            try
            {
                if (!isSegInitialize)
                    SegInitialize();

                return true;
            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            isSegInitialize = false;
        }

        static Dictionary<string, Rect> regions = new Dictionary<string, Rect>()
        {
            { "1_1",             new Rect(800, 1800, 5120-800,5120-1800) },
            { "1_2",             new Rect(1040, 0, 5120-1040,5120) },
            { "1_3",             new Rect(1040, 0, 5120-1040,5120) },
            { "1_4",             new Rect(800, 0, 5120-800,4110) },
            { "2_1",             new Rect(0, 1800, 5120,5120-1800) },
            { "2_2",             new Rect(0, 0, 5120,5120) },
            { "2_3",             new Rect(0, 0, 5120,5120) },
            { "2_4",             new Rect(0, 0, 5120,4070) },
            { "3_1",             new Rect(0, 1800, 3060,5120-1800) },
            { "3_2",             new Rect(0, 0, 3060,5120) },
            { "3_3",             new Rect(0, 0, 3060,5120) },
            { "3_4",             new Rect(0, 0, 3060,4130) }
        };

        public static Rect GetRegion(string regionKey)
        {
            if (regions.ContainsKey(regionKey))
                return regions[regionKey];
            else
                throw new NotImplementedException("该点位没有给出ROI区域");
        }
    }
}
