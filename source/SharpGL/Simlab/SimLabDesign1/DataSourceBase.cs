﻿using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    /// <summary>
    /// 所有数据源的抽象基类。
    /// </summary>
    public abstract class DataSourceBase : IVertexBuffers
    {
        /// <summary>
        /// 含有opengl上下文（gl.RenderContextProvider）的OpenGL对象。
        /// </summary>
        OpenGL gl;

        /// <summary>
        /// 用于渲染此数据源描述的模型。
        /// </summary>
        IVertexBuffers renderableElement;

        /// <summary>
        /// 记录VBO的key和VBO对象的对应关系。
        /// </summary>
        Dictionary<string, uint> vboDict = new Dictionary<string, uint>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl">含有opengl上下文（gl.RenderContextProvider）的OpenGL对象。</param>
        /// <param name="renderableElement">用于渲染此数据源描述的模型。</param>
        public DataSourceBase(OpenGL gl, IVertexBuffers renderableElement)
        {
            this.gl = gl;
            this.renderableElement = renderableElement;
        }


        void IVertexBuffers.SetupVertexBuffer(string key)
        {
            renderableElement.SetupVertexBuffer(key);
        }

        void IVertexBuffers.SetupVertexBuffer<T>(string key, SharpGL.SceneComponent.UnmanagedArray<T> newValue, int startIndex)
        {
            renderableElement.SetupVertexBuffer(key, newValue, startIndex);
        }

        void IVertexBuffers.UpdateVertexBuffer<T>(string key, SharpGL.SceneComponent.UnmanagedArray<T> newValue, int startIndex)
        {
            renderableElement.UpdateVertexBuffer(key, newValue, startIndex);
        }

        void IVertexBuffers.DeleteVertexBuffer(string key)
        {
            renderableElement.DeleteVertexBuffer(key);
        }
    }
}
