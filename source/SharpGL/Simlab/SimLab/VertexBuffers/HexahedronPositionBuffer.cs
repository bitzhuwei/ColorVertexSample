using SharpGL.SceneComponent;
using SimLab.GridSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 描述六面体的顶点的位置。
    /// </summary>
    public class HexahedronPositionBuffer : PositionBuffer
    {
        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<HexahedronPosition>(elementCount);
        }
    }
}
