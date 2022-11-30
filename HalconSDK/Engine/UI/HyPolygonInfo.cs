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
    public partial class HyPolygonInfo : UserControl
    {

        private HyPolygon srcPolygon;
        private HalconEngineUI_2 HalconEngineViewer;
        private bool Editing = false;

        public HyPolygonInfo(HalconEngineUI_2 HalconEngineViewer, HyPolygon InputHyPolygon)
        {
            srcPolygon = InputHyPolygon;
            this.HalconEngineViewer = HalconEngineViewer;
            InitializeComponent();
            DisplayROIdata(srcPolygon);
        }

        private void nudPointX_ValueChanged(object sender, EventArgs e)
        {
            TreeNode Tn = tvwRoiPointsList.SelectedNode;
            if (Tn != null && Editing == false)
            {
                int index = tvwRoiPointsList.Nodes.IndexOf(Tn);
                srcPolygon.PolygonPoints[index] = new PointF((float)nudPointX.Value, srcPolygon.PolygonPoints[index].Y);
              
                HalconEngineViewer.EditSrcRoidataToDisplay(srcPolygon);
                tvwRoiPointsList.Focus();
            }
        }

        private void nudPointY_ValueChanged(object sender, EventArgs e)
        {
            TreeNode Tn = tvwRoiPointsList.SelectedNode;
            if (Tn != null && Editing == false)
            {
                int index = tvwRoiPointsList.Nodes.IndexOf(Tn);
                srcPolygon.PolygonPoints[index] = new PointF(srcPolygon.PolygonPoints[index].X, (float)nudPointY.Value);
               
                HalconEngineViewer.EditSrcRoidataToDisplay(srcPolygon);
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
                nudPointX.Value = (decimal)srcPolygon.PolygonPoints[index].X;
                nudPointY.Value = (decimal)srcPolygon.PolygonPoints[index].Y;
            }
            Editing = false;
        }

        public void DisplayROIdata(HyPolygon InputHyPolgon)
        {
            srcPolygon = InputHyPolgon;
            tvwRoiPointsList.BeginUpdate();
            tvwRoiPointsList.Nodes.Clear();
            string Index = "";

            for (int i = 0; i < srcPolygon.PolygonPoints.Count; i++)
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
                nudPointX.Value = (decimal)srcPolygon.PolygonPoints[0].X;
                nudPointY.Value = (decimal)srcPolygon.PolygonPoints[0].Y;
            }

            tvwRoiPointsList.EndUpdate();

        }


    }
}
