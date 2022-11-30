using System;
using System.IO;
using System.Windows.Forms;

namespace HyVision.Tools.ToolBox
{
    public class UserToolEventArgs : EventArgs
    {
        public IHyUserTool Tool { get; }

        public UserToolEventArgs(IHyUserTool userTool)
        {
            Tool = userTool;
        }
    }

    public partial class HyToolboxForm : Form
    {
        public event EventHandler<UserToolEventArgs> SelectTool;

        public HyToolboxForm()
        {
            InitializeComponent();
        }

        private void HyToolboxForm_Load(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();

            DirectoryInfo toolTemplatesDirectoryInfo = new DirectoryInfo(HyVisionUtils.HyVisionToolPath);
            if (toolTemplatesDirectoryInfo != null)
            {
                PopulateNodes(treeView1.Nodes, toolTemplatesDirectoryInfo);
            }

            treeView1.EndUpdate();
        }

        private void PopulateNodes(TreeNodeCollection nodes, DirectoryInfo directoryInfo)
        {
            if (nodes == null || directoryInfo == null)
            {
                throw new ArgumentNullException((nodes == null) ? "nodes" : "directoryInfo");
            }

            FileSystemInfo[] toolTemplatesFileSystemInfos = FileUtils.GetFileSystems(directoryInfo);
            foreach (FileSystemInfo fileSystemInfo in toolTemplatesFileSystemInfos)
            {
                if (fileSystemInfo is FileInfo)
                {
                    if ((!(fileSystemInfo.Extension.ToLower() == HyVisionUtils.TemplatesExtension)
                        && !(fileSystemInfo.Extension.ToLower() == ".hy")))
                    {
                        continue;
                    }

                    TreeNode treeNode = new TreeNode(fileSystemInfo.Name.Substring(0, fileSystemInfo.Name.LastIndexOf('.')));
                    treeNode.Tag = fileSystemInfo.FullName;

                    nodes.Add(treeNode);
                }
                else if (fileSystemInfo is DirectoryInfo)
                {
                    //CogTreeNode cogTreeNode2 = new CogToolTemplateNode(toolTreeView, fileSystemInfo);
                    //PopulateNodes(cogTreeNode2.Nodes, (DirectoryInfo)fileSystemInfo);
                    //if (cogTreeNode2.Nodes.Count != 0)
                    //{
                    //    cogTreeNode2.Text = fileSystemInfo.Name;
                    //    cogTreeNode2.BeforeCollapse += node_BeforeCollapse;
                    //    cogTreeNode2.BeforeExpand += node_BeforeExpand;
                    //    cogTreeNode2.Image = (cogTreeNode2.IsExpanded ? folderOpenIcon_ : folderClosedIcon_);
                    //    nodes.Add(cogTreeNode2);
                    //}
                }
            }

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string path = (string)e.Node.Tag;

            IHyUserTool tool = (IHyUserTool)HySerializer.LoadFromFile(path);

            SelectTool?.Invoke(this, new UserToolEventArgs(tool));
        }
    }
}
