using GL.Kit.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace HyVision
{
    public static class HySerializer
    {
        static readonly XmlSerializer serializer = new XmlSerializer();

        public static object LoadFromFile(string filename)
        {
            string s;
            using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
            {
                s = sr.ReadToEnd();
            }

            int index = s.IndexOf('<');

            string[] info = s.Substring(0, index).Trim().Split(',');

            Type type;
            if (info.Length == 1)
            {
                type = Type.GetType(info[0]);
            }
            else
            {
                Assembly assembly = Assembly.LoadFrom(info[0]);

                type = assembly.GetType(info[1]);
            }

            string xml = s.Substring(index);
            return serializer.Deserialize(xml, type);
        }

        public static void SaveToFile(object obj, string filename)
        {
            Type type = obj.GetType();

            string xml = serializer.Serialize(obj);

            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                sw.Write(type.Assembly.GetName().Name);
                sw.Write(".dll,");
                sw.WriteLine(type.FullName);
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
}
