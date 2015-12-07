using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.MeshGeometrys
{

    /// <summary>
    /// map to opengl buffer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HexahedronPositions
    {
        /// <summary>
        ///  front left top p0
        /// </summary>
        public Vertex FLT;

        /// <summary>
        /// front right top p1
        /// </summary>
        public Vertex FRT;

        /// <summary>
        /// back right top p2
        /// </summary>
        public Vertex BRT;

        /// <summary>
        /// back left top p4
        /// </summary>
        public Vertex BLT;

        /// <summary>
        /// 
        /// </summary>
        public Vertex FLB;
        public Vertex FRB;
        public Vertex BRB;
        public Vertex BLB;

    }
}
