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
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneComponent;

namespace ColorVertexSample
{
    public partial class FormScientificControl : Form
    {
        public FormScientificControl()
        {
            InitializeComponent();

            InitilizeViewTypeControl();
        }

        private void InitilizeViewTypeControl()
        {
            foreach (string item in Enum.GetNames(typeof(EViewType)))
            {
                this.cmbViewType.Items.Add(item);
            }
        }

        private void Create3DObject(object sender, EventArgs e)
        {
            try
            {
                int nx = System.Convert.ToInt32(tbNX.Text);
                int ny = System.Convert.ToInt32(tbNY.Text);
                int nz = System.Convert.ToInt32(tbNZ.Text);
                float step = System.Convert.ToSingle(tbColorIndicatorStep.Text);
                float radius = System.Convert.ToSingle(this.tbRadius.Text);
                float minValue = System.Convert.ToSingle(this.tbRangeMin.Text);
                float maxValue = System.Convert.ToSingle(this.tbRangeMax.Text);
                if (minValue >= maxValue)
                    throw new ArgumentException("min value equal or equal to maxValue");

                PointModel model = PointModel.Create(nx, ny, nz, radius, minValue, maxValue);

                //this.sceneControl.AddScientificModel(model);// This is replaced by codes below.
                ScientificModelElement element = new ScientificModelElement(model);//, false, true);
                this.scientificControl.AddModelElement(element);
                // update ModelContainer's BoundingBox.
                BoundingBox boundingBox = this.scientificControl.ModelContainer.BoundingBox;
                IBoundingBox modelBoundingBox = model.BoundingBox;
                if (this.scientificControl.ModelContainer.Children.Count > 1)
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
                // update ViewType to UserView.
                //this.scientificControl.ViewType = EViewType.UserView;
                this.scientificControl.UpdateCamera();

                //this.sceneControl.SetColorIndicator(minValue, maxValue, step);
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
            this.tbRangeMax.Text = rangeMax[testCaseIndex];
            this.tbRangeMin.Text = rangeMin[testCaseIndex];
            this.tbColorIndicatorStep.Text = stepList[testCaseIndex];
            testCaseIndex = testCaseIndex >= rangeMin.Count - 1 ? 0 : testCaseIndex + 1;
        }

        private void btnClearModels_Click(object sender, EventArgs e)
        {
            this.scientificControl.ClearScientificModels();
        }

        private void rdoPerspective_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoPerspective.Checked)
            {
                this.scientificControl.CameraType = ECameraType.Perspecitive;
            }
        }

        private void rdoOrtho_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoOrtho.Checked)
            {
                this.scientificControl.CameraType = ECameraType.Ortho;
            }
        }

        private void lblDebugInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button== System.Windows.Forms.MouseButtons.Right)
            {
                bool depthTest = this.scientificControl.OpenGL.IsEnabled(OpenGL.GL_DEPTH_TEST);
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("depth test: {0}", depthTest ? "enabled" : "disabled"));
                MessageBox.Show(builder.ToString());
            }
        }

        private void chkRenderModels_CheckedChanged(object sender, EventArgs e)
        {
            //this.sceneControl.RenderModels = this.chkRenderModels.Checked;
        }

        private void chkRenderModelsBox_CheckedChanged(object sender, EventArgs e)
        {
            //this.sceneControl.RenderModelsBoundingBox = this.chkRenderModelsBox.Checked;
        }

        private void chkRenderContainerBox_CheckedChanged(object sender, EventArgs e)
        {
            this.scientificControl.RenderBoundingBox = this.chkRenderContainerBox.Checked;
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbViewType.SelectedItem.ToString();
            EViewType viewType = (EViewType)Enum.Parse(typeof(EViewType), selected);
            this.scientificControl.ViewType = viewType;
        }

    }
}
