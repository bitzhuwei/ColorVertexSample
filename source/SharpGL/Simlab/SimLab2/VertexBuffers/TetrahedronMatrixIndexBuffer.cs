using SharpGL;
using SharpGL.SceneComponent;
using SimLab2.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    //TODO:可优化
    /// <summary>
    /// 基质(Matrix)为四面体时用此索引。
    /// </summary>
    public class TetrahedronMatrixIndexBuffer : IndexBuffer
    {
        public TetrahedronMatrixIndexBuffer()
            : base(OpenGL.GL_TRIANGLES)
        {

        }

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TetrahedronIndex>(elementCount);
        }
    }
}
