using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpGL.SceneGraph;

namespace ColorVertexSample.Model
{
    public class PointModel : IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// 中心点数组
        /// </summary>
        private IntPtr _positionHeader = IntPtr.Zero;

        private IntPtr _colorHeader = IntPtr.Zero;

        public Vertex translateVector;

        public Rect3D Bounds { get; set; }

        public PointModel(int pointCount)
        {
            if (pointCount <= 0)
                throw new ArgumentException("size can not less equal to zero");
            unsafe
            {
                long bytes = sizeof(Vertex) * (pointCount);
                if (bytes >= int.MaxValue)
                    throw new ArgumentException("size exceed");

                IntPtr ptrBytes = new IntPtr(bytes);
                _positionHeader = Marshal.AllocHGlobal(ptrBytes);
            }
            unsafe
            {
                long colorBytes = sizeof(Color) * pointCount;
                IntPtr ptrColors = new IntPtr(colorBytes);
                this._colorHeader = Marshal.AllocHGlobal(ptrColors);
            }
            this.PointCount = pointCount;
        }

        public int PointCount { get; protected set; }

        public unsafe Vertex* Positions
        {
            get
            {
                Vertex* positions = (Vertex*)this._positionHeader;
                return positions;
            }
        }

        public unsafe Color* Colors
        {
            get
            {
                Color* colors = (Color*)this._colorHeader;
                return colors;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {

                if (this._positionHeader != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this._positionHeader);
                    this._positionHeader = IntPtr.Zero;
                }

                if (this._colorHeader != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this._colorHeader);
                    this._colorHeader = IntPtr.Zero;
                }

                this._disposed = true;
            }
        }

        ~PointModel()
        {
            Dispose(false);
        }

        public Vertex WorldCoordCenter()
        {
            var result = this.Bounds.location 
                + (Vertex)this.Bounds.size3D / 2 
                + this.translateVector;
            return result;
        }
    }
}
