using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyVision.Tools;
using HalconSDK.DataReport.BL;
using GL.Kit.Log;
using HyVision.Tools.TerminalCollection;
using HyVision.Models;

namespace HalconSDK.DataReport.UI
{
    //public partial class HalconDataReportGeneraterUI : UserControl
    public partial class HalconDataReportGeneraterUI : BaseHyUserToolEdit<HalconDataReportGeneraterBL>
    {
        HalconDataReportGeneraterBL generaterBL;
        readonly IGLog log = Autofac.AutoFacContainer.Resolve<LogPublisher>();

        public HalconDataReportGeneraterUI()
        {
            InitializeComponent();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (generaterBL == null)
                    generaterBL = new HalconDataReportGeneraterBL();

                generaterBL.IsGeneratExcel = cboGeneratExcel.Checked;
                generaterBL.IsGeneratFeatureMap = cboGeneratFeatureMap.Checked;
                generaterBL.IsGeneratMidImage = cboGeneratMidImage.Checked;
                generaterBL.IsGeneratScoreTxt = cboGeneratScoreTxt.Checked;
                generaterBL.ReportFolderPath = txtGeneratReportFolderPath.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HalconDataReportGeneraterBL Subject
        {
            get { return generaterBL; }
            set
            {
                if (!object.Equals(generaterBL, value))
                {
                    generaterBL = value;
                    hyTerminalCollectionEditInput.Subject = generaterBL.Inputs;

                    cboGeneratScoreTxt.Checked = generaterBL.IsGeneratScoreTxt;
                    cboGeneratMidImage.Checked = generaterBL.IsGeneratMidImage;
                    cboGeneratFeatureMap.Checked = generaterBL.IsGeneratFeatureMap;
                    cboGeneratExcel.Checked = generaterBL.IsGeneratExcel;
                    txtGeneratReportFolderPath.Text = generaterBL.ReportFolderPath;
                }
            }
        }

        private void cboGeneratMidImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(cboGeneratMidImage.Checked)
                {
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_MASK, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_GRADX, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_GRADY, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CLOUDZ, typeof(HyImage));
                }
                else
                {
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_MASK);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_GRADX);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_GRADY);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CLOUDZ);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboGeneratFeatureMap_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(cboGeneratFeatureMap.Checked)
                {
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_ROW_SLOPE, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COL_SLOPE, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COLORZ, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_COL, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_ROW, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MAX, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MIN, typeof(HyImage));
                }
                else
                {
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_ROW_SLOPE);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COL_SLOPE);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COLORZ);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_COL);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_ROW);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MAX);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MIN);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboGeneratScoreTxt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(cboGeneratScoreTxt.Checked)
                {
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_TXT, typeof(string));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_HEAD, typeof(string));
                }
                else
                {
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_TXT);
                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_HEAD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboGeneratExcel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboGeneratExcel.Checked)
                {
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_ROW_SLOPE, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COL_SLOPE, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COLORZ, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_COL, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_ROW, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MAX, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MIN, typeof(HyImage));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_TXT, typeof(string));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_HEAD, typeof(string));
                    hyTerminalCollectionEditInput.AddTerminal(HalconDataReportGeneraterBL.INPUT_NAME_ALGORITHM_PRO_START_TIME, typeof(string));
                }
                else
                {
                    if(!cboGeneratFeatureMap.Checked)
                    {
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_ROW_SLOPE);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COL_SLOPE);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_COLORZ);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_COL);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_ROW);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MAX);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_CURVATURE_MIN);
                    }

                    if(!cboGeneratScoreTxt.Checked)
                    {
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_TXT);
                        hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_SCORE_HEAD);
                    }

                    hyTerminalCollectionEditInput.RemoveTerminal(HalconDataReportGeneraterBL.INPUT_NAME_ALGORITHM_PRO_START_TIME);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtGeneratReportFolderPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
