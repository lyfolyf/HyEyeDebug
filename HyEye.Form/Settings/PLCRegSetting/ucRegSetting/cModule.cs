using System;
using System.Drawing;
using System.Windows.Forms;

namespace HyEye.WForm.Settings.PLCRegSetting
{
    /// <summary>
    /// 折叠控件样式以及行数操作类
    /// </summary>
    sealed class cModule
    {
        #region CustomGrid
        static DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };

        static DataGridViewCellStyle amountCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" };

        static DataGridViewCellStyle gridCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(Convert.ToInt32(Convert.ToByte(79)), Convert.ToInt32(Convert.ToByte(129)), Convert.ToInt32(System.Convert.ToByte(189))),
            Font = new Font("Segoe UI", (float)(10.0F), FontStyle.Regular, GraphicsUnit.Point, Convert.ToByte(0)),
            ForeColor = SystemColors.ControlLightLight,
            SelectionBackColor = SystemColors.Highlight,
            SelectionForeColor = SystemColors.ControlText,
            WrapMode = DataGridViewTriState.True
        };

        static DataGridViewCellStyle gridCellStyle2 = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = SystemColors.ControlLightLight,
            Font = new Font("Segoe UI", (float)(10.0F), FontStyle.Regular, GraphicsUnit.Point, Convert.ToByte(0)),
            ForeColor = SystemColors.ControlText,
            SelectionBackColor = Color.FromArgb(Convert.ToInt32(Convert.ToByte(173)), Convert.ToInt32(Convert.ToByte(216)), Convert.ToInt32(Convert.ToByte(230))),
            SelectionForeColor = SystemColors.ControlText,
            WrapMode = DataGridViewTriState.False
        };

        static DataGridViewCellStyle gridCellStyle3 = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.WhiteSmoke,
            Font = new Font("Segoe UI", (float)(10.0F), FontStyle.Regular, GraphicsUnit.Point, Convert.ToByte(0)),
            ForeColor = SystemColors.WindowText,
            SelectionBackColor = Color.FromArgb(Convert.ToInt32(Convert.ToByte(173)), Convert.ToInt32(Convert.ToByte(216)), Convert.ToInt32(Convert.ToByte(230))),
            SelectionForeColor = SystemColors.ControlText,
            WrapMode = DataGridViewTriState.True
        };

        static DataGridViewCellStyle NormalCenterCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleCenter,
            BackColor = SystemColors.ControlLightLight,
            Font = new Font("Segoe UI", (float)(10.0F), FontStyle.Regular, GraphicsUnit.Point, Convert.ToByte(0)),
            ForeColor = SystemColors.ControlText,
            SelectionBackColor = Color.FromArgb(Convert.ToInt32(Convert.ToByte(173)), Convert.ToInt32(Convert.ToByte(216)), Convert.ToInt32(Convert.ToByte(230))),
            SelectionForeColor = SystemColors.ControlText,
            WrapMode = DataGridViewTriState.False
        };

        //设置表格的主题样式
        static public void applyGridTheme(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;

            grid.BackgroundColor = SystemColors.Window;
            grid.BorderStyle = BorderStyle.None;

            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersDefaultCellStyle = gridCellStyle;
            grid.ColumnHeadersHeight = 32;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.DefaultCellStyle = gridCellStyle2;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = SystemColors.GradientInactiveCaption;
            //grid.ReadOnly = true;
            grid.RowHeadersVisible = true;
            grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.RowHeadersDefaultCellStyle = gridCellStyle3;
            grid.Font = gridCellStyle.Font;
        }

        //设置表格单元格样式
        static public void setGridRowHeader(DataGridView dgv, bool hSize = false)
        {
            dgv.TopLeftHeaderCell.Value = "No.";
            dgv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
            foreach (DataGridViewColumn cCol in dgv.Columns)
            {
                if (cCol.ValueType.ToString() == typeof(DateTime).ToString())
                {
                    cCol.DefaultCellStyle = dateCellStyle;
                }
                else if (cCol.ValueType.ToString() == typeof(decimal).ToString() || cCol.ValueType.ToString() == typeof(double).ToString())
                {
                    cCol.DefaultCellStyle = amountCellStyle;
                }
            }
            if (hSize)
            {
                dgv.RowHeadersWidth = dgv.RowHeadersWidth + 16;
            }
            dgv.AutoResizeColumns();
        }

        //设置表格的行号
        static public void rowPostPaint_HeaderCount(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                //set rowheader count
                DataGridView dgv = (DataGridView)sender;
                string rowIdx = Convert.ToString((e.RowIndex + 1).ToString());
                var centerFormat = new StringFormat();
                centerFormat.Alignment = StringAlignment.Center;
                centerFormat.LineAlignment = StringAlignment.Center;
                Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top,
                    dgv.RowHeadersWidth, e.RowBounds.Height - dgv.Rows[e.RowIndex].DividerHeight);
                e.Graphics.DrawString(rowIdx, dgv.Font, SystemBrushes.ControlText,
                    headerBounds, centerFormat);
            }
            catch (Exception)
            {

            }
        }

        static public void setParentGrid(DataGridView dgv)
        {
            foreach (DataGridViewColumn cCol in dgv.Columns)
            {
                switch (cCol.Name)
                {
                    case "ID":
                        cCol.Visible = false;
                        break;
                    case "任务名称":
                        {
                            cCol.ReadOnly = true;
                            cCol.Width = 200;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "拍照名称":
                        {
                            cCol.ReadOnly = true;
                            cCol.Width = 200;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "标定名称":
                        {
                            cCol.ReadOnly = true;
                            cCol.Width = 200;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "起始地址":
                        {
                            //cCol.ReadOnly = true;
                            cCol.Width = 100;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "拍照索引":
                        {
                            cCol.ReadOnly = true;
                            cCol.Width = 100;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "总指令长度":
                        {
                            cCol.ReadOnly = true;
                            cCol.Width = 100;
                            cCol.DefaultCellStyle = NormalCenterCellStyle;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                }
            }
            //dgv.AutoResizeColumns();
        }

        static public int modeindex { get; set; }

        static public void setSonGrid(DataGridView dgv)
        {
            bool tempVis;
            switch (modeindex)
            {
                case 0:
                case 2:
                    tempVis = true;
                    break;
                case 1:
                case 3:
                    tempVis = false;
                    break;
                default:
                    tempVis = false;
                    break;
            }

            DataGridViewComboBoxColumn cb_UseSelect = new DataGridViewComboBoxColumn();
            cb_UseSelect.Name = "cb_UseSelect";
            cb_UseSelect.DefaultCellStyle = NormalCenterCellStyle;
            cb_UseSelect.HeaderText = "字段用途";
            cb_UseSelect.Width = 90;
            cb_UseSelect.Items.Add("None");
            cb_UseSelect.Items.Add("ToolBlock");
            cb_UseSelect.Items.Add("Script");
            cb_UseSelect.Items.Add("SaveImage");
            cb_UseSelect.DisplayIndex = 7;
            cb_UseSelect.ValueMember = "字段用途";
            cb_UseSelect.DataPropertyName = "字段用途";
            cb_UseSelect.Visible = tempVis;
            dgv.Columns.Add(cb_UseSelect);

            DataGridViewButtonColumn btn_Read = new DataGridViewButtonColumn();
            btn_Read.Name = "btn_Read";
            btn_Read.HeaderText = "读操作";
            btn_Read.DefaultCellStyle.NullValue = "读";
            btn_Read.Width = 70;
            btn_Read.DisplayIndex = 9;
            dgv.Columns.Add(btn_Read);

            DataGridViewButtonColumn btn_Write = new DataGridViewButtonColumn();
            btn_Write.Name = "btn_Write";
            btn_Write.HeaderText = "写操作";
            btn_Write.DefaultCellStyle.NullValue = "写";
            btn_Write.Width = 70;
            btn_Write.DisplayIndex = 10;
            dgv.Columns.Add(btn_Write);

            DataGridViewComboBoxColumn cb_ValueTypeSelect = new DataGridViewComboBoxColumn();
            cb_ValueTypeSelect.Name = "cb_ValueTypeSelect";
            cb_ValueTypeSelect.DefaultCellStyle = NormalCenterCellStyle;
            cb_ValueTypeSelect.HeaderText = "值类型";
            cb_ValueTypeSelect.Width = 90;
            cb_ValueTypeSelect.Items.Add("Short");
            cb_ValueTypeSelect.Items.Add("Int");
            cb_ValueTypeSelect.Items.Add("String");
            cb_ValueTypeSelect.DisplayIndex = 2;
            cb_ValueTypeSelect.ValueMember = "值类型";
            cb_ValueTypeSelect.DataPropertyName = "值类型";
            dgv.Columns.Add(cb_ValueTypeSelect);

            DataGridViewComboBoxColumn cb_RatSelect = new DataGridViewComboBoxColumn();
            cb_RatSelect.Name = "cb_RatSelect";
            cb_RatSelect.HeaderText = "系数";
            cb_RatSelect.Width = 80;
            cb_RatSelect.Items.Add("1");
            cb_RatSelect.Items.Add("10");
            cb_RatSelect.Items.Add("100");
            cb_RatSelect.Items.Add("1000");
            cb_RatSelect.ValueMember = "系数";
            cb_RatSelect.DataPropertyName = "系数";
            cb_RatSelect.DisplayIndex = 5;
            dgv.Columns.Add(cb_RatSelect);

            foreach (DataGridViewColumn cCol in dgv.Columns)
            {
                switch (cCol.Name)
                {
                    case "ID":
                        {
                            cCol.DisplayIndex = 0;
                            cCol.Visible = false;
                        }
                        break;
                    case "寄存器地址":
                        {
                            cCol.ReadOnly = true;
                            cCol.DisplayIndex = 1;
                            cCol.Width = 100;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "值类型":
                        {
                            cCol.DisplayIndex = 11;
                            cCol.Width = 80;
                            cCol.Visible = false;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "寄存器长度":
                        {
                            cCol.DisplayIndex = 3;
                            cCol.Width = 100;
                            cCol.DefaultCellStyle = NormalCenterCellStyle;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "值":
                        {
                            cCol.DisplayIndex = 4;
                            cCol.Width = 100;
                            cCol.DefaultCellStyle = NormalCenterCellStyle;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "系数":
                        {
                            cCol.DisplayIndex = 12;
                            cCol.Width = 60;
                            cCol.Visible = false;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "说明":
                        {
                            cCol.DisplayIndex = 8;
                            cCol.Width = 180;
                            cCol.DefaultCellStyle = NormalCenterCellStyle;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "字段名称":
                        {
                            cCol.DisplayIndex = 6;
                            cCol.Width = 100;
                            cCol.DefaultCellStyle = NormalCenterCellStyle;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                    case "字段用途":
                        {
                            cCol.DisplayIndex = 13;
                            cCol.Width = 80;
                            cCol.Visible = false;
                            cCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        break;
                }
            }
            //dgv.AutoResizeColumns();
        }

        #endregion
    }

    /// <summary>
    /// 控件类型，是最外层的表格还是中间层的表格
    /// </summary>
    public enum controlType
    {
        outside = 0,
        middle = 1
    }

    /// <summary>
    /// 展开图标
    /// </summary>
    public enum rowHeaderIcons
    {
        expand = 0,
        collapse = 1
    }

}
