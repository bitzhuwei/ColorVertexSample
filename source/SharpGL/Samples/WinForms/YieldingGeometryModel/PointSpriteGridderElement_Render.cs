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
    public partial class PointSpriteGridderElement 
    {

        protected override void BeforeRendering(OpenGL gl, RenderMode renderMode)
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

            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendEquation(OpenGL.GL_FUNC_ADD_EXT);
            gl.BlendFuncSeparate(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA, OpenGL.GL_ONE, OpenGL.GL_ONE);


            this.texture.Bind(gl);

            int[] viewport;
            var projectionMatrix = GetProjectionMatrix(gl, out viewport);
            var cameraPosition = GetCameraPosition();
            var basePointSize = 30f;
            //var Color = new float[] { 1, 0, 0 };
            //var lightDir = new float[] { 1, 1, 1 };

            ShaderProgram shader = this.shader;

            shader.Bind(gl);
            shader.SetUniform1(gl, "tex", this.texture.TextureName);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            shader.SetUniform1(gl, "basePointSize", basePointSize);
            shader.SetUniform3(gl, "cameraPosition", cameraPosition.x, cameraPosition.y, cameraPosition.z);
            shader.SetUniform1(gl, "canvasWidth", viewport[2]);
            shader.SetUniform1(gl, "canvasHeight", viewport[3]);
            //shaderProgram.SetUniform3(gl, "Color", Color[0], Color[1], Color[2]);
            //shaderProgram.SetUniform3(gl, "lighDir", lightDir[0], lightDir[1], lightDir[2]);
        }

        protected override void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            shader.Unbind(gl);
            gl.Disable(OpenGL.GL_BLEND);
            gl.Disable(OpenGL.GL_VERTEX_PROGRAM_POINT_SIZE);
            gl.Disable(OpenGL.GL_POINT_SPRITE_ARB);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }

        protected mat4 GetProjectionMatrix(OpenGL gl, out int[] viewport)
        {
            //  get perspective projection matrix.
            viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            var projectionMatrix = glm.perspective(rads, (float)viewport[2] / (float)viewport[3], 0.01f, 1000.0f);
            return projectionMatrix;
        }

        protected mat4 GetViewMatrix()
        {
            var viewMatrix = glm.lookAt(GetCameraPosition(),
                new vec3(0, 0, 0), new vec3(0, 0, -1));
            return viewMatrix;
        }

        protected vec3 GetCameraPosition()
        {
            return new vec3(-12.5f, 12.5f, -12.5f);
        }
    }
}
