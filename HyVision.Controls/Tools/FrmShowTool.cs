using System;
using System.Windows.Forms;

namespace HyVision.Tools
{
    public partial class FrmShowTool : Form
    {
        readonly IHyUserTool hyUserTool;
        dynamic toolEdit;

        public FrmShowTool(IHyUserTool hyUserTool)
        {
            InitializeComponent();

            this.hyUserTool = hyUserTool;
        }

        private void FrmShowTool_Load(object sender, EventArgs e)
        {
            Text = hyUserTool.Name;

            toolEdit = Activator.CreateInstance(hyUserTool.ToolEditType);
            toolEdit.Subject = (dynamic)hyUserTool;
            toolEdit.Dock = DockStyle.Fill;

            Width = toolEdit.Width + 50;
            Height = toolEdit.Height + 47;

            Controls.Add(toolEdit);
        }

        private void FrmShowTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            toolEdit.UpdateDataToObject();
        }

    }
}
