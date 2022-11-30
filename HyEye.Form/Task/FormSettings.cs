using Autofac;
using HyEye.API.Repository;
using HyEye.Models.VO;
using HyEye.Services;
using HyEye.WForm.Settings;
using HyEye.WForm.Vision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm
{
    public partial class FormSettings : Form
    {
        readonly ITaskRepository taskRepo;
        readonly ICommunicationRepository communicationRepo;
        readonly ISimulationRepository simulationRepo;
        readonly ITaskService taskService;

        public FormSettings(
            ITaskRepository taskRepo,
            ICommunicationRepository communicationRepo,
            ISimulationRepository simulationRepo,
            ITaskService taskService)
        {
            InitializeComponent();

            Text = $"{Global.Name} [V{Global.Version}]";

            dockPanelMain.DockLeftPortion = 0.17;

            this.taskRepo = taskRepo;
            this.communicationRepo = communicationRepo;
            this.simulationRepo = simulationRepo;
            this.taskService = taskService;

            SimulationRepo_EnabledChanged();
            simulationRepo.EnabledChanged += SimulationRepo_EnabledChanged;
        }

        private void SimulationRepo_EnabledChanged()
        {
            if (simulationRepo.Enabled)
            {
                tsmiOnLine.Text = "离线模式";
                tsmiOnLine.Image = Properties.Resources.离线1;
            }
            else
            {
                tsmiOnLine.Text = "在线模式";
                tsmiOnLine.Image = Properties.Resources.在线1;
            }
        }

        FrmTaskSetting frmTaskSetting;
        FrmToolBlockSetting frmVisionToolSetting;

        private void FormSettings_Load(object sender, EventArgs e)
        {
            frmTaskSetting = ShowForm<FrmTaskSetting>(DockState.DockLeft);
            frmTaskSetting.showToolBlockSetHandle += showToolBlockSet;

            frmVisionToolSetting = ShowForm<FrmToolBlockSetting>(DockState.Document);

            ShowForm<FormLog>(DockState.DockBottom);

            foreach (DockContent dc in documents)
            {
                if (!dc.IsDisposed)
                    dc.Show(dockPanelMain, DockState.Document);
            }
        }

        private void showToolBlockSet(string taskName)
        {
            frmVisionToolSetting?.TaskClickShowToolBolckSet(taskName);
        }

        List<DockContent> documents = new List<DockContent>();

        readonly Dictionary<string, DockContent> dict = new Dictionary<string, DockContent>();

        #region 视图

        private void tsmiTaskSetting_Click(object sender, EventArgs e)
        {
            ShowForm<FrmTaskSetting>(DockState.DockLeft);
        }

        private void tsmiVisionHandle_Click(object sender, EventArgs e)
        {
            ShowForm<FrmToolBlockSetting>(DockState.Document);
        }

        private void tsmiLog_Click(object sender, EventArgs e)
        {
            ShowForm<FormLog>(DockState.DockBottom);
        }

        #endregion

        #region 设置

        // 相机设置
        private void tsmiCameraSetting_Click(object sender, EventArgs e)
        {
            FrmCameraSetting form = (FrmCameraSetting)FindSubForm("相机设置");

            if (form == null)
            {
                form = AutoFacContainer.Resolve<FrmCameraSetting>();
            }

            form.Show(dockPanelMain, DockState.Document);
        }

        // 光源设置
        private void tsmiLightControllerSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmLightControllerSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        // 指令设置
        private void tsmiCommandSetting_Click(object sender, EventArgs e)
        {
            CommunicationInfoVO commInfo = communicationRepo.GetCommunicationInfo();
            if (commInfo.CommProtocol == GL.Kit.Net.CommProtocol.TCP)
                AutoFacContainer.Resolve<FrmCommandSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
            else
                AutoFacContainer.Resolve<Settings.PLCRegSetting.FrmPLCRegAgg>(new NamedParameter("readOnly", taskService.Running), new NamedParameter("closeSave", true)).ShowDialog();
        }

        // 通讯设置
        private void tsmiCommunicationSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmCommunicationSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        // 图像管理
        private void tsmiImageSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmImageManager>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        // 其他设置
        private void tsmiSystemSetting_Click(object sender, EventArgs e)
        {
            AutoFacContainer.Resolve<FrmSystemSetting>(new NamedParameter("readOnly", taskService.Running)).ShowDialog();
        }

        #endregion

        #region 在线/离线

        private void tsmiOnLine_Click(object sender, EventArgs e)
        {
            simulationRepo.Enabled = !simulationRepo.Enabled;
        }

        #endregion

        #region 流程控制

        private void tsmiProjectProcess_Click(object sender, EventArgs e)
        {
            new FrmProjectProcess().ShowDialog();
        }

        #endregion

        public TForm ShowForm<TForm>(DockState dockState) where TForm : DockContent
        {
            string formname = typeof(TForm).Name;

            DockContent frm;
            if (dict.ContainsKey(formname))
            {
                frm = dict[formname];
            }
            else
            {
                frm = AutoFacContainer.Resolve<TForm>();
                dict.Add(formname, frm);
            }

            frm.Show(dockPanelMain, dockState);

            return frm as TForm;
        }

        /// <summary>
        /// 直接显示传入窗体
        /// </summary>
        public void ShowAloneForm<TForm>(DockState dockState, TForm dockContent) where TForm : DockContent
        {
            dockContent.Show(dockPanelMain, dockState);
        }

        public Form FindSubForm(string text)
        {
            return MdiChildren.FirstOrDefault(f => f.Text == text);
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ShowDialog 的窗体，Visible 设置为 false 将触发 Close
            if (!Visible) return;

            DialogResult dialog = MessageBoxUtils.ShowQuestionCanCancel("是否保存配置？");

            if (dialog == DialogResult.Yes)
            {
                taskRepo.Save();
            }
            else if (dialog == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            string formname = typeof(FormLog).Name;
            if (dict.ContainsKey(formname))
            {
                dict[formname].Parent = null;
            }

            documents.Clear();

            foreach (Form f in MdiChildren)
            {
                if (f is DockContent dc)
                {
                    if (dc.DockState == DockState.Document)
                    {
                        documents.Add(dc);
                        dc.DockState = DockState.Hidden;
                    }
                }
            }

            Visible = false;
            e.Cancel = true;
        }

    }

    class DockSaver
    {
        readonly string configFile = "DockPanel.temp.config";

        DockPanel dockPanel;
        DeserializeDockContent m_deserializeDockContent;

        Assembly assembly;
        readonly Dictionary<string, IDockContent> forms = new Dictionary<string, IDockContent>();

        public DockSaver(DockPanel dockPanel)
        {
            this.dockPanel = dockPanel;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            assembly = Assembly.GetExecutingAssembly();
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (forms.ContainsKey(persistString))
                return forms[persistString];

            Type type = assembly.GetType(persistString);
            IDockContent frm = (IDockContent)AutoFacContainer.Resolve(type);

            forms[persistString] = frm;

            return frm;
        }

        public void Load()
        {
            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        public void Save()
        {
            dockPanel.SaveAsXml(configFile);
        }
    }
}
