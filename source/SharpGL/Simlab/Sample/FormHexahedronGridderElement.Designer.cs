namespace Sample
{
    partial class FormHexahedronGridderElement
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sim3D = new SharpGL.SceneComponent.ScientificVisual3DControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.barBrightness = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxPropertyMaxValue = new System.Windows.Forms.TextBox();
            this.tbxPropertyMinValue = new System.Windows.Forms.TextBox();
            this.cmbCameraType = new System.Windows.Forms.ComboBox();
            this.cmbViewType = new System.Windows.Forms.ComboBox();
            this.chkRenderContainerBox = new System.Windows.Forms.CheckBox();
            this.btnClearModels = new System.Windows.Forms.Button();
            this.btnCreate3D = new System.Windows.Forms.Button();
            this.tbColorIndicatorStep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDZ = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbNZ = new System.Windows.Forms.TextBox();
            this.gbDY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNY = new System.Windows.Forms.TextBox();
            this.tbDX = new System.Windows.Forms.TextBox();
            this.lblBrightnessValue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.slicesTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbxNI = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbxNJ = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbxNZ = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxShowWireframe = new System.Windows.Forms.CheckBox();
            this.btnSlicesApply = new System.Windows.Forms.Button();
            this.cbxGridProperties = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblPickedPrimitive = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sim3D)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barBrightness)).BeginInit();
            this.slicesTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.87556F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.12444F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 264F));
            this.tableLayoutPanel1.Controls.Add(this.sim3D, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.slicesTab, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1380, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sim3D
            // 
            this.sim3D.CameraType = SharpGL.SceneComponent.CameraTypes.Ortho;
            this.tableLayoutPanel1.SetColumnSpan(this.sim3D, 2);
            this.sim3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sim3D.DrawFPS = false;
            this.sim3D.EnablePicking = false;
            this.sim3D.Location = new System.Drawing.Point(5, 156);
            this.sim3D.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.sim3D.Name = "sim3D";
            this.sim3D.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.sim3D.PickedPrimitive = null;
            this.sim3D.RenderBoundingBox = true;
            this.sim3D.RenderContextType = SharpGL.RenderContextType.FBO;
            this.sim3D.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.sim3D.Size = new System.Drawing.Size(1105, 521);
            this.sim3D.TabIndex = 0;
            this.sim3D.ViewType = SharpGL.SceneComponent.ViewTypes.UserView;
            this.sim3D.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scientificVisual3DControl_KeyPress);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.barBrightness);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tbxPropertyMaxValue);
            this.panel1.Controls.Add(this.tbxPropertyMinValue);
            this.panel1.Controls.Add(this.cmbCameraType);
            this.panel1.Controls.Add(this.cmbViewType);
            this.panel1.Controls.Add(this.chkRenderContainerBox);
            this.panel1.Controls.Add(this.btnClearModels);
            this.panel1.Controls.Add(this.btnCreate3D);
            this.panel1.Controls.Add(this.tbColorIndicatorStep);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbDZ);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbNZ);
            this.panel1.Controls.Add(this.gbDY);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbNY);
            this.panel1.Controls.Add(this.tbDX);
            this.panel1.Controls.Add(this.lblBrightnessValue);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbNX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1107, 144);
            this.panel1.TabIndex = 1;
            // 
            // barBrightness
            // 
            this.barBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barBrightness.Location = new System.Drawing.Point(111, 77);
            this.barBrightness.Maximum = 1000;
            this.barBrightness.Name = "barBrightness";
            this.barBrightness.Size = new System.Drawing.Size(954, 56);
            this.barBrightness.TabIndex = 26;
            this.barBrightness.Value = 100;
            this.barBrightness.Scroll += new System.EventHandler(this.barBrightness_Scroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(873, 12);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "maxValue:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(644, 14);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "minValue:";
            // 
            // tbxPropertyMaxValue
            // 
            this.tbxPropertyMaxValue.Location = new System.Drawing.Point(953, 8);
            this.tbxPropertyMaxValue.Margin = new System.Windows.Forms.Padding(4);
            this.tbxPropertyMaxValue.Name = "tbxPropertyMaxValue";
            this.tbxPropertyMaxValue.Size = new System.Drawing.Size(155, 25);
            this.tbxPropertyMaxValue.TabIndex = 23;
            this.tbxPropertyMaxValue.Text = "200000";
            // 
            // tbxPropertyMinValue
            // 
            this.tbxPropertyMinValue.Location = new System.Drawing.Point(731, 11);
            this.tbxPropertyMinValue.Margin = new System.Windows.Forms.Padding(4);
            this.tbxPropertyMinValue.Name = "tbxPropertyMinValue";
            this.tbxPropertyMinValue.Size = new System.Drawing.Size(132, 25);
            this.tbxPropertyMinValue.TabIndex = 22;
            this.tbxPropertyMinValue.Text = "500";
            // 
            // cmbCameraType
            // 
            this.cmbCameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameraType.FormattingEnabled = true;
            this.cmbCameraType.Location = new System.Drawing.Point(539, 41);
            this.cmbCameraType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCameraType.Name = "cmbCameraType";
            this.cmbCameraType.Size = new System.Drawing.Size(109, 23);
            this.cmbCameraType.TabIndex = 20;
            this.cmbCameraType.SelectedIndexChanged += new System.EventHandler(this.cmbCameraType_SelectedIndexChanged);
            // 
            // cmbViewType
            // 
            this.cmbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewType.FormattingEnabled = true;
            this.cmbViewType.Location = new System.Drawing.Point(657, 41);
            this.cmbViewType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbViewType.Name = "cmbViewType";
            this.cmbViewType.Size = new System.Drawing.Size(109, 23);
            this.cmbViewType.TabIndex = 20;
            this.cmbViewType.SelectedIndexChanged += new System.EventHandler(this.cmbViewType_SelectedIndexChanged);
            // 
            // chkRenderContainerBox
            // 
            this.chkRenderContainerBox.AutoSize = true;
            this.chkRenderContainerBox.Checked = true;
            this.chkRenderContainerBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRenderContainerBox.Location = new System.Drawing.Point(776, 44);
            this.chkRenderContainerBox.Margin = new System.Windows.Forms.Padding(4);
            this.chkRenderContainerBox.Name = "chkRenderContainerBox";
            this.chkRenderContainerBox.Size = new System.Drawing.Size(149, 19);
            this.chkRenderContainerBox.TabIndex = 19;
            this.chkRenderContainerBox.Text = "model container";
            this.chkRenderContainerBox.UseVisualStyleBackColor = true;
            this.chkRenderContainerBox.CheckedChanged += new System.EventHandler(this.chkRenderContainerBox_CheckedChanged);
            // 
            // btnClearModels
            // 
            this.btnClearModels.Location = new System.Drawing.Point(461, 50);
            this.btnClearModels.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearModels.Name = "btnClearModels";
            this.btnClearModels.Size = new System.Drawing.Size(69, 29);
            this.btnClearModels.TabIndex = 12;
            this.btnClearModels.Text = "Clear";
            this.btnClearModels.UseVisualStyleBackColor = true;
            this.btnClearModels.Click += new System.EventHandler(this.btnClearModels_Click);
            // 
            // btnCreate3D
            // 
            this.btnCreate3D.Location = new System.Drawing.Point(384, 50);
            this.btnCreate3D.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreate3D.Name = "btnCreate3D";
            this.btnCreate3D.Size = new System.Drawing.Size(69, 29);
            this.btnCreate3D.TabIndex = 12;
            this.btnCreate3D.Text = "Add";
            this.btnCreate3D.UseVisualStyleBackColor = true;
            this.btnCreate3D.Click += new System.EventHandler(this.CreateCatesianGridVisual3D);
            // 
            // tbColorIndicatorStep
            // 
            this.tbColorIndicatorStep.Location = new System.Drawing.Point(556, 8);
            this.tbColorIndicatorStep.Margin = new System.Windows.Forms.Padding(4);
            this.tbColorIndicatorStep.Name = "tbColorIndicatorStep";
            this.tbColorIndicatorStep.Size = new System.Drawing.Size(79, 25);
            this.tbColorIndicatorStep.TabIndex = 11;
            this.tbColorIndicatorStep.Text = "1000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(373, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Color Indicator Step:";
            // 
            // tbDZ
            // 
            this.tbDZ.Location = new System.Drawing.Point(285, 44);
            this.tbDZ.Margin = new System.Windows.Forms.Padding(4);
            this.tbDZ.Name = "tbDZ";
            this.tbDZ.Size = new System.Drawing.Size(79, 25);
            this.tbDZ.TabIndex = 5;
            this.tbDZ.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(255, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "DZ:";
            // 
            // tbNZ
            // 
            this.tbNZ.Location = new System.Drawing.Point(285, 10);
            this.tbNZ.Margin = new System.Windows.Forms.Padding(4);
            this.tbNZ.Name = "tbNZ";
            this.tbNZ.Size = new System.Drawing.Size(79, 25);
            this.tbNZ.TabIndex = 5;
            this.tbNZ.Text = "3";
            // 
            // gbDY
            // 
            this.gbDY.Location = new System.Drawing.Point(167, 45);
            this.gbDY.Margin = new System.Windows.Forms.Padding(4);
            this.gbDY.Name = "gbDY";
            this.gbDY.Size = new System.Drawing.Size(79, 25);
            this.gbDY.TabIndex = 3;
            this.gbDY.Text = "500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "NZ:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "DY:";
            // 
            // tbNY
            // 
            this.tbNY.Location = new System.Drawing.Point(167, 11);
            this.tbNY.Margin = new System.Windows.Forms.Padding(4);
            this.tbNY.Name = "tbNY";
            this.tbNY.Size = new System.Drawing.Size(79, 25);
            this.tbNY.TabIndex = 3;
            this.tbNY.Text = "3";
            // 
            // tbDX
            // 
            this.tbDX.Location = new System.Drawing.Point(41, 43);
            this.tbDX.Margin = new System.Windows.Forms.Padding(4);
            this.tbDX.Name = "tbDX";
            this.tbDX.Size = new System.Drawing.Size(79, 25);
            this.tbDX.TabIndex = 1;
            this.tbDX.Text = "500";
            // 
            // lblBrightnessValue
            // 
            this.lblBrightnessValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBrightnessValue.AutoSize = true;
            this.lblBrightnessValue.Location = new System.Drawing.Point(1072, 92);
            this.lblBrightnessValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBrightnessValue.Name = "lblBrightnessValue";
            this.lblBrightnessValue.Size = new System.Drawing.Size(31, 15);
            this.lblBrightnessValue.TabIndex = 0;
            this.lblBrightnessValue.Text = "1.0";
            this.lblBrightnessValue.Click += new System.EventHandler(this.lblBrightnessValue_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 92);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "brightness:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "NY:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "DX:";
            // 
            // tbNX
            // 
            this.tbNX.Location = new System.Drawing.Point(41, 9);
            this.tbNX.Margin = new System.Windows.Forms.Padding(4);
            this.tbNX.Name = "tbNX";
            this.tbNX.Size = new System.Drawing.Size(79, 25);
            this.tbNX.TabIndex = 1;
            this.tbNX.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "NX";
            // 
            // slicesTab
            // 
            this.slicesTab.Controls.Add(this.tabPage1);
            this.slicesTab.Controls.Add(this.tabPage2);
            this.slicesTab.Controls.Add(this.tabPage3);
            this.slicesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slicesTab.Location = new System.Drawing.Point(1119, 156);
            this.slicesTab.Margin = new System.Windows.Forms.Padding(4);
            this.slicesTab.Name = "slicesTab";
            this.slicesTab.SelectedIndex = 0;
            this.slicesTab.Size = new System.Drawing.Size(257, 521);
            this.slicesTab.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbxNI);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(249, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NI(NX)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbxNI
            // 
            this.lbxNI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxNI.FormattingEnabled = true;
            this.lbxNI.ItemHeight = 15;
            this.lbxNI.Location = new System.Drawing.Point(4, 4);
            this.lbxNI.Margin = new System.Windows.Forms.Padding(4);
            this.lbxNI.Name = "lbxNI";
            this.lbxNI.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxNI.Size = new System.Drawing.Size(241, 484);
            this.lbxNI.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbxNJ);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(248, 492);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NJ(NY)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbxNJ
            // 
            this.lbxNJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxNJ.FormattingEnabled = true;
            this.lbxNJ.ItemHeight = 15;
            this.lbxNJ.Location = new System.Drawing.Point(4, 4);
            this.lbxNJ.Margin = new System.Windows.Forms.Padding(4);
            this.lbxNJ.Name = "lbxNJ";
            this.lbxNJ.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxNJ.Size = new System.Drawing.Size(240, 484);
            this.lbxNJ.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lbxNZ);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(248, 492);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NK(NZ)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbxNZ
            // 
            this.lbxNZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxNZ.FormattingEnabled = true;
            this.lbxNZ.ItemHeight = 15;
            this.lbxNZ.Location = new System.Drawing.Point(0, 0);
            this.lbxNZ.Margin = new System.Windows.Forms.Padding(4);
            this.lbxNZ.Name = "lbxNZ";
            this.lbxNZ.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxNZ.Size = new System.Drawing.Size(248, 492);
            this.lbxNZ.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxShowWireframe);
            this.panel2.Controls.Add(this.btnSlicesApply);
            this.panel2.Controls.Add(this.cbxGridProperties);
            this.panel2.Location = new System.Drawing.Point(1119, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 125);
            this.panel2.TabIndex = 3;
            // 
            // cbxShowWireframe
            // 
            this.cbxShowWireframe.AutoSize = true;
            this.cbxShowWireframe.Location = new System.Drawing.Point(20, 65);
            this.cbxShowWireframe.Margin = new System.Windows.Forms.Padding(4);
            this.cbxShowWireframe.Name = "cbxShowWireframe";
            this.cbxShowWireframe.Size = new System.Drawing.Size(141, 19);
            this.cbxShowWireframe.TabIndex = 2;
            this.cbxShowWireframe.Text = "Show Wireframe";
            this.cbxShowWireframe.UseVisualStyleBackColor = true;
            this.cbxShowWireframe.CheckedChanged += new System.EventHandler(this.cbxShowWireframe_CheckedChanged);
            // 
            // btnSlicesApply
            // 
            this.btnSlicesApply.Location = new System.Drawing.Point(20, 92);
            this.btnSlicesApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnSlicesApply.Name = "btnSlicesApply";
            this.btnSlicesApply.Size = new System.Drawing.Size(117, 29);
            this.btnSlicesApply.TabIndex = 1;
            this.btnSlicesApply.Text = "Slices";
            this.btnSlicesApply.UseVisualStyleBackColor = true;
            this.btnSlicesApply.Click += new System.EventHandler(this.btnSlicesApply_Click);
            // 
            // cbxGridProperties
            // 
            this.cbxGridProperties.FormattingEnabled = true;
            this.cbxGridProperties.Location = new System.Drawing.Point(20, 19);
            this.cbxGridProperties.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGridProperties.Name = "cbxGridProperties";
            this.cbxGridProperties.Size = new System.Drawing.Size(220, 23);
            this.cbxGridProperties.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPickedPrimitive});
            this.statusStrip1.Location = new System.Drawing.Point(0, 687);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1380, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblPickedPrimitive
            // 
            this.lblPickedPrimitive.Name = "lblPickedPrimitive";
            this.lblPickedPrimitive.Size = new System.Drawing.Size(61, 20);
            this.lblPickedPrimitive.Text = "Picked:";
            // 
            // FormHexahedronGridderElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 712);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(18, 47);
            this.Name = "FormHexahedronGridderElement";
            this.Text = "ScientificVisual3DControl Demo.";
            this.Load += new System.EventHandler(this.FormHexahedronGridder_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sim3D)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barBrightness)).EndInit();
            this.slicesTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbNZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate3D;
        private SharpGL.SceneComponent.ScientificVisual3DControl sim3D;
        private System.Windows.Forms.TextBox tbColorIndicatorStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClearModels;
        private System.Windows.Forms.CheckBox chkRenderContainerBox;
        private System.Windows.Forms.ComboBox cmbViewType;
        private System.Windows.Forms.ComboBox cmbCameraType;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblPickedPrimitive;
        private System.Windows.Forms.TextBox tbDZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox gbDY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl slicesTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox lbxNI;
        private System.Windows.Forms.ListBox lbxNJ;
        private System.Windows.Forms.ListBox lbxNZ;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbxGridProperties;
        private System.Windows.Forms.Button btnSlicesApply;
        private System.Windows.Forms.TextBox tbxPropertyMaxValue;
        private System.Windows.Forms.TextBox tbxPropertyMinValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbxShowWireframe;
        private System.Windows.Forms.TrackBar barBrightness;
        private System.Windows.Forms.Label lblBrightnessValue;
        private System.Windows.Forms.Label label10;
    }
}

