using GL.Kit.Log;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconSDK.Calib.CalibUI
{


    public partial class FrmGamma : Form
    {
        readonly IGLog log;

        public FrmGamma(IGLog log)
        {
            InitializeComponent();
            this.log = log;
        }

        private void btnOpenGammaImgFold_1_Click(object sender, EventArgs e)
        {
            string DefaultDirPathGammaImg = "";

            System.Windows.Forms.FolderBrowserDialog op = new System.Windows.Forms.FolderBrowserDialog();
            op.Description = "选择gamma图像存放目录";
            //OpenFileDialog op = new OpenFileDialog();
            //op.ShowDialog();

            if (DefaultDirPathGammaImg != "")
            {
                //设置此次默认目录为上一次选中目录
                op.SelectedPath = DefaultDirPathGammaImg;
            }

            if (op.ShowDialog() == DialogResult.OK)
            {
                MachineMaster.LstFoldImage_1.Clear();
                try
                {
                    switch (Utils.cbNestIndexText)
                    {
                        case "1穴":
                            Utils.cbNestIndexText = "1穴";
                            DefaultDirPathGammaImg = op.SelectedPath;
                            MachineMaster.clibxml.gamma_path = op.SelectedPath;
                            text_gammaPath_1.Text = MachineMaster.clibxml.gamma_path;
                            MessageBox.Show(Utils.cbNestIndexText + "gamma校正图像路径设置成功！");
                            break;
                        case "2穴":
                            Utils.cbNestIndexText = "2穴";
                            //MachineMaster.clibxml.innerImg_path = op.FileName;
                            DefaultDirPathGammaImg = op.SelectedPath;
                            MachineMaster.clibxml.gamma_path = op.SelectedPath;
                            text_gammaPath_1.Text = MachineMaster.clibxml.gamma_path;
                            MessageBox.Show(Utils.cbNestIndexText + "gamma校正图像路径设置成功！");
                            break;
                        default:
                            // 默认一穴
                            DefaultDirPathGammaImg = op.SelectedPath;
                            MachineMaster.clibxml.gamma_path = op.SelectedPath;
                            text_gammaPath_1.Text = MachineMaster.clibxml.gamma_path;
                            MessageBox.Show("1穴相机gamma校正图像路径设置成功！！");
                            break;
                    }

                    //MachineMaster.clibxml.gamma_path= op.SelectedPath; 
                    //text_gammaPath_1.Text = MachineMaster.clibxml.gamma_path;
                    //MachineMaster.tsk.UpdateLog("gamma校正图像已加载！");
                }
                catch
                {
                    MessageBox.Show("没有打开正确的gamma图像路径或文件！");
                }
            }
            else
            {
                MessageBox.Show("没有打开正确的路径！");
            }
        }

        private void Gamma校正_Click(object sender, EventArgs e)
        {
            string Gamma;
            string Rmse;
            MachineMaster.clibxml.Count_Num(MachineMaster.clibxml.gamma_path, out HTuple hv_stdfiles, out HTuple Num_std);

            if (Num_std = 51)
            {
                try
                {

                    switch (Utils.cbNestIndexText)
                    {
                        case "1穴":

                            MachineMaster.clibxml.adjust_Gamma(MachineMaster.clibxml.gamma_path, out MachineMaster.clibxml.hv_rmse, out MachineMaster.clibxml.hv_gamma);
                            Rmse = MachineMaster.clibxml.hv_rmse.ToString();
                            Gamma = MachineMaster.clibxml.hv_gamma.ToString();


                            text_gamma_result.Text = "1穴 相机Gamma校正结果：\n" + " RMSE = " + Rmse + ";\n" + "Gamma = " + Gamma + ";";
                            MessageBox.Show(Utils.cbNestIndexText + "gamma校正成功！");
                            break;
                        case "2穴":
                            //MachineMaster.clibxml.innerImg_path = op.FileName;

                            MachineMaster.clibxml.adjust_Gamma(MachineMaster.clibxml.gamma_path, out MachineMaster.clibxml.hv_rmse, out MachineMaster.clibxml.hv_gamma);
                            Rmse = MachineMaster.clibxml.hv_rmse.ToString();
                            Gamma = MachineMaster.clibxml.hv_gamma.ToString();

                            text_gamma_result.Text = "2穴 相机Gamma校正结果：\n" + " RMSE = " + Rmse + ";\n" + "Gamma = " + Gamma + ";";

                            MessageBox.Show(Utils.cbNestIndexText + "gamma校正成功！");
                            break;
                        default:
                            MachineMaster.clibxml.adjust_Gamma(MachineMaster.clibxml.gamma_path, out MachineMaster.clibxml.hv_rmse, out MachineMaster.clibxml.hv_gamma);
                            Rmse = MachineMaster.clibxml.hv_rmse.ToString();
                            Gamma = MachineMaster.clibxml.hv_gamma.ToString();

                            text_gamma_result.Text = "1穴 相机Gamma校正结果：\n" + " RMSE = " + Rmse + ";\n" + "Gamma = " + Gamma + ";";
                            MessageBox.Show("1穴相机gamma校正成功！！");
                            break;
                    }


                }

                catch
                {

                    MessageBox.Show(Utils.cbNestIndexText + "计算矩阵生成失败！");
                }

            }
            else

            {
                MessageBox.Show(Utils.cbNestIndexText + "gamma图像数量错误！");
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    TestXmlDataModel testXmlDataModel = new TestXmlDataModel();
        //    testXmlDataModel.Param1 = 101;
        //    testXmlDataModel.Param2 = 202;
        //    testXmlDataModel.Param3 = 303;
        //    XmlSerializerHelper.WriteXML(testXmlDataModel,"testXmlDataModel.xml", typeof(TestXmlDataModel));

        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    TestXmlDataModel testXmlDataModel = new TestXmlDataModel();
        //    testXmlDataModel = XmlSerializerHelper.ReadXML("testXmlDataModel.xml", typeof(TestXmlDataModel)) as TestXmlDataModel;

        //    Double test1 = Convert.ToDouble(testXmlDataModel.Param1);
        //    Double test2 = Convert.ToDouble(testXmlDataModel.Param2);
        //    Double test3 = Convert.ToDouble(testXmlDataModel.Param3);
        //}

    }
}
