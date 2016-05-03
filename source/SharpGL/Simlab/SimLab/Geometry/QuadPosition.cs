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
    /// 四边形描述信息,p1,p2，p3,p4; p1,p2,p3,p4 按照顺序连接就是一个四边形
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
