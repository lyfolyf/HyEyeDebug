using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HyVision.Tools.ImageDisplay;
using HyVision.Models;
using HyVision.Tools;
using HalconSDK.Engine.BL;
using HalconDotNet;




namespace HalconSDK.Engine.UI
{
    public partial class HalconEngineUI_2 : BaseHyUserToolEdit<HalconProgramEngineBL_2>
    {
        public HalconEngineUI_2()
        {
            InitializeComponent();
        }


      


        private void HalconEngineUI_2_Load(object sender, EventArgs e)
        {
     
            HalconEngineModel.IsInitialization = false;
            hyImageDisplayControl1.SetBackColor(Color.Gray);
            hyImageDisplayControl1.ShowEditROIForm = false;
            hyImageDisplayControl1.TopToolVisible = false;


            for (int i = 0; i < dgvFunctionParam.Columns.Count; i++)
            {
                dgvFunctionParam.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvFunctionParam.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgvFunctionParam.Columns[0].Width = 50;
            dgvFunctionParam.Columns[1].Width = 200;
            dgvFunctionParam.Columns[2].Width = 90;
            dgvFunctionParam.Columns[3].Width = 200;
            dgvFunctionParam.Columns[4].Width = 100;
            dgvFunctionParam.Columns[5].Width = 90;
            dgvFunctionParam.Columns[6].Width = 80;

            hyImageDisplayControl1.HyDisplayPanel.MouseMove += HyDisplayPanel_MouseMove1;
        }

        //反变换暂时不做
        private void HyDisplayPanel_MouseMove1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //BaseHyROI roi = hyImageDisplayControl1.GetHyROIRepository().GetCurrentSelectedHyROI();

                //BaseHyROI SourceRoi = InverseAffineTransform(roi);
                //TreeNode Tn = tvwRoiList.Nodes.FirstOrDefault(false, r => ((BaseHyROI)r.Tag).Index == SourceRoi.Index);

                //Tn.Tag = SourceRoi;
                //tvwRoiList.SelectedNode = Tn;
            }
        }

        HalconProgramEngineBL_2 HalconEngineModel;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HalconProgramEngineBL_2 Subject
        {
            get
            {
                return HalconEngineModel;
            }
            set
            {
                HalconEngineModel = value;

                //初始化建模界面
                tvwRoiList.Nodes.Clear();
                tvwFuncList.Nodes.Clear();
                tvwSelectFuncList.Nodes.Clear();
                dgvFunctionParam.Rows.Clear();
                tbxHalconFilePath.Text = HalconEngineModel.HalconFilePath;
                tbxModelImagePath.Text = HalconEngineModel.ModelImagePath;
                tbxCsvFilePath.Text = HalconEngineModel.CsvFilePath;
                cbxMainAxis.Text = HalconEngineModel.MainAxis;
                nudPixelRes.Value = (decimal)HalconEngineModel.PixelRes;
                tabControl1.SelectedIndex = HalconEngineModel.UIIndex;

                if (File.Exists(tbxModelImagePath.Text))
                {
                    hyImageDisplayControl1.DisplayImage(tbxModelImagePath.Text);
                }
                if (File.Exists(HalconEngineModel.CsvFilePath))
                {
                    ReadCsvData(HalconEngineModel.CsvFilePath);
                    UpdateRoiTreeView(OriginalRoiData);
                }
                if (File.Exists(tbxHalconFilePath.Text))
                {
                    //初始化Halcon引擎界面
                    string folderPath = Directory.GetCurrentDirectory();
                    int ret = HalconEngineModel.InitializeHalconEngine();
                    if (ret != 0)
                    {
                        MessageBox.Show("初始化Halcon引擎出错！，请检查Halcon程序文件和函数库文件是否齐全。", "初始化出错"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Directory.SetCurrentDirectory(folderPath);
                    LoadFunctionList();
                    LoadSelectedFunction(HalconEngineModel.SelectFuncNames);

                    //更新表格
                    dgvFunctionParam.CellValueChanged -= dgvFunctionParam_CellValueChanged;
                    UpdateDataGridView(tbxHalconFilePath.Text, HalconEngineModel.SelectFuncNames);
                    CompareValue();
                    dgvFunctionParam.CellValueChanged += dgvFunctionParam_CellValueChanged;
                }

                if (File.Exists(tbxHalconFilePath.Text) &&
                    File.Exists(tbxModelImagePath.Text) &&
                    File.Exists(HalconEngineModel.CsvFilePath))
                {
                    //hyImageDisplayControl1.DisplayImage(tbxModelImagePath.Text);
                    HalconEngineModel.CreateAffineMatrix();
                    CreateAffineRoiData();
                    hyImageDisplayControl1.SetHyROIs(AffineRoiData);
                }

            }
        }

        public override void UpdateDataToObject()
        {
            //1.保存参数列表和函数列表
            btnSave_Click(null, null);

            //2.刷新外层连线的输入和输出
            List<string> NewInputNames = new List<string>();
            List<string> NewOutputNames = new List<string>();
            for (int i = 0; i < HalconEngineModel.Parameters.Count; i++)
            {
                ParameterInfo Info = HalconEngineModel.Parameters[i];
                string FuncParamName = $"{Info.FunctionName},{Info.ParamName}";

                if (Info.IsConnect == true)
                {
                    if (Info.InputOutputType == "输入")
                    {
                        NewInputNames.Add(FuncParamName);
                        HyTerminal OldInput = HalconEngineModel.Inputs.FirstOrDefault(t =>
                        t.Description == FuncParamName);
                        if (OldInput == null)
                        {
                            HyTerminal NewInput = ParameterInfoToHyTerminal(Info);
                            HalconEngineModel.Inputs.Add(NewInput);
                        }
                    }
                    else
                    {
                        NewOutputNames.Add(FuncParamName);
                        HyTerminal OldOutput = HalconEngineModel.Outputs.FirstOrDefault(t =>
                        t.Description == FuncParamName);
                        if (OldOutput == null)
                        {
                            HyTerminal NewOutput = ParameterInfoToHyTerminal(Info);
                            HalconEngineModel.Outputs.Add(NewOutput);
                        }
                    }
                }
            }

            for (int i = 0; i < HalconEngineModel.Inputs.Count; i++)
            {
                HyTerminal ht = HalconEngineModel.Inputs[i];
                string str = NewInputNames.Find(n => n == ht.Description);
                if (str == null)
                {
                    HalconEngineModel.Inputs.Remove(ht.Name);
                    i -= 1;
                }
            }

            for (int i = 0; i < HalconEngineModel.Outputs.Count; i++)
            {
                HyTerminal ht = HalconEngineModel.Outputs[i];
                string str = NewOutputNames.Find(n => n == ht.Description);
                if (str == null)
                {
                    HalconEngineModel.Outputs.Remove(ht.Name);
                    i -= 1;
                }
            }

          

        }

        public override void Save()
        {

        }




        private HyTerminal ParameterInfoToHyTerminal(ParameterInfo ParamInfo)
        {
            Type dataType = null;
            if (ParamInfo.DataType == HalconDataType.Image.ToString())
            {
                dataType = typeof(HyImage);
            }
            else if (ParamInfo.DataType == HalconDataType.Region.ToString())
            {
                //dataType = typeof(HyVision.Tools.ImageDisplay.BaseHyROI);
                dataType = typeof(List<HyDefectXLD>);

                //dataType = typeof(HyDefectRegion);
            }
            else if (ParamInfo.DataType == HalconDataType.XLD.ToString())
            {
                //dataType = typeof(HyVision.Tools.ImageDisplay.BaseHyROI);
                dataType = typeof(HyDefectXLD);
            }
            else if (ParamInfo.DataType == HalconDataType.Int.ToString())
            {
                dataType = typeof(int);
            }
            else if (ParamInfo.DataType == HalconDataType.Double.ToString())
            {
                dataType = typeof(double);
            }
            else if (ParamInfo.DataType == HalconDataType.Bool.ToString())
            {
                dataType = typeof(bool);
            }
            else if (ParamInfo.DataType == HalconDataType.String.ToString())
            {
                dataType = typeof(string);
            }

            HyTerminal hyTerminal = new HyTerminal(ParamInfo.ParamName, dataType);
            hyTerminal.GUID = Guid.NewGuid().ToString("N");
            hyTerminal.Description = $"{ParamInfo.FunctionName},{ParamInfo.ParamName}";
            return hyTerminal;
        }




        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HalconEngineModel.UIIndex = tabControl1.SelectedIndex;

        }

        private void btnHalconFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择Halcon程序路径";
            openFileDialog.Filter = "HDEV文件(*.hdev)|*.hdev";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                HalconEngineModel.HalconFilePath = openFileDialog.FileName;
                tbxHalconFilePath.Text = openFileDialog.FileName;
                AffineRoiData.Clear();
                hyImageDisplayControl1.DisplayControlInvalidate();

                //加载Halcon引擎可能会更改Directory.GetCurrentDirectory()的路径，而这个路径框架会用到
                string folderPath = Directory.GetCurrentDirectory();
                HalconEngineModel.InitializeHalconEngine();
                Directory.SetCurrentDirectory(folderPath);
                LoadFunctionList();
                tvwSelectFuncList.Nodes.Clear();
                dgvFunctionParam.Rows.Clear();
            }
        }

        private void btnImagePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择标准模板图片";
            openFileDialog.Filter = "图片(jpg;png;gif;bmp;jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                HalconEngineModel.ModelImagePath = openFileDialog.FileName;
                tbxModelImagePath.Text = openFileDialog.FileName;
                hyImageDisplayControl1.DisplayImage(tbxModelImagePath.Text);
                AffineRoiData.Clear();
                hyImageDisplayControl1.DisplayControlInvalidate();
            }
        }

        private void btnCSVPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择ROI坐标文件";
            openFileDialog.Filter = "csv文件(.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                HalconEngineModel.CsvFilePath = openFileDialog.FileName;
                tbxCsvFilePath.Text = openFileDialog.FileName;
                AffineRoiData.Clear();
                hyImageDisplayControl1.DisplayControlInvalidate();

                ReadCsvData(HalconEngineModel.CsvFilePath);
                UpdateRoiTreeView(OriginalRoiData);
            }
        }

        private void cbxMainAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            AffineRoiData.Clear();
            hyImageDisplayControl1.DisplayControlInvalidate();
            HalconEngineModel.MainAxis = cbxMainAxis.Text;
        }

        private void nudPixelRes_ValueChanged(object sender, EventArgs e)
        {
            AffineRoiData.Clear();
            hyImageDisplayControl1.DisplayControlInvalidate();
            HalconEngineModel.PixelRes = (float)nudPixelRes.Value;
        }

        private void btnCreateROI_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbxHalconFilePath.Text))
            {
                MessageBox.Show("Halcon程序路径为空或者文件已经不存在，请重新配置！", "重新配置文件路径", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(tbxModelImagePath.Text))
            {
                MessageBox.Show("模板图片路径为空或者文件已经不存在，请重新配置！", "重新配置模板图片路径", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(tbxCsvFilePath.Text))
            {
                MessageBox.Show("ROI坐标文件路径为空或者文件已经不存在，请重新配置！", "重新配置ROI坐标文件路径", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hyImageDisplayControl1.DisplayImage(tbxModelImagePath.Text);
            int ret = HalconEngineModel.CreateAffineMatrix();
            if (ret != 0)
            {
                MessageBox.Show("生成ROI出错，请检查Halcon程序文件和函数库文件是否齐全。", "生成ROI出错"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CreateAffineRoiData();
            hyImageDisplayControl1.SetHyROIs(AffineRoiData);
        }

        private void btnReReadData_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbxCsvFilePath.Text))
            {
                MessageBox.Show("找不到ROI坐标文件，请重新配置ROI坐标文件路径！", "文件丢失", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AffineRoiData.Clear();
                hyImageDisplayControl1.DisplayControlInvalidate();

                ReadCsvData(tbxCsvFilePath.Text);
                UpdateRoiTreeView(OriginalRoiData);
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (WriteCsvData(tbxCsvFilePath.Text) == 0)
            {
                MessageBox.Show("保存数据成功！", "保存数据成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (tvwRoiList.Nodes.Count > 1 && tvwRoiList.SelectedNode != null && tvwRoiList.SelectedNode != tvwRoiList.Nodes[0])
            {
                int IndexNum = tvwRoiList.Nodes.IndexOf(tvwRoiList.SelectedNode);
                TreeNode tn = tvwRoiList.SelectedNode;
                tvwRoiList.Nodes.RemoveAt(IndexNum);
                tvwRoiList.Nodes.Insert(IndexNum - 1, tn);

                string str1 = tvwRoiList.Nodes[IndexNum - 1].Text;
                string str2 = tvwRoiList.Nodes[IndexNum].Text;
                tvwRoiList.Nodes[IndexNum - 1].Text = str2.Substring(0, 2) + str1.Substring(2, str1.Length - 2);
                tvwRoiList.Nodes[IndexNum].Text = str1.Substring(0, 2) + str2.Substring(2, str2.Length - 2);

                tvwRoiList.SelectedNode = tvwRoiList.Nodes[IndexNum - 1];
            }
            tvwRoiList.Focus();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (tvwRoiList.Nodes.Count > 1 && tvwRoiList.SelectedNode != null &&
                tvwRoiList.SelectedNode != tvwRoiList.Nodes[tvwRoiList.Nodes.Count - 1])
            {
                int IndexNum = tvwRoiList.Nodes.IndexOf(tvwRoiList.SelectedNode);
                TreeNode tn = tvwRoiList.SelectedNode;
                tvwRoiList.Nodes.RemoveAt(IndexNum);
                tvwRoiList.Nodes.Insert(IndexNum + 1, tn);

                string str1 = tvwRoiList.Nodes[IndexNum + 1].Text;
                string str2 = tvwRoiList.Nodes[IndexNum].Text;
                tvwRoiList.Nodes[IndexNum + 1].Text = str2.Substring(0, 2) + str1.Substring(2, str1.Length - 2);
                tvwRoiList.Nodes[IndexNum].Text = str1.Substring(0, 2) + str2.Substring(2, str2.Length - 2);
                tvwRoiList.SelectedNode = tvwRoiList.Nodes[IndexNum + 1];
            }
            tvwRoiList.Focus();
        }

        private void tvwRoiList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            BaseHyROI roi = e.Node.Tag as BaseHyROI;
            if (roi == null)
            {
                return;
            }

            //显示ROI具体坐标信息
            gbxRoiInfo.Controls.Clear();
            gbxRoiInfo.Text = e.Node.Text;
            switch (roi.RoiType)
            {
                case RoiType.Circle:
                    HyCircleInfo CircleInfo = new HyCircleInfo(this, roi as HyCircle);
                    CircleInfo.Dock = DockStyle.Fill;
                    gbxRoiInfo.Controls.Add(CircleInfo);
                    break;

                case RoiType.Rectangle1:
                    HyRectangle1Info Rectangle1Info = new HyRectangle1Info(this, roi as HyRectangle1);
                    Rectangle1Info.Dock = DockStyle.Fill;
                    gbxRoiInfo.Controls.Add(Rectangle1Info);
                    break;

                case RoiType.Rectangle2:
                    HyRectangle2Info Rectangle2Info = new HyRectangle2Info(this, roi as HyRectangle2);
                    Rectangle2Info.Dock = DockStyle.Fill;
                    gbxRoiInfo.Controls.Add(Rectangle2Info);
                    break;

                case RoiType.Polygon:
                    HyPolygonInfo PolygonInfo = new HyPolygonInfo(this, roi as HyPolygon);
                    PolygonInfo.Dock = DockStyle.Fill;
                    gbxRoiInfo.Controls.Add(PolygonInfo);
                    break;

                case RoiType.Points:
                    HyPointsInfo PointsInfo = new HyPointsInfo(this, roi as HyPoints);
                    PointsInfo.Dock = DockStyle.Fill;
                    gbxRoiInfo.Controls.Add(PointsInfo);
                    break;
            }


        }

        private void tsmiFold_Click(object sender, EventArgs e)
        {
            tvwFuncList.CollapseAll();
        }

        private void tsmiSpread_Click(object sender, EventArgs e)
        {
            tvwFuncList.ExpandAll();
        }

        private void tvwFuncList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                int cout = tvwSelectFuncList.Nodes.Count;
                string index = "";
                if (cout <= 8)
                {
                    index = $"0{cout + 1}.";
                }
                else
                {
                    index = $"{cout + 1}.";
                }
                if (tvwSelectFuncList.Nodes.FirstOrDefault(false, n => n.Text.Contains(e.Node.Text)) == null)
                {
                    TreeNode Tn = tvwSelectFuncList.Nodes.Add($"{index}{e.Node.Text}");
                    Tn.ImageIndex = 1; Tn.SelectedImageIndex = 1;
                    btnConfirm_Click(null, null);
                }
                else
                {
                    MessageBox.Show($"函数{e.Node.Text}已经添加在已选列表，请添加其他函数！", "函数已添加", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void tvwSelectFuncList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Tn = e.Node;
            if (Tn != null)
            {
                string FuncName = Tn.Text.Substring(3);

                for (int i = 0; i < dgvFunctionParam.Rows.Count; i++)
                {
                    if (dgvFunctionParam[1, i].Value.ToString() == FuncName)
                    {
                        dgvFunctionParam.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
        }


        private void btnAddFunc_Click(object sender, EventArgs e)
        {
            TreeNode Tn = tvwFuncList.SelectedNode;
            if (Tn != null && Tn.Level == 1)
            {
                int cout = tvwSelectFuncList.Nodes.Count;
                string index = "";
                if (cout <= 8)
                {
                    index = $"0{cout + 1}.";
                }
                else
                {
                    index = $"{cout + 1}.";
                }
                if (tvwSelectFuncList.Nodes.FirstOrDefault(false, n => n.Text.Contains(Tn.Text)) == null)
                {
                    TreeNode TnAdd = tvwSelectFuncList.Nodes.Add($"{index}{Tn.Text}");
                    TnAdd.ImageIndex = 1; TnAdd.SelectedImageIndex = 1;
                }
                else
                {
                    MessageBox.Show($"函数{ Tn.Text }已经添加在已选列表，请添加其他函数！", "函数已添加", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tvwFuncList.Focus();
            btnConfirm_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode Tn = tvwSelectFuncList.SelectedNode;
            if (Tn != null)
            {
                int IndexNum1 = tvwSelectFuncList.Nodes.IndexOf(Tn);
                tvwSelectFuncList.Nodes.RemoveAt(IndexNum1);

                for (int i = IndexNum1; i < tvwSelectFuncList.Nodes.Count; i++)
                {
                    string str = tvwSelectFuncList.Nodes[i].Text;
                    tvwSelectFuncList.Nodes[i].Text = (i + 1).ToString().PadLeft(2, '0') + str.Substring(2, str.Length - 2);
                }
                tvwSelectFuncList.Focus();
                btnConfirm_Click(null, null);
            }

        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (tvwSelectFuncList.Nodes.Count > 1 && tvwSelectFuncList.SelectedNode != null && tvwSelectFuncList.SelectedNode != tvwSelectFuncList.Nodes[0])
            {
                int IndexNum = tvwSelectFuncList.Nodes.IndexOf(tvwSelectFuncList.SelectedNode);
                TreeNode tn = tvwSelectFuncList.SelectedNode;
                tvwSelectFuncList.Nodes.RemoveAt(IndexNum);
                tvwSelectFuncList.Nodes.Insert(IndexNum - 1, tn);

                string str1 = tvwSelectFuncList.Nodes[IndexNum - 1].Text;
                string str2 = tvwSelectFuncList.Nodes[IndexNum].Text;
                tvwSelectFuncList.Nodes[IndexNum - 1].Text = str2.Substring(0, 2) + str1.Substring(2, str1.Length - 2);
                tvwSelectFuncList.Nodes[IndexNum].Text = str1.Substring(0, 2) + str2.Substring(2, str2.Length - 2);

                tvwSelectFuncList.SelectedNode = tvwSelectFuncList.Nodes[IndexNum - 1];
            }
            tvwSelectFuncList.Focus();
            btnConfirm_Click(null, null);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (tvwSelectFuncList.Nodes.Count > 1 && tvwSelectFuncList.SelectedNode != null &&
              tvwSelectFuncList.SelectedNode != tvwSelectFuncList.Nodes[tvwSelectFuncList.Nodes.Count - 1])
            {
                int IndexNum = tvwSelectFuncList.Nodes.IndexOf(tvwSelectFuncList.SelectedNode);
                TreeNode tn = tvwSelectFuncList.SelectedNode;
                tvwSelectFuncList.Nodes.RemoveAt(IndexNum);
                tvwSelectFuncList.Nodes.Insert(IndexNum + 1, tn);

                string str1 = tvwSelectFuncList.Nodes[IndexNum + 1].Text;
                string str2 = tvwSelectFuncList.Nodes[IndexNum].Text;
                tvwSelectFuncList.Nodes[IndexNum + 1].Text = str2.Substring(0, 2) + str1.Substring(2, str1.Length - 2);
                tvwSelectFuncList.Nodes[IndexNum].Text = str1.Substring(0, 2) + str2.Substring(2, str2.Length - 2);
                tvwSelectFuncList.SelectedNode = tvwSelectFuncList.Nodes[IndexNum + 1];
            }
            tvwSelectFuncList.Focus();
            btnConfirm_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveParameters();
            HalconEngineModel.SelectFuncNames = new List<string>(tvwSelectFuncList.Nodes.Count);
            for (int i = 0; i < tvwSelectFuncList.Nodes.Count; i++)
            {
                HalconEngineModel.SelectFuncNames.Add(tvwSelectFuncList.Nodes[i].Text.Substring(3));
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbxHalconFilePath.Text))
            {
                MessageBox.Show("Halcon程序路径为空或者文件已经不存在，请重新配置！", "重新配置文件路径", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            HalconEngineModel.SelectFuncNames = new List<string>(tvwSelectFuncList.Nodes.Count);
            for (int i = 0; i < tvwSelectFuncList.Nodes.Count; i++)
            {
                HalconEngineModel.SelectFuncNames.Add( tvwSelectFuncList.Nodes[i].Text.Substring(3));
            }

            dgvFunctionParam.CellValueChanged -= dgvFunctionParam_CellValueChanged;
            UpdateDataGridView(tbxHalconFilePath.Text, HalconEngineModel.SelectFuncNames);
            CompareValue();
            dgvFunctionParam.CellValueChanged += dgvFunctionParam_CellValueChanged;
        }

        private void dgvFunctionParam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 5)
            {
                if (dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    return;
                }

                if (dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "设置")
                {
                    ParameterInfo ParamInfo = dgvFunctionParam.Rows[e.RowIndex].Tag as ParameterInfo;
                    FrmSelectROI frmSelectROI = new FrmSelectROI(AffineRoiData, ParamInfo);
                    frmSelectROI.DisplayImage(tbxModelImagePath.Text);
                    frmSelectROI.ShowDialog();
                }
            }
        }

        private void dgvFunctionParam_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (HalconEngineModel == null) return;

            //根据参数类型确定值的表格是下拉框、按钮还是文本框
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                if (dgvFunctionParam.Rows[e.RowIndex].Cells[2].Value.ToString() != "输入")
                {
                    return;
                }
                string DataType = dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (DataType == HalconDataType.Region.ToString())
                {
                    DataGridViewButtonCell btnSeting = new DataGridViewButtonCell();
                    btnSeting.Value = "设置";
                    dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = btnSeting;
                }
                else if (DataType == HalconDataType.Bool.ToString())
                {
                    DataGridViewComboBoxCell cbxBool = new DataGridViewComboBoxCell
                    {
                        Style = this.dgvFunctionParam.DefaultCellStyle
                    };
                    cbxBool.Items.Add("true");
                    cbxBool.Items.Add("false");
                    cbxBool.Value = "false";
                    dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = cbxBool;
                }
                else if (DataType == HalconDataType.内部输出.ToString())
                {
                    dgvFunctionParam.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = GetInnerOutputItem(e.RowIndex);
                }
                else
                {
                    if (dgvFunctionParam.Rows[e.RowIndex].Cells[5].GetType() != typeof(DataGridViewTextBoxCell))
                    {
                        DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
                        dgvFunctionParam.Rows[e.RowIndex].Cells[5] = textBoxCell;
                        dgvFunctionParam.Rows[e.RowIndex].Cells[5].Value = "0";
                    }
                }
            }
        }

        private DataGridViewComboBoxCell GetInnerOutputItem(int CurrentRowIndex)
        {
            DataGridViewComboBoxCell cbxInnerOutput = new DataGridViewComboBoxCell
            {
                Style = this.dgvFunctionParam.DefaultCellStyle
            };

            string CurrentFuncName = dgvFunctionParam.Rows[CurrentRowIndex].Cells[1].Value.ToString();
            for (int i = 0; i < CurrentRowIndex; i++)
            {
                string Funcname = dgvFunctionParam.Rows[i].Cells[1].Value.ToString();

                if (CurrentFuncName != Funcname)
                {
                    string inputOutputTpye = dgvFunctionParam.Rows[i].Cells[2].Value.ToString();

                    if (inputOutputTpye == "输入")
                    {
                        continue;
                    }
                    else
                    {
                        string funcIndex = dgvFunctionParam.Rows[i].Cells[0].Value.ToString();
                        string paramName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString();
                        cbxInnerOutput.Items.Add($"{funcIndex},{paramName}");
                    }
                }
                else
                {
                    break;
                }
            }
            return cbxInnerOutput;
        }

        private void dgvFunctionParam_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex <= 2 && e.RowIndex != -1)
            {
                int count = 0, UpRows = 0, DownRows = 0;
                string CellValue = e.Value == null ? "" : e.Value.ToString();
                Brush gridBrush = new SolidBrush(dgvFunctionParam.GridColor);
                Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor);

                for (int i = e.RowIndex; i < dgvFunctionParam.Rows.Count; i++)
                {
                    if (CellValue == dgvFunctionParam.Rows[i].Cells[e.ColumnIndex].Value.ToString())
                    {
                        DownRows += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = e.RowIndex; i >= 0; i--)
                {
                    if (CellValue == dgvFunctionParam.Rows[i].Cells[e.ColumnIndex].Value.ToString())
                    {
                        UpRows++;
                    }
                    else
                    {
                        break;
                    }
                }

                count = DownRows + UpRows - 1;
                if (count < 2)
                {
                    return;
                }

                e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                e.Graphics.DrawLine(new Pen(gridBrush), e.CellBounds.Right - 1, e.CellBounds.Top,
                e.CellBounds.Right - 1, e.CellBounds.Bottom);

                if (DownRows == 1)
                {
                    e.Graphics.DrawLine(new Pen(gridBrush), e.CellBounds.Left, e.CellBounds.Bottom - 1,
                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }

                int fontheight = (int)e.Graphics.MeasureString(CellValue, e.CellStyle.Font).Height;
                int fontwidth = (int)e.Graphics.MeasureString(CellValue, e.CellStyle.Font).Width;
                int cellheight = e.CellBounds.Height;
                int cellwidth = e.CellBounds.Width;
                e.Graphics.DrawString(CellValue, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor),
                    e.CellBounds.X + (cellwidth - fontwidth) / 2,
                    e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);

                e.Handled = true;
            }
        }

        //保存表格的数据到  HalconEngineModel.lstParameters
        private void SaveParameters()
        {
            for (int i = 0; i < dgvFunctionParam.Rows.Count; i++)
            {
                if (dgvFunctionParam.Rows[i].Cells[4].Value == null ||
                    dgvFunctionParam.Rows[i].Cells[5].Value == null)
                {
                    dgvFunctionParam.Rows[i].Selected = true;
                    dgvFunctionParam.FirstDisplayedScrollingRowIndex = i;
                    MessageBox.Show($"第{i}列有值为空，请重新配置！", "值为空", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                ParameterInfo ParamInfo = HalconEngineModel.Parameters[i];
                ParamInfo.RowIndex = i.ToString();
                ParamInfo.FuncIndex = dgvFunctionParam.Rows[i].Cells[0].Value.ToString();
                ParamInfo.FunctionName = dgvFunctionParam.Rows[i].Cells[1].Value.ToString();
                ParamInfo.InputOutputType = dgvFunctionParam.Rows[i].Cells[2].Value.ToString();
                ParamInfo.ParamName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString();
                ParamInfo.DataType = dgvFunctionParam.Rows[i].Cells[4].Value.ToString();
                ParamInfo.Value = dgvFunctionParam.Rows[i].Cells[5].Value.ToString();
                ParamInfo.IsConnect = (bool)((DataGridViewCheckBoxCell)dgvFunctionParam.Rows[i].Cells[6]).EditedFormattedValue;

                ParamInfo.MappingInfo = "";
                if (ParamInfo.DataType == HalconDataType.内部输出.ToString())
                {
                    string[] NameInfo = ParamInfo.Value.Split(',');
                    int FuncNameIndex = int.Parse(NameInfo[0]) - 1;
                    ParamInfo.MappingInfo = HalconEngineModel.SelectFuncNames[FuncNameIndex] +
                        "," + NameInfo[1];
                }


            }
        }

        //根据HalconEngineModel.lstParameters的值给表格赋值（给参数类型、值、是否连线 赋值）
        private void CompareValue(bool Sync = true)
        {
            for (int i = 0; i < dgvFunctionParam.Rows.Count; i++)
            {
                string funcName = dgvFunctionParam.Rows[i].Cells[1].Value.ToString();
                string paramName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString();
                ParameterInfo ParamInfo = HalconEngineModel.Parameters.FirstOrDefault(p =>
                p.FunctionName == funcName && p.ParamName == paramName);

                if (ParamInfo == null)
                {
                    ParameterInfo Info = new ParameterInfo
                    {
                        RowIndex = i.ToString(),
                        FuncIndex = dgvFunctionParam.Rows[i].Cells[0].Value.ToString(),
                        FunctionName = dgvFunctionParam.Rows[i].Cells[1].Value.ToString(),
                        InputOutputType = dgvFunctionParam.Rows[i].Cells[2].Value.ToString(),
                        ParamName = dgvFunctionParam.Rows[i].Cells[3].Value.ToString(),
                        DataType = dgvFunctionParam.Rows[i].Cells[4].Value.ToString(),
                        Value = dgvFunctionParam.Rows[i].Cells[5].Value.ToString(),
                        IsConnect = (bool)((DataGridViewCheckBoxCell)dgvFunctionParam.Rows[i].Cells[6]).EditedFormattedValue,
                    };
                    HalconEngineModel.Parameters.Insert(i, Info);
                }
                else
                {
                    string dataType = ParamInfo.DataType;
                    string inputOutType = ParamInfo.InputOutputType;

                    //根据输入输出类型，设置列表第6列的值
                    if (inputOutType == "输入")
                    {
                        if (dataType == HalconDataType.Region.ToString())
                        {
                            DataGridViewButtonCell btnSeting = new DataGridViewButtonCell
                            {
                                Value = "设置"
                            };
                            dgvFunctionParam.Rows[i].Cells[5] = btnSeting;
                        }
                        else if (dataType == HalconDataType.Bool.ToString())
                        {
                            DataGridViewComboBoxCell cbxBool = new DataGridViewComboBoxCell
                            {
                                Style = this.dgvFunctionParam.DefaultCellStyle
                            };
                            cbxBool.Items.Add("true");
                            cbxBool.Items.Add("false");
                            cbxBool.Value = ParamInfo.Value;
                            dgvFunctionParam.Rows[i].Cells[5] = cbxBool;
                        }
                        else if (dataType == HalconDataType.内部输出.ToString())
                        {
                            DataGridViewComboBoxCell cbxInnerOuput = GetInnerOutputItem(i);
                            int index = cbxInnerOuput.Items.IndexOf(ParamInfo.Value);
                            if (index != -1)
                            {
                                cbxInnerOuput.Value = ParamInfo.Value;
                            }
                            dgvFunctionParam.Rows[i].Cells[5] = cbxInnerOuput;
                        }
                        else
                        {
                            dgvFunctionParam.Rows[i].Cells[4].Value = ParamInfo.DataType;
                            dgvFunctionParam.Rows[i].Cells[5].Value = ParamInfo.Value;
                        }
                    }
                    else
                    {
                        dgvFunctionParam.Rows[i].Cells[5].Value = ParamInfo.Value;
                    }
                    dgvFunctionParam.Rows[i].Cells[4].Value = ParamInfo.DataType;
                    dgvFunctionParam.Rows[i].Cells[6].Value = ParamInfo.IsConnect;

                    //让顺序对齐
                    if (i.ToString() != ParamInfo.RowIndex)
                    {
                        HalconEngineModel.Parameters.Remove(ParamInfo);
                        HalconEngineModel.Parameters.Insert(i, ParamInfo);
                    }
                }
                dgvFunctionParam.Rows[i].Tag = HalconEngineModel.Parameters[i];
            }

            //删除多余的参数
            if (Sync == true)
            {
                int ParamCount = HalconEngineModel.Parameters.Count;
                int RowCount = dgvFunctionParam.Rows.Count;
                if (ParamCount > RowCount)
                {
                    HalconEngineModel.Parameters.RemoveRange(RowCount, ParamCount - RowCount);
                }
            }
        }

        private void CompareSelectedFuncName()
        {
            HalconEngineModel.SelectFuncNames = new List<string>(tvwSelectFuncList.Nodes.Count);
            for (int i = 0; i < tvwSelectFuncList.Nodes.Count; i++)
            {
                HalconEngineModel.SelectFuncNames[i] = tvwSelectFuncList.Nodes[i].Text.Substring(3);
            }
        }

        //根据已选函数，更新表格，此时表格的值都是默认值
        private void UpdateDataGridView(string HalconFilePath, List<string> SelectFuncNames)
        {
            //调用Halcon引擎读取Halcon文件会修改 Directory.GetCurrentDirectory() 的路径。先保存原来的值，
            //读取Halcon文件后再恢复原来的路径值
            string folderPath = Directory.GetCurrentDirectory();

            int FuncCount = SelectFuncNames.Count;
            dgvFunctionParam.Rows.Clear();
            for (int i = 0; i < FuncCount; i++)
            {
                try
                {
                    HDevProcedure Procedure = new HDevProcedure(HalconEngineModel.hDevProgram, SelectFuncNames[i]);

                    //当前函数图形变量、控制变量的输入
                    HTuple InputIconicParms = Procedure.GetInputIconicParamNames();
                    int InputIconicParmCount = InputIconicParms.TupleLength();
                    for (int j = 0; j < InputIconicParmCount; j++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输入", InputIconicParms.TupleSelect(j).S);
                    }

                    HTuple InputCtrlParms = Procedure.GetInputCtrlParamNames();
                    int InputCtrlParmCount = InputCtrlParms.TupleLength();
                    for (int j = 0; j < InputCtrlParmCount; j++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输入", InputCtrlParms.TupleSelect(j).S);
                    }

                    //当前函数图形变量、控制变量的输出
                    HTuple OutputIconicParms = Procedure.GetOutputIconicParamNames();
                    int OutputIconicParmCount = OutputIconicParms.TupleLength();
                    for (int k = 0; k < OutputIconicParmCount; k++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输出", OutputIconicParms.TupleSelect(k).S);
                    }

                    HTuple OutputCtrlParms = Procedure.GetOutputCtrlParamNames();
                    int OutputCtrlParmCount = OutputCtrlParms.TupleLength();
                    for (int k = 0; k < OutputCtrlParmCount; k++)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "输出", OutputCtrlParms.TupleSelect(k).S);
                    }

                    //无参数函数，一般不存在这种情况
                    if (InputIconicParmCount == 0 && InputCtrlParmCount == 0
                        && OutputIconicParmCount == 0 && OutputCtrlParmCount == 0)
                    {
                        AddNewRow((i + 1).ToString(), Procedure.Name, "-", "-");
                    }

                    //InnerOutputItems 增加建模函数生成的输出值
                }
                catch (Exception ex)
                {
                    SelectFuncNames.Remove(SelectFuncNames[i]);
                    continue;
                }
            }

            Directory.SetCurrentDirectory(folderPath);
        }

        private void AddNewRow(string FunIndex, string FunName, string InputOutputType, string ParmName)
        {
            DataGridViewButtonCell btnSeting = new DataGridViewButtonCell
            {
                Value = "设置"
            };

            DataGridViewComboBoxCell cbxParmType = new DataGridViewComboBoxCell
            {
                Style = this.dgvFunctionParam.DefaultCellStyle
            };
            foreach (string Name in Enum.GetNames(typeof(HalconDataType)))
            {
                cbxParmType.Items.Add(Name);
            }

            DataGridViewComboBoxCell cbxBool = new DataGridViewComboBoxCell()
            {
                Style = this.dgvFunctionParam.DefaultCellStyle
            };
            cbxBool.Items.Add("true");
            cbxBool.Items.Add("false");
            cbxBool.Value = "false";

            //序号、函数名、输入输出类型、参数名、值
            int rowIndex = dgvFunctionParam.Rows.Add();
            dgvFunctionParam.Rows[rowIndex].Cells[0].ReadOnly = true;
            dgvFunctionParam.Rows[rowIndex].Cells[1].ReadOnly = true;
            dgvFunctionParam.Rows[rowIndex].Cells[2].ReadOnly = true;
            dgvFunctionParam.Rows[rowIndex].Cells[3].ReadOnly = true;
            //设置cell0 、cell1、cell2
            dgvFunctionParam.Rows[rowIndex].Cells[0].Value = FunIndex;
            dgvFunctionParam.Rows[rowIndex].Cells[1].Value = FunName;
            dgvFunctionParam.Rows[rowIndex].Cells[2].Value = InputOutputType;
            if (InputOutputType == "-")
            {
                dgvFunctionParam.Rows[rowIndex].Cells[3].Value = "-";
                dgvFunctionParam.Rows[rowIndex].Cells[4].Value = "-";
                dgvFunctionParam.Rows[rowIndex].Cells[5].Value = "-";
                //dgvFunctionParam.Rows[rowIndex].Cells[4].ReadOnly = true;
                dgvFunctionParam.Rows[rowIndex].Cells[5].ReadOnly = true;
            }
            else
            {
                //设置cell3 、cell4
                dgvFunctionParam.Rows[rowIndex].Cells[3].Value = ParmName;
                dgvFunctionParam.Rows[rowIndex].Cells[4] = cbxParmType;
                string Mark = ParmName.Substring(0, 1);
                Dictionary<string, string> dnyParmType = new Dictionary<string, string>
                 {
                      {"S",HalconDataType.String.ToString() },
                      {"B",HalconDataType.Bool.ToString()   },
                      {"I",HalconDataType.Int.ToString()    },
                      //{"F",HalconDataType.Float.ToString()  },
                      {"D",HalconDataType.Double.ToString() },
                      //{"L",HalconDataType.Long.ToString()   },
                      {"T",HalconDataType.Image.ToString()  },
                      {"Q",HalconDataType.Region.ToString() },
                      {"Y",HalconDataType.XLD.ToString()}
                 };
                dnyParmType.TryGetValue(Mark, out string result);

                if (result != null)
                {
                    cbxParmType.Value = result;
                }
                else
                {
                    cbxParmType.Value = "Int";
                }

                //设置cell5
                if (InputOutputType == "输出")
                {
                    dgvFunctionParam.Rows[rowIndex].Cells[5].Value = "/";
                    dgvFunctionParam.Rows[rowIndex].Cells[5].ReadOnly = true;
                }
                else
                {
                    dgvFunctionParam.Rows[rowIndex].Cells[5].Value = 0;
                    if (Mark == "Q")
                    {
                        dgvFunctionParam.Rows[rowIndex].Cells[5] = btnSeting;
                    }
                    else if (Mark == "B")
                    {
                        dgvFunctionParam.Rows[rowIndex].Cells[5] = cbxBool;
                    }
                }
            }
        }



        private List<BaseHyROI> OriginalRoiData = new List<BaseHyROI>();  //用于记录原始ROI坐标数据
        private List<BaseHyROI> AffineRoiData = new List<BaseHyROI>();    //经过仿射变换后ROI坐标，用于显示
        private string strHeadLine = "";                                  //CSV文件第一行数据
        private string SelectRoiName = "";                                //当前展示ROI的名字

        //读取数据到OriginalRoiData，OriginalRoiData只是记录数据，不作为ROI显示
        private void ReadCsvData(string CsvFilePath)
        {
            string OneLine = "";
            string[] OneLineSplit = null;
            int OneLineLength = 0, RoiIndex = 0;
            bool IsFirstLine = false;

            try
            {
                OriginalRoiData.Clear();
                FileStream fs = new FileStream(CsvFilePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                while ((OneLine = sr.ReadLine()) != null)
                {
                    if (IsFirstLine == false)
                    {
                        strHeadLine = OneLine;
                        IsFirstLine = true;
                    }
                    else
                    {
                        OneLineSplit = OneLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        OneLineLength = OneLineSplit.Length;

                        BaseHyROI Roi = null;
                        switch (OneLineSplit[2])
                        {
                            case "Circle":
                                Roi = new HyCircle()
                                {
                                    Name = OneLineSplit[1],
                                    Center = new PointF { X = float.Parse(OneLineSplit[3]), Y = float.Parse(OneLineSplit[4]) },
                                    Radius = float.Parse(OneLineSplit[5])
                                };
                                break;

                            case "Rectangle1":
                                Roi = new HyRectangle1()
                                {
                                    Name = OneLineSplit[1],
                                    Center = new PointF(float.Parse(OneLineSplit[3]), float.Parse(OneLineSplit[4])),
                                    Width = float.Parse(OneLineSplit[5]),
                                    Height = float.Parse(OneLineSplit[6])
                                };
                                break;

                            case "Rectagnle2":
                                Roi = new HyRectangle2()
                                {
                                    Name = OneLineSplit[1],
                                    Center = new PointF(float.Parse(OneLineSplit[3]), float.Parse(OneLineSplit[4])),
                                    Angle = float.Parse(OneLineSplit[5]),
                                    Width = float.Parse(OneLineSplit[6]),
                                    Height = float.Parse(OneLineSplit[7])
                                };
                                break;

                            case "Polygon":
                                Roi = new HyPolygon() { Name = OneLineSplit[1] };
                                for (int i = 3; i < OneLineLength - 1; i += 2)
                                {
                                    ((HyPolygon)Roi).PolygonPoints.Add(new PointF(float.Parse(OneLineSplit[i]),
                                        float.Parse(OneLineSplit[i + 1])));
                                }
                                break;

                            case "Points":
                                Roi = new HyPoints() { Name = OneLineSplit[1] };
                                for (int i = 3; i < OneLineLength - 1; i += 2)
                                {
                                    ((HyPoints)Roi).RoiPoints.Add(new PointF(float.Parse(OneLineSplit[i]),
                                        float.Parse(OneLineSplit[i + 1])));
                                }
                                break;
                        }
                        if (Roi != null)
                        {
                            RoiIndex += 1;
                            Roi.Index = RoiIndex;
                            OriginalRoiData.Add(Roi);
                        }

                        //break;
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                if (OneLineLength >= 2)
                {
                    MessageBox.Show($"{ex}\r\n序号为 {OneLineSplit[0]}，名字为 {OneLineSplit[1]} 的数据有错误！", "ROI坐标文件数据有误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"{ex}\r\n数据有错误,请检查ROI坐标文件数据！", "ROI坐标文件数据有误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private int WriteCsvData(string CsvFilePath)
        {
            try
            {
                FileStream fileStream = new FileStream(CsvFilePath, FileMode.Truncate, FileAccess.ReadWrite);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                string strOneLine = "";
                //写表头栏
                streamWriter.WriteLine(strHeadLine);

                //float X1, Y1, X2, Y2, Angle;
                //写ROI数据
                for (int i = 0; i < tvwRoiList.Nodes.Count; i++)
                {
                    BaseHyROI roi = (BaseHyROI)tvwRoiList.Nodes[i].Tag;
                    strOneLine = $"{i + 1},{roi.Name},{roi.RoiType},";

                    switch (roi.RoiType)
                    {
                        case RoiType.Circle:
                            HyCircle circle = roi as HyCircle;
                            //简化写法，四舍五入保留3位小数
                            strOneLine += $"{circle.Center.X:f3},{circle.Center.Y:f3},{circle.Radius:f3}";
                            break;

                        case RoiType.Rectangle1:
                            HyRectangle1 rectangle1 = roi as HyRectangle1;
                            strOneLine += $"{rectangle1.Center.X:f3},{rectangle1.Center.Y:f3},{rectangle1.Width:f3},{rectangle1.Height:f3}";
                            break;

                        case RoiType.Rectangle2:
                            HyRectangle2 rectangle2 = roi as HyRectangle2;
                            strOneLine += $"{rectangle2.Center.X:f3},{rectangle2.Center.Y:f3},{rectangle2.Angle:f3},{rectangle2.Width:f3},{rectangle2.Height:f3}";
                            break;

                        case RoiType.Polygon:
                            HyPolygon polygon = roi as HyPolygon;
                            for (int j = 0; j < polygon.PolygonPoints.Count; j++)
                            {
                                strOneLine += $"{polygon.PolygonPoints[j].X:f3},{polygon.PolygonPoints[j].Y:f3}";

                                if (j < polygon.PolygonPoints.Count - 1)
                                {
                                    strOneLine += ",";
                                }
                            }
                            break;

                        case RoiType.Points:
                            HyPoints points = roi as HyPoints;
                            for (int j = 0; j < points.RoiPoints.Count; j++)
                            {
                                strOneLine += $"{points.RoiPoints[j].X:f3},{points.RoiPoints[j].Y:f3}";
                                if (j < points.RoiPoints.Count - 1)
                                {
                                    strOneLine += ",";
                                }
                            }
                            break;
                    }
                    streamWriter.WriteLine(strOneLine);
                }


                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}\r\n保存ROI坐标文件数据出错，保存失败！", "保存ROI坐标文件出错",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return 0;
        }

        private void UpdateRoiTreeView(List<BaseHyROI> lstRoiData)
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

        private void LoadFunctionList()
        {
            tvwFuncList.Nodes.Clear();
            tvwSelectFuncList.Nodes.Clear();
            TreeNode Tn1 = tvwFuncList.Nodes.Add("FirstLevel");
            TreeNode Tn2 = tvwFuncList.Nodes.Add("SecondLevel");
            TreeNode Tn3 = tvwFuncList.Nodes.Add("ThirdLevel");
            TreeNode Tn4 = tvwFuncList.Nodes.Add("Other");
            Tn1.ImageIndex = 0; Tn1.SelectedImageIndex = 0;
            Tn2.ImageIndex = 0; Tn2.SelectedImageIndex = 0;
            Tn3.ImageIndex = 0; Tn3.SelectedImageIndex = 0;
            Tn4.ImageIndex = 0; Tn4.SelectedImageIndex = 0;

            string[] FuncNames = HalconEngineModel.GetFuncNames();
            if (FuncNames != null)
            {
                TreeNode NewTn = null;
                for (int i = 0; i < FuncNames.Length; i++)
                {
                    if (FuncNames[i].EndsWith("01"))
                    {
                        NewTn = tvwFuncList.Nodes[0].Nodes.Add(FuncNames[i]);

                    }
                    else if (FuncNames[i].EndsWith("02"))
                    {
                        NewTn = tvwFuncList.Nodes[1].Nodes.Add(FuncNames[i]);
                    }
                    else if (FuncNames[i].EndsWith("03"))
                    {
                        NewTn = tvwFuncList.Nodes[2].Nodes.Add(FuncNames[i]);
                    }
                    else
                    {
                        NewTn = tvwFuncList.Nodes[3].Nodes.Add(FuncNames[i]);
                    }
                    NewTn.ImageIndex = 1; NewTn.SelectedImageIndex = 1;
                }
            }

        }

        private void LoadSelectedFunction(List<string> SelectFuncNames)
        {
            tvwSelectFuncList.Nodes.Clear();

            int cout = SelectFuncNames.Count;
            string index = "";
            for (int i = 0; i < cout; i++)
            {
                if (i <= 8)
                {
                    index = $"0{i + 1}.";
                }
                else
                {
                    index = $"{i + 1}.";
                }
                TreeNode NewTn = tvwSelectFuncList.Nodes.Add($"{index}{SelectFuncNames[i]}");
                NewTn.ImageIndex = 1; NewTn.SelectedImageIndex = 1;
            }
        }

        private void CreateAffineRoiData()
        {
            AffineRoiData.Clear();
            for (int i = 0; i < OriginalRoiData.Count; i++)
            {
                BaseHyROI dtnRoi = AffineTransform(OriginalRoiData[i]);
                if (dtnRoi != null)
                {
                    AffineRoiData.Add(dtnRoi);
                }

            }
        }

        private BaseHyROI AffineTransform(BaseHyROI SourceRoi)
        {
            BaseHyROI AffineRoi = null;
            float res = HalconEngineModel.PixelRes;
            float X1 = 0, Y1 = 0, X2 = 0, Y2 = 0, Angle = 0, Radius = 0;

            try
            {
                switch (SourceRoi.RoiType)
                {
                    case RoiType.Circle:
                        {
                            HyCircle srcCircle = SourceRoi as HyCircle;
                            X1 = srcCircle.Center.X / res;
                            Y1 = srcCircle.Center.Y / res;
                            Radius = srcCircle.Radius / res;
                            HOperatorSet.AffineTransPixel(HalconEngineModel.dnyCtrlVal["AffineHomMat"],
                                X1, Y1, out HTuple XX1, out HTuple YY1);

                            AffineRoi = new HyCircle()
                            {
                                Index = srcCircle.Index,
                                Name = srcCircle.Name,
                                Center = new PointF((float)YY1.D, (float)XX1.D),
                                Radius = Radius
                            };
                        }
                        break;

                    case RoiType.Rectangle1:
                        {
                            HyRectangle1 srcRectangle1 = SourceRoi as HyRectangle1;
                            X1 = srcRectangle1.Center.X / res;
                            Y1 = srcRectangle1.Center.Y / res;
                            X2 = srcRectangle1.Width / res;
                            Y2 = srcRectangle1.Height / res;

                            //XX1、YY1分别对应halcon坐标的Row和Column，对于C#来说则对应于Y坐标和X坐标。
                            HOperatorSet.AffineTransPixel(HalconEngineModel.dnyCtrlVal["AffineHomMat"],
                                X1, Y1, out HTuple XX1, out HTuple YY1);
                            HOperatorSet.AffineTransPixel(HalconEngineModel.dnyCtrlVal["AffineHomMat"],
                               X2, Y2, out HTuple XX2, out HTuple YY2);

                            AffineRoi = new HyRectangle1()
                            {
                                Index = srcRectangle1.Index,
                                Name = srcRectangle1.Name,
                                Center = new PointF((float)(YY1 + (YY2 - YY1) / 2).D, (float)(XX1 + (XX2 - XX1) / 2).D),
                                Width = Math.Abs((float)(YY2 - YY1).D),
                                Height = Math.Abs((float)(XX2 - XX1).D)
                            };
                        }
                        break;

                    case RoiType.Rectangle2:
                        {
                            HyRectangle2 srcRectangle2 = SourceRoi as HyRectangle2;
                            X1 = srcRectangle2.Center.X / res;
                            Y1 = srcRectangle2.Center.Y / res;
                            X2 = srcRectangle2.Width / res;
                            Y2 = srcRectangle2.Height / res;
                            Angle = srcRectangle2.Angle;

                            HOperatorSet.AffineTransPixel(HalconEngineModel.dnyCtrlVal["AffineHomMat"],
                                X1, Y1, out HTuple XX1, out HTuple YY1);

                            AffineRoi = new HyRectangle2()
                            {
                                Index = srcRectangle2.Index,
                                Name = srcRectangle2.Name,
                                Center = new PointF((float)YY1.D, (float)XX1.D),
                                Width = X2,
                                Height = Y2,
                                Angle = Angle
                            };
                        }
                        break;

                    case RoiType.Polygon:
                        {
                            HyPolygon srcPolygon = SourceRoi as HyPolygon;
                            AffineRoi = new HyPolygon()
                            {
                                Index = srcPolygon.Index,
                                Name = srcPolygon.Name
                            };

                            for (int j = 0; j < srcPolygon.PolygonPoints.Count; j++)
                            {
                                X1 = srcPolygon.PolygonPoints[j].X / res;
                                Y1 = srcPolygon.PolygonPoints[j].Y / res;
                                HOperatorSet.AffineTransPixel(HalconEngineModel.dnyCtrlVal["AffineHomMat"],
                              X1, Y1, out HTuple XX1, out HTuple YY1);

                                ((HyPolygon)AffineRoi).PolygonPoints.Add(new PointF((float)YY1.D, (float)XX1.D));
                            }
                        }
                        break;

                    case RoiType.Points:
                        {
                            HyPoints srcHyPoints = SourceRoi as HyPoints;
                            AffineRoi = new HyPoints()
                            {
                                Index = srcHyPoints.Index,
                                Name = srcHyPoints.Name
                            };

                            for (int j = 0; j < srcHyPoints.RoiPoints.Count; j++)
                            {
                                X1 = srcHyPoints.RoiPoints[j].X / res;
                                Y1 = srcHyPoints.RoiPoints[j].Y / res;
                                HOperatorSet.AffineTransPixel(HalconEngineModel.dnyCtrlVal["AffineHomMat"],
                              X1, Y1, out HTuple XX1, out HTuple YY1);

                                ((HyPoints)AffineRoi).RoiPoints.Add(new PointF((float)YY1.D, (float)XX1.D));
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                AffineRoi = null;
            }
            return AffineRoi;
        }

        private BaseHyROI InverseAffineTransform(BaseHyROI AffineRoi)
        {
            BaseHyROI SourceRoi = null;
            float res = HalconEngineModel.PixelRes;
            float X1 = 0, Y1 = 0, X2 = 0, Y2 = 0, Angle = 0, Radius = 0;

            try
            {
                HOperatorSet.HomMat2dInvert(HalconEngineModel.dnyCtrlVal["AffineHomMat"], out HTuple InvertAffineHomMat);

                switch (AffineRoi.RoiType)
                {
                    case RoiType.Circle:
                        {
                            HyCircle AffineCircle = AffineRoi as HyCircle;
                            X1 = AffineCircle.Center.X;
                            Y1 = AffineCircle.Center.Y;
                            Radius = AffineCircle.Radius;

                            HOperatorSet.AffineTransPixel(InvertAffineHomMat,
                               Y1, X1, out HTuple XX1, out HTuple YY1);

                            SourceRoi = new HyCircle()
                            {
                                Index = AffineCircle.Index,
                                Name = AffineCircle.Name,
                                Center = new PointF((float)(XX1.D * res), (float)(YY1.D * res)),
                                Radius = Radius * res
                            };
                        }
                        break;

                    case RoiType.Rectangle1:
                        {
                            HyRectangle1 AffineRectangle1 = AffineRoi as HyRectangle1;
                            X1 = (AffineRectangle1.Center.X - AffineRectangle1.Width / 2);
                            Y1 = (AffineRectangle1.Center.Y - AffineRectangle1.Height / 2);
                            X2 = (AffineRectangle1.Center.X + AffineRectangle1.Width / 2);
                            Y2 = (AffineRectangle1.Center.Y + AffineRectangle1.Height / 2);

                            HOperatorSet.AffineTransPixel(InvertAffineHomMat,
                               Y1, X1, out HTuple XX1, out HTuple YY1);
                            HOperatorSet.AffineTransPixel(InvertAffineHomMat,
                               Y2, X2, out HTuple XX2, out HTuple YY2);

                            XX1 *= res;
                            YY1 *= res;
                            XX2 *= res;
                            YY2 *= res;

                            SourceRoi = new HyRectangle1()
                            {
                                Index = AffineRectangle1.Index,
                                Name = AffineRectangle1.Name,
                                Center = new PointF((float)XX1.D, (float)YY1.D),
                                Width = (float)XX2.D,
                                Height = (float)YY2.D
                            };
                        }
                        break;

                    case RoiType.Rectangle2:
                        {
                            HyRectangle2 AffineRectangle2 = AffineRoi as HyRectangle2;
                            X1 = (AffineRectangle2.Center.X - AffineRectangle2.Width / 2);
                            Y1 = (AffineRectangle2.Center.Y - AffineRectangle2.Height / 2);
                            X2 = AffineRectangle2.Width * res;
                            Y2 = AffineRectangle2.Height * res;
                            Angle = AffineRectangle2.Angle;

                            HOperatorSet.AffineTransPixel(InvertAffineHomMat,
                               Y1, X1, out HTuple XX1, out HTuple YY1);

                            XX1 *= res;
                            YY1 *= res;

                            SourceRoi = new HyRectangle2()
                            {
                                Index = AffineRectangle2.Index,
                                Name = AffineRectangle2.Name,
                                Center = new PointF((float)XX1.D, (float)YY1.D),
                                Width = X2,
                                Height = Y2,
                                Angle = Angle
                            };
                        }
                        break;

                    case RoiType.Polygon:
                        {
                            HyPolygon AffinePolygon = AffineRoi as HyPolygon;
                            SourceRoi = new HyPolygon()
                            {
                                Index = AffinePolygon.Index,
                                Name = AffinePolygon.Name
                            };

                            for (int j = 0; j < AffinePolygon.PolygonPoints.Count; j++)
                            {
                                X1 = AffinePolygon.PolygonPoints[j].X;
                                Y1 = AffinePolygon.PolygonPoints[j].Y;
                                HOperatorSet.AffineTransPixel(InvertAffineHomMat,
                                   Y1, X1, out HTuple XX1, out HTuple YY1);

                                ((HyPolygon)SourceRoi).PolygonPoints.Add(new PointF((float)(XX1.D * res), (float)(YY1.D * res)));
                            }
                        }
                        break;

                    case RoiType.Points:
                        {
                            HyPoints AffineHyPoints = AffineRoi as HyPoints;
                            SourceRoi = new HyPoints()
                            {
                                Index = AffineHyPoints.Index,
                                Name = AffineHyPoints.Name
                            };

                            for (int j = 0; j < AffineHyPoints.RoiPoints.Count; j++)
                            {
                                X1 = AffineHyPoints.RoiPoints[j].X;
                                Y1 = AffineHyPoints.RoiPoints[j].Y;
                                HOperatorSet.AffineTransPixel(InvertAffineHomMat,
                                   Y1, X1, out HTuple XX1, out HTuple YY1);

                                ((HyPoints)SourceRoi).RoiPoints.Add(new PointF((float)(XX1.D * res), (float)(YY1.D * res)));
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                SourceRoi = null;
            }
            return SourceRoi;

        }

        public void EditSrcRoidataToDisplay(BaseHyROI SourceRoi)
        {
            if ((AffineRoiData.Count > 0))
            {
                hyImageDisplayControl1.GetHyROIRepository().SetSelected(false);
                BaseHyROI AffineRoi = AffineTransform(SourceRoi);
                AffineRoi.IsSelected = true;
                AffineRoiData.Remove(r => r.Index == AffineRoi.Index);
                AffineRoiData.Insert(AffineRoi.Index - 1, AffineRoi);
                hyImageDisplayControl1.DisplayControlInvalidate();
            }
            tvwRoiList.Focus();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "导出参数";
                saveFile.Filter = "XML文件(*.xml)|*.xml";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer xml = new XmlSerializer(typeof(HalconProgramEngineBL_2)); //, new XmlRootAttribute("AlgoRoot")
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.Write);
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xml.Serialize(fs, HalconEngineModel, ns);
                    fs.Close();
                }

                MessageBox.Show("成功导出参数！", "导出参数", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("导出参数失败！", "导出参数失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.Title = "导入参数";
            Openfile.Filter = "XML文件(*.xml)|*.xml";

            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(typeof(HalconProgramEngineBL_2));
                    FileStream fs = new FileStream(Openfile.FileName, FileMode.Open, FileAccess.Read);
                    HalconProgramEngineBL_2 HalconModel = (HalconProgramEngineBL_2)xml.Deserialize(fs);
                    fs.Close();


                    HalconEngineModel.SelectFuncNames = HalconModel.SelectFuncNames;
                    HalconEngineModel.Parameters = HalconModel.Parameters;
                   
                    LoadSelectedFunction(HalconEngineModel.SelectFuncNames);
                    dgvFunctionParam.CellValueChanged -= dgvFunctionParam_CellValueChanged;
                    UpdateDataGridView(tbxHalconFilePath.Text, HalconEngineModel.SelectFuncNames);
                    CompareValue();
                    dgvFunctionParam.CellValueChanged += dgvFunctionParam_CellValueChanged;
                }
                catch
                {
                    MessageBox.Show("导入参数失败！请检查导入文件是否正确。", "导出参数失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
