using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyVision.Tools.PhaseDeflection.BL;
using HyVision.Models;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using LeadHawkCS;
using GL.Kit.Log;

namespace HyVision.Tools.PhaseDeflection.UI
{
    public partial class PhaseDeflectionUI : BaseHyUserToolEdit<PhaseDeflectionBL>
    {
        PhaseDeflectionBL phaseDeflectionBL;
        string filePath;
        LogPublisher log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

        private string str1PositionBlue = "./Data/PhasePara/cam1/blue";
        private string str2PositionBlue = "./Data/PhasePara/cam2/blue";
        private string str1PositionWhite = "./Data/PhasePara/cam1/white";
        private string str2PositionWhite = "./Data/PhasePara/cam2/white";

        public PhaseDeflectionUI()
        {
            InitializeComponent();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (phaseDeflectionBL == null)
                    phaseDeflectionBL = new PhaseDeflectionBL();

                phaseDeflectionBL.CalibDataFolderPath = RefreshXMLDataFolderPath();

                //phaseDeflectionBL.Inputs已经是引用到inputEdit.Subject，不需要重新赋值
                //phaseDeflectionBL.Inputs.Clear();
                //foreach (HyTerminal item in inputEdit.Subject)
                //{
                //    phaseDeflectionBL.Inputs.Add(item);
                //}

                //先移除已经删除的Output
                for (int i = 0; i < phaseDeflectionBL.Outputs.Count; i++)
                {
                    bool bRemove = true;
                    foreach (DataGridViewRow row in dgvOutputItems.Rows)
                    {
                        string name = row.Cells[ColumnName.Index].Value.ToString();
                        if (phaseDeflectionBL.Outputs[i].Name.Equals(name) && 
                            (row.Cells[ColumnOutput.Index].Value != null && bool.Parse(row.Cells[ColumnOutput.Index].Value.ToString())))
                            bRemove = false;
                    }
                    if (bRemove)
                        phaseDeflectionBL.Outputs.RemoveAt(i--);
                }

                foreach (DataGridViewRow row in dgvOutputItems.Rows)
                {
                    if (row.Cells[ColumnName.Index].Value == null ||
                        row.Cells[ColumnName.Index].Value.ToString().Equals(""))
                    {
                        log?.Warn($"运行[{Name}]时发生异常，输入参数 {ColumnName.HeaderText} 中含有非法值！");
                        continue;
                    }

                    if (row.Cells[ColumnType.Index].Value == null ||
                        row.Cells[ColumnType.Index].Value.ToString().Equals(""))
                    {
                        log?.Warn($"运行[{Name}]时发生异常，输入参数 {ColumnType.HeaderText} 中含有非法值！");
                        continue;
                    }

                    if (row.Cells[ColumnOutput.Index].Value != null && bool.Parse(row.Cells[ColumnOutput.Index].Value.ToString()))
                    {
                        HyTerminal outputTerminal;
                        Type outputValueType;
                        string outputName = row.Cells[ColumnName.Index].Value.ToString();
                        if (row.Cells[ColumnType.Index].Value.ToString().Equals(typeof(HyImage).ToString()))
                            outputValueType = typeof(HyImage);
                        else if (row.Cells[ColumnType.Index].Value.ToString().Equals(typeof(int).ToString()))
                            outputValueType = typeof(int);
                        else
                            outputValueType = typeof(string);

                        outputTerminal = new HyTerminal(outputName, outputValueType);

                        string typeName = outputValueType?.Name;
                        if (typeName.StartsWith("List"))
                        {
                            typeName = string.Format("List<{0}>", outputValueType?.GenericTypeArguments[0].Name);
                        }
                        outputTerminal.ValueTypeString = typeName;
                        outputTerminal.GUID = Guid.NewGuid().ToString("N");

                        bool bAddNew = true;
                        for (int i = 0; i < phaseDeflectionBL.Outputs.Count; i++)
                        {
                            if (phaseDeflectionBL.Outputs[i].Name.Equals(outputName))
                            {
                                outputTerminal.GUID = phaseDeflectionBL.Outputs[i].GUID;
                                outputTerminal.Description = phaseDeflectionBL.Outputs[i].Description;
                                outputTerminal.From = phaseDeflectionBL.Outputs[i].From;
                                phaseDeflectionBL.Outputs[i] = outputTerminal;
                                bAddNew = false;
                            }
                        }
                        if (bAddNew)
                            phaseDeflectionBL.Outputs.Add(outputTerminal);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void Save()
        {
            try
            {
                foreach (DataGridViewRow row in dgvOutputItems.Rows)
                {
                    if (row.Cells[ColumnName.Index].Value == null ||
                        row.Cells[ColumnName.Index].Value.ToString().Equals(""))
                        throw new Exception($"运行[{Name}]时发生异常", new HyVisionException($"输入参数 {ColumnName.HeaderText} 中含有非法值！"));

                    if (row.Cells[ColumnType.Index].Value == null ||
                        row.Cells[ColumnType.Index].Value.ToString().Equals(""))
                        throw new Exception($"运行[{Name}]时发生异常", new HyVisionException($"输入参数 {ColumnType.HeaderText} 中含有非法值！"));
                }

                UpdateDataToObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PhaseDeflectionBL Subject
        {
            get { return phaseDeflectionBL; }
            set
            {
                if (!object.Equals(phaseDeflectionBL, value))
                {
                    phaseDeflectionBL = value;
                    inputEdit.Subject = phaseDeflectionBL.Inputs;

                    if (phaseDeflectionBL.CalibDataFolderPath == null)
                        return;

                    filePath = $"{phaseDeflectionBL.CalibDataFolderPath}\\Para.xml";
                    if (!File.Exists(filePath))
                        log.Warn($"根据当前工位及产品颜色，未找到指定的文件[{filePath}]！");


                    if (phaseDeflectionBL.CalibDataFolderPath.Equals(str2PositionBlue))
                    {
                        this.cbNestIndex.SelectedIndex = 1;
                        this.productColor1.SelectedIndex = 0;
                    }
                    else if (phaseDeflectionBL.CalibDataFolderPath.Equals(str1PositionWhite))
                    {
                        this.cbNestIndex.SelectedIndex = 0;
                        this.productColor1.SelectedIndex = 1;
                    }
                    else if (phaseDeflectionBL.CalibDataFolderPath.Equals(str2PositionWhite))
                    {
                        this.cbNestIndex.SelectedIndex = 1;
                        this.productColor1.SelectedIndex = 1;
                    }
                    else
                    {
                        this.cbNestIndex.SelectedIndex = 0;
                        this.productColor1.SelectedIndex = 0;
                    }

                }
            }
        }

        private void PhaseDeflectionUI_Load(object sender, EventArgs e)
        {
            try
            {
                dgvOutputItems.Rows.Clear();
                foreach(string terminalName in phaseDeflectionBL.OutputImageName)
                {
                    int index = dgvOutputItems.Rows.Add();
                    dgvOutputItems.Rows[index].Cells[ColumnIndex.Index].Value = index;
                    dgvOutputItems.Rows[index].Cells[ColumnName.Index].Value = terminalName;
                    dgvOutputItems.Rows[index].Cells[ColumnType.Index].Value = typeof(HyImage);
                }

                foreach (DataGridViewRow row in dgvOutputItems.Rows)
                {
                    if (phaseDeflectionBL.Outputs.Contains(row.Cells[ColumnName.Index].Value.ToString()))
                        row.Cells[ColumnOutput.Index].Value = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string RefreshXMLDataFolderPath()
        {
            try
            {
                if(cbNestIndex.SelectedItem == null)
                {
                    log.Warn($"没有选择穴位！");
                    return null;
                }
                if (productColor1.SelectedItem == null)
                {
                    log.Warn($"没有选择产品颜色！");
                    return null;
                }

                string strNestIndex = cbNestIndex.SelectedItem.ToString();
                string strProductColor = productColor1.SelectedItem.ToString();
                switch (strNestIndex + strProductColor)
                {
                    case "1穴蓝色":
                        return str1PositionBlue;
                    case "2穴蓝色":
                        return str2PositionBlue;
                    case "1穴白色":
                        return str1PositionWhite;
                    case "2穴白色":
                        return str2PositionWhite;
                    default:
                        return str1PositionBlue;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }
    }
}
