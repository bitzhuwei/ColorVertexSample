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
    public class HexahedronGrid : SimLabGrid, IRenderable
    {
        private const string in_position = "in_Position";
        private const string in_uv = "in_uv";
        private const uint ATTRIB_INDEX_POSITION = 0;
        private const uint ATTRIB_INDEX_COLOUR = 1;

        private GlmNet.mat4 projectionMatrix;
        private GlmNet.mat4 viewMatrix;
        private mat4 modelMatrix;
        private ShaderProgram shaderProgram;

        public HexahedronGrid(OpenGL gl, IScientificCamera camera)
            : base(gl, camera)
        {

        }
        #region IRenderable

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

            gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (this.shaderProgram == null)
            {
                this.shaderProgram = InitShader(gl, renderMode);
            }

            BeforeRendering(gl, renderMode);

            if (this.RenderGrid)
            {
                // prepare positions
                {
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
                    gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                    gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);
                }
                // prepare colors
                {
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
                    gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                    gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);
                }
                // prepare index
                {
                    gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
                }

                gl.DrawElements(OpenGL.GL_TRIANGLES, this.geometry.Positions.SizeInBytes / (3 * 4), OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
            }
            //gl.MultiDrawArrays(OpenGL.GL_QUAD_STRIP, this.firsts, this.counts, this.firsts.Length);

            //gl.BindVertexArray(0);

            if (this.RenderGridWireFrame)
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

                //gl.BindVertexArray(vertexArrayObject);

                gl.DisableVertexAttribArray(ATTRIB_INDEX_COLOUR);
                gl.VertexAttrib3(ATTRIB_INDEX_COLOUR, 1.0f, 1.0f, 1.0f);

                //gl.MultiDrawArrays(OpenGL.GL_QUAD_STRIP, this.firsts, this.counts, this.firsts.Length);

                gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);

                //gl.BindVertexArray(0);

                gl.PolygonMode(SharpGL.Enumerations.FaceMode.FrontAndBack, SharpGL.Enumerations.PolygonMode.Filled);

            }

            AfterRendering(gl, renderMode);
        }

        private ShaderProgram InitShader(OpenGL gl, RenderMode renderMode)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronGrid.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronGrid.frag");
            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.BindAttributeLocation(gl, ATTRIB_INDEX_POSITION, in_position);
            shaderProgram.BindAttributeLocation(gl, ATTRIB_INDEX_COLOUR, in_uv);
            shaderProgram.AssertValid(gl);
            return shaderProgram;
        }

        #endregion

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            shaderProgram.Unbind(gl);
        }

        #endregion IRenderable
    }
}
