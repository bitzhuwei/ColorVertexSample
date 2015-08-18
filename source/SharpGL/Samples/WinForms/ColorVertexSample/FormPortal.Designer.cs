namespace ColorVertexSample
{
    partial class FormPortal
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
            this.btnWell = new System.Windows.Forms.Button();
            this.btnFormHexahedronGridder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWell
            // 
            this.btnWell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWell.Location = new System.Drawing.Point(12, 12);
            this.btnWell.Name = "btnWell";
            this.btnWell.Size = new System.Drawing.Size(930, 23);
            this.btnWell.TabIndex = 0;
            this.btnWell.Text = "Well";
            this.btnWell.UseVisualStyleBackColor = true;
            this.btnWell.Click += new System.EventHandler(this.btnWell_Click);
            // 
            // btnFormHexahedronGridder
            // 
            this.btnFormHexahedronGridder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormHexahedronGridder.Location = new System.Drawing.Point(12, 41);
            this.btnFormHexahedronGridder.Name = "btnFormHexahedronGridder";
            this.btnFormHexahedronGridder.Size = new System.Drawing.Size(930, 23);
            this.btnFormHexahedronGridder.TabIndex = 0;
            this.btnFormHexahedronGridder.Text = "FormHexahedronGridder";
            this.btnFormHexahedronGridder.UseVisualStyleBackColor = true;
            this.btnFormHexahedronGridder.Click += new System.EventHandler(this.btnFormHexahedronGridder_Click);
            // 
            // FormPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 432);
            this.Controls.Add(this.btnFormHexahedronGridder);
            this.Controls.Add(this.btnWell);
            this.Name = "FormPortal";
            this.Text = "FormPortal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWell;
        private System.Windows.Forms.Button btnFormHexahedronGridder;
    }
}