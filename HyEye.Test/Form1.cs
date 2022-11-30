using GL.Kit.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.Test
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        XmlSerializer serializer = new XmlSerializer();

        TestConfig config;

        SimulationStation simulation;

        private void Form1_Load(object sender, EventArgs e)
        {
            config = serializer.DeserializeFromFile<TestConfig>(AppDomain.CurrentDomain.BaseDirectory + "test.config");

            if (config == null)
            {
                config = new TestConfig();
            }

            tbServerIP.Text = config.NetworkInfo.IP;
            tbPort.Text = config.NetworkInfo.Port.ToString();

            tbCount.Text = config.Count.ToString();
            tbInterval.Text = config.Interval.ToString();
            rtbCmds.Lines = config.Commands;

            simulation = new SimulationStation();
            simulation.Sended += Simulation_Sended;

            ucProcessLog frmpl = new ucProcessLog();
            frmpl.Dock = DockStyle.Fill;
            tabPage3.Controls.Add(frmpl);

            ucLicense frmlic = new ucLicense();
            frmlic.Dock = DockStyle.Fill;
            tabPage4.Controls.Add(frmlic);

        }

        // 发送指令条数
        int count = 0;

        private void Simulation_Sended()
        {
            statusStrip1.AsyncAction(c =>
            {
                count++;

                lblMsgCount.Text = "指令发送条数：" + count.ToString();
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            getConfig();

            serializer.SerializeToFile(config, AppDomain.CurrentDomain.BaseDirectory + "test.config");
        }

        void getConfig()
        {
            config.NetworkInfo.IP = tbServerIP.Text;
            config.NetworkInfo.Port = int.Parse(tbPort.Text);

            config.Count = int.Parse(tbCount.Text);
            config.Interval = int.Parse(tbInterval.Text);

            config.Commands = rtbCmds.Lines.Where(a => a.Length > 0).ToArray();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            getConfig();

            count = 0;

            try
            {
                Task task = simulation.Start(config);

                btnStart.Enabled = false;
                btnStop.Enabled = true;

                await task;

                simulation.Stop();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBoxUtils.ShowError(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            simulation.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            string[] cmds = rtbSrc.Text.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).OrderBy(a => a).ToArray();

            rtbDest.Lines = cmds;
            lblCmdCount.Text = cmds.Length.ToString();
        }
    }

}
