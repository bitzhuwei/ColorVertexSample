using SharpGL;
using SimLabDesign1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1_DataSources
{
    /// <summary>
    /// 所有数据源的抽象基类。
    /// </summary>
    public abstract class DataSourceBase : IVertexBuffers
    {
        ///// <summary>
        ///// 含有opengl上下文（gl.RenderContextProvider）的OpenGL对象。
        ///// </summary>
        //OpenGL gl;

        /// <summary>
        /// 用于渲染此数据源描述的模型。
        /// </summary>
        IVertexBuffers renderableElement;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="gl">含有opengl上下文（gl.RenderContextProvider）的OpenGL对象。</param>
        ///// <param name="renderableElement">用于渲染此数据源描述的模型。</param>
        //public DataSourceBase(OpenGL gl, IVertexBuffers renderableElement)
        //{
        //    this.gl = gl;
        //    this.renderableElement = renderableElement;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderableElement">用于渲染此数据源描述的模型。</param>
        public DataSourceBase(IVertexBuffers renderableElement)
        {
            this.renderableElement = renderableElement;
        }

        PropertyBuffer IVertexBuffers.AddAttributeBuffer(string varNameInShader, SharpGL.SceneComponent.UnmanagedArrayBase values, UsageType usage, int size, uint type)
        {
            return renderableElement.AddAttributeBuffer(varNameInShader, values, usage, size, type);
        }

        IndexBuffer IVertexBuffers.SetupIndexBuffer(SharpGL.SceneComponent.UnmanagedArrayBase indexes, UsageType usage, int vertexCount)
        {
            return renderableElement.SetupIndexBuffer(indexes, usage, vertexCount);
        }

        void IVertexBuffers.UpdateVertexBuffer(string key, SharpGL.SceneComponent.UnmanagedArrayBase newValues)
        {
            renderableElement.UpdateVertexBuffer(key, newValues);
        }

        void IVertexBuffers.UpdateVertexBuffer(string key, SharpGL.SceneComponent.UnmanagedArrayBase newValues, int startIndex)
        {
            renderableElement.UpdateVertexBuffer(key, newValues, startIndex);
        }

        void IVertexBuffers.UpdateIndexBuffer(SharpGL.SceneComponent.UnmanagedArrayBase indexes)
        {
            renderableElement.UpdateIndexBuffer(indexes);
        }

        void IVertexBuffers.UpdateIndexBuffer(SharpGL.SceneComponent.UnmanagedArrayBase indexes, int startIndex)
        {
            renderableElement.UpdateIndexBuffer(indexes, startIndex);
        }
    }
}
