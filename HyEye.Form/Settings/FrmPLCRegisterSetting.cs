using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmPLCRegisterSetting : Form
    {
        readonly ICommandRepository commandRepo;

        public FrmPLCRegisterSetting(bool readOnly, ICommandRepository commandRepo)
        {
            InitializeComponent();

            this.commandRepo = commandRepo;
        }

        private void FrmRegisterConfig_Load(object sender, EventArgs e)
        {
            LoadReceiveCommands();

            LoadSendCommands();

            LoadCalibrationReceiveCommands();

            LoadCalibrationSendCommands();
        }

        void LoadReceiveCommands()
        {
            List<ReceiveCommandInfoVO> commands = commandRepo.GetTaskRecvCommandInfos();

            foreach (ReceiveCommandInfoVO cmd in commands)
            {
                //int rowIndex = dgvRecvCommands.Rows.Add(
                //    cmd.TaskName,
                //    cmd.Name,
                //    cmd.CommandHeader,
                //    cmd.Index,
                //    CommandTypeString,
                //    string.Join(",", cmd.Fields.Select(f => f.Name)));
                //dgvRecvCommands.Rows[rowIndex].Tag = cmd;
            }
        }

        void LoadSendCommands()
        {

        }

        void LoadCalibrationReceiveCommands()
        {

        }

        void LoadCalibrationSendCommands()
        {
        }
    }
}
