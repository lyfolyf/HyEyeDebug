using GL.Kit.Log;
using HyVision.Models;
using HyVision.Tools.Classifier.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyVision.Tools.Classifier.UI
{
    public partial class ClassifierUI : BaseHyUserToolEdit<ClassifierBL>
    {
        ClassifierBL classifier;
        private LogPublisher log;

        public ClassifierUI()
        {
            InitializeComponent();
            log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (classifier == null)
                    classifier = new ClassifierBL();

                if (classifier.LstOriginalRuleData == null)
                    classifier.LstOriginalRuleData = new List<(bool, string, string, double)>();
                if (classifier.LstAdvanceRuleData == null)
                    classifier.LstAdvanceRuleData = new List<(bool, string)>();
                if (classifier.DicOutputRuleData == null)
                    classifier.DicOutputRuleData = new List<(string, string, string)>();

                classifier.LstOriginalRuleData.Clear();
                foreach (DataGridViewRow row in dgvOriginalRule.Rows)
                {
                    string strInputName;
                    if (row.Cells[ColumnOriginalRuleInputName.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString()))
                        strInputName = "";
                    else
                        strInputName = row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString();

                    string strOperator;
                    if (row.Cells[ColumnOriginalRuleOperator.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleOperator.Index].Value.ToString()))
                        strOperator = "";
                    else
                        strOperator = row.Cells[ColumnOriginalRuleOperator.Index].Value.ToString();

                    double dRuleVal = 0;
                    if (row.Cells[ColumnOriginalRuleThreshold.Index].Value != null && !string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString()))
                        double.TryParse(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString(), out dRuleVal);

                    bool bSelect = false;
                    if (row.Cells[ColumnOriginalRuleSelect.Index].Value != null && row.Cells[ColumnOriginalRuleSelect.Index].Value.ToString().ToUpper().Equals("TRUE"))
                        bSelect = true;

                    classifier.LstOriginalRuleData.Add((bSelect, strInputName, strOperator, dRuleVal));
                }

                classifier.LstAdvanceRuleData.Clear();
                foreach (DataGridViewRow row in dgvAdvanceRule.Rows)
                {
                    bool bSelect = false;
                    if (row.Cells[ColumnAdvanceRuleSelect.Index].Value != null && row.Cells[ColumnAdvanceRuleSelect.Index].Value.ToString().ToUpper().Equals("TRUE"))
                        bSelect = true;

                    string strRule;
                    if (row.Cells[ColumnAdvanceRule.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnAdvanceRule.Index].Value.ToString()))
                        strRule = "";
                    else
                        strRule = row.Cells[ColumnAdvanceRule.Index].Value.ToString();

                    classifier.LstAdvanceRuleData.Add((bSelect, strRule));
                }

                classifier.DicOutputRuleData.Clear();
                foreach (DataGridViewRow row in dgvOutputRule.Rows)
                {
                    string strName;
                    if (row.Cells[ColumnOutputName.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOutputName.Index].Value.ToString()))
                        strName = "";
                    else
                        strName = row.Cells[ColumnOutputName.Index].Value.ToString();

                    string strRule;
                    if (row.Cells[ColumnOutputRule.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOutputRule.Index].Value.ToString()))
                        strRule = "";
                    else
                        strRule = row.Cells[ColumnOutputRule.Index].Value.ToString();

                    string strResult;
                    if (row.Cells[ColumnResult.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnResult.Index].Value.ToString()))
                        strResult = "";
                    else
                        strResult = row.Cells[ColumnResult.Index].Value.ToString();

                    classifier.DicOutputRuleData.Add((strName, strRule, strResult));
                }


                if (classifier.Outputs == null)
                    classifier.Outputs = new TerminalCollection.HyTerminalCollection();

                //先移除已经删除的Output
                for (int i = 0; i < classifier.Outputs.Count; i++)
                {
                    bool bRemove = true;
                    foreach (DataGridViewRow row in dgvOutputRule.Rows)
                    {
                        string name = row.Cells[ColumnOutputName.Index].Value.ToString();
                        if (classifier.Outputs[i].Name.Equals($"{name}.Result") || classifier.Outputs[i].Name.Equals($"{name}.Result") ||
                            classifier.Outputs[i].Name.Equals($"{name}.Rule") || classifier.Outputs[i].Name.Equals($"{name}.Rule"))
                        {
                            bRemove = false;
                            break;
                        }
                    }
                    if (bRemove)
                        classifier.Outputs.RemoveAt(i--);
                }


                //添加输出
                foreach((string strName, string strRule, string strNGResult) in classifier.DicOutputRuleData)
                {
                    string strOutputRuleName = $"{strName}.Rule";
                    HyTerminal outputRuleTerminal = new HyTerminal(strOutputRuleName, typeof(List<string>));
                    outputRuleTerminal.GUID = Guid.NewGuid().ToString("N");
                    bool bAddNew = true;
                    for (int i = 0; i < classifier.Outputs.Count; i++)
                    {
                        if (classifier.Outputs[i].Name.Equals(strOutputRuleName))
                        {
                            outputRuleTerminal.GUID = classifier.Outputs[i].GUID;
                            outputRuleTerminal.Description = classifier.Outputs[i].Description;
                            outputRuleTerminal.From = classifier.Outputs[i].From;
                            classifier.Outputs[i] = outputRuleTerminal;
                            bAddNew = false;
                            break;
                        }
                    }
                    if (bAddNew)
                        classifier.Outputs.Add(outputRuleTerminal);

                    string strOutputResultName = $"{strName}.Result";
                    HyTerminal outputResultTerminal = new HyTerminal(strOutputResultName, typeof(List<string>));
                    outputResultTerminal.GUID = Guid.NewGuid().ToString("N");
                    bAddNew = true;
                    for (int i = 0; i < classifier.Outputs.Count; i++)
                    {
                        if (classifier.Outputs[i].Name.Equals(strOutputResultName))
                        {
                            outputResultTerminal.GUID = classifier.Outputs[i].GUID;
                            outputResultTerminal.Description = classifier.Outputs[i].Description;
                            outputResultTerminal.From = classifier.Outputs[i].From;
                            classifier.Outputs[i] = outputResultTerminal;
                            bAddNew = false;
                            break;
                        }
                    }
                    if (bAddNew)
                        classifier.Outputs.Add(outputResultTerminal);
                }
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }

        public override void Save()
        {
            try
            {
                UpdateDataToObject();
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ClassifierBL Subject
        {
            get { return classifier; }
            set
            {
                if (!object.Equals(classifier, value))
                {
                    classifier = value;
                    hyTerminalCollInputData.Subject = classifier.Inputs;

                    
                    if(classifier.LstOriginalRuleData != null && classifier.LstOriginalRuleData.Count > 0)
                    {
                        dgvOriginalRule.Rows.Clear();
                        List<string> lstValidInputName = new List<string>();
                        foreach (HyTerminal input in hyTerminalCollInputData.Subject)
                        {
                            if (input != null && (input.ValueType == typeof(int) || input.ValueType == typeof(List<int>) ||
                                input.ValueType == typeof(double) || input.ValueType == typeof(List<double>)))
                            {
                                lstValidInputName.Add(input.Name);
                            }
                        }

                        foreach ((bool bSelect, string strInputName, string strOperator, double dRuleVal) in classifier.LstOriginalRuleData)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            for (int i = 0; i < dgvOriginalRule.Columns.Count; i++)
                            {
                                if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewCheckBoxCell))
                                {
                                    DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell();
                                    checkBoxCell.Value = bSelect;
                                    row.Cells.Add(checkBoxCell);
                                }
                                else if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewComboBoxCell) && i == ColumnOriginalRuleInputName.Index)
                                {
                                    DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                                    foreach (string item in lstValidInputName)
                                        comboBoxCell.Items.Add(item);
                                    if (lstValidInputName.Contains(strInputName))
                                        comboBoxCell.Value = strInputName;
                                    row.Cells.Add(comboBoxCell);
                                }
                                else if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewComboBoxCell) && i == ColumnOriginalRuleOperator.Index)
                                {
                                    DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                                    foreach (string item in ColumnOriginalRuleOperator.Items)
                                        comboBoxCell.Items.Add(item);
                                    if (ColumnOriginalRuleOperator.Items.Contains(strOperator))
                                        comboBoxCell.Value = strOperator;
                                    row.Cells.Add(comboBoxCell);
                                }
                                else if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                                {
                                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                    textboxcell.Value = dRuleVal;
                                    row.Cells.Add(textboxcell);
                                }
                            }

                            dgvOriginalRule.Rows.Add(row);
                        }
                    }

                    if (classifier.LstAdvanceRuleData != null && classifier.LstAdvanceRuleData.Count > 0)
                    {
                        foreach ((bool bSelect, string strRule) in classifier.LstAdvanceRuleData)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            for (int i = 0; i < dgvAdvanceRule.Columns.Count; i++)
                            {
                                if (dgvAdvanceRule.Columns[i].CellType == typeof(DataGridViewCheckBoxCell))
                                {
                                    DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell();
                                    checkBoxCell.Value = bSelect;
                                    row.Cells.Add(checkBoxCell);
                                }
                                else if (dgvAdvanceRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                                {
                                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                    textboxcell.Value = strRule;
                                    row.Cells.Add(textboxcell);
                                }
                            }

                            dgvAdvanceRule.Rows.Add(row);
                        }
                    }

                    if (classifier.DicOutputRuleData != null && classifier.DicOutputRuleData.Count > 0)
                    {
                        foreach ((string strName, string strRule, string strResult) in classifier.DicOutputRuleData)
                        {
                            DataGridViewRow rowAdd = new DataGridViewRow();
                            for (int i = 0; i < dgvOutputRule.Columns.Count; i++)
                            {
                                if (dgvOutputRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                                {
                                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                    if (i == ColumnOutputRule.Index)
                                        textboxcell.Value = strRule;
                                    else if (i == ColumnResult.Index)
                                        textboxcell.Value = strResult;
                                    else if(i == ColumnOutputName.Index)
                                        textboxcell.Value = strName;
                                    rowAdd.Cells.Add(textboxcell);
                                }
                            }

                            dgvOutputRule.Rows.Add(rowAdd);
                        }
                    }  
                }
            }
        }

        private void dgvOriginalRule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //先获取有效的输入缺陷数据的名称
            List<string> lstValidInputName = new List<string>();
            if(hyTerminalCollInputData.Subject != null && hyTerminalCollInputData.Subject.Count > 0)
            {
                foreach (HyTerminal input in hyTerminalCollInputData.Subject)
                {
                    if (input != null && (input.ValueType == typeof(int) || input.ValueType == typeof(List<int>) ||
                        input.ValueType == typeof(double) || input.ValueType == typeof(List<double>)))
                    {
                        lstValidInputName.Add(input.Name);
                    }
                }
            }
            
            //要根据输入的缺陷数据的名称，实时更新表格中的输入名称这一列中的选项列表
            foreach (DataGridViewRow row in dgvOriginalRule.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    if(cell.ColumnIndex == ColumnOriginalRuleInputName.Index)
                    {
                        List<string> lstRemoveItems = new List<string>();
                        foreach(string item in ((DataGridViewComboBoxCell)cell).Items)
                        {
                            if (!lstValidInputName.Contains(item))
                                lstRemoveItems.Add(item);
                        }
                        foreach (string item in lstRemoveItems)
                            ((DataGridViewComboBoxCell)cell).Items.Remove(item);

                        foreach(string inputName in lstValidInputName)
                        {
                            if (!((DataGridViewComboBoxCell)cell).Items.Contains(inputName))
                                ((DataGridViewComboBoxCell)cell).Items.Add(inputName);
                        }
                    }
                }
            }
        }

        private void btnAddOriginalRule_Click(object sender, EventArgs e)
        {
            if(hyTerminalCollInputData.Subject == null || hyTerminalCollInputData.Subject.Count < 1)
            {
                log?.Error($"添加初始规则之前，需要先添加输入的缺陷数据！");
                return;
            }

            bool bHasNoValidInput = true;
            List<string> lstValidInputName = new List<string>();
            foreach(HyTerminal input in hyTerminalCollInputData.Subject)
            {
                if(input != null && (input.ValueType == typeof(int) || input.ValueType == typeof(List<int>) || 
                    input.ValueType == typeof(double) || input.ValueType == typeof(List<double>)))
                {
                    bHasNoValidInput = false;
                    lstValidInputName.Add(input.Name);
                }
            }
            if (bHasNoValidInput)
            {
                log?.Error($"添加初始规则之前，发现添加的输入缺陷数据中，并没有可以用于制定规则的int、double等数据类型！");
                return;
            }

            DataGridViewRow row = new DataGridViewRow();
            for (int i = 0; i < dgvOriginalRule.Columns.Count; i++)
            {
                if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewCheckBoxCell))
                {
                    DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell();
                    row.Cells.Add(checkBoxCell);
                }
                else if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewComboBoxCell) && i == ColumnOriginalRuleInputName.Index)
                {
                    DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                    foreach (string item in lstValidInputName)
                        comboBoxCell.Items.Add(item);
                    row.Cells.Add(comboBoxCell);
                }
                else if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewComboBoxCell) && i == ColumnOriginalRuleOperator.Index)
                {
                    DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                    foreach (string item in ColumnOriginalRuleOperator.Items)
                        comboBoxCell.Items.Add(item);
                    row.Cells.Add(comboBoxCell);
                }
                else if (dgvOriginalRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                {
                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                    row.Cells.Add(textboxcell);
                }
            }

            dgvOriginalRule.Rows.Add(row);
        }

        private void btnDeleteOriginalRule_Click(object sender, EventArgs e)
        {
            if (dgvOriginalRule.SelectedCells != null && dgvOriginalRule.SelectedCells.Count > 0)
            {
                int rowIndex = dgvOriginalRule.SelectedCells[0].RowIndex;
                dgvOriginalRule.Rows.RemoveAt(rowIndex);
            }
        }

        private void btnRuleAnd_Click(object sender, EventArgs e)
        {
            if(dgvAdvanceRule.SelectedCells == null || dgvAdvanceRule.SelectedCells.Count < 1)
            {
                log?.Error($"在把初始规则合并到高级规则之前，需要先选中高级规则中要添加过去的那一行中的某个单元格！");
                return;
            }

            string strAdanceRule = dgvAdvanceRule.Rows[dgvAdvanceRule.SelectedCells[0].RowIndex].Cells[ColumnAdvanceRule.Index].Value == null ? "": dgvAdvanceRule.Rows[dgvAdvanceRule.SelectedCells[0].RowIndex].Cells[ColumnAdvanceRule.Index].Value.ToString();
            if (!string.IsNullOrEmpty(strAdanceRule))
                strAdanceRule += ClassifierBL.ADVANCE_RULE_KEY_AND;

            foreach (DataGridViewRow row in dgvOriginalRule.Rows)
            {
                if(row.Cells[ColumnOriginalRuleSelect.Index].Value != null && row.Cells[ColumnOriginalRuleSelect.Index].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    if(row.Cells[ColumnOriginalRuleInputName.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的初始规则中，存在没有选择输入名称的情况！");
                        return;
                    }

                    if (row.Cells[ColumnOriginalRuleOperator.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleOperator.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的初始规则中，存在没有选择运算操作符的情况！");
                        return;
                    }

                    if (row.Cells[ColumnOriginalRuleThreshold.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString()) || 
                        !double.TryParse(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString(), out double dRuleVal))
                    {
                        log?.Error($"在选中的初始规则中，存在非法阈值的情况！");
                        return;
                    }

                    //规则的格式示例：面积.>.100 并且 数量.>.5
                    strAdanceRule += $"{row.Cells[ColumnOriginalRuleInputName.Index].Value}{ClassifierBL.RULE_SUB_KEY}{row.Cells[ColumnOriginalRuleOperator.Index].Value}{ClassifierBL.RULE_SUB_KEY}{row.Cells[ColumnOriginalRuleThreshold.Index].Value}{ClassifierBL.ADVANCE_RULE_KEY_AND}";
                }
            }

            strAdanceRule = strAdanceRule.Remove(strAdanceRule.LastIndexOf(ClassifierBL.ADVANCE_RULE_KEY_AND), ClassifierBL.ADVANCE_RULE_KEY_AND.Length);

            dgvAdvanceRule.Rows[dgvAdvanceRule.SelectedCells[0].RowIndex].Cells[ColumnAdvanceRule.Index].Value = strAdanceRule;
        }

        private void btnRuleOr_Click(object sender, EventArgs e)
        {
            if (dgvAdvanceRule.SelectedCells == null || dgvAdvanceRule.SelectedCells.Count < 1)
            {
                log?.Error($"在把初始规则合并到高级规则之前，需要先选中高级规则中要添加过去的那一行中的某个单元格！");
                return;
            }

            string strAdanceRule = dgvAdvanceRule.Rows[dgvAdvanceRule.SelectedCells[0].RowIndex].Cells[ColumnAdvanceRule.Index].Value == null ? "" : dgvAdvanceRule.Rows[dgvAdvanceRule.SelectedCells[0].RowIndex].Cells[ColumnAdvanceRule.Index].Value.ToString();
            if (!string.IsNullOrEmpty(strAdanceRule))
                strAdanceRule += ClassifierBL.ADVANCE_RULE_KEY_OR;

            foreach (DataGridViewRow row in dgvOriginalRule.Rows)
            {
                if (row.Cells[ColumnOriginalRuleSelect.Index].Value != null && row.Cells[ColumnOriginalRuleSelect.Index].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    if (row.Cells[ColumnOriginalRuleInputName.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的初始规则中，存在没有选择输入名称的情况！");
                        return;
                    }

                    if (row.Cells[ColumnOriginalRuleOperator.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleOperator.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的初始规则中，存在没有选择运算操作符的情况！");
                        return;
                    }

                    if (row.Cells[ColumnOriginalRuleThreshold.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString()) ||
                        !double.TryParse(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString(), out double dRuleVal))
                    {
                        log?.Error($"在选中的初始规则中，存在非法阈值的情况！");
                        return;
                    }

                    strAdanceRule += $"{row.Cells[ColumnOriginalRuleInputName.Index].Value}{ClassifierBL.RULE_SUB_KEY}{row.Cells[ColumnOriginalRuleOperator.Index].Value}{ClassifierBL.RULE_SUB_KEY}{row.Cells[ColumnOriginalRuleThreshold.Index].Value}{ClassifierBL.ADVANCE_RULE_KEY_OR}";
                }
            }

            strAdanceRule = strAdanceRule.Remove(strAdanceRule.LastIndexOf(ClassifierBL.ADVANCE_RULE_KEY_OR), ClassifierBL.ADVANCE_RULE_KEY_OR.Length);

            dgvAdvanceRule.Rows[dgvAdvanceRule.SelectedCells[0].RowIndex].Cells[ColumnAdvanceRule.Index].Value = strAdanceRule;
        }

        private void btnAddAdvanceRule_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            for (int i = 0; i < dgvAdvanceRule.Columns.Count; i++)
            {
                if (dgvAdvanceRule.Columns[i].CellType == typeof(DataGridViewCheckBoxCell))
                {
                    DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell();
                    row.Cells.Add(checkBoxCell);
                }
                else if (dgvAdvanceRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                {
                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                    row.Cells.Add(textboxcell);
                }
            }

            dgvAdvanceRule.Rows.Add(row);
        }

        private void btnDeleteAdvanceRule_Click(object sender, EventArgs e)
        {
            if (dgvAdvanceRule.SelectedCells != null && dgvAdvanceRule.SelectedCells.Count > 0)
            {
                int rowIndex = dgvAdvanceRule.SelectedCells[0].RowIndex;
                dgvAdvanceRule.Rows.RemoveAt(rowIndex);
            }
        }

        private void btnDeleteOutputRule_Click(object sender, EventArgs e)
        {
            if (dgvOutputRule.SelectedCells != null && dgvOutputRule.SelectedCells.Count > 0)
            {
                int rowIndex = dgvOutputRule.SelectedCells[0].RowIndex;

                //object Result = dgvOutputRule.Rows[rowIndex].Cells[ColumnOutputRule.Index].Value;
                //if(Result != null)
                //    classifier.Outputs.Remove(Result.ToString());

                dgvOutputRule.Rows.RemoveAt(rowIndex);
            }
        }

        private void btnRuleDirectOut_Click(object sender, EventArgs e)
        {
            //规则的格式示例：面积.>.100
            foreach (DataGridViewRow row in dgvOriginalRule.Rows)
            {
                if (row.Cells[ColumnOriginalRuleSelect.Index].Value != null && row.Cells[ColumnOriginalRuleSelect.Index].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    if (row.Cells[ColumnOriginalRuleInputName.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的初始规则中，存在没有选择输入名称的情况！");
                        return;
                    }

                    if (row.Cells[ColumnOriginalRuleOperator.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleOperator.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的初始规则中，存在没有选择运算操作符的情况！");
                        return;
                    }

                    if (row.Cells[ColumnOriginalRuleThreshold.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString()) ||
                        !double.TryParse(row.Cells[ColumnOriginalRuleThreshold.Index].Value.ToString(), out double dRuleVal))
                    {
                        log?.Error($"在选中的初始规则中，存在非法阈值的情况！");
                        return;
                    }

                    string strAdanceRule = $"{row.Cells[ColumnOriginalRuleInputName.Index].Value}{ClassifierBL.RULE_SUB_KEY}{row.Cells[ColumnOriginalRuleOperator.Index].Value}{ClassifierBL.RULE_SUB_KEY}{row.Cells[ColumnOriginalRuleThreshold.Index].Value}";

                    DataGridViewRow rowAdd = new DataGridViewRow();
                    for (int i = 0; i < dgvOutputRule.Columns.Count; i++)
                    {
                        if (dgvOutputRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                        {
                            DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                            if (i == ColumnOutputRule.Index)
                                textboxcell.Value = strAdanceRule;
                            rowAdd.Cells.Add(textboxcell);
                        }
                    }

                    dgvOutputRule.Rows.Add(rowAdd);

                    //if(!classifier.Outputs.Contains(row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString()))
                    //{
                    //    HyTerminal output = new HyTerminal(row.Cells[ColumnOriginalRuleInputName.Index].Value.ToString(), typeof(string));
                    //    classifier.Outputs.Add(output);
                    //}
                }
            }
        }

        private void btnAdvanceRuleOut_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvAdvanceRule.Rows)
            {
                if (row.Cells[ColumnAdvanceRuleSelect.Index].Value != null && row.Cells[ColumnAdvanceRuleSelect.Index].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    if (row.Cells[ColumnAdvanceRule.Index].Value == null || string.IsNullOrEmpty(row.Cells[ColumnAdvanceRule.Index].Value.ToString()))
                    {
                        log?.Error($"在选中的高级规则中，存在没有设定规则的情况！");
                        return;
                    }

                    string strAdanceRule = row.Cells[ColumnAdvanceRule.Index].Value.ToString();

                    DataGridViewRow rowAdd = new DataGridViewRow();
                    for (int i = 0; i < dgvOutputRule.Columns.Count; i++)
                    {
                        if (dgvOutputRule.Columns[i].CellType == typeof(DataGridViewTextBoxCell))
                        {
                            DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                            if (i == ColumnOutputRule.Index)
                                textboxcell.Value = strAdanceRule;
                            rowAdd.Cells.Add(textboxcell);
                        }
                    }

                    dgvOutputRule.Rows.Add(rowAdd);

                    //if (!classifier.Outputs.Contains(strAdanceRule))
                    //{
                    //    HyTerminal output = new HyTerminal(strAdanceRule, typeof(string));
                    //    classifier.Outputs.Add(output);
                    //}
                }
            }
        }
    }
}
