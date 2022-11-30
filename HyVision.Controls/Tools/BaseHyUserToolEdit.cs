using Autofac;
using HyEye.API.Repository;
using HyVision.Models;
using HyVision.Tools.ToolBlock;
using HyVision.Tools.ToolBox;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HyVision.Tools
{
    public partial class BaseHyUserToolEdit<T> : UserControl, IHyUserToolEdit<T>// where T : IHyUserTool
    {
        public virtual T Subject { get; set; }

        //add by LuoDian @ 20211214 用于子料号的快速切换时，获取当前选择的子料号
        IMaterialRepository materialRepo;

        public BaseHyUserToolEdit()
        {
            InitializeComponent();
        }

        private void BaseHyUserToolEdit_Load(object sender, EventArgs e)
        {
            tslblRunTime.Text = null;
            tslblErrMsg.Text = null;
        }

        string filename;

        private void tsBtnRun_Click(object sender, EventArgs e)
        {
            if (Subject is BaseHyUserTool tool)
            {
                foreach (HyTerminal input in tool.Inputs)
                {
                    if (input.From != null)
                    {
                        input.Value = tool.InputOutputs[input.From].Value;
                    }
                }

                if (materialRepo == null)
                    materialRepo = AutoFacContainer.Resolve<MaterialRepository>();
                //update by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
                tool.Run(materialRepo.CurSubName);
            }
        }

        private void tsBtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "hy|*.hy";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    T toolBlock = (T)HySerializer.LoadFromFile(open.FileName);

                    Subject = toolBlock;
                    filename = open.FileName;
                }
                catch (Exception)
                {
                    MessageBoxUtils.ShowError($"配置文件不包含 {nameof(T)}");
                }
            }
        }

        private void tsbtnSaveWithImage_Click(object sender, EventArgs e)
        {
            //add by LuoDian @ 20210730 子类需要保存XML
            Save();

            GetFilename();

            if (filename != null)
            {
                UpdateDataToObject();

                HySerializer.SaveToFile(Subject, filename);
            }
        }

        private void tsbtnSaveWithoutImage_Click(object sender, EventArgs e)
        {
            //add by LuoDian @ 20210730 子类需要保存XML
            Save();

            GetFilename();

            if (filename != null)
            {
                UpdateDataToObject();

                HySerializer.SaveToFile(((IHyUserTool)Subject).Clone(false), filename);
            }
        }

        private void GetFilename()
        {
            if (filename == null)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "hy|*.hy";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    filename = save.FileName;
                }
            }
        }

        private void tsbtnToolbox_Click(object sender, EventArgs e)
        {
            if (Subject is HyToolBlock)
            {
                HyToolboxForm toolbox = new HyToolboxForm();
                toolbox.SelectTool += Toolbox_SelectTool;

                toolbox.Show();
            }
        }

        //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
        private void Toolbox_SelectTool(object sender, UserToolEventArgs e)
        {
            if(materialRepo == null)
                materialRepo = AutoFacContainer.Resolve<MaterialRepository>();
            HyToolCollection tempToolCollection = (Subject as HyToolBlock).Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (tempToolCollection == null)
            {
                GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                log.Error($"往ToolBlock中添加工具失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                return;
            }

            tempToolCollection.Add(e.Tool);
        }

        public virtual void UpdateDataToObject()
        {

        }

        //add by LuoDian @ 20210730 子类需要保存XML
        public virtual void Save()
        {

        }
    }
}
