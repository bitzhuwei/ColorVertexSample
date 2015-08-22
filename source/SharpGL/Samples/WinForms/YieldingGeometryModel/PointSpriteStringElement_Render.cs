using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
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
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class PointSpriteStringElement 
    {

        protected void BeforeRendering(OpenGL gl, RenderMode renderMode)
        {
            IScientificCamera camera = this.camera;
            if (camera != null)
            {
                if (camera.CameraType == CameraTypes.Perspecitive)
                {
                    IPerspectiveViewCamera perspective = camera;
                    this.projectionMatrix = perspective.GetProjectionMat4();
                    this.viewMatrix = perspective.GetViewMat4();
                }
                else if (camera.CameraType == CameraTypes.Ortho)
                {
                    IOrthoViewCamera ortho = camera;
                    this.projectionMatrix = ortho.GetProjectionMat4();
                    this.viewMatrix = ortho.GetViewMat4();
                }
                else
                { throw new NotImplementedException(); }
            }

            modelMatrix = mat4.identity();

            gl.Enable(OpenGL.GL_VERTEX_PROGRAM_POINT_SIZE);
            gl.Enable(OpenGL.GL_POINT_SPRITE_ARB);
            gl.TexEnv(OpenGL.GL_POINT_SPRITE_ARB, OpenGL.GL_COORD_REPLACE_ARB, OpenGL.GL_TRUE);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendEquation(OpenGL.GL_FUNC_ADD_EXT);
            gl.BlendFuncSeparate(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA, OpenGL.GL_ONE, OpenGL.GL_ONE);


            //this.texture.Bind(gl);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, this.texture[0]);

            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

            ShaderProgram shader = this.shaderProgram;

            shader.Bind(gl);
            shader.SetUniform1(gl, "tex", this.texture[0]);//.TextureName);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            shader.SetUniform1(gl, "canvasWidth", viewport[2] + 0.0f);
            shader.SetUniform1(gl, "canvasHeight", viewport[3] + 0.0f);
        }

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            shaderProgram.Unbind(gl);
            gl.Disable(OpenGL.GL_BLEND);
            gl.Disable(OpenGL.GL_VERTEX_PROGRAM_POINT_SIZE);
            gl.Disable(OpenGL.GL_POINT_SPRITE_ARB);
            gl.Disable(OpenGL.GL_POINT_SMOOTH);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }

    }
}
