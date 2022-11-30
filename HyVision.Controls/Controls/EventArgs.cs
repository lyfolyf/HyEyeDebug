using System;
using System.ComponentModel;

namespace HyVision.Controls
{
    public class NodeLinkedEventArgs : EventArgs
    {
        public HyTreeNode StartNode { get; set; }

        public HyTreeNode EndNode { get; set; }

        public NodeLinkedEventArgs(HyTreeNode startNode, HyTreeNode endNode)
        {
            StartNode = startNode;
            EndNode = endNode;
        }
    }

    public class NodeLinkingEventArgs : CancelEventArgs
    {
        public HyTreeNode StartNode { get; set; }

        public HyTreeNode EndNode { get; set; }

        public NodeLinkingEventArgs(HyTreeNode startNode, HyTreeNode endNode, bool cancel)
             : base(cancel)
        {
            StartNode = startNode;
            EndNode = endNode;
        }
    }
}
