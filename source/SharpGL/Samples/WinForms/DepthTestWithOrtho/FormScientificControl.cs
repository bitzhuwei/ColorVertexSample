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
using SharpGL.SceneGraph;

namespace DepthTestWithOrtho
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class FormScientificControl : Form
    {
        //CameraRotation cameraRotation = new CameraRotation();
        int verticesCount = 100000;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public FormScientificControl()
        {
            InitializeComponent();

            var camera = this.scientificControl.Scene.CurrentCamera;
            camera.Target = new SharpGL.SceneGraph.Vertex(0, 0, 0);
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
            camera.Position = new SharpGL.SceneGraph.Vertex(0, 0, 5);
            IOrthoCamera orthoCamera = camera;
            orthoCamera.Left = -10; orthoCamera.Bottom = -10; orthoCamera.Near = -10;
            orthoCamera.Right = 10; orthoCamera.Top = 10; orthoCamera.Far = 10;
            //this.cameraRotation.Camera = camera;
            this.scientificControl.Scene.SceneContainer.Children.Clear();
            this.scientificControl.Scene.SceneContainer.Effects.Clear();
            var model = new ModelDemo(
                new Vertex(-1, -1, -1), new Vertex(1, 1, 1), 
                verticesCount, SharpGL.Enumerations.BeginMode.Points);
            this.scientificControl.Scene.SceneContainer.AddChild(model);

            //this.scientificControl.MouseWheel += openGLControl_MouseWheel;
            //this.scientificControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            //this.scientificControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            //this.scientificControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
        }

        //void openGLControl_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    ScientificCamera camera = this.scientificControl.Scene.CurrentCamera;
        //    //if (camera == null) { return; }

        //    camera.Scale(e.Delta);

        //    this.openGLControl_Resized(sender, e);
        //}

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //var gl = this.sceneControl.OpenGL;
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            //DrawPoints(gl);
            //DrawCube(gl);
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
            OpenGL gl = scientificControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);

            openGLControl_Resized(sender, e);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            CameraResized();
            //NewResized();
            //OirginalResized();

        }

        private void CameraResized()
        {
            var scientificCamera = this.scientificControl.Scene.CurrentCamera;
            if (scientificCamera == null) { return; }

            OpenGL gl = scientificControl.OpenGL;
            var h = this.scientificControl.Height;
            var w = this.scientificControl.Width;

            if (h == 0)
                h = 1;

            {
                IPerspectiveCamera camera = scientificCamera;
                camera.AspectRatio = (double)w / (double)h;
            }

            {
                IOrthoCamera camera = scientificCamera;
                if (w < h)
                {
                    camera.Bottom = camera.Left * h / w;
                    camera.Top = camera.Right * h / w;
                }
                else
                {
                    camera.Left = camera.Bottom * w / h;
                    camera.Right = camera.Top * w / h;
                }
            }
            gl.Viewport(0, 0, w, h);
            scientificCamera.Project(gl);
        }

        //private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.cameraRotation.SetBounds(this.scientificControl.Width, this.scientificControl.Height);
        //    this.cameraRotation.MouseDown(e.X, e.Y);
        //}

        //private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        //{
        //    this.cameraRotation.MouseUp(e.X, e.Y);
        //}

        //private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        //{
        //    this.cameraRotation.MouseMove(e.X, e.Y);
        //    this.CameraResized();
        //}

        private void SharpGLForm_Load(object sender, EventArgs e)
        {
            var cameraTypes = new ECameraType[] { ECameraType.Ortho, ECameraType.Perspecitive };
            foreach (var item in cameraTypes)
            {
                this.cmbCameraType.Items.Add(item);
            }
        }

        private void cmbCameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (ECameraType)this.cmbCameraType.SelectedItem;
            this.groupBox1.Visible = type == ECameraType.Ortho;
            this.scientificControl.Scene.CurrentCamera.CameraType = type;
            this.CameraResized();
        }

        private void txtZNear_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if (double.TryParse(txtZNear.Text,out value))
            {
                IOrthoCamera camera = this.scientificControl.Scene.CurrentCamera;
                camera.Near = value;
                CameraResized();
            }
        }

        private void txtZFar_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if(double.TryParse(txtZFar.Text,out value))
            {
                IOrthoCamera camera = this.scientificControl.Scene.CurrentCamera;
                camera.Far = value;
                CameraResized();
            }
        }
    }
}
