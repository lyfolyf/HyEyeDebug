using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyVision.Tools;
using HalconSDK.Engine.BL;
using HalconSDK.Engine.DA;
using HyVision;
using System.IO;
using HyVision.Models;
using HyVision.Tools.ImageDisplay;
using GL.Kit.Log;

namespace HalconSDK.Engine.UI
{
    //public partial class HalconEngineUI : UserControl
    public partial class HalconEngineUI : BaseHyUserToolEdit<HalconProcedureEngineBL>
    {
        HalconProcedureEngineBL halconEngineBL;
        readonly IGLog log;

        public HalconEngineUI(IGLog log)
        {
            InitializeComponent();
            this.log = log;
            //dgvInputItems.DataError += dgv_DataError;
            //dgvOutputItems.DataError += dgv_DataError;

            this.hyImageDisplayControl.AddROIEvent += HyImageDisplayControl_AddROIEvent;
            this.hyImageDisplayControl.DeleteROIEvent += HyImageDisplayControl_DeleteROIEvent;
        }

        public HalconEngineUI()
        {
            InitializeComponent();
            //dgvInputItems.DataError += dgv_DataError;
            //dgvOutputItems.DataError += dgv_DataError;

            this.hyImageDisplayControl.AddROIEvent += HyImageDisplayControl_AddROIEvent;
            this.hyImageDisplayControl.DeleteROIEvent += HyImageDisplayControl_DeleteROIEvent;
        }

        private void HyImageDisplayControl_DeleteROIEvent(int roiIndex)
        {
            try
            {
                lstROI.Items.Remove(roiIndex);

                foreach (DataGridViewRow row in dgvInputItems.Rows)
                {
                    if (row.Cells[InputColumnValue.Index].Value != null && row.Cells[InputColumnValue.Index].Value.ToString().Contains(roiIndex.ToString()))
                    {
                        string[] arrVal = row.Cells[InputColumnValue.Index].Value.ToString().Split(',').Where(v => !v.Equals(roiIndex.ToString())).ToArray();
                        string val = "";
                        if (arrVal != null && arrVal.Length > 0)
                        {
                            foreach (string item in arrVal)
                            {
                                val += item + ",";
                            }
                            val = val.Remove(val.LastIndexOf(','));
                        }
                        row.Cells[InputColumnValue.Index].Value = val;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HyImageDisplayControl_AddROIEvent(int roiIndex)
        {
            try
            {
                lstROI.Items.Add(roiIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 把UI的数据写到基类的Inputs、Outputs里面去，同时会在ToolBlock的界面中添加工具的子节点，用于做数据Mapping
        /// 如果不把数据放到Inputs、Outputs里面去，则数据也可以保存到本地，但是在ToolBlock的界面中不会添加工具的子节点
        /// </summary>
        public override void UpdateDataToObject()
        {
            try
            {
                UpdateDataToObj();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void Save()
        {
            try
            {
                if (txtFilePath.Text == null || txtFilePath.Text.Equals(""))
                    throw new Exception($"需要选择要执行的 Procedure 文件！");
                if (!File.Exists(txtFilePath.Text))
                    throw new Exception($"在指定的文件夹“{txtFilePath.Text}”找不到 Procedure 文件！");
                foreach (DataGridViewRow row in dgvInputItems.Rows)
                {
                    if (row.Cells[InputColumnName.Index].Value == null ||
                        row.Cells[InputColumnName.Index].Value.ToString().Equals(""))
                        throw new Exception($"输入参数 {InputColumnName.HeaderText} 中含有非法值！");

                    if (row.Cells[InputColumnType.Index].Value == null ||
                        row.Cells[InputColumnType.Index].Value.ToString().Equals(""))
                        throw new Exception($"输入参数 {InputColumnType.HeaderText} 中含有非法值！");
                }
                foreach (DataGridViewRow row in dgvOutputItems.Rows)
                {
                    if (row.Cells[OutputColumnName.Index].Value == null ||
                        row.Cells[OutputColumnName.Index].Value.ToString().Equals(""))
                        throw new Exception($"输出参数 {OutputColumnName.HeaderText} 中含有非法值！");

                    if (row.Cells[OutputColumnType.Index].Value == null ||
                        row.Cells[OutputColumnType.Index].Value.ToString().Equals(""))
                        throw new Exception($"输出参数 {OutputColumnType.HeaderText} 中含有非法值！");
                }

                UpdateDataToObj();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 从基类的Inputs、Outputs中加载数据到UI
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HalconProcedureEngineBL Subject
        {
            get
            {
                //SaveData();
                return halconEngineBL;
            }
            set
            {
                if (!object.Equals(halconEngineBL, value))
                {
                    halconEngineBL = value;
                    txtFilePath.Text = halconEngineBL.FilePath;

                    lstROI.Items.Clear();
                    List<BaseHyROI> arrROI = new List<BaseHyROI>();

                    dgvInputItems.Rows.Clear();
                    if (halconEngineBL != null && halconEngineBL.Inputs != null && halconEngineBL.Inputs.Count > 0)
                    {
                        for (int i = 0; i < halconEngineBL.Inputs.Count; i++)
                        {
                            if (halconEngineBL.Inputs[i].GetType() != typeof(HyTerminal))
                                throw new Exception($"运行[{Name}]时发生异常", new HyVisionException($"参数 {halconEngineBL.Inputs[i].Name} 不是一个有效的 {typeof(HyTerminal)} 类型参数！当前类型：{halconEngineBL.Inputs[i].GetType()}"));

                            HyTerminal inputTerminal = (HyTerminal)halconEngineBL.Inputs[i];
                            if (inputTerminal == null || inputTerminal.Name == null ||
                                inputTerminal.Name.Equals(HalconProcedureEngineBL.DEFAULT_IMAGE_TYPE_NAME) ||
                                inputTerminal.Name.Equals(HalconProcedureEngineBL.DEFAULT_IMAGE_WIDTH_NAME) ||
                                inputTerminal.Name.Equals(HalconProcedureEngineBL.DEFAULT_IMAGE_HEIGHT_NAME))
                                continue;

                            int index = dgvInputItems.Rows.Add();
                            dgvInputItems.Rows[index].Cells[InputColumnIndex.Index].Value = index;
                            dgvInputItems.Rows[index].Cells[InputColumnName.Index].Value = inputTerminal.Name;

                            if (inputTerminal.ConvertTargetType != null && inputTerminal.ConvertTargetType.Equals(typeof(BaseHyROI).FullName))
                                dgvInputItems.Rows[index].Cells[InputColumnType.Index].Value = inputTerminal.ValueType.Name;
                            else if (inputTerminal.ConvertTargetType != null)
                                dgvInputItems.Rows[index].Cells[InputColumnType.Index].Value = inputTerminal.ConvertTargetType;
                            //if (inputTerminal.ConvertTargetType != null)
                            //    dgvInputItems.Rows[index].Cells[ColumnType.Index].Value = inputTerminal.ConvertTargetType;


                            if (inputTerminal.Value != null)
                            {
                                if (inputTerminal.ConvertTargetType != null && (inputTerminal.ConvertTargetType.ToUpper().Equals("HXLD".ToUpper()) ||
                            inputTerminal.ConvertTargetType.ToUpper().Equals("HRegion".ToUpper())))
                                {
                                    //List<BaseHyROI> arrROISingleRow = (List<BaseHyROI>)inputTerminal.Value;
                                    //if(arrROISingleRow != null && arrROISingleRow.Count > 0)
                                    //{
                                    //    string val = "";
                                    //    foreach (BaseHyROI roi in arrROISingleRow)
                                    //    {
                                    //        if (roi == null)
                                    //            continue;

                                    //        val += roi.Index + ",";
                                    //        if(!lstROI.Items.Contains(roi.Index.ToString()))
                                    //        {
                                    //            lstROI.Items.Add(roi.Index);
                                    //            arrROI.Add(roi);
                                    //        }
                                    //    }
                                    //    val = val.Remove(val.LastIndexOf(','));
                                    //    dgvInputItems.Rows[index].Cells[ColumnValue.Index].Value = val;
                                    //}
                                    BaseHyROI roi = (BaseHyROI)inputTerminal.Value;
                                    dgvInputItems.Rows[index].Cells[InputColumnValue.Index].Value = roi.Index;
                                    arrROI.Add(roi);
                                }
                                else if (inputTerminal.ConvertTargetType != null && !inputTerminal.ConvertTargetType.Equals("HObject"))
                                    dgvInputItems.Rows[index].Cells[InputColumnValue.Index].Value = inputTerminal.Value;
                            }

                            if (dgvInputItems.Rows[index].Cells[InputColumnType.Index].Value.ToString().Equals("HTuple"))
                                dgvInputItems.Rows[index].Cells[InputColumnValue.Index].ReadOnly = false;
                            else
                                dgvInputItems.Rows[index].Cells[InputColumnValue.Index].ReadOnly = true;


                            //dgvInputItems.Rows[index].Cells[ColumnAdd.Index].Value = "+";
                            //dgvInputItems.Rows[index].Cells[ColumnRemove.Index].Value = "-";
                        }
                    }

                    this.hyImageDisplayControl.SetHyROIs(arrROI);

                    dgvOutputItems.Rows.Clear();
                    if (halconEngineBL != null && halconEngineBL.Outputs != null && halconEngineBL.Outputs.Count > 0)
                    {
                        for (int i = 0; i < halconEngineBL.Outputs.Count; i++)
                        {
                            if (halconEngineBL.Outputs[i].GetType() != typeof(HyTerminal))
                                throw new Exception($"运行[{Name}]时发生异常", new HyVisionException($"参数 {halconEngineBL.Outputs[i].Name} 不是一个有效的 {typeof(HyTerminal)} 类型参数！当前类型：{halconEngineBL.Outputs[i].GetType()}"));

                            HyTerminal outputTerminal = (HyTerminal)halconEngineBL.Outputs[i];

                            int index = dgvOutputItems.Rows.Add();
                            dgvOutputItems.Rows[index].Cells[OutputColumnIndex.Index].Value = i + 1;
                            dgvOutputItems.Rows[index].Cells[OutputColumnName.Index].Value = outputTerminal.Name;
                            dgvOutputItems.Rows[index].Cells[OutputColumnType.Index].Value = outputTerminal.ConvertTargetType;
                            //dgvOutputItems.Rows[index].Cells[ColumnAdd.Index].Value = "+";
                            //dgvOutputItems.Rows[index].Cells[ColumnRemove.Index].Value = "-";
                        }
                    }

                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(File.Exists(txtFilePath.Text))
                    openFileDialog.FileName = txtFilePath.Text;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsertInputItems_Click(object sender, EventArgs e)
        {
            try
            {
                AddInputItemsToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCoverInputItems_Click(object sender, EventArgs e)
        {
            try
            {
                dgvInputItems.Rows.Clear();
                AddInputItemsToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearInputItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInputItems.SelectedCells != null && dgvInputItems.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvInputItems.SelectedCells[0].RowIndex;
                    dgvInputItems.Rows.RemoveAt(rowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvInputItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    dgvInputItems.SelectedRows[e.RowIndex].Selected = true;
            //    if (dgvInputItems.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            //    {
            //        if (e.ColumnIndex == ColumnAdd.Index)
            //        {
            //            for (int i = dgvInputItems.SelectedRows[0].Index + 2; i < dgvInputItems.RowCount; i++)
            //            {
            //                dgvInputItems.Rows[i].Cells[ColumnIndex.Index].Value = i + 2;
            //            }

            //            //DataGridViewButtonCell btnCellAdd = new DataGridViewButtonCell();
            //            //btnCellAdd.Value = "+";
            //            //row.Cells[ColumnAdd.Index].Value = btnCellAdd;
            //            //DataGridViewButtonCell btnCellRemove = new DataGridViewButtonCell();
            //            //btnCellRemove.Value = "-";
            //            //row.Cells[ColumnRemove.Index].Value = btnCellRemove;
            //            //dgvInputItems.Rows.Insert(dgvInputItems.SelectedRows[0].Index + 1, new object[] { dgvInputItems.SelectedRows[0].Index + 2, btnCellAdd, btnCellRemove });

            //            dgvInputItems.Rows.Insert(dgvInputItems.SelectedRows[0].Index + 1, new object[] { dgvInputItems.SelectedRows[0].Index + 2, "+", "-" });
            //        }

            //        if (e.ColumnIndex == ColumnRemove.Index)
            //            dgvInputItems.Rows.Remove(dgvInputItems.SelectedRows[0]);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void AddInputItemsToDataGridView()
        {
            if (txtInputItems.Lines == null || txtInputItems.Lines.Length < 1)
                return;

            foreach (string strLine in txtInputItems.Lines)
            {
                if (strLine == null || strLine.Equals("") || strLine.StartsWith("*"))
                    continue;

                string[] arrInputs = strLine.Split(',');
                foreach (string strInput in arrInputs)
                {
                    if (strInput == null || strInput.Equals("") || !strInput.Contains("="))
                        throw new Exception($"输入的Input数据 {strLine} 参数格式不正确！");

                    string[] arrParam = strInput.Split('=');
                    if (arrParam == null || arrParam.Length != 2)
                        throw new Exception($"输入的Input数据 {strLine} 参数格式不正确！");

                    string strType = arrParam[0];
                    string strName = arrParam[1];

                    if (!InputColumnType.Items.Cast<string>().Contains(strType))
                        throw new Exception($"输入的Input数据 {strLine} 参数类型不合法！");


                    DataGridViewRow row = new DataGridViewRow();
                    for(int i = 0; i < dgvInputItems.Columns.Count; i++)
                    {
                        if(dgvInputItems.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                        {
                            DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                            row.Cells.Add(textboxcell);
                        }
                        else if(dgvInputItems.Columns[i].CellType == typeof(DataGridViewComboBoxCell))
                        {
                            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                            foreach (string item in InputColumnType.Items)
                                comboBoxCell.Items.Add(item);
                            row.Cells.Add(comboBoxCell);
                        }
                        else if(dgvInputItems.Columns[i].CellType == typeof(DataGridViewButtonCell))
                        {
                            DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                            row.Cells.Add(buttonCell);
                        }
                    }

                    row.Cells[InputColumnName.Index].Value = strName;
                    row.Cells[InputColumnType.Index].Value = strType;

                    if (strType.Equals("HTuple"))
                        row.Cells[InputColumnValue.Index].ReadOnly = false;
                    else
                        row.Cells[InputColumnValue.Index].ReadOnly = true;

                    int index = dgvInputItems.Rows.Count;
                    row.Cells[InputColumnIndex.Index].Value = index;
                    dgvInputItems.Rows.Add(row);
                }
            }
        }

        private void AddOutputItemsToDataGridView()
        {
            if (txtOutputItems.Lines == null || txtOutputItems.Lines.Length < 1)
                return;

            foreach (string strLine in txtOutputItems.Lines)
            {
                if (strLine == null || strLine.Equals("") || strLine.StartsWith("*"))
                    continue;

                string[] arrOutputs = strLine.Split(',');
                foreach (string strOutput in arrOutputs)
                {
                    if (strOutput == null || strOutput.Equals("") || !strOutput.Contains("="))
                        throw new Exception($"输入的Output数据 {strLine} 参数格式不正确！");

                    string[] arrParam = strOutput.Split('=');
                    if (arrParam == null || arrParam.Length != 2)
                        throw new Exception($"输入的Output数据 {strLine} 参数格式不正确！");

                    string strType = arrParam[0];
                    string strName = arrParam[1];

                    if (!OutputColumnType.Items.Cast<string>().Contains(strType))
                        throw new Exception($"输入的Output数据 {strLine} 参数类型不合法！");


                    DataGridViewRow row = new DataGridViewRow();
                    for (int i = 0; i < dgvOutputItems.Columns.Count; i++)
                    {
                        if (dgvOutputItems.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                        {
                            DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                            row.Cells.Add(textboxcell);
                        }
                        else if (dgvOutputItems.Columns[i].CellType == typeof(DataGridViewComboBoxCell))
                        {
                            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                            foreach (string item in OutputColumnType.Items)
                                comboBoxCell.Items.Add(item);
                            row.Cells.Add(comboBoxCell);
                        }
                        else if (dgvOutputItems.Columns[i].CellType == typeof(DataGridViewButtonCell))
                        {
                            DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                            row.Cells.Add(buttonCell);
                        }
                    }

                    row.Cells[OutputColumnName.Index].Value = strName;
                    row.Cells[OutputColumnType.Index].Value = strType;
                    //DataGridViewButtonCell btnCellAdd = new DataGridViewButtonCell();
                    //btnCellAdd.Value = "+";
                    //row.Cells[ColumnAdd.Index].Value = "+";
                    //DataGridViewButtonCell btnCellRemove = new DataGridViewButtonCell();
                    //btnCellRemove.Value = "-";
                    //row.Cells[ColumnRemove.Index].Value = "-";

                    int index = dgvOutputItems.Rows.Count;
                    row.Cells[OutputColumnIndex.Index].Value = index;
                    dgvOutputItems.Rows.Add(row);


                    //DataGridViewRow rowTemp = null;
                    //if(dgvOutputItems.Rows.Count > 0 && dgvOutputItems.SelectedCells.Count > 0)
                    //    rowTemp = (DataGridViewRow)dgvOutputItems.Rows[dgvOutputItems.SelectedCells[0].RowIndex].Clone();
                    //int index;
                    //if (rowTemp == null)
                    //{
                    //    index = dgvOutputItems.Rows.Count;
                    //    row.Cells[ColumnIndex.Index].Value = index;
                    //    dgvOutputItems.Rows.Add(row);
                    //}
                    //else
                    //{
                    //    index = rowTemp.Index + 2;
                    //    for (int i = row.Index + 1; i < dgvOutputItems.RowCount; i++)
                    //    {
                    //        dgvOutputItems.Rows[i].Cells[ColumnIndex.Index].Value = i + 2;
                    //    }
                    //    row.Cells[ColumnIndex.Index].Value = index;
                    //    dgvOutputItems.Rows.Insert(index, row);
                    //}
                    //dgvOutputItems.Rows[index].Cells[0].Selected = true;
                }
            }
        }

        private void btnInsertOutputItems_Click(object sender, EventArgs e)
        {
            try
            {
                AddOutputItemsToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCoverOutputItems_Click(object sender, EventArgs e)
        {
            try
            {
                dgvOutputItems.Rows.Clear();
                AddOutputItemsToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearOutputItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOutputItems.SelectedCells != null && dgvOutputItems.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvOutputItems.SelectedCells[0].RowIndex;
                    dgvOutputItems.Rows.RemoveAt(rowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //throw new NotImplementedException();    注释这行代码
        }

        private void UpdateDataToObj()
        {
            if (halconEngineBL == null)
                halconEngineBL = new HalconProcedureEngineBL();

            halconEngineBL.FilePath = txtFilePath.Text;

            //先移除已经删除的Input
            for (int i = 0; i < halconEngineBL.Inputs.Count; i++)
            {
                bool bRemove = true;
                foreach (DataGridViewRow row in dgvInputItems.Rows)
                {
                    string name = row.Cells[InputColumnName.Index].Value.ToString();
                    if (halconEngineBL.Inputs[i].Name.Equals(name) || 
                        halconEngineBL.Inputs[i].Name.Equals(HalconProcedureEngineBL.DEFAULT_IMAGE_TYPE_NAME) ||
                        halconEngineBL.Inputs[i].Name.Equals(HalconProcedureEngineBL.DEFAULT_IMAGE_WIDTH_NAME) ||
                        halconEngineBL.Inputs[i].Name.Equals(HalconProcedureEngineBL.DEFAULT_IMAGE_HEIGHT_NAME))
                        bRemove = false;
                }
                if (bRemove)
                    halconEngineBL.Inputs.RemoveAt(i--);
            }

            //再根据数据表中的数据更新Inputs
            foreach (DataGridViewRow row in dgvInputItems.Rows)
            {
                if (row.Cells[InputColumnName.Index].Value == null ||
                    row.Cells[InputColumnName.Index].Value.ToString().Equals(""))
                {
                    log?.Warn($"运行[{Name}]时发生异常，输入参数 {InputColumnName.HeaderText} 中含有非法值！");
                    continue;
                }

                if (row.Cells[InputColumnType.Index].Value == null ||
                    row.Cells[InputColumnType.Index].Value.ToString().Equals(""))
                {
                    log?.Warn($"运行[{Name}]时发生异常，输入参数 {InputColumnType.HeaderText} 中含有非法值！");
                    continue;
                }

                string name = row.Cells[InputColumnName.Index].Value.ToString();
                string strType = row.Cells[InputColumnType.Index].Value.ToString();
                Type type;
                if (strType.Equals("HRegion") || strType.Equals("HXLD"))
                    type = typeof(BaseHyROI);
                else if (strType.Equals("HObject") || strType.Equals("HImage"))
                    type = typeof(HyImage);
                else if (strType.Equals("HTuple"))
                {
                    if(row.Cells[InputColumnValue.Index].Value == null)
                        type = typeof(string);
                    else
                    {
                        int iValue;
                        string strValue = row.Cells[InputColumnValue.Index].Value.ToString();
                        if (int.TryParse(strValue, out iValue))
                            type = typeof(int);
                        else
                        {
                            double dValue;
                            if (double.TryParse(strValue, out dValue))
                                type = typeof(double);
                            else
                                type = typeof(string);
                        }
                    }
                }
                else
                    type = typeof(string);
                string convertTargetType = row.Cells[InputColumnType.Index].Value.ToString();

                HyTerminal inputTerminal = new HyTerminal(name, type);
                inputTerminal.ConvertTargetType = convertTargetType;
                inputTerminal.GUID = Guid.NewGuid().ToString("N");

                if (row.Cells[InputColumnValue.Index].Value != null && !row.Cells[InputColumnValue.Index].Value.ToString().Equals(""))
                {
                    if (strType.Equals("HRegion") || strType.Equals("HXLD"))
                    {
                        int index = int.Parse(row.Cells[InputColumnValue.Index].Value.ToString());
                        BaseHyROI roi = this.hyImageDisplayControl.GetHyROI(index);
                        inputTerminal.Value = roi;
                    }
                    else if (strType.Equals("HTuple"))
                    {
                        int iValue;
                        string strValue = row.Cells[InputColumnValue.Index].Value.ToString();
                        if (int.TryParse(strValue, out iValue))
                            inputTerminal.Value = iValue;
                        else
                        {
                            double dValue;
                            if(double.TryParse(strValue, out dValue))
                                inputTerminal.Value = dValue;
                            else
                                inputTerminal.Value = row.Cells[InputColumnValue.Index].Value.ToString();
                        }
                    }
                }

                bool bAddNew = true;
                for(int i = 0; i < halconEngineBL.Inputs.Count; i++)
                {
                    if (halconEngineBL.Inputs[i].Name.Equals(name))
                    {
                        inputTerminal.GUID = halconEngineBL.Inputs[i].GUID;
                        inputTerminal.Description = halconEngineBL.Inputs[i].Description;
                        inputTerminal.From = halconEngineBL.Inputs[i].From;
                        halconEngineBL.Inputs[i] = inputTerminal;
                        halconEngineBL.UpdateInputOutputItem(inputTerminal.GUID, inputTerminal);
                        bAddNew = false;
                    }
                }
                if(bAddNew)
                    halconEngineBL.Inputs.Add(inputTerminal);
            }

            //先移除已经删除的Output
            for (int i = 0; i < halconEngineBL.Outputs.Count; i++)
            {
                bool bRemove = true;
                foreach (DataGridViewRow row in dgvOutputItems.Rows)
                {
                    string name = row.Cells[OutputColumnName.Index].Value.ToString();
                    if (halconEngineBL.Outputs[i].Name.Equals(name))
                        bRemove = false;
                }
                if (bRemove)
                    halconEngineBL.Outputs.RemoveAt(i--);
            }

            //再根据数据表中的数据更新Outputs
            foreach (DataGridViewRow row in dgvOutputItems.Rows)
            {
                if (row.Cells[OutputColumnName.Index].Value == null ||
                    row.Cells[OutputColumnName.Index].Value.ToString().Equals(""))
                {
                    log?.Warn($"运行[{Name}]时发生异常，输出参数 {OutputColumnName.HeaderText} 中含有非法值！");
                    continue;
                }

                if (row.Cells[OutputColumnType.Index].Value == null ||
                    row.Cells[OutputColumnType.Index].Value.ToString().Equals(""))
                {
                    log?.Warn($"运行[{Name}]时发生异常，输出参数 {InputColumnType.HeaderText} 中含有非法值！");
                    continue;
                }

                string name = row.Cells[OutputColumnName.Index].Value.ToString();
                string strType = row.Cells[OutputColumnType.Index].Value.ToString();
                Type type;
                if (strType.Equals("HRegion") || strType.Equals("HXLD"))
                    type = typeof(BaseHyROI);
                else if (strType.Equals("HObject") || strType.Equals("HImage") ||
                    strType.Equals("HObject_24") || strType.Equals("HImage_24"))
                    type = typeof(HyImage);
                else
                    type = typeof(string);
                string convertTargetType = row.Cells[OutputColumnType.Index].Value.ToString();

                HyTerminal outputTerminal = new HyTerminal(name, type);
                outputTerminal.ConvertTargetType = convertTargetType;
                outputTerminal.GUID = Guid.NewGuid().ToString("N");

                bool bAddNew = true;
                for (int i = 0; i < halconEngineBL.Outputs.Count; i++)
                {
                    if (halconEngineBL.Outputs[i].Name.Equals(name))
                    {
                        outputTerminal.GUID = halconEngineBL.Outputs[i].GUID;
                        outputTerminal.Description = halconEngineBL.Outputs[i].Description;
                        outputTerminal.From = halconEngineBL.Outputs[i].From;
                        halconEngineBL.Outputs[i] = outputTerminal;
                        halconEngineBL.UpdateInputOutputItem(outputTerminal.GUID, outputTerminal);
                        bAddNew = false;
                    }
                }
                if (bAddNew)
                    halconEngineBL.Outputs.Add(outputTerminal);
            }
        }

        private void lstROI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(lstROI.SelectedItems != null && lstROI.SelectedItems.Count > 0)
                {
                    foreach(int index in lstROI.SelectedItems)
                    {
                        hyImageDisplayControl.SetSelected(index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetROI_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInputItems.SelectedCells == null || dgvInputItems.SelectedCells.Count < 1)
                    throw new Exception("需要先选择要设置ROI的输入项！");
                if(lstROI.SelectedItems == null || lstROI.SelectedItems.Count < 1)
                    throw new Exception("需要先选择要设置的ROI！");

                string val = "";
                foreach (int item in lstROI.SelectedItems)
                {
                    val += item.ToString() + ",";
                }
                val = val.Remove(val.LastIndexOf(','));

                int rowIndex = dgvInputItems.SelectedCells[0].RowIndex;
                dgvInputItems.Rows[rowIndex].Cells[InputColumnValue.Index].Value = val;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvInputItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvInputItems.Rows[e.RowIndex].Cells[InputColumnType.Index].Value != null && (dgvInputItems.Rows[e.RowIndex].Cells[InputColumnType.Index].Value.ToString().Equals("HRegion") ||
                    dgvInputItems.Rows[e.RowIndex].Cells[InputColumnType.Index].Value.ToString().Equals("HXLD")))
                    btnSetROI.Enabled = true;
                else
                    btnSetROI.Enabled = false;

                if (dgvInputItems.Rows[e.RowIndex].Cells[InputColumnType.Index].Value.ToString().Equals("HTuple"))
                    dgvInputItems.Rows[e.RowIndex].Cells[InputColumnValue.Index].ReadOnly = false;
                else
                    dgvInputItems.Rows[e.RowIndex].Cells[InputColumnValue.Index].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
