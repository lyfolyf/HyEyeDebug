using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.Implementation.Internal;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace VisionSDK._VisionPro
{
    public static class VisionProUtils
    {
        #region Bitmap 转 ICogImage

        public static ICogImage ToCogImage(Bitmap bitmap)
        {
            return ToCogImage(bitmap, bitmap.PixelFormat == PixelFormat.Format8bppIndexed);
        }

        public static ICogImage ToCogImage(Bitmap bitmap, bool isGrey)
        {
            if (bitmap == null) return null;

            return isGrey ? (ICogImage)new CogImage8Grey(bitmap) : new CogImage24PlanarColor(bitmap);
        }

        #endregion

        #region 创建 CogTool

        static readonly string checkerBoardPath = @"C:\Program Files (x86)\Cognex\VisionPro\bin\Templates\Tools\Calibration & Fixturing\CogCalibCheckerboardTool.vtt";
        static readonly string nPointToNpointPath = @"C:\Program Files (x86)\Cognex\VisionPro\bin\Templates\Tools\Calibration & Fixturing\CogCalibNPointToNPointTool.vtt";

        public static CogCalibCheckerboardTool CreateCheckerboardTool()
        {
            object obj = CogSerializer.LoadObjectFromFile(checkerBoardPath);

            ICogTool tool = CreateToolFromType((Type)obj);

            return (CogCalibCheckerboardTool)tool;
        }

        public static CogCalibNPointToNPointTool CreateNPointToNPointTool()
        {
            return (CogCalibNPointToNPointTool)CogSerializer.LoadObjectFromFile(nPointToNpointPath);
        }

        static ICogTool CreateToolFromType(Type toolType)
        {
            ICogTool tool = null;
            ConstructorInfo constructor = toolType.GetConstructor(Type.EmptyTypes);
            if (constructor != null)
            {
                tool = constructor.Invoke(null) as ICogTool;
                InitializeDefaultToolTerminals(tool);
            }
            return tool;
        }

        static void InitializeDefaultToolTerminals(ICogTool tool)
        {
            if (tool == null)
            {
                throw new ArgumentNullException("tool");
            }

            //添加输入
            var inputAttributes = (CogDefaultToolInputTerminalAttribute[])tool.GetType().GetCustomAttributes(typeof(CogDefaultToolInputTerminalAttribute), false);
            string[] inputs = new string[inputAttributes.Length];
            foreach (CogDefaultToolInputTerminalAttribute attribute in inputAttributes)
            {
                inputs[attribute.Index] = CogToolTerminals.FormatTerminal(attribute.Name, attribute.Path);
            }
            CogToolTerminals.SetToolInputTerminals(tool, inputs);

            //添加输出
            var outputAttributes = (CogDefaultToolOutputTerminalAttribute[])tool.GetType().GetCustomAttributes(typeof(CogDefaultToolOutputTerminalAttribute), false);
            string[] outputs = new string[outputAttributes.Length];
            foreach (CogDefaultToolOutputTerminalAttribute attribute in outputAttributes)
            {
                outputs[attribute.Index] = CogToolTerminals.FormatTerminal(attribute.Name, attribute.Path);
            }
            CogToolTerminals.SetToolOutputTerminals(tool, outputs);
        }

        #endregion

        #region 保存 VPP

        static readonly Type mFormatterType = typeof(BinaryFormatter);

        /// <summary>
        /// 保存 VPP
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="path"></param>
        /// <param name="excludeImage">排除图片</param>
        public static void SaveSubject(object subject, string path, bool excludeImage)
        {
            if (subject == null) return;

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            //DG SaveObjectToFile(Object obj  => ArgumentNullException
            //因为上面即使加了 subject判断，Fatal里还是有时候会报Save的时候subject为null，所以里面每个加一下先看看。
            if (excludeImage)
            {
                if (subject != null)
                    CogSerializer.SaveObjectToFile(subject, path, mFormatterType, CogSerializationOptionsConstants.Minimum);
            }
            else
            {
                if (subject != null)
                    CogSerializer.SaveObjectToFile(subject, path);
            }
        }

        #endregion

    }
}
