using System.Collections.Generic;

namespace HyEye.Models.VO
{
    /// <summary>
    /// 任务
    /// </summary>
    public class TaskInfoVO
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        public TaskType Type { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 拍照信息
        /// </summary>
        public CameraAcquireImageInfoVO CameraAcquireImage { get; set; }
    }

    /// <summary>
    /// 相机和拍照
    /// </summary>
    public class CameraAcquireImageInfoVO
    {
        public string CameraSN { get; set; }

        public List<AcquireImageInfoVO> AcquireImages { get; set; } = new List<AcquireImageInfoVO>();
    }

    public class AcquireImageInfoVO
    {
        /// <summary>
        /// 拍照名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Checkerboard 标定名称
        /// </summary>
        public string CheckerboardName { get; set; }

        /// <summary>
        /// HandEye 标定名称
        /// </summary>
        public List<string> HandEyeNames { get; set; }

        /// <summary>
        /// HandEye（单点）标定名称
        /// </summary>
        public string HandEyeSingleName { get; set; }

        /// <summary>
        /// 联合标定名称
        /// </summary>
        public string JointName { get; set; }
    }

}
