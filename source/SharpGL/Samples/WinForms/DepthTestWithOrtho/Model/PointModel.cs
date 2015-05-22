using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Cameras;

namespace DepthTestWithOrtho.Model
{
    /// <summary>
    /// Demo class showing model as points.
    /// </summary>
    public class PointModel : PointerScientificModel
    {
        protected PointModel(int pointCount, SharpGL.Enumerations.BeginMode mode)
            : base(pointCount, mode)
        { }

        public static PointModel Create(int nx, int ny, int nz, float radius, float minValue, float maxValue)
        {
            int pointCount = nx * ny * nz;
            pointCount += 3 - pointCount % 3;
            PointModel model = new PointModel(pointCount, SharpGL.Enumerations.BeginMode.Points);
            PointModelHelper.Build(model, nx, ny, nz, radius, minValue, maxValue);

            return model;
        }

        //public override void AdjustCamera(SharpGL.OpenGL gl,  SharpGL.SceneGraph.Cameras.Camera camera)
        //{
        //    float xSize, ySize, zSize;
        //    this.BoundingBox.GetBoundDimensions(out xSize, out ySize, out zSize);
        //    float x, y, z;
        //    this.BoundingBox.GetCenter(out x, out y, out z);
        //    Vertex center = new Vertex(x, y, z);

        //    float size = Math.Max(Math.Max(xSize, ySize), zSize);

        //    Vertex position = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 2);
        //    //Vertex PositionNear = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 0.5f);
        //    LookAtCamera lookAtCamera = camera as LookAtCamera;
        //    if(lookAtCamera==null)
        //    { throw new ArgumentNullException("camera", "camera is not LookAtCamera."); }

        //    int[] viewport = new int[4];
        //    gl.GetInteger(SharpGL.Enumerations.GetTarget.Viewport, viewport);
        //    int width = viewport[2]; int height = viewport[3];
        //    lookAtCamera.Position = position;
        //    lookAtCamera.Target = center;
        //    lookAtCamera.UpVector = new Vertex(0f, 1f, 0f);
        //    lookAtCamera.FieldOfView = 60;
        //    lookAtCamera.AspectRatio = (double)width / (double)height;
        //    lookAtCamera.Near = 0.001;
        //    lookAtCamera.Far = float.MaxValue;
        //}
    }
}
