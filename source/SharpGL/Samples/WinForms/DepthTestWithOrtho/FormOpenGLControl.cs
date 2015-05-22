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
using SharpGL.SceneGraph.Core;

namespace DepthTestWithOrtho
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class FormOpenGLControl : Form
    {
        SatelliteRotation cameraRotation = new SatelliteRotation();
        ScientificCamera camera = new ScientificCamera(ECameraType.Ortho)
        {
            Target = new SharpGL.SceneGraph.Vertex(0, 0, 0),
            UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0),
            Position = new SharpGL.SceneGraph.Vertex(0, 0, 10),
        };

        List<SceneElement> elements = new List<SceneElement>();
        int verticesCount = 100000;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormOpenGLControl"/> class.
        /// </summary>
        public FormOpenGLControl()
        {
            InitializeComponent();

            {
                var model = Model.PointModel.Create(verticesCount, 1, 1, 1, -3, -1);
                var element = new ScientificModelElement(model);
                elements.Add(element);
            }
            {
                var model = Model.PointModel.Create(verticesCount, 1, 1, 1, -1, 1);
                var element = new ScientificModelElement(model);
                elements.Add(element);
            }
            {
                var model = Model.PointModel.Create(verticesCount, 1, 1, 1, 1, 3);
                var element = new ScientificModelElement(model);
                elements.Add(element);
            }
            {
                var modelContainer = new ModelContainer();
                modelContainer.BoundingBox.Set(-3.1f, -3.1f, -3.1f, 3.1f, 3.1f, 3.1f);
                elements.Add(modelContainer);
            }

            IOrthoCamera orthoCamera = camera;
            orthoCamera.Left = -10; orthoCamera.Bottom = -10; orthoCamera.Near = 0;
            orthoCamera.Right = 10; orthoCamera.Top = 10; orthoCamera.Far = 15;
            this.cameraRotation.Camera = this.camera;
            this.openGLControl.MouseWheel += openGLControl_MouseWheel;
        }

        void openGLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            ScientificCamera camera = this.camera;
            //if (camera == null) { return; }

            camera.Scale(e.Delta);

            this.openGLControl_Resized(sender, e);
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            var gl = this.openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            foreach (var item in this.elements)
            {
                IRenderable renderable = item as IRenderable;
                if (renderable != null)
                {
                    renderable.Render(gl, RenderMode.Render);
                }
            }
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
        }

        private void CameraResized()
        {
            OpenGL gl = openGLControl.OpenGL;
            var h = this.openGLControl.Height;
            var w = this.openGLControl.Width;

            if (h == 0)
                h = 1;

            {
                IPerspectiveCamera camera = this.camera;
                camera.AspectRatio = (double)w / (double)h;
            }

            {
                IOrthoCamera camera = this.camera;
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
            this.camera.Project(gl);
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.cameraRotation.SetBounds(this.openGLControl.Width, this.openGLControl.Height);
            this.cameraRotation.MouseDown(e.X, e.Y);
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.cameraRotation.MouseUp(e.X, e.Y);
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            this.cameraRotation.MouseMove(e.X, e.Y);
            this.CameraResized();
        }

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
            this.camera.CameraType = type;
            this.CameraResized();
        }

        private void txtZNear_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if (double.TryParse(txtZNear.Text,out value))
            {
                IOrthoCamera camera = this.camera;
                camera.Near = value;
                CameraResized();
            }
        }

        private void txtZFar_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if(double.TryParse(txtZFar.Text,out value))
            {
                IOrthoCamera camera = this.camera;
                camera.Far = value;
                CameraResized();
            }
        }
    }
}
