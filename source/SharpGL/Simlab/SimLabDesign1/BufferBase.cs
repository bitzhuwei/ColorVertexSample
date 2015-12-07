using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    /// <summary>
    /// vertex buffer object的基类。
    /// </summary>
    public abstract class BufferBase
    {
        /// <summary>
        /// buffers[0] in GenBuffers(int n, uint[] buffers)
        /// <para>buffer in BindBuffer(uint target, uint buffer)</para>
        /// </summary>
        public uint BufferID { get; set; }

        ///// <summary>
        ///// target in BufferData(uint target, int size, IntPtr data, uint usage)
        ///// </summary>
        //public TargetType Target { get; set; }

        /// <summary>
        /// usage in BufferData(uint target, int size, IntPtr data, uint usage)
        /// </summary>
        public UsageType Usage { get; set; }

        public abstract uint Target { get; }

        public override string ToString()
        {
            return string.Format("BufferID: {0}, Usage: {1}",
                BufferID, Usage);
            //return base.ToString();
        }

        /// <summary>
        /// 从<paramref name="shaderProgram"/>中获取各个in变量的指针。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="shaderProgram"></param>
        public virtual void FetchInfoFromShaderProgram(OpenGL gl, SharpGL.Shaders.ShaderProgram shaderProgram)
        {

        }

        /// <summary>
        /// </summary>
        /// <param name="gl"></param>
        public abstract void LayoutForVAO(OpenGL gl);
    }
}
