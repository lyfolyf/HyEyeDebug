using HyEye.Models;
using System;

namespace HyEye.API.Config
{
    [Serializable]
    public class CommunicationConfig
    {
        public CommunicationInfo CommunicationInfo { get; set; }
    }
}
