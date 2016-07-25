using SharpGL.SceneGraph;
using SimLab.GridSource;
using SimLab.SimGrid.Factory;
using SimLab.VertexBuffers;
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
        public const int MATRIX_FORMAT6_TRIANGULAR_PRISM = 6;
        /// <summary>
        /// 组成裂缝的形状
        /// </summary>
        public const int FRACTURE_FORMAT2_LINE = 2;
        public const int FRACTURE_FORMAT3_TRIANGLE = 3;
        public const int FRACTURE_FORMAT4_QUAD = 4;


        /// <summary>
        /// 当基质为三菱柱时有效，表示三菱柱基质有多少层，0为无效值
        /// </summary>
        private int matrixLayers = 0;


        private List<int> visibleLayers = new List<int>();
        private Dictionary<int, bool> visibleLayersDict = new Dictionary<int, bool>();

        /// <summary>
        /// 文件头定义: 点的个数
        /// </summary>
        public int NodeNum { get; internal set; }

        public Vertex[] Nodes { get; set; }

        /// <summary>
        /// 基质的个数， 如果nodeInElem 为NODE_FORMAT3 时，element部分表示三角形。
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

        /// <summary>
        /// 母体不可见
        /// </summary>
        public int[] MatrixInvisibles { get; internal set; }

        /// <summary>
        /// 断层不可见
        /// </summary>
        public int[] FracturesInvisible { get; internal set; }


        /// <summary>
        /// 不可见母体纹理,初始值值全部为2
        /// </summary>
        public float[] InvisibleMatrixTextures { get; internal set; }


        /// <summary>
        /// 不可见裂缝纹理,值全部为2
        /// </summary>
        public float[] InvisibleFractureTextures { get; internal set; }


        public int[] ActiveMatrix { get; internal set; }
        public int[] ActiveFractures { get; internal set; }

        public int[] LayerVisibleMatrix { get; internal set; }

        /// <summary>
        /// active matrix  and layer visible matrix binding
        /// </summary>
        public int[] VisibleMatrix { get; internal set; }


        private int[] InitActiveMatrix()
        {
            int matrixSize = this.DimenSize - this.FractureNum;
            int fractureSize = this.FractureNum;
            int[] activematrix = new int[matrixSize];
            Array.Copy(this.ActNums, fractureSize, activematrix, 0, matrixSize);
            return activematrix;
        }

        private int[] InitActiveFractures()
        {
            int fractureSize = this.FractureNum;
            int[] activeFractures = new int[fractureSize];
            Array.Copy(this.ActNums, activeFractures, fractureSize);
            return activeFractures;
        }

        private void InitMatrixFracturesInvisibles()
        {
            int matrixSize = this.DimenSize - this.FractureNum;
            this.MatrixInvisibles = this.InitIntArray(matrixSize, 0);
            int fractureSize = this.FractureNum;
            this.FracturesInvisible = this.InitIntArray(fractureSize, 0);
            this.InvisibleMatrixTextures = this.InitFloatArray(matrixSize, 2.0f);
            this.InvisibleFractureTextures = this.InitFloatArray(fractureSize, 2.0f);
        }

        /// <summary>
        /// 将结果整理成直接访问索引转化为可见
        /// </summary>
        /// <param name="gridIndexes">结果集合</param>
        /// <returns></returns>
        public int[] ExpandMatrixVisibles(int[] gridIndexes)
        {
            int matrixSize = this.ElementNum;
            int dimenSize = this.DimenSize;
            int matrixStartIndex = this.FractureNum;
            int[] results = new int[matrixSize];
            Array.Copy(this.MatrixInvisibles, results, results.Length);
            for (int mixedIndex = 0; mixedIndex < gridIndexes.Length; mixedIndex++)
            {
                int gridIndex = gridIndexes[mixedIndex];
                int[] mappedBlockIndexes = this.MapBlockIndexes(gridIndex);

                for (int jblockIndex = 0; jblockIndex < mappedBlockIndexes.Length; jblockIndex++)
                {
                    int jblock = mappedBlockIndexes[jblockIndex];
                    if (jblock >= matrixStartIndex && jblock < dimenSize)
                    {
                        results[jblock - matrixStartIndex] = 1;
                    }
                }
            }
            return results;
        }

        public int[] ExpandFractureVisibles(int[] gridIndexes)
        {
            int fractureNum = this.FractureNum;
            int[] results = new int[fractureNum];
            Array.Copy(FracturesInvisible, results, fractureNum);

            for (int mixedIndex = 0; mixedIndex < gridIndexes.Length; mixedIndex++)
            {
                int gridIndex = gridIndexes[mixedIndex];
                int[] mappedBlockIndexes = this.MapBlockIndexes(gridIndex);
                for (int jblockIndex = 0; jblockIndex < mappedBlockIndexes.Length; jblockIndex++)
                {
                    int mapBlock =mappedBlockIndexes[jblockIndex];
                    if (mapBlock >= 0 && mapBlock < fractureNum)
                    {
                        results[mapBlock] = 1;
                    }
                }
            }
            return results;
        }


        /// <summary>
        /// 控制可显示的基质的层
        /// </summary>
        public int MatrixLayers
        {

            get
            {
                return this.matrixLayers;
            }
            set
            {
                if (value == 0)
                {
                    this.matrixLayers = 0;
                    this.VisibleLayers = null;
                    return;
                }
                int modValue = this.ElementNum % value;
                if (modValue != 0)
                    throw new ArgumentException("invalid matrix layers");
                if (value > 300)
                    throw new ArgumentException("invalid matrix layers to big");
                this.matrixLayers = value;
                this.VisibleLayers = this.CreateDefaultLayerVisibles(value);
            }
        }

        private List<int> CreateDefaultLayerVisibles(int mxLayers)
        {

            List<int> list = new List<int>();
            for (int i = 1; i <= mxLayers; i++)
            {
                list.Add(i);
            }
            return list;
        }

        private bool IsLayerVisible(int layerNumber)
        {
            bool value=false;
            if (this.visibleLayersDict.TryGetValue(layerNumber, out value))
                return true;
            else
                return value;
        }

        /// <summary>
        /// start from
        /// </summary>
        /// <param name="matrixIndex"></param>
        /// <returns></returns>
        private int GetLayerNumber(int matrixIndex)
        {
            if (this.matrixLayers <= 0)
                throw new ArgumentException("layers not defined");
            int layerCount = this.ElementNum / this.matrixLayers;
            int layerNumber = (matrixIndex / layerCount)+1;
            return layerNumber;
        }

        private int[] InitLayerMatrixVisibles()
        {

            int[] mxLayerVisibles = this.InitIntArray(this.ElementNum, 1);
            if (matrixLayers == 0)
              return mxLayerVisibles;

            for (int i = 0; i < mxLayerVisibles.Length; i++)
            {
                int matrixIndex = i;
                int layerNumber = this.GetLayerNumber(matrixIndex);
                bool isVisible = this.IsLayerVisible(layerNumber);
                mxLayerVisibles[i] = isVisible ? 1 : 0;
            }
            return mxLayerVisibles;
        }

        /// <summary>
        /// active nums and visible layer matrix bind
        /// </summary>
        public void BindVisibleMatrix()
        {

            this.LayerVisibleMatrix = this.InitLayerMatrixVisibles();
            this.VisibleMatrix = this.BindVisibles(this.ActiveMatrix, this.LayerVisibleMatrix);

        }

        protected Dictionary<int, bool> CreateVisibleLayersDictionary(List<int> vLayers)
        {
            if (vLayers == null)
                return null;
            Dictionary<int, bool> dict = new Dictionary<int, bool>();
            for (int i = 0; i < vLayers.Count; i++)
            {
                dict.Add(vLayers[i], true);
            }
            return dict;

        }

        public List<int> VisibleLayers
        {
            get
            {
                return this.visibleLayers;
            }
            set
            {
                this.visibleLayers = value;
                this.visibleLayersDict = CreateVisibleLayersDictionary(value);
            }
        }


        public int[] BindTextureVisibleMatrix(int[] gridIndexes)
        {
            int[] results = this.ExpandMatrixVisibles(gridIndexes);
            return this.BindVisibles(results, this.VisibleMatrix);
        }

        public int[] BindResultsAndActiveFractures(int[] gridIndexes)
        {
            int[] results = this.ExpandFractureVisibles(gridIndexes);
            return this.BindVisibles(results, this.ActiveFractures);
        }


        public override void Init()
        {
            base.Init();
            this.InitMatrixFracturesInvisibles();
            if (this.ActiveMatrix == null)
            {
                this.ActiveMatrix = InitActiveMatrix();
            }
            this.BindVisibleMatrix();

            if (this.ActiveFractures == null)
            {
                this.ActiveFractures = this.InitActiveFractures();
            }
        }

        public new Vertex Min
        {
            get
            {
                return base.Min;
            }
            set
            {
                base.Min = value;
            }
        }


        protected override void InitGridCoordinates()
        {
            //do nothing
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

        protected override SharpGL.SceneComponent.Rectangle3D InitSourceActiveBounds()
        {
            if (this.NodeNum <= 0)
                throw new ArgumentException("No nodes found");
            Vertex[] nodes = this.Nodes;
            SharpGL.SceneComponent.Rectangle3D rect = new SharpGL.SceneComponent.Rectangle3D(nodes[0], nodes[0]);
            for (int i = 0; i < nodes.Length; i++)
            {
                rect.Union(nodes[i]);
            }
            return rect;
        }


        protected override GridSource.Factory.GridBufferDataFactory CreateFactory()
        {
            return new DynamicUnstructureGridFactory();
        }

        public TexCoordBuffer CreateFractureTextureCoordinates(int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            return ((DynamicUnstructureGridFactory)this.Factory).CreateFractureTextureCoordinates(this, gridIndexes, values, minValue, maxValue);
        }
    }
}
