using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Generic;

using GL.Kit.Log;
using HyVision.Models;
using ImageCollectionLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace HyVision.Tools.ImageProcess
{
    public class ImageProcessTool : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(ImageProcessUI);

        public bool UseResize { get; set; }
        public bool UseQuality { get; set; }
        public bool UseRotation { get; set; }
        public bool UseSaveImage { get; set; }
        public bool UseSendImage { get; set; }
        public bool UseSendResult { get; set; }
        public bool UseTestMode { get; set; }
        public bool UseSaveNgImage { get; set; }

        public int Width { get => width; set => width = value; }

        public int Height { get => height; set => height = value; }

        public int Quality { get => quality; set => quality = value; }

        public string SaveImageFolder { get => saveImageFolder; set => saveImageFolder = value; }

        public string SaveNgImageFolder { get => saveNgImageFolder; set => saveNgImageFolder = value; }

        public string IP { get => ip; set => ip = value; }

        public string Port { get => port; set => port = value; }

        public string TcpIP { get; set; }

        public string TcpPort { get; set; }

        private int width;
        private int height;
        private int quality;
        private string ip;
        private string port;
        private string saveImageFolder;
        private string saveNgImageFolder;
        private bool initialized = false;

        private ImageCollection collection;
        private IGLog log;

        const string LOGO = "Logo";
        const string CONER = "Corner";
        const string OTHER = "Other";
        const string SEPARATOR = "_";
        Dictionary<string, string> timeSet = new Dictionary<string, string>();

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        public override bool Initialize()
        {
            if (log == null)
                log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

            if (UseSendImage)
            {
                if (string.IsNullOrEmpty(IP) || !IPAddress.TryParse(IP, out IPAddress restIP))
                {
                    log?.Error("请输入正确的IP地址");
                    return false;
                }

                if (string.IsNullOrEmpty(Port) || !int.TryParse(Port, out int restPort))
                {
                    log?.Error("请输入正确的端口号");
                    return false;
                }

                if (UseSendResult)
                {
                    if (string.IsNullOrEmpty(TcpIP) || !IPAddress.TryParse(TcpIP, out IPAddress tcpAddress))
                    {
                        log?.Error("TCP IP地址输入不正确");
                        return false;
                    }

                    if (string.IsNullOrEmpty(TcpPort) || !int.TryParse(TcpPort, out int tcpPort))
                    {
                        log?.Error("TCP 端口号输入不正确");
                        return false;
                    }

                    collection = ImageCollection.GetInstance(restIP, restPort, tcpAddress, tcpPort, log);
                    collection.UseSendResult = UseSendResult;
                    collection.UseSaveNgImage = UseSaveNgImage;

                    if (UseSaveNgImage)
                    {
                        collection.SaveNgImageFolder = saveNgImageFolder;
                    }
                }
                else
                {
                    collection = ImageCollection.GetInstance(restIP, restPort, log);
                }

                collection.UseTestMode = UseTestMode;
            }

            initialized = true;
            return initialized;
        }

        public override void Save()
        {

        }

        protected override void Dispose(bool disposing)
        {
            collection?.Dispose();
        }

        protected override void Run2(string subName)
        {
            try
            {
                if (!initialized)
                    Initialize();

                Process();
            }
            catch (Exception ex)
            {
                OnException($"错误信息：{ex.Message}", new Exception($"ImageProcess 运行失败!"));
            }
        }

        private void Process()
        {
            if (UseResize)
            {
                if (Width == 0 || Height == 0)
                    OnException($"没有设置 Resize 的大小！", new Exception($"ImageProcess 模块运行失败!"));
            }

            if (UseQuality)
            {
                if (Quality < 1)
                    OnException($"没有设置图像质量！", new Exception($"ImageProcess 模块运行失败!"));
            }

            if (UseSaveImage)
            {
                if (string.IsNullOrEmpty(SaveImageFolder))
                    OnException($"没有设置存图文件夹的地址！", new Exception($"ImageProcess 模块运行失败!"));

                if (!Directory.Exists(SaveImageFolder))
                    OnException($"未找到指定的存图文件夹！", new Exception($"ImageProcess 模块运行失败!"));
            }

            PathInfo pathInfo = new PathInfo();
            foreach ((string name, Type type) in ConstField.GetProperties())
            {
                if (Inputs[name] != null && Inputs[name].Value != null)
                {
                    string value = Inputs[name].Value as string;
                    switch (name)
                    {
                        case ConstField.CAMERA_NO:
                            pathInfo.CamNo = value;
                            break;
                        case ConstField.PRODUCT_SN:
                            pathInfo.SN = value;
                            break;
                        case ConstField.FOV_LOCATION:
                            pathInfo.FovLoc = value;
                            break;
                        case ConstField.SURFACE_CODE:
                            pathInfo.SurfaceCode = value;
                            break;
                        case ConstField.LIGHT_NAME:
                            pathInfo.LightName = value;
                            break;
                        case ConstField.PRODUCT_MODEL:
                            pathInfo.ProductModel = value;
                            break;
                        case ConstField.PRODUCT_COLOR:
                            pathInfo.ProductColor = value;
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    log.Error($"输入项{name}为空！");
                }
            }

            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i] != null && Inputs[i].Value != null && Inputs[i]?.ValueType == typeof(HyImage))
                {
                    HyImage hyImage = (HyImage)Inputs[i].Value;
                    if (hyImage != null)
                    {
                        // Resize
                        Mat resizeMat = null;
                        if (UseResize)
                        {
                            resizeMat = Resize(hyImage.Image, Width, Height);
                        }

                        // Rotation
                        Mat retateMat = null;
                        if (UseRotation)
                        {
                            Camera camera;
                            Surface surface;
                            GetInfoFromInput(pathInfo, out camera, out surface);

                            Mat mat;
                            if (UseResize && resizeMat != null)
                            {
                                mat = resizeMat;
                            }
                            else
                            {
                                mat = BitmapConverter.ToMat(hyImage.Image);
                            }

                            retateMat = Rotation(mat, camera, surface);
                        }

                        // Compress
                        byte[] buffer = null;
                        if (UseQuality)
                        {
                            Mat mat;
                            if (UseRotation && retateMat != null)
                            {
                                mat = retateMat;
                            }
                            else
                            {
                                if (UseResize && resizeMat != null)
                                {
                                    mat = resizeMat;
                                }
                                else
                                {
                                    mat = BitmapConverter.ToMat(hyImage.Image);
                                }
                            }

                            buffer = CompressImageCV(mat);
                            mat.Dispose();
                        }

                        // Save image
                        if (UseSaveImage)
                        {
                            string imagePath = GetImagePath(pathInfo);
                            string imageName = GetImageName(pathInfo);
                            string imageFullPath = $@"{imagePath}\{imageName}";

                            if (UseQuality && buffer != null)
                            {
                                BytesToFile(buffer, imageFullPath);
                            }
                            else
                            {
                                Mat mat;
                                if (UseRotation && retateMat != null)
                                {
                                    mat = retateMat;
                                }
                                else if (UseResize && resizeMat != null)
                                {
                                    mat = resizeMat;
                                }
                                else
                                {
                                    mat = BitmapConverter.ToMat(hyImage.Image);
                                }

                                mat.SaveImage(imagePath);
                                mat.Dispose();
                            }
                        }

                        // Send image
                        if (UseSendImage && buffer != null)
                        {
                            if (collection == null)
                            {
                                log.Error("Image collection instance not initialized!");
                                return;
                            }

                            string imageName = GetImageName(pathInfo);
                            string productColor = ConvertColorCode(pathInfo.ProductColor);
                            string lightName = pathInfo.SurfaceCode == CONER ? pathInfo.LightName.Substring(0, pathInfo.LightName.Length - 1) : pathInfo.LightName;
                            string contents = string.Format("{0}-{1}-{2}-{3}-{4}", pathInfo.SurfaceCode, pathInfo.CamNo, pathInfo.FovLoc, productColor, lightName);

                            if (lightName != "Bar") // 条光不需要发送
                            {
                                collection.Push(pathInfo.SN, pathInfo.SurfaceCode, pathInfo.FovLoc, contents, imageName, buffer);

                                if (UseSaveNgImage)
                                {
                                    collection.Push(pathInfo.SN, contents, imageName, hyImage.Image);
                                }
                            }
                        }
                    }
                }
            }
        }

        private Mat Resize(Bitmap ImageOriginal, int width, int height)
        {
            Stopwatch watch = Stopwatch.StartNew();
            Mat mat = BitmapConverter.ToMat(ImageOriginal);
            OpenCvSharp.Size opencvSize = new OpenCvSharp.Size(width, height);
            Mat sizeMat = new Mat();
            Cv2.Resize(mat, sizeMat, opencvSize);
            mat.Dispose();
            watch.Stop();
            log.InfoFormat("改变图像大小耗时：{0}ms", watch.ElapsedMilliseconds);

            return sizeMat;
        }

        private Mat Rotation(Mat mat, Camera camera, Surface surface)
        {
            Stopwatch watch = Stopwatch.StartNew();
            if (camera == Camera.Side_CAM3 && (surface == Surface.Side1 || surface == Surface.Side3))
            {
                mat = mat.Flip(FlipMode.X);
            }
            else if (camera == Camera.Side_CAM3 && (surface == Surface.Side2 || surface == Surface.Side4))
            {
                mat = mat.Flip(FlipMode.XY);
            }
            else if (surface == Surface.TC || surface == Surface.LCM || surface == Surface.DH || surface == Surface.BC)
            {
                mat = mat.Transpose();
                mat = mat.Flip(FlipMode.Y);
            }

            watch.Stop();
            log.InfoFormat("旋转图像耗时：{0}ms", watch.ElapsedMilliseconds);
            return mat;
        }

        private byte[] CompressImageCV(Mat mat)
        {
            Stopwatch watch = Stopwatch.StartNew();
            ImageEncodingParam encodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, Quality);
            byte[] buf;
            Cv2.ImEncode(".jpg", InputArray.Create(mat), out buf, encodingParam);

            watch.Stop();
            log.InfoFormat("压缩图像耗时：{0}ms", watch.ElapsedMilliseconds);
            return buf;
        }

        private byte[] CompressImageSharp(Bitmap bitmap)
        {
            ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(a => a.FormatID == ImageFormat.Jpeg.Guid);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, Quality);
            encoderParameters.Param[0] = encoderParameter;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, imageCodecInfo, encoderParameters);
                return ms.GetBuffer();
            }
        }

        private void BytesToFile(byte[] buffer, string imagePath)
        {
            Stopwatch watch = Stopwatch.StartNew();
            FileStream fs = new FileStream(imagePath, FileMode.Create);
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
            fs.Close();
            watch.Stop();
            log.InfoFormat("保存图像耗时：{0}ms", watch.ElapsedMilliseconds);
        }

        private void GetInfoFromInput(PathInfo pathInfo, out Camera camera, out Surface surface)
        {
            surface = Surface.DH;

            if (pathInfo.SurfaceCode.Contains("Side"))
            {
                int fov = int.Parse(pathInfo.FovLoc);
                switch (fov)
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
                surface = (Surface)Enum.Parse(typeof(Surface), pathInfo.SurfaceCode);
            }

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
                    int camNo = int.Parse(pathInfo.CamNo);
                    switch (camNo)
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

        private string GetImagePath(PathInfo pathInfo)
        {
            Action<string> checkDir = (dir) => { if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); };

            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            string dateDir = $@"{SaveImageFolder}\{dateStr}";
            checkDir(dateDir);

            string colorDir = $@"{dateDir}\{pathInfo.ProductColor}";
            checkDir(colorDir);

            string snDir = $@"{colorDir}\{pathInfo.SN}";
            checkDir(snDir);

            string surfaceDir = $@"{snDir}\{pathInfo.SurfaceCode}";
            checkDir(surfaceDir);

            return surfaceDir;
        }

        private string GetImageName(PathInfo pathInfo)
        {
            // 客户要求一个产品相同的面的同一个点位的时间戳一样
            string sn = string.Empty;
            string[] arrSN = pathInfo.SN.Split('-');
            if (arrSN.Length > 0)
                sn = arrSN[0];

            string camNo = pathInfo.CamNo;
            string fovLoc = pathInfo.FovLoc;
            string surfaceCode = pathInfo.SurfaceCode;
            string lightName = pathInfo.LightName;
            string productModel = pathInfo.ProductModel;
            string productColor = ConvertColorCode(pathInfo.ProductColor);

            string imageName = string.Empty;
            switch (surfaceCode)
            {
                case LOGO:
                    // Logo 只有一个点位，所以只有一个时间戳
                    string logoKey = sn + SEPARATOR + LOGO;
                    string logoTime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                    if (!timeSet.ContainsKey(logoKey))
                    {
                        // 移除Logo旧的时间戳，防止长时间运行这个时间集合越来越大
                        var obsoletedKeys = timeSet.Keys.Where(key => key.EndsWith(SEPARATOR + LOGO));
                        for (int i = 0; i < obsoletedKeys.Count(); i++)
                        {
                            timeSet.Remove(obsoletedKeys.ElementAt(i));
                        }

                        timeSet.Add(logoKey, logoTime);
                    }
                    break;
                case CONER:
                    // Coner的点位信息按照光源来区分, 一个产品有多个点位
                    string conerPos = lightName.Substring(lightName.Length - 1);
                    string conerKey = conerPos + SEPARATOR + sn + SEPARATOR + CONER;
                    string conerTime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                    if (!timeSet.ContainsKey(conerKey))
                    {
                        // 当新的sn到达时移除Coner旧的sn时间戳，防止长时间运行这个时间集合越来越大
                        var obsoletedKeys = timeSet.Keys.Where(key => key.EndsWith(SEPARATOR + CONER) && !key.Contains(sn));
                        for (int i = 0; i < obsoletedKeys.Count(); i++)
                        {
                            timeSet.Remove(obsoletedKeys.ElementAt(i));
                        }

                        timeSet.Add(conerKey, conerTime);
                    }
                    break;
                default:
                    // LCM, TC, DH, BC, Side 的点位信息时间戳处理
                    string otherPos = camNo + SEPARATOR + fovLoc;
                    string otherKey = otherPos + SEPARATOR + sn + SEPARATOR + OTHER;
                    string otherTime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                    if (!timeSet.ContainsKey(otherKey))
                    {
                        // 当新的sn到达时移除其它面旧的sn时间戳，防止长时间运行这个时间集合越来越大
                        var obsoletedKeys = timeSet.Keys.Where(key => key.EndsWith(SEPARATOR + OTHER) && !key.Contains(sn));
                        for (int i = 0; i < obsoletedKeys.Count(); i++)
                        {
                            timeSet.Remove(obsoletedKeys.ElementAt(i));
                        }

                        timeSet.Add(otherKey, otherTime);
                    }
                    break;
            }

            if (surfaceCode.Equals(LOGO) && lightName.Equals("Coaxial"))
            {
                string logoKey = sn + SEPARATOR + LOGO;
                string logoTime = timeSet[logoKey];
                imageName = $"{productModel}.{productColor}.{sn}.CosmeticAOI.{logoTime}.{surfaceCode}.{lightName}.jpg";
            }
            else if (surfaceCode.Equals(CONER))
            {
                string conerPos = lightName.Substring(lightName.Length - 1);
                string conerKey = conerPos + SEPARATOR + sn + SEPARATOR + CONER;
                string conerTime = timeSet[conerKey];
                imageName = $"{productModel}.{productColor}.{sn}.CosmeticAOI.{conerTime}.{surfaceCode}.{lightName.Substring(0, lightName.Length - 1)}.{conerPos}.jpg";
            }
            else
            {
                string otherPos = camNo + SEPARATOR + fovLoc;
                string otherKey = otherPos + SEPARATOR + sn + SEPARATOR + OTHER;
                string otherTime = timeSet[otherKey];
                imageName = $"{productModel}.{productColor}.{sn}.CosmeticAOI.{otherTime}.{surfaceCode}.{lightName}.{otherPos}.jpg";
            }

            return imageName;
        }

        private string ConvertColorCode(string productColor)
        {
            if (productColor.Contains(HyEye.API.GlobalParams.SubName_DeepBlue))
                productColor = "D";
            if (productColor.Contains(HyEye.API.GlobalParams.SubName_Bassalt))
                productColor = "B";
            else if (productColor.Contains(HyEye.API.GlobalParams.SubName_Gray))
                productColor = "R";
            else if (productColor.Contains(HyEye.API.GlobalParams.SubName_Gold))
                productColor = "G";
            else if (productColor.Contains(HyEye.API.GlobalParams.SubName_Silver))
                productColor = "S";
            return productColor;
        }
    }

    public struct PathInfo
    {
        public string SN { get; set; }
        public string CamNo { get; set; }
        public string FovLoc { get; set; }
        public string SurfaceCode { get; set; }
        public string LightName { get; set; }
        public string ProductModel { get; set; }
        public string ProductColor { get; set; }
    }

    public class ConstField
    {
        // input const
        public const string PRODUCT_SN = "SN";
        public const string CAMERA_NO = "CameraNo";
        public const string FOV_LOCATION = "GrabPosNo";
        public const string SURFACE_CODE = "InspectPosName";
        public const string LIGHT_NAME = "LightName";
        public const string PRODUCT_MODEL = "ProductType";
        public const string PRODUCT_COLOR = "SubName";

        public static List<(string, Type)> GetProperties()
        {
            List<(string, Type)> nameList = new List<(string, Type)>();
            nameList.Add((CAMERA_NO, typeof(string)));
            nameList.Add((SURFACE_CODE, typeof(string)));
            nameList.Add((FOV_LOCATION, typeof(string)));
            nameList.Add((LIGHT_NAME, typeof(string)));
            nameList.Add((PRODUCT_MODEL, typeof(string)));
            nameList.Add((PRODUCT_COLOR, typeof(string)));
            nameList.Add((PRODUCT_SN, typeof(string)));
            return nameList;
        }
    }

    public enum Camera
    {
        TC_CAM,
        LCM_CAM,
        Mandrel_CAM,
        Logo_CAM,
        Corner_CAM,
        Side_CAM1,//up 45
        Side_CAM2,//mid 45
        Side_CAM3,//down 45
        DH_CAM,
        BC_CAM
    }

    public enum Surface
    {
        TC,
        LCM,
        Mandrel,
        Logo,
        Corner,
        Side1, //1~3
        Side2, //4~5
        Side3, //6~8
        Side4, //9~10
        DH,
        BC
    }
}
