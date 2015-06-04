using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModernOpenGLSample._3MySceneControl
{
    public partial class FormModernSceneControlDemo : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModernOpenGLControlDemo"/> class.
        /// </summary>
        public FormModernSceneControlDemo()
        {
            InitializeComponent();

            foreach (var name in Enum.GetNames(typeof(BeginMode)))
            {
                var item = new ComboBoxItem<BeginMode>(
                    name, (BeginMode)Enum.Parse(typeof(BeginMode), name));
                this.cmbBeginMode.Items.Add(item);
            }
            this.cmbBeginMode.SelectedIndex = 4;

            // prepare camera.
            ScientificCamera camera = new ScientificCamera(CameraTypes.Perspecitive);
            camera.Position = new SharpGL.SceneGraph.Vertex(0, 0, 5);
            camera.Target = new SharpGL.SceneGraph.Vertex();
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
            this.cameraRotation.Camera = camera;
            this.sceneElement.Camera = camera;
            this.sceneElement2.Camera = camera;

            // prepare scene's settings.
            this.mySceneControl.Scene.SceneContainer.Children.Clear();
            this.mySceneControl.Scene.SceneContainer.Effects.Clear();
            this.mySceneControl.Scene.ClearColor = new GLColor(0.4f, 0.6f, 0.9f, 0.5f);

            // prepare models' elements.
            this.sceneElement.Initialise(this.mySceneControl.OpenGL,
                this.mySceneControl.Width, this.mySceneControl.Height);
            this.mySceneControl.Scene.SceneContainer.AddChild(this.sceneElement);
            this.sceneElement2.Initialise(this.mySceneControl.OpenGL,
                this.mySceneControl.Width, this.mySceneControl.Height);
            this.mySceneControl.Scene.SceneContainer.AddChild(this.sceneElement2);

        }

        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly ModernMySceneControlSceneElement sceneElement = new ModernMySceneControlSceneElement();
        private readonly ModernMySceneControlSceneElement sceneElement2 = new ModernMySceneControlSceneElement(false);
        SatelliteRotation cameraRotation = new SatelliteRotation();

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.cameraRotation.SetBounds(this.mySceneControl.Width, this.mySceneControl.Height);
                this.cameraRotation.MouseDown(e.X, e.Y);
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.cameraRotation.MouseMove(e.X, e.Y);
            }

            {
                // render the scene for color-coded picking.
                this.mySceneControl.Scene.Draw(RenderMode.HitTest);
                // get coded color.
                byte[] codedColor = new byte[4];
                this.mySceneControl.OpenGL.ReadPixels(
                    e.X, this.mySceneControl.Height - e.Y - 1, 1, 1,
                    OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, codedColor);

                /* // This is how is vertexID coded into color in vertex shader.
                 * 	int objectID = gl_VertexID;
                    codedColor = vec4(
                        float(objectID & 0xFF), 
                        float((objectID >> 8) & 0xFF), 
                        float((objectID >> 16) & 0xFF), 
                        float((objectID >> 24) & 0xFF));
                 */

                // get vertexID from coded color.
                // the vertexID is the last vertex that constructs the primitive.
                // see http://www.cnblogs.com/bitzhuwei/p/modern-opengl-picking-primitive-in-VBO-2.html
                uint shiftedR = (uint)codedColor[0];
                uint shiftedG = (uint)codedColor[1] << 8;
                uint shiftedB = (uint)codedColor[2] << 16;
                uint shiftedA = (uint)codedColor[3] << 24;
                uint stageVertexID = shiftedR + shiftedG + shiftedB + shiftedA;

                // get picked primitive.
                IPickedGeometry pickedGeometry = null;
                pickedGeometry = this.mySceneControl.Scene.Pick(stageVertexID);

                // print result.
                var strColor = string.Format("R:{0},G:{1},B:{2},A:{3}",
                    codedColor[0], codedColor[1], codedColor[2], codedColor[3]);
                this.txtInfo.Text = string.Format("{1}{0}vertex ID:{0}={2}{0}+{3}{0}+{4}{0}+{5}{0}={6}{0}+{7}{0}+{8}{0}+{9}{0}={10}{0}Picked:{0}{11}",
                    Environment.NewLine, strColor,
                    codedColor[0], codedColor[1] + " << 8", codedColor[2] + " << 16", codedColor[3] + " << 24",
                    shiftedR, shiftedG, shiftedB, shiftedA, stageVertexID, pickedGeometry);
            }

        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.cameraRotation.MouseUp(e.X, e.Y);
            }
        }

        private void mySceneControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.mySceneControl.Scene.DoHitTest(e.X, e.Y);
            }
        }

        private void cmbBeginMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = this.cmbBeginMode.SelectedItem as ComboBoxItem<BeginMode>;
            this.sceneElement.mode = item.value;
            this.sceneElement2.mode = item.value;
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
