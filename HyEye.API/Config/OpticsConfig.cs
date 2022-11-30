using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class OpticsConfig
    {
        private List<Optics> optics = new List<Optics>();

        //update by LuoDian @ 20211210 用于子料号的快速切换
        //public List<OpticsInfo> Optics { get; set; } = new List<OpticsInfo>();
        [System.Xml.Serialization.XmlElementAttribute("Optics")]
        public List<Optics> Optics { get => optics; set => optics = value; }
    }

    //add by LuoDian @ 20211210 用于子料号的快速切换
    [Serializable]
    public class Optics : ICloneable
    {
        private string materialSubName = "default";
        private List<OpticsInfo> opticsInfo = new List<OpticsInfo>();

        [System.Xml.Serialization.XmlElementAttribute("MaterialSubName")]
        public string MaterialSubName { get => materialSubName; set => materialSubName = value; }

        [System.Xml.Serialization.XmlElementAttribute("OpticsInfo")]
        public List<OpticsInfo> OpticsInfo { get => opticsInfo; set => opticsInfo = value; }
        public Optics Clone()
        {
            return (Optics)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
