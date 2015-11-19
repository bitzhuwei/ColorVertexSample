using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVertexSample
{
    /// <summary>
    /// 用纹理装饰的正方体。此类型用于演示GLSL+Texture2D的用法。
    /// </summary>
    class TexturedCube : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {
        /// <summary>
        /// shader program
        /// </summary>
        public ShaderProgram shaderProgram;
        const string strin_Position = "in_Position";
        const string strin_uv = "in_uv";
        public const string strMVP = "MVP";

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

        /// <summary>
        /// 金字塔的posotion array.
        /// </summary>
        static vec3[] positions = new vec3[]
		{
			new vec3(-1.0f, -1.0f, 1.0f),	
			new vec3(1.0f, -1.0f, 1.0f),	
			new vec3(1.0f, 1.0f, 1.0f),	
			new vec3(-1.0f, 1.0f, 1.0f),	
			new vec3(-1.0f, -1.0f, -1.0f),
			new vec3(-1.0f, 1.0f, -1.0f),	
			new vec3(1.0f, 1.0f, -1.0f),	
			new vec3(1.0f, -1.0f, -1.0f),	
			new vec3(-1.0f, 1.0f, -1.0f),	
			new vec3(-1.0f, 1.0f, 1.0f),	
			new vec3(1.0f, 1.0f, 1.0f),	
			new vec3(1.0f, 1.0f, -1.0f),	
			new vec3(-1.0f, -1.0f, -1.0f),
			new vec3(1.0f, -1.0f, -1.0f),	
			new vec3(1.0f, -1.0f, 1.0f),	
			new vec3(-1.0f, -1.0f, 1.0f),	
			new vec3(1.0f, -1.0f, -1.0f),	
			new vec3(1.0f, 1.0f, -1.0f),	
			new vec3(1.0f, 1.0f, 1.0f),	
			new vec3(1.0f, -1.0f, 1.0f),	
			new vec3(-1.0f, -1.0f, -1.0f),
			new vec3(-1.0f, -1.0f, 1.0f),	
			new vec3(-1.0f, 1.0f, 1.0f),	
			new vec3(-1.0f, 1.0f, -1.0f),	
		};

        static readonly vec2[] uvs = new vec2[]
        {
            new vec2(0.0f, 0.0f),
            new vec2(1.0f, 0.0f),
            new vec2(1.0f, 1.0f),
            new vec2(0.0f, 1.0f),
            new vec2(1.0f, 0.0f),
            new vec2(1.0f, 1.0f),
            new vec2(0.0f, 1.0f),
            new vec2(0.0f, 0.0f),
            new vec2(0.0f, 1.0f),
            new vec2(0.0f, 0.0f),
            new vec2(1.0f, 0.0f),
            new vec2(1.0f, 1.0f),
            new vec2(1.0f, 1.0f),
            new vec2(0.0f, 1.0f),
            new vec2(0.0f, 0.0f),
            new vec2(1.0f, 0.0f),
            new vec2(1.0f, 0.0f),
            new vec2(1.0f, 1.0f),
            new vec2(0.0f, 1.0f),
            new vec2(0.0f, 0.0f),
            new vec2(0.0f, 0.0f),
            new vec2(1.0f, 0.0f),
            new vec2(1.0f, 1.0f),
            new vec2(0.0f, 1.0f),
        };
        private string textureFile;

        private Texture tex = new Texture();

        public TexturedCube(string textureFile)
        {
            this.textureFile = textureFile;
        }

        protected void InitializeShader(OpenGL gl, out ShaderProgram shaderProgram)
        {
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"TexturedCube.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"TexturedCube.frag");

            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.AssertValid(gl);
        }

        public void Initialize(OpenGL gl)
        {
            InitializeTexture2D(gl);

            InitializeShader(gl, out shaderProgram);

            InitializeVAO(gl, out vao, out primitiveMode, out vertexCount);

            //base.BeforeRendering += IMVPHelper.Getelement_BeforeRendering();
            //base.AfterRendering += IMVPHelper.Getelement_AfterRendering();
        }

        private void InitializeTexture2D(OpenGL gl)
        {
            this.tex = new Texture();
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(this.textureFile);
            this.tex.Create(gl, this.textureFile);
        }


        protected void InitializeVAO(OpenGL gl, out uint[] vao, out BeginMode primitiveMode, out int vertexCount)
        {
            primitiveMode = BeginMode.Quads;
            vertexCount = positions.Length;

            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);

            //  Create a vertex buffer for the vertex data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);
                var positionArray = new UnmanagedArray<vec3>(positions.Length);
                for (int i = 0; i < positions.Length; i++)
                {
                    positionArray[i] = positions[i];
                }

                uint positionLocation = (uint)shaderProgram.GetAttributeLocation(gl, strin_Position);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(positionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(positionLocation);

                positionArray.Dispose();
            }
            //  Create a vertex buffer for the uv data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);
                var uvArray = new UnmanagedArray<vec2>(uvs.Length);
                for (int i = 0; i < uvs.Length; i++)
                {
                    uvArray[i] = uvs[i];
                }

                uint uvLocation = (uint)shaderProgram.GetAttributeLocation(gl, strin_uv);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, uvArray.ByteLength, uvArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(uvLocation, 2, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(uvLocation);

                uvArray.Dispose();
            }
            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }

        public void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
            gl.BindVertexArray(vao[0]);

            gl.DrawArrays((uint)primitiveMode, 0, vertexCount);

            gl.BindVertexArray(0);
        }
    }
}
