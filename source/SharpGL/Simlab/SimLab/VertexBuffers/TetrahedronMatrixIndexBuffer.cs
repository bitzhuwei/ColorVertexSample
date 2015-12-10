using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.VertexBuffers
{
    /// <summary>
    /// 基质(Matrix)为四面体时用此索引。
    /// </summary>
    public class TetrahedronMatrixIndexBuffer : IndexBuffer
    {
        public TetrahedronMatrixIndexBuffer()
            : base(OpenGL.GL_TRIANGLE_STRIP)
        {

        }

        /// <summary>
        /// 申请指定长度的非托管数组。
        /// </summary>
        /// <param name="elementCount">数组元素的数目。</param>
        protected override UnmanagedArrayBase CreateElements(int elementCount)
        {
             return new UnmanagedArray<TetrahedronIndex>(elementCount);
        }
    }
}
