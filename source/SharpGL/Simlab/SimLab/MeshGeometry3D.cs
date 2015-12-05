using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public class MeshGeometry3D
    {
        /// <summary>
        /// 点的位置
        /// </summary>
        private PositionsBufferData positions;

        /// <summary>
        /// 三角形的索引
        /// </summary>
        private TriangleIndicesBufferData triangleIndices;

        public MeshGeometry3D(PositionsBufferData positions, TriangleIndicesBufferData triangleIndices)
        {
             this.positions = positions;
             this.triangleIndices = triangleIndices;
        }


        public PositionsBufferData Positions
        {
            get { return this.positions; }
        }

        public TriangleIndicesBufferData TriangleIndices
        {
            get { return this.triangleIndices; }
        }

        
    }
}
