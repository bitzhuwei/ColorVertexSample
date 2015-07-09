using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryModel
{

    /// <summary>
    /// 网格数据源
    /// </summary>
    public abstract class GridderSource
    {
        private int _nx = 0;
        private int _ny = 0;
        private int _nz = 0;

        private int[] _actNums = null;

        public int NX
        {
            get { return this._nx; }
            set { this._nx = value; }
        }

        public int NY
        {
            get { return this._ny; }
            set { this._ny = value; }
        }

        public int NZ
        {
            get { return this._nz; }
            set { this._nz = value; }
        }

        public int DimenSize
        {
            get
            {
                return this.NX * this.NY * this.NZ;
            }
        }

        public bool IsDimensEmpty
        {
            get
            {
                if (this._nx <= 0 || this._ny <= 0 || this._nz <= 0)
                    return true;
                return false;
            }
        }

        public int[] ActNums
        {
            get
            {
                return this._actNums;
            }
            set
            {
                this._actNums = value;
            }
        }


        /// <summary>
        /// 将一维数组索引转化为三维（I,J,K）表示的网格索引号
        /// </summary>
        /// <param name="index">0开始的网格索引</param>
        /// <param name="iv"></param>
        /// <param name="jv"></param>
        /// <param name="kv"></param>
        public void InvertIJK(int index, out int iv, out int jv, out int kv)
        {
            int ijsize = this._nx * this._ny;
            kv = index / ijsize + 1;
            int ijLeft = index % ijsize;
            jv = ijLeft / this._nx + 1;
            iv = ijLeft % this._nx + 1;
        }

        public void IJK2Index(int I, int J, int K, out int index)
        {
            index = (K - 1) * (this._nx * this._ny) + (J - 1) * this._nx + (I - 1);
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
            //超出网格定义的边界，总是none active 的网格块
            if (i <= 0 || i > this._nx || j <= 0 || j > this._ny || k <= 0 || k > this._nz)
                return false;

            //如果为定义actNums，符合范围的都是Active的网格块
            if (this._actNums == null || this._actNums.Length <= 0)
                return true;

            int index;
            this.IJK2Index(i, j, k, out index);
            if (index < 0 || index >= this._actNums.Length)
                return false;

            int actnum = this._actNums[index];
            if (actnum <= 0) //小于或等于0的网格块都是活动的网格块
                return false;

            return true;
        }
    }
    
}
