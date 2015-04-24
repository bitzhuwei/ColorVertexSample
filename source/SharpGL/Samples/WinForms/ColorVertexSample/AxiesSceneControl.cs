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

namespace ColorVertexSample
{
    public class AxiesSceneControl : SceneControl
    {
        private Scene axisScene = new Scene();
        private ArcBallEffect2 rotationEffect;
        private Bitmap bmpAxis = new Bitmap(80, 80);
        private LookAtCamera parallelCamera = new LookAtCamera();

        public void SetAxisSize(int width, int height)
        {
            this.bmpAxis = new Bitmap(width, height);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CreateOpenGL(axisScene);

            this.InitAxis(this.axisScene);

            InitParallelCamera();

            this.MouseDown += AxiesSceneControl_MouseDown;
            this.MouseMove += AxiesSceneControl_MouseMove;
            this.MouseUp += AxiesSceneControl_MouseUp;
            this.GDIDraw += AxiesSceneControl_GDIDraw;
        }

        private void InitParallelCamera()
        {
            parallelCamera.AspectRatio = (double)bmpAxis.Width / (double)bmpAxis.Height;
            parallelCamera.Near = 0.001f;
            parallelCamera.Far = float.MaxValue;
        }

        void AxiesSceneControl_GDIDraw(object sender, RenderEventArgs args)
        {
            var modelSceneCamera = this.Scene.CurrentCamera as LookAtCamera;
            if (modelSceneCamera != null)
            {
                var position = modelSceneCamera.Position - modelSceneCamera.Target;
                position.Normalize();
                parallelCamera.Position = position * 7;
                parallelCamera.UpVector = modelSceneCamera.UpVector;
                parallelCamera.FieldOfView = modelSceneCamera.FieldOfView; //60;
                parallelCamera.AspectRatio = (double)bmpAxis.Width / (double)bmpAxis.Height;
            }

            using (var graphics = Graphics.FromImage(bmpAxis))
            {
                this.axisScene.OpenGL.MakeCurrent();
                this.axisScene.Draw(modelSceneCamera != null ? parallelCamera : null);
                var handleDeviceContext = graphics.GetHdc();
                this.axisScene.OpenGL.Blit(handleDeviceContext);
                graphics.ReleaseHdc(handleDeviceContext);
            }

            args.Graphics.DrawImage(bmpAxis, 0, this.Height - bmpAxis.Height);
        }

        private void CreateOpenGL(Scene scene)
        {
            OpenGL gl = new OpenGL();
            gl.Create(this.OpenGLVersion, this.renderContextType, 
                bmpAxis.Width, bmpAxis.Height, 32, null);
            //  Set the most basic OpenGL styles.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
            gl.SetDimensions(bmpAxis.Width, bmpAxis.Height);
            gl.Viewport(0, 0, bmpAxis.Width, bmpAxis.Height);
            scene.OpenGL = gl; 
        }

        private void InitAxis(SharpGL.SceneGraph.Scene scene)
        {
            scene.ClearColor = Color.Gold;
            //  Create the 'Look At' camera
            var lookAtCamera = new LookAtCamera()
            {
                Position = new Vertex(0f, 0f, 7f),
                Target = new Vertex(0f, 0f, 0f),
                UpVector = new Vertex(0f, 1f, 0f)
            };

            //  Set the look at camera as the current camera.
            scene.CurrentCamera = lookAtCamera;

            //  Add some design-time primitives.
            var folder = new Folder() { Name = "AxisLights" };
            scene.SceneContainer.AddChild(folder);
            this.rotationEffect = new ArcBallEffect2(lookAtCamera);
            folder.AddEffect(this.rotationEffect);

            Axies axies = new Axies();
            folder.AddChild(new Axies());

            //  Create a set of scene attributes.
            var sceneAttributes = new OpenGLAttributesEffect()
            {
                Name = "Scene Attributes"
            };

            //  Specify the scene attributes.
            sceneAttributes.EnableAttributes.EnableDepthTest = true;
            sceneAttributes.EnableAttributes.EnableNormalize = true;
            sceneAttributes.EnableAttributes.EnableLighting = false;
            sceneAttributes.EnableAttributes.EnableTexture2D = true;
            sceneAttributes.EnableAttributes.EnableBlend = true;
            sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            sceneAttributes.LightingAttributes.TwoSided = true;
            scene.SceneContainer.AddEffect(sceneAttributes);
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
