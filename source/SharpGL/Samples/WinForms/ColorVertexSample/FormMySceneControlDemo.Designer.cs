using SharpGL.SceneComponent;
namespace ColorVertexSample
{
    partial class FormMySceneControlDemo
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
            this.rdoOrtho = new System.Windows.Forms.RadioButton();
            this.rdoPerspective = new System.Windows.Forms.RadioButton();
            this.mySceneControl = new SharpGL.SceneComponent.ColorCodedPickingSceneControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRenderOrder = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mySceneControl)).BeginInit();
            this.SuspendLayout();
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
            // sceneControl
            // 
            this.mySceneControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mySceneControl.DrawFPS = false;
            this.mySceneControl.Location = new System.Drawing.Point(12, 34);
            this.mySceneControl.Name = "sceneControl";
            this.mySceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.mySceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.mySceneControl.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.mySceneControl.Size = new System.Drawing.Size(640, 347);
            this.mySceneControl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "模型绘制顺序";
            // 
            // cmbRenderOrder
            // 
            this.cmbRenderOrder.FormattingEnabled = true;
            this.cmbRenderOrder.Location = new System.Drawing.Point(249, 8);
            this.cmbRenderOrder.Name = "cmbRenderOrder";
            this.cmbRenderOrder.Size = new System.Drawing.Size(121, 20);
            this.cmbRenderOrder.TabIndex = 24;
            this.cmbRenderOrder.SelectedIndexChanged += new System.EventHandler(this.cmbRenderOrder_SelectedIndexChanged);
            // 
            // FormMySceneControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 393);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRenderOrder);
            this.Controls.Add(this.rdoOrtho);
            this.Controls.Add(this.rdoPerspective);
            this.Controls.Add(this.mySceneControl);
            this.Name = "FormMySceneControlDemo";
            this.Text = "MySceneControl Demo";
            this.Load += new System.EventHandler(this.FormMySceneControlDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mySceneControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorCodedPickingSceneControl mySceneControl;
        private System.Windows.Forms.RadioButton rdoOrtho;
        private System.Windows.Forms.RadioButton rdoPerspective;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRenderOrder;
    }
}