using Autofac;
using GL.Kit.Log;
using HyVision.Models;
using HyVision.Tools.PhaseDeflection.UI;
using LeadHawkCS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyVision.Tools.PhaseDeflection.BL
{
    [Serializable]
    public class PhaseDeflectionBL : BaseHyUserTool
    {
        private string calibDataFolderPath;
        public string CalibDataFolderPath { get => calibDataFolderPath; set => calibDataFolderPath = value; }

        //private string stdImageFolderPath;
        //public string STDImageFolderPath { get => stdImageFolderPath; set => stdImageFolderPath = value; }

        private bool isSaveResult;
        public bool IsSaveResult { get => isSaveResult; set => isSaveResult = value; }

        public override Type ToolEditType => typeof(PhaseDeflectionUI);

        LogPublisher log;
        private XD_PhaseDeflectionRun PhaseRun = new XD_PhaseDeflectionRun();
        private static readonly Type ImageType = typeof(HyImage);

        public string[] OutputImageName = new string[]{ "ImageMask", "GradX", "GradY", "CloudPtX", "CloudPtY", "CloudPtZ" };

        private List<Bitmap> lstInputImages = new List<Bitmap>();

        public PhaseDeflectionBL() : this($"{typeof(PhaseDeflectionBL)}")
        {

        }

        public PhaseDeflectionBL(string name)
        {
            Name = name;
        }

        protected override void Run2(string subName)
        {
            lstInputImages.Clear();
            //统计输入参数中图像类型或指针类型的参数数量
            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i].ValueType == typeof(HyImage) && Inputs[i].Value != null)
                {
                    lstInputImages.Add(((HyImage)Inputs[i].Value).Image);
                }
            }

            if (lstInputImages.Count < 10)
                OnException($"未拍完足够图像，需要拍完10张图像再运行算法！当前拍照次数：{lstInputImages.Count}！", new HyVisionException($"未拍完足够图像，需要拍完10张图像再运行算法！当前拍照次数：{lstInputImages.Count}！"));

            if(log == null)
                log = AutoFacContainer.Resolve<LogPublisher>();

            log.Info($"【算法前处理】开始执行!");
            if (!Directory.Exists(CalibDataFolderPath))
                OnException($"未找到指定的 XML 文件所在的文件夹！文件夹路径：{CalibDataFolderPath}", new HyVisionException($"未找到指定的 XML 文件所在的文件夹！文件夹路径：{CalibDataFolderPath}"));

            log.Info($"【算法前处理】开始读取XML数据");
            byte[] pPath = System.Text.Encoding.Default.GetBytes(CalibDataFolderPath);
            int nReturn = PhaseRun.GetParaByPath(pPath);
            if (nReturn == 1)
            {
                //标准镜面数据读取失败！
                OnException($"标准镜面数据读取失败！", new HyVisionException($"标准镜面数据读取失败！"));
            }
            else if (nReturn == 2)
            {
                //读取参数失败！
                OnException($"读取参数失败！", new HyVisionException($"读取参数失败！"));
            }
            log.Info($"【算法前处理】XML数据读取完成！");

            log.Info($"【算法前处理】开始锁定图像数据！");
            XDImage[] ImageList = new XDImage[lstInputImages.Count];
            Bitmap[] bmpList = lstInputImages.ToArray<Bitmap>();
            BitmapData[] bmpDataList = new BitmapData[lstInputImages.Count];

            try
            {
                for (int i = 0; i < bmpList.Length; i++)
                {
                    bmpDataList[i] = bmpList[i].LockBits(new Rectangle(0, 0, bmpList[i].Width, bmpList[i].Height), ImageLockMode.ReadWrite, bmpList[i].PixelFormat);
                    ImageList[i].imgdata = bmpDataList[i].Scan0;
                    ImageList[i].nWidth = bmpDataList[i].Stride;
                    ImageList[i].nHeight = bmpDataList[i].Height;
                }

                byte[] pSaveResultFilePath = null;

                log.Info($"【算法前处理】开始执行算法！");
                ImageDataStr ImageMask = new ImageDataStr();
                ImageDataStr GradX = new ImageDataStr();
                ImageDataStr GradY = new ImageDataStr();
                ImageDataStr CloudPtX = new ImageDataStr();
                ImageDataStr CloudPtY = new ImageDataStr();
                ImageDataStr CloudPtZ = new ImageDataStr();
                if (!PhaseRun.GetGradImage(ref ImageList[0], bmpList.Length, pSaveResultFilePath, ref ImageMask, ref GradX, ref GradY, ref CloudPtX, ref CloudPtY, ref CloudPtZ))
                {
                    OnException($"产品数据生成失败！", new HyVisionException($"产品数据生成失败！"));
                }
                log.Info($"【算法前处理】算法执行完成，已生成产品数据！");


                log.Info($"【算法前处理】开始转换产品数据类型！");
                string strMaskType = EHalconImageDataType2String(ImageMask.Type);
                string strGradXType = EHalconImageDataType2String(GradX.Type);
                string strGradYType = EHalconImageDataType2String(GradY.Type);
                string strCloudPtXType = EHalconImageDataType2String(CloudPtX.Type);
                string strCloudPtYType = EHalconImageDataType2String(CloudPtY.Type);
                string strCloudPtZType = EHalconImageDataType2String(CloudPtZ.Type);
                HyImage hyImageMask = new HyImage(strMaskType, ImageMask.nWidth, ImageMask.nHeight, ImageMask.pData);
                HyImage hyImageGradX = new HyImage(strGradXType, GradX.nWidth, GradX.nHeight, GradX.pData);
                HyImage hyImageGradY = new HyImage(strGradYType, GradY.nWidth, GradY.nHeight, GradY.pData);
                HyImage hyImageCloudPtX = new HyImage(strCloudPtXType, CloudPtX.nWidth, CloudPtX.nHeight, CloudPtX.pData);
                HyImage hyImageCloudPtY = new HyImage(strCloudPtYType, CloudPtY.nWidth, CloudPtY.nHeight, CloudPtY.pData);
                HyImage hyImageCloudPtZ = new HyImage(strCloudPtZType, CloudPtZ.nWidth, CloudPtZ.nHeight, CloudPtZ.pData);
                log.Info($"【算法前处理】产品数据类型转换完成！");


                log.Info($"【算法前处理】开始获取输出结果！");
                if (Outputs.Contains(nameof(ImageMask)))
                    Outputs[nameof(ImageMask)].Value = hyImageMask;
                if (Outputs.Contains(nameof(GradX)))
                    Outputs[nameof(GradX)].Value = hyImageGradX;
                if (Outputs.Contains(nameof(GradY)))
                    Outputs[nameof(GradY)].Value = hyImageGradY;
                if (Outputs.Contains(nameof(CloudPtX)))
                    Outputs[nameof(CloudPtX)].Value = hyImageCloudPtX;
                if (Outputs.Contains(nameof(CloudPtY)))
                    Outputs[nameof(CloudPtY)].Value = hyImageCloudPtY;
                if (Outputs.Contains(nameof(CloudPtZ)))
                    Outputs[nameof(CloudPtZ)].Value = hyImageCloudPtZ;
                log.Info($"【算法前处理】所有输出结果获取完毕！");

            }
            catch (Exception ex)
            {
                OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
            }
            finally
            {
                for (int i = 0; i < bmpList.Length; i++)
                {
                    if(bmpDataList[i] != null)
                        bmpList[i].UnlockBits(bmpDataList[i]);
                }
                log.Info($"【算法前处理】执行完成！");
            }
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public bool CalibMirrorFiles(string mirrorFilesFolderPath)
        {
            Bitmap[] bmpList = new Bitmap[10];
            BitmapData[] bmpDataList = new BitmapData[10];
            try
            {
                if (!Directory.Exists(mirrorFilesFolderPath))
                    throw new HyVisionException($"镜面图像文件夹“{mirrorFilesFolderPath}”不存在！");

                DirectoryInfo root = new DirectoryInfo(mirrorFilesFolderPath);
                IEnumerable<FileInfo> filesArry = root.GetFiles().Where(a => a.Extension.ToLower().Equals(".bmp"));
                if (filesArry == null || filesArry.Count() < 1)
                    throw new HyVisionException($"在指定的镜面图像文件夹“{mirrorFilesFolderPath}”中未找到任何BMP图像文件！");

                FileInfo[] files = filesArry.ToArray();
                if (files.Length != 10)
                    throw new HyVisionException($"在指定的镜面图像文件夹“{mirrorFilesFolderPath}”中的BMP图像文件数量应该为10，当前数量为“{files.Length}”！");

                int nBmpNum = 0;
                string[] filepaths = new string[10];
                for (int i = 0; i < files.Length && nBmpNum < 10; i++)
                {
                    if (files[i].Extension == ".bmp")
                    {
                        filepaths[nBmpNum] = files[i].FullName;
                        nBmpNum++;
                    }
                }

                Array.Sort(filepaths);
                XDImage[] ImageList = new XDImage[10];
                for (int i = 0; i < 10; i++)
                {
                    bmpList[i] = new Bitmap(filepaths[i]);
                    bmpDataList[i] = bmpList[i].LockBits(new Rectangle(0, 0, bmpList[i].Width, bmpList[i].Height), ImageLockMode.ReadWrite, bmpList[i].PixelFormat);
                    ImageList[i].imgdata = bmpDataList[i].Scan0;
                    ImageList[i].nWidth = bmpDataList[i].Stride;
                    ImageList[i].nHeight = bmpDataList[i].Height;
                }

                bool bResult = PhaseRun.GetStdAbsPhase(ref ImageList[0], 10);

                return bResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                for (int i = 0; i < 10; i++)
                {
                    bmpList[i]?.UnlockBits(bmpDataList[i]);
                }
            }
        }

        public bool CalibProductMatrix(string productFilesFolderPath)
        {
            Bitmap[] bmpList = new Bitmap[10];
            BitmapData[] bmpDataList = new BitmapData[10];
            try
            {
                if (!Directory.Exists(productFilesFolderPath))
                    throw new HyVisionException($"产品图像文件夹“{productFilesFolderPath}”不存在！");

                DirectoryInfo root = new DirectoryInfo(productFilesFolderPath);
                IEnumerable<FileInfo> filesArry = root.GetFiles().Where(a => a.Extension.ToLower().Equals(".bmp"));
                if (filesArry == null || filesArry.Count() < 1)
                    throw new HyVisionException($"在指定的产品图像文件夹“{productFilesFolderPath}”中未找到任何BMP图像文件！");

                FileInfo[] files = filesArry.ToArray();
                if (files.Length != 10)
                    throw new HyVisionException($"在指定的产品图像文件夹“{productFilesFolderPath}”中的BMP图像文件数量应该为10，当前数量为“{files.Length}”！");

                int nBmpNum = 0;
                string[] filepaths = new string[10];
                for (int i = 0; i < files.Length && nBmpNum < 10; i++)
                {
                    if (files[i].Extension == ".bmp")
                    {
                        filepaths[nBmpNum] = files[i].FullName;
                        nBmpNum++;
                    }
                }

                Array.Sort(filepaths);
                XDImage[] ImageList = new XDImage[10];
                for (int i = 0; i < 10; i++)
                {
                    bmpList[i] = new Bitmap(filepaths[i]);
                    bmpDataList[i] = bmpList[i].LockBits(new Rectangle(0, 0, bmpList[i].Width, bmpList[i].Height), ImageLockMode.ReadWrite, bmpList[i].PixelFormat);
                    ImageList[i].imgdata = bmpDataList[i].Scan0;
                    ImageList[i].nWidth = bmpDataList[i].Stride;
                    ImageList[i].nHeight = bmpDataList[i].Height;
                }

                bool bResult = PhaseRun.GetProjectHomm(ref ImageList[0], 10);

                return bResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                for (int i = 0; i < 10; i++)
                {
                    bmpList[i].UnlockBits(bmpDataList[i]);
                }
            }
        }

        public bool SaveXMLData(string xmlFilePath)
        {
            byte[] pPath = System.Text.Encoding.Default.GetBytes(xmlFilePath);
            return PhaseRun.SaveParaToXML(pPath);
        }

        public bool ReadXMLData(string xmlFilePath)
        {
            byte[] pPath = System.Text.Encoding.Default.GetBytes(xmlFilePath);
            int nReturn = PhaseRun.GetParaByPath(pPath);
            if (nReturn == 1)
            {
                throw new Exception("标准镜面数据读取失败！");
            }
            else if (nReturn == 2)
            {
                throw new Exception("读取参数失败！");
            }
            return true;
        }

        public XD_PhaseDeflectionPara GetXMLData()
        {
            return PhaseRun.m_Para;
        }

        public bool SetXMLData(XD_PhaseDeflectionPara data)
        {
            PhaseRun.m_Para = data;
            return true;
        }

        public override object Clone(bool containsImage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 图像类型的枚举转string
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string EHalconImageDataType2String(EHalconImageDataType dataType)
        {
            switch (dataType)
            {
                case EHalconImageDataType.halcon_byte:
                    return "byte";
                case EHalconImageDataType.halcon_complex:
                    return "complex";
                case EHalconImageDataType.halcon_cyclic:
                    return "cyclic";
                case EHalconImageDataType.halcon_direction:
                    return "direction";
                case EHalconImageDataType.halcon_int1:
                    return "int1";
                case EHalconImageDataType.halcon_int2:
                    return "int2";
                case EHalconImageDataType.halcon_int4:
                    return "int4";
                case EHalconImageDataType.halcon_int8:
                    return "int8";
                case EHalconImageDataType.halcon_real:
                    return "real";
                case EHalconImageDataType.halcon_uint2:
                    return "uint2";
                case EHalconImageDataType.halcon_vector_field_absolute:
                    return "vector_field_absolute";
                case EHalconImageDataType.halcon_vector_field_relative:
                    return "vector_field_relative";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            
        }
    }
}
