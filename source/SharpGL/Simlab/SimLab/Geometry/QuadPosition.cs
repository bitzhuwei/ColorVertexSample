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
    /// 四边形描述信息,p1,p2，p3,p4，理解为在一个平面上。四个边组成为
    /// (p1,p2) (p3,p4) (p1,p3) (p2,p4) 四边组成一个平面
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QuadPosition
    {
        public Vertex P1;
        public Vertex P2;
        public Vertex P3;
        public Vertex P4;
    }
}
