using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于记录非托管的数组。
    /// </summary>
    public class IntPtrWrapper
    {
        /// <summary>
        /// 数组指针。
        /// </summary>
        public IntPtr pointer;

        /// <summary>
        /// 数组元素数 * 元素长度。
        /// </summary>
        public int length;
    }
}
