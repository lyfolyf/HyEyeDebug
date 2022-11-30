using System.Reflection;

namespace HyEye.WForm
{
    class Global
    {
        public static string Version { get; set; }

        public static string Name { get; } = "HyInspect";

        public static string LicType { get; set; } = "RC";

        static Global()
        {
            //update by LuoDian @ 20211207 需要根据DLL动态库的版本号，动态更新软件界面显示的版本号
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            if (attributes.Length == 0)
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            else
                Version = ((AssemblyFileVersionAttribute)attributes[0]).Version;
        }
    }
}
