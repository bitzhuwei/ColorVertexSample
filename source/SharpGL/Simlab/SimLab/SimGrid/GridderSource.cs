using SharpGL.SceneGraph;
using SimLab.GridSource.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSource
{
    /// <summary>
    /// 网格数据源
    /// </summary>
    public abstract class GridderSource
    {
        private  GridBufferDataFactory factory;
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
        /// 每个网格一个值，全部为零
        /// </summary>
        private int[] zeroVisibles;


        /// <summary>
        /// 透明texure坐标,大小为DimenSize
        /// </summary>
        private float[] textures;

       

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
        public void InvertIJK(int index, out int iv, out int jv, out int kv)
        {
            int ijsize = this.NX * this.NY;
            kv = index / ijsize + 1;
            int ijLeft = index % ijsize;
            jv = ijLeft / this.NX + 1;
            iv = ijLeft % this.NX + 1;
        }

        protected void IJK2Index(int I, int J, int K, out int index)
        {
            index = (K - 1) * (this.NX * this.NY) + (J - 1) * this.NX + (I - 1);
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
            if (actnum <= 0) //小于或等于0的网格块都是活动的网格块
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


        protected abstract GridBufferDataFactory CreateFactory();
      

        protected  GridBufferDataFactory Factory
        {
            get
            {
                if (this.factory == null)
                {
                    this.factory = CreateFactory();
                }
                return factory;
            }
        }


        /// <summary>
        /// 生成默认活动的网格
        /// </summary>
        /// <param name="dimenSize"></param>
        /// <returns></returns>
        private int[] InitIntArray(int dimenSize, int value = 1)
        {
            int[] actNums = new int[this.DimenSize];
            int dimsize = this.DimenSize;
            for (int i = 0; i < dimsize; i++)
            {
                actNums[i] = value;
            }
            return actNums;
        }

        private float[] InitFloatArray(int size, float value=2)
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
            if (this.ActNums == null)
            {
                this.ActNums = InitIntArray(this.DimenSize);
            }
            if (this.zeroVisibles == null)
            {
                this.zeroVisibles = InitIntArray(this.DimenSize,0);
            }
            if (this.textures == null)
            {
                this.textures = InitFloatArray(this.DimenSize, 2);
            }
        }





        public abstract TextureCoordinatesBufferData CreateTextureCoordinates(int[] gridIndexes, float[] values, float minValue, float maxValue);

        public abstract MeshGeometry3D CreateMesh();

        public abstract WireFrameBufferData CreateWireframe();


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
        public float[] GetDefaultTextureCoords()
        {
            float[] none = new float [this.DimenSize];
            Array.Copy(this.textures, none, this.DimenSize);
            return none;
        }

        public Vertex Min
        {
            get;
            protected set;
        }


        public Vertex Max
        {
            get;
            protected set;
        }









    }



}
