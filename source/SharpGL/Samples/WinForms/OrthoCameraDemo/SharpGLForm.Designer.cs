namespace OrthoCameraDemo
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
            this.trackLRBT = new System.Windows.Forms.TrackBar();
            this.lblOrthoWidth = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numBack = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numFront = new System.Windows.Forms.NumericUpDown();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cmbCameraPosition = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackLRBT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFront)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(122, 0);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(502, 361);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            // 
            // trackLRBT
            // 
            this.trackLRBT.Location = new System.Drawing.Point(12, 24);
            this.trackLRBT.Name = "trackLRBT";
            this.trackLRBT.Size = new System.Drawing.Size(104, 45);
            this.trackLRBT.TabIndex = 2;
            this.trackLRBT.ValueChanged += new System.EventHandler(this.trackLRBT_ValueChanged);
            // 
            // lblOrthoWidth
            // 
            this.lblOrthoWidth.AutoSize = true;
            this.lblOrthoWidth.Location = new System.Drawing.Point(12, 9);
            this.lblOrthoWidth.Name = "lblOrthoWidth";
            this.lblOrthoWidth.Size = new System.Drawing.Size(77, 12);
            this.lblOrthoWidth.TabIndex = 3;
            this.lblOrthoWidth.Text = "Ortho Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Back:";
            // 
            // numBack
            // 
            this.numBack.Location = new System.Drawing.Point(59, 70);
            this.numBack.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numBack.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numBack.Name = "numBack";
            this.numBack.Size = new System.Drawing.Size(57, 21);
            this.numBack.TabIndex = 4;
            this.numBack.ValueChanged += new System.EventHandler(this.numBack_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Front:";
            // 
            // numFront
            // 
            this.numFront.Location = new System.Drawing.Point(59, 97);
            this.numFront.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFront.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numFront.Name = "numFront";
            this.numFront.Size = new System.Drawing.Size(57, 21);
            this.numFront.TabIndex = 4;
            this.numFront.ValueChanged += new System.EventHandler(this.numFront_ValueChanged);
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInfo.Location = new System.Drawing.Point(12, 167);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(104, 182);
            this.txtInfo.TabIndex = 5;
            // 
            // cmbCameraPosition
            // 
            this.cmbCameraPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameraPosition.FormattingEnabled = true;
            this.cmbCameraPosition.Location = new System.Drawing.Point(12, 141);
            this.cmbCameraPosition.Name = "cmbCameraPosition";
            this.cmbCameraPosition.Size = new System.Drawing.Size(104, 20);
            this.cmbCameraPosition.TabIndex = 6;
            this.cmbCameraPosition.SelectedIndexChanged += new System.EventHandler(this.cmbCameraPosition_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Camera Position:";
            // 
            // SharpGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.cmbCameraPosition);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numFront);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numBack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblOrthoWidth);
            this.Controls.Add(this.trackLRBT);
            this.Controls.Add(this.openGLControl);
            this.Name = "SharpGLForm";
            this.Text = "SharpGL Form";
            this.Load += new System.EventHandler(this.SharpGLForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackLRBT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFront)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.TrackBar trackLRBT;
        private System.Windows.Forms.Label lblOrthoWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFront;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.ComboBox cmbCameraPosition;
        private System.Windows.Forms.Label label4;
    }
}

