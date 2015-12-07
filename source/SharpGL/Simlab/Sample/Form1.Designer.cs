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
            this.btnDynamicUnstructoreForm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFormHexahedronGridderElement
            // 
            this.btnFormHexahedronGridderElement.Location = new System.Drawing.Point(9, 10);
            this.btnFormHexahedronGridderElement.Margin = new System.Windows.Forms.Padding(2);
            this.btnFormHexahedronGridderElement.Name = "btnFormHexahedronGridderElement";
            this.btnFormHexahedronGridderElement.Size = new System.Drawing.Size(591, 41);
            this.btnFormHexahedronGridderElement.TabIndex = 0;
            this.btnFormHexahedronGridderElement.Text = "FormHexahedronGridderElement";
            this.btnFormHexahedronGridderElement.UseVisualStyleBackColor = true;
            this.btnFormHexahedronGridderElement.Click += new System.EventHandler(this.btnFormHexahedronGridderElement_Click);
            // 
            // btnFormPointGrid
            // 
            this.btnFormPointGrid.Location = new System.Drawing.Point(9, 55);
            this.btnFormPointGrid.Margin = new System.Windows.Forms.Padding(2);
            this.btnFormPointGrid.Name = "btnFormPointGrid";
            this.btnFormPointGrid.Size = new System.Drawing.Size(591, 41);
            this.btnFormPointGrid.TabIndex = 0;
            this.btnFormPointGrid.Text = "FormPointGrid";
            this.btnFormPointGrid.UseVisualStyleBackColor = true;
            this.btnFormPointGrid.Click += new System.EventHandler(this.btnFormPointGrid_Click);
            // 
            // btnDynamicUnstructoreForm
            // 
            this.btnDynamicUnstructoreForm.Location = new System.Drawing.Point(11, 100);
            this.btnDynamicUnstructoreForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnDynamicUnstructoreForm.Name = "btnDynamicUnstructoreForm";
            this.btnDynamicUnstructoreForm.Size = new System.Drawing.Size(591, 41);
            this.btnDynamicUnstructoreForm.TabIndex = 1;
            this.btnDynamicUnstructoreForm.Text = "FormDynamicUnstructureGridSample";
            this.btnDynamicUnstructoreForm.UseVisualStyleBackColor = true;
            this.btnDynamicUnstructoreForm.Click += new System.EventHandler(this.btnDynamicUnstructoreForm_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 145);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(591, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "FormDynamicUnstructureGridTetrahedronSample";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 412);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDynamicUnstructoreForm);
            this.Controls.Add(this.btnFormPointGrid);
            this.Controls.Add(this.btnFormHexahedronGridderElement);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFormHexahedronGridderElement;
        private System.Windows.Forms.Button btnFormPointGrid;
        private System.Windows.Forms.Button btnDynamicUnstructoreForm;
        private System.Windows.Forms.Button button1;
    }
}

