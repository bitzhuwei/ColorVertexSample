using SharpGL;
using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 描述一个顶点的半径。
    /// <para>一个顶点的半径信息由'1'个'float'描述。</para>
    /// </summary>
    public class PointRadiusBuffer : PropertyBuffer
    {
        public PointRadiusBuffer()
            : base(1, OpenGL.GL_FLOAT)//一个顶点的半径信息由'1'个'float'描述。
        {
        }

        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
            return new UnmanagedArray<float>(elementCount);
        }
    }
}
