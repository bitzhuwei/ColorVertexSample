using SharpGL.SceneGraph;
using SimLab.GridSource.Factory;
using SimLab.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSource
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
            int gridIndex = this.GridIndexOf(i, j, k);
            return this.Positions[gridIndex];
        }

        protected int[] ExpandVisibles(int[] gridIndexes)
        {

            int dimenSize = this.DimenSize;
            int[] visibles = InitIntArray(this.DimenSize, 0);
            for (int i = 0; i < gridIndexes.Length; i++)
            {
                int gridIndex = gridIndexes[i];
                int[] mappedBlockIndexes = this.MapBlockIndexes(gridIndex);
                for (int j = 0; j < mappedBlockIndexes.Length; j++)
                {
                    int block = mappedBlockIndexes[j];
                    if (block >= 0 && block < dimenSize)
                    {
                        visibles[block] = 1;
                    }
                }
            }
            return visibles;

        }

        /// <summary>
        /// 确定结果是否显示 返回数组大小为DimenSize
        /// </summary>
        /// <param name="gridIndexes"></param>
        /// <returns></returns>
        public int[] BindResultsVisibles(int[] gridIndexes)
        {
            int[] blockResultVisibles = this.ExpandVisibles(gridIndexes);
            return this.BindVisibles(blockResultVisibles, this.ActNums);
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
            return this.Factory.CreateRadiusBufferData(this, radius);
        }

        public PointRadiusBuffer CreateRadiusBuffer(float radius)
        {
            return this.Factory.CreateRadiusBufferData(this, radius);
        }


        protected override void InitGridCoordinates()
        {
            //do nothing;
        }

        protected override SharpGL.SceneComponent.Rectangle3D InitSourceActiveBounds()
        {
            if (positions == null || this.positions.Length <= 0)
                throw new ArgumentException("Points has No Value");
            Vertex v = positions[0];
            SharpGL.SceneComponent.Rectangle3D rect3d = new SharpGL.SceneComponent.Rectangle3D(v, v);
            for (int i = 0; i < this.positions.Length; i++)
            {
                rect3d.Union(this.positions[i]);
            }
            return rect3d;
        }

    }
}
