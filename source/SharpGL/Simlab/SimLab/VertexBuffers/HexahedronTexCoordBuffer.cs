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
    /// 描述六面体的8个顶点的颜色。
    /// </summary>
    public class HexahedronTexCoordBuffer : TexCoordBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<HexahedronTexCoord>(elementCount);
        }
    }
}
