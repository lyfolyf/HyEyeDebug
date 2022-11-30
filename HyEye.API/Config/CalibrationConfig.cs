using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class CalibrationConfig
    {
        public List<CalibrationInfo> Calibrations { get; set; } = new List<CalibrationInfo>();
    }
}
