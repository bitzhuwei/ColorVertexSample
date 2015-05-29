using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// replace of <see cref="SharpGL.WinForms.SceneControl"/>
    /// <para>Invoke base.OnSizeChanged to update opengl's view size.</para>
    /// <para>Use <see cref="MyScene"/> instead of Scene. <see cref="MyScene"/> pushes and pops <see cref="IBindable"/> scene elements.</para>
    /// </summary>
    public partial class MySceneControl : OpenGLControl
    {
        public MySceneControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            // this.scene.OpenGL.RenderContextProvider is null, so we use this.OpenGL instead.
            this.scene.OpenGL = this.OpenGL;

            //  Initialise the scene.
            MySceneControlHelper.InitialiseModelingScene(scene);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Start the stopwatch so that we can time the rendering.
            stopwatch.Restart();

            //	Make sure it's our instance of openSharpGL that's active.
            OpenGL.MakeCurrent();

            //	Do the scene drawing.
            scene.Draw(SharpGL.SceneGraph.Core.RenderMode.Render);

            //	If there is a draw handler, then call it.
            DoOpenGLDraw(new RenderEventArgs(e.Graphics));

            //  Draw the FPS.
            if (DrawFPS)
            {
                OpenGL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f,
                    string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
                OpenGL.Flush();
            }

            //	Blit our offscreen bitmap.
            IntPtr handleDeviceContext = e.Graphics.GetHdc();
            OpenGL.Blit(handleDeviceContext);
            e.Graphics.ReleaseHdc(handleDeviceContext);

            //	If's there's a GDI draw handler, then call it.
            DoGDIDraw(new RenderEventArgs(e.Graphics));

            //  Stop the stopwatch.
            stopwatch.Stop();

            //  Store the frame time.
            frameTime = stopwatch.Elapsed.TotalMilliseconds;
        }


        /// <summary>
        /// Raises the <see cref="E:PaintBackground"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //	We override this, and don't call the base, i.e we don't paint
            //	the background.
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            //  Don't call the base- we handle sizing ourselves.
            base.OnSizeChanged(e);

            //	OpenGL needs to resize the viewport.
            OpenGL.SetDimensions(Width, Height);
            scene.Resize(Width, Height);

            Invalidate();
        }

        /// <summary>
        /// This is the scene itself.
        /// </summary>
        private MyScene scene = new MyScene();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MyScene Scene
        {
            get { return scene; }
            set { scene = value; }
        }

        /// <summary>
        /// Get picked primitive at specified screen location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public IPickedPrimitive Pick(int x, int y)
        {
            // render the scene for color-coded picking.
            this.Scene.Draw(RenderMode.HitTest);
            // get coded color.
            byte[] codedColor = new byte[4];
            this.OpenGL.ReadPixels(x, this.Height - y - 1, 1, 1,
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
            var vertexID = 0;
            var shiftedR = codedColor[0];
            var shiftedG = codedColor[1] << 8;
            var shiftedB = codedColor[2] << 16;
            var shiftedA = codedColor[3] << 24;
            vertexID = shiftedR + shiftedG + shiftedB + shiftedA;

            // get picked primitive.
            IPickedPrimitive picked = null;
            picked = this.Scene.Pick(vertexID);

            return picked;
        }
    }

}
