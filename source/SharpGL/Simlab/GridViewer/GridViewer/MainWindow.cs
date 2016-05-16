using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SimLab;
using SimLab.GridSource;
using SimLab.helper;
using SimLab.SimGrid;
using SimLab.SimGrid.Loader;
using SimLab.VertexBuffers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracyEnergy.Simba.Data.Keywords;
using TracyEnergy.Simba.Data.Keywords.impl;

namespace GridViewer
{
    public partial class MainWindow : Form
    {
        private SceneElement selectedElement;
        public MainWindow()
        {
            InitializeComponent();
            this.InitNode(this.treeView1, this.scene.ModelContainer);
        }

        private void InitNode(TreeView tree, ModelContainer modelContainer)
        {

            TreeNode root = tree.Nodes.Add("Model");
            root.Tag = modelContainer;
        }

      

        private void OnTreeViewAfterSelected(object sender, TreeViewEventArgs e)
        {
         
            OnSelectedSceneElementChanged(e.Node);

            if (e.Node.Tag is GridBlockProperty)
            {

                SimLabGrid gridder = e.Node.Parent.Tag as SimLabGrid;
                GridBlockProperty gbp = e.Node.Tag as GridBlockProperty;


                if (gridder is DynamicUnstructureGrid)
                {

                    DynamicUnstructuredGridderSource src = gridder.Tag as DynamicUnstructuredGridderSource;
                    DynamicUnstructureGrid dynamicGridder = (DynamicUnstructureGrid)gridder;
                    this.UpdateColorIndicator(gbp);
                    float minValue = this.scene.uiColorIndicator.Data.MinValue;
                    float maxValue = this.scene.uiColorIndicator.Data.MaxValue;
                    Bitmap textureBitMap = this.scene.uiColorIndicator.CreateTextureImage();
                    TexCoordBuffer matrixTextureCoords = src.CreateTextureCoordinates(gbp.Positions, gbp.Values, minValue, maxValue);
                    TexCoordBuffer fractureTextureCoords = src.CreateFractureTextureCoordinates(gbp.Positions, gbp.Values, minValue, maxValue);
                    dynamicGridder.SetMatrixTextureCoords(matrixTextureCoords);
                    dynamicGridder.SetFractionTextureCoords(fractureTextureCoords);
                    dynamicGridder.SetTexture(textureBitMap);
                    this.scene.Invalidate();
                    return;
                }


                {
                    GridderSource src = gridder.Tag as GridderSource;
                    this.UpdateColorIndicator(gbp);
                    float minValue = this.scene.uiColorIndicator.Data.MinValue;
                    float maxValue = this.scene.uiColorIndicator.Data.MaxValue;
                    Bitmap textureBitMap = this.scene.uiColorIndicator.CreateTextureImage();
                    TexCoordBuffer textureCoords = src.CreateTextureCoordinates(gbp.Positions, gbp.Values, minValue, maxValue);
                    gridder.SetTexture(textureBitMap);
                    gridder.SetTextureCoods(textureCoords);
                    this.scene.Invalidate();
                }


            }
        }

        private void OnSelectedSceneElementChanged(TreeNode node)
        {

            this.propertyGrid1.SelectedObject = node.Tag;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.scene.Invalidate();
        }

        private SimulationInputData LoadEclInputData(String fileName)
        {

            KeywordSchema schema = KeywordSchemaExtension.RestoreSchemaFromEmbededResource();
            SimulationInputData inputData = new SimulationInputData(schema);
            inputData.ThrowError = true;
            inputData.LoadFromFile(fileName);
            return inputData;
        }


        private GridderSource CreateGridderSource(SimulationInputData inputData)
        {

            GridDimens dimens = inputData.RootDataFile.GetDIMENS();
            float[] coord = inputData.RootDataFile.GetCOORD();
            float[] zcorn = inputData.RootDataFile.GetZCORN();
            if (zcorn != null && coord != null)
            {
                return CreateCornerPointGridSource(inputData, dimens, coord, zcorn);
            }


            float[] dx = inputData.RootDataFile.GetDX();
            float[] dy = inputData.RootDataFile.GetDY();
            float[] dz = inputData.RootDataFile.GetDZ();
            if (dx != null && dy != null && dz != null)
            {
                return this.CreateCatesianGridderSource(inputData, dimens, dx, dy, dz);
            }

            throw new NotImplementedException("Unknown gridder Source");
        }

        private List<int> CreateAllSlices(int max)
        {

            List<int> results = new List<int>();
            for (int i = 1; i <= max; i++)
            {
                results.Add(i);
            }
            return results;
        }

        private CatesianGridderSource CreateCatesianGridderSource(SimulationInputData inputData, GridDimens dimens, float[] dx, float[] dy, float[] dz)
        {

            if (dimens == null)
                throw new ArgumentException("Missing DIMENS or SPECGRID");
            if (dx == null)
                throw new ArgumentException("Missing DX or related description");
            if (dy == null)
                throw new ArgumentException("Missing DY or related description");
            if (dy == null)
                throw new ArgumentException("Missing DZ or related description");
            CatesianGridderSource cgs = new CatesianGridderSource();
            cgs.NX = dimens.NI;
            cgs.NY = dimens.NJ;
            cgs.NZ = dimens.NK;
            cgs.DX = dx;
            cgs.DY = dy;
            cgs.DZ = dz;
            cgs.TOPS = inputData.RootDataFile.GetTOPS();
            cgs.ActNums = inputData.RootDataFile.GetACTNUM();
            cgs.IBlocks = CreateAllSlices(dimens.NI);
            cgs.JBlocks = CreateAllSlices(dimens.NJ);
            cgs.KBlocks = CreateAllSlices(dimens.NK);
            cgs.Init();
            return cgs;
        }

        private CornerPointGridderSource CreateCornerPointGridSource(SimulationInputData inputData, GridDimens dimens, float[] coord, float[] zcorn)
        {

            if (dimens == null)
                throw new ArgumentException("Missing DIMENS or SPECGRID");
            if (coord == null)
                throw new ArgumentException(String.Format("Missing {0} keyword", EclSymbols.COORD));
            if (zcorn == null)
                throw new ArgumentException(String.Format("Missing {0} keyword", EclSymbols.ZCORN));

            CornerPointGridderSource cpg = new CornerPointGridderSource();
            cpg.NX = dimens.NI;
            cpg.NY = dimens.NJ;
            cpg.NZ = dimens.NK;
            cpg.COORDS = coord;
            cpg.ZCORN = zcorn;
            cpg.ActNums = inputData.RootDataFile.GetACTNUM();
            cpg.IBlocks = CreateAllSlices(dimens.NI);
            cpg.JBlocks = CreateAllSlices(dimens.NJ);
            cpg.KBlocks = CreateAllSlices(dimens.NK);
            cpg.Init();

            return cpg;
        }


        private void UpdateColorIndicator(GridBlockProperty gbp)
        {

            double axisMin, axisMax, step;
            ColorIndicatorAxisAutomator.Automate(gbp.MinValue, gbp.MaxValue, out axisMin, out axisMax, out step);
            this.scene.uiColorIndicator.Data.MinValue = (float)axisMin;
            this.scene.uiColorIndicator.Data.MaxValue = (float)axisMax;
            this.scene.uiColorIndicator.Data.Step = (float)step;
            this.scene.uiColorIndicator.Data.UseLogarithmic = false;

        }


        private SimLabGrid CreateGridder(GridderSource src, GridBlockProperty gbp, out Vertex min, out Vertex max)
        {

            if (src is HexahedronGridderSource)
            {

                this.UpdateColorIndicator(gbp);
                float axisMin = this.scene.uiColorIndicator.Data.MinValue;
                float axisMax = this.scene.uiColorIndicator.Data.MaxValue;

                Bitmap textureBitmap = this.scene.uiColorIndicator.CreateTextureImage();
                HexahedronMeshGeometry3D geometry = src.CreateMesh() as HexahedronMeshGeometry3D;
                HexahedronGrid gridder = new HexahedronGrid(this.scene.OpenGL, this.scene.Scene.CurrentCamera);
                gridder.Init(geometry);
                gridder.SetTexture(textureBitmap);

                TexCoordBuffer textureCoords = src.CreateTextureCoordinates(gbp.Positions, gbp.Values, (float)axisMin, (float)axisMax);
                gridder.SetTextureCoods(textureCoords);
                min = geometry.Min;
                max = geometry.Max;
                return gridder;
            }

            if (src is DynamicUnstructuredGridderSource)
            {

                DynamicUnstructuredGridderSource fractureSource = (DynamicUnstructuredGridderSource)src;
                this.UpdateColorIndicator(gbp);
                float textureMin = this.scene.uiColorIndicator.Data.MinValue;
                float textureMax = this.scene.uiColorIndicator.Data.MaxValue;
                Bitmap textureBitmap = this.scene.uiColorIndicator.CreateTextureImage();
                DynamicUnstructureGeometry geometry = fractureSource.CreateMesh() as DynamicUnstructureGeometry;
                TexCoordBuffer matrixTextureCoords = fractureSource.CreateTextureCoordinates(gbp.Positions, gbp.Values, textureMin, textureMax);
                TexCoordBuffer fracTextureCoords = fractureSource.CreateFractureTextureCoordinates(gbp.Positions, gbp.Values, textureMin, textureMax);

                DynamicUnstructureGrid gridder = new DynamicUnstructureGrid(this.scene.OpenGL, this.scene.Scene.CurrentCamera);
                gridder.Init(geometry);
                gridder.SetTexture(textureBitmap);
                gridder.SetMatrixTextureCoords(matrixTextureCoords);
                gridder.SetFractionTextureCoords(fracTextureCoords);
                gridder.FractionLineWidth = 10;

                min = geometry.Min;
                max = geometry.Max;
                return gridder;
            }


            if (src is PointGridderSource)
            {

                PointGridderSource pgs = (PointGridderSource)src;
                this.UpdateColorIndicator(gbp);
                float textureMin = this.scene.uiColorIndicator.Data.MinValue;
                float textureMax = this.scene.uiColorIndicator.Data.MaxValue;

                PointMeshGeometry3D geometry = pgs.CreateMesh() as PointMeshGeometry3D;
                Bitmap textureBitmap = this.scene.uiColorIndicator.CreateTextureImage();
                TexCoordBuffer textureCoords = pgs.CreateTextureCoordinates(gbp.Positions, gbp.Values, textureMin, textureMax);
                PointRadiusBuffer radiusBuffer = pgs.CreateRadiusBuffer(60);

                PointGrid gridder = new PointGrid(this.scene.OpenGL, this.scene.Scene.CurrentCamera);
                gridder.Init(geometry);
                gridder.SetRadius(radiusBuffer);
                gridder.SetTexture(textureBitmap);
                gridder.SetTextureCoods(textureCoords);
                min = pgs.Min;
                max = pgs.Max;
                return gridder;
            }

            throw new NotImplementedException("Not Supported Gridder");

        }

        private void LoadEclDataClick(object sender, EventArgs e)
        {

            TreeNode treeNode = treeView1.SelectedNode;
            ModelContainer modelContainer = treeNode.Tag as ModelContainer;
            if (modelContainer == null)
            {

                MessageBox.Show(String.Format("Selecte model Node first"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Ecl Data files (*.DATA)|*.DATA|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            string fileName = openFileDialog1.FileName;
            SimulationInputData inputData;
            try
            {
                MouseHelper.WaitIdle();
                inputData = this.LoadEclInputData(fileName);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Load Error,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            GridderSource gridderSource;
            SimLabGrid gridder = null;
            try
            {
                gridderSource = CreateGridderSource(inputData);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Create Gridder Failed,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (gridderSource != null)
            {

                SceneElement sceneElement = treeNode.Tag as SceneElement;
                string caseFileName = System.IO.Path.GetFileName(fileName);
                TreeNode gridderNode = treeNode.Nodes.Add(caseFileName);
                gridderNode.ToolTipText = fileName;
                List<GridBlockProperty> gridProps = inputData.RootDataFile.GetGridProperties();
                if (gridProps.Count <= 0)
                {
                    GridBlockProperty gbp = this.CreateGridSequenceGridBlockProperty(gridderSource, "INDEX");
                    gridProps.Add(gbp);
                }
                foreach (GridBlockProperty gbp in gridProps)
                {
                    TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                    propNode.Tag = gbp;
                }


                Vertex boundMin;
                Vertex boundMax;
                gridder = CreateGridder(gridderSource, gridProps[0], out boundMin, out boundMax);
                if (gridder != null)
                {
                    this.treeView1.ExpandAll();
                    modelContainer.AddChild(gridder);
                    modelContainer.BoundingBox.SetBounds(boundMin, boundMax);
                    this.scene.ViewType = ViewTypes.UserView;
                    gridderNode.Tag = gridder;
                    gridder.Tag = gridderSource;
                }
                this.scene.Invalidate();
            }
        }

        private void LoadFracturesClick(object sender, EventArgs e)
        {
            TreeNode treeNode = treeView1.SelectedNode;
            ModelContainer modelContainer = treeNode.Tag as ModelContainer;
            if (modelContainer == null)
            {
                MessageBox.Show(String.Format("Selecte model Node first"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Simba Fracture files (*.SFRAC)|*.SFRAC|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String pathFileName = openFileDialog1.FileName;
            DynamicUnstructuredGridderSource gridderSource;
            try
            {
                DynamicUnstructureGeometryLoader loader = new DynamicUnstructureGeometryLoader();
                gridderSource = loader.LoadSource(pathFileName);
                gridderSource.Init();
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Load Gridder Failed,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gridderSource != null)
            {

                string fileName = System.IO.Path.GetFileName(pathFileName);
                TreeNode gridderNode = treeNode.Nodes.Add(fileName);
                GridBlockProperty gbp = this.CreateGridSequenceGridBlockProperty(gridderSource, "INDEX");
                TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                propNode.Tag = gbp;
                Vertex min, max;
                SimLabGrid gridder = null;
                try
                {
                    gridder = this.CreateGridder(gridderSource, gbp, out min, out max);
                }
                catch (Exception err)
                {
                    gridderNode.Remove();
                    MessageBox.Show(String.Format("Create Gridder Failed:{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (gridder != null)
                {
                    this.treeView1.ExpandAll();
                    gridderNode.Tag = gridder;
                    gridder.Tag = gridderSource;
                    modelContainer.AddChild(gridder);
                    modelContainer.BoundingBox.SetBounds(min, max);
                    this.scene.ViewType = ViewTypes.UserView;
                    this.scene.Invalidate();
                }
            }

        }


        private GridBlockProperty CreateGridSequenceGridBlockProperty(GridderSource src, string propName)
        {


            int size = src.NX * src.NY * src.NZ;
            int[] gridIndexes = new int[size];
            float[] values = new float[size];
            for (int i = 0; i < size; i++)
            {
                gridIndexes[i] = i;
                values[i] = i + 1;
            }
            GridDimens dimens = new GridDimens(src.NX, src.NY, src.NZ);
            GridBlockProperty gbp = new GridBlockProperty(propName, dimens, gridIndexes, values);
            return gbp;
        }

        private void LoadPointSetCick(object sender, EventArgs e)
        {

            TreeNode treeNode = treeView1.SelectedNode;
            ModelContainer modelContainer = treeNode.Tag as ModelContainer;
            if (modelContainer == null)
            {
                MessageBox.Show(String.Format("Selecte model Node first"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Simba PointSet files (*.xyz)|*.xyz|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string pathFileName = openFileDialog1.FileName;
            PointGridderSource gridderSource;
            try
            {
                PointSetLoader loader = new PointSetLoader();
                gridderSource = loader.LoadFromFile(pathFileName);
                gridderSource.Init();
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Load Points Failed:{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gridderSource == null)
            {
                MessageBox.Show("No Points Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //create point gridder;
            string fileName = System.IO.Path.GetFileName(pathFileName);
            TreeNode gridderNode = treeNode.Nodes.Add(fileName);
            GridBlockProperty gbp = this.CreateGridSequenceGridBlockProperty(gridderSource, "INDEX");
            TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
            propNode.Tag = gbp;

            SimLabGrid gridder;
            Vertex min, max;
            try
            {
                gridder = this.CreateGridder(gridderSource, gbp, out min, out max);
            }
            catch (Exception err)
            {
                gridderNode.Remove();
                MessageBox.Show(String.Format("Create PointSet Failed:{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (gridder != null)
            {
                gridderNode.Tag = gridder;
                gridder.Tag = gridderSource;
                modelContainer.AddChild(gridder);
                modelContainer.BoundingBox.SetBounds(min, max);
                this.scene.ViewType = ViewTypes.UserView;
                this.scene.Invalidate();
                this.treeView1.ExpandAll();
            }

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (e.Node.Tag is SimLabGrid)
                {
                    //add remove button
                    Point p = new Point(e.X, e.Y);
                    this.mnuRemoveGridNode.Enabled = true;
                    this.appContexMenuStrip.Show(this.treeView1, p);
                }
                else
                {
                    this.mnuRemoveGridNode.Enabled = false;

                }

            }
        }

        private void OnRemoveGridNodeClick(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node.Tag is SimLabGrid)
            {

                SimLabGrid grid = (SimLabGrid)node.Tag;
                this.scene.OpenGL.MakeCurrent();
                this.scene.ModelContainer.RemoveChild(grid);
                node.Tag = null;
                node.Remove();
                if (this.scene.ModelContainer.Children.Count <= 0)
                    this.scene.ModelContainer.BoundingBox.IsInitialized = false;
                this.scene.Invalidate();
            }

        }

        private void loadECLPropertyClick(object sender, EventArgs e)
        {
            TreeNode gridderNode = this.treeView1.SelectedNode;
            if (gridderNode == null)
            {
                MessageBox.Show("Select Grid Node First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(gridderNode.Tag is SimLabGrid))
            {
                MessageBox.Show("Select Grid Node First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SimLabGrid gridder = (SimLabGrid)gridderNode.Tag;
            GridderSource gridderSource = gridder.Tag as GridderSource;


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Ecl Data files (*.DATA)|*.DATA|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string fileName = openFileDialog1.FileName;
            SimulationInputData inputData;
            try
            {
                MouseHelper.WaitIdle();
                inputData = this.LoadEclInputData(fileName);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Load Error,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<GridBlockProperty> gridProps = inputData.RootDataFile.GetGridProperties();
            if (gridProps.Count <= 0)
            {
                MessageBox.Show("No Grid Property Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int dimenSize = gridderSource.DimenSize;
            bool notConfirmAsked =false;
            bool notEqualConfirmedAdd = false;
            foreach (GridBlockProperty gbp in gridProps)
            {

                if (gbp.Size != dimenSize)
                {

                    if (!notConfirmAsked)
                    {
                        DialogResult dlgResult = MessageBox.Show(String.Format("Grid {0} Property Size not equal to Grid DIMENS, OK to add,Cancel to ignore", gbp.Name), "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dlgResult == DialogResult.OK)
                        {
                            TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                            propNode.Tag = gbp;
                            notEqualConfirmedAdd = true;
                        }else{
                            notEqualConfirmedAdd = false;
                        }
                        notConfirmAsked = true;
                    }else{
                        if(notEqualConfirmedAdd){
                          TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                          propNode.Tag = gbp;
                        }
                    }
                }
                else
                {
                    TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                    propNode.Tag = gbp;
                }
            }





        }

        private void loadPropertyFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TreeNode gridderNode = this.treeView1.SelectedNode;
            if (gridderNode == null)
            {
                MessageBox.Show("Select Grid Node First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(gridderNode.Tag is SimLabGrid))
            {
                MessageBox.Show("Select Grid Node First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SimLabGrid gridder = (SimLabGrid)gridderNode.Tag;
            GridderSource gridderSource = gridder.Tag as GridderSource;


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Grid Property Data files (*.in)|*.in|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            GridDimens dimens = new GridDimens(gridderSource.NX,gridderSource.NY,gridderSource.NZ);
            try{
              SingleFilePropertyLoader loader = new SingleFilePropertyLoader();
              GridBlockProperty gbp = loader.Load(openFileDialog1.FileName,dimens);
              if(dimens.Size!= gbp.Size){
                DialogResult result = MessageBox.Show(String.Format("Grid {0} Property Size({1}) not equal to Grid DIMENS({2}), OK to add,Cancel to ignore", gbp.Name,gbp.Size,dimens.Size), "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(result == DialogResult.Cancel)
                   return;
              }
              
              
              TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
              propNode.Tag = gbp;
             



            }catch(Exception err){
              MessageBox.Show(String.Format("Load Grid Property Failed,{0}",err.Message),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
              return;
            }
        }

    }
}
