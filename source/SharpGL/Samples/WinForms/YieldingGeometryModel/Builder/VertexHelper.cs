using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.Builder
{
    public class VertexHelper
    {

        public static void MinMax(Vertex value, ref Vertex min, ref Vertex max)
        {

            if (value.X < min.X)
                min.X = value.X;
            if (value.Y < min.Y)
                min.Y = value.Y;
            if (value.Z < min.Z)
                min.Z = value.Z;

            if (value.X > max.X)
                max.X = value.X;
            if (value.Y > max.Y)
                max.Y = value.Y;
            if (value.Z > max.Z)
                max.Z = value.Z;
        }

        /// <summary>
        /// 计算半径,X,Y平面距离来确定半径
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static float EvalRadius(Vertex min, Vertex max, float ratio)
        {
            float dx  = Math.Abs(max.X - min.X);
            float dy  =  Math.Abs(max.Y - min.Y);
            return Math.Max(dx, dy) * ratio;

        }
    }
}
