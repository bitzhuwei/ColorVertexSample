using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;

namespace ColorVertexSample
{
    class CameraRotation
    {
        private SharpGL.SceneGraph.Cameras.LookAtCamera lookAtCamera;
        private Point downPosition;
        private Size bound;
        private bool isDown = false;
        private float horizontalRotation = 4;
        private float verticalRotation = 4;
        private SharpGL.SceneGraph.Vertex up;
        private SharpGL.SceneGraph.Vertex back;
        private SharpGL.SceneGraph.Vertex right;

        public CameraRotation(SharpGL.SceneGraph.Cameras.LookAtCamera lookAtCamera)
        {
            // TODO: Complete member initialization
            this.lookAtCamera = lookAtCamera;
            this.back = lookAtCamera.Position - lookAtCamera.Target;
            this.back.Normalize();
            this.up = lookAtCamera.UpVector;
            this.right = this.up.VectorProduct(this.back);
            this.right.Normalize();
            this.up = this.back.VectorProduct(this.right);
            this.up.Normalize();
        }
        public void MouseUp(int x, int y)
        {
            this.isDown = false;
        }

        public void MouseMove(int x, int y)
        {
            if (this.isDown)
            {
                {
                    var deltaX = -horizontalRotation * (x - downPosition.X) / this.bound.Width;
                    var cos = (float)Math.Cos(deltaX);
                    var sin = (float)Math.Sin(deltaX);
                    var newBack = new Vertex(
                        back.X * cos + right.X * sin,
                        back.Y * cos + right.Y * sin,
                        back.Z * cos + right.Z * sin);
                    this.back = newBack;
                    this.right = this.up.VectorProduct(this.back);
                    this.back.Normalize();
                    this.right.Normalize();
                }
                {
                    var deltaY = verticalRotation * (y - downPosition.Y) / this.bound.Height;
                    var cos = (float)Math.Cos(deltaY);
                    var sin = (float)Math.Sin(deltaY);
                    var newBack = new Vertex(
                        back.X * cos + up.X * sin,
                        back.Y * cos + up.Y * sin,
                        back.Z * cos + up.Z * sin);
                    this.back = newBack;
                    this.up = this.back.VectorProduct(this.right);
                    this.back.Normalize();
                    this.up.Normalize();
                }

                this.downPosition = new Point(x, y);

                this.lookAtCamera.Position = this.lookAtCamera.Target +
                    this.back * (float)((this.lookAtCamera.Position - this.lookAtCamera.Target).Magnitude());
                this.lookAtCamera.UpVector = this.up;
            }
        }

        public void MouseDown(int x, int y)
        {
            this.downPosition = new Point(x, y);
            this.isDown = true;
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
