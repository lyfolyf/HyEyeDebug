using HyEye.API.Repository;
using System;
using System.Windows.Forms;

namespace HyEye.WForm.Login
{
    public partial class FrmChangePassword : Form
    {
        readonly IUserRepository userRepo;

        public FrmChangePassword(IUserRepository userRepo)
        {
            InitializeComponent();

            this.userRepo = userRepo;
        }

        private void btnPWChange_Click(object sender, EventArgs e)
        {
            changePassword();
        }

        private void tbNewPassWordAgain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                changePassword();
            }
        }

        void changePassword()
        {
            if (tbOldPassWord.CheckNotEmpty("请输入旧密码")
                && tbNewPassWord.CheckNotEmpty("请输入新密码")
                && tbNewPassWordAgain.CheckNotEmpty("请确认新密码")
                && tbNewPassWord.CheckMinLength(6, "密码长度不能小于 6 位")
                && tbNewPassWord.CheckSame(tbNewPassWordAgain, "新密码两次输入不一致")
                && tbOldPassWord.CheckNotSame(tbNewPassWord, "新密码不能和旧密码一致"))
            {
                userRepo.ChangePassword(tbOldPassWord.Text, tbNewPassWord.Text);

                MessageBoxUtils.ShowInfo("修改成功");

                DialogResult = DialogResult.OK;
            }
        }
    }
}
