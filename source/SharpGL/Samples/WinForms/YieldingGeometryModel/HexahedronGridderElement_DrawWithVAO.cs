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
            var shader = (renderMode == RenderMode.HitTest) ? pickingShaderProgram : shaderProgram;

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            // TODO: draw hexahedron
            //ScientificModel model = this.Model;
            //if (model.First != null && model.Count != null && model.PrimitiveCount > 0)
            //{ gl.MultiDrawArrays((uint)model.Mode, model.First, model.Count, model.PrimitiveCount); }
            //else
            //{ gl.DrawArrays((uint)this.Model.Mode, 0, this.Model.VertexCount); }

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shader.Unbind(gl);
        }

    }
}
