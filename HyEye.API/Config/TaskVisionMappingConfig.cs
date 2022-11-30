using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class TaskVisionMappingConfig
    {
        public List<TaskVisionMapping> TaskVisionMappings { get; set; } = new List<TaskVisionMapping>();
    }
}
