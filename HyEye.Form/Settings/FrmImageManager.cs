using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmImageManager : Form
    {
        readonly IImageSaveRepository imageSaveRepo;

        // 用来避免 OK/NG 的 ValueChanged 事件在初始化时执行
        bool cancelToken = false;

        public FrmImageManager(bool readOnly, IImageSaveRepository imageSaveRepo)
        {
            InitializeComponent();

            pnlMain.Enabled = !readOnly;

            this.imageSaveRepo = imageSaveRepo;

            cmbResultImageFormat.Items.Add(ImageFormat.Bmp);
            cmbResultImageFormat.Items.Add(ImageFormat.Jpeg);
        }

        private void FrmImageManager_Load(object sender, EventArgs e)
        {
            cancelToken = true;

            ckbLowestPriority.Checked = imageSaveRepo.LowestPriority;

            nudScanTime.Value = imageSaveRepo.ScanTime;
            nudMinSpace.Value = imageSaveRepo.MinSpace;
            nudCacheNum.Value = imageSaveRepo.CacheNum;

            // 保存路径绑定
            tbSavePath.Text = PathUtils.GetAbsolutePath(imageSaveRepo.Root);
            ckbSubDireByTask.Checked = (imageSaveRepo.SubDirectory & SubDireOfSaveImage.Task) != 0;
            ckbSubDireBySN.Checked = (imageSaveRepo.SubDirectory & SubDireOfSaveImage.SN) != 0;
            ckbSubDireByFlag.Checked = (imageSaveRepo.SubDirectory & SubDireOfSaveImage.Flag) != 0;
            ckbSubDireBySource.Checked = (imageSaveRepo.SubDirectory & SubDireOfSaveImage.Source) != 0;

            ImageDeleteInfoVO deleteInfo = imageSaveRepo.GetDeleteInfo();

            // 删除模式
            rdoNoDelete.Checked = deleteInfo.DeleteMode == ImageDeleteMode.NoDelete;
            rdoCycleDelete.Checked = deleteInfo.DeleteMode == ImageDeleteMode.Cycle;
            rdoDefiniteTimeDelete.Checked = deleteInfo.DeleteMode == ImageDeleteMode.DefiniteTime;

            // 周期删除
            nudCycleMin.Value = deleteInfo.CycleDelete.CycleMin;
            nudCycleDeleteSize.Value = deleteInfo.CycleDelete.DeleteSize;

            rdoDiskUsageExceedsSize.Checked = deleteInfo.CycleDelete.Condition == ImageDeleteCondition.DiskUsageExceeds;
            rdoDiskFreeLessSize.Checked = deleteInfo.CycleDelete.Condition == ImageDeleteCondition.DiskFreeLess;
            nudDiskUsageExceedsSize.Value = deleteInfo.CycleDelete.CriticalValue;
            nudDiskFreeLessSize.Value = deleteInfo.CycleDelete.CriticalValue;

            // 定时删除绑定
            nudStartTimeHour.Value = deleteInfo.DefiniteTimeDelete.StartHour;
            nudStartTimeMin.Value = deleteInfo.DefiniteTimeDelete.StartMin;
            nudStopTimeHour.Value = deleteInfo.DefiniteTimeDelete.StopHour;
            nudStopTimeMin.Value = deleteInfo.DefiniteTimeDelete.StopMin;
            nudRetentionDays.Value = deleteInfo.DefiniteTimeDelete.RetentionDays;

            // 保存设置
            cmbVTasks.DisplayMember = "TaskName";
            cmbVTasks.DataSource = imageSaveRepo.GetSaveInfos();

            cancelToken = false;
        }

        // 切换任务
        private void cmbVTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVTasks.SelectedIndex == -1)
            {
                gbSaveSrc.Enabled = false;
                gbSaveRet.Enabled = false;
                gbNamedBySN.Enabled = false;
                return;
            }
            else
            {
                gbSaveSrc.Enabled = true;
                gbSaveRet.Enabled = true;
                gbNamedBySN.Enabled = true;
            }

            cancelToken = true;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;

            cbNamedBySN.Checked = saveInfo.NamedBySN;
            rdbEachHasSN.Enabled = cbNamedBySN.Checked;
            rdbOnlyOneHasSN.Enabled = cbNamedBySN.Checked;
            if (saveInfo.OnlyOneSN)
                rdbOnlyOneHasSN.Checked = true;
            else
                rdbEachHasSN.Checked = true;

            lblAcqIndexHasSN.Enabled = saveInfo.NamedBySN && rdbOnlyOneHasSN.Checked;
            nudAcqIndexHasSN.Enabled = saveInfo.NamedBySN && rdbOnlyOneHasSN.Checked;
            nudAcqIndexHasSN.Value = Math.Max(saveInfo.AcqIndexHasSN, 1);

            nudSrcCompressionFactor.Value = saveInfo.SrcCompressionFactor;
            nudResultCompressionFactor.Value = saveInfo.ResultCompressionFactor;

            ckbSrcSaveOK.Checked = (saveInfo.SrcSaveMode & ImageSaveMode.OK) != 0;
            ckbSrcSaveNG.Checked = (saveInfo.SrcSaveMode & ImageSaveMode.NG) != 0;
            ckbRetSaveOK.Checked = (saveInfo.ResultSaveMode & ImageSaveMode.OK) != 0;
            ckbRetSaveNG.Checked = (saveInfo.ResultSaveMode & ImageSaveMode.NG) != 0;

            cmbResultImageFormat.SelectedItem = saveInfo.ResultImageFormat;

            cancelToken = false;
        }

        // 删除模式切换显示
        private void rdoNoDelete_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked == false) return;

            if (sender == rdoNoDelete)
            {
                grpCycleDelete.Enabled = false;
                grpDefiniteTimeDelete.Enabled = false;
            }
            else if (sender == rdoCycleDelete)
            {
                grpCycleDelete.Enabled = true;
                grpDefiniteTimeDelete.Enabled = false;
            }
            else if (sender == rdoDefiniteTimeDelete)
            {
                grpCycleDelete.Enabled = false;
                grpDefiniteTimeDelete.Enabled = true;
            }
        }

        // 删除条件切换显示
        private void rdoDiskUsageExceedsSize_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == rdoDiskUsageExceedsSize)
            {
                nudDiskUsageExceedsSize.Enabled = true;
                nudDiskFreeLessSize.Enabled = false;
            }
            else if (sender == rdoDiskFreeLessSize)
            {
                nudDiskUsageExceedsSize.Enabled = false;
                nudDiskFreeLessSize.Enabled = true;
            }
        }

        #region 原图

        private void nudSrcCompressionFactor_ValueChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;
            saveInfo.SrcCompressionFactor = nudSrcCompressionFactor.Value;
        }

        private void ckbSrcSaveOK_CheckedChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;

            saveInfo.SrcSaveMode = ckbSrcSaveOK.Checked ?
                saveInfo.SrcSaveMode | ImageSaveMode.OK :
                saveInfo.SrcSaveMode & ~ImageSaveMode.OK;
        }

        private void ckbSrcSaveNG_CheckedChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;

            saveInfo.SrcSaveMode = ckbSrcSaveNG.Checked ?
                saveInfo.SrcSaveMode | ImageSaveMode.NG :
                saveInfo.SrcSaveMode & ~ImageSaveMode.NG;
        }

        #endregion

        #region 结果图

        private void nudResultCompressionFactor_ValueChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;
            saveInfo.ResultCompressionFactor = nudResultCompressionFactor.Value;
        }

        private void cmbResultImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;

            saveInfo.StrResultImageFormat = cmbResultImageFormat.SelectedItem.ToString();
        }

        private void ckbRetSaveOK_CheckedChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;

            saveInfo.ResultSaveMode = ckbRetSaveOK.Checked ?
                saveInfo.ResultSaveMode | ImageSaveMode.OK :
                saveInfo.ResultSaveMode & ~ImageSaveMode.OK;
        }

        private void ckbRetSaveNG_CheckedChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;

            saveInfo.ResultSaveMode = ckbRetSaveNG.Checked ?
                saveInfo.ResultSaveMode | ImageSaveMode.NG :
                saveInfo.ResultSaveMode & ~ImageSaveMode.NG;
        }

        #endregion

        #region SN 命名

        private void cbNamedBySN_CheckedChanged(object sender, EventArgs e)
        {
            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;
            saveInfo.NamedBySN = cbNamedBySN.Checked;

            rdbEachHasSN.Enabled = cbNamedBySN.Checked;
            rdbOnlyOneHasSN.Enabled = cbNamedBySN.Checked;

            if (cbNamedBySN.Checked)
            {
                lblAcqIndexHasSN.Enabled = rdbOnlyOneHasSN.Checked;
                nudAcqIndexHasSN.Enabled = rdbOnlyOneHasSN.Checked;

                //ckbSubDireBySN.Enabled = true;
            }
            else
            {
                lblAcqIndexHasSN.Enabled = false;
                nudAcqIndexHasSN.Enabled = false;

                //ckbSubDireBySN.Enabled = false;
                //ckbSubDireBySN.Checked = false;
            }
        }

        private void rdbOnlyOneHasSN_CheckedChanged(object sender, EventArgs e)
        {
            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;
            saveInfo.OnlyOneSN = rdbOnlyOneHasSN.Checked;

            lblAcqIndexHasSN.Enabled = rdbOnlyOneHasSN.Checked;
            nudAcqIndexHasSN.Enabled = rdbOnlyOneHasSN.Checked;
        }

        private void nudAcqIndexHasSN_ValueChanged(object sender, EventArgs e)
        {
            if (cancelToken) return;

            ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)cmbVTasks.SelectedItem;
            saveInfo.AcqIndexHasSN = (int)nudAcqIndexHasSN.Value;
        }

        #endregion

        #region 应用到所有任务

        private void btnSetToAllTask_Click(object sender, EventArgs e)
        {
            foreach (object item in cmbVTasks.Items)
            {
                ImageSaveInfoVO saveInfo = (ImageSaveInfoVO)item;

                saveInfo.SrcCompressionFactor = nudSrcCompressionFactor.Value;
                saveInfo.SrcSaveMode = (ckbSrcSaveOK.Checked ? ImageSaveMode.OK : ImageSaveMode.None) |
                    (ckbSrcSaveNG.Checked ? ImageSaveMode.NG : ImageSaveMode.None);

                saveInfo.ResultCompressionFactor = nudResultCompressionFactor.Value;
                saveInfo.ResultSaveMode = (ckbRetSaveOK.Checked ? ImageSaveMode.OK : ImageSaveMode.None) |
                    (ckbRetSaveNG.Checked ? ImageSaveMode.NG : ImageSaveMode.None);

                saveInfo.StrResultImageFormat = cmbResultImageFormat.SelectedItem.ToString();

                saveInfo.NamedBySN = cbNamedBySN.Checked;
                saveInfo.OnlyOneSN = rdbOnlyOneHasSN.Checked;
                saveInfo.AcqIndexHasSN = (int)nudAcqIndexHasSN.Value;
            }
        }

        #endregion

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (fbdSelectPath.ShowDialog() == DialogResult.OK)
            {
                tbSavePath.Text = fbdSelectPath.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 删除
            ImageDeleteInfoVO deleteInfo = new ImageDeleteInfoVO();
            if (rdoNoDelete.Checked)
                deleteInfo.DeleteMode = ImageDeleteMode.NoDelete;
            else if (rdoCycleDelete.Checked)
                deleteInfo.DeleteMode = ImageDeleteMode.Cycle;
            else
                deleteInfo.DeleteMode = ImageDeleteMode.DefiniteTime;

            deleteInfo.CycleDelete = new CycleDeleteInfo
            {
                CycleMin = (int)nudCycleMin.Value,
                Condition = rdoDiskUsageExceedsSize.Checked ? ImageDeleteCondition.DiskUsageExceeds : ImageDeleteCondition.DiskFreeLess,
                CriticalValue = rdoDiskUsageExceedsSize.Checked ? (int)nudDiskUsageExceedsSize.Value : (int)nudDiskFreeLessSize.Value,
                DeleteSize = (int)nudCycleDeleteSize.Value
            };
            deleteInfo.DefiniteTimeDelete = new DefiniteTimeDeleteInfo
            {
                StartHour = (int)nudStartTimeHour.Value,
                StartMin = (int)nudStartTimeMin.Value,
                StopHour = (int)nudStopTimeHour.Value,
                StopMin = (int)nudStopTimeMin.Value,
                RetentionDays = (int)nudRetentionDays.Value
            };

            #endregion

            imageSaveRepo.LowestPriority = ckbLowestPriority.Checked;
            imageSaveRepo.ScanTime = (int)nudScanTime.Value;
            imageSaveRepo.MinSpace = (int)nudMinSpace.Value;
            imageSaveRepo.CacheNum = (int)nudCacheNum.Value;

            #region 路径

            imageSaveRepo.Root = tbSavePath.Text;

            imageSaveRepo.SubDirectory = SubDireOfSaveImage.Date;
            if (ckbSubDireByTask.Checked)
                imageSaveRepo.SubDirectory |= SubDireOfSaveImage.Task;
            if (ckbSubDireBySN.Checked)
                imageSaveRepo.SubDirectory |= SubDireOfSaveImage.SN;
            if (ckbSubDireByFlag.Checked)
                imageSaveRepo.SubDirectory |= SubDireOfSaveImage.Flag;
            if (ckbSubDireBySource.Checked)
                imageSaveRepo.SubDirectory |= SubDireOfSaveImage.Source;

            #endregion

            imageSaveRepo.SetInfo((List<ImageSaveInfoVO>)cmbVTasks.DataSource, deleteInfo);

            imageSaveRepo.Save();
        }

    }
}
