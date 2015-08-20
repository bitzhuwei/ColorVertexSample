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
        /// 描述三角形，四面体的坐标点
        /// </summary>
        private Vertex[] coords;

        /// <summary>
        /// 三角行描述索引，一个int[]表示一个三角形，对应的三角型由coords来确定
        /// 三角形由int[] 来描述，索引int[0]，int[1]，int[2]表示一个三角形；int[3] 表示三角形的标记，此标记值-1表示无效值,有效值表示依靠
        /// </summary>
        private int[][] triangles;

        
        /// <summary>
        /// 裂缝，
        /// 数组的值为triangles的索引,格式同上，int[3]全为有效值
        /// </summary>
        private int[][] fractions;


        /// <summary>
        /// 边界
        /// 数组的值为triangles的索引,格式同上，int[3]全为有效值
        /// </summary>
        private int[][] borders;


       
        /// <summary>
        /// 四面体网格
        /// 四面体格式 int[0],int[1],int[2],int[3] 分别表示triangles描述的三角形；
        /// 4个三角形构造出一个四面体, int[4]表示结果中映射的属性值
        /// </summary>
        private int[][] tetras;


        /// <summary>
        /// 描述三角形，四面体的共享坐标点
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
        /// 三角行描述索引，一个int[]表示一个三角形，对应的三角型由coords来确定
        /// 1. 三角形由int[] 来描述，索引int[0]，int[1]，int[2]表示一个三角形；
        /// 2. int[3] 表示三角形的标记，此标记值-1表示无效值,有效值某种标记，例如裂缝或边界
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
        /// 裂缝 ,三角形表示
        /// 
        /// 1. 三角形由int[] 来描述，索引int[0]，int[1]，int[2]表示一个三角形；
        /// 2. int[3] 表示裂缝标记
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
        /// 四面体格式 int[0],int[1],int[2],int[3] 分别表示triangles描述的三角形；
        /// 4个三角形构造出一个四面体, int[4]表示结果中映射的属性值
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
        /// Fracitons三角形个数同四边形个数之和
        /// </summary>
        public int FractionTetraCount
        {
            get
            {
                return this.Fractions.Length + this.Tetras.Length;
            }

        }


       

        /// <summary>
        /// 边界 ,三角形表示
        /// 1. 三角形由int[] 来描述，索引int[0]，int[1]，int[2]表示一个三角形；
        /// 2. int[3] 表示三角形的Border标记;
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

      
    }
}
