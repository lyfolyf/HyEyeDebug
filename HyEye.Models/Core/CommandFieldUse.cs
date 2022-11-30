using System.ComponentModel;

namespace HyEye.Models
{
    /// <summary>
    /// 指令字段用途
    /// </summary>
    public enum CommandFieldUse
    {
        None,

        [Description("传入 ToolBlock")]
        ToolBlock,

        [Description("传入脚本")]
        Script,

        [Description("保存图像")]
        SaveImage
    }
}
