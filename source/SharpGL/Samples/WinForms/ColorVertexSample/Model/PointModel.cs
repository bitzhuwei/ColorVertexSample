using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Cameras;

namespace ColorVertexSample.Model
{
    /// <summary>
    /// Demo class showing model as points.
    /// </summary>
    public class PointModel : PointerScientificModel
    {
        //public Rect3D Bounds { get; set; }

        ///// <summary>
        ///// Get model's center position in world coordinate.
        ///// </summary>
        ///// <returns></returns>
        //public Vertex WorldCoordCenter()
        //{
        //    Vertex result = this.Bounds.location
        //        + (Vertex)this.Bounds.size3D / 2
        //        + base.Translate;
        //    return result;
        //}

        protected PointModel(int pointCount, SharpGL.Enumerations.BeginMode mode)
            : base(pointCount, mode)
        { }

        public static PointModel Create(int nx, int ny, int nz, float radius, float minValue, float maxValue)
        {
            int pointCount = nx * ny * nz;
            PointModel model = new PointModel(pointCount, SharpGL.Enumerations.BeginMode.Points);
            PointModelHelper.Build(model, nx, ny, nz, radius, minValue, maxValue);

            return model;
        }

        public override void AdjustCamera(SharpGL.OpenGL gl,  SharpGL.SceneGraph.Cameras.Camera camera)
        {
            //Rect3D rect3D = this.Bounds;
            //Vertex center = this.WorldCoordCenter();
            float x, y, z;
            this.BoundingBox.GetBoundDimensions(out x, out y, out z);
            Vertex center = this.BoundingBox.GetCenter();

            float size = Math.Max(Math.Max(x, y), z);

            Vertex position = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 2);
            //Vertex PositionNear = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 0.5f);
            LookAtCamera lookAtCamera = camera as LookAtCamera;
            if(lookAtCamera==null)
            { throw new ArgumentNullException("camera", "camera is not LookAtCamera."); }

            int[] viewport = new int[4];
            gl.GetInteger(SharpGL.Enumerations.GetTarget.Viewport, viewport);
            int width = viewport[2]; int height = viewport[3];
            lookAtCamera.Position = position;
            lookAtCamera.Target = center;
            lookAtCamera.UpVector = new Vertex(0f, 1f, 0f);
            lookAtCamera.FieldOfView = 60;
            lookAtCamera.AspectRatio = (double)width / (double)height;
            lookAtCamera.Near = 0.001;
            lookAtCamera.Far = float.MaxValue;
        }
    }
    //public class PointModel : IDisposable
    //{
    //    private bool _disposed = false;

    //    /// <summary>
    //    /// 中心点数组
    //    /// </summary>
    //    private IntPtr _positionHeader = IntPtr.Zero;

    //    private IntPtr _colorHeader = IntPtr.Zero;

    //    public Vertex translateVector;

    //    public Rect3D Bounds { get; set; }

    //    public PointModel(int pointCount)
    //    {
    //        if (pointCount <= 0)
    //            throw new ArgumentException("size can not less equal to zero");
    //        unsafe
    //        {
    //            long bytes = sizeof(Vertex) * (pointCount);
    //            if (bytes >= int.MaxValue)
    //                throw new ArgumentException("size exceed");

    //            IntPtr ptrBytes = new IntPtr(bytes);
    //            _positionHeader = Marshal.AllocHGlobal(ptrBytes);
    //        }
    //        unsafe
    //        {
    //            long colorBytes = sizeof(Color) * pointCount;
    //            IntPtr ptrColors = new IntPtr(colorBytes);
    //            this._colorHeader = Marshal.AllocHGlobal(ptrColors);
    //        }
    //        this.PointCount = pointCount;
    //    }

    //    public int PointCount { get; protected set; }

    //    public unsafe Vertex* Positions
    //    {
    //        get
    //        {
    //            Vertex* positions = (Vertex*)this._positionHeader;
    //            return positions;
    //        }
    //    }

    //    public unsafe Color* Colors
    //    {
    //        get
    //        {
    //            Color* colors = (Color*)this._colorHeader;
    //            return colors;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        this.Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this._disposed)
    //        {

    //            if (this._positionHeader != IntPtr.Zero)
    //            {
    //                Marshal.FreeHGlobal(this._positionHeader);
    //                this._positionHeader = IntPtr.Zero;
    //            }

    //            if (this._colorHeader != IntPtr.Zero)
    //            {
    //                Marshal.FreeHGlobal(this._colorHeader);
    //                this._colorHeader = IntPtr.Zero;
    //            }

    //            this._disposed = true;
    //        }
    //    }

    //    ~PointModel()
    //    {
    //        Dispose(false);
    //    }

    //    public Vertex WorldCoordCenter()
    //    {
    //        Vertex result = this.Bounds.location 
    //            + (Vertex)this.Bounds.size3D / 2 
    //            + this.translateVector;
    //        return result;
    //    }
    //}
}
