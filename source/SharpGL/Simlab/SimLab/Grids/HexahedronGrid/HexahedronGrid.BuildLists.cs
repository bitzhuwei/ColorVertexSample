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


        private void BuildLists(OpenGL gl)
        {
            {
                ShaderProgram shaderProgram = this.buildListsShaderProgram;
                shaderProgram.Bind(gl);
            }

            if (this.RenderGridWireframe && this.buildListsVAO != null)
            {
                //if (wireframeIndexBuffer != null)
                if (positionBuffer != null && colorBuffer != null && indexBuffer != null)
                {
                    buildListsShaderProgram.SetUniform1(gl, "renderingWireframe", 1.0f);// shader一律上白色。

                    gl.Disable(OpenGL.GL_LINE_STIPPLE);
                    gl.Disable(OpenGL.GL_POLYGON_STIPPLE);
                    gl.Enable(OpenGL.GL_LINE_SMOOTH);
                    gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
                    gl.ShadeModel(SharpGL.Enumerations.ShadeModel.Smooth);
                    gl.Hint(SharpGL.Enumerations.HintTarget.LineSmooth, SharpGL.Enumerations.HintMode.Nicest);
                    gl.Hint(SharpGL.Enumerations.HintTarget.PolygonSmooth, SharpGL.Enumerations.HintMode.Nicest);
                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Lines);

                    gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
                    gl.PrimitiveRestartIndex(uint.MaxValue);

                    gl.BindVertexArray(this.buildListsVAO[0]);
                    gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
                    gl.DrawElements(OpenGL.GL_QUAD_STRIP, this.indexBufferLength, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                    gl.BindVertexArray(0);

                    gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Filled);
                    gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

                }
            }

            if (this.RenderGrid && this.buildListsVAO != null)
            {
                if (positionBuffer != null && colorBuffer != null && indexBuffer != null)
                {
                    buildListsShaderProgram.SetUniform1(gl, "renderingWireframe", 0.0f);// shader根据uv buffer来上色。

                    gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
                    gl.PrimitiveRestartIndex(uint.MaxValue);

                    gl.BindVertexArray(this.buildListsVAO[0]);
                    gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
                    gl.DrawElements(OpenGL.GL_QUAD_STRIP, this.indexBufferLength, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                    gl.BindVertexArray(0);

                    gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);
                }
            }
        }

    }
}
