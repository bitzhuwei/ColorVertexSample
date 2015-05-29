namespace ShadeModeDemo
{
    partial class SharpGLForm
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
            this.openGLControl = new SharpGL.OpenGLControl();
            this.cmbBeginMode = new System.Windows.Forms.ComboBox();
            this.cmbShadeMode = new System.Windows.Forms.ComboBox();
            this.cmbRotation = new System.Windows.Forms.ComboBox();
            this.txtModelInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(181, 0);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(443, 414);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            // 
            // cmbBeginMode
            // 
            this.cmbBeginMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeginMode.FormattingEnabled = true;
            this.cmbBeginMode.Location = new System.Drawing.Point(12, 12);
            this.cmbBeginMode.Name = "cmbBeginMode";
            this.cmbBeginMode.Size = new System.Drawing.Size(163, 20);
            this.cmbBeginMode.TabIndex = 1;
            this.cmbBeginMode.SelectedIndexChanged += new System.EventHandler(this.cmbBeginMode_SelectedIndexChanged);
            // 
            // cmbShadeMode
            // 
            this.cmbShadeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShadeMode.FormattingEnabled = true;
            this.cmbShadeMode.Location = new System.Drawing.Point(12, 38);
            this.cmbShadeMode.Name = "cmbShadeMode";
            this.cmbShadeMode.Size = new System.Drawing.Size(163, 20);
            this.cmbShadeMode.TabIndex = 1;
            this.cmbShadeMode.SelectedIndexChanged += new System.EventHandler(this.cmbShadeMode_SelectedIndexChanged);
            // 
            // cmbRotation
            // 
            this.cmbRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRotation.FormattingEnabled = true;
            this.cmbRotation.Location = new System.Drawing.Point(12, 64);
            this.cmbRotation.Name = "cmbRotation";
            this.cmbRotation.Size = new System.Drawing.Size(163, 20);
            this.cmbRotation.TabIndex = 1;
            this.cmbRotation.SelectedIndexChanged += new System.EventHandler(this.cmbRotation_SelectedIndexChanged);
            // 
            // txtModelInfo
            // 
            this.txtModelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtModelInfo.Location = new System.Drawing.Point(12, 90);
            this.txtModelInfo.Multiline = true;
            this.txtModelInfo.Name = "txtModelInfo";
            this.txtModelInfo.ReadOnly = true;
            this.txtModelInfo.Size = new System.Drawing.Size(163, 312);
            this.txtModelInfo.TabIndex = 2;
            // 
            // SharpGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 414);
            this.Controls.Add(this.txtModelInfo);
            this.Controls.Add(this.cmbRotation);
            this.Controls.Add(this.cmbShadeMode);
            this.Controls.Add(this.cmbBeginMode);
            this.Controls.Add(this.openGLControl);
            this.Name = "SharpGLForm";
            this.Text = "Shade Mode demo.";
            this.Load += new System.EventHandler(this.SharpGLForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.ComboBox cmbBeginMode;
        private System.Windows.Forms.ComboBox cmbShadeMode;
        private System.Windows.Forms.ComboBox cmbRotation;
        private System.Windows.Forms.TextBox txtModelInfo;
    }
}

