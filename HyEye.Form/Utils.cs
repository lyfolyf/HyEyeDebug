using HyVision;
using HyEye.Models;

namespace HyEye.WForm
{
    public class Utils
    {
        public static void CreatePattern()
        {
            //add by LuoDian @ 20220106 添加Halcon引擎、水波纹的报表模块、AI模块的.hy配置文件的生成
            //HySerializer.SaveToFile(new HyEye.AI.HyAIDetEngineBL(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(HyEye.AI.HyAIDetEngineBL)}{HyVisionUtils.TemplatesExtension}");
            //HySerializer.SaveToFile(new HyEye.AI.HyAISegEngineBL(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(HyEye.AI.HyAISegEngineBL)}{HyVisionUtils.TemplatesExtension}");
            HySerializer.SaveToFile(new HalconSDK.Engine.BL.HalconProgramEngineBL_2(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(HalconSDK.Engine.BL.HalconProgramEngineBL_2)}{HyVisionUtils.TemplatesExtension}");
            HySerializer.SaveToFile(new HalconSDK.Engine.BL.HalconEngineTool(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(HalconSDK.Engine.BL.HalconEngineTool)}{HyVisionUtils.TemplatesExtension}");
            HySerializer.SaveToFile(new HalconSDK.DataReport.BL.HalconDataReportGeneraterBL(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(HalconSDK.DataReport.BL.HalconDataReportGeneraterBL)}{HyVisionUtils.TemplatesExtension}");


            // 增加ImageProcess 工具 2022/6/21
            HySerializer.SaveToFile(new HyVision.Tools.ImageProcess.ImageProcessTool(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(HyVision.Tools.ImageProcess.ImageProcessTool)}{HyVisionUtils.TemplatesExtension}");



            // Add by louis on Feb. 28 2022 添加针对MacBook AOI的数据收集模块
            HySerializer.SaveToFile(new ResultsProcess.HyResultCollectionBL(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(ResultsProcess.HyResultCollectionBL)}{HyVisionUtils.TemplatesExtension}");

            // Add by louis on Apr. 20 2022 添加针对MacBook AOI的图像堆叠模块
            HySerializer.SaveToFile(new ImageProcess.ImageStitchTool(), $"{HyVisionUtils.HyVisionToolPath}\\{nameof(ImageProcess.ImageStitchTool)}{HyVisionUtils.TemplatesExtension}");
        }
        public static string GetCalibrationFormName(string taskName, string calibName, CalibrationType type)
        {
            if (type == CalibrationType.Joint)
                return $"{calibName}-联合标定";
            else
                return $"{taskName}/{calibName}-标定";
        }

        public static string GetOpticalFormName(string taskName, string acqOrCalibName)
        {
            return $"{taskName}/{acqOrCalibName}-光学设置";
        }
    }

}
