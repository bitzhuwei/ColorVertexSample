using SharpGL;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernOpenGLSample._3MySceneControl
{
    ///// <summary>
    ///// OVITO中的OpenGLBuffer.
    ///// </summary>

    /// <summary>
    /// provides functions for creating and managing OpenGL buffer objects.
    /// Buffer objects are created in the OpenGL server so that the client application can avoid uploading vertices, indices, texture image data, etc every time they are needed.
    /// <see cref="QOpenGLBuffer"/> objects can be copied around as a reference to the underlying OpenGL buffer object:
    /// </summary>
    public class QOpenGLBuffer
    {
        //private int elementCount;
        //private int verticesPerElement;
        private TargetType targetType;
        private int count;
        //private VertexArrayObject vertexArrayObject;
        //private VertexBufferObject vertexBufferObject;

        /// <summary>
        /// Gets or sets the usage pattern for this buffer object. The default value is StaticDraw.
        /// <para>The settor must be called before allocate() or write().</para>
        /// </summary>
        public UsagePattern usagePattern { get; set; }
        /// <summary>
        /// Gets the vertex buffer object.
        /// </summary>
        public uint VertexBufferObject { get; protected set; }


        //public QOpenGLBuffer(int id, TargetType targetType = TargetType.VertexBuffer)
        //{
        //    this.elementCount = 0;
        //    this.verticesPerElement = 0;
        //    this.targetType = targetType;
        //}

        //public bool Create(SharpGL.OpenGL gl, UsagePattern usagePattern, int elementCount, int verticesPerElement = 1)
        //{
        //    Debug.Assert(verticesPerElement >= 1);
        //    Debug.Assert(elementCount >= 0);
        //    Debug.Assert(elementCount < int.MaxValue / Marshal.SizeOf(typeof(T)) / verticesPerElement);
        //    if(this.elementCount!=elementCount||this.verticesPerElement!=verticesPerElement)
        //    {
        //        this.elementCount = elementCount;
        //        this.verticesPerElement = verticesPerElement;
        //        if(!this.IsCreated)
        //        {
        //            try
        //            {
        //                {
        //                    ////  Create the vertex array object.
        //                    //VertexArrayObject vertexArrayObject = new VertexArrayObject();
        //                    //vertexArrayObject.Create(gl);
        //                    //vertexArrayObject.Bind(gl);

        //                    //  Create a vertex buffer for the vertex data.
        //                    VertexBufferObject vertexBufferObject = new VertexBufferObject();
        //                    vertexBufferObject.Create(gl, (uint)targetType, (uint)usagePattern);
        //                    vertexBufferObject.Bind(gl);
        //                    vertexBufferObject.Allocate(
        //                        Marshal.SizeOf(typeof(T)) * verticesPerElement * elementCount);

        //                    ////  Unbind the vertex array, we've finished specifying data for it.
        //                    //vertexBufferArray.Unbind(gl);
        //                }
        //            }
        //            catch (Exception e)
        //            {

        //                throw;
        //            }
        //        }
        //    }

        //}


        //internal void TempMethod()
        //{
        //    //http://www.cnblogs.com/freeliver54/archive/2011/05/05/2037509.html
        //    {
        //        Array src = new float[] { 1, 2, 3, 4, 5, 6 };
        //        Array dst = new float[6];
        //        System.Buffer.BlockCopy(src, 0, dst, 0, 6);
        //    }
        //    //System.Buffer.ByteLength()
        //    //System.Buffer.GetByte()
        //    //System.Buffer.SetByte()
        //}

        /// <summary>
        /// Constructs a new buffer object of <paramref name="type"/>.
        /// <para>Note: this constructor just creates the QOpenGLBuffer instance. The actual buffer object in the OpenGL server is not created until create() is called.</para>
        /// </summary>
        /// <param name="type"></param>
        public QOpenGLBuffer(TargetType type = TargetType.VertexBuffer)
        {
            this.targetType = type;
        }

        /// <summary>
        /// Constructs a shallow copy of other.
        /// <para>Note: QOpenGLBuffer does not implement copy-on-write semantics, so other will be affected whenever the copy is modified.</para>
        /// </summary>
        /// <param name="other"></param>
        public QOpenGLBuffer(QOpenGLBuffer other)
        {
            throw new NotImplementedException();
        }

        //TODO: Use IDisposable.
        /// <summary>
        /// Destroys this buffer object, including the storage being used in the OpenGL server.
        /// </summary>
        ~QOpenGLBuffer() { }

        /// <summary>
        /// Allocates count bytes of space to the buffer, initialized to the contents of data. Any previous contents will be removed.
        /// It is assumed that create() has been called on this buffer and that it has been bound to the current context.
        /// See also create(), read(), and write().
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        void allocate(OpenGL gl, IntPtr data, int count)
        {
            gl.BufferData((uint)this.targetType, count, data, (uint)this.usagePattern);
            this.count = count;
        }

        /// <summary>
        /// Allocates count bytes of space to the buffer. Any previous contents will be removed.
        /// It is assumed that create() has been called on this buffer and that it has been bound to the current context.
        /// See also create() and write().
        /// </summary>
        /// <param name="count"></param>
        void allocate(OpenGL gl, int count)
        {
            gl.BufferData((uint)this.targetType, count, IntPtr.Zero, (uint)this.usagePattern);
        }

        /// <summary>
        /// Binds the buffer associated with this object to the current OpenGL context. Returns false if binding was not possible, usually because type() is not supported on this OpenGL implementation.
        /// The buffer must be bound to the same QOpenGLContext current when create() was called, or to another QOpenGLContext that is sharing with it. Otherwise, false will be returned from this function.
        /// See also release() and create().
        /// </summary>
        /// <returns></returns>
        bool bind(OpenGL gl)
        {
            gl.BindBuffer((uint)this.targetType, this.VertexBufferObject);
            return true;
        }

        /// <summary>
        /// Returns the OpenGL identifier associated with this buffer; zero if the buffer has not been created.
        /// See also isCreated().
        /// </summary>
        /// <returns></returns>
        uint bufferId() { return this.VertexBufferObject; }

        /// <summary>
        /// Creates the buffer object in the OpenGL server. Returns true if the object was created; false otherwise.
        /// This function must be called with a current QOpenGLContext. The buffer will be bound to and can only be used in that context (or any other context that is shared with it).
        /// This function will return false if the OpenGL implementation does not support buffers, or there is no current QOpenGLContext.
        /// See also isCreated(), allocate(), write(), and destroy().
        /// </summary>
        /// <param name="gl"></param>
        /// <returns></returns>
        bool create(OpenGL gl)
        {
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            VertexBufferObject = ids[0];
            return true;
        }

        /// <summary>
        /// Destroys this buffer object, including the storage being used in the OpenGL server. All references to the buffer will become invalid.
        /// </summary>
        void destroy(OpenGL gl)
        {
            gl.DeleteBuffers(1, new uint[] { this.VertexBufferObject });
        }

        /// <summary>
        /// Returns true if this buffer has been created; false otherwise.
        /// </summary>
        /// <returns></returns>
        bool isCreated() { return this.VertexBufferObject != 0; }

        /// <summary>
        /// Maps the contents of this buffer into the application's memory space and returns a pointer to it. Returns null if memory mapping is not possible. The access parameter indicates the type of access to be performed.
        /// It is assumed that create() has been called on this buffer and that it has been bound to the current context.
        /// Note: This function is only supported under OpenGL ES 2.0 or earlier if the GL_OES_mapbuffer extension is present.
        /// Note: On OpenGL ES 3.0 and newer, or, in case if desktop OpenGL, if GL_ARB_map_buffer_range is supported, this function uses glMapBufferRange instead of glMapBuffer.
        /// See also unmap(), create(), bind(), and mapRange().
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        IntPtr map(OpenGL gl, Access access)
        {
            IntPtr result = gl.MapBuffer((uint)this.targetType, (uint)access);
            return result;
        }

        /// <summary>
        /// Maps the range specified by offset and count of the contents of this buffer into the application's memory space and returns a pointer to it. Returns null if memory mapping is not possible. The access parameter specifies a combination of access flags.
        /// It is assumed that create() has been called on this buffer and that it has been bound to the current context.
        /// Note: This function is not available on OpenGL ES 2.0 and earlier.
        /// See also unmap(), create(), and bind().
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        IntPtr mapRange(OpenGL gl, int offset, int count, RangeAccessFlags access)
        {
            IntPtr result = gl.MapBufferRange((uint)this.targetType, offset, count, (uint)access);
            return result;
        }

        /// <summary>
        /// Reads the count bytes in this buffer starting at offset into data. Returns true on success; false if reading from the buffer is not supported. Buffer reading is not supported under OpenGL/ES.
        /// It is assumed that this buffer has been bound to the current context.
        /// See also write() and bind().
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool read(OpenGL gl, int offset, IntPtr data, int count)
        {
            gl.GetBufferSubData((uint)this.targetType, offset, count, data);
            return true;
        }

        /// <summary>
        /// Releases the buffer associated with this object from the current OpenGL context.
        /// This function must be called with the same QOpenGLContext current as when bind() was called on the buffer.
        /// See also bind().
        /// </summary>
        void release(OpenGL gl)
        {
            gl.BindBuffer((uint)this.targetType, 0);
        }

        ///// <summary>
        ///// Sets the usage pattern for this buffer object to value. This function must be called before allocate() or write().
        ///// </summary>
        ///// <param name="value"></param>
        //public void setUsagePattern(UsagePattern value)
        //{
        //    this.usagePattern = value;
        //}

        /// <summary>
        /// Returns the size of the data in this buffer, for reading operations. Returns -1 if fetching the buffer size is not supported, or the buffer has not been created.
        /// It is assumed that this buffer has been bound to the current context.
        /// </summary>
        /// <returns></returns>
        int size()
        {
            if (!isCreated()) { return -1; }

            return this.count;
        }

        /// <summary>
        /// Returns the type of buffer represented by this object.
        /// </summary>
        /// <returns></returns>
        TargetType type() { return this.targetType; }

        /// <summary>
        /// Unmaps the buffer after it was mapped into the application's memory space with a previous call to map(). Returns true if the unmap succeeded; false otherwise.
        /// It is assumed that this buffer has been bound to the current context, and that it was previously mapped with map().
        /// Note: This function is only supported under OpenGL ES 2.0 and earlier if the GL_OES_mapbuffer extension is present.
        /// See also map().
        /// </summary>
        /// <returns></returns>
        bool unmap(OpenGL gl)
        {
            gl.UnmapBuffer((uint)this.targetType);
            return true;
        }

        /// <summary>
        /// Replaces the count bytes of this buffer starting at offset with the contents of data. Any other bytes in the buffer will be left unmodified.
        /// It is assumed that create() has been called on this buffer and that it has been bound to the current context.
        /// See also create(), read(), and allocate().
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="data"></param>
        /// <param name="count"></param>
        void write(OpenGL gl, int offset, IntPtr data, int count)
        {
            IntPtr originalData = this.mapRange(gl, offset, count, RangeAccessFlags.RangeWrite);
            unsafe
            {
                byte* originalBytes = (byte*)originalData;
                byte* bytes = (byte*)data;
                for (int i = 0; i < count; i++)
                {
                    originalBytes[i] = bytes[i];
                    //*(originalData + i) = *(data + i);
                    //originalData[i] = data[i];
                }
            }
            this.unmap(gl);
        }

        //static     QOpenGLBuffer operator=(OpenGLBuffer  other){throw new NotImplementedException();}

        /// <summary>
        /// Releases the buffer associated with type in the current QOpenGLContext.
        /// This function is a direct call to glBindBuffer(type, 0) for use when the caller does not know which QOpenGLBuffer has been bound to the context but wants to make sure that it is released.
        /// </summary>
        /// <param name="type"></param>
        public static void Release(OpenGL gl, TargetType type)
        {
            gl.BindBuffer((uint)type, 0);
        }

        /// <summary>
        /// This enum defines the access mode for QOpenGLBuffer.map().
        /// </summary>
        public enum Access
        {
            /// <summary>
            ///	The buffer will be mapped for reading only.
            /// </summary>
            ReadOnly = 0x88B8,

            /// <summary>
            ///	The buffer will be mapped for writing only.
            /// </summary>
            WriteOnly = 0x88B9,

            /// <summary>
            ///	The buffer will be mapped for reading and writing.
            /// </summary>
            ReadWrite = 0x88BA,
        }

        /// <summary>
        /// This enum defines the access mode bits for QOpenGLBuffer.mapRange().
        /// </summary>
        public enum RangeAccessFlag
        {
            /// <summary>
            /// The buffer will be mapped for reading.
            /// </summary>
            RangeRead = 0x0001,

            /// <summary>
            ///	The buffer will be mapped for writing.
            /// </summary>
            RangeWrite = 0x0002,

            /// <summary>
            /// Discard the previous contents of the specified range.
            /// </summary>
            RangeInvalidate = 0x0004,

            /// <summary>
            /// Discard the previous contents of the entire buffer.
            /// </summary>
            RangeInvalidateBuffer = 0x0008,

            /// <summary>
            /// Indicates that modifications are to be flushed explicitly via glFlushMappedBufferRange.
            /// </summary>
            RangeFlushExplicit = 0x0010,

            /// <summary>
            /// Indicates that pending operations should not be synchronized before returning from mapRange().
            /// </summary>
            RangeUnsynchronized = 0x0020,
        }

        /// <summary>
        /// This enum defines the access mode for QOpenGLBuffer.map().
        /// </summary>
        [Flags]
        public enum RangeAccessFlags
        {
            /// <summary>
            /// The buffer will be mapped for reading.
            /// </summary>
            RangeRead = 0x0001,

            /// <summary>
            ///	The buffer will be mapped for writing.
            /// </summary>
            RangeWrite = 0x0002,

            /// <summary>
            /// Discard the previous contents of the specified range.
            /// </summary>
            RangeInvalidate = 0x0004,

            /// <summary>
            /// Discard the previous contents of the entire buffer.
            /// </summary>
            RangeInvalidateBuffer = 0x0008,

            /// <summary>
            /// Indicates that modifications are to be flushed explicitly via glFlushMappedBufferRange.
            /// </summary>
            RangeFlushExplicit = 0x0010,

            /// <summary>
            /// Indicates that pending operations should not be synchronized before returning from mapRange().
            /// </summary>
            RangeUnsynchronized = 0x0020,
        }

        /// <summary>
        /// This enum defines the type of OpenGL buffer object to create with <see cref="QOpenGLBuffer"/>
        /// </summary>
        public enum TargetType
        {
            /// <summary>
            /// Vertex buffer object for use when specifying vertex arrays.
            /// </summary>
            VertexBuffer = 0x8892,

            /// <summary>
            /// Index buffer object for use with glDrawElements().
            /// </summary>
            IndexBuffer = 0x8893,

            /// <summary>
            /// Pixel pack buffer object for reading pixel data from the OpenGL server (for example, with glReadPixels()). Not supported under OpenGL/ES.
            /// </summary>
            PixelPackBuffer = 0x88EB,

            /// <summary>
            /// Pixel unpack buffer object for writing pixel data to the OpenGL server (for example, with glTexImage2D()). Not supported under OpenGL/ES.
            /// </summary>
            PixelUnpackBuffer = 0x88EC,
        }

        /// <summary>
        /// This enum defines the usage pattern of an QOpenGLBuffer object.
        /// </summary>
        public enum UsagePattern
        {
            /// <summary>
            ///	The data will be set once and used a few times for drawing operations. Under OpenGL/ES 1.1 this is identical to StaticDraw.
            /// </summary>
            StreamDraw = 0x88E0,

            /// <summary>
            ///	The data will be set once and used a few times for reading data back from the OpenGL server. Not supported under OpenGL/ES.
            /// </summary>
            StreamRead = 0x88E1,

            /// <summary>
            ///	The data will be set once and used a few times for reading data back from the OpenGL server for use in further drawing operations. Not supported under OpenGL/ES.
            /// </summary>
            StreamCopy = 0x88E2,

            /// <summary>
            ///	The data will be set once and used many times for drawing operations.
            /// </summary>
            StaticDraw = 0x88E4,

            /// <summary>
            ///	The data will be set once and used many times for reading data back from the OpenGL server. Not supported under OpenGL/ES.
            /// </summary>
            StaticRead = 0x88E5,

            /// <summary>
            ///	The data will be set once and used many times for reading data back from the OpenGL server for use in further drawing operations. Not supported under OpenGL/ES.
            /// </summary>
            StaticCopy = 0x88E6,

            /// <summary>
            ///	The data will be modified repeatedly and used many times for drawing operations.
            /// </summary>
            DynamicDraw = 0x88E8,

            /// <summary>
            ///	The data will be modified repeatedly and used many times for reading data back from the OpenGL server. Not supported under OpenGL/ES.
            /// </summary>
            DynamicRead = 0x88E9,

            /// <summary>
            ///	The data will be modified repeatedly and used many times for reading data back from the OpenGL server for use in further drawing operations. Not supported under OpenGL/ES.
            /// </summary>
            DynamicCopy = 0x88EA,
        }

        //public bool IsCreated { get; protected set; }
    }
}
