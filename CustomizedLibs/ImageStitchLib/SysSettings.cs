namespace ImageStitchLib
{
    internal static class SysSetting
    {
        public static SysConfig SysConfig { get; set; } = new SysConfig();

        /*public static void LoadConfig()
        {
            var path = $@"{Environment.CurrentDirectory}\SysConfig.cfg";
            if (System.IO.File.Exists(path))
                SysConfig = Serialize.BinaryDeserialize<SysConfig>(path);
        }

        public static void SaveConfig()
        {
            var path = $@"{Environment.CurrentDirectory}\SysConfig.cfg";
            Serialize.BinarySerialize(path, SysConfig);
        }*/
    }
}
