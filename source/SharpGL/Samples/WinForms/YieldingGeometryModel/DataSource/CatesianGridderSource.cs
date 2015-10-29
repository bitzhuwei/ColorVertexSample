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
        public float[] DX { get; set; }

        /// <summary>
        /// Y(J)方向上的网格宽度
        /// </summary>
        public float[] DY { get; set; }

        /// <summary>
        /// Z(K)方向上的网格宽度
        /// </summary>
        public float[] DZ { get; set; }



        private float GetCellX(int iCount,int jIndex,int kIndex)
        {
           
            float x = 0;
            for(int iIndex =0; iIndex<iCount; iIndex++){
               int gridIndex = kIndex * (this.NX * this.NY) + jIndex * this.NX + iIndex;
               x += DX[gridIndex];
            }
            return x;
        }

        private float GetCellY(int iIndex,int jCount,int kIndex)
        {

            float y = 0;
            for (int jIndex = 0; jIndex < jCount; jIndex++)
            {
                int gridIndex = kIndex * (this.NX * this.NY) + jIndex * this.NX + iIndex;
                y += DY[gridIndex];
            }
            return y;
        }

        private float GetCellZ(int iIndex, int jIndex,int zCount)
        {
            float z = 0;
            for (int kIndex = 0; kIndex < zCount; kIndex++)
            {
                int gridIndex = kIndex * (this.NX * this.NY) + jIndex * this.NX + iIndex;
                z += DZ[gridIndex];
            }
            return z;
        }

        public override Vertex PointFLT(int i, int j, int k)
        {
            float x =  GetCellX(i-1,j-1,k-1);
            float y =  GetCellY(i-1,j-1,k-1);
            float z =  GetCellZ(i-1,j-1,k-1);
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointFRT(int i, int j, int k)
        {
            float x = GetCellX(i,j-1,k-1);
            float y = GetCellY(i-1,j-1,k-1);
            float z = GetCellZ(i-1,j-1,k-1);
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointFLB(int i, int j, int k)
        {
            float x = GetCellX(i-1,j-1,k-1);
            float y = GetCellY(i-1,j-1,k-1);
            float z = GetCellZ(i-1,j-1,k);
            Vertex p = new Vertex(x, y, z);
            return p;

        }

        public override Vertex PointFRB(int i, int j, int k)
        {

            float x = GetCellX(i,j-1,k-1);
            float y = GetCellY(i-1,j-1,k-1);
            float z = GetCellZ(i-1,j-1,k);
            Vertex p = new Vertex(x, y, z);
            return p;
        }
        public override Vertex PointBLT(int i, int j, int k)
        {
            float x = GetCellX(i-1,j-1,k-1);
            float y = GetCellY(i-1,j,k-1);
            float z = GetCellZ(i-1,j-1,k - 1);
            Vertex p = new Vertex(x, y, z);
            return p;

        }
        public override Vertex PointBRT(int i, int j, int k)
        {
            float x = GetCellX(i,j-1,k-1);
            float y = GetCellY(i-1,j,k-1);
            float z = GetCellZ(i-1,j-1,k-1);
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBLB(int i, int j, int k)
        {
            float x = GetCellX(i - 1,j-1,k-1);
            float y = GetCellY(i-1,j,k-1);
            float z = GetCellZ(i-1,j-1,k);
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override Vertex PointBRB(int i, int j, int k)
        {
            float x = GetCellX(i,j-1,k-1);
            float y = GetCellY(i-1,j,k-1);
            float z = GetCellZ(i-1,j-1,k);
            Vertex p = new Vertex(x, y, z);
            return p;
        }

    }
}
