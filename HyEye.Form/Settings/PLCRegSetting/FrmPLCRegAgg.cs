using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using PlcSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.WForm.Settings.PLCRegSetting
{
    public partial class FrmPLCRegAgg : Form
    {
        readonly IPlcRepository plcRepo;
        readonly ITaskRepository taskRepo;
        readonly ICalibrationRepository calibrationRepo;
        readonly IGLog log;
        readonly ICommunicationRepository communicationRepo;

        public CommunicationInfoVO CommunicationInfo { get; set; }

        public FrmPLCRegAgg(
            ITaskRepository taskRepo,
            IPlcRepository plcRepo,
            ICalibrationRepository calibrationRepo,
            ICommunicationRepository communicationRepo,
            IGLog log)
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.plcRepo = plcRepo;
            this.calibrationRepo = calibrationRepo;
            this.communicationRepo = communicationRepo;
            this.log = log;
        }



        FrmPLCRegSetPage TaskRecv;
        FrmPLCRegSetPage TaskSend;
        FrmPLCRegSetPage CalibRecv;
        FrmPLCRegSetPage CalibSend;

        private void FrmPLCRegAgg_Load(object sender, EventArgs e)
        {
            CommunicationInfo = communicationRepo.GetCommunicationInfo();
            if (CommunicationInfo == null)
            {
                MessageBox.Show("请先设置PLC通讯");
                return;
            }

            tbReadFlagDeviceName.Text = (plcRepo.ReadFlagDeviceName == "0") ? "D0" : plcRepo.ReadFlagDeviceName.ToString();
            tbWriteFlagDeviceName.Text = (plcRepo.WriteFlagDeviceName == "0") ? "D500" : plcRepo.WriteFlagDeviceName.ToString();
            tbStartReadDeviceName.Text = (plcRepo.StartReadDeviceName == "0") ? "D1" : plcRepo.StartReadDeviceName.ToString();
            tbReadLength.Text = plcRepo.ReadLength.ToString();

            if (taskRepo.GetTasks().Count > 0)
            {
                tabPage1.Parent = tabControl1;
                tabPage2.Parent = tabControl1;

                TaskRecv = new FrmPLCRegSetPage(0, CommunicationInfo, taskRepo, plcRepo, log);
                TaskRecv.setCountHandle += SetCount;
                TaskRecv.setHeadHandle += SetHead;
                TaskRecv.Dock = DockStyle.Fill;
                TaskRecv.TopLevel = false;
                TaskRecv.FormBorderStyle = FormBorderStyle.None;
                tabPage1.Controls.Add(TaskRecv);
                TaskRecv.Show();

                TaskSend = new FrmPLCRegSetPage(1, CommunicationInfo, taskRepo, plcRepo, log);
                TaskSend.setCountHandle += SetCount;
                TaskSend.setHeadHandle += SetHead;
                TaskSend.Dock = DockStyle.Fill;
                TaskSend.TopLevel = false;
                TaskSend.FormBorderStyle = FormBorderStyle.None;
                tabPage2.Controls.Add(TaskSend);
                TaskSend.Show();
            }
            else
            {
                tabPage1.Parent = null;
                tabPage2.Parent = null;
            }

            if (calibrationRepo.GetCalibrations().Count > 0)
            {
                tabPage3.Parent = tabControl1;
                tabPage4.Parent = tabControl1;

                CalibRecv = new FrmPLCRegSetPage(2, CommunicationInfo, taskRepo, plcRepo, log);
                CalibRecv.setCountHandle += SetCount;
                CalibRecv.setHeadHandle += SetHead;
                CalibRecv.Dock = DockStyle.Fill;
                CalibRecv.TopLevel = false;
                CalibRecv.FormBorderStyle = FormBorderStyle.None;
                tabPage3.Controls.Add(CalibRecv);
                CalibRecv.Show();

                CalibSend = new FrmPLCRegSetPage(3, CommunicationInfo, taskRepo, plcRepo, log);
                CalibSend.setCountHandle += SetCount;
                CalibSend.setHeadHandle += SetHead;
                CalibSend.Dock = DockStyle.Fill;
                CalibSend.TopLevel = false;
                CalibSend.FormBorderStyle = FormBorderStyle.None;
                tabPage4.Controls.Add(CalibSend);
                CalibSend.Show();
            }
            else
            {
                tabPage3.Parent = null;
                tabPage4.Parent = null;
            }

            int TRec = TaskRecv == null ? 0 : TaskRecv.GetCurrentCount();
            //int TSend = TaskSend == null ? 0 : TaskSend.GetCurrentCount();
            int CRec = CalibRecv == null ? 0 : CalibRecv.GetCurrentCount();
            //int CSend = CalibSend == null ? 0 : CalibSend.GetCurrentCount();

            tbReadLength.Text = (TRec + CRec).ToString();
        }

        private void SetHead(string head, string rId, int modeIndex)
        {
            switch (modeIndex)
            {
                case 0:
                    TaskSend?.ChangeHead(head, rId);
                    break;
                case 1:
                    TaskRecv?.ChangeHead(head, rId);
                    break;
                case 2:
                    CalibSend?.ChangeHead(head, rId);
                    break;
                case 3:
                    CalibRecv?.ChangeHead(head, rId);
                    break;
            }
        }

        private void SetCount()
        {
            int TRec = TaskRecv == null ? 0 : TaskRecv.GetCurrentCount();
            int TSend = TaskSend == null ? 0 : TaskSend.GetCurrentCount();
            int CRec = CalibRecv == null ? 0 : CalibRecv.GetCurrentCount();
            int CSend = CalibSend == null ? 0 : CalibSend.GetCurrentCount();

            tbReadLength.Text = (TRec + TSend + CRec + CSend).ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbReadFlagDeviceName.CheckNotEmpty("请输入读标志位地址")
                  || !tbWriteFlagDeviceName.CheckNotEmpty("请输入写标志位地址")
                  || !tbStartReadDeviceName.CheckNotEmpty("请输入接收指令寄存器起始地址")
                  || !tbReadLength.CheckNotEmpty("请输入接收指令寄存器总长度"))
                return;
            if (tbReadFlagDeviceName.Text.Trim() == tbWriteFlagDeviceName.Text.Trim())
            {
                MessageBox.Show("读标志位地址 与 写标志位地址 不能一样");
                return;
            }


            bool ckTaskRecv = TaskRecv == null ? false : TaskRecv.SaveCheckPath(tbReadFlagDeviceName.Text.Trim(), tbWriteFlagDeviceName.Text.Trim());
            if (ckTaskRecv)
            {
                MessageBox.Show("生产接收指令中，数据地址与标志位地址重复");
                return;
            }
            bool ckTaskSend = TaskSend == null ? false : TaskSend.SaveCheckPath(tbReadFlagDeviceName.Text.Trim(), tbWriteFlagDeviceName.Text.Trim());
            if (ckTaskSend)
            {
                MessageBox.Show("生产发送指令中，数据地址与标志位地址重复");
                return;
            }
            bool ckCalibRecv = CalibRecv == null ? false : CalibRecv.SaveCheckPath(tbReadFlagDeviceName.Text.Trim(), tbWriteFlagDeviceName.Text.Trim());
            if (ckCalibRecv)
            {
                MessageBox.Show("标定接收指令中，数据地址与标志位地址重复");
                return;
            }
            bool ckCalibSend = CalibSend == null ? false : CalibSend.SaveCheckPath(tbReadFlagDeviceName.Text.Trim(), tbWriteFlagDeviceName.Text.Trim());
            if (ckCalibSend)
            {
                MessageBox.Show("标定发送指令中，数据地址与标志位地址重复");
                return;
            }

            TaskRecv?.SetSave();
            TaskSend?.SetSave();
            CalibRecv?.SetSave();
            CalibSend?.SetSave();

            plcRepo.ReadFlagDeviceName = tbReadFlagDeviceName.Text.Trim();
            plcRepo.WriteFlagDeviceName = tbWriteFlagDeviceName.Text.Trim();
            plcRepo.StartReadDeviceName = tbStartReadDeviceName.Text.Trim();
            plcRepo.ReadLength = tbReadLength.IntValue();

            plcRepo.Save();

            //DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSetAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbReadFlagDeviceName.Text))
            {
                MessageBox.Show("读标志位地址未设置");
                return;
            }
            if (string.IsNullOrEmpty(tbWriteFlagDeviceName.Text))
            {
                MessageBox.Show("写标志位地址未设置");
                return;
            }
            if (MessageBox.Show("确定一键全部设置吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int DevCount = 0;
                //读
                PlcDeviceName readpd = tbStartReadDeviceName.Text;
                if (TaskRecv != null)
                {
                    DataTable dt = TaskRecv.masterDetail._cDataset.Tables["PLCTask"];
                    foreach (DataRow item in dt.Rows)
                    {
                        item["起始地址"] = readpd;
                        int currentCount = Convert.ToInt32(item["总指令长度"]);
                        int currentId = Convert.ToInt32(item["ID"]);
                        TaskRecv.masterDetail.SetAll(readpd, currentId);
                        readpd += currentCount;
                        DevCount += currentCount;
                    }
                }
                if (CalibRecv != null)
                {
                    DataTable dt = CalibRecv.masterDetail._cDataset.Tables["PLCTask"];
                    foreach (DataRow item in dt.Rows)
                    {
                        item["起始地址"] = readpd;
                        int currentCount = Convert.ToInt32(item["总指令长度"]);
                        int currentId = Convert.ToInt32(item["ID"]);
                        CalibRecv.masterDetail.SetAll(readpd, currentId);
                        readpd += currentCount;
                        DevCount += currentCount;
                    }
                }
                //写
                PlcDeviceName writepb = tbWriteFlagDeviceName.Text;
                writepb += 1;
                if (TaskSend != null)
                {
                    DataTable dt = TaskSend.masterDetail._cDataset.Tables["PLCTask"];
                    foreach (DataRow item in dt.Rows)
                    {
                        item["起始地址"] = writepb;
                        int currentCount = Convert.ToInt32(item["总指令长度"]);
                        int currentId = Convert.ToInt32(item["ID"]);
                        TaskSend.masterDetail.SetAll(writepb, currentId);
                        writepb += currentCount;
                    }
                }

                if (CalibSend != null)
                {
                    DataTable dt = CalibSend.masterDetail._cDataset.Tables["PLCTask"];
                    foreach (DataRow item in dt.Rows)
                    {
                        item["起始地址"] = writepb;
                        int currentCount = Convert.ToInt32(item["总指令长度"]);
                        int currentId = Convert.ToInt32(item["ID"]);
                        CalibSend.masterDetail.SetAll(writepb, currentId);
                        writepb += currentCount;
                    }
                }

                tbReadLength.Text = DevCount.ToString();
            }
            else
                return;
        }
    }
}
