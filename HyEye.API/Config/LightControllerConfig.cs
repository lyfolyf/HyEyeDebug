using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class LightControllerConfig
    {
        public List<LightControllerInfo> LightControllers { get; set; } = new List<LightControllerInfo>();
    }
}
