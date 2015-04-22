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

namespace ColorVertexSample
{
    public partial class MainView : Form
    {

        //private ArcBallEffect arcBallEffect = new ArcBallEffect();
        private CameraRotation cameraTransform;

        float? lookIncrement;
        //private AxisRotation axisRotation;
        //private AxisTranslation axisTranslation;
        //private LinearTransformationEffect axisTransform = new LinearTransformationEffect();
        private AxisTransformEffect axisTransform = new AxisTransformEffect();

        public MainView()
        {
            InitializeComponent();

        }

        private int ToInt(TextBox tb)
        {
            int value = System.Convert.ToInt32(tb.Text);
            return value;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.sceneControl1.Scene.SceneContainer.Children.Clear();

            this.sceneControl1.MouseWheel += sceneControl1_MouseWheel;
            this.sceneControl1.MouseDown += sceneControl1_MouseDown;
            this.sceneControl1.MouseMove += sceneControl1_MouseMove;
            this.sceneControl1.MouseUp += sceneControl1_MouseUp;
            this.sceneControl1.SizeChanged += sceneControl1_SizeChanged;
            this.SizeChanged += MainView_SizeChanged;
            Application.Idle += Application_Idle;
        }

        void MainView_SizeChanged(object sender, EventArgs e)
        {
            sceneControl1_SizeChanged(sender, e);
        }

        void sceneControl1_SizeChanged(object sender, EventArgs e)
        {
            this.cameraTransform.SetBounds(sceneControl1.Width, sceneControl1.Height);
            /*
              this.axisArcBallEffect.ArcBall.SetBounds(this.sceneControl.Width, this.sceneControl.Height);
            var gl = this.sceneControl.OpenGL;
            var axis = gl.UnProject(50, 50, 0.1);
            axisArcBallEffect.ArcBall.SetTranslate(axis[0], axis[1], axis[2]);
            axisArcBallEffect.ArcBall.Scale = 0.02f;
             */
            //UpdateAxisTransform();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            //var cameraTransform = this.cameraTransform;
            //if (cameraTransform == null) { return; }
            //this.lblDebugInfo.Text = cameraTransform.ToString();
            //UpdateAxisTransform();
        }

        private void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            //arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
            cameraTransform.MouseUp(e.X, e.Y);
        }

        private void sceneControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
                //arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
                cameraTransform.MouseMove(e.X, e.Y);

                //UpdateAxisTransform();

                this.sceneControl1.Invalidate();
            }
        }

        private void sceneControl1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                int width = sceneControl1.Width;
                int height = sceneControl1.Height;
                //arcBallEffect.ArcBall.SetBounds(width, height);
                //arcBallEffect.ArcBall.MouseDown(e.X,e.Y);
                cameraTransform.MouseDown(e.X, e.Y);
                cameraTransform.SetBounds(width, height);
            }
        }

        private void sceneControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!this.lookIncrement.HasValue)
                return;

            //scale by move the camera position
            float moveDirection = e.Delta > 0 ? 1 : -1;
            var camera = this.sceneControl1.Scene.CurrentCamera as LookAtCamera;
            Vertex direction = camera.Target - camera.Position;
            direction.Normalize();
            Vertex distance = direction * (moveDirection * this.lookIncrement.Value);
            camera.Position += distance;
            this.sceneControl1.Invalidate();

        }



        private float ToFloat(TextBox tb)
        {
            float value = System.Convert.ToSingle(tb.Text);
            return value;
        }

        private void output(ColorVertexes particles)
        {
            System.Console.WriteLine(String.Format("Particle Count:{0}", particles.Size));
            long size = particles.Size;
            for (long i = 0; i < size; i++)
            {
                unsafe
                {
                    Vertex p = particles.Centers[i];
                    System.Console.WriteLine(String.Format("P({0},{1},{2})", p.X, p.Y, p.Z));
                }

            }

            System.Console.WriteLine(String.Format("Particle Color:{0}", particles.Size));
            for (long i = 0; i < size; i++)
            {
                unsafe
                {
                    Model.Color color = particles.Colors[i];
                    System.Console.WriteLine(String.Format("rgb({0},{1},{2})", color.red, color.green, color.blue));
                }
            }
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

                var root = this.sceneControl1.Scene.SceneContainer;

                ClearChildren(root);

                ClearEffects(root);

                var colorVertexes = InitializeColorVertexesModel(nx, ny, nz, radius, minValue, maxValue, root);

                var camera = InitializeCamera(colorVertexes, this.sceneControl1);
                this.sceneControl1.Scene.CurrentCamera = camera;

                var axis = InitializeAxis(root);

                var attr = InitializeAttributes(root);

                this.sceneControl1.Scene.RenderBoundingVolumes = false;

                this.sceneControl1.Invalidate();

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
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

        /// <summary>
        /// 初始化坐标系 
        /// </summary>
        private SceneElement InitializeAxis(SceneElement parent)
        {
            var axisRoot = new SharpGL.SceneGraph.Primitives.Folder() { Name = "axis root" };
            parent.AddChild(axisRoot);

            //  Create light
            Light light1 = new Light()
            {
                Name = "Light 1",
                On = true,
                Position = new Vertex(-9, -9, 11),
                GLCode = OpenGL.GL_LIGHT0
            };

            axisRoot.AddChild(light1);
            // this light only light up the axis
            DoInitAxis(light1);

            //this.axisRotation = new AxisRotation(axisRoot);
            //this.axisTranslation = new AxisTranslation(axisRoot);
            axisRoot.AddEffect(this.axisTransform);

            return axisRoot;
        }

        private void DoInitAxis(SceneElement parent)
        {
            const float factor = 1;
            // X轴
            Material red = new Material();
            red.Emission = System.Drawing.Color.Red;
            red.Diffuse = System.Drawing.Color.Red;

            Cylinder x1 = new Cylinder() { Name = "X1" };
            x1.BaseRadius = 0.05 * factor;
            x1.TopRadius = 0.05 * factor;
            x1.Height = 1.5 * factor;
            x1.Transformation.RotateY = 90f;
            x1.Material = red;
            parent.AddChild(x1);

            Cylinder x2 = new Cylinder() { Name = "X2" };
            x2.BaseRadius = 0.1 * factor;
            x2.TopRadius = 0 * factor;
            x2.Height = 0.2 * factor;
            x2.Transformation.TranslateX = 1.5f * factor;
            x2.Transformation.RotateY = 90f;
            x2.Material = red;
            parent.AddChild(x2);

            // Y轴
            Material green = new Material();
            green.Emission = System.Drawing.Color.Green;
            green.Diffuse = System.Drawing.Color.Green;

            Cylinder y1 = new Cylinder() { Name = "Y1" };
            y1.BaseRadius = 0.05 * factor;
            y1.TopRadius = 0.05 * factor;
            y1.Height = 1.5 * factor;
            y1.Transformation.RotateX = -90f;
            y1.Material = green;
            parent.AddChild(y1);

            Cylinder y2 = new Cylinder() { Name = "Y2" };
            y2.BaseRadius = 0.1 * factor;
            y2.TopRadius = 0 * factor;
            y2.Height = 0.2 * factor;
            y2.Transformation.TranslateY = 1.5f * factor;
            y2.Transformation.RotateX = -90f;
            y2.Material = green;
            parent.AddChild(y2);

            // Z轴
            Material blue = new Material();
            blue.Emission = System.Drawing.Color.Blue;
            blue.Diffuse = System.Drawing.Color.Blue;

            Cylinder z1 = new Cylinder() { Name = "Z1" };
            z1.BaseRadius = 0.05 * factor;
            z1.TopRadius = 0.05 * factor;
            z1.Height = 1.5 * factor;
            z1.Material = blue;
            parent.AddChild(z1);

            Cylinder z2 = new Cylinder() { Name = "Z2" };
            z2.BaseRadius = 0.1 * factor;
            z2.TopRadius = 0 * factor;
            z2.Height = 0.2 * factor;
            z2.Transformation.TranslateZ = 1.5f * factor;
            z2.Material = blue;
            parent.AddChild(z2);
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
        private ColorVertexes InitializeColorVertexesModel(int nx, int ny, int nz, float radius, float minValue, float maxValue, SceneElement parent)
        {
            var colorVertexes = ColorVertexesFactory.Create(nx, ny, nz, radius, minValue, maxValue);

            var visualElement = new ColorVertexesElement(colorVertexes);

            parent.AddChild(visualElement);

            return colorVertexes;
        }

        private void ClearChildren(SceneElement target)
        {
            var elements = new SceneElement[target.Children.Count];
            target.Children.CopyTo(elements, 0);
            foreach (var item in elements)
            {
                target.RemoveChild(item);
            }
        }

        private static void ClearEffects(SceneElement target)
        {
            var effects = new Effect[target.Effects.Count];
            foreach (var item in effects)
            {
                target.RemoveEffect(item);
            }
        }

        private Camera InitializeCamera(ColorVertexes colorVertexes, SceneControl control)
        {
            var rect3D = colorVertexes.Bounds;
            float centerX = rect3D.X + rect3D.Size.x / 2.0f;
            float centerY = rect3D.Y + rect3D.Size.y / 2.0f;
            float centerZ = rect3D.Z + rect3D.Size.z / 2.0f;

            float size = Math.Max(Math.Max(rect3D.Size.x, rect3D.Size.y), rect3D.Size.z);

            Vertex center = new Vertex(centerX, centerY, centerZ);
            Vertex position = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 2);
            //Vertex PositionNear = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 0.5f);

            this.lookIncrement = size * 0.1f;

            var lookAtCamera = new LookAtCamera()
            {
                Position = position,
                Target = center,
                UpVector = new Vertex(0f, 1f, 0f),
                FieldOfView =  60,
                AspectRatio = (double)control.Width / (double)control.Height,//1.0f,
                Near = 0.001,//(PositionNear - center).Magnitude(),
                Far = float.MaxValue
            };

            this.cameraTransform = new CameraRotation(lookAtCamera);

            return lookAtCamera;
        }


        private void lblDebugInfo_Click(object sender, EventArgs e)
        {
            this.tbRangeMin.Text = "-1000";
            this.tbRangeMax.Text = "2000";
        }

    }
}
