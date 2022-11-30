using HyEye.WForm.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm
{
    public partial class FrmAOIResultDisplay : DockContent
    {

        [Serializable]
        internal class FormConfig
        {
            private List<SurfaceInfo> surfaceInfos = new List<SurfaceInfo>();
            internal List<string> SurfaceList
            {
                get
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < surfaceInfos.Count; i++)
                    {
                        list.Add(surfaceInfos[i].Name);
                    }
                    return list;
                }
            }

            internal List<SurfaceInfo> SurfaceInfos
            {
                get { return surfaceInfos; }
                set
                {
                    surfaceInfos = value;
                }
            }

            internal SurfaceInfo GetSurface(string name)
            {
                if (surfaceInfos.Count(k => k.Name == name) == 0)
                {
                    return null;
                }
                return surfaceInfos.Find(k => k.Name == name);
            }
            internal int GetIndex(string surface)
            {
                for (int i = 0; i < surfaceInfos.Count; i++)
                {
                    if (surfaceInfos[i].Name == surface)
                    {
                        return i;
                    }
                }
                return -1;
            }
            internal SurfaceInfo GetSurface(int index)
            {
                if (index < 0 || index >= surfaceInfos.Count)
                {
                    return null;
                }
                return surfaceInfos[index];
            }
            internal void SetSurfaceModelPath(string surface, string path)
            {
                SurfaceInfo surfaceInfo;
                if (surfaceInfos.Count(v => v.Name == surface) == 0)
                {
                    surfaceInfo = new SurfaceInfo { Name = surface, SurfaceModelPath = path };
                    surfaceInfos.Add(surfaceInfo);
                }
                surfaceInfo = surfaceInfos.Find(v => v.Name == surface);
                surfaceInfo.SurfaceModelPath = path;
            }

            internal string GetSurfaceModelPath(string surface)
            {
                SurfaceInfo surfaceInfo;
                if (surfaceInfos.Count(v => v.Name == surface) == 0)
                {
                    return string.Empty;
                }
                surfaceInfo = surfaceInfos.Find(v => v.Name == surface);
                return surfaceInfo.SurfaceModelPath;
            }
            //internal void SetBlockPath(string surface, int index, string path)
            //{
            //    SurfaceInfo surfaceInfo;
            //    if (surfaceInfos.Count(v => v.Name == surface) == 0)
            //    {
            //        return;
            //        //surfaceInfo = new SurfaceInfo { Name = surface, SurfaceModelPath = path };
            //        //surfaceInfos.Add(surfaceInfo);
            //    }
            //    surfaceInfo = surfaceInfos.Find(v => v.Name == surface);
            //    surfaceInfo.SetModelPath(index, path);
            //}

            //internal string GetBlockPath(string surface, int index)
            //{
            //    SurfaceInfo surfaceInfo;
            //    if (surfaceInfos.Count(v => v.Name == surface) == 0)
            //    {
            //        return string.Empty;
            //    }
            //    surfaceInfo = surfaceInfos.Find(v => v.Name == surface);
            //    return surfaceInfo.GetModelPath(index);
            //}

            internal void SaveConfig()
            {
                var path = $@"{Environment.CurrentDirectory}";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                using (FileStream fileStream = new FileStream($@"{path}\AoiFormConfig.cfg", FileMode.Create))
                {
                    // 用二进制格式序列化
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, this);
                    fileStream.Close();
                }
            }
            internal static FormConfig LoadConfig()
            {
                var path = $@"{Environment.CurrentDirectory}\AoiFormConfig.cfg";
                FormConfig cfg = new FormConfig();
                if (!System.IO.File.Exists(path))
                {
                    cfg.SaveConfig();
                }
                System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cfg = (FormConfig)formatter.Deserialize(stream);
                    stream.Close();
                }
                return cfg;
            }
        }
        enum Surface
        {
            TopCase,
            LCM,
            Mandrel,
            DisplayHousing,
            BottomCase,
            Side,
            Corner
        }

        //string selectedSurface = string.Empty;
        FormConfig formConfig = new FormConfig();
        Dictionary<string, SurfaceResultInfo> resultsDict = new Dictionary<string, SurfaceResultInfo>();
        List<DefectInfo> defects = new List<DefectInfo>();

        public List<DefectInfo> Defects
        {
            get { return defects; }
            set
            {
                defects = value;
            }
        }

        public FrmAOIResultDisplay()
        {
            InitializeComponent();
            formConfig = FormConfig.LoadConfig();
            Clear();
            SetPreviewPages();
        }
        public void Push(BlockResultInfo resultInfo)
        {
            //this.Invoke(new Action(() =>
            //{
            if (resultsDict.ContainsKey(resultInfo.Surface))
            {
                resultsDict[resultInfo.Surface].Push(resultInfo);
                defects.AddRange(resultInfo.Defects);
                dgvDefects.DataSource = null;
                dgvDefects.DataSource = defects;
                var count = resultsDict[resultInfo.Surface].DefectCount(resultInfo.Block);
                ShowBlockInfo(resultInfo.Surface, resultInfo.Block, count.ToString());
            }

            //}));
        }

        private BlockResultInfo GetBlockResultInfo(DefectInfo defectInfo)
        {
            var surfaceInfo = resultsDict[defectInfo.Surface];
            return surfaceInfo.GetBlockResultInfo(defectInfo);
        }
        private List<BlockResultInfo> GetBlockResultInfos(string surface, string block)
        {
            var surfaceInfo = resultsDict[surface];
            return surfaceInfo.GetBlockResultInfos(block);
        }
        private BlockResultInfo GetBlockResultInfo(string surface, string block, string light)
        {
            var surfaceInfo = resultsDict[surface];
            var list = surfaceInfo.GetBlockResultInfos(block);
            return list.Find(k => k.Light == light);
        }

        /// <summary>
        /// 当有新的产品结果输入时，需要清空前产品结果信息
        /// </summary>
        public void Clear()
        {
            foreach (var item in resultsDict)
            {
                item.Value.Reset();
            }
            dgvDefects.DataSource = null;

            lblDefectArea.Text = string.Empty;
            lblDefectBlock.Text = string.Empty;
            lblDefectCamera.Text = string.Empty;
            lblDefectData.Text = string.Empty;
            lblDefectLight.Text = string.Empty;
            lblDefectSurface.Text = string.Empty;
            lblDefectType.Text = string.Empty;
            lblDate.Text = string.Empty;
            lblProductModel.Text = string.Empty;
            lblProductSn.Text = string.Empty;
            lblProductSurface.Text = string.Empty;
            lblDefectCount.Text = string.Empty;
            lblDefectImage.Text = string.Empty;
            lblTotalArea.Text = string.Empty;
            hyDpModel.ClearImage();
            hyDpCurrent.ClearImage();
            hyDpCurrent.ClearGraphic();
            GC.Collect();
        }

        private void FrmAOIResultDisplay_Load(object sender, EventArgs e)
        {

        }


        private void FrmAOIResultDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            formConfig.SaveConfig();
        }

        private void tabPreview_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (formConfig.SurfaceList.Count < tabPreview.SelectedIndex + 1 || tabPreview.SelectedIndex < 0)
            {
                return;
            }
            var selectedSurface = formConfig.SurfaceList[tabPreview.SelectedIndex];
            SurfaceResultInfo surfaceResultInfo = resultsDict[selectedSurface];
            ShowSurfaceInfo(surfaceResultInfo);
        }

        private void linkLblSurfaceConfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSurfacesEdit frmSurfacesEdit = new FrmSurfacesEdit(this.formConfig.SurfaceInfos);
            frmSurfacesEdit.NameList = Enum.GetNames(typeof(Surface)).ToList();
            frmSurfacesEdit.ListChangedAct = (list) =>
            {
                this.formConfig.SurfaceInfos = (List<SurfaceInfo>)list;
                SetPreviewPages();
                dgvDefects.DataSource = null;
            };
            frmSurfacesEdit.ShowDialog();
        }

        private void SetPreviewPages()
        {
            resultsDict = new Dictionary<string, SurfaceResultInfo>();
            tabPreview.TabPages.Clear();
            foreach (var item in this.formConfig.SurfaceInfos)
            {
                TabPage page = new TabPage() { Text = item.Name, Name = item.Name };
                UcAreaSelect ucAreaSelect = new UcAreaSelect
                {
                    Name = item.Name,
                    VCount = item.VCount,
                    HCount = item.HCount,
                    Dock = DockStyle.Fill
                };
                foreach (var areaInfo in item.AreaInfos)
                {
                    areaInfo.Info = string.Empty;
                }
                ucAreaSelect.SetAreaInfos(item.AreaInfos);
                if (File.Exists(item.SurfaceModelPath))
                {
                    Image image = Image.FromFile(item.SurfaceModelPath);
                    ucAreaSelect.BackgroundImage = image;
                }
                ucAreaSelect.AeraClick += UcAreaSelect_AeraClick;
                page.Controls.Add(ucAreaSelect);
                tabPreview.TabPages.Add(page);
                resultsDict.Add(item.Name, new SurfaceResultInfo());
            }
        }

        private void UcAreaSelect_AeraClick(object sender, AreaSelectEventArgs e)
        {
            hyDpModel.ClearImage();
            hyDpCurrent.ClearImage();
            hyDpCurrent.ClearGraphic();

            for (int i = 0; i < dgvDefects.Rows.Count; i++)
            {
                dgvDefects.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            var block = e.Block;
            string surface = ((UcAreaSelect)sender).Name;

            var path = formConfig.GetSurface(surface).AreaInfos.Find(k => k.Block == block).ModelPath;
            if (File.Exists(path))
            {
                Bitmap bitmap = new Bitmap(path);
                hyDpModel.Image = bitmap;
            }
            var blockInfos = GetBlockResultInfos(surface, block);
            foreach (var blockInfo in blockInfos)
            {
                if (blockInfo != null)
                {
                    hyDpCurrent.Image = blockInfo.Image;
                    hyDpCurrent.ClearGraphic();
                    int first = 0;
                    foreach (var item in blockInfo.Defects)
                    {
                        hyDpCurrent.AddROI(item.ROI);
                        for (int i = 0; i < defects.Count; i++)
                        {
                            if (defects[i] == item)
                            {
                                first = first == 0 ? i : first;
                                dgvDefects.Rows[i].DefaultCellStyle.BackColor = Color.Bisque;
                                break;
                            }
                        }
                    }
                    dgvDefects.FirstDisplayedScrollingRowIndex = first;
                    hyDpCurrent.SelecedtIndex = -1;
                    dgvDefects.CurrentCell = null;
                }
            }
        }

        private void ShowBlockInfo(string surface, string block, string blockInfo = "")
        {
            var tabpage = tabPreview.FindPage(k => k.Text == surface);
            tabPreview.SelectedTab = tabpage;
            foreach (var ctr in tabpage.Controls)
            {
                if (ctr is UcAreaSelect)
                {
                    UcAreaSelect ucAreaSelect = ctr as UcAreaSelect;
                    ucAreaSelect.SetActivateArea(block, true, blockInfo);
                    break;
                }
            }
        }

        private void dgvDefects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hyDpModel.ClearImage();
            hyDpCurrent.ClearImage();
            hyDpCurrent.ClearGraphic();
            int row = e.RowIndex;
            if (row < 0)
                return;
            var defectInfo = defects[row];
            var blockInfo = GetBlockResultInfo(defectInfo);
            hyDpCurrent.Image = blockInfo.Image;
            hyDpCurrent.Show(defectInfo.ROI);
            ShowDefectDetail(defectInfo);
            string surface = blockInfo.Surface;

            ShowBlockInfo(surface, blockInfo.Block);
            var path = formConfig.GetSurface(surface).AreaInfos.Find(k => k.Block == blockInfo.Block).ModelPath;
            if (File.Exists(path))
            {
                Bitmap bitmap = new Bitmap(path);
                hyDpModel.Image = bitmap;
            }
            var surfaceResultInfo = resultsDict[surface];
            ShowSurfaceInfo(surfaceResultInfo);
            lblDefectImage.Text = blockInfo.ImageName;
        }
        private void ShowDefectDetail(DefectInfo defectInfo)
        {
            lblDefectArea.Text = defectInfo.Area.ToString();
            lblDefectBlock.Text = defectInfo.Block.ToString();
            lblDefectCamera.Text = defectInfo.Camera.ToString();
            lblDefectData.Text = defectInfo.AlgorithmData;
            lblDefectLight.Text = defectInfo.Light;
            lblDefectSurface.Text = defectInfo.Surface;
            lblDefectType.Text = defectInfo.DefectType;
            hyDpDefect.Image = defectInfo.Image;
        }

        private void ShowSurfaceInfo(SurfaceResultInfo surfaceResultInfo)
        {
            lblDate.Text = surfaceResultInfo.Date;
            lblProductModel.Text = surfaceResultInfo.PruductModel;
            lblProductSn.Text = surfaceResultInfo.SN;
            lblProductSurface.Text = surfaceResultInfo.Surface;
            lblDefectCount.Text = surfaceResultInfo.Defects.Count.ToString();
            lblTotalArea.Text = surfaceResultInfo.DefectTotalArea.ToString();
        }
    }
}
