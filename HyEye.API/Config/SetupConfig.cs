using System.IO;
using System.Linq;

namespace HyEye.API.Config
{
    /// <summary>
    /// 获取/设置系统启动时加载的当前料号
    /// update by LuoDian @ 20211215 添加子料号的逻辑
    /// </summary>
    class SetupConfig
    {
        string m_startMaterial;
        string m_startSubMaterial;

        public string StartMaterial
        {
            get { return m_startMaterial; }
            set
            {
                m_startMaterial = value;

                File.WriteAllText(ConfigRoot + "setup.config", $"{m_startMaterial},{m_startSubMaterial}");
            }
        }
        public string StartSubMaterial 
        {
            get 
            { 
                if(string.IsNullOrEmpty(m_startSubMaterial))
                    m_startSubMaterial = "default";
                return m_startSubMaterial; 
            }
            set
            {
                m_startSubMaterial = value;

                File.WriteAllText(ConfigRoot + "setup.config", $"{m_startMaterial},{m_startSubMaterial}");
            }
        }


        public string ConfigRoot { get; } = PathUtils.CurrentDirectory + "config\\";
        public SetupConfig()
        {
            string path = ConfigRoot + "setup.config";

            if (File.Exists(path))
            {
                string content = File.ReadAllText(path).Trim();

                if(!string.IsNullOrEmpty(content))
                {
                    string[] contentArr = content.Split(',');
                    if (contentArr != null && contentArr.Length > 0)
                        m_startMaterial = contentArr[0];
                    else
                        m_startMaterial = getFirstMaterial();

                    if (contentArr != null && contentArr.Length > 1)
                        m_startSubMaterial = contentArr[1];
                    else
                        m_startSubMaterial = "default";
                }

                File.WriteAllText(path, $"{m_startMaterial},{m_startSubMaterial}");
            }
            else
            {
                Directory.CreateDirectory(ConfigRoot);

                m_startMaterial = getFirstMaterial();
                m_startSubMaterial = "default";

                File.WriteAllText(path, $"{m_startMaterial},{m_startSubMaterial}");
            }
        }

        string getFirstMaterial()
        {
            string[] materials = Directory.GetDirectories(ConfigRoot).Select(a => Path.GetFileName(a)).ToArray();

            if (materials.Length == 0)
            {
                // 如果没有配置，则加一个默认配置

                string defaultMaterial = "料号1";

                Directory.CreateDirectory(ConfigRoot + defaultMaterial);

                return defaultMaterial;
            }
            else
            {
                return materials[0];
            }
        }
    }
}
