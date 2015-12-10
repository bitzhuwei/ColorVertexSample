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
    /// 描述一个三角形的位置信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TrianglePositions
    {
        public Vertex P1;
        public Vertex P2;
        public Vertex P3;


    }
}
