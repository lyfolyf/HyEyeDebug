using GL.Kit.Log;
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
using System.Xml;

namespace HalconSDK.Calib.CalibUI
{
    public partial class FrmProductColorParam : Form
    {
        readonly IGLog log;

        /// <summary>
        /// 从对应的XML中读取对应参数，在赋值到窗口显示
        /// </summary>
        public FrmProductColorParam(IGLog log)
        {
            InitializeComponent();
            this.log = log;
            // 读取Para.xml
            switch (Utils.cbNestIndexText + Utils.productColor1Text)
            {
                case "1穴蓝色":
                    ShowValue("./Data/PhasePara/cam1/blue/Para.xml");
                    break;
                case "2穴蓝色":
                    ShowValue("./Data/PhasePara/cam2/blue/Para.xml");
                    break;
                case "1穴白色":
                    ShowValue("./Data/PhasePara/cam1/white/Para.xml");
                    break;
                case "2穴白色":
                    ShowValue("./Data/PhasePara/cam2/white/Para.xml");
                    break;
                default:
                    ShowValue("./Data/PhasePara/cam1/blue/Para.xml");
                    break;
            }
        }

        /// <summary>
        /// 查找XML,返回查找结果
        /// </summary>
        /// <param name="ParaPath"></param>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        private List<string> SearchXML(string ParaPath,string NodeName)
        {
            List<string> resList = new List<string>();
            string xmlContent = File.ReadAllText(ParaPath); //"./Data/PhasePara/cam1/Para.xml"
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            XmlElement root_xml = xmlDoc.DocumentElement;
            foreach (XmlNode childNode in root_xml.ChildNodes)
            {
                if (childNode.Name == NodeName)
                {
                    resList.Add(childNode.InnerText);
                }
            }

            return resList;

        }

        /// <summary>
        /// 从Para.XML中读取参数并设置
        /// </summary>
        /// <param name="ParaPath"></param>
        private void ShowValue(string ParaPath)
        {

            Threshstdmax1.Text = SearchXML(ParaPath, "ThreshStdMaxs")[0];
            Threshstdmax2.Text = SearchXML(ParaPath, "ThreshStdMaxs")[1];
            Threshstdmin1.Text = SearchXML(ParaPath, "ThreshStdMins")[0];
            Threshstdmin2.Text = SearchXML(ParaPath, "ThreshStdMins")[1];
            ThreshProductmax1.Text = SearchXML(ParaPath, "ThreshProductMaxs")[0];
            ThreshProductmax2.Text = SearchXML(ParaPath, "ThreshProductMaxs")[1];
            ThreshProductmin1.Text = SearchXML(ParaPath, "ThreshProductMins")[0];
            ThreshProductmin2.Text = SearchXML(ParaPath, "ThreshProductMins")[1];

            //Threshstdmax1.Text = Utils.Threshstdmax1Text;
            //Threshstdmax2.Text = Utils.Threshstdmax2Text;
            //Threshstdmin1.Text = Utils.Threshstdmin1Text;
            //Threshstdmin2.Text = Utils.Threshstdmin2Text;
            //ThreshProductmax1.Text = Utils.ThreshProductmax1Text;
            //ThreshProductmax2.Text = Utils.ThreshProductmax2Text;
            //ThreshProductmin1.Text = Utils.ThreshProductmin1Text;
            //ThreshProductmin2.Text = Utils.ThreshProductmin2Text;
        }


        /// <summary>
        /// 关闭离开界面，保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmProductColorParam_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalibFormTest.calibParams.Threshstdmax1 = Threshstdmax1.Text;
            CalibFormTest.calibParams.Threshstdmax2 = Threshstdmax2.Text;
            CalibFormTest.calibParams.Threshstdmin1 = Threshstdmin1.Text;
            CalibFormTest.calibParams.Threshstdmin2 = Threshstdmin2.Text;
            CalibFormTest.calibParams.ThreshProductmax1 = ThreshProductmax1.Text;
            CalibFormTest.calibParams.ThreshProductmax2 = ThreshProductmax2.Text;
            CalibFormTest.calibParams.ThreshProductmin1 = ThreshProductmin1.Text;
            CalibFormTest.calibParams.ThreshProductmin2 = ThreshProductmin2.Text;
            CalibFormTest.calibParams.widRate = widRate.Text;
            CalibFormTest.calibParams.heightRate = heightRate.Text;
        }

        /// <summary>
        /// 设置分割阈值与长宽比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetMinMax_Click(object sender, EventArgs e)
        {
            CalibFormTest.calibParams.Threshstdmax1 = Threshstdmax1.Text;
            CalibFormTest.calibParams.Threshstdmax2 = Threshstdmax2.Text;
            CalibFormTest.calibParams.Threshstdmin1 = Threshstdmin1.Text;
            CalibFormTest.calibParams.Threshstdmin2 = Threshstdmin2.Text;
            CalibFormTest.calibParams.ThreshProductmax1 = ThreshProductmax1.Text;
            CalibFormTest.calibParams.ThreshProductmax2 = ThreshProductmax2.Text;
            CalibFormTest.calibParams.ThreshProductmin1 = ThreshProductmin1.Text;
            CalibFormTest.calibParams.ThreshProductmin2 = ThreshProductmin2.Text;
            CalibFormTest.calibParams.widRate = widRate.Text;
            CalibFormTest.calibParams.heightRate = heightRate.Text;
        }
    }
}
