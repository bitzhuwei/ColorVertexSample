using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneComponent.Utility;
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
    /// 用Shader+VAO进行渲染的元素。
    /// </summary>
    public abstract class VAOElement : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {
        /// <summary>
        /// vertex array buffer object.
        /// </summary>
        protected uint[] vao;

        /// <summary>
        /// Specifies what kind of primitives to render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted
        /// </summary>
        protected uint primitiveMode;
        protected int primitiveCount;

        protected ShaderProgram shader;

        //  Constants that specify the attribute indexes.
        public  const uint ATTRIB_INDEX_POSITION = 0;
        public  const uint ATTRIB_INDEX_COLOUR = 1;

        protected bool isInitialized = false;

        /// <summary>
        /// 初始化VAO、EBO、Shader. Deprecated;
        /// </summary>
        /// <param name="gl"></param>
        public void Initialize(OpenGL gl, TriangleMesh mesh)
        {
            InitShader(gl, out this.shader);

            InitVertexArrayBufferObject(gl, out this.primitiveMode, out this.vao, out this.primitiveCount, mesh);

            this.isInitialized = true;
        }

        /// <summary>
        /// 创建顶点数组缓存对象（VAO）。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="mode">必须指定图元格式。可以是OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON</param>
        /// <param name="vao">必须设置VAO</param>
        /// <param name="primitiveCount">元素的数目。Count of elements.</param>
        /// <returns></returns>
        protected abstract void InitVertexArrayBufferObject(OpenGL gl, out uint mode, out uint[] vao, out int primitiveCount, TriangleMesh mesh);

        /// <summary>
        /// 创建Shader。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="shader">必须设置shader</param>
        /// <returns></returns>
        protected abstract void InitShader(OpenGL gl, out ShaderProgram shader);

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

            BeforeRendering(gl, renderMode);

            ShaderProgram shader = this.shader;// GetShader(gl, renderMode);
            shader.Bind(gl);

            // 用VAO+EBO进行渲染。
            //  Bind the out vertex array.
            gl.BindVertexArray(vao[0]);

            gl.DrawArrays(this.primitiveMode, 0, this.primitiveCount);

            //  Unbind our vertex array and shader.
            gl.BindVertexArray(0);

            shader.Unbind(gl);

            AfterRendering(gl, renderMode);
        }

        #endregion

    }
}
