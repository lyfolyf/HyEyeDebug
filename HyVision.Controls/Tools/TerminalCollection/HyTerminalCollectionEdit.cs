using HyVision.Models;
using HyVision.Tools.ImageDisplay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HyVision.Tools.TerminalCollection
{
    public partial class HyTerminalCollectionEdit : UserControl
    {
        public Action<HyTerminal> TerminalAdded;
        public Action<string> TerminalDeleted;

        static readonly Type IntType = typeof(int);
        static readonly Type DoubleType = typeof(double);
        static readonly Type StringType = typeof(string);
        static readonly Type DatetimeType = typeof(DateTime);
        static readonly Type ImageType = typeof(HyImage);

        //add by LuoDian @ 20210805 添加ROI类型的输出，用于在输出图像中显示ROI
        static readonly Type ROIType = typeof(BaseHyROI);
        //add by LuoDian @ 20211101 添加HRegion、XLD及 List<HyDefectXLD>类型的输出，用于在输出图像中显示ROI
        static readonly Type HRegionType = typeof(HyDefectRegion);
        static readonly Type XLDType = typeof(HyDefectXLD);
        static readonly Type ListHyDefectXLD = typeof(List<HyDefectXLD>);
        //add by LuoDian @ 20220118 添加Bitmap
        static readonly Type BitmapType = typeof(Bitmap);

        //add by LuoDian @ 20220220 添加List<int>
        static readonly Type ListInt = typeof(List<int>);
        //add by LuoDian @ 20220220 添加List<Double>
        static readonly Type ListDouble = typeof(List<double>);
        //add by LuoDian @ 20220220 添加List<String>
        static readonly Type ListString = typeof(List<string>);
        //add by LuoDian @ 20220220 添加List<HyImage>
        static readonly Type ListHyImage = typeof(List<HyImage>);
        //add by LuoDian @ 20220220 添加List<Bitmap>
        static readonly Type ListBitmap = typeof(List<Bitmap>);


        public HyTerminalCollectionEdit()
        {
            InitializeComponent();
        }

        private void HyTerminalCollectionEdit_Load(object sender, EventArgs e)
        {
            tstbText.Size = new Size(toolStripInput.Width - tsBtnAdd.Width - tsBtnDelete.Width * 3 - 5, tstbText.Size.Height);
        }

        public string MaterialSubName { get; set; }

        [Browsable(true)]
        public string DefaultNameHeader { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public override string Text
        {
            get => tstbText.Text;
            set => tstbText.Text = value;
        }

        HyTerminalCollection terminals;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HyTerminalCollection Subject
        {
            get { return terminals; }
            set
            {
                if (terminals == null && value == null) return;

                if (!object.Equals(terminals, value))
                {
                    if (terminals != null)
                    {
                        terminals.Inserted -= Terminals_Inserted;
                        terminals.Removed -= Terminals_Removed;
                        terminals.ItemValueChanged -= Terminals_ItemValueChanged;
                        terminals.MovedItem -= Terminals_Moved;
                        terminals.Cleared -= Terminals_Cleared;
                    }

                    terminals = value;
                    terminals.Inserted += Terminals_Inserted;
                    terminals.Removed += Terminals_Removed;
                    terminals.ItemValueChanged += Terminals_ItemValueChanged;
                    terminals.MovedItem += Terminals_Moved;
                    terminals.Cleared += Terminals_Cleared;

                    dgvTerminals.Rows.Clear();
                    foreach (HyTerminal terminal in terminals)
                    {
                        ShowTerminal(terminal, false);
                    }

                    dgvTerminals.CellValueChanged -= dgvTerminals_CellValueChanged;
                    dgvTerminals.CellValueChanged += dgvTerminals_CellValueChanged;
                }
            }
        }

        private void Terminals_Inserted(object sender, CollectionItemEventArgs e)
        {
            HyTerminal terminal = (HyTerminal)e.Value;

            ShowTerminal(terminal, true);
        }

        private void Terminals_Removed(object sender, CollectionItemEventArgs e)
        {
            dgvTerminals.Rows.RemoveAt(e.Index);
        }

        private void Terminals_ItemValueChanged(object sender, CollectionItemEventArgs e)
        {
            ignoreValueChange = true;

            HyTerminal terminal = (HyTerminal)e.Value;
            DataGridViewRow row = dgvTerminals.Rows[e.Index];
            row.Cells[0].Value = terminal.Name;
            row.Cells[2].Value = terminal.Value;
            row.Cells[3].Value = terminal.Description;

            ignoreValueChange = false;
        }

        private void Terminals_Moved(object sender, CollectionItemMoveEventArgs e)
        {
            DataGridViewRow row = dgvTerminals.Rows[e.FromIndex];
            dgvTerminals.Rows.RemoveAt(e.FromIndex);
            dgvTerminals.Rows.Insert(e.ToIndex, row);

            row.Selected = true;
            if (dgvTerminals.CurrentCell != null)
                dgvTerminals.CurrentCell = row.Cells[dgvTerminals.CurrentCell.ColumnIndex];
        }

        private void Terminals_Cleared(object sender, EventArgs e)
        {
            dgvTerminals.Rows.Clear();
        }

        private void tsmiAddInt_Click(object sender, EventArgs e) => AddTerminal(IntType);

        private void tsmiAddDouble_Click(object sender, EventArgs e) => AddTerminal(DoubleType);

        private void tsmiAddString_Click(object sender, EventArgs e) => AddTerminal(StringType);

        private void tsmiAddDatetime_Click(object sender, EventArgs e) => AddTerminal(DatetimeType);

        private void tsmiAddHyImage_Click(object sender, EventArgs e) => AddTerminal(ImageType);

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTerminals.CurrentCell == null) return;

            if (MessageBoxUtils.ShowQuestion("确定要删除所选项目吗？") == DialogResult.No) return;

            var terminal = terminals[dgvTerminals.CurrentRow.Index];
            TerminalDeleted?.Invoke(terminal.Name);
            terminals.RemoveAt(dgvTerminals.CurrentRow.Index);
        }

        private void tsBtnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvTerminals.CurrentCell == null) return;

            terminals.MoveUp(dgvTerminals.CurrentRow.Index);
        }

        private void tsBtnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvTerminals.CurrentCell == null) return;

            terminals.MoveDown(dgvTerminals.CurrentRow.Index);
        }

        int index = 1;

        void AddTerminal(Type type)
        {
            string name = GetDefaltName();

            var terminal = new HyTerminal(name, type);
            terminal.MaterialSubName = MaterialSubName;
            terminals.Add(new HyTerminal(name, type));
            TerminalAdded?.Invoke(terminal);
        }

        public string GetDefaltName()
        {
            string name;
            do
            {
                name = DefaultNameHeader + index++.ToString();
            } while (terminals.Contains(name));
            return name;
        }

        void ShowTerminal(HyTerminal terminal, bool selected)
        {
            string typeName = terminal.ValueType?.Name;
            if (typeName.StartsWith("List"))
            {
                typeName = string.Format("List<{0}>", terminal.ValueType?.GenericTypeArguments[0].Name);
            }

            int index = dgvTerminals.Rows.Add(terminal.Name, typeName, terminal.Value, terminal.Description);
            if (selected)
            {
                dgvTerminals.Rows[index].Selected = true;
            }
        }

        bool clearErrorMsg = false;
        bool ignoreValueChange = false;

        private void dgvTerminals_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && terminals[e.RowIndex].ValueType == ImageType)
            {
                e.Cancel = true;
                return;
            }

            if (clearErrorMsg)
            {
                if (dgvTerminals.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText != null)
                    dgvTerminals.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
            }
        }

        private void dgvTerminals_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreValueChange) return;

            if (e.RowIndex == -1) return;

            HyTerminal terminal = terminals[e.RowIndex];
            if (e.ColumnIndex == 0)
            {
                DataGridViewCell nameCell = dgvTerminals.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string name = nameCell.Value?.ToString().Trim();

                if (name == null || name.Length == 0)
                {
                    nameCell.ErrorText = "名称不能为空";

                    ignoreValueChange = true;
                    nameCell.Value = terminal.Name;
                    ignoreValueChange = false;

                    clearErrorMsg = false;
                    dgvTerminals.BeginEdit(false);
                    clearErrorMsg = true;
                }
                else if (terminals.Contains(name))
                {
                    nameCell.ErrorText = "名称已存在";

                    ignoreValueChange = true;
                    nameCell.Value = terminal.Name;
                    ignoreValueChange = false;

                    clearErrorMsg = false;
                    dgvTerminals.BeginEdit(false);
                    clearErrorMsg = true;
                }
                else
                {
                    terminal.Name = name;
                }
            }
            else if (e.ColumnIndex == 2)
            {
                terminal.Value = dgvTerminals.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ChanageType(terminal.ValueType);
            }
            else if (e.ColumnIndex == 3)
            {
                terminal.Description = dgvTerminals.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            }
        }

        //add by LuoDian @ 20210805 添加ROI类型的输出，用于在输出图像中显示ROI
        private void tsmiAddHyROI_Click(object sender, EventArgs e) => AddTerminal(ROIType);
        //add by LuoDian @ 20211101 添加HRegion及XLD类型的输出，用于在输出图像中显示ROI
        private void tsmiAddHRegion_Click(object sender, EventArgs e) => AddTerminal(HRegionType);
        private void tsmiAddXLD_Click(object sender, EventArgs e) => AddTerminal(XLDType);
        private void tsmiAddListHyDefectXLD_Click(object sender, EventArgs e) => AddTerminal(ListHyDefectXLD);
        //add by LuoDian @ 20220118 添加Bitmap
        private void tsmiAddBitmap_Click(object sender, EventArgs e) => AddTerminal(BitmapType);

        //add by LuoDian @ 20220220 添加List<int>
        private void tsmiAddListInt_Click(object sender, EventArgs e) => AddTerminal(ListInt);
        //add by LuoDian @ 20220220 添加List<double>
        private void tsmiAddListDouble_Click(object sender, EventArgs e) => AddTerminal(ListDouble);
        //add by LuoDian @ 20220220 添加List<string>
        private void tsmiAddListString_Click(object sender, EventArgs e) => AddTerminal(ListString);
        //add by LuoDian @ 20220220 添加List<HyImage>
        private void tsmiAddListHyImage_Click(object sender, EventArgs e) => AddTerminal(ListHyImage);
        //add by LuoDian @ 20220220 添加List<Bitmap>
        private void tsmiAddListBitmap_Click(object sender, EventArgs e) => AddTerminal(ListBitmap);

        //新增RoiData类型  Heweile  2022/4/3
        private void tsmiAddRoiData_Click(object sender, EventArgs e)
        {
            AddTerminal(typeof(HyRoiManager.RoiData));
        }

        //新增AI缺陷和AI缺陷集合   Heweile2022/4/15
        private void tsmiAddAIDefect_Click(object sender, EventArgs e)
        {
            AddTerminal(typeof(HyRoiManager.ROI.BaseHyROI));
        }

        private void tsmiAddListAIDefect_Click(object sender, EventArgs e)
        {
            AddTerminal(typeof(List<HyRoiManager.ROI.BaseHyROI>));
        }

        //add by LuoDian @ 20210826 用于自动添加指定参数名称的参数
        public void AddTerminal(string name, Type type)
        {
            if (!terminals.Contains(name))
                terminals.Add(new HyTerminal(name, type));
        }

        //add by LuoDian @ 20210826 用于根据参数名称自动删除指定参数
        public void RemoveTerminal(string name)
        {
            if (terminals.Contains(name))
                terminals.Remove(name);
        }


    }
}
