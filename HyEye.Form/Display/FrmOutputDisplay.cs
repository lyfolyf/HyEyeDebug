using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using HyEye.API.Repository;
using HyEye.Models.VO;
using VisionSDK;
using VisionFactory;
using HyVision.SDK;
using HyVision.Models;
using HyVision.Tools.ImageDisplay;
using GL.Kit.Log;

namespace HyEye.WForm.Display
{
    public partial class FrmOutputDisplay : DockContent
    {
        delegate void delegateRefrshControl(string taskName);
        readonly ITaskRepository taskRepo;
        readonly ToolBlockComponentSet toolBlockComponentSet;
        readonly OutputDisplayComponentSet displayControlSet;
        int rowCount;
        int colCount;
        string showTaskName;
        Dictionary<string, Control[,]> dicDisplayArr = new Dictionary<string, Control[,]>();
        string curTaskName = "";
        readonly IGLog log;

        public FrmOutputDisplay(ITaskRepository taskRepo, ToolBlockComponentSet toolBlockComponentSet,
            IGLog log, OutputDisplayComponentSet displayControlSet, int rowCount, int colCount, string showTaskName)
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.toolBlockComponentSet = toolBlockComponentSet;
            this.displayControlSet = displayControlSet;
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.showTaskName = showTaskName;
        }

        private void FrmOutputDisplay_Load(object sender, EventArgs e)
        {
            BindDisplay();
            RefreshDisplay(showTaskName);
        }

        public void RefreshDisplay(string taskName)
        {
            DisplayLayout(rowCount, colCount);
            BindDisplayToDisplay(taskName);
        }

        void DisplayLayout(int rowCount, int colCount)
        {
            panel1.Controls.Clear();

            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = colCount;
            for(int i = 0; i < colCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, (1 / (float)colCount)));
            }
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (1 / (float)rowCount)));
            }
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        }

        void BindDisplayToDisplay(string taskName)
        {
            if (dicDisplayArr.ContainsKey(taskName))
            {
                tableLayoutPanel.Controls.Clear();
                Control[,] displayArr = dicDisplayArr[taskName];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (displayArr[i, j] != null)
                            tableLayoutPanel.Controls.Add(displayArr[i, j], j, i);
                    }
                }
            }
        }

        void BindDisplay()
        {
            Control[,] displayArr = new Control[rowCount, colCount];
            IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(showTaskName);
            if (toolBlockComponent == null)
            {
                log.Warn($"未能找到名称为“{showTaskName}”的ToolBlock组件！");
                return;
            }

            if (toolBlockComponent.GetType() != typeof(HyToolBlockComponent))
            {
                log.Warn($"目前只支持“{typeof(HyToolBlockComponent)}”的输出结果图像显示");
                return;
            }

            HyToolBlockComponent hyToolBlockComponent = (HyToolBlockComponent)toolBlockComponent;
            hyToolBlockComponent.OutputImageAdd -= AddOutputImageControl;
            hyToolBlockComponent.OutputImageAdd += AddOutputImageControl;
            hyToolBlockComponent.OutputImageRemove -= RemoveOutputImageControl;
            hyToolBlockComponent.OutputImageRemove += RemoveOutputImageControl;
            hyToolBlockComponent.ShowOutputImage -= ShowOutputImage;
            hyToolBlockComponent.ShowOutputImage += ShowOutputImage;
            hyToolBlockComponent.ShowOutputGraphic -= HyToolBlockComponent_ShowOutputGraphic;
            hyToolBlockComponent.ShowOutputGraphic += HyToolBlockComponent_ShowOutputGraphic;
            hyToolBlockComponent.ClearOutputGraphic -= HyToolBlockComponent_ClearOutputGraphic;
            hyToolBlockComponent.ClearOutputGraphic += HyToolBlockComponent_ClearOutputGraphic;

            GL.Kit.IO.IniFileUtils.SetFilePath(HyEye.API.GlobalParams.Output_Graphic_IniFile_FilePath);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    string strOutputName = GL.Kit.IO.IniFileUtils.ReadIniData($"{showTaskName}", $"Row{i}Col{j}OutputName", "");
                    if (string.IsNullOrEmpty(strOutputName))
                    {
                        if (hyToolBlockComponent.GetOutputsTerminal() != null && hyToolBlockComponent.GetOutputsTerminal().Count > 0)
                        {
                            foreach (HyTerminal terminal in hyToolBlockComponent.GetOutputsTerminal().Where(a => a.ValueType == typeof(HyImage) || a.ValueType == typeof(Bitmap)))
                            {
                                if (displayControlSet.GetDisplayControl(showTaskName, terminal.Name) != null &&
                                    displayControlSet.GetDisplayControl(showTaskName, terminal.Name).GetType() == typeof(OutputDisplayTaskImageComponent))
                                {
                                    if(!((OutputDisplayTaskImageComponent)displayControlSet.GetDisplayControl(showTaskName, terminal.Name)).IsBinding)
                                    {
                                        strOutputName = terminal.Name;
                                        GL.Kit.IO.IniFileUtils.WriteIniData($"{showTaskName}", $"Row{i}Col{j}OutputName", strOutputName);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                            continue;
                    }

                    IDisplayTaskImageComponent displayControl = displayControlSet.GetDisplayControl(showTaskName, strOutputName);

                    if (displayControl == null)
                    {
                        GL.Kit.IO.IniFileUtils.WriteIniData($"{showTaskName}", $"Row{i}Col{j}OutputName", "");
                        continue;
                    }

                    if (displayControl.GetType() != typeof(OutputDisplayTaskImageComponent))
                    {
                        log.Warn($"输出结果的显示只支持类型为“{typeof(OutputDisplayTaskImageComponent)}”的控件！");
                        return;
                    }

                    OutputDisplayTaskImageComponent outputDisplayControl = (OutputDisplayTaskImageComponent)displayControl;
                    if (outputDisplayControl.IsBinding)
                        continue;


                    int index = 0;
                    List<string> lstGraphicNames = outputDisplayControl.GraphicNames;
                    if (lstGraphicNames == null)
                        lstGraphicNames = new List<string>();
                    while (true)
                    {
                        index++;
                        string strGraphicName = GL.Kit.IO.IniFileUtils.ReadIniData($"{showTaskName}", $"Row{i}Col{j}GraphicName{index}", "");
                        if (!string.IsNullOrEmpty(strGraphicName))
                        {
                            if (!lstGraphicNames.Contains(strGraphicName))
                                lstGraphicNames.Add(strGraphicName);
                        }
                        else
                            break;
                    }
                    outputDisplayControl.GraphicNames = lstGraphicNames;


                    outputDisplayControl.IsBinding = true;
                    outputDisplayControl.ShowSelectOutput -= DisplayControl_DoubleClick;
                    outputDisplayControl.ShowSelectOutput += DisplayControl_DoubleClick;
                    displayControlSet.TaskRepo_AcqImageUpdate(outputDisplayControl);

                    displayArr[i, j] = outputDisplayControl.DisplayedControl;
                    outputDisplayControl.Visible = true;
                    outputDisplayControl.Dock = DockStyle.Fill;

                    outputDisplayControl.DisplayedControl.SendToBack();
                }
            }

            dicDisplayArr[showTaskName] = displayArr;
        }





        private void HyToolBlockComponent_ShowOutputGraphic(string taskName, string graphicName, object value)
        {
            IDisplayTaskImageComponent displayControl = displayControlSet.GetDisplayControlByGraphicName(taskName, graphicName);
            if(displayControl == null)
            {
                log.Warn($"任务名为[{taskName}]，输出名为[{graphicName}]的输出未绑定显示控件！");
                return;
            }

            if (dicDisplayArr == null || dicDisplayArr[taskName] == null || dicDisplayArr[taskName].Length < 1)
                return;

            //只有当前Form的绑定控件，才显示
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (dicDisplayArr[taskName][i, j] != null)
                    {
                        IDisplayTaskImageComponent tempControl = (IDisplayTaskImageComponent)dicDisplayArr[taskName][i, j];
                        if (tempControl.TaskName == displayControl.TaskName && tempControl.AcqOrCalibName == displayControl.AcqOrCalibName)
                        {
                            displayControl?.ShowGraphic(value);
                            return;
                        }
                    }
                }
            }
        }

        private void HyToolBlockComponent_ClearOutputGraphic(string taskName, string graphicName)
        {
            IDisplayTaskImageComponent displayControl = displayControlSet.GetDisplayControlByGraphicName(taskName, graphicName);
            if (displayControl == null)
            {
                log.Warn($"任务名为[{taskName}]，输出名为[{graphicName}]的输出未绑定显示控件！");
                return;
            }
            displayControl?.ClearGraphic();
        }

        private void DisplayControl_DoubleClick(object sender, EventArgs e)
        {
            IDisplayTaskImageComponent displayControl = (IDisplayTaskImageComponent)sender;
            //把row index和col index传递进去
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (dicDisplayArr[showTaskName][i, j] != null)
                    {
                        IDisplayTaskImageComponent tempControl = (IDisplayTaskImageComponent)dicDisplayArr[showTaskName][i, j];
                        if (displayControl == tempControl)
                        {
                            FrmSelectOutputShow frmSelectOutput = new FrmSelectOutputShow(showTaskName, i, j, this.toolBlockComponentSet, displayControl);
                            frmSelectOutput.ShowDialog();
                            return;
                        }
                    }
                }
            }
        }

        private void AddOutputImageControl(string taskName, string outputName)
        {
            if(this.displayControlSet.TaskRepo_ControlSelect(taskName, outputName) == null)
            {
                GL.Kit.IO.IniFileUtils.SetFilePath(HyEye.API.GlobalParams.Output_Graphic_IniFile_FilePath);

                Control[,] displayArr;
                if (dicDisplayArr.ContainsKey(taskName))
                    displayArr = dicDisplayArr[taskName];
                else
                    displayArr = new Control[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (displayArr[i, j] == null)
                        {
                            this.displayControlSet.TaskRepo_AcqImageAdd(taskName, outputName);
                            IDisplayTaskImageComponent displayControl = displayControlSet.GetDisplayControl(taskName, outputName);

                            if (displayControl == null)
                            {
                                if (displayControl == null)
                                {
                                    log.Warn($"未找到Task名称为“{taskName}”及Output名称为“{outputName}”的控件！");
                                    return;
                                }
                            }
                                
                            if (displayControl.GetType() != typeof(OutputDisplayTaskImageComponent))
                            {
                                if (displayControl == null)
                                {
                                    log.Warn($"输出结果的显示只支持类型为“{typeof(OutputDisplayTaskImageComponent)}”的控件！");
                                    return;
                                }
                            }
                                
                            OutputDisplayTaskImageComponent outputDisplayControl = (OutputDisplayTaskImageComponent)displayControl;
                            
                            outputDisplayControl.IsBinding = true;
                            outputDisplayControl.ShowSelectOutput -= DisplayControl_DoubleClick;
                            outputDisplayControl.ShowSelectOutput += DisplayControl_DoubleClick;
                            displayControlSet.TaskRepo_AcqImageUpdate(outputDisplayControl);

                            displayArr[i, j] = outputDisplayControl.DisplayedControl;
                            outputDisplayControl.Visible = true;
                            outputDisplayControl.Dock = DockStyle.Fill;
                            outputDisplayControl.DisplayedControl.SendToBack();

                            dicDisplayArr[taskName] = displayArr;
                            RefreshDisplay(taskName);

                            GL.Kit.IO.IniFileUtils.WriteIniData($"{taskName}", $"Row{i}Col{j}OutputName", outputName);

                            return;
                        }
                    }
                }
            }
        }

        private void RemoveOutputImageControl(string taskName, string outputName)
        {
            IDisplayTaskImageComponent displayControl = this.displayControlSet.TaskRepo_ControlSelect(taskName, outputName);
            if(displayControl != null)
            {
                Control[,] displayArr;
                if (dicDisplayArr.ContainsKey(taskName))
                    displayArr = dicDisplayArr[taskName];
                else
                    displayArr = new Control[rowCount, colCount];

                GL.Kit.IO.IniFileUtils.SetFilePath(HyEye.API.GlobalParams.Output_Graphic_IniFile_FilePath);

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if(displayArr[i, j] != null)
                        {
                            IDisplayTaskImageComponent tempControl = (IDisplayTaskImageComponent)displayArr[i, j];
                            if(tempControl.TaskName == displayControl.TaskName && tempControl.AcqOrCalibName == displayControl.AcqOrCalibName)
                            {
                                displayArr[i, j] = null;
                                this.displayControlSet.TaskRepo_AcqImageDelete(taskName, outputName);
                                dicDisplayArr[taskName] = displayArr;
                                RefreshDisplay(taskName);

                                string strOutputName = GL.Kit.IO.IniFileUtils.ReadIniData($"{showTaskName}", $"Row{i}Col{j}OutputName", "");
                                if (!string.IsNullOrEmpty(strOutputName))
                                    GL.Kit.IO.IniFileUtils.WriteIniData($"{showTaskName}", $"Row{i}Col{j}OutputName", "");

                                int index = 1;
                                while(true)
                                {
                                    string strGraphicName = GL.Kit.IO.IniFileUtils.ReadIniData($"{showTaskName}", $"Row{i}Col{j}GraphicName{index}", "");
                                    if (!string.IsNullOrEmpty(strGraphicName))
                                        GL.Kit.IO.IniFileUtils.WriteIniData($"{showTaskName}", $"Row{i}Col{j}GraphicName{index}", "");
                                    else
                                        break;
                                    index++;
                                }

                                return;
                            }
                        }
                    }
                }
            }
            //int index = tableLayoutPanel.Controls.IndexOf((Control)this.displayControlSet.TaskRepo_ControlSelect(taskName, outputName));
            //if (index > -1)
            //{
            //    this.displayControlSet.TaskRepo_AcqImageDelete(taskName, outputName);
            //    this.displayControlSet.TaskRepo_ResetBinding();
            //    RefreshDisplay();
            //}


            //int rowIndex = index / colCount;
            //int colIndex = index % colCount;

            //for (; rowIndex < rowCount; rowIndex++)
            //{
            //    for (; colIndex < colCount; colIndex++)
            //    {
            //        Control control = null;
            //        bool isAdd = false;
            //        if (index + 1 < rowCount * colCount && tableLayoutPanel.Controls.Count > index + 1)
            //        {
            //            control = tableLayoutPanel.Controls[index + 1];
            //            isAdd = true;
            //        }
            //        tableLayoutPanel.Controls.RemoveAt(index++);
            //        if (isAdd)
            //        {
            //            tableLayoutPanel.Controls.Add(control, colIndex, rowIndex);
            //        }
            //    }
            //}
        }

        private void ShowOutputImage(string taskName, string outputName, object value)
        {
            if(!curTaskName.Equals(taskName))
            {
                delegateRefrshControl delegateControl = new delegateRefrshControl(RefreshDisplay);
                this.Invoke(delegateControl, taskName);
                curTaskName = taskName;
            }

            IDisplayTaskImageComponent displayControl = displayControlSet.GetDisplayControl(taskName, outputName);
            //只有当前Form的绑定控件，才显示
            if (displayControl == null || dicDisplayArr == null || dicDisplayArr[taskName] == null || dicDisplayArr[taskName].Length < 1)
                return;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (dicDisplayArr[taskName][i, j] != null)
                    {
                        IDisplayTaskImageComponent tempControl = (IDisplayTaskImageComponent)dicDisplayArr[taskName][i, j];
                        if (tempControl.TaskName == displayControl.TaskName && tempControl.AcqOrCalibName == displayControl.AcqOrCalibName)
                        {
                            //if (value is HyImage)
                            //{
                            //    displayControl?.ShowImage(((HyImage)value).Image, false);
                            //    return;
                            //}
                            
                            displayControl?.ShowImage(value, false);
                            return;
                        }
                    }
                }
            }
        }
    }
}
