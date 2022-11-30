using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using GL.Kit.Log;
using HyVision.Tools;

namespace HyEye.AI
{
    // public partial class HyAIDetEngineUI : UserControl
    public partial class HyAIDetEngineUI : BaseHyUserToolEdit<HyAIDetEngineBL>
    {
        HyAIDetEngineBL aiEngine;
        private LogPublisher log;

        public HyAIDetEngineUI()
        {
            InitializeComponent();
            log = Autofac.AutoFacContainer.Resolve<LogPublisher>();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (aiEngine == null)
                    aiEngine = new HyAIDetEngineBL();

                aiEngine.DetInferenceCfgPath = txtDetCfgFilePath.Text;
                aiEngine.DetInferenceDraw = cboDetInferenceDraw.Checked;
                if (!string.IsNullOrEmpty(txtDetScoreThreshold.Text))
                    aiEngine.DetScoreThreshold = float.Parse(txtDetScoreThreshold.Text);
                if (!string.IsNullOrEmpty(txtDetOptInputSizeH.Text))
                    aiEngine.DetOptInputSizeH = int.Parse(txtDetOptInputSizeH.Text);
                if (!string.IsNullOrEmpty(txtDetOptInputSizeW.Text))
                    aiEngine.DetOptInputSizeW = int.Parse(txtDetOptInputSizeW.Text);
                if (!string.IsNullOrEmpty(txtDetInputMaxH.Text))
                    aiEngine.DetInputMaxH = int.Parse(txtDetInputMaxH.Text);
                if (!string.IsNullOrEmpty(txtDetInputMaxW.Text))
                    aiEngine.DetInputMaxW = int.Parse(txtDetInputMaxW.Text);
                if (!string.IsNullOrEmpty(txtDetEngineBatch.Text))
                    aiEngine.DetEngineBatch = int.Parse(txtDetEngineBatch.Text);
                if (!string.IsNullOrEmpty(txtDetMaxDetections.Text))
                    aiEngine.DetMaxDetections = int.Parse(txtDetMaxDetections.Text);
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
        public override HyAIDetEngineBL Subject
        {
            get { return aiEngine; }
            set
            {
                if (!object.Equals(aiEngine, value))
                {
                    aiEngine = value;
                    hyTerminalCollInput.Subject = aiEngine.Inputs;
                    hyTerminalCollOutput.Subject = aiEngine.Outputs;

                    txtDetCfgFilePath.Text = aiEngine.DetInferenceCfgPath;
                    cboDetInferenceDraw.Checked = aiEngine.DetInferenceDraw;
                    txtDetScoreThreshold.Text = aiEngine.DetScoreThreshold.ToString();
                    txtDetOptInputSizeH.Text = aiEngine.DetOptInputSizeH.ToString();
                    txtDetOptInputSizeW.Text = aiEngine.DetOptInputSizeW.ToString();
                    txtDetInputMaxH.Text = aiEngine.DetInputMaxH.ToString();
                    txtDetInputMaxW.Text = aiEngine.DetInputMaxW.ToString();
                    txtDetEngineBatch.Text = aiEngine.DetEngineBatch.ToString();
                    txtDetMaxDetections.Text = aiEngine.DetMaxDetections.ToString();
                }
            }
        }

        private void txtDetScoreThreshold_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDetOptInputSizeH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtDetOptInputSizeW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtDetInputMaxH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtDetInputMaxW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtDetEngineBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void txtDetMaxDetections_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;//消除不合适字符  
            }
        }

        private void btnDetSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtDetCfgFilePath.Text))
                    folderBrowserDialog.SelectedPath = txtDetCfgFilePath.Text;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDetCfgFilePath.Text = folderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
