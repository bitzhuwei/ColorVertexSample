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
    /// show bar in front of everything at fixed position with width changes according to window's width.
    /// </summary>
    public class OrthoColorIndicatorBarEffect : Effect
    {
        private LookAtCamera _camera;

        public LookAtCamera Camera
        {
            get { return _camera; }
            set
            {
                _camera = value;
            }
        }

        /// <summary>
        /// if null, please set Camera property later.
        /// </summary>
        /// <param name="camera"></param>
        public OrthoColorIndicatorBarEffect(LookAtCamera camera = null)
        {
            //this.Camera = camera;
        }

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
            var margin = colorTemplate.Margin;
            var targetOrthoBarWidth = width - colorTemplate.Margin.Left - colorTemplate.Margin.Right;
            if (targetOrthoBarWidth <= 0) { targetOrthoBarWidth = double.Epsilon; }
            var scaledWidth = width * colorTemplate.Width / targetOrthoBarWidth;
            var scaledLeft = -colorTemplate.Margin.Left * colorTemplate.Width / targetOrthoBarWidth;
            var scaledRight = scaledLeft + scaledWidth;
            if (scaledLeft >= scaledRight) { scaledRight = scaledLeft + double.Epsilon; }
            gl.Ortho(scaledLeft, scaledRight,
                -colorTemplate.Margin.Bottom, height - colorTemplate.Margin.Bottom,
                zNear, zFar);

            LookAtCamera camera = null;// this.Camera;
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
        }

        public override void Pop(OpenGL gl, SceneElement parentElement)
        {
            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.PopMatrix();

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Modelview);
            gl.PopMatrix();
        }

        public ColorTemplate colorTemplate { get; set; }
    }
}
