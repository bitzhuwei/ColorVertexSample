using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public static class UnmanagedArrayCopyHelper
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint size);

        /// <summary>
        /// 将非托管数组复制到指定的位置。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="dest">目的地址的起始位置。</param>
        public static void CopyTo<T>(this UnmanagedArray<T> array, IntPtr dest) where T : struct
        {
            CopyMemory(dest, array.Header, (uint)array.ByteLength);
        }
    }
}
