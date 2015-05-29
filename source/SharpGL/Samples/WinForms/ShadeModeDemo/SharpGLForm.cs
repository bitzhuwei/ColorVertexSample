using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;

namespace ShadeModeDemo
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        private BeginMode beginMode;
        private ShadeModel shadeMode;
        private bool rotating;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();

            foreach (var name in Enum.GetNames(typeof(BeginMode)))
            {
                var item = new ComboBoxItem<BeginMode>(
                    name, (BeginMode)Enum.Parse(typeof(BeginMode), name));
                this.cmbBeginMode.Items.Add(item);
            }
            this.cmbBeginMode.SelectedIndex = 4;

            foreach (var name in Enum.GetNames(typeof(ShadeModel)))
            {
                var item = new ComboBoxItem<ShadeModel>(
                    name, (ShadeModel)Enum.Parse(typeof(ShadeModel), name));
                this.cmbShadeMode.Items.Add(item);
            }
            this.cmbShadeMode.SelectedIndex = 1;

            {
                var rotating = new ComboBoxItem<bool>("Rotate", true);
                this.cmbRotation.Items.Add(rotating);

                var noRotating = new ComboBoxItem<bool>("No Rotate", false);
                this.cmbRotation.Items.Add(noRotating);
            }
            this.cmbRotation.SelectedIndex = 0;
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

            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            gl.ShadeModel(this.shadeMode);

            gl.Begin(this.beginMode);

            //DrawPyramid(gl);
            DrawModel(gl);

            gl.End();

            if (this.rotating)
            {
                //  Nudge the rotation.
                rotation += 3.0f;
            }
        }

        private void DrawModel(OpenGL gl)
        {
            //gl.Color(0.5f, 0.5f, 0.5f);
            gl.Color(0, 0, 0);
            gl.Vertex(0, 0);

            gl.Color(1f, 0, 0);
            gl.Vertex(0, 1);

            gl.Color(0, 1f, 0);
            gl.Vertex(1, 0);

            gl.Color(1f, 1f, 0);
            gl.Vertex(1, 1);

            gl.Color(0, 0, 1f);
            gl.Vertex(2, 0);

            gl.Color(1f, 0, 1f);
            gl.Vertex(2, 1);

            gl.Color(0, 1f, 1f);
            gl.Vertex(3, 0);

            gl.Color(1f, 1f, 1f);
            gl.Vertex(3, 1);
        }

        private static void DrawPyramid(OpenGL gl)
        {
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
            gl.LookAt(-3, 3, -3, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;

        private void SharpGLForm_Load(object sender, EventArgs e)
        {
            this.txtModelInfo.Text = @"gl.Color(0, 0, 0);
gl.Vertex(0, 0);

gl.Color(1f, 0, 0);
gl.Vertex(0, 1);

gl.Color(0, 1f, 0);
gl.Vertex(1, 0);

gl.Color(1f, 1f, 0);
gl.Vertex(1, 1);

gl.Color(0, 0, 1f);
gl.Vertex(2, 0);

gl.Color(1f, 0, 1f);
gl.Vertex(2, 1);

gl.Color(0, 1f, 1f);
gl.Vertex(3, 0);

gl.Color(1f, 1f, 1f);
gl.Vertex(3, 1);";
        }

        private void cmbBeginMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmbBeginMode.SelectedItem as ComboBoxItem<BeginMode>;
            this.beginMode = item.value;
        }

        private void cmbShadeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmbShadeMode.SelectedItem as ComboBoxItem<ShadeModel>;
            this.shadeMode = item.value;
        }

        private void cmbRotation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmbRotation.SelectedItem as ComboBoxItem<bool>;
            this.rotating = item.value;
        }
    }

    class ComboBoxItem<T>
    {
        public T value;
        public string name;

        public ComboBoxItem(string name, T value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}", name);
            //return base.ToString();
        }
    }
}
