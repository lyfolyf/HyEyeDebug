using System;

namespace HyEye.Models
{
    [Flags]
    public enum SubDireOfSaveImage
    {
        Date = 1,

        Task = 2,

        SN = 4,

        Flag = 8,

        Source = 16
    }
}
