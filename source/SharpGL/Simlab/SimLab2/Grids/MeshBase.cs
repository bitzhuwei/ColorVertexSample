using SharpGL.SceneGraph;
using SimLab2.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public abstract class MeshBase
    {
         /// <summary>
        /// 点的位置
        /// </summary>
        private PositionBuffer positions;

        public MeshBase(PositionBuffer positions)
        {
             this.positions = positions;
        }


        public PositionBuffer Positions
        {
            get { return this.positions; }
        }

        /// <summary>
        /// 最小边界坐标
        /// </summary>
        public Vertex Min
        {
            get;
            set;
        }

        /// <summary>
        /// 最大边界坐标
        /// </summary>
        public Vertex Max
        {
            get;
            set;
        }
    }
}
