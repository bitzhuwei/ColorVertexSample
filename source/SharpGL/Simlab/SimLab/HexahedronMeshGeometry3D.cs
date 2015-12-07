using SharpGL.SceneGraph;
using SimLab.SimGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public class HexahedronMeshGeometry3D : MeshBase
    {
        /// <summary>
        /// 三角形的索引
        /// </summary>
        private HalfHexahedronIndicesBufferData halfHexahedronIndices;

        public HexahedronMeshGeometry3D(PositionsBufferData positions, HalfHexahedronIndicesBufferData triangleIndices)
            :base(positions)
        {
            this.halfHexahedronIndices = triangleIndices;
        }


        public HalfHexahedronIndicesBufferData HalfHexahedronIndices
        {
            get { return this.halfHexahedronIndices; }
        }

    }
}
