using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabBridge
{
    public class GeometryMath
    {

        /// <summary>
        /// 求三维空间的A点到b点的距离
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Distance(Vertex a, Vertex b)
        {
            Vertex c = b - a;
            return (float)c.Magnitude();
        }

        /// <summary>
        /// 三维空间,b和a z方向垂线所在的平面的距离
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float XYPlanDistance(Vertex a, Vertex b)
        {
            Vertex a1 = a;
            a1.Z = b.Z;
            return Distance(a1, b);
        }


        /// <summary>
        /// 三维空间
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float XZPlanDistance(Vertex b, Vertex a)
        {
            Vertex a1 = a;
            a1.Z = b.Z;
            return Distance(a1, a);
        }


        /// <summary>
        /// 二维空间的
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="pointCount"></param>
        /// <returns></returns>
        public static PointF[] Arc2D(PointF p1, PointF p2, int segCount =5)
        {
            List<PointF> points = new List<PointF>();

            float dx = System.Math.Abs(p2.X - p1.X);
            float dy = System.Math.Abs(p2.Y - p1.Y);
            float r = System.Math.Min(dx, dy); //内切坐标轴的半径

            //内切圆的圆心
            float xc = r;
            float yc = r;

            float xstep = r / (5 * 1.0f);
            float x = 0.0f;
            for (int i = 0; i <= segCount; i++)
            {
                float xi = x + (i * xstep);
                float v = r*r-(xi-xc)*(xi-xc);
                if (v < 0.0f)
                    break;
                //取递减的值
                float yi = (yc-(float)System.Math.Sqrt(v));
                PointF p = new PointF(xi, yi);
                points.Add(p);
            }
            return points.ToArray<PointF>();
        }


        public static List<Vertex> Arc3D(Vertex a, Vertex b, int segments = 5)
        {
            List<Vertex> arcPoints = new List<Vertex>();

            Vertex a1 = a;
            a1.Z = b.Z;
            Vertex Vxya1b = b - a1; //a1到b的向量
            float xyDistance = (float)Vxya1b.Magnitude();
            Vxya1b.Normalize();

            Vertex Vxzaa1 = a1 - a;
            float xzDistance = (float)Vxzaa1.Magnitude();
            Vxzaa1.Normalize();

            float ArcR = System.Math.Min(xyDistance, xzDistance);

            PointF Pa2 = new PointF(0, xzDistance);
            PointF Pb2 = new PointF(xyDistance, 0);

            //二维a,to b的圆角坐标
            PointF[] points = Arc2D(Pa2, Pb2, segments);

            for (int i = 0; i < points.Length; i++)
            {
                //将二维空间的点转化为三维空间的点
                Vertex xy = a1 + Vxya1b * points[i].X;
                Vertex z =  a  + Vxzaa1 * (ArcR-points[i].Y);
                Vertex xyz = new Vertex();
                xyz.X = xy.X;
                xyz.Y = xy.Y;
                xyz.Z = z.Z;
                arcPoints.Add(xyz);
            }
            return arcPoints;
        }

      
    }
}
