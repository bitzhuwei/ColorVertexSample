namespace DepthTestWithOrtho
{
    partial class FormMain
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
            this.btnOpenGLControl = new System.Windows.Forms.Button();
            this.btnSceneControl = new System.Windows.Forms.Button();
            this.btnMySceneControl = new System.Windows.Forms.Button();
            this.btnScientificControl = new System.Windows.Forms.Button();
            this.btnScientificVisual3DControl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenGLControl
            // 
            this.btnOpenGLControl.Location = new System.Drawing.Point(12, 28);
            this.btnOpenGLControl.Name = "btnOpenGLControl";
            this.btnOpenGLControl.Size = new System.Drawing.Size(260, 23);
            this.btnOpenGLControl.TabIndex = 1;
            this.btnOpenGLControl.Text = "OpenGLControl";
            this.btnOpenGLControl.UseVisualStyleBackColor = true;
            this.btnOpenGLControl.Click += new System.EventHandler(this.btnOpenGLControl_Click);
            // 
            // btnSceneControl
            // 
            this.btnSceneControl.Location = new System.Drawing.Point(12, 57);
            this.btnSceneControl.Name = "btnSceneControl";
            this.btnSceneControl.Size = new System.Drawing.Size(260, 23);
            this.btnSceneControl.TabIndex = 1;
            this.btnSceneControl.Text = "SceneControl";
            this.btnSceneControl.UseVisualStyleBackColor = true;
            this.btnSceneControl.Click += new System.EventHandler(this.btnSceneControl_Click);
            // 
            // btnMySceneControl
            // 
            this.btnMySceneControl.Location = new System.Drawing.Point(12, 86);
            this.btnMySceneControl.Name = "btnMySceneControl";
            this.btnMySceneControl.Size = new System.Drawing.Size(260, 23);
            this.btnMySceneControl.TabIndex = 1;
            this.btnMySceneControl.Text = "MySceneControl";
            this.btnMySceneControl.UseVisualStyleBackColor = true;
            this.btnMySceneControl.Click += new System.EventHandler(this.btnMySceneControl_Click);
            // 
            // btnScientificControl
            // 
            this.btnScientificControl.Location = new System.Drawing.Point(12, 115);
            this.btnScientificControl.Name = "btnScientificControl";
            this.btnScientificControl.Size = new System.Drawing.Size(260, 23);
            this.btnScientificControl.TabIndex = 1;
            this.btnScientificControl.Text = "ScientificControl";
            this.btnScientificControl.UseVisualStyleBackColor = true;
            this.btnScientificControl.Click += new System.EventHandler(this.btnScientificControl_Click);
            // 
            // btnScientificVisual3DControl
            // 
            this.btnScientificVisual3DControl.Location = new System.Drawing.Point(12, 144);
            this.btnScientificVisual3DControl.Name = "btnScientificVisual3DControl";
            this.btnScientificVisual3DControl.Size = new System.Drawing.Size(260, 23);
            this.btnScientificVisual3DControl.TabIndex = 1;
            this.btnScientificVisual3DControl.Text = "ScientificVisual3DControl";
            this.btnScientificVisual3DControl.UseVisualStyleBackColor = true;
            this.btnScientificVisual3DControl.Click += new System.EventHandler(this.btnScientificVisual3DControl_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnScientificVisual3DControl);
            this.Controls.Add(this.btnScientificControl);
            this.Controls.Add(this.btnMySceneControl);
            this.Controls.Add(this.btnSceneControl);
            this.Controls.Add(this.btnOpenGLControl);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenGLControl;
        private System.Windows.Forms.Button btnSceneControl;
        private System.Windows.Forms.Button btnMySceneControl;
        private System.Windows.Forms.Button btnScientificControl;
        private System.Windows.Forms.Button btnScientificVisual3DControl;
    }
}