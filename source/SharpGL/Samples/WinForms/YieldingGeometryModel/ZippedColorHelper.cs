using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用一个uint表示256色的RGBA颜色。
    /// </summary>
    public static class ZippedColorHelper
    {
        /// <summary>
        /// 把uint值转换为0~255的4个颜色分量。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public static void ParseColor(this uint value, out byte a, out byte b, out byte g, out byte r)
        {
            a = (byte)(value >> 24);
            b = (byte)((value >> 16) & 0xFF);
            g = (byte)((value >> 8) & 0xFF);
            r = (byte)((value) & 0xFF);
        }

        /// <summary>
        /// 根据0~255的颜色分量给出对应的uint。（压缩的颜色表示法）
        /// </summary>
        /// <param name="r">0~255</param>
        /// <param name="g">0~255</param>
        /// <param name="b">0~255</param>
        /// <param name="a">0~255</param>
        /// <returns></returns>
        public static uint GetZippedColor(byte a, byte b, byte g, byte r)
        {
            //if (r < 0) { r = 0; }
            //else if (r > 255) { r = 255; }

            //if (g < 0) { g = 0; }
            //else if (g > 255) { g = 255; }

            //if (b < 0) { b = 0; }
            //else if (b > 255) { b = 255; }

            //if (a < 0) { a = 0; }
            //else if (a > 255) { a = 255; }

            uint result =
                (((uint)a) << 24)
                + (((uint)b) << 16)
                + (((uint)g) << 8)
                + (((uint)r));

            return result;
        }

        static void TestZippedColorHelper()
        {
            Random random = new Random();
            byte r, g, b, a;
            r = (byte)random.Next(0, 256);
            g = (byte)random.Next(0, 256);
            b = (byte)random.Next(0, 256);
            a = (byte)random.Next(0, 256);
            uint zippedColor = ZippedColorHelper.GetZippedColor(a, b, g, r);

            byte r2, g2, b2, a2;
            zippedColor.ParseColor(out a2, out b2, out g2, out r2);

            if (r != r2)
            { throw new Exception(); }

            if (g != g2)
            { throw new Exception(); }

            if (b != b2)
            { throw new Exception(); }

            if (a != a2)
            { throw new Exception(); }
        }
    }
}
