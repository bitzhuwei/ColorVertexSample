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
            this.btnFormPointSpriteStringElement = new System.Windows.Forms.Button();
            this.btnFormPointSpriteGridderElement = new System.Windows.Forms.Button();
            this.btnFormUnStructuredGridderElement = new System.Windows.Forms.Button();
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
            this.btnWell.Text = "FormWell";
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
            this.btnFormHexahedronGridder.Text = "FormHexahedronGridderElement";
            this.btnFormHexahedronGridder.UseVisualStyleBackColor = true;
            this.btnFormHexahedronGridder.Click += new System.EventHandler(this.btnFormHexahedronGridder_Click);
            // 
            // btnFormPointSpriteStringElement
            // 
            this.btnFormPointSpriteStringElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormPointSpriteStringElement.Location = new System.Drawing.Point(12, 99);
            this.btnFormPointSpriteStringElement.Name = "btnFormPointSpriteStringElement";
            this.btnFormPointSpriteStringElement.Size = new System.Drawing.Size(930, 23);
            this.btnFormPointSpriteStringElement.TabIndex = 0;
            this.btnFormPointSpriteStringElement.Text = "FormPointSpriteStringElement";
            this.btnFormPointSpriteStringElement.UseVisualStyleBackColor = true;
            this.btnFormPointSpriteStringElement.Click += new System.EventHandler(this.btnFormPointSpriteStringElement_Click);
            // 
            // btnFormPointSpriteGridderElement
            // 
            this.btnFormPointSpriteGridderElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormPointSpriteGridderElement.Location = new System.Drawing.Point(12, 70);
            this.btnFormPointSpriteGridderElement.Name = "btnFormPointSpriteGridderElement";
            this.btnFormPointSpriteGridderElement.Size = new System.Drawing.Size(930, 23);
            this.btnFormPointSpriteGridderElement.TabIndex = 0;
            this.btnFormPointSpriteGridderElement.Text = "FormPointSpriteGridderElement";
            this.btnFormPointSpriteGridderElement.UseVisualStyleBackColor = true;
            this.btnFormPointSpriteGridderElement.Click += new System.EventHandler(this.btnFormPointSpriteGridderElement_Click);
            // 
            // btnFormUnStructuredGridderElement
            // 
            this.btnFormUnStructuredGridderElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormUnStructuredGridderElement.Location = new System.Drawing.Point(12, 128);
            this.btnFormUnStructuredGridderElement.Name = "btnFormUnStructuredGridderElement";
            this.btnFormUnStructuredGridderElement.Size = new System.Drawing.Size(930, 23);
            this.btnFormUnStructuredGridderElement.TabIndex = 0;
            this.btnFormUnStructuredGridderElement.Text = "FormUnStructuredGridderElement";
            this.btnFormUnStructuredGridderElement.UseVisualStyleBackColor = true;
            this.btnFormUnStructuredGridderElement.Click += new System.EventHandler(this.btnFormUnStructuredGridderElement_Click);
            // 
            // FormPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 432);
            this.Controls.Add(this.btnFormUnStructuredGridderElement);
            this.Controls.Add(this.btnFormPointSpriteStringElement);
            this.Controls.Add(this.btnFormPointSpriteGridderElement);
            this.Controls.Add(this.btnFormHexahedronGridder);
            this.Controls.Add(this.btnWell);
            this.Name = "FormPortal";
            this.Text = "FormPortal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWell;
        private System.Windows.Forms.Button btnFormHexahedronGridder;
        private System.Windows.Forms.Button btnFormPointSpriteStringElement;
        private System.Windows.Forms.Button btnFormPointSpriteGridderElement;
        private System.Windows.Forms.Button btnFormUnStructuredGridderElement;
    }
}