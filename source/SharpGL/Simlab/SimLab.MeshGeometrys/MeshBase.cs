using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.MeshGeometrys
{
    public abstract class MeshBase
    {
         /// <summary>
        /// 点的位置
        /// </summary>
        private PositionsBufferData positions;

        public MeshBase(PositionsBufferData positions)
        {
             this.positions = positions;
        }


        public PositionsBufferData Positions
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
