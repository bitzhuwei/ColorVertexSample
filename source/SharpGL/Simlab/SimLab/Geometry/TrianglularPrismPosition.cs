using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.Geometry
{
    /// <summary>
    /// 三棱行的描述
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TriangularPrismPosition
    {
        /**
         * 顶三角形的坐标按顺序组成一个三角形,(p1,p2),(p4,p5)构成一个四边形，（
         * */
        public Vertex P1;
        public Vertex P2;
        public Vertex P3;

        /**
         * 底三角形的坐标按顺序组成一个三角形，
         * */
        public Vertex P4;
        public Vertex P5;
        public Vertex P6;

    }
}
