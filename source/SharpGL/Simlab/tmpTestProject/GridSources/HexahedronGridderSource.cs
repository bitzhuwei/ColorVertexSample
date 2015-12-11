using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SimLab.GridderSources;
using SimLab.SimGrid;
using SimLab.SimGrid.Geometry;
using SimLab.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSources
{
    public class HexahedronGridderSource : GridSource<HexahedronGrid>
    {

        private SimLab.SimGrid.GridIndexer gridIndexer;


        /// <summary>
        /// X轴方向上的网格数。
        /// </summary>
        public int NX { get; set; }

        /// <summary>
        /// Y轴方向上的网格数。
        /// </summary>
        public int NY { get; set; }

        /// <summary>
        /// Z轴方向上的网格数。
        /// </summary>
        public int NZ { get; set; }

        /// <summary>
        /// 获取网格包含的元素总数。
        /// </summary>
        public int DimenSize
        {
            get
            {
                return this.NX * this.NY * this.NZ;
            }
        }

        /// <summary>
        /// 每个网格一个值，全部为零，表示全部不可视
        /// </summary>
        private int[] zeroVisibles;


        /// <summary>
        /// 透明texure坐标,大小为DimenSize
        /// </summary>
        private float[] invisibleTextures;



        /// <summary>
        /// 此网格至少包含1个元素，返回true；否则返回false。
        /// </summary>
        public bool IsDimensEmpty
        {
            get
            {
                if (this.NX <= 0 || this.NY <= 0 || this.NZ <= 0)
                    return true;
                return false;
            }
        }

        public int[] ActNums { get; set; }


        /// <summary>
        /// 将一维数组索引转化为三维（I,J,K）表示的网格索引号
        /// </summary>
        /// <param name="index">0开始的网格索引</param>
        /// <param name="iv"></param>
        /// <param name="jv"></param>
        /// <param name="kv"></param>
        public void InvertIJK(int index, out int I, out int J, out int K)
        {
            this.gridIndexer.IJKOfIndex(index, out I, out J, out K);
        }

        /// <summary>
        /// 求出网格索引位置
        /// </summary>
        /// <param name="I">网格坐标 I方向，1起始</param>
        /// <param name="J">网格坐标 J方向，1起始</param>
        /// <param name="K">网格坐标 K方向，1起始</param>
        /// <returns></returns>
        protected int GridIndexOf(int I, int J, int K)
        {
            return gridIndexer.IndexOf(I, J, K);
        }

        protected void IJK2Index(int I, int J, int K, out int index)
        {
            index = this.gridIndexer.IndexOf(I, J, K);
            return;
        }

        /// <summary>
        /// 判断网格是否是活动网格
        /// </summary>
        /// <param name="i">下标从1开始</param>
        /// <param name="j">下标从1开始</param>
        /// <param name="k">下标从1开始</param>
        /// <returns></returns>
        public bool IsActiveBlock(int i, int j, int k)
        {

            int gridIndex;
            this.IJK2Index(i, j, k, out gridIndex);
            int actnum = this.ActNums[gridIndex];
            if (actnum <= 0) //小于或等于0的网格块都是非活动的网格块
                return false;
            return true;
        }


        /// <summary>
        /// 网格索引
        /// </summary>
        /// <param name="gridIndex"></param>
        /// <returns></returns>
        public bool IsActiveBlock(int gridIndex)
        {
            return this.ActNums[gridIndex] > 0;
        }

        /// <summary>
        /// 创建数组
        /// </summary>
        /// <param name="dimenSize">数组大小</param>
        /// <param name="value">初始值</param>
        /// <returns></returns>
        protected int[] InitIntArray(int dimenSize, int value = 1)
        {
            int[] actNums = new int[dimenSize];
            for (int i = 0; i < dimenSize; i++)
            {
                actNums[i] = value;
            }
            return actNums;
        }

        public float[] InitFloatArray(int size, float value = 2)
        {
            float[] array = new float[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = value;
            }
            return array;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {

            if (this.gridIndexer == null)
            {
                this.gridIndexer = new SimLab.SimGrid.GridIndexer(this.NX, this.NY, this.NZ);
            }

            if (this.ActNums == null)
            {
                this.ActNums = InitIntArray(this.DimenSize);
            }
            if (this.zeroVisibles == null)
            {
                this.zeroVisibles = InitIntArray(this.DimenSize, 0);
            }
            if (this.invisibleTextures == null)
            {
                //初始化不可视
                this.invisibleTextures = InitFloatArray(this.DimenSize, 2);
            }


        }

        public int[] BindCellActive(int[] a1, int[] a2)
        {
            if (a1.Length != a2.Length)
                throw new ArgumentException("array size not equal");
            int length = a1.Length;
            int[] results = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (a1[i] > 0 && a2[i] > 0)
                    results[i] = 1;
                else
                    results[i] = 0;
            }
            return results;
        }


        /// <summary>
        /// 将网格索引转化为可视结果
        /// </summary>
        /// <param name="gridIndexes"></param>
        /// <returns></returns>
        public int[] ExpandVisibles(int[] gridIndexes)
        {
            int[] gridVisibles = new int[this.DimenSize];
            Array.Copy(this.zeroVisibles, gridVisibles, this.DimenSize);
            for (int i = 0; i < gridIndexes.Length; i++)
            {
                gridVisibles[gridIndexes[i]] = 1;
            }
            return gridVisibles;
        }


        /// <summary>
        /// 快速生成默认的网格Texture,值为空(值大于1）
        /// </summary>
        /// <returns></returns>
        public float[] GetInvisibleTextureCoords()
        {
            float[] none = new float[this.DimenSize];
            Array.Copy(this.invisibleTextures, none, this.DimenSize);
            return none;
        }

        #region
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


        #endregion

        bool isSet = false;

        public override HexahedronGrid GenerateGrid(OpenGL gl, IScientificCamera camera, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            HexahedronPositionBuffer positionBuffer = GeneratePositionBuffer(ref this.max, ref this.min);
            HexahedronTexCoordBuffer texCoordBuffer = GenerateTexCoordBuffer(gridIndexes, values, minValue, maxValue);
            HalfHexahedronIndexBuffer indexBuffer = GenerateIndexBuffer();

            HexahedronGrid grid = new HexahedronGrid(gl, camera);
            grid.
        }

        private unsafe HalfHexahedronIndexBuffer GenerateIndexBuffer()
        {
            HexahedronGridderSource src = this;
            HalfHexahedronIndexBuffer indexBuffer = new HalfHexahedronIndexBuffer();
            int dimSize = src.DimenSize;

            //网格个数*2：每个六面体需要2个半六面体索引来描述。
            //半六面体描述的是GL_QUAD_STRIP格式的3个四边形。
            int indexLength = dimSize * 2;
            indexBuffer.AllocMem(indexLength);

            HalfHexahedronIndex* array = (HalfHexahedronIndex*)indexBuffer.Data;
            for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
            {
                array[gridIndex * 2].dot0 = (uint)(8 * gridIndex + 6);
                array[gridIndex * 2].dot1 = (uint)(8 * gridIndex + 2);
                array[gridIndex * 2].dot2 = (uint)(8 * gridIndex + 7);
                array[gridIndex * 2].dot3 = (uint)(8 * gridIndex + 3);
                array[gridIndex * 2].dot4 = (uint)(8 * gridIndex + 4);
                array[gridIndex * 2].dot5 = (uint)(8 * gridIndex + 0);
                array[gridIndex * 2].dot6 = (uint)(8 * gridIndex + 5);
                array[gridIndex * 2].dot7 = (uint)(8 * gridIndex + 1);
                array[gridIndex * 2].restartIndex = uint.MaxValue;

                array[gridIndex * 2 + 1].dot0 = (uint)(8 * gridIndex + 3);
                array[gridIndex * 2 + 1].dot1 = (uint)(8 * gridIndex + 0);
                array[gridIndex * 2 + 1].dot2 = (uint)(8 * gridIndex + 2);
                array[gridIndex * 2 + 1].dot3 = (uint)(8 * gridIndex + 1);
                array[gridIndex * 2 + 1].dot4 = (uint)(8 * gridIndex + 6);
                array[gridIndex * 2 + 1].dot5 = (uint)(8 * gridIndex + 5);
                array[gridIndex * 2 + 1].dot6 = (uint)(8 * gridIndex + 7);
                array[gridIndex * 2 + 1].dot7 = (uint)(8 * gridIndex + 4);
                array[gridIndex * 2 + 1].restartIndex = uint.MaxValue;
            }

            return indexBuffer;
        }

        private HexahedronTexCoordBuffer GenerateTexCoordBuffer(int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            HexahedronGridderSource src = this;
            int[] resultsVisibles = src.ExpandVisibles(gridIndexes);
            int[] bindVisibles = src.BindCellActive(src.BindVisibles, resultsVisibles);

            int dimenSize = src.DimenSize;
            float[] textures = src.GetInvisibleTextureCoords();
            float distance = Math.Abs(maxValue - minValue);
            for (int i = 0; i < gridIndexes.Length; i++)
            {
                int gridIndex = gridIndexes[i];
                float value = values[i];
                if (value < minValue)
                    value = minValue;
                if (value > maxValue)
                    value = maxValue;

                if (bindVisibles[gridIndex] > 0)
                {
                    if (!(distance <= 0.0f))
                    {
                        textures[gridIndex] = (value - minValue) / distance;
                    }
                    else
                    {
                        //最小值最大值相等时，显示最小值的颜色
                        textures[gridIndex] = 0.0f;
                    }
                }
            }

            HexahedronTexCoordBuffer coordBuffer = new HexahedronTexCoordBuffer();
            unsafe
            {
                int gridCellCount = src.DimenSize;
                coordBuffer.AllocMem(gridCellCount);
                HexahedronTexCoord* coords = (HexahedronTexCoord*)coordBuffer.Data;
                for (int gridIndex = 0; gridIndex < dimenSize; gridIndex++)
                {
                    coords[gridIndex].SetCoord(textures[gridIndex]);
                }
            }

            return coordBuffer;
        }

        private unsafe HexahedronPositionBuffer GeneratePositionBuffer(ref Vertex max, ref Vertex min)
        {
            HexahedronGridderSource src = this;
            HexahedronPositionBuffer positionBuffer = new HexahedronPositionBuffer();
            int dimSize = src.DimenSize;
            int I, J, K;

            positionBuffer.AllocMem(dimSize);

            HexahedronPosition* cell = (HexahedronPosition*)positionBuffer.Data;
            for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
            {
                src.InvertIJK(gridIndex, out I, out J, out K);
                cell[gridIndex].FLT = src.PointFLT(I, J, K);
                cell[gridIndex].FRT = src.PointFRT(I, J, K);
                cell[gridIndex].BRT = src.PointBRT(I, J, K);
                cell[gridIndex].BLT = src.PointBLT(I, J, K);
                cell[gridIndex].FLB = src.PointFLB(I, J, K);
                cell[gridIndex].FRB = src.PointFRB(I, J, K);
                cell[gridIndex].BRB = src.PointBRB(I, J, K);
                cell[gridIndex].BLB = src.PointBLB(I, J, K);

                if (!isSet && src.IsActiveBlock(gridIndex))
                {
                    min = cell[gridIndex].FLT;
                    max = min;
                    isSet = true;
                }

                if (isSet && src.IsActiveBlock(gridIndex))
                {
                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].FLT);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].FLT);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].FRT);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].FRT);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].BRT);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].BRT);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].BLT);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].BLT);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].FLB);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].FLB);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].FRB);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].FRB);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].BRB);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].BRB);

                    min = SimLab.SimGrid.helper.VertexHelper.MinVertex(min, cell[gridIndex].BLB);
                    max = SimLab.SimGrid.helper.VertexHelper.MaxVertex(max, cell[gridIndex].BLB);
                }
            }

            return positionBuffer;
        }

    }
}
