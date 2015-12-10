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
        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
             return  new UnmanagedArray<HexahedronTexCoord>(elementCount);
        }
    }
}
