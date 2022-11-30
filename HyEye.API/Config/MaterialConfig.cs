using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class MaterialConfig
    {
        public List<MaterialInfo> Materials { get; set; } = new List<MaterialInfo>();
    }
}
