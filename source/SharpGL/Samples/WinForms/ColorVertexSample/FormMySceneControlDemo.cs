using SharpGL.Enumerations;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Effects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorVertexSample
{
    public partial class FormMySceneControlDemo : Form
    {
        private ModelContainer modelContainer;

        private SatelliteRotation cameraRotation;

        public SatelliteRotation CameraRotation
        {
            get { return cameraRotation; }
            set { cameraRotation = value; }
        }
    
        public FormMySceneControlDemo()
        {
            InitializeComponent();
         
            InitializeSceneControl();

        }

        private void InitializeSceneControl()
        {
            var root = this.sceneControl.Scene.SceneContainer;
            root.Children.Clear();
            root.Effects.Clear();
            //InitializeSceneAttributes(root);
            this.modelContainer = new ModelContainer();
            this.modelContainer.RenderBoundingBox = false;
            root.AddChild(this.modelContainer);

            {
                var pointModel = ColorVertexSample.Model.PointModel.Create(100, 100, 100, 0, -5, 5);
                ScientificModelElement element = new ScientificModelElement(pointModel);
                this.modelContainer.AddChild(element);
                this.modelContainer.BoundingBox.Extend(pointModel.BoundingBox.MaxPosition);
                this.modelContainer.BoundingBox.Extend(pointModel.BoundingBox.MinPosition);
            }

            // Diff: MySceneControl don't need this.
            //var camera = new ScientificCamera()
            //{
            //    Position = new Vertex(-10f, -10f, 10f),
            //    Target = new Vertex(0f, 0f, 0f),
            //    UpVector = new Vertex(0f, 0f, 1f)
            //};
            //this.sceneControl.Scene.CurrentCamera = camera;

            this.cameraRotation = new SatelliteRotation();
            this.cameraRotation.Camera = this.sceneControl.Scene.CurrentCamera as ScientificCamera;

            this.sceneControl.MouseDown += ScientificVisual3DControl_MouseDown;
            this.sceneControl.MouseMove += ScientificVisual3DControl_MouseMove;
            this.sceneControl.MouseUp += ScientificVisual3DControl_MouseUp;
            this.sceneControl.MouseWheel += ScientificVisual3DControl_MouseWheel;
            this.sceneControl.Resized += ScientificVisual3DControl_Resized;
        }

        void ScientificVisual3DControl_Resized(object sender, EventArgs e)
        {
            //this.modelContainer.AdjustCamera(this.OpenGL, this.Scene.CurrentCamera);
            //CameraHelper.AdjustCamera(this.modelContainer.BoundingBox, this.sceneControl.OpenGL, this.sceneControl.Scene.CurrentCamera);
            this.sceneControl.Scene.CurrentCamera.AdjustCamera(this.modelContainer.BoundingBox, this.sceneControl.OpenGL);
            ManualRender(this.sceneControl);
        }

        void ScientificVisual3DControl_MouseWheel(object sender, MouseEventArgs e)
        {
            ScientificCamera camera = this.sceneControl.Scene.CurrentCamera;
            //if (camera == null) { return; }

            camera.Scale(e.Delta);

            ManualRender(this.sceneControl);
        }

        void ScientificVisual3DControl_MouseUp(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                var rotation = this.CameraRotation;
                if (rotation != null)
                {
                    rotation.MouseUp(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        void ScientificVisual3DControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool render = false;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                var cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.MouseMove(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }

        void ScientificVisual3DControl_MouseDown(object sender, MouseEventArgs e)
        {
            bool render = false;

            if ((e.Button & MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                var cameraRotation = this.CameraRotation;
                if (cameraRotation != null)
                {
                    cameraRotation.SetBounds(this.sceneControl.Width, this.sceneControl.Height);
                    cameraRotation.MouseDown(e.X, e.Y);

                    render = true;
                }
            }

            if (render)
            { ManualRender(this.sceneControl); }
        }



        private void ManualRender(Control control)
        {
            control.Invalidate();// this will invokes OnPaint(PaintEventArgs e);
        }
        

        private OpenGLAttributesEffect InitializeSceneAttributes(SceneElement parent)
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
            //sceneAttributes.EnableAttributes.EnableBlend = true;
            sceneAttributes.ColorBufferAttributes.BlendingSourceFactor = BlendingSourceFactor.SourceAlpha;
            sceneAttributes.ColorBufferAttributes.BlendingDestinationFactor = BlendingDestinationFactor.OneMinusSourceAlpha;
            sceneAttributes.LightingAttributes.TwoSided = true;
            sceneAttributes.LightingAttributes.AmbientLight = new GLColor(1, 1, 1, 1);
            parent.AddEffect(sceneAttributes);

            return sceneAttributes;
        }

        private void rdoPerspective_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoPerspective.Checked)
            {
                //this.sceneControl.CameraType = ECameraType.Perspecitive;
                var camera  = this.sceneControl.Scene.CurrentCamera as ScientificCamera;
                camera.CameraType = ECameraType.Perspecitive;
                ManualRender(this.sceneControl);
            }
        }

        private void rdoOrtho_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoOrtho.Checked)
            {
                //this.sceneControl.CameraType = ECameraType.Ortho;
                var camera = this.sceneControl.Scene.CurrentCamera as ScientificCamera;
                camera.CameraType = ECameraType.Ortho;
                ManualRender(this.sceneControl);
            }
        }

        private void btnManualRender_Click(object sender, EventArgs e)
        {
            ManualRender(this.sceneControl);
        }

        private void FormMySceneControlDemo_Load(object sender, EventArgs e)
        {
            List<ScientificModelElement.Order> orders = new List<ScientificModelElement.Order>()
            {
                ScientificModelElement.Order.ModelBoundingBox, 
                ScientificModelElement.Order.BoundingBoxModel 
            };
            foreach (var item in orders)
            {
                this.cmbRenderOrder.Items.Add(item);
            }

            this.ScientificVisual3DControl_Resized(sceneControl, e);
        }

        private void cmbRenderOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScientificModelElement.Order order = (ScientificModelElement.Order)
                this.cmbRenderOrder.SelectedItem;
            foreach (var item in this.modelContainer.Children)
            {
                ScientificModelElement element = item as ScientificModelElement;
                if (element != null)
                {
                    element.RenderOrder = order;
                }
            }
            ManualRender(this.sceneControl);
        }

    }
}
