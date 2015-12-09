using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    /// <summary>
    /// 描述三维世界的位置信息。
    /// <para>一个顶点的位置信息由'3'个'float'描述。</para>
    /// </summary>
    public abstract class PositionBuffer : PropertyBuffer
    {
        /// <summary>
        /// 描述三维世界的位置信息。
        /// <para>一个顶点的位置信息由'3'个'float'描述。</para>
        /// </summary>
        public PositionBuffer()
            : base(3, OpenGL.GL_FLOAT)//一个顶点的位置信息由'3'个'float'描述。
        {
        }


        private unsafe void DoDump()
        {
            if (this.GLDataType == OpenGL.GL_FLOAT)
            {
                Vertex* positions = (Vertex*)this.Data;
                int dimenSize = this.SizeInBytes / (sizeof(float) * this.GLSize);
                Console.WriteLine(String.Format("Positions:{0}, Position Components:{1}", dimenSize, this.GLSize));
                Console.WriteLine("=============Positions Start==================");
                for (int i = 0; i < dimenSize; i++)
                {
                    System.Console.WriteLine(String.Format("{0}: ({1},{2},{3})", i, positions[i].X, positions[i].Y, positions[i].Z));
                }
                Console.WriteLine("=============Positions End ==================");
            }
        }

        public void Dump()
        {
            this.DoDump();
        }
    }
}
