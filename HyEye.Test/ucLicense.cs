using System;
using System.Linq;
using System.Windows.Forms;
using HyCommon.License;

namespace HyEye.Test
{
    public partial class ucLicense : UserControl
    {
        HyLicense hl;

        public ucLicense()
        {
            InitializeComponent();
        }

        private void ucLicense_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(LicenseType))
               .Cast<Enum>().ToList();

            hl = new HyLicense();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LicenseInfo lic = new LicenseInfo()
            {
                ProgramName = "HyInspect",
                RegisterTime = DateTime.Now,
                CustomRole = (LicenseType)comboBox1.SelectedValue,
                CustomMachineCode = textBox1.Text
            };
            textBox2.Text = hl.Register(lic);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LicenseInfo lic = new LicenseInfo();
            lic = hl.DesLic(textBox2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((LicenseType)comboBox1.SelectedValue == LicenseType.Permanent)
            { 
                textBox1.Visible = true;
                textBox1.Text = LicenseUtils.GetCustomMachineCode();
            }
            else
                textBox1.Visible = false;
        }



    }
}
