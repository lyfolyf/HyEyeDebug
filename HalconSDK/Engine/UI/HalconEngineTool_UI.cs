using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

using HyVision.Tools;
using HyVision.Models;
using HyRoiManager;

using HalconDotNet;
using HalconSDK.Engine.BL;

namespace HalconSDK.Engine.UI
{
    //public partial class HalconEngineTool_UI : UserControl
    public partial class HalconEngineTool_UI : BaseHyUserToolEdit<HalconEngineTool>
    {
        private HalconEngineTool HalconEnginetool;

        public HalconEngineTool_UI()
        {
            InitializeComponent();
        }

        private void HalconEngineTool_UI_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvFunctionParam.Columns.Count; i++)
            {
                dgvFunctionParam.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvFunctionParam.Columns[dgvFunctionParam.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgvFunctionParam.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgvFunctionParam.Columns[0].Width = 50;
            dgvFunctionParam.Columns[1].Width = 200;
            dgvFunctionParam.Columns[2].Width = 90;
            dgvFunctionParam.Columns[3].Width = 200;
            dgvFunctionParam.Columns[4].Width = 100;
            dgvFunctionParam.Columns[5].Width = 90;
            dgvFunctionParam.Columns[6].Width = 80;

            HalconEnginetool.IsInitialization = false;
            if (HalconEnginetool != null)
            {
                cbxDebugModel.CheckedChanged -= cbxDebugModel_CheckedChanged;
                cbxDebugModel.Checked = HalconEnginetool.IsDebugMode;
                cbxDebugModel.CheckedChanged += cbxDebugModel_CheckedChanged;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HalconEngineTool Subject
        {
            get
            {
                return HalconEnginetool;
            }
            set
            {
                HalconEnginetool = value;

                tbxHalconFilePath.Text = HalconEnginetool.HalconFilePath;
                tabControl1.SelectedIndex = HalconEnginetool.UISelectedIndex;
                LoadFunctionList(HalconEnginetool.HalconFilePath);
                LoadSelectedFunction(HalconEnginetool.SelectFuncNames);

                //更新表格
                dgvFunctionParam.CellValueChanged -= dgvFunctionParam_CellValueChanged;
                UpdateDataGridView(HalconEnginetool.SelectFuncNames);
                CompareValue();
                dgvFunctionParam.CellValueChanged += dgvFunctionParam_CellValueChanged;
            }
        }

        public override void UpdateDataToObject()
        {
            //1.保存参数列表和函数列表
            btnSave_Click(null, null);

            //2.刷新外层连线的输入和输出
            List<string> NewInputNames = new List<string>();
            List<string> NewOutputNames = new List<string>();
            for (int i = 0; i < HalconEnginetool.Parameters.Count; i++)
            {
                ParameterInfo Info = HalconEnginetool.Parameters[i];
                string FuncParamName = $"{Info.FunctionName},{Info.ParamName}";

                if (Info.IsConnect == true)
                {
                    if (Info.InputOutputType == "输入")
                    {
                        NewInputNames.Add(FuncParamName);
                        HyTerminal OldInput = HalconEnginetool.Inputs.FirstOrDefault(t =>
                        t.Description == FuncParamName);
                        if (OldInput == null )
                        {
                            HyTerminal NewInput = ParameterInfoToHyTerminal(Info);
                            HalconEnginetool.Inputs.Add(NewInput);
                        }
                        else if (OldInput != null && OldInput.ValueType != DataTypeToType(Info.DataType))
                        {
                            HalconEnginetool.Inputs.Remove(OldInput.Name);

                            HyTerminal NewInput = ParameterInfoToHyTerminal(Info);
                            HalconEnginetool.Inputs.Add(NewInput);
                        }
                    }
                    else
                    {
                        NewOutputNames.Add(FuncParamName);
                        HyTerminal OldOutput = HalconEnginetool.Outputs.FirstOrDefault(t =>
                        t.Description == FuncParamName);
                        if (OldOutput == null)
                        {
                            HyTerminal NewOutput = ParameterInfoToHyTerminal(Info);
                            HalconEnginetool.Outputs.Add(NewOutput);
                        }
                        else if (OldOutput != null && OldOutput.ValueType != DataTypeToType(Info.DataType))
                        {
                            HalconEnginetool.Inputs.Remove(OldOutput.Name);

                            HyTerminal NewOutput = ParameterInfoToHyTerminal(Info);
                            HalconEnginetool.Outputs.Add(NewOutput);
                        }
                    }
                }
            }

            for (int i = 0; i < HalconEnginetool.Inputs.Count; i++)
            {
                HyTerminal ht = HalconEnginetool.Inputs[i];
                string str = NewInputNames.Find(n => n == ht.Description);
                if (str == null)
                {
                    HalconEnginetool.Inputs.Remove(ht.Name);
                    i -= 1;
                }
            }

            for (int i = 0; i < HalconEnginetool.Outputs.Count; i++)
            {
                HyTerminal ht = HalconEnginetool.Outputs[i];
                string str = NewOutputNames.Find(n => n == ht.Description);
                if (str == null)
                {
                    HalconEnginetool.Outputs.Remove(ht.Name);
                    i -= 1;
                }
            }
        }

        public override void Save()
        {

        }


        private Type DataTypeToType(string dataType)
        {
            Type retDataType = null;
            if (dataType == HalconDataType.Image.ToString())
            {
                retDataType = typeof(HyImage);
            }
            else if (dataType == HalconDataType.Region.ToString())
            {
                retDataType = typeof(HyRoiManager.RoiData);
            }
            else if (dataType == HalconDataType.XLD.ToString())
            {
                retDataType = typeof(HyRoiManager.RoiData);
            }
            else if (dataType == HalconDataType.Int.ToString())
            {
                retDataType = typeof(int);
            }
            else if (dataType == HalconDataType.Double.ToString())
            {
                retDataType = typeof(double);
            }
            else if (dataType == HalconDataType.Bool.ToString())
            {
                retDataType = typeof(bool);
            }
            else if (dataType == HalconDataType.String.ToString())
            {
                retDataType = typeof(string);
            }
            else if (dataType == HalconDataType.List_Image.ToString())
            {
                retDataType = typeof(List<HyImage>);
            }
            else if (dataType == HalconDataType.List_Int.ToString())
            {
                retDataType = typeof(List<int>);
            }
            else if (dataType == HalconDataType.List_Double.ToString())
            {
                retDataType = typeof(List<double>);
            }
            else if (dataType == HalconDataType.List_Bool.ToString())
            {
                retDataType = typeof(List<bool>);
            }
            else if (dataType == HalconDataType.List_String.ToString())
            {
                retDataType = typeof(List<string>);
            }
            else if (dataType == HalconDataType.ImagePointer.ToString())
            {
                retDataType = typeof(IntPtr);
            }

            return retDataType;
        }

        private HyTerminal ParameterInfoToHyTerminal(ParameterInfo ParamInfo)
        {
            Type dataType = null;
            if (ParamInfo.DataType == HalconDataType.Image.ToString())
            {
                dataType = typeof(HyImage);
            }
            else if (ParamInfo.DataType == HalconDataType.Region.ToString())
            {
                //dataType = typeof(HyVision.Tools.ImageDisplay.BaseHyROI);
                //dataType = typeof(List<HyDisplayWindow.ROI.HyDefectXLD>);

                dataType = typeof(HyRoiManager.RoiData);
            }
            else if (ParamInfo.DataType == HalconDataType.XLD.ToString())
            {
                //dataType = typeof(HyVision.Tools.ImageDisplay.BaseHyROI);
                //dataType = typeof(HyDisplayWindow.ROI.HyDefectXLD);

                dataType = typeof(HyRoiManager.RoiData);
            }
            else if (ParamInfo.DataType == HalconDataType.Int.ToString())
            {
                dataType = typeof(int);
            }
            else if (ParamInfo.DataType == HalconDataType.Double.ToString())
            {
                dataType = typeof(double);
            }
            else if (ParamInfo.DataType == HalconDataType.Bool.ToString())
            {
                dataType = typeof(bool);
            }
            else if (ParamInfo.DataType == HalconDataType.String.ToString())
            {
                dataType = typeof(string);
            }
            else if (ParamInfo.DataType == HalconDataType.List_Image.ToString())
            {
                dataType = typeof(List<HyImage>);
            }
            else if (ParamInfo.DataType == HalconDataType.List_Int.ToString())
            {
                dataType = typeof(List<int>);
            }
            else if (ParamInfo.DataType == HalconDataType.List_Double.ToString())
            {
                dataType = typeof(List<double>);
            }
            else if (ParamInfo.DataType == HalconDataType.List_Bool.ToString())
            {
                dataType = typeof(List<bool>);
            }
            else if (ParamInfo.DataType == HalconDataType.List_String.ToString())
            {
                dataType = typeof(List<string>);
            }
            //add by LuoDian @ 20220118 添加一个IntPtr类型的输出数据
            else if (ParamInfo.DataType == HalconDataType.ImagePointer.ToString())
            {
                dataType = typeof(IntPtr);
            }


            HyTerminal hyTerminal = new HyTerminal(ParamInfo.ParamName, dataType);
            if (dataType == typeof(HyRoiManager.RoiData))
            {
                hyTerminal.Value = new RoiData();
            }
            hyTerminal.GUID = Guid.NewGuid().ToString("N");
            hyTerminal.Description = $"{ParamInfo.FunctionName},{ParamInfo.ParamName}";
            return hyTerminal;
        }

        private void dgvFunctionParam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 5)
            {
                if (dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    return;
                }

                if (dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "设置")
                {
                    ParameterInfo ParamInfo = dgvFunctionParam.Rows[e.RowIndex].Tag as ParameterInfo;

                    HyRoiManager.UI.FrmRoiSeting RoiSeting = null;
                    if (ParamInfo.Roidata != null)
                    {
                        RoiSeting = new HyRoiManager.UI.FrmRoiSeting(ParamInfo.Roidata);
                    }
                    else
                    {
                        RoiSeting = new HyRoiManager.UI.FrmRoiSeting();
                    }

                    if (!string.IsNullOrEmpty(ParamInfo.ImagePath))
                    {
                        RoiSeting.DisplayImage(ParamInfo.ImagePath);
                    }

                    if (RoiSeting.ShowDialog() == DialogResult.OK)
                    {
                        ParamInfo.Roidata = RoiSeting.RoiController.GetPaintingImageRoiData();
                        ParamInfo.ImagePath = RoiSeting.GetImagePath();
                    }




                    //HyDisplayWindow.UI.FrmRoiSeting frmRoiSeting = new HyDisplayWindow.UI.FrmRoiSeting(ParamInfo.Roidata, null);
                    //frmRoiSeting.ShowDialog();
                    //if (frmRoiSeting.Modify)
                    //{
                    //    ParamInfo.Roidata = frmRoiSeting.GetRoiData();
                    //}
                    //frmRoiSeting.Dispose();
                }
            }
        }

        private void dgvFunctionParam_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (HalconEnginetool == null) return;

            //根据参数类型确定值的表格是下拉框、按钮还是文本框
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                if (dgvFunctionParam.Rows[e.RowIndex].Cells[2].Value.ToString() != "输入")
                {
                    return;
                }
                string DataType = dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (DataType == HalconDataType.Region.ToString())
                {
                    DataGridViewButtonCell btnSeting = new DataGridViewButtonCell();
                    btnSeting.Value = "设置";
                    dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = btnSeting;
                }
                else if (DataType == HalconDataType.Bool.ToString())
                {
                    DataGridViewComboBoxCell cbxBool = new DataGridViewComboBoxCell
                    {
                        Style = this.dgvFunctionParam.DefaultCellStyle
                    };
                    cbxBool.Items.Add("true");
                    cbxBool.Items.Add("false");
                    cbxBool.Value = "false";
                    dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = cbxBool;
                }
                else if (DataType == HalconDataType.内部输出.ToString())
                {
                    dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = GetInnerOutputItem(e.RowIndex);
                }
                else
                {
                    if (dgvFunctionParam.Rows[e.RowIndex].Cells[5].GetType() != typeof(DataGridViewTextBoxCell))
                    {
                        DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
                        dgvFunctionParam.Rows[e.RowIndex].Cells[5] = textBoxCell;
                        dgvFunctionParam.Rows[e.RowIndex].Cells[5].Value = "0";
                    }
                }
            }
        }

        private DataGridViewComboBoxCell GetInnerOutputItem(int CurrentRowIndex)
        {
            DataGridViewComboBoxCell cbxInnerOutput = new DataGridViewComboBoxCell
            {
                Style = this.dgvFunctionParam.DefaultCellStyle
            };

            string CurrentFuncName = dgvFunctionParam.Rows[CurrentRowIndex].Cells[1].Value.ToString();
            for (int i = 0; i < CurrentRowIndex; i++)
            {
                string Funcname = dgvFunctionParam.Rows[i].Cells[1].Value.ToString();

                if (CurrentFuncName != Funcname)
                {
                    string inputOutputTpye = dgvFunctionParam.Rows[i].Cells[2].Value.ToString();

                    if (inputOutputTpye == "输入")
                    {
                        continue;
                    }
                    else
                    {
                        string funcIndex = dgvFunctionParam.Rows[i].Cells[0].Value.ToString();
                        string paramName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString();
                        cbxInnerOutput.Items.Add($"{funcIndex},{paramName}");
                    }
                }
                else
                {
                    break;
                }
            }
            return cbxInnerOutput;
        }

        private void dgvFunctionParam_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex <= 2 && e.RowIndex != -1)
            {
                int count = 0, UpRows = 0, DownRows = 0;
                string CellValue = e.Value == null ? "" : e.Value.ToString();
                Brush gridBrush = new SolidBrush(dgvFunctionParam.GridColor);
                Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor);

                for (int i = e.RowIndex; i < dgvFunctionParam.Rows.Count; i++)
                {
                    if (CellValue == dgvFunctionParam.Rows[i].Cells[e.ColumnIndex].Value.ToString())
                    {
                        DownRows += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = e.RowIndex; i >= 0; i--)
                {
                    if (CellValue == dgvFunctionParam.Rows[i].Cells[e.ColumnIndex].Value.ToString())
                    {
                        UpRows++;
                    }
                    else
                    {
                        break;
                    }
                }

                count = DownRows + UpRows - 1;
                if (count < 2)
                {
                    return;
                }

                e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                e.Graphics.DrawLine(new Pen(gridBrush), e.CellBounds.Right - 1, e.CellBounds.Top,
                e.CellBounds.Right - 1, e.CellBounds.Bottom);

                if (DownRows == 1)
                {
                    e.Graphics.DrawLine(new Pen(gridBrush), e.CellBounds.Left, e.CellBounds.Bottom - 1,
                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }

                int fontheight = (int)e.Graphics.MeasureString(CellValue, e.CellStyle.Font).Height;
                int fontwidth = (int)e.Graphics.MeasureString(CellValue, e.CellStyle.Font).Width;
                int cellheight = e.CellBounds.Height;
                int cellwidth = e.CellBounds.Width;
                e.Graphics.DrawString(CellValue, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor),
                    e.CellBounds.X + (cellwidth - fontwidth) / 2,
                    e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);

                e.Handled = true;
            }
        }

        //保存表格的数据到  HalconEngineModel.lstParameters
        private void SaveParameters()
        {
            for (int i = 0; i < dgvFunctionParam.Rows.Count; i++)
            {
                if (dgvFunctionParam.Rows[i].Cells[4].Value == null ||
                    dgvFunctionParam.Rows[i].Cells[5].Value == null)
                {
                    dgvFunctionParam.Rows[i].Selected = true;
                    dgvFunctionParam.FirstDisplayedScrollingRowIndex = i;
                    MessageBox.Show($"第{i}列有值为空，请重新配置！", "值为空", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                ParameterInfo ParamInfo = HalconEnginetool.Parameters[i];
                ParamInfo.RowIndex = i.ToString();
                ParamInfo.FuncIndex = dgvFunctionParam.Rows[i].Cells[0].Value.ToString();
                ParamInfo.FunctionName = dgvFunctionParam.Rows[i].Cells[1].Value.ToString();
                ParamInfo.InputOutputType = dgvFunctionParam.Rows[i].Cells[2].Value.ToString();
                ParamInfo.ParamName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString();
                ParamInfo.DataType = dgvFunctionParam.Rows[i].Cells[4].Value.ToString();
                ParamInfo.Value = dgvFunctionParam.Rows[i].Cells[5].Value.ToString();
                ParamInfo.IsConnect = (bool)((DataGridViewCheckBoxCell)dgvFunctionParam.Rows[i].Cells[6]).EditedFormattedValue;

                ParamInfo.MappingInfo = "";
                if (ParamInfo.DataType == HalconDataType.内部输出.ToString())
                {
                    string[] NameInfo = ParamInfo.Value.Split(',');
                    int FuncNameIndex = int.Parse(NameInfo[0]) - 1;
                    ParamInfo.MappingInfo = HalconEnginetool.SelectFuncNames[FuncNameIndex] +
                        "," + NameInfo[1];
                }
            }
        }

        //根据HalconEngineModel.Parameters（给参数类型、值、是否连线 赋值）
        private void CompareValue(bool Sync = true)
        {
            for (int i = 0; i < dgvFunctionParam.Rows.Count; i++)
            {
                string funcName = dgvFunctionParam.Rows[i].Cells[1].Value.ToString();
                string paramName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString();
                ParameterInfo ParamInfo = HalconEnginetool.Parameters.FirstOrDefault(p =>
                p.FunctionName == funcName && p.ParamName == paramName);

                if (ParamInfo == null)
                {
                    ParameterInfo Info = new ParameterInfo
                    {
                        RowIndex = i.ToString(),
                        FuncIndex = dgvFunctionParam.Rows[i].Cells[0].Value.ToString(),
                        FunctionName = dgvFunctionParam.Rows[i].Cells[1].Value.ToString(),
                        InputOutputType = dgvFunctionParam.Rows[i].Cells[2].Value.ToString(),
                        ParamName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString(),
                        DataType = dgvFunctionParam.Rows[i].Cells[4].Value.ToString(),
                        Value = dgvFunctionParam.Rows[i].Cells[5].Value.ToString(),
                        IsConnect = (bool)((DataGridViewCheckBoxCell)dgvFunctionParam.Rows[i].Cells[6]).EditedFormattedValue,
                    };
                    HalconEnginetool.Parameters.Insert(i, Info);
                }
                else
                {
                    string dataType = ParamInfo.DataType;
                    string inputOutType = ParamInfo.InputOutputType;

                    //根据输入输出类型，设置列表第6列的值
                    if (inputOutType == "输入")
                    {
                        if (dataType == HalconDataType.Region.ToString())
                        {
                            DataGridViewButtonCell btnSeting = new DataGridViewButtonCell
                            {
                                Value = "设置"
                            };
                            dgvFunctionParam.Rows[i].Cells[5] = btnSeting;
                        }
                        else if (dataType == HalconDataType.Bool.ToString())
                        {
                            DataGridViewComboBoxCell cbxBool = new DataGridViewComboBoxCell
                            {
                                Style = this.dgvFunctionParam.DefaultCellStyle
                            };
                            cbxBool.Items.Add("true");
                            cbxBool.Items.Add("false");
                            cbxBool.Value = ParamInfo.Value;
                            dgvFunctionParam.Rows[i].Cells[5] = cbxBool;
                        }
                        else if (dataType == HalconDataType.内部输出.ToString())
                        {
                            DataGridViewComboBoxCell cbxInnerOuput = GetInnerOutputItem(i);
                            int index = cbxInnerOuput.Items.IndexOf(ParamInfo.Value);
                            if (index != -1)
                            {
                                cbxInnerOuput.Value = ParamInfo.Value;
                            }
                            dgvFunctionParam.Rows[i].Cells[5] = cbxInnerOuput;
                        }
                        else
                        {
                            dgvFunctionParam.Rows[i].Cells[4].Value = ParamInfo.DataType;
                            dgvFunctionParam.Rows[i].Cells[5].Value = ParamInfo.Value;
                        }
                    }
                    else
                    {
                        dgvFunctionParam.Rows[i].Cells[5].Value = ParamInfo.Value;
                    }
                    dgvFunctionParam.Rows[i].Cells[4].Value = ParamInfo.DataType;
                    dgvFunctionParam.Rows[i].Cells[6].Value = ParamInfo.IsConnect;

                    //让顺序对齐
                    if (i.ToString() != ParamInfo.RowIndex)
                    {
                        HalconEnginetool.Parameters.Remove(ParamInfo);
                        HalconEnginetool.Parameters.Insert(i, ParamInfo);
                    }
                }
                dgvFunctionParam.Rows[i].Tag = HalconEnginetool.Parameters[i];
            }

            //删除多余的参数
            if (Sync == true)
            {
                int ParamCount = HalconEnginetool.Parameters.Count;
                int RowCount = dgvFunctionParam.Rows.Count;
                if (ParamCount > RowCount)
                {
                    HalconEnginetool.Parameters.RemoveRange(RowCount, ParamCount - RowCount);
                }
            }
        }


        //根据已选函数，更新表格，此时表格的值都是默认值
        private void UpdateDataGridView(List<string> SelectFuncNames)
        {
            string folderPath = Directory.GetCurrentDirectory();

            dgvFunctionParam.Rows.Clear();
            HDevProcedure Procedure = new HDevProcedure();
            for (int i = 0; i < SelectFuncNames.Count; i++)
            {
                try
                {
                    Procedure.LoadProcedure(SelectFuncNames[i]);
                    //当前函数图形变量、控制变量的输入
                    HTuple InputIconicParms = Procedure.GetInputIconicParamNames();
                    int InputIconicParmCount = InputIconicParms.TupleLength();
                    for (int j = 0; j < InputIconicParmCount; j++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输入", InputIconicParms[j].S);
                    }

                    HTuple InputCtrlParms = Procedure.GetInputCtrlParamNames();
                    int InputCtrlParmCount = InputCtrlParms.TupleLength();
                    for (int j = 0; j < InputCtrlParmCount; j++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输入", InputCtrlParms[j].S);
                    }

                    //当前函数图形变量、控制变量的输出
                    HTuple OutputIconicParms = Procedure.GetOutputIconicParamNames();
                    int OutputIconicParmCount = OutputIconicParms.TupleLength();
                    for (int k = 0; k < OutputIconicParmCount; k++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输出", OutputIconicParms[k].S);
                    }

                    HTuple OutputCtrlParms = Procedure.GetOutputCtrlParamNames();
                    int OutputCtrlParmCount = OutputCtrlParms.TupleLength();
                    for (int k = 0; k < OutputCtrlParmCount; k++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输出", OutputCtrlParms[k].S);
                    }

                    //无参数函数，一般不存在这种情况
                    if (InputIconicParmCount == 0 && InputCtrlParmCount == 0
                        && OutputIconicParmCount == 0 && OutputCtrlParmCount == 0)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "-", "-");
                    }
                }
                catch (Exception ex)
                {
                    SelectFuncNames.Remove(SelectFuncNames[i]);
                    continue;
                }
            }

            Directory.SetCurrentDirectory(folderPath);
        }

        private void AddNewRow(string FunIndex, string FunName, string InputOutputType, string ParmName)
        {
            DataGridViewButtonCell btnSeting = new DataGridViewButtonCell
            {
                Value = "设置"
            };

            DataGridViewComboBoxCell cbxParmType = new DataGridViewComboBoxCell
            {
                Style = this.dgvFunctionParam.DefaultCellStyle
            };
            foreach (string Name in Enum.GetNames(typeof(HalconDataType)))
            {
                cbxParmType.Items.Add(Name);
            }

            DataGridViewComboBoxCell cbxBool = new DataGridViewComboBoxCell()
            {
                Style = this.dgvFunctionParam.DefaultCellStyle
            };
            cbxBool.Items.Add("true");
            cbxBool.Items.Add("false");
            cbxBool.Value = "false";

            //序号、函数名、输入输出类型、参数名、值
            int rowIndex = dgvFunctionParam.Rows.Add();
            dgvFunctionParam.Rows[rowIndex].Cells[0].ReadOnly = true;
            dgvFunctionParam.Rows[rowIndex].Cells[1].ReadOnly = true;
            dgvFunctionParam.Rows[rowIndex].Cells[2].ReadOnly = true;
            dgvFunctionParam.Rows[rowIndex].Cells[3].ReadOnly = true;
            //设置cell0 、cell1、cell2
            dgvFunctionParam.Rows[rowIndex].Cells[0].Value = FunIndex;
            dgvFunctionParam.Rows[rowIndex].Cells[1].Value = FunName;
            dgvFunctionParam.Rows[rowIndex].Cells[2].Value = InputOutputType;
            if (InputOutputType == "-")
            {
                dgvFunctionParam.Rows[rowIndex].Cells[3].Value = "-";
                dgvFunctionParam.Rows[rowIndex].Cells[4].Value = "-";
                dgvFunctionParam.Rows[rowIndex].Cells[5].Value = "-";
                //dgvFunctionParam.Rows[rowIndex].Cells[4].ReadOnly = true;
                dgvFunctionParam.Rows[rowIndex].Cells[5].ReadOnly = true;
            }
            else
            {
                //设置cell3 、cell4
                dgvFunctionParam.Rows[rowIndex].Cells[3].Value = ParmName;
                dgvFunctionParam.Rows[rowIndex].Cells[4] = cbxParmType;
                string Mark = ParmName.Substring(0, 1);
                Dictionary<string, string> dnyParmType = new Dictionary<string, string>
                 {
                      {"S",HalconDataType.String.ToString() },
                      {"B",HalconDataType.Bool.ToString()   },
                      {"I",HalconDataType.Int.ToString()    },
                      //{"F",HalconDataType.Float.ToString()  },
                      {"D",HalconDataType.Double.ToString() },
                      //{"L",HalconDataType.Long.ToString()   },
                      {"T",HalconDataType.Image.ToString()  },
                      {"Q",HalconDataType.Region.ToString() },
                      {"Y",HalconDataType.XLD.ToString()}
                 };
                dnyParmType.TryGetValue(Mark, out string result);

                if (result != null)
                {
                    cbxParmType.Value = result;
                }
                else
                {
                    cbxParmType.Value = "Int";
                }

                //设置cell5
                if (InputOutputType == "输出")
                {
                    dgvFunctionParam.Rows[rowIndex].Cells[5].Value = "/";
                    dgvFunctionParam.Rows[rowIndex].Cells[5].ReadOnly = true;
                }
                else
                {
                    dgvFunctionParam.Rows[rowIndex].Cells[5].Value = 0;
                    if (Mark == "Q")
                    {
                        dgvFunctionParam.Rows[rowIndex].Cells[5] = btnSeting;
                    }
                    else if (Mark == "B")
                    {
                        dgvFunctionParam.Rows[rowIndex].Cells[5] = cbxBool;
                    }
                }
            }
        }

        private void LoadFunctionList(string HdplFilePath)
        {
            tvwFunctionList.Nodes.Clear();
            TreeNode Tn1 = tvwFunctionList.Nodes.Add("FirstLevel");
            TreeNode Tn2 = tvwFunctionList.Nodes.Add("SecondLevel");
            TreeNode Tn3 = tvwFunctionList.Nodes.Add("ThirdLevel");
            TreeNode Tn4 = tvwFunctionList.Nodes.Add("Other");
            Tn1.ImageIndex = 0; Tn1.SelectedImageIndex = 0;
            Tn2.ImageIndex = 0; Tn2.SelectedImageIndex = 0;
            Tn3.ImageIndex = 0; Tn3.SelectedImageIndex = 0;
            Tn4.ImageIndex = 0; Tn4.SelectedImageIndex = 0;

            HalconEnginetool.SetProcedurePath(HdplFilePath);
            string[] FuncNames = HalconEnginetool.GetProcedureNames();

            if (FuncNames != null)
            {
                TreeNode NewTn = null;
                for (int i = 0; i < FuncNames.Length; i++)
                {
                    if (FuncNames[i].EndsWith("01"))
                    {
                        NewTn = tvwFunctionList.Nodes[0].Nodes.Add(FuncNames[i]);

                    }
                    else if (FuncNames[i].EndsWith("02"))
                    {
                        NewTn = tvwFunctionList.Nodes[1].Nodes.Add(FuncNames[i]);
                    }
                    else if (FuncNames[i].EndsWith("03"))
                    {
                        NewTn = tvwFunctionList.Nodes[2].Nodes.Add(FuncNames[i]);
                    }
                    else
                    {
                        NewTn = tvwFunctionList.Nodes[3].Nodes.Add(FuncNames[i]);
                    }
                    NewTn.ImageIndex = 1; NewTn.SelectedImageIndex = 1;
                }
            }

        }

        private void LoadSelectedFunction(List<string> SelectFuncNames)
        {
            tvwSelectFuncList.Nodes.Clear();

            int cout = SelectFuncNames.Count;
            string index = "";
            for (int i = 0; i < cout; i++)
            {
                if (i <= 8)
                {
                    index = $"0{i + 1}.";
                }
                else
                {
                    index = $"{i + 1}.";
                }
                TreeNode NewTn = tvwSelectFuncList.Nodes.Add($"{index}{SelectFuncNames[i]}");
                NewTn.ImageIndex = 1; NewTn.SelectedImageIndex = 1;
            }
        }

        private void btnHalconFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择.HDPL文件路径";
            openFileDialog.Filter = "Hdpl或Hdvp文件(*.hdpl;*.hdvp)|*.hdpl;*.hdvp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbxHalconFilePath.Text = openFileDialog.FileName;
                HalconEnginetool.HalconFilePath = openFileDialog.FileName;
                HalconEnginetool.SetProcedurePath(openFileDialog.FileName);
                LoadFunctionList(openFileDialog.FileName);
            }
        }

        private void btnAddFunc_Click(object sender, EventArgs e)
        {
            TreeNode Tn = tvwFunctionList.SelectedNode;
            if (Tn != null && Tn.Level == 1)
            {
                int cout = tvwSelectFuncList.Nodes.Count;
                string index = "";
                if (cout <= 8)
                {
                    index = $"0{cout + 1}.";
                }
                else
                {
                    index = $"{cout + 1}.";
                }
                if (tvwSelectFuncList.Nodes.FirstOrDefault(false, n => n.Text.Contains(Tn.Text)) == null)
                {
                    TreeNode TnAdd = tvwSelectFuncList.Nodes.Add($"{index}{Tn.Text}");
                    TnAdd.ImageIndex = 1; TnAdd.SelectedImageIndex = 1;
                }
                else
                {
                    MessageBox.Show($"函数{ Tn.Text }已经添加在已选列表，请添加其他函数！", "函数已添加", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tvwFunctionList.Focus();
            btnConfirm_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode Tn = tvwSelectFuncList.SelectedNode;
            if (Tn != null)
            {
                int IndexNum1 = tvwSelectFuncList.Nodes.IndexOf(Tn);
                tvwSelectFuncList.Nodes.RemoveAt(IndexNum1);

                for (int i = IndexNum1; i < tvwSelectFuncList.Nodes.Count; i++)
                {
                    string str = tvwSelectFuncList.Nodes[i].Text;
                    tvwSelectFuncList.Nodes[i].Text = (i + 1).ToString().PadLeft(2, '0') + str.Substring(2, str.Length - 2);
                }
                tvwSelectFuncList.Focus();
                btnConfirm_Click(null, null);
            }

        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (tvwSelectFuncList.Nodes.Count > 1 && tvwSelectFuncList.SelectedNode != null && tvwSelectFuncList.SelectedNode != tvwSelectFuncList.Nodes[0])
            {
                int IndexNum = tvwSelectFuncList.Nodes.IndexOf(tvwSelectFuncList.SelectedNode);
                TreeNode tn = tvwSelectFuncList.SelectedNode;
                tvwSelectFuncList.Nodes.RemoveAt(IndexNum);
                tvwSelectFuncList.Nodes.Insert(IndexNum - 1, tn);

                string str1 = tvwSelectFuncList.Nodes[IndexNum - 1].Text;
                string str2 = tvwSelectFuncList.Nodes[IndexNum].Text;
                tvwSelectFuncList.Nodes[IndexNum - 1].Text = str2.Substring(0, 2) + str1.Substring(2, str1.Length - 2);
                tvwSelectFuncList.Nodes[IndexNum].Text = str1.Substring(0, 2) + str2.Substring(2, str2.Length - 2);

                tvwSelectFuncList.SelectedNode = tvwSelectFuncList.Nodes[IndexNum - 1];
            }
            tvwSelectFuncList.Focus();
            btnConfirm_Click(null, null);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (tvwSelectFuncList.Nodes.Count > 1 && tvwSelectFuncList.SelectedNode != null &&
              tvwSelectFuncList.SelectedNode != tvwSelectFuncList.Nodes[tvwSelectFuncList.Nodes.Count - 1])
            {
                int IndexNum = tvwSelectFuncList.Nodes.IndexOf(tvwSelectFuncList.SelectedNode);
                TreeNode tn = tvwSelectFuncList.SelectedNode;
                tvwSelectFuncList.Nodes.RemoveAt(IndexNum);
                tvwSelectFuncList.Nodes.Insert(IndexNum + 1, tn);

                string str1 = tvwSelectFuncList.Nodes[IndexNum + 1].Text;
                string str2 = tvwSelectFuncList.Nodes[IndexNum].Text;
                tvwSelectFuncList.Nodes[IndexNum + 1].Text = str2.Substring(0, 2) + str1.Substring(2, str1.Length - 2);
                tvwSelectFuncList.Nodes[IndexNum].Text = str1.Substring(0, 2) + str2.Substring(2, str2.Length - 2);
                tvwSelectFuncList.SelectedNode = tvwSelectFuncList.Nodes[IndexNum + 1];
            }
            tvwSelectFuncList.Focus();
            btnConfirm_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveParameters();
            HalconEnginetool.SelectFuncNames = new List<string>(tvwSelectFuncList.Nodes.Count);
            for (int i = 0; i < tvwSelectFuncList.Nodes.Count; i++)
            {
                HalconEnginetool.SelectFuncNames.Add(tvwSelectFuncList.Nodes[i].Text.Substring(3));
            }

            if (sender != null)
            {
                MessageBox.Show("保存参数成功！", "保存成功", MessageBoxButtons.OK);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            HalconEnginetool.SelectFuncNames = new List<string>(tvwSelectFuncList.Nodes.Count);
            for (int i = 0; i < tvwSelectFuncList.Nodes.Count; i++)
            {
                HalconEnginetool.SelectFuncNames.Add(tvwSelectFuncList.Nodes[i].Text.Substring(3));
            }

            dgvFunctionParam.CellValueChanged -= dgvFunctionParam_CellValueChanged;
            UpdateDataGridView(HalconEnginetool.SelectFuncNames);
            CompareValue();
            dgvFunctionParam.CellValueChanged += dgvFunctionParam_CellValueChanged;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "导出参数";
                saveFile.Filter = "XML文件(*.xml)|*.xml";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer xml = new XmlSerializer(typeof(HalconEngineTool)); //, new XmlRootAttribute("AlgoRoot")
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.Write);
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xml.Serialize(fs, HalconEnginetool, ns);
                    fs.Close();

                    MessageBox.Show("成功导出参数！", "导出参数", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("导出参数失败！", "导出参数失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.Title = "导入参数";
            Openfile.Filter = "XML文件(*.xml)|*.xml";

            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(typeof(HalconEngineTool));
                    FileStream fs = new FileStream(Openfile.FileName, FileMode.Open, FileAccess.Read);
                    HalconEngineTool HalconModel = (HalconEngineTool)xml.Deserialize(fs);
                    fs.Close();


                    HalconEnginetool.SelectFuncNames = HalconModel.SelectFuncNames;
                    HalconEnginetool.Parameters = HalconModel.Parameters;

                    LoadSelectedFunction(HalconEnginetool.SelectFuncNames);
                    dgvFunctionParam.CellValueChanged -= dgvFunctionParam_CellValueChanged;
                    UpdateDataGridView(HalconEnginetool.SelectFuncNames);
                    CompareValue();
                    dgvFunctionParam.CellValueChanged += dgvFunctionParam_CellValueChanged;

                    MessageBox.Show("成功导入参数！", "导入参数", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("导入参数失败！请检查导入文件是否正确。", "导出参数失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tvwFunctionList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                int cout = tvwSelectFuncList.Nodes.Count;
                string index = "";
                if (cout <= 8)
                {
                    index = $"0{cout + 1}.";
                }
                else
                {
                    index = $"{cout + 1}.";
                }
                if (tvwSelectFuncList.Nodes.FirstOrDefault(false, n => n.Text.Contains(e.Node.Text)) == null)
                {
                    TreeNode Tn = tvwSelectFuncList.Nodes.Add($"{index}{e.Node.Text}");
                    Tn.ImageIndex = 1; Tn.SelectedImageIndex = 1;
                    btnConfirm_Click(null, null);
                }
                else
                {
                    MessageBox.Show($"函数{e.Node.Text}已经添加在已选列表，请添加其他函数！", "函数已添加", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void tvwSelectFuncList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Tn = e.Node;
            if (Tn != null)
            {
                string FuncName = Tn.Text.Substring(3);

                for (int i = 0; i < dgvFunctionParam.Rows.Count; i++)
                {
                    if (dgvFunctionParam[1, i].Value.ToString() == FuncName)
                    {
                        dgvFunctionParam.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
        }

        private void cbxDebugModel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDebugModel.Checked == true)
            {
                DialogResult dr = MessageBox.Show("调试模式是专门用于调试Halcon程序代码，勾选后软件会进入调试状态，非开发人员不能勾选！！",
                    "Halcon引擎调试模式", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (dr == DialogResult.OK)
                {
                    HalconEnginetool.IsDebugMode = cbxDebugModel.Checked;
                }
                else
                {
                    cbxDebugModel.Checked = false;
                }
            }
            else
            {
                if (HalconEnginetool.IsDebugMode != false)
                {
                    HalconEnginetool.IsDebugMode = false;
                }
            }

        }
    }
}
