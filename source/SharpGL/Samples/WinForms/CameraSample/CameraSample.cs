using CameraSample.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraSample
{
    public partial class CameraSample : Form
    {

        private ModelSpace modelSpace = new ModelSpace() { Left = -10000.0f, Right = 10000.0f, Top = 10000.0f, Bottom = -10000.0f,Near=10000.0f,Far=-10000.0f };

        private ArcBallEffect arcBallEffect = new ArcBallEffect();

        public CameraSample()
        {
            InitializeComponent();
           
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //fix render Context Provider
            this.sceneControl1.Scene.OpenGL = this.sceneControl1.OpenGL;



            //clear the default scene
            this.sceneControl1.Scene.SceneContainer.Children.Clear();

            //create the default camera
            OrthographicCamera orthCamera = new OrthographicCamera();
            this.comboBox1.Items.Add(orthCamera);
            Size clientSize = this.sceneControl1.ClientSize;
            orthCamera.Left = this.modelSpace.Left;
            orthCamera.Right = this.modelSpace.Right;
            orthCamera.Bottom = this.modelSpace.Bottom;
            orthCamera.Top = this.modelSpace.Top;
            orthCamera.Near = this.modelSpace.Near;
            orthCamera.Far = this.modelSpace.Far;
            orthCamera.Position = new Vertex(0.0f, 0.0f, 0.0f);
            orthCamera.AspectRatio = 1.0f;



            LookAtCamera persCamera = new LookAtCamera();
            this.comboBox1.Items.Add(persCamera);
            persCamera.Position = new Vertex(0.0f, 0.0f, (float)(modelSpace.Near+modelSpace.Near));
            persCamera.Near = Math.Abs(modelSpace.Near);
            persCamera.Far = float.MaxValue;
            persCamera.Target = new Vertex(0, 0, 0);
            persCamera.UpVector = new Vertex(0, 1, 0);


            //create model
            BoundingBox space = new BoundingBox();
            space.Name = "Virtual Space";
            space.LBN = new Vertex(-5000, -5000, 5000);
            space.RTF = new Vertex(5000, 5000, -5000);
            this.sceneControl1.Scene.SceneContainer.AddChild(space);
            space.AddEffect(arcBallEffect);


            BoundingBox volume1 = new BoundingBox();
            volume1.Name = "volume1";
            volume1.LBN = new Vertex(-4000, -2000, 4000);
            volume1.RTF = new Vertex(-3500, -1500, 3500); //size is 5000
            space.AddChild(volume1);


            BoundingBox volume2 = new BoundingBox();
            volume2.Name = "volume2";
            volume2.LBN = new Vertex(-2000, -2000, 2000);
            volume2.RTF = new Vertex(2000, 2000, -2000);
            space.AddChild(volume2);

            this.comboBox1.SelectedIndex = 0;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera camera = this.comboBox1.SelectedItem as Camera;

            this.sceneControl1.Scene.CurrentCamera = camera;
            this.sceneControl1.Invalidate();


        }

        private void sceneControl1_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
        }

        private void sceneControl1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
            }
        }

        private void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        }
    }
}
