using SharpGL.SceneComponent;
using SimLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 顶点缓存（VBO）
    /// </summary>
    public abstract class VertexBuffer : Disposable
    {
        protected UnmanagedArrayBase array;

        /// <summary>
        /// 此VBO中的数据在内存中的起始地址
        /// </summary>
        public IntPtr Data
        {
            get { return (this.array == null) ? IntPtr.Zero : this.array.Header; }
        }


        /// <summary>
        /// 此VBO中的数据在内存中的内存大小（单位：字节）
        /// </summary>
        public int SizeInBytes
        {
            get { return (this.array == null) ? 0 : this.array.ByteLength; }
        }


        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        public abstract void AllocMem(int elementCount);

        protected void FreeMem()
        {
            this.array.Dispose();

            this.array = null;
        }

        protected override void DisposeUnmanagedResources()
        {
            //recuresive distroy
            if (this.Data != IntPtr.Zero)
            {
                this.FreeMem();
            }
            base.DisposeUnmanagedResources();
        }
    }

}
