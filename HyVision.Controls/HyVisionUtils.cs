using HyVision.Tools.ThridLib;
using HyVision.Tools.ToolBlock;
using System.IO;
using System.Reflection;

namespace HyVision
{
    public class HyVisionUtils
    {
        public static readonly string TemplatesExtension = ".hyt";

        public static readonly string HyVisionToolPath = @"C:\Program Files (x86)\HyEye\Templates\Tools";

        public static readonly string HyVisionToolImagePath = @"C:\Program Files (x86)\HyEye\Templates\Images";

        public static void CreatePattern()
        {
            Directory.CreateDirectory(HyVisionToolPath);

            HySerializer.SaveToFile(new HyToolBlock(), $"{HyVisionToolPath}\\{nameof(HyToolBlock)}{TemplatesExtension}");

            HySerializer.SaveToFile(new HyThridLibTool(), $"{HyVisionToolPath}\\{nameof(HyThridLibTool)}{TemplatesExtension}");

            //add by LuoDian @ 20220106 添加PMD模块的.hy配置文件的生成
            HySerializer.SaveToFile(new Tools.PhaseDeflection.BL.PhaseDeflectionBL(), $"{HyVisionToolPath}\\{nameof(Tools.PhaseDeflection.BL.PhaseDeflectionBL)}{TemplatesExtension}");

            //add by LuoDian @ 20220228 添加分类器的.hy配置文件的生成
            HySerializer.SaveToFile(new HyVision.Tools.Classifier.BL.ClassifierBL(), $"{HyVisionToolPath}\\{nameof(HyVision.Tools.Classifier.BL.ClassifierBL)}{TemplatesExtension}");

            // Added by louis on June 20 添加图像上传前处理的.hy配置文件的生成
            HySerializer.SaveToFile(new Tools.ImageProcess.ImageProcessTool(), $"{HyVisionToolPath}\\{nameof(Tools.ImageProcess.ImageProcessTool)}{TemplatesExtension}");

            // Added by louis on Mar. 21 添加ImageSaveTool的.hy配置文件的生成
            HySerializer.SaveToFile(new Tools.ImageSave.ImageSaveTool(), $"{HyVisionToolPath}\\{nameof(Tools.ImageSave.ImageSaveTool)}{TemplatesExtension}");
        }

        public static void CreateImages()
        {
            Directory.CreateDirectory(HyVisionToolImagePath);

            Assembly assembly = Assembly.GetExecutingAssembly();

            string preResourceName = assembly.GetName().Name + ".Image";

            string[] resourceNames = assembly.GetManifestResourceNames();

            foreach (string name in resourceNames)
            {
                if (name.StartsWith(preResourceName))
                {
                    using (Stream stream = assembly.GetManifestResourceStream(name))
                    {
                        using (FileStream fs = File.Create($"{HyVisionToolImagePath}\\{name.Substring(preResourceName.Length + 1)}"))
                        {
                            stream.CopyTo(fs);
                        }
                    }
                }
            }

        }

    }
}
