using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    /// <summary>
    /// 描述由线段组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public class LineFractureTexCoordBufer : TexCoordBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<LineTexCoord>(elementCount);
        }

    }

    /// <summary>
    /// 描述由三角形组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public class TriangleFractureTexCoordBuffer : TexCoordBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TriangleTexCoord>(elementCount);
        }

    }

}
