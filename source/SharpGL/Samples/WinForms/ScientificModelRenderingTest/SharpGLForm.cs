using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneComponent;

namespace ScientificModelRenderingTest
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        ScientificModel legacyModel = new ScientificModel(100, SharpGL.Enumerations.BeginMode.Lines);
        ScientificModel vertexArrayModel = new ScientificModel(100, SharpGL.Enumerations.BeginMode.Points);
        ScientificModel VBOModel = new ScientificModel(100, SharpGL.Enumerations.BeginMode.LineLoop);
        ScientificModelElement element;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();

            int radius = 1;
            legacyModel.Build(
               new SharpGL.SceneGraph.Vertex(-radius, -0, -radius),
               new SharpGL.SceneGraph.Vertex(radius, 0, radius));

            vertexArrayModel.Build(
                new SharpGL.SceneGraph.Vertex(-radius, -radius, -radius),
                new SharpGL.SceneGraph.Vertex(radius, radius, radius));

            VBOModel.Build(
                new SharpGL.SceneGraph.Vertex(-radius, -radius, -radius),
                new SharpGL.SceneGraph.Vertex(radius, radius, radius));
            ScientificCamera camera = new ScientificCamera(CameraTypes.Perspecitive);
            camera.Target = new SharpGL.SceneGraph.Vertex(0, 0, 0);
            camera.Position = new SharpGL.SceneGraph.Vertex(-2, 2, -2);
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
            IPerspectiveViewCamera perspective = camera;
            perspective.AspectRatio = (double)Width / (double)Height;
            perspective.Far = 100;
            perspective.Near = 0.01;
            perspective.FieldOfView = 60;
            this.element = new ScientificModelElement(VBOModel, camera);
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            //  Draw a coloured pyramid.
            DrawPyramid(gl);

            gl.LineWidth(3);// LINES
            this.legacyModel.RenderLegacyOpenGL(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PointSize(20);// POINTS
            this.vertexArrayModel.RenderVertexArray(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.LineWidth(1);// LINE_STRIP
            SharpGL.SceneGraph.Core.IRenderable renderable = this.element;
            renderable.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            //  Nudge the rotation.
            rotation += 3.0f;
        }

        private void DrawPyramid(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_LINE_LOOP);
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



        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
            //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);
            gl.LookAt(-2, 2, -2, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;
    }
}
