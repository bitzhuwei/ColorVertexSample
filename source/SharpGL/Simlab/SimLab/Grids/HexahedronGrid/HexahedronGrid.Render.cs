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

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (positionBuffer == null || colorBuffer == null) { return; }

            if (this.resolveListsShaderProgram == null)
            {
                this.InitMisc(gl);
                this.buildListsShaderProgram = InitBuildListsShaderProgram(gl, renderMode);
                this.resolveListsShaderProgram = InitResolveListsShaderProgram(gl, renderMode);
            }
            if (this.vertexArrayObject == null)
            {
                CreateVertexArrayObject(gl, renderMode);
            }

            BeforeRendering(gl, renderMode);

            gl.Disable(OpenGL.GL_DEPTH_TEST);
            //gl.Disable(OpenGL.GL_CULL_FACE);
            ResetMisc(gl);

            modelMatrix = mat4.identity();
            {
                ShaderProgram shaderProgram = this.buildListsShaderProgram;
                //  Bind the shader, set the matrices.
                shaderProgram.Bind(gl);
                shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
                shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
                shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
                //shaderProgram.SetUniform1(gl, "tex", this.texture.TextureName);
                //shaderProgram.SetUniform1(gl, "tex", 1);
                //gl.ActiveTexture(OpenGL.GL_TEXTURE1);
                //gl.Enable(OpenGL.GL_TEXTURE_2D);
                //this.texture.Bind(gl);
                //shaderProgram.SetUniform1(gl, "brightness", this.Brightness);
                //shaderProgram.SetUniform1(gl, "opacity", this.Opacity);
            }
            {
                ShaderProgram shaderProgram = this.resolveListsShaderProgram;
                shaderProgram.Bind(gl);
                shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
                shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
                shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            }

            BuildLists(gl);

            ResolveLists(gl);

            //gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            AfterRendering(gl, renderMode);
        }

        #endregion

    }
}
