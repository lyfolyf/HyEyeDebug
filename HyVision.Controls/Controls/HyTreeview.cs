using HyVision.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HyVision.Controls
{
    public partial class HyTreeView : TreeView
    {
        static readonly Type ImageType = typeof(HyImage);

        public event EventHandler<NodeLinkingEventArgs> NodeLinking;
        public event EventHandler<NodeLinkedEventArgs> NodeLinked;

        HyTreeNode m_LinkStartNode;     // MouseDown 时选中的 Node
        HyTreeNode m_HoveredNode;       // MouseMove 时悬停的 Node

        public HyTreeView()
        {
            InitializeComponent();
        }

        public new HyTreeNodeCollection Nodes
        {
            get { return new HyTreeNodeCollection(base.Nodes); }
        }

        public new HyTreeNode SelectedNode
        {
            get { return (HyTreeNode)base.SelectedNode; }
            set { base.SelectedNode = value; }
        }

        int X = -1;

        protected override void OnPaint(PaintEventArgs e)
        {
            List<HyTreeNode> allNodes = new List<HyTreeNode>();
            List<HyTreeNode> linkNodes = new List<HyTreeNode>();

            GetLinkNodes(allNodes, linkNodes);

            if (linkNodes.Count == 0) return;

            X = allNodes.Max(a => a.VisibleBounds.Right);

            //AdjustNodeLinkPositions(allNodes, linkNodes);

            Graphics g = e.Graphics;

            using (Pen pen = new Pen(Color.Blue))
            {
                foreach (HyTreeNode node in linkNodes)
                {
                    int startX = node.VisibleBounds.Right + 1;
                    int startY = node.VisibleBounds.Y + node.VisibleBounds.Height / 2 + 2;

                    X += offset;

                    foreach (HyTreeNode endNode in node.LinkEndNodes)
                    {
                        pen.Width = node.IsSelected || endNode.IsSelected ? 2 : 1;

                        Rectangle endRec = endNode.VisibleBounds;
                        int endX = endNode.VisibleBounds.Right + 1;
                        int endY = endRec.Y + endRec.Height / 2;

                        g.DrawLine(pen, startX, startY, X, startY);
                        g.DrawLine(pen, X, startY, X, endY);
                        g.DrawLine(pen, X, endY, endX, endY);

                        g.DrawLine(pen, endX, endY, endX + 5, endY + 5);
                        g.DrawLine(pen, endX, endY, endX + 5, endY - 5);
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0F)
            {
                using (Graphics graphics = CreateGraphics())
                {
                    PaintEventArgs e = new PaintEventArgs(graphics, ClientRectangle);
                    OnPaint(e);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            m_LinkStartNode = (HyTreeNode)GetNodeAt(e.X, e.Y);

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            HyTreeNode linkEndNode = (HyTreeNode)GetNodeAt(e.X, e.Y);

            if (m_LinkStartNode != null && linkEndNode != null
                && m_LinkStartNode.NodeType == HyNodeType.Start && linkEndNode.NodeType == HyNodeType.End
                && linkEndNode.LinkStartNode != m_LinkStartNode
                && m_LinkStartNode.Parent != linkEndNode.Parent
                && m_LinkStartNode.Terminal.ValueType == linkEndNode.Terminal.ValueType)
                //add by Luodian @ 20211029 支持父类的输入输出值连线传递
                //|| (m_LinkStartNode.Terminal.ValueType.IsSubclassOf(linkEndNode.Terminal.ValueType))))
            {
                NodeLinkingEventArgs args = new NodeLinkingEventArgs(m_LinkStartNode, linkEndNode, false);

                OnNodeLinking(args);

                if (!args.Cancel)
                {
                    if (linkEndNode.LinkStartNode != null)
                    {
                        linkEndNode.LinkStartNode.LinkEndNodes.Remove(linkEndNode);
                    }

                    if (m_LinkStartNode.LinkEndNodes == null)
                        m_LinkStartNode.LinkEndNodes = new List<HyTreeNode>();
                    m_LinkStartNode.LinkEndNodes.Add(linkEndNode);

                    linkEndNode.LinkStartNode = m_LinkStartNode;

                    OnNodeLinked(new NodeLinkedEventArgs(m_LinkStartNode, linkEndNode));

                    Invalidate();
                }
            }

            m_LinkStartNode = null;

            base.OnMouseUp(e);
        }

        int order = 0;
        int offset = 10;

        void GetLinkNodes(List<HyTreeNode> allNodes, List<HyTreeNode> linkNodes)
        {
            order = 0;

            GetLinkNodes(Nodes, allNodes, linkNodes);
        }

        void GetLinkNodes(HyTreeNodeCollection nodeCollection, List<HyTreeNode> allNodes, List<HyTreeNode> linkNodes)
        {
            foreach (HyTreeNode node in nodeCollection)
            {
                node.UpdateVisibleBounds();
                node.Order = order++;

                allNodes.Add(node);

                if (node.LinkEndNodes != null && node.LinkEndNodes.Count > 0)
                    linkNodes.Add(node);

                if (node.Nodes.Count > 0)
                {
                    GetLinkNodes(node.Nodes, allNodes, linkNodes);
                }
            }
        }

        void AdjustNodeLinkPositions(List<HyTreeNode> allNodes, List<HyTreeNode> linkNodes)
        {
            //foreach (HyTreeNode linkNode in linkNodes)
            //{
            //    int startIndex = linkNode.Order;
            //    int endIndex = linkNode.LinkEndNodes.Max(a => a.Order);

            //    linkNode.X = allNodes.Skip(startIndex).Take(endIndex - startIndex + 1).Max(a => a.VisibleBounds.Right) + offset;
            //}
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            HyTreeNode linkEndNode = (HyTreeNode)GetNodeAt(e.X, e.Y);

            if (m_LinkStartNode != null && linkEndNode != null)
            {
                if (linkEndNode.NodeType == HyNodeType.Start || m_LinkStartNode.Parent == linkEndNode.Parent)
                {
                    Cursor = Cursors.No;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }


            if (!object.ReferenceEquals(linkEndNode, m_HoveredNode))
            {
                m_HoveredNode = linkEndNode;

                if (ShowNodeToolTips)
                {
                    if (m_HoveredNode != null && m_HoveredNode.Terminal != null)
                        toolTip1.Show(m_HoveredNode.Terminal?.GetToolTip(), this, e.X, e.Y + 26, 30000);
                    else
                        toolTip1.Hide(this);
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            toolTip1.Hide(this);

            base.OnMouseLeave(e);
        }

        protected void OnNodeLinking(NodeLinkingEventArgs e)
        {
            NodeLinking?.Invoke(this, e);
        }

        protected void OnNodeLinked(NodeLinkedEventArgs e)
        {
            NodeLinked?.Invoke(this, e);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            HyTerminal terminal = m_HoveredNode.Terminal;
            if (terminal == null)
                return;

            Size borderSize = new Size(8, 8);

            if (terminal.Value != null && terminal.ValueType == ImageType)
            {
                HyImage image = (HyImage)terminal.Value;

                //add by LuoDian @ 20210819 有时候会出现运行失败，没有输出图像数据的情况，这时候再获取图像数据会导致异常
                if (image.Image == null || image.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined)
                    return;

                Size imageSize = getSize(image);

                e.ToolTipSize = imageSize + borderSize;
            }
            else
            {
                Size stringSize = Graphics.FromHwnd(e.AssociatedWindow.Handle).MeasureString(terminal.GetToolTip(), SystemFonts.DefaultFont).ToSize();

                e.ToolTipSize = stringSize + borderSize;
            }
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            HyTerminal terminal = m_HoveredNode.Terminal;
            if (terminal == null)
                return;

            e.Graphics.FillRectangle(toolTip1.BackColor, e.Bounds);

            if (terminal.Value != null && terminal.ValueType == ImageType)
            {
                int x = e.Bounds.X + 4;
                int y = e.Bounds.Y + 4;

                HyImage image = (HyImage)terminal.Value;

                //add by LuoDian @ 20210819 有时候会出现运行失败，没有输出图像数据的情况，这时候再获取图像数据会导致异常
                if (image.Image == null || image.Image.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined)
                    return;

                Size imageSize = getSize(image);
                Rectangle rect = new Rectangle(x, y, imageSize.Width, imageSize.Height);

                ControlPaint.DrawBorder(e.Graphics, e.Bounds, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
                e.Graphics.DrawImage(image.Image, rect);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, e.Bounds, SystemColors.WindowFrame, ButtonBorderStyle.Solid);

                e.Graphics.DrawString(e.ToolTipText, SystemFonts.DefaultFont, toolTip1.ForeColor, new PointF(e.Bounds.X + 4, e.Bounds.Y + 4));
            }
        }

        const int defaultSize = 200;

        Size getSize(HyImage image)
        {
            if (image.Image.Width > image.Image.Height)
            {
                return new Size(defaultSize, defaultSize * image.Image.Height / image.Image.Width);
            }
            else
            {
                return new Size(defaultSize * image.Image.Width / image.Image.Height, defaultSize);
            }
        }

    }
}
