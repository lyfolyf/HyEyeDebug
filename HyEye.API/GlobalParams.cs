using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyEye.API
{
    /// <summary>
    /// add by LuoDian
    /// </summary>
    public class GlobalParams
    {
        public static string ProductName;
        public static DateTime BatchRunStartTime = DateTime.MaxValue;

        public static string Output_Graphic_IniFile_FilePath = "simulation\\ShowOutputGraphicConfig.ini";
        public static string Output_Graphic_IniFile_SectionKey_TaskName = "TaskName";
        public static string Output_Graphic_IniFile_SectionKey_OutputName = "OutputName";


        //add by LuoDian @ 20210922 用于统一应用程序的绝对路径，防止因系统环境变量被串改导致的程序文件加载或写入错误
        public static readonly string ApplicationStartupPath = System.IO.Directory.GetCurrentDirectory();

        public static readonly object LightControlLockObj = new object();
        public static readonly object CloseLightControlLockObj = new object();
        public static readonly object SaveImageLockObj = new object();

        public static readonly string ImageRotatoAngle = "Image Rotato Angle";
        public static readonly string IsWaitAllImage = "IsWaitAllImage";

        public static readonly string SubName_DeepBlue = "DeepBlue";
        public static readonly string SubName_Bassalt = "Bassalt";
        public static readonly string SubName_Gray = "Gray";
        public static readonly string SubName_Gold = "Gold";
        public static readonly string SubName_Silver = "Silver";
    }
}
