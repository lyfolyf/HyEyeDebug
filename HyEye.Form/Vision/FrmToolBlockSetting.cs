using Autofac;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm.Vision
{
    public partial class FrmToolBlockSetting : DockContentEx
    {
        readonly ITaskRepository taskRepo;
        readonly ToolBlockComponentSet toolBlockComponentSet;
        readonly IGLog log;

        public FrmToolBlockSetting(ITaskRepository taskRepo,
            ToolBlockComponentSet toolBlockComponentSet,
            IGLog log
            )
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.toolBlockComponentSet = toolBlockComponentSet;
            this.log = log;

            // 用于界面联动
            taskRepo.TaskAdd += TaskRepo_TaskAdd;
            taskRepo.TaskDelete += TaskRepo_TaskDelete;
            taskRepo.TaskRename += TaskRepo_TaskRename;
        }

        List<string> tasklist = new List<string>();

        private void FrmVisionToolSetting_Load(object sender, EventArgs e)
        {
            //暂时保留
            //List<TaskInfoVO> tasks = taskRepo.GetTasks();

            //foreach (TaskInfoVO task in tasks)
            //{
            //    addComboBox(task.Name);
            //    addTabPage(task.Name);
            //}


            List<TaskInfoVO> tasks = taskRepo.GetTasks();
            if (tasks.Count > 0)
            {
                addComboBox(tasks[0].Name);
                addTabPage(tasks[0].Name);
                toolBlockComponentSet.ExpandOne(tasks[0].Name);
            }


            if (cmbTasks.Items.Count > 0)
                cmbTasks.SelectedIndex = 0;
        }

        public void TaskClickShowToolBolckSet(string taskName)
        {
            TabPage tempPage = new TabPage(taskName);
            if (!tabVision.TabPages.ContainsKey(taskName))
            {
                addComboBox(taskName);
                addTabPage(taskName);
                toolBlockComponentSet.ExpandOne(taskName);
            }
        }

        #region 任务联动

        private void TaskRepo_TaskAdd(TaskInfoVO task)
        {
            addComboBox(task.Name);
            addTabPage(task.Name);
        }

        private void TaskRepo_TaskDelete(string taskname)
        {
            deleteComboBox(taskname);
            deleteTabPage(taskname);
        }

        private void TaskRepo_TaskRename(string oldname, string newname)
        {
            renameComboBox(oldname, newname);
            renameTabPage(oldname, newname);
        }

        public void addComboBox(string taskname)
        {
            cmbTasks.Items.Add(taskname);
        }

        void renameComboBox(string oldname, string newname)
        {
            int index = cmbTasks.Items.IndexOf(oldname);
            if (index > -1)
            {
                cmbTasks.Items[index] = newname;
            }
        }

        void deleteComboBox(string taskname)
        {
            cmbTasks.Items.Remove(taskname);
        }

        public void addTabPage(string taskname)
        {
            TabPage page = new TabPage(taskname);
            page.Name = taskname;
            tabVision.TabPages.Add(page);

            Control control = toolBlockComponentSet.GetComponent(taskname).DisplayedControl;
            control.Dock = DockStyle.Fill;
            page.Controls.Add(control);
        }

        void renameTabPage(string oldname, string newname)
        {
            TabPage page = tabVision.TabPages.FirstOrDefault(a => a.Text == oldname);
            if (page != null)
            {
                page.Text = newname;
            }
            tabVision.SelectedTab = page;
        }

        void deleteTabPage(string taskname)
        {
            tabVision.TabPages.RemoveFirst((page) => page.Text == taskname);
        }

        #endregion

        private void cmbTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTasks.SelectedIndex == -1) return;

            string taskname = cmbTasks.Text;

            TabPage taskPage = tabVision.FindPage((page) => page.Text == taskname);
            tabVision.SelectedTab = taskPage;
        }

        private void btnSaveCurTask_Click(object sender, EventArgs e)
        {
            if (tabVision.SelectedTab != null)
            {
                toolBlockComponentSet.Save(tabVision.SelectedTab.Text);
            }
        }

        private void btnSaveAllTasks_Click(object sender, EventArgs e)
        {
            toolBlockComponentSet.SaveAll();
        }

        private void btnSetParams_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmSetParams>(new NamedParameter("readOnly", false), new NamedParameter("closeSave", false)).ShowDialog();
        }

        private void btnResetParam_Click(object sender, EventArgs e)
        {
            foreach (IToolBlockComponent toolBlock in toolBlockComponentSet)
            {
                toolBlock.ResetDefaultParam();
            }

            MessageBoxUtils.ShowInfo("重置成功");
        }



        //private void FrmToolBlockSetting_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (Visible)
        //    {
        //        //DG 410 toolBlockComponentSet => NullReferenceException 
        //        toolBlockComponentSet?.ExpandAll();
        //    }
        //}




    }
}
