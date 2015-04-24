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
        //private const int defaultAxisSize = 80;
        private int axisWidth = 80;
        private int axisHeight = 80;
        private Scene axisScene = new Scene();
        private ArcBallEffect2 rotationEffect;
        //private Image imgAxis = new Bitmap(defaultAxisSize, defaultAxisSize);
        private LookAtCamera parallelCamera = new LookAtCamera();
        private Pen[] pens;
        private Vertex[] axisVertexes;
        private Vertex[] projectedAxisVertexes;
        private AxisSpy axisSpy;
        
        public void SetAxisSize(int width, int height)
        {
            //this.imgAxis = new Bitmap(width, height);
            axisWidth = width;
            axisHeight = height;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //using (var graphics = Graphics.FromImage(imgAxis))
            //{
            //    graphics.Clear(Color.FromArgb(0, 122, 122, 122));
            //}

            CreateOpenGL(axisScene);

            this.InitAxis(this.axisScene);

            InitParallelCamera();

            this.pens = new Pen[] { new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Blue) };
            this.axisVertexes = new Vertex[] { new Vertex(), new Vertex(600, 0, 0), new Vertex(0, 600, 0), new Vertex(0, 0, 600) };
            //this.axisVertexes = new Vertex[] { new Vertex(), new Vertex(0, -6, 0), new Vertex(-6, 0, 0), new Vertex(0, 0, -6) };
            this.projectedAxisVertexes = this.axisVertexes.ToArray();

            this.MouseDown += AxiesSceneControl_MouseDown;
            this.MouseMove += AxiesSceneControl_MouseMove;
            this.MouseUp += AxiesSceneControl_MouseUp;
            this.GDIDraw += AxiesSceneControl_GDIDraw;
        }

        private void InitParallelCamera()
        {
            //parallelCamera.AspectRatio = (double)imgAxis.Width / (double)imgAxis.Height;
            parallelCamera.AspectRatio = (double)axisWidth / (double)axisHeight;
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
                this.rotationEffect.ArcBall.SetCamera(parallelCamera);
            }
            //set the coordinate orignate to bottom left.

            //transform and flip y axis of the control world coordinate system.
            args.Graphics.TranslateTransform(0, this.Height);
            System.Drawing.Drawing2D.Matrix flip = new System.Drawing.Drawing2D.Matrix(1, 0, 0, -1, 0, 0);
            args.Graphics.MultiplyTransform(flip);

            //using (var graphics = Graphics.FromImage(imgAxis))
            {
                //graphics.Clear(Color.FromArgb(0, 122, 122, 122));
                this.axisScene.OpenGL.MakeCurrent();
                this.axisScene.Draw();
                //var handleDeviceContext = graphics.GetHdc();
                //this.axisScene.OpenGL.Blit(handleDeviceContext);
                //graphics.ReleaseHdc(handleDeviceContext);
                //TransformAxisVertex1Fail();
                //TransformAxisVertex2Fail();
                //TransformAxisVertex3Fail();
            }

            //args.Graphics.Clear(Color.FromArgb(255, 122, 122, 122));
            //args.Graphics.DrawImage(imgAxis, 0, 0);
            //for (int i = 1; i < this.projectedAxisVertexes.Length; i++)
            //{
            //    args.Graphics.DrawLine(this.pens[i - 1],
            //        this.projectedAxisVertexes[0].X,
            //        this.projectedAxisVertexes[0].Y + this.Height - imgAxis.Height,
            //        this.projectedAxisVertexes[i].X,
            //        this.projectedAxisVertexes[i].Y + this.Height - imgAxis.Height);
            //}
            var targets = this.axisSpy.projectedAxisVertexes;
            for (int i = 1; i < targets.Length; i++)
            {
                //args.Graphics.DrawLine(this.pens[i - 1],
                //    targets[0].X,
                //    targets[0].Y + this.Height - imgAxis.Height,
                //    targets[i].X,
                //    targets[i].Y + this.Height - imgAxis.Height);
                args.Graphics.DrawLine(this.pens[i - 1],
                    targets[0].X,
                    targets[0].Y,
                    targets[i].X,
                    targets[i].Y);
            }
        }

        //private void TransformAxisVertex3Fail()
        //{
        //    var projection = this.parallelCamera.projectionMatrix;
        //    var array = this.rotationEffect.ArcBall.GetRotation().to_array();
        //    var dArray = new double[array.Length];
        //    array.CopyTo(dArray, 0);
        //    var view = Matrix.FromColumnMajorArray(dArray, 4, 4);
        //    //var view = Matrix.FromRowMajorArray(dArray, 4, 4);
        //    for (int i = 0; i < this.projectedAxisVertexes.Length; i++)
        //    {
        //        this.projectedAxisVertexes[i] = projection * view * this.axisVertexes[i];
        //    }
        //}

        private void TransformAxisVertex2Fail()
        {
            var gl = this.axisScene.OpenGL;
            var view = this.rotationEffect.ArcBall.GetRotation();
            for (int i = 0; i < this.axisVertexes.Length; i++)
            {
                var rotated = new vec4(this.axisVertexes[i].X, this.axisVertexes[i].Y, this.axisVertexes[i].Z, 0);
                rotated = view * rotated;
                this.projectedAxisVertexes[i] = new Vertex(rotated.x, rotated.y, rotated.z);
            }
            for (int i = 0; i < this.projectedAxisVertexes.Length; i++)
            {
                this.projectedAxisVertexes[i] = gl.Project(this.projectedAxisVertexes[i]);
            }
        }

        private void TransformAxisVertex1Fail()
        {
            var view = this.rotationEffect.ArcBall.GetRotation();
            var projection = glm.perspective((float)parallelCamera.FieldOfView,
                (float)parallelCamera.AspectRatio,
                (float)parallelCamera.Near, (float)parallelCamera.Far);
            for (int i = 0; i < this.axisVertexes.Length; i++)
            {
                var rotated = new vec4(this.axisVertexes[i].X, this.axisVertexes[i].Y, this.axisVertexes[i].Z, 0);
                rotated = projection * view * rotated;
                this.projectedAxisVertexes[i] = new Vertex(rotated.x, rotated.y, rotated.z);
            }
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
            scene.ClearColor = Color.Gold;
            //  Create the 'Look At' camera
            this.parallelCamera = new LookAtCamera()
            {
                Position = new Vertex(0f, 0f, 7f),
                Target = new Vertex(0f, 0f, 0f),
                UpVector = new Vertex(0f, 1f, 0f)
            };

            //  Set the look at camera as the current camera.
            scene.CurrentCamera = this.parallelCamera;

            //  Add some design-time primitives.
            var folder = new Folder() { Name = "AxisLights" };
            scene.SceneContainer.AddChild(folder);
            this.rotationEffect = new ArcBallEffect2(this.parallelCamera);
            folder.AddEffect(this.rotationEffect);

            //Axies axies = new Axies();
            //folder.AddChild(new Axies());
            this.axisSpy = new AxisSpy();
            folder.AddChild(this.axisSpy);

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
