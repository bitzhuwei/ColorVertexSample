using SharpGL;
using SharpGL.SceneGraph.Core;
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
    /// </summary>
    public class HexahedronElement : IVertexBuffers, IRenderable
    {
        Dictionary<string, VBOInfo> vboDict = new Dictionary<string, VBOInfo>();

        void IVertexBuffers.SetupVertexBuffer<T>(string key, uint target, SharpGL.SceneComponent.UnmanagedArray<T> values, uint usage)
        {
            if (this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] already exists!")); }

            OpenGL gl = new OpenGL();
            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            gl.BindBuffer(target, buffers[0]);
            gl.BufferData(target, values.ByteLength, values.Header, usage);

            this.vboDict.Add(key, new VBOInfo() { ID = buffers[0], Target = target, Usage = usage });
        }

        void IVertexBuffers.UpdateVertexBuffer<T>(string key, SharpGL.SceneComponent.UnmanagedArray<T> newValue, int startIndex)
        {
            if (!this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] NOT exists!")); }

            VBOInfo vbo = this.vboDict[key];
            OpenGL gl = new OpenGL();

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vbo.ID);

            //IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            IntPtr dest = gl.MapBuffer(vbo.Target, OpenGL.GL_READ_WRITE);

            //MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            newValue.CopyTo(dest);

            gl.UnmapBuffer(vbo.Target);
        }

        void IVertexBuffers.DeleteVertexBuffer(string key)
        {
            if (!this.vboDict.ContainsKey(key))
            { throw new ArgumentException(string.Format("key[{0}] NOT exists!")); }

            VBOInfo vbo = this.vboDict[key];
            OpenGL gl = new OpenGL();
            gl.DeleteBuffers(1, new uint[] { vbo.ID });
        }

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {

            throw new NotImplementedException();
        }
    }
}
