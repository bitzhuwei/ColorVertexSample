using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse when using orthogonal view.
    /// <para>supports arcball rotation in a moving camera</para>
    /// </summary>
    public class OrthoArcBallEffect : Effect
    {
        private LookAtCamera camera;

        public LookAtCamera Camera
        {
            get { return camera; }
            set
            {
                camera = value;
                this.arcBall.Camera = value;
            }
        }

        /// <summary>
        /// if null, please set Camera property later.
        /// </summary>
        /// <param name="camera"></param>
        public OrthoArcBallEffect(LookAtCamera camera = null)
        {
            this.Camera = camera;
            this.CenterX = 50;
            this.CenterY = 50;
        }

        protected ArcBall2 arcBall = new ArcBall2();

        public double CenterX { get; set; }
        public double CenterY { get; set; }
        private double zNear = -1000;
        private double zFar = 1000;

        public override void Push(OpenGL gl, SceneElement parentElement)
        {
            var rc = gl.RenderContextProvider;
            Debug.Assert(rc != null);

            var width = 0.0;
            var height = 0.0;

            if (rc != null)
            {
                width = rc.Width;
                height = rc.Height;
            }
            else
            {
                int[] viewport = new int[4];
                gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
                width = viewport[2];
                height = viewport[3];
            }

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Ortho(-CenterX, width - CenterX, -CenterY, height - CenterY, zNear, zFar);
            var camera = this.Camera;
            if (camera == null)
            {
                gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);
                //throw new Exception("Camera not set!");
            }
            else
            {
                var position = camera.Position - camera.Target;
                position.Normalize();
                gl.LookAt(position.X, position.Y, position.Z,
                    0, 0, 0,
                    camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            }

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PushMatrix();
            //gl.LoadIdentity();
            arcBall.TransformMatrix(gl);
        }

        public override void Pop(OpenGL gl, SceneElement parentElement)
        {
            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PopMatrix();

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PopMatrix();
        }


        public void SetBounds(int width, int height)
        {
            this.arcBall.SetBounds(width, height);
        }

        public void MouseDown(int x, int y)
        {
            this.arcBall.MouseDown(x, y);
        }

        public void MouseMove(int x, int y)
        {
            this.arcBall.MouseMove(x, y);
        }

        public void MouseUp(int x, int y)
        {
            this.arcBall.MouseUp(x, y);
        }

        public float Scale
        {
            get { return this.arcBall.Scale; }
            set { this.arcBall.Scale = value; }
        }

        public Vertex Translate
        {
            get { return this.arcBall.Translate; }
            set { this.arcBall.Translate = value; }
        }
    }
}
