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
using GridViewer.Commands;
using TracyEnergy.Simba.Data;
using TracyEnergy.Simba.Data.Results;
using SimLabBridge;
using Simlab.Well;
using SharpGL.SceneGraph.Primitives;
using GridViewer.Dialogs;
using SimLab.Utils;
using SimLab.Dialogs;
using TracyEnergy.Simba.Data.Well;
using GlmNet;
using System.IO;

namespace GridViewer
{
    public partial class MainWindow : Form
    {
        private List<Command> commands;


        protected List<Command> Commands
        {

            get
            {
                return commands;
            }
            private set
            {
                this.commands = value;
            }

        }

        public MainWindow()
        {
            InitializeComponent();
            this.InitObjectsTree(this.objectsTreeView, this.scene.ModelContainer);
            this.BuildCommands();

            this.scene.CoordinateSystem = CoordinateSystem.RightHand;
            this.scene.ZAxisScale = 1;
            this.UpdateCommandUI(this.objectsTreeView.SelectedNode);
        }

        private void UpdateCommandUI(object param)
        {

            foreach (Command command in this.commands)
            {
                command.UpdateUI(param);
            }
        }

        private void BuildCommands()
        {

            List<Command> cmds = new List<Command>();
            Command cmdLoadEclGrid = new Command(new ToolStripItem[] { this.toolLoadEclGrid, this.mniLoadECLGrid }, this.CanExecuteLoadGrid);
            Command cmdLoadSimbaGrid = new Command(new ToolStripItem[] { this.toolLoadSimbaGrid, this.mniLoadSimbaGrid }, this.CanExecuteLoadGrid);
            Command cmdLoadSimbaPoints = new Command(new ToolStripItem[] { this.toolLoadSimbaPoints, this.mniLoadSimbaPoints }, this.CanExecuteLoadGrid);
            Command cmdLoadEclPropery = new Command(new ToolStripItem[] { this.toolLoadEclProperty, this.mniLoadEclProperty }, this.CanExecuteLoadGridPropery);
            Command cmdLoadProperty = new Command(new ToolStripItem[] { this.toolLoadProperty, this.mniLoadProperty }, this.CanExecuteLoadGridPropery);
            Command cmdLoadCloudResults = new Command(new ToolStripItem[] { this.toolLoadCloudResutls, this.mniLoadCloudResults }, this.CanExecuteLoadGridPropery);
            Command cmdLoadEclWells = new Command(new ToolStripItem[] { this.toolLoadECLWells, this.mniLoadEclWells }, this.CanExecuteLoadEclWells);
            Command cmdSceneColorBarRange = new Command(new ToolStripItem[] { this.toolSceneColorBarRange, this.mniSceneColorBarRange }, this.CanExecuteChangeColorBarRange);
            Command cmdGridderIJKSlices = new Command(new ToolStripItem[] { this.toolIJKSlices, this.mniIJKSlices }, this.CanExecuteIJKSlices);
            Command cmdZDistortion = new Command(new ToolStripItem[] { this.toolZDistortion, this.mniZAxisDistortion }, this.CanExecuteZDistortion);
            Command cmdWellSettings = new Command(new ToolStripItem[] { this.toolWellSettings, this.mniWellSettings }, this.CanExecuteSceneWellSettings);
            Command cmdLoadWellTrajectory = new Command(new ToolStripItem[] { this.toolLoadWellTrajectory, this.mniLoadWellTrajectory }, this.CanExecuteLoadWellTrajectory);
            Command cmdLoadWellInfo  = new Command(new ToolStripItem[]{ this.toolLoadWellInfo, this.mniLoadWellInfo},this.CanExecuteLoadWellTrajectory);
            Command cmdMatrixLayers  = new Command(new ToolStripItem[]{ this.toolMatrixLayers, this.mniMatrixLayers}, this.CanExecuteMatrixLayers);

            cmds.Add(cmdLoadEclGrid);
            cmds.Add(cmdLoadSimbaGrid);
            cmds.Add(cmdLoadSimbaPoints);
            cmds.Add(cmdLoadEclPropery);
            cmds.Add(cmdLoadProperty);
            cmds.Add(cmdLoadCloudResults);
            cmds.Add(cmdLoadEclWells);
            cmds.Add(cmdSceneColorBarRange);
            cmds.Add(cmdGridderIJKSlices);
            cmds.Add(cmdZDistortion);
            cmds.Add(cmdWellSettings);
            cmds.Add(cmdLoadWellTrajectory);
            cmds.Add(cmdLoadWellInfo);
            cmds.Add(cmdMatrixLayers);
            this.Commands = cmds;
        }

        private bool CanExecuteLoadGrid(object param)
        {
            return true;
        }


        private bool CanExecuteSceneWellSettings(object param)
        {

            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;

            TreeNode node = (TreeNode)param;
            if (node.Tag is Folder)
            {
                return node.Nodes.Count > 0;
            }
            return false;
        }

        private bool CanExecuteLoadWellTrajectory(object param)
        {

            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;
            TreeNode node = (TreeNode)param;
            if (node.Tag is SimLabGrid)
                return true;
            return false;
        }

        private bool CanExecuteMatrixLayers(object param){

            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;
            TreeNode node = (TreeNode)param;
            if(node.Tag is GridBlockProperty){
               
              TreeNode gridderNode = this.GetParentNodeTag<DynamicUnstructureGrid>(node);
              if(gridderNode!=null)
                return true;
              return false;
            }
            return false;
        }

        private bool CanExecuteLoadEclWells(object param)
        {
            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;
            TreeNode node = (TreeNode)param;
            if (node.Tag is HexahedronGrid)
                return true;

            //不支持Ecl格式的井加载到无结构化网络中
            if (node.Tag is DynamicUnstructureGrid)
                return false;
            if (node.Tag is PointGrid)
                return true;

            return false;

        }

        private bool CanExecuteChangeColorBarRange(object param)
        {

            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;
            TreeNode node = (TreeNode)param;
            if (node.Tag is GridBlockProperty)
                return true;
            return false;
        }

        private bool CanExecuteLoadGridPropery(object param)
        {

            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;
            TreeNode node = (TreeNode)param;
            if (node.Tag is SimLabGrid)
            {
                return true;
            }
            return false;
        }


        private bool CanExecuteZDistortion(object param)
        {

            if (param == null)
                return false;
            if (!(param is TreeNode))
            {
                return false;
            }
            TreeNode treeNode = (TreeNode)param;
            if(treeNode.Tag is SimLabGrid){
              return true;
            }
            return false;

        }

        private bool CanExecuteIJKSlices(object param)
        {

            if (param == null)
                return false;
            if (!(param is TreeNode))
                return false;

            TreeNode node = (TreeNode)param;
            if (node.Tag is SimLabGrid)
            {
                SimLabGrid gridder = (SimLabGrid)node.Tag;
                if (gridder.Tag is HexahedronGridderSource)
                {

                    HexahedronGridderSource gridderSource = (HexahedronGridderSource)gridder.Tag;
                    if (gridderSource.Tag is TreeNode)
                    {
                        TreeNode propNode = gridderSource.Tag as TreeNode;
                        if (propNode.Tag is GridBlockProperty)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void InitObjectsTree(TreeView tree, ModelContainer modelContainer)
        {
            tree.Tag = modelContainer;
            tree.CheckBoxes = true;
        }


        /// <summary>
        /// 获得父节点Tag是T类型的对象的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode GetParentNodeTag<T>(TreeNode node)
        {

            TreeNode parent = node.Parent;
            while (parent != null)
            {
                if (parent.Tag is T)
                {
                    break;
                }
                parent = parent.Parent;
            }
            return parent;
        }


        /// <summary>
        /// get the first child node tag is T, recursively
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode GetChildNodeTag<T>(TreeNode node){
            
          TreeNode result = null;
          TreeNodeCollection children =   node.Nodes;
          foreach(TreeNode child  in children){

            if(child.Tag is T){
              result = child;
              break;
            }
            if(child.Nodes.Count > 0){

              TreeNode found = GetChildNodeTag<T>(child);
              if(found!=null){
                result = found;
                break;
              }
            }
          }
          return result;
        }

        private List<T> GetChildNodeTagObjects<T>(TreeNode node){
           List<T> list = new List<T>();
           foreach(TreeNode child in node.Nodes){
              this.DoGetNodeChildTagObjects<T>(child,list);
           }
           return list;
        }

        private void DoGetNodeChildTagObjects<T>(TreeNode node,List<T> results){
            
            if(node.Tag is T){
               results.Add((T)node.Tag);
            }
            foreach(TreeNode child in node.Nodes){
               DoGetNodeChildTagObjects<T>(child,results);             
            }
        }

        private void ApplyGridBlockPropertyToGridder(TreeNode gridPropNode)
        {

            GridBlockProperty gbp = gridPropNode.Tag as GridBlockProperty;
            TreeNode gridderNode = this.GetParentNodeTag<SimLabGrid>(gridPropNode);
            SimLabGrid gridder = gridderNode.Tag as SimLabGrid;
            GridderSource gridderSource = gridder.Tag as GridderSource;
            gridderSource.Tag = gridPropNode;

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

        private void OnTreeViewAfterSelected(object sender, TreeViewEventArgs e)
        {

            OnSelectedSceneElementChanged(e.Node);
            if (e.Node.Tag is GridBlockProperty)
            {
                this.ApplyGridBlockPropertyToGridder(e.Node);
            }
            this.UpdateCommandUI(e.Node);
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
            if (gbp.Tag == null)
            {
                ColorIndicatorAxisAutomator.Automate(gbp.MinValue, gbp.MaxValue, out axisMin, out axisMax, out step);
                this.scene.uiColorIndicator.Data.MinValue = (float)axisMin;
                this.scene.uiColorIndicator.Data.MaxValue = (float)axisMax;
                this.scene.uiColorIndicator.Data.Step = (float)step;
                this.scene.uiColorIndicator.Data.UseLogarithmic = false;
            }
            else
            {
                ColorMapParams rangeParams = gbp.Tag as ColorMapParams;
                this.scene.uiColorIndicator.Data.MinValue = (float)rangeParams.MinValue;
                this.scene.uiColorIndicator.Data.MaxValue = (float)rangeParams.MaxValue;
                this.scene.uiColorIndicator.Data.Step = (float)rangeParams.Step;
                this.scene.uiColorIndicator.Data.UseLogarithmic = rangeParams.UseLogarithmic;
                this.scene.uiColorIndicator.Data.LogBase = (float)rangeParams.LogBase;
            }

        }


        private SimLabGrid CreateGridder(GridderSource src, GridBlockProperty gbp)
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
                
                gridder.ZAxisScale = 1;
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
                gridder.ZAxisScale = 1;
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
                PointRadiusBuffer radiusBuffer = pgs.CreateRadiusBuffer(0.5f);

                PointGrid gridder = new PointGrid(this.scene.OpenGL, this.scene.Scene.CurrentCamera);
                gridder.Init(geometry);
                gridder.SetRadius(radiusBuffer);
                gridder.SetTexture(textureBitmap);
                gridder.SetTextureCoods(textureCoords);
                gridder.ZAxisScale = 1;
                
                return gridder;
            }

            throw new NotImplementedException("Not Supported Gridder");

        }

        private void LoadEclGridClick(object sender, EventArgs e)
        {


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Ecl Data files (*.DATA)|*.DATA|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ModelContainer modelContainer = this.ModelContainer;

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


                string caseFileName = System.IO.Path.GetFileName(fileName);
                TreeNode gridderNode = this.objectsTreeView.Nodes.Add(caseFileName);
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

           
                gridder = CreateGridder(gridderSource, gridProps[0]);
                if (gridder != null)
                {
                    this.objectsTreeView.ExpandAll();
                    modelContainer.AddChild(gridder);
                    modelContainer.BoundingBox.SetBounds(gridderSource.TransformedActiveBounds.Min, gridderSource.TransformedActiveBounds.Max);
                    this.scene.ViewType = ViewTypes.UserView;
                    gridderNode.Tag = gridder;
                    gridder.Tag = gridderSource;
                    gridderSource.Tag = gridderNode.Nodes[0];
                    gridderNode.Checked = gridder.IsEnabled;
                    gridderNode.Nodes[0].Checked = true;
                }



                List<Well> well3dList;
                try
                {
                    well3dList = this.CreateWell3D(inputData, this.scene, gridderSource);
                }
                catch (Exception err)
                {
                    MessageBox.Show(String.Format("Create Well3d,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (well3dList != null && well3dList.Count > 0)
                    this.AddWellNodes(gridderNode, this.scene, well3dList);


            }
        }

        private void AddWellNodes(TreeNode gridderNode, ScientificVisual3DControl sceneDisplay, List<Well> well3dList)
        {

            Folder wellFolder = null;
            TreeNode folderNode =  this.GetChildNodeTag<Folder>(gridderNode);
            if(folderNode == null){

                wellFolder = new Folder() { Name = "Wells" };
                folderNode = new TreeNode(wellFolder.Name);
                folderNode.Tag = wellFolder;
                gridderNode.Nodes.Add(folderNode);
                sceneDisplay.ModelContainer.AddChild(wellFolder);
            }
            wellFolder  = folderNode.Tag as Folder;
            foreach (Well well in well3dList)
            {
                TreeNode wellNode = new TreeNode(well.WellName);
                wellNode.Tag = well;
                folderNode.Nodes.Add(wellNode);
                well.Initialize(this.scene.OpenGL);
                wellFolder.AddChild(well);
            }
            sceneDisplay.Invalidate();

        }

        /// <summary>
        /// Unstructed GridderSource粗支持
        /// </summary>
        /// <param name="gridderSource"></param>
        /// <param name="Camera"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private Well3DHelper CreateWell3DHelper(GridderSource gridderSource, ScientificCamera Camera)
        {

            if (gridderSource == null)
                throw new ArgumentNullException("gridder source can not be empty");

            if (gridderSource is HexahedronGridderSource)
            {
                return new HexahedronGridderWell3DHelper((HexahedronGridderSource)gridderSource, Camera);
            }
            if (gridderSource is PointGridderSource)
            {
                return new PointSetGridderWell3DHelper((PointGridderSource)gridderSource, Camera);
            }
            if (gridderSource is DynamicUnstructuredGridderSource)
                throw new ArgumentException("Simba grid can not support create well 3d display.");

            throw new ArgumentException("unknown supported gridder source type.");
        }

        private List<Well> CreateWell3D(SimulationInputData inputData, ScientificVisual3DControl sceneDisplay, GridderSource gridderSource)
        {

            WellSpecsCollection wellSpecsList = inputData.RootDataFile.GetWELSPECS();
            WellCompatCollection wellCompatList = inputData.RootDataFile.GetCOMPDAT();
            if (wellSpecsList == null || wellSpecsList.Count <= 0)
            {
                throw new ArgumentException("not found WELLSPECS info for the well");
            }
            ScientificCamera camera = sceneDisplay.Scene.CurrentCamera;
            Well3DHelper well3DHelper;
            well3DHelper = CreateWell3DHelper(gridderSource, camera);
            return well3DHelper.Convert(wellSpecsList, wellCompatList);

        }

        private void LoadEclWellsClick(object sender, EventArgs e)
        {

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
            TreeNode gridderNode = this.objectsTreeView.SelectedNode;
            gridder = this.objectsTreeView.SelectedNode.Tag as SimLabGrid;
            gridderSource = gridder.Tag as GridderSource;

            List<Well> well3dList;
            try
            {
                well3dList = this.CreateWell3D(inputData, this.scene, gridderSource);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Failed to create Well3d,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (well3dList != null && well3dList.Count > 0)
                this.AddWellNodes(gridderNode, this.scene, well3dList);

        }



        private void LoadSimbaGridClick(object sender, EventArgs e)
        {

            ModelContainer modelContainer = this.ModelContainer;
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
                TreeNode gridderNode = this.objectsTreeView.Nodes.Add(fileName);
                GridBlockProperty gbp = this.CreateGridSequenceGridBlockProperty(gridderSource, "INDEX");
                TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                propNode.Tag = gbp;
                SimLabGrid gridder = null;
                try
                {
                    gridder = this.CreateGridder(gridderSource, gbp);
                }
                catch (Exception err)
                {
                    gridderNode.Remove();
                    MessageBox.Show(String.Format("Create Gridder Failed:{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (gridder != null)
                {
                    this.objectsTreeView.ExpandAll();
                    gridderNode.Tag = gridder;
                    gridder.Tag = gridderSource;
                    modelContainer.AddChild(gridder);
                    modelContainer.BoundingBox.SetBounds(gridderSource.TransformedActiveBounds.Min,gridderSource.TransformedActiveBounds.Max);
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

        private ModelContainer ModelContainer
        {

            get
            {
                return this.objectsTreeView.Tag as ModelContainer;
            }
        }

        private void LoadSimbaPointsCick(object sender, EventArgs e)
        {


            ModelContainer modelContainer = this.ModelContainer;

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
            TreeNode gridderNode = this.objectsTreeView.Nodes.Add(fileName);
            GridBlockProperty gbp = this.CreateGridSequenceGridBlockProperty(gridderSource, "INDEX");
            TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
            propNode.Tag = gbp;

            SimLabGrid gridder;
            try
            {
                gridder = this.CreateGridder(gridderSource, gbp);
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
                modelContainer.BoundingBox.SetBounds(gridderSource.TransformedActiveBounds.Min, gridderSource.TransformedActiveBounds.Max);
                modelContainer.AddChild(gridder);
                this.scene.ViewType = ViewTypes.UserView;
                this.scene.Invalidate();
                this.objectsTreeView.ExpandAll();
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
                    this.appContexMenuStrip.Show(this.objectsTreeView, p);
                }
                else
                {
                    this.mnuRemoveGridNode.Enabled = false;

                }

            }
        }

        private void OnRemoveGridNodeClick(object sender, EventArgs e)
        {
            TreeNode node = this.objectsTreeView.SelectedNode;
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

        private void LoadECLPropertyClick(object sender, EventArgs e)
        {
            TreeNode gridderNode = this.objectsTreeView.SelectedNode;
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
            bool notConfirmAsked = false;
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
                        }
                        else
                        {
                            notEqualConfirmedAdd = false;
                        }
                        notConfirmAsked = true;
                    }
                    else
                    {
                        if (notEqualConfirmedAdd)
                        {
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

        private void LoadPropertyFileClick(object sender, EventArgs e)
        {

            TreeNode gridderNode = this.objectsTreeView.SelectedNode;
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
            GridDimens dimens = new GridDimens(gridderSource.NX, gridderSource.NY, gridderSource.NZ);
            try
            {
                SingleFilePropertyLoader loader = new SingleFilePropertyLoader();
                GridBlockProperty gbp = loader.Load(openFileDialog1.FileName, dimens);
                if (dimens.Size != gbp.Size)
                {
                    DialogResult result = MessageBox.Show(String.Format("Grid {0} Property Size({1}) not equal to Grid DIMENS({2}), OK to add,Cancel to ignore", gbp.Name, gbp.Size, dimens.Size), "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Cancel)
                        return;
                }

                TreeNode propNode = gridderNode.Nodes.Add(gbp.Name);
                propNode.Tag = gbp;
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Load Grid Property Failed,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void LoadCloudResultsClick(object sender, EventArgs e)
        {

            TreeNode gridderNode = this.objectsTreeView.SelectedNode;
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
            openFileDialog1.Filter = "Cloud Results File (*.out)|*.out|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            String pathFileName = openFileDialog1.FileName;
            GridDimens dimens = new GridDimens(gridderSource.NX, gridderSource.NY, gridderSource.NZ);
            int[] actNums = gridderSource.ActNums;
            int activeCount = dimens.Size;
            if (actNums != null)
            {
                int statActCount = 0;
                for (int i = 0; i < actNums.Length; i++)
                {
                    if (actNums[i] > 0)
                        statActCount++;
                }
                activeCount = statActCount;
            }

            GridIndexMapLoader giml = new GridIndexMapLoader();
            String searchDir = Path.GetDirectoryName(pathFileName);
            int[] mapperIndexes =  giml.SmartRead(searchDir);
            gridderSource.GridIndexMapper = mapperIndexes;

            SimulationCaseResult3DData resultLoader = new SimulationCaseResult3DData(null);
            try
            {
                resultLoader.LoadResults(dimens, activeCount, pathFileName);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Load Cloud results failed,{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IList<GridBlockPropertyTimeStepCollection> props = resultLoader.BlockPropertyTimeSteps;
            string fileName = System.IO.Path.GetFileName(pathFileName);

            TreeNode propRoot = new TreeNode(fileName);
            propRoot.Tag = props;
            foreach (GridBlockPropertyTimeStepCollection timeSteps in props)
            {

                TreeNode propertyTimeStepsNode = new TreeNode(timeSteps.BlockPropertyName);
                propertyTimeStepsNode.Tag = timeSteps;
                propRoot.Nodes.Add(propertyTimeStepsNode);
                foreach (TimedStepGridBlockProperty timeStep in timeSteps)
                {
                    TreeNode gbpNode = new TreeNode(timeStep.TimeStep.ToString());
                    gbpNode.Tag = timeStep;
                    propertyTimeStepsNode.Nodes.Add(gbpNode);
                }

            }
            gridderNode.Nodes.Add(propRoot);


        }


        private void SceneBackgroundClicked(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.Color = this.scene.Scene.ClearColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                this.scene.Scene.ClearColor = colorDlg.Color;
                this.scene.Invalidate();
            }

        }

        private void SetView(ScientificVisual3DControl sceneDisplay, ViewTypes viewType)
        {
            sceneDisplay.ViewType = viewType;
            sceneDisplay.Invalidate();
        }

        private void UserViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.UserView);
        }

        private void TopViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.Top);
        }

        private void BottomViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.Bottom);
        }

        private void LeftViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.Left);
        }

        private void RightViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.Right);
        }

        private void BackViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.Back);
        }

        private void FrontViewClick(object sender, EventArgs e)
        {
            this.SetView(this.scene, ViewTypes.Front);
        }

        private void SceneColorBarRangeClick(object sender, EventArgs e)
        {

            TreeNode gridPropNode = this.objectsTreeView.SelectedNode;
            GridBlockProperty gbp = gridPropNode.Tag as GridBlockProperty;

            ColorBarRangeEditDialog rangEditDialog = new ColorBarRangeEditDialog();
            rangEditDialog.StartPosition = FormStartPosition.CenterScreen;

            if (gbp.Tag == null)
            {
                rangEditDialog.UseAutoRange = true;
            }
            else
            {
                ColorMapParams rangeParams = (ColorMapParams)gbp.Tag;
                rangEditDialog.UseAutoRange = rangeParams.IsAutomatic;
                rangEditDialog.UseLogarithmic = rangeParams.UseLogarithmic;
                rangEditDialog.Minimum = rangeParams.MinValue;
                rangEditDialog.Maximum = rangeParams.MaxValue;
                rangEditDialog.Step = rangeParams.Step;
            }
            rangEditDialog.GridBlockPropery = gbp;
            DialogResult result = rangEditDialog.ShowDialog();
            if (result == DialogResult.OK)
            {



                ColorMapParams rangeParams = rangEditDialog.GetColorMapParams();
                if (!rangeParams.IsAutomatic)
                    gbp.Tag = rangeParams;
                else
                    gbp.Tag = null;

                if (!rangeParams.IsAutomatic)
                {

                    if (gbp is TimedStepGridBlockProperty)
                    {

                        if (DialogResult.OK == MessageBox.Show("do you want to apply to all time steps?, if just only the current gridder,select cancel", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        {

                            TreeNode timeStepsNode = this.GetParentNodeTag<GridBlockPropertyTimeStepCollection>(gridPropNode);
                            GridBlockPropertyTimeStepCollection timeStepProperties = timeStepsNode.Tag as GridBlockPropertyTimeStepCollection;
                            foreach (TimedStepGridBlockProperty iter in timeStepProperties)
                            {
                                iter.Tag = rangeParams;
                            }
                        }
                    }
                }
                this.ApplyGridBlockPropertyToGridder(gridPropNode);
            }
        }


        private void OnSceneIJKSlicesApply(object sender, EventArgs e)
        {

            IJKSlicesEditorDialog dlg = sender as IJKSlicesEditorDialog;
            if (dlg != null)
            {
                TreeNode gridderNode = dlg.Tag as TreeNode;
                HexahedronGrid gridder = gridderNode.Tag as HexahedronGrid;
                HexahedronGridderSource gridderSource = gridder.Tag as HexahedronGridderSource;
                gridderSource.IBlocks = dlg.ISlices;
                gridderSource.JBlocks = dlg.JSlices;
                gridderSource.KBlocks = dlg.KSlices;
                gridderSource.Init();

                TreeNode propNode = gridderSource.Tag as TreeNode;

                this.ApplyGridBlockPropertyToGridder(propNode);
            }



        }

        private void SceneIJKSlicesClick(object sender, EventArgs e)
        {
            TreeNode gridderNode = this.objectsTreeView.SelectedNode;
            HexahedronGrid gridder = gridderNode.Tag as HexahedronGrid;
            HexahedronGridderSource gridderSource = gridder.Tag as HexahedronGridderSource;

            IJKSlicesEditorDialog dlg = new IJKSlicesEditorDialog();
            dlg.Apply += OnSceneIJKSlicesApply;
            dlg.Tag = gridderNode;
            dlg.StartPosition = FormStartPosition.CenterScreen;

            dlg.NI = gridderSource.NX;
            dlg.NJ = gridderSource.NY;
            dlg.NK = gridderSource.NZ;
            dlg.ISlices = gridderSource.IBlocks;
            dlg.JSlices = gridderSource.JBlocks;
            dlg.KSlices = gridderSource.KBlocks;
            dlg.Show();
        }

        private void ApplyWellModelTransform(TreeNode gridderNode, mat4 modelMatrix){
           
           List<Well> wells = this.GetChildNodeTagObjects<Well>(gridderNode);
           foreach(Well well in wells){
             well.Transform = modelMatrix;
             well.CreateVisualElements(this.scene.Scene.CurrentCamera);
             well.Initialize(this.scene.OpenGL);
           }
        }

        private void SceneZDistortionClick(object sender, EventArgs e)
        {

            TreeNode gridderNode = this.objectsTreeView.SelectedNode;
            SimLabGrid gridder = gridderNode.Tag as SimLabGrid;
            GridderSource source = gridder.Tag as GridderSource;
            ZAxisDistortionEditorDialog dlg = new ZAxisDistortionEditorDialog();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.Distortion = source.ZScale;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                source.ZScale= dlg.Distortion;
                source.InitTransform();

                TreeNode propNode =  this.GetChildNodeTag<GridBlockProperty>(gridderNode);
                GridBlockProperty gbp = propNode.Tag as GridBlockProperty;
                SimLabGrid ngridder = this.CreateGridder(source,gbp);
                ngridder.Tag = source;
                gridderNode.Tag = ngridder;
                this.scene.ReleaseElement(gridder);
                this.scene.ModelContainer.AddChild(ngridder);
                this.ApplyWellModelTransform(gridderNode,source.ScaleTranslateform);
                this.scene.Invalidate();
            }
        }

        private void SceneClearClick(object sender, EventArgs e)
        {
            this.scene.ReleaseContainerElements();
        }

        private void SceneWellSettingsClick(object sender, EventArgs e)
        {

            TreeNode node = this.objectsTreeView.SelectedNode;
            TreeNode wellNode = node.Nodes[0];
            Well well = (Well)wellNode.Tag;

            WellEditorDialog dlg = new WellEditorDialog();
            dlg.MinZValue = well.WellPathInitMinZ;
            dlg.MaxZValue = well.WellPathInitMaxZ;
            dlg.MaxDisplayZValue = well.WellPathMaxDisplayZ;
            dlg.RadiusScale = well.WellRadiusScale;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //apply to all wells;
                float radiusScale = dlg.RadiusScale;
                float maxWellDisplayZ = dlg.MaxDisplayZValue;
                this.scene.OpenGL.MakeCurrent();
                foreach (TreeNode wnode in node.Nodes)
                {
                    if (wnode.Tag is Well)
                    {
                        Well w = (Well)wnode.Tag;
                        w.WellRadiusScale = radiusScale;
                        w.WellPathMaxDisplayZ = maxWellDisplayZ;
                        w.CreateVisualElements(this.scene.Scene.CurrentCamera);
                        w.Initialize(this.scene.OpenGL);
                    }
                }
                this.scene.Invalidate();
            }
        }

        private void LoadWellTrajectoryClick(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Well Trajectory File (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String pathName = openFileDialog1.FileName;
            WellTrajectoryReader reader = new WellTrajectoryReader();
            WellTrajectory wellTrajectory;
            try
            {
                wellTrajectory = reader.Load(pathName);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("load failed:{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TreeNode gridderNode = objectsTreeView.SelectedNode;
            SimLabGrid gridder = (SimLabGrid)gridderNode.Tag;
            GridderSource source = (GridderSource)gridder.Tag;
            List<WellTrajectory> wellTrajectories = new List<WellTrajectory>();
            wellTrajectories.Add(wellTrajectory);

            Well3DTrajectoryHelper helper = new Well3DTrajectoryHelper(source, this.scene.Scene.CurrentCamera, wellTrajectories);
            List<Well> well3dList = helper.Convert();
            if (well3dList.Count > 0)
            {
                this.AddWellNodes(gridderNode, this.scene, well3dList);
            }
            this.scene.Invalidate();
        }

        private void LoadWellInfoClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Well Trajectory File (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String pathName = openFileDialog1.FileName;
            WellInfoReader reader = new WellInfoReader();
            List<WellInfo> wellInfos;
            try
            {
                wellInfos = reader.Load(pathName);
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("load failed:{0}", err.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TrajectoryConvert convert = new TrajectoryConvert();
            List<WellTrajectory> wellTrajectories = convert.ToTrajectories(wellInfos);

            TreeNode gridderNode = objectsTreeView.SelectedNode;
            SimLabGrid gridder = (SimLabGrid)gridderNode.Tag;
            GridderSource source = (GridderSource)gridder.Tag;
            Well3DTrajectoryHelper helper = new Well3DTrajectoryHelper(source, this.scene.Scene.CurrentCamera, wellTrajectories);
            List<Well> well3dList = helper.Convert();
            this.AddWellNodes(gridderNode, this.scene, well3dList);
            this.scene.Invalidate();

        }

        private void SceneMatrixLayersClick(object sender, EventArgs e)
        {
            TreeNode propNode =   objectsTreeView.SelectedNode;
            TreeNode gridderNode = GetParentNodeTag<SimLabGrid>(propNode);

            GridBlockProperty gbp = propNode.Tag as GridBlockProperty;
            SimLabGrid gridder  = gridderNode.Tag  as SimLabGrid;
            DynamicUnstructuredGridderSource gridderSource = gridder.Tag as DynamicUnstructuredGridderSource;
            
            DynamicSourceLayerEditDialog dlg = new DynamicSourceLayerEditDialog();
            dlg.MatrixCount = gridderSource.ElementNum;
            dlg.MatrixLayers = gridderSource.MatrixLayers;
            dlg.VisibleLayers = gridderSource.VisibleLayers;
            
            if(dlg.ShowDialog()==DialogResult.OK){
               
              List<int>  visibleMatrixLayers = dlg.VisibleLayers;
              int matrixLayers = dlg.MatrixLayers;
              gridderSource.MatrixLayers = matrixLayers;
              gridderSource.VisibleLayers = visibleMatrixLayers;
              gridderSource.BindVisibleMatrix();
              TexCoordBuffer buffer =  gridderSource.CreateTextureCoordinates(gbp.Positions,gbp.Values,(float)gbp.MinValue,(float)gbp.MaxValue);
              gridder.SetTextureCoods(buffer);
              this.scene.Invalidate();      
            }
        }






    }
}
