using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmSetOutput : Form
    {
        readonly ITaskRepository taskRepo;
        readonly IRecordShowRepository recordShowRepo;

        public List<RecordShowInfoVO> RecordShowInfoVOs { get; set; }


        public FrmSetOutput(
            ITaskRepository _taskRepo,
            IRecordShowRepository _recordShowRepo)
        {
            InitializeComponent();
            this.taskRepo = _taskRepo;
            this.recordShowRepo = _recordShowRepo;
        }

        private void FrmSetOutput_Load(object sender, EventArgs e)
        {
            List<TaskInfoVO> tasklist = taskRepo.GetTasks().ToList();
            if (tasklist == null || tasklist.Count <= 0)
            {
                MessageBox.Show("未添加任务，请先添加任务。");
                Close();
            }

            foreach (var item in tasklist)
            {
                ucSetOutput ucTemp;
                var tempRS = recordShowRepo.GetRecordShow(item.Name);
                if (tempRS != null)
                    ucTemp = new ucSetOutput(tempRS.TaskName, tempRS.RecordIndex);
                else
                    ucTemp = new ucSetOutput(item.Name, -1);
                ucTemp.Width = fMain.Width - 25;
                fMain.Controls.Add(ucTemp);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fMain.Controls.Count <= 0)
                return;

            List<RecordShowInfoVO> RecordShowInfoVOs = new List<RecordShowInfoVO>();

            foreach (var item in fMain.Controls)
            {
                if (item is ucSetOutput)
                {
                    RecordShowInfoVOs.Add(new RecordShowInfoVO()
                    {
                        TaskName = ((ucSetOutput)item).TaskName,
                        RecordIndex = ((ucSetOutput)item).RecordIndex
                    });
                }
            }

            try
            {
                recordShowRepo.SetRecordShow(RecordShowInfoVOs);
                recordShowRepo.Save();

                Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
