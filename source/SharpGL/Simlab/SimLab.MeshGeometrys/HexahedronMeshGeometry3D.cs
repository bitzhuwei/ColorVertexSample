using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.MeshGeometrys
{
    public class HexahedronMeshGeometry3D : MeshBase
    {
        /// <summary>
        /// 三角形的索引
        /// </summary>
        private TriangleIndicesBufferData triangleIndices;

        public HexahedronMeshGeometry3D(PositionsBufferData positions, TriangleIndicesBufferData triangleIndices)
            :base(positions)
        {
            this.triangleIndices = triangleIndices;
        }


        public TriangleIndicesBufferData TriangleIndices
        {
            get { return this.triangleIndices; }
        }

    }
}
