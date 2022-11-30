using GL.Kit.Log;
using GL.Kit.Serialization;
using LeadHawkCS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconSDK.Calib.CalibUI
{
    public partial class FrmMirror : Form
    {
        readonly IGLog log;
        private string cbNestIndex;
        XD_PhaseDeflectionRun Test2 = new XD_PhaseDeflectionRun();
        static readonly XmlSerializer serializer = new XmlSerializer();

        public FrmMirror(IGLog log, string cbNestIndex)
        {
            InitializeComponent();
            this.log = log;
            //text_MirrorPathbox_1.Text = Utils.text_MirrorPathbox_1Text;
            this.cbNestIndex = cbNestIndex;

            try
            {
                CalibParams calibXmlDataModel = new CalibParams();
                calibXmlDataModel = serializer.Deserialize("CalibFormTest.xml", typeof(CalibParams)) as CalibParams;
                string test1 = calibXmlDataModel.MirrorPath;
                text_MirrorPathbox_1.Text = test1;
            }
            catch
            {

            }

            text_BackupBox.Text = CalibFormTest.calibParams.BackupPath;

        }


        string DefaultDirPath4MirrorParam = "";
        string MirrorParamPath;
        private void btnOpenMirrorPath_1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "选择镜像文件存放目录";

            if (DefaultDirPath4MirrorParam != "")
            {
                //设置此次默认目录为上一次选中目录
                folderDialog.SelectedPath = DefaultDirPath4MirrorParam;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                DefaultDirPath4MirrorParam = folderDialog.SelectedPath;
                text_MirrorPathbox_1.Text = folderDialog.SelectedPath;
                MirrorParamPath = folderDialog.SelectedPath;
                CalibFormTest.calibParams.MirrorPath = MirrorParamPath;
                //MessageBox.Show(folderDialog.SelectedPath);
                //保存参数
                //图像及日志保存路径
                serializer.SerializeToFile<CalibParams>(CalibFormTest.calibParams, "CalibFormTest.xml");
            }

            
        }

        private void btn_absphase_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////
            string sPath;
            if (Utils.text_XMLBox1Text != null)
            {
                sPath = Utils.text_XMLBox1Text;

                //上一次的默认值
                //m_defaultfilePath = sPath;
                byte[] pPath = System.Text.Encoding.Default.GetBytes(sPath);
                int nReturn = Test2.GetParaByPath(pPath);
                if (nReturn == 1)
                {
                    MessageBox.Show("标准镜面数据读取失败！");
                }
                else if (nReturn == 2)
                {
                    MessageBox.Show("读取参数失败！");
                }

                MessageBox.Show("读取参数成功！");
                //double xxx = Test2.m_Para.m_HomScreenData[0];
                //xxx = Test2.m_Para.m_HomScreenData[1];
            }

            //////////////////////////////////////////////////
            //string filepaths;

            FileInfo[] files;

            DirectoryInfo root = new DirectoryInfo(text_MirrorPathbox_1.Text);
            files = root.GetFiles();

            string[] filepaths = new string[10];

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
                return;
            }

            Array.Sort(filepaths);

            XDImage[] ImageList = new XDImage[10];
            Bitmap[] bmpList = new Bitmap[10];
            BitmapData[] bmpDataList = new BitmapData[10];


            for (int i = 0; i < 10; i++)
            {
                bmpList[i] = new Bitmap(filepaths[i]);
                bmpDataList[i] = bmpList[i].LockBits(new Rectangle(0, 0, bmpList[i].Width, bmpList[i].Height), ImageLockMode.ReadWrite, bmpList[i].PixelFormat);
                ImageList[i].imgdata = bmpDataList[i].Scan0;
                ImageList[i].nWidth = bmpDataList[i].Stride;
                ImageList[i].nHeight = bmpDataList[i].Height;
            }


            if (!Test2.GetStdAbsPhase(ref ImageList[0], 10))
            {
                MessageBox.Show("标准镜面数据生成失败！");
            }
            MessageBox.Show("标准镜面数据生成成功！");

            for (int i = 0; i < 10; i++)
            {
                bmpList[i].UnlockBits(bmpDataList[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog op = new FolderBrowserDialog();
            op.Description = "备份XML配置文件路径";
            if (op.ShowDialog() == DialogResult.OK)
            {
                MachineMaster.clibxml.backup_XmlFile_path = op.SelectedPath;
                text_BackupBox.Text = MachineMaster.clibxml.backup_XmlFile_path;

                CalibFormTest.calibParams.BackupPath = op.SelectedPath;
                //MessageBox.Show(folderDialog.SelectedPath);
                //保存参数
                //图像及日志保存路径
                serializer.SerializeToFile<CalibParams>(CalibFormTest.calibParams, "CalibFormTest.xml");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = @".\Data\PhasePara";
            //string BackUpPath = text_BackUpPath.Text + "//" + DateTime.Now.ToString("yyyyMMddHHmm");
            string BackUpPath = text_BackupBox.Text + "//" + DateTime.Now.ToString("yyyyMMddHHmm");
            string BackUpPath_1 = BackUpPath + "//cam1//white";
            string BackUpPath_2 = BackUpPath + "//cam2//white";
            string BackUpPath_3 = BackUpPath + "//cam1//blue";
            string BackUpPath_4 = BackUpPath + "//cam2//blue";

            string[] filesName = new string[] { "absPhaseStdCol.tif", "absPhaseStdRow.tif", "Para.xml" };
            try
            {
                if (!Directory.Exists(BackUpPath_1) && !Directory.Exists(BackUpPath_2) && !Directory.Exists(BackUpPath_3) && !Directory.Exists(BackUpPath_4))
                {
                    Directory.CreateDirectory(BackUpPath_1);
                    Directory.CreateDirectory(BackUpPath_2);
                    Directory.CreateDirectory(BackUpPath_3);
                    Directory.CreateDirectory(BackUpPath_4);
                }
                for (int i = 0; i < filesName.Length; i++)
                {
                    File.Copy(path + "//cam1" + "//white//" + filesName[i], BackUpPath_1 + "//" + filesName[i]);
                    File.Copy(path + "//cam2" + "//white//" + filesName[i], BackUpPath_2 + "//" + filesName[i]);
                    File.Copy(path + "//cam1" + "//blue//" + filesName[i], BackUpPath_3 + "//" + filesName[i]);
                    File.Copy(path + "//cam2" + "//blue//" + filesName[i], BackUpPath_4 + "//" + filesName[i]);
                }

                MessageBox.Show($"备份矩阵成功，备份路径为{BackUpPath}");
            }
            catch (Exception)
            {
                MessageBox.Show($"备份矩阵失败，备份路径为{BackUpPath}");
            }
        }
    }
}
