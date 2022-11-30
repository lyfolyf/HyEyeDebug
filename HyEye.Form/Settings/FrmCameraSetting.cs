using AutoMapper;
using WeifenLuo.WinFormsUI.Docking;
using CameraSDK;
using CameraSDK.HIK;
using CameraSDK.Models;
using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HyDisplayWindow;

namespace HyEye.WForm.Settings
{
    public partial class FrmCameraSetting : DockContentEx
    {
        readonly ICameraRepository cameraRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        public FrmCameraSetting(
            ICameraRepository cameraRepo,
            IMapper mapper,
            IGLog log,
            ICameraFactoryCollection cameraFactory)
        {
            InitializeComponent();

            tbExposure.AddEvent_KeyPress_InputNumberAndPoint();
            tbGain.AddEvent_KeyPress_InputNumberAndPoint();
            tbImageWidth.AddEvent_KeyPress_InputNumber();
            tbImageHeight.AddEvent_KeyPress_InputNumber();
            tbDelay.AddEvent_KeyPress_InputNumberAndPoint();

            tbIP.AddEvent_KeyPress_InputNumberAndPoint();
            tbSubnetMask.AddEvent_KeyPress_InputNumberAndPoint();
            tbDefaultGateway.AddEvent_KeyPress_InputNumberAndPoint();

            colNum.Width = 50;
            colCameraBrand.Width = 75;
            colCameraSN.Width = 75;
            colCameraUserID.Width = 75;
            colTriggerMode.Width = 75;

            this.cameraRepo = cameraRepo;
            this.mapper = mapper;
            this.log = log;
            this.m_cameraFactory = cameraFactory;

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            dgvCameraInfos.AllowUserToResizeRows = false;


            displayControl = new HyImageViewer();
            displayControl.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(displayControl);
        }

        readonly ICameraFactoryCollection m_cameraFactory;
        ICamera m_camera;


        //新版图像显示控件替换旧版     Heweile 2022/3/30
        private HyImageViewer displayControl;
        EnumCombobox ecmbPreampGain;

        private void FrmCameraSetting_Load(object sender, EventArgs e)
        {
            IEnumerable<CameraBrand> cameraBrands = cameraRepo.GetCameraBrand();

            if (cameraBrands != null && cameraBrands.Count() > 0)
            {
                foreach (CameraBrand brand in cameraBrands)
                {
                    if (brand == CameraBrand.HIK_Gige)
                        ckbHIK_GigE.Checked = true;
                    else if (brand == CameraBrand.HIK_GenTL)
                        ckbHIK_GenTL.Checked = true;
                    else if (brand == CameraBrand.Basler)
                        ckbBasler.Checked = true;
                    else if (brand == CameraBrand.FLIR)
                        ckbFLIR.Checked = true;
                    else if (brand == CameraBrand.DaHua)
                        ckbDahua.Checked = true;
                    else if (brand == CameraBrand.Avt)
                        ckbAvt.Checked = true;
                    else if (brand == CameraBrand.MindVision)
                        ckbMDV.Checked = true;
                }
            }

            tbCtiPath.Text = cameraRepo.CtiPath;

            List<CameraInfoVO> cameras = cameraRepo.GetCameras();

            loadCameras(cameras.ToList());

            loadCameraParams();
        }

        void loadCameras(List<CameraInfoVO> cameras)
        {
            dgvCameraInfos.Rows.Clear();

            int i = 1;
            foreach (CameraInfoVO camera in cameras)
            {
                int rowIndex = dgvCameraInfos.Rows.Add(i++, camera.Brand.ToAlias(), camera.SN, camera.UserDefinedName, camera.SoftTrigger ? "软触发" : "硬触发", camera.ImageCacheCount.ToString());
                dgvCameraInfos.Rows[rowIndex].Tag = camera;
            }

            if (cameras.Count > 0)
                dgvCameraInfos.CurrentCell = dgvCameraInfos.Rows[0].Cells[1];
        }

        void loadCameraParams()
        {
            ecmbPreampGain = new EnumCombobox(cmbPreampGain, typeof(PreampGainEnum));
        }

        OpenFileDialog openFile;
        private void btnSelectCti_Click(object sender, EventArgs e)
        {
            if (openFile == null)
            {
                openFile = new OpenFileDialog
                {
                    Filter = "DCF文件(*.cti)|*.cti"
                };
            }

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                tbCtiPath.Text = openFile.FileName;
                m_cameraFactory.SetCtiPath(tbCtiPath.Text);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<CameraBrand> cameraBrands = getSelectedCameraBrand();

            if (cameraBrands.Count == 0)
            {
                MessageBoxUtils.ShowWarn("请选择相机品牌");
                return;
            }

            m_cameraFactory.SetCameraBrands(cameraBrands);

            List<ComCameraInfo> comCameraInfos = m_cameraFactory.SearchDevice();

            List<CameraInfoVO> cameraInfos = mapper.Map<List<CameraInfoVO>>(comCameraInfos);

            List<CameraInfoVO> oldCameras = cameraRepo.GetCameras();

            // 将之前设置的触发模式复制过来
            cameraInfos.ForEach(a =>
            {
                a.SoftTrigger = oldCameras.FirstOrDefault(b => b.SN == a.SN)?.SoftTrigger ?? true;
                a.ImageCacheCount = oldCameras.FirstOrDefault(b => b.SN == a.SN)?.ImageCacheCount ?? 1;
            });

            cameraRepo.SetCameraInfo(cameraInfos);

            loadCameras(cameraInfos);

            if (!cameraRepo.CheckSingle())
            {
                MessageBoxUtils.ShowWarn("搜索到的相机信息中有重复序列号，请检查！！！");
            }
        }

        List<CameraBrand> getSelectedCameraBrand()
        {
            List<CameraBrand> cameraBrands = new List<CameraBrand>();

            if (ckbHIK_GigE.Checked)
                cameraBrands.Add(CameraBrand.HIK_Gige);
            if (ckbHIK_GenTL.Checked)
                cameraBrands.Add(CameraBrand.HIK_GenTL);
            if (ckbBasler.Checked)
                cameraBrands.Add(CameraBrand.Basler);
            if (ckbFLIR.Checked)
                cameraBrands.Add(CameraBrand.FLIR);
            if (ckbDahua.Checked)
                cameraBrands.Add(CameraBrand.DaHua);
            if (ckbAvt.Checked)
                cameraBrands.Add(CameraBrand.Avt);
            if (ckbMDV.Checked)
                cameraBrands.Add(CameraBrand.MindVision);

            return cameraBrands;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!cameraRepo.CheckSingle())
            {
                MessageBoxUtils.ShowWarn("搜索到的相机信息中有重复序列号，请检查！！！");
                return;
            }

            Save();
        }

        private void tbSetIP_Click(object sender, EventArgs e)
        {
            if (!tbIP.CheckRegex(ComPattern.IpPattern, "非法的 IP 地址")
                || !tbSubnetMask.CheckRegex(ComPattern.IpPattern, "非法的子网掩码")
                || (tbDefaultGateway.Visible && !tbDefaultGateway.CheckRegex(ComPattern.IpPattern, "非法的默认网关")))
                return;

            CameraInfoVO cameraInfo = (CameraInfoVO)dgvCameraInfos.CurrentCell.OwningRow.Tag;
            ComCameraInfo comCameraInfo = mapper.Map<ComCameraInfo>(cameraInfo);

            ICamera camera = m_cameraFactory.GetCamera(comCameraInfo);
            if (camera == null)
            {
                log.Error("未找到相机");
                MessageBoxUtils.ShowError("未找到相机，请检查相机是否已连接");
                return;
            }
            if (camera.IsOpen)
            {
                MessageBoxUtils.ShowWarn("请先关闭相机");
                return;
            }
            if (camera.SetIP(tbIP.Text, tbSubnetMask.Text, tbDefaultGateway.Text))
            {
                cameraRepo.SetIP(cameraInfo.SN, tbIP.Text, tbSubnetMask.Text, tbDefaultGateway.Text);

                cameraInfo.IP = tbIP.Text;
                cameraInfo.SubnetMask = tbSubnetMask.Text;
                cameraInfo.DefaultGateway = tbDefaultGateway.Text;
            }
        }

        void ResetParams(string cameraSN)
        {
            List<CameraParamInfoVO> paramInfos = cameraRepo.GetParamInfos(cameraSN);
            if (paramInfos == null) return;

            foreach (CameraParamInfoVO paramInfo in paramInfos)
            {
                switch (paramInfo.Name)
                {
                    case CameraParamList.C_Gain:
                        tbGain.Visible = paramInfo.Enabled;
                        break;
                    case CameraParamList.C_PreampGain:
                        cmbPreampGain.Visible = paramInfo.Enabled;
                        break;
                }
            }
        }

        void Save()
        {
            List<CameraBrand> cameraBrands = getSelectedCameraBrand();

            cameraRepo.SetCameraBrand(cameraBrands);
            cameraRepo.CtiPath = tbCtiPath.Text;

            m_cameraFactory.SetCameraBrands(cameraBrands);
            m_cameraFactory.SetCtiPath(tbCtiPath.Text);

            cameraRepo.Save();
        }

        #region 相机操作

        int imgNum = 0;
        int seconds = 0;
        System.Timers.Timer timer;

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            seconds++;

            double frameRate = (double)imgNum / seconds;

            lblFrameRate.AsyncAction(c =>
            {
                c.Text = "帧率：" + frameRate.ToString("0.00");
            });
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgvCameraInfos.CurrentCell == null)
            {
                MessageBoxUtils.ShowWarn("请选择相机");
                return;
            }

            CameraInfoVO cameraInfo = (CameraInfoVO)dgvCameraInfos.CurrentCell.OwningRow.Tag;
            ComCameraInfo comCameraInfo = mapper.Map<ComCameraInfo>(cameraInfo);

            m_camera = m_cameraFactory.GetCamera(comCameraInfo);
            if (m_camera == null)
            {
                MessageBoxUtils.ShowError("未找到相机，请检查相机是否已连接");
                return;
            }

            if (m_camera.IsOpen)
            {
                MessageBoxUtils.ShowWarn("相机已打开");
                return;
            }

            m_camera.ImageReceived += Camera_ImageReceived;
            m_camera.Open();
            dgvCameraInfos.Columns[3].ReadOnly = false;

            btnSearch.Enabled = false;
            btnOpen.Enabled = false;
            btnClose.Enabled = true;

            rdoSoftTrigger.Enabled = true;
            rdoExternTrigger.Enabled = true;

            btnGetCameraParams.Enabled = true;
            btnSetCameraParams.Enabled = true;

            ResetParams(cameraInfo.SN);

            getCameraParams();
        }

        private void Camera_ImageReceived(object sender, CameraImageEventArgs e)
        {
            imgNum++;

            displayControl.DisplayImage(e.CameraImage.Bitmap);

            lblAcqTime.AsyncAction(c =>
            {
                c.Text = "序号：" + e.CameraImage.FrameNum.ToString();
            });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            closeCamera();

            btnSearch.Enabled = true;
            btnOpen.Enabled = true;
            btnClose.Enabled = false;

            rdoSoftTrigger.Enabled = false;
            rdoExternTrigger.Enabled = false;

            rdoSoftTrigger.Checked = false;
            rdoExternTrigger.Checked = false;
            if (btnContinuous.Text == "停止实时")
                btnContinuous.Text = "实时";

            btnContinuous.Enabled = false;
            btnTriggerExec.Enabled = false;

            btnGetCameraParams.Enabled = false;
            btnSetCameraParams.Enabled = false;

            if (dgvCameraInfos.Columns.Count > 0)
                dgvCameraInfos.Columns[2].ReadOnly = true;
        }

        void closeCamera()
        {
            if (m_camera != null)
            {
                if (m_camera.IsOpen)
                    m_camera.Close();
                m_camera.ImageReceived -= Camera_ImageReceived;
                m_camera = null;
            }
        }

        private void dgvCameraInfos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            CameraInfoVO cameraInfo = (CameraInfoVO)dgvCameraInfos.Rows[e.RowIndex].Tag;

            if (e.ColumnIndex == 3)
            {
                ComCameraInfo comCameraInfo = mapper.Map<ComCameraInfo>(cameraInfo);

                ICamera camera = m_cameraFactory.GetCamera(comCameraInfo);

                string newUserID = dgvCameraInfos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (cameraRepo.CameraNameExists(newUserID))
                {
                    MessageBoxUtils.ShowWarn("相机名称已存在");
                }
                else
                {
                    if (camera.Rename(newUserID))
                    {
                        cameraRepo.RenameCamera(cameraInfo.SN, newUserID);
                    }
                }
            }
            else if (e.ColumnIndex == 4)
            {
                bool softTrigger = dgvCameraInfos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "软触发";

                cameraRepo.SetSoftTrigger(cameraInfo.SN, softTrigger);
            }
            else if (e.ColumnIndex == 5)
            {
                ComCameraInfo comCameraInfo = mapper.Map<ComCameraInfo>(cameraInfo);

                ICamera camera = m_cameraFactory.GetCamera(comCameraInfo);

                int cacheCount = Convert.ToInt32(dgvCameraInfos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (camera.SetCache(cacheCount))
                {
                    cameraRepo.SetImageCacheCount(cameraInfo.SN, cacheCount);
                }
            }
        }

        private void dgvCameraInfos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 3)
            {
                if (m_camera == null || !m_camera.IsOpen)
                {
                    MessageBoxUtils.ShowInfo("必须先打开相机才可以给相机改名");
                }
            }
        }

        private void dgvCameraInfos_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvCameraInfos.CurrentCell == null) return;

            CameraInfoVO cameraInfo = dgvCameraInfos.CurrentCell.OwningRow.Tag as CameraInfoVO;
            if (cameraInfo == null) return;

            tbIP.Text = cameraInfo.IP;
            tbSubnetMask.Text = cameraInfo.SubnetMask;
            tbDefaultGateway.Text = cameraInfo.DefaultGateway;

            //gbSetIP.Enabled = !string.IsNullOrEmpty(cameraInfo.IP);
            gbSetIP.Enabled = cameraInfo.ConnectionType == ConnectionType.GigE;

            tbExposure.Text = cameraInfo.Params.ExposureTime?.ToString();
            tbGain.Text = cameraInfo.Params.Gain?.ToString();
            tbImageWidth.Text = cameraInfo.Params.ImageWidth?.ToString();
            tbImageHeight.Text = cameraInfo.Params.ImageHeight?.ToString();
            tbDelay.Text = cameraInfo.Params.TriggerDelay?.ToString();

            //ResetParams(cameraInfo.SN);
        }

        private void rdoSoftTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSoftTrigger.Checked)
            {
                if (m_camera == null)
                {
                    MessageBoxUtils.ShowWarn("请选择相机");
                    return;
                }

                btnContinuous.Enabled = true;
                btnTriggerExec.Enabled = true;

                m_camera.Start(TriggerMode.Trigger, TriggerSource.Software);
            }
        }

        private void rdoExternTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExternTrigger.Checked)
            {
                if (m_camera == null)
                {
                    MessageBoxUtils.ShowWarn("请选择相机");
                    return;
                }

                btnContinuous.Enabled = false;
                btnTriggerExec.Enabled = false;

                if (btnContinuous.Text == "停止实时")
                    btnContinuous.Text = "实时";

                m_camera.Start(TriggerMode.Trigger, TriggerSource.Extern);
            }
        }

        private void btnContinuous_Click(object sender, EventArgs e)
        {
            if (btnContinuous.Text == "实时")
            {
                btnContinuous.Text = "停止实时";
                btnTriggerExec.Enabled = false;
                lblFrameRate.Visible = true;

                m_camera.SetTriggerMode(TriggerMode.Continuous);

                imgNum = 0;
                seconds = 0;
                timer.Start();
            }
            else
            {
                btnContinuous.Text = "实时";
                btnTriggerExec.Enabled = true;
                lblFrameRate.Visible = false;

                m_camera.SetTriggerMode(TriggerMode.Trigger);

                timer.Stop();
            }
        }

        private void btnTriggerExec_Click(object sender, EventArgs e)
        {
            m_camera.SoftTrigger();
        }

        private void btnGetCameraParams_Click(object sender, EventArgs e)
        {
            getCameraParams();
        }

        void getCameraParams()
        {
            CameraParams param = m_camera.GetParams();

            tbExposure.Text = param.ExposureTime?.ToString();
            tbGain.Text = param.Gain?.ToString();
            ecmbPreampGain.SelectedItem = param.PreampGain;

            tbImageWidth.Text = param.ImageWidth?.ToString();
            tbImageHeight.Text = param.ImageHeight?.ToString();

            tbDelay.Text = param.TriggerDelay?.ToString();
        }

        private void btnSetCameraParams_Click(object sender, EventArgs e)
        {
            CameraParams @params = new CameraParams
            {
                ExposureTime = tbExposure.Text.Length > 0 ? float.Parse(tbExposure.Text) : (float?)null,
                Gain = tbGain.Text.Length > 0 ? float.Parse(tbGain.Text) : (float?)null,
                PreampGain = (PreampGainEnum?)ecmbPreampGain.SelectedItem,

                ImageWidth = tbImageWidth.Text.Length > 0 ? int.Parse(tbImageWidth.Text) : (int?)null,
                ImageHeight = tbImageHeight.Text.Length > 0 ? int.Parse(tbImageHeight.Text) : (int?)null,

                TriggerDelay = tbDelay.Text.Length > 0 ? float.Parse(tbDelay.Text) : (float?)null
            };

            m_camera.SetParams(@params);
        }

        #endregion

        private void FrmCameraSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeCamera();

            if (cameraRepo.ConfigChanged && MessageBoxUtils.ShowQuestion("是否保存配置？") == DialogResult.Yes)
            {
                Save();
            }
        }

        private void FrmCameraSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == false && btnClose.Enabled)
            {
                btnClose_Click(null, null);
            }
        }

        private void ckbHIK_GenTL_CheckedChanged(object sender, EventArgs e)
        {
            tbCtiPath.Enabled = ckbHIK_GenTL.Checked;
            btnSelectCti.Enabled = ckbHIK_GenTL.Checked;
        }

        private void tsmiDeleteCameraInfo_Click(object sender, EventArgs e)
        {
            if (dgvCameraInfos.CurrentCell == null) return;

            CameraInfoVO cameraInfo = (CameraInfoVO)dgvCameraInfos.CurrentRow.Tag;

            if (MessageBoxUtils.ShowQuestion("确定要删除当前相机信息吗？") == DialogResult.Yes)
            {
                cameraRepo.DeleteCameraInfo(cameraInfo);

                dgvCameraInfos.Rows.RemoveAt(dgvCameraInfos.CurrentRow.Index);

                int i = 1;
                foreach (DataGridViewRow row in dgvCameraInfos.Rows)
                {
                    row.Cells["colNum"].Value = i++;
                }
            }
        }
    }
}
