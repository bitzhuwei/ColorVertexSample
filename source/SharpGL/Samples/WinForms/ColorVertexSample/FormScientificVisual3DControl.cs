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
    public partial class FormScientificVisual3DControl : Form
    {
        public FormScientificVisual3DControl()
        {
            InitializeComponent();
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

                this.sceneControl.AddScientificModel(model);
                this.sceneControl.SetColorIndicator(minValue, maxValue, step);
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
            this.sceneControl.ClearScientificModels();
        }

        private void rdoPerspective_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoPerspective.Checked)
            {
                this.sceneControl.CameraType = ECameraType.Perspecitive;
            }
        }

        private void rdoOrtho_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoOrtho.Checked)
            {
                this.sceneControl.CameraType = ECameraType.Ortho;
            }
        }
    }
}
