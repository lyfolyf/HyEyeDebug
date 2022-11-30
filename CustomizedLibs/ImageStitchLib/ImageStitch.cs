using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Collections.Concurrent;
using OpenCvSharp;

namespace ImageStitchLib
{
    using OPoint = OpenCvSharp.Point;

    public class ImageStitch
    {
        private const int IMAGE_IN_MEMORY_TIMEOUT = 300000;
        private string outDirectory;
        private int imageQuality;

        // 用于接收客户端推送数据
        private static Dictionary<string, ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>> dictSurfaceQueue
            = new Dictionary<string, ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>>();

        // 用于存放整个面的大图
        private static Dictionary<string, (Mat, DateTime)> bigImages = new Dictionary<string, (Mat, DateTime)>();

        // 用于标记每个大面中那些局部图已经堆叠，那些还没有堆叠
        private static Dictionary<string, Dictionary<OPoint, bool>> surfaceMaps = new Dictionary<string, Dictionary<OPoint, bool>>();

        // 用于上报消息
        public static event Action<bool, string> LogMsg;
        private TcpClient client;

        private static object syncRoot = new Object();
        private static volatile ImageStitch imageStitch = null;

        private ImageStitch(string rootOut, int quality)
        {
            outDirectory = rootOut;
            imageQuality = quality;
            client = new TcpClient();

            ThreadPool.SetMaxThreads(20, 10);
        }

        public static ImageStitch GetInstance(string rootOut, int quality, Action<bool, string> callback)
        {
            // double-check locking 方法解决了线程并发问题
            if (imageStitch == null)
            {
                lock (syncRoot)
                {
                    if (imageStitch == null)
                    {
                        imageStitch = new ImageStitch(rootOut, quality);
                        imageStitch.init();
                        LogMsg += callback;
                    }
                }
            }

            return imageStitch;
        }

        public bool TcpConnect(IPAddress ipAddress, int iPort)
        {
            if (client.Connected == false)
                client.Connect(ipAddress, iPort);

            return client.Connected;
        }

        private void init()
        {
            dictSurfaceQueue.Add("BC.Polarize", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("BC.Universal", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("DH.Polarize", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("DH.Universal", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("LCM.Coaxial", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("LCM.Pattern_1", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("LCM.Pattern_3", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("TC.Polarize", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("TC.Universal_Metal", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("Corner", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("Side1", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("Side2", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("Side3", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
            dictSurfaceQueue.Add("Side4", new ConcurrentQueue<(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)>());
        }

        public void Exit()
        {
            dictSurfaceQueue.Clear();
            client?.Dispose();
            bigImages.Clear();
            surfaceMaps.Clear();
            imageStitch = null;
            LogMsg = null;
        }

        public void Run(string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat)
        {
            try
            {
                string bigImageKey = GetBigImageKey(surfaceCode, fovLoc, lightName);
                if (dictSurfaceQueue.ContainsKey(bigImageKey))
                {
                    var queue = dictSurfaceQueue[bigImageKey];
                    queue.Enqueue((sn, model, color, surfaceCode, lightName, cameraNo, fovLoc, mat));

                    ThreadPool.QueueUserWorkItem(DoProcess, bigImageKey);
                }
            }
            catch (Exception ex)
            {
                // Logger.Error(ex.Message, ex);
                LogMsg?.Invoke(true, ex.Message);
            }
        }

        private void DoProcess(object param)
        {
            try
            {
                string bigImageKey = param as string;
                if (dictSurfaceQueue[bigImageKey].Count > 0)
                {
                    lock (syncRoot)
                    {
                        // 将接收数据队列中拿走满足该面需要的图片数量进入到处理队列
                        (string, string, string, string, string, int, int, Mat) target;
                        if (dictSurfaceQueue[bigImageKey].TryDequeue(out target))
                        {
                            (string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat) = target;
                            Mat bigMat = null;
                            if (StitchImage(bigImageKey, sn, model, color, surfaceCode, lightName, cameraNo, fovLoc, mat, out bigMat))
                            {
                                saveStitchedImageCV(bigMat, bigImageKey, model, color, sn);
                                //CompressImageCV(bigMat);
                                bigMat.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("发生严重错误，请联系程序员小哥哥调试! 错误消息: {0}", ex.Message);
                // Logger.Error(msg, ex);
                LogMsg?.Invoke(true, msg);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public bool StitchImage(string bigImageKey, string sn, string model, string color, string surfaceCode, string lightName, int cameraNo, int fovLoc, Mat mat, out Mat bigMat)
        {
            Light light;
            Surface surface;
            Camera camera;
            GetInfoFromFile(surfaceCode, lightName, cameraNo, fovLoc, out camera, out surface, out light);

            var surfCfg = SysSetting.SysConfig.DictSurfaceCfg[surface];
            int rows = surfCfg.RowsCount;
            int cols = surfCfg.ColsCount;

            string imageUnionKey = string.Format("{0}.{1}.{2}.CosmeticAOI.{3}", model, color, sn, bigImageKey);

            bigMat = null;
            DateTime createdTime = DateTime.Now;
            Dictionary<OPoint, bool> surfaceMap = null;

            if (bigImages.ContainsKey(imageUnionKey))
            {
                (bigMat, createdTime) = bigImages[imageUnionKey];
                if (bigMat == null)
                    bigMat = new Mat(rows * mat.Height, cols * mat.Width, MatType.CV_8UC3);

                surfaceMap = surfaceMaps[imageUnionKey];
                if (surfaceMap == null)
                    surfaceMap = createSurfaceMap(rows, cols);
            }
            else
            {
                bigMat = new Mat(rows * mat.Height, cols * mat.Width, MatType.CV_8UC3);
                bigImages[imageUnionKey] = (bigMat, createdTime);

                surfaceMap = createSurfaceMap(rows, cols);
                surfaceMaps[imageUnionKey] = surfaceMap;
            }

            // Prepare stitching position when loading
            OPoint p = new OPoint();
            if (surface == Surface.Corner)//Corner has special stiching arrangement
            {
                if (light == Light.Polarize)
                {
                    p.X = 1;
                }
                else if (light == Light.Universal)
                {
                    p.X = 2;
                }
                p.Y = fovLoc;
            }
            // Side has special stiching arrangement
            else if (surface == Surface.Side1 || surface == Surface.Side2 || surface == Surface.Side3 || surface == Surface.Side4)
            {
                p = GetSidePoint(light, camera, fovLoc);
            }
            else
            {
                p = SysSetting.SysConfig.DictSurfaceCfg[surface].DictBlock.First(k => k.Value == (cameraNo, fovLoc)).Key;
            }

            // Rotate/flip image if necessary.
            //if (camera == Camera.Side_CAM3 && (surface == Surface.Side1 || surface == Surface.Side3))
            //{
            //    mat = mat.Flip(FlipMode.X);
            //}
            //else if (camera == Camera.Side_CAM3 && (surface == Surface.Side2 || surface == Surface.Side4))
            //{
            //    mat = mat.Flip(FlipMode.XY);
            //}
            //else if (surface == Surface.TC || surface == Surface.LCM || surface == Surface.DH || surface == Surface.BC)
            //{
            //    mat = mat.Transpose();
            //    mat = mat.Flip(FlipMode.Y);
            //}

            if (surfaceMap.ContainsKey(p))
            {
                surfaceMap[p] = true;
            }
            else
            {
                throw new Exception(string.Format("{0}大面中图像位置不包含该位置{1}", bigImageKey, p.ToString()));
            }

            var rect = new Rect((p.X - 1) * mat.Width, (p.Y - 1) * mat.Height, mat.Width, mat.Height);
            Mat subMat = bigMat[rect];
            mat.CopyTo(subMat);

            mat.Dispose();

            var fovFlags = surfaceMap.Values.ToList();
            for (int i = 0; i < fovFlags.Count; i++)
            {
                if (fovFlags[i] == false)
                {
                    return false;
                }
            }

            string msg = string.Format("内存中还有 {0} 个大面的图!", bigImages.Count);
            PrintMessage(msg);
            bigImages.Remove(imageUnionKey);

            msg = ("图像堆叠成功!");
            PrintMessage(msg);

            for (int i = 0; i < bigImages.Count; i++)
            {
                var kv = bigImages.ElementAt(i);
                (Mat cvMat, DateTime ctTime) = kv.Value;
                double elapsedTime = (DateTime.Now - ctTime).TotalMilliseconds;
                if (elapsedTime > IMAGE_IN_MEMORY_TIMEOUT)
                {
                    saveTimeoutImageCV(cvMat, kv.Key);
                    cvMat.Dispose();
                    bigImages.Remove(kv.Key);
                }
            }

            return true;
        }

        private static void PrintMessage(string msg)
        {
            LogMsg?.Invoke(false, msg);
            // Logger.Info(msg);
        }

        private static OPoint GetSidePoint(Light light, Camera cam, int posIdx)
        {
            OPoint pt = new OPoint();
            if (light == Light.Polarize && cam == Camera.Side_CAM1)
                pt.Y = 1;
            else if (light == Light.Polarize && cam == Camera.Side_CAM2)
                pt.Y = 2;
            else if (light == Light.Polarize && cam == Camera.Side_CAM3)
                pt.Y = 3;
            else if (light == Light.Universal && cam == Camera.Side_CAM1)
                pt.Y = 4;
            else if (light == Light.Universal && cam == Camera.Side_CAM2)
                pt.Y = 5;
            else if (light == Light.Universal && cam == Camera.Side_CAM3)
                pt.Y = 6;

            if (posIdx == 1 || posIdx == 4 || posIdx == 6 || posIdx == 9)
                pt.X = 1;
            else if (posIdx == 2 || posIdx == 5 || posIdx == 7 || posIdx == 10)
                pt.X = 2;
            else if (posIdx == 3 || posIdx == 8)
                pt.X = 3;

            //if (cam == Camera.Side_CAM3)
            //{
            //    if (posIdx <= 3 || (posIdx >= 6 && posIdx <= 8))
            //    {
            //        if (pt.X == 1) pt.X = 3;
            //        else if (pt.X == 3) pt.X = 1;
            //    }
            //}

            return pt;
        }

        private string GetBigImageKey(string surfaceCode, int fovLoc, string lightName)
        {
            string bigImageKey = string.Empty;

            if (surfaceCode.Contains("Side"))
            {
                switch (fovLoc)
                {
                    case 1:
                    case 2:
                    case 3:
                        bigImageKey = "Side1";
                        break;
                    case 4:
                    case 5:
                        bigImageKey = "Side2";
                        break;
                    case 6:
                    case 7:
                    case 8:
                        bigImageKey = "Side3";
                        break;
                    case 9:
                    case 10:
                        bigImageKey = "Side4";
                        break;
                    default:
                        break;
                }
            }
            else if (surfaceCode.Contains("Corner"))
            {
                bigImageKey = "Corner";
            }
            else
            {
                bigImageKey = string.Format("{0}.{1}", surfaceCode, lightName);
            }

            return bigImageKey;
        }

        // J314.R.S111.CosmeticAOI.20220309.122350999.DH.Universal.1_3.jpg
        private static void GetInfoFromFile(string surfaceCode, string lightName, int cameraNo, int fovLoc, out Camera camera, out Surface surface, out Light light)
        {
            surface = Surface.DH;

            if (surfaceCode.Contains("Side"))
            {
                switch (fovLoc)
                {
                    case 1:
                    case 2:
                    case 3:
                        surface = Surface.Side1;
                        break;
                    case 4:
                    case 5:
                        surface = Surface.Side2;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        surface = Surface.Side3;
                        break;
                    case 9:
                    case 10:
                        surface = Surface.Side4;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                surface = (Surface)Enum.Parse(typeof(Surface), surfaceCode);
            }

            if (surfaceCode == "Corner")
                lightName = lightName.Substring(0, lightName.Length - 1);

            light = (Light)Enum.Parse(typeof(Light), lightName);

            switch (surface)
            {
                case Surface.TC:
                    camera = Camera.TC_CAM;
                    break;
                case Surface.LCM:
                    camera = Camera.LCM_CAM;
                    break;
                case Surface.Mandrel:
                    camera = Camera.Mandrel_CAM;
                    break;
                case Surface.Logo:
                    camera = Camera.Logo_CAM;
                    break;
                case Surface.Corner:
                    camera = Camera.Corner_CAM;
                    break;
                case Surface.Side1:
                case Surface.Side2:
                case Surface.Side3:
                case Surface.Side4:
                    switch (cameraNo)
                    {
                        case 1:
                            camera = Camera.Side_CAM1;
                            break;
                        case 2:
                            camera = Camera.Side_CAM2;
                            break;
                        case 3:
                            camera = Camera.Side_CAM3;
                            break;
                        default:
                            camera = Camera.Side_CAM1;
                            break;
                    }
                    break;
                case Surface.DH:
                    camera = Camera.DH_CAM;
                    break;
                case Surface.BC:
                    camera = Camera.BC_CAM;
                    break;
                default:
                    camera = Camera.TC_CAM;
                    break;
            }
        }

        private static Dictionary<OPoint, bool> createSurfaceMap(int row, int col)
        {
            Dictionary<OPoint, bool> dict = new Dictionary<OPoint, bool>();
            for (int i = 0; i < col; i++)
                for (int j = 0; j < row; j++)
                {
                    OPoint pt = new OPoint(i + 1, j + 1);
                    dict.Add(pt, false);
                }
            return dict;
        }

        private void SendMessage(string imageName)
        {
            // 通知上位机
            if (client.Connected)
            {
                NetworkStream ns = client.GetStream();
                Byte[] sendBytes = Encoding.UTF8.GetBytes(imageName);
                ns.Write(sendBytes, 0, sendBytes.Length);
                ns.Flush();
            }
        }

        private void saveTimeoutImageCV(Mat mat, string imageUnionKey)
        {
            if (!Directory.Exists(outDirectory))
                Directory.CreateDirectory(outDirectory);

            string time = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
            string fileName = imageUnionKey.Replace("CosmeticAOI", $"CosmeticAOI.{time}") + ".jpg";
            string filePath = outDirectory + "\\" + fileName;

            ImageEncodingParam encodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, imageQuality);
            mat.SaveImage(filePath, encodingParam);

            string msg = string.Format("堆叠图像超时 {0}ms, 图片位置: {1}", IMAGE_IN_MEMORY_TIMEOUT, filePath);
            LogMsg?.Invoke(true, msg);
        }

        private void saveStitchedImageCV(Mat mat, string bigImageKey, string model, string color, string sn)
        {
            Action<string> checkDir = (dir) => { if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); };
            checkDir(outDirectory);

            string snDir = $@"{outDirectory}\{sn}";
            checkDir(snDir);

            string[] arrSN = sn.Split('-');
            if (arrSN.Length > 0)
                sn = arrSN[0];

            string time = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
            string fileName = $"{model}.{color}.{sn}.CosmeticAOI.{time}.{bigImageKey}.jpg";
            string filePath = snDir + "\\" + fileName;
            ImageEncodingParam encodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, imageQuality);
            mat.SaveImage(filePath, encodingParam);

            SendMessage(filePath);
        }

        private byte[] CompressImageCV(Mat mat)
        {
            DateTime start = DateTime.Now;
            ImageEncodingParam encodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, imageQuality);
            byte[] buf = null;
            Cv2.ImEncode(".jpg", InputArray.Create(mat), out buf, encodingParam);
            if (buf != null)
                Console.WriteLine("压缩后的图像大小{0}MB", buf.Length / (1024 * 1024));

            double diff = (DateTime.Now - start).TotalMilliseconds;
            Console.WriteLine("堆叠图像后压缩耗时：{0}ms", diff);
            BytesToFile(buf);
            //BytesToBitmap(buf);
            return buf;
        }

        private Mat Decompress2Mat(byte[] buf)
        {
            DateTime start = DateTime.Now;
            Mat mat = Cv2.ImDecode(buf, ImreadModes.Color);
            if (mat != null)
                Console.WriteLine("解压缩后的图像大小{0}MB", (mat.Rows * mat.Cols * mat.Channels()) / (1024 * 1024));

            double diff = (DateTime.Now - start).TotalMilliseconds;
            Console.WriteLine("解缩耗时：{0}ms", diff);
            return mat;
        }

        private void BytesToFile(byte[] buf)
        {
            DateTime start = DateTime.Now;
            FileStream fs = new FileStream("test.jpg", FileMode.Create);
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
            fs.Close();
            double diff = (DateTime.Now - start).TotalMilliseconds;
            Console.WriteLine("存盘耗时：{0}ms", diff);
        }

        public static Bitmap BytesToBitmap(byte[] bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                bitmap.Save("bitmap.jpg", ImageFormat.Jpeg);
                Console.WriteLine("Save bitmap finished!");
                return bitmap;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
