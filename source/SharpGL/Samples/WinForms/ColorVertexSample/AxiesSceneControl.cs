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
        private Size axisSize = new Size(80, 80);
        private ArcBallEffect2 rotationEffect;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CreateOpenGL(axisScene);

            this.InitAxisScene(this.axisScene);

            this.MouseDown += AxiesSceneControl_MouseDown;
            this.MouseMove += AxiesSceneControl_MouseMove;
            this.MouseUp += AxiesSceneControl_MouseUp;
            this.GDIDraw += AxiesSceneControl_GDIDraw;
        }

        System.Drawing.Drawing2D.Matrix flip = new System.Drawing.Drawing2D.Matrix(1, 0, 0, -1, 0, 0);
        void AxiesSceneControl_GDIDraw(object sender, RenderEventArgs args)
        {
            var g = args.Graphics;
            //set the coordinate orignate to bottom left.

            //transform and flip y axis of the control world coordinate system.
            g.TranslateTransform(0, this.Height);
            g.MultiplyTransform(flip);

            //	Blit our offscreen bitmap.
            using (var imgAxies = new Bitmap(this.axisSize.Width, this.axisSize.Height))
            {
                using (var gImage = Graphics.FromImage(imgAxies))
                {
                    this.axisScene.OpenGL.MakeCurrent();
                    this.axisScene.Draw();
                    var hdc = gImage.GetHdc();
                    this.axisScene.OpenGL.Blit(hdc);
                    gImage.ReleaseHdc(hdc);
                }
                g.DrawImage(imgAxies, 0, 0);
            }
        }

        private void CreateOpenGL(Scene scene)
        {
            OpenGL gl = new OpenGL();
            gl.Create(this.OpenGLVersion, this.renderContextType, axisSize.Width, axisSize.Height, 32, null);
            //  Set the most basic OpenGL styles.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
            gl.SetDimensions(axisSize.Width, axisSize.Height);
            gl.Viewport(0, 0, axisSize.Width, axisSize.Height);
            scene.OpenGL = gl; 
        }

        private void InitAxisScene(SharpGL.SceneGraph.Scene scene)
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

        private void OnPaintAxies(System.Windows.Forms.PaintEventArgs e)
        {
            //set the coordinate orignate to bottom left.

            ////transform and flip y axis of the control world coordinate system.
            //e.Graphics.TranslateTransform(0, this.Height);
            //System.Drawing.Drawing2D.Matrix flip = new System.Drawing.Drawing2D.Matrix(1, 0, 0, -1, 0, 0);
            //e.Graphics.MultiplyTransform(flip);

            //	Blit our offscreen bitmap.
            //using (var imgAxies = new Bitmap(this.axisArea.Width, this.axisArea.Height))
            {
                //using (var g = Graphics.FromImage(imgAxies))
                var g = e.Graphics;
                {
                    this.axisScene.OpenGL.MakeCurrent();
                    this.axisScene.Draw();
                    var hdc = g.GetHdc();
                    this.axisScene.OpenGL.Blit(hdc);
                    g.ReleaseHdc(hdc);
                }
                //e.Graphics.DrawImage(imgAxies, 0, 0);
            }
        }

        public void ResetAxisRotation()
        {
            this.rotationEffect.ArcBall.ResetRotation();
        }
    }
}
