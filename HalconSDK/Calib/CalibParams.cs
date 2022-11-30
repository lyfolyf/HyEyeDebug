using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK.Calib
{
    public class CalibParams
    {
        public string InnerParamPath { get; set; } = "path1";
        public string InnerCheckPath { get; set; } = "path4";

        public string MirrorPath { get; set; } = "path2";
        public string path3 { get; set; } = "path3";

        public string BackupPath { get; set; } = @"./Data/Params_BackUp";
        public string Threshstdmax1 { get; set; } = "Threshstdmax1";
        public string Threshstdmax2 { get; set; } = "Threshstdmax2";
        public string Threshstdmin1 { get; set; } = "Threshstdmin1";
        public string Threshstdmin2 { get; set; } = "Threshstdmin2";
        public string ThreshProductmax1 { get; set; } = "ThreshProductmax1";
        public string ThreshProductmax2 { get; set; } = "ThreshProductmax2";
        public string ThreshProductmin1 { get; set; } = "ThreshProductmin1";
        public string ThreshProductmin2 { get; set; } = "ThreshProductmin2";
        public string heightRate { get; set; } = "392.4/1080";
        public string widRate { get; set; } = "698.4/1920";

        public string SingleMaterialPath { get; set; } = "SingleMaterialPath";
    }
}
