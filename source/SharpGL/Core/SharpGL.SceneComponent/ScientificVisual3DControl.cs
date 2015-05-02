using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SharpGL.Version;
using SharpGL.SceneGraph;
using System.Collections.ObjectModel;
using SharpGL.SceneGraph.Cameras;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// scene control which contains axis, color indicator, etc.
    /// </summary>
    public partial class ScientificVisual3DControl : MySceneControl, ISupportInitialize
    {
        public ScientificVisual3DControl()
        {
            //this.RotationObjects = new ObservableCollection<IRotation>();

            MyScene UIScene = new MyScene();
            UIScene.IsClear = false;
            UIScene.OpenGL = this.OpenGL;
            this.UIScene = UIScene;
            
            ScientificVisual3DControlHelper.InitializeUIScene(this);

            this.MouseDown += ScientificVisual3DControl_MouseDown;
            this.MouseMove += ScientificVisual3DControl_MouseMove;
            this.MouseUp += ScientificVisual3DControl_MouseUp;
        }

        void ScientificVisual3DControl_MouseUp(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                CameraRotation cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.MouseUp(e.X, e.Y);

                    render = true;
                }
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                IRotation rotation = this.uiAxis;
                if (rotation != null)
                {
                    rotation.MouseUp(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this); }
        }

        void ScientificVisual3DControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool render = false;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                CameraRotation cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.MouseMove(e.X, e.Y);

                    render = true;
                }
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                IRotation rotation = this.uiAxis;
                if (rotation != null)
                {
                    rotation.MouseMove(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this); }
        }

        void ScientificVisual3DControl_MouseDown(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                CameraRotation cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.SetBounds(this.Width, this.Height);
                    cameraRotation.MouseDown(e.X, e.Y);

                    render = true;
                }
            }

            if ((e.Button & MouseButtons.Right) == System.Windows.Forms.MouseButtons.Right)
            {
                IRotation rotation = this.uiAxis;
                if (rotation != null)
                {
                    rotation.SetBounds(this.Width, this.Height);
                    rotation.MouseDown(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this); }
        }

        private void ManualRender(Control control)
        {
            control.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Start the stopwatch so that we can time the rendering.
            stopwatch.Restart();

            //	Make sure it's our instance of openSharpGL that's active.
            OpenGL.MakeCurrent();

            //	Do the scene drawing.
            Scene.Draw();

            UIScene.Draw();

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

        public void SetSceneCameraToUICamera()
        {
            LookAtCamera camera = this.Scene.CurrentCamera as LookAtCamera;
            this.UIScene.CurrentCamera = camera;
            //if (camera == null) { return; }

            this.uiAxis.Camera = camera;
            this.CameraRotation.Camera = camera;
        }

        public MyScene UIScene { get; set; }

        //public ObservableCollection<IRotation> RotationObjects { get; protected set; }

        public CameraRotation CameraRotation { get; set; }

        public OpenGLUIAxis uiAxis { get; set; }

        public OpenGLUIColorIndicator uiColorIndicator { get; set; }
    }
}
