using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;

namespace HyEye.WForm.Vision
{
    public partial class FrmSetParams : Form
    {
        readonly ITaskRepository taskRepo;
        readonly IGLog log;
        readonly ToolBlockComponentSet toolBlockComponentSet;
        bool closeSave;

        public FrmSetParams(bool readOnly,
            bool closeSave,
            ITaskRepository taskRepo,
            IGLog log,
            ToolBlockComponentSet toolBlockComponentSet)
        {
            InitializeComponent();

            LoadAddMenu();

            //主界面设置菜单栏加入该界面入口，此时加入任务运行，禁止操作的判断。
            tabControl1.Enabled = !readOnly;

            this.closeSave = closeSave;
            this.taskRepo = taskRepo;
            this.log = log;
            this.toolBlockComponentSet = toolBlockComponentSet;
        }

        void LoadAddMenu()
        {
            foreach (Type type in Types)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem
                {
                    Text = type.ToString(),
                    Tag = type
                };
                tsmi.Click += tsmiAdd_Click;

                tsmiAdd.DropDownItems.Add(tsmi);
            }
        }

        private void FrmSetParams_Load(object sender, EventArgs e)
        {
            List<TaskInfoVO> tasks = taskRepo.GetTasks();

            foreach (TaskInfoVO task in tasks)
            {
                TabPage page = new TabPage { Text = task.Name };
                tabControl1.TabPages.Add(page);

                DataGridView dgv = createDataGridView();
                dgv.ContextMenuStrip = contextMenuStrip1;
                page.Controls.Add(dgv);

                IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(task.Name);

                //add by LuoDian @ 20211227 添加图像旋转的角度选择，也就是拍完的图像可以旋转固定的角度之后再显示及运算
                if (!toolBlockComponent.InputContains(API.GlobalParams.ImageRotatoAngle))
                {
                    ParamInfo param = new ParamInfo
                    {
                        Name = API.GlobalParams.ImageRotatoAngle,
                        Type = typeof(string),
                        Value = "0",
                        Description = "图像旋转的角度 | 0度、90度、180度、270度"
                    };
                    toolBlockComponent.AddInput(@param);
                }

                //add by LuoDian @ 20211227 添加是否等所有图像拍完再运行算法
                if (!toolBlockComponent.InputContains(API.GlobalParams.IsWaitAllImage))
                {
                    ParamInfo param = new ParamInfo
                    {
                        Name = API.GlobalParams.IsWaitAllImage,
                        Type = typeof(string),
                        Value = "No",
                        Description = "是否等所有图像拍完再运行算法"
                    };
                    toolBlockComponent.AddInput(@param);
                }

                List<ParamInfo> @params = toolBlockComponent.GetInputs();

                foreach (ParamInfo param in @params)
                {
                    int rowIndex = dgv.Rows.Add(param.Name, param.Type.ToString(), param.Value, param.Description);
                    dgv.Rows[rowIndex].Tag = param;
                }
            }
        }

        DataGridView createDataGridView()
        {
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                ReadOnly = false
            };

            var colParamName = new DataGridViewTextBoxColumn { HeaderText = "参数名称" };
            var colParamType = new DataGridViewComboBoxColumn { HeaderText = "参数类型", Width = 140 };
            colParamType.Items.AddRange(Types.Select(t => t.ToString()).ToArray());
            var colParamValue = new DataGridViewTextBoxColumn { HeaderText = "参数值" };
            var colDescription = new DataGridViewTextBoxColumn { HeaderText = "参数说明", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 125 };

            dgv.Columns.Add(colParamName);
            dgv.Columns.Add(colParamType);
            dgv.Columns.Add(colParamValue);
            dgv.Columns.Add(colDescription);

            dgv.CellBeginEdit += Dgv_CellBeginEdit;
            //dgv.CellEndEdit += Dgv_CellEndEdit;
            dgv.CellValueChanged += Dgv_CellValueChanged;
            //dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;

            return dgv;
        }

        private void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.CurrentCell.OwningColumn is DataGridViewComboBoxColumn)
            {
                if (dgv.IsCurrentCellDirty)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            string inputName = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();
            if (inputName != InputOutputConst.Input_AcqIndex)
            {
                if (!clearErrorMsg)
                {
                    if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText != null)
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
                }
            }
            else
            {
                MessageBoxUtils.ShowWarn("拍照索引无法修改参数名");
                e.Cancel = true;
            }
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            cellEndEdit(dgv, e.RowIndex, e.ColumnIndex);
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreValueChange) return;

            cellEndEdit(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        bool ignoreValueChange = false;
        bool clearErrorMsg = false;

        void cellEndEdit(DataGridView dgv, int rowIndex, int colIndex)
        {
            if (ignoreValueChange) return;

            DataGridViewCell nameCell = dgv.Rows[rowIndex].Cells[0];
            DataGridViewCell typeCell = dgv.Rows[rowIndex].Cells[1];
            DataGridViewCell valueCell = dgv.Rows[rowIndex].Cells[2];
            DataGridViewCell descriptionCell = dgv.Rows[rowIndex].Cells[3];

            string paramName = nameCell.Value?.ToString().Trim();

            if (colIndex == 0)
            {
                if (!verifyParamName(paramName, out string errmsg))
                {
                    if (errmsg != null)
                        nameCell.ErrorText = errmsg;

                    ignoreValueChange = true;
                    nameCell.Value = (dgv.Rows[rowIndex].Tag as ParamInfo).Name;
                    ignoreValueChange = false;

                    clearErrorMsg = true;
                    dgv.BeginEdit(false);
                    clearErrorMsg = false;
                }
                else
                {
                    string oldname = (dgv.Rows[rowIndex].Tag as ParamInfo).Name;

                    IToolBlockComponent toolBlockComponent = getToolBlockComponent();

                    toolBlockComponent.RenameInput(oldname, paramName);
                }
            }
            else if (colIndex == 1)
            {
                IToolBlockComponent toolBlockComponent = getToolBlockComponent();
                toolBlockComponent.ChangeInputType(paramName, Type.GetType(typeCell.Value.ToString()));

                ignoreValueChange = true;
                valueCell.Value = string.Empty;
                ignoreValueChange = false;
            }
            else if (colIndex == 2)
            {
                IToolBlockComponent toolBlockComponent = getToolBlockComponent();

                try
                {
                    ParamInfo @param = dgv.Rows[rowIndex].Tag as ParamInfo;

                    toolBlockComponent.SetInputValue(paramName, valueCell.Value.ChanageType(@param.Type));
                }
                catch (Exception e)
                {
                    ignoreValueChange = true;
                    valueCell.Value = (dgv.Rows[rowIndex].Tag as ParamInfo).Value;
                    ignoreValueChange = false;

                    valueCell.ErrorText = e.Message;

                    clearErrorMsg = true;
                    dgv.BeginEdit(false);
                    clearErrorMsg = false;
                }
            }
            else if (colIndex == 3)
            {
                IToolBlockComponent toolBlockComponent = getToolBlockComponent();
                toolBlockComponent.SetInputDescription(paramName, descriptionCell.Value?.ToString().Trim());
            }
        }

        int inputIndex = 1;

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            Type type = (sender as ToolStripMenuItem).Tag as Type;

            IToolBlockComponent toolBlockComponent = getToolBlockComponent();
            string paramName = getDefaultName(toolBlockComponent);

            DataGridView dgv = tabControl1.SelectedTab.Controls[0] as DataGridView;

            int index = dgv.Rows.Add(paramName, type.ToString(), string.Empty);

            ParamInfo param = new ParamInfo
            {
                Name = paramName,
                Type = type,
                Value = null
            };

            toolBlockComponent.AddInput(param);

            dgv.Rows[index].Tag = param;
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("是否确定删除？") == DialogResult.Yes)
            {
                IToolBlockComponent toolBlockComponent = getToolBlockComponent();

                DataGridView dgv = tabControl1.SelectedTab.Controls[0] as DataGridView;

                if (dgv.Rows.Count <= 0)
                    return;

                string inputName = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();

                if (inputName != InputOutputConst.Input_AcqIndex)
                {
                    toolBlockComponent.DeleteInput(inputName);

                    dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex);
                }
                else
                {
                    MessageBoxUtils.ShowError("拍照索引无法删除");
                }
            }
        }

        bool verifyParamName(string paramName, out string errmsg)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                errmsg = "参数名不能为空";
                return false;
            }

            if (!paramName.IsMatch(ComPattern.NamePattern))
            {
                errmsg = "参数名只能包含字母、数字和下划线，并且不能以数字开头";
                return false;
            }

            IToolBlockComponent toolBlockComponent = getToolBlockComponent();

            if (toolBlockComponent.InputContains(paramName))
            {
                errmsg = "参数名已存在";
                return false;
            }

            errmsg = null;
            return true;
        }

        IToolBlockComponent getToolBlockComponent()
        {
            string taskName = tabControl1.SelectedTab.Text;

            return toolBlockComponentSet.GetComponent(taskName);
        }

        string getDefaultName(IToolBlockComponent toolBlockComponent)
        {
            string paramName = $"Input{inputIndex++}";

            while (toolBlockComponent.InputContains(paramName))
            {
                paramName = $"Input{inputIndex++}";
            }

            return paramName;
        }

        static readonly Type[] Types = new Type[]
        {
            typeof(bool),
            typeof(char),
            typeof(DateTime),
            typeof(double),
            typeof(int),
            typeof(string),
            typeof(TimeSpan)
        };

        private void FrmSetParams_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                (page.Controls[0] as DataGridView).EndEdit();
            }

            if (closeSave)
                taskRepo.Save();
        }
    }
}
