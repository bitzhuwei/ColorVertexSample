using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneComponent.Utility;
using SharpGL.SceneGraph;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL.SceneComponent
{

    
    /// <summary>
    /// 用Shader+VAO+index array buffer object进行渲染的元素。
    /// </summary>
    public abstract class IndexedVAOElement : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {
        /// <summary>
        /// vertex array buffer object.
        /// </summary>
        protected uint[] vao;

        private  uint  vertexArrayObject = 0;

        private  uint  vertexsBufferObject = 0;

        private  uint  colorsBufferObject = 0;

        protected uint  visiblesBufferObject = 0;

        private  uint  triangleBufferObject = 0;

        private  int   triangleIndexCount = 0;


        private bool IsRenderable()
        {
           
            if (vertexArrayObject == 0||vertexsBufferObject==0||colorsBufferObject==0
                ||triangleBufferObject==0||triangleBufferObject==0||triangleIndexCount==0)
                return false;
             return  true;
            
        }

        /// <summary>
        /// element array buffer object.
        /// </summary>
        protected uint[] ebo;

        /// <summary>
        /// Specifies what kind of primitives to render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted
        /// </summary>
        protected uint primitiveMode;

        /// <summary>
        /// 索引元素的数目。
        /// Count of elements in index buffer array.
        /// </summary>
        protected int indexArrayElementCount;

        protected ShaderProgram shader;

        //  Constants that specify the attribute indexes.
        public  const uint ATTRIB_INDEX_POSITION = 0;
        public  const uint ATTRIB_INDEX_COLOUR = 1;
        public  const uint ATTRIB_INDEX_VISIBLE = 2;

        protected bool isInitialized = false;

        private int method = 1;



        private void InitVertexes(OpenGL gl, UnmanagedArray<Vertex> vertexes, UnmanagedArray<ColorF> colorArray, UnmanagedArray<float> visibles)
        {
            uint[] vao = new uint[1];
            gl.GenVertexArrays(vao.Length, vao);
            gl.BindVertexArray(vao[0]);

            this.vertexArrayObject = vao[0];
            uint[] vboVertex = new uint[1];
            gl.GenBuffers(vboVertex.Length, vboVertex);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVertex[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, vertexes.ByteLength, vertexes.Header, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);
            this.vertexsBufferObject = vboVertex[0];


            uint[] vboColor = new uint[1];
            gl.GenBuffers(vboColor.Length, vboColor);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboColor[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_DYNAMIC_DRAW);
            gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);
            this.colorsBufferObject = vboColor[0];

            uint[] vboVisual = new uint[1];
            gl.GenBuffers(vboVisual.Length, vboVisual);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVisual[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, visibles.ByteLength, visibles.Header, OpenGL.GL_DYNAMIC_READ);
            gl.VertexAttribPointer(ATTRIB_INDEX_VISIBLE, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_VISIBLE);
            this.visiblesBufferObject = vboVisual[0];

            gl.BindVertexArray(0);
        }

        private void InitTrianglesBuffer(OpenGL gl, UnmanagedArray<uint> TriangleStrip)
        {
            uint[] triangleBuffer = new uint[1];
            gl.GenBuffers(triangleBuffer.Length, triangleBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, triangleBuffer[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, TriangleStrip.ByteLength, TriangleStrip.Header, OpenGL.GL_STATIC_DRAW);
            this.triangleIndexCount = TriangleStrip.Length;
            this.triangleBufferObject = triangleBuffer[0];
        }


        public void UpdateColorBuffer(OpenGL gl, UnmanagedArray<ColorF> colors, UnmanagedArray<float> visibles)
        {
            if (this.visiblesBufferObject == 0 || this.colorsBufferObject == 0)
                return;
            gl.MakeCurrent();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.visiblesBufferObject);
            IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorsBufferObject);
            IntPtr destColors = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER,OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destColors, colors.Header, (uint)colors.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

        }


       

        /// <summary>
        /// 初始化VAO、EBO、Shader. Deprecated;
        /// </summary>
        /// <param name="gl"></param>
        public void Initialize(OpenGL gl)
        {
            InitShader(gl, out this.shader);
           
            InitElementArrayBufferObject(gl, out this.ebo, out this.primitiveMode, out this.indexArrayElementCount);

            InitVertexArrayBufferObject(gl, out this.vao);
            this.primitiveMode = OpenGL.GL_TRIANGLE_STRIP;
            this.isInitialized = true;
            this.method = 1;
        }

        public void Initialize(OpenGL gl, TriangleMesh mesh)
        {
            gl.MakeCurrent();
            this.shader = this.CreateShaderProgram(gl);
            this.InitVertexes(gl, mesh.Vertexes, mesh.VertexColors, mesh.Visibles);
            this.InitTrianglesBuffer(gl, mesh.StripTriangles);
            this.primitiveMode = OpenGL.GL_TRIANGLE_STRIP;
            this.isInitialized = true;
            this.method = 2;
        }

        

        /// <summary>
        /// 创建顶点数组缓存对象（VAO）。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="vao">必须设置VAO</param>
        /// <returns></returns>
        protected abstract void InitVertexArrayBufferObject(OpenGL gl, out uint[] vao);

        /// <summary>
        /// 创建索引数组缓存对象。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="ebo">必须设置EBO</param>
        /// <param name="mode">必须指定图元格式。可以是OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON</param>
        /// <param name="indexArrayElementCount">索引元素的数目。Count of elements in index buffer array.</param>
        /// <returns></returns>
        protected abstract void InitElementArrayBufferObject(OpenGL gl, out uint[] ebo, out uint mode, out int indexArrayElementCount);

        /// <summary>
        /// 创建Shader。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="shader">必须设置shader</param>
        /// <returns></returns>
        protected abstract void InitShader(OpenGL gl, out ShaderProgram shader);


        /// <summary>
        /// 创建Shader,返回OpenGL Shader对象,如果失败抛出异常
        /// </summary>
        /// <param name="gl"></param>
        /// <returns></returns>
        protected abstract ShaderProgram CreateShaderProgram(OpenGL gl);

        /// <summary>
        /// 在IRenderable.Render()即将执行时会调用此方法。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        protected abstract void BeforeRendering(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode);

        /// <summary>
        /// 在IRenderable.Render()执行完毕时会调用此方法。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        protected abstract void AfterRendering(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode);

        #region IRenderable 成员

        /// <summary>
        /// 渲染此元素。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        public virtual void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
            if (!this.isInitialized)
            {
                this.Initialize(gl);
                this.isInitialized = true;
            }

            BeforeRendering(gl, renderMode);

            if(this.method == 1)
              this.RenderZhuwei(gl, renderMode);
            if(this.method == 2)
              this.RenderWithStruct(gl, renderMode);

            AfterRendering(gl, renderMode);
        }

        public void RenderZhuwei(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
            //if (!this.IsRenderable())
            //return;

            ShaderProgram shader = this.shader;// GetShader(gl, renderMode);
            shader.Bind(gl);

            // 用VAO+EBO进行渲染。
            //  Bind the out vertex array.
            gl.BindVertexArray(vao[0]);
           
            //  Draw the square.
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ebo[0]);
           

            // 启用Primitive restart
            gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
            gl.PrimitiveRestartIndex(uint.MaxValue);// 截断图元（四边形带、三角形带等）的索引值。
            gl.DrawElements(this.primitiveMode, this.indexArrayElementCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

            //  Unbind our vertex array and shader.
            gl.BindVertexArray(0);
            gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

            shader.Unbind(gl);
            

        }

        public void RenderWithStruct(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
 
            //if (!this.IsRenderable())
            //return;

            ShaderProgram shader = this.shader; //GetShader(gl, renderMode);
            shader.Bind(gl);

            // 用VAO+EBO进行渲染。
            //  Bind the out vertex array.
            //gl.BindVertexArray(vao[0]);
            gl.BindVertexArray(vertexArrayObject);
            //  Draw the square.
            //gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ebo[0]);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.triangleBufferObject);

            // 启用Primitive restart
            gl.Enable(OpenGL.GL_PRIMITIVE_RESTART);
            gl.PrimitiveRestartIndex(uint.MaxValue);// 截断图元（四边形带、三角形带等）的索引值。
            gl.DrawElements(this.primitiveMode, this.triangleIndexCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

            //  Unbind our vertex array and shader.
            gl.BindVertexArray(0);
            gl.Disable(OpenGL.GL_PRIMITIVE_RESTART);

            shader.Unbind(gl);

        }

        ///// <summary>
        ///// 获取即将用于<see cref="IRenderable.Render"/>的Shader。可在此处更新Shader。
        ///// </summary>
        ///// <param name="gl"></param>
        ///// <param name="renderMode"></param>
        ///// <returns></returns>
        //protected abstract ShaderProgram GetShader(OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode);

        #endregion

    }
}
