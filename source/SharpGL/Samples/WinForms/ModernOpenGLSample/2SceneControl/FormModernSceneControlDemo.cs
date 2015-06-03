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
            ScientificCamera camera = new ScientificCamera(CameraTypes.Perspecitive);
            camera.Position = new SharpGL.SceneGraph.Vertex(0, 0, 5);
            camera.Target = new SharpGL.SceneGraph.Vertex();
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
            this.cameraRotation.Camera = camera;
            this.sceneElement.Camera = camera;

            // prepare scene's settings.
            this.sceneControl.Scene.SceneContainer.Children.Clear();
            this.sceneControl.Scene.SceneContainer.Effects.Clear();
            this.sceneControl.Scene.ClearColor = new GLColor(0.4f, 0.6f, 0.9f, 0.5f);

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

            //// Color-coded picking.
            var gl = this.sceneControl.OpenGL;
            if (gl == null) { return; }

            var camera = this.sceneControl.Scene.CurrentCamera;


            gl.ClearColor(1, 1, 1, 1);

            //  Reproject.
            if (camera != null)
                camera.Project(gl);

            //	Clear.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT |
                OpenGL.GL_STENCIL_BUFFER_BIT);

            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            //  Render the root element, this will then render the whole
            //  of the scene tree.
            this.sceneControl.Scene.RenderElement(this.sceneControl.Scene.SceneContainer,
                 SharpGL.SceneGraph.Core.RenderMode.HitTest);

            //  TODO: Adding this code here re-enables textures- it should work without it but it
            //  doesn't, look into this.
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            //gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Flush();

            byte[] result = new byte[4];
            this.sceneControl.OpenGL.ReadPixels(
                e.X, this.sceneControl.Height - e.Y, 1, 1,
                OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, result);
            var color = string.Format("R:{0},G:{1},B:{2},A:{3}",
                result[0], result[1], result[2], result[3]);
            /*
             * 	int objectID = gl_VertexID;
	pass_Color = vec4(
		float(objectID & 0xFF) / 255.0, 
		float((objectID >> 8) & 0xFF) / 255.0, 
		float((objectID >> 16) & 0xFF) / 255.0, 
		float((objectID >> 24) & 0xFF) / 255.0);
             */

            var vertexID = 0u;
            var r = (uint)result[0];
            var g = (uint)result[1] << 8;
            var b = (uint)result[2] << 16;
            var a = (uint)result[3] << 24;
            vertexID = r + g + b + a;
            this.txtInfo.Text = string.Format("{1}{0}vertex ID:{0}={2}{0}+{3}{0}+{4}{0}+{5}{0}={6}{0}+{7}{0}+{8}{0}+{9}{0}={10}",
                Environment.NewLine, color,
                result[0], result[1] + " << 8", result[2] + " << 16", result[3] + " << 24",
                r, g, b, a, vertexID);
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
