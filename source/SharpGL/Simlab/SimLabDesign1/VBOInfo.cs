using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public class VBOInfo
    {
        /// <summary>
        /// buffers[0] in GenBuffers(int n, uint[] buffers)
        /// <para>buffer in BindBuffer(uint target, uint buffer)</para>
        /// </summary>
        public uint BufferID { get; set; }

        /// <summary>
        /// target in BufferData(uint target, int size, IntPtr data, uint usage)
        /// </summary>
        public uint Target { get; set; }

        /// <summary>
        /// usage in BufferData(uint target, int size, IntPtr data, uint usage)
        /// </summary>
        public uint Usage { get; set; }


        /// <summary>
        /// 此VBO代表的数组在shader中的对应名称（例如in vec3 positions里的positions）
        /// <para>index in VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)</para>
        /// </summary>
        public uint AttribLocation { get; set; }

        /// <summary>
        /// size in VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// type in VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        /// </summary>
        public uint Type { get; set; }


        public override string ToString()
        {
            return string.Format("BufferID: {0}, Target: {1}, Usage: {2}, AttribLocation: {3}, Size: {4}, Type: {5}",
                BufferID, Target, Usage, AttribLocation, Size, Type);
            //return base.ToString();
        }
    }
}
