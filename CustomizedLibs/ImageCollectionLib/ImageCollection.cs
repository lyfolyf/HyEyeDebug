using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Collections.Concurrent;
using GL.Kit.Log;
using RestSharp;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Newtonsoft.Json.Linq;

namespace ImageCollectionLib
{
    public class ImageCollection
    {
        private const string SURFACE_TC = "TC";
        private const string SURFACE_LCM = "LCM";
        private const string SURFACE_MANDREL = "Mandrel";
        private const string SURFACE_CORNER = "Corner";
        private const string SURFACE_SIDE = "Side";
        private const string SURFACE_DH = "DH";
        private const string SURFACE_BC = "BC";

        private const int FILE_NAME_MAX_INDEX = 5;
        private const int FILE_NAME_SURFACE_INDEX = 0;

        private const int SRC_IMAGE_IN_MEMORY_DURATION = 60000; // unit ms

        private const int TIMER_DELAY_START = 30000;
        private const int TIMER_INTERVAL = 10000;

        private static Timer timeoutImageClearTimer;

        private static Dictionary<(string, string, string), Queue<(string, string, byte[])>> imageSet;
        private static Dictionary<(string, string), (Mat, string, DateTime)> srcImageSet;
        private static Dictionary<string, List<string>> imageNameSet;
        private static ConcurrentQueue<JObject> resultQueue;

        private static Dictionary<string, int> capacitySet;

        private static TcpClient tcpClient;
        private static RestClient restClient;
        private static IPAddress ipAdress;
        private static int port;

        private IGLog log;

        private static object syncRoot = new Object();
        private static object syncSrc = new Object();
        private static volatile ImageCollection imageCollection = null;

        public bool UseTestMode { get; set; }

        public bool UseSendResult { get; set; }

        public bool UseSaveNgImage { get; set; }

        public string SaveNgImageFolder { get; set; }

        private ImageCollection(IPAddress restIP, int restPort, IGLog log)
        {
            capacitySet = new Dictionary<string, int>();
            imageSet = new Dictionary<(string, string, string), Queue<(string, string, byte[])>>();
            srcImageSet = new Dictionary<(string, string), (Mat, string, DateTime)>();
            resultQueue = new ConcurrentQueue<JObject>();
            restClient = new RestClient(string.Format("http://{0}:{1}/v1/inspect", restIP, restPort));
            timeoutImageClearTimer = new Timer(ClearTimeoutImage, null, TIMER_DELAY_START, TIMER_INTERVAL);
            string imageNameJson = ImageNameJsonParser.ReadJsonString("ImageNameList.json");
            imageNameSet = ImageNameJsonParser.Parse(imageNameJson);
            this.log = log;
        }

        private ImageCollection(IPAddress restIP, int restPort, IPAddress tcpIP, int tcpPort, IGLog log) : this(restIP, restPort, log)
        {
            ipAdress = tcpIP;
            port = tcpPort;
            tcpClient = new TcpClient();
            tcpClient.Connect(tcpIP, tcpPort);
        }

        public static ImageCollection GetInstance(IPAddress restIP, int restPort, IGLog log)
        {
            // double-check locking 方法解决了线程并发问题
            if (imageCollection == null)
            {
                lock (syncRoot)
                {
                    if (imageCollection == null)
                    {
                        imageCollection = new ImageCollection(restIP, restPort, log);
                        imageCollection.init();
                    }
                }
            }

            return imageCollection;
        }

        public static ImageCollection GetInstance(IPAddress restIP, int restPort, IPAddress tcpIP, int tcpPort, IGLog log)
        {
            // double-check locking 方法解决了线程并发问题
            if (imageCollection == null)
            {
                lock (syncRoot)
                {
                    if (imageCollection == null)
                    {
                        imageCollection = new ImageCollection(restIP, restPort, tcpIP, tcpPort, log);
                        imageCollection.init();
                    }
                }
            }

            return imageCollection;
        }

        private void init()
        {
            capacitySet[SURFACE_TC] = 9;
            capacitySet[SURFACE_LCM] = 18 + 1; // Mandrel的一个点位一张图并入LCM一起上传
            capacitySet[SURFACE_MANDREL] = 1;
            capacitySet[SURFACE_CORNER] = 8;
            capacitySet[SURFACE_SIDE] = 12;
            capacitySet[SURFACE_DH] = 6;
            capacitySet[SURFACE_BC] = 6;

            ThreadPool.SetMaxThreads(20, 10);
        }

        public void Dispose()
        {
            imageSet.Clear();
            srcImageSet.Clear();
            resultQueue.Clear();
            restClient?.Dispose();
            tcpClient?.Dispose();
            imageCollection = null;
        }

        public void Push(string sn, string surfaceCode, string fov, string contents, string imageName, byte[] buffer)
        {
            lock (syncRoot)
            {
                switch (surfaceCode)
                {
                    case SURFACE_MANDREL:
                        surfaceCode = SURFACE_LCM;
                        break;
                    case SURFACE_CORNER:
                        fov = "1.2.3.4";
                        break;
                    case SURFACE_SIDE:
                        switch (fov)
                        {
                            case "1":
                            case "6":
                                fov = "1.6";
                                break;
                            case "2":
                            case "7":
                                fov = "2.7";
                                break;
                            case "3":
                            case "8":
                                fov = "3.8";
                                break;
                            case "4":
                            case "9":
                                fov = "4.9";
                                break;
                            case "5":
                            case "10":
                                fov = "5.10";
                                break;
                        }

                        break;
                    default:
                        break;
                }

                if (!imageSet.ContainsKey((sn, surfaceCode, fov)))
                {
                    imageSet[(sn, surfaceCode, fov)] = new Queue<(string, string, byte[])>();
                    log.InfoFormat("SN:{0}, {1}面点位{2}初始化收集队列！", sn, surfaceCode, fov);
                }

                imageSet[(sn, surfaceCode, fov)].Enqueue((contents, imageName, buffer));
                int imageCount = imageSet[(sn, surfaceCode, fov)].Count;

                string msg = string.Format("SN:{0}, {1}面点位{2}收集到图片{3}, 共收集了{4}张", sn, surfaceCode, fov, imageName, imageCount);
                log.Info(msg);

                if (imageCount == capacitySet[surfaceCode])
                {
                    ThreadPool.QueueUserWorkItem(Send, (sn, surfaceCode, fov));
                    Thread.Sleep(10); // 休眠10ms等待消耗
                }
            }
        }

        public void Push(string sn, string contents, string imageName, Bitmap bitmap)
        {
            try
            {
                lock (syncSrc)
                {
                    Stopwatch watch = Stopwatch.StartNew();
                    var mat = bitmap.ToMat();
                    watch.Stop();
                    log.DebugFormat("Bitmap 转化 Mat 耗时 {0}ms", watch.ElapsedMilliseconds);

                    srcImageSet.Add((sn, contents), (mat, imageName, DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("缓存原图发生错误，错误详情：{0}, {1}", ex.Message, ex.StackTrace);
            }
        }

        private void Send(object state)
        {
            (string sn, string surfaceCode, string fov) = ((string, string, string))state;

            const int timeout = 10000; // unit ms
            string timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffffzz00");
            RestRequest request = new RestRequest("", Method.Post);
            request.Timeout = timeout;
            string[] arrSN = sn.Split('-');
            if (arrSN.Length > 0)
                request.AddParameter("serialnumber", arrSN[0]);

            if (arrSN.Length > 1)
                request.AddParameter("identifier", arrSN[1]);

            request.AddParameter("vendor", "LEAD");
            request.AddParameter("timestamp", timeStamp);
            request.AddParameter("use_sample", "0");
            request.AddParameter("test_mode", UseTestMode ? "1" : "0");

            float totalSize = 0;
            int capacity = capacitySet[surfaceCode];
            for (int i = 0; i < capacity; i++)
            {
                (string contents, string imageName, byte[] buffer) = imageSet[(sn, surfaceCode, fov)].Dequeue();
                request.AddParameter("contents", contents);
                if (buffer != null)
                {
                    request.AddFile(contents, buffer, imageName);
                    totalSize += buffer.Length;
                }
            }

            if (IsInvalidSendImageName(sn, surfaceCode, fov, request))
                return;

            totalSize = totalSize / (1024 * 1024);
            string sendTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            if (totalSize >= 60)
            {
                log.ErrorFormat("{0}面点位{1}发送{2}张图片总大小：{3:F} MB, 超过60 MB，未发送", surfaceCode, fov, capacity, totalSize);
                log.ErrorFormat("SN: {0}, 发送时间: {1}", sn, timeStamp);
                foreach (var file in request.Files)
                {
                    log.ErrorFormat("图片名: {0}", file.FileName);
                }

                Logger.Info(string.Format("{0},{1},{2}_{3},{4:F} MB,,,,未发送, 图片包大小超过60 MB", sendTime, sn, surfaceCode, fov, totalSize));
                return;
            }
            else
            {
                log.InfoFormat("{0}面点位{1}发送{2}张图片总大小：{3:F} MB", surfaceCode, fov, capacity, totalSize);
            }

            Stopwatch singleWatch = Stopwatch.StartNew();
            RestResponse response = restClient.Execute(request);

            int statusCode = (int)response.StatusCode;
            if (response != null && statusCode == 200)
            {
                singleWatch.Stop();
                long transferTime = singleWatch.ElapsedMilliseconds;
                log.InfoFormat("{0}面点位{1}发送{2}张图片成功, 耗时：{3} ms", surfaceCode, fov, capacity, transferTime);

                log.InfoFormat("SN: {0}, 发送时间: {1}", sn, timeStamp);
                foreach (var file in request.Files)
                {
                    log.InfoFormat("图片名: {0}", file.FileName);
                }

                if (UseSendResult || UseSaveNgImage)
                    ProcessResponse(response);

                string uuid = GetUUID(response);
                Logger.Info(string.Format("{0},{1},{2}_{3},{4:F} MB,HTTP/1.1 {5},{6},{7},未超时", sendTime, sn, surfaceCode, fov, totalSize, statusCode, uuid, transferTime));
            }
            else
            {
                singleWatch.Stop();
                long transferTime = singleWatch.ElapsedMilliseconds;
                log.ErrorFormat("{0}面点位{1}发送{2}张图片失败, 耗时：{3} ms", surfaceCode, fov, capacity, transferTime);

                log.ErrorFormat("SN: {0}, 发送时间: {1}", sn, timeStamp);
                foreach (var file in request.Files)
                {
                    log.ErrorFormat("图片名: {0}", file.FileName);
                }

                if (response != null)
                {
                    string uuid = GetUUID(response);
                    string errorMsg = response.ErrorMessage;
                    if (response.StatusCode == HttpStatusCode.RequestTimeout || transferTime > timeout)
                    {
                        Logger.Error(string.Format("{0},{1},{2}_{3},{4:F} MB,HTTP/1.1 {5},{6},{7},超时,{8}", sendTime, sn, surfaceCode, fov, totalSize, statusCode, uuid, transferTime, errorMsg));
                    }
                    else
                    {
                        Logger.Error(string.Format("{0},{1},{2}_{3},{4:F} MB,HTTP/1.1 {5},{6},{7},未超时,{8}", sendTime, sn, surfaceCode, fov, totalSize, statusCode, uuid, transferTime, errorMsg));
                    }
                }
                else
                {
                    Logger.Error(string.Format("{0},{1},{2}_{3},{4:F} MB, HTTP/1.1 408,no mac_uuid,{5},超时,responese is null", sendTime, sn, surfaceCode, fov, totalSize, transferTime));
                }

                // 如果返回错误码为472，等待1s后会尝试重传
                if (response != null && (int)response.StatusCode == 472)
                {
                    RestResponse retransmitResponse = restClient.Execute(request);

                    log.InfoFormat("{0}面点位{1}的{2}张图片开启重传...", surfaceCode, fov, capacity);
                    Thread.Sleep(1000);
                    if (retransmitResponse != null && retransmitResponse.StatusCode == HttpStatusCode.OK)
                    {
                        log.Info("重传成功");
                    }
                    else
                    {
                        log.Info("重传失败");
                    }
                }
            }
        }

        private bool IsInvalidSendImageName(string sn, string surfaceCode, string fov, RestRequest request)
        {
            try
            {
                string surfaceFov = string.Format("{0}_{1}", surfaceCode, fov);
                List<string> expectImageNames = imageNameSet[surfaceFov];

                foreach (string expectName in expectImageNames)
                {
                    bool found = false;
                    foreach (var file in request.Files)
                    {
                        if (CompareName(expectName, file.Name, "B-"))
                        {
                            found = true;
                            break;
                        }

                        if (CompareName(expectName, file.Name, "R-"))
                        {
                            found = true;
                            break;
                        }

                        if (CompareName(expectName, file.Name, "G-"))
                        {
                            found = true;
                            break;
                        }

                        if (CompareName(expectName, file.Name, "S-"))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        log.ErrorFormat("发送SN:{0} 丢失图片 {1}, 该点位的图像包未发送！", sn, expectName);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("验证发送图片名时发生错误，错误详情：{0}, {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private void ValidResponseImageName(List<(string, string, string)> judgeList)
        {
            try
            {
                if (judgeList.Count > 1)
                {
                    (string sn, string contents, string decision) = judgeList[0];
                    if (contents.StartsWith("Mandrel"))
                        (sn, contents, decision) = judgeList[1];

                    string[] contentArr = contents.Split('-');
                    if (contentArr.Length != 5)
                    {
                        log.ErrorFormat("返回结果SN: {0} 的图片名称 {1} 不正确！", sn, contents);
                        return;
                    }

                    string surface = contentArr[0];
                    string fov = contentArr[2];

                    List<string> expectImageNames = new List<string>();
                    foreach (string key in imageNameSet.Keys)
                    {
                        if (key.Contains(surface) && key.Contains(fov))
                        {
                            expectImageNames = imageNameSet[key];
                            break;
                        }
                    }

                    foreach (string expectName in expectImageNames)
                    {
                        bool found = false;
                        foreach ((string s, string actualName, string d) in judgeList)
                        {
                            if (CompareName(expectName, actualName, "B-"))
                            {
                                found = true;
                                break;
                            }

                            if (CompareName(expectName, actualName, "R-"))
                            {
                                found = true;
                                break;
                            }

                            if (CompareName(expectName, actualName, "G-"))
                            {
                                found = true;
                                break;
                            }

                            if (CompareName(expectName, actualName, "S-"))
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            log.ErrorFormat("返回结果SN:{0} 丢失图片 {1}, 请联系客户确认！", sn, expectName);
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("验证返回结果图片名时发生错误，错误详情：{0}, {1}", ex.Message, ex.StackTrace);
            }
        }

        private bool CompareName(string expectName, string fileName, string diffSubStr)
        {
            if (fileName.Contains(diffSubStr))
            {
                string actualName = fileName.Replace(diffSubStr, "");
                if (expectName == actualName)
                    return true;
            }

            return false;
        }

        private void ClearTimeoutImage(object state)
        {
            try
            {
                lock (syncSrc)
                {
                    // 遍历检查内存中驻留时间已经比较长的图片，从内存中清除，防止内存堆积
                    List<(string, string)> deleteKeys = new List<(string, string)>();
                    foreach (var kv in srcImageSet)
                    {
                        (Mat cvmat, string name, DateTime time) = kv.Value;
                        if ((DateTime.Now - time).TotalMilliseconds > SRC_IMAGE_IN_MEMORY_DURATION)
                        {
                            log.WarnFormat("图片{0}无结果匹配，在内存中驻留已超过{1}ms, 已自动清除！", name, SRC_IMAGE_IN_MEMORY_DURATION);
                            cvmat.Dispose();
                            deleteKeys.Add(kv.Key);
                        }
                    }

                    if (deleteKeys.Count > 0)
                    {
                        foreach (var delKey in deleteKeys)
                        {
                            srcImageSet.Remove(delKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("清除缓存超时图片发生错误，错误详情：{0}, {1}", ex.Message, ex.StackTrace);
            }
        }

        private string GetUUID(RestResponse response)
        {
            string uuid = string.Empty;
            if (response.Content != null)
            {
                try
                {
                    JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
                    uuid = jsonDoc.RootElement.GetProperty("uuid").GetString();
                }
                catch
                {
                    uuid = "Cannot find uuid in content";
                }
            }

            return uuid;
        }

        private void ProcessResponse(RestResponse response)
        {
            if (response.Content != null)
            {
                try
                {
                    JObject jsonObj = (JObject)JToken.Parse(response.Content);

                    if (UseSendResult)
                    {
                        if (tcpClient.Connected == false)
                            tcpClient.Connect(ipAdress, port);

                        if (tcpClient.Connected)
                        {
                            var paramId = response.Request.Parameters.TryFind("identifier");
                            if (paramId != null)
                            {
                                string identifier = paramId.Value as string;
                                jsonObj.AddFirst(new JProperty("identifier", identifier));
                            }

                            // 发送结果
                            Stopwatch watch = Stopwatch.StartNew();
                            NetworkStream ns = tcpClient.GetStream();
                            string msg = "@" + jsonObj.ToString() + "&";
                            Byte[] sendBytes = Encoding.UTF8.GetBytes(msg);
                            ns.Write(sendBytes, 0, sendBytes.Length);
                            ns.Flush();
                            watch.Stop();
                            log.InfoFormat("发送结果耗时：{0}ms", watch.ElapsedMilliseconds);
                            log.DebugFormat("发送结果数据：{0}", msg);
                        }
                        else
                        {
                            log.Error("TCP 重连失败，连接断开");
                        }
                    }

                    if (UseSaveNgImage)
                    {
                        // 将返回的json结果加入队列，等待解析
                        resultQueue.Enqueue(jsonObj);
                        ThreadPool.QueueUserWorkItem(ProcessResult);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("发送检测返回结果出错", ex);
                }
            }
        }

        private void ProcessResult(object state)
        {
            JObject result;
            if (resultQueue.TryDequeue(out result))
            {
                var judgeList = MLJsonParser.Parse(result);
                ValidResponseImageName(judgeList);
                foreach ((string sn, string contents, string decision) in judgeList)
                {
                    if (srcImageSet.ContainsKey((sn, contents)))
                    {
                        (Mat mat, string imageName, DateTime time) = srcImageSet[(sn, contents)];
                        if (decision == "FAIL")
                        {
                            string surface = GetInfoFromContents(contents);
                            string imagePath = GetImagePath(sn, surface);
                            mat.SaveImage($@"{imagePath}\{imageName}");
                        }

                        mat.Dispose();
                        srcImageSet.Remove((sn, contents));
                    }
                }
            }
        }

        private string GetInfoFromContents(string contents)
        {
            var infos = contents.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (infos.Length < FILE_NAME_MAX_INDEX)
                throw new Exception($"图片文件名{contents}不符合定义");

            string surfStr = infos[FILE_NAME_SURFACE_INDEX];

            return surfStr;
        }

        private string GetImagePath(string sn, string surface)
        {
            Action<string> checkDir = (dir) => { if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); };

            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            string dateDir = $@"{SaveNgImageFolder}\{dateStr}";
            checkDir(dateDir);

            string snDir = $@"{dateDir}\{sn}";
            checkDir(snDir);

            string surfaceDir = $@"{snDir}\{surface}";
            checkDir(surfaceDir);

            return surfaceDir;
        }
    }
}
