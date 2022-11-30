using GL.Kit.Log;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LogDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GLogAdapter logAdapter;
        IGLogger fileLog;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            logAdapter = new GLogAdapter("test", PathUtils.CurrentDirectory + "Log", 7, LogFormat.General);
            fileLog = GLogger.CreateLog("test", LogLevel.Debug, logAdapter);
        }

        bool flag = false;
        int i;

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out i))
            {
                MessageBox.Show("间隔输入不正确");
                textBox1.Focus();
                return;
            }

            flag = true;
            Thread thread = new Thread(() => WriteLog());
            thread.Start();

            btnStart.Enabled = false;
            btnClose.Enabled = true;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            flag = false;

            btnStart.Enabled = true;
            btnClose.Enabled = false;
        }

        void WriteLog()
        {
            while (flag)
            {
                fileLog.Info("");
                Thread.Sleep(i);
            }
        }


    }
}
