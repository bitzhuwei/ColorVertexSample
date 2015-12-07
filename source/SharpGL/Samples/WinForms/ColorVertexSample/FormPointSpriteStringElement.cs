using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
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
    public partial class FormPointSpriteStringElement : Form
    {
        public FormPointSpriteStringElement()
        {
            InitializeComponent();

            this.Load += FormPointSpriteStringElement_Load;
            this.openGLControl.MouseWheel += openGLControl_MouseWheel;
        }

        void openGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            this.camera.Scale(e.Delta);
        }

        private SatelliteRotation rotator;
        private SharpGL.SceneComponent.ScientificCamera camera;
        private PointSpriteStringElement fontElement;

        private void FormPointSpriteStringElement_Load(object sender, EventArgs e)
        {
            this.camera = new ScientificCamera(CameraTypes.Perspecitive);
            this.rotator = new SatelliteRotation(this.camera);

            //this.fontElement = new PointSpriteStringElement(this.camera, "A", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AAAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AAAAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AAAAAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AAAAAAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "AAAAAAAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            this.fontElement = new PointSpriteStringElement(
                this.camera, "AAAAAAAAAAAAAAAA", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "hi text!", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            //this.fontElement = new PointSpriteStringElement(this.camera, "qwertyuiop[]", new SharpGL.SceneGraph.Vertex(0, 0, 0));
            this.fontElement.Initialize(this.openGLControl.OpenGL);
        }

        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            if (this.fontElement != null)
            {
                this.fontElement.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
            }

            DrawPyramide(gl);

        }

        private void DrawPyramide(OpenGL gl)
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
            gl.ClearColor(0x87 / 255.0f, 0xce / 255.0f, 0xeb / 255.0f, 0xff / 255.0f);
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
