using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL.SceneComponent
{
    [StructLayout(LayoutKind.Sequential,Pack=1)]
    public struct Vertex3D
    {
        public float X;
        public float Y;
        public float Z;

        public static explicit operator Vertex3D(Vertex v)
        {
            Vertex3D d;
            d.X = v.X;
            d.Y = v.Y;
            d.Z = v.Z;
            return d;
        }
    }


    /// <summary>
    /// 连续内存的数组
    /// </summary>
    public class Array : IDisposable
    {

        private int count=0;

        private int itemSize=0;

        protected IntPtr head = IntPtr.Zero;


        public Array(int count, int itemSize)
        {
            if(itemSize<=0)
                throw new ArgumentException("itemSize is not illegal");
            this.head = Marshal.AllocHGlobal(count*itemSize);
            this.count = count;
            this.itemSize = itemSize;
        }

        /// <summary>
        /// 数组所占用的字节数
        /// </summary>
        public int Size
        {
            get
            {
                return count * itemSize;
            }
        }

        /// <summary>
        /// head pointer of the first element
        /// </summary>
        public IntPtr Header
        {
            get
            {
                return this.head;
            }
        }

        public unsafe void* this[int index]
        {
            get
            {
                if (index < 0 || index >= this.count)
                   throw new IndexOutOfRangeException("out of range");

                return (void*)(this.Header + (index * itemSize));
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }




        /// <summary>
        /// Dispose data
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        private bool disposed = false;

        private void Free()
        {
            if (this.head != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.head);
                this.head = IntPtr.Zero;
                this.count = 0;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.

                }
                this.Free();
                this.disposed = true;
            }
        }
    }


    public class Vertex3DArray : Array
    {
        public unsafe Vertex3DArray(int count)
            : base(count, sizeof(Vertex3D))
        {
        }

        public new unsafe Vertex3D* this[int index]
        {
            get
            {
                return (Vertex3D*)base[index];
            }
        }
    }


    public class ColorArray : Array
    {
        public unsafe ColorArray(int count):base(count,sizeof(ColorF)){

        }
        public new unsafe ColorF* this[int index]
        {
            get
            {
                return (ColorF*)base[index];
            }
        }
    }

    public class FloatArray : Array
    {
        public unsafe FloatArray(int count):base(count,sizeof(float))
        {

        }

        public new unsafe float* this[int index]
        {
            get
            {
                return (float*)base[index];
            }
        }
    }

    public class IntArray : Array
    {
        public unsafe IntArray(int count)
            : base(count, sizeof(int))
        {
        }

        
        public new unsafe int* this[int index]
        {
            get
            {
                return (int*)base[index];
            }
        }
    }

    public class ByteArray : Array
    {
        public unsafe ByteArray(int count)
            : base(count, sizeof(byte))
        {
        }

        public new unsafe byte* this[int index]
        {
            get
            {
                return (byte*)base[index];
            }
        }

    }

    public class UIntArray : Array
    {
        public unsafe UIntArray(int count):base(count,sizeof(uint)){
        }

        public new unsafe uint* this[int index]
        {
            get
            {
                return (uint*)base[index];
            }
        }


    }
    




    /// <summary>
    /// 用于记录非托管的数组。
    /// </summary>
    public class UnmanagedArray : IDisposable
    {

        protected int count;

       
        /// <summary>
        /// memsize of per element
        /// </summary>
        private  int elementSize;

        /// <summary>
        /// 用于记录非托管的数组。
        /// </summary>
        /// <param name="elementCount">元素数目。</param>
        /// <param name="elementSize">单个元素的字节数。</param>
        public UnmanagedArray(int elementCount, int elementSize)
        {
            
            int memSize = elementCount * elementSize;
            this.count = elementCount;
            this.elementSize = elementSize;
            this.ByteLength = memSize;
            this.Pointer = Marshal.AllocHGlobal(memSize);
        }

        /// <summary>
        /// 数组指针。
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// 申请到的字节数。（元素数目 * 单个元素的字节数）。
        /// </summary>
        public int ByteLength { get; set; }


       
        /// <summary>
        /// 元素数
        /// </summary>
        public int ElementCount {

            get
            {
               return this.count;
            }
        }

        

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

            if (this.Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
                this.Pointer = IntPtr.Zero;
                this.count = 0;
                
            }
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
