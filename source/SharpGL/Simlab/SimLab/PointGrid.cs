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
    public class PointGrid : SimLabGrid, IRenderable
    {
        private const string in_Position = "in_Position";
        private const string in_uv = "in_uv";
        private const string in_radius = "in_radius";

        uint ATTRIB_INDEX_POSITION = 0;
        uint ATTRIB_INDEX_COLOUR = 1;
        uint ATTRIB_INDEX_RADIUS = 2;

        private uint[] radiusBuffer;

        private GlmNet.mat4 projectionMatrix;
        private GlmNet.mat4 viewMatrix;
        private mat4 modelMatrix;
        private ShaderProgram shaderProgram;

        public PointGrid(OpenGL gl, IScientificCamera camera)
            : base(gl, camera)
        {

        }

        #region IRenderable

        protected void BeforeRendering(OpenGL gl, RenderMode renderMode)
        {
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (this.shaderProgram == null)
            {
                this.shaderProgram = InitShader(gl, renderMode);
            }
            if (this.vertexArrayObject == null)
            {
                CreateVertexArrayObject(gl, renderMode);
            }

            BeforeRendering(gl, renderMode);

            if (this.RenderGrid)
            {
                if (positionBuffer != null && colorBuffer != null && radiusBuffer != null)
                {
                    gl.BindVertexArray(this.vertexArrayObject[0]);
                    //gl.DrawArrays(OpenGL.GL_POINTS, 0, );
                    gl.BindVertexArray(0);
                    //// prepare positions
                    //{
                    //    int location = shaderProgram.GetAttributeLocation(gl, in_Position);
                    //    ATTRIB_INDEX_POSITION = (uint)location;
                    //    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
                    //    gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                    //    gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);
                    //}
                    //// prepare colors
                    //{
                    //    int location = shaderProgram.GetAttributeLocation(gl, in_uv);
                    //    ATTRIB_INDEX_COLOUR = (uint)location;
                    //    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
                    //    gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                    //    gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);
                    //}
                    //// prepare index
                    //{
                    //    gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
                    //}

                    //gl.DrawElements(OpenGL.GL_TRIANGLES, this.indexBufferLength, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                }
            }

            AfterRendering(gl, renderMode);
        }

        uint[] vertexArrayObject;

        private void CreateVertexArrayObject(OpenGL gl, RenderMode renderMode)
        {
            this.vertexArrayObject = new uint[1];
            gl.GenVertexArrays(1, this.vertexArrayObject);
            gl.BindVertexArray(this.vertexArrayObject[0]);

            // prepare positions
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_Position);
                ATTRIB_INDEX_POSITION = (uint)location;
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
                gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);
            }
            // prepare colors
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_uv);
                ATTRIB_INDEX_COLOUR = (uint)location;
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
                gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);
            }
            // prepare radius
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_radius);
                ATTRIB_INDEX_RADIUS = (uint)location;
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
                gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);
            }

            gl.BindVertexArray(0);
        }

        private ShaderProgram InitShader(OpenGL gl, RenderMode renderMode)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"PointGrid.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"PointGrid.frag");

            var shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);

            {
                int location = shaderProgram.GetAttributeLocation(gl, in_Position);
                if (location < 0) { throw new ArgumentException(); }
                this.ATTRIB_INDEX_POSITION = (uint)location;
            }
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_uv);
                if (location < 0) { throw new ArgumentException(); }
                this.ATTRIB_INDEX_POSITION = (uint)location;
            }
            {
                int location = shaderProgram.GetAttributeLocation(gl, in_radius);
                if (location < 0) { throw new ArgumentException(); }
                this.ATTRIB_INDEX_POSITION = (uint)location;
            }

            shaderProgram.AssertValid(gl);

            return shaderProgram;
        }

        #endregion

        protected void AfterRendering(OpenGL gl, RenderMode renderMode)
        {
            gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            shaderProgram.Unbind(gl);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }

        #endregion IRenderable
    }
}
