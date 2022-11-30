using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using HyVision.Tools;
using HalconSDK.Engine.BL;
using HyVision.Models;
using HyVision;
using System.IO;





namespace HalconSDK.Engine.UI
{
    public partial class HalconEngineUI_1 : BaseHyUserToolEdit<HalconProgramEngineBL>
    {
        HalconProgramEngineBL halconProgramEngineBL;


        private void HalconEngineUI_1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvHalconFunc.Columns.Count; i++)
            {
                dgvHalconFunc.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            hyImageDisplayControl1.ShowEditROIForm = true;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HalconProgramEngineBL Subject
        {
            get
            {
                return halconProgramEngineBL;
            }
            set
            {
                halconProgramEngineBL = value;
                //设置ROI和图片
                hyImageDisplayControl1.SetHyROIs(halconProgramEngineBL.HyROIs);
                if (halconProgramEngineBL.ShowImage != null && halconProgramEngineBL.ShowImage.Image != null)
                {
                    hyImageDisplayControl1.DisplayImage(halconProgramEngineBL.ShowImage.Image);
                }
                tbHalconFilePath.Text = halconProgramEngineBL.HalconFilePath;

                if (!string.IsNullOrEmpty(halconProgramEngineBL.HalconFilePath))
                {
                    dgvHalconFunc.CellValueChanged -= dgvHalconFunc_CellValueChanged;
                    UpdateDataGridView(halconProgramEngineBL.HalconFilePath);

                    for (int i = 0; i < dgvHalconFunc.Rows.Count; i++)
                    {
                        string FunName = dgvHalconFunc.Rows[i].Cells[1].Value.ToString();
                        string InputOutputType = dgvHalconFunc.Rows[i].Cells[2].Value.ToString();
                        string ParmName = dgvHalconFunc.Rows[i].Cells[3].Value.ToString();

                        if (InputOutputType == "输入")
                        {
                            HyTerminal HyterminalInput = halconProgramEngineBL.Inputs.FirstOrDefault(a =>
                            a.Description.Contains(FunName) && a.Description.Contains(ParmName));

                            if (HyterminalInput != null)
                            {
                                if (HyterminalInput.ConvertTargetType == HalconDataType.Image.ToString()
                                    || HyterminalInput.ConvertTargetType == HalconDataType.Region.ToString())
                                {
                                    dgvHalconFunc.Rows[i].Cells[4].Value = HyterminalInput.ConvertTargetType;
                                    DataGridViewButtonCell Seting = new DataGridViewButtonCell();
                                    Seting.Value = "设置";
                                    dgvHalconFunc.Rows[i].Cells[5] = Seting;
                                }
                                else
                                {
                                    DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
                                    dgvHalconFunc.Rows[i].Cells[5] = textBoxCell;
                                    dgvHalconFunc.Rows[i].Cells[4].Value = HyterminalInput.ConvertTargetType;
                                    dgvHalconFunc.Rows[i].Cells[5].Value = HyterminalInput.Value;
                                }
                            }
                        }
                        else
                        {
                            HyTerminal HyterminalOutput = halconProgramEngineBL.Outputs.FirstOrDefault(a =>
                          a.Description.Contains(FunName) && a.Description.Contains(ParmName));

                            if (HyterminalOutput != null)
                            {
                                dgvHalconFunc.Rows[i].Cells[4].Value = HyterminalOutput.ConvertTargetType;
                            }
                        }
                    }
                    dgvHalconFunc.CellValueChanged += dgvHalconFunc_CellValueChanged;
                }
            }
        }

        public override void UpdateDataToObject()
        {
            //保存图片
            if (hyImageDisplayControl1.GetBitmap() == null)
            {
                //halconProgramEngineBL.ShowImage = new HyImage();
            }
            else
            {
                halconProgramEngineBL.ShowImage = new HyImage(hyImageDisplayControl1.GetBitmap());
            }

            //1.先清除原来的输入和输出
            int InputCount = halconProgramEngineBL.Inputs.Count;
            int OutputCount = halconProgramEngineBL.Outputs.Count;
            for (int i = 0; i < InputCount; i++)
            {
                halconProgramEngineBL.Inputs.RemoveAt(0);
            }
            for (int i = 0; i < OutputCount; i++)
            {
                halconProgramEngineBL.Outputs.RemoveAt(0);
            }

            //2.更新新的输入和输出
            //值为“-”保存至HalconProgramEngineBL基类的Inputs和Outputs，用于在ToolBlock连线
            //反之，保存至保存至HalconProgramEngineBL的HalconEngineInputOutput
            string Index, FunName, InputOutputType, ParmName, DataType, Value;
            for (int i = 0; i < dgvHalconFunc.Rows.Count; i++)
            {
                Index = dgvHalconFunc.Rows[i].Cells[0].Value.ToString();
                FunName = dgvHalconFunc.Rows[i].Cells[1].Value.ToString();
                InputOutputType = dgvHalconFunc.Rows[i].Cells[2].Value.ToString();
                ParmName = dgvHalconFunc.Rows[i].Cells[3].Value.ToString();
                DataType = dgvHalconFunc.Rows[i].Cells[4].Value.ToString();
                Value = dgvHalconFunc.Rows[i].Cells[5].Value.ToString();


                Type type = null;
                if (DataType == HalconDataType.Image.ToString())
                {
                    type = typeof(HyImage);
                }
                else if (DataType == HalconDataType.Region.ToString())
                {
                    type = typeof(HyVision.Tools.ImageDisplay.BaseHyROI);
                    //type = typeof(HyVision.Tools.ImageDisplay.HyDefectRegion);
                }
                else if (DataType == HalconDataType.XLD.ToString())
                {
                    type = typeof(HyVision.Tools.ImageDisplay.BaseHyROI);
                }
                else if (DataType == HalconDataType.Int.ToString())
                {
                    type = typeof(int);
                }
                else if (DataType == HalconDataType.Double.ToString())
                {
                    type = typeof(double);
                }
                else if (DataType == HalconDataType.Bool.ToString())
                {
                    type = typeof(bool);
                }
                else if (DataType == HalconDataType.String.ToString())
                {
                    type = typeof(string);
                }

                HyTerminal hyTerminal = new HyTerminal($"{FunName}({ParmName})", type);
                hyTerminal.GUID = Guid.NewGuid().ToString("N");
                hyTerminal.ConvertTargetType = DataType;
                hyTerminal.Description = $"{Index},{FunName},{InputOutputType},{ParmName},{DataType},{Value}";

                if (InputOutputType == "输入")
                {
                    if (DataType == HalconDataType.Image.ToString())
                    {
                        SettingsBL ImageSetting = halconProgramEngineBL.Settings.Find(s =>
                        s.FunName == FunName && s.ParmName == ParmName);
                        if (ImageSetting != null)
                        {
                            hyTerminal.Value = ImageSetting.InputImage;
                        }

                    }
                    else if (DataType == HalconDataType.Region.ToString())
                    {
                        SettingsBL RegionSetting = halconProgramEngineBL.Settings.Find(s =>
                        s.FunName == FunName && s.ParmName == ParmName);
                        if (RegionSetting != null && RegionSetting.ResultHObjectROI != null)
                        {
                            HalconDataConvert dataConvert = new HalconDataConvert();
                            hyTerminal.Value = dataConvert.HobjectToHyDefectRegion(RegionSetting.ResultHObjectROI);
                        }
                    }
                    else if (DataType == HalconDataType.XLD.ToString())
                    {
                        //保留
                    }
                    else if (DataType == HalconDataType.Int.ToString())
                    {
                        hyTerminal.Value = int.Parse(Value);
                    }
                    else if (DataType == HalconDataType.Double.ToString())
                    {
                        hyTerminal.Value = double.Parse(Value);
                    }
                    else if (DataType == HalconDataType.Bool.ToString())
                    {
                        hyTerminal.Value = bool.Parse(Value);
                    }
                    else if (DataType == HalconDataType.String.ToString())
                    {
                        hyTerminal.Value = Value;
                    }

                    halconProgramEngineBL.Inputs.Add(hyTerminal);
                }
                else if (InputOutputType == "输出")
                {
                    halconProgramEngineBL.Outputs.Add(hyTerminal);
                }
            }

            hyImageDisplayControl1.ShowEditROIForm = false;
        }

        public override void Save()
        {
            //UpdateDataToObject();
        }

        public HalconEngineUI_1()
        {
            InitializeComponent();
        }

        private void btnOpenHalconFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择Halcon程序路径";
                openFileDialog.Filter = "HDEV文件(*.hdev)|*.hdev";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dgvHalconFunc.CellValueChanged -= dgvHalconFunc_CellValueChanged;

                    halconProgramEngineBL.HalconFilePath = openFileDialog.FileName;
                    tbHalconFilePath.Text = openFileDialog.FileName;
                    UpdateDataGridView(openFileDialog.FileName);

                    dgvHalconFunc.CellValueChanged += dgvHalconFunc_CellValueChanged;
                }

                openFileDialog.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"导入Halcon程序有误，请重新导入！原因：{ex.Message}", "导入出差", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHalconFunc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 5)
            {
                if (dgvHalconFunc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "设置")
                {
                    string FunNmae = dgvHalconFunc.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string ParmNmae = dgvHalconFunc.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string DataType = dgvHalconFunc.Rows[e.RowIndex].Cells[4].Value.ToString();

                    SettingsBL setting = halconProgramEngineBL.Settings.FirstOrDefault(a =>
                     a.FunName == FunNmae && a.ParmName == ParmNmae);

                    // Image设置
                    if (DataType == HalconDataType.Image.ToString())
                    {
                        if (setting == null)
                        {
                            setting = new SettingsBL
                            {
                                FunName = FunNmae,
                                ParmName = ParmNmae
                            };
                            halconProgramEngineBL.Settings.Add(setting);
                        }
                        FrmImageSeting frmImageSeting = new FrmImageSeting(setting);
                        frmImageSeting.ShowDialog();
                    }

                    // Region设置
                    if (DataType == HalconDataType.Region.ToString())
                    {
                        if (setting == null)
                        {
                            setting = new SettingsBL()
                            {
                                FunName = FunNmae,
                                ParmName = ParmNmae,
                            };
                            halconProgramEngineBL.Settings.Add(setting);
                        }

                        if (hyImageDisplayControl1.GetBitmap() != null)
                        {
                            if (halconProgramEngineBL.ShowImage == null)
                            {
                                halconProgramEngineBL.ShowImage = new HyImage();
                            }
                            halconProgramEngineBL.ShowImage.Image = hyImageDisplayControl1.GetBitmap();
                        }
                        FrmHyROISeting frmHyROISeting = new FrmHyROISeting(halconProgramEngineBL, setting);
                        frmHyROISeting.Show();
                    }
                }
            }
        }

        private void dgvHalconFunc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex <= 2 && e.RowIndex != -1)
            {
                int count = 0, UpRows = 0, DownRows = 0;
                string CellValue = e.Value == null ? "" : e.Value.ToString();
                Brush gridBrush = new SolidBrush(dgvHalconFunc.GridColor);
                Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor);

                for (int i = e.RowIndex; i < dgvHalconFunc.Rows.Count; i++)
                {
                    if (CellValue == dgvHalconFunc.Rows[i].Cells[e.ColumnIndex].Value.ToString())
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
                    if (CellValue == dgvHalconFunc.Rows[i].Cells[e.ColumnIndex].Value.ToString())
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

        private void dgvHalconFunc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                if (dgvHalconFunc.Rows[e.RowIndex].Cells[2].Value.ToString() != "输入")
                {
                    return;
                }
                string DataType = dgvHalconFunc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (DataType == "Image" || DataType == "Region")
                {
                    DataGridViewButtonCell Seting = new DataGridViewButtonCell();
                    Seting.Value = "设置";
                    dgvHalconFunc.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = Seting;
                }
                else
                {
                    string OldValue = dgvHalconFunc.Rows[e.RowIndex].Cells[5].Value.ToString();
                    DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();

                    if (OldValue == "设置")
                    {
                        textBoxCell.Value = 0;
                    }
                    else
                    {
                        textBoxCell.Value = OldValue;
                    }
                    dgvHalconFunc.Rows[e.RowIndex].Cells[5] = textBoxCell;

                }
            }
        }

        private void UpdateDataGridView(string HalconFilePath)
        {
            dgvHalconFunc.Rows.Clear();

            //调用Halcon引擎读取Halcon文件会修改 Directory.GetCurrentDirectory() 的路径。先保存原来的值，
            //读取Halcon文件后再恢复原来的路径值
            string folderPath = Directory.GetCurrentDirectory();

            HDevProgram hDevProgram = new HDevProgram();
            //hDevProgram.LoadProgram(HalconFilePath);
            HTuple FuncNames = hDevProgram.GetUsedProcedureNames();
            int FuncCount = FuncNames.TupleLength();
            //只读取第一个外部函数的输入输出
            FuncCount = FuncCount > 0 ? 1 : 0;
            for (int i = 0; i < FuncCount; i++)
            {
                HDevProcedure FuncName1 = new HDevProcedure(hDevProgram, FuncNames.TupleSelect(i).S);

                //图形变量、控制变量的输入
                HTuple InputIconicParms = FuncName1.GetInputIconicParamNames();
                int InputIconicParmCount = InputIconicParms.TupleLength();
                for (int j = 0; j < InputIconicParmCount; j++)
                {
                    AddNewRow((i + 1).ToString(), FuncName1.Name, "输入", InputIconicParms.TupleSelect(j).S);
                }

                HTuple InputCtrlParms = FuncName1.GetInputCtrlParamNames();
                int InputCtrlParmCount = InputCtrlParms.TupleLength();
                for (int j = 0; j < InputCtrlParmCount; j++)
                {
                    AddNewRow((i + 1).ToString(), FuncName1.Name, "输入", InputCtrlParms.TupleSelect(j).S);
                }


                //图形变量、控制变量的输出
                HTuple OutputIconicParms = FuncName1.GetOutputIconicParamNames();
                int OutputIconicParmCount = OutputIconicParms.TupleLength();
                for (int k = 0; k < OutputIconicParmCount; k++)
                {
                    AddNewRow((i + 1).ToString(), FuncName1.Name, "输出", OutputIconicParms.TupleSelect(k).S);
                }

                HTuple OutputCtrlParms = FuncName1.GetOutputCtrlParamNames();
                int OutputCtrlParmCount = OutputCtrlParms.TupleLength();
                for (int k = 0; k < OutputCtrlParmCount; k++)
                {
                    AddNewRow((i + 1).ToString(), FuncName1.Name, "输出", OutputCtrlParms.TupleSelect(k).S);
                }

                if (InputIconicParmCount == 0 && InputCtrlParmCount == 0
                    && OutputIconicParmCount == 0 && OutputCtrlParmCount == 0)
                {
                    AddNewRow((i + 1).ToString(), FuncName1.Name, "-", "-");
                }
            }

            Directory.SetCurrentDirectory(folderPath);
            string folderPath1 = Directory.GetCurrentDirectory();
        }

        private void AddNewRow(string FunIndex, string FunName, string InputOutputType, string ParmName)
        {
            DataGridViewButtonCell Seting = new DataGridViewButtonCell();
            Seting.Value = "设置";
            DataGridViewComboBoxCell ParmType = new DataGridViewComboBoxCell();
            ParmType.Style = dgvHalconFunc.DefaultCellStyle;
            foreach (string Name in Enum.GetNames(typeof(HalconDataType)))
            {
                ParmType.Items.Add(Name);
            }
          

            int rowIndex = dgvHalconFunc.Rows.Add();
            dgvHalconFunc.Rows[rowIndex].Cells[0].ReadOnly = true;
            dgvHalconFunc.Rows[rowIndex].Cells[1].ReadOnly = true;
            dgvHalconFunc.Rows[rowIndex].Cells[2].ReadOnly = true;
            dgvHalconFunc.Rows[rowIndex].Cells[3].ReadOnly = true;
            //设置cell0 、cell1、cell2
            dgvHalconFunc.Rows[rowIndex].Cells[0].Value = FunIndex;
            dgvHalconFunc.Rows[rowIndex].Cells[1].Value = FunName;
            dgvHalconFunc.Rows[rowIndex].Cells[2].Value = InputOutputType;
            if (InputOutputType == "-")
            {
                dgvHalconFunc.Rows[rowIndex].Cells[3].Value = "-";
                dgvHalconFunc.Rows[rowIndex].Cells[4].Value = "-";
                dgvHalconFunc.Rows[rowIndex].Cells[5].Value = "-";
                dgvHalconFunc.Rows[rowIndex].Cells[4].ReadOnly = true;
                //dgvHalconFunc.Rows[rowIndex].Cells[5].ReadOnly = true;
            }
            else
            {
                //设置cell3 、cell4
                dgvHalconFunc.Rows[rowIndex].Cells[3].Value = ParmName;
                dgvHalconFunc.Rows[rowIndex].Cells[4] = ParmType;
                string Mark = ParmName.Substring(0, 1);
                Dictionary<string, string> DictParmType = new Dictionary<string, string>
                 {
                      {"S",HalconDataType.String.ToString() },
                      {"B",HalconDataType.Bool.ToString()   },
                      {"I",HalconDataType.Int.ToString()    },
                      //{"F",HalconDataType.Float.ToString()  },
                      {"D",HalconDataType.Double.ToString() },
                      //{"L",HalconDataType.Long.ToString()   },
                      {"T",HalconDataType.Image.ToString()  },
                      {"Q",HalconDataType.Region.ToString() },
                      {"Y","XLD" }
                 };
                string result = DictParmType.Keys.FirstOrDefault(s => s == Mark);
                if (result != null)
                {
                    ParmType.Value = DictParmType[result];
                }
                else
                {
                    ParmType.Value = DictParmType["I"];
                }

                //设置cell5
                if (InputOutputType == "输出")
                {
                    dgvHalconFunc.Rows[rowIndex].Cells[5].Value = "-";
                    dgvHalconFunc.Rows[rowIndex].Cells[5].ReadOnly = true;
                }
                else
                {
                    dgvHalconFunc.Rows[rowIndex].Cells[5].Value = 0;
                    if (result == "T" || result == "Q")
                    {
                        dgvHalconFunc.Rows[rowIndex].Cells[5] = Seting;
                    }
                }
            }
        }


    }

}
