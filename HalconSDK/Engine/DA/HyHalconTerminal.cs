using HyVision.Models;
using System;
using System.Xml.Serialization;

namespace HalconSDK.Engine.DA
{
    [Serializable]
    [XmlInclude(typeof(HyHalconTerminal))]
    public class HyHalconTerminal : HyTerminal
    {
        public string ConvertTargetType { get; set; }
    }
}
