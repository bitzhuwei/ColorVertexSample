using SharpGL.SceneGraph;
using SimLab.SimGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridderSources
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


        /// <summary>
        /// 数组大小为 (nx+1)*(ny+1)*(nz+1);
        /// </summary>
        private float[] xcoords;

        /// <summary>
        ///  数组大小为 (nx+1)*(ny+1)*(nz+1);
        /// </summary>
        private float[] ycoords;
        private float[] zcoords;

        private GridIndexer coordIndexer;


        /// <summary>
        /// 网格起始原点X坐标
        /// </summary>
        public float OX
        {
            get;
            set;
        }

        public float OY
        {
            get;
            set;
        }

        public float OZ
        {
            get;
            set;
        }



   
        /// <summary>
        /// 前左上角坐标
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public override Vertex PointFLT(int i, int j, int k)
        {
           Vertex p = new Vertex();
           int gridIndex = this.coordIndexer.IndexOf(i, j, k);
           p.X= this.xcoords[gridIndex];
           p.Y = this.ycoords[gridIndex];
           p.Z = this.zcoords[gridIndex];
            return p;
        }
        public override Vertex PointFRT(int i, int j, int k)
        {
            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i+1,j,k);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;
        }
        public override Vertex PointFLB(int i, int j, int k)
        {
            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i, j, k + 1);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;
        }

        public override Vertex PointFRB(int i, int j, int k)
        {
            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i + 1, j, k + 1);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;
        }
        public override Vertex PointBLT(int i, int j, int k)
        {

            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i, j + 1, k);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;

        }
        public override Vertex PointBRT(int i, int j, int k)
        {

            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i + 1, j + 1, k);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;
        }

        public override Vertex PointBLB(int i, int j, int k)
        {

            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i, j + 1, k + 1);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;


         
        }

        public override Vertex PointBRB(int i, int j, int k)
        {

            Vertex p = new Vertex();
            int gridIndex = this.coordIndexer.IndexOf(i + 1, j + 1, k + 1);
            p.X = this.xcoords[gridIndex];
            p.Y = this.ycoords[gridIndex];
            p.Z = this.zcoords[gridIndex];
            return p;
        }

        /// <summary>
        /// 初始化网格坐标
        /// </summary>
        protected void InitGridCoordinates()
        {
                //xcoords;
            int coordSize = (this.NX + 1) * (this.NY + 1) * (this.NZ + 1);
            float[] coordX  = new float[coordSize];
            float[] coordY =   new float[coordSize];
            float[] coordZ =   new float[coordSize];

            float[] srcDX = this.DX;
            float[] srcDY = this.DY;
            float[] srcDZ = this.DZ;
            int cnx = this.NX+1;
            int cny = this.NY+1;
            int cnz = this.NZ+1;

            int dnx = this.NX;
            int dny = this.NY;
            int dnz = this.NZ;

            GridIndexer  coordIndexer = new GridIndexer(cnx, cny, cnz);
            //dx, dy,dx 描述
            GridIndexer  dIndexer = new GridIndexer(this.NX, this.NY, this.NZ);

            int  coordIndex;
            int  prevcIndex;
            int  di,dj,dk, xGridIndex,yGridIndex,zGridIndex;
            for (int kcz = 1; kcz <= cnz; kcz++)
            {
                for (int jcy = 1; jcy <= cny; jcy++)
                {
                    for (int icx = 1; icx <= cnx; icx++)
                    {
                         coordIndex = coordIndexer.IndexOf(icx, jcy, kcz);

                        //处理x坐标
                         if (icx == 1)
                         {
                             coordX[coordIndex] = this.OX;
                         }
                         else
                         {
                              prevcIndex = coordIndexer.IndexOf(icx - 1, jcy, kcz);
                              //距离坐标
                              di = icx-1;
                              dj = jcy > dny ? jcy-1: jcy;
                              dk = kcz > dnz ? kcz-1:kcz;
                              xGridIndex = dIndexer.IndexOf(di,dj,dk);
                              coordX[coordIndex] = coordX[prevcIndex] + srcDX[xGridIndex];
                         }

                        //计算(icx,jcy,kcz)网格的坐标
                         if (jcy == 1)
                         {
                             coordY[coordIndex] = this.OY;
                         }
                         else
                         {
                             prevcIndex = coordIndexer.IndexOf(icx, jcy - 1, kcz);
                             di = icx > dnx ? icx - 1 : icx;
                             dj = jcy - 1;
                             dk = kcz > dnz ? kcz - 1 : kcz;
                             yGridIndex = dIndexer.IndexOf(di, dj, dk);
                             coordY[coordIndex] = coordY[prevcIndex] + srcDY[yGridIndex];
                         }

                         if (kcz == 1)
                         {
                             coordZ[coordIndex] = this.OZ;
                         }
                         else
                         {
                             prevcIndex = coordIndexer.IndexOf(icx, jcy, kcz - 1);
                             di = icx > dnx ? dnx : icx;
                             dj = jcy > dny ? dny : jcy;
                             dk = kcz - 1;
                             zGridIndex = dIndexer.IndexOf(di, dj, dk);
                             coordZ[coordIndex] = coordZ[prevcIndex] + srcDZ[zGridIndex];
                         }
                    }
                }
            }
            this.xcoords = coordX;
            this.ycoords = coordY;
            this.zcoords = coordZ;
            this.coordIndexer = coordIndexer;

        }

        public override void Init()
        {
               base.Init();
              //初始化Coordinates
               InitGridCoordinates();
        }

    }
}
