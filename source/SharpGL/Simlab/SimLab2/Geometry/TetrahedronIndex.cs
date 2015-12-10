using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.SimGrid.Geometry
{
    /// <summary>
    /// 用OpenGL.GL_TRIANGLES来渲染四面体。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TetrahedronIndex
    {
        public uint dot0;
        public uint dot1;
        public uint dot2;
        public uint dot3;
        public uint dot4;
        public uint dot5;
        public uint restartIndex;
    }
}
