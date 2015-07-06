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
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneComponent;
using GeometryModel;
using GeometryModel.Gridder;
using GeometryModel.Builder;

namespace ColorVertexSample
{
    public partial class FormHexahedronGridder : Form
    {
        public FormHexahedronGridder()
        {
            InitializeComponent();

            InitilizeViewTypeControl();

            Application.Idle += Application_Idle;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            IPickedGeometry picked = this.scientificVisual3DControl.PickedPrimitive;
            this.lblPickedPrimitive.Text = string.Format("Picked:{0}", picked);
            this.lblPickingInfo.Text = string.Format("Picked:{0}", picked);
        }

        private void InitilizeViewTypeControl()
        {
            foreach (string item in Enum.GetNames(typeof(ViewTypes)))
            {
                this.cmbViewType.Items.Add(item);
            }

            foreach (string item in Enum.GetNames(typeof(CameraTypes)))
            {
                this.cmbCameraType.Items.Add(item);
            }
        }

        int elementCounter = 0;
        private void Create3DObject(object sender, EventArgs e)
        {
            try
            {
                int nx = System.Convert.ToInt32(tbNX.Text);
                int ny = System.Convert.ToInt32(tbNY.Text);
                int nz = System.Convert.ToInt32(tbNZ.Text);
                float step = System.Convert.ToSingle(tbColorIndicatorStep.Text);
                float radius = System.Convert.ToSingle(this.tbRadius.Text);
                //float minValue = System.Convert.ToSingle(this.tbRangeMin.Text);
                //float maxValue = System.Convert.ToSingle(this.tbRangeMax.Text);
                //if (minValue >= maxValue)
                //throw new ArgumentException("min value equal or equal to maxValue");

                //int vertexCount = nx * ny * nz;
                //Vertex min = new Vertex(minValue, minValue, minValue);
                //Vertex max = new Vertex(maxValue, maxValue, maxValue);

                //ScientificModel model = new ScientificModel(vertexCount, BeginMode.Points);
                //model.Build(min, max);

                // use HexahedronGridder to fill ScientificModel's content.
                float dx = System.Convert.ToSingle(this.tbDX.Text);
                float dy = System.Convert.ToSingle(this.gbDY.Text);
                float dz = System.Convert.ToSingle(this.tbDZ.Text);
                CatesianGridderSource catesianSource = new CatesianGridderSource();

                catesianSource.NX = nx;
                catesianSource.NY = ny;
                catesianSource.NZ = nz;
                catesianSource.DX = dx;
                catesianSource.DY = dy;
                catesianSource.DZ = dz;

                HexahedronGridder gridder = HexahedronGridderBuilder.BuildGridder(catesianSource);
                ScientificModel model = HexahedronGridderHelper.GetModel(gridder);

                //ScientificModel model = new ScientificModel()

                //this.sceneControl.AddScientificModel(model);// This is replaced by codes below.
                ScientificModelElement element = new ScientificModelElement(
                    model, this.scientificVisual3DControl.Scene.CurrentCamera);
                element.Name = string.Format("element {0}", elementCounter++);
                this.scientificVisual3DControl.AddModelElement(element);
                // update ModelContainer's BoundingBox.
                BoundingBox boundingBox = this.scientificVisual3DControl.ModelContainer.BoundingBox;
                IBoundingBox modelBoundingBox = model.BoundingBox;
                if (this.scientificVisual3DControl.ModelContainer.Children.Count > 1)
                {
                    boundingBox.Extend(modelBoundingBox.MinPosition);
                    boundingBox.Extend(modelBoundingBox.MaxPosition);
                }
                else
                {
                    boundingBox.Set(modelBoundingBox.MinPosition.X,
                        modelBoundingBox.MinPosition.Y,
                        modelBoundingBox.MinPosition.Z,
                        modelBoundingBox.MaxPosition.X,
                        modelBoundingBox.MaxPosition.Y,
                        modelBoundingBox.MaxPosition.Z);
                }
                boundingBox.Expand();

                // update ViewType to UserView.
                this.scientificVisual3DControl.ViewType = ViewTypes.UserView;

                //this.scientificVisual3DControl.SetColorIndicator(minValue, maxValue, step);
                var min = model.BoundingBox.MinPosition.MinField();
                var max = model.BoundingBox.MaxPosition.MaxField();
                this.scientificVisual3DControl.SetColorIndicator(min, max, step);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private List<string> rangeMin = new List<string>() { "-1000", "1100", "3200" };
        private List<string> rangeMax = new List<string>() { "1000", "3100", "5200" };
        private List<string> stepList = new List<string>() { "110", "110", "100" };
        private int testCaseIndex = 0;

        /// <summary>
        /// quick way to set min and max value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDebugInfo_Click(object sender, EventArgs e)
        {
            //this.tbRangeMax.Text = rangeMax[testCaseIndex];
            //this.tbRangeMin.Text = rangeMin[testCaseIndex];
            this.tbColorIndicatorStep.Text = stepList[testCaseIndex];
            testCaseIndex = testCaseIndex >= rangeMin.Count - 1 ? 0 : testCaseIndex + 1;
        }

        private void btnClearModels_Click(object sender, EventArgs e)
        {
            this.scientificVisual3DControl.ClearScientificModels();
        }

        private void lblDebugInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bool depthTest = this.scientificVisual3DControl.OpenGL.IsEnabled(OpenGL.GL_DEPTH_TEST);
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("depth test: {0}", depthTest ? "enabled" : "disabled"));
                MessageBox.Show(builder.ToString());
            }
        }

        private void chkRenderContainerBox_CheckedChanged(object sender, EventArgs e)
        {
            this.scientificVisual3DControl.RenderBoundingBox = this.chkRenderContainerBox.Checked;
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbViewType.SelectedItem.ToString();
            ViewTypes viewType = (ViewTypes)Enum.Parse(typeof(ViewTypes), selected);
            this.scientificVisual3DControl.ViewType = viewType;
        }

        private void cmbCameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbCameraType.SelectedItem.ToString();
            CameraTypes cameraType = (CameraTypes)Enum.Parse(typeof(CameraTypes), selected);
            this.scientificVisual3DControl.CameraType = cameraType;
        }

    }
}
