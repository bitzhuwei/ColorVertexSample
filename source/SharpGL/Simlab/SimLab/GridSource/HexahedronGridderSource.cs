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
    /// 块为六面体组成的模拟网格几何信息,支持切片分析
    /// </summary>
    public abstract class HexahedronGridderSource : GridderSource
    {

        private IList<int> _iBlocks;
        private IList<int> _jBlocks;
        private IList<int> _kBlocks;

        private Dictionary<int, bool> _iDict;
        private Dictionary<int, bool> _jDict;
        private Dictionary<int, bool> _kDict;



        private Dictionary<int, bool> ConvertToDict(IList<int> slices)
        {
            Dictionary<int, bool> result = new Dictionary<int, bool>();
            for (int i = 0; i < slices.Count; i++)
            {
                result.Add(slices[i], true);
            }
            return result;
        }


        public IList<int> IBlocks
        {
            get { return this._iBlocks; }
            set
            {
                this._iBlocks = value;
                this._iDict = this.ConvertToDict(this._iBlocks);
            }
        }

        public IList<int> JBlocks
        {
            get { return this._jBlocks; }
            set
            {
                this._jBlocks = value;
                this._jDict = this.ConvertToDict(this._jBlocks);
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
                this._kDict = this.ConvertToDict(this._kBlocks);
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
            bool exist = false;
            if (this._iDict.TryGetValue(i, out exist))
            {
                if (!exist)
                    return false;
            }
            else
            {
                return false;
            }

            exist = false;
            if (this._jDict.TryGetValue(j, out exist))
            {
                if (!exist)
                    return false;
            }
            else
            {
                return false;
            }

            if (this._kDict.TryGetValue(k, out exist))
            {
                if (!exist)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断IJ是否
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool IsSliceBlock(int i, int j)
        {
            if (this._iBlocks.IndexOf(i) < 0)
                return false;
            if (this._jBlocks.IndexOf(j) < 0)
                return false;
            return true;
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


        protected override Factory.BufferDataFactory CreateFactory()
        {
              return new HexahedronFactory();
        }
    }
}
