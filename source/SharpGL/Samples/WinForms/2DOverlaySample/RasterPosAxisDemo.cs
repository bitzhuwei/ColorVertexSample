using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;

namespace _2DOverlaySample
{
    class RasterPosAxisDemo : Effect
    {
        public LookAtCamera Camera { get; set; }
        public RasterPosAxisDemo(LookAtCamera camera)
        {
            this.arcBall.SetCamera(camera);
            this.Camera = camera;
        }

        public ArcBall2 arcBall = new ArcBall2();

        private double zNear = -100;
        private double zFar = 100;

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
            gl.Ortho(0, width, 0, height, zNear, zFar);
            gl.LookAt(Camera.Position.X, Camera.Position.Y, Camera.Position.Z,
                Camera.Target.X, Camera.Target.Y, Camera.Target.Z,
                Camera.UpVector.X, Camera.UpVector.Y, Camera.UpVector.Z);

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PushMatrix();
            arcBall.TransformMatrix(gl);
        }

        public override void Pop(OpenGL gl, SceneElement parentElement)
        {
            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PopMatrix();

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PopMatrix();
        }
    }
}
