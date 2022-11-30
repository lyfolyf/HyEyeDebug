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



namespace HalconSDK.Engine.UI
{
    public partial class HyPointsInfo : UserControl
    {

        private HyPoints srcPoints;
        private HalconEngineUI_2 HalconEngineViewer;
        private bool Editing = false;

        public HyPointsInfo(HalconEngineUI_2 HalconEngineViewer, HyPoints InputHyPoints)
        {
            srcPoints = InputHyPoints;
            this.HalconEngineViewer = HalconEngineViewer;
            InitializeComponent();
            DisplayROIdata(srcPoints);
        }

        private void nudPointX_ValueChanged(object sender, EventArgs e)
        {
            TreeNode Tn = tvwRoiPointsList.SelectedNode;
            if (Tn != null && Editing == false)
            {
                int index = tvwRoiPointsList.Nodes.IndexOf(Tn);
                srcPoints.RoiPoints[index] = new PointF((float)nudPointX.Value, srcPoints.RoiPoints[index].Y);
               
                HalconEngineViewer.EditSrcRoidataToDisplay(srcPoints);
                tvwRoiPointsList.Focus();
            }
        }

        private void nudPointY_ValueChanged(object sender, EventArgs e)
        {
            TreeNode Tn = tvwRoiPointsList.SelectedNode;
            if (Tn != null && Editing == false)
            {
                int index = tvwRoiPointsList.Nodes.IndexOf(Tn);
                srcPoints.RoiPoints[index] = new PointF(srcPoints.RoiPoints[index].X, (float)nudPointY.Value);
                
                HalconEngineViewer.EditSrcRoidataToDisplay(srcPoints);
                tvwRoiPointsList.Focus();
            }
        }

        private void tvwRoiPointsList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Editing = true;
            TreeNode Tn = e.Node;

            if (Tn != null)
            {
                int index = tvwRoiPointsList.Nodes.IndexOf(Tn);
                nudPointX.Value = (decimal)srcPoints.RoiPoints[index].X;
                nudPointY.Value = (decimal)srcPoints.RoiPoints[index].Y;
            }
            Editing = false;
        }

        public void DisplayROIdata(HyPoints InputHyPoints)
        {
            srcPoints = InputHyPoints;
            tvwRoiPointsList.BeginUpdate();
            tvwRoiPointsList.Nodes.Clear();
            string Index = "";

            for (int i = 0; i < srcPoints.RoiPoints.Count; i++)
            {
                if (i <= 8)
                {
                    Index = $"0{i + 1}.";
                }
                else
                {
                    Index = $"{i + 1}.";
                }

                tvwRoiPointsList.Nodes.Add($"{Index}Point{i + 1}");
            }
            if (tvwRoiPointsList.Nodes.Count > 0)
            {
                tvwRoiPointsList.SelectedNode = tvwRoiPointsList.Nodes[0];
                nudPointX.Value = (decimal)srcPoints.RoiPoints[0].X;
                nudPointY.Value = (decimal)srcPoints.RoiPoints[0].Y;
            }

            tvwRoiPointsList.EndUpdate();

        }


    }
}
