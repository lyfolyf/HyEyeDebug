using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class TaskVisionMappingVO
    {
        public string TaskName { get; set; }

        public List<NameMapperVO> Inputs { get; set; }

        public List<NameMapperVO> Calibrations { get; set; }

        public List<NameMapperVO> Graphics { get; set; }
    }

    public class NameMapperVO
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
