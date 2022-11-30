using System.Windows.Forms;
using System.Collections.Generic;
using HalconSDK.Engine.BL;
using HyVision.Tools.ImageDisplay;




namespace HalconSDK.Engine.UI
{
    public partial class FrmSelectROI : Form
    {

        private ParameterInfo ParamInfo;
        private List<BaseHyROI> AffineRoiData;

        public FrmSelectROI(List<BaseHyROI> AffineRoiData, ParameterInfo ParamInfo)
        {
            this.AffineRoiData = AffineRoiData;
            this.ParamInfo = ParamInfo;
            InitializeComponent();
        }

        private void FrmSelectROI_Load(object sender, System.EventArgs e)
        {
            UpdateRoiList(AffineRoiData);
            UpdataSelectRoiList();
            hyImageDisplayControl1.TopToolVisible = false;
            hyImageDisplayControl1.EditRoiEnable = false;
        }

        private void FrmSelectROI_FormClosing(object sender, FormClosingEventArgs e)
        {
            string Indexs = "";
            for (int i = 0; i < tvwSelectedRoi.Nodes.Count; i++)
            {
                BaseHyROI roi = tvwSelectedRoi.Nodes[i].Tag as BaseHyROI;
                Indexs += roi.Index;
                if (i < tvwSelectedRoi.Nodes.Count - 1)
                {
                    Indexs += ",";
                }
            }

            ParamInfo.SelectedRoiIndex = Indexs;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            TreeNode Tn = tvwRoiList.SelectedNode;
            if (Tn != null)
            {
                BaseHyROI roi = Tn.Tag as BaseHyROI;
                if (tvwSelectedRoi.Nodes.FirstOrDefault(false, n => n.Text.Contains(roi.Name)) == null)
                {
                    //TreeNode TnNew = tvwSelectedRoi.Nodes.Add($"{index}{roi.Name}.{roi.RoiType}");
                    TreeNode TnNew = tvwSelectedRoi.Nodes.Add(Tn.Text);
                    TnNew.ImageIndex = 1; Tn.SelectedImageIndex = 1;
                    TnNew.Tag = roi;
                }
                else
                {
                    MessageBox.Show($"ROI{ roi.Name }已经添加在已选列表，请添加其他ROI！", "ROI已添加", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tvwRoiList.Focus();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            TreeNode Tn = tvwSelectedRoi.SelectedNode;
            if (Tn != null)
            {
                int IndexNum1 = tvwSelectedRoi.Nodes.IndexOf(Tn);
                tvwSelectedRoi.Nodes.RemoveAt(IndexNum1);

                for (int i = IndexNum1; i < tvwSelectedRoi.Nodes.Count; i++)
                {
                    string str = tvwSelectedRoi.Nodes[i].Text;
                    tvwSelectedRoi.Nodes[i].Text = (i + 1).ToString().PadLeft(2, '0') + str.Substring(2, str.Length - 2);
                }
                tvwSelectedRoi.Focus();
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string Indexs = "";
            for (int i = 0; i < tvwSelectedRoi.Nodes.Count; i++)
            {
                BaseHyROI roi = tvwSelectedRoi.Nodes[i].Tag as BaseHyROI;
                Indexs += roi.Index;
                if (i < tvwSelectedRoi.Nodes.Count - 1)
                {
                    Indexs += ",";
                }
            }

            ParamInfo.SelectedRoiIndex = Indexs;
        }

        private void UpdateRoiList(List<BaseHyROI> lstRoiData)
        {
            tvwRoiList.Nodes.Clear();
            string Index = "";

            for (int i = 0; i < lstRoiData.Count; i++)
            {
                if (i <= 8)
                {
                    Index = $"0{i + 1}.";
                }
                else
                {
                    Index = $"{i + 1}.";
                }
                TreeNode Tn = tvwRoiList.Nodes.Add($"{Index}{lstRoiData[i].Name}.{lstRoiData[i].RoiType}");
                Tn.Tag = lstRoiData[i];
            }
        }

        private void UpdataSelectRoiList()
        {
            tvwSelectedRoi.Nodes.Clear();
            if (string.IsNullOrEmpty(ParamInfo.SelectedRoiIndex)) return;
            string[] RoiIndexs = ParamInfo.SelectedRoiIndex.Split(',');
            for (int i = 0; i < RoiIndexs.Length; i++)
            {
                int SelectIndex = int.Parse(RoiIndexs[i]);
                BaseHyROI roi = AffineRoiData.Find(r => r.Index == SelectIndex);

                if (roi != null)
                {
                    string Index = roi.Index.ToString().PadLeft(2,'0') + ".";
                    TreeNode Tn = tvwSelectedRoi.Nodes.Add($"{Index}{roi.Name}.{roi.RoiType}");
                    Tn.Tag = roi;
                }
            }
        }

        public void DisplayImage(string ImagePath)
        {
            if (!string.IsNullOrEmpty(ImagePath))
            {
                hyImageDisplayControl1.DisplayImage(ImagePath);
                hyImageDisplayControl1.SetHyROIs(AffineRoiData);
            }

        }

        private void tvwRoiList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Tn = tvwRoiList.SelectedNode;
            if (Tn != null)
            {
                BaseHyROI roi = Tn.Tag as BaseHyROI;
                hyImageDisplayControl1.GetHyROIRepository().SetSelected(false);
                roi.IsSelected = true;
                hyImageDisplayControl1.DisplayControlInvalidate();
            }
        }

        private void tvwSelectedRoi_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Tn = tvwSelectedRoi.SelectedNode;
            if (Tn != null)
            {
                BaseHyROI roi = Tn.Tag as BaseHyROI;
                hyImageDisplayControl1.GetHyROIRepository().SetSelected(false);
                roi.IsSelected = true;
                hyImageDisplayControl1.DisplayControlInvalidate();
            }
        }

        private void tvwRoiList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnAdd_Click(null, null);
        }
    }
}
