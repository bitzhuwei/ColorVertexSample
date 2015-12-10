using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.SimGrid.Geometry
{

    /// <summary>
    ///  描述一个四面体的位置信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TetrahedronPositions
    {
        public Vertex p1;
        public Vertex p2;
        public Vertex p3;
        public Vertex p4;
    }
}
