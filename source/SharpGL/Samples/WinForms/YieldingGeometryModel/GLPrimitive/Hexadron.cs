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
        /// 依次获取此六面体的8个顶点。
        /// Get vertexs of this hexahedron successively.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vertex> GetVertexes()
        {
            /// <summary>
            /// front left top
            /// </summary>
            yield return flt;

            /// <summary>
            /// front right top
            /// </summary>
            yield return frt;

            /// <summary>
            /// front left bottom
            /// </summary>
            yield return flb;

            /// <summary>
            /// front right bottom
            /// </summary>
            yield return frb;

            /// <summary>
            /// back left top
            /// </summary>
            yield return blt;

            /// <summary>
            /// back right top
            /// </summary>
            yield return brt;

            /// <summary>
            /// back left bottom
            /// </summary>
            yield return blb;


            /// <summary>
            /// back right bottom
            /// </summary>
            yield return brb;
        }

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
