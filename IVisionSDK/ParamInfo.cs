using System;

namespace VisionSDK
{
    public class ParamInfo
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public object Value { get; set; }

        public string Description { get; set; }

        public static readonly Type IntType = typeof(int);

        public static readonly Type DoubleType = typeof(double);

        public static readonly Type StringType = typeof(string);
    }
}
