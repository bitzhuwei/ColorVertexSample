using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2
{
    public class PointMeshGeometry3D : MeshBase
    {
        private RadiusBufferData radius;

        public RadiusBufferData Radius
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
        public PointMeshGeometry3D(PositionsBufferData positions, RadiusBufferData radius, int count)
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
