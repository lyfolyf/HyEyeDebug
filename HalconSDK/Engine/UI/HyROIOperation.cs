using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyVision.Tools.ImageDisplay;
using HalconSDK.Engine.BL;




namespace HalconSDK.Engine.UI
{
    public partial class HyROIOperation : UserControl
    {
  
        ROICollectionsBL uIInfo;



        //public string Description { get; set; }


        public HyROIOperation( ROICollectionsBL uIInfo)
        {
            this.uIInfo = uIInfo;
            InitializeComponent();

            InitializeWindow();
        }

        private void InitializeWindow()
        {
            gbIndexName.Text = $"序号{uIInfo.Index}";
            lbUnion1.Text = $"并集{uIInfo.Index * 2 - 1}";
            lbUnion2.Text = $"并集{uIInfo.Index * 2}";
            lbResult.Text = $"Result{uIInfo.Index}";
            cbOperation.Text = uIInfo.ROIOperation;

            //清除和重新加载两个集合列表数据
            lbxCollection1.Items.Clear();
            lbxCollection2.Items.Clear();
            foreach (string RoiName in uIInfo.UnionCollection1)
            {
                lbxCollection1.Items.Add(RoiName);
            }
            foreach (string RoiName in uIInfo.UnionCollection2)
            {
                lbxCollection2.Items.Add(RoiName);
            }
        }

        private void HyROIOperation_Load(object sender, EventArgs e)
        {
            lbxCollection1.GotFocus += LbROIList1_GotFocus;
            lbxCollection2.GotFocus += LbROIList1_GotFocus;

        }

        private void LbROIList1_GotFocus(object sender, EventArgs e)
        {
            //ListBox listBox = (ListBox)sender;
            //if (listBox.Name == "lbxROIList1")
            //{
            //    halconProgramEngineBL.OnEventSelectedListbox(listBox.Name, uIInfo.HyRois1);
            //}
            //else if (listBox.Name == "lbxROIList2")
            //{
            //    halconProgramEngineBL.OnEventSelectedListbox(listBox.Name, uIInfo.HyRois2);
            //}
        }



        private void cbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            uIInfo.ROIOperation = cbOperation.Text;
        }

        public void AddROI(string CollectionName, string ROIName)
        {
            if (CollectionName == lbUnion1.Text)
            {
                if (uIInfo.UnionCollection1.Contains(ROIName))
                {
                    MessageBox.Show($"{CollectionName} 已经包含 {ROIName} ,请选择添加其他ROI。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

              
                lbxCollection1.Items.Add(ROIName);
                uIInfo.UnionCollection1.Add(ROIName);
            }
            else if (CollectionName == lbUnion2.Text)
            {
                if (uIInfo.UnionCollection2.Contains(ROIName))
                {
                    MessageBox.Show($"{CollectionName} 已经包含 {ROIName} ,请选择添加其他ROI。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                lbxCollection2.Items.Add(ROIName);
                uIInfo.UnionCollection2.Add(ROIName);
            }
        }

        public void DeleteROI(string ROIName)
        {
            if (uIInfo.UnionCollection1.Contains(ROIName))
            {
                lbxCollection1.Items.Remove(ROIName);
                uIInfo.UnionCollection1.Remove(ROIName);
            }

            if (uIInfo.UnionCollection2.Contains(ROIName))
            {
                lbxCollection2.Items.Remove(ROIName);
                uIInfo.UnionCollection2.Remove(ROIName);
            }
        }

        private void tsmiDeleteROI1_Click(object sender, EventArgs e)
        {
            if (lbxCollection1.SelectedIndex != -1)
            {
                uIInfo.UnionCollection1.Remove(lbxCollection1.SelectedItem.ToString());
                lbxCollection1.Items.Remove(lbxCollection1.SelectedItem.ToString());            
            }
        }

        private void stmiDeleteROI2_Click(object sender, EventArgs e)
        {
            if (lbxCollection2.SelectedIndex != -1)
            {
                uIInfo.UnionCollection2.Remove(lbxCollection2.SelectedItem.ToString());
                lbxCollection2.Items.Remove(lbxCollection2.SelectedItem.ToString());
            }
        }
    }
}
