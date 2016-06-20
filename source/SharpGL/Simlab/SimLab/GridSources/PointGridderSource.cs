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

        private float[] radius;

        protected override GridBufferDataFactory Factory
        {
            get
            {
                return new PointGridFactory();
            }
        }

        /// <summary>
        /// 初始化切片可视性
        /// </summary>
        public override void Init()
        {
            base.Init();

            this.Radius = ArrayHelper.NewFloatArray(this.DimenSize, 1.0f);
        }

        public Vertex[] Positions { get; set; }

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
            if (gridIndexes.Length == this.DimenSize)
            {
                return ArrayHelper.NewIntArray(this.DimenSize, 1);
            }
            else
            {
                int dimenSize = this.DimenSize;
                int[] visibles = ArrayHelper.NewIntArray(this.DimenSize, 0);
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
            int[] resultHas = this.ExpandVisibles(gridIndexes);
            return this.BindCellActive(resultHas, this.activeBlocks);
        }

        public PointRadiusBuffer CreateRadiusBuffer(float[] radius)
        {
            return (this.Factory as PointGridFactory).CreateRadiusBufferData(this, radius);
        }

        public PointRadiusBuffer CreateRadiusBuffer(float radius)
        {
            return (this.Factory as PointGridFactory).CreateRadiusBufferData(this, radius);
        }


        protected override void InitGridCoordinates()
        {
            //do nothing;
        }

        protected override SharpGL.SceneComponent.Rectangle3D InitSourceActiveBounds()
        {
            if (this.Positions == null || this.Positions.Length <= 0)
            { throw new ArgumentException("Points has No Value"); }

            Vertex v = this.Positions[0];
            SharpGL.SceneComponent.Rectangle3D rect3d = new SharpGL.SceneComponent.Rectangle3D(v, v);
            for (int i = 0; i < this.Positions.Length; i++)
            {
                rect3d.Union(this.Positions[i]);
            }
            return rect3d;
        }

    }
}
