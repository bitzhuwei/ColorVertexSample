using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    /// <summary>
    /// 描述点集的顶点的位置。
    /// </summary>
    class PointPositionBuffer : PositionBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<Vertex>(elementCount);
        }
    }
}
