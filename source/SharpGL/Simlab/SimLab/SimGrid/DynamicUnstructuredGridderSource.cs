using SharpGL.SceneGraph;
using SimLab.GridSource;
using SimLab.SimGrid.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid
{
    /// <summary>
    /// 无结构的四面体网格，包含二维无结构和三维四面体网格的格式,
    /// 文件内容分为三个段,依次为nodes,elements,fractures.
    /// nodes为（x,y,z,0)的数组
    /// elements元素为nodes数组的索引,element[ELEMENT_FORMAT3+1](三角形) 或element[ELEMENT_FORMAT4+1](4面体)
    /// fratures元素为node数组的索引, fracture[FRACTURE_FORMAT2+1] (线段） 或fracture[FRACTURE_FORMAT3+1](三角形)]  
    /// elements.Length+fractures.Length = NX*NY*NZ ,通常NY,NZ =1， 所以NX = (elements.length+fratures.length)
    /// </summary>
    public class DynamicUnstructuredGridderSource : GridderSource
    {
        /// <summary>
        /// 组成母体的形状
        /// </summary>
        public const int MATRIX_FORMAT3_TRIANGLE = 3;
        public const int MATRIX_FORMAT4_TETRAHEDRON = 4;

        /// <summary>
        /// 组成裂缝的形状
        /// </summary>
        public const int FRACTURE_FORMAT2_LINE = 2;
        public const int FRACTURE_FORMAT3_TRIANGLE = 3;

        /// <summary>
        /// 文件头定义: 点的个数
        /// </summary>
        public int NodeNum { get; internal set; }

        public Vertex[] Nodes { get; set; }

        /// <summary>
        /// 如果nodeInElem 为NODE_FORMAT3 时，element部分表示三角形，elem
        /// </summary>
        public int ElementNum { get; internal set; }

        /// <summary>
        /// 基质几何结构描述
        /// </summary>
        public int[][] Elements { get; internal set; }

        /// <summary>
        /// 基质格式定义
        /// 当值为ElEMENT_FORMAT3,表示elements段为三角型，此时任意element为elements[i][ELEMENT_FORMAT3+1]，
        /// ELEMENT_FORMAT4时表示为四面体,此时elements[i][ELEMENT_FORMAT4+1]四面体
        /// 每个element数组最后一个描述保留，值为0
        /// </summary>
        public int ElementFormat { get; internal set; }

        /// <summary>
        /// 断层和裂缝数
        /// </summary>
        public int FractureNum { get; internal set; }

        public int[][] Fractures { get; internal set; }

        /// <summary>
        /// FRACTURE_FORMAT2是 fractures[i][FRACTURE_FORMAT2+1]
        /// FRACTURE_FORMAT2是 fractures[i][FRACTURE_FORMAT3+1]
        /// fracure[]中最后一个数组元素表示MARKER
        /// </summary>
        public int FractureFormat { get; internal set; }



        public override void Init()
        {
               base.Init();
        }

        public new Vertex Min
        {
            get
            {
               return  base.Min;
            }
            set
            {
                base.Min = value;
            }
        }


        public new Vertex Max
        {
            get
            {
                return base.Max;
            }
            set
            {
                base.Max = value;
            }
        }


        protected override GridSource.Factory.GridBufferDataFactory CreateFactory()
        {
            return new DynamicUnstructureGridFactory();
        }
    }
}
