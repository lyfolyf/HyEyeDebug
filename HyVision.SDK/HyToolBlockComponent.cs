using GL.Kit.Log;
using HyEye.API;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using HyVision.Models;
using HyVision.Tools;
using HyVision.Tools.ImageDisplay;
using HyVision.Tools.TerminalCollection;
using HyVision.Tools.ToolBlock;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyVision.SDK
{
    public class HyToolBlockComponent : IToolBlockComponent
    {
        public event EventHandler<ToolBlockRanEventArgs> Ran;

        public event RemovedCalibHandle ComponentRemovedCalib;

        //add by LuoDian @ 20210722 显示输出结果
        public event AcqImageNameHandler OutputImageAdd;
        public event AcqImageNameHandler OutputImageRemove;
        public event ShowOutputImageHandler ShowOutputImage;
        //add by LuoDian @ 20210804 显示缺陷
        public event ShowOutputImageHandler ShowOutputGraphic;
        //add by LuoDian @ 20211101 显示缺陷List之前需要先清除之前的缺陷
        public event ClearOutputGraphicHandler ClearOutputGraphic;

        //add by LuoDian @ 20211210 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        static readonly Type ImageType = typeof(HyImage);

        static readonly ParamInfo AcqIndexParam = new ParamInfo { Name = InputOutputConst.Input_AcqIndex, Type = ParamInfo.IntType, Description = "拍照索引" };
        static readonly ParamInfo ErrorCodeParam = new ParamInfo { Name = InputOutputConst.Output_ErrorCode, Type = ParamInfo.IntType, Description = "错误码" };

        readonly INameMappingRepository nameMappingRepo;
        readonly IPathRepository pathRepo;
        readonly IGLog log;

        readonly HyToolBlock toolBlock;
        List<int> ImageIndexes = new List<int>();

        HyToolBlockEdit toolBlockEdit;
        string vppPath;

        public HyToolBlockComponent(
            TaskInfoVO task,
            INameMappingRepository nameMappingRepo,
            IPathRepository pathRepo,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            TaskName = task.Name;

            this.nameMappingRepo = nameMappingRepo;
            this.pathRepo = pathRepo;
            this.log = log;

            //add by LuoDian @ 20211210 用于子料号的快速切换
            this.materialRepo = materialRepo;

            vppPath = pathRepo.GetTaskVppPath(TaskName, task.Type);
            //add by He @ 20210913
            //vppPath = @"E:\项目\HyEye\代码\HyEye\编译\DLL\config\料号1\HY 任务1.hy";

            if (File.Exists(vppPath))
            {
                try
                {
                    toolBlock = (HyToolBlock)HySerializer.LoadFromFile(vppPath);
                }
                catch (Exception e)
                {
                    //add by LuoDian @ 20210827 有时会出现toolBlock为null的情况，怀疑是加载配置文件的时候抛了异常，所以在异常处理这重新初始化对象
                    toolBlock = new HyToolBlock();
                    AddInput(AcqIndexParam);
                    AddOutput(ErrorCodeParam);


                    log.Error(new TaskVisionLogMessage(TaskName, null, A_LoadVpp, R_Fail, $"加载 HyToolBlock 文件 \"{vppPath}\" 出错，{e.Message}"));
                }
            }
            else
            {
                toolBlock = new HyToolBlock();

                AddInput(AcqIndexParam);
                AddOutput(ErrorCodeParam);
            }

            //add by LuoDian @ 20210722 显示输出结果
            toolBlock.Outputs.Inserted += AddOutputEvent;
            toolBlock.Outputs.Removed += RemoveOutputEvent;
        }

        public string TaskName { get; private set; }

        public Control DisplayedControl
        {
            get
            {
                if (toolBlockEdit == null)
                {
                    if (toolBlock.Tools.Count < 1)
                    {
                        HyToolCollection toolCollection = new HyToolCollection { MaterialSubName = materialRepo.CurSubName };
                        toolBlock.Tools.Add(toolCollection);
                    }

                    toolBlockEdit = new HyToolBlockEdit()
                    {
                        Subject = toolBlock
                    };
                }
                return toolBlockEdit;
            }
        }

        public bool Check()
        {
            TaskVisionMappingVO nameMapper = nameMappingRepo.GetTaskVisionMapping(TaskName);

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
            }

            return true;
        }

        public void RenameTaskName(string newName)
        {
            TaskName = newName;
            vppPath = pathRepo.GetTaskVppPath(newName, TaskType.HY);
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
                        toolBlock.Inputs.Add(new HyTerminal(input.Value, ImageType));
                    }
                }
            }
        }

        #region 取像

        public void AddAcqImage(string acqImageName)
        {
            string inputName = getDefaultInputName();
            HyTerminal terminal = new HyTerminal(inputName, ImageType) { Description = acqImageName };
            toolBlock.Inputs.Add(terminal);
            nameMappingRepo.AddAcqImageMapping(TaskName, acqImageName, inputName);
        }

        int i = 1;  // Img1

        string getDefaultInputName()
        {
            string inputName = $"Img{i++}";

            while (toolBlock.Inputs.Contains(inputName))
            {
                inputName = $"Img{i++}";
            }

            return inputName;
        }

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

        public void AddCalibration(CalibrationType calibType, string calibName)
        {
            throw new NotImplementedException();
        }

        public void RemoveTool(string toolName)
        {
            throw new NotImplementedException();
        }

        public void RenameTool(string oldToolName, string newToolName)
        {
            throw new NotImplementedException();
        }

        public object GetTool(string toolName)
        {
            throw new NotImplementedException();
        }

        public Control GetToolEdit(string toolName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 输入

        public List<ParamInfo> GetInputs()
        {
            return toolBlock.Inputs.Where(t => t.ValueType != ImageType && t.Name != InputOutputConst.Input_AcqIndex)
                .Select(t => new ParamInfo
                {
                    Name = t.Name,
                    Type = t.ValueType,
                    Value = t.Value,
                    Description = t.Description
                }).ToList();
        }

        public bool InputContains(string paramName)
        {
            return toolBlock.Inputs.Contains(paramName);
        }

        public void AddInput(ParamInfo input)
        {
            HyTerminal terminal = new HyTerminal(input.Name, input.Type)
            {
                Description = input.Description,
                Value = input.Value?.ChanageType(input.Type)
            };

            toolBlock.Inputs.Add(terminal);
        }

        public void DeleteInput(string paramName)
        {
            if (toolBlock.Inputs.Contains(paramName))
            {
                toolBlock.Inputs.Remove(paramName);
            }
        }

        public void RenameInput(string oldname, string newname)
        {
            toolBlock.Inputs[oldname].Name = newname;
        }

        public void SetInputValue(string paramName, object value)
        {
            HyTerminal terminal = toolBlock.Inputs[paramName];
            if (value == null || value.ToString().Length == 0)
                terminal.Value = null;
            else
                terminal.Value = value;
        }

        public void SetInputDescription(string paramName, string description)
        {
            toolBlock.Inputs[paramName].Description = description;
        }

        public void ChangeInputType(string paramName, Type newType)
        {
            //add by LuoDian @ 20211228 为了在修改指令参数的时候，把ToolBlock部分的参数同步更新到ToolBlock中
            HyTerminal terminal = new HyTerminal(paramName, newType)
            {
                Description = toolBlock.Inputs[paramName].Description
            };
            DeleteInput(paramName);
            toolBlock.Inputs.Add(terminal);
        }

        #region SetImage

        // Added by louis on Mar. 19 2022 解决收集多张图像再运行时其它参数信息不对应的问题
        void SetImage(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params)
        {
            string toolName = nameMappingRepo.GetAcqImageMapping(TaskName, hyImage.AcqOrCalibName);
            toolBlock.Inputs[toolName].Value = new HyImage(hyImage.Bitmap, hyImage.IsGrey, hyImage.AcqOrCalibIndex);
            toolBlock.Inputs[toolName].AttachedParams = @params;
        }

        #endregion

        #endregion

        #region 输出

        public LinkedDictionary<string, object> GetErrorOutputs()
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            foreach (HyTerminal terminal in toolBlock.Outputs)
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

        public LinkedDictionary<string, object> GetOutputs(string subName)
        {
            LinkedDictionary<string, object> output = new LinkedDictionary<string, object>();

            //add by LuoDian @ 20211101 显示缺陷List之前需要先清除之前的缺陷
            foreach (HyTerminal terminal in toolBlock.Outputs)
            {
                if (terminal.ValueType != null && terminal.ValueType == typeof(BaseHyROI))
                    ClearOutputGraphic?.Invoke(TaskName, terminal.Name);
                else if (terminal.ValueType != null && terminal.ValueType == typeof(List<BaseHyROI>))
                    ClearOutputGraphic?.Invoke(TaskName, terminal.Name);
                else if (terminal.ValueType != null && terminal.ValueType == typeof(List<HyDefectXLD>))
                    ClearOutputGraphic?.Invoke(TaskName, terminal.Name);
                else if (terminal.ValueType != null && terminal.ValueType == typeof(List<HyDefectRegion>))
                    ClearOutputGraphic?.Invoke(TaskName, terminal.Name);
            }

            foreach (HyTerminal terminal in toolBlock.Outputs)
            {
                if (terminal.MaterialSubName == subName)
                    output.Add(terminal.Name, terminal.Value);

                //add by LuoDian @ 20210722 显示输出结果
                if (terminal.ValueType == typeof(HyImage) && terminal.Value != null)
                    ShowOutputImage?.Invoke(TaskName, terminal.Name, ((HyImage)terminal.Value).Image);
                else if (terminal.ValueType == typeof(Bitmap) && terminal.Value != null)
                    ShowOutputImage?.Invoke(TaskName, terminal.Name, (Bitmap)terminal.Value);


                //add by LuoDian @ 20210804 显示缺陷
                if (terminal.ValueType != null && terminal.ValueType == typeof(HyDefectRegion) && terminal.Value != null)
                    ShowOutputGraphic?.Invoke(TaskName, terminal.Name, terminal.Value);
                if (terminal.ValueType != null && terminal.ValueType == typeof(HyDefectXLD) && terminal.Value != null)
                    ShowOutputGraphic?.Invoke(TaskName, terminal.Name, terminal.Value);
                if (terminal.ValueType != null && terminal.ValueType == typeof(BaseHyROI) && terminal.Value != null)
                    ShowOutputGraphic?.Invoke(TaskName, terminal.Name, terminal.Value);
                if (terminal.ValueType != null && terminal.ValueType == typeof(List<HyDefectXLD>) && terminal.Value != null)
                    ShowOutputGraphic?.Invoke(TaskName, terminal.Name, terminal.Value);
            }

            return output;
        }

        public bool OutputContains(string paramName)
        {
            throw new NotImplementedException();
        }

        public void AddOutput(ParamInfo output)
        {
            HyTerminal terminal = new HyTerminal(output.Name, output.Type)
            {
                Description = output.Description
            };

            toolBlock.Outputs.Add(terminal);

        }

        public void SetOutputValue(string paramName, object value)
        {
            throw new NotImplementedException();
        }

        //add by LuoDian @ 20210722 显示输出结果
        private void AddOutputEvent(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;

            if (terminal.ValueType == typeof(HyImage))
                OutputImageAdd?.Invoke(TaskName, terminal.Name);
        }

        //add by LuoDian @ 20210722 显示输出结果
        private void RemoveOutputEvent(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;

            if (terminal.ValueType == typeof(HyImage))
                OutputImageRemove?.Invoke(TaskName, terminal.Name);
        }

        //add by LuoDian @ 20210722 显示输出结果
        public HyTerminalCollection GetOutputsTerminal()
        {
            return toolBlock.Outputs;
        }

        #endregion

        #region 运行

        BlockingCollection<TaskRunningTimeParams> queue;

        Thread runningThread;

        public void StartSerial()
        {
            //add by LuoDian @ 20220116 添加工具的初始化动作
            foreach (IHyUserTool tool in toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
            {
                bool isInitialize = tool.Initialize();
                if (!isInitialize)
                    throw new Exception($"工具 {tool.Name} 初始化失败！");
            }

            queue = new BlockingCollection<TaskRunningTimeParams>();

            runningThread = new Thread(RunSerial)
            {
                IsBackground = true
            };
            runningThread.SetApartmentState(ApartmentState.STA);
            runningThread.Start();
        }

        public void StopSerial()
        {
            queue?.CompleteAdding();

            // Added by Louis on Aug. 4 2022 停止运行后释放工具资源
            foreach (IHyUserTool tool in toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
            {
                tool.Dispose();
            }
        }

        void RunSerial()
        {
            while (!queue.IsCompleted)
            {
                TaskRunningTimeParams @param = null;

                try
                {
                    @param = queue.Take();
                    //log?.Info($"【ToolBlock运行】任务[{TaskName}]的图像[{param.HyImage.AcqOrCalibName}]已采集完毕！");
                }
                catch (InvalidOperationException) { }

                if (@param != null)
                {
                    //add by LuoDian @ 20211112 在HyImage的构造函数里面拷贝图像，有时会导致

                    //log?.Info($"【ToolBlock运行】任务[{TaskName}]的第[{param.HyImage.AcqOrCalibName}]张图像 开始执行！");
                    //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
                    (int errCode, LinkedDictionary<string, object> outputs) = RunToolBlock(@param.HyImage, @param.Params, @param.SubName);
                    //log?.Info($"【ToolBlock运行】任务[{TaskName}]的第[{param.HyImage.AcqOrCalibName}]张图像 已执行完毕！");

                    //add by LuoDian @ 20210812  拍完所有输入图像才run，在run之前，outputs为null，不要执行call back
                    if (outputs != null)

                        @param.CallBack?.Invoke(@param.HyImage.AcqOrCalibName, @param.HyImage.AcqOrCalibIndex, errCode, outputs);
                }
            }
        }

        //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
        public void RunSerial(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params, string subName, ToolBlockCallBack callBack = null)
        {
            //add by LuoDian @ 20211020 监控相机的图像缓存，防止因图像处理速度过慢，导致图像一直增加，处理这种情况后，需要优化图像处理速度
            if (queue.Count > 8)
            {
                log.Warn($"当前任务[{hyImage.TaskName}]的图像缓存数量已经达到{8}张！");
            }

            queue.Add(new TaskRunningTimeParams
            {
                HyImage = hyImage,
                Params = @params,
                CallBack = callBack,
                SubName = subName
            });
        }

        //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
        (int errorCode, LinkedDictionary<string, object> outputs) RunToolBlock(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params, string subName)
        {
            LinkedDictionary<string, object> outputs = null;

            SetImage(hyImage, @params);

            SetInputValue(InputOutputConst.Input_AcqIndex, hyImage.AcqOrCalibIndex);

            //add by LuoDian @ 20211019 处理LCM的拍完多张图才运行ToolBlock的需求
            bool bWaitAllImage = false;

            //add by LuoDian @ 20211019 处理LCM的拍完多张图才运行ToolBlock的需求
            if (toolBlock.Inputs.Contains(GlobalParams.IsWaitAllImage) && toolBlock.Inputs[GlobalParams.IsWaitAllImage] != null && toolBlock.Inputs[GlobalParams.IsWaitAllImage].Value != null && toolBlock.Inputs[GlobalParams.IsWaitAllImage].Value.ToString().ToUpper().Equals("YES"))
                bWaitAllImage = true;

            if (@params != null)
            {
                foreach (var (Name, Value) in @params)
                {
                    SetInputValue(Name, Value);
                }
            }

            try
            {
                //add by LuoDian @ 20210803  拍完所有输入图像才run
                if (bWaitAllImage)
                {
                    if (ImageIndexes.Count < toolBlock.Inputs.Where(a => a.ValueType == typeof(HyImage)).Count())
                    {
                        ImageIndexes.Add(hyImage.AcqOrCalibIndex);
                        if (ImageIndexes.Count < toolBlock.Inputs.Where(a => a.ValueType == typeof(HyImage)).Count())
                            return (ErrorCodeConst.OK, outputs);
                    }

                    ImageIndexes.Sort();
                    for (i = 0; i < ImageIndexes.Count; i++)
                    {
                        if (ImageIndexes[i] != i + 1)
                        {
                            log.Error($"Task[{TaskName}]中的拍照索引[{ImageIndexes[i]}]不连续！");
                            ImageIndexes.Clear();
                            return (ErrorCodeConst.IndexError, outputs);
                        }
                    }
                    ImageIndexes.Clear();
                }

                TimeSpan ts = FuncWatch.ElapsedTime(() =>
                {
                    toolBlock.Run(subName);
                    //add by LuoDian @ 20211014 添加HyToolBlock执行完成的日志
                    log.Info($"Task[{TaskName}]中的拍照[{hyImage?.AcqOrCalibName}]的任务执行完成！");
                });

                outputs = GetOutputs(subName);

                //add by LuoDian @ 20210803  运行完后要把输入图像数据清除，不然下次运行的时候，会有上次运行的图像残留，影响后续运行
                foreach (HyTerminal input in toolBlock.Inputs)
                {
                    if (input != null && input.Value != null && input.ValueType == typeof(HyImage))
                    {
                        ((HyImage)input.Value).Image?.Dispose();
                        input.Value = null;
                    }
                }

                log.Debug(new TaskVisionLogMessage(TaskName, hyImage.AcqOrCalibName, A_RunToolBlock, R_Success,
                    $"\"[{hyImage.CmdID}]ToolBlock 运行：{string.Join(",", outputs.Select(kv => $"{kv.Key} = {kv.Value}"))}\"", ts.TotalMilliseconds));

                Ran?.Invoke(this, new ToolBlockRanEventArgs(TaskName, hyImage.AcqOrCalibName, hyImage.AcqOrCalibIndex, outputs));

                return (ErrorCodeConst.OK, outputs);
            }
            catch (HyVisionException e)
            {
                //delete by LuoDian @ 20210817  运行失败为什么还要再获取一次Output？
                //outputs = GetOutputs();

                log.Error(new TaskVisionLogMessage(TaskName, hyImage?.AcqOrCalibName, A_RunToolBlock, R_Fail, $"{e.Message}{(e.InnerException == null ? null : ": " + e.InnerException?.Message)}"));

                return (ErrorCodeConst.ToolBlockRunFail, outputs);
            }
        }


        #endregion

        public void ExpandAll()
        {
            //SZ toolBlockEdit => NullReferenceException 

            toolBlockEdit?.ExpandAll();
        }

        public void Save()
        {
            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
            {
                HyToolCollection toolCollection = new HyToolCollection { MaterialSubName = materialRepo.CurSubName };
                toolBlock.Tools.Add(toolCollection);
            }

            Directory.CreateDirectory(pathRepo.TaskVppDirectory);

            //add by LuoDian @ 20210919 为了解决图像数据序列化到XML文件带来的解析异常，在序列化之前把图像数据拿掉
            //这里先把HyToolBlock中的Inputs中的图像数据拿掉
            foreach (HyTerminal input in toolBlock.Inputs)
            {
                if (input.ValueType == typeof(HyImage) || input.ValueType == typeof(Bitmap) || input.ValueType == typeof(IntPtr))
                {
                    input.DisposeInternalResources();
                    input.Value = null;
                }
            }

            //add by LuoDian @ 20210919 为了解决图像数据序列化到XML文件带来的解析异常，在序列化之前把图像数据拿掉
            //这里先把HyToolBlock中每个工具的的Inputs和Outputs中的图像数据拿掉
            foreach (IHyUserTool tool in toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
            {
                foreach (HyTerminal input in tool.Inputs)
                {
                    if (input.ValueType == typeof(HyImage) || input.ValueType == typeof(Bitmap) || input.ValueType == typeof(IntPtr))
                    {
                        input.DisposeInternalResources();
                        input.Value = null;
                    }
                }
                foreach (HyTerminal output in tool.Outputs)
                {
                    if (output.ValueType == typeof(HyImage) || output.ValueType == typeof(Bitmap) || output.ValueType == typeof(IntPtr))
                    {
                        output.DisposeInternalResources();
                        output.Value = null;
                    }
                }
            }

            //add by LuoDian @ 20210919 为了解决图像数据序列化到XML文件带来的解析异常，在序列化之前把图像数据拿掉
            //这里先把HyToolBlock中的Outputs中的图像数据拿掉
            foreach (HyTerminal output in toolBlock.Outputs)
            {
                if (output.ValueType == typeof(HyImage) || output.ValueType == typeof(Bitmap) || output.ValueType == typeof(IntPtr))
                {
                    output.DisposeInternalResources();
                    output.Value = null;
                }
            }

            HySerializer.SaveToFile(toolBlock, vppPath);

            log.Info(new TaskVisionLogMessage(TaskName, null, A_Save, R_Success, $"保存至“{vppPath}”"));

            nameMappingRepo.Save();
        }

        public void Dispose()
        {

        }

        public object CreateRecord(int index)
        {
            return null;
        }

        //add by LuoDian @ 20211213 用于子料号的快速切换
        public void AddNewHyToolBlock(string curSubName)
        {
            toolBlock.AddNewHyToolBlock(curSubName);
        }

        //add by LuoDian @ 20211213 用于子料号的快速切换
        public void DeleteHyToolBlockBySubName(string subName)
        {
            toolBlock.DeleteHyToolBlockBySubName(subName);
        }
    }
}
