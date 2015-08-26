using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YieldingGeometryModel;

namespace ColorVertexSample
{
    public partial class FormWell : Form
    {
        Well wellElement;
        ScientificCamera camera;
        SatelliteRotation rotator;

        public FormWell()
        {
            this.camera = new ScientificCamera(CameraTypes.Perspecitive);
            this.rotator = new SatelliteRotation(camera);

            InitializeComponent();

            this.openGLControl.MouseWheel += openGLControl_MouseWheel;
        }

        void openGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            this.camera.Scale(e.Delta);
        }

        private void FormWell_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();
            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            //DrawPyramid(gl);

            //  Nudge the rotation.
            rotation += 3.0f;

            this.wellElement.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
        }

        private static void DrawPyramid(OpenGL gl)
        {
            //  Draw a coloured pyramid.
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);

            List<Vertex> pipe = new List<Vertex>();
            pipe.Add(new Vertex(-1, -1, -1));
            pipe.Add(new Vertex(-1, -1, 1));
            pipe.Add(new Vertex(-1, 1, 1));
            pipe.Add(new Vertex(1, 1, 1));
            pipe.Add(new Vertex(1, -1, 1));
            pipe.Add(new Vertex(1, -1, -1));
            pipe.Add(new Vertex(-1, -1, -1));
            GLColor pipeColor = new GLColor(
                (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            this.wellElement = new Well(this.camera,
                pipe, 0.1f, pipeColor,
                "hi well", new Vertex(-1.5f, -1.5f, -1.5f), new GLColor(1, 1, 1, 1), 32, 256);
            this.wellElement.Initialize(gl);
        }

        static Random random = new Random();
        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.rotator.SetBounds(this.openGLControl.Width, this.openGLControl.Height);
            this.rotator.MouseDown(e.X, e.Y);
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.rotator.mouseDownFlag)
            {
                this.rotator.MouseMove(e.X, e.Y);
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.rotator.MouseUp(e.X, e.Y);
        }
    }
}
