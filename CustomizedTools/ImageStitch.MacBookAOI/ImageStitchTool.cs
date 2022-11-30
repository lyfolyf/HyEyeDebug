using System;
using System.IO;
using System.Text;
using System.Net;
using System.Drawing;
using System.Net.Sockets;
using System.Collections.Generic;

using GL.Kit.Log;
using HyVision.Models;
using HyVision.Tools;

using ImageStitchLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ImageProcess
{
    public class ImageStitchTool : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(ImageStitchUI);

        public int Width { get => width; set => width = value; }

        public int Height { get => height; set => height = value; }

        public int Quality { get => quality; set => quality = value; }

        public string SaveImageFolder { get => saveImageFolder; set => saveImageFolder = value; }

        public string IP { get => ip; set => ip = value; }

        public string Port { get => port; set => port = value; }

        private int width;
        private int height;
        private int quality;
        private string ip;
        private string port;
        private string saveImageFolder;
        private bool initialized = false;

        private IGLog log;
        private ImageStitch stitch;

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        public override bool Initialize()
        {
            if (log == null)
                log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

            if (string.IsNullOrEmpty(saveImageFolder))
                OnException("没有设置堆叠完成图像输出路径！", new Exception($"ImageStitch 模块初始化失败!"));
            if (!Directory.Exists(saveImageFolder))
                OnException($"图像输出路径文件夹不存在！", new Exception($"ImageStitch 模块初始化失败!"));

            if (quality <= 0 || quality > 100)
                quality = 100;

            stitch = ImageStitch.GetInstance(saveImageFolder, quality, StitchMsg);
            if (IPAddress.TryParse(IP, out IPAddress ipAddress) && int.TryParse(Port, out int iPort))
            {
                stitch.TcpConnect(ipAddress, iPort);
            }

            initialized = true;
            return initialized;
        }

        public override void Save()
        {

        }

        protected override void Dispose(bool disposing)
        {
            stitch.Exit();
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
                OnException($"ImageStitch 运行出错！错误信息：{ex.Message}", new Exception($"ImageStitch 运行失败!"));
            }
        }

        private void Process()
        {
            string sn = string.Empty;
            string model = string.Empty;
            string color = string.Empty;
            string surfaceCode = string.Empty;
            string lightName = string.Empty;
            int camNo = 0;
            int fovLoc = 0;

            if (Inputs[ConstField.CAMERA_NO] != null && Inputs[ConstField.CAMERA_NO].Value != null)
            {
                camNo = Convert.ToInt32(Inputs[ConstField.CAMERA_NO].Value);
            }
            else
            {
                log.Error($"输入项{ConstField.CAMERA_NO}为空！");
            }

            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i] != null && Inputs[i].Value != null && Inputs[i]?.ValueType == typeof(HyImage))
                {
                    if (Inputs[i].AttachedParams != null)
                    {
                        foreach (var (Name, Value) in Inputs[i].AttachedParams)
                        {
                            switch (Name)
                            {
                                case ConstField.PRODUCT_SN:
                                    sn = Value as string;
                                    break;
                                case ConstField.FOV_LOCATION:
                                    fovLoc = Convert.ToInt32(Value);
                                    break;
                                case ConstField.SURFACE_CODE:
                                    surfaceCode = Value as string;
                                    break;
                                case ConstField.LIGHT_NAME:
                                    lightName = Value as string;
                                    break;
                                case ConstField.PRODUCT_MODEL:
                                    model = Value as string;
                                    break;
                                case ConstField.PRODUCT_COLOR:
                                    color = Value as string;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    Bitmap bitmap = ((HyImage)Inputs[i].Value).Image;

                    // Resize
                    Mat mat;
                    if (Width > 0 && Height > 0 && (Width != bitmap.Width || Height != bitmap.Height))
                    {
                        mat = ResizeBySharpCV(bitmap);
                    }
                    else
                    {
                        mat = BitmapConverter.ToMat(bitmap);
                    }

                    color = ConvertColorCode(color);
                    stitch.Run(sn, model, color, surfaceCode, lightName, camNo, fovLoc, mat);
                }
            }
        }

        public Mat ResizeBySharpCV(Bitmap bitmap)
        {
            DateTime start = DateTime.Now;
            Mat mat = BitmapConverter.ToMat(bitmap);
            double diff = (DateTime.Now - start).TotalMilliseconds;
            log.DebugFormat("Bitmap 转化 cvmat 耗时 {0}ms", diff);

            start = DateTime.Now;
            OpenCvSharp.Size opencvSize = new OpenCvSharp.Size(Width, Height);
            Mat sizeMat = new Mat();
            Cv2.Resize(mat, sizeMat, opencvSize);
            mat.Dispose();
            diff = (DateTime.Now - start).TotalMilliseconds;
            log.DebugFormat("Resize 耗时 {0}ms", diff);
            return sizeMat;
        }

        private void StitchMsg(bool isError, string msg)
        {
            if (isError)
                log.Error(msg);
            else
                log.Info(msg);
        }

        private string ConvertColorCode(string productColor)
        {
            if (productColor.Contains("DeepBlue"))
                productColor = "D";
            if (productColor.Contains("Bassalt"))
                productColor = "B";
            else if (productColor.Contains("Gray"))
                productColor = "R";
            else if (productColor.Contains("Gold"))
                productColor = "G";
            else if (productColor.Contains("Silver"))
                productColor = "S";
            return productColor;
        }
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
            nameList.Add((SURFACE_CODE, typeof(string)));
            nameList.Add((CAMERA_NO, typeof(string)));
            nameList.Add((FOV_LOCATION, typeof(string)));
            nameList.Add((LIGHT_NAME, typeof(string)));
            nameList.Add((PRODUCT_MODEL, typeof(string)));
            nameList.Add((PRODUCT_COLOR, typeof(string)));
            nameList.Add((PRODUCT_SN, typeof(string)));
            return nameList;
        }
    }
}
