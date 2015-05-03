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
        public FormScientificVisual3DControl()
        {
            InitializeComponent();

            this.Text = "Rotation tip: left mouse for camera & right mouse for model";
        }

        private void Create3DObject(object sender, EventArgs e)
        {
            try
            {
                int nx = System.Convert.ToInt32(tbNX.Text);
                int ny = System.Convert.ToInt32(tbNY.Text);
                int nz = System.Convert.ToInt32(tbNZ.Text);
                float radius = System.Convert.ToSingle(this.tbRadius.Text);
                float minValue = System.Convert.ToSingle(this.tbRangeMin.Text);
                float maxValue = System.Convert.ToSingle(this.tbRangeMax.Text);
                if (minValue >= maxValue)
                    throw new ArgumentException("min value equal or equal to maxValue");

                PointModel model = PointModel.Create(nx, ny, nz, radius, minValue, maxValue);

                this.sceneControl.ScientificModel = model;
                this.sceneControl.uiColorIndicator.Data.minValue = minValue;
                this.sceneControl.uiColorIndicator.Data.maxValue = maxValue;
                this.sceneControl.Invalidate();// redraw the scene.
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        /// <summary>
        /// quick way to set min and max value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDebugInfo_Click(object sender, EventArgs e)
        {
            this.tbRangeMin.Text = "-1000";
            this.tbRangeMax.Text = "1000";
        }
    }
}
