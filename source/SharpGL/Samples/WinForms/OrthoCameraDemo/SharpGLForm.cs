using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace OrthoCameraDemo
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();
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
            rotation += 3.0f;
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
            //gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);
            var width = this.openGLControl.Width;
            var height = this.openGLControl.Height;
            //gl.Ortho(-width / 20, width / 20, -height / 20, height / 20, -100, 100);
            gl.Ortho(-this.orthoWidth / 2, this.orthoWidth / 2,
                -this.orthoWidth / (double)width * (double)height / 2,
                this.orthoWidth / (double)width * (double)height / 2,
                this.back, this.front);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(lookAtParam.eyeX, lookAtParam.eyeY, lookAtParam.eyeZ,
                lookAtParam.centerX, lookAtParam.centerY, lookAtParam.centerZ,
                lookAtParam.upX, lookAtParam.upY, lookAtParam.upZ);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;
        private double orthoWidth;
        private double back;
        private double front;
        private LookAtParam lookAtParam = new LookAtParam() { eyeX = -5, eyeY = 5, eyeZ = -5, upY = 1 };

        private void SharpGLForm_Load(object sender, EventArgs e)
        {
            this.trackLRBT.Minimum = 1;
            this.trackLRBT.Maximum = 250;
            this.trackLRBT.Value = 25;
            this.orthoWidth = this.trackLRBT.Value;

            this.back = -100;
            this.numBack.Value = (decimal)this.back;
            this.front = 100;
            this.numFront.Value = (decimal)this.front;

            var lookAtParams = new List<LookAtParam>()
            {
                new LookAtParam(){ eyeX=-5, eyeY=5, eyeZ=-5, upY=1},
                new LookAtParam(){ eyeY=5,upZ=1},
                new LookAtParam(){  centerY=-5 ,upZ=1},
            };
            foreach (var item in lookAtParams)
            {
                this.cmbCameraPosition.Items.Add(item);
            }

            this.openGLControl_Resized(this.openGLControl, e);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            this.txtInfo.Clear();
            this.txtInfo.AppendText(string.Format("ortho width: {0}", this.orthoWidth));
            this.txtInfo.AppendText(Environment.NewLine);
            this.txtInfo.AppendText(string.Format("back: {0}", this.back));
            this.txtInfo.AppendText(Environment.NewLine);
            this.txtInfo.AppendText(string.Format("front: {0}", this.front));
            this.txtInfo.AppendText(Environment.NewLine);
        }

        private void trackLRBT_ValueChanged(object sender, EventArgs e)
        {
            this.orthoWidth = this.trackLRBT.Value;
            this.lblOrthoWidth.Text = string.Format("Ortho Width: {0}", this.orthoWidth);
            this.openGLControl_Resized(this.openGLControl, e);
            UpdateInfo();
        }

        private void numBack_ValueChanged(object sender, EventArgs e)
        {
            this.back = (double)numBack.Value;
            this.openGLControl_Resized(this.openGLControl, e);
            UpdateInfo();
        }

        private void numFront_ValueChanged(object sender, EventArgs e)
        {
            this.front = (double)numFront.Value;
            this.openGLControl_Resized(this.openGLControl, e);
            UpdateInfo();
        }

        private void cmbCameraPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lookAtParam = this.cmbCameraPosition.SelectedItem as LookAtParam;
            this.openGLControl_Resized(this.openGLControl, e);
            UpdateInfo();
        }
    }

    class LookAtParam
    {
        public double eyeX;
        public double eyeY;
        public double eyeZ;

        public double centerX;
        public double centerY;
        public double centerZ;

        public double upX;
        public double upY;
        public double upZ;

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", eyeX, eyeY, eyeZ);
            //return base.ToString();
        }
    }
}
