using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyVision.Tools.PhaseDeflection.BL;
using HyVision.Tools.XMLEdit.BL;
using System.IO;
using LeadHawkCS;

namespace HyVision.Tools.XMLEdit.DA
{
    public partial class XMLEditUI : BaseHyUserToolEdit<XMLEditBL>
    {
        private XMLEditBL xMLEditBL;
        public XMLEditUI()
        {
            InitializeComponent();
        }

        public override void UpdateDataToObject()
        {
            try
            {
                if (xMLEditBL == null)
                    xMLEditBL = new XMLEditBL();

                if(txtFilePath.Text != null && File.Exists(txtFilePath.Text))
                    xMLEditBL.FilePath = txtFilePath.Text;

                if (propertyGrid1.SelectedObject != null && propertyGrid1.SelectedObject.GetType() == typeof(XD_PhaseDeflectionPara))
                    xMLEditBL.XmlData = (XD_PhaseDeflectionPara)propertyGrid1.SelectedObject;
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
                if (txtFilePath.Text == null || !File.Exists(txtFilePath.Text))
                    throw new Exception($"请选择一个要保存的XML文件！");

                UpdateDataToObject();

                xMLEditBL.SaveXMLData(txtFilePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override XMLEditBL Subject
        {
            get { return xMLEditBL; }
            set
            {
                if (!object.Equals(xMLEditBL, value))
                {
                    xMLEditBL = value;

                    if(xMLEditBL != null && xMLEditBL.XmlData != null)
                        propertyGrid1.SelectedObject = xMLEditBL.XmlData;

                    if (xMLEditBL != null && xMLEditBL.FilePath != null && File.Exists(xMLEditBL.FilePath))
                        txtFilePath.Text = xMLEditBL.FilePath;
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    string folderPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf(@"\"));

                    int returnVal = xMLEditBL.ReadXMLData(folderPath);
                    if (returnVal == 1)
                    {
                        MessageBox.Show("标准镜面数据读取失败！");
                    }
                    else if (returnVal == 2)
                    {
                        MessageBox.Show("读取参数失败！");
                    }
                    else
                        propertyGrid1.SelectedObject = xMLEditBL.XmlData;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
