using HyVision.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HyVision.Controls
{
    public class HyTreeNode : TreeNode
    {
        /// <summary>
        /// 连接的起始节点
        /// </summary>
        internal HyTreeNode LinkStartNode { get; set; }

        /// <summary>
        /// 连接的结束节点，可以有多个
        /// </summary>
        internal List<HyTreeNode> LinkEndNodes { get; set; }

        /// <summary>
        /// 节点顺序
        /// <para>从上到下的可见顺序</para>
        /// </summary>
        internal int Order { get; set; }

        internal HyNodeType NodeType { get; set; }

        internal HyTerminal Terminal { get; set; }

        public HyTreeNode(string text) : base(text) { }

        public new HyTreeNodeCollection Nodes
        {
            get { return new HyTreeNodeCollection(base.Nodes); }
        }

        public new HyTreeNode Parent
        {
            get { return (HyTreeNode)base.Parent; }
        }

        internal Rectangle VisibleBounds { get; set; }

        internal void UpdateVisibleBounds()
        {
            if (base.TreeView != null && base.TreeView.InvokeRequired)
            {
                base.TreeView.Invoke(new MethodInvoker(UpdateVisibleBounds));
                return;
            }

            HyTreeNode node = this;
            for (HyTreeNode parent = (HyTreeNode)Parent; parent != null; parent = (HyTreeNode)parent.Parent)
            {
                if (!parent.IsExpanded)
                {
                    node = parent;
                }
            }

            VisibleBounds = node.Bounds;
        }
    }

    public class HyTreeNodeCollection : ICollection, IEnumerable
    {
        readonly TreeNodeCollection _realNodes;

        internal HyTreeNodeCollection(TreeNodeCollection realNodes)
        {
            if (realNodes == null)
            {
                throw new ArgumentNullException("realNodes");
            }

            _realNodes = realNodes;
        }

        public HyTreeNode this[int index]
        {
            get
            {
                return (HyTreeNode)_realNodes[index];
            }
        }

        public int Count => _realNodes.Count;

        public object SyncRoot => ((ICollection)_realNodes).SyncRoot;

        public bool IsSynchronized => ((ICollection)_realNodes).IsSynchronized;

        public void Add(HyTreeNode node)
        {
            _realNodes.Add(node);
        }

        public void Insert(int index, HyTreeNode node)
        {
            _realNodes.Insert(index, node);
        }

        public void Remove(HyTreeNode node)
        {
            _realNodes.Remove(node);
        }

        public void RemoveAt(int index)
        {
            _realNodes.RemoveAt(index);
        }

        public void Clear()
        {
            _realNodes.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            _realNodes.CopyTo(array, index);
        }

        public void MoveNode(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= _realNodes.Count) throw new IndexOutOfRangeException();
            if (toIndex < 0 || toIndex >= _realNodes.Count) throw new IndexOutOfRangeException();

            if (fromIndex == toIndex) return;

            TreeNode node = _realNodes[fromIndex];

            _realNodes.RemoveAt(fromIndex);

            _realNodes.Insert(toIndex, node);

            node.TreeView.SelectedNode = node;
        }

        public IEnumerator GetEnumerator()
        {
            return _realNodes.GetEnumerator();
        }
    }
}
