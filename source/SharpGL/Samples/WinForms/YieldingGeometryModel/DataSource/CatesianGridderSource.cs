using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{

    /// <summary>
    /// 正交网格数据源
    /// </summary>
    public class CatesianGridderSource : HexahedronGridderSource
    {

        /// <summary>
        /// X(I)方向上的网格宽度
        /// </summary>
        public float DX { get; set; }

        /// <summary>
        /// Y(J)方向上的网格宽度
        /// </summary>
        public float DY { get; set; }

        /// <summary>
        /// Z(K)方向上的网格宽度
        /// </summary>
        public float DZ { get; set; }


        public override Vertex PointFLT(int i, int j, int k)
        {
            float x = (i - 1) * this.DX;
            float y = (j - 1) * this.DY;
            float z = (k - 1) * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointFRT(int i, int j, int k)
        {
            float x = i * this.DX;
            float y = (j - 1) * this.DY;
            float z = (k - 1) * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointFLB(int i, int j, int k)
        {
            float x = (i - 1) * this.DX;
            float y = (j - 1) * this.DY;
            float z = k * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;

        }

        public override Vertex PointFRB(int i, int j, int k)
        {
            float x = i * this.DX;
            float y = (j - 1) * this.DY;
            float z = k * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointBLT(int i, int j, int k)
        {
            float x = (i - 1) * this.DX;
            float y = j * this.DY;
            float z = (k - 1) * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;

        }
        public override Vertex PointBRT(int i, int j, int k)
        {
            float x = i * this.DX;
            float y = j * this.DY;
            float z = (k - 1) * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBLB(int i, int j, int k)
        {
            float x = (i - 1) * this.DX;
            float y = j * this.DY;
            float z = k * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBRB(int i, int j, int k)
        {
            float x = i * this.DX;
            float y = j * this.DY;
            float z = k * this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }



    }
}
