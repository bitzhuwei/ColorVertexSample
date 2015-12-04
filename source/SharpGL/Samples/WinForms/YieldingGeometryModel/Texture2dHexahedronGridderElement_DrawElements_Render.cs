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
    public partial class Texture2dHexahedronGridderElement_DrawElements
    {
        public bool renderWireframe = false;

        protected void BeforeRendering(OpenGL gl, RenderMode renderMode)
        {
            this.visualBuffer = visiblesBufferObject;

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

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            this.texture.Bind(gl);

            modelMatrix = mat4.identity();
            ShaderProgram shader = this.shader;
            //  Bind the shader, set the matrices.
            shader.Bind(gl);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            shader.SetUniform1(gl, "tex", this.texture.TextureName);
            

            gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            BeforeRendering(gl, renderMode);

            gl.BindVertexArray(vertexArrayObject);

            gl.MultiDrawArrays(OpenGL.GL_QUAD_STRIP, this.firsts, this.counts, this.firsts.Length);

            gl.BindVertexArray(0);

            if (this.renderWireframe)
            {
                
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.Disable(OpenGL.GL_POLYGON_STIPPLE);
                
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
                gl.ShadeModel(SharpGL.Enumerations.ShadeModel.Smooth);
               
                gl.Hint(SharpGL.Enumerations.HintTarget.LineSmooth, SharpGL.Enumerations.HintMode.Nicest);
                gl.Hint(SharpGL.Enumerations.HintTarget.PolygonSmooth, SharpGL.Enumerations.HintMode.Nicest);
                //gl.BlendFunc(SharpGL.Enumerations.BlendingSourceFactor.SourceAlpha, SharpGL.Enumerations.BlendingDestinationFactor.OneMinusSourceAlpha);
                //gl.LineWidth(1);
                
                
                gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Lines);

                gl.BindVertexArray(vertexArrayObject);

                gl.DisableVertexAttribArray(ATTRIB_INDEX_COLOUR);
                gl.VertexAttrib3(ATTRIB_INDEX_COLOUR, 1.0f, 1.0f, 1.0f);

                gl.MultiDrawArrays(OpenGL.GL_QUAD_STRIP, this.firsts, this.counts, this.firsts.Length);

                gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);

                gl.BindVertexArray(0);

                gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Filled);
               
            }


            AfterRendering(gl, renderMode);
        }

        #endregion

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            shader.Unbind(gl);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
    }
}
