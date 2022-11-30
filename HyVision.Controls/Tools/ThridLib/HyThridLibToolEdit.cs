using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HyVision.Tools.ThridLib
{
    public partial class HyThridLibToolEdit : BaseHyUserToolEdit<HyThridLibTool>
    {
        public HyThridLibToolEdit()
        {
            InitializeComponent();
        }

        HyThridLibTool thridLibTool;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HyThridLibTool Subject
        {
            get { return thridLibTool; }
            set
            {
                if (!object.Equals(thridLibTool, value))
                {
                    thridLibTool = value;

                    inputEdit.Subject = thridLibTool.Inputs;
                    outputEdit.Subject = thridLibTool.Outputs;

                    cmbClassName.Items.Clear();
                    cmbFuncName.Items.Clear();

                    tbDllName.Text = thridLibTool.DllName;
                    Type[] types = thridLibTool.GetClasses();
                    if (types != null)
                    {
                        cmbClassName.Items.Clear();
                        cmbClassName.Items.AddRange(types);

                        cmbClassName.SelectedItem = types.FirstOrDefault(a => a.FullName == thridLibTool.ClassName);
                    }
                    else
                    {
                        if (thridLibTool.ClassName != null)
                        {
                            cmbClassName.Items.Add(thridLibTool.ClassName);
                            cmbClassName.Text = thridLibTool.ClassName;
                        }
                    }
                    string[] funcs = thridLibTool.GetFuncs();
                    if (funcs != null)
                    {
                        cmbFuncName.Items.Clear();
                        cmbFuncName.Items.AddRange(funcs);

                        cmbFuncName.SelectedItem = thridLibTool.FuncName;
                    }
                    else
                    {
                        if (thridLibTool.FuncName != null)
                        {
                            cmbFuncName.Items.Add(thridLibTool.FuncName);
                            cmbFuncName.Text = thridLibTool.FuncName;
                        }
                    }
                }
            }
        }

        private void HyThridLibToolEdit_Load(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Height / 2;
        }

        private void tbDllName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SetDllName(tbDllName.Text.Trim());
            }
        }

        private void tbDllName_Leave(object sender, EventArgs e)
        {
            SetDllName(tbDllName.Text.Trim());
        }

        private void cmbClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetClassName(((Type)cmbClassName.SelectedItem).FullName);
        }

        private void cmbFuncName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFuncName((string)cmbFuncName.SelectedItem);
        }

        void SetDllName(string dllName)
        {
            try
            {
                thridLibTool.DllName = dllName;

                tslblErrMsg.Text = string.Empty;

                Type[] types = thridLibTool.GetClasses();
                if (types != null)
                {
                    cmbClassName.Items.Clear();
                    cmbClassName.Items.AddRange(types);

                    cmbClassName.SelectedItem = types.FirstOrDefault(a => a.FullName == thridLibTool.ClassName);

                    if (cmbClassName.SelectedItem == null)
                    {
                        cmbFuncName.Items.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                tslblErrMsg.Text = e.Message;
            }
        }

        void SetClassName(string className)
        {
            try
            {
                thridLibTool.ClassName = className;

                tslblErrMsg.Text = string.Empty;

                string[] funcs = thridLibTool.GetFuncs();
                if (funcs != null)
                {
                    cmbFuncName.Items.Clear();
                    cmbFuncName.Items.AddRange(funcs);

                    cmbFuncName.SelectedItem = thridLibTool.FuncName;
                }
            }
            catch (Exception ex)
            {
                tslblErrMsg.Text = ex.Message;
            }
        }

        void SetFuncName(string funcName)
        {
            try
            {
                thridLibTool.FuncName = funcName;

                tslblErrMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                tslblErrMsg.Text = ex.Message;
            }
        }

    }
}
