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


    public partial class FrmCheckImg : Form
    {
        readonly IGLog log;
        private HObject ShowImage;
        HTuple hv_WindowHandle;
        string DefaultPathCheckInnerImg = "";

        public FrmCheckImg(IGLog log)
        {
            InitializeComponent();
            this.log = log;
        }

        private void btnOpenInnerImgFold_1_Click(object sender, EventArgs e)
        {

           System.Windows.Forms.FolderBrowserDialog op = new System.Windows.Forms.FolderBrowserDialog();
            op.Description = "选择Inner图像存放目录";
            //OpenFileDialog op = new OpenFileDialog();
            //op.ShowDialog();

            if (DefaultPathCheckInnerImg != "")
            {
                //设置此次默认目录为上一次选中目录
                op.SelectedPath = DefaultPathCheckInnerImg;
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
                            DefaultPathCheckInnerImg = op.SelectedPath;
                            MachineMaster.clibxml.innerImg_path = op.SelectedPath;
                            txt_CheckImgPath.Text = op.SelectedPath;
                            MessageBox.Show(Utils.cbNestIndexText + "Inner图像路径设置成功！");
                                                       
                            break;
                        case "2穴":
                            Utils.cbNestIndexText = "2穴";
                            //MachineMaster.clibxml.innerImg_path = op.FileName;
                            DefaultPathCheckInnerImg = op.SelectedPath;
                            MachineMaster.clibxml.innerImg_path = op.SelectedPath;
                            txt_CheckImgPath.Text = op.SelectedPath;

                            MessageBox.Show(Utils.cbNestIndexText + "Inner图像路径设置成功！");
                            break;
                        default:
                            // 默认一穴
                            DefaultPathCheckInnerImg = op.SelectedPath;
                            MachineMaster.clibxml.innerImg_path = op.SelectedPath;
                            txt_CheckImgPath.Text = op.SelectedPath;

                            MessageBox.Show("1穴相机Inner图像路径设置成功！！");
                            break;
                    }

                    //MachineMaster.clibxml.gamma_path= op.SelectedPath; 
                    //text_gammaPath_1.Text = MachineMaster.clibxml.gamma_path;
                    //MachineMaster.tsk.UpdateLog("gamma校正图像已加载！");
                }
                catch
                {
                    MessageBox.Show("没有打开正确的Inner图像路径或文件！");
                }
            }
            else
            {
                MessageBox.Show("没有打开正确的路径！");
            }
        }

        private void btn_CheckImg_Click(object sender, EventArgs e)
        {
            string NG_Img;
            //txt_CheckImg_result.Text = null;
            

            //HTuple hv_Width1, hv_Height1;
            HTuple hv_WindowHandle = hWindowControl2.HalconWindow;
            //HOperatorSet.GetImageSize(CurvatureMax, out hv_Width1, out hv_Height1);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, 2048, 2448);
            //HOperatorSet.DispObj(CurvatureMax, hv_WindowHandle);
            HOperatorSet.ClearWindow(hv_WindowHandle);

            try
            {
                if (txt_Singe_Img.Text!= "")
                {
                    string Index;                    
                    //int Index;
                    //Index = Convert.ToInt32(txt_Singe_Img.Text);
                    Index = txt_Singe_Img.Text;
                    MachineMaster.clibxml.check_Singleimg(out HObject MirrorImg, out MachineMaster.clibxml.Indextxt, MachineMaster.clibxml.innerImg_path, Index);
                    if(MachineMaster.clibxml.Indextxt == null)
                    {
                        
                        HOperatorSet.DispObj(MirrorImg, hv_WindowHandle);
                        this.ShowImage = MirrorImg;
                    }
                    else
                    {
                        MessageBox.Show("图像Index设置错误");
                    }
                    
                }
                else
                {
                    switch (Utils.cbNestIndexText)
                    {
                        case "1穴":

                            //MachineMaster.clibxml.check_img(MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            MachineMaster.clibxml.check_img_caltab(hv_WindowHandle, MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            //MachineMaster.clibxml.check_img_marks(hv_WindowHandle, MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);

                            if (MachineMaster.clibxml.ng_img != null)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, "NG 图像为：" + MachineMaster.clibxml.ng_img,
                                        "window", "top", "left", "red", (new HTuple("box_color")).TupleConcat(
                                        "shadow"), (new HTuple("white")).TupleConcat("false"));
                                }
                            }
                            NG_Img = MachineMaster.clibxml.ng_img.ToString();
                            txt_CheckImg_result.Text = "1穴 NG图像为：" + "\r\n" + "NG Image = " + NG_Img + ";";
                            MessageBox.Show(Utils.cbNestIndexText + "检测完成！");
                            break;
                        case "2穴":
                            //MachineMaster.clibxml.innerImg_path = op.FileName;

                            //MachineMaster.clibxml.check_img(MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            MachineMaster.clibxml.check_img_caltab(hv_WindowHandle, MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            //MachineMaster.clibxml.check_img_marks(hv_WindowHandle, MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            if (MachineMaster.clibxml.ng_img != null)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, "NG 图像为：" + MachineMaster.clibxml.ng_img,
                                        "window", "top", "left", "red", (new HTuple("box_color")).TupleConcat(
                                        "shadow"), (new HTuple("white")).TupleConcat("false"));
                                }
                            }
                            NG_Img = MachineMaster.clibxml.ng_img.ToString();
                            txt_CheckImg_result.Text = "2穴 NG图像为：" + "\r\n" + "NG Image = " + NG_Img + ";";
                            MessageBox.Show(Utils.cbNestIndexText + "检测完成！");
                            break;
                        default:
                            //MachineMaster.clibxml.check_img(MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            MachineMaster.clibxml.check_img_caltab(hv_WindowHandle, MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            //MachineMaster.clibxml.check_img_marks(hv_WindowHandle, MachineMaster.clibxml.innerImg_path, out MachineMaster.clibxml.ng_img);
                            if (MachineMaster.clibxml.ng_img != null)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, "NG 图像为：" + MachineMaster.clibxml.ng_img,
                                        "window", "top", "left", "red", (new HTuple("box_color")).TupleConcat(
                                        "shadow"), (new HTuple("white")).TupleConcat("false"));
                                }
                            }

                            NG_Img = MachineMaster.clibxml.ng_img.ToString();
                            txt_CheckImg_result.Text = "1穴 NG图像为：\n" + "\r\n" + "NG Image = " + NG_Img + "; ";
                            MessageBox.Show("1穴相机图像检测完成！！");
                            break;
                    }

                }
            }
            catch
            {

                MessageBox.Show(Utils.cbNestIndexText + "图像检测失败！");
            }
        }

        private void FrmCheckImg_Load(object sender, EventArgs e)
        {

        }

        private void hWindowControl2_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                HTuple Zoom, Row, Col, Button;
                HTuple Row0, Column0, Row00, Column00, Ht, Wt, r1, c1, r2, c2;
                if (e.Delta > 0)
                {
                    Zoom = 1.5;
                }
                else
                {
                    Zoom = 0.5;
                }
                HOperatorSet.GetMposition(hv_WindowHandle, out Row, out Col, out Button);
                HOperatorSet.GetPart(hv_WindowHandle, out Row0, out Column0, out Row00, out Column00);
                Ht = Row00 - Row0;
                Wt = Column00 - Column0;
                if (Ht * Wt < 32000 * 32000 || Zoom == 1.5)//普通版halcon能处理的图像最大尺寸是32K*32K。如果无限缩小原图像，导致显示的图像超出限制，则会造成程序崩溃
                {
                    r1 = (Row0 + ((1 - (1.0 / Zoom)) * (Row - Row0)));
                    c1 = (Column0 + ((1 - (1.0 / Zoom)) * (Col - Column0)));
                    r2 = r1 + (Ht / Zoom);
                    c2 = c1 + (Wt / Zoom);
                    HOperatorSet.SetPart(hv_WindowHandle, r1, c1, r2, c2);
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ShowImage, hv_WindowHandle);
                }
            }
            catch
            {

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
