using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class UserBlockConfig
    {
        public List<UserBlockInfo> UserBlockInfos { get; set; }
    }
}
