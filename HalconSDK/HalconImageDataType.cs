using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK
{
    /// <summary>
    /// Halcon的图像数据类型
    /// </summary>
    public enum EHalconImageDataType
    {
        /// <summary>
        /// 1 byte per pixel, unsigned
        /// Value range: (0..255)
        /// </summary>
        halcon_byte,

        /// <summary>
        /// 1 byte per pixel, signed
        /// Value range: (-128..127)
        /// </summary>
        halcon_int1,

        /// <summary>
        /// 2 bytes per pixel, unsigned
        /// Value range: (0..65535)
        /// </summary>
        halcon_uint2,

        /// <summary>
        /// 2 bytes per pixel, signed
        /// Value range: (-32768..32767)
        /// </summary>
        halcon_int2,

        /// <summary>
        /// 4 bytes per pixel, signed
        /// Value range: (-2147483648..2147483647)
        /// </summary>
        halcon_int4,

        /// <summary>
        /// 8 bytes per pixel, signed (only available on 64 bit systems)
        /// Value range: (-9223372036854775808..9223372036854775807)
        /// </summary>
        halcon_int8,

        /// <summary>
        /// 4 bytes per pixel, floating point
        /// Value range: (-3.4e38..3.4e38)
        /// Precision: 6 significant decimal digits
        /// </summary>
        halcon_real,

        /// <summary>
        /// Two matrices of type 'real'
        /// </summary>
        halcon_complex,

        /// <summary>
        /// Two matrices of type 'real'
        /// Interpretation: Vectors
        /// </summary>
        halcon_vector_field_relative,

        /// <summary>
        /// Two matrices of type 'real'
        /// Interpretation: Absolute coordinates
        /// </summary>
        halcon_vector_field_absolute,

        /// <summary>
        /// 1 byte per pixel, unsigned
        /// Value range: (0..179)
        /// Interpretation: Angle divided by two
        /// Attention: The values 180..254 are automatically set to the value 255, which is interpreted as undefined angle
        /// </summary>
        halcon_direction,

        /// <summary>
        /// 1 byte per pixel, unsigned, cyclic arithmetics
        /// Value range: (0..255)
        /// </summary>
        halcon_cyclic
    }
}
