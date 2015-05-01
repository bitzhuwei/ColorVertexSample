using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorVertexSample.Model;
using ColorVertexSample.Visual;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneComponent;

namespace ColorVertexSample
{
    public partial class FormFreeCamera : Form
    {
        private CameraRotation cameraRotation;
        private ArcBallEffect2 modelArcBallEffect;
        private OrthoColorIndicatorElement orthoColorIndicatorElement;
        private OpenGLUIAxis uiAxis;

        public FormFreeCamera()
        {
            InitializeComponent();

            this.Text = "Rotation tip: left mouse for camera & right mouse for model";
        }

        private int ToInt(TextBox tb)
        {
            int value = System.Convert.ToInt32(tb.Text);
            return value;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.sceneControl.Scene.SceneContainer.Children.Clear();
            this.sceneControl.Scene.SceneContainer.Effects.Clear();

            sceneControl.MouseDown += sceneControl_MouseDown;
            sceneControl.MouseMove += sceneControl_MouseMove;
            sceneControl.MouseUp += sceneControl_MouseUp;
            sceneControl.MouseWheel += sceneControl_MouseWheel;
        }

        private void ApplyCamera()
        {
            LookAtCamera camera = this.sceneControl.Scene.CurrentCamera as LookAtCamera;

            // This means the same:
            // this.cameraRotation = new CameraRotation() { LookAtCamera = camera };
            this.cameraRotation = new CameraRotation(camera);
            this.modelArcBallEffect.ArcBall.Camera = camera;
            this.orthoColorIndicatorElement.bar.scaleEffect.Camera = camera;
            this.uiAxis.Camera = camera;
        }

        private void Initialize2DUI(SceneContainer parent)
        {
            ColorTemplate colorTemplate = ColorTemplateFactory.CreateRainbow();
            OrthoColorIndicatorElement orthoColorIndicatorElement = OrthoColorIndicatorElementFactory.Create(colorTemplate);
            //orthoColorIndicatorElement.scaleEffect.Margin=...
            //parent.AddChild(orthoColorIndicatorElement);
            this.orthoColorIndicatorElement = orthoColorIndicatorElement;

            OpenGLUIAxis uiAxis = new OpenGLUIAxis() { Name = "UI: Axis", Anchor = AnchorStyles.Left | AnchorStyles.Bottom, Margin = new Padding(5, 0, 0, 20), Size = new Size(40, 40), };
            parent.AddChild(uiAxis);
            this.uiAxis = uiAxis;
            OpenGLUIColorIndicator uiColorIndicator = new OpenGLUIColorIndicator() { Name = "UI: Color Indicator", Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right, Margin = new Padding(5 + 40 + 8, 0, 40, 20), Size = new Size(100, 35), };
            parent.AddChild(uiColorIndicator);
        }

        private void sceneControl_MouseUp(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                CameraRotation cameraRotation = this.cameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.MouseUp(e.X, e.Y);
                    render = true;
                }
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                ArcBallEffect2 modelArcBallEffect = this.modelArcBallEffect;
                if (modelArcBallEffect != null)
                {
                    modelArcBallEffect.ArcBall.MouseUp(e.X, e.Y);
                    render = true;
                }

                OpenGLUIAxis uiAxis = this.uiAxis;
                if (uiAxis != null)
                {
                    uiAxis.MouseUp(e.X, e.Y);
                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        private void sceneControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool render = false;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                CameraRotation cameraRotation = this.cameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.MouseMove(e.X, e.Y);
                    render = true;
                }
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                ArcBallEffect2 modelArcBallEffect = this.modelArcBallEffect;
                if (modelArcBallEffect != null)
                {
                    modelArcBallEffect.ArcBall.MouseMove(e.X, e.Y);
                    render = true;
                }

                OpenGLUIAxis uiAxis = this.uiAxis;
                if (uiAxis != null)
                {
                    uiAxis.MouseMove(e.X, e.Y);
                    render = true;
                } 
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        private void sceneControl_MouseDown(object sender, MouseEventArgs e)
        {
            bool render = false;
            if ((e.Button & MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                CameraRotation cameraRotation = this.cameraRotation;
                if (cameraRotation != null)
                {
                    int width = sceneControl.Width;
                    int height = sceneControl.Height;

                    cameraRotation.SetBounds(width, height);
                    cameraRotation.MouseDown(e.X, e.Y);

                    render = true;
                } 
            }

            if ((e.Button & MouseButtons.Right) == System.Windows.Forms.MouseButtons.Right)
            {
                int width = sceneControl.Width;
                int height = sceneControl.Height; 
                
                ArcBallEffect2 modelArcBallEffect = this.modelArcBallEffect;
                if (modelArcBallEffect != null)
                {
                    modelArcBallEffect.ArcBall.SetBounds(width, height);
                    modelArcBallEffect.ArcBall.MouseDown(e.X, e.Y);
                    render = true;
                }

                OpenGLUIAxis uiAxis = this.uiAxis;
                if (uiAxis != null)
                {
                    uiAxis.SetBounds(width, height);
                    uiAxis.MouseDown(e.X, e.Y);
                    render = true;
                } 
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        private void sceneControl_MouseWheel(object sender, MouseEventArgs e)
        {
            ArcBallEffect2 modelArcBallEffect = this.modelArcBallEffect;
            if (modelArcBallEffect == null) { return; }

            modelArcBallEffect.ArcBall.Scale += e.Delta * 0.001f;
            if (modelArcBallEffect.ArcBall.Scale < 0.01f)
            { modelArcBallEffect.ArcBall.Scale = 0.01f; }

            ManualRender(this.sceneControl);
        }

        private float ToFloat(TextBox tb)
        {
            float value = System.Convert.ToSingle(tb.Text);
            return value;
        }

        private void Create3DObject(object sender, EventArgs e)
        {
            try
            {
                int nx = this.ToInt(this.tbNX);
                int ny = this.ToInt(this.tbNY);
                int nz = this.ToInt(this.tbNZ);
                float radius = this.ToFloat(this.tbRadius);
                float minValue = this.ToFloat(this.tbRangeMin);
                float maxValue = this.ToFloat(this.tbRangeMax);
                if (minValue >= maxValue)
                    throw new ArgumentException("min value equal or equal to maxValue");

                Scene scene = this.sceneControl.Scene;
                SceneContainer root = scene.SceneContainer;

                root.Children.Clear();
                root.Effects.Clear();

                PointModelElement element = InitializeElement(nx, ny, nz, radius, minValue, maxValue, root);

                Initialize2DUI(root);

                InitializeCamera(element, this.sceneControl);

                ApplyCamera();

                OpenGLAttributesEffect attr = InitializeAttributes(root);
                root.AddEffect(attr);

                scene.RenderBoundingVolumes = false;

                this.orthoColorIndicatorElement.number.SetBound(minValue, maxValue);
                this.orthoColorIndicatorElement.number.SetControl(this.sceneControl);

                ManualRender(this.sceneControl);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void ManualRender(Control control)
        {
            control.Invalidate();
        }

        private OpenGLAttributesEffect InitializeAttributes(SceneElement parent)
        {
            //  Create a set of scene attributes.
            OpenGLAttributesEffect sceneAttributes = new OpenGLAttributesEffect()
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
            sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
            parent.AddEffect(sceneAttributes);

            return sceneAttributes;
        }

        private void InitializeCamera(PointModelElement element, MySceneControl control)
        {
            PointModel model = element.Model;
            Rect3D rect3D = model.Bounds;
            Vertex center = model.WorldCoordCenter();

            float size = Math.Max(Math.Max(rect3D.Size.x, rect3D.Size.y), rect3D.Size.z);

            Vertex position = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 2);
            //Vertex PositionNear = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 0.5f);
            LookAtCamera camera = control.Scene.CurrentCamera as LookAtCamera;

            camera.Position = position;
            camera.Target = center;
            camera.UpVector = new Vertex(0f, 1f, 0f);
            camera.FieldOfView = 60;
            camera.AspectRatio = (double)control.Width / (double)control.Height;
            camera.Near = 0.001;
            camera.Far = float.MaxValue;
        }

        /// <summary>
        /// 生成需要画的模型
        /// </summary>
        /// <param name="nx"></param>
        /// <param name="ny"></param>
        /// <param name="nz"></param>
        /// <param name="radius"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        private PointModelElement InitializeElement(int nx, int ny, int nz, float radius, float minValue, float maxValue, SceneElement parent)
        {
            PointModel model = PointModelFactory.Create(nx, ny, nz, radius, minValue, maxValue);

            PointModelElement element = new PointModelElement(model);

            parent.AddChild(element);

            Axies axies = new Axies();
            LinearTransformationEffect effect = new LinearTransformationEffect();
            float length = maxValue - minValue;
            effect.LinearTransformation.ScaleX = length;
            effect.LinearTransformation.ScaleY = length;
            effect.LinearTransformation.ScaleZ = length;
            axies.AddEffect(effect);
            element.AddChild(axies);

            ArcBallEffect2 modelArcBallEffect = new ArcBallEffect2();
            modelArcBallEffect.ArcBall.Translate = element.Model.translateVector;
            element.AddEffect(modelArcBallEffect);

            this.modelArcBallEffect = modelArcBallEffect;

            return element;
        }

        private void lblDebugInfo_Click(object sender, EventArgs e)
        {
            this.tbRangeMin.Text = "-1000";
            this.tbRangeMax.Text = "1000";
        }
    }
}
