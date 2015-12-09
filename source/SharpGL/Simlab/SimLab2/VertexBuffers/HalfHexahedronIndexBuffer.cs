using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
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

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<HalfHexahedronIndex>(elementCount);
            this.ElementCount = elementCount * 9;
        }
    }
}
