namespace ModernOpenGLSample._3MySceneControl
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
            this.mySceneControl = new SharpGL.SceneComponent.ColorCodedPickingSceneControl();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cmbBeginMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mySceneControl)).BeginInit();
            this.SuspendLayout();
            // 
            // mySceneControl
            // 
            this.mySceneControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mySceneControl.DrawFPS = false;
            this.mySceneControl.Location = new System.Drawing.Point(156, 12);
            this.mySceneControl.Name = "mySceneControl";
            this.mySceneControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL4_0;
            this.mySceneControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.mySceneControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.mySceneControl.Size = new System.Drawing.Size(425, 346);
            this.mySceneControl.TabIndex = 0;
            this.mySceneControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mySceneControl_MouseDoubleClick);
            this.mySceneControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.mySceneControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.mySceneControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInfo.Location = new System.Drawing.Point(12, 38);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(138, 320);
            this.txtInfo.TabIndex = 1;
            // 
            // cmbBeginMode
            // 
            this.cmbBeginMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeginMode.FormattingEnabled = true;
            this.cmbBeginMode.Location = new System.Drawing.Point(12, 12);
            this.cmbBeginMode.Name = "cmbBeginMode";
            this.cmbBeginMode.Size = new System.Drawing.Size(138, 20);
            this.cmbBeginMode.TabIndex = 2;
            this.cmbBeginMode.SelectedIndexChanged += new System.EventHandler(this.cmbBeginMode_SelectedIndexChanged);
            // 
            // FormModernSceneControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 370);
            this.Controls.Add(this.cmbBeginMode);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.mySceneControl);
            this.Name = "FormModernSceneControlDemo";
            this.Text = "Modern MySceneControl Demo";
            ((System.ComponentModel.ISupportInitialize)(this.mySceneControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.SceneComponent.ColorCodedPickingSceneControl mySceneControl;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.ComboBox cmbBeginMode;
    }
}