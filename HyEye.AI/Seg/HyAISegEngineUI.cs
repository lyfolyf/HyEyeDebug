using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using GL.Kit.Log;
using HyVision.Tools;
using HyVision.Models;

namespace HyEye.AI
{
    // public partial class HyAISegEngineUI : UserControl
    public partial class HyAISegEngineUI : BaseHyUserToolEdit<HyAISegEngineBL>
    {
        HyAISegEngineBL aiEngine;
        private LogPublisher log;

        public HyAISegEngineUI()
        {
            InitializeComponent();
            log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (aiEngine == null)
                    aiEngine = new HyAISegEngineBL();

                aiEngine.SegInferenceCfgPath = txtSegInferenceCfgPath.Text;
                aiEngine.SegInferenceDraw = cbxSegInferenceDraw.Checked;
                aiEngine.SegIsPatch = cbxSegIsPatch.Checked;
                if (!string.IsNullOrEmpty(txtSegBatchMax.Text))
                    aiEngine.SegBatchMax = int.Parse(txtSegBatchMax.Text);
                if (!string.IsNullOrEmpty(txtSegOptBatch.Text))
                    aiEngine.SegOptBatch = int.Parse(txtSegOptBatch.Text);
                if (!string.IsNullOrEmpty(txtSegBatchPatchSplit.Text))
                    aiEngine.SegBatchPatchSplit = int.Parse(txtSegBatchPatchSplit.Text);

            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }

        public override void Save()
        {
            try
            {
                UpdateDataToObject();
            }
            catch (Exception ex)
            {
                log?.Error(ex.Message);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HyAISegEngineBL Subject
        {
            get { return aiEngine; }
            set
            {
                if (!object.Equals(aiEngine, value))
                {
                    aiEngine = value;
                    hyTerminalCollInput.Subject = aiEngine.Inputs;
                    hyTerminalCollOutput.Subject = aiEngine.Outputs;

                    txtSegInferenceCfgPath.Text = aiEngine.SegInferenceCfgPath;
                    cbxSegInferenceDraw.Checked = aiEngine.SegInferenceDraw;
                    cbxSegIsPatch.Checked = aiEngine.SegIsPatch = true;
                    txtSegBatchMax.Text = aiEngine.SegBatchMax.ToString();
                    txtSegOptBatch.Text = aiEngine.SegOptBatch.ToString();
                    txtSegBatchPatchSplit.Text = aiEngine.SegBatchPatchSplit.ToString();

                    // Added by louis on Mar. 11 2022 MacBookAOI 定制化需求
                    HyTerminal cameraLocation = new HyTerminal(HyAIConst.CAM_LOC, typeof(string));
                    cameraLocation.GUID = Guid.NewGuid().ToString("N");
                    if (!aiEngine.Inputs.Contains(HyAIConst.CAM_LOC))
                        aiEngine.Inputs.Add(cameraLocation);

                    HyTerminal fovLocation = new HyTerminal(HyAIConst.FOV_LOC, typeof(string));
                    fovLocation.GUID = Guid.NewGuid().ToString("N");
                    if (!aiEngine.Inputs.Contains(HyAIConst.FOV_LOC))
                        aiEngine.Inputs.Add(fovLocation);
                }
            }
        }

        private void txtSegPadding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtSegBatchMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtSegOptBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtSegBatchPatchSplit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtSegBatchSplitThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                if (e.KeyChar != '.' || ((TextBox)sender).Text.Length == 0)//小数点  
                {
                    e.Handled = true;
                }
                if (((TextBox)sender).Text.LastIndexOf('.') != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSegSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtSegInferenceCfgPath.Text))
                    folderBrowserDialog.SelectedPath = txtSegInferenceCfgPath.Text;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtSegInferenceCfgPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
