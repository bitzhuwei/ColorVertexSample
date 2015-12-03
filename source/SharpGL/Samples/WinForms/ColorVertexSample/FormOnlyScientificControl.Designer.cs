namespace ColorVertexSample
{
    partial class FormOnlyScientificControl
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
            this.scientificVisual3DControl = new SharpGL.SceneComponent.ScientificVisual3DControl();
            ((System.ComponentModel.ISupportInitialize)(this.scientificVisual3DControl)).BeginInit();
            this.SuspendLayout();
            // 
            // scientificVisual3DControl
            // 
            this.scientificVisual3DControl.CameraType = SharpGL.SceneComponent.CameraTypes.Ortho;
            this.scientificVisual3DControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scientificVisual3DControl.DrawFPS = false;
            this.scientificVisual3DControl.EnablePicking = false;
            this.scientificVisual3DControl.Location = new System.Drawing.Point(0, 0);
            this.scientificVisual3DControl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.scientificVisual3DControl.Name = "scientificVisual3DControl";
            this.scientificVisual3DControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.scientificVisual3DControl.PickedPrimitive = null;
            this.scientificVisual3DControl.RenderBoundingBox = true;
            this.scientificVisual3DControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.scientificVisual3DControl.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.scientificVisual3DControl.Size = new System.Drawing.Size(664, 389);
            this.scientificVisual3DControl.TabIndex = 1;
            this.scientificVisual3DControl.ViewType = SharpGL.SceneComponent.ViewTypes.UserView;
            // 
            // FormOnlyScientificControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 389);
            this.Controls.Add(this.scientificVisual3DControl);
            this.Name = "FormOnlyScientificControl";
            this.Text = "FormOnlyScientificControl";
            ((System.ComponentModel.ISupportInitialize)(this.scientificVisual3DControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.SceneComponent.ScientificVisual3DControl scientificVisual3DControl;

    }
}