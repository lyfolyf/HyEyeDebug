namespace HyEye.Models
{
    public class ControlPermission
    {
        public string FormName { get; set; }

        public string ControlName { get; set; }

        public bool IsContextMenuStrip { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public Role Role { get; set; }

        public bool HideOrDisable { get; set; }
    }
}
