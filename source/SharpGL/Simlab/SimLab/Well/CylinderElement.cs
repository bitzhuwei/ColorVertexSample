using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 圆柱体
    /// </summary>
    public class CylinderElement : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {

        /// <summary>
        /// shader program
        /// </summary>
        public ShaderProgram shaderProgram;
        const string strin_Position = "in_Position";
        const string strin_Color = "in_Color";
        public const string strprojectionMatrix = "projectionMatrix";
        public const string strviewMatrix = "viewMatrix";
        public const string strmodelMatrix = "modelMatrix";

        /// <summary>
        /// VAO
        /// </summary>
        protected uint[] vao;

        /// <summary>
        /// 图元类型
        /// </summary>
        protected BeginMode primitiveMode;

        /// <summary>
        /// 顶点数
        /// </summary>
        protected int vertexCount;

        private float radius;
        private float height;
        private int faceCount;
        private GLColor color;

        public CylinderElement(float radius, float height, int faceCount, GLColor color)
        {
            this.radius = radius;
            this.height = height;
            this.faceCount = faceCount;
            this.color = color;
        }

        protected void InitializeShader(OpenGL gl, out ShaderProgram shaderProgram)
        {
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"Well.CylinderElement.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"Well.CylinderElement.frag");

            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.AssertValid(gl);
        }

        protected void InitializeVAO(OpenGL gl, out uint[] vao, out BeginMode primitiveMode, out int vertexCount)
        {
            primitiveMode =  BeginMode.QuadStrip;
            vertexCount = faceCount * 2;

            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);

            //  Create a vertex buffer for the vertex data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);
                UnmanagedArray<vec3> positionArray = new UnmanagedArray<vec3>(faceCount * 2);
                for (int i = 0; i < faceCount * 2; i++)
                {
                    int face = i / 2;
                    positionArray[i] = new vec3(
                        (float)(this.radius * Math.Cos(face * (Math.PI * 2) / faceCount)),
                        (i % 2 == 1 ? -1 : 1) * this.height / 2,
                        (float)(this.radius * Math.Sin(face * (Math.PI * 2) / faceCount))
                        );
                }

                int location = gl.GetAttribLocation(shaderProgram.ShaderProgramObject, strin_Position);
                if (location < 0) { throw new Exception(); }
                uint positionLocation = (uint)location;

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(positionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(positionLocation);
                positionArray.Dispose();
            }

            //  Now do the same for the colour data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);
                UnmanagedArray<vec3> colorArray = new UnmanagedArray<vec3>(faceCount * 2);
                vec3 vec3Color = new vec3(this.color.R, this.color.G, this.color.B);
                for (int i = 0; i < colorArray.Length; i++)
                {
                    colorArray[i] = vec3Color;
                }

                int location = gl.GetAttribLocation(shaderProgram.ShaderProgramObject,strin_Color);
                if (location < 0) { throw new Exception(); }
                uint colorLocation = (uint)location;

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(colorLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(colorLocation);
                colorArray.Dispose();
            }
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ids[0]);
                UnmanagedArray<uint> cylinderIndex = new UnmanagedArray<uint>(faceCount * 2 + 2);
                for (int i = 0; i < cylinderIndex.Length - 2; i++)
                {
                    cylinderIndex[i] = (uint)i;
                }
                cylinderIndex[cylinderIndex.Length - 2] = 0;
                cylinderIndex[cylinderIndex.Length - 1] = 1;
                gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, cylinderIndex.ByteLength, cylinderIndex.Header, OpenGL.GL_STATIC_DRAW);
                cylinderIndex.Dispose();
            }
            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }

        public void Initialize(SharpGL.OpenGL gl)
        {
            InitializeShader(gl, out shaderProgram);

            InitializeVAO(gl, out vao, out primitiveMode, out vertexCount);

        }

        public void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
            gl.BindVertexArray(vao[0]);

            //GL.DrawArrays(primitiveMode, 0, vertexCount);
            gl.DrawElements((uint)primitiveMode, faceCount * 2 + 2, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

            gl.BindVertexArray(0);
        }
    }
}
