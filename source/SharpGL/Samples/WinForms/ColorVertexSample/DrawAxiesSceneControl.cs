using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Primitives;
using GlmNet;

namespace ColorVertexSample
{
    /// <summary>
    /// SceneControl with an 3D axis shown at the corner of view.
    /// </summary>
    public class DrawAxiesSceneControl : SceneControl
    {
        private int axisWidth = 80;
        private int axisHeight = 80;
        private Scene axisScene = new Scene();
        private ArcBallEffect2 rotationEffect;
        private LookAtCamera parallelCamera;
        private Pen[] pens;
        private AxisSpy axisSpy;

        public void SetAxisSize(int width, int height)
        {
            axisWidth = width;
            axisHeight = height;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CreateOpenGL(axisScene);

            InitParallelCamera(axisScene);

            InitAxis(this.axisScene);

            this.pens = new Pen[] { new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Blue) };

            this.MouseDown += AxiesSceneControl_MouseDown;
            this.MouseMove += AxiesSceneControl_MouseMove;
            this.MouseUp += AxiesSceneControl_MouseUp;
            this.GDIDraw += AxiesSceneControl_GDIDraw;
        }

        private void InitParallelCamera(Scene scene)
        {
            parallelCamera = new LookAtCamera();
            parallelCamera.AspectRatio = (double)axisWidth / (double)axisHeight;
            parallelCamera.Near = 0.001f;
            parallelCamera.Far = float.MaxValue;

            var modelSceneCamera = this.Scene.CurrentCamera as LookAtCamera;
            if (modelSceneCamera != null)
            {
                var position = modelSceneCamera.Position - modelSceneCamera.Target;
                position.Normalize();
                parallelCamera.Position = position * 7;
                parallelCamera.UpVector = modelSceneCamera.UpVector;
                parallelCamera.FieldOfView = modelSceneCamera.FieldOfView;
            }
            else
            {
                parallelCamera.Position = new Vertex(0f, 0f, 7f);
                parallelCamera.UpVector = new Vertex(0f, 1f, 0f);
                parallelCamera.FieldOfView = 60;
            }

            scene.CurrentCamera = this.parallelCamera;
        }

        /// <summary>
        /// Draw axis at corner of view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void AxiesSceneControl_GDIDraw(object sender, RenderEventArgs args)
        {
            var modelSceneCamera = this.Scene.CurrentCamera as LookAtCamera;
            if (modelSceneCamera != null)
            {
                UpdateParallelCamera(modelSceneCamera);
            }

            // update data of this.axisSpy.projectedAxisVertexes
            this.axisScene.OpenGL.MakeCurrent();
            this.axisScene.Draw();

            var targets = this.axisSpy.projectedAxisVertexes;
            for (int i = 1; i < targets.Length; i++)
            {
                args.Graphics.DrawLine(this.pens[i - 1],
                    targets[0].X, this.Height - targets[0].Y,
                    targets[i].X, this.Height - targets[i].Y);
            }
        }

        private void UpdateParallelCamera(LookAtCamera modelSceneCamera)
        {
            var position = modelSceneCamera.Position - modelSceneCamera.Target;
            position.Normalize();
            parallelCamera.Position = position * 7;
            parallelCamera.UpVector = modelSceneCamera.UpVector;
            parallelCamera.FieldOfView = modelSceneCamera.FieldOfView;
            this.rotationEffect.ArcBall.SetCamera(parallelCamera);
        }

        private void CreateOpenGL(Scene scene)
        {
            OpenGL gl = new OpenGL();
            gl.Create(this.OpenGLVersion, SharpGL.RenderContextType.FBO,
                axisWidth, axisHeight, 32, null);
            //  Set the most basic OpenGL styles.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
            gl.SetDimensions(axisWidth, axisHeight);
            gl.Viewport(0, 0, axisWidth, axisHeight);
            scene.OpenGL = gl;
        }

        private void InitAxis(SharpGL.SceneGraph.Scene scene)
        {
            this.axisSpy = new AxisSpy();
            this.rotationEffect = new ArcBallEffect2(this.parallelCamera);
            scene.SceneContainer.AddChild(this.axisSpy);
            scene.SceneContainer.AddEffect(this.rotationEffect);
        }

        void AxiesSceneControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.rotationEffect.ArcBall.MouseUp(e.X, e.Y);
                this.Invalidate();
            }
        }

        void AxiesSceneControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.rotationEffect.ArcBall.MouseMove(e.X, e.Y);
                this.Invalidate();
            }
        }

        void AxiesSceneControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.rotationEffect.ArcBall.SetBounds(this.Width, this.Height);
                this.rotationEffect.ArcBall.MouseDown(e.X, e.Y);
                this.Invalidate();
            }
        }

        public void ResetAxisRotation()
        {
            this.rotationEffect.ArcBall.ResetRotation();
        }
    }
}
