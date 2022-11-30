using System.ComponentModel;

namespace HyEye.Models
{
    public enum CalibrationType
    {
        Checkerboard = 1,

        [Description("HandEye（多点）")]
        HandEye = 2,

        [Description("HandEye（单点）")]
        HandEyeSingle,

        [Description("联合标定")]
        Joint
    }

}
