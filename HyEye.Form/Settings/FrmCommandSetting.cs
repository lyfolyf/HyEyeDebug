using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;

namespace HyEye.WForm.Settings
{
    public partial class FrmCommandSetting : Form
    {
        readonly ICommandRepository commandRepo;

        //add by LuoDian @ 20211228 为了在修改指令参数的时候，把ToolBlock部分的参数同步更新到ToolBlock中
        readonly ToolBlockComponentSet toolBlockComponentSet;

        const string CommandTypeString = "A/R/AR";
        const string CalibrationReceiveCommandTypeString = "S/C";
        const string CalibrationSendCommandTypeString = "S/C/E";

        static readonly Type[] DataTypes = new Type[]
        {
            typeof(int),
            typeof(double),
            typeof(string),
            typeof(DateTime)
        };

        public FrmCommandSetting(bool readOnly, ICommandRepository commandRepo,
            ToolBlockComponentSet toolBlockComponentSet)
        {
            InitializeComponent();

            DisableSorting();

            LoadField();

            btnSave.Enabled = !readOnly;

            foreach (Type type in DataTypes)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem
                {
                    Text = type.ToString(),
                    Tag = type
                };
                tsmi.Click += tsmiRecvFieldAdd_Click;

                tsmiRecvFieldAdd.DropDownItems.Add(tsmi);

                ToolStripMenuItem tsmi2 = new ToolStripMenuItem
                {
                    Text = type.ToString(),
                    Tag = type
                };
                tsmi2.Click += tsmiCalibrationRecvFieldAdd_Click;

                tsmiCalibrationRecvFieldAdd.DropDownItems.Add(tsmi2);
            }

            this.commandRepo = commandRepo;

            //add by LuoDian @ 20211228 为了在修改指令参数的时候，把ToolBlock部分的参数同步更新到ToolBlock中
            this.toolBlockComponentSet = toolBlockComponentSet;
        }

        // 禁用 DataGridView 排序
        void DisableSorting()
        {
            dgvRecvCommands.SetSortMode(DataGridViewColumnSortMode.NotSortable);
            dgvRecvCmdField.SetSortMode(DataGridViewColumnSortMode.NotSortable);
            dgvSendCommands.SetSortMode(DataGridViewColumnSortMode.NotSortable);

            dgvCalibrationRecvCmd.SetSortMode(DataGridViewColumnSortMode.NotSortable);
            dgvCalibrationRecvCmdField.SetSortMode(DataGridViewColumnSortMode.NotSortable);
            dgvCalibrationSendCmd.SetSortMode(DataGridViewColumnSortMode.NotSortable);

            dataGridView1.SetSortMode(DataGridViewColumnSortMode.NotSortable);
        }

        // 加载字段类型和用途下拉框
        void LoadField()
        {
            colFieldType.DataSource = DataTypes.Select(d => d.FullName).ToList();
            foreach (CommandFieldUse commandFieldUse in Enum.GetValues(typeof(CommandFieldUse)))
            {
                colFieldUse.Items.Add(commandFieldUse.ToString());
            }

            colFieldType3.DataSource = DataTypes.Select(d => d.FullName).ToList();
            colFieldUse3.Items.Add(CommandFieldUse.None.ToString());
        }

        private void FrmCommunicationSetting_Load(object sender, EventArgs e)
        {
            LoadReceiveCommands();

            LoadSendCommands();

            LoadCalibrationReceiveCommands();

            LoadCalibrationSendCommands();

            dataGridView1.Rows.Add("心跳指令", "HyInspect", "HyInspect", "大小写不限，接收和发送一致");
            dataGridView1.Rows.Add("暂停指令", "P,S", "P,S,0", "");
            dataGridView1.Rows.Add("继续指令", "P,R", "P,R,0", "");
            dataGridView1.Rows.Add("复位指令", "P,RE", "P,RE,0", "");
            dataGridView1.Rows.Add("脚本指令", "RS,params", "RS,0/1", "接收指令：params：传进脚本方法的参数列表，以逗号分隔。\r\n发送指令：0：执行成功，1：执行失败");
            dataGridView1.Rows.Add("料号切换指令", "MC,料号索引", "");
        }

        void LoadReceiveCommands()
        {
            List<ReceiveCommandInfoVO> commands = commandRepo.GetTaskRecvCommandInfos();

            foreach (ReceiveCommandInfoVO cmd in commands)
            {
                int rowIndex = dgvRecvCommands.Rows.Add(
                    cmd.TaskName,
                    cmd.Name,
                    cmd.CommandHeader,
                    cmd.Index,
                    CommandTypeString,
                    string.Join(",", cmd.Fields.Select(f => f.Name)));
                dgvRecvCommands.Rows[rowIndex].Tag = cmd;
            }

            dgvRecvCommands_CurrentCellChanged(dgvRecvCommands, EventArgs.Empty);
            dgvRecvCommands.CurrentCellChanged += dgvRecvCommands_CurrentCellChanged;
        }

        void LoadSendCommands()
        {
            List<SendCommandInfoVO> commands = commandRepo.GetTaskSendCommandInfos();

            foreach (SendCommandInfoVO cmd in commands)
            {
                int rowIndex = dgvSendCommands.Rows.Add(
                    cmd.TaskName,
                    cmd.Name,
                    cmd.CommandHeader,
                    cmd.Index,
                    CommandTypeString,
                    "0/非0",
                    cmd.Fields);
                dgvSendCommands.Rows[rowIndex].Tag = cmd;
            }

            dgvSendCommands.CellValueChanged += dgvSendCommands_CellValueChanged;
        }

        void LoadCalibrationReceiveCommands()
        {
            List<ReceiveCommandInfoVO> commands = commandRepo.GetCalibRecvCommandInfos();

            foreach (ReceiveCommandInfoVO cmd in commands)
            {
                int rowIndex = dgvCalibrationRecvCmd.Rows.Add(
                    cmd.TaskName,
                    cmd.Name,
                    cmd.CommandHeader,
                    cmd.Index,
                    CalibrationReceiveCommandTypeString,
                    string.Join(",", cmd.Fields.Select(f => f.Name)));
                dgvCalibrationRecvCmd.Rows[rowIndex].Tag = cmd;
            }

            DgvCalibrationRecvCmd_CurrentCellChanged(dgvCalibrationRecvCmd, EventArgs.Empty);
            dgvCalibrationRecvCmd.CurrentCellChanged += DgvCalibrationRecvCmd_CurrentCellChanged;
        }

        void LoadCalibrationSendCommands()
        {
            List<SendCommandInfoVO> commands = commandRepo.GetCalibSendCommandInfos();

            foreach (SendCommandInfoVO cmd in commands)
            {
                int rowIndex = dgvCalibrationSendCmd.Rows.Add(
                    cmd.TaskName,
                    cmd.Name,
                    cmd.CommandHeader,
                    cmd.Index,
                    CalibrationSendCommandTypeString,
                    "0/非0",
                    cmd.Fields);
                dgvCalibrationSendCmd.Rows[rowIndex].Tag = cmd;
            }

            dgvCalibrationSendCmd.CellValueChanged += DgvCalibrationSendCmd_CellValueChanged;
        }

        int index = 1;
        int index2 = 1;

        int getIndex(List<CommandFieldInfoVO> fields)
        {
            if (fields.Count == 0)
                return 1;

            var indexs = fields.Select(field => Regex.Match(field.Name, @"^V(\d+)$"))
                    .Where(m => m.Success)
                    .Select(m => int.Parse(m.Groups[1].Value));

            if (indexs.Count() > 0)
                return indexs.Max() + 1;
            else
                return 1;
        }

        bool clearErrorMsg = false;
        bool ignoreValueChange = false;

        #region 生产指令

        private void dgvRecvCommands_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvRecvCommands.CurrentCell == null) return;

            int rowIndex = dgvRecvCommands.CurrentCell.RowIndex;
            if (rowIndex == -1) return;

            dgvRecvCmdField.Rows.Clear();

            ReceiveCommandInfoVO cmd = dgvRecvCommands.CurrentRow.Tag as ReceiveCommandInfoVO;

            foreach (CommandFieldInfoVO field in cmd.Fields)
            {
                dgvRecvCmdField.Rows.Add(field.Name, field.DataType.FullName, field.Use.ToString());
            }

            index = getIndex(cmd.Fields);
        }

        private void dgvRecvCmdField_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreValueChange) return;

            if (dgvRecvCommands.CurrentRow == null) return;

            ReceiveCommandInfoVO cmd = dgvRecvCommands.CurrentRow.Tag as ReceiveCommandInfoVO;

            if (e.ColumnIndex == 0)
            {
                DataGridViewCell nameCell = dgvRecvCmdField.Rows[e.RowIndex].Cells[0];
                string fieldName = nameCell.Value?.ToString().Trim();
                if (!verifyParamName(cmd.Fields, fieldName, out string errmsg))
                {
                    if (errmsg != null)
                        nameCell.ErrorText = errmsg;

                    ignoreValueChange = true;
                    nameCell.Value = cmd.Fields[e.RowIndex].Name;
                    ignoreValueChange = false;

                    clearErrorMsg = true;
                    dgvRecvCmdField.BeginEdit(false);
                    clearErrorMsg = false;
                }
                else
                {
                    cmd.Fields[e.RowIndex].Name = fieldName;

                    dgvRecvCommands.CurrentRow.Cells["colAppendData"].Value = string.Join(",", cmd.Fields.Select(c => c.Name));
                }
            }
            else if (e.ColumnIndex == 1)
            {
                cmd.Fields[e.RowIndex].DataType = Type.GetType(dgvRecvCmdField.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            else if (e.ColumnIndex == 2)
            {
                cmd.Fields[e.RowIndex].Use = (CommandFieldUse)Enum.Parse(typeof(CommandFieldUse), dgvRecvCmdField.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
        }

        private void dgvRecvCmdField_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!clearErrorMsg)
            {
                if (dgvRecvCmdField.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText != null)
                    dgvRecvCmdField.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
            }
        }

        private void tsmiRecvFieldAdd_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            CommandFieldInfoVO newField = new CommandFieldInfoVO
            {
                Name = $"V{index++}",
                DataType = tsmi.Tag as Type,
                Use = CommandFieldUse.None
            };

            dgvRecvCmdField.Rows.Add(newField.Name, newField.DataType.FullName, newField.Use.ToString());

            ReceiveCommandInfoVO cmd = dgvRecvCommands.CurrentRow.Tag as ReceiveCommandInfoVO;
            cmd.Fields.Add(newField);

            dgvRecvCommands.CurrentRow.Cells["colAppendData"].Value = string.Join(",", cmd.Fields.Select(c => c.Name));
        }

        private void tsmiRecvFieldDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxUtils.ShowQuestion("确定要删除该字段吗？") == DialogResult.No) return;

            var row = dgvRecvCmdField.CurrentRow;
            dgvRecvCmdField.Rows.Remove(row);

            ReceiveCommandInfoVO cmd = dgvRecvCommands.CurrentRow.Tag as ReceiveCommandInfoVO;
            cmd.Fields.Remove(c => c.Name == row.Cells[0].Value.ToString());

            dgvRecvCommands.CurrentRow.Cells["colAppendData"].Value = string.Join(",", cmd.Fields.Select(c => c.Name));
        }

        private void dgvSendCommands_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreValueChange) return;

            if (dgvSendCommands.CurrentRow == null) return;

            SendCommandInfoVO cmd = dgvSendCommands.CurrentRow.Tag as SendCommandInfoVO;

            if (e.ColumnIndex == 6)
            {
                string strFields = dgvSendCommands.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string[] fields = strFields.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

                strFields = string.Join(",", fields.Distinct());

                ignoreValueChange = true;
                dgvSendCommands.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = strFields;
                cmd.Fields = strFields;
                ignoreValueChange = false;
            }
        }

        #endregion

        #region 标定指令

        private void DgvCalibrationRecvCmd_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvCalibrationRecvCmd.CurrentCell == null) return;

            int rowIndex = dgvCalibrationRecvCmd.CurrentCell.RowIndex;
            if (rowIndex == -1) return;

            dgvCalibrationRecvCmdField.Rows.Clear();

            ReceiveCommandInfoVO cmd = dgvCalibrationRecvCmd.CurrentRow.Tag as ReceiveCommandInfoVO;

            foreach (CommandFieldInfoVO field in cmd.Fields)
            {
                dgvCalibrationRecvCmdField.Rows.Add(field.Name, field.DataType.FullName, field.Use.ToString());
            }

            index2 = getIndex(cmd.Fields);
        }

        private void dgvCalibrationRecvCmdField_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreValueChange) return;

            if (dgvCalibrationRecvCmd.CurrentRow == null) return;

            ReceiveCommandInfoVO cmd = dgvCalibrationRecvCmd.CurrentRow.Tag as ReceiveCommandInfoVO;

            if (e.ColumnIndex == 0)
            {
                DataGridViewCell nameCell = dgvCalibrationRecvCmdField.Rows[e.RowIndex].Cells[0];
                string fieldName = nameCell.Value?.ToString().Trim();
                if (!verifyParamName(cmd.Fields, fieldName, out string errmsg))
                {
                    if (errmsg != null)
                        nameCell.ErrorText = errmsg;

                    ignoreValueChange = true;
                    nameCell.Value = cmd.Fields[e.RowIndex].Name;
                    ignoreValueChange = false;

                    clearErrorMsg = true;
                    dgvCalibrationRecvCmdField.BeginEdit(false);
                    clearErrorMsg = false;
                }
                else
                {
                    cmd.Fields[e.RowIndex].Name = fieldName;

                    dgvCalibrationRecvCmd.CurrentRow.Cells["colAppendData3"].Value = string.Join(",", cmd.Fields.Select(c => c.Name));
                }
            }
            else if (e.ColumnIndex == 1)
            {
                cmd.Fields[e.RowIndex].DataType = Type.GetType(dgvCalibrationRecvCmdField.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            else if (e.ColumnIndex == 2)
            {
                //cmd.Fields[e.RowIndex].Use = (CommandFieldUse)Enum.Parse(typeof(CommandFieldUse), dgvRecvCmdField.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
        }

        private void dgvCalibrationRecvCmdField_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!clearErrorMsg)
            {
                if (dgvCalibrationRecvCmdField.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText != null)
                    dgvCalibrationRecvCmdField.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
            }

            // 手眼标定的附加参数“X”,“Y”禁止改名
            ReceiveCommandInfoVO cmd = dgvCalibrationRecvCmd.CurrentRow.Tag as ReceiveCommandInfoVO;
            string fieldName = dgvCalibrationRecvCmdField.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (cmd.CalibrationType == CalibrationType.HandEye && (fieldName == "X" || fieldName == "Y"))
            {
                e.Cancel = true;
            }
        }

        private void tsmiCalibrationRecvFieldAdd_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            CommandFieldInfoVO newField = new CommandFieldInfoVO
            {
                Name = $"V{index2++}",
                DataType = tsmi.Tag as Type,
                Use = CommandFieldUse.None
            };

            dgvCalibrationRecvCmdField.Rows.Add(newField.Name, newField.DataType.FullName, newField.Use.ToString());

            ReceiveCommandInfoVO cmd = dgvCalibrationRecvCmd.CurrentRow.Tag as ReceiveCommandInfoVO;
            cmd.Fields.Add(newField);

            dgvCalibrationRecvCmd.CurrentRow.Cells["colAppendData3"].Value = string.Join(",", cmd.Fields.Select(c => c.Name));
        }

        private void tsmiCalibrationRecvFieldDelete_Click(object sender, EventArgs e)
        {
            var row = dgvCalibrationRecvCmdField.CurrentRow;
            string fieldName = row.Cells[0].Value.ToString();
            ReceiveCommandInfoVO cmd = dgvCalibrationRecvCmd.CurrentRow.Tag as ReceiveCommandInfoVO;

            if (cmd.CalibrationType == CalibrationType.HandEye && (fieldName == "X" || fieldName == "Y"))
            {
                MessageBoxUtils.ShowWarn("手眼标定必须有附加参数“X”和“Y”");
                return;
            }

            if (MessageBoxUtils.ShowQuestion("确定要删除该字段吗？") == DialogResult.No) return;

            dgvCalibrationRecvCmdField.Rows.Remove(row);

            cmd.Fields.Remove(c => c.Name == row.Cells[0].Value.ToString());

            dgvCalibrationRecvCmd.CurrentRow.Cells["colAppendData3"].Value = string.Join(",", cmd.Fields.Select(c => c.Name));
        }

        private void DgvCalibrationSendCmd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreValueChange) return;

            if (dgvCalibrationSendCmd.CurrentRow == null) return;

            SendCommandInfoVO cmd = dgvCalibrationSendCmd.CurrentRow.Tag as SendCommandInfoVO;

            if (e.ColumnIndex == 6)
            {
                string strFields = dgvCalibrationSendCmd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string[] fields = strFields.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

                strFields = string.Join(",", fields.Distinct());

                ignoreValueChange = true;
                dgvCalibrationSendCmd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = strFields;
                cmd.Fields = strFields;
                ignoreValueChange = false;
            }
        }

        #endregion

        bool verifyParamName(List<CommandFieldInfoVO> fields, string fieldName, out string errmsg)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                errmsg = "字段名不能为空";
                return false;
            }

            if (!fieldName.IsMatch(ComPattern.NamePattern))
            {
                errmsg = "参数名只能包含字母、数字和下划线，并且不能以数字开头";
                return false;
            }

            if (fields.Any(a => a.Name == fieldName))
            {
                errmsg = "字段名称已存在";
                return false;
            }

            errmsg = null;
            return true;
        }

        #region 保存

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void FrmCommandSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    Save();
            //}
            //catch
            //{
            //    e.Cancel = true;
            //    throw;
            //}
        }

        void Save()
        {
            ResetCommands();

            ResetCalibrationCommands();

            commandRepo.Save();
        }

        void ResetCommands()
        {
            dgvRecvCmdField.EndEdit();

            List<ReceiveCommandInfoVO> recvCmds = new List<ReceiveCommandInfoVO>();
            foreach (DataGridViewRow row in dgvRecvCommands.Rows)
            {
                recvCmds.Add(row.Tag as ReceiveCommandInfoVO);
            }
            commandRepo.ResetTaskRecvCommands(recvCmds);

            foreach(ReceiveCommandInfoVO commVO in recvCmds)
            {
                //add by LuoDian @ 20211228 为了在修改指令参数的时候，把ToolBlock部分的参数同步更新到ToolBlock中
                IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(commVO.TaskName);
                foreach (CommandFieldInfoVO commFieldVO in commVO.Fields)
                {
                    if (commFieldVO.Use == CommandFieldUse.ToolBlock)
                    {
                        if(!toolBlockComponent.InputContains(commFieldVO.Name))
                        {
                            ParamInfo param = new ParamInfo
                            {
                                Name = commFieldVO.Name,
                                Type = commFieldVO.DataType
                            };
                            toolBlockComponent.AddInput(@param);
                        }
                        else
                        {
                            toolBlockComponent.ChangeInputType(commFieldVO.Name, commFieldVO.DataType);
                        }
                    }
                }
                toolBlockComponentSet.Save(commVO.TaskName);
            }

            List<SendCommandInfoVO> sendCmds = new List<SendCommandInfoVO>();
            foreach (DataGridViewRow row in dgvSendCommands.Rows)
            {
                sendCmds.Add(row.Tag as SendCommandInfoVO);
            }
            commandRepo.ResetTaskSendCommands(sendCmds);
        }

        void ResetCalibrationCommands()
        {
            dgvCalibrationRecvCmdField.EndEdit();

            List<ReceiveCommandInfoVO> recvCmds = new List<ReceiveCommandInfoVO>();
            foreach (DataGridViewRow row in dgvCalibrationRecvCmd.Rows)
            {
                recvCmds.Add(row.Tag as ReceiveCommandInfoVO);
            }
            commandRepo.ResetCalibRecvCommands(recvCmds);

            List<SendCommandInfoVO> sendCmds = new List<SendCommandInfoVO>();
            foreach (DataGridViewRow row in dgvCalibrationSendCmd.Rows)
            {
                sendCmds.Add(row.Tag as SendCommandInfoVO);
            }
            commandRepo.ResetCalibSendCommands(sendCmds);
        }

        #endregion

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMenuStrip1.Enabled = btnSave.Enabled && dgvRecvCommands.CurrentCell != null;
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMenuStrip2.Enabled = btnSave.Enabled && dgvCalibrationRecvCmd.CurrentCell != null;
        }

        private void tsmiRenameCmdHeader_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)((ContextMenuStrip)(((ToolStripItem)sender).Owner)).SourceControl;

            if (dgv.CurrentCell == null) return;

            ReceiveCommandInfoVO recvCommand = (ReceiveCommandInfoVO)dgv.CurrentRow.Tag;

            FrmRenameCmdHeader frm = new FrmRenameCmdHeader(recvCommand, commandRepo);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (recvCommand.CalibrationType == null)
                {
                    foreach (DataGridViewRow row in dgvRecvCommands.Rows)
                    {
                        ReceiveCommandInfoVO cmd = (ReceiveCommandInfoVO)row.Tag;
                        if (cmd.TaskName == recvCommand.TaskName)
                        {
                            cmd.CommandHeader = frm.CmdHeader;
                            row.Cells[2].Value = frm.CmdHeader;
                        }
                    }

                    foreach (DataGridViewRow row in dgvSendCommands.Rows)
                    {
                        SendCommandInfoVO cmd = (SendCommandInfoVO)row.Tag;
                        if (cmd.TaskName == recvCommand.TaskName)
                        {
                            cmd.CommandHeader = frm.CmdHeader;
                            row.Cells[2].Value = frm.CmdHeader;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dgvCalibrationRecvCmd.Rows)
                    {
                        ReceiveCommandInfoVO cmd = (ReceiveCommandInfoVO)row.Tag;
                        if (cmd.Name == recvCommand.Name)
                        {
                            cmd.CommandHeader = frm.CmdHeader;
                            row.Cells[2].Value = frm.CmdHeader;
                        }
                    }

                    foreach (DataGridViewRow row in dgvCalibrationSendCmd.Rows)
                    {
                        SendCommandInfoVO cmd = (SendCommandInfoVO)row.Tag;
                        if (cmd.Name == recvCommand.Name)
                        {
                            cmd.CommandHeader = frm.CmdHeader;
                            row.Cells[2].Value = frm.CmdHeader;
                        }
                    }
                }
            }
        }
    }
}
