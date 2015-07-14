using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于记录非托管的数组。
    /// </summary>
    public class UnmanagedArray : IDisposable
    {
        /// <summary>
        /// 用于记录非托管的数组。
        /// </summary>
        /// <param name="byteLength">所需的内存中的字节数。</param>
        public UnmanagedArray(int byteLength)
        {
            this.pointer = Marshal.AllocHGlobal(byteLength);
            this.byteLength = byteLength;
        }
        /// <summary>
        /// 数组指针。
        /// </summary>
        public IntPtr pointer;

        /// <summary>
        /// 申请到的字节数。（数组元素数 * 元素长度）。
        /// </summary>
        public int byteLength;


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
                //TODO: Managed cleanup code here, while managed refs still valid
            }
            //TODO: Unmanaged cleanup code here
            IntPtr ptr = this.pointer;
            this.pointer = IntPtr.Zero;
            this.byteLength = 0;
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
