using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Utility;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{


    /// <summary>
    /// 3D Visual Object
    /// </summary>
    public abstract class SimLabGrid : SceneElement, IDisposable
    {

        private bool renderGridWireframe = false;

        /// <summary>
        /// 是否渲染网格的wireframe
        /// </summary>
        public bool RenderGridWireframe
        {
            get { return renderGridWireframe; }
            set { renderGridWireframe = value; }
        }

        private bool renderGrid = true;

        /// <summary>
        /// 是否渲染网格
        /// </summary>
        public bool RenderGrid
        {
            get { return renderGrid; }
            set { renderGrid = value; }
        }

        protected uint[] positionBuffer;
        protected uint[] colorBuffer;

        protected Texture texture;

        protected OpenGL gl;
        protected IScientificCamera camera;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl">OpenGLControl.OpenGL</param>
        /// <param name="camera"></param>
        public SimLabGrid(OpenGL gl, IScientificCamera camera)
        {
            if (gl == null || camera == null) { throw new ArgumentNullException(); }

            this.gl = gl;
            this.camera = camera;
        }

        protected uint CreateVertexBufferObject(uint mode, BufferData bufferData, uint usage)
        {
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            gl.BindBuffer(mode, ids[0]);
            gl.BufferData(mode, bufferData.SizeInBytes, bufferData.Data, usage);

            return ids[0];
        }
        /// <summary>
        /// 初始化顶点位置和索引
        /// </summary>
        /// <param name="gridCoords"></param>
        protected void Init(MeshBase geometry)
        {
            ////TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            //OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;
            positionBuffer = new uint[1];
            positionBuffer[0] = CreateVertexBufferObject(OpenGL.GL_ARRAY_BUFFER, geometry.Positions, OpenGL.GL_STATIC_DRAW);

        }

        public void SetTextureCoods(BufferData textureCoords)
        {
            ////TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            //OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;
            if (colorBuffer == null)
            {
                colorBuffer = new uint[1];
                colorBuffer[0] = CreateVertexBufferObject(OpenGL.GL_ARRAY_BUFFER, textureCoords, OpenGL.GL_STREAM_DRAW);
            }
            else
            {
                UpdateTextureCoords(textureCoords);
            }
        }

        protected void UpdateTextureCoords(BufferData textureCoords)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorBuffer[0]);
            IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destVisibles, textureCoords.Data, (uint)textureCoords.SizeInBytes);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);
        }

        public void SetTexture(Bitmap bitmap)
        {
            ////TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            //OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;

            this.texture = new Texture();
            this.texture.Create(gl, bitmap);
        }



        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~SimLabGrid()
        {
            this.Dispose(false);
        }

        private bool disposedValue = false;


        private void Dispose(bool disposing)
        {

            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    DisposeManagedResources();

                } // end if

                // Dispose unmanaged resources.
                DisposeUnmanagedResources();

            }

            this.disposedValue = true;
        }

        protected virtual void DisposeUnmanagedResources()
        {
            if (this.positionBuffer != null)
            {
                gl.DeleteBuffers(this.positionBuffer.Length, this.positionBuffer);
            }

            if (this.colorBuffer != null)
            {
                gl.DeleteBuffers(this.colorBuffer.Length, this.colorBuffer);
            }

            if (this.texture != null)
            {
                this.texture.Destroy(gl);
            }

            //gl.DeleteVertexArrays(1, new uint[] { this.vertexArrayObject });
        }

        protected virtual void DisposeManagedResources()
        {
        }

        #endregion

    }










}
