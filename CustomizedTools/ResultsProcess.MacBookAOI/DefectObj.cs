using System;
using System.Collections.Generic;
using HyVision.Models;

namespace ResultsProcess
{
    /// <summary>
    /// 单个缺陷的信息
    /// </summary>
    public class DefectObj
    {
        /// <summary>
        /// 产品型号
        /// </summary>
        public string strProductModel;

        /// <summary>
        /// 产品颜色
        /// </summary>
        public string strProductColor;

        /// <summary>
        /// 时间
        /// </summary>
        public string strTime;

        /// <summary>
        /// 产品SN
        /// </summary>
        public string strSN;

        /// <summary>
        /// 检测区域
        /// </summary>
        public string strLocation;

        /// <summary>
        /// 缺陷类型
        /// </summary>
        public string strDefectType;

        /// <summary>
        /// 缺陷坐标
        /// </summary>
        public string strDefectLocation;

        /// <summary>
        /// 演算法數值
        /// </summary>
        public string strAlgorithmJudgeMethod;

        /// <summary>
        /// AI得分
        /// </summary>
        public double dAIScore;

        /// <summary>
        /// 检出效果图
        /// </summary>
        public byte[] bArrDetectImage;

        /// <summary>
        /// 图像名称
        /// </summary>
        public string strPictureName;

        /// <summary>
        /// AI算法 Model版本
        /// </summary>
        public string strAIModelVer;

        /// <summary>
        /// Halcon算法版本
        /// </summary>
        public string strHalconScriptVer;
    }

    public class ConstField
    {
        // input const
        public const string CAMERA_NO = "CamNo";
        public const string FOV_LOCATION = "FOVLocation";
        public const string LIGHT_NAME = "LightName";
        public const string PRODUCT_MODEL = "ProductModel";
        public const string PRODUCT_COLOR = "ProductColor";
        public const string PRODUCT_SN = "SN";
        public const string DEFECT_TYPE = "DefectType";
        public const string DEFECT_LOCATION = "DefectLocation";
        public const string ALGORITHM_JUDGE_METHOD = "AlgorithmJudgeMethod";
        public const string AI_SCORE = "AIScore";
        public const string DEFECT_IMAGES = "DetectImages";
        public const string AI_MODEL_VER = "AIModelVer";
        public const string HALCON_SCRIPT_VER = "HalconScriptVer";

        // output const
        public const string RESULT_JSON_STR = "ResultJsonString";

        public static List<(string, Type)> GetProperties ()
        {
            List<(string, Type)> nameList = new List<(string, Type)>();
            nameList.Add((CAMERA_NO, typeof(string)));
            nameList.Add((FOV_LOCATION, typeof(string)));
            nameList.Add((LIGHT_NAME, typeof(string)));
            nameList.Add((PRODUCT_MODEL, typeof(string)));
            nameList.Add((PRODUCT_COLOR, typeof(string)));
            nameList.Add((PRODUCT_SN, typeof(string)));
            nameList.Add((DEFECT_TYPE, typeof(List<string>)));
            nameList.Add((DEFECT_LOCATION, typeof(List<string>)));
            nameList.Add((ALGORITHM_JUDGE_METHOD, typeof(List<string>)));
            nameList.Add((AI_SCORE, typeof(double)));
            nameList.Add((DEFECT_IMAGES, typeof(List<HyImage>)));
            nameList.Add((AI_MODEL_VER, typeof(string)));
            nameList.Add((HALCON_SCRIPT_VER, typeof(string)));
            return nameList;
        }
    }
}
