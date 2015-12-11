using SharpGL.SceneGraph;
using SimLab.GridderSources.Factory;
using SimLab.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridderSources
{
    /// <summary>
    /// 块为六面体组成的模拟网格几何信息,支持切片分析
    /// </summary>
    public class PointGridderSource : GridderSource
    {
       

        


        private Vertex[] positions;

        private float[] radius;

        protected override Factory.GridBufferDataFactory CreateFactory()
        {
            return new PointGridFactory();
        }

        /// <summary>
        /// 初始化切片可视性
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.InitRadius();
        }

        protected void InitRadius()
        {
            this.Radius = this.InitFloatArray(this.DimenSize, 1.0f);
        }


        public Vertex[] Positions
        {
            get { return this.positions; }
            set { this.positions = value; }
        }

        public float OriginalRadius
        {
            get;
            set;
        }


        /// <summary>
        /// 对应的每个点的半径
        /// </summary>
        public float[] Radius
        {
            get { return this.radius; }
            set { this.radius = value; }
        }

        public Vertex GetPosition(int i, int j, int k)
        {
            int gridIndex= this.GridIndexOf(i, j, k);
            return this.Positions[gridIndex];
        }

        protected int[] ExpandVisibles(int[] gridIndexes)
        {
            if (gridIndexes.Length == this.DimenSize)
            {
                return this.InitIntArray(this.DimenSize, 1);
            }
            else
            {
                int dimenSize = this.DimenSize;
                int[] visibles = InitIntArray(this.DimenSize, 0);
                for (int i = 0; i < gridIndexes.Length; i++)
                {
                    int gridIndex = gridIndexes[i];
                    if (gridIndex >= 0 && gridIndex < dimenSize)
                    {
                        visibles[i] = 1;
                    }

                }
                return visibles;
            }
        }

        /// <summary>
        /// 确定结果是否显示 返回数组大小为DimenSize
        /// </summary>
        /// <param name="gridIndexes"></param>
        /// <returns></returns>
        public int[] BindResultsVisibles(int[] gridIndexes)
        {
            int[] resultHas =  this.ExpandVisibles(gridIndexes);
            return this.BindCellActive(resultHas, this.ActNums);
        }

        public new PointGridFactory Factory
        {
            get
            {
               return (PointGridFactory)base.Factory;
            }
        }

        public PointRadiusBuffer CreateRadiusBuffer(float[] radius)
        {
           return  this.Factory.CreateRadiusBufferData(this, radius);
        }

        public PointRadiusBuffer CreateRadiusBuffer(float radius)
        {
            return this.Factory.CreateRadiusBufferData(this, radius);
        }


    }
}
