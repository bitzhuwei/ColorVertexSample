using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 用3个QUAD渲染半个六面体。
    /// </summary>
    public class HalfHexahedronIndexBuffer : IndexBuffer
    {
        public HalfHexahedronIndexBuffer()
            : base(OpenGL.GL_QUAD_STRIP)
        {

        }

        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
             return new UnmanagedArray<HalfHexahedronIndex>(elementCount);
        }
    }
}
