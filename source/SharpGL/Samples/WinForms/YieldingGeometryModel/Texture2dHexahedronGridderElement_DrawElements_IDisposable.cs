using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class Texture2dHexahedronGridderElement_DrawElements : IDisposable
    {

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        } // end sub

        /// <summary>
        /// Destruct instance of the class.
        /// </summary>
        ~Texture2dHexahedronGridderElement_DrawElements()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Backing field to track whether Dispose has been called.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed and unmanaged resources of this instance.
        /// </summary>
        /// <param name="disposing">If disposing equals true, managed and unmanaged resources can be disposed. If disposing equals false, only unmanaged resources can be disposed. </param>
        protected virtual void Dispose(bool disposing)
        {

            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // TODO: Dispose managed resources.
                    CleanManagedRes();
                } // end if

                // TODO: Dispose unmanaged resources.
                //CleanUnmanagedRes();
            } // end if

            this.disposedValue = true;
        } // end sub

        #endregion

        protected void CleanUnmanagedRes()
        {
            OpenGL gl = new OpenGL();// this is not cool.

            var buffers = new uint[] { this.vertexsBufferObject, this.colorsBufferObject, this.visiblesBufferObject };
            gl.DeleteBuffers(buffers.Length, buffers);
            gl.DeleteTextures(1, new uint[] { this.texture.TextureName });
        }

        protected void CleanManagedRes()
        {
        }

    }
}
