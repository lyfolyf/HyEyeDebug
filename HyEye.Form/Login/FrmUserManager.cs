using GL.Kit.Extension;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HyEye.WForm.Login
{
    public partial class FrmUserManager : Form
    {
        readonly IUserRepository userRepo;

        public FrmUserManager(IUserRepository userRepo)
        {
            InitializeComponent();

            this.userRepo = userRepo;

            dgvUserList.AutoGenerateColumns = false;
        }

        BindingList<UserVO> dataSource;

        private void FrmUserManager_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add(new RoleDescription(Role.Administrator));
            cmbRole.Items.Add(new RoleDescription(Role.Engineer));
            cmbRole.Items.Add(new RoleDescription(Role.Operator));

            List<UserVO> users = userRepo.GetUserList();

            dataSource = new BindingList<UserVO>(users);

            dgvUserList.DataSource = dataSource;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (tbUserName.CheckNotEmpty("请输入用户名")
                && cmbRole.CheckSelected("请选择用户角色"))
            {
                UserVO user = new UserVO
                {
                    UserName = tbUserName.Text.Trim(),
                    Role = ((RoleDescription)cmbRole.SelectedItem).Role
                };

                userRepo.Add(user);

                dataSource.Add(user);
            }
        }

        private void tsmiDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count == 0) return;

            string username = dgvUserList.SelectedRows[0].Cells[1].Value.ToString();

            if (username == userRepo.CurrUser.UserName)
            {
                MessageBoxUtils.ShowWarn("不能删除自己");
                return;
            }

            if (MessageBoxUtils.ShowQuestion($"确定要删除用户[{username}]吗？") == DialogResult.Yes)
            {
                userRepo.Delete(username);

                dgvUserList.Rows.Remove(dgvUserList.SelectedRows[0]);
            }
        }

        private void tsmiResetPwd_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count == 0) return;

            string username = dgvUserList.SelectedRows[0].Cells[1].Value.ToString();

            if (MessageBoxUtils.ShowQuestion($"确定要重置用户[{username}]的密码吗？") == DialogResult.Yes)
            {
                userRepo.ResetPassword(username);
            }
        }

        private void dgvUserList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
            else if (e.ColumnIndex == 2)
            {
                e.Value = ((Role)e.Value).ToDescription();
            }
        }

        class RoleDescription
        {
            public RoleDescription(Role role)
            {
                Role = role;
                Description = role.ToDescription();
            }

            public Role Role { get; set; }

            public string Description { get; set; }

            public override string ToString()
            {
                return Description;
            }
        }
    }
}
