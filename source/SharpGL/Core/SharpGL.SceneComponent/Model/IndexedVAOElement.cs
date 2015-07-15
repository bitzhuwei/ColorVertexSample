using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected bool isInitialized = false;

        /// <summary>
        /// 初始化VAO、EBO、Shader。
        /// </summary>
        /// <param name="gl"></param>
        public void Initialize(OpenGL gl)
        {
            InitShader(gl, out this.shader);

            InitElementArrayBufferObject(gl, out this.ebo, out this.primitiveMode, out this.indexArrayElementCount);

            InitVertexArrayBufferObject(gl, out this.vao);

            this.isInitialized = true;
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

        #region IRenderable 成员

        /// <summary>
        /// 渲染此元素。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        public virtual void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode)
        {
            if(!this.isInitialized)
            {
                this.Initialize(gl);
                this.isInitialized = true;
            }

            ShaderProgram shader = GetShader(gl, renderMode);
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

        /// <summary>
        /// 获取即将用于<see cref="IRenderable.Render"/>的Shader。可在此处更新Shader。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        /// <returns></returns>
        protected abstract ShaderProgram GetShader(OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode);

        #endregion

    }
}
