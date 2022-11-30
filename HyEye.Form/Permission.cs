using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public interface IPermission
    {
        void SetPermission(Form form);
    }

    public class Permission : IPermission
    {
        readonly IUserRepository userRepo;
        readonly IPermissionRepository permissionRepo;

        public Permission(IUserRepository userRepo, IPermissionRepository permissionRepo)
        {
            this.userRepo = userRepo;
            this.permissionRepo = permissionRepo;

            userRepo.AfterLogin += RoleChanged;
            userRepo.AfterExit += RoleChanged;
        }

        readonly LinkedList<Form> forms = new LinkedList<Form>();

        private void RoleChanged()
        {
            foreach (Form f in forms)
            {
                set(f);
            }
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            forms.Remove((Form)sender);
        }

        public void SetPermission(Form form)
        {
            forms.AddLast(form);
            form.FormClosed += Form_FormClosed;

            set(form);
        }

        void set(Form form)
        {
            Role currRole = userRepo.CurrUser?.Role ?? Role.Operator;

            List<ControlPermissionVO> permissions = permissionRepo.GetControlPermissions(form.Name);

            foreach (ControlPermissionVO cp in permissions)
            {
                bool enabled = currRole >= cp.Role;

                Control[] controls = form.Controls.Find(cp.ControlName, true);
                if (controls.Length > 0)
                {
                    Control c = controls[0];

                    if (cp.IsContextMenuStrip)
                    {
                        ToolStripItem[] items = c.ContextMenuStrip.Items.Find(cp.ItemName, true);
                        if (items != null && items.Length > 0)
                        {
                            if (cp.HideOrDisable)
                            {
                                // remove 最好，但是有问题
                                // c.ContextMenuStrip.Items.Remove(items[0]);
                                items[0].Visible = enabled;
                            }
                            else
                                items[0].Enabled = enabled;
                        }
                    }
                    else
                    {
                        if (c is MenuStrip menuStrip)
                        {
                            ToolStripItem[] items = menuStrip.Items.Find(cp.ItemName, true);
                            if (items.Length > 0)
                            {
                                if (cp.HideOrDisable)
                                    items[0].Visible = enabled;
                                else
                                    items[0].Enabled = enabled;
                            }
                        }
                        else if (c is StatusStrip statusStrip)
                        {
                            ToolStripItem[] items = statusStrip.Items.Find(cp.ItemName, true);
                            if (items.Length > 0)
                            {
                                if (cp.HideOrDisable)
                                    items[0].Visible = enabled;
                                else
                                    items[0].Enabled = enabled;
                            }
                        }
                        else
                        {
                            if (cp.HideOrDisable)
                                c.Visible = enabled;
                            else
                                c.Enabled = enabled;
                        }
                    }
                }
            }
        }

    }
}
