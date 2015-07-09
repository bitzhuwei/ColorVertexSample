using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryModel
{

    /// <summary>
    /// 正交网格数据源
    /// </summary>
    public class CatesianGridderSource:HexahedronGridderSource
    {

        private float _dx;
        private float _dy;
        private float _dz;

        /// <summary>
        /// X(I)方向上的网格宽度
        /// </summary>
        public float DX
        {
            get { return this._dx; }
            set { this._dx = value; }
        }

        /// <summary>
        /// Y(J)方向上的网格宽度
        /// </summary>
        public float DY
        {
            get { return this._dy; }
            set { this._dy = value; }
        }

        /// <summary>
        /// Z(K)方向上的网格宽度
        /// </summary>
        public float DZ
        {
            get { return this._dz; }
            set { this._dz = value; }
        }


        public override Vertex PointFLT(int i, int j, int k)
        {
            float x = (i - 1) * this._dx;
            float y = (j - 1) * this._dy;
            float z = (k - 1) * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointFRT(int i, int j, int k)
        {
            float x = i * this._dx;
            float y = (j - 1) * this._dy;
            float z = (k - 1) * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointFLB(int i, int j, int k)
        {
            float x = (i - 1) * this._dx;
            float y = (j - 1) * this._dy;
            float z = k * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;

        }

        public override Vertex PointFRB(int i, int j, int k)
        {
            float x = i * this._dx;
            float y = (j - 1) * this._dy;
            float z = k * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointBLT(int i, int j, int k)
        {
            float x = (i - 1) * this._dx;
            float y = j * this._dy;
            float z = (k - 1) * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;

        }
        public override Vertex PointBRT(int i, int j, int k)
        {
            float x = i * this._dx;
            float y = j * this._dy;
            float z = (k - 1) * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBLB(int i, int j, int k)
        {
            float x = (i - 1) * this._dx;
            float y = j * this._dy;
            float z = k * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBRB(int i, int j, int k)
        {
            float x = i * this._dx;
            float y = j * this._dy;
            float z = k * this._dz;
            Vertex p = new Vertex(x, y, z);
            return p;
        }



    }
}
