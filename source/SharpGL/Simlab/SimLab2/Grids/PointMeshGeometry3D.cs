using SimLab2.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public class PointMeshGeometry3D : MeshBase
    {
        private RadiusBuffer radius;

        public RadiusBuffer Radius
        {
            get { return radius; }
        }

        /// <summary>
        /// 顶点数目
        /// </summary>
        private int count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="count">有多少个顶点</param>
        public PointMeshGeometry3D(PositionBuffer positions, RadiusBuffer radius, int count)
            : base(positions)
        {
            this.radius = radius;
            this.count = count;
        }


        public int Count
        {
            get { return this.count; }
        }

    }
}
