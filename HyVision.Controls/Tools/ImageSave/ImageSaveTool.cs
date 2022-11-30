using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

using GL.Kit.Log;
using HyVision.Models;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace HyVision.Tools.ImageSave
{
    public class ImageSaveTool : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(ImageSaveToolUI);

        public int Width { get => width; set => width = value; }

        public int Height { get => height; set => height = value; }

        public int Quality { get => quality; set => quality = value; }

        public string SaveImageFolder { get => saveImageFolder; set => saveImageFolder = value; }

        private int width;
        private int height;
        private int quality;
        private string saveImageFolder;

        private LogPublisher log;

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        public override bool Initialize()
        {
            if (log == null)
                log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

            return true;
        }

        public override void Save()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
        }

        protected override void Run2(string subName)
        {
            try
            {
                Process();
            }
            catch (Exception ex)
            {
                OnException($"ImageSaveTool 运行出错！错误信息：{ex.Message}", new Exception($"ImageSaveTool 运行失败!"));
            }
        }

        private void Process()
        {          
            if (string.IsNullOrEmpty(SaveImageFolder))
                OnException($"ImageSaveTool 没有定义存图文件夹的地址！", new Exception($"ImageSaveTool 运行失败!"));
            if (!Directory.Exists(SaveImageFolder))
                OnException($"ImageSaveTool 未找到指定的存图文件夹！", new Exception($"ImageSaveTool 运行失败!"));

            PathInfo pathInfo = new PathInfo();

            for (int i = 0; i < Inputs.Count; i++)
            {
                //if (Inputs[i] == null || Inputs[i].Value == null)
                //{
                //    if (Inputs[i] != null)
                //    {
                //        OnException($"输入参数[{Inputs[i].Name}]值为空！", new Exception($"ResizeQuality 模块运行失败!"));
                //    }
                //    else
                //    {
                //        OnException($"第[{i + 1}]个输入参数值为空！", new Exception($"ResizeQuality 模块运行失败!"));
                //    }
                //}
                //

                if (Inputs[i] != null && Inputs[i].Value != null && Inputs[i]?.ValueType == typeof(HyImage))
                {
                    if (Inputs[i].AttachedParams != null)
                    {
                        foreach (var (Name, Value) in Inputs[i].AttachedParams)
                        {
                            switch (Name)
                            {
                                case ConstField.PRODUCT_SN:
                                    pathInfo.SN = Value as string;
                                    break;
                                case ConstField.FOV_LOCATION:
                                    pathInfo.FovLoc = Value as string;
                                    break;
                                case ConstField.SURFACE_CODE:
                                    pathInfo.SurfaceCode = Value as string;
                                    break;
                                case ConstField.LIGHT_NAME:
                                    pathInfo.LightName = Value as string;
                                    break;
                                case ConstField.PRODUCT_MODEL:
                                    pathInfo.ProductModel = Value as string;
                                    break;
                                case ConstField.PRODUCT_COLOR:
                                    pathInfo.ProductColor = Value as string;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (Inputs[ConstField.CAMERA_NO] != null && Inputs[ConstField.CAMERA_NO].Value != null)
                    {
                        pathInfo.CamNo = Inputs[ConstField.CAMERA_NO].Value as string;
                    }
                    else
                    {
                        log.Error($"输入项{ConstField.CAMERA_NO}为空！");
                    }

                    if (Inputs[ConstField.PRODUCT_SN] != null && Inputs[ConstField.PRODUCT_SN].Value != null)
                    {
                        pathInfo.SN = Inputs[ConstField.PRODUCT_SN].Value as string;
                    }
                    else
                    {
                        log.Error($"输入项{ConstField.PRODUCT_SN}为空！");
                    }

                    if (Inputs[ConstField.FOV_LOCATION] != null && Inputs[ConstField.FOV_LOCATION].Value != null)
                    {
                        pathInfo.FovLoc = Inputs[ConstField.FOV_LOCATION].Value as string;
                    }
                    else
                    {
                        log.Error($"输入项{ConstField.FOV_LOCATION}为空！");
                    }

                    if (Inputs[ConstField.SURFACE_CODE] != null && Inputs[ConstField.SURFACE_CODE].Value != null)
                    {
                        pathInfo.SurfaceCode = Inputs[ConstField.SURFACE_CODE].Value as string;
                    }
                    else
                    {
                        log.Error($"输入项{ConstField.SURFACE_CODE}为空！");
                    }

                    if (Inputs[ConstField.PRODUCT_MODEL] != null && Inputs[ConstField.PRODUCT_MODEL].Value != null)
                    {
                        pathInfo.ProductModel = Inputs[ConstField.PRODUCT_MODEL].Value as string;
                    }
                    else
                    {
                        log.Error($"输入项{ConstField.PRODUCT_MODEL}为空！");
                    }

                    if (Inputs[ConstField.PRODUCT_COLOR] != null && Inputs[ConstField.PRODUCT_COLOR].Value != null)
                    {
                        pathInfo.ProductColor = Inputs[ConstField.PRODUCT_COLOR].Value as string;
                    }
                    else
                    {
                        log.Error($"输入项{ConstField.PRODUCT_COLOR}为空！");
                    }

                    if (string.IsNullOrEmpty(pathInfo.LightName))
                        pathInfo.LightName = "ALL";

                    // Resize and quality
                    ResizeAndQualityUsingSharpCV(((HyImage)Inputs[i].Value).Image, Width, Height, pathInfo);
                }
            }
        }

        public void ResizeAndQualityUsingSharpCV(Bitmap ImageOriginal, int width, int height, PathInfo pathInfo)
        {
            Mat mat = BitmapConverter.ToMat(ImageOriginal);
            Mat saveMat = null;
            if (width > 0 && height > 0)
            {
                OpenCvSharp.Size opencvSize = new OpenCvSharp.Size(width, height);
                Mat SizeMat = new Mat();
                Cv2.Resize(mat, SizeMat, opencvSize);
                saveMat = SizeMat;
            }
            else
            {
                saveMat = mat;
            }

            if (quality == 0)
                quality = 100;

            ImageEncodingParam encodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, quality);
            string imageName = $"{pathInfo.ProductModel}.{pathInfo.ProductColor}.{pathInfo.SN}.CosmeticAOI.{DateTime.Now.ToString("yyyyMMdd.HHmmssfff")}.{pathInfo.SurfaceCode}.{pathInfo.LightName}.{pathInfo.CamNo}_{pathInfo.FovLoc}.jpg";
            Cv2.ImWrite($@"{SaveImageFolder}\{imageName}", saveMat, encodingParam);
            saveMat.Dispose();
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
