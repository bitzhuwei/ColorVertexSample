using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModernOpenGLSample._1OpenGLControl
{
    public partial class FormModernOpenGLControlDemo : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModernOpenGLControlDemo"/> class.
        /// </summary>
        public FormModernOpenGLControlDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Initialise the scene.
            this.sceneElement.Initialise(openGLControl.OpenGL, 
                openGLControl.Width, openGLControl.Height);
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            //  Draw the scene.
            this.sceneElement.Draw(openGLControl.OpenGL);
        }

        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly ModernOpenGLControlSceneElement sceneElement = new ModernOpenGLControlSceneElement();

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.sceneElement.SetBound(this.openGLControl.Width, this.openGLControl.Height);
                this.sceneElement.MouseDown(e.X, e.Y);
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.sceneElement.MouseMove(e.X, e.Y);
            }
            //// Color-coded picking.
            this.sceneElement.Draw(this.openGLControl.OpenGL, SharpGL.SceneGraph.Core.RenderMode.HitTest);

            byte[] result = new byte[4];
            this.openGLControl.OpenGL.ReadPixels(
                e.X, this.openGLControl.Height - e.Y, 1, 1,
                OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, result);
            var color = string.Format("R:{0},G:{1},B:{2},A:{3}",
                result[0], result[1], result[2], result[3]);
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
                this.sceneElement.MouseUp(e.X, e.Y);
            }
        }

    }
}
