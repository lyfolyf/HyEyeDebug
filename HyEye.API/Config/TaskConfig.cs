using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class TaskConfig
    {
        public List<TaskInfo> Tasks { get; set; } = new List<TaskInfo>();
    }
}
