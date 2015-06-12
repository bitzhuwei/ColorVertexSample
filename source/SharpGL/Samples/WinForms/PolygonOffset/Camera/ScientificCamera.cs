using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PolygonOffset
{
    /// <summary>
    /// projects in perspective view or ortho view.
    /// </summary>
    public class ScientificCamera : SharpGL.SceneGraph.Cameras.Camera
    {
        static int count = 0;
        public override string ToString()
        {
            return string.Format("{0}/{1}", Name, count);
            //return base.ToString();
        }
        public ScientificCamera()
        {
            Name = "Scientific Camera: " + count++;
            this.FieldOfView = 60f;
            this.AspectRatio = 1f;
            this.Near = 0.01;
            this.Far = 1000;

            this.Target = new Vertex(0, 0, 0);
            this.UpVector = new Vertex(0, 1, 0);
            this.Position = new Vertex(0, 0, 0);

        }

        public override void Project(OpenGL gl)
        {
            //	Load the projection identity matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            //	Perform the projection.
            TransformProjectionMatrix(gl);

            ////	Get the matrix.
            //float[] matrix = new float[16];
            //gl.GetFloat(OpenGL.GL_PROJECTION_MATRIX, matrix);
            //for (int i = 0; i < 4; i++)
            //    for (int j = 0; j < 4; j++)
            //        projectionMatrix[i, j] = matrix[(i * 4) + j];

            //	Back to the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        /// <summary>
        /// This is the class' main function, to override this function and perform a 
        /// perspective transformation.
        /// </summary>
        public override void TransformProjectionMatrix(OpenGL gl)
        {
            //  Perform the look at transformation.
            gl.Perspective(this.FieldOfView, this.AspectRatio, this.Near, this.Far);
            gl.LookAt((double)Position.X, (double)Position.Y, (double)Position.Z,
                (double)Target.X, (double)Target.Y, (double)Target.Z,
                (double)UpVector.X, (double)UpVector.Y, (double)UpVector.Z);
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        [Description("The target of the camera (the point it's looking at)"), Category("Camera")]
        public Vertex Target { get; set; }

        /// <summary>
        /// Gets or sets up vector.
        /// </summary>
        /// <value>
        /// Up vector.
        /// </value>
        [Description("The up direction, relative to camera. (Controls tilt)."), Category("Camera")]
        public Vertex UpVector { get; set; }



        public double FieldOfView { get; set; }

        public double AspectRatio
        {
            get { return base.AspectRatio; }
            set { base.AspectRatio = value; }
        }

        public double Near { get; set; }

        public double Far { get; set; }


        public void Scale(int delta)
        {
            ScientificCamera camera = this;
            var target2Position = camera.Position - camera.Target;
            if (target2Position.Magnitude() < 0.01)
            {
                target2Position.Normalize();
                target2Position.X *= 0.01f;
                target2Position.Y *= 0.01f;
                target2Position.Z *= 0.01f;
            }
            var scaledTarget2Position = target2Position * (1 - delta * 0.001f);
            camera.Position = camera.Target + scaledTarget2Position;
            double lengthDiff = scaledTarget2Position.Magnitude() - target2Position.Magnitude();
            // Increase ortho camera's Near/Far property in case the camera's position changes too much.
            camera.Far += lengthDiff;
            //camera.Near += lengthDiff;
        }
    }
}
