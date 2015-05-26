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
using System.Windows.Forms;

namespace ModernOpenGLSample._2SceneControl
{
    public partial class FormModernSceneControlDemo : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModernOpenGLControlDemo"/> class.
        /// </summary>
        public FormModernSceneControlDemo()
        {
            InitializeComponent();

            // prepare camera.
            ScientificCamera camera = new ScientificCamera(ECameraType.Perspecitive);
            camera.Position = new SharpGL.SceneGraph.Vertex(0, 0, -5);
            camera.Target = new SharpGL.SceneGraph.Vertex();
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
            this.cameraRotation.Camera = camera;
            this.sceneElement.Camera = camera;

            // prepare scene's settings.
            this.sceneControl.Scene.SceneContainer.Children.Clear();
            this.sceneControl.Scene.SceneContainer.Effects.Clear();
            this.sceneControl.Scene.ClearColor = new GLColor(0.4f, 0.6f, 0.9f, 0.0f);

            // prepare model's element.
            this.sceneElement.Initialise(this.sceneControl.OpenGL, 
                this.sceneControl.Width, this.sceneControl.Height);
            this.sceneControl.Scene.SceneContainer.AddChild(this.sceneElement);
        }

        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly ModernSceneControlSceneElement sceneElement = new ModernSceneControlSceneElement();
        SatelliteRotation cameraRotation = new SatelliteRotation();

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.cameraRotation.SetBounds(this.sceneControl.Width, this.sceneControl.Height);
                this.cameraRotation.MouseDown(e.X, e.Y);
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.cameraRotation.MouseMove(e.X, e.Y);
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.cameraRotation.MouseUp(e.X, e.Y);
            }
        }

    }
}
