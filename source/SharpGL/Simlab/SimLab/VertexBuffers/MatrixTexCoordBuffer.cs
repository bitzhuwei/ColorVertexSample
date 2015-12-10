using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{

    /// <summary>
    /// 描述由四面体组成的基质(Matrix)的顶点的颜色。
    /// </summary>
    public abstract class MatrixTexCoordBuffer : TexCoordBuffer
    {

    }

    /// <summary>
    /// 描述由四面体组成的基质(Matrix)的顶点的颜色。
    /// </summary>
    public class TetrahedronMatrixTexCoordBuffer : MatrixTexCoordBuffer
    {

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TetrahedronTexCoord>(elementCount);
        }

    }

    /// <summary>
    /// 描述由三角形组成的基质(Matrix)的顶点的颜色。
    /// </summary>
    public class TriangleMatrixTexCoordBuffer : MatrixTexCoordBuffer
    {

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TriangleTexCoord>(elementCount);
        }

    }
}
