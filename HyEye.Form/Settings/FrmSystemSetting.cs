using GL.Kit.Log;
using HyEye.API.Repository;
using System;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmSystemSetting : Form
    {
        readonly ISystemRepository systemRepo;
        readonly IDataRepository dataRepo;
        readonly ILogRepository logRepo;
        readonly ICommunicationRepository communicationRepo;
        readonly ICommandRepository commandRepo;

        public FrmSystemSetting(bool readOnly,
            ISystemRepository systemRepo,
            IDataRepository dataRepo,
            ILogRepository logRepo,
            ICommunicationRepository communicationRepo,
            ICommandRepository commandRepo)
        {
            InitializeComponent();

            pnlTop.Enabled = !readOnly;
            pnlBottom.Enabled = !readOnly;

            cmbFileLogLevel.BindEnum<LogLevel>();

            this.systemRepo = systemRepo;
            this.dataRepo = dataRepo;
            this.logRepo = logRepo;
            this.communicationRepo = communicationRepo;
            this.commandRepo = commandRepo;
        }

        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            // 基本设置
            cbAutoStart.Checked = systemRepo.AutoStart;
            cbDeleteVPP.Checked = systemRepo.DeleteVPP;
            if (systemRepo.VPPExcludeDataBindings)
                rdbtnSaveExcludeDataBindings.Checked = true;
            else
                rdbtnSaveAll.Checked = true;
            tbSimulationPath.Text = systemRepo.SimulationPath;

            //config保存
            ckConfigSavePath.Checked = systemRepo.AutoSaveConfig;
            txtConfigSavePath.Text = systemRepo.ConfigSavePath;
            txtConfigSaveType.Text = systemRepo.SaveType.ToString();

            // 运行设置
            nudAcqImageTimeout.Value = systemRepo.AcquireImageTimeout;
            nudCmdRTimeout.Value = systemRepo.CmdRTimeout;

            // 数据保存
            ckbDataSaveEnabled.Checked = dataRepo.Enabled;
            tbDataSavePath.Text = dataRepo.SavePath;
            if (dataRepo.SaveMode == API.Config.DataSaveMode.All)
            {
                rdbtnDataSaveModeAll.Checked = true;
            }
            else
            {
                rdbtnDataSaveModeLast.Checked = true;
            }

            // 指令设置
            if (commandRepo.SendCmdFormat)
                rdbCmdKeyValue.Checked = true;
            else
                rdbOnlyValue.Checked = true;

            nudDecimalPlaces.Value = commandRepo.DecimalPlaces;
            ckbEnableHandCmd.Checked = commandRepo.EnableHandCmd;
            ckbEnableCmdIndex.Checked = commandRepo.EnableCmdIndex;

            if (communicationRepo?.GetCommunicationInfo()?.CommProtocol == GL.Kit.Net.CommProtocol.PLC)
            {
                rdbCmdKeyValue.Enabled = false;
                rdbOnlyValue.Enabled = false;
                nudDecimalPlaces.Enabled = false;
                ckbEnableCmdIndex.Enabled = false;
            }
            else
            {
                rdbCmdKeyValue.Enabled = true;
                rdbOnlyValue.Enabled = true;
                nudDecimalPlaces.Enabled = true;
                ckbEnableCmdIndex.Enabled = true;
            }

            // 日志
            cmbFileLogLevel.SelectedItem = logRepo.FileLevel;
        }

        FolderBrowserDialog simulationFolderDialog;
        FolderBrowserDialog dataSaveFolderDialog;
        FolderBrowserDialog configSaveFolderDialog;

        private void btnSearchSimulationPath_Click(object sender, EventArgs e)
        {
            if (simulationFolderDialog == null)
            {
                simulationFolderDialog = new FolderBrowserDialog
                {
                    Description = "选择模拟图片路径",
                    SelectedPath = tbSimulationPath.Text
                };
            }

            if (simulationFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tbSimulationPath.Text = simulationFolderDialog.SelectedPath;
            }
        }

        private void ckbDataSaveEnabled_CheckedChanged(object sender, EventArgs e)
        {
            btnSearchDataSavePath.Enabled = ckbDataSaveEnabled.Checked;
            rdbtnDataSaveModeAll.Enabled = ckbDataSaveEnabled.Checked;
            rdbtnDataSaveModeLast.Enabled = ckbDataSaveEnabled.Checked;
        }

        private void btnSearchDataSavePath_Click(object sender, EventArgs e)
        {
            if (dataSaveFolderDialog == null)
            {
                dataSaveFolderDialog = new FolderBrowserDialog
                {
                    Description = "选择数据存储路径",
                    SelectedPath = tbDataSavePath.Text
                };
            }

            if (dataSaveFolderDialog.ShowDialog() == DialogResult.OK)
            {
                tbDataSavePath.Text = dataSaveFolderDialog.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            systemRepo.AutoStart = cbAutoStart.Checked;
            systemRepo.DeleteVPP = cbDeleteVPP.Checked;
            systemRepo.VPPExcludeDataBindings = rdbtnSaveExcludeDataBindings.Checked;
            systemRepo.SimulationPath = tbSimulationPath.Text;

            //config保存
            systemRepo.AutoSaveConfig = ckConfigSavePath.Checked;
            systemRepo.ConfigSavePath = txtConfigSavePath.Text;
            systemRepo.SaveType = Convert.ToInt32(txtConfigSaveType.Text);

            systemRepo.AcquireImageTimeout = (int)nudAcqImageTimeout.Value;
            systemRepo.CmdRTimeout = (int)nudCmdRTimeout.Value;

            systemRepo.Save();

            dataRepo.Enabled = ckbDataSaveEnabled.Checked;
            dataRepo.SavePath = tbDataSavePath.Text;
            dataRepo.SaveMode = rdbtnDataSaveModeAll.Checked ? API.Config.DataSaveMode.All : API.Config.DataSaveMode.Last;
            dataRepo.Save();

            commandRepo.SendCmdFormat = rdbCmdKeyValue.Checked;
            commandRepo.DecimalPlaces = (int)nudDecimalPlaces.Value;
            commandRepo.EnableCmdIndex = ckbEnableCmdIndex.Checked;
            commandRepo.EnableHandCmd = ckbEnableHandCmd.Checked;
            commandRepo.Save();

            logRepo.FileLevel = (LogLevel)cmbFileLogLevel.SelectedItem;
            logRepo.Save();

            DialogResult = DialogResult.OK;
        }

        private void btnSelectConfigSavePath_Click(object sender, EventArgs e)
        {
            if (configSaveFolderDialog == null)
            {
                configSaveFolderDialog = new FolderBrowserDialog
                {
                    Description = "选择配置保存路径",
                    SelectedPath = txtConfigSavePath.Text
                };
            }

            if (configSaveFolderDialog.ShowDialog() == DialogResult.OK)
            {
                txtConfigSavePath.Text = configSaveFolderDialog.SelectedPath;
            }
        }

        private void ckConfigSavePath_CheckedChanged(object sender, EventArgs e)
        {
            btnSelectConfigSavePath.Enabled = ckConfigSavePath.Checked;
            txtConfigSaveType.Enabled = ckConfigSavePath.Checked;
        }

    }
}
