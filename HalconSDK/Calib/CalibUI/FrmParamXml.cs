using GL.Kit.Log;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconSDK.Calib.CalibUI
{
    public partial class FrmParamXml : Form
    {

        readonly IGLog log;
        private string cbNestIndex;
        private string productColor;

        public FrmParamXml(IGLog log, string cbNestIndex)
        {
            InitializeComponent();
            this.log = log;
            this.cbNestIndex = cbNestIndex;
            this.productColor = Utils.productColor1Text;


            switch (Utils.cbNestIndexText + Utils.productColor1Text)
            {
                case "1穴蓝色":
                    text_XMLBox1.Text = "./Data/PhasePara/cam1/blue"; //MachineMaster.clibxml.open_XmlFile_path;
                    Utils.text_XMLBox1Text = text_XMLBox1.Text;
                    log.Info(cbNestIndex + "XML配置文件路径设置成功！");
                    break;
                case "2穴蓝色":
                    text_XMLBox1.Text = "./Data/PhasePara/cam2/blue"; //MachineMaster.clibxml.open_XmlFile_path;
                    Utils.text_XMLBox1Text = text_XMLBox1.Text;
                    log.Info(cbNestIndex + "XML配置文件路径设置成功！");
                    break;
                case "1穴白色":
                    text_XMLBox1.Text = "./Data/PhasePara/cam1/white"; //MachineMaster.clibxml.open_XmlFile_path;
                    Utils.text_XMLBox1Text = text_XMLBox1.Text;
                    log.Info(cbNestIndex + "XML配置文件路径设置成功！");
                    break;
                case "2穴白色":
                    text_XMLBox1.Text = "./Data/PhasePara/cam2/white"; //MachineMaster.clibxml.open_XmlFile_path;
                    Utils.text_XMLBox1Text = text_XMLBox1.Text;
                    log.Info(cbNestIndex + "XML配置文件路径设置成功！");
                    break;
                default:
                    text_XMLBox1.Text = "./Data/PhasePara/cam1/blue"; //MachineMaster.clibxml.open_XmlFile_path;
                    Utils.text_XMLBox1Text = text_XMLBox1.Text;
                    log.Info("1穴XML配置文件路径设置成功！");
                    break;
            }

        }

        //public void SetParamXmlPath()
        //{
        //    switch (Utils.cbNestIndexText)
        //    {
        //        case "1穴":
        //            text_XMLBox1.Text = "./Data/PhasePara/cam1"; //MachineMaster.clibxml.open_XmlFile_path;
        //            MachineMaster.tsk.UpdateLog(cbNestIndex + "XML配置文件路径设置成功！");
        //            break;
        //        case "2穴":
        //            text_XMLBox1.Text = "./Data/PhasePara/cam2"; //MachineMaster.clibxml.open_XmlFile_path;
        //            MachineMaster.tsk.UpdateLog(cbNestIndex + "XML配置文件路径设置成功！");
        //            break;
        //        default:
        //            text_XMLBox1.Text = "./Data/PhasePara/cam1"; //MachineMaster.clibxml.open_XmlFile_path;
        //            MachineMaster.tsk.UpdateLog("1穴XML配置文件路径设置成功！");
        //            break;
        //    }
        //}


        private void button2_Click(object sender, EventArgs e)
        {
            // 先读取XML文件，构成对象
            try
            {
                //HOperatorSet.SetColor(m_windowHandle, "red");

                MachineMaster.clibxml.open_XmlFile_path = text_XMLBox1.Text;

                // 
                MachineMaster.clibxml.read_XMLfiles(MachineMaster.clibxml.open_XmlFile_path, out MachineMaster.clibxml.Elements);

                if (MachineMaster.clibxml.Elements.Length == 0)
                {
                    MessageBox.Show("XML文件数据读取失败！");
                    return;
                }
                //IfTupleGeneratedMark = true;

                //MessageBox.Show("XML文件读取完成 ！");
                //MachineMaster.tsk.UpdateLog("XML文件读取完成！");
                this.Enabled = true;//运行完成显示相互对应的窗口
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取XML文件失败");
            }


            //保存XML文件
            if (Utils.IfTupleGenerated)
            {
                try
                {
                    if (CalibFormTest.calibParams.Threshstdmax1 != string.Empty && CalibFormTest.calibParams.Threshstdmax2 != string.Empty && CalibFormTest.calibParams.Threshstdmin1 != string.Empty && CalibFormTest.calibParams.Threshstdmin2 != string.Empty 
                        && CalibFormTest.calibParams.ThreshProductmax1 != string.Empty && CalibFormTest.calibParams.ThreshProductmax2 != string.Empty && CalibFormTest.calibParams.ThreshProductmin1 != string.Empty && CalibFormTest.calibParams.ThreshProductmin2 != string.Empty)
                    {
                        int StdMaxs1 = Convert.ToInt32(CalibFormTest.calibParams.Threshstdmax1);
                        int StdMaxs2 = Convert.ToInt32(CalibFormTest.calibParams.Threshstdmax2);
                        int StdMins1 = Convert.ToInt32(CalibFormTest.calibParams.Threshstdmin1);
                        int StdMins2 = Convert.ToInt32(CalibFormTest.calibParams.Threshstdmin2);
                        int ProductMaxs1 = Convert.ToInt32(CalibFormTest.calibParams.ThreshProductmax1);
                        int ProductMaxs2 = Convert.ToInt32(CalibFormTest.calibParams.ThreshProductmax2);
                        int ProductMins1 = Convert.ToInt32(CalibFormTest.calibParams.ThreshProductmin1);
                        int ProductMins2 = Convert.ToInt32(CalibFormTest.calibParams.ThreshProductmin2);
                        //999
                        double Raw_Width = Convert.ToDouble(CalibFormTest.calibParams.widRate.Split('/')[0]); //实际尺寸
                        double Raw_Height = Convert.ToDouble(CalibFormTest.calibParams.heightRate.Split('/')[0]);//实际尺寸
                        double pix_Width = Convert.ToDouble(CalibFormTest.calibParams.widRate.Split('/')[1]); //像素宽度
                        double pix_Height = Convert.ToDouble(CalibFormTest.calibParams.widRate.Split('/')[1]); //像素高度

                        double Wid_Rate = Raw_Width / pix_Width;
                        double Height_Rate = Raw_Height / pix_Height;

                        MachineMaster.clibxml.StdMaxs[0] = StdMaxs1;
                        MachineMaster.clibxml.StdMaxs[1] = StdMaxs2;
                        MachineMaster.clibxml.StdMins[0] = StdMins1;
                        MachineMaster.clibxml.StdMins[1] = StdMins2;
                        MachineMaster.clibxml.ProductMaxs[0] = ProductMaxs1;
                        MachineMaster.clibxml.ProductMaxs[1] = ProductMaxs2;
                        MachineMaster.clibxml.ProductMins[0] = ProductMins1;
                        MachineMaster.clibxml.ProductMins[1] = ProductMins2;
                        
                        if(MachineMaster.clibxml.HomMat3DObjInCamera.TupleLength()==12  &&  MachineMaster.clibxml.HomMat3Dscreen.TupleLength()==12 && MachineMaster.clibxml.HomTemp.TupleLength() == 9 && MachineMaster.clibxml.pixelSizeX != null && MachineMaster.clibxml.pixelSizeY != null)
                        { 
                        
                            MachineMaster.clibxml.save_XMLfiles(text_XMLBox1.Text, MachineMaster.clibxml.HomMat3DObjInCamera, MachineMaster.clibxml.HomMat3Dscreen, MachineMaster.clibxml.HomTemp,MachineMaster.clibxml.Elements,
                                MachineMaster.clibxml.StdMaxs, MachineMaster.clibxml.StdMins, MachineMaster.clibxml.ProductMaxs, MachineMaster.clibxml.ProductMins, MachineMaster.clibxml.pixelSizeX, MachineMaster.clibxml.pixelSizeY, Wid_Rate, Height_Rate,out MachineMaster.clibxml.hv_value);
                        
                            
                            MessageBox.Show(cbNestIndex + "XML文件保存成功！");
                        }
                        else
                        {
                            MessageBox.Show(cbNestIndex + "矩阵参数错误，XML文件保存失败！");
                        }
                    }
                    else
                        MessageBox.Show(cbNestIndex + "阈值参数未设置！");
                }
                catch
                {
                    MessageBox.Show(cbNestIndex + "XML文件保存失败！");
                }
                finally
                {
                    Utils.IfTupleGenerated = false;
                }

            }
            else
            {
                MessageBox.Show(cbNestIndex + "XML数据保存失败，请先生成矩阵！");
            }
                
            


        }

        HTuple m_windowHandle = null;       //图像显示控件的句柄


   
    }
}
