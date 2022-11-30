using Autofac;
using CameraSDK;
using CameraSDK.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmCameraSDKLog : Form
    {
        public FrmCameraSDKLog()
        {
            InitializeComponent();
        }

        Dictionary<CameraBrand, ICameraSDKLog> dict = new Dictionary<CameraBrand, ICameraSDKLog>();

        private void FrmCameraSDKLog_Load(object sender, EventArgs e)
        {
            foreach (Enum @enum in Enum.GetValues(typeof(CameraBrand)))
            {
                cmbCameraBrand.Items.Add(@enum);
            }
        }

        private void btnCopyLog_Click(object sender, EventArgs e)
        {
            if (cmbCameraBrand.SelectedIndex == -1)
            {
                MessageBoxUtils.ShowWarn("请选择相机品牌");
                cmbCameraBrand.Focus();
                return;
            }

            CameraBrand brand = (CameraBrand)cmbCameraBrand.SelectedItem;

            ICameraSDKLog sdkLog = GetCameraSDKLog(brand);
            sdkLog.CopyLog();
        }

        ICameraSDKLog GetCameraSDKLog(CameraBrand brand)
        {
            if (dict.ContainsKey(brand))
            {
                return dict[brand];
            }
            else
            {
                ICameraSDKLog sdkLog = AutoFacContainer.Resolve<ICameraSDKLog>(new NamedParameter("brand", brand));
                dict[brand] = sdkLog;

                return sdkLog;
            }
        }

        private void rdoOpen_Click(object sender, EventArgs e)
        {
            if (cmbCameraBrand.SelectedIndex == -1)
            {
                MessageBoxUtils.ShowWarn("请选择相机品牌");
                cmbCameraBrand.Focus();
                return;
            }

            CameraBrand brand = (CameraBrand)cmbCameraBrand.SelectedItem;

            ICameraSDKLog sdkLog = GetCameraSDKLog(brand);

            if (brand == CameraBrand.DaHua)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                    sdkLog.ConfigFile = ofd.FileName;
                else
                    return;
            }

            sdkLog.OpenDebugLevel();
        }

        private void rdoClose_Click(object sender, EventArgs e)
        {
            if (cmbCameraBrand.SelectedIndex == -1)
            {
                MessageBoxUtils.ShowWarn("请选择相机品牌");
                cmbCameraBrand.Focus();
                return;
            }

            CameraBrand brand = (CameraBrand)cmbCameraBrand.SelectedItem;

            ICameraSDKLog sdkLog = GetCameraSDKLog(brand);

            if (brand == CameraBrand.DaHua)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                    sdkLog.ConfigFile = ofd.FileName;
                else
                    return;
            }

            sdkLog.CloseLog();
        }


    }
}
