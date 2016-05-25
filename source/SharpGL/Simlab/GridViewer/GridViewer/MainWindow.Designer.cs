namespace GridViewer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFracturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPointSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGridPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadECLPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPropertyFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.scene = new SharpGL.SceneComponent.ScientificVisual3DControl();
            this.appContexMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRemoveGridNode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scene)).BeginInit();
            this.appContexMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.loadGridPropertyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1061, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem,
            this.loadFracturesToolStripMenuItem,
            this.loadPointSetToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.loadToolStripMenuItem.Text = "Load Data";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadDataToolStripMenuItem.Text = "Load Ecl GRID";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.LoadEclDataClick);
            // 
            // loadFracturesToolStripMenuItem
            // 
            this.loadFracturesToolStripMenuItem.Name = "loadFracturesToolStripMenuItem";
            this.loadFracturesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadFracturesToolStripMenuItem.Text = "Load Fractures";
            this.loadFracturesToolStripMenuItem.Click += new System.EventHandler(this.LoadFracturesClick);
            // 
            // loadPointSetToolStripMenuItem
            // 
            this.loadPointSetToolStripMenuItem.Name = "loadPointSetToolStripMenuItem";
            this.loadPointSetToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadPointSetToolStripMenuItem.Text = "Load Point Set";
            this.loadPointSetToolStripMenuItem.Click += new System.EventHandler(this.LoadPointSetCick);
            // 
            // loadGridPropertyToolStripMenuItem
            // 
            this.loadGridPropertyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadECLPropertyToolStripMenuItem,
            this.loadPropertyFileToolStripMenuItem});
            this.loadGridPropertyToolStripMenuItem.Name = "loadGridPropertyToolStripMenuItem";
            this.loadGridPropertyToolStripMenuItem.Size = new System.Drawing.Size(132, 21);
            this.loadGridPropertyToolStripMenuItem.Text = "Load Grid Property";
            // 
            // loadECLPropertyToolStripMenuItem
            // 
            this.loadECLPropertyToolStripMenuItem.Name = "loadECLPropertyToolStripMenuItem";
            this.loadECLPropertyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.loadECLPropertyToolStripMenuItem.Text = "Load ECL Property";
            this.loadECLPropertyToolStripMenuItem.Click += new System.EventHandler(this.loadECLPropertyClick);
            // 
            // loadPropertyFileToolStripMenuItem
            // 
            this.loadPropertyFileToolStripMenuItem.Name = "loadPropertyFileToolStripMenuItem";
            this.loadPropertyFileToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.loadPropertyFileToolStripMenuItem.Text = "Load Property File";
            this.loadPropertyFileToolStripMenuItem.Click += new System.EventHandler(this.loadPropertyFileToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1061, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scene);
            this.splitContainer1.Size = new System.Drawing.Size(1061, 555);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(266, 555);
            this.splitContainer2.SplitterDistance = 242;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(266, 242);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeViewAfterSelected);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(266, 309);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // scene
            // 
            this.scene.CameraType = SharpGL.SceneComponent.CameraTypes.Ortho;
            this.scene.CoordinateSystem = SharpGL.SceneComponent.CoordinateSystem.RightHand;
            this.scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scene.DrawFPS = false;
            this.scene.EnablePicking = false;
            this.scene.Location = new System.Drawing.Point(0, 0);
            this.scene.Name = "scene";
            this.scene.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.scene.PickedPrimitive = null;
            this.scene.RenderBoundingBox = false;
            this.scene.RenderContextType = SharpGL.RenderContextType.FBO;
            this.scene.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.scene.Size = new System.Drawing.Size(787, 555);
            this.scene.TabIndex = 0;
            this.scene.ViewType = SharpGL.SceneComponent.ViewTypes.UserView;
            // 
            // appContexMenuStrip
            // 
            this.appContexMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemoveGridNode});
            this.appContexMenuStrip.Name = "appContexMenuStrip";
            this.appContexMenuStrip.Size = new System.Drawing.Size(153, 26);
            // 
            // mnuRemoveGridNode
            // 
            this.mnuRemoveGridNode.Name = "mnuRemoveGridNode";
            this.mnuRemoveGridNode.Size = new System.Drawing.Size(152, 22);
            this.mnuRemoveGridNode.Text = "Remove Grid";
            this.mnuRemoveGridNode.Click += new System.EventHandler(this.OnRemoveGridNodeClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 602);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainWindow";
            this.Text = "GridViewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scene)).EndInit();
            this.appContexMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFracturesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private SharpGL.SceneComponent.ScientificVisual3DControl scene;
        private System.Windows.Forms.ToolStripMenuItem loadPointSetToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip appContexMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveGridNode;
        private System.Windows.Forms.ToolStripMenuItem loadGridPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadECLPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPropertyFileToolStripMenuItem;
    }
}

