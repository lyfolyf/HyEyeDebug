using Autofac;
using HyEye.API.Repository;
using HyVision.Controls;
using HyVision.Models;
using HyVision.Tools.TerminalCollection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HyVision.Tools.ToolBlock
{
    public partial class HyToolBlockEdit : BaseHyUserToolEdit<HyToolBlock>
    {
        static readonly Type IntType = typeof(int);
        static readonly Type DoubleType = typeof(double);
        static readonly Type StringType = typeof(string);
        static readonly Type DatetimeType = typeof(DateTime);
        static readonly Type ImageType = typeof(HyImage);

        readonly HyTreeNode inputsNode;
        readonly HyTreeNode outputsNode;

        HyToolBlock toolBlock;
        HyTerminalCollection subOutputs;
        readonly ImageList imageList;

        //add by LuoDian @ 20211213 用于子料号的快速切换时，获取当前选择的子料号
        IMaterialRepository materialRepo = AutoFacContainer.Resolve<MaterialRepository>();

        static readonly Color InputColor = Color.FromArgb(0x00, 0x80, 0x80);
        static readonly Color OutputColor = Color.FromArgb(0x80, 0x00, 0x80);
        static readonly Color DefaultColor = Color.Black;

        static readonly string Inputs = "[Inputs]";
        static readonly string Outputs = "[Outputs]";

        public HyToolBlockEdit()
        {
            InitializeComponent();

            imageList = new ImageList();
            foreach (KeyValuePair<string, Image> kv in ImageCache.Imgs)
            {
                imageList.Images.Add(kv.Value);
                imageList.Images.SetKeyName(imageList.Images.Count - 1, kv.Key);
            }

            treeView1.ImageList = imageList;

            treeView1.NodeLinked += TreeView1_NodeLinked;

            inputsNode = CreateNode(Inputs, HyNodeType.None, null, DefaultColor, "Input");
            outputsNode = CreateNode(Outputs, HyNodeType.None, null, DefaultColor, "Output");

            treeView1.Nodes.Add(inputsNode);
            treeView1.Nodes.Add(outputsNode);
        }

        private void TreeView1_NodeLinked(object sender, NodeLinkedEventArgs e)
        {
            e.EndNode.Terminal.From = e.StartNode.Terminal.GUID;
        }

        private void HyToolBlockEdit_Load(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Height / 2;

            //add by LuoDian @ 20211215 不同子料号切换之后，需要刷新TreeView
            if (toolBlock != null)
                LoadTreeView();
        }

        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HyToolBlock Subject
        {
            get { return toolBlock; }
            set
            {
                if (!object.Equals(toolBlock, value))
                {
                    if (toolBlock != null)
                    {
                        //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                        if (toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
                        {
                            HyToolCollection toolCollection = new HyToolCollection { MaterialSubName = materialRepo.CurSubName };
                            toolBlock.Tools.Add(toolCollection);
                        }

                        ToolBlockRemoveEvent();

                        foreach (IHyUserTool tool in toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
                        {
                            ToolRemoveEvent(tool);
                        }

                        toolBlock.Dispose();
                    }

                    try
                    {
                        toolBlock = value;

                        LoadTreeView();
                    }
                    catch (Exception e)
                    {
                        tslblErrMsg.Text = e.Message;
                    }
                }
            }
        }

        //add by LuoDian @ 20211215 为了区分不同子料号的ToolBlock里面添加的工具，把创建树型节点的逻辑放到这里，供Subject和Form_Load调用
        private void LoadTreeView()
        {
            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
            {
                HyToolCollection toolCollection = new HyToolCollection { MaterialSubName = materialRepo.CurSubName };
                toolBlock.Tools.Add(toolCollection);
            }

            treeView1.BeginUpdate();

            treeView1.Nodes.Clear();
            inputsNode.Nodes.Clear();
            outputsNode.Nodes.Clear();

            inputEdit.Subject = toolBlock.Inputs;

            subOutputs = new HyTerminalCollection();
            var outputs = toolBlock.Outputs.Where(r => r.MaterialSubName == null || r.MaterialSubName == materialRepo.CurSubName);
            foreach (HyTerminal terminal in outputs)
            {
                subOutputs.Add(terminal);
            }

            outputEdit.Subject = subOutputs;
            outputEdit.MaterialSubName = materialRepo.CurSubName;

            outputEdit.TerminalAdded -= Outputs_Added;
            outputEdit.TerminalAdded += Outputs_Added;
            outputEdit.TerminalDeleted -= Outputs_Deleted;
            outputEdit.TerminalDeleted += Outputs_Deleted;
            outputEdit.Subject.ItemValueChanged -= Outputs_ItemValueChanged;
            outputEdit.Subject.ItemValueChanged += Outputs_ItemValueChanged;

            ToolBlockAddEvent();

            treeView1.Nodes.Add(inputsNode);
            treeView1.Nodes.Add(outputsNode);

            Dictionary<string, HyTreeNode> dict = new Dictionary<string, HyTreeNode>();

            foreach (HyTerminal terminal in toolBlock.Inputs)
            {
                HyTreeNode node = AddInput(terminal);

                dict.Add(terminal.GUID, node);
            }

            foreach (HyTerminal terminal in subOutputs)
            {
                HyTreeNode node = AddOutput(terminal);

                dict.Add(terminal.GUID, node);
            }

            if (toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) != null)
            {
                foreach (IHyUserTool tool in toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName))
                {
                    HyTreeNode toolNode = CreateNode(tool.Name, HyNodeType.None, null, DefaultColor, tool.GetType().Name);
                    treeView1.Nodes.Insert(treeView1.Nodes.Count - 1, toolNode);

                    foreach (HyTerminal input in tool.Inputs)
                    {
                        HyTreeNode inputNode = CreateNode(input.Name, HyNodeType.End, input, InputColor, "Output");
                        toolNode.Nodes.Add(inputNode);

                        dict.Add(input.GUID, inputNode);
                    }
                    foreach (HyTerminal output in tool.Outputs)
                    {
                        HyTreeNode outputNode = CreateNode(output.Name, HyNodeType.Start, output, OutputColor, "Input");
                        toolNode.Nodes.Add(outputNode);

                        dict.Add(output.GUID, outputNode);
                    }

                    ToolAddEvent(tool);
                }
            }

            InitLinkNode(dict);

            treeView1.ExpandAll();

            treeView1.EndUpdate();
        }

        void InitLinkNode(Dictionary<string, HyTreeNode> dict)
        {
            foreach (HyTreeNode node in dict.Values)
            {
                if (node.Terminal.From != null)
                {
                    if (dict.ContainsKey(node.Terminal.From))
                    {
                        HyTreeNode linkNode = dict[node.Terminal.From];

                        node.LinkStartNode = linkNode;
                        if (linkNode.LinkEndNodes == null)
                            linkNode.LinkEndNodes = new List<HyTreeNode>();
                        linkNode.LinkEndNodes.Add(node);
                    }
                }
            }
        }

        //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
        void ToolBlockAddEvent()
        {
            HyToolCollection tempToolCollection = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (tempToolCollection == null)
            {
                tempToolCollection = new HyToolCollection { MaterialSubName = materialRepo.CurSubName };
                toolBlock.Tools.Add(tempToolCollection);
            }

            //add by LuoDian @ 20211216 添加了子料号之后，会重复触发事件，因此在添加之前，得先移除之前的事件
            toolBlock.Inputs.Inserted -= Inputs_Inserted;
            toolBlock.Inputs.Removed -= Inputs_Removed;
            toolBlock.Inputs.ItemValueChanged -= Inputs_ItemValueChanged;
            toolBlock.Inputs.MovedItem -= Inputs_Moved;
            toolBlock.Inputs.Cleared -= Inputs_Cleared;
            subOutputs.Inserted -= Outputs_Inserted;
            subOutputs.Removed -= Outputs_Removed;
            subOutputs.ItemValueChanged -= Outputs_ItemValueChanged;
            subOutputs.MovedItem -= Outputs_Moved;
            subOutputs.Cleared -= Outputs_Cleared;
            tempToolCollection.Inserted -= Tools_Inserted;
            tempToolCollection.Removed -= Tools_Removed;
            tempToolCollection.ItemValueChanged -= Tools_ItemValueChanged;
            tempToolCollection.MovedItem -= Tools_MovedItem;
            toolBlock.Exception -= ToolBlock_Exception;


            toolBlock.Inputs.Inserted += Inputs_Inserted;
            toolBlock.Inputs.Removed += Inputs_Removed;
            toolBlock.Inputs.ItemValueChanged += Inputs_ItemValueChanged;
            toolBlock.Inputs.MovedItem += Inputs_Moved;
            toolBlock.Inputs.Cleared += Inputs_Cleared;

            subOutputs.Inserted += Outputs_Inserted;
            subOutputs.Removed += Outputs_Removed;
            subOutputs.ItemValueChanged += Outputs_ItemValueChanged;
            subOutputs.MovedItem += Outputs_Moved;
            subOutputs.Cleared += Outputs_Cleared;

            tempToolCollection.Inserted += Tools_Inserted;
            tempToolCollection.Removed += Tools_Removed;
            tempToolCollection.ItemValueChanged += Tools_ItemValueChanged;
            tempToolCollection.MovedItem += Tools_MovedItem;

            toolBlock.Exception += ToolBlock_Exception;
        }

        //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
        void ToolBlockRemoveEvent()
        {
            HyToolCollection tempToolCollection = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (tempToolCollection == null)
            {
                tempToolCollection = new HyToolCollection { MaterialSubName = materialRepo.CurSubName };
                toolBlock.Tools.Add(tempToolCollection);
            }

            toolBlock.Inputs.Inserted -= Inputs_Inserted;
            toolBlock.Inputs.Removed -= Inputs_Removed;
            toolBlock.Inputs.ItemValueChanged -= Inputs_ItemValueChanged;
            toolBlock.Inputs.MovedItem -= Inputs_Moved;
            toolBlock.Inputs.Cleared -= Inputs_Cleared;

            subOutputs.Inserted -= Outputs_Inserted;
            subOutputs.Removed -= Outputs_Removed;
            subOutputs.ItemValueChanged -= Outputs_ItemValueChanged;
            subOutputs.MovedItem -= Outputs_Moved;
            subOutputs.Cleared -= Outputs_Cleared;

            tempToolCollection.Inserted -= Tools_Inserted;
            tempToolCollection.Removed -= Tools_Removed;
            tempToolCollection.ItemValueChanged -= Tools_ItemValueChanged;
            tempToolCollection.MovedItem -= Tools_MovedItem;

            toolBlock.Exception -= ToolBlock_Exception;
        }

        void ToolAddEvent(IHyUserTool tool)
        {
            //add by LuoDian @ 20211216 添加了子料号之后，会重复触发事件，因此在添加之前，得先移除之前的事件
            ToolRemoveEvent(tool);

            tool.Inputs.Inserted += ToolInputs_Inserted;
            tool.Inputs.Removed += ToolInputs_Removed;
            tool.Inputs.ItemValueChanged += ToolInputs_ItemValueChanged;
            tool.Inputs.MovedItem += ToolInputs_Moved;
            tool.Inputs.Cleared += ToolInputs_Cleared;

            tool.Outputs.Inserted += ToolOutputs_Inserted;
            tool.Outputs.Removed += ToolOutputs_Removed;
            tool.Outputs.ItemValueChanged += ToolOutputs_ItemValueChanged;
            tool.Outputs.MovedItem += ToolOutputs_Moved;
            tool.Outputs.Cleared += ToolOutputs_Cleared;
        }

        void ToolRemoveEvent(IHyUserTool tool)
        {
            tool.Inputs.Inserted -= ToolInputs_Inserted;
            tool.Inputs.Removed -= ToolInputs_Removed;
            tool.Inputs.ItemValueChanged -= ToolInputs_ItemValueChanged;
            tool.Inputs.MovedItem -= ToolInputs_Moved;
            tool.Inputs.Cleared -= ToolInputs_Cleared;

            tool.Outputs.Inserted -= ToolOutputs_Inserted;
            tool.Outputs.Removed -= ToolOutputs_Removed;
            tool.Outputs.ItemValueChanged -= ToolOutputs_ItemValueChanged;
            tool.Outputs.MovedItem -= ToolOutputs_Moved;
            tool.Outputs.Cleared -= ToolOutputs_Cleared;
        }

        HyTreeNode CreateNode(string text, HyNodeType nodeType, HyTerminal terminal, Color foreColor, string imageKey)
        {
            HyTreeNode node = new HyTreeNode(text);
            node.ForeColor = foreColor;
            node.NodeType = nodeType;
            node.Terminal = terminal;

            if (imageList.Images.ContainsKey(imageKey))
            {
                node.ImageKey = imageKey;
                node.SelectedImageKey = imageKey;
            }
            else
            {
                node.ImageKey = "";
                node.SelectedImageKey = "";
            }

            return node;
        }

        #region 事件

        HyTreeNode AddInput(HyTerminal terminal)
        {
            HyTreeNode input = CreateNode(terminal.Name, HyNodeType.Start, terminal, InputColor, "Input");
            inputsNode.Nodes.Add(input);
            inputsNode.Expand();

            if (terminal.ValueType == ImageType)
            {
                cmbImages.Items.Add(terminal.Name);
            }

            return input;
        }

        HyTreeNode AddOutput(HyTerminal terminal)
        {
            if (string.IsNullOrEmpty(terminal.MaterialSubName))
                terminal.MaterialSubName = materialRepo.CurSubName;

            HyTreeNode output = CreateNode(terminal.Name, HyNodeType.End, terminal, OutputColor, "Output");
            outputsNode.Nodes.Add(output);
            outputsNode.Expand();

            return output;
        }

        #region Input/Output

        private void Inputs_Inserted(object sender, CollectionItemEventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                AddInput((HyTerminal)e.Value);
            });
        }

        private void Inputs_Removed(object sender, CollectionItemEventArgs e)
        {
            HyTerminal input = (HyTerminal)e.Value;

            treeView1.AsyncAction(_ =>
            {
                inputsNode.Nodes.RemoveAt(e.Index);
            });

            if (input.ValueType == ImageType)
            {
                cmbImages.AsyncAction(c =>
                {
                    cmbImages.Items.Remove(input.Name);
                });
            }
        }

        private void Inputs_ItemValueChanged(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;

            treeView1.AsyncAction(_ =>
            {
                if (inputsNode.Nodes[e.Index].Text != terminal.Name)
                {
                    inputsNode.Nodes[e.Index].Text = terminal.Name;
                }
            });
        }

        private void Inputs_Moved(object sender, CollectionItemMoveEventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                inputsNode.Nodes.MoveNode(e.FromIndex, e.ToIndex);
            });
        }

        private void Inputs_Cleared(object sender, EventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                inputsNode.Nodes.Clear();
            });
        }

        private void Outputs_Inserted(object sender, CollectionItemEventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                AddOutput((HyTerminal)e.Value);
            });
        }

        private void Outputs_Removed(object sender, CollectionItemEventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                outputsNode.Nodes.RemoveAt(e.Index);
            });
        }

        private void Outputs_Added(HyTerminal terminal)
        {
            toolBlock.Outputs.Add(terminal);
        }

        private void Outputs_Deleted(string key)
        {
            if (toolBlock.Outputs.Contains(key))
                toolBlock.Outputs.Remove(key);
        }

        private void Outputs_ItemValueChanged(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;

            if (outputsNode.Nodes[e.Index].Text != terminal.Name)
            {
                treeView1.AsyncAction(_ =>
                {
                    outputsNode.Nodes[e.Index].Text = terminal.Name;
                });
            }
        }

        private void Outputs_Moved(object sender, CollectionItemMoveEventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                outputsNode.Nodes.MoveNode(e.FromIndex, e.ToIndex);
            });

            var terminal = subOutputs[e.ToIndex];
            int moveIndex = toolBlock.Outputs.IndexOf(terminal.Name);
            if (e.FromIndex > e.ToIndex)
            {
                toolBlock.Outputs.MoveUp(moveIndex);
            }
            else
            {
                toolBlock.Outputs.MoveDown(moveIndex);
            }
        }

        private void Outputs_Cleared(object sender, EventArgs e)
        {
            treeView1.AsyncAction(_ =>
            {
                outputsNode.Nodes.Clear();
            });
        }

        #endregion

        #region Tools

        private void Tools_Inserted(object sender, CollectionItemEventArgs e)
        {
            //add by LuoDian @ 20211216 添加了子料号之后，需要根据子料号判断现在修改的是否是当前的料号，不是的话就不修改
            if (((HyToolCollection)sender).MaterialSubName != materialRepo.CurSubName)
                return;

            IHyUserTool tool = (IHyUserTool)e.Value;
            ToolAddEvent(tool);

            HyTreeNode toolNode = new HyTreeNode(tool.Name);
            treeView1.Nodes.Insert(e.Index + 1, toolNode);
            treeView1.ExpandAll();
        }

        private void Tools_Removed(object sender, CollectionItemEventArgs e)
        {
            //add by LuoDian @ 20211216 添加了子料号之后，需要根据子料号判断现在修改的是否是当前的料号，不是的话就不修改
            if (((HyToolCollection)sender).MaterialSubName != materialRepo.CurSubName)
                return;

            IHyUserTool tool = (IHyUserTool)e.Value;
            ToolRemoveEvent(tool);

            foreach (HyTreeNode node in treeView1.Nodes[e.Index + 1].Nodes)
            {
                DelLink(node);
            }

            treeView1.Nodes.RemoveAt(e.Index + 1);
        }

        private void Tools_ItemValueChanged(object sender, CollectionItemEventArgs e)
        {
            //add by LuoDian @ 20211216 添加了子料号之后，需要根据子料号判断现在修改的是否是当前的料号，不是的话就不修改
            if (((HyToolCollection)sender).MaterialSubName != materialRepo.CurSubName)
                return;

            HyTreeNode node = treeView1.Nodes[e.Index + 1];

            if (node.Text != ((IHyUserTool)e.Value).Name)
            {
                node.Text = ((IHyUserTool)e.Value).Name;
            }
        }

        private void Tools_MovedItem(object sender, CollectionItemMoveEventArgs e)
        {
            //add by LuoDian @ 20211216 添加了子料号之后，需要根据子料号判断现在修改的是否是当前的料号，不是的话就不修改
            if (((HyToolCollection)sender).MaterialSubName != materialRepo.CurSubName)
                return;

            treeView1.Nodes.MoveNode(e.FromIndex + 1, e.ToIndex + 1);
        }

        #endregion

        #region Tool Input/Output
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        HyTreeNode GetToolNode(IHyUserTool tool)
        {
            //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
            if (toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
            {
                GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                log.Error($"获取HyToolBlock节点失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                return null;
            }

            int toolIndex = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName).IndexOf(a => a == tool);

            return treeView1.Nodes[toolIndex + 1];
        }

        private void ToolInputs_Inserted(object sender, CollectionItemEventArgs e)
        {
            HyTreeNode toolNode = GetToolNode(((HyTerminalCollection)sender).Parent);

            HyTerminal terminal = (HyTerminal)e.Value;

            treeView1.AsyncAction(_ =>
            {
                HyTreeNode inputNode = CreateNode(terminal.Name, HyNodeType.End, terminal, InputColor, "Output");

                toolNode.Nodes.Insert(e.Index, inputNode);

                toolNode.ExpandAll();
            });
        }

        private void ToolInputs_Removed(object sender, CollectionItemEventArgs e)
        {
            HyTreeNode toolNode = GetToolNode(((HyTerminalCollection)sender).Parent);

            treeView1.AsyncAction(_ =>
            {
                DelLink(toolNode.Nodes[e.Index]);

                toolNode.Nodes.RemoveAt(e.Index);
            });
        }

        private void ToolInputs_ItemValueChanged(object sender, CollectionItemEventArgs e)
        {
            HyTreeNode toolNode = GetToolNode(((HyTerminalCollection)sender).Parent);

            string nodeName = ((HyTerminal)e.Value).Name;
            if (toolNode.Nodes[e.Index].Text != nodeName)
            {
                treeView1.AsyncAction(_ =>
                {
                    toolNode.Nodes[e.Index].Text = nodeName;
                });
            }
        }

        private void ToolInputs_Moved(object sender, CollectionItemMoveEventArgs e)
        {
            HyTreeNode toolNode = GetToolNode(((HyTerminalCollection)sender).Parent);

            treeView1.AsyncAction(_ =>
            {
                toolNode.Nodes.MoveNode(e.FromIndex, e.ToIndex);
            });
        }

        private void ToolInputs_Cleared(object sender, EventArgs e)
        {
            HyTerminalCollection toolInputs = (HyTerminalCollection)sender;
            HyTreeNode toolNode = GetToolNode(toolInputs.Parent);

            treeView1.AsyncAction(_ =>
            {
                List<HyTreeNode> inputNodes = new List<HyTreeNode>();
                foreach (HyTreeNode node in toolNode.Nodes)
                {
                    if (node.NodeType == HyNodeType.End)
                    {
                        inputNodes.Add(node);
                    }
                }
                foreach (HyTreeNode node in inputNodes)
                {
                    DelLink(node);

                    toolNode.Nodes.Remove(node);
                }
            });
        }

        private void ToolOutputs_Inserted(object sender, CollectionItemEventArgs e)
        {
            IHyUserTool tool = ((HyTerminalCollection)sender).Parent;
            HyTreeNode toolNode = GetToolNode(tool);

            HyTerminal terminal = (HyTerminal)e.Value;

            treeView1.AsyncAction(_ =>
            {
                HyTreeNode inputNode = CreateNode(terminal.Name, HyNodeType.Start, terminal, OutputColor, "Input");

                toolNode.Nodes.Insert(tool.Inputs.Count + e.Index, inputNode);

                toolNode.Expand();
            });
        }

        private void ToolOutputs_Removed(object sender, CollectionItemEventArgs e)
        {
            IHyUserTool tool = ((HyTerminalCollection)sender).Parent;
            HyTreeNode toolNode = GetToolNode(tool);

            treeView1.AsyncAction(_ =>
            {
                int nodeIndex = tool.Inputs.Count + e.Index;

                DelLink(toolNode.Nodes[nodeIndex]);

                toolNode.Nodes.RemoveAt(nodeIndex);
            });
        }

        private void ToolOutputs_ItemValueChanged(object sender, CollectionItemEventArgs e)
        {
            IHyUserTool tool = ((HyTerminalCollection)sender).Parent;
            HyTreeNode toolNode = GetToolNode(tool);

            string name = ((HyTerminal)e.Value).Name;

            if (toolNode.Nodes[tool.Inputs.Count + e.Index].Text != name)
            {
                treeView1.AsyncAction(_ =>
                {
                    toolNode.Nodes[tool.Inputs.Count + e.Index].Text = name;
                });
            }
        }

        private void ToolOutputs_Moved(object sender, CollectionItemMoveEventArgs e)
        {
            IHyUserTool tool = ((HyTerminalCollection)sender).Parent;
            HyTreeNode toolNode = GetToolNode(tool);

            treeView1.AsyncAction(_ =>
            {
                toolNode.Nodes.MoveNode(tool.Inputs.Count + e.FromIndex, tool.Inputs.Count + e.ToIndex);
            });
        }

        private void ToolOutputs_Cleared(object sender, EventArgs e)
        {
            HyTerminalCollection toolOutputs = (HyTerminalCollection)sender;
            HyTreeNode toolNode = GetToolNode(toolOutputs.Parent);

            treeView1.AsyncAction(_ =>
            {
                List<HyTreeNode> outputNodes = new List<HyTreeNode>();
                foreach (HyTreeNode node in toolNode.Nodes)
                {
                    if (node.NodeType == HyNodeType.Start)
                    {
                        outputNodes.Add(node);
                    }
                }
                foreach (HyTreeNode node in outputNodes)
                {
                    DelLink(node);

                    toolNode.Nodes.Remove(node);
                }
            });
        }

        #endregion

        private void ToolBlock_Exception(object sender, HyExceptionEventArgs e)
        {
            tslblErrMsg.Text = e.Exception.Message;
        }

        #endregion

        #region 打开子工具
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0 && e.Node != inputsNode && e.Node != outputsNode)
            {
                //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                if (toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName) == null)
                {
                    GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                    log.Error($"从HyToolBlock中打开工具失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                    return;
                }

                IHyUserTool hyUserTool = Subject.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName)[e.Node.Text];

                FrmShowTool form = new FrmShowTool(hyUserTool);
                form.ShowDialog();
            }
        }

        #endregion

        #region 阻止 TreeView 双击展开/折叠节点

        bool doubleClick = false;

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            doubleClick = e.Clicks > 1;
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (doubleClick)
            {
                doubleClick = false;
                e.Cancel = true;
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (doubleClick)
            {
                doubleClick = false;
                e.Cancel = true;
            }
        }

        #endregion

        #region 新增 输入/输出

        private void tsmiAddInt_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == inputsNode)
                ToolBlockAddInput(IntType);
            else
                ToolBlockAddOutput(IntType);
        }

        private void tsmiAddDouble_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == inputsNode)
                ToolBlockAddInput(DoubleType);
            else
                ToolBlockAddOutput(DoubleType);
        }

        private void tsmiAddString_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == inputsNode)
                ToolBlockAddInput(StringType);
            else
                ToolBlockAddOutput(StringType);
        }

        private void tsmiAddDatetime_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == inputsNode)
                ToolBlockAddInput(DatetimeType);
            else
                ToolBlockAddOutput(DatetimeType);
        }

        private void tsmiAddHyImage_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == inputsNode)
                ToolBlockAddInput(ImageType);
            else
                ToolBlockAddOutput(ImageType);
        }

        void ToolBlockAddInput(Type type)
        {
            string inputName = inputEdit.GetDefaltName();
            toolBlock.Inputs.Add(new HyTerminal(inputName, type));
        }

        void ToolBlockAddOutput(Type type)
        {
            string outputName = outputEdit.GetDefaltName();
            HyTerminal terminal = new HyTerminal(outputName, type);
            subOutputs.Add(terminal);
            toolBlock.Outputs.Add(terminal);
        }

        #endregion

        #region 取消链接

        private void tsmiDelLink_Click(object sender, EventArgs e)
        {
            HyTreeNode selNode = treeView1.SelectedNode;

            DelLink(selNode);

            treeView1.Invalidate();
        }

        private void DelLink(HyTreeNode node)
        {
            if (node.LinkStartNode != null)
            {
                node.LinkStartNode.LinkEndNodes.Remove(node);
                node.LinkStartNode = null;
                node.Terminal.From = null;
            }
            else if (node.LinkEndNodes != null)
            {
                foreach (HyTreeNode endNode in node.LinkEndNodes)
                {
                    endNode.LinkStartNode = null;
                    endNode.Terminal.From = null;
                }
                node.LinkEndNodes.Clear();
            }
        }

        #endregion

        #region 删除
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;

            if (MessageBoxUtils.ShowQuestion("确定要删除所选项目吗？") == DialogResult.No) return;

            HyTreeNode selNode = treeView1.SelectedNode;

            if (selNode.Parent == inputsNode)
            {
                toolBlock.Inputs.RemoveAt(selNode.Index);
            }
            else if (selNode.Parent == outputsNode)
            {
                toolBlock.Outputs.Remove(subOutputs[selNode.Index].Name);
                subOutputs.RemoveAt(selNode.Index);
            }
            else
            {
                //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                HyToolCollection tempToolCollection = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
                if (tempToolCollection == null)
                {
                    GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                    log.Error($"从HyToolBlock中删除节点失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                    return;
                }

                if (selNode.Level == 0)
                {
                    tempToolCollection.RemoveAt(selNode.Index - 1);
                }
                else
                {
                    IHyUserTool tool = tempToolCollection[selNode.Parent.Index - 1];
                    if (selNode.Index < tool.Inputs.Count)
                        tool.Inputs.Remove(selNode.Text);
                    else
                        tool.Outputs.Remove(selNode.Text);
                }
            }
        }

        #endregion

        #region 移动
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        private void tsmiMoveUp_Click(object sender, EventArgs e)
        {
            HyTreeNode selNode = treeView1.SelectedNode;

            if (selNode.Parent == inputsNode)
            {
                toolBlock.Inputs.MoveUp(selNode.Index);
            }
            else if (selNode.Parent == outputsNode)
            {
                var terminal = subOutputs[selNode.Index];
                int moveIndex = toolBlock.Outputs.IndexOf(terminal.Name);
                toolBlock.Outputs.MoveUp(moveIndex);
                subOutputs.MoveUp(selNode.Index);
            }
            else
            {
                //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                HyToolCollection tempToolCollection = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
                if (tempToolCollection == null)
                {
                    GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                    log.Error($"从HyToolBlock中移动节点失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                    return;
                }

                if (selNode.Level == 0)
                {
                    tempToolCollection.MoveUp(selNode.Index - 1);
                }
                else
                {
                    IHyUserTool tool = tempToolCollection[selNode.Parent.Index - 1];
                    if (selNode.Index < tool.Inputs.Count)
                        tool.Inputs.MoveUp(selNode.Index);
                    else
                        tool.Outputs.MoveUp(selNode.Index - tool.Inputs.Count);
                }
            }
        }
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        private void tsmiMoveDown_Click(object sender, EventArgs e)
        {
            HyTreeNode selNode = treeView1.SelectedNode;

            if (selNode.Parent == inputsNode)
            {
                toolBlock.Inputs.MoveDown(selNode.Index);
            }
            else if (selNode.Parent == outputsNode)
            {
                var terminal = subOutputs[selNode.Index];
                int moveIndex = toolBlock.Outputs.IndexOf(terminal.Name);
                toolBlock.Outputs.MoveDown(moveIndex);
                subOutputs.MoveDown(selNode.Index);
            }
            else
            {
                //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                HyToolCollection tempToolCollection = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
                if (tempToolCollection == null)
                {
                    GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                    log.Error($"从HyToolBlock中移动节点失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                    return;
                }

                if (selNode.Level == 0)
                {
                    tempToolCollection.MoveDown(selNode.Index - 1);
                }
                else
                {
                    IHyUserTool tool = tempToolCollection[selNode.Parent.Index - 1];
                    if (selNode.Index < tool.Inputs.Count)
                        tool.Inputs.MoveDown(selNode.Index);
                    else
                        tool.Outputs.MoveDown(selNode.Index - tool.Inputs.Count);
                }
            }
        }

        #endregion

        #region 重命名

        private void tsmiRename_Click(object sender, EventArgs e)
        {
            treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
        }
        //update by LuoDian @ 20211213 把HyToolCollection放到List中，用于子料号的快速切换
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || e.Label == e.Node.Text) return;

            if (e.Label.Length == 0)
            {
                e.CancelEdit = true;

                MessageBoxUtils.ShowWarn("名称不能为空");
                TopLevelControl.BringToFront();

                e.Node.BeginEdit();

                return;
            }

            if (e.Node.Parent == inputsNode)
            {
                if (toolBlock.Inputs.Contains(e.Label))
                {
                    e.CancelEdit = true;

                    MessageBoxUtils.ShowWarn("名称已存在");
                    TopLevelControl.BringToFront();

                    e.Node.BeginEdit();

                    return;
                }

                toolBlock.Inputs[e.Node.Index].Name = e.Label;
            }
            else if (e.Node.Parent == outputsNode)
            {
                if (subOutputs.Contains(e.Label))
                {
                    e.CancelEdit = true;

                    MessageBoxUtils.ShowWarn("名称已存在");
                    TopLevelControl.BringToFront();

                    e.Node.BeginEdit();

                    return;
                }

                subOutputs[e.Node.Index].Name = e.Label;
            }
            else
            {
                //add by LuoDian @ 20211215 因添加子料号之后，需要找到当前子料号的ToolBlock，再保存，所以需要加这个判断
                HyToolCollection tempToolCollection = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName);
                if (tempToolCollection == null)
                {
                    GL.Kit.Log.IGLog log = Autofac.AutoFacContainer.Resolve<GL.Kit.Log.LogPublisher>();
                    log.Error($"从HyToolBlock中重命名节点失败！原因：未找到当前子料号[{materialRepo.CurSubName}]的信息！");
                    return;
                }

                if (e.Node.Level == 0)
                {
                    if (tempToolCollection.Contains(e.Label))
                    {
                        e.CancelEdit = true;

                        MessageBoxUtils.ShowWarn("名称已存在");
                        TopLevelControl.BringToFront();

                        e.Node.BeginEdit();

                        return;
                    }

                    tempToolCollection[e.Node.Index - 1].Name = e.Label;
                }
                else
                {
                    HyTreeNode hyNode = (HyTreeNode)e.Node;
                    IHyUserTool tool = toolBlock.Tools.Find(a => a.MaterialSubName == materialRepo.CurSubName)[hyNode.Parent.Index - 1];
                    if (hyNode.Index < tool.Inputs.Count)
                    {
                        if (tool.Inputs.Contains(e.Label))
                        {
                            e.CancelEdit = true;

                            MessageBoxUtils.ShowWarn("名称已存在");
                            TopLevelControl.BringToFront();

                            e.Node.BeginEdit();

                            return;
                        }
                        tool.Inputs[hyNode.Text].Name = e.Label;
                    }
                    else
                    {
                        if (tool.Outputs.Contains(e.Label))
                        {
                            e.CancelEdit = true;

                            MessageBoxUtils.ShowWarn("名称已存在");
                            TopLevelControl.BringToFront();

                            e.Node.BeginEdit();

                            return;
                        }
                        tool.Outputs[hyNode.Text].Name = e.Label;
                    }
                }
            }
        }

        #endregion

        private void cmbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            HyTerminal terminal = toolBlock.Inputs[(string)cmbImages.SelectedItem];

            pictureBox1.Image = ((HyImage)terminal.Value).Image;
        }

        public void ExpandAll()
        {
            treeView1.ExpandAll();
        }

        #region 右键菜单 显示

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode == inputsNode || treeView1.SelectedNode == outputsNode)
            {
                AddMenuVisible(true);
                OtherMenuVisible(false);
            }
            else
            {
                AddMenuVisible(false);
                OtherMenuVisible(true);
            }
        }

        void AddMenuVisible(bool visible)
        {
            tsmiAddInt.Visible = visible;
            tsmiAddDouble.Visible = visible;
            tsmiAddString.Visible = visible;
            tsmiAddDatetime.Visible = visible;
            tsmiAddHyImage.Visible = visible;
        }

        void OtherMenuVisible(bool visible)
        {
            HyTreeNode selNode = treeView1.SelectedNode;

            tsmiDelLink.Visible = visible && selNode.Parent != null;
            tsmiDelete.Visible = visible;

            toolStripSeparator1.Visible = visible;

            tsmiMoveUp.Visible = visible;
            tsmiMoveDown.Visible = visible;
            tsmiRename.Visible = visible;

            if (visible && selNode.Parent != null)
            {
                tsmiDelLink.Enabled = selNode.LinkStartNode != null
                    || (selNode.LinkEndNodes != null && selNode.LinkEndNodes.Count > 0);
            }

            if (visible)
            {
                int index = selNode.Index;

                if (selNode.Parent == null)
                {
                    int count = treeView1.Nodes.Count;

                    tsmiMoveUp.Enabled = index > 1;
                    tsmiMoveDown.Enabled = index < count - 2;
                }
                else
                {
                    int count = selNode.Parent.Nodes.Count;

                    tsmiMoveUp.Enabled = index != 0;
                    tsmiMoveDown.Enabled = index != count - 1;
                }
            }
        }

        #endregion

    }
}
