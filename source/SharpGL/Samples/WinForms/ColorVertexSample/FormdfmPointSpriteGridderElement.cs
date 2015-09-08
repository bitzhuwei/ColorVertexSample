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
using YieldingGeometryModel.DataSource;

namespace ColorVertexSample
{
    public partial class FormdfmPointSpriteGridderElement : Form
    {
        public FormdfmPointSpriteGridderElement(float maxRadius)
        {
            InitializeComponent();

            InitilizeViewTypeControl();

            Application.Idle += Application_Idle;

            this.maxRadius = maxRadius;
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

                float dx = System.Convert.ToSingle(this.tbDX.Text);
                float dy = System.Convert.ToSingle(this.gbDY.Text);
                float dz = System.Convert.ToSingle(this.tbDZ.Text);
                // use CatesianGridderSource to fill HexahedronGridderElement's content.
                //DemoPointSpriteGridderSource source = new DemoPointSpriteGridderSource() { NX = nx, NY = ny, NZ = nz, };
                //var source = new dfmPointSpriteGridderSource() { NX = nx, NY = ny, NZ = nz, };
                var source = new dfmPointSpriteGridderSource(this.maxRadius) { NX = 386527, NY = 1, NZ = 1, };

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

                // use PointSpriteGridderElement
                PointSpriteMesh mesh = PointSpriteGridderElementHelper.CreateMesh(source);
                mesh.ColorArray = PointSpriteGridderElementHelper.FromColors(source, gridIndexes, colors, mesh.VisibleArray);

                PointSpriteGridderElement gridderElement = new PointSpriteGridderElement(source, this.scientificVisual3DControl.Scene.CurrentCamera);
                gridderElement.Initialize(this.scientificVisual3DControl.OpenGL, mesh);

                //
                gridderElement.Name = string.Format("element {0}", elementCounter++);

                this.scientificVisual3DControl.AddModelElement(gridderElement);

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
        private float maxRadius;

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
            {
                string message = string.Format("{0}", "确保在此exe文件夹内有dfm_cvxyz.in文件！");
                MessageBox.Show(message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            {
                string message = string.Format("{0}", "Add模型后，可通过'R'键观察随机隐藏hexahedron的情形。");
                MessageBox.Show(message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

    }
}
