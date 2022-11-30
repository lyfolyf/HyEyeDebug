using CameraSDK.Models;
using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class CameraConfig
    {
        public List<CameraBrand> CameraBrands { get; set; }

        public string CtiPath { get; set; }

        public List<CameraInfo> CameraInfos { get; set; }

        public List<CameraParamInfoList> ParamInfoList { get; set; }
    }
}
