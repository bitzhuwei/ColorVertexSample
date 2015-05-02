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
    public partial class FormScientificVisual3DControl : Form
    {
        ArcBall2 modelArcBall;

        public FormScientificVisual3DControl()
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

            sceneControl.MouseDown += sceneControl_MouseDown;
            sceneControl.MouseMove += sceneControl_MouseMove;
            sceneControl.MouseUp += sceneControl_MouseUp;
            sceneControl.MouseWheel += sceneControl_MouseWheel;
        }

        private void ApplyCamera()
        {
            LookAtCamera camera = this.sceneControl.Scene.CurrentCamera as LookAtCamera;

            this.modelArcBall.Camera = camera;
            this.sceneControl.SetSceneCameraToUICamera();
        }

        private void sceneControl_MouseUp(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                ArcBall2 modelArcBall = this.modelArcBall;
                if (modelArcBall != null)
                {
                    modelArcBall.MouseUp(e.X, e.Y);
                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        private void sceneControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                ArcBall2 modelArcBall = this.modelArcBall;
                if (modelArcBall != null)
                {
                    modelArcBall.MouseMove(e.X, e.Y);
                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        private void sceneControl_MouseDown(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Right) == System.Windows.Forms.MouseButtons.Right)
            {
                int width = sceneControl.Width;
                int height = sceneControl.Height;

                ArcBall2 modelArcBall = this.modelArcBall;
                if (modelArcBall != null)
                {
                    modelArcBall.SetBounds(width, height);
                    modelArcBall.MouseDown(e.X, e.Y);
                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        private void sceneControl_MouseWheel(object sender, MouseEventArgs e)
        {
            ArcBall2 modelArcBall = this.modelArcBall;
            if (modelArcBall == null) { return; }

            modelArcBall.Scale += e.Delta * 0.001f;
            if (modelArcBall.Scale < 0.01f)
            { modelArcBall.Scale = 0.01f; }

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
                this.modelArcBall = element.modelArcBallEffect.ArcBall; 

                InitializeCamera(element, this.sceneControl);

                ApplyCamera();

                OpenGLAttributesEffect attr = InitializeAttributes(root);
                root.AddEffect(attr);

                scene.RenderBoundingVolumes = false;

                this.sceneControl.uiColorIndicator.Data.minValue = minValue;
                this.sceneControl.uiColorIndicator.Data.maxValue = maxValue;
                this.sceneControl.uiAxis.ResetRotation();

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
            element = PointModelElementFactory.Create(nx, ny, nz, radius, minValue, maxValue);

            parent.AddChild(element);

            return element;
        }

        private void lblDebugInfo_Click(object sender, EventArgs e)
        {
            this.tbRangeMin.Text = "-1000";
            this.tbRangeMax.Text = "1000";
        }
    }
}
