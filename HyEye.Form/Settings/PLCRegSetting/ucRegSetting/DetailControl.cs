using HyEye.Models;
using HyEye.Models.VO;
using PlcSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HyEye.WForm.Settings.PLCRegSetting
{
    public delegate void SetRegLengthDelegate(int PID, int length);
    /// <summary>
    /// 此类用来定义盛放子DataGridview的容器
    /// </summary>
    [ToolboxItem(false)]
    public class detailControl : Panel
    {
        IPLC plc;

        public SetRegLengthDelegate SetRegHandle;

        #region 字段
        public List<DataGridView> childGrid = new List<DataGridView>();
        public DataSet _cDataset;
        public int ModeIndex { get; set; }
        public CommunicationInfoVO CommunicationInfo { get; set; }

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecvFieldAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecvFieldDelete;
        #endregion

        #region 方法
        public void Add(string tableName, string strPrimaryKey, string strForeignKey)
        {
            //TabPage tPage = new TabPage() { Text = pageCaption };
            //this.Controls.Add(tPage);
            var newGrid = new ExpandableDataGridView(_cDataset, controlType.middle)
            {
                Dock = DockStyle.Fill,
                DataSource = new DataView(_cDataset.Tables[tableName])
            };
            newGrid.setParentSource(tableName, strPrimaryKey, strForeignKey);//设置主外键
            //newGrid.Name = "ChildrenMaster";
            //tPage.Controls.Add(newGrid);
            this.Controls.Add(newGrid);
            //this.BorderStyle = BorderStyle.FixedSingle;
            cModule.applyGridTheme(newGrid);
            cModule.setGridRowHeader(newGrid);
            newGrid.RowPostPaint += cModule.rowPostPaint_HeaderCount;
            childGrid.Add(newGrid);
        }

        DataGridView subGrid;
        string _tableName;
        public void Add2(string tableName)
        {
            _tableName = tableName;
            //TabPage tPage = new TabPage() { Text = pageCaption };
            //this.Controls.Add(tPage);
            subGrid = new DataGridView()
            {
                Dock = DockStyle.Fill,
                DataSource = new DataView(_cDataset.Tables[tableName])
            };

            subGrid.ContextMenuStrip = this.contextMenuStrip1;

            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.tsmiRecvFieldAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecvFieldDelete = new System.Windows.Forms.ToolStripMenuItem();

            //this.contextMenuStrip1.SuspendLayout();

            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRecvFieldAdd,
            this.tsmiRecvFieldDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // tsmiRecvFieldAdd
            // 
            this.tsmiRecvFieldAdd.Name = "tsmiRecvFieldAdd";
            this.tsmiRecvFieldAdd.Size = new System.Drawing.Size(108, 24);
            this.tsmiRecvFieldAdd.Text = "添加";
            this.tsmiRecvFieldAdd.Click += new System.EventHandler(this.tsmiRecvFieldAdd_Click);
            // 
            // tsmiRecvFieldDelete
            // 
            this.tsmiRecvFieldDelete.Name = "tsmiRecvFieldDelete";
            this.tsmiRecvFieldDelete.Size = new System.Drawing.Size(108, 24);
            this.tsmiRecvFieldDelete.Text = "删除";
            this.tsmiRecvFieldDelete.Click += new System.EventHandler(this.tsmiRecvFieldDelete_Click);

            //this.contextMenuStrip1.ResumeLayout(false);



            subGrid.CellEndEdit += new DataGridViewCellEventHandler(Detail_CellEndEdit);
            subGrid.CellBeginEdit += new DataGridViewCellCancelEventHandler(Detail_CellBeginEdit);
            subGrid.AllowUserToAddRows = false;
            //tPage.Controls.Add(subGrid);
            this.Controls.Add(subGrid);
            cModule.applyGridTheme(subGrid);
            cModule.setGridRowHeader(subGrid);
            subGrid.RowPostPaint += cModule.rowPostPaint_HeaderCount;
            subGrid.CellContentClick += new DataGridViewCellEventHandler(Detail_CellContentClick);
            subGrid.CellMouseDown += new DataGridViewCellMouseEventHandler(Detail_CellMouseDown);
            childGrid.Add(subGrid);
        }


        private void tsmiRecvFieldAdd_Click(object sender, EventArgs e)
        {
            var currentRow = subGrid.CurrentRow.Cells;
            string name = "Field" + (subGrid.Rows.Count + 1);
            CommandFieldUse use = CommandFieldUse.None;
            //SetField _setField = new SetField();
            //if (_setField.ShowDialog() == DialogResult.OK)
            //{
            //    name = _setField.FieldName;
            //    use = _setField.Fielduse;
            //}
            int lastlenth = (int)subGrid.Rows[subGrid.Rows.Count - 1].Cells["寄存器长度"].Value;
            PlcDeviceName currentPath = subGrid.Rows[subGrid.Rows.Count - 1].Cells["寄存器地址"].Value.ToString();
            _cDataset.Tables[_tableName].Rows.Add(currentRow["ID"].Value, (currentPath + lastlenth), "Short", "1", "", "1", name, use.ToString(), "附加字段");

            int regCount = 0;
            for (int i = 0; i < subGrid.Rows.Count; i++)
            {
                //计算总长度
                regCount += Convert.ToInt32(subGrid.Rows[i].Cells["寄存器长度"].Value);
            }
            int pid = Convert.ToInt32(currentRow["ID"].Value);
            SetRegHandle?.Invoke(pid, regCount);
        }

        private void tsmiRecvFieldDelete_Click(object sender, EventArgs e)
        {
            if (subGrid.CurrentRow.Index > 2)
            {
                if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow foundRow = _cDataset.Tables[_tableName].Select($"字段名称='{subGrid.CurrentRow.Cells["字段名称"].Value}'").FirstOrDefault();
                    if (foundRow != null)
                        _cDataset.Tables[_tableName].Rows.Remove(foundRow);

                    int regCount = 0;
                    for (int i = 0; i < subGrid.Rows.Count; i++)
                    {
                        //计算总长度
                        regCount += Convert.ToInt32(subGrid.Rows[i].Cells["寄存器长度"].Value);
                    }
                    int pid = Convert.ToInt32(subGrid.CurrentRow.Cells["ID"].Value);
                    SetRegHandle?.Invoke(pid, regCount);

                    //只管处理地址变化
                    PlcDeviceName startPath = subGrid.Rows[0].Cells["寄存器地址"].Value.ToString();
                    for (int i = 0; i < subGrid.Rows.Count; i++)//从当前行开始，对后面几个有影响
                    {
                        //修改当前，并赋值给下一个用做起始
                        subGrid.Rows[i].Cells["寄存器地址"].Value = startPath;
                        startPath += Convert.ToInt32(subGrid.Rows[i].Cells["寄存器长度"].Value);
                    }
                }
            }
            else
            {
                MessageBox.Show("前三行不能删除");
            }
        }

        private void Detail_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (subGrid.Rows[e.RowIndex].Selected == false)
                    {
                        subGrid.ClearSelection();
                        subGrid.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (subGrid.SelectedRows.Count == 1)
                    {
                        subGrid.CurrentCell = subGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void Detail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            plc = new Hsl(CommunicationInfo.Network);

            if (subGrid.Columns[e.ColumnIndex].HeaderText == "读操作" && e.RowIndex >= 0)
            {
                string value = "";
                string value_Type = subGrid.Rows[e.RowIndex].Cells["值类型"].Value.ToString();
                string value_Dev = subGrid.Rows[e.RowIndex].Cells["寄存器地址"].Value.ToString();
                int value_Length = Convert.ToInt32(subGrid.Rows[e.RowIndex].Cells["寄存器长度"].Value);

                try
                {
                    switch (value_Type)
                    {
                        case "Short":
                            value = string.Join("", plc.ReadBlock(value_Dev, value_Length));
                            break;
                        case "Int":
                            value = plc.ReadInt32(value_Dev).ToString();
                            break;
                        case "String":
                            value = plc.ReadString(value_Dev, value_Length);
                            break;
                    }
                    MessageBox.Show($"读取成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"读取失败：{ex.Message}");
                }

                subGrid.Rows[e.RowIndex].Cells["值"].Value = value;
            }
            if (subGrid.Columns[e.ColumnIndex].HeaderText == "写操作" && e.RowIndex >= 0)
            {
                string value = subGrid.Rows[e.RowIndex].Cells["值"].Value.ToString();
                string value_Type = subGrid.Rows[e.RowIndex].Cells["值类型"].Value.ToString();
                string value_Dev = subGrid.Rows[e.RowIndex].Cells["寄存器地址"].Value.ToString();
                int value_Length = Convert.ToInt32(subGrid.Rows[e.RowIndex].Cells["寄存器长度"].Value);

                try
                {
                    switch (value_Type)
                    {
                        case "Short":
                            {
                                short[] data = new short[value_Length];
                                string[] temp = value.Split();
                                for (int i = 0; i < value_Length; i++)
                                {
                                    data[i] = short.Parse(temp[i]);
                                }
                                plc.WriteBlock(value_Dev, data);
                            }
                            break;
                        case "Int":
                            plc.WriteInt32(value_Dev, Convert.ToInt32(value));
                            break;
                        case "String":
                            plc.WriteString(value_Dev, value);
                            break;
                    }
                    MessageBox.Show($"写入成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"写入失败：{ex.Message}");
                }
            }
        }

        string tempHeadValue = "";
        string tempDes = "";
        private void Detail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == 0 && subGrid.Columns[e.ColumnIndex].HeaderText == "值")
            {
                tempHeadValue = subGrid.Rows[e.RowIndex].Cells["值"].Value.ToString();
            }
            if (e.RowIndex == 1 && subGrid.Columns[e.ColumnIndex].HeaderText == "值")
            {
                tempHeadValue = subGrid.Rows[e.RowIndex].Cells["值"].Value.ToString();
            }
            if (e.RowIndex == 2 && subGrid.Columns[e.ColumnIndex].HeaderText == "说明")
            {
                tempDes = subGrid.Rows[e.RowIndex].Cells["说明"].Value.ToString();
            }
        }

        private void Detail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (subGrid == null)
                return;
            if (e.RowIndex < 0)
                return;

            int endIndex;
            switch (ModeIndex)
            {
                case 0:
                case 2:
                    endIndex = 2;
                    break;
                case 1:
                case 3:
                    endIndex = 3;
                    break;
                default:
                    endIndex = 2;
                    break;
            }

            if (e.RowIndex <= endIndex &&
                    (subGrid.Columns[e.ColumnIndex].HeaderText == "字段名称"
                    || subGrid.Columns[e.ColumnIndex].HeaderText == "字段用途")
                )
            {
                switch (e.RowIndex)
                {
                    case 0:
                        subGrid.Rows[e.RowIndex].Cells["字段名称"].Value = "指令头";
                        break;
                    case 1:
                        subGrid.Rows[e.RowIndex].Cells["字段名称"].Value = "拍照索引";
                        break;
                    case 2:
                        subGrid.Rows[e.RowIndex].Cells["字段名称"].Value = "指令类型";
                        break;
                    case 3:
                        subGrid.Rows[e.RowIndex].Cells["字段名称"].Value = "执行结果";
                        break;
                }
                subGrid.Rows[e.RowIndex].Cells["字段用途"].Value = "None";
                MessageBox.Show($"前{endIndex + 1}行不可编辑");
                return;
            }

            if (e.RowIndex <= endIndex && subGrid.Columns[e.ColumnIndex].HeaderText == "寄存器长度")
            {
                subGrid.Rows[e.RowIndex].Cells["寄存器长度"].Value = 1;
                MessageBox.Show($"前{endIndex + 1}行不可编辑");
                return;
            }

            if (e.RowIndex <= endIndex && subGrid.Columns[e.ColumnIndex].HeaderText == "值")
            {
                if (e.RowIndex > 1)
                {
                    subGrid.Rows[e.RowIndex].Cells["值"].Value = "";
                }
                else
                {
                    subGrid.Rows[e.RowIndex].Cells["值"].Value = tempHeadValue;
                }
                MessageBox.Show($"前{endIndex + 1}行不可编辑");
                return;
            }

            if (e.RowIndex <= endIndex && subGrid.Columns[e.ColumnIndex].HeaderText == "说明")
            {
                if (e.RowIndex == 2)
                {
                    subGrid.Rows[e.RowIndex].Cells["说明"].Value = tempDes;
                }
                else
                {
                    subGrid.Rows[e.RowIndex].Cells["说明"].Value = "";
                }
                MessageBox.Show($"前{endIndex + 1}行不可编辑");
                return;
            }

            if (e.RowIndex <= endIndex && subGrid.Columns[e.ColumnIndex].HeaderText == "值类型")
            {
                subGrid.Rows[e.RowIndex].Cells["值类型"].Value = "Short";
                MessageBox.Show($"前{endIndex + 1}行不可编辑");
                return;
            }

            if (e.RowIndex <= endIndex && subGrid.Columns[e.ColumnIndex].HeaderText == "系数")
            {
                subGrid.Rows[e.RowIndex].Cells["系数"].Value = "1";
                MessageBox.Show($"前{endIndex + 1}行不可编辑");
                return;
            }

            if (subGrid.Columns[e.ColumnIndex].HeaderText == "寄存器长度")
            {
                //只管统计长度
                int regCount = 0;
                for (int i = 0; i < subGrid.Rows.Count; i++)
                {
                    //计算总长度
                    regCount += Convert.ToInt32(subGrid.Rows[i].Cells[e.ColumnIndex].Value);
                }
                int pid = Convert.ToInt32(subGrid.Rows[e.RowIndex].Cells["ID"].Value);
                SetRegHandle?.Invoke(pid, regCount);

                //只管处理地址变化
                PlcDeviceName tempReg = subGrid.Rows[e.RowIndex].Cells["寄存器地址"].Value.ToString();
                for (int i = e.RowIndex; i < subGrid.Rows.Count; i++)//从当前行开始，对后面几个有影响
                {
                    //修改当前，并赋值给下一个用做起始
                    subGrid.Rows[i].Cells["寄存器地址"].Value = tempReg;
                    tempReg += Convert.ToInt32(subGrid.Rows[i].Cells[e.ColumnIndex].Value);
                }
            }

            if (subGrid.Columns[e.ColumnIndex].HeaderText == "值类型")
            {
                string temptype = subGrid.Rows[e.RowIndex].Cells["值类型"].Value.ToString();
                if (temptype == "Int")
                    subGrid.Rows[e.RowIndex].Cells["寄存器长度"].Value = "2";
                else
                    subGrid.Rows[e.RowIndex].Cells["寄存器长度"].Value = "1";

                //只管统计长度
                int regCount = 0;
                for (int i = 0; i < subGrid.Rows.Count; i++)
                {
                    //计算总长度
                    regCount += Convert.ToInt32(subGrid.Rows[i].Cells["寄存器长度"].Value);
                }
                int pid = Convert.ToInt32(subGrid.Rows[e.RowIndex].Cells["ID"].Value);
                SetRegHandle?.Invoke(pid, regCount);

                //只管处理地址变化
                PlcDeviceName tempReg = subGrid.Rows[e.RowIndex].Cells["寄存器地址"].Value.ToString();
                for (int i = e.RowIndex; i < subGrid.Rows.Count; i++)//从当前行开始，对后面几个有影响
                {
                    //修改当前，并赋值给下一个用做起始
                    subGrid.Rows[i].Cells["寄存器地址"].Value = tempReg;
                    tempReg += Convert.ToInt32(subGrid.Rows[i].Cells["寄存器长度"].Value);
                }
            }

            //暂留
            //if (subGrid.Columns[e.ColumnIndex].HeaderText == "寄存器地址")
            //{
            //    string tempRegPathValue = subGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //    if (tempRegPathValue.Contains('D'))
            //    {
            //        int startNum;
            //        if (int.TryParse(tempRegPathValue.Split('D')[1], out startNum))
            //        {
            //            for (int i = e.RowIndex; i < subGrid.Rows.Count; i++)//从当前行开始，对后面几个有影响
            //            {
            //                //修改当前，并赋值给下一个用做起始
            //                subGrid.Rows[i].Cells["寄存器地址"].Value = $"D{startNum}";
            //                startNum += Convert.ToInt32(subGrid.Rows[i].Cells["寄存器长度"].Value);
            //            }
            //        }
            //    }
            //}
        }

        public void RemoveControl()
        {
            this.Controls.Remove(childGrid[0]);
            childGrid.Clear();
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

    }

}
