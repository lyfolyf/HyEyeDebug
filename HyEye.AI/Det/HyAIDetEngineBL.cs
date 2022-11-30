using System;
using System.Collections.Generic;
using System.Drawing;

using Autofac;
using GL.Kit.Log;
using HyVision;
using HyVision.Models;
using HyVision.Tools;
using HyEye.Models;
using HyWrapper;
using Newtonsoft.Json;
using HyRoiManager.ROI;
using HyRoiManagerForAI;

namespace HyEye.AI
{
    [Serializable]
    public class HyAIDetEngineBL : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(HyAIDetEngineUI);

        #region 引擎Det模块参数 及 接口
        // 第一步 在程序启动时，如果添加了AI算子，则需要在程序启动时将算子实列创建出来
        HyWrapper.ViAlgorithm detAlgorithm;
        HyWrapper.DetInferenceParameters detParams;

        private bool isDetInitialize = false;
        private HyWrapper.Results[] detOutputs;
        public const string OUTPUT_NAME_JUDGE_RESULT = "JudgeResult";
        public const string OUTPUT_NAME_DET_INFERENCE = "DetInferenceResult";

        private string detInferenceCfgPath;
        public string DetInferenceCfgPath { get => detInferenceCfgPath; set => detInferenceCfgPath = value; }

        private bool detInferenceDraw;
        public bool DetInferenceDraw { get => detInferenceDraw; set => detInferenceDraw = value; }

        private int detOptInputSizeH;
        public int DetOptInputSizeH { get => detOptInputSizeH; set => detOptInputSizeH = value; }

        private int detOptInputSizeW;
        public int DetOptInputSizeW { get => detOptInputSizeW; set => detOptInputSizeW = value; }

        private int detInputMaxH;
        public int DetInputMaxH { get => detInputMaxH; set => detInputMaxH = value; }

        private int detInputMaxW;
        public int DetInputMaxW { get => detInputMaxW; set => detInputMaxW = value; }

        private int detEngineBatch;
        public int DetEngineBatch { get => detEngineBatch; set => detEngineBatch = value; }

        private int detMaxDetections;
        public int DetMaxDetections { get => detMaxDetections; set => detMaxDetections = value; }

        private float detScoreThreshold;
        public float DetScoreThreshold { get => detScoreThreshold; set => detScoreThreshold = value; }

        public void DetInitialize()
        {
            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            if (detAlgorithm == null)
            {
                detAlgorithm = new HyWrapper.ViAlgorithm((int)HyWrapper.AlgorithmType.ALGORITHM_DETINFERENCE);
            }
            if (detParams == null)
            {
                detParams = new HyWrapper.DetInferenceParameters();
            }

            // 第二步 根据用户的设定构造参数，并传给算子实列，这个步骤可以在程序启动或者相机启动但未开始执行任务前做
            detParams.detInferenceCfgPath = DetInferenceCfgPath;
            detParams.InferenceDraw = DetInferenceDraw;
            detParams.optInputSizeH = DetOptInputSizeH;
            detParams.optInputSizeW = DetOptInputSizeW;
            detParams.inputMaxH = DetInputMaxH;
            detParams.inputMaxW = DetInputMaxW;
            detParams.engineBatch = DetEngineBatch;                   // 新增项  （int）
            detParams.detMaxDetections = DetMaxDetections;
            detParams.detScoreThreshold = DetScoreThreshold;      // 新增项  （float）

            int resultCode = detAlgorithm.SetParameters(detParams);
            if (resultCode != (int)AIErrorType.SET_PARAMS_OK)
            {
                OnException($"错误码：{resultCode}: {HyAIError.GetErrMsg(resultCode)}", new HyVisionException($"AI 引擎的 DET 模块设置参数失败！"));
            }

            // 第四步 初始化算子, 前面的参数传递给底层后，通过Initialize方法完成对底层算子的初始化，执行时间同第二、三步
            resultCode = detAlgorithm.Initialize();
            if (resultCode == (int)AIErrorType.ONNX_ENGINE_DEPLOY_OK)
            {
                isDetInitialize = true;
            }
            else
            {
                OnException($"错误码：{resultCode}: {HyAIError.GetErrMsg(resultCode)}", new HyVisionException($"AI 引擎的 DET 模块初始化失败！"));
            }
        }

        public void DetProcess()
        {
            if (!isDetInitialize)
            {
                DetInitialize();
            }

            if (log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            //在运行之前，先对上一次运行的结果进行释放
            if (detOutputs == null)
                detOutputs = new HyWrapper.Results[0];
            else
            {
                // 输出结果在使用完毕后通过如下方式释放
                foreach (HyWrapper.Results rst in detOutputs)
                {
                    rst?.outImage?.Dispose();
                    rst?.Dispose();
                }
            }

            List<HyWrapper.Image> images = new List<HyWrapper.Image>();

            HyWrapper.Image aiImage = null;
            //把输入图像的数据转成 HyWrapper.Image
            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i] == null || Inputs[i].Value == null)
                    OnException($"AI引擎 DET 模块第 {i + 1} 张输入图像没有图像数据！", new Exception($"AI引擎 DET 模块运行失败!"));

                if (Inputs[i].ValueType == typeof(HyImage))
                {
                    HyImage img = (HyImage)Inputs[i].Value;

                    if (img != null)
                    {
                        aiImage = ImageConvertor.GetHyImage(img.Image);
                    }
                }
                else if (Inputs[i].ValueType == typeof(Bitmap))
                {
                    Bitmap img = (Bitmap)Inputs[i].Value;

                    if (img != null)
                    {
                        aiImage = ImageConvertor.GetHyImage(img);
                    }
                }

                if (aiImage == null)
                    OnException($"AI引擎 DET 模块第 {i + 1} 张输入图像转换成AI图像时失败！", new Exception($"AI引擎 DET 模块运行失败!"));

                images.Add(aiImage);
            }

            int resultCode = detAlgorithm.Process(images.ToArray(), out detOutputs);
            if (resultCode == ErrorCodeConst.OK)
            {
                if (detOutputs == null)
                    OnException($"AI引擎 DET 模块没有输出图像！", new Exception($"AI引擎 DET 模块运行失败!"));

                if (detOutputs.Length != Outputs.Count)
                    OnException($"AI引擎 DET 模块输出图像的数量与设定的输出数量不一致！当前输出图像的数量：{detOutputs.Length}", new Exception($"AI引擎 DET 模块运行失败!"));

                //要求UI中添加的输出的数量、类型、及对应的顺序，要跟算法中输出参数的类型及顺序保持一致
                for (int i = 0; i < Outputs.Count; i++)
                {
                    if (Outputs[i]?.ValueType == typeof(Bitmap))
                        Outputs[i].Value = ImageConvertor.GetBitmap(detOutputs[i]?.outImage);
                    else if (Outputs[i] != null && Outputs[i]?.ValueType == typeof(string) && detOutputs[i] != null && typeof(DetInferenceResultsProperties) == detOutputs[i].GetType())
                    {
                        string jsonStr = JsonConvert.SerializeObject(detOutputs[i]);
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
                        DetInferenceImageResults DetResults = detOutputs[i] as DetInferenceImageResults;

                        if (DetResults != null)
                        {
                            List<BaseHyROI> LstRois = new List<BaseHyROI>();

                            for (int k = 0; k < DetResults.ImageResults.Length; k++)
                            {
                                BaseHyROI roi = DataAdapterForAI.AiDefectToHyRoi(DetResults.ImageResults[k].rectange);
                                LstRois.Add(roi);
                            }
                            Outputs[i].Value = LstRois;
                        }
                        else
                        {
                            log.Error($"AI引擎 DET模块  获取AI缺陷结果时为null！");
                        }
                    }

                }

                log.Info($"AI引擎 DET模块 执行成功！");
            }
            else
            {
                OnException($"错误码：{resultCode}: {HyAIError.GetErrMsg(resultCode)}", new Exception($"AI引擎 DET 模块运行失败!"));
            }

            // 释放使用完毕的图像
            foreach (HyWrapper.Image image in images)
            {
                image.Dispose();
            }
        }
        #endregion

        ~HyAIDetEngineBL()
        {
            //如果输出结果不为null，则对结果先进行释放
            if (detOutputs != null && detOutputs.Length > 0)
            {
                // 输出结果在使用完毕后通过如下方式释放
                foreach (HyWrapper.Results rst in detOutputs)
                {
                    rst.Dispose();
                }
            }

            // 释放Det模块算子实例
            detAlgorithm?.Dispose();
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
                DetProcess();
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
                if (!isDetInitialize)
                    DetInitialize();

                return true;
            }
            catch (Exception ex)
            {
                OnException($"初始化AI引擎 出错！错误信息：{ex.Message}", new Exception($"AI引擎初始化失败!"));
                return false;
            }
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            isDetInitialize = false;
        }
    }
}
