using SharpGL;
using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 用于存储索引的VBO。
    /// </summary>
    public abstract class IndexBuffer : VertexBuffer
    {
        /// <summary>
        /// 用于存储索引的VBO。
        /// </summary>
        /// <param name="mode">用哪种方式渲染各个顶点？（OpenGL.GL_TRIANGLES etc.）</param>
        public IndexBuffer(uint mode)
            : base()
        {
            this.Mode = mode;
        }


        /// <summary>
        /// 用哪种方式渲染各个顶点？（OpenGL.GL_TRIANGLES etc.）
        /// </summary>
        public uint Mode { get; private set; }

        /// <summary>
        /// 索引数组中有多少个元素。
        /// </summary>
        public int ElementCount
        {
            get
            {
                UnmanagedArrayBase array = this.array;
                if (array != null)
                {
                    return array.ByteLength / sizeof(uint);
                }
                else
                {
                    return 0;
                }
            }
        }

        public override uint CreateVBO(UsageType usage)
        {
            OpenGL gl = new OpenGL();

            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, buffers[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.SizeInBytes, this.Data, (uint)usage);

            return buffers[0];
        }
    }
}
