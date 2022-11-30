using System;
using System.Collections.Generic;

namespace HyEye.AI
{
    /// <summary>
    /// add by LuoDian @ 20220117 用于存储AI的输入图像数据
    /// </summary>
    public struct HyAIInputImage
    {
        public int width;
        public int height;
        public int channels;
        public IntPtr data;
    }

    class HyAIConst
    {
        public const string CAM_LOC = "CameraLocation";
        public const string FOV_LOC = "FOVLocation";
    }

    class HyAIError
    {
        static Dictionary<int, string> errors = new Dictionary<int, string>()
        {
            { (int)HyWrapper.AIErrorType.NOT_SUPORT,                                                    "当前算法不支持" },
            { (int)HyWrapper.AIErrorType.LOADCFG_NOT_EXIST,                                     "加载配置文件不存在" },
            { (int)HyWrapper.AIErrorType.LOADCFG_OPEN_FAIL,                                     "加载配置文件打开失败" },
            { (int)HyWrapper.AIErrorType.LOADCFG_OK,                                                   "加载配置文件成功" },
            { (int)HyWrapper.AIErrorType.LOADCFG_FAIL,                                                 "加载配置文件失败" },

            { (int)HyWrapper.AIErrorType.DEVICE_NOT_AVAILABLE,                                 "没有可用GPU设备" },
            { (int)HyWrapper.AIErrorType.DEVICE_AVAILABLE,                                           "存在可用GPU设备" },
            { (int)HyWrapper.AIErrorType.DEVICE_CPU_SERVER,                                      "CPU设备启用服务" },

            { (int)HyWrapper.AIErrorType.IMAGE_TRANSFORM_FAIL,                                "图像格式转换失败" },
            { (int)HyWrapper.AIErrorType.IMAGE_TRANSFORM_OK,                                  "图像格式转换成功" },
            { (int)HyWrapper.AIErrorType.SET_PARAMS_OK,                                               "引擎参数设置成功" },
            { (int)HyWrapper.AIErrorType.SET_PARAMS_FAIL,                                             "引擎参数设置失败" },

            { (int)HyWrapper.AIErrorType.ONNX_ENGINE_DEPLOY_OK,                             "引擎部署成功" },
            { (int)HyWrapper.AIErrorType.ONNX_ENGINE_DEPLOY_FAIL,                           "引擎部署失败" },
            { (int)HyWrapper.AIErrorType.ONNX_MODEL_NOT_FOUND,                              "引擎模型文件未找到" },
            { (int)HyWrapper.AIErrorType.CREATE_ONNX_ENGINE_OK,                              "创建引擎成功" },
            { (int)HyWrapper.AIErrorType.CREATE_ONNX_ENGINE_FAIL,                            "创建引擎失败" },
            { (int)HyWrapper.AIErrorType.GET_RESULTS_OK,                                               "获取结果成功" },
            { (int)HyWrapper.AIErrorType.GET_RESULTS_FAIL,                                             "获取结果失败" },

            { (int)HyWrapper.AIErrorType.INFERENCE_INPUT_NULL,                                    "引擎输入数据为空" },
            { (int)HyWrapper.AIErrorType.ENGINE_INFERENCE_OK,                                     "引擎推理成功" },
            { (int)HyWrapper.AIErrorType.ENGINE_INFERENCE_FAIL,                                   "引擎推理失败" },
            { (int)HyWrapper.AIErrorType.ENGINE_INFERENCE_OUT_BATCHMAX,             "引擎输入超限" }
        };

        public static string GetErrMsg(int errCode)
        {
            if (errors.ContainsKey(errCode))
                return errors[errCode];
            else
                return errCode.ToString();
        }
    }
}
