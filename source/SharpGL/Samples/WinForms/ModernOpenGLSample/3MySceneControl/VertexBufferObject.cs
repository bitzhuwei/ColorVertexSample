using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernOpenGLSample._3MySceneControl
{
    /// <summary>
    /// Very useful reference for management of VBOs and VBAs:
    /// http://stackoverflow.com/questions/8704801/glvertexattribpointer-clarification 
    /// </summary>
    public class VertexBufferObject
    {
        public void Create(OpenGL gl, uint target, uint usagePattern)
        {
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            vertexBufferObjectId = ids[0];
            this.target = target;
            this.usagePattern = usagePattern;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="target"></param>
        /// <param name="attributeIndex"></param>
        /// <param name="rawData"></param>
        /// <param name="isNormalised"></param>
        /// <param name="stride"></param>
        public void SetData(OpenGL gl, uint attributeIndex, float[] rawData, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            gl.BufferData(target,//OpenGL.GL_ARRAY_BUFFER, 
                rawData, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="attributeIndex"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        public void SetData(OpenGL gl, uint attributeIndex, int size, IntPtr data, bool isNormalised, int stride)
        {
            gl.BufferData(this.target, size, data, this.usagePattern);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);
        }

        public void Bind(OpenGL gl)
        {
            gl.BindBuffer(target, vertexBufferObjectId);
        }

        public void Unbind(OpenGL gl)
        {
            gl.BindBuffer(target, 0);
        }

        public bool IsCreated() { return vertexBufferObjectId != 0; }

        /// <summary>
        /// Gets the vertex buffer object.
        /// </summary>
        public uint VertexBufferObjectId
        {
            get { return vertexBufferObjectId; }
        }

        private uint vertexBufferObjectId;
        private uint target;
        private uint usagePattern;

        internal void Allocate(int p)
        {
            throw new NotImplementedException();
        }
    }
}
