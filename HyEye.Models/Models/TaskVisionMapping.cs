using System.Collections.Generic;

namespace HyEye.Models
{
    /// <summary>
    /// 拍照、标定和 ToolBlock 中 InputName 的映射关系
    /// </summary>
    public class TaskVisionMapping
    {
        public string TaskName { get; set; }

        public List<NameMapper> Inputs { get; set; } = new List<NameMapper>();

        public List<NameMapper> Calibrations { get; set; } = new List<NameMapper>();

        public List<NameMapper> Graphics { get; set; } = new List<NameMapper>();
    }

    public class NameMapper
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

}
