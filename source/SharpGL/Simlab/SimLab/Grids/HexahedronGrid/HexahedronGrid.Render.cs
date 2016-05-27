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

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (positionBuffer == null || colorBuffer == null) { return; }

            if (this.resolveListsShaderProgram == null)
            {
                this.buildListsShaderProgram = InitBuildListsShaderProgram(gl, renderMode);
                this.resolveListsShaderProgram = InitResolveListsShaderProgram(gl, renderMode);
            }
            if (this.buildListsVAO == null)
            {
                this.buildListsShaderProgram.Bind(gl);
                CreateBuildListsVertexArrayObject(gl, renderMode);
                this.resolveListsShaderProgram.Bind(gl);
                CreateResolveListsVertexArrayObject(gl, renderMode);

                this.InitMisc(gl);
            }

            ResetMisc(gl);

            BeforeRendering(gl, renderMode);

            //gl.Enable(OpenGL.GL_BLEND);
            //gl.BlendFunc(SharpGL.Enumerations.BlendingSourceFactor.SourceAlpha, SharpGL.Enumerations.BlendingDestinationFactor.OneMinusSourceAlpha);

            gl.Disable(OpenGL.GL_DEPTH_TEST);
            //gl.Disable(OpenGL.GL_CULL_FACE);

            modelMatrix = mat4.identity();
            {
                ShaderProgram shaderProgram = this.buildListsShaderProgram;
                //  Bind the shader, set the matrices.
                shaderProgram.Bind(gl);
                shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
                shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
                shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
                //shaderProgram.SetUniform1(gl, "tex", this.texture.TextureName);
                gl.ActiveTexture(OpenGL.GL_TEXTURE1);
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                this.texture.Bind(gl);
                shaderProgram.SetUniform1(gl, "tex", 1);
                shaderProgram.SetUniform1(gl, "brightness", this.Brightness);
                shaderProgram.SetUniform1(gl, "opacity", this.Opacity);
                shaderProgram.SetUniform1(gl, "list_buffer_length", this.width * this.height * this.backup);
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


            gl.GetDelegateFor<OpenGL.glBindImageTexture>()(1, 0, 0, false, 0, OpenGL.GL_WRITE_ONLY, OpenGL.GL_RGBA32UI);
            gl.GetDelegateFor<OpenGL.glBindImageTexture>()(0, 0, 0, false, 0, OpenGL.GL_READ_WRITE, OpenGL.GL_R32UI);

            //gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            //gl.Disable(OpenGL.GL_BLEND);

            AfterRendering(gl, renderMode);
        }

    }
}
