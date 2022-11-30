namespace HyEye.Models.VO
{
    public class ControlPermissionVO
    {
        /// <summary>
        /// 窗体 Name
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 控件 Name
        /// <para>如果是右键菜单，则填触发它的控件</para>
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 是否右键菜单
        /// </summary>
        public bool IsContextMenuStrip { get; set; }

        public string ItemName { get; set; }

        public Role Role { get; set; }

        /// <summary>
        /// 隐藏或禁用
        /// </summary>
        public bool HideOrDisable { get; set; }
    }
}
