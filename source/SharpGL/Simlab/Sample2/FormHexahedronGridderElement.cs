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
using System.Drawing.Imaging;
using SimLab2.VertexBuffers;

namespace Sample2
{
    public partial class FormHexahedronGridderElement : Form
    {
        public FormHexahedronGridderElement()
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
            box.BeginUpdate();
            box.Items.Clear();
            foreach (int coord in slices)
            {
                box.Items.Add(coord);
            }
            box.EndUpdate();
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
            this.sim3D.Invalidate();

        }

     


        private void InitPropertiesAndSelectDefault(int dimenSize, float minValue, float maxValue)
        {

            GridProperty prop1 = GridPropertyGenerator.CreateGridIndexProperty(dimenSize, "Grid Position");
            GridProperty prop2 = GridPropertyGenerator.CreateRandomProperty(dimenSize, "Random", minValue, maxValue);
            this.cbxGridProperties.BeginUpdate();
            this.cbxGridProperties.Items.Clear();
            this.cbxGridProperties.Items.Add(prop1);
            this.cbxGridProperties.Items.Add(prop2);
            this.cbxGridProperties.SelectedIndex = 0;
            this.cbxGridProperties.SelectedValueChanged -= this.OnGridPropertyChanged;
            this.cbxGridProperties.SelectedValueChanged += this.OnGridPropertyChanged;
            this.cbxGridProperties.EndUpdate();
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
                int nx = System.Convert.ToInt32(tbNX.Text);
                int ny = System.Convert.ToInt32(tbNY.Text);
                int nz = System.Convert.ToInt32(tbNZ.Text);
                float step = System.Convert.ToSingle(tbColorIndicatorStep.Text);
                //float radius = System.Convert.ToSingle(this.tbRadius.Text);
                float propMin = System.Convert.ToSingle(this.tbxPropertyMinValue.Text, CultureInfo.InvariantCulture);
                float propMax = System.Convert.ToSingle(this.tbxPropertyMaxValue.Text, CultureInfo.InvariantCulture);
                int dimenSize = nx * ny * nz;
                float dx = System.Convert.ToSingle(this.tbDX.Text);
                float dy = System.Convert.ToSingle(this.gbDY.Text);
                float dz = System.Convert.ToSingle(this.tbDZ.Text);
                float[] dxArray = initArray(dimenSize, dx);
                float[] dyArray = initArray(dimenSize, dy);
                float[] dzArray = initArray(dimenSize, dz);

                DateTime t0 = DateTime.Now;
                CatesianGridderSource source = new CatesianGridderSource() { NX = nx, NY = ny, NZ = nz, DX = dxArray, DY = dyArray, DZ = dzArray, };
                source.IBlocks = GridBlockHelper.CreateBlockCoords(nx);
                source.JBlocks = GridBlockHelper.CreateBlockCoords(ny);
                source.KBlocks = GridBlockHelper.CreateBlockCoords(nz);
                source.Init();
                DateTime t1 = DateTime.Now;

                InitSlice(lbxNI, source.IBlocks);
                InitSlice(lbxNJ, source.JBlocks);
                InitSlice(lbxNZ, source.KBlocks);
                InitPropertiesAndSelectDefault(dimenSize, propMin, propMax);

                DateTime t2 = DateTime.Now;

                ///模拟获得网格属性
                ///获得当前选中的属性


                float minValue = this.CurrentProperty.MinValue;
                float maxValue = this.CurrentProperty.MaxValue;
                step = (maxValue * 1.0f - minValue * 1.0f) / 10;
                int[] gridIndexes = this.CurrentProperty.GridIndexes;
                float[] gridValues = this.CurrentProperty.Values;
                //设置色标的范围
                this.sim3D.SetColorIndicator(minValue, maxValue, step);


                DateTime t3 = DateTime.Now;
                HexahedronMeshGeometry3D geometry = (HexahedronMeshGeometry3D)source.CreateMesh();
                DateTime t4 = DateTime.Now;
                TexCoordBuffer textureCoodinates = source.CreateTextureCoordinates(gridIndexes, gridValues, minValue, maxValue);
                DateTime t5 = DateTime.Now;

                Bitmap texture = this.sim3D.uiColorIndicator.CreateTextureImage();
                HexahedronGrid gridder = new HexahedronGrid(this.sim3D.OpenGL, this.sim3D.Scene.CurrentCamera);
                gridder.Init(geometry);
                gridder.RenderGrid = true;
                gridder.RenderGridWireframe = this.IsShowWireframe;
                gridder.SetTexture(texture);
                gridder.SetTextureCoods(textureCoodinates);
                //textureCoodinates.Dump();
                DateTime t6 = DateTime.Now;



                //gridderElement.SetBoundingBox(mesh.Min, mesh.Max);
                this.sim3D.Tag = source;





                // update ModelContainer's BoundingBox.
                BoundingBox boundingBox = this.sim3D.ModelContainer.BoundingBox;
                boundingBox.SetBounds(geometry.Min, geometry.Max);

                // update ViewType to UserView.
                this.sim3D.ViewType = ViewTypes.UserView;
                this.sim3D.AddModelElement(gridder);


                StringBuilder msgBuilder = new StringBuilder();
                msgBuilder.AppendLine(String.Format("Init Grid DataSource  in {0} secs", (t1 - t0).TotalSeconds));
                msgBuilder.AppendLine(String.Format("init ControlValues in {0} secs", (t2 - t1).TotalSeconds));
                msgBuilder.AppendLine(String.Format("prepare other params  in {0} secs", (t3 - t2).TotalSeconds));
                msgBuilder.AppendLine(String.Format("CreateMesh in {0} secs", (t4 - t3).TotalSeconds));
                msgBuilder.AppendLine(String.Format("CreateTextures in {0} secs", (t5 - t4).TotalSeconds));
                msgBuilder.AppendLine(String.Format("Init SimLabGrid  in {0} secs", (t6 - t5).TotalSeconds));
                msgBuilder.AppendLine(String.Format("Total, Create 3D Grid  in {0} secs", (t6 - t0).TotalSeconds));
                String msg = msgBuilder.ToString();
                MessageBox.Show(msg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            this.cbxGridProperties.SelectedIndex = -1;
            this.cbxGridProperties.Items.Clear();

            GC.Collect();
        }

        private void lblDebugInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bool depthTest = this.sim3D.OpenGL.IsEnabled(OpenGL.GL_DEPTH_TEST);
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("depth test: {0}", depthTest ? "enabled" : "disabled"));
                MessageBox.Show(builder.ToString());
            }
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

            HexahedronGridderSource source = this.CurrentHexahedronGrid;
            if (source == null)
                return;

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

        private void lblBrightnessValue_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnTextureSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                  string pathFileName =  dlg.FileName;
                  Bitmap textureImage = this.sim3D.uiColorIndicator.CreateTextureImage();
                  textureImage.Save(pathFileName,ImageFormat.Bmp);
            }
        }

    }
}
