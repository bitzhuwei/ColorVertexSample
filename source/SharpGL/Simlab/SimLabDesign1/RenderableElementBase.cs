using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public abstract class RenderableElementBase : IVertexBuffers, IRenderable, IDisposable
    {

        #region 初始化和更新VBO

        /// <summary>
        /// 记录VBO的key和VBO对象的对应关系。
        /// </summary>
        Dictionary<string, AttributeBuffer> vboDict;
        //List<AttributeBuffer> attributeBufferList;
        IndexBuffer indexBuffer;

        void IVertexBuffers.AddAttributeBuffer(string varNameInShader, UnmanagedArrayBase values, UsageType usage, int size, uint type)
        {
            if (this.vboDict == null) { this.vboDict = new Dictionary<string, AttributeBuffer>(); }

            if (this.vboDict.ContainsKey(varNameInShader))
            { throw new ArgumentException(string.Format("key[{0}] already exists!")); }

            OpenGL gl = new OpenGL();
            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, buffers[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, values.ByteLength, values.Header, (uint)usage);

            this.vboDict.Add(varNameInShader, new AttributeBuffer() { BufferID = buffers[0], VarNameInShader = varNameInShader, Usage = usage, Size = size, Type = type, });
        }

        void IVertexBuffers.SetupIndexBuffer(UnmanagedArrayBase indexes, UsageType usage, int vertexCount)
        {
            OpenGL gl = new OpenGL();

            if (this.indexBuffer != null)
            {
                gl.DeleteBuffers(1, new uint[] { this.indexBuffer.BufferID });
            }

            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, buffers[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexes.ByteLength, indexes.Header, (uint)usage);

            this.indexBuffer = new IndexBuffer() { BufferID = buffers[0], Usage = usage, VertexCount = indexes.Length, };
        }

        void IVertexBuffers.UpdateVertexBuffer(string key, UnmanagedArrayBase newValues)
        {
            if (!this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] NOT exists!")); }

            BufferBase vbo = this.vboDict[key];
            OpenGL gl = new OpenGL();

            gl.BindBuffer(vbo.Target, vbo.BufferID);

            //IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            IntPtr dest = gl.MapBuffer(vbo.Target, OpenGL.GL_READ_WRITE);

            //MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            newValues.CopyTo(dest);

            gl.UnmapBuffer(vbo.Target);
        }

        void IVertexBuffers.UpdateVertexBuffer(string key, UnmanagedArrayBase newValues, int startIndex)
        {
            if (this.vboDict == null)
            { throw new Exception(string.Format("VBO must be setup first!")); }

            if (!this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] NOT exists!")); }

            BufferBase vbo = this.vboDict[key];
            OpenGL gl = new OpenGL();

            gl.BindBuffer(vbo.Target, vbo.BufferID);

            //IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            // TODO:此函数尚未验证过是否可用。
            IntPtr dest = gl.MapBufferRange(vbo.Target, startIndex, newValues.ByteLength, OpenGL.GL_READ_WRITE);

            //MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            newValues.CopyTo(dest);

            gl.UnmapBuffer(vbo.Target);
        }

        #endregion 初始化和更新VBO

        #region 初始化shader、VAO和渲染

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderer">指定renderer（用MultiDrawArrays或DrawElements）</param>
        public RenderableElementBase(Renderer renderer, IScientificCamera camera)
        {
            this.renderer = renderer;
            this.camera = camera;
        }

        ShaderProgram shaderProgram;
        uint[] vertexBufferArray;
        Renderer renderer;

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            if (vertexBufferArray == null)
            {
                lock (this)
                {
                    if (vertexBufferArray == null)
                    {
                        this.shaderProgram = InitShader(gl, renderMode);
                        GenerateVAO(gl, renderMode);

                    }
                }
            }

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

            modelMatrix = GlmNet.mat4.identity();
            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);

            this.renderer.Render(gl, renderMode);

            gl.Disable(OpenGL.GL_POLYGON_SMOOTH);

            shaderProgram.Unbind(gl);
        }

        private ShaderProgram InitShader(OpenGL gl, RenderMode renderMode)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronElement.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronElement.frag");
            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);

            foreach (var vbo in this.vboDict)
            {
                vbo.Value.FetchInfoFromShaderProgram(gl, shaderProgram);
            }

            shaderProgram.AssertValid(gl);
            return shaderProgram;
        }

        private void GenerateVAO(OpenGL gl, RenderMode renderMode)
        {
            vertexBufferArray = new uint[1];
            gl.GenVertexArrays(1, vertexBufferArray);
            gl.BindVertexArray(vertexBufferArray[0]);

            foreach (var vbo in vboDict)
            {
                vbo.Value.LayoutForVAO(gl);
            }

            gl.BindVertexArray(0);
        }

        #endregion 初始化shader、VAO和渲染

        #region 释放VBO和VAO

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RenderableElementBase()
        {
            this.Dispose(false);
        }

        private bool disposedValue = false;
        private IScientificCamera camera;
        private GlmNet.mat4 projectionMatrix;
        private GlmNet.mat4 viewMatrix;
        private GlmNet.mat4 modelMatrix;

        protected virtual void Dispose(bool disposing)
        {

            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.

                }

                // Dispose unmanaged resources.
                OpenGL gl = new OpenGL();// this is not cool.

                if (this.vboDict != null)
                {
                    uint[] buffers = new uint[this.vboDict.Count];
                    int i = 0;
                    foreach (var vbo in vboDict)
                    {
                        buffers[i++] = vbo.Value.BufferID;
                    }

                    gl.DeleteBuffers(buffers.Length, buffers);
                }

                if(this.indexBuffer!=null)
                {
                    gl.DeleteBuffers(1, new uint[] { this.indexBuffer.BufferID });
                }

                if (this.vertexBufferArray != null)
                {
                    gl.DeleteVertexArrays(this.vertexBufferArray.Length, this.vertexBufferArray);
                }

            }

            this.disposedValue = true;
        }


        #endregion 释放VBO和VAO


    }
}
