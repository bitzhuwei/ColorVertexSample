using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    /// <summary>
    /// 创建、更新或删除指定的vertex buffer object。
    /// </summary>
    public interface IVertexBuffers
    {
        ///// <summary>
        ///// 创建指定<paramref name="key"/>的vertex buffer object，此时并不赋值。
        ///// </summary>
        ///// <param name="key">为即将创建的VBO赋予一个名字。</param>
        ///// <param name="target">OpenGL.GL_ARRAY_BUFFER or OpenGL.GL_ELEMENT_ARRAY_BUFFER</param>
        //void SetupVertexBuffer(string key, uint target);

        /// <summary>
        /// 创建指定<paramref name="key"/>的vertex buffer object并赋值。
        /// </summary>
        /// <param name="key">为即将创建的VBO赋予一个名字，在更新或删除VBO时用于识别此VBO。</param>
        /// <param name="target">GL_ARRAY_BUFFER, GL_COPY_READ_BUFFER, GL_COPY_WRITE_BUFFER, GL_ELEMENT_ARRAY_BUFFER, GL_PIXEL_PACK_BUFFER, GL_PIXEL_UNPACK_BUFFER, GL_TEXTURE_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, or GL_UNIFORM_BUFFER</param>
        /// <param name="newValues"></param>
        /// <param name="usage">GL_STREAM_DRAW, GL_STREAM_READ, GL_STREAM_COPY, GL_STATIC_DRAW, GL_STATIC_READ, GL_STATIC_COPY, GL_DYNAMIC_DRAW, GL_DYNAMIC_READ, or GL_DYNAMIC_COPY</param>
        void SetupVertexBuffer<T>(string key, uint target, UnmanagedArray<T> values, uint usage) where T : struct;

        /// <summary>
        /// 更新指定<paramref name="key"/>的vertex buffer object。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">根据指定的名字找到要更新的VBO。</param>
        /// <param name="newValues"></param>
        void UpdateVertexBuffer<T>(string key, UnmanagedArray<T> newValues) where T : struct;

        /// <summary>
        /// 更新指定<paramref name="key"/>的vertex buffer object。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">根据指定的名字找到要更新的VBO。</param>
        /// <param name="newValues"></param>
        /// <param name="startIndex"></param>
        void UpdateVertexBuffer<T>(string key, UnmanagedArray<T> newValues, int startIndex) where T : struct;

        /// <summary>
        /// 从opengl中删除指定<paramref name="key"/>的vertex buffer object。
        /// </summary>
        /// <param name="key">根据指定的名字找到要更新的VBO。</param>
        void DeleteVertexBuffer(string key);
    }
}
