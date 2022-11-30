using HyEye.API.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HyEye.Services
{
    public interface IDataService
    {
        void WriteRecord(string taskName, int acqIndex, LinkedDictionary<string, object> data);
    }

    public class DataService : IDataService
    {
        // 每个任务一个文件
        // 每天一个文件
        // 每天一个文件夹

        const string Delimiter = ",";
        const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        readonly IDataRepository dataSaveRepo;

        public DataService(
            IDataRepository dataSaveRepo)
        {
            this.dataSaveRepo = dataSaveRepo;
        }

        Dictionary<string, string> filenameCache = new Dictionary<string, string>();

        public virtual void WriteRecord(string taskName, int acqIndex, LinkedDictionary<string, object> outputs)
        {
            if (outputs == null) return;

            DateTime now = DateTime.Now;

            string filename = getFilename(taskName, acqIndex, now);

            Directory.CreateDirectory(Path.GetDirectoryName(filename));

            if (!File.Exists(filename))
            {
                using (StreamWriter sw = new StreamWriter(filename, true, Encoding.UTF8))
                {
                    sw.Write("时间");
                    sw.Write(Delimiter);
                    sw.Write("拍照索引");

                    foreach (string key in outputs.Keys)
                    {
                        sw.Write(Delimiter);
                        sw.Write(key);
                    }
                    sw.WriteLine();
                    sw.Flush();
                }
            }

            string record = BuildRecord(now, acqIndex, outputs);

            using (StreamWriter sw = new StreamWriter(filename, true, Encoding.UTF8))
            {
                sw.WriteLine(record);
                sw.Flush();
            }
        }

        string BuildRecord(DateTime now, int acqIndex, LinkedDictionary<string, object> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t")
                .Append(now.ToString(DateTimeFormat))
                .Append(Delimiter)
                .Append(acqIndex.ToString());

            foreach (object value in data.Values)
            {
                if (value is int || value is double || value is bool || value is char)
                {
                    sb.Append(Delimiter);
                    sb.Append(value.ToString());
                }
                else if (value is string strV)
                {
                    sb.Append(Delimiter);
                    sb.Append(strV);
                }
                else if (value is DateTime dtV)
                {
                    sb.Append(Delimiter);
                    sb.Append("\t");
                    sb.Append(dtV.ToString(DateTimeFormat));
                }
                else if (value is null)
                {
                    sb.Append(Delimiter);
                }
                else
                {
                    sb.Append(Delimiter);
                    sb.Append(value.ToString());
                }
            }
            return sb.ToString();
        }

        string getFilename(string taskName, int acqIndex, DateTime now)
        {
            if (acqIndex == 1)
            {
                string fn = filename();
                filenameCache[taskName] = fn;
                return fn;
            }
            else
            {
                if (filenameCache.ContainsKey(taskName))
                    return filenameCache[taskName];
                else
                {
                    string fn = filename();
                    filenameCache[taskName] = fn;
                    return fn;
                }
            }

            string filename() => $@"{dataSaveRepo.SavePath}\{now: yyyy-MM-dd}\{taskName}.csv";
        }
    }
}
