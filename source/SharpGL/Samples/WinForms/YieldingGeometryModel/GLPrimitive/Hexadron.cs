using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.GLPrimitive
{
    /// <summary>
    /// Hexadron which composed the CatesianGridder or CornerPointGridder
    /// </summary>
    public struct Hexahedron
    {
        /// <summary>
        /// front left top
        /// </summary>
        public Vertex flt;

        /// <summary>
        /// front right top
        /// </summary>
        public Vertex frt;

        /// <summary>
        /// front left bottom
        /// </summary>
        public Vertex flb;

        /// <summary>
        /// front right bottom
        /// </summary>
        public Vertex frb;

        /// <summary>
        /// back left top
        /// </summary>
        public Vertex  blt;

        /// <summary>
        /// back right top
        /// </summary>
        public Vertex  brt;

        /// <summary>
        /// back left bottom
        /// </summary>
        public Vertex  blb;


        /// <summary>
        /// back right bottom
        /// </summary>
        public Vertex  brb;

        /// <summary>
        /// color of the hexadron
        /// </summary>
        public GLColor color;

        ///// <summary>
        ///// gridder index, see GridderSource.IJK2Index
        ///// </summary>
        //public int gridIndex;
    }
}
