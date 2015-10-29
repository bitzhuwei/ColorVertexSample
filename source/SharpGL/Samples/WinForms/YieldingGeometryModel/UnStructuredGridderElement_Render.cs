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

        //private int currentTetrasIndexCount = 2;

        //public int CurrentTetrasIndexCount
        //{
        //    get { return currentTetrasIndexCount; }
        //    set
        //    {
        //        if (2 <= value && value <= this.tetrasIndexBufferObjectCount)
        //        {
        //            currentTetrasIndexCount = value;
        //        }
        //    }
        //}
        public void Render(OpenGL gl, RenderMode renderMode)
        {
            BeforeRendering(gl, renderMode);


            //gl.MultiDrawArrays(OpenGL.GL_QUAD_STRIP, this.firsts, this.counts, this.firsts.Length);
            if (this.renderFractions)
            {
                gl.Enable(OpenGL.GL_POLYGON_OFFSET_FILL);
                gl.PolygonOffset(1.0f, 1.0f);

                gl.BindVertexArray(vertexArrayObject[0]);

                //gl.DrawElements(OpenGL.GL_TRIANGLES, this.fractionsIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, this.fractionsBufferObjectCount);

                gl.BindVertexArray(0);

                gl.Disable(OpenGL.GL_POLYGON_OFFSET_FILL);
            }

            if (this.renderFractionsWireframe)
            {
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.Disable(OpenGL.GL_POLYGON_STIPPLE);

                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
                gl.ShadeModel(SharpGL.Enumerations.ShadeModel.Smooth);

                gl.Hint(SharpGL.Enumerations.HintTarget.LineSmooth, SharpGL.Enumerations.HintMode.Nicest);
                gl.Hint(SharpGL.Enumerations.HintTarget.PolygonSmooth, SharpGL.Enumerations.HintMode.Nicest);

                gl.BindVertexArray(vertexArrayObject[0]);
                {
                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Lines);

                    gl.DisableVertexAttribArray(this.in_ColorLocation);
                    gl.VertexAttrib3(this.in_ColorLocation, 1.0f, 1.0f, 1.0f);

                    //gl.DrawElements(OpenGL.GL_TRIANGLES, this.fractionsIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                    gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, this.fractionsBufferObjectCount);

                    gl.EnableVertexAttribArray(this.in_ColorLocation);

                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Filled);
                }
                gl.BindVertexArray(0);
            }

            if (this.renderTetras)
            {
                gl.Enable(OpenGL.GL_POLYGON_OFFSET_FILL);
                gl.PolygonOffset(1.0f, 1.0f);

                gl.BindVertexArray(vertexArrayObject[1]);

                //gl.DrawElements(OpenGL.GL_TRIANGLES, this.tetrasIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
                gl.PrimitiveRestartIndex(uint.MaxValue);
                gl.DrawElements(OpenGL.GL_TRIANGLE_STRIP, this.tetrasIndexBufferObjectCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                //gl.DrawElements(OpenGL.GL_TRIANGLE_STRIP, this.CurrentTetrasIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                //gl.DrawArrays(OpenGL.GL_TRIANGLE_STRIP, 0, this.tetrasIndexBufferObjectCount);
                gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

                gl.BindVertexArray(0);

                gl.Disable(OpenGL.GL_POLYGON_OFFSET_FILL);
            }

            if (this.renderTetrasWireframe)
            {
                gl.BindVertexArray(vertexArrayObject[1]);
                {
                    gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Lines);

                    gl.DisableVertexAttribArray(this.in_ColorLocation);
                    gl.VertexAttrib3(this.in_ColorLocation, 1.0f, 1.0f, 1.0f);

                    gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
                    gl.PrimitiveRestartIndex(uint.MaxValue);
                    gl.DrawElements(OpenGL.GL_TRIANGLE_STRIP, this.tetrasIndexBufferObjectCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                    //gl.DrawElements(OpenGL.GL_TRIANGLE_STRIP, this.CurrentTetrasIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                    //gl.DrawArrays(OpenGL.GL_TRIANGLE_STRIP, 0, this.tetrasIndexBufferObjectCount);
                    gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

                    gl.EnableVertexAttribArray(this.in_ColorLocation);

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
