using HyEye.Models;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    public class PermissionList
    {
        public List<ControlPermission> ControlPermissions { get; set; }

        public PermissionList()
        {
            ControlPermissions = new List<ControlPermission>
            {
                new ControlPermission { FormName = "FormMain", ControlName = "statusStrip1", IsContextMenuStrip = false, ItemName = "tslblAcqImageCount", Description = "取像次数", HideOrDisable = true, Role = Role.Developer },

                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiUserManager",    Description = "用户管理", HideOrDisable = false, Role = Role.Administrator },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiCameraSetting",  Description = "相机设置", HideOrDisable = false, Role = Role.Engineer },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiLightSetting",   Description = "光控设置", HideOrDisable = false, Role = Role.Engineer },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiCommSetting",    Description = "通讯设置", HideOrDisable = false, Role = Role.Engineer },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiCommandSetting", Description = "指令设置", HideOrDisable = false, Role = Role.Engineer },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiImageSetting",   Description = "图像设置", HideOrDisable = false, Role = Role.Operator },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiSystemSetting",  Description = "系统设置", HideOrDisable = false, Role = Role.Operator },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiDisplaySetting", Description = "显示设置", HideOrDisable = false, Role = Role.Engineer },
                new ControlPermission { FormName = "FormMain", ControlName = "menuStrip1", IsContextMenuStrip = false, ItemName = "tsmiTaskSetting",    Description = "任务设置", HideOrDisable = false, Role = Role.Engineer },

                new ControlPermission { FormName = "FrmHandEyeSetting", ControlName = "dgvData", IsContextMenuStrip = true, ItemName = "tsmiAutoSetValue",    Description = "自动填值", HideOrDisable = true, Role = Role.Developer }
            };
        }
    }
}
