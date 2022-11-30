using GL.Kit.Log;
using HalconDotNet;
using LeadHawkCS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HalconSDK.Calib
{
    class Utils
    {
        public static string cbNestIndexText = "1穴";
        public static string widRate = "698.4/1920";
        public static string heightRate = "392.4/1080";
        public static bool IfTupleGenerated = false;
        public static string text_XMLBox1Text;
        public static string productColor1Text = "蓝色";

        XD_PhaseDeflectionRun XD_Algorithm = new XD_PhaseDeflectionRun();

        readonly IGLog log;
        public Utils(IGLog log)
        {
            this.log = log;
        }

        public void initParam(string param_path)
        {

            string sPath = param_path;
            byte[] pPath = System.Text.Encoding.Default.GetBytes(sPath);
            int nReturn = XD_Algorithm.GetParaByPath(pPath);  // 重要API
            if (nReturn == 1)
            {
                MessageBox.Show("标准镜面数据读取失败！");
            }
            else if (nReturn == 2)
            {
                MessageBox.Show("读取参数失败！");
            }

            // MessageBox.Show("读取参数成功！");

        }

        public void GetMiddleResNew2(string source_path, string paramPath, out MiddleImages middleImages)
        {
            string sPath = source_path;  // "C:/Users/29939/Desktop/当前项目文件/图像资源/0713 Blue/31-3";
            DirectoryInfo root = new DirectoryInfo(sPath);
            FileInfo[] files;
            files = root.GetFiles();

            if (files.Length < 10)
            {
                MessageBox.Show("产品数据生成失败！");
                // return;
            }

            string[] filepaths;
            filepaths = new string[10];

            int nBmpNum = 0;

            for (int i = 0; i < files.Length && nBmpNum < 10; i++)
            {
                if (files[i].Extension == ".bmp")
                {
                    filepaths[nBmpNum] = files[i].FullName;
                    nBmpNum++;
                }

            }

            if (nBmpNum != 10)
            {
                //return;
            }

            Array.Sort(filepaths);


            //// 3-6  ==> 7-10
            //filepaths = SwapStringArr(filepaths, 2, 6);
            //filepaths = SwapStringArr(filepaths, 3, 7);
            //filepaths = SwapStringArr(filepaths, 4, 8);
            //filepaths = SwapStringArr(filepaths, 5, 9);

            XDImage[] ImageList = new XDImage[10];
            Bitmap[] bmpList = new Bitmap[10];
            BitmapData[] bmpDataList = new BitmapData[10];


            for (int i = 0; i < 10; i++)
            {
                //if (i >= 2)
                //{
                //    HObject ho_Image, ho_ImageMean;
                //    HOperatorSet.ReadImage(out ho_Image, filepaths[i]);
                //    HOperatorSet.MeanImage(ho_Image, out ho_ImageMean, 1.5, 1.5);
                //    HObject2Bitmap8(ho_ImageMean, out bmpList[i]);
                //}
                //else
                //{
                //    bmpList[i] = new Bitmap(filepaths[i]);
                //}

                bmpList[i] = new Bitmap(filepaths[i]);
                bmpDataList[i] = bmpList[i].LockBits(new Rectangle(0, 0, bmpList[i].Width, bmpList[i].Height), ImageLockMode.ReadWrite, bmpList[i].PixelFormat);
                ImageList[i].imgdata = bmpDataList[i].Scan0;
                ImageList[i].nWidth = bmpDataList[i].Stride;
                ImageList[i].nHeight = bmpDataList[i].Height;
            }


            //IntPtr Mask, GradX, GradY, CloudX, CloudY, CloudZ= IntPtr.Zero;
            //IntPtr Mask, GradX, GradY, CloudX, CloudY, CloudZ =new IntPtr();
            IntPtr Mask = IntPtr.Zero;
            IntPtr GradX = IntPtr.Zero;
            IntPtr GradY = IntPtr.Zero;
            IntPtr CloudX = IntPtr.Zero;
            IntPtr CloudY = IntPtr.Zero;
            IntPtr CloudZ = IntPtr.Zero;

            // 算法处理结束时间
            DateTime ProStartTimeGetGradImageData = DateTime.Now;
            if (!XD_Algorithm.GetGradImageData(ref ImageList[0], 10, ref Mask, ref GradX, ref GradY, ref CloudX, ref CloudY, ref CloudZ))
            {
                MessageBox.Show("产品数据生成失败！");
            }
            DateTime ProEndTimeGetGradImageData = DateTime.Now;
            // 计算的处理时间
            string ProDurationGetGradImageData = (ProEndTimeGetGradImageData - ProStartTimeGetGradImageData).ToString("ssfff");
            log.Info("GetGradImageData算法处理时间:" + (ProEndTimeGetGradImageData - ProStartTimeGetGradImageData).TotalMilliseconds + "ms");

            /***
             * 读取cam2的xml文件进行配置长宽配置
             * ***/
            string xmlContent = File.ReadAllText(paramPath + "/Para.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            XmlElement root_xml = xmlDoc.DocumentElement;
            string WidthOut = root_xml.SelectSingleNode("WidthOut").InnerText;
            string HeightOut = root_xml.SelectSingleNode("HeightOut").InnerText;
            int width = Convert.ToInt32(WidthOut);
            int height = Convert.ToInt32(HeightOut);
            HObject ho_ShowMask = new HObject();
            HObject ho_ShowGradX = new HObject();
            HObject ho_ShowGradY = new HObject();
            HObject ho_ShowCloudZ = new HObject();
            // intPtr转HObject

            HOperatorSet.GenImage1(out ho_ShowMask, "byte", width, height, Mask);
            HOperatorSet.GenImage1(out ho_ShowGradX, "real", width, height, GradX);
            HOperatorSet.GenImage1(out ho_ShowGradY, "real", width, height, GradY);
            HOperatorSet.GenImage1(out ho_ShowCloudZ, "real", width, height, CloudZ);
            middleImages = new MiddleImages()
            {
                Mask = ho_ShowMask,
                GradX = ho_ShowGradX,
                GradY = ho_ShowGradY,
                CloudZ = ho_ShowCloudZ
            };


            // 解锁内存
            for (int i = 0; i < 10; i++)
            {
                bmpList[i].UnlockBits(bmpDataList[i]);
            }
        }

    }
}
