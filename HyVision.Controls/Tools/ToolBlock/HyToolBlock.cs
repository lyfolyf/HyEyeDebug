using Autofac;
using HyEye.API.Repository;
using HyVision.Models;
using System;
using System.Collections.Generic;

namespace HyVision.Tools.ToolBlock
{
    [Serializable]
    public class HyToolBlock : BaseHyUserTool
    {
        public override Type ToolEditType => typeof(HyToolBlockEdit);

        //add by LuoDian @ 20211213 用于子料号的快速切换时，获取当前选择的子料号
        IMaterialRepository materialRepo;

        public HyToolBlock()
        {

        }

        private void Tools_Inserted(object sender, CollectionItemEventArgs e)
        {
            BaseHyUserTool tool = (BaseHyUserTool)e.Value;
            tool.InputOutputs = InputOutputs;
            foreach (HyTerminal input in tool.Inputs)
            {
                InputOutputs.Add(input.GUID, input);
            }
            foreach (HyTerminal output in tool.Outputs)
            {
                InputOutputs.Add(output.GUID, output);
            }
        }

        private void Tools_Removed(object sender, CollectionItemEventArgs e)
        {
            BaseHyUserTool tool = (BaseHyUserTool)e.Value;
            foreach (HyTerminal input in tool.Inputs)
            {
                InputOutputs.Remove(input.GUID);
            }
            foreach (HyTerminal output in tool.Outputs)
            {
                InputOutputs.Remove(output.GUID);
            }
            tool.InputOutputs = null;
        }

        private void Tools_Exception(object sender, HyExceptionEventArgs e)
        {
            OnException(e);
        }

        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        List<HyToolCollection> tools;
        /// <summary>
        /// 这里的 Set 用于 Xml 序列化，一般不建议使用
        /// update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Tools")]
        public List<HyToolCollection> Tools
        {
            get
            {
                if (tools == null)
                {
                    tools = new List<HyToolCollection>();
                }
                return tools;
            }
            set
            {
                if (value == null)
                    throw new HyVisionException("Tools 不能设置为 null");

                if (tools != value)
                {
                    if (tools != null)
                    {
                        if(materialRepo == null)
                            materialRepo = AutoFacContainer.Resolve<MaterialRepository>();

                        HyToolCollection tempToolCollection1 = tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
                        //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                        if (tempToolCollection1 == null)
                        {
                            GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                            log.Error($"设定HyToolBlock失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                            return;
                        }

                        tempToolCollection1.Inserted -= Tools_Inserted;
                        tempToolCollection1.Removed -= Tools_Removed;
                        tempToolCollection1.Exception -= Tools_Exception;

                        //delete by LuoDian @ 20211216 为了根据子料号做快速换型，在把Tools的类型从HyToolCollection改为List<HyToolCollection>后，需要根据实际运行
                        //的子料号来删除后再添加所有工具的Inputs和Outputs
                        //foreach (BaseHyUserTool tool in tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
                        //{
                        //    foreach (HyTerminal input in tool.Inputs)
                        //    {
                        //        InputOutputs.Remove(input.GUID);
                        //    }
                        //    foreach (HyTerminal output in tool.Outputs)
                        //    {
                        //        InputOutputs.Remove(output.GUID);
                        //    }
                        //}
                    }

                    tools = value;
                    HyToolCollection tempToolCollection2 = tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
                    tempToolCollection2.Inserted += Tools_Inserted;
                    tempToolCollection2.Removed += Tools_Removed;
                    tempToolCollection2.Exception += Tools_Exception;

                    //delete by LuoDian @ 20211216 为了根据子料号做快速换型，在把Tools的类型从HyToolCollection改为List<HyToolCollection>后，需要根据实际运行
                    //的子料号来删除后再添加所有工具的Inputs和Outputs
                    //foreach (BaseHyUserTool tool in tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
                    //{
                    //    if (tool.InputOutputs == null || tool.InputOutputs != InputOutputs)
                    //    {
                    //        foreach (HyTerminal input in tool.Inputs)
                    //        {
                    //            InputOutputs.Add(input.GUID, input);
                    //        }
                    //        foreach (HyTerminal output in tool.Outputs)
                    //        {
                    //            InputOutputs.Add(output.GUID, output);
                    //        }
                    //    }

                    //    tool.InputOutputs = InputOutputs;
                    //}
                }
            }
        }

        // update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换, 同时添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        protected override void Run2(string subName)
        {
            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (tools.Find(a => a.MaterialSubName == subName) == null)
            {
                GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                log.Error($"运行HyToolBlock失败！原因：未找到当前子料号[{subName}]的信息！");
                return;
            }

            //add by LuoDian @ 20211216 为了根据子料号做快速换型，在把Tools的类型从HyToolCollection改为List<HyToolCollection>后，需要根据实际运行
            //的子料号来删除后再添加所有工具的Inputs和Outputs
            foreach (BaseHyUserTool tool in tools.Find(a => a.MaterialSubName == subName))
            {
                foreach (HyTerminal input in tool.Inputs)
                {
                    InputOutputs.Remove(input.GUID);
                    InputOutputs.Add(input.GUID, input);
                }
                foreach (HyTerminal output in tool.Outputs)
                {
                    InputOutputs.Remove(output.GUID);
                    InputOutputs.Add(output.GUID, output);
                }
                tool.InputOutputs = InputOutputs;
            }
            
            foreach (IHyUserTool tool in Tools.Find(a => a.MaterialSubName == subName))
            {
                tool.Run(subName);
            }
        }

        // update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        public override object Clone(bool containsData)
        {
            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
            {
                GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                log.Error($"拷贝HyToolBlock失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                return null;
            }

            HyToolBlock toolBlock = new HyToolBlock();
            foreach (HyTerminal input in Inputs)
            {
                toolBlock.Inputs.Add((HyTerminal)input.Clone(containsData));
            }

            foreach (HyTerminal output in Outputs)
            {
                toolBlock.Outputs.Add((HyTerminal)output.Clone(containsData));
            }

            if (materialRepo == null)
                materialRepo = AutoFacContainer.Resolve<MaterialRepository>();

            HyToolCollection hyToolCollection = Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            foreach (IHyUserTool tool in hyToolCollection)
            {
                hyToolCollection.Add((IHyUserTool)tool.Clone(containsData));
            }

            return hyToolCollection;
        }

        /// <summary>
        /// 释放 ToolBlock 使用的所有内部资源
        /// update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        /// </summary>
        public override void DisposeInternalResources()
        {
            base.DisposeInternalResources();

            if (materialRepo == null)
                materialRepo = AutoFacContainer.Resolve<MaterialRepository>();

            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
            {
                GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                log.Error($"释放HyToolBlock资源失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                return;
            }

            foreach (IHyUserTool tool in Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
            {
                tool.DisposeInternalResources();
            }
        }

        bool mDisposed;
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        protected override void Dispose(bool disposing)
        {
            if (mDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (materialRepo == null)
                    materialRepo = AutoFacContainer.Resolve<MaterialRepository>();

                //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                if (Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
                {
                    GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                    log.Error($"Dispose HyToolBlock失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                    return;
                }

                foreach (IHyUserTool mTool in Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
                {
                    mTool.Dispose();
                }
            }

            mDisposed = true;
        }

        //add by LuoDian @ 20211213 用于子料号的快速切换
        public void AddNewHyToolBlock(string curSubName)
        {
            if (tools == null)
                tools = new List<HyToolCollection>();
            HyToolCollection defaultTool;
            if (tools.Count > 0)
                defaultTool = tools[0].Clone();
            else
                defaultTool = new HyToolCollection();
            defaultTool.MaterialSubName = curSubName;
            tools.Add(defaultTool);
        }

        //add by LuoDian @ 20211213 用于子料号的快速切换
        public void DeleteHyToolBlockBySubName(string subName)
        {
            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (Tools.Find(a => a.MaterialSubName == subName) == null)
            {
                GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                log.Error($"删除HyToolBlock子料号失败！原因：未找到当前子料号[{subName}]的信息！");
                return;
            }

            if (materialRepo == null)
                materialRepo = AutoFacContainer.Resolve<MaterialRepository>();

            tools.Remove(tools.Find(a => a.MaterialSubName == subName));
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            
        }
    }
}
