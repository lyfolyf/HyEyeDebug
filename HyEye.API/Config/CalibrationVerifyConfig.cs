using HyEye.Models;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    public class CalibrationVerifyConfig
    {
        public List<CalibrationVerifyInfo> CalibrationVerifys { get; set; } = new List<CalibrationVerifyInfo>();
    }
}
