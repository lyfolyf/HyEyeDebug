using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;
using WeifenLuo.WinFormsUI.Docking;

namespace HyEye.WForm
{
    public partial class FrmDisplay : DockContent
    {
        readonly IDisplayLayoutRepository layoutRepo;
        readonly DisplayComponentSet displayControlSet;

        public FrmDisplay(IDisplayLayoutRepository layoutRepo,
            DisplayComponentSet displayControlSet)
        {
            InitializeComponent();

            tableLayoutPanel2.SetDoubleBuffered();

            this.layoutRepo = layoutRepo;
            this.displayControlSet = displayControlSet;

            layoutRepo.LayoutChanged += AfterLayoutChanged;
        }

        private void AfterLayoutChanged()
        {
            // 这刷新做得太复杂了，强制要求了事件加载的顺序，故而拍照做成默认不显示更好
            RefreshDisplay();
        }

        private void FrmDisplay_Load(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            int rowCount = layoutRepo.RowCount;
            int colCount = layoutRepo.ColumnCount;

            DisplayLayout(rowCount, colCount);

            BindDisplay(rowCount, colCount);
        }

        void DisplayLayout(int rowCount, int colCount)
        {
            panel1.Controls.Clear();
            tableLayoutPanel2.Controls.Clear();

            if (layoutRepo.AutoSize)
            {
                tableLayoutPanel2.Dock = DockStyle.Fill;

                tableLayoutPanel2.SetRowColumnCount(rowCount, colCount);
            }
            else
            {
                tableLayoutPanel2.Dock = DockStyle.None;
                tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);

                tableLayoutPanel2.Width = colCount * layoutRepo.Width;
                tableLayoutPanel2.Height = rowCount * layoutRepo.Height;

                tableLayoutPanel2.SetRowColumnCount(rowCount, colCount, layoutRepo.Width, layoutRepo.Height);
            }
        }

        void BindDisplay(int rowCount, int colCount)
        {
            List<DisplayLayoutInfoVO> displayLayoutInfos = layoutRepo.Reset();
            int maxCount = rowCount * colCount;

            foreach (DisplayLayoutInfoVO layoutInfo in displayLayoutInfos)
            {
                IDisplayTaskImageComponent displayControl = displayControlSet.GetDisplayControl(layoutInfo.TaskName, layoutInfo.AcquireImageName);

                if (layoutInfo.Index > -1 && layoutInfo.Index < maxCount)
                {
                    displayControl.ShowSelectOutput -= DisplayControl_DoubleClick;
                    displayControl.ShowSelectOutput += DisplayControl_DoubleClick;

                    displayControl.DisplayedControl.Dock = DockStyle.Fill;
                    tableLayoutPanel2.Controls.Add(displayControl.DisplayedControl, layoutInfo.Index % colCount, layoutInfo.Index / colCount);
                    displayControl.Visible = true;

                    displayControl.DisplayedControl.SendToBack();
                }
                else
                {
                    // 保存结果图的时候，需要用到 CogDisplay
                    // 如果此控件不显示一下，会报：在创建窗口句柄之前,不能在控件上调用 Invoke 或 BeginInvoke
                    panel1.Controls.Add(displayControl.DisplayedControl);
                    displayControl.Visible = false;
                    //displayControl.DisplayedControl.BringToFront();
                }
            }
        }

        private void DisplayControl_DoubleClick(object sender, EventArgs e)
        {
            IDisplayTaskImageComponent displayControl = (IDisplayTaskImageComponent)sender;

            if (tableLayoutPanel2.RowCount == 1 && tableLayoutPanel2.ColumnCount == 1)
            {
                RefreshDisplay();
            }
            else
            {
                tableLayoutPanel2.SetRowColumnCount(1, 1);
                tableLayoutPanel2.Controls.Add(displayControl.DisplayedControl, 0, 0);
            }
        }
    }
}
