using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.DataSource;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public partial class UnStructuredGridderElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="colors"></param>
        public void UpdateColorBuffer(OpenGL gl, UnmanagedArray<vec4> colors)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorsBufferObject[0]);
            IntPtr destColors = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            SharpGL.SceneComponent.Utility.MemoryHelper.CopyMemory(
                destColors, colors.Header, (uint)colors.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

        }

    }
}
