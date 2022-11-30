using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class CameraParamInfoVO
    {
        public string Name { get; set; }

        public bool Enabled { get; set; } = true;

        public bool ReadOnly { get; set; } = true;
    }

    public class CameraParamInfoListVO
    {
        public string SN { get; set; }

        public List<CameraParamInfoVO> ParamInfos { get; set; }
    }
}
