using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public partial class HexahedronGrid
    {


        private void ResolveLists(OpenGL gl)
        {
            ShaderProgram shaderProgram = this.resolveListsShaderProgram;
            shaderProgram.Bind(gl);
            {
                gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
                gl.PrimitiveRestartIndex(uint.MaxValue);

                gl.BindVertexArray(this.resolveListsVAO[0]);
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
                gl.DrawElements(OpenGL.GL_QUAD_STRIP, this.indexBufferLength, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                gl.BindVertexArray(0);

                gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);
            }
            shaderProgram.Unbind(gl);
        }

    }
}
