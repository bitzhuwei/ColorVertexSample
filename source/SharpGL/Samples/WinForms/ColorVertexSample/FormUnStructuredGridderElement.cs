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
using YieldingGeometryModel;
using YieldingGeometryModel.Builder;
using SharpGL.SceneComponent.Model;
using System.IO;
using YieldingGeometryModel.loader;
using YieldingGeometryModel.DataSource;

namespace ColorVertexSample
{
    public partial class FormUnStructuredGridderElement : Form
    {
        private UnStructuredGridderElement element;

        public FormUnStructuredGridderElement()
        {
            InitializeComponent();

            InitilizeViewTypeControl();

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

        private void Create3DObject(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Please make sure you put 'ListCVFracture.txt', 'ListCVBorder.txt', 'Model.grid' under the same directory with this exe.");
                String dataRoot = @".";
                String fractionTagsName = "ListCVFracture.txt";
                String borderTagsName = "ListCVBorder.txt";
                String modelGrid = "Model.grid";

                var startTime = DateTime.Now;

                string gridPathFileName = Path.Combine(dataRoot, modelGrid);

                string fractionTagPathFileName = Path.Combine(dataRoot, fractionTagsName);

                string borderTagPathFileName = Path.Combine(dataRoot, borderTagsName);

                UnstructureGeometryLoader loader = new UnstructureGeometryLoader();

                DateTime start = DateTime.Now;
                UnStructuredGridderSource source = loader.LoadSource(gridPathFileName, fractionTagPathFileName, borderTagPathFileName);
                DateTime stop = DateTime.Now;
                double seconds = (stop.Ticks - start.Ticks) / 1000.0d;

                this.element = new UnStructuredGridderElement(source, this.scientificVisual3DControl.Scene.CurrentCamera) { Name = "UnStructuredGridderElement}" };
                element.Initialize(this.scientificVisual3DControl.OpenGL);

                ///模拟获得网格属性
                int minValue = 100;
                int maxValue = 10000;
                float step = (maxValue * 1.0f - minValue * 1.0f) / 10;
                //设置色标的范围
                this.scientificVisual3DControl.SetColorIndicator(minValue, maxValue, step);

                this.scientificVisual3DControl.AddModelElement(element);

                // update ModelContainer's BoundingBox.
                BoundingBox boundingBox = this.scientificVisual3DControl.ModelContainer.BoundingBox;
                if (this.scientificVisual3DControl.ModelContainer.Children.Count > 1)
                {
                    boundingBox.Extend(source.Min);
                    boundingBox.Extend(source.Max);
                }
                else
                {
                    boundingBox.SetBounds(source.Min, source.Max);
                }
                //boundingBox.Expand();

                // update ViewType to UserView.
                this.scientificVisual3DControl.ViewType = ViewTypes.UserView;

                var endTime = DateTime.Now;
                var interval = endTime.Subtract(startTime);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }


        private void btnClearModels_Click(object sender, EventArgs e)
        {
            this.scientificVisual3DControl.ClearScientificModels();
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

        private void chkrenderFractions_CheckedChanged(object sender, EventArgs e)
        {
            if (this.element != null)
            {
                this.element.renderFractions = chkrenderFractions.Checked;
                this.scientificVisual3DControl.Invalidate();
            }
        }

        private void chkrenderFractionsWireframe_CheckedChanged(object sender, EventArgs e)
        {
            if (this.element != null)
            {
                this.element.renderFractionsWireframe = this.chkrenderFractionsWireframe.Checked;
                this.scientificVisual3DControl.Invalidate();
            }
        }
        private void chkrenderTetras_CheckedChanged(object sender, EventArgs e)
        {
            if (this.element != null)
            {
                this.element.renderTetras = this.chkrenderTetras.Checked;
                this.scientificVisual3DControl.Invalidate();
            }
        }
        private void chkrenderTetrasWireframe_CheckedChanged(object sender, EventArgs e)
        {
            if (this.element != null)
            {
                this.element.renderTetrasWireframe = this.chkrenderTetrasWireframe.Checked;
                this.scientificVisual3DControl.Invalidate();
            }
        }

        private void scientificVisual3DControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 'j')
            //{
            //    this.element.CurrentTetrasIndexCount++;
            //}
            //else if (e.KeyChar == 'k')
            //{
            //    this.element.CurrentTetrasIndexCount--;
            //}
            //this.scientificVisual3DControl.Invalidate();
        }
    }
}
