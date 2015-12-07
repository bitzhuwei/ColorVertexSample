namespace Sample
{
    partial class Form1
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
            this.btnFormHexahedronGridderElement = new System.Windows.Forms.Button();
            this.btnFormPointGrid = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFormHexahedronGridderElement
            // 
            this.btnFormHexahedronGridderElement.Location = new System.Drawing.Point(12, 12);
            this.btnFormHexahedronGridderElement.Name = "btnFormHexahedronGridderElement";
            this.btnFormHexahedronGridderElement.Size = new System.Drawing.Size(788, 51);
            this.btnFormHexahedronGridderElement.TabIndex = 0;
            this.btnFormHexahedronGridderElement.Text = "FormHexahedronGridderElement";
            this.btnFormHexahedronGridderElement.UseVisualStyleBackColor = true;
            this.btnFormHexahedronGridderElement.Click += new System.EventHandler(this.btnFormHexahedronGridderElement_Click);
            // 
            // btnFormPointGrid
            // 
            this.btnFormPointGrid.Location = new System.Drawing.Point(12, 69);
            this.btnFormPointGrid.Name = "btnFormPointGrid";
            this.btnFormPointGrid.Size = new System.Drawing.Size(788, 51);
            this.btnFormPointGrid.TabIndex = 0;
            this.btnFormPointGrid.Text = "FormPointGrid";
            this.btnFormPointGrid.UseVisualStyleBackColor = true;
            this.btnFormPointGrid.Click += new System.EventHandler(this.btnFormPointGrid_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 515);
            this.Controls.Add(this.btnFormPointGrid);
            this.Controls.Add(this.btnFormHexahedronGridderElement);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFormHexahedronGridderElement;
        private System.Windows.Forms.Button btnFormPointGrid;
    }
}

