using System.ComponentModel;

namespace HyEye.Models
{
    public enum Role
    {
        /// <summary>
        /// 开发者
        /// </summary>
        [Description("开发者")]
        Developer = 1000,

        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Administrator = 100,

        /// <summary>
        /// 工程师
        /// </summary>
        [Description("工程师")]
        Engineer = 10,

        /// <summary>
        /// 操作员
        /// </summary>
        [Description("操作员")]
        Operator = 1
    }
}
