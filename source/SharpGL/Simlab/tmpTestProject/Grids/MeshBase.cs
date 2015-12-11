using SharpGL.SceneGraph;
using SimLab.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    internal abstract class MeshBase
    {
        /// <summary>
        /// 最小边界坐标
        /// </summary>
        internal Vertex Min
        {
            get;
            set;
        }

        /// <summary>
        /// 最大边界坐标
        /// </summary>
        internal Vertex Max
        {
            get;
            set;
        }
    }
}
