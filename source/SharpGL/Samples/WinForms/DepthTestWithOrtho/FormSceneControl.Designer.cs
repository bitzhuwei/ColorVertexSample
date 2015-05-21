namespace DepthTestWithOrtho
{
    partial class FormSceneControl
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
            this.sceneControl = new SharpGL.SceneControl();
            this.cmbCameraType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtZNear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZFar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.sceneControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sceneControl.DrawFPS = true;
            this.sceneControl.Location = new System.Drawing.Point(159, 0);
            this.sceneControl.Name = "openGLControl";
            this.sceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.sceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.sceneControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.sceneControl.Size = new System.Drawing.Size(465, 361);
            this.sceneControl.TabIndex = 0;
            this.sceneControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.sceneControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.sceneControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.sceneControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.sceneControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.sceneControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "zNear";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "zFar";
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
            // SharpGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbCameraType);
            this.Controls.Add(this.sceneControl);
            this.Name = "SharpGLForm";
            this.Text = "SharpGL Form";
            this.Load += new System.EventHandler(this.SharpGLForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.SceneControl sceneControl;
        private System.Windows.Forms.ComboBox cmbCameraType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtZFar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZNear;
        private System.Windows.Forms.Label label1;
    }
}

