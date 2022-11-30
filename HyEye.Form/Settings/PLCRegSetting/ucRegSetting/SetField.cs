using HyEye.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.WForm.Settings.PLCRegSetting
{
    public partial class SetField : Form
    {
        public string FieldName { get; set; } = "Field1";
        public CommandFieldUse Fielduse { get; set; }

        public SetField()
        {
            InitializeComponent();
        }

        private void SetField_Load(object sender, EventArgs e)
        {
            foreach (CommandFieldUse commandFieldUse in Enum.GetValues(typeof(CommandFieldUse)))
            {
                comboBox1.Items.Add(commandFieldUse.ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FieldName = textBox1.Text == null ? "Field1" : textBox1.Text;
            Fielduse = (CommandFieldUse)comboBox1.SelectedIndex;
            DialogResult = DialogResult.OK;
        }


    }
}
