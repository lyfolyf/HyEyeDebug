using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.WForm.Display
{
    internal partial class FrmSurfacesEdit : Form
    {
        public FrmSurfacesEdit(IList<SurfaceInfo> surfaces)
        {
            InitializeComponent();
            Surfaces = surfaces;
        }
        internal Action<IList<SurfaceInfo>> ListChangedAct;
        IList<SurfaceInfo> surfaces;
        public IList NameList
        {
            set
            {
                cbSurfaces.DataSource = value;
            }
        }

        internal IList<SurfaceInfo> Surfaces
        {
            get { return surfaces; }
            set
            {
                surfaces = value;
                listBox1.DataSource = null;
                listBox1.DataSource = surfaces;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            SurfaceInfo surfaceInfo = new SurfaceInfo { Name = cbSurfaces.Text, VCount = (int)numericUdVcount.Value, HCount = (int)numericUdHcount.Value };
            Surfaces.Add(surfaceInfo);
            listBox1.DataSource = null;
            listBox1.DataSource = surfaces;
            listBox1.Refresh();
            ListChangedAct?.Invoke(surfaces);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("未选中任何项！");
                return;
            }
            surfaces.RemoveAt(listBox1.SelectedIndex);
            listBox1.DataSource = null;
            listBox1.DataSource = surfaces;
            listBox1.Refresh();
            ListChangedAct?.Invoke(surfaces);
        }

        private void btnBlockEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("未选中任何项！");
                return;
            }
            var surface = surfaces[listBox1.SelectedIndex];
            FrmSetAreaInfo frmSetAreaInfo = new FrmSetAreaInfo(surface.VCount, surface.HCount);
            if (frmSetAreaInfo.ShowDialog() == DialogResult.OK)
            {
                var areaInfo = frmSetAreaInfo.AreaInfo;
                if (surfaces[listBox1.SelectedIndex].AreaInfos.Count(k => k.Block == areaInfo.Block) > 0)
                {
                    surfaces[listBox1.SelectedIndex].AreaInfos.Remove(k => k.Block == areaInfo.Block);
                }
                surfaces[listBox1.SelectedIndex].AreaInfos.Add(frmSetAreaInfo.AreaInfo);
                ListChangedAct?.Invoke(surfaces);
            }
        }

        private void btnSetPreviewImage_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("未选中任何项！");
                return;
            }
            var surface = surfaces[listBox1.SelectedIndex];
            FrmLoadImage frmLoadImage = new FrmLoadImage();
            if (frmLoadImage.ShowDialog() == DialogResult.OK)
            {
                surface.SurfaceModelPath = frmLoadImage.Path;
                ListChangedAct?.Invoke(surfaces);
            }
        }
    }
}
