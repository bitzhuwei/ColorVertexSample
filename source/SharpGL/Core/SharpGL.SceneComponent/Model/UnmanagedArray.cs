using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// 顺序布局的Vertex，便于<see cref="UnmanagedArray"/>操作。
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vertex3D
    {
        public float X;
        public float Y;
        public float Z;

        public static implicit operator Vertex3D(Vertex v)
        {
            Vertex3D d;
            d.X = v.X;
            d.Y = v.Y;
            d.Z = v.Z;
            return d;
        }
    }

    /// <summary>
    /// 顺序布局的GLColor，便于<see cref="UnmanagedArray"/>操作。
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ColorF
    {

        public float R { get; private set; }

        public float G { get; private set; }

        public float B { get; private set; }

        public float A { get; private set; }

        public static implicit operator ColorF(GLColor color)
        {
            ColorF c = new ColorF() { R = color.R, G = color.G, B = color.B, A = color.A };
            return c;
        }

    }

    /// <summary>
    /// 元素类型为<see cref="SharpGL.SceneComponent.Vertex3D"/>的非托管数组。
    /// </summary>
    public class Vertex3DArray : UnmanagedArray
    {
        public unsafe Vertex3DArray(int count)
            : base(count, sizeof(Vertex3D))
        {
        }

        public unsafe Vertex3D* this[int index]
        {
            get
            {
                return (Vertex3D*)base.Get(index);
            }
        }
    }

    /// <summary>
    /// 元素类型为<see cref="SharpGL.SceneComponent.ColorF"/>的非托管数组。
    /// </summary>
    public class ColorFArray : UnmanagedArray
    {
        public unsafe ColorFArray(int count)
            : base(count, sizeof(ColorF))
        {

        }
        public unsafe ColorF* this[int index]
        {
            get
            {
                return (ColorF*)base.Get(index);
            }
        }
    }

    /// <summary>
    /// 元素类型为float的非托管数组。
    /// </summary>
    public class FloatArray : UnmanagedArray
    {
        public unsafe FloatArray(int count)
            : base(count, sizeof(float))
        {

        }

        public unsafe float* this[int index]
        {
            get
            {
                return (float*)base.Get(index);
            }
        }
    }

    /// <summary>
    /// 元素类型为Int32的非托管数组。
    /// </summary>
    public class IntArray : UnmanagedArray
    {
        public unsafe IntArray(int count)
            : base(count, sizeof(int))
        {
        }


        public unsafe int* this[int index]
        {
            get
            {
                return (int*)base.Get(index);
            }
        }
    }

    /// <summary>
    /// 元素类型为Byte的非托管数组。
    /// </summary>
    public class ByteArray : UnmanagedArray
    {
        public unsafe ByteArray(int count)
            : base(count, sizeof(Byte))
        {
        }

        public unsafe Byte* this[int index]
        {
            get
            {
                return (Byte*)base.Get(index);
            }
        }

    }

    /// <summary>
    /// 元素类型为UInt32的非托管数组。
    /// </summary>
    public class UIntArray : UnmanagedArray
    {
        public unsafe UIntArray(int count)
            : base(count, sizeof(UInt32))
        {
        }

        public unsafe UInt32* this[int index]
        {
            get
            {
                return (UInt32*)base.Get(index);
            }
        }

    }

    /// <summary>
    /// 用于记录非托管的数组。
    /// </summary>
    public abstract class UnmanagedArray : IDisposable
    {

        /// <summary>
        /// 单个元素的字节数。
        /// </summary>
        private int elementSize;

        /// <summary>
        /// 用于记录非托管的数组。
        /// </summary>
        /// <param name="elementCount">元素数目。</param>
        /// <param name="elementSize">单个元素的字节数。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public UnmanagedArray(int elementCount, int elementSize)
        {
            this.Count = elementCount;
            this.elementSize = elementSize;

            int memSize = elementCount * elementSize;
            this.Header = Marshal.AllocHGlobal(memSize);

            allocatedArrays.Add(this);
        }

        private static readonly List<UnmanagedArray> allocatedArrays = new List<UnmanagedArray>();

        /// <summary>
        /// 立即释放所有<see cref="UnmanagedArray"/>。
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void FreeAll()
        {
            foreach (var item in allocatedArrays)
            {
                item.Dispose();
            }
            allocatedArrays.Clear();
        }

        /// <summary>
        /// 数组指针。
        /// </summary>
        public IntPtr Header { get; private set; }

        /// <summary>
        /// 申请到的字节数。（元素数目 * 单个元素的字节数）。
        /// </summary>
        public int ByteLength
        {
            get { return this.Count * this.elementSize; }
        }

        /// <summary>
        /// 元素数目。
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 获取索引为<paramref name="index"/>的元素。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected unsafe void* Get(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new IndexOutOfRangeException("out of range");

            var pElement = this.Header + (index * elementSize);
            return pElement.ToPointer();
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
            IntPtr ptr = this.Header;

            if (ptr != IntPtr.Zero)
            {
                this.Count = 0;
                this.Header = IntPtr.Zero;
                Marshal.FreeHGlobal(ptr);

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
