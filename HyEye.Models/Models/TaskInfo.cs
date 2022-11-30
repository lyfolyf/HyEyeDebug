using System.Collections.Generic;

namespace HyEye.Models
{
    /// <summary>
    /// 任务
    /// </summary>
    public class TaskInfo
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        public TaskType Type { get; set; } = TaskType.VP;

        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 拍照信息
        /// </summary>
        public CameraAcquireImageInfo CameraAcquireImage { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (!(obj is TaskInfo task))
                return false;

            return Name == task.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    /// <summary>
    /// 相机和拍照
    /// </summary>
    public class CameraAcquireImageInfo
    {
        public string CameraSN { get; set; }

        public List<AcquireImageInfo> AcquireImages { get; set; } = new List<AcquireImageInfo>();
    }

    public class AcquireImageInfo
    {
        /// <summary>
        /// 拍照名称
        /// </summary>
        public string Name { get; set; }

        // 因为可以引用，所以标定必须绑定，不能通过 任务/相机/拍照 来搜索

        /// <summary>
        /// Checkerboard 标定名称
        /// </summary>
        public string CheckerboardName { get; set; }

        /// <summary>
        /// HandEye（多点） 标定名称
        /// </summary>
        public List<string> HandEyeNames { get; set; }

        /// <summary>
        /// HandEye（单点）标定名称
        /// </summary>
        public string HandEyeSingleName { get; set; }

        /// <summary>
        /// 联合标定
        /// </summary>
        public string JointName { get; set; }
    }

}
