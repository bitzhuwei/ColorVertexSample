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

    /// <summary>
    /// 描述由线段组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public abstract class FractureTexCoordBuffer : TexCoordBuffer
    {

    }

    /// <summary>
    /// 描述由线段组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public class LineFractureTexCoordBufer : FractureTexCoordBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<LineTexCoord>(elementCount);
        }

    }

    /// <summary>
    /// 描述由三角形组成的裂缝(fracture)的顶点的颜色。
    /// </summary>
    public class TriangleFractureTexCoordBuffer : FractureTexCoordBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TriangleTexCoord>(elementCount);
        }

    }

}
