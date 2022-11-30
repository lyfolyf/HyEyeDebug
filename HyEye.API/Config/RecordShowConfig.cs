using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class RecordShowConfig
    {

        public List<RecordShowInfo> RecordShowInfos { get; set; } = new List<RecordShowInfo>();

    }
}
