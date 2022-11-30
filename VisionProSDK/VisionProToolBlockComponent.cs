using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace VisionSDK._VisionPro
{
    /// <summary>
    /// 视觉处理组件
    /// </summary>
    public class VisionProToolBlockComponent : IToolBlockComponent
    {
        static Color BackColor { get; } = Color.Gray;

        public event EventHandler<ToolBlockRanEventArgs> Ran;
        public event RemovedCalibHandle ComponentRemovedCalib;

        readonly IBasicRepository basicRepo;
        readonly INameMappingRepository nameMappingRepo;
        readonly IPathRepository pathRepo;
        readonly IGLog log;

        CogToolBlockEditV2 toolBlockEdit;
        CogToolBlock toolBlock;
        string vppPath;

        static readonly ParamInfo AcqIndexParam = new ParamInfo { Name = InputOutputConst.Input_AcqIndex, Type = ParamInfo.IntType, Description = "拍照索引" };
        static readonly ParamInfo ErrorCodeParam = new ParamInfo { Name = InputOutputConst.Output_ErrorCode, Type = ParamInfo.IntType, Description = "错误码" };

        int i = 1;  // Img1

        public VisionProToolBlockComponent(
            TaskInfoVO task,
            IBasicRepository basicRepo,
            INameMappingRepository nameMappingRepo,
            IPathRepository pathRepo,
            IGLog log)
        {
            this.basicRepo = basicRepo;
            this.nameMappingRepo = nameMappingRepo;
            this.pathRepo = pathRepo;
            this.log = log;
            TaskName = task.Name;

            vppPath = pathRepo.GetTaskVppPath(TaskName, task.Type);
            if (File.Exists(vppPath))
            {
                LoadToolBlock(vppPath);
            }
            else
            {
                CreateToolBlock(task);
            }

            //用打开按钮加载标定 VPP 会触发此事件，故而此方法无法达到预期效果
            //toolBlock.Tools.RemovedItem += Tools_RemovedItem;

            TaskVisionMappingVO taskVisionMapping = nameMappingRepo.GetTaskVisionMapping(TaskName);
            if (taskVisionMapping == null)
            {
                nameMappingRepo.AddTaskVisionMapping(TaskName);
            }
        }

        void LoadToolBlock(string vppPath)
        {
            try
            {
                toolBlock = (CogToolBlock)CogSerializer.LoadObjectFromFile(vppPath);
            }
            catch (Exception e)
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_LoadVpp, R_Fail, $"加载 VPP 文件 \"{vppPath}\" 出错，{e.Message}"));
            }
        }

        void CreateToolBlock(TaskInfoVO task)
        {
            toolBlock = new CogToolBlock();

            if (task.CameraAcquireImage != null)
            {
                foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                {
                    AddAcqImage(acqImage.Name);

                    if (!string.IsNullOrEmpty(acqImage.CheckerboardName))
                    {
                        AddCalibration(CalibrationType.Checkerboard, acqImage.CheckerboardName);
                    }
                    if (acqImage != null && acqImage.HandEyeNames.Count > 0)
                    {
                        foreach (string handeyeName in acqImage.HandEyeNames)
                        {
                            AddCalibration(CalibrationType.HandEye, handeyeName);
                        }
                    }
                    if (!string.IsNullOrEmpty(acqImage.JointName))
                    {
                        AddCalibration(CalibrationType.Checkerboard, $"{acqImage.JointName}_{acqImage.Name}");
                    }
                }
            }

            AddInput(AcqIndexParam);
            AddOutput(ErrorCodeParam);
        }

        public string TaskName { get; private set; }

        public Control DisplayedControl
        {
            get
            {
                if (toolBlockEdit == null)
                {
                    toolBlockEdit = new CogToolBlockEditV2
                    {
                        Subject = toolBlock
                    };
                    toolBlockEdit.SubjectChanged += (sender, e) =>
                    {
                        toolBlock = toolBlockEdit.Subject;
                    };
                }

                return toolBlockEdit;
            }
        }

        public void RenameTaskName(string newName)
        {
            TaskName = newName;
            vppPath = pathRepo.GetTaskVppPath(newName, TaskType.VP);
        }

        public bool Check()
        {
            TaskVisionMappingVO nameMapper = nameMappingRepo.GetTaskVisionMapping(TaskName);

            //DG 409 Check => NullReferenceException 
            if (toolBlock == null)
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"toolBlock 对象为null"));

                return false;
            }
            //DG 409 Check => NullReferenceException 
            if (toolBlock.Inputs == null)
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"toolBlock.Inputs 对象为null"));

                return false;
            }
            if (!toolBlock.Inputs.Contains(InputOutputConst.Input_AcqIndex))
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"Inputs 中缺失[{InputOutputConst.Input_AcqIndex}]"));

                return false;
            }
            if (!toolBlock.Outputs.Contains(InputOutputConst.Output_ErrorCode))
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"Outputs 中缺失[{InputOutputConst.Output_ErrorCode}]"));

                return false;
            }
            if (toolBlock.Outputs[InputOutputConst.Output_ErrorCode].ValueType != ParamInfo.IntType)
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"Outputs 参数[{InputOutputConst.Output_ErrorCode}]的类型必须为 Int"));

                return false;
            }

            if (nameMapper.Inputs != null)
            {
                foreach (NameMapperVO mapper in nameMapper.Inputs)
                {
                    if (!toolBlock.Inputs.Contains(mapper.Value))
                    {
                        log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"缺失拍照[{mapper.Key}]对应的 Input [{mapper.Value}]"));

                        return false;
                    }
                }

                foreach (NameMapperVO mapper in nameMapper.Graphics)
                {
                    if (!toolBlock.Outputs.Contains(mapper.Value))
                    {
                        log.Error(new TaskVisionLogMessage(TaskName, null, A_Check, R_Fail, $"缺失拍照[{mapper.Key}]对应的 Graphic [{mapper.Value}]"));

                        return false;
                    }
                }
            }

            return true;
        }

        public void ResetDefaultParam()
        {
            TaskVisionMappingVO nameMapper = nameMappingRepo.GetTaskVisionMapping(TaskName);

            if (!toolBlock.Inputs.Contains(InputOutputConst.Input_AcqIndex))
            {
                AddInput(AcqIndexParam);
            }
            if (!toolBlock.Outputs.Contains(InputOutputConst.Output_ErrorCode))
            {
                AddOutput(ErrorCodeParam);
            }

            if (nameMapper.Inputs != null)
            {
                foreach (NameMapperVO input in nameMapper.Inputs)
                {
                    if (!toolBlock.Inputs.Contains(input.Value))
                    {
                        toolBlock.Inputs.Add(new CogToolBlockTerminal(input.Value, CogImageType));
                    }
                }

                foreach (NameMapperVO graphic in nameMapper.Graphics)
                {
                    if (!toolBlock.Outputs.Contains(graphic.Value))
                    {
                        toolBlock.Outputs.Add(new CogToolBlockTerminal(graphic.Value, CogGraphicCollectionType));
                    }
                }
            }
        }

        #region 取像

        /// <summary>
        /// 添加取像
        /// </summary>
        public void AddAcqImage(string acqImageName)
        {
            string inputName = getDefaultInputName();
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(inputName, CogImageType) { Description = acqImageName };
            toolBlock.Inputs.Add(terminal);
            nameMappingRepo.AddAcqImageMapping(TaskName, acqImageName, inputName);
        }

        string getDefaultInputName()
        {
            string inputName = $"Img{i++}";

            while (toolBlock.Inputs.Contains(inputName))
            {
                inputName = $"Img{i++}";
            }

            return inputName;
        }

        /// <summary>
        /// 删除取像
        /// </summary>
        public void RemoveAcqImage(string acqImageName)
        {
            string inputName = nameMappingRepo.GetAcqImageMapping(TaskName, acqImageName);

            if (!string.IsNullOrEmpty(inputName) && toolBlock.Inputs.Contains(inputName))
            {
                toolBlock.Inputs.Remove(inputName);
            }

            nameMappingRepo.DeleteAcqImageMapping(TaskName, acqImageName);
        }

        #endregion

        #region 工具

        /// <summary>
        /// 添加标定
        /// </summary>
        public void AddCalibration(CalibrationType calibType, string calibName)
        {
            if (toolBlock.Tools.Contains(calibName))
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_AddTool, R_Fail, $"工具[{calibName}]已存在"));

                throw new VisionException("ToolBlock 中工具已存在");
            }

            if (calibType == CalibrationType.Checkerboard)
            {
                CogCalibCheckerboardTool checkerboardTool = VisionProUtils.CreateCheckerboardTool();
                checkerboardTool.Name = calibName;

                toolBlock.Tools.Add(checkerboardTool);
            }
            else if (calibType == CalibrationType.HandEye || calibType == CalibrationType.HandEyeSingle)
            {
                CogCalibNPointToNPointTool nPointToNPointTool = VisionProUtils.CreateNPointToNPointTool();
                nPointToNPointTool.Name = calibName;

                toolBlock.Tools.Add(nPointToNPointTool);
            }
            else if (calibType == CalibrationType.Joint)
            {
                CogCalibNPointToNPointTool nPointToNPointTool = VisionProUtils.CreateNPointToNPointTool();
                nPointToNPointTool.Name = calibName;

                toolBlock.Tools.Add(nPointToNPointTool);
            }
            else
            {
                // 不会走到这里的
                throw new VisionException("不支持的标定类型");
            }

            log.Info(new TaskVisionLogMessage(TaskName, null, A_AddTool, R_Success, $"添加标定工具[{calibName}]"));
        }

        /// <summary>
        /// 删除工具
        /// </summary>
        public void RemoveTool(string toolName)
        {
            if (toolBlock.Tools.Contains(toolName))
            {
                toolBlock.Tools.Remove(toolName);

                log.Info(new TaskVisionLogMessage(TaskName, null, A_DelTool, R_Success, $"删除工具[{toolName}]"));
            }
        }

        /// <summary>
        /// 重命名工具
        /// </summary>
        public void RenameTool(string oldToolName, string newToolName)
        {
            if (toolBlock.Tools.Contains(oldToolName))
            {
                toolBlock.Tools[oldToolName].Name = newToolName;

                log.Info(new TaskVisionLogMessage(TaskName, null, A_RenameTool, R_Success, $"重命名工具[{oldToolName}] -> [{newToolName}]"));
            }
        }

        /// <summary>
        /// 获取工具
        /// </summary>
        public object GetTool(string toolName)
        {
            if (toolBlock.Tools.Contains(toolName))
                return toolBlock.Tools[toolName];
            else
                return null;
        }

        public Control GetToolEdit(string toolName)
        {
            object tool = GetTool(toolName);
            if (tool is CogCalibNPointToNPointTool np)
            {
                return new CogCalibNPointToNPointEditV2()
                {
                    Subject = np,
                    Dock = DockStyle.Fill
                };
            }

            return null;
        }

        #endregion

        #region 入参

        static readonly Type CogImageType = typeof(ICogImage);
        static readonly Type CogGraphicCollectionType = typeof(CogGraphicCollection);

        public List<ParamInfo> GetInputs()
        {
            return toolBlock.Inputs.Where(t => !CogImageType.IsAssignableFrom(t.ValueType) && t.Name != InputOutputConst.Input_AcqIndex)
                .Select(t => new ParamInfo
                {
                    Name = t.Name,
                    Type = t.ValueType,
                    Value = t.Value,
                    Description = t.Description
                }).ToList();
        }

        public void AddInput(ParamInfo input)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(input.Name, input.Type)
            {
                Description = input.Description,
                Value = input.Value?.ChanageType(input.Type)
            };

            toolBlock.Inputs.Add(terminal);
        }

        public void RenameInput(string oldname, string newname)
        {
            toolBlock.Inputs[oldname].Name = newname;
        }

        public void ChangeInputType(string paramName, Type newType)
        {
            int index = getInputIndex(paramName);
            if (index > 0)
            {
                CogToolBlockTerminal terminal = new CogToolBlockTerminal(toolBlock.Inputs[index].Name, newType);
                toolBlock.Inputs[index] = terminal;
            }
        }

        int getInputIndex(string paramName)
        {
            int index = 0;

            foreach (CogToolBlockTerminal terminal in toolBlock.Inputs)
            {
                if (terminal.Name == paramName)
                    return index;
                else
                    index++;
            }
            return -1;
        }

        public void SetInputValue(string paramName, object value)
        {
            CogToolBlockTerminal terminal = toolBlock.Inputs[paramName];
            if (value == null || value.ToString().Length == 0)
                terminal.Value = null;
            else
                terminal.Value = value.ChanageType(terminal.ValueType);
        }

        public void SetInputDescription(string paramName, string description)
        {
            CogToolBlockTerminal terminal = toolBlock.Inputs[paramName];
            terminal.Description = description;
        }

        public void DeleteInput(string paramName)
        {
            if (toolBlock.Inputs.Contains(paramName))
            {
                toolBlock.Inputs.Remove(paramName);
            }
        }

        public bool InputContains(string paramName)
        {
            return toolBlock.Inputs.Contains(paramName);
        }

        #endregion

        #region 出参

        LinkedDictionary<string, object> getOutputs(int acqImageIndex)
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            foreach (CogToolBlockTerminal terminal in toolBlock.Outputs)
            {
                output.Add(terminal.Name, terminal.Value);
            }

            return output;
        }

        /// <summary>
        /// 获取当前的 Output
        /// </summary>
        public LinkedDictionary<string, object> GetErrorOutputs()
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            foreach (CogToolBlockTerminal terminal in toolBlock.Outputs)
            {
                switch (terminal.Value)
                {
                    case int _:
                        output.Add(terminal.Name, 999);
                        break;
                    case double _:
                        output.Add(terminal.Name, 999d);
                        break;
                    case string _:
                        output.Add(terminal.Name, "999");
                        break;
                    default:
                        output.Add(terminal.Name, null);
                        break;
                }
            }

            return output;
        }

        public void AddOutput(ParamInfo output)
        {
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(output.Name, output.Type)
            {
                Description = output.Description
            };

            toolBlock.Outputs.Add(terminal);
        }

        public bool OutputContains(string paramName)
        {
            return toolBlock.Outputs.Contains(paramName);
        }

        public void SetOutputValue(string paramName, object value)
        {
            CogToolBlockTerminal terminal = toolBlock.Outputs[paramName];
            if (value == null || value.ToString().Length == 0)
                terminal.Value = null;
            else
                terminal.Value = value.ChanageType(terminal.ValueType);
        }

        #endregion

        #region SetImage

        void SetImage(string acqImageName, Bitmap bitmap, bool isGrey)
        {
            string toolName = nameMappingRepo.GetAcqImageMapping(TaskName, acqImageName);

            ICogImage cogImage = VisionProUtils.ToCogImage(bitmap, isGrey);

            //if (toolBlock.Inputs[toolName].Value is IDisposable dis)
            //{
            //    // 这句代码会报错，实在是想不通
            //    dis?.Dispose();
            //}

            toolBlock.Inputs[toolName].Value = cogImage;

            bitmap.Dispose();
        }

        #endregion

        #region 运行

        BlockingCollection<TaskRunningTimeParams> block;

        Thread runningThread;

        public void StartSerial()
        {
            block = new BlockingCollection<TaskRunningTimeParams>();

            runningThread = new Thread(RunSerial)
            {
                IsBackground = true
            };
            runningThread.SetApartmentState(ApartmentState.STA);
            runningThread.Start();
        }

        public void StopSerial()
        {
            block.CompleteAdding();
        }

        void RunSerial()
        {
            while (!block.IsCompleted)
            {
                TaskRunningTimeParams @param = null;

                try
                {
                    @param = block.Take();
                }
                catch (InvalidOperationException) { }

                if (@param != null)
                {
                    (int errCode, LinkedDictionary<string, object> outputs) = RunToolBlock(@param.HyImage, @param.Params);

                    @param.CallBack?.Invoke(@param.HyImage.AcqOrCalibName, @param.HyImage.AcqOrCalibIndex, errCode, outputs);
                }
            }
        }

        (int errorCode, LinkedDictionary<string, object> outputs) RunToolBlock(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params)
        {
            //VisionProUtils.DisposeInternalResources(toolBlock);
            LinkedDictionary<string, object> outputs = null;

            try
            {
                SetImage(hyImage.AcqOrCalibName, hyImage.Bitmap, hyImage.IsGrey);

                SetInputValue(InputOutputConst.Input_AcqIndex, hyImage.AcqOrCalibIndex);
                if (@params != null)
                {
                    foreach (var (Name, Value) in @params)
                    {
                        SetInputValue(Name, Value);
                    }
                }

                log.Debug(new TaskVisionLogMessage(TaskName, hyImage.AcqOrCalibName, A_RunToolBlock, R_Start, $"[{hyImage.CmdID}]ToolBlock 运行开始"));

                TimeSpan ts = FuncWatch.ElapsedTime(() =>
                {
                    toolBlock.Run();
                });

                outputs = getOutputs(hyImage.AcqOrCalibIndex);

                log.Debug(new TaskVisionLogMessage(TaskName, hyImage.AcqOrCalibName, A_RunToolBlock, R_End,
                    $"\"[{hyImage.CmdID}]ToolBlock 运行：{string.Join(",", outputs.Select(kv => $"{kv.Key} = {kv.Value}"))}\"", ts.TotalMilliseconds));

                Ran?.Invoke(this, new ToolBlockRanEventArgs(TaskName, hyImage.AcqOrCalibName, hyImage.AcqOrCalibIndex, outputs));

                return (ErrorCodeConst.OK, outputs);
            }
            catch (AggregateException aex)
            {
                outputs = getOutputs(hyImage.AcqOrCalibIndex);

                log.Error(new TaskVisionLogMessage(TaskName, hyImage.AcqOrCalibName, A_RunToolBlock, R_Fail, aex.InnerException.Message));

                return (ErrorCodeConst.ToolBlockRunFail, outputs);
            }
            catch (Exception aex)
            {
                outputs = getOutputs(hyImage.AcqOrCalibIndex);

                log.Error(new TaskVisionLogMessage(TaskName, hyImage.AcqOrCalibName, A_RunToolBlock, R_Fail, aex.Message));

                return (ErrorCodeConst.ToolBlockRunFail, outputs);
            }
        }

        public object CreateRecord(int index)
        {
            try
            {
                //如果SubRecords为0，而默认的index又是0，原判断会走到SubRecords[index]，是错误的。
                if (toolBlock.CreateLastRunRecord().SubRecords.Count == 0)
                    return toolBlock.CreateLastRunRecord();

                if (index >= toolBlock.CreateLastRunRecord().SubRecords.Count)
                {
                    log.Error(new TaskVisionLogMessage(TaskName, null, A_GetRecord, R_Fail, $"Record 索引超出范围"));
                    return null;
                }

                if (index < 0)
                    return toolBlock.CreateLastRunRecord();
                else
                    return toolBlock.CreateLastRunRecord().SubRecords[index];
            }
            catch (Exception ex)
            {
                log.Error($"CreateRecord Exception {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 串行
        /// update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
        /// </summary>
        public void RunSerial(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params, string subName, ToolBlockCallBack callBack = null)
        {
            try
            {
                block.Add(new TaskRunningTimeParams
                {
                    HyImage = hyImage,
                    Params = @params,
                    CallBack = callBack,
                    SubName = subName
                }) ;
            }
            catch (Exception ex)
            {
                //Fatal.log捕捉过此异常，block先标记为完成添加，add失败后闪退
                log.Warn($"RunSerial => block has CompleteAdding. {ex.Message}");
            }
        }

        #endregion

        public void ExpandAll()
        {
            //DG ExpandAll => NullReferenceException
            try
            {
                if (toolBlockEdit != null)
                {
                    Control[] controls = toolBlockEdit.Controls.Find("_treeView", true);
                    if (controls.Length > 0)
                    {
                        controls[0].AsyncAction(c =>
                        {
                            ((TreeView)c).ExpandAll();
                        });
                    }
                }
                else
                {
                    log.Error($"ExpandAll Excute Fail ： toolBlockEdit is null");
                }
            }
            catch (Exception ex)
            {
                log.Error($"ExpandAll Exception {ex.Message}");
            }
        }

        public void Save()
        {
            if (toolBlock == null)
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Save, R_Fail, $"保存至“{vppPath}”失败，toolBlock为null"));
                return;
            }

            string savePath = AppDomain.CurrentDomain.BaseDirectory + vppPath;
            string filename = vppPath.Split('\\').Last();
            string curVppPath = vppPath.Replace(".", "_temp.");

            //VisionProUtils.SaveSubject(toolBlock, vppPath, basicRepo.VPPExcludeDataBindings);
            try
            {
                VisionProUtils.SaveSubject(toolBlock, curVppPath, basicRepo.VPPExcludeDataBindings);
            }
            catch (Exception ex)
            {
                log.Error(new TaskVisionLogMessage(TaskName, null, A_Save, R_Fail, $"保存至“{vppPath}”失败，{ex.Message}"));
                return;
            }

            File.Delete(savePath);
            FileUtils.Rename(curVppPath, filename);

            log.Info(new TaskVisionLogMessage(TaskName, null, A_Save, R_Success, $"保存至“{vppPath}”"));

            nameMappingRepo.Save();
        }


        public void Dispose()
        {
            toolBlock?.Dispose();

            toolBlockEdit?.Dispose();
        }

        //add by LuoDian @ 20211213 HyToolBlock那里通过添加子料号，实现实时切换料号的功能，在基类接口那里添加了这个接口，所以在这里需要先实现下接口
        public void AddNewHyToolBlock(string curSubName)
        {
            
        }

        //add by LuoDian @ 20211213 HyToolBlock那里通过添加子料号，实现实时切换料号的功能，在基类接口那里添加了这个接口，所以在这里需要先实现下接口
        public void DeleteHyToolBlockBySubName(string subName)
        {
            
        }
    }
}
