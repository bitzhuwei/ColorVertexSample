using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.IndexVAOElementBase
{
    /// <summary>
    /// 用Shader+VAO+index array buffer object进行渲染的元素。
    /// </summary>
    public abstract class IndexedVAOElement : SharpGL.SceneGraph.Core.SceneElement, SharpGL.SceneGraph.Core.IRenderable
    {
        /// <summary>
        /// vertex array buffer object.
        /// </summary>
        uint[] vao = new uint[1];

        /// <summary>
        /// element array buffer object.
        /// </summary>
        uint[] ebo = new uint[1];

        /// <summary>
        /// 索引元素的数目。
        /// Count of elements in index buffer array.
        /// </summary>
        int indexArrayElementCount;

        ShaderProgram shader;

        protected bool isInitialized = false;

        /// <summary>
        /// 初始化VAO、EBO、Shader。
        /// </summary>
        /// <param name="gl"></param>
        public void Initialize(OpenGL gl)
        {
            this.shader = InitShader(gl);

            this.ebo = InitElementArrayBufferObject(gl);

            this.vao = InitVertexArrayBufferObject(gl);

            this.isInitialized = true;
        }

        /// <summary>
        /// 创建顶点数组缓存对象（VAO）。
        /// </summary>
        /// <param name="gl"></param>
        /// <returns></returns>
        protected abstract uint[] InitVertexArrayBufferObject(OpenGL gl);

        /// <summary>
        /// 创建索引数组缓存对象。
        /// </summary>
        /// <param name="gl"></param>
        /// <returns></returns>
        protected abstract uint[] InitElementArrayBufferObject(OpenGL gl);

        /// <summary>
        /// 创建Shader。
        /// </summary>
        /// <param name="gl"></param>
        /// <returns></returns>
        protected abstract ShaderProgram InitShader(OpenGL gl);

        #region IRenderable 成员

        /// <summary>
        /// 渲染此元素。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        public abstract void Render(SharpGL.OpenGL gl, SharpGL.SceneGraph.Core.RenderMode renderMode);

        #endregion

    }
}
