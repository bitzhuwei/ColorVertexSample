using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SimLabDesign1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1_Elements
{
    /// <summary>
    /// 可渲染多个六面体的场景元素。
    /// <para>初始化和使用类似这种Element的操作顺序：</para>
    /// <para>1. 用SetupVertexBuffer设定所有的VBO（及时暂时没有数据也要setup）</para>
    /// <para>2. 用InitShader初始化shader并获取其中各个in数组的location</para>
    /// <para>3. 用GenerateVAO创建VAO</para>
    /// <para>4. 指定renderer（用MultiDrawArrays还是DrawElements）</para>
    /// </summary>
    public class HexahedronElement : IVertexBuffers, IRenderable, IDisposable
    {
        /// <summary>
        /// 对应shader中的in变量；对应vboDict中的某个key。
        /// </summary>
        public const string key_in_Position = "in_Position";

        /// <summary>
        /// 对应shader中的in变量；对应vboDict中的某个key。
        /// </summary>
        public const string key_in_Color = "in_Color";

        /// <summary>
        /// 对应shader中的in变量；对应vboDict中的某个key。
        /// </summary>
        public const string key_in_visible = "in_visible";

        #region 初始化和更新VBO

        /// <summary>
        /// 记录VBO的key和VBO对象的对应关系。
        /// </summary>
        Dictionary<string, VBOInfo> vboDict;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">如果是顶点属性数组，请从HexahedronElement.key_...中选择。
        /// <para>如果是索引数组，可自行定义。</para></param>
        /// <param name="target"></param>
        /// <param name="values"></param>
        /// <param name="usage"></param>
        /// <param name="size"></param>
        /// <param name="type"></param>
        void IVertexBuffers.CreateVertexBuffer<T>(string key, uint target, UnmanagedArray<T> values, uint usage, int size, uint type)
        {
            if (this.vboDict == null) { this.vboDict = new Dictionary<string, VBOInfo>(); }

            if (this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] already exists!")); }

            OpenGL gl = new OpenGL();
            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            gl.BindBuffer(target, buffers[0]);
            gl.BufferData(target, values.ByteLength, values.Header, usage);

            this.vboDict.Add(key,
                new VBOInfo() { BufferID = buffers[0], Target = target, Usage = usage, Size = size, Type = type });
        }

        void IVertexBuffers.UpdateVertexBuffer<T>(string key, UnmanagedArray<T> newValues)
        {
            if (this.vboDict == null)
            { throw new Exception(string.Format("VBO must be setup first!")); }

            if (!this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] NOT exists!")); }

            VBOInfo vbo = this.vboDict[key];
            OpenGL gl = new OpenGL();

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vbo.BufferID);

            //IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            IntPtr dest = gl.MapBuffer(vbo.Target, OpenGL.GL_READ_WRITE);

            //MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            newValues.CopyTo(dest);

            gl.UnmapBuffer(vbo.Target);
        }

        void IVertexBuffers.UpdateVertexBuffer<T>(string key, UnmanagedArray<T> newValues, int startIndex)
        {
            if (this.vboDict == null)
            { throw new Exception(string.Format("VBO must be setup first!")); }

            if (!this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] NOT exists!")); }

            VBOInfo vbo = this.vboDict[key];
            OpenGL gl = new OpenGL();

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vbo.BufferID);

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
        /// <param name="renderer">指定renderer（用MultiDrawArrays还是DrawElements）</param>
        public HexahedronElement(Renderer renderer)
        {
            this.renderer = renderer;
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

            this.renderer.Render(gl, renderMode);
        }

        private ShaderProgram InitShader(OpenGL gl, RenderMode renderMode)
        {
            String vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronElement.vert");
            String fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"HexahedronElement.frag");
            ShaderProgram shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);

            foreach (var vbo in this.vboDict)
            {
                if (vbo.Value.Target != OpenGL.GL_ELEMENT_ARRAY_BUFFER)//不是索引的vbo才在shader里有对应的location（目前就是OpenGL.GL_ARRAY_BUFFER）
                {
                    int location = shaderProgram.GetAttributeLocation(gl, vbo.Key);
                    if (location < 0) { throw new Exception(string.Format("key[{0}] NOT exists in shader!")); }
                    vbo.Value.AttribLocation = (uint)location;
                }
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
                if (vbo.Value.Target != OpenGL.GL_ELEMENT_ARRAY_BUFFER)//不是索引的vbo才在shader里有对应的location（目前就是OpenGL.GL_ARRAY_BUFFER）
                {
                    gl.BindBuffer(vbo.Value.Target, vbo.Value.BufferID);
                    gl.VertexAttribPointer(vbo.Value.AttribLocation, vbo.Value.Size, vbo.Value.Type, false, 0, IntPtr.Zero);
                    gl.EnableVertexAttribArray(vbo.Value.AttribLocation);
                }
                else
                {
                    //TODO:也许这一步也不需要？
                    gl.BindBuffer(vbo.Value.Target, vbo.Value.BufferID);
                }
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

        ~HexahedronElement()
        {
            this.Dispose(false);
        }

        private bool disposedValue = false;

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
