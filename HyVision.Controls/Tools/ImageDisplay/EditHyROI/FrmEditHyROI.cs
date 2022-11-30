using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace HyVision.Tools.ImageDisplay
{

    public partial class FrmEditHyROI : Form
    {
        public FrmEditHyROI()
        {
            InitializeComponent();
        }


        private HyROIRepository hyROIRepository = new HyROIRepository();
        private BaseHyROI CurrentHyROI;
        private Panel pl;
        private RoiType roiType;




        private void FrmEditHyROI_Load(object sender, EventArgs e)
        {
            InitializeWindow();
        }

        public void InitializeWindow()
        {
            foreach (var item in typeof(Color).GetMembers())
            {
                if (item.MemberType == System.Reflection.MemberTypes.Property &&
                    Color.FromName(item.Name).IsKnownColor == true)
                {
                    cbRoiColor.Items.Add(item.Name);
                }
            }
        }

        public void UpDateWindow(Panel DisplayPanel, HyROIRepository hyROIRepository)
        {
            if (this.hyROIRepository.GetHashCode() != hyROIRepository.GetHashCode())
            {
                this.hyROIRepository = hyROIRepository;
                pl = DisplayPanel;
            }

            CurrentHyROI = hyROIRepository.GetCurrentSelectedHyROI();

            if (CurrentHyROI == null)
            {
                if (hyROIRepository.GetHyROIs().Count == 0)
                {
                    this.Hide();
                }
                else
                {
                    BaseHyROI roi = hyROIRepository.GetHyROIs()[0];
                    roi.IsSelected = true;
                    UpDateWindow(roi);
                }
            }
            else
            {
                UpDateWindow(CurrentHyROI);
            }
        }

        private void UpDateWindow(BaseHyROI InputHyROI)
        {
            cbRoiIndex.Text = "ROI" + InputHyROI.Index.ToString();
            tbRoiType.Text = InputHyROI.RoiType.ToString();
            cbRoiColor.SelectedItem = ColorTranslator.ToHtml(InputHyROI.Color);
            nudLineWidth.Value = (decimal)InputHyROI.LineWidth;

            cbxFill.Checked = InputHyROI.IsFill;
            cbxVisible.Checked = InputHyROI.Visible;
            DisplayROIData(InputHyROI);

        }

        private void DisplayROIData(BaseHyROI InputHyROI)
        {
            if (InputHyROI == null) return;
            if (roiType != InputHyROI.RoiType)
            {
                panel1.Controls.Clear();
                if (InputHyROI.RoiType == RoiType.Circle)
                {
                    HyCircleInfo hyCircleInfo = new HyCircleInfo(pl);
                    hyCircleInfo.Dock = DockStyle.Fill;
                    panel1.Controls.Add(hyCircleInfo);
                }
                else if (InputHyROI.RoiType == RoiType.Ellipse)
                {
                    HyEllipseInfo hyEllipseInfo = new HyEllipseInfo(pl);
                    hyEllipseInfo.Dock = DockStyle.Fill;
                    panel1.Controls.Add(hyEllipseInfo);
                }
                else if (InputHyROI.RoiType == RoiType.Rectangle2)
                {
                    HyRectangleInfo hyRectInfo = new HyRectangleInfo(pl);
                    hyRectInfo.Dock = DockStyle.Fill;
                    panel1.Controls.Add(hyRectInfo);
                }
                else if (InputHyROI.RoiType == RoiType.Sector)
                {
                    HySectorInfo hySectorInfo = new HySectorInfo(pl);
                    hySectorInfo.Dock = DockStyle.Fill;
                    panel1.Controls.Add(hySectorInfo);

                }
                else if (InputHyROI.RoiType == RoiType.Polygon)
                {
                    HyPolygonInfo hyPolygonInfo = new HyPolygonInfo(pl);
                    hyPolygonInfo.Dock = DockStyle.Fill;
                    panel1.Controls.Add(hyPolygonInfo);
                }

                roiType = InputHyROI.RoiType;
            }


            if (roiType == RoiType.Circle)
            {
                HyCircleInfo hyCircleInfo = (HyCircleInfo)panel1.Controls[0];
                hyCircleInfo.DisplayROIdata(InputHyROI);
            }
            else if (roiType == RoiType.Ellipse)
            {
                HyEllipseInfo hyEllipseInfo = (HyEllipseInfo)panel1.Controls[0];
                hyEllipseInfo.DisplayROIdata(InputHyROI);
            }
            else if (roiType == RoiType.Rectangle2)
            {
                HyRectangleInfo hyRectInfo = (HyRectangleInfo)panel1.Controls[0];
                hyRectInfo.DisplayROIdata(InputHyROI);
            }
            else if (roiType == RoiType.Sector)
            {
                HySectorInfo hySectorInfo = (HySectorInfo)panel1.Controls[0];
                hySectorInfo.DisplayROIdata(InputHyROI);
            }
            else if (roiType == RoiType.Polygon)
            {
                HyPolygonInfo hyPolygonInfo = (HyPolygonInfo)panel1.Controls[0];
                hyPolygonInfo.DisplayROIdata(InputHyROI);
                testN += 1;
                Console.WriteLine(testN);
            }

            pl.Invalidate();
        }

        int testN;
        private void cbRoiIndex_DropDown(object sender, EventArgs e)
        {
            LoadHyROIs();
        }

        private void cbRoiIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roiIndex = int.Parse(cbRoiIndex.Text.Substring(3));
            //hyROIRepository.SetSelected(false);
            BaseHyROI roi = hyROIRepository.GetHyROI(roiIndex);
            roi.IsSelected = true;
            UpDateWindow(roi);
        }

        //private void comboBox1_DrawItem(object sender,
        //   DrawItemEventArgs e)
        //{
        //    string s = this.cbRoiIndex.Items[e.Index].ToString();
        //    // 计算字符串尺寸（以像素为单位）
        //    SizeF ss = e.Graphics.MeasureString(s, e.Font);
        //    // 水平居中
        //    float left = (float)(e.Bounds.Width - ss.Width) / 2;
        //    if (left < 0) left = 0f;
        //    float top = (float)(e.Bounds.Height - ss.Height) / 2;
        //    // 垂直居中
        //    if (top < 0) top = 0f;
        //    top = top + this.cbRoiIndex.ItemHeight * e.Index;
        //    // 输出
        //    e.DrawBackground();
        //    e.DrawFocusRectangle();
        //    e.Graphics.DrawString(
        //        s,
        //        e.Font,
        //        new SolidBrush(e.ForeColor),
        //        left, top);
        //}

        private void tbROIType_TextChanged(object sender, EventArgs e)
        {
            //panel1.Controls.Clear();
            //string roiType = tbRoiType.Text;
            //{
            //    if (roiType == ROIType.Circle.ToString())
            //    {

            //    }
            //    else if (roiType == ROIType.Rectangle2.ToString())
            //    {
            //        userControl = new HyRectangleInfo((BaseHyROI)tbRoiType.Tag);
            //        userControl.Dock = DockStyle.Fill;
            //        panel1.Controls.Add(userControl);

            //    }
            //}

        }

        private void cbRoiColor_DropDown(object sender, EventArgs e)
        {

        }

        private void cbRoiColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roiIndex = int.Parse(cbRoiIndex.Text.Substring(3));
            BaseHyROI roi = hyROIRepository.GetHyROI(roiIndex);
            roi.Color = Color.FromName(cbRoiColor.Text);
            pl.Invalidate();
        }

        private void nudLineWidth_ValueChanged(object sender, EventArgs e)
        {
            int roiIndex = int.Parse(cbRoiIndex.Text.Substring(3));
            BaseHyROI roi = hyROIRepository.GetHyROI(roiIndex);
            roi.LineWidth = (float)nudLineWidth.Value;
            pl.Invalidate();
        }

        private void cbxFill_CheckedChanged(object sender, EventArgs e)
        {
            int roiIndex = int.Parse(cbRoiIndex.Text.Substring(3));
            BaseHyROI roi = hyROIRepository.GetHyROI(roiIndex);
            roi.IsFill = cbxFill.Checked;
            pl.Invalidate();
        }

        private void cbxAllFill_CheckedChanged(object sender, EventArgs e)
        {
            cbxFill.Checked = cbxAllFill.Checked;
            hyROIRepository.SetFill(cbxAllFill.Checked);
            pl.Invalidate();
        }

        private void cbxVisible_CheckedChanged(object sender, EventArgs e)
        {
            int roiIndex = int.Parse(cbRoiIndex.Text.Substring(3));
            BaseHyROI roi = hyROIRepository.GetHyROI(roiIndex);
            roi.Visible = cbxVisible.Checked;
            pl.Invalidate();
        }

        private void cbxAllVisible_CheckedChanged(object sender, EventArgs e)
        {
            cbxVisible.Checked = cbxAllVisible.Checked;
            hyROIRepository.SetVisible(cbxAllVisible.Checked);
            pl.Invalidate();
        }

        private void FrmEditHyROI_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


        private void LoadHyROIs()
        {
            List<BaseHyROI> hyROIs = hyROIRepository.GetHyROIs();

            cbRoiIndex.Items.Clear();
            foreach (BaseHyROI roi in hyROIs)
            {
                cbRoiIndex.Items.Add("ROI" + roi.Index);
            }
        }


    }
}
