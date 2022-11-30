using HyEye.API.Repository;
using HyEye.Models.VO;
using HyVision.Models;
using HyVision.SDK;
using HyVision.Tools.ImageDisplay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;

namespace HyEye.WForm.Display
{
    public partial class FrmSelectOutputShow : Form
    {
        string taskName;
        int rowIndex, colIndex;
        readonly ToolBlockComponentSet toolBlockComponentSet;
        IDisplayTaskImageComponent displayControl;

        public FrmSelectOutputShow(string taskName, int rowIndex, int colIndex, 
            ToolBlockComponentSet toolBlockComponentSet, IDisplayTaskImageComponent displayControl)
        {
            InitializeComponent();

            this.taskName = taskName;
            this.toolBlockComponentSet = toolBlockComponentSet;
            this.displayControl = displayControl;
            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
        }

        private void FrmSelectOutputShow_Load(object sender, EventArgs e)
        {
            try
            {
                lstTaskName.Items.Clear();
                lstOutputName.Items.Clear();
                lstGraphicName.Items.Clear();

                lstTaskName.Items.Add(taskName);

                if (lstTaskName.Items.Contains(this.displayControl.TaskName))
                    lstTaskName.SelectedItem = this.displayControl.TaskName;
                if (lstOutputName.Items.Contains(this.displayControl.AcqOrCalibName))
                    lstOutputName.SelectedItem = this.displayControl.AcqOrCalibName;

                OutputDisplayTaskImageComponent displayComponent = (OutputDisplayTaskImageComponent)this.displayControl;
                if(displayComponent.GraphicNames.Count > 0)
                {
                    foreach (string graphicName in displayComponent.GraphicNames)
                    {
                        if (lstGraphicName.Items.Contains(graphicName))
                            lstGraphicName.SelectedItems.Add(graphicName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTaskName.SelectedItem == null || lstTaskName.SelectedItem.ToString().Equals(""))
                    throw new Exception($"请选择Task名称！");
                if (lstOutputName.SelectedItem == null || lstOutputName.SelectedItem.ToString().Equals(""))
                    throw new Exception($"请选择Output结果名称！");

                this.displayControl.TaskName = lstTaskName.SelectedItem.ToString();
                this.displayControl.AcqOrCalibName = lstOutputName.SelectedItem.ToString();

                GL.Kit.IO.IniFileUtils.SetFilePath(API.GlobalParams.Output_Graphic_IniFile_FilePath);
                GL.Kit.IO.IniFileUtils.WriteIniData($"{taskName}", $"Row{rowIndex}Col{colIndex}OutputName", lstOutputName.SelectedItem.ToString());

                if (lstGraphicName.SelectedItems != null && lstGraphicName.SelectedItems.Count > 0)
                {
                    OutputDisplayTaskImageComponent displayComponent = (OutputDisplayTaskImageComponent)this.displayControl;
                    displayComponent.GraphicNames = lstGraphicName.SelectedItems.Cast<string>().ToList();
                    this.displayControl = displayComponent;

                    int i = 1;
                    foreach(string GraphicName in lstGraphicName.SelectedItems)
                    {
                        GL.Kit.IO.IniFileUtils.WriteIniData($"{taskName}", $"Row{rowIndex}Col{colIndex}GraphicName{i++}", GraphicName);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstTaskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lstOutputName.Items.Clear();
                lstGraphicName.Items.Clear();
                string taskName = lstTaskName.SelectedItem.ToString();
                IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(taskName);
                if (toolBlockComponent == null)
                    throw new Exception($"未能找到名称为“{taskName}”的ToolBlock组件！");

                if (toolBlockComponent.GetType() != typeof(HyToolBlockComponent))
                    throw new Exception($"目前只支持“{typeof(HyToolBlockComponent)}”的输出结果图像显示");

                HyToolBlockComponent hyToolBlockComponent = (HyToolBlockComponent)toolBlockComponent;
                if (hyToolBlockComponent.GetOutputsTerminal() != null && hyToolBlockComponent.GetOutputsTerminal().Count > 0)
                {
                    foreach (HyTerminal terminal in hyToolBlockComponent.GetOutputsTerminal())
                    {
                        if (terminal.ValueType == typeof(HyImage))
                        {
                            lstOutputName.Items.Add(terminal.Name);
                        }
                        else if (terminal.ValueType == typeof(Bitmap))
                        {
                            lstOutputName.Items.Add(terminal.Name);
                        }
                        else if (terminal.ValueType == typeof(HyDefectRegion))
                        {
                            lstGraphicName.Items.Add(terminal.Name);
                        }
                        else if (terminal.ValueType == typeof(HyDefectXLD))
                        {
                            lstGraphicName.Items.Add(terminal.Name);
                        }
                        else if (terminal.ValueType == typeof(BaseHyROI))
                        {
                            lstGraphicName.Items.Add(terminal.Name);
                        }
                        else if (terminal.ValueType == typeof(List<HyDefectXLD>))
                        {
                            lstGraphicName.Items.Add(terminal.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
