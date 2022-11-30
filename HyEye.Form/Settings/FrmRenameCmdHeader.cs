using HyEye.API.Repository;
using HyEye.Models.VO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmRenameCmdHeader : Form
    {
        readonly ICommandRepository commandRepo;

        int len;
        bool isCalib;

        public string CmdHeader { get; private set; }

        public FrmRenameCmdHeader(ReceiveCommandInfoVO recvCommand, ICommandRepository commandRepo)
        {
            InitializeComponent();

            this.commandRepo = commandRepo;

            if (recvCommand.CalibrationType == null)
                tbTaskName.Text = recvCommand.TaskName;
            else
                tbTaskName.Text = recvCommand.Name;
            tbCmdHeader.Text = recvCommand.CommandHeader;

            len = recvCommand.CommandHeader.Match("[A-Z]+").Length;
            isCalib = recvCommand.CalibrationType != null;

            tbCmdHeader.MaxLength = len + 2;

            tbCmdHeader.Focus();
            tbCmdHeader.Select(recvCommand.TaskName.Length, 0);
        }

        private void tbCmdHeader_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= (char)Keys.D0 && e.KeyChar <= (char)Keys.D9)
            {
                if (tbCmdHeader.SelectionStart < len)
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                if (tbCmdHeader.Text.Length == len)
                    e.Handled = true;

                if (tbCmdHeader.SelectionStart < len)
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK.PerformClick();
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (tbCmdHeader.Text.Length == len)
            {
                MessageBoxUtils.ShowWarn("请输入指令头序号");
                return;
            }

            if (isCalib)
            {
                commandRepo.RenameCalibCmdHeader(tbTaskName.Text, tbCmdHeader.Text);
                CmdHeader = tbCmdHeader.Text;
            }
            else
            {
                commandRepo.RenameTaskCmdHeader(tbTaskName.Text, tbCmdHeader.Text);
                CmdHeader = tbCmdHeader.Text;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
