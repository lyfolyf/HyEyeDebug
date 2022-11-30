using System;
using HalconDotNet;
using HyRoiManager;





namespace HalconSDK.Engine.BL
{

    [Serializable]
    public class ParameterInfo
    {
        public string RowIndex { get; set; }

        public string FuncIndex { get; set; }

        public string FunctionName { get; set; }

        public string InputOutputType { get; set; }

        public string ParamName { get; set; }

        public string DataType { get; set; }

        public string Value { get; set; }

        public bool IsConnect { get; set; }

        public string MappingInfo { get; set; }

        public string SelectedRoiIndex { get; set; }

        public string ImagePath { get; set; }

        public RoiData Roidata { get; set; }

    }
}
