using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// 用于记录非托管的数组。
    /// </summary>
    public class UnmanagedArray : IDisposable
    {

        /// <summary>
        /// 用于记录非托管的数组。
        /// </summary>
        /// <param name="elementCount">元素数目。</param>
        /// <param name="elementSize">单个元素的字节数。</param>
        public UnmanagedArray(int elementCount, int elementSize)
        {
            int length = elementCount * elementSize;
            this.ElementCount = elementCount;
            this.ByteLength = length;
            this.Pointer = Marshal.AllocHGlobal(length);
        }

        /// <summary>
        /// 数组指针。
        /// </summary>
        public IntPtr Pointer { get; set; }

        /// <summary>
        /// 申请到的字节数。（元素数目 * 单个元素的字节数）。
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 元素数目。
        /// </summary>
        public int ElementCount { get; private set; }

        #region IDisposable Members

        /// <summary>
        /// Internal variable which checks if Dispose has already been called
        /// </summary>
        private Boolean disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(Boolean disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                //Managed cleanup code here, while managed refs still valid
            }
            //Unmanaged cleanup code here
            IntPtr ptr = this.Pointer;
            this.Pointer = IntPtr.Zero;
            this.ByteLength = 0;
            Marshal.FreeHGlobal(ptr);

            disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Call the private Dispose(bool) helper and indicate 
            // that we are explicitly disposing
            this.Dispose(true);

            // Tell the garbage collector that the object doesn't require any
            // cleanup when collected since Dispose was called explicitly.
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
