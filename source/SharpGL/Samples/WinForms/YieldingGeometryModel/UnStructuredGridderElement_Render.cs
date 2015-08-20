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
    public partial class UnStructuredGridderElement
    {
        public bool renderFractions = true;
        public bool renderFractionsWireframe = false;
        public bool renderTetras = true;
        public bool renderTetrasWireframe = false;

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
            ShaderProgram shaderProgram = this.shaderProgram;
            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            //gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            //gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            BeforeRendering(gl, renderMode);

            //gl.MultiDrawArrays(OpenGL.GL_QUAD_STRIP, this.firsts, this.counts, this.firsts.Length);
            if (this.renderFractions)
            {
                gl.BindVertexArray(vertexArrayObject[0]);

                gl.DrawElements(OpenGL.GL_TRIANGLES, this.fractionsIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

                if (this.renderFractionsWireframe)
                {
                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Lines);

                    gl.DrawElements(OpenGL.GL_TRIANGLES, this.fractionsIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Filled);
                }

                gl.BindVertexArray(0);
            }

            if (this.renderTetras)
            {
                gl.BindVertexArray(vertexArrayObject[1]);

                gl.DrawElements(OpenGL.GL_TRIANGLES, this.tetrasIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

                if (this.renderTetrasWireframe)
                {
                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Lines);

                    gl.DrawElements(OpenGL.GL_TRIANGLES, this.tetrasIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Filled);
                }

                gl.BindVertexArray(0);
            }


            AfterRendering(gl, renderMode);
        }

        #endregion

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            //gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            shaderProgram.Unbind(gl);
        }
    }
}
