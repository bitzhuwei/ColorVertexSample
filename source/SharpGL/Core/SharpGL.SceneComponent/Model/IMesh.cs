using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.Model
{

    public class PointSpriteMesh : IDisposable
    {

        public Vertex3DArray PositionArray;

        public ColorFArray ColorArray;

        public FloatArray RadiusArray;

        public FloatArray VisibleArray;

        public Vertex Min;

        public Vertex Max;


        #region IDisposable Members

        /// <summary>
        /// Internal variable which checks if Dispose has already been called
        /// </summary>
        private Boolean disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(Boolean disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                //TODO: Managed cleanup code here, while managed refs still valid
            }
            //TODO: Unmanaged cleanup code here
            if (PositionArray != null)
            { PositionArray.Dispose(); }
            if (ColorArray != null)
            { ColorArray.Dispose(); }
            if (RadiusArray != null)
            { RadiusArray.Dispose(); }
            if (VisibleArray != null)
            { VisibleArray.Dispose(); }

            disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Call the private Dispose(bool) helper and indicate 
            // that we are explicitly disposing
            this.Dispose(true);

            // Tell the garbage collector that the object doesn't require any
            // cleanup when collected since Dispose was called explicitly.
            GC.SuppressFinalize(this);
        }

        #endregion


    }

    public abstract class TriangleMesh
    {
        public abstract Vertex3DArray Vertexes
        {
            get;
            set;
        }


        public abstract UIntArray StripTriangles
        {
            get;
            set;
        }

        public abstract FloatArray Visibles
        {
            get;
            set;
        }

        public abstract Vertex Min
        {
            get;
            set;
        }

        public abstract Vertex Max
        {
            get;
            set;
        }

        public abstract ColorFArray VertexColors
        {
            get;
            set;
        }

    }
}
