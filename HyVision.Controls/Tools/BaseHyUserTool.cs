using System;
using System.Collections.Generic;

using HyVision.Models;
using HyVision.Tools.TerminalCollection;


namespace HyVision.Tools
{
    [Serializable]
    public abstract class BaseHyUserTool : IHyUserTool
    {
        public event EventHandler Ran;

        public event EventHandler<ValueChangedEventArgs<string>> NameChanged;

        internal event EventHandler<HyExceptionEventArgs> Exception;

        protected internal Dictionary<string, HyTerminal> InputOutputs;

        public abstract Type ToolEditType { get; }

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentNullException("name", "工具名称不能为空");

                if (name != value)
                {
                    string oldname = name;

                    name = value;

                    OnNameChanged(oldname, value);
                }
            }
        }

        HyTerminalCollection inputs;
        public HyTerminalCollection Inputs
        {
            get
            {
                if (inputs == null)
                {
                    inputs = new HyTerminalCollection(this);
                    //add by LuoDian @ 20220104 解决新建HyVision任务的时候，添加拍照的Inputs节点没有自动添加到InputOutputs中去的问题
                    inputs.Inserted += InputOutput_Inserted;
                    inputs.Removed += InputOutput_Removed;
                    inputs.Cleared += InputOutput_Cleared;
                }
                return inputs;
            }
            set
            {
                if (value == null)
                    throw new HyVisionException("Inputs 不能设置为 null");

                if (inputs != value)
                {
                    if (inputs != null)
                    {
                        inputs.Inserted -= InputOutput_Inserted;
                        inputs.Removed -= InputOutput_Removed;
                        inputs.Cleared -= InputOutput_Cleared;

                        if (InputOutputs != null)
                        {
                            foreach (HyTerminal input in inputs)
                            {
                                InputOutputs.Remove(input.GUID);
                            }
                        }
                    }

                    inputs = value;
                    inputs.Parent = this;
                    inputs.Inserted += InputOutput_Inserted;
                    inputs.Removed += InputOutput_Removed;
                    inputs.Cleared += InputOutput_Cleared;

                    if (InputOutputs != null)
                    {
                        foreach (HyTerminal input in inputs)
                        {
                            InputOutputs.Add(input.GUID, input);
                        }
                    }
                }
            }
        }

        HyTerminalCollection outputs;
        public HyTerminalCollection Outputs
        {
            get
            {
                if (outputs == null)
                {
                    outputs = new HyTerminalCollection(this);
                    //add by LuoDian @ 20220104 解决新建HyVision任务的时候，添加拍照的Outputs节点没有自动添加到InputOutputs中去的问题
                    outputs.Inserted += InputOutput_Inserted;
                    outputs.Removed += InputOutput_Removed;
                    outputs.Cleared += InputOutput_Cleared;
                }
                return outputs;
            }
            set
            {
                if (value == null)
                    throw new HyVisionException("Inputs 不能设置为 null");

                if (outputs != value)
                {
                    if (outputs != null)
                    {
                        outputs.Inserted -= InputOutput_Inserted;
                        outputs.Removed -= InputOutput_Removed;
                        outputs.Cleared -= InputOutput_Cleared;

                        if (InputOutputs != null)
                        {
                            foreach (HyTerminal output in outputs)
                            {
                                InputOutputs.Remove(output.GUID);
                            }
                        }
                    }

                    outputs = value;
                    outputs.Parent = this;
                    outputs.Inserted += InputOutput_Inserted;
                    outputs.Removed += InputOutput_Removed;
                    outputs.Cleared += InputOutput_Cleared;

                    if (InputOutputs != null)
                    {
                        foreach (HyTerminal output in outputs)
                        {
                            InputOutputs.Add(output.GUID, output);
                        }
                    }
                }
            }
        }

        public BaseHyUserTool()
        {
            InputOutputs = new Dictionary<string, HyTerminal>();
        }

        private void InputOutput_Inserted(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;
            InputOutputs.Add(terminal.GUID, terminal);
        }

        private void InputOutput_Removed(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;
            InputOutputs.Remove(terminal.GUID);
        }

        //add by LuoDian @ 20210810 在更新Inputs、Outputs的数据类型的时候，需要一起更新InputsOutputs里面节点的数据类型
        public void UpdateInputOutputItem(string key, HyTerminal item)
        {
            InputOutputs[key] = item;
        }

        private void InputOutput_Cleared(object sender, EventArgs e)
        {
            if (InputOutputs != null)
            {
                HyTerminalCollection terminals = (HyTerminalCollection)sender;
                foreach (HyTerminal terminal in terminals)
                {
                    InputOutputs.Remove(terminal.GUID);
                }
            }
        }

        protected virtual void OnRan()
        {
            Ran?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnNameChanged(string oldName, string newName)
        {
            NameChanged?.Invoke(this, new ValueChangedEventArgs<string>(oldName, newName));
        }

        // 用于引发异常
        protected virtual void OnException(string message, Exception e)
        {
            Exception?.Invoke(this, new HyExceptionEventArgs(this, message, e));
            throw new HyVisionException(message, e);
        }

        // 用于传递子工具异常
        protected virtual void OnException(HyExceptionEventArgs e)
        {
            Exception?.Invoke(this, e);
        }

        protected virtual void UpdateDataToObject()
        {

        }

        // update by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        public void Run(string subName)
        {
            foreach (HyTerminal input in Inputs)
            {
                //add by LuoDian @ 20210819 有时会出现input.From有值，但是InputOutputs却没有这个节点信息，所以需要加异常处理
                if (input.From != null && !InputOutputs.ContainsKey(input.From))
                    OnException($"运行[{Name}]时发生异常", new HyVisionException($"InputOutputs中找不到名称为 {input.Name} 的节点信息！"));

                //update by LuoDian @ 20220104 为了兼容某些工具运行时，不需要所有输入图像均有数据的情况，修改这个逻辑，允许某些输入为null
                //if (input.From != null && InputOutputs[input.From].Value == null)
                //    OnException($"运行[{Name}]时发生异常", new HyVisionException($"名称为 {input.Name} 的输入数据为空！"));
                //if (input.From != null)
                //{
                //    input.Value = InputOutputs[input.From].Value;
                //}
                if (input.From != null && InputOutputs.ContainsKey(input.From))
                {
                    input.Value = InputOutputs[input.From].Value;
                    // Added by louis on Mar. 19 2022 解决收集多张图像再运行时其它参数信息不对应的问题
                    input.AttachedParams = InputOutputs[input.From].AttachedParams;
                }
            }

            Run2(subName);

            foreach (HyTerminal output in Outputs)
            {
                 if (output.From != null && InputOutputs.ContainsKey(output.From))
                {
                    output.Value = InputOutputs[output.From].Value;
                    // Added by louis on Mar. 19 2022 解决收集多张图像再运行时其它参数信息不对应的问题
                    output.AttachedParams = InputOutputs[output.From].AttachedParams;
                }

                /*add by LuoDian @ 20210810 在Halcon引擎中对InputOutputs的元素进行了更新，导致执行完后结果不能刷新到InputOutputs中
                * 或是本身就没有就没有把结果刷新到InputOutputs中，故在这里添加刷新结果到InputOutputs的逻辑
                */
                else if (InputOutputs.ContainsKey(output.GUID))
                {
                    InputOutputs[output.GUID] = output;
                }
            }

            /*add by LuoDian @ 20210817 运行完之后，要把前面映射过来数据的那部分Inputs的数据清空，不然在UI界面中打开工具的时候，会有之前运行的
              数据残留，影响数据保存
             */
            foreach (HyTerminal input in Inputs)
            {
                if (input.From != null)
                {
                    input.Value = null;
                }
            }

            OnRan();
        }

        /// <summary>
        /// 实际的运行方法，此时 Inputs 中已经赋值
        /// update by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        /// </summary>
        protected abstract void Run2(string subName);

        public abstract object Clone(bool containsData);

        /// <summary>
        /// 释放 Inputs 和 Outputs 中的资源
        /// </summary>
        public virtual void DisposeInternalResources()
        {
            foreach (HyTerminal input in Inputs)
            {
                input.DisposeInternalResources();
            }

            foreach (HyTerminal output in Outputs)
            {
                output.DisposeInternalResources();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);


        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public abstract bool Initialize();

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public abstract void Save();
    }
}
