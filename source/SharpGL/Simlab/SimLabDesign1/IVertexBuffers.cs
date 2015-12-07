using SharpGL;
using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    /// <summary>
    /// 创建、更新指定的vertex buffer object。
    /// </summary>
    public interface IVertexBuffers
    {
        /// <summary>
        /// 添加一个指定<paramref name="varNameInShader"/>的顶点属性数组的vertex buffer object并赋值。
        /// <para>一个<see cref="IVertexBuffers"/>可以有多个属性buffer。</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varNameInShader">为即将添加的VBO赋予一个名字，在更新或删除VBO时用于识别此VBO。
        /// <para>请从HexahedronElement.key_...中选择。</para>
        /// <para>致开发者：HexahedronElement.key_...必须和vertex shader中的'in someType xxx;'中的xxx命名相同。</para>
        /// </param>
        /// <param name="target">GL_ARRAY_BUFFER, GL_COPY_READ_BUFFER, GL_COPY_WRITE_BUFFER, GL_ELEMENT_ARRAY_BUFFER, GL_PIXEL_PACK_BUFFER, GL_PIXEL_UNPACK_BUFFER, GL_TEXTURE_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, or GL_UNIFORM_BUFFER</param>
        /// <param name="newValues"></param>
        /// <param name="usage">GL_STREAM_DRAW, GL_STREAM_READ, GL_STREAM_COPY, GL_STATIC_DRAW, GL_STATIC_READ, GL_STATIC_COPY, GL_DYNAMIC_DRAW, GL_DYNAMIC_READ, or GL_DYNAMIC_COPY</param>
        /// <param name="size">T中的分量数目，是VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)里的size，常见的如UnmanagedArray&lt;Vertex&gt;时为3，UnmanagedArray&lt;float&gt;时为1，</param>
        /// <param name="type">T中的分量类型，是VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)里的type，常见的例如OpenGL.GL_FLOAT</param>
        void AddAttributeBuffer(string varNameInShader, UnmanagedArrayBase values, UsageType usage, int size, uint type);

        /// <summary>
        /// 设置指定<paramref name="key"/>的顶点索引数组的vertex buffer object并赋值。
        /// <para>一个<see cref="IVertexBuffers"/>最多有1个索引buffer。</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="indexes"></param>
        /// <param name="vertexCount"></param>
        void SetupIndexBuffer(UnmanagedArrayBase indexes,UsageType usage, int vertexCount);

        /// <summary>
        /// 更新指定<paramref name="key"/>的vertex buffer object。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">根据指定的名字找到要更新的VBO。
        /// <para>此key是在<see cref="CreateVertexBuffer()"/>中的参数key。</para></param>
        /// <param name="newValues"></param>
        void UpdateVertexBuffer(string key, UnmanagedArrayBase newValues);

        /// <summary>
        /// 更新指定<paramref name="key"/>的vertex buffer object。
        /// 仅更新其中一部分。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">根据指定的名字找到要更新的VBO。
        /// <para>此key是在<see cref="CreateVertexBuffer()"/>中的参数key。</para></param>
        /// <param name="newValues"></param>
        /// <param name="startIndex">要更新的部分在整个VBO数组中的起始位置。</param>
        void UpdateVertexBuffer(string key, UnmanagedArrayBase newValues, int startIndex);

    }

    //public enum TargetType : uint
    //{
    //    ArrayBuffer = OpenGL.GL_ARRAY_BUFFER,
    //    ElementBuffer = OpenGL.GL_ELEMENT_ARRAY_BUFFER,
    //}

    public enum UsageType : uint
    {
        StreamDraw = OpenGL.GL_STREAM_DRAW,
        StreamRead = OpenGL.GL_STREAM_READ,
        StreamCopy = OpenGL.GL_STREAM_COPY,
        StaticDraw = OpenGL.GL_STATIC_DRAW,
        StaticRead = OpenGL.GL_STATIC_READ,
        StaticCopy = OpenGL.GL_STATIC_COPY,
        DynamicDraw = OpenGL.GL_DYNAMIC_DRAW,
        DynamicRead = OpenGL.GL_DYNAMIC_READ,
        DynamicCopy = OpenGL.GL_DYNAMIC_COPY,
    }
}
