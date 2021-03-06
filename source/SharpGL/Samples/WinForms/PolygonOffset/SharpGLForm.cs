﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace PolygonOffset
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        SatelliteRotation satelliteRotation = new SatelliteRotation();
        ScientificCamera camera = new ScientificCamera();

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();

            ScientificCamera camera = this.camera;

            this.satelliteRotation.Camera = camera;

            camera.Position = new SharpGL.SceneGraph.Vertex(-2, 2, -2);
            camera.Target = new SharpGL.SceneGraph.Vertex();
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);

            this.openGLControl_Resized(this.openGLControl, EventArgs.Empty);

            this.openGLControl.MouseDown += openGLControl_MouseDown;
            this.openGLControl.MouseMove += openGLControl_MouseMove;
            this.openGLControl.MouseUp += openGLControl_MouseUp;
            this.openGLControl.MouseWheel += openGLControl_MouseWheel;

            this.openGLControl.KeyDown += openGLControl_KeyDown;
        }

        void openGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                offset = !offset;
            }
        }

        void openGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            this.camera.Scale(e.Delta);
            this.openGLControl_Resized(sender, e);
        }

        void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.satelliteRotation.MouseUp(e.X, e.Y);
        }

        void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            this.satelliteRotation.MouseMove(e.X, e.Y);
            if (this.satelliteRotation.mouseDownFlag)
            {
                this.openGLControl_Resized(sender, e);
            }
        }

        void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.satelliteRotation.SetBounds(this.openGLControl.Width, this.openGLControl.Height);
            this.satelliteRotation.MouseDown(e.X, e.Y);
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

            //DrawPyramid(gl);

            DrawPolygonAndBox(gl);

        }

        private void DrawPolygonAndBox(OpenGL gl)
        {
            bool offset = this.offset;

            if (offset)
            {
                gl.Enable(OpenGL.GL_POLYGON_OFFSET_FILL);
                gl.PolygonOffset(1.0f, 1.0f);
            }

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
            drawBox(gl);

            if (offset)
                gl.Disable(OpenGL.GL_POLYGON_OFFSET_FILL);

            gl.Color(0.0f, 1.0f, 0.0f);
            gl.PushMatrix();
            gl.Translate(-0.25f, -0.25f, 0.5f);
            gl.Scale(0.5f, 0.5f, 0.5f);
            drawPolygon(gl);
            gl.PopMatrix();

            gl.Color(1.0f, 1.0f, 0.0f);
            gl.PushMatrix();
            gl.Translate(0.25f, 0.25f, 0.5f);
            gl.Scale(0.5f, 0.5f, 0.5f);
            drawPolygon(gl);
            gl.PopMatrix();

            if (offset)
            {
                gl.Enable(OpenGL.GL_POLYGON_OFFSET_FILL);
                gl.PolygonOffset(-1.0f, -1.0f);
            }

            gl.Color(0.0f, 0.0f, 1.0f);
            gl.PushMatrix();
            gl.Translate(0.0f, 0.0f, 0.5f);
            gl.Scale(0.5f, 0.5f, 0.5f);
            drawPolygon(gl);
            gl.PopMatrix();

            if (offset)
                gl.Disable(OpenGL.GL_POLYGON_OFFSET_FILL);

            if (offset)
            {
                gl.Enable(OpenGL.GL_POLYGON_OFFSET_LINE);
                gl.PolygonOffset(-1.0f, -1.0f);
            }

            gl.Color(0.0f, 0.0f, 0.0f);
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
            drawBox(gl);

            if (offset)
                gl.Disable(OpenGL.GL_POLYGON_OFFSET_LINE);
        }

        void drawBox(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_QUADS);
            // FRONT
            gl.Vertex(-0.5f, -0.5f, 0.5f);
            gl.Vertex(0.5f, -0.5f, 0.5f);
            gl.Vertex(0.5f, 0.5f, 0.5f);
            gl.Vertex(-0.5f, 0.5f, 0.5f);
            // BACK
            gl.Vertex(-0.5f, -0.5f, -0.5f);
            gl.Vertex(-0.5f, 0.5f, -0.5f);
            gl.Vertex(0.5f, 0.5f, -0.5f);
            gl.Vertex(0.5f, -0.5f, -0.5f);
            // LEFT
            gl.Vertex(-0.5f, -0.5f, 0.5f);
            gl.Vertex(-0.5f, 0.5f, 0.5f);
            gl.Vertex(-0.5f, 0.5f, -0.5f);
            gl.Vertex(-0.5f, -0.5f, -0.5f);
            // RIGHT
            gl.Vertex(0.5f, -0.5f, -0.5f);
            gl.Vertex(0.5f, 0.5f, -0.5f);
            gl.Vertex(0.5f, 0.5f, 0.5f);
            gl.Vertex(0.5f, -0.5f, 0.5f);
            // TOP
            gl.Vertex(-0.5f, 0.5f, 0.5f);
            gl.Vertex(0.5f, 0.5f, 0.5f);
            gl.Vertex(0.5f, 0.5f, -0.5f);
            gl.Vertex(-0.5f, 0.5f, -0.5f);
            // BOTTOM
            gl.Vertex(-0.5f, -0.5f, 0.5f);
            gl.Vertex(-0.5f, -0.5f, -0.5f);
            gl.Vertex(0.5f, -0.5f, -0.5f);
            gl.Vertex(0.5f, -0.5f, 0.5f);
            gl.End();
        }

        void drawPolygon(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(-0.5f, -0.5f, 0.0f);
            gl.Vertex(0.5f, -0.5f, 0.0f);
            gl.Vertex(0.5f, 0.5f, 0.0f);
            gl.Vertex(-0.5f, 0.5f, 0.0f);
            gl.End();
        }

        private void DrawPyramid(OpenGL gl)
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

            //  Nudge the rotation.
            //rotation += 3.0f;
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
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            ScientificCamera camera = this.camera;
            camera.AspectRatio = (double)Width / (double)Height;
            camera.Far = 100;
            camera.Near = 0.01;
            camera.FieldOfView = 60;

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            camera.Project(gl);

        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 180.0f;

        /// <summary>
        /// determins whether enalbe or disable polygon offset.
        /// </summary>
        private bool offset = true;
    }
}
