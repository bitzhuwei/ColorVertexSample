using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL.SceneGraph.Effects;
using SharpGL.Enumerations;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Cameras;

namespace _2DOverlaySample
{
    public partial class FormScene : Form
    {
        CameraRotation cameraRotation;
        //private OrthoArcBallEffect orthoAxisArcBallEffect;
        ArcBallEffect2 modelArcBallEffect;
        private OrthoAxisElement orthoAxisElement;

        public FormScene()
        {
            InitializeComponent();
        }

        private void FormScene_Load(object sender, EventArgs e)
        {
            var scene = this.sceneControl.Scene;
            var root = scene.SceneContainer;

            var children = root.Children.ToArray();
            foreach (var item in children)
            {
                root.RemoveChild(item);
            }

            InitializeCamera(this.sceneControl);

            InitializeModel(root);

            Initialize2DUI(root);

            ApplyCamera();

            sceneControl.MouseDown += sceneControl_MouseDown;
            sceneControl.MouseMove += sceneControl_MouseMove;
            sceneControl.MouseUp += sceneControl_MouseUp;
            sceneControl.MouseWheel += sceneControl_MouseWheel;
        }

        private void ApplyCamera()
        {
            var camera = this.sceneControl.Scene.CurrentCamera as LookAtCamera;


            this.cameraRotation = new CameraRotation();
            this.cameraRotation.LookAtCamera = camera;
            this.modelArcBallEffect.ArcBall.Camera = camera;
            this.orthoAxisElement.orthoArcBallEffect.Camera = camera;
        }

        private void InitializeCamera(SharpGL.SceneControl sceneControl)
        {
            var scene = sceneControl.Scene;
            var camera = scene.CurrentCamera as LookAtCamera;
            camera.Near = 0.0001;
            camera.Far = 1000000;
            camera.Position = new SharpGL.SceneGraph.Vertex(50, 0, 50);
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
        }

        void sceneControl_MouseWheel(object sender, MouseEventArgs e)
        {
            const float factor = 10;
            //scale by move the camera position
            float moveDirection = e.Delta > 0 ? 1 : -1;
            var camera = this.sceneControl.Scene.CurrentCamera as LookAtCamera;
            var direction = camera.Target - camera.Position;
            direction.Normalize();
            var distance = direction * (moveDirection * factor);
            camera.Position += distance;
            this.sceneControl.Invalidate();
        }

        void sceneControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.cameraRotation != null)
                {
                    this.cameraRotation.MouseUp(e.X, e.Y);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.SetCamera(
                        this.sceneControl.Scene.CurrentCamera as LookAtCamera);
                    this.Text = string.Format("Left Mouse Up {0},{1},{2}",
                        this.cameraRotation.isDown,
                        this.orthoAxisElement.orthoArcBallEffect.arcBall.mouseDownFlag,
                        this.modelArcBallEffect.ArcBall.mouseDownFlag);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.modelArcBallEffect != null)
                {
                    this.modelArcBallEffect.ArcBall.MouseUp(e.X, e.Y);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.MouseUp(e.X, e.Y);
                    this.Text = string.Format("Right Mouse Up {0},{1},{2}",
                        this.cameraRotation.isDown,
                        this.orthoAxisElement.orthoArcBallEffect.arcBall.mouseDownFlag,
                        this.modelArcBallEffect.ArcBall.mouseDownFlag);
                }
            }
        }

        void sceneControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.cameraRotation != null)
                {
                    this.cameraRotation.MouseMove(e.X, e.Y);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.SetCamera(
                           this.sceneControl.Scene.CurrentCamera as LookAtCamera);
                    this.Text = string.Format("Left Mouse Move {0},{1},{2}",
                        this.cameraRotation.isDown,
                        this.orthoAxisElement.orthoArcBallEffect.arcBall.mouseDownFlag,
                        this.modelArcBallEffect.ArcBall.mouseDownFlag);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.modelArcBallEffect != null)
                {
                    this.modelArcBallEffect.ArcBall.MouseMove(e.X, e.Y);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.MouseMove(e.X, e.Y);
                    this.Text = string.Format("Right Mouse Move {0},{1},{2}",
                        this.cameraRotation.isDown,
                        this.orthoAxisElement.orthoArcBallEffect.arcBall.mouseDownFlag,
                        this.modelArcBallEffect.ArcBall.mouseDownFlag); ;
                }
            }
        }

        void sceneControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.cameraRotation != null)
                {
                    this.cameraRotation.SetBounds(this.sceneControl.Width, this.sceneControl.Height);
                    if (this.cameraRotation != null) this.cameraRotation.MouseDown(e.X, e.Y);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.SetCamera(
           this.sceneControl.Scene.CurrentCamera as LookAtCamera);
                    this.Text = string.Format("Left Mouse Down {0},{1},{2}",
                        this.cameraRotation.isDown,
                        this.orthoAxisElement.orthoArcBallEffect.arcBall.mouseDownFlag,
                        this.modelArcBallEffect.ArcBall.mouseDownFlag);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.modelArcBallEffect != null)
                {
                    this.modelArcBallEffect.ArcBall.SetBounds(this.sceneControl.Width, this.sceneControl.Height);
                    this.modelArcBallEffect.ArcBall.MouseDown(e.X, e.Y);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.SetBounds(this.sceneControl.Width, this.sceneControl.Height);
                    this.orthoAxisElement.orthoArcBallEffect.arcBall.MouseDown(e.X, e.Y);
                    this.Text = string.Format("Right Mouse Down {0},{1},{2}",
                        this.cameraRotation.isDown,
                        this.orthoAxisElement.orthoArcBallEffect.arcBall.mouseDownFlag,
                        this.modelArcBallEffect.ArcBall.mouseDownFlag);
                }
            }
        }

        private void Initialize2DUI(SharpGL.SceneGraph.Core.SceneElement parent)
        {
            var orthoAxisElement = OrthoAxisElementFactory.Create();
            parent.AddChild(orthoAxisElement);
            this.orthoAxisElement = orthoAxisElement;
        }

        private void InitializeModel(SharpGL.SceneGraph.Core.SceneElement parent)
        {
            var modelRoot = new Folder() { Name = "model root" };
            parent.AddChild(modelRoot);

            var camera = this.sceneControl.Scene.CurrentCamera as LookAtCamera;
            var modelArcBallEffect = new ArcBallEffect2();
            modelRoot.AddEffect(modelArcBallEffect);
            this.modelArcBallEffect = modelArcBallEffect;
            //this.modelArcBallEffect = null;

            var modelAxis = new Axies();
            modelRoot.AddChild(modelAxis);
            var modelGrid = new Grid();
            modelRoot.AddChild(modelGrid);

            var modelScaleEffect = new LinearTransformationEffect();
            modelScaleEffect.LinearTransformation.ScaleX = 1000;
            modelScaleEffect.LinearTransformation.ScaleY = 1000;
            modelScaleEffect.LinearTransformation.ScaleZ = 1000;
            modelAxis.AddEffect(modelScaleEffect);

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
            modelRoot.AddEffect(sceneAttributes);
        }
    }
}
