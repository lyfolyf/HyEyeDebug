using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HyVision.Tools.ImageDisplay;
using HyVision.Models;
using HalconDotNet;




namespace HalconSDK.Engine.BL
{
    [Serializable]
    public class SettingsBL
    {

        public string FunName { get; set; }

        public string ParmName { get; set; }


        [XmlIgnore]
        public HObject ResultHObjectROI { get; set; }

        //ROI设置信息记录
        public List<ROICollectionsBL> RoiCollection { get; set; } = new List<ROICollectionsBL>();

        //图像设置信息记录
        public HyImage InputImage { get; set; }


        public string ImagePath { get; set; }

    }

    [Serializable]
    public class ROICollectionsBL
    {
        public int Index { get; set; } = 1;

        public List<string> UnionCollection1 { get; set; } = new List<string>();

        public List<string> UnionCollection2 { get; set; } = new List<string>();

        public string Description { get; set; }

        public string ROIOperation { get; set; }

    }





}
