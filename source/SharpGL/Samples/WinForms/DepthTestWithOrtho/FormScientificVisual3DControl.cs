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
    public partial class FormScientificVisual3DControl : Form
    {
        //CameraRotation cameraRotation = new CameraRotation();
        //ScientificCamera camera;
        //ScientificCamera camera = new ScientificCamera(ECameraType.Ortho)
        //{
        //    Target = new SharpGL.SceneGraph.Vertex(0, 0, 0),
        //    UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0),
        //    Position = new SharpGL.SceneGraph.Vertex(0, 0, 5),
        //};

        //List<Vertex> positions = new List<Vertex>();
        //List<GLColor> colors = new List<GLColor>();
        int verticesCount = 100000;
        //Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public FormScientificVisual3DControl()
        {
            InitializeComponent();

            //for (int i = 0; i < verticesCount; i++)
            //{
            //    var position = new Vertex();
            //    position.X = (float)random.NextDouble() * 2 - 1;
            //    position.Y = (float)random.NextDouble() * 2 - 1;
            //    position.Z = (float)random.NextDouble() * 2 - 1;
            //    positions.Add(position);
            //    var color = new GLColor();
            //    color.R = (float)random.NextDouble();
            //    color.G = (float)random.NextDouble();
            //    color.B = (float)random.NextDouble();
            //    color.A = (float)random.NextDouble();
            //    colors.Add(color);
            //}

            var camera = this.scientificVisual3DControl.Scene.CurrentCamera;
            IOrthoCamera orthoCamera = camera;
            orthoCamera.Left = -10; orthoCamera.Bottom = -10; orthoCamera.Near = -10;
            orthoCamera.Right = 10; orthoCamera.Top = 10; orthoCamera.Far = 10;
            //this.sceneControl.Scene.CurrentCamera = camera;
            //this.cameraRotation.Camera = this.camera;
            //this.scientificVisual3DControl.Scene.SceneContainer.Children.Clear();
            //this.scientificVisual3DControl.Scene.SceneContainer.Effects.Clear();
            var model = new ModelDemo(
                new Vertex(-1, -1, -1), new Vertex(1, 1, 1), 
                verticesCount, SharpGL.Enumerations.BeginMode.Points);
            //this.scientificVisual3DControl.Scene.SceneContainer.AddChild(model);
            this.scientificVisual3DControl.AddModelElement(model);
            var box = this.scientificVisual3DControl.ModelContainer.BoundingBox;
            box.Set(-1.1f, -1.1f, -1.1f, 1.1f, 1.1f, 1.1f);

            //this.scientificVisual3DControl.MouseWheel += openGLControl_MouseWheel;
        }

        //void openGLControl_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    ScientificCamera camera = this.camera;
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

        //private void DrawPoints(OpenGL gl)
        //{
        //    gl.LoadIdentity();

        //    gl.Begin(SharpGL.Enumerations.BeginMode.Points);
        //    for (int i = 0; i < verticesCount; i++)
        //    {
        //        gl.Color(colors[i].R, colors[i].G, colors[i].B);
        //        gl.Vertex(positions[i].X, positions[i].Y, positions[i].Z);
        //    }
        //    gl.End();

        //}

       
        private void DrawCube(OpenGL gl)
        {
            float minX = -1;
            float minY = -1;
            float minZ = -1;
            float maxX = 1;
            float maxY = 1;
            float maxZ = 1;

            gl.LoadIdentity();

            gl.Color(1f, 1f, 1f);
            //gl.Color(1f, 0f, 0f);
            gl.Begin(SharpGL.Enumerations.BeginMode.LineLoop);
            gl.Vertex(minX, minY, minZ);
            gl.Vertex(minX, minY, maxZ);
            gl.Vertex(minX, maxY, maxZ);
            gl.Vertex(minX, maxY, minZ);
            gl.End();

            //gl.Color(0f, 1f, 0f);
            gl.Begin(SharpGL.Enumerations.BeginMode.LineLoop);
            gl.Vertex(maxX, minY, minZ);
            gl.Vertex(maxX, minY, maxZ);
            gl.Vertex(maxX, maxY, maxZ);
            gl.Vertex(maxX, maxY, minZ);
            gl.End();

            //gl.Color(0, 0, 1f);
            gl.Begin(SharpGL.Enumerations.BeginMode.Lines);
            gl.Vertex(minX, minY, minZ);
            gl.Vertex(maxX, minY, minZ);
            gl.Vertex(minX, minY, maxZ);
            gl.Vertex(maxX, minY, maxZ);
            gl.Vertex(minX, maxY, maxZ);
            gl.Vertex(maxX, maxY, maxZ);
            gl.Vertex(minX, maxY, minZ);
            gl.Vertex(maxX, maxY, minZ);
            gl.End();

        }

        //private void NewDraw(OpenGL gl)
        //{
        //    gl.Enable(OpenGL.GL_DEPTH_TEST);//深度测试
        //    gl.Begin(OpenGL.GL_TRIANGLES);
        //    gl.Color(255, 0, 0);      //红色
        //    gl.Vertex(-50.0f, 0.0f, 0.0f);
        //    gl.Vertex(50.0f, 0.0f, 0.0f);
        //    gl.Vertex(0.0f, 80.0f, 0.0f);

        //    gl.Color(0, 255, 0);    //绿色
        //    gl.Vertex(-50.0f, 0.0f, -10.0f);
        //    gl.Vertex(50.0f, 0.0f, -10.0f);
        //    gl.Vertex(0.0f, 80.0f, -10.0f);
        //    gl.End();
        //}

        //private void OriginalDraw(OpenGL gl)
        //{

        //    //  Load the identity matrix.
        //    gl.LoadIdentity();

        //    //  Rotate around the Y axis.
        //    gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

        //    //  Draw a coloured pyramid.
        //    gl.Begin(OpenGL.GL_TRIANGLES);
        //    gl.Color(1.0f, 0.0f, 0.0f);
        //    gl.Vertex(0.0f, 1.0f, 0.0f);
        //    gl.Color(0.0f, 1.0f, 0.0f);
        //    gl.Vertex(-1.0f, -1.0f, 1.0f);
        //    gl.Color(0.0f, 0.0f, 1.0f);
        //    gl.Vertex(1.0f, -1.0f, 1.0f);
        //    gl.Color(1.0f, 0.0f, 0.0f);
        //    gl.Vertex(0.0f, 1.0f, 0.0f);
        //    gl.Color(0.0f, 0.0f, 1.0f);
        //    gl.Vertex(1.0f, -1.0f, 1.0f);
        //    gl.Color(0.0f, 1.0f, 0.0f);
        //    gl.Vertex(1.0f, -1.0f, -1.0f);
        //    gl.Color(1.0f, 0.0f, 0.0f);
        //    gl.Vertex(0.0f, 1.0f, 0.0f);
        //    gl.Color(0.0f, 1.0f, 0.0f);
        //    gl.Vertex(1.0f, -1.0f, -1.0f);
        //    gl.Color(0.0f, 0.0f, 1.0f);
        //    gl.Vertex(-1.0f, -1.0f, -1.0f);
        //    gl.Color(1.0f, 0.0f, 0.0f);
        //    gl.Vertex(0.0f, 1.0f, 0.0f);
        //    gl.Color(0.0f, 0.0f, 1.0f);
        //    gl.Vertex(-1.0f, -1.0f, -1.0f);
        //    gl.Color(0.0f, 1.0f, 0.0f);
        //    gl.Vertex(-1.0f, -1.0f, 1.0f);
        //    gl.End();

        //    //  Nudge the rotation.
        //    rotation += 3.0f;
        //}



        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = scientificVisual3DControl.OpenGL;

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
            //CameraResized();
            //NewResized();
            //OirginalResized();

        }

        //private void CameraResized()
        //{
        //    if (this.camera == null) { return; }

        //    OpenGL gl = scientificVisual3DControl.OpenGL;
        //    var h = this.scientificVisual3DControl.Height;
        //    var w = this.scientificVisual3DControl.Width;

        //    if (h == 0)
        //        h = 1;

        //    {
        //        IPerspectiveCamera camera = this.camera;
        //        camera.AspectRatio = (double)w / (double)h;
        //    }

        //    {
        //        IOrthoCamera camera = this.camera;
        //        if (w < h)
        //        {
        //            camera.Bottom = camera.Left * h / w;
        //            camera.Top = camera.Right * h / w;
        //        }
        //        else
        //        {
        //            camera.Left = camera.Bottom * w / h;
        //            camera.Right = camera.Top * w / h;
        //        }
        //    }
        //    gl.Viewport(0, 0, w, h);
        //    this.camera.Project(gl);
        //}

        //private void NewResized()
        //{
        //    OpenGL gl = openGLControl.OpenGL;
        //    var h = this.openGLControl.Height;
        //    var w = this.openGLControl.Width;

        //    if (h == 0)
        //        h = 1;
        //    gl.Viewport(0, 0, w, h);
        //    gl.MatrixMode(OpenGL.GL_PROJECTION);
        //    gl.LoadIdentity();
        //    if (w < h)
        //        gl.Ortho(-10.0f, 10.0f, -10.0f * h / w, 10.0f * h / w, -10.0f, 10.0f);
        //    else
        //        gl.Ortho(-10.0f * w / h, 10.0f * w / h, -10.0f, 10.0f, -10.0f, 10.0f);
        //    gl.MatrixMode(OpenGL.GL_MODELVIEW);
        //    gl.LoadIdentity();
        //}

        //private void OirginalResized()
        //{
        //    //  TODO: Set the projection matrix here.

        //    //  Get the OpenGL object.
        //    OpenGL gl = openGLControl.OpenGL;

        //    //  Set the projection matrix.
        //    gl.MatrixMode(OpenGL.GL_PROJECTION);

        //    //  Load the identity.
        //    gl.LoadIdentity();

        //    //  Create a perspective transformation.
        //    gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

        //    //  Use the 'look at' helper function to position and aim the camera.
        //    gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

        //    //  Set the modelview matrix.
        //    gl.MatrixMode(OpenGL.GL_MODELVIEW);
        //}

        ///// <summary>
        ///// The current rotation.
        ///// </summary>
        //private float rotation = 0.0f;

        //private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.cameraRotation.SetBounds(this.scientificVisual3DControl.Width, this.scientificVisual3DControl.Height);
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
            this.scientificVisual3DControl.CameraType = type;
            //this.camera.CameraType = type;
            //this.CameraResized();
        }

        private void txtZNear_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if (double.TryParse(txtZNear.Text,out value))
            {
                IOrthoCamera camera = this.scientificVisual3DControl.Scene.CurrentCamera;
                camera.Near = value;
                //CameraResized();
            }
        }

        private void txtZFar_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if(double.TryParse(txtZFar.Text,out value))
            {
                IOrthoCamera camera = this.scientificVisual3DControl.Scene.CurrentCamera;
                camera.Far = value;
                //CameraResized();
            }
        }
    }
}
