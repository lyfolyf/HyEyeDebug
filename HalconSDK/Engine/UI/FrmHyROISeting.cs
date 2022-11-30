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
using HalconDotNet;





namespace HalconSDK.Engine.UI
{
    public partial class FrmHyROISeting : Form
    {
        private HalconProgramEngineBL halconProgramEngineBL;
        private SettingsBL hyROISetingBL;
        private List<BaseHyROI> HyROIs;
        private HyROIRepository hyROIRepository;
        private List<HRegion> OutputResults = new List<HRegion>();
        private Bitmap Bmp;
     

        public FrmHyROISeting(HalconProgramEngineBL halconProgramEngineBL, SettingsBL hyROISetingBL)
        {
            
            this.halconProgramEngineBL = halconProgramEngineBL;
            this.hyROISetingBL = hyROISetingBL;
            this.HyROIs = halconProgramEngineBL.HyROIs;
            if (halconProgramEngineBL.ShowImage != null && halconProgramEngineBL.ShowImage.Image != null)
            {
                this.Bmp = halconProgramEngineBL.ShowImage.Image;
            }

            InitializeComponent();
        }

        private void FrmHyROISeting_Load(object sender, EventArgs e)
        {
            InitializeWindow();

            hyImageDisplayControl1.SetHyROIs(HyROIs);
            if (halconProgramEngineBL.ShowImage !=null && halconProgramEngineBL.ShowImage.Image != null)
            {
                hyImageDisplayControl1.DisplayImage(halconProgramEngineBL.ShowImage.Image);
            }
            this.hyROIRepository = hyImageDisplayControl1.GetHyROIRepository();
            hyImageDisplayControl1.AddROIEvent += AddNewROIEvent;
            hyImageDisplayControl1.DeleteROIEvent += DeleteROIEvent;

            if (Bmp != null)
            {
                hyImageDisplayControl1.DisplayImage(Bmp);
                HOperatorSet.SetSystem("width", Bmp.Width);
                HOperatorSet.SetSystem("height", Bmp.Height);
            }
            else
            {
                HOperatorSet.SetSystem("width", 7000);
                HOperatorSet.SetSystem("height", 7000);
            }


        }

        private void FrmHyROISeting_FormClosed(object sender, FormClosedEventArgs e)
        {
            hyROISetingBL.ResultHObjectROI = CalculationHobjectResult(hyROISetingBL.RoiCollection.Count);
        }


        private void AddNewROIEvent(int Index)
        {
            LoadROIList();
        }

        private void DeleteROIEvent(int Index)
        {
            LoadROIList();

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                ((HyROIOperation)panel1.Controls[i]).DeleteROI($"ROI{Index}");
            }
        }

        private void InitializeWindow()
        {
            LoadROIList();
            LoadOutputResult();
            LoadCollectionNames();
            LoadROIOperationControl();
        }

        private void LoadROIList()
        {
            lbROIListbox.Items.Clear();
            foreach (BaseHyROI roi in HyROIs)
            {
                lbROIListbox.Items.Add($"ROI{roi.Index}");
            }

            foreach (ROICollectionsBL uIInfo in hyROISetingBL.RoiCollection)
            {
                lbROIListbox.Items.Add($"Result{uIInfo.Index}");
            }
        }

        private void LoadOutputResult()
        {
            cbOutputResult.Items.Clear();
            foreach (ROICollectionsBL uIInfo in hyROISetingBL.RoiCollection)
            {
                cbOutputResult.Items.Add($"Result{uIInfo.Index}");

                HRegion hRegion = new HRegion();
                hRegion.GenEmptyObj();
                OutputResults.Add(hRegion);


            }
        }

        private void LoadCollectionNames()
        {
            cbCollectionName.Items.Clear();
            foreach (ROICollectionsBL uIInfo in hyROISetingBL.RoiCollection)
            {
                //集合添加至下拉框
                cbCollectionName.Items.Add($"并集{uIInfo.Index * 2 - 1}");
                cbCollectionName.Items.Add($"并集{uIInfo.Index * 2 }");
            }

            if (cbCollectionName.Items.Count > 0)
            {
                cbCollectionName.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ROICollectionsBL uIInfo = new ROICollectionsBL
            {
                Index = hyROISetingBL.RoiCollection.Count + 1,
                ROIOperation = "交集"
            };
            string UnionName1 = $"并集{uIInfo.Index * 2 - 1}";
            string UnionName2 = $"并集{uIInfo.Index * 2}";
            uIInfo.Description = $"{UnionName1},{UnionName2}";
            hyROISetingBL.RoiCollection.Add(uIInfo);

            HyROIOperation hyROIOperation = new HyROIOperation(uIInfo)
            {
                Name = uIInfo.Description,
                Tag = uIInfo,
                Dock = DockStyle.Top
            };
            panel1.Controls.Add(hyROIOperation);
            panel1.Controls.SetChildIndex(hyROIOperation, 0);
            panel1.ScrollControlIntoView(hyROIOperation);

            cbOutputResult.Items.Add($"Result{uIInfo.Index}");
            lbROIListbox.Items.Add($"Result{uIInfo.Index}");
            cbCollectionName.Items.Add(UnionName1);
            cbCollectionName.Items.Add(UnionName2);


            HRegion hRegion = new HRegion();
            hRegion.GenEmptyObj();
            OutputResults.Add(hRegion);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int count = hyROISetingBL.RoiCollection.Count;
            if (count != 0)
            {
                hyROISetingBL.RoiCollection.RemoveAt(count - 1);

                //更新输出集合下拉框、ROI列表、并集下拉框
                cbOutputResult.Items.RemoveAt(count - 1);
                lbROIListbox.Items.RemoveAt(lbROIListbox.Items.Count - 1);
                //并集下拉框移除最后两项
                cbCollectionName.Items.RemoveAt(cbCollectionName.Items.Count - 1);
                cbCollectionName.Items.RemoveAt(cbCollectionName.Items.Count - 1);

                //更新Panle1的用户控件，以及其各个集合列表
                panel1.Controls.RemoveAt(0);
                for (int i = 0; i < panel1.Controls.Count; i++)
                {
                    ((HyROIOperation)panel1.Controls[i]).DeleteROI($"Result{count}");
                }

                OutputResults.RemoveAt(count - 1);
            }
        }

        private void UpdateROIList()
        {
            lbROIListbox.Items.Clear();
            foreach (BaseHyROI roi in HyROIs)
            {
                lbROIListbox.Items.Add($"ROI{roi.Index}");
            }

            foreach (ROICollectionsBL uIInfo in hyROISetingBL.RoiCollection)
            {
                lbROIListbox.Items.Add($"Result{uIInfo.Index}");
            }
        }

        private void LoadROIOperationControl()
        {
            //添加所有包含操作集合信息的用户控件      
            panel1.Controls.Clear();
            int count = hyROISetingBL.RoiCollection.Count;
            if (count > 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    HyROIOperation hyROIOperation = new HyROIOperation(hyROISetingBL.RoiCollection[i]);
                    hyROIOperation.Dock = DockStyle.Top;
                    hyROIOperation.Name = hyROISetingBL.RoiCollection[i].Description;
                    hyROIOperation.Tag = hyROISetingBL.RoiCollection[i];
                    panel1.Controls.Add(hyROIOperation);
                }
            }
            else
            {
                btnAdd_Click(null, null);
            }


        }

        private void lbROIListbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbROIListbox.SelectedItem != null)
            {
                string SelectROIName = lbROIListbox.SelectedItem.ToString();
                if (SelectROIName.Contains("ROI"))
                {
                    int Index = int.Parse(SelectROIName.Substring(3));
                    hyROIRepository.SetSelected(Index);
                    hyImageDisplayControl1.ClearGraphic();
                    hyImageDisplayControl1.DisplayControlInvalidate();
                }
                else if (SelectROIName.Contains("Result"))
                {
                    int Index = int.Parse(SelectROIName.Substring(6));

                    HObject result = CalculationHobjectResult(Index);
                    HalconDataConvert DataSwitch = new HalconDataConvert();
                    hyImageDisplayControl1.ShowGraphic(DataSwitch.HobjectToHyDefectRegion(result));
                }
            }

        }

        private void lbROIListbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(cbCollectionName.Text))
            {
                MessageBox.Show("请先选择要需要添加ROI的目标集合", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (lbROIListbox.SelectedIndex == -1)
                {
                    return;
                }
                HyROIOperation hyROIOperation = null;
                foreach (Control c in panel1.Controls)
                {
                    if ((c as HyROIOperation).Name.Contains(cbCollectionName.Text))
                    {
                        hyROIOperation = c as HyROIOperation;
                        break;
                    }
                }

                string SelectItemName = lbROIListbox.SelectedItem.ToString();
                if (SelectItemName.Contains("Result"))
                {
                    int index = int.Parse(SelectItemName.Substring(6));
                    if (index >= ((ROICollectionsBL)hyROIOperation.Tag).Index)
                    {
                        MessageBox.Show($"只能添加前序的结果集合。", "提示", MessageBoxButtons.OK);
                        return;
                    }
                }

                hyROIOperation.AddROI(cbCollectionName.Text, lbROIListbox.Items[lbROIListbox.SelectedIndex].ToString());
            }
            catch
            { }



        }

        private void cbCollectionName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private HRegion CalculationResult(int ResultIndex)
        {
            ROICollectionsBL collectionInfoBL = hyROISetingBL.RoiCollection.FirstOrDefault(u => u.Index == ResultIndex);
            HalconDataConvert DataSwitch = new HalconDataConvert();

            HRegion Union1 = new HRegion();
            Union1.GenEmptyObj();
            foreach (string RoiName in collectionInfoBL.UnionCollection1)
            {
                int Index = 0;
                if (RoiName.Contains("ROI"))
                {
                    Index = int.Parse(RoiName.Substring(3));
                    BaseHyROI Roi = HyROIs.FirstOrDefault(r => r.Index == Index);
                    HRegion region = DataSwitch.HyRoiToHRegion(Roi);
                    Union1 = Union1.Union2(region);
                }
                else if (RoiName.Contains("Result"))
                {
                    Index = int.Parse(RoiName.Substring(6));
                    HRegion result = CalculationResult(Index);
                    Union1 = Union1.Union2(result);
                }
            }

            HRegion Union2 = new HRegion();
            Union2.GenEmptyObj();
            foreach (string RoiName in collectionInfoBL.UnionCollection2)
            {
                int Index = 0;
                if (RoiName.Contains("ROI"))
                {
                    Index = int.Parse(RoiName.Substring(3));
                    BaseHyROI Roi = HyROIs.FirstOrDefault(r => r.Index == Index);
                    Union2 = Union2.Union2(DataSwitch.HyRoiToHRegion(Roi));

                }
                else if (RoiName.Contains("Result"))
                {
                    Index = int.Parse(RoiName.Substring(6));
                    HRegion result = CalculationResult(Index);
                    Union2 = Union2.Union2(result);
                }
            }

            if (collectionInfoBL.ROIOperation == "并集")
            {
                return Union1.Union2(Union2);
            }
            else if (collectionInfoBL.ROIOperation == "交集")
            {
                return Union1.Intersection(Union2);
            }
            else if (collectionInfoBL.ROIOperation == "差集")
            {
                return Union1.Difference(Union2);
            }


            return default;

        }


        private HObject CalculationHobjectResult(int ResultIndex)
        {
            HObject[] results = new HObject[ResultIndex];
            HalconDataConvert DataSwitch = new HalconDataConvert();

            for (int i = 0; i < ResultIndex; i++)
            {
                HOperatorSet.GenEmptyObj(out HObject EachResult);
                results[i] = EachResult;

                HOperatorSet.GenEmptyObj(out HObject Union1);
                foreach (string RoiName in hyROISetingBL.RoiCollection[i].UnionCollection1)
                {
                    int Index = 0;
                    if (RoiName.Contains("ROI"))
                    {
                        Index = int.Parse(RoiName.Substring(3));
                        BaseHyROI Roi = HyROIs.FirstOrDefault(r => r.Index == Index);
                        HObject HalconROI = DataSwitch.HyRoiToHObject(Roi);
                        HOperatorSet.Union2(Union1, HalconROI, out HObject ExpTmpOutVar);
                        Union1.Dispose();
                        Union1 = ExpTmpOutVar;

                    }
                    else if (RoiName.Contains("Result"))
                    {
                        Index = int.Parse(RoiName.Substring(6));

                        HOperatorSet.Union2(Union1, results[Index - 1], out HObject ExpTmpOutVar);
                        Union1.Dispose();
                        Union1 = ExpTmpOutVar;
                    }
                }

                HOperatorSet.GenEmptyObj(out HObject Union2);
                foreach (string RoiName in hyROISetingBL.RoiCollection[i].UnionCollection2)
                {
                    int Index = 0;
                    if (RoiName.Contains("ROI"))
                    {
                        Index = int.Parse(RoiName.Substring(3));
                        BaseHyROI Roi = HyROIs.FirstOrDefault(r => r.Index == Index);
                        HObject HalconROI = DataSwitch.HyRoiToHObject(Roi);
                        HOperatorSet.Union2(Union2, HalconROI, out HObject ExpTmpOutVar);
                        Union2.Dispose();
                        Union2 = ExpTmpOutVar;

                    }
                    else if (RoiName.Contains("Result"))
                    {
                        Index = int.Parse(RoiName.Substring(6));

                        HOperatorSet.Union2(Union2, results[Index - 1], out HObject ExpTmpOutVar);
                        Union2.Dispose();
                        Union2 = ExpTmpOutVar;
                    }
                }

                if (hyROISetingBL.RoiCollection[i].ROIOperation == "并集")
                {
                    HOperatorSet.Union2(Union1, Union2, out results[i]);
                }
                else if (hyROISetingBL.RoiCollection[i].ROIOperation == "交集")
                {
                    HOperatorSet.Intersection(Union1, Union2, out results[i]);
                }
                else if (hyROISetingBL.RoiCollection[i].ROIOperation == "差集")
                {
                    HOperatorSet.Difference(Union1, Union2, out results[i]);
                }
            }

            return results[ResultIndex - 1];
        }

    }
}
