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
    public partial class HexahedronGridderElement 
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

            modelMatrix = mat4.identity();
            ShaderProgram shader = this.shader;
            //  Bind the shader, set the matrices.
            shader.Bind(gl);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            BeforeRendering(gl, renderMode);

            // 用VAO+EBO进行渲染。
            //  Bind the out vertex array.
            gl.BindVertexArray(vertexArrayObject);

            gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);

            //  Draw the square.
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.triangleStripIndexBuffer);

            // 启用Primitive restart
            gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
            gl.PrimitiveRestartIndex(uint.MaxValue);// 截断图元（四边形带、三角形带等）的索引值。
            gl.DrawElements(OpenGL.GL_TRIANGLE_STRIP, this.triangleIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

            //  Unbind our vertex array and shader.
            gl.BindVertexArray(0);
            gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

            if (this.renderWireframe)
            {
                gl.BindVertexArray(vertexArrayObject);

                gl.DisableVertexAttribArray(ATTRIB_INDEX_COLOUR);
                gl.VertexAttrib3(ATTRIB_INDEX_COLOUR, 1.0f, 1.0f, 1.0f);

                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.lineIndexBuffer);
                gl.DrawElements(OpenGL.GL_LINES, this.lineIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

                gl.BindVertexArray(0);
            }

            
            AfterRendering(gl, renderMode);
        }

        #endregion

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            shader.Unbind(gl);
        }
    }
}
