namespace ColorVertexSample
{
    partial class FormSceneControlDemo
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
            this.rdoOrtho = new System.Windows.Forms.RadioButton();
            this.rdoPerspective = new System.Windows.Forms.RadioButton();
            this.btnNewDemo = new System.Windows.Forms.Button();
            this.cmbRenderOrder = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sceneControl
            // 
            this.sceneControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sceneControl.DrawFPS = false;
            this.sceneControl.Location = new System.Drawing.Point(12, 34);
            this.sceneControl.Name = "sceneControl";
            this.sceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.sceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.sceneControl.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.sceneControl.Size = new System.Drawing.Size(640, 347);
            this.sceneControl.TabIndex = 0;
            // 
            // rdoOrtho
            // 
            this.rdoOrtho.AutoSize = true;
            this.rdoOrtho.Location = new System.Drawing.Point(107, 12);
            this.rdoOrtho.Name = "rdoOrtho";
            this.rdoOrtho.Size = new System.Drawing.Size(53, 16);
            this.rdoOrtho.TabIndex = 19;
            this.rdoOrtho.TabStop = true;
            this.rdoOrtho.Text = "Ortho";
            this.rdoOrtho.UseVisualStyleBackColor = true;
            this.rdoOrtho.CheckedChanged += new System.EventHandler(this.rdoOrtho_CheckedChanged);
            // 
            // rdoPerspective
            // 
            this.rdoPerspective.AutoSize = true;
            this.rdoPerspective.Checked = true;
            this.rdoPerspective.Location = new System.Drawing.Point(12, 12);
            this.rdoPerspective.Name = "rdoPerspective";
            this.rdoPerspective.Size = new System.Drawing.Size(89, 16);
            this.rdoPerspective.TabIndex = 20;
            this.rdoPerspective.TabStop = true;
            this.rdoPerspective.Text = "Perspective";
            this.rdoPerspective.UseVisualStyleBackColor = true;
            this.rdoPerspective.CheckedChanged += new System.EventHandler(this.rdoPerspective_CheckedChanged);
            // 
            // btnNewDemo
            // 
            this.btnNewDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewDemo.Location = new System.Drawing.Point(520, 6);
            this.btnNewDemo.Name = "btnNewDemo";
            this.btnNewDemo.Size = new System.Drawing.Size(132, 23);
            this.btnNewDemo.TabIndex = 21;
            this.btnNewDemo.Text = "MySceneControl Demo";
            this.btnNewDemo.UseVisualStyleBackColor = true;
            this.btnNewDemo.Click += new System.EventHandler(this.btnNewDemo_Click);
            // 
            // cmbRenderOrder
            // 
            this.cmbRenderOrder.FormattingEnabled = true;
            this.cmbRenderOrder.Location = new System.Drawing.Point(249, 8);
            this.cmbRenderOrder.Name = "cmbRenderOrder";
            this.cmbRenderOrder.Size = new System.Drawing.Size(121, 20);
            this.cmbRenderOrder.TabIndex = 22;
            this.cmbRenderOrder.SelectedIndexChanged += new System.EventHandler(this.cmbRenderOrder_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "模型绘制顺序";
            // 
            // FormSceneControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 393);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRenderOrder);
            this.Controls.Add(this.btnNewDemo);
            this.Controls.Add(this.rdoOrtho);
            this.Controls.Add(this.rdoPerspective);
            this.Controls.Add(this.sceneControl);
            this.Name = "FormSceneControlDemo";
            this.Text = "SceneControl Demo";
            this.Load += new System.EventHandler(this.FormSceneControlDemo_Load);
            this.Resize += new System.EventHandler(this.FormSceneControlDemo_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.SceneControl sceneControl;
        private System.Windows.Forms.RadioButton rdoOrtho;
        private System.Windows.Forms.RadioButton rdoPerspective;
        private System.Windows.Forms.Button btnNewDemo;
        private System.Windows.Forms.ComboBox cmbRenderOrder;
        private System.Windows.Forms.Label label1;
    }
}