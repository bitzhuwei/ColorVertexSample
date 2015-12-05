﻿using GlmNet;
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
    public partial class DynamicUnStructuredGridderElement : IDisposable
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
        ~DynamicUnStructuredGridderElement()
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
                CleanUnmanagedRes();
            } // end if

            this.disposedValue = true;
        } // end sub

        #endregion

        protected void CleanUnmanagedRes()
        {
            OpenGL gl = new OpenGL();// this is not cool.

            gl.DeleteBuffers(this.fractionsPositionBufferObject.Length, this.fractionsPositionBufferObject);
            gl.DeleteBuffers(this.fractionsColorBufferObject.Length, this.fractionsColorBufferObject);
            gl.DeleteBuffers(this.tetrasPositionBufferObject.Length, this.tetrasPositionBufferObject);
            gl.DeleteBuffers(this.tetrasColorBufferObject.Length, this.tetrasColorBufferObject);
            gl.DeleteBuffers(this.tetrasIndexBufferObject.Length, this.tetrasIndexBufferObject);

            gl.DeleteVertexArrays(this.vertexArrayObject.Length, this.vertexArrayObject);
        }

        protected void CleanManagedRes()
        {
        }

    }
}