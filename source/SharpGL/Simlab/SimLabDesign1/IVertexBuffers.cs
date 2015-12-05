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
        /// <summary>
        /// 创建指定<paramref name="key"/>的vertex buffer object，此时并不赋值。
        /// </summary>
        /// <param name="key">为即将创建的VBO赋予一个名字。</param>
        void SetupVertexBuffer(string key);

        /// <summary>
        /// 创建指定<paramref name="key"/>的vertex buffer object并赋值。
        /// </summary>
        /// <param name="key">为即将创建的VBO赋予一个名字，在更新或删除VBO时用于识别此VBO。</param>
        /// <param name="newValue"></param>
        /// <param name="startIndex"></param>
        void SetupVertexBuffer<T>(string key, UnmanagedArray<T> newValue, int startIndex) where T : struct;

        /// <summary>
        /// 更新指定<paramref name="key"/>的vertex buffer object。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">根据指定的名字找到要更新的VBO。</param>
        /// <param name="newValue"></param>
        void UpdateVertexBuffer<T>(string key, UnmanagedArray<T> newValue, int startIndex) where T : struct;

        /// <summary>
        /// 从opengl中删除指定<paramref name="key"/>的vertex buffer object。
        /// </summary>
        /// <param name="key">根据指定的名字找到要更新的VBO。</param>
        void DeleteVertexBuffer(string key);
    }
}
