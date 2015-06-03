namespace DepthTestWithOrtho
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
            this.scientificControl = new SharpGL.SceneComponent.ScientificVisual3DControl();
            this.cmbCameraType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtZFar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZNear = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scientificControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scientificControl
            // 
            this.scientificControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scientificControl.CameraType = SharpGL.SceneComponent.CameraTypes.Perspecitive;
            this.scientificControl.DrawFPS = true;
            this.scientificControl.Location = new System.Drawing.Point(159, 0);
            this.scientificControl.Name = "scientificControl";
            this.scientificControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.scientificControl.RenderBoundingBox = true;
            this.scientificControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.scientificControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.scientificControl.Size = new System.Drawing.Size(597, 361);
            this.scientificControl.TabIndex = 0;
            this.scientificControl.ViewType = SharpGL.SceneComponent.EViewType.UserView;
            // 
            // cmbCameraType
            // 
            this.cmbCameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameraType.FormattingEnabled = true;
            this.cmbCameraType.Location = new System.Drawing.Point(12, 12);
            this.cmbCameraType.Name = "cmbCameraType";
            this.cmbCameraType.Size = new System.Drawing.Size(141, 20);
            this.cmbCameraType.TabIndex = 1;
            this.cmbCameraType.SelectedIndexChanged += new System.EventHandler(this.cmbCameraType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtZFar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtZNear);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ortho";
            // 
            // txtZFar
            // 
            this.txtZFar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZFar.Location = new System.Drawing.Point(47, 41);
            this.txtZFar.Name = "txtZFar";
            this.txtZFar.Size = new System.Drawing.Size(88, 21);
            this.txtZFar.TabIndex = 1;
            this.txtZFar.TextChanged += new System.EventHandler(this.txtZFar_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "zFar";
            // 
            // txtZNear
            // 
            this.txtZNear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZNear.Location = new System.Drawing.Point(47, 14);
            this.txtZNear.Name = "txtZNear";
            this.txtZNear.Size = new System.Drawing.Size(88, 21);
            this.txtZNear.TabIndex = 1;
            this.txtZNear.TextChanged += new System.EventHandler(this.txtZNear_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "zNear";
            // 
            // FormScientificVisual3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 361);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbCameraType);
            this.Controls.Add(this.scientificControl);
            this.Name = "FormScientificVisual3DControl";
            this.Text = "Scientific Visual 3D Control";
            this.Load += new System.EventHandler(this.SharpGLForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scientificControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.SceneComponent.ScientificVisual3DControl scientificControl;
        private System.Windows.Forms.ComboBox cmbCameraType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtZFar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZNear;
        private System.Windows.Forms.Label label1;
    }
}

