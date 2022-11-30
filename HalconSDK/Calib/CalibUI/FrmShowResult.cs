using GL.Kit.Log;
using GL.Kit.Serialization;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconSDK.Calib.CalibUI
{
    public partial class FrmShowResult : Form
    {
        readonly IGLog log;
        Utils utils;
        private string paramPath;
        static readonly XmlSerializer serializer = new XmlSerializer();

        public FrmShowResult(IGLog log)
        {
            InitializeComponent();
            this.log = log;
            utils = new Utils(log);
        }

        string DefaultDirPath = "";
        string SingleMaterialPath;


        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "选择单料文件存放目录";
            // 算法参数配置
            //utils.initParam("C:/Users/29939/Desktop/当前项目文件/代码资源/para");
            //utils.initMirror("C:/Users/29939/Desktop/当前项目文件/图像资源/0713 Blue/mirror");

            if (DefaultDirPath != "")
            {
                //设置此次默认目录为上一次选中目录
                folderDialog.SelectedPath = DefaultDirPath;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                DefaultDirPath = folderDialog.SelectedPath;
                textBox1.Text = folderDialog.SelectedPath;
                SingleMaterialPath = folderDialog.SelectedPath;
                CalibFormTest.calibParams.SingleMaterialPath = SingleMaterialPath;
                // MessageBox.Show(folderDialog.SelectedPath)
                // 保存参数
                // 图像及日志保存路径
                serializer.SerializeToFile<CalibParams>(CalibFormTest.calibParams, "CalibFormTest.xml");
            }

            switch (Utils.cbNestIndexText + Utils.productColor1Text)
            {
                case "1穴蓝色":
                    utils.initParam("./Data/PhasePara/cam1/blue");
                    paramPath = "./Data/PhasePara/cam1/blue";
                    break;
                case "2穴蓝色":
                    utils.initParam("./Data/PhasePara/cam2/blue");
                    paramPath = "./Data/PhasePara/cam2/blue";
                    break;
                case "1穴白色":
                    utils.initParam("./Data/PhasePara/cam1/white");
                    paramPath = "./Data/PhasePara/cam1/white";
                    break;
                case "2穴白色":
                    utils.initParam("./Data/PhasePara/cam2/white");
                    paramPath = "./Data/PhasePara/cam2/white";
                    break;
                default:
                    utils.initParam("./Data/PhasePara/cam1/blue");
                    paramPath = "./Data/PhasePara/cam1/blue";
                    break;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MiddleImages middleImages = null;
                //utils.GetMiddleRes(source_path, out ho_ShowMask, out ho_ShowGradX, out ho_ShowGradY, out ho_ShowCloudZ); // 返回中间结果
                utils.GetMiddleResNew2(textBox1.Text, paramPath, out middleImages);
                
                HOperatorSet.DispObj(middleImages.Mask, hWindowControl1.HalconWindow);
            }
            catch
            {
                MessageBox.Show(Utils.cbNestIndexText + "产品生成参数有错误！");
            }
        }

        private void FrmShowResult_Load(object sender, EventArgs e)
        {

        }

    }
}
