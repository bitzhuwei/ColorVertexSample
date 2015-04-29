using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// rotate and translate camera on a sphere, whose center is camera's Target.
    /// </summary>
    public class CameraRotation
    {
        private SharpGL.SceneGraph.Cameras.LookAtCamera lookAtCamera;

        public SharpGL.SceneGraph.Cameras.LookAtCamera LookAtCamera
        {
            get { return lookAtCamera; }
            set
            {
                lookAtCamera = value;
                if (value != null)
                {
                    this.back = lookAtCamera.Position - lookAtCamera.Target;
                    this.back.Normalize();
                    this.up = lookAtCamera.UpVector;
                    this.right = this.up.VectorProduct(this.back);
                    this.right.Normalize();
                    this.up = this.back.VectorProduct(this.right);
                    this.up.Normalize();
                }
            }
        }     
        
        private Point downPosition;
        private Size bound;
        public bool mouseDownFlag = false;
        private float horizontalRotationFactor = 4;
        private float verticalRotationFactor = 4;
        private SharpGL.SceneGraph.Vertex up;
        private SharpGL.SceneGraph.Vertex back;
        private SharpGL.SceneGraph.Vertex right;

        public CameraRotation(SharpGL.SceneGraph.Cameras.LookAtCamera lookAtCamera = null)
        {
            this.LookAtCamera = lookAtCamera;
        }

        public void MouseUp(int x, int y)
        {
            this.mouseDownFlag = false;
        }

        public void MouseMove(int x, int y)
        {
            if (this.mouseDownFlag)
            {
                var camera = this.LookAtCamera;
                if (camera == null) { return; }

                var back = this.back;
                var right = this.right;
                var up = this.up;
                var bound = this.bound;
                var downPosition = this.downPosition;
                {
                    var deltaX = -horizontalRotationFactor * (x - downPosition.X) / bound.Width;
                    var cos = (float)Math.Cos(deltaX);
                    var sin = (float)Math.Sin(deltaX);
                    var newBack = new Vertex(
                        back.X * cos + right.X * sin,
                        back.Y * cos + right.Y * sin,
                        back.Z * cos + right.Z * sin);
                    back = newBack;
                    right = up.VectorProduct(back);
                    back.Normalize();
                    right.Normalize();
                }
                {
                    var deltaY = verticalRotationFactor * (y - downPosition.Y) / bound.Height;
                    var cos = (float)Math.Cos(deltaY);
                    var sin = (float)Math.Sin(deltaY);
                    var newBack = new Vertex(
                        back.X * cos + up.X * sin,
                        back.Y * cos + up.Y * sin,
                        back.Z * cos + up.Z * sin);
                    back = newBack;
                    up = back.VectorProduct(right);
                    back.Normalize();
                    up.Normalize();
                }

                camera.Position = camera.Target +
                    back * (float)((camera.Position - camera.Target).Magnitude());
                camera.UpVector = up;
                this.back = back;
                this.right = right;
                this.up = up;
                this.downPosition = new Point(x, y);
            }
        }

        public void MouseDown(int x, int y)
        {
            this.downPosition = new Point(x, y);
            this.mouseDownFlag = true;
        }

        public void SetBounds(int width, int height)
        {
            this.bound = new Size(width, height);
        }

        public override string ToString()
        {
            return string.Format("back:{0}|{3:0.00},up:{1}|{4:0.00},right:{2}|{5:0.00}",
                FormatVertex(back), FormatVertex(up), FormatVertex(right), back.Magnitude(), up.Magnitude(), right.Magnitude());
            //return base.ToString();
        }

        private string FormatVertex(Vertex v)
        {
            return string.Format("{0:0.00},{1:0.00},{2:0.00}",
                v.X, v.Y, v.Z);
        }
    }
}
