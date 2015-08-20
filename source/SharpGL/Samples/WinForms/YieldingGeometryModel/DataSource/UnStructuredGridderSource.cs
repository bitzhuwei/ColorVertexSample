using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.DataSource
{

    /// <summary>
    /// 非结构化网格
    /// </summary>
    public class UnStructuredGridderSource
    {
        /// <summary>
        /// 描述三角形，四面体的三维空间坐标点
        /// </summary>
        private Vertex[] coords;

        /// <summary>
        /// 三角行描述索引，一个int[]表示一个三角形，对应的三角型坐标由coords来确定,
        /// 任意一个三角形i对应的三角型为 int[] triangle = triangles[i];
        /// 在每个triangle中:
        /// 1.三角形由int[] 来描述，索引triangle[0]，triangle[1]，triangle[2]表示一个三角形的三个顶点对于coords的索引；
        /// 2. triangle[3] 表示一个标记，此标记值-1表示无效值,有效值>=0，标记指示此triangle用来表示裂缝(fraction)，或者表示Border三角形；
        /// </summary>
        private int[][] triangles;

        
        /// <summary>
        /// 裂缝描述，
        /// 同triangles描述
        /// 此时任意一个三角形triangle的triangle[3]!=-1,表示裂缝，具体可以参见UnstructureGeometryLoader的处理
        /// </summary>
        private int[][] fractions;


        /// <summary>
        /// 边界描述
        /// 数组的值为triangles的索引,格式同上，triangle[3]!=-1 表示三角形为边界.
        /// </summary>
        private int[][] borders;


        /// <summary>
        /// 网格最小坐标
        /// </summary>
        private Vertex min;

        /// <summary>
        /// 网格最大坐标
        /// </summary>
        private Vertex max;


       
        /// <summary>
        /// 四面体网格
        /// 任意一个四面体i,tetra表示为 int[] tetra=tetras[i];
        /// 其中:
        ///1. 四面体格式 tetra[0],tetra[1],tetra[2],tetra[3] 分别表示triangles描述的三角形,4个三角形构造出一个四面体；
        ///2. 任意一个三角形i的描述为 int[] triangle = triangles[tetra[i]];
        ///3. tetra[4]表示顺序
        /// </summary>
        private int[][] tetras;




        /// <summary>
        /// 描述三角形，四面体的三维空间坐标点
        /// </summary>
        public Vertex[] Coords
        {
            get
            {
                return this.coords;
            }
            set
            {
                this.coords = value;
            }
        }


        /// <summary>
        /// 三角行描述索引，一个int[]表示一个三角形，对应的三角型坐标由coords来确定,
        /// 任意一个三角形i对应的三角型为 int[] triangle = triangles[i];
        /// 在每个triangle中:
        /// 1.三角形由int[] 来描述，索引triangle[0]，triangle[1]，triangle[2]表示一个三角形的三个顶点对于coords的索引；
        /// 2. triangle[3] 表示一个标记，此标记值-1表示无效值,有效值>=0，标记指示此triangle用来表示裂缝(fraction)，或者表示Border三角形；
        /// </summary>
        public int[][] Triangles
        {
            get
            {

                return this.triangles;
            }
            set
            {
                this.triangles = value;
            }

        }


        /// <summary>
        /// 裂缝描述，
        /// 同triangles描述
        /// 此时任意一个三角形triangle的triangle[3]!=-1,表示裂缝，具体可以参见UnstructureGeometryLoader的处理
        /// </summary>
        public int[][] Fractions
        {
            get
            {
                return this.fractions;
            }
            set
            {
                this.fractions = value;
            }
        }


        /// <summary>
        /// 四面体网格
        /// 任意一个四面体i,tetra表示为 int[] tetra=tetras[i];
        /// 其中:
        ///1. 四面体格式 tetra[0],tetra[1],tetra[2],tetra[3] 分别表示triangles描述的三角形,4个三角形构造出一个四面体；
        ///2. 任意一个三角形i的描述为 int[] triangle = triangles[tetra[i]];
        ///3. tetra[4]表示顺序
        /// </summary>
        public int[][] Tetras
        {
            get
            {
               return this.tetras;
            }
            set
            {
               this.tetras = value;
            }
        }


        /// <summary>
        /// 裂缝同四边形网格的对象个数
        /// </summary>
        public int FractionTetraCount
        {
            get
            {
                return this.Fractions.Length + this.Tetras.Length;
            }

        }




        /// <summary>
        /// 边界描述
        /// 数组的值为triangles的索引,格式同上，triangle[3]!=-1 表示三角形为边界.
        /// </summary>
        public int[][] Borders
        {
            get
            {
                return this.borders;
            }
            set
            {
                this.borders = value;
            }
        }



        public Vertex Min
        {
            get
            {
                return this.min;
            }
            set
            {
                this.min = value;
            }
        }


        public Vertex Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

      
    }
}
