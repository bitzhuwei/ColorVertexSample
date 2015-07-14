using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{

    /// <summary>
    /// 网格数据源
    /// </summary>
    public abstract class GridderSource
    {

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
            //超出网格定义的边界，总是none active 的网格块
            if (i <= 0 || i > this.NX || j <= 0 || j > this.NY || k <= 0 || k > this.NZ)
                return false;

            //如果为定义actNums，符合范围的都是Active的网格块
            if (this.ActNums == null || this.ActNums.Length <= 0)
                return true;

            int index;
            this.IJK2Index(i, j, k, out index);
            if (index < 0 || index >= this.ActNums.Length)
                return false;

            int actnum = this.ActNums[index];
            if (actnum <= 0) //小于或等于0的网格块都是活动的网格块
                return false;

            return true;
        }
    }

}
