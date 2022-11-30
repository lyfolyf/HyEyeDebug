using GL.Kit.Log;
using GL.Kit.Serialization;
using HalconDotNet;
using HalconSDK.Calib.CalibUI;
using HyVision.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconSDK.Calib
{
    //public partial class CalibFormTest : Form
    public partial class CalibFormTest : BaseHyUserToolEdit<CalibFormTestBL>
    {
        //Caves cave1 = new Caves() {
        //    CaveId = 1,
        //    CaveName = "1穴"
        //};

        //Caves cave2 = new Caves()
        //{
        //    CaveId = 2,
        //    CaveName = "2穴"
        //};

        //Colors color1 = new Colors()
        //{
        //    ColorId = 1,
        //    ColorName = "blue"
        //};
        //Colors color2 = new Colors()
        //{
        //    ColorId = 2,
        //    ColorName = "white"
        //};

        CalibFormTestBL calibFormBL;
        static readonly XmlSerializer serializer = new XmlSerializer();
        LogPublisher log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

        public CalibFormTest()
        {
            InitializeComponent();
            OpenForm(new FrmCheckImg(log));
            btn_checkImg1.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;


            // 穴号和颜色
            Utils.cbNestIndexText = cbNestIndex.Text;
            Utils.productColor1Text = productColor1.Text;
            //List<Colors> listColors = new List<Colors>() { color1, color2 };
            //List<Caves> listCaves = new List<Caves>() { cave1, cave2 };
            //cbNestIndex.DataSource = listCaves;
            //cbNestIndex.DisplayMember = "CaveName";
            //cbNestIndex.ValueMember = "CaveId";
            //productColor1.DataSource = listColors;
            //productColor1.DisplayMember = "ColorName";
            //productColor1.ValueMember = "ColorId";

            // 初始化标定参数
            try
            {
                calibParams = serializer.Deserialize("CalibFormTest.xml", typeof(CalibParams)) as CalibParams;
            }
            catch
            {

            }
        }

        public static CalibParams calibParams = new CalibParams();//图像及日志保存路径



        public override void UpdateDataToObject()
        {
            
        }

        public override void Save()
        {
            
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override CalibFormTestBL Subject
        {
            get { return calibFormBL; }
            set
            {
                if (!object.Equals(calibFormBL, value))
                {
                    calibFormBL = value;

                }
            }
        }



        /// <summary>
        /// 窗体嵌入通用方法
        /// </summary>
        /// <param name="subForm"></param>
        private void OpenForm(Form subForm)
        {
            foreach (Control item in this.panel1.Controls)
            {
                if (item is Form)
                {
                    ((Form)item).Close();
                }
            }
            subForm.TopLevel = false;// 将子窗体设置为非顶级控件
            subForm.FormBorderStyle = FormBorderStyle.None;//设置无边框
            subForm.Parent = this.panel1;//设置窗体容器
            subForm.Dock = DockStyle.Fill; //容器大小随着调整窗体大小自动变化
            subForm.Show();
        }

        private void btnGamma_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmGamma(log));
            btnGamma.BackColor = Color.Yellow;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;
            btn_checkImg1.BackColor= Color.DarkGray;
        }


        private void btn_checkImg1_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmCheckImg(log));
            btn_checkImg1.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmProductColorParam(log));
            button2.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;
            btn_checkImg1.BackColor = Color.DarkGray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmCamInnerParam(log));
            button3.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;
            btn_checkImg1.BackColor = Color.DarkGray;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmMirror(log,cbNestIndex.Text));
            button4.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;
            btn_checkImg1.BackColor = Color.DarkGray;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmParamXml(log,cbNestIndex.Text));
            button5.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button6.BackColor = Color.DarkGray;
            btn_checkImg1.BackColor = Color.DarkGray;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmShowResult(log));
            button6.BackColor = Color.Yellow;
            btnGamma.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.DarkGray;
            btn_checkImg1.BackColor = Color.DarkGray;
        }


        private void cbNestIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 当前穴号
            Utils.cbNestIndexText = cbNestIndex.Text;
            // 更新当前界面
            foreach (Control item in this.panel1.Controls)
            {
                if (item.Name == "FrmParamXml")
                {
                    OpenForm(new FrmParamXml(log,cbNestIndex.Text));
                }
                if (item.Name == "FrmProductColorParam")
                {
                    OpenForm(new FrmProductColorParam(log));
                }
            }
       }

        private void productColor1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 设置当前颜色
            Utils.productColor1Text = productColor1.Text;
            //MessageBox.Show(this.productColor1.SelectedValue.ToString());
            
            // 更新当前界面
            foreach (Control item in this.panel1.Controls)
            {

                if (item.Name == "FrmParamXml")
                {
                    OpenForm(new FrmParamXml(log,cbNestIndex.Text));
                }
                if (item.Name == "FrmProductColorParam")
                {
                    OpenForm(new FrmProductColorParam(log));
                }
            }
        }

        private void CalibFormTest_Load(object sender, EventArgs e)
        {

        }
    }
}
