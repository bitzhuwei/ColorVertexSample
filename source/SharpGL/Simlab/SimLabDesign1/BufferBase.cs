using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
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

        public virtual void FetchInfoFromShaderProgram(OpenGL gl, SharpGL.Shaders.ShaderProgram shaderProgram)
        {

        }

        public abstract void LayoutForVAO(OpenGL gl);
    }
}
