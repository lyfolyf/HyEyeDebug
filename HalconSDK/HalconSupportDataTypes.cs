using HalconDotNet;
using HyVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK
{
    public class HalconSupportDataTypes
    {
        public readonly static Type[] HTupleSupportedDataType = { typeof(string), typeof(float), typeof(double), typeof(IntPtr), typeof(long), typeof(int), typeof(bool), typeof(object) };
        public readonly static Type[] HObjectSupportedDataType = { typeof(HyImage), typeof(HObject) };
        public readonly static Type[] HImageSupportedDataType = { typeof(HyImage), typeof(string), typeof(HObject) };
        public readonly static Type[] HRegionSupportedDataType = { typeof(IntPtr), typeof(HObject) };
        public readonly static Type[] HXLDSupportedDataType = { typeof(IntPtr), typeof(HObject) };
    }
}
