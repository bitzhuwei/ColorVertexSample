﻿namespace ColorVertexSample
{
    partial class FormUnStructuredGridderElement
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
            this.lblPickingInfo = new System.Windows.Forms.Label();
            this.cmbCameraType = new System.Windows.Forms.ComboBox();
            this.cmbViewType = new System.Windows.Forms.ComboBox();
            this.chkrenderTetrasWireframe = new System.Windows.Forms.CheckBox();
            this.chkrenderTetras = new System.Windows.Forms.CheckBox();
            this.chkrenderFractionsWireframe = new System.Windows.Forms.CheckBox();
            this.chkrenderFractions = new System.Windows.Forms.CheckBox();
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblPickedPrimitive = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scientificVisual3DControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.87556F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.12444F));
            this.tableLayoutPanel1.Controls.Add(this.scientificVisual3DControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1025, 554);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // scientificVisual3DControl
            // 
            this.scientificVisual3DControl.CameraType = SharpGL.SceneComponent.CameraTypes.Perspecitive;
            this.tableLayoutPanel1.SetColumnSpan(this.scientificVisual3DControl, 2);
            this.scientificVisual3DControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scientificVisual3DControl.DrawFPS = false;
            this.scientificVisual3DControl.EnablePicking = false;
            this.scientificVisual3DControl.Location = new System.Drawing.Point(5, 139);
            this.scientificVisual3DControl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.scientificVisual3DControl.Name = "scientificVisual3DControl";
            this.scientificVisual3DControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.scientificVisual3DControl.PickedPrimitive = null;
            this.scientificVisual3DControl.RenderBoundingBox = true;
            this.scientificVisual3DControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.scientificVisual3DControl.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.scientificVisual3DControl.Size = new System.Drawing.Size(1015, 411);
            this.scientificVisual3DControl.TabIndex = 0;
            this.scientificVisual3DControl.ViewType = SharpGL.SceneComponent.ViewTypes.UserView;
            this.scientificVisual3DControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scientificVisual3DControl_KeyPress);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.lblPickingInfo);
            this.panel1.Controls.Add(this.cmbCameraType);
            this.panel1.Controls.Add(this.cmbViewType);
            this.panel1.Controls.Add(this.chkrenderTetrasWireframe);
            this.panel1.Controls.Add(this.chkrenderTetras);
            this.panel1.Controls.Add(this.chkrenderFractionsWireframe);
            this.panel1.Controls.Add(this.chkrenderFractions);
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
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbNX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1017, 127);
            this.panel1.TabIndex = 1;
            // 
            // lblPickingInfo
            // 
            this.lblPickingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPickingInfo.ForeColor = System.Drawing.Color.Red;
            this.lblPickingInfo.Location = new System.Drawing.Point(4, 81);
            this.lblPickingInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPickingInfo.Name = "lblPickingInfo";
            this.lblPickingInfo.Size = new System.Drawing.Size(762, 41);
            this.lblPickingInfo.TabIndex = 21;
            this.lblPickingInfo.Text = "Picking:";
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
            // chkrenderTetrasWireframe
            // 
            this.chkrenderTetrasWireframe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkrenderTetrasWireframe.AutoSize = true;
            this.chkrenderTetrasWireframe.Location = new System.Drawing.Point(779, 85);
            this.chkrenderTetrasWireframe.Margin = new System.Windows.Forms.Padding(4);
            this.chkrenderTetrasWireframe.Name = "chkrenderTetrasWireframe";
            this.chkrenderTetrasWireframe.Size = new System.Drawing.Size(197, 19);
            this.chkrenderTetrasWireframe.TabIndex = 19;
            this.chkrenderTetrasWireframe.Text = "renderTetrasWireframe";
            this.chkrenderTetrasWireframe.UseVisualStyleBackColor = true;
            this.chkrenderTetrasWireframe.CheckedChanged += new System.EventHandler(this.chkrenderTetrasWireframe_CheckedChanged);
            // 
            // chkrenderTetras
            // 
            this.chkrenderTetras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkrenderTetras.AutoSize = true;
            this.chkrenderTetras.Checked = true;
            this.chkrenderTetras.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkrenderTetras.Location = new System.Drawing.Point(779, 58);
            this.chkrenderTetras.Margin = new System.Windows.Forms.Padding(4);
            this.chkrenderTetras.Name = "chkrenderTetras";
            this.chkrenderTetras.Size = new System.Drawing.Size(125, 19);
            this.chkrenderTetras.TabIndex = 19;
            this.chkrenderTetras.Text = "renderTetras";
            this.chkrenderTetras.UseVisualStyleBackColor = true;
            this.chkrenderTetras.CheckedChanged += new System.EventHandler(this.chkrenderTetras_CheckedChanged);
            // 
            // chkrenderFractionsWireframe
            // 
            this.chkrenderFractionsWireframe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkrenderFractionsWireframe.AutoSize = true;
            this.chkrenderFractionsWireframe.Location = new System.Drawing.Point(779, 31);
            this.chkrenderFractionsWireframe.Margin = new System.Windows.Forms.Padding(4);
            this.chkrenderFractionsWireframe.Name = "chkrenderFractionsWireframe";
            this.chkrenderFractionsWireframe.Size = new System.Drawing.Size(221, 19);
            this.chkrenderFractionsWireframe.TabIndex = 19;
            this.chkrenderFractionsWireframe.Text = "renderFractionsWireframe";
            this.chkrenderFractionsWireframe.UseVisualStyleBackColor = true;
            this.chkrenderFractionsWireframe.CheckedChanged += new System.EventHandler(this.chkrenderFractionsWireframe_CheckedChanged);
            // 
            // chkrenderFractions
            // 
            this.chkrenderFractions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkrenderFractions.AutoSize = true;
            this.chkrenderFractions.Checked = true;
            this.chkrenderFractions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkrenderFractions.Location = new System.Drawing.Point(779, 4);
            this.chkrenderFractions.Margin = new System.Windows.Forms.Padding(4);
            this.chkrenderFractions.Name = "chkrenderFractions";
            this.chkrenderFractions.Size = new System.Drawing.Size(149, 19);
            this.chkrenderFractions.TabIndex = 19;
            this.chkrenderFractions.Text = "renderFractions";
            this.chkrenderFractions.UseVisualStyleBackColor = true;
            this.chkrenderFractions.CheckedChanged += new System.EventHandler(this.chkrenderFractions_CheckedChanged);
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
            this.btnCreate3D.Click += new System.EventHandler(this.Create3DObject);
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
            this.tbNZ.Text = "8";
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
            this.tbNY.Text = "10";
            // 
            // tbDX
            // 
            this.tbDX.Location = new System.Drawing.Point(40, 48);
            this.tbDX.Margin = new System.Windows.Forms.Padding(4);
            this.tbDX.Name = "tbDX";
            this.tbDX.Size = new System.Drawing.Size(79, 25);
            this.tbDX.TabIndex = 1;
            this.tbDX.Text = "500";
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
            this.label4.Location = new System.Drawing.Point(9, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "DX:";
            // 
            // tbNX
            // 
            this.tbNX.Location = new System.Drawing.Point(40, 14);
            this.tbNX.Margin = new System.Windows.Forms.Padding(4);
            this.tbNX.Name = "tbNX";
            this.tbNX.Size = new System.Drawing.Size(79, 25);
            this.tbNX.TabIndex = 1;
            this.tbNX.Text = "6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "NX";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPickedPrimitive});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1025, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblPickedPrimitive
            // 
            this.lblPickedPrimitive.Name = "lblPickedPrimitive";
            this.lblPickedPrimitive.Size = new System.Drawing.Size(61, 20);
            this.lblPickedPrimitive.Text = "Picked:";
            // 
            // FormUnStructuredGridderElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 585);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(18, 47);
            this.Name = "FormUnStructuredGridderElement";
            this.Text = "UnStructuredGridderElement";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scientificVisual3DControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCreate3D;
        private SharpGL.SceneComponent.ScientificVisual3DControl scientificVisual3DControl;
        private System.Windows.Forms.TextBox tbColorIndicatorStep;
        private System.Windows.Forms.Button btnClearModels;
        private System.Windows.Forms.CheckBox chkrenderFractions;
        private System.Windows.Forms.ComboBox cmbViewType;
        private System.Windows.Forms.ComboBox cmbCameraType;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblPickedPrimitive;
        private System.Windows.Forms.Label lblPickingInfo;
        private System.Windows.Forms.CheckBox chkrenderTetrasWireframe;
        private System.Windows.Forms.CheckBox chkrenderTetras;
        private System.Windows.Forms.CheckBox chkrenderFractionsWireframe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbNZ;
        private System.Windows.Forms.TextBox gbDY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNY;
        private System.Windows.Forms.TextBox tbDX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNX;
        private System.Windows.Forms.Label label1;
    }
}

