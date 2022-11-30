#region Copyright and author
/*
Copyright ?Lead 3C Dec.25,2021.
Author Louis
*/
#endregion

using System.Runtime.InteropServices;

namespace HyCommon.ImageUtils
{
    internal static class NativeMethods
    {
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static unsafe extern int memcpy(void* dest, void* src, int count);
    }
}
