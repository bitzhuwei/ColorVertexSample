using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
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

        /// <summary>
        /// 控制是否渲染网格的wireframe
        /// </summary>
        public bool RenderGridWireFrame { get; set; }
        public bool RenderGrid { get; set; }

        protected uint[] positionBuffer;
        protected uint[] colorBuffer;
        protected uint[] indexBuffer;
        protected uint[] wireframeIndexBuffer;

        protected OpenGL gl;
        protected IScientificCamera camera;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gl">OpenGLControl.OpenGL</param>
        /// <param name="camera"></param>
        public SimLabGrid(OpenGL gl, IScientificCamera camera)
        {
            this.gl = gl;
            this.camera = camera;
        }

        /// <summary>
        /// 初始化顶点位置和索引
        /// </summary>
        /// <param name="gridCoords"></param>
        public void Init(MeshGeometry3D Geomtry)
        {
            //TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;
            positionBuffer = new uint[1];
            gl.GenBuffers(1, positionBuffer);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, Geomtry.Positions.GLSize, Geomtry.Positions.Data, OpenGL.GL_STATIC_DRAW);

            indexBuffer = new uint[1];
            gl.GenBuffers(1, indexBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, Geomtry.TriangleIndices.GLSize, Geomtry.TriangleIndices.Data, OpenGL.GL_STATIC_DRAW);

            this.geometry = Geomtry;
        }


        public void SetWireframe(BufferData lineIndexes)
        {


        }


        public void SetTextureCoods(BufferData textureCoords)
        {
            ////TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            //OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;
            colorBuffer = new uint[1];
            gl.GenBuffers(1, colorBuffer);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, colorBuffer[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, textureCoords.GLSize, textureCoords.Data, OpenGL.GL_STATIC_DRAW);

            this.textureCoords = textureCoords;
        }

        public void SetTexture(Bitmap bitmap)
        {
            ////TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            //OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;

            Texture texture = new Texture();
            texture.Create(gl, bitmap);
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
        protected MeshGeometry3D geometry;
        protected BufferData textureCoords;
      
      
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
            gl.DeleteBuffers(this.positionBuffer.Length, this.positionBuffer);
            gl.DeleteBuffers(this.colorBuffer.Length, this.colorBuffer);
            gl.DeleteBuffers(this.indexBuffer.Length, this.indexBuffer);
            gl.DeleteBuffers(this.wireframeIndexBuffer.Length, this.wireframeIndexBuffer);
            //gl.DeleteVertexArrays(1, new uint[] { this.vertexArrayObject });
        }

        protected virtual void DisposeManagedResources()
        {
        } 

        #endregion

    }










}
