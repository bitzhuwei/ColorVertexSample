namespace ColorVertexSample
{
    partial class FormScientificVisual3DControl
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
            this.scientificVisual3DControl = new SharpGL.SceneComponent.ScientificVisual3DControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCameraType = new System.Windows.Forms.ComboBox();
            this.cmbViewType = new System.Windows.Forms.ComboBox();
            this.chkRenderContainerBox = new System.Windows.Forms.CheckBox();
            this.lblDebugInfo = new System.Windows.Forms.Label();
            this.tbRangeMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRangeMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearModels = new System.Windows.Forms.Button();
            this.btnCreate3D = new System.Windows.Forms.Button();
            this.tbColorIndicatorStep = new System.Windows.Forms.TextBox();
            this.tbRadius = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRadius = new System.Windows.Forms.Label();
            this.tbNZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scientificVisual3DControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.87556F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.12444F));
            this.tableLayoutPanel1.Controls.Add(this.scientificVisual3DControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 468);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // scientificVisual3DControl
            // 
            this.scientificVisual3DControl.CameraType = SharpGL.SceneComponent.ECameraType.Perspecitive;
            this.tableLayoutPanel1.SetColumnSpan(this.scientificVisual3DControl, 2);
            this.scientificVisual3DControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scientificVisual3DControl.DrawFPS = false;
            this.scientificVisual3DControl.Location = new System.Drawing.Point(3, 88);
            this.scientificVisual3DControl.Name = "scientificVisual3DControl";
            this.scientificVisual3DControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.scientificVisual3DControl.RenderBoundingBox = true;
            this.scientificVisual3DControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.scientificVisual3DControl.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.scientificVisual3DControl.Size = new System.Drawing.Size(687, 377);
            this.scientificVisual3DControl.TabIndex = 0;
            this.scientificVisual3DControl.ViewType = SharpGL.SceneComponent.EViewType.UserView;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.cmbCameraType);
            this.panel1.Controls.Add(this.cmbViewType);
            this.panel1.Controls.Add(this.chkRenderContainerBox);
            this.panel1.Controls.Add(this.lblDebugInfo);
            this.panel1.Controls.Add(this.tbRangeMax);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbRangeMin);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnClearModels);
            this.panel1.Controls.Add(this.btnCreate3D);
            this.panel1.Controls.Add(this.tbColorIndicatorStep);
            this.panel1.Controls.Add(this.tbRadius);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblRadius);
            this.panel1.Controls.Add(this.tbNZ);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbNY);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbNX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 79);
            this.panel1.TabIndex = 1;
            // 
            // cmbCameraType
            // 
            this.cmbCameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameraType.FormattingEnabled = true;
            this.cmbCameraType.Location = new System.Drawing.Point(469, 32);
            this.cmbCameraType.Name = "cmbCameraType";
            this.cmbCameraType.Size = new System.Drawing.Size(83, 20);
            this.cmbCameraType.TabIndex = 20;
            this.cmbCameraType.SelectedIndexChanged += new System.EventHandler(this.cmbCameraType_SelectedIndexChanged);
            // 
            // cmbViewType
            // 
            this.cmbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewType.FormattingEnabled = true;
            this.cmbViewType.Location = new System.Drawing.Point(558, 32);
            this.cmbViewType.Name = "cmbViewType";
            this.cmbViewType.Size = new System.Drawing.Size(83, 20);
            this.cmbViewType.TabIndex = 20;
            this.cmbViewType.SelectedIndexChanged += new System.EventHandler(this.cmbViewType_SelectedIndexChanged);
            // 
            // chkRenderContainerBox
            // 
            this.chkRenderContainerBox.AutoSize = true;
            this.chkRenderContainerBox.Checked = true;
            this.chkRenderContainerBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRenderContainerBox.Location = new System.Drawing.Point(528, 55);
            this.chkRenderContainerBox.Name = "chkRenderContainerBox";
            this.chkRenderContainerBox.Size = new System.Drawing.Size(114, 16);
            this.chkRenderContainerBox.TabIndex = 19;
            this.chkRenderContainerBox.Text = "model container";
            this.chkRenderContainerBox.UseVisualStyleBackColor = true;
            this.chkRenderContainerBox.CheckedChanged += new System.EventHandler(this.chkRenderContainerBox_CheckedChanged);
            // 
            // lblDebugInfo
            // 
            this.lblDebugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebugInfo.Location = new System.Drawing.Point(660, 35);
            this.lblDebugInfo.Name = "lblDebugInfo";
            this.lblDebugInfo.Size = new System.Drawing.Size(24, 41);
            this.lblDebugInfo.TabIndex = 17;
            this.lblDebugInfo.Text = "debug info";
            this.lblDebugInfo.Click += new System.EventHandler(this.lblDebugInfo_Click);
            this.lblDebugInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblDebugInfo_MouseClick);
            // 
            // tbRangeMax
            // 
            this.tbRangeMax.Location = new System.Drawing.Point(182, 41);
            this.tbRangeMax.Name = "tbRangeMax";
            this.tbRangeMax.Size = new System.Drawing.Size(100, 21);
            this.tbRangeMax.TabIndex = 16;
            this.tbRangeMax.Text = "1000000.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "max";
            // 
            // tbRangeMin
            // 
            this.tbRangeMin.Location = new System.Drawing.Point(36, 41);
            this.tbRangeMin.Name = "tbRangeMin";
            this.tbRangeMin.Size = new System.Drawing.Size(100, 21);
            this.tbRangeMin.TabIndex = 14;
            this.tbRangeMin.Text = "50000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "min";
            // 
            // btnClearModels
            // 
            this.btnClearModels.Location = new System.Drawing.Point(346, 40);
            this.btnClearModels.Name = "btnClearModels";
            this.btnClearModels.Size = new System.Drawing.Size(52, 23);
            this.btnClearModels.TabIndex = 12;
            this.btnClearModels.Text = "Clear";
            this.btnClearModels.UseVisualStyleBackColor = true;
            this.btnClearModels.Click += new System.EventHandler(this.btnClearModels_Click);
            // 
            // btnCreate3D
            // 
            this.btnCreate3D.Location = new System.Drawing.Point(288, 40);
            this.btnCreate3D.Name = "btnCreate3D";
            this.btnCreate3D.Size = new System.Drawing.Size(52, 23);
            this.btnCreate3D.TabIndex = 12;
            this.btnCreate3D.Text = "Add";
            this.btnCreate3D.UseVisualStyleBackColor = true;
            this.btnCreate3D.Click += new System.EventHandler(this.Create3DObject);
            // 
            // tbColorIndicatorStep
            // 
            this.tbColorIndicatorStep.Location = new System.Drawing.Point(417, 6);
            this.tbColorIndicatorStep.Name = "tbColorIndicatorStep";
            this.tbColorIndicatorStep.Size = new System.Drawing.Size(60, 21);
            this.tbColorIndicatorStep.TabIndex = 11;
            this.tbColorIndicatorStep.Text = "100000";
            // 
            // tbRadius
            // 
            this.tbRadius.Location = new System.Drawing.Point(578, 8);
            this.tbRadius.Name = "tbRadius";
            this.tbRadius.ReadOnly = true;
            this.tbRadius.Size = new System.Drawing.Size(100, 21);
            this.tbRadius.TabIndex = 11;
            this.tbRadius.Text = "0.5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(280, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Color Indicator Step:";
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(526, 12);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(41, 12);
            this.lblRadius.TabIndex = 10;
            this.lblRadius.Text = "Radius";
            // 
            // tbNZ
            // 
            this.tbNZ.Location = new System.Drawing.Point(214, 8);
            this.tbNZ.Name = "tbNZ";
            this.tbNZ.Size = new System.Drawing.Size(60, 21);
            this.tbNZ.TabIndex = 5;
            this.tbNZ.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "NZ:";
            // 
            // tbNY
            // 
            this.tbNY.Location = new System.Drawing.Point(125, 9);
            this.tbNY.Name = "tbNY";
            this.tbNY.Size = new System.Drawing.Size(60, 21);
            this.tbNY.TabIndex = 3;
            this.tbNY.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "NY:";
            // 
            // tbNX
            // 
            this.tbNX.Location = new System.Drawing.Point(30, 11);
            this.tbNX.Name = "tbNX";
            this.tbNX.Size = new System.Drawing.Size(60, 21);
            this.tbNX.TabIndex = 1;
            this.tbNX.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "NX";
            // 
            // FormScientificVisual3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 468);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(16, 38);
            this.Name = "FormScientificVisual3DControl";
            this.Text = "ScientificVisual3DControl Demo.";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scientificVisual3DControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox tbRadius;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRangeMin;
        private System.Windows.Forms.TextBox tbRangeMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDebugInfo;
        private SharpGL.SceneComponent.ScientificVisual3DControl scientificVisual3DControl;
        private System.Windows.Forms.TextBox tbColorIndicatorStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClearModels;
        private System.Windows.Forms.CheckBox chkRenderContainerBox;
        private System.Windows.Forms.ComboBox cmbViewType;
        private System.Windows.Forms.ComboBox cmbCameraType;
    }
}

