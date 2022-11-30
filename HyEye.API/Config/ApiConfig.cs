using GL.Kit.Security.Cryptography;
using GL.Kit.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HyEye.API.Config
{
    // 这就相当于一个数据库了

    class ApiConfig
    {
        static readonly XmlSerializer serializer;

        const string HyintPath = "C:\\Program Files (x86)\\HyEye\\";

        //E:\\项目\\HyEye\\代码\\HyEye\\编译\\DLL\\

        //update by LuoDian @ 20210922 用于统一应用程序的绝对路径，防止因系统环境变量被串改导致的程序文件加载或写入错误
        static readonly Dictionary<string, string> configPathDict = new Dictionary<string, string>()
        {
            { typeof(LogConfig).Name,               GlobalParams.ApplicationStartupPath+"\\config\\{0}\\log.config" },
            { typeof(MaterialConfig).Name,          GlobalParams.ApplicationStartupPath+"\\config\\material.config" },
            { typeof(CameraConfig).Name,            GlobalParams.ApplicationStartupPath+"\\config\\{0}\\camera.config" },
            { typeof(TaskConfig).Name,              GlobalParams.ApplicationStartupPath+"\\config\\{0}\\task.config" },
            { typeof(UserBlockConfig).Name,         GlobalParams.ApplicationStartupPath+"\\config\\{0}\\userblock.config" },
            { typeof(ImageConfig).Name,             GlobalParams.ApplicationStartupPath+"\\config\\{0}\\image.config" },
            { typeof(LightControllerConfig).Name,   GlobalParams.ApplicationStartupPath+"\\config\\{0}\\light.config" },
            { typeof(OpticsConfig).Name,            GlobalParams.ApplicationStartupPath+"\\config\\{0}\\optics.config" },
            { typeof(CalibrationConfig).Name,       GlobalParams.ApplicationStartupPath+"\\config\\{0}\\calibaration.config" },
            { typeof(CalibrationVerifyConfig).Name, GlobalParams.ApplicationStartupPath+"\\config\\{0}\\calibverify.config" },
            { typeof(TaskVisionMappingConfig).Name, GlobalParams.ApplicationStartupPath+"\\config\\{0}\\mapping.config" },
            { typeof(CommandConfig).Name,           GlobalParams.ApplicationStartupPath+"\\config\\{0}\\command.config" },
            { typeof(CommunicationConfig).Name,     GlobalParams.ApplicationStartupPath+"\\config\\communication.config" },
            { typeof(DisplayLayoutConfig).Name,     GlobalParams.ApplicationStartupPath+"\\config\\{0}\\layout.config" },
            { typeof(SystemConfig).Name,            GlobalParams.ApplicationStartupPath+"\\config\\{0}\\sys.config" },
            { typeof(DataConfig).Name,              GlobalParams.ApplicationStartupPath+"\\config\\{0}\\data.config"},
            { typeof(RecordShowConfig).Name,        GlobalParams.ApplicationStartupPath+"\\config\\{0}\\recordshow.config"},
            { typeof(PlcSettings).Name,             GlobalParams.ApplicationStartupPath+"config\\{0}\\plcSettings.config"},


            { typeof(UserList).Name,                HyintPath + "HyEye.U.dll" },
            { typeof(PermissionList).Name,          HyintPath + "HyEye.R.dll" }

            //{ typeof(UserList).Name,                PathUtils.CurrentDirectory + "HyEye.U.dll" },
            //{ typeof(PermissionList).Name,          PathUtils.CurrentDirectory + "HyEye.R.dll" }
        };

        public static SetupConfig SetupConfig { get; private set; }

        public static MaterialConfig MaterialConfig { get; private set; }

        public static LogConfig LogConfig { get; private set; }

        public static CameraConfig CameraConfig { get; private set; }

        public static TaskConfig TaskConfig { get; private set; }

        public static UserBlockConfig UserBlockConfig { get; private set; }

        public static ImageConfig ImageConfig { get; private set; }

        public static LightControllerConfig LightControllerConfig { get; private set; }

        public static OpticsConfig OpticsConfig { get; private set; }

        public static CalibrationConfig CalibarationConfig { get; private set; }

        public static CalibrationVerifyConfig CalibrationVerifyConfig { get; private set; }

        public static TaskVisionMappingConfig TaskVisionMappingConfig { get; private set; }

        public static CommandConfig CommandConfig { get; private set; }

        public static CommunicationConfig CommunicationConfig { get; private set; }

        public static DisplayLayoutConfig DisplayLayoutConfig { get; private set; }

        public static SystemConfig SystemConfig { get; private set; }

        public static DataConfig DataConfig { get; private set; }

        public static UserList UserList { get; private set; }

        public static RecordShowConfig RecordShowConfig { get; private set; }

        public static PermissionList PermissionList { get; private set; }

        public static PlcSettings PlcSettings { get; private set; }

        // 用户列表和权限列表不受此变量控制
        static readonly bool isEncryption = false;

        static ApiConfig()
        {
            if (!Directory.Exists(HyintPath))
                Directory.CreateDirectory(HyintPath);

            serializer = new XmlSerializer();

            SetupConfig = new SetupConfig();

            Load(SetupConfig.StartMaterial);
        }

        public static void Load(string meterialName)
        {
            MaterialConfig = Load<MaterialConfig>(null);

            LogConfig = Load<LogConfig>(meterialName);
            CameraConfig = Load<CameraConfig>(meterialName);
            TaskConfig = Load<TaskConfig>(meterialName);
            UserBlockConfig = Load<UserBlockConfig>(meterialName);
            ImageConfig = Load<ImageConfig>(meterialName);
            LightControllerConfig = Load<LightControllerConfig>(meterialName);
            OpticsConfig = Load<OpticsConfig>(meterialName);
            //if(OpticsConfig == null || OpticsConfig.Optics == null || OpticsConfig.Optics.Count < 1)
            //{
            //    Optics optic = Load<Optics>(meterialName);
            //    if(optic != null && optic.OpticsInfo != null)
            //    {
            //        OpticsConfig = new OpticsConfig();
            //        OpticsConfig.Optics.Add(optic);
            //    }
            //}

            CalibarationConfig = Load<CalibrationConfig>(meterialName);
            CalibrationVerifyConfig = Load<CalibrationVerifyConfig>(meterialName);
            TaskVisionMappingConfig = Load<TaskVisionMappingConfig>(meterialName);
            CommandConfig = Load<CommandConfig>(meterialName);
            CommunicationConfig = Load<CommunicationConfig>(meterialName);
            DisplayLayoutConfig = Load<DisplayLayoutConfig>(meterialName);
            SystemConfig = Load<SystemConfig>(meterialName);
            DataConfig = Load<DataConfig>(meterialName);
            RecordShowConfig = Load<RecordShowConfig>(meterialName);
            PlcSettings = Load<PlcSettings>(meterialName);


            UserList = Load<UserList>(meterialName, true);
            //PermissionList = Load<PermissionList>(meterialName, true);
            PermissionList = new PermissionList();
        }

        static T Load<T>(string meterialName, bool forcedEncrypt = false) where T : class, new()
        {
            if (forcedEncrypt || isEncryption)
                return LoadEncrypted<T>(meterialName);
            else
                return LoadUnEncrypted<T>(meterialName);
        }

        public static void Save<T>(T config, bool forcedEncrypt = false)
        {
            if (forcedEncrypt || isEncryption)
                SaveEncrypted(config, SetupConfig.StartMaterial);
            else
                SaveUnEncrypted(config, SetupConfig.StartMaterial);
        }

        #region 明文保存

        static T LoadUnEncrypted<T>(string meterialName) where T : class, new()
        {
            string path = string.Format(configPathDict[typeof(T).Name], meterialName);

            T config = serializer.DeserializeFromFile<T>(path);

            if (config == null)
            {
                config = new T();

                SaveUnEncrypted(config, meterialName);
            }

            return config;
        }

        static void SaveUnEncrypted<T>(T config, string meterialName)
        {
            string path = string.Format(configPathDict[typeof(T).Name], meterialName);
            serializer.SerializeToFile(config, path);
        }

        #endregion

        #region 加密保存

        const string PublicKey = "<RSAKeyValue><Modulus>xIShz1FfALWhNACC96rhcx5DPqUUvSC10KCgazOhQprNcU1gPDjzCc5Rc0lzJooz9lAElYmYQ8ImnuoMNXik/qbqRAnxlHrl+oifRsPYBOiMaB9rD4+LthK0a+VDVgd8TRMAAPjLqxanmPc6ITv/LAwLnNOcllMCCC3z5F8x920=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        const string PrivateKey = "<RSAKeyValue><Modulus>xIShz1FfALWhNACC96rhcx5DPqUUvSC10KCgazOhQprNcU1gPDjzCc5Rc0lzJooz9lAElYmYQ8ImnuoMNXik/qbqRAnxlHrl+oifRsPYBOiMaB9rD4+LthK0a+VDVgd8TRMAAPjLqxanmPc6ITv/LAwLnNOcllMCCC3z5F8x920=</Modulus><Exponent>AQAB</Exponent><P>xy1CnNjTtk03hdpWDuK6TheL3uCzJ0mAmnOGNqp66DhgR68E4mztev2jweCrA8p9qu2TItN9CarLsb7+hBGT0w==</P><Q>/JUx/dRIDvJp4aXNejZwGT4CA8GHSOFgaxpkwmzajClKJuoIObApfOg8StXq/YEqnwkYeoc/X0uHEgvjVuV/vw==</Q><DP>RdSfcyAt8c9WtQ4cd/GTwgdNWaHMDe0eMohoOMhCeSIy108MVlo8VI+Sl0mui5C1yHSszlv5jacc7T96OlFzUw==</DP><DQ>YrHJfu1b2rtONCWdTIvI8ZQRJXaRbiDik/cUOARgwTPBDcOUwzXagDwekv9TewrlpI7hSb9fO9U/SgXrJyE18Q==</DQ><InverseQ>dneCPIQAsXpdfFSzv7U7ZC0PAYMR1cPNFQ3N2RR1SNdWtF9btUqCOyvqguU1gW+r3M6rQg5DFsFAC6rGilEABQ==</InverseQ><D>uQiU/oqEZDugVEFjD2VzL5w//p0bANTqvExxn16WqQrvA46yXSHWHNaJtH5lENWyBWrkVhSfLLWC/cArzLES0BXmJxeCJhILVgKsudQXXc+qYSQ4iQspdpESPvOgns3jl14qndSdAFRRXRZa015rJlU4uMG4kUEg0yjpAjiU5nU=</D></RSAKeyValue>";

        static T LoadEncrypted<T>(string meterialName) where T : class, new()
        {
            string path = string.Format(configPathDict[typeof(T).Name], meterialName);

            if (File.Exists(path))
            {
                string ciphertext = File.ReadAllText(path);

                string plaintext = RSA.DecryptBase64(PrivateKey, ciphertext);

                return serializer.Deserialize<T>(plaintext);
            }
            else
            {
                T config = new T();
                SaveEncrypted(config, meterialName);
                return config;
            }
        }

        static void SaveEncrypted<T>(T config, string meterialName)
        {
            string s = serializer.Serialize(config);
            string ciphertext = RSA.EncryptBase64(PublicKey, s);

            string path = string.Format(configPathDict[typeof(T).Name], meterialName);
            File.WriteAllText(path, ciphertext);
        }

        #endregion

    }
}
