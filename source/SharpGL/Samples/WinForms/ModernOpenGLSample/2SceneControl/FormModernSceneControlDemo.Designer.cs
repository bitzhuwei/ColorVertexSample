namespace ModernOpenGLSample._2SceneControl
{
    partial class FormModernSceneControlDemo
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
            this.txtInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.sceneControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sceneControl.DrawFPS = false;
            this.sceneControl.Location = new System.Drawing.Point(156, 12);
            this.sceneControl.Name = "openGLControl";
            this.sceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL4_0;
            this.sceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.sceneControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.sceneControl.Size = new System.Drawing.Size(425, 346);
            this.sceneControl.TabIndex = 0;
            this.sceneControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.sceneControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.sceneControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInfo.Location = new System.Drawing.Point(12, 12);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(138, 346);
            this.txtInfo.TabIndex = 1;
            // 
            // FormModernSceneControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 370);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.sceneControl);
            this.Name = "FormModernSceneControlDemo";
            this.Text = "Modern SceneControl Demo";
            ((System.ComponentModel.ISupportInitialize)(this.sceneControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.SceneControl sceneControl;
        private System.Windows.Forms.TextBox txtInfo;
    }
}