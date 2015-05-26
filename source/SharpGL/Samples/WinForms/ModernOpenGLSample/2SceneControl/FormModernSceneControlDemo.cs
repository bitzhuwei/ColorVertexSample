using SharpGL;
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
            this.sceneControl.Scene.SceneContainer.Children.Clear();
            this.sceneControl.Scene.SceneContainer.Effects.Clear();
            this.sceneControl.Scene.ClearColor = new GLColor(0.4f, 0.6f, 0.9f, 0.0f);
            this.sceneElement.Initialise(this.sceneControl.OpenGL, 
                this.sceneControl.Width, this.sceneControl.Height);
            this.sceneControl.Scene.SceneContainer.AddChild(this.sceneElement);
        }

        ///// <summary>
        ///// Handles the OpenGLInitialized event of the openGLControl1 control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        //{
        //    //  Initialise the scene.
        //    this.sceneElement.Initialise(sceneControl.OpenGL, 
        //        sceneControl.Width, sceneControl.Height);
        //}

        ///// <summary>
        ///// Handles the OpenGLDraw event of the openGLControl1 control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="args">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        //private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        //{
        //    //  Draw the scene.
        //    this.sceneElement.Draw(sceneControl.OpenGL);
        //}

        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly ModernOpenGLControlSceneElement sceneElement = new ModernOpenGLControlSceneElement();

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.sceneElement.SetBound(this.sceneControl.Width, this.sceneControl.Height);
                this.sceneElement.MouseDown(e.X, e.Y);
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.sceneElement.MouseMove(e.X, e.Y);
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.sceneElement.MouseUp(e.X, e.Y);
            }
        }

    }
}
