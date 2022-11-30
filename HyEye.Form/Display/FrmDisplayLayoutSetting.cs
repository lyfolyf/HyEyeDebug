using Autofac;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmDisplayLayoutSetting : Form
    {
        readonly IDisplayLayoutRepository layoutRepo;

        public FrmDisplayLayoutSetting(
            IDisplayLayoutRepository layoutRepo)
        {
            InitializeComponent();

            layoutPanel.SetDoubleBuffered();
            tvAcquireImage.HideCheckBox(n => n.Level == 0);

            this.layoutRepo = layoutRepo;
        }

        private void FrmMainSetting_Load(object sender, EventArgs e)
        {
            nudRowCount.Value = layoutRepo.RowCount;
            nudColumnCount.Value = layoutRepo.ColumnCount;
            ckbShowRetImage.Checked = layoutRepo.ShowRetImage;

            DisplayLayoutInfos = layoutRepo.Reset();

            Init();

            nudRowCount.ValueChanged += (sender1, e1) => Init();
            nudColumnCount.ValueChanged += (sender1, e1) => Init();
        }


        int maxCount = 0;
        int curIndex = 0;

        public List<DisplayLayoutInfoVO> DisplayLayoutInfos { get; set; }

        void Init()
        {
            initPanel(DisplayLayoutInfos);
            initTreeview(DisplayLayoutInfos);

            label1.Text = $"拍照总次数：{DisplayLayoutInfos.Count}  当前显示次数{curIndex}";
        }

        void initPanel(List<DisplayLayoutInfoVO> displayLayoutInfos)
        {
            int rowCount = (int)nudRowCount.Value;
            int columnCount = (int)nudColumnCount.Value;

            layoutPanel.Controls.Clear();

            layoutPanel.SetRowColumnCount(rowCount, columnCount);

            maxCount = rowCount * columnCount;
            curIndex = 0;

            foreach (DisplayLayoutInfoVO displayLayoutInfo in displayLayoutInfos)
            {
                if (curIndex < maxCount)
                {
                    if (displayLayoutInfo.Index > -1)
                    {
                        Label label = getLabel($"{displayLayoutInfo.TaskName}/{displayLayoutInfo.AcquireImageName}");

                        layoutPanel.Controls.Add(label, curIndex % columnCount, curIndex / columnCount);

                        curIndex++;
                    }
                }
                else
                {
                    displayLayoutInfo.Index = -1;
                }
            }
        }

        Label getLabel(string text)
        {
            return new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("宋体", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))),
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        void initTreeview(List<DisplayLayoutInfoVO> displayLayoutInfos)
        {
            tvAcquireImage.Nodes.Clear();

            var look = displayLayoutInfos.ToLookup(a => a.TaskName);

            foreach (var taskGroup in look)
            {
                TreeNode taskNode = new TreeNode(taskGroup.Key);
                tvAcquireImage.Nodes.Add(taskNode);

                foreach (DisplayLayoutInfoVO displayLayoutInfo in taskGroup)
                {
                    TreeNode acqImageNode = new TreeNode(displayLayoutInfo.AcquireImageName)
                    {
                        Checked = displayLayoutInfo.Index > -1
                    };

                    taskNode.Nodes.Add(acqImageNode);
                }
            }

            tvAcquireImage.ExpandAll();
        }

        private void tvAcquireImage_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!e.Node.Checked && curIndex == maxCount)
            {
                MessageBoxUtils.ShowInfo("已经没有显示区域显示了，请调整行列增加显示区域");
                e.Cancel = true;
            }
        }

        private void tvAcquireImage_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string acqImageName = e.Node.Text;
            string taskName = e.Node.Parent.Text;

            DisplayLayoutInfoVO layoutInfo = DisplayLayoutInfos.First(a => a.TaskName == taskName && a.AcquireImageName == acqImageName);

            layoutInfo.Index = e.Node.Checked ? 1 : -1;

            initPanel(DisplayLayoutInfos);

            label1.Text = $"拍照总次数：{DisplayLayoutInfos.Count}  当前显示次数{curIndex}";
        }

        bool changedToken = false;

        private void btnSave_Click(object sender, EventArgs e)
        {
            layoutRepo.RowCount = (int)nudRowCount.Value;
            layoutRepo.ColumnCount = (int)nudColumnCount.Value;
            layoutRepo.ShowRetImage = ckbShowRetImage.Checked;
            layoutRepo.SetLayout(DisplayLayoutInfos);

            layoutRepo.Save();

            changedToken = true;

            Close();
        }

        private void FrmDisplayLayoutSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (changedToken)
                DialogResult = DialogResult.OK;
        }

        private void btnAdvancedSetting_Click(object sender, EventArgs e)
        {
            FrmLayoutAdvancedSetting frm = AutoFacContainer.Resolve<FrmLayoutAdvancedSetting>();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                changedToken = true;
            }
        }

        private void btnSetOutPut_Click(object sender, EventArgs e)
        {
            FrmSetOutput frmSetOutput = AutoFacContainer.Resolve<FrmSetOutput>();
            frmSetOutput.StartPosition = FormStartPosition.CenterScreen;
            frmSetOutput.ShowDialog();

        }
    }
}
