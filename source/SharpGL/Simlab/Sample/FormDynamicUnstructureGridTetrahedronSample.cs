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
using SharpGL.SceneComponent.Model;
using SimLab.GridSource;
using SimLab;
using System.Globalization;
using SimLab.SimGrid;
using SimLab.SimGrid.Loader;
using SimLab.VertexBuffers;
using System.IO;
using System.Drawing.Imaging;

namespace Sample
{
    public partial class FormDynamicUnstructureGridTetrahedronSample : Form
    {
        public FormDynamicUnstructureGridTetrahedronSample()
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

        private float[] initArray(int size, float value)
        {
            float[] result = new float[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = value;
            }
            return result;
        }

        protected void InitSlice(ListBox box, IList<int> slices)
        {
            box.Items.Clear();
            foreach (int coord in slices)
            {
                box.Items.Add(coord);
            }

        }

        private void OnGridPropertyChanged(object sender, EventArgs e)
        {
            GridderSource source = this.sim3D.Tag as GridderSource;
            if (source == null)
                return;

            List<SimLabGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<SimLabGrid>().ToList<SimLabGrid>();
            if (gridders.Count <= 0)
                return;

            SimLabGrid grid = gridders[0];

            GridProperty prop = this.CurrentProperty;
            if (prop == null)
                return;
            float minValue = prop.MinValue;
            float maxValue = prop.MaxValue;
            float step = (maxValue - minValue) / 10.0f;
            if (step <= 0.0f)
                step = 1.0f;

            this.sim3D.SetColorIndicator(minValue, maxValue, step);
            TexCoordBuffer textureCoordinates = source.CreateTextureCoordinates(prop.GridIndexes, prop.Values, minValue, maxValue);
            grid.SetTextureCoods(textureCoordinates);

            DynamicUnstructureGrid dynamicUnstructureGrid = gridders[0] as DynamicUnstructureGrid;
            DynamicUnstructuredGridderSource dynamicUnstructureGridderSource = source as DynamicUnstructuredGridderSource;
            if (dynamicUnstructureGrid != null && dynamicUnstructureGridderSource != null)
            {
                TexCoordBuffer anotherTextureCoordinates = dynamicUnstructureGridderSource.CreateFractureTextureCoordinates(prop.GridIndexes, prop.Values, minValue, maxValue);
                dynamicUnstructureGrid.SetFractionTextureCoords(anotherTextureCoordinates);
            }

            this.sim3D.Invalidate();

        }


        private void InitPropertiesAndSelectDefault(int dimenSize, float minValue, float maxValue)
        {

            GridProperty prop1 = GridPropertyGenerator.CreateGridIndexProperty(dimenSize, "Grid Position");
            GridProperty prop2 = GridPropertyGenerator.CreateRandomProperty(dimenSize, "Random", minValue, maxValue);
            GridProperty prop3 = GridPropertyGenerator.CreateMaxValueProperty(dimenSize, "MaxValue", minValue, maxValue);
            this.cbxGridProperties.Items.Clear();
            this.cbxGridProperties.Items.Add(prop1);
            this.cbxGridProperties.Items.Add(prop2);
            this.cbxGridProperties.Items.Add(prop3);
            this.cbxGridProperties.SelectedIndex = 0;
            this.cbxGridProperties.SelectedValueChanged -= this.OnGridPropertyChanged;
            this.cbxGridProperties.SelectedValueChanged += this.OnGridPropertyChanged;
        }

        public GridProperty CurrentProperty
        {
            get
            {
                return cbxGridProperties.SelectedItem as GridProperty;
            }
        }

        public bool IsShowWireframe
        {
            get
            {
                return cbxShowWireframe.Checked;
            }
        }

        private void CreateCatesianGridVisual3D(object sender, EventArgs e)
        {
            try
            {

                int nx = 6090;
                int ny = 1;
                int nz = 1;
                int dimenSize = nx * ny * nz;

                float step = System.Convert.ToSingle(tbColorIndicatorStep.Text);
                //float radius = System.Convert.ToSingle(this.tbRadius.Text);
                float propMin = System.Convert.ToSingle(this.tbxPropertyMinValue.Text, CultureInfo.InvariantCulture);
                float propMax = System.Convert.ToSingle(this.tbxPropertyMaxValue.Text, CultureInfo.InvariantCulture);

                float dx = System.Convert.ToSingle(this.tbDX.Text);
                float dy = System.Convert.ToSingle(this.gbDY.Text);
                float dz = System.Convert.ToSingle(this.tbDZ.Text);




                string fileName = @"prism_geometry.txt";
                DynamicUnstructureGeometryLoader loader = new DynamicUnstructureGeometryLoader();
                // use CatesianGridderSource to fill HexahedronGridderElement's content.
                DynamicUnstructuredGridderSource source = loader.LoadSource(fileName, nx, ny, nz);
                source.Init();

                InitPropertiesAndSelectDefault(dimenSize, propMin, propMax);


                ///模拟获得网格属性
                ///获得当前选中的属性


                float minValue = this.CurrentProperty.MinValue;
                float maxValue = this.CurrentProperty.MaxValue;
                step = (maxValue * 1.0f - minValue * 1.0f) / 10;
                int[] gridIndexes = this.CurrentProperty.GridIndexes;
                float[] gridValues = this.CurrentProperty.Values;


                //设置色标的范围
                this.sim3D.SetColorIndicator(minValue, maxValue, step);


                // use HexahedronGridderElement
                DateTime t0 = DateTime.Now;
                DynamicUnstructureGeometry geometry = source.CreateMesh() as DynamicUnstructureGeometry;
                TexCoordBuffer matrixTextureCoordinates = source.CreateTextureCoordinates(gridIndexes, gridValues, minValue, maxValue);
                TexCoordBuffer fractureTextureCoordindates = source.CreateFractureTextureCoordinates(gridIndexes, gridValues, minValue, maxValue);
                Bitmap texture = ColorPaletteHelper.GenBitmap(this.sim3D.uiColorIndicator.Data.ColorPalette);
                //geometry.Positions.Dump();
                //geometry.TriangleIndices.Dump();
                //MeshGeometry mesh = HexahedronGridderHelper.CreateMesh(source);
                DynamicUnstructureGrid gridder = new DynamicUnstructureGrid(this.sim3D.OpenGL, this.sim3D.Scene.CurrentCamera);
                gridder.Init(geometry);
                gridder.RenderGrid = true;
                gridder.RenderGridWireframe = this.IsShowWireframe;
                gridder.SetTexture(texture);
                gridder.SetMatrixTextureCoords(matrixTextureCoordinates);
                gridder.SetFractionTextureCoords(fractureTextureCoordindates);


                //textureCoodinates.Dump();


                DateTime t1 = DateTime.Now;
                TimeSpan ts1 = t1 - t0;

                //mesh.VertexColors = HexahedronGridderHelper.FromColors(source, gridIndexes, colors, mesh.Visibles);
                //this.DebugMesh(mesh);

                //HexahedronGridderElement gridderElement = new HexahedronGridderElement(source, this.scientificVisual3DControl.Scene.CurrentCamera);
                //gridderElement.renderWireframe = false;
                //method1
                //gridderElement.Initialize(this.scientificVisual3DControl.OpenGL);

                //method2
                //gridderElement.Initialize(this.scientificVisual3DControl.OpenGL, mesh);
                DateTime t2 = DateTime.Now;

                //gridderElement.SetBoundingBox(mesh.Min, mesh.Max);
                this.sim3D.Tag = source;


                //gridderElement.Name = string.Format("element {0}", elementCounter++);
                //this.scientificVisual3DControl.AddModelElement(gridderElement);

                DateTime t3 = DateTime.Now;
                // update ModelContainer's BoundingBox.
                BoundingBox boundingBox = this.sim3D.ModelContainer.BoundingBox;
                boundingBox.SetBounds(geometry.Min, geometry.Max);

                // update ViewType to UserView.
                this.sim3D.ViewType = ViewTypes.UserView;
                this.sim3D.AddModelElement(gridder);
                //mesh.Dispose();

                StringBuilder msgBuilder = new StringBuilder();
                msgBuilder.AppendLine(String.Format("create mesh in {0} secs", (t1 - t0).TotalSeconds));
                msgBuilder.AppendLine(String.Format("init SceneElement in {0} secs", (t2 - t1).TotalSeconds));
                msgBuilder.AppendLine(String.Format("total load in {0} secs", (t2 - t0).TotalSeconds));
                String msg = msgBuilder.ToString();
                MessageBox.Show(msg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GC.Collect();
        }

        private void btnClearModels_Click(object sender, EventArgs e)
        {
            this.sim3D.ClearScientificModels();
            this.CurrentHexahedronGrid = null;
            this.lbxNI.Items.Clear();
            this.lbxNJ.Items.Clear();
            this.lbxNZ.Items.Clear();
            GC.Collect();
        }

        private void chkRenderContainerBox_CheckedChanged(object sender, EventArgs e)
        {
            this.sim3D.RenderBoundingBox = this.chkRenderContainerBox.Checked;
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbViewType.SelectedItem.ToString();
            ViewTypes viewType = (ViewTypes)Enum.Parse(typeof(ViewTypes), selected);
            this.sim3D.ViewType = viewType;
        }

        private void cmbCameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cmbCameraType.SelectedItem.ToString();
            CameraTypes cameraType = (CameraTypes)Enum.Parse(typeof(CameraTypes), selected);
            this.sim3D.CameraType = cameraType;
        }

        private void scientificVisual3DControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r')
            {

                this.sim3D.Invalidate();
            }


            if (e.KeyChar == 'c')
            {
                OpenGL gl = this.sim3D.OpenGL;
            }

            if (e.KeyChar == 's')
            {
                this.SaveTextureBitmap();
            }
        }


        private void SaveTextureBitmap()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "png files (*.png)|*.png";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    Bitmap texture = ColorPaletteHelper.GenBitmap(this.sim3D.uiColorIndicator.Data.ColorPalette);
                    texture.Save(myStream, ImageFormat.Png);
                    myStream.Close();
                }
            }



        }

        private void FormHexahedronGridder_Load(object sender, EventArgs e)
        {
            string message = string.Format("{0}", "Add模型后，可通过'R'键观察随机隐藏hexahedron的情形。");
            MessageBox.Show(message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }


        public HexahedronGridderSource CurrentHexahedronGrid
        {
            get
            {
                return this.sim3D.Tag as HexahedronGridderSource;
            }

            set
            {
                this.sim3D.Tag = null;
            }

        }

        private List<int> GetSelectedSlices(ListBox slice)
        {
            List<int> list = new List<int>();
            ListBox.SelectedObjectCollection collection = slice.SelectedItems;
            foreach (object item in collection)
            {
                list.Add((int)item);
            }
            return list;
        }



        private void cbxShowWireframe_CheckedChanged(object sender, EventArgs e)
        {

            //HexahedronGridderSource source = this.CurrentHexahedronGrid;
            //if (source == null)
            //    return;

            List<SimLabGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<SimLabGrid>().ToList<SimLabGrid>();
            if (gridders.Count <= 0)
                return;
            SimLabGrid gridder = gridders[0];

            //if (this.IsShowWireframe)
            //{
            //    WireFrameBufferData wireFrame = source.CreateWireframe();
            //    gridder.SetWireframe(wireFrame);
            //}
            //else
            //{
            //     gridder.SetWireframe(null);
            //}
            gridder.RenderGridWireframe = this.IsShowWireframe;

            this.sim3D.Invalidate();

        }


        /// <summary>
        /// 应用切片分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSlicesApply_Click(object sender, EventArgs e)
        {

            GridProperty prop = this.CurrentProperty;
            if (prop == null)
                return;


            HexahedronGridderSource source = this.CurrentHexahedronGrid;
            if (source == null)
                return;

            List<SimLabGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<SimLabGrid>().ToList<SimLabGrid>();
            if (gridders.Count <= 0)
                return;

            SimLabGrid gridder = gridders[0];
            List<int> islices = this.GetSelectedSlices(this.lbxNI);
            List<int> jslices = this.GetSelectedSlices(this.lbxNJ);
            List<int> kslices = this.GetSelectedSlices(this.lbxNZ);
            source.IBlocks = islices;
            source.JBlocks = jslices;
            source.KBlocks = kslices;
            source.RefreashSlices();
            float minValue = this.sim3D.uiColorIndicator.Data.MinValue;
            float maxValue = this.sim3D.uiColorIndicator.Data.MaxValue;
            TexCoordBuffer textureCoordinates = source.CreateTextureCoordinates(prop.GridIndexes, prop.Values, minValue, maxValue);
            gridder.SetTextureCoods(textureCoordinates);

            this.sim3D.Invalidate();
        }

        private void chkShowMatrix_CheckedChanged(object sender, EventArgs e)
        {
            List<SimLabGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<SimLabGrid>().ToList<SimLabGrid>();
            if (gridders.Count <= 0)
                return;
            SimLabGrid gridder = gridders[0];

            //if (this.IsShowWireframe)
            //{
            //    WireFrameBufferData wireFrame = source.CreateWireframe();
            //    gridder.SetWireframe(wireFrame);
            //}
            //else
            //{
            //     gridder.SetWireframe(null);
            //}
            gridder.RenderGrid = this.chkShowMatrix.Checked;

            this.sim3D.Invalidate();
        }

        private void chkShowMatrixWireframe_CheckedChanged(object sender, EventArgs e)
        {
            List<SimLabGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<SimLabGrid>().ToList<SimLabGrid>();
            if (gridders.Count <= 0)
                return;
            SimLabGrid gridder = gridders[0];

            //if (this.IsShowWireframe)
            //{
            //    WireFrameBufferData wireFrame = source.CreateWireframe();
            //    gridder.SetWireframe(wireFrame);
            //}
            //else
            //{
            //     gridder.SetWireframe(null);
            //}
            gridder.RenderGridWireframe = this.chkShowMatrixWireframe.Checked;

            this.sim3D.Invalidate();
        }

        private void chkShowFraction_CheckedChanged(object sender, EventArgs e)
        {
            List<DynamicUnstructureGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<DynamicUnstructureGrid>().ToList<DynamicUnstructureGrid>();
            if (gridders.Count <= 0)
                return;
            DynamicUnstructureGrid gridder = gridders[0];

            //if (this.IsShowWireframe)
            //{
            //    WireFrameBufferData wireFrame = source.CreateWireframe();
            //    gridder.SetWireframe(wireFrame);
            //}
            //else
            //{
            //     gridder.SetWireframe(null);
            //}
            gridder.RenderFraction = this.chkShowFraction.Checked;

            this.sim3D.Invalidate();
        }

        private void chkShowFractionWireframe_CheckedChanged(object sender, EventArgs e)
        {
            List<DynamicUnstructureGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<DynamicUnstructureGrid>().ToList<DynamicUnstructureGrid>();
            if (gridders.Count <= 0)
                return;
            DynamicUnstructureGrid gridder = gridders[0];

            //if (this.IsShowWireframe)
            //{
            //    WireFrameBufferData wireFrame = source.CreateWireframe();
            //    gridder.SetWireframe(wireFrame);
            //}
            //else
            //{
            //     gridder.SetWireframe(null);
            //}
            gridder.RenderFractionWireframe = this.chkShowFractionWireframe.Checked;

            this.sim3D.Invalidate();
        }

        private void barBrightness_Scroll(object sender, EventArgs e)
        {
            this.lblBrightnessValue.Text = (this.barBrightness.Value * 1.0f / 100).ToString();

            List<SimLabGrid> gridders = this.sim3D.Scene.SceneContainer.Traverse<SimLabGrid>().ToList<SimLabGrid>();
            if (gridders.Count <= 0)
                return;

            SimLabGrid gridder = gridders[0] as SimLabGrid;
            gridder.Brightness = this.barBrightness.Value * 1.0f / 100;

            this.sim3D.Invalidate();
        }


    }
}
