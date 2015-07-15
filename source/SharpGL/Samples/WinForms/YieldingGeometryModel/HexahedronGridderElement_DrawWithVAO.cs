using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public partial class HexahedronGridderElement : SceneElement, IRenderable
    {

        /// <summary>
        /// 用VAO渲染此元素。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        private void DrawWithVAO(OpenGL gl, RenderMode renderMode)
        {
            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            indexDataBuffer.Bind(gl);
            gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
            gl.PrimitiveRestartIndex(uint.MaxValue);// 截断三角形带的索引值。
            gl.DrawElements(OpenGL.GL_TRIANGLE_STRIP, this.indexArrayElementCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);
        }

    }
}
