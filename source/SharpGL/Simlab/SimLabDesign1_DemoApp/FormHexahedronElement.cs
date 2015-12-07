using SharpGL;
using SharpGL.SceneComponent;
using SimLabDesign1;
using SimLabDesign1_Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YieldingGeometryModel;
using YieldingGeometryModel.Builder;

namespace SimLabDesign1_DemoApp
{
    public partial class FormHexahedronElement : Form
    {
        public FormHexahedronElement()
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
            this.cmbViewType.SelectedIndex = 0;

            foreach (string item in Enum.GetNames(typeof(CameraTypes)))
            {
                this.cmbCameraType.Items.Add(item);
            }
            this.cmbCameraType.SelectedIndex = 0;
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
                int dimenSize = nx * ny * nz;
                float dx = System.Convert.ToSingle(this.tbDX.Text);
                float dy = System.Convert.ToSingle(this.gbDY.Text);
                float dz = System.Convert.ToSingle(this.tbDZ.Text);
                float[] dxArray = initArray(dimenSize, dx);
                float[] dyArray = initArray(dimenSize, dy);
                float[] dzArray = initArray(dimenSize, dz);
                // use CatesianGridderSource to fill HexahedronGridderElement's content.
                CatesianGridderSource source = new CatesianGridderSource() { NX = nx, NY = ny, NZ = nz, DX = dxArray, DY = dyArray, DZ = dzArray, };
                source.IBlocks = GridBlockHelper.CreateBlockCoords(nx);
                source.JBlocks = GridBlockHelper.CreateBlockCoords(ny);
                source.KBlocks = GridBlockHelper.CreateBlockCoords(nz);


                //DemoPointSpriteGridderSource source = new DemoPointSpriteGridderSource() { NX = nx, NY = ny, NZ = nz, };

                ///模拟获得网格属性
                int minValue = 100;
                int maxValue = 10000;
                step = (maxValue * 1.0f - minValue * 1.0f) / 10;
                int[] gridIndexes;
                float[] gridValues;
                //设置色标的范围
                this.scientificVisual3DControl.SetColorIndicator(minValue, maxValue, step);
                //获得每个网格上的属性值
                HexahedronGridderHelper.RandomValue(source.DimenSize, minValue, maxValue, out gridIndexes, out gridValues);
                ColorF[] colors = new ColorF[source.DimenSize];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = (ColorF)this.scientificVisual3DControl.MapToColor(gridValues[i]);
                }

                // use HexahedronGridderElement
                DateTime t0 = DateTime.Now;
                MeshGeometry mesh = HexahedronGridderHelper.CreateMesh(source);
                DateTime t1 = DateTime.Now;
                TimeSpan ts1 = t1 - t0;

                mesh.VertexColors = HexahedronGridderHelper.FromColors(source, gridIndexes, colors, mesh.Visibles);
                //this.DebugMesh(mesh);

                NonIndexRenderer renderer = new NonIndexRenderer(8, mesh.Vertexes.Length / 8);
                renderer.Mode = OpenGL.GL_QUAD_STRIP;
                HexahedronElement element = new HexahedronElement(renderer, this.scientificVisual3DControl.Scene.CurrentCamera);
                IVertexBuffers buffers = element as IVertexBuffers;
                buffers.AddAttributeBuffer(HexahedronElement.key_in_Position, mesh.Vertexes, UsageType.StaticDraw, 3, OpenGL.GL_FLOAT);
                buffers.AddAttributeBuffer(HexahedronElement.key_in_Color, mesh.VertexColors, UsageType.StaticDraw, 4, OpenGL.GL_FLOAT);
                buffers.AddAttributeBuffer(HexahedronElement.key_in_visible, mesh.Visibles, UsageType.StaticDraw, 1, OpenGL.GL_FLOAT);
                //this.scientificVisual3DControl.AddModelElement(element);

                HexahedronGridderElement gridderElement = new HexahedronGridderElement(source, this.scientificVisual3DControl.Scene.CurrentCamera);
                gridderElement.renderWireframe = false;
                //method1
                //gridderElement.Initialize(this.scientificVisual3DControl.OpenGL);

                //method2
                gridderElement.Initialize(this.scientificVisual3DControl.OpenGL, mesh);
                DateTime t2 = DateTime.Now;

                //gridderElement.SetBoundingBox(mesh.Min, mesh.Max);

                //// use PointSpriteGridderElement
                //PointSpriteMesh mesh = PointSpriteGridderElementHelper.CreateMesh(source);
                //mesh.ColorArray = PointSpriteGridderElementHelper.FromColors(source, gridIndexes, colors, mesh.VisibleArray);

                //PointSpriteGridderElement gridderElement = new PointSpriteGridderElement(source, this.scientificVisual3DControl.Scene.CurrentCamera);
                //gridderElement.Initialize(this.scientificVisual3DControl.OpenGL, mesh);
                ////DebugMesh(mesh);

                //


                gridderElement.Name = string.Format("element {0}", elementCounter++);
                //this.scientificVisual3DControl.AddModelElement(gridderElement);
                DateTime t3 = DateTime.Now;
                // update ModelContainer's BoundingBox.
                BoundingBox boundingBox = this.scientificVisual3DControl.ModelContainer.BoundingBox;
                if (this.scientificVisual3DControl.ModelContainer.Children.Count > 1)
                {
                    boundingBox.Extend(mesh.Min);
                    boundingBox.Extend(mesh.Max);
                }
                else
                {
                    boundingBox.SetBounds(mesh.Min, mesh.Max);
                }
                //boundingBox.Expand();

                // update ViewType to UserView.
                this.scientificVisual3DControl.ViewType = ViewTypes.UserView;
                mesh.Dispose();

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
            this.scientificVisual3DControl.ClearScientificModels();

            GC.Collect();
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

        private void scientificVisual3DControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r')
            {
                // 随机显示某些hexahedron
                foreach (var item in this.scientificVisual3DControl.ModelContainer.Children)
                {
                    {
                        HexahedronGridderElement element = item as HexahedronGridderElement;
                        if (element != null)
                        {
                            YieldingGeometryModel.Builder.HexahedronGridderElementHelper.RandomVisibility(element, this.scientificVisual3DControl.OpenGL, 0.2);
                        }
                    }
                    {
                        PointSpriteGridderElement element = item as PointSpriteGridderElement;
                        if (element != null)
                        {
                            YieldingGeometryModel.Builder.PointSpriteGridderElementHelper.RandomVisibility(element, this.scientificVisual3DControl.OpenGL, 0.2);
                        }
                    }
                }

                this.scientificVisual3DControl.Invalidate();
            }


            if (e.KeyChar == 'c')
            {
                OpenGL gl = this.scientificVisual3DControl.OpenGL;

                List<HexahedronGridderElement> elements = this.scientificVisual3DControl.ModelContainer.Traverse<HexahedronGridderElement>().ToList<HexahedronGridderElement>();
                if (elements.Count > 0)
                {
                    HexahedronGridderElement gridder = elements[0];
                    HexahedronGridderSource source = gridder.Source;
                    UnmanagedArray<float> visibles = HexahedronGridderHelper.GridVisibleFromActive(source);

                    //随机生成不完整网格的属性。
                    int propCount = source.DimenSize / 2;
                    if (propCount <= 0)
                        return;

                    int minValue = 5000;
                    int maxValue = 10000;
                    int[] gridIndexes;
                    float[] gridValues;
                    HexahedronGridderHelper.RandomValue(propCount, minValue, maxValue, out gridIndexes, out gridValues);
                    float step = (maxValue - minValue) / 10.0f;
                    this.scientificVisual3DControl.SetColorIndicator(minValue, maxValue, step);

                    ColorF[] colors = new ColorF[propCount];
                    for (int i = 0; i < colors.Length; i++)
                    {
                        colors[i] = (ColorF)this.scientificVisual3DControl.MapToColor(gridValues[i]);
                    }

                    UnmanagedArray<ColorF> colorArray = HexahedronGridderHelper.FromColors(source, gridIndexes, colors, visibles);
                    gridder.UpdateColorBuffer(gl, colorArray, visibles);
                    colorArray.Dispose();
                    visibles.Dispose();
                    this.scientificVisual3DControl.Invalidate();
                }
            }
        }

        private void FormHexahedronGridder_Load(object sender, EventArgs e)
        {
            string message = string.Format("{0}", "Add模型后，可通过'R'键观察随机隐藏hexahedron的情形。");
            MessageBox.Show(message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

    }
}
