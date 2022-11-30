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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconSDK.Calib.CalibUI
{
    public partial class FrmCamInnerParam : Form
    {
        readonly IGLog log;
        static readonly XmlSerializer serializer = new XmlSerializer();

        public FrmCamInnerParam(IGLog log)
        {
            InitializeComponent();
            this.log = log;
            try
            {
                CalibParams calibXmlDataModel = new CalibParams();
                calibXmlDataModel = serializer.Deserialize("CalibFormTest.xml", typeof(CalibParams)) as CalibParams;
                string test1 = calibXmlDataModel.InnerParamPath;
                text_InnerImgPath_1.Text = test1;
            }
            catch
            {

            }
        }


        string DefaultDirPath4InnerParam = "";
        string InnerParamPath;

        private void btnOpenInnerImgPath_1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "选择内参文件存放目录";
            // 算法参数配置
            //utils.initParam("C:/Users/29939/Desktop/当前项目文件/代码资源/para");
            //utils.initMirror("C:/Users/29939/Desktop/当前项目文件/图像资源/0713 Blue/mirror");

            if (DefaultDirPath4InnerParam != "")
            {
                //设置此次默认目录为上一次选中目录
                folderDialog.SelectedPath = DefaultDirPath4InnerParam;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                DefaultDirPath4InnerParam = folderDialog.SelectedPath;
                text_InnerImgPath_1.Text = folderDialog.SelectedPath;
                InnerParamPath = folderDialog.SelectedPath;
                CalibFormTest.calibParams.InnerParamPath = InnerParamPath;
                //MessageBox.Show(folderDialog.SelectedPath);
                //保存参数
                //图像及日志保存路径
                serializer.SerializeToFile<CalibParams>(CalibFormTest.calibParams, "CalibFormTest.xml");
            }

            //System.Windows.Forms.FolderBrowserDialog op = new System.Windows.Forms.FolderBrowserDialog();
            //op.Description = "选择标定图像存放目录";
            ////OpenFileDialog op = new OpenFileDialog();
            ////op.ShowDialog();
            //if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    try
            //    {
            //        switch (Utils.cbNestIndexText)
            //        {
            //            case "1穴":
            //                MachineMaster.clibxml.innerImg_path = op.SelectedPath;
            //                text_InnerImgPath_1.Text = MachineMaster.clibxml.innerImg_path;
            //                MachineMaster.tsk.UpdateLog(cbNestIndex + "相机标定图像路径设置成功！");
            //                break;
            //            case "2穴":
            //                //MachineMaster.clibxml.innerImg_path = op.FileName;
            //                MachineMaster.clibxml.innerImg_path = op.SelectedPath;
            //                text_InnerImgPath_1.Text = MachineMaster.clibxml.innerImg_path;
            //                MachineMaster.tsk.UpdateLog(cbNestIndex + "相机标定图像路径设置成功！");
            //                break;
            //            default:
            //                MachineMaster.clibxml.innerImg_path = op.SelectedPath;
            //                text_InnerImgPath_1.Text = MachineMaster.clibxml.innerImg_path;
            //                MachineMaster.tsk.UpdateLog("1穴相机标定图像路径设置成功！");
            //                break;
            //        }
            //    }
            //    catch
            //    {
            //        MachineMaster.tsk.UpdateLog("没有打开正确的图像路径或文件！");
            //    }


            //}
            //else
            //{
            //    MachineMaster.tsk.UpdateLog("没有打开正确的路径！");
            //}
        }

        private void GenCalibFiles_1_Click(object sender, EventArgs e)
        {

            //确定std图像和screen图像。
            //MachineMaster.clibxml.pathStd = text_InnerImgPath_1.Text + "\pathStd";
            MachineMaster.clibxml.stdscreen_path(text_InnerImgPath_1.Text, out HTuple pathStd, out HTuple pathScreen);

            MachineMaster.clibxml.Count_Num(pathStd, out HTuple hv_stdfiles, out HTuple Num_std);
            MachineMaster.clibxml.Count_Num(pathScreen, out HTuple hv_screenfiles, out HTuple Num_screen);

            //if (Num_std!=1 && Num_screen!=4)
            if (true)
            {
                //HTuple hv_index = null;
                //HObject ExpTmpOutVar_0 = null;
                try
                {

                    #region
                    //图像对象列表中提取图像对象并联合成图像数组

                    //HOperatorSet.GenEmptyObj(out MachineMaster.halcon.ho_ListImage1);

                    //HOperatorSet.GenEmptyObj(out ExpTmpOutVar_0);

                    //HTuple end_val10 = MachineMaster.LstFoldImage_1.Count;
                    //HTuple step_val10 = 1;

                    //for (hv_index = 1; hv_index.Continue(end_val10, step_val10); hv_index = hv_index.TupleAdd(step_val10))
                    //{
                    //    //HObject TempImg = LstImage_1.Find(x => x.Index == hv_index.I).Image;
                    //    HOperatorSet.ConcatObj(MachineMaster.halcon.ho_ListImage1, MachineMaster.LstFoldImage_1.Find(x => x.Index == hv_index.I).Image, out ExpTmpOutVar_0);
                    //    MachineMaster.halcon.ho_ListImage1.Dispose();
                    //    MachineMaster.halcon.ho_ListImage1 = ExpTmpOutVar_0;
                    //}
                    //生成拼接和计算矩阵
                    //MachineMaster.halcon.GenStitchMathTuple(MachineMaster.halcon.strStitchImgPath_1, out MachineMaster.halcon.hv_MathSquare1, out MachineMaster.halcon.hv_StitchSquare1);
                    //999
                    ///2021-08-30     
                    ///
                    #endregion

                    double Raw_Width = Convert.ToDouble(Utils.widRate.Split('/')[0]); //实际尺寸
                    double Raw_Height = Convert.ToDouble(Utils.heightRate.Split('/')[0]);//实际尺寸
                    double pix_Width = Convert.ToDouble(Utils.widRate.Split('/')[1]); //像素宽度
                    double pix_Height = Convert.ToDouble(Utils.heightRate.Split('/')[1]); //像素高度

                    double Wid_Rate = Raw_Width / pix_Width;
                    double Height_Rate = Raw_Height / pix_Height;

                    //double Wid_Rate = Convert.ToDouble(widRate.Text);
                    //double Height_Rate = Convert.ToDouble(heightRate.Text);

                    MachineMaster.clibxml.innerImg_path = text_InnerImgPath_1.Text;
                    //MachineMaster.clibxml.clib_Matrix_generated(MachineMaster.clibxml.innerImg_path, Wid_Rate, Height_Rate, out MachineMaster.clibxml.HomMat3DObjInCamera, out MachineMaster.clibxml.HomMat3Dscreen);

                    MachineMaster.clibxml.clib_Matrix_generated(MachineMaster.clibxml.innerImg_path, Wid_Rate, Height_Rate, out MachineMaster.clibxml.HomMat3DObjInCamera, out MachineMaster.clibxml.HomMat3Dscreen,
                        out MachineMaster.clibxml.HomTemp, out MachineMaster.clibxml.pixelSizeX, out MachineMaster.clibxml.pixelSizeY);

                    if (MachineMaster.clibxml.HomMat3DObjInCamera.TupleLength() == 12 && MachineMaster.clibxml.HomMat3Dscreen.TupleLength() == 12 && MachineMaster.clibxml.HomTemp.TupleLength()==9 && MachineMaster.clibxml.pixelSizeX!=null &&  MachineMaster.clibxml.pixelSizeY !=null)
                    {
                        Utils.IfTupleGenerated = true;

                        this.dgv_ShowMatrix.Rows.Add(11);

                        this.dgv_ShowMatrix.Rows[0].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[0].D.ToString();
                        this.dgv_ShowMatrix.Rows[1].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[1].D.ToString();
                        this.dgv_ShowMatrix.Rows[2].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[2].D.ToString();
                        this.dgv_ShowMatrix.Rows[3].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[3].D.ToString();
                        this.dgv_ShowMatrix.Rows[4].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[4].D.ToString();
                        this.dgv_ShowMatrix.Rows[5].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[5].D.ToString();
                        this.dgv_ShowMatrix.Rows[6].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[6].D.ToString();
                        this.dgv_ShowMatrix.Rows[7].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[7].D.ToString();
                        this.dgv_ShowMatrix.Rows[8].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[8].D.ToString();
                        this.dgv_ShowMatrix.Rows[9].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[9].D.ToString();
                        this.dgv_ShowMatrix.Rows[10].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[10].D.ToString();
                        this.dgv_ShowMatrix.Rows[11].Cells[0].Value = MachineMaster.clibxml.HomMat3DObjInCamera[11].D.ToString();

                        this.dgv_ShowMatrix.Rows[0].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[0].D.ToString();
                        this.dgv_ShowMatrix.Rows[1].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[1].D.ToString();
                        this.dgv_ShowMatrix.Rows[2].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[2].D.ToString();
                        this.dgv_ShowMatrix.Rows[3].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[3].D.ToString();
                        this.dgv_ShowMatrix.Rows[4].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[4].D.ToString();
                        this.dgv_ShowMatrix.Rows[5].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[5].D.ToString();
                        this.dgv_ShowMatrix.Rows[6].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[6].D.ToString();
                        this.dgv_ShowMatrix.Rows[7].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[7].D.ToString();
                        this.dgv_ShowMatrix.Rows[8].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[8].D.ToString();
                        this.dgv_ShowMatrix.Rows[9].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[9].D.ToString();
                        this.dgv_ShowMatrix.Rows[10].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[10].D.ToString();
                        this.dgv_ShowMatrix.Rows[11].Cells[1].Value = MachineMaster.clibxml.HomMat3Dscreen[11].D.ToString();


                        this.dgv_ShowMatrix.Rows[0].Cells[2].Value = MachineMaster.clibxml.HomTemp[0].D.ToString();
                        this.dgv_ShowMatrix.Rows[1].Cells[2].Value = MachineMaster.clibxml.HomTemp[1].D.ToString();
                        this.dgv_ShowMatrix.Rows[2].Cells[2].Value = MachineMaster.clibxml.HomTemp[2].D.ToString();
                        this.dgv_ShowMatrix.Rows[3].Cells[2].Value = MachineMaster.clibxml.HomTemp[3].D.ToString();
                        this.dgv_ShowMatrix.Rows[4].Cells[2].Value = MachineMaster.clibxml.HomTemp[4].D.ToString();
                        this.dgv_ShowMatrix.Rows[5].Cells[2].Value = MachineMaster.clibxml.HomTemp[5].D.ToString();
                        this.dgv_ShowMatrix.Rows[6].Cells[2].Value = MachineMaster.clibxml.HomTemp[6].D.ToString();
                        this.dgv_ShowMatrix.Rows[7].Cells[2].Value = MachineMaster.clibxml.HomTemp[7].D.ToString();
                        this.dgv_ShowMatrix.Rows[8].Cells[2].Value = MachineMaster.clibxml.HomTemp[8].D.ToString();
                        this.dgv_ShowMatrix.Rows[9].Cells[2].Value = MachineMaster.clibxml.pixelSizeX.D.ToString();
                        this.dgv_ShowMatrix.Rows[10].Cells[2].Value = MachineMaster.clibxml.pixelSizeY.D.ToString();


                        MessageBox.Show(Utils.cbNestIndexText + "计算矩阵生成成功！");
                    }
                    else
                    {
                        Utils.IfTupleGenerated = false;
                        MessageBox.Show(Utils.cbNestIndexText + "计算矩阵生成失败！");
                    }

                }
                catch (Exception exp)
                {
                    Utils.IfTupleGenerated = false;
                    MessageBox.Show(Utils.cbNestIndexText + "计算矩阵生成失败！"+exp.Message);
                }

            }
            else
            {
                MessageBox.Show(Utils.cbNestIndexText + "图像数量错误！");
            }
        }

        private void FrmCamInnerParam_Load(object sender, EventArgs e)
        {

        }
    }
}
