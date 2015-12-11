using SharpGL.SceneGraph;
using SimLab.GridderSources.Factory;
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
    public abstract class HexahedronGridderSource : GridderSource
    {

        private IList<int> _iBlocks;
        private IList<int> _jBlocks;
        private IList<int> _kBlocks;
        private Dictionary<int, bool> iSlices;
        private Dictionary<int, bool> jSlices;

        private int[] sliceVisibles;


        /// <summary>
        /// 切片同ActNum的AND后的结果，表示某个网格是否画不画
        /// </summary>
        private int[] bindVisibles;

        private Dictionary<int, bool> CreateSliceDict(IList<int> slices)
        {

            Dictionary<int, bool> result = new Dictionary<int, bool>();
            for (int i = 0; i < slices.Count; i++)
            {
                result.Add(slices[i], true);
            }
            return result;
        }


        public int[] Slices
        {
            get
            {
                return this.sliceVisibles;
            }
            protected set
            {
                this.sliceVisibles = value;
            }
        }


        /// <summary>
        /// ACTNUM 同 切面的可视合并结果
        /// </summary>
        public int[] BindVisibles
        {
            get
            {
                return this.bindVisibles;
            }
            protected set
            {
                this.bindVisibles = value;
            }

        }

        public IList<int> IBlocks
        {
            get { return this._iBlocks; }
            set
            {
                this._iBlocks = value;
                this.iSlices = CreateSliceDict(value);
            }
        }

        public IList<int> JBlocks
        {
            get { return this._jBlocks; }
            set
            {
                this._jBlocks = value;
                this.jSlices = CreateSliceDict(value);
            }
        }

        public IList<int> KBlocks
        {
            get
            {
                return this._kBlocks;
            }
            set
            {
                this._kBlocks = value;
            }
        }


        /// <summary>
        /// 判断(I,J,K)是否是切片的网格块
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool IsSliceBlock(int i, int j, int k)
        {
            int gridIndex;
            this.IJK2Index(i, j, k, out gridIndex);
            return sliceVisibles[gridIndex] > 0;
        }

        /// <summary>
        /// 判断IJ是否 处于IJ的平面
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool IsSliceBlock(int i, int j)
        {
            return iSlices.ContainsKey(i) && jSlices.ContainsKey(j);
        }


        /// <summary>
        /// 获取FRONT平面，LEFT TOP 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointFLT(int i, int j, int k);


        /// <summary>
        /// 获取FRONT平面，RIGHT TOP 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointFRT(int i, int j, int k);

        /// <summary>
        /// 获取FRONT平面，LEFT BUTTOM 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointFLB(int i, int j, int k);


        /// <summary>
        ///  获取FRONT平面，RIGHT BUTTOM 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointFRB(int i, int j, int k);


        /// <summary>
        /// 获取BACK平面，LEFT TOP 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointBLT(int i, int j, int k);

        /// <summary>
        /// 获取BACK平面，RIGHT TOP 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointBRT(int i, int j, int k);

        /// <summary>
        /// 获取BACK平面，LEFT BOTTOM 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointBLB(int i, int j, int k);


        /// <summary>
        ///  获取BACK平面，RIGHT BOTTOM 上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract Vertex PointBRB(int i, int j, int k);


        protected override Factory.GridBufferDataFactory CreateFactory()
        {
            return new HexahedronGridFactory();
        }


        protected void InitSliceVisibles()
        {
            this.Slices = CreateSliceVisibles();
            this.bindVisibles = BindCellActive(this.Slices, this.ActNums);
        }

        protected int[] CreateSliceVisibles()
        {
            int[] sliceVisibles = new int[this.DimenSize];
            //默认不显示
            for (int gridIndex = 0; gridIndex < sliceVisibles.Length; gridIndex++)
            {
                sliceVisibles[gridIndex] = 0;
            }

            {

                //计算显示的网格
                int I, J, K, gridIndex;
                for (int kindex = 0; kindex < _kBlocks.Count; kindex++)
                {
                    for (int jindex = 0; jindex < _jBlocks.Count; jindex++)
                    {
                        for (int iindex = 0; iindex < _iBlocks.Count; iindex++)
                        {
                            I = _iBlocks[iindex];
                            J = _jBlocks[jindex];
                            K = _kBlocks[kindex];
                            this.IJK2Index(I, J, K, out gridIndex);
                            sliceVisibles[gridIndex] = 1;
                        }
                    }
                }
            }
            return sliceVisibles;
        }

        /// <summary>
        /// 初始化切片可视性
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.InitSliceVisibles();
        }

        /// <summary>
        /// 重新生成切片信息
        /// </summary>
        public void RefreashSlices()
        {
            this.InitSliceVisibles();
        }

    }
}
