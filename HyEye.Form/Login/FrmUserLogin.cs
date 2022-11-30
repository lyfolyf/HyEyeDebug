using HyEye.API.Repository;
using HyEye.Models.VO;
using System.Windows.Forms;

namespace HyEye.WForm.Login
{
    public partial class FrmUserLogin : Form
    {
        readonly IUserRepository userRepo;

        public UserVO Loginer { get; private set; }

        public FrmUserLogin(IUserRepository userRepo)
        {
            InitializeComponent();

            this.userRepo = userRepo;
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            login();
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
            }
        }

        void login()
        {
            if (!tbUserName.CheckNotWhiteSpace("请输入用户名") || !tbPassword.CheckNotEmpty("请输入密码"))
                return;

            string username = tbUserName.Text;
            string password = tbPassword.Text;

            userRepo.Login(username, password);

            DialogResult = DialogResult.OK;
        }
    }
}
