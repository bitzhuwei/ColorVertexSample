using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSource
{

    public class CornerPointGridderSource : HexahedronGridderSource
    {
        public float[] COORDS { get; set; }

        public float[] ZCORN { get; set; }


        /// <summary>
        /// 返回Front Left Top 的左上角的点(x)的起始偏移量
        /// </summary>
        /// <param name="iv"></param>
        /// <param name="jv"></param>
        /// <returns></returns>
        public int IndexOfCoordFLT(int i, int j)
        {
            return (i - 1) * 2 * 3 + ((this.NX + 1) * 2 * 3) * (j - 1);
        }

        /// <summary>
        /// 返回(I,J)Front Left Bottom 的左上角的点(x)的起始偏移量
        /// </summary>
        /// <param name="iv"></param>
        /// <param name="jv"></param>
        /// <returns></returns>
        public int IndexOfCoordFLB(int iv, int jv)
        {
            //return this.IndexOfCoordFLT(iv, jv) + 1 * 3;
            return IndexOfCoordFLT(iv, jv);
        }

        public int IndexOfCoordFRT(int i, int j)
        {
            //return this.IndexOfCoordFLT(i, j) + 2 * 3;
            return IndexOfCoordFLT(i + 1, j);
        }

        public int IndexOfCoordFRB(int i, int j)
        {
            //return this.IndexOfCoordFLT(iv, jv) + 3 * 3;
            return IndexOfCoordFRT(i, j);
        }

        public int IndexOfCoordBLT(int i, int j)
        {
            return (i - 1) * 2 * 3 + (this.NX + 1) * 2 * 3 * (j);
        }

        public int IndexOfCoordBLB(int i, int j)
        {
            //return this.IndexOfCoordBLT(iv, jv) + 1 * 3;
            return IndexOfCoordBLT(i, j);
        }

        public int IndexOfCoordBRT(int i, int j)
        {
            //return this.IndexOfCoordBLT(i, j) + 2 * 3;
            return IndexOfCoordBLT(i + 1, j);
        }

        public int IndexOfCoordBRB(int i, int j)
        {
            //return this.IndexOfCoordBLT(i, j) + 3 * 3;
            return this.IndexOfCoordBRT(i, j);
        }


        public int IndexOfZCornFLT(int iv, int jv, int kv)
        {
            return ((kv - 1) * 2) * (this.NX * 2) * (this.NY * 2) + ((jv - 1) * 2) * (this.NX * 2) + (iv - 1) * 2;
        }

        public int IndexOfZCornFRT(int iv, int jv, int kv)
        {
            return this.IndexOfZCornFLT(iv, jv, kv) + 1;
        }

        public int IndexOfZCornFLB(int iv, int jv, int kv)
        {
            return ((kv - 1) * 2 + 1) * (this.NX * 2) * (this.NY * 2) + ((jv - 1) * 2) * (this.NX * 2) + (iv - 1) * 2;
        }

        public int IndexOfZCornFRB(int i, int j, int k)
        {
            return this.IndexOfZCornFLB(i, j, k) + 1;
        }

        public int IndexOfZCornBLT(int i, int j, int k)
        {
            return ((k - 1) * 2) * (this.NX * 2) * (this.NY * 2) + ((j - 1) * 2 + 1) * (this.NX * 2) + (i - 1) * 2;
        }

        public int IndexOfZCornBRT(int i, int j, int k)
        {
            return this.IndexOfZCornBLT(i, j, k) + 1;
        }

        public int IndexOfZCornBLB(int i, int j, int k)
        {
            return ((k - 1) * 2 + 1) * (this.NX * 2) * (this.NY * 2) + ((j - 1) * 2 + 1) * (this.NX * 2) + (i - 1) * 2;
        }

        public int IndexOfZCornBRB(int i, int j, int k)
        {
            return this.IndexOfZCornBLB(i, j, k) + 1;
        }


        public override Vertex PointFLT(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordFLT(i, j);
            int zindex = this.IndexOfZCornFLT(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointFRT(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordFRT(i, j);
            int zindex = this.IndexOfZCornFRT(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointFLB(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordFLB(i, j);
            int zindex = this.IndexOfZCornFLB(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointFRB(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordFRB(i, j);
            int zindex = this.IndexOfZCornFRB(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }


        public override Vertex PointBLT(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordBLT(i, j);
            int zindex = this.IndexOfZCornBLT(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBRT(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordBRT(i, j);
            int zindex = this.IndexOfZCornBRT(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBLB(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordBLB(i, j);
            int zindex = this.IndexOfZCornBLB(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBRB(int i, int j, int k)
        {
            int xyzOffset = this.IndexOfCoordBRB(i, j);
            int zindex = this.IndexOfZCornBRB(i, j, k);
            float x = this.COORDS[xyzOffset];
            float y = this.COORDS[xyzOffset + 1];
            float z = this.ZCORN[zindex];
            Vertex p = new Vertex(x, y, z);
            return p;
        }

    }
    
}
