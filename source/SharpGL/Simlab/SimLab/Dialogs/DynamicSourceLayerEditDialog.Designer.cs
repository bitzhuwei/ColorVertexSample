namespace SimLab.Dialogs
{
    partial class DynamicSourceLayerEditDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLayers = new System.Windows.Forms.Label();
            this.cbLayersList = new System.Windows.Forms.ComboBox();
            this.lblMatrixCount = new System.Windows.Forms.Label();
            this.cblVisibleLayers = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.54545F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.45454F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cblVisibleLayers, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.79461F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.20539F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(567, 383);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.lblLayers);
            this.flowLayoutPanel1.Controls.Add(this.cbLayersList);
            this.flowLayoutPanel1.Controls.Add(this.lblMatrixCount);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 36);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblLayers
            // 
            this.lblLayers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLayers.AutoSize = true;
            this.lblLayers.Location = new System.Drawing.Point(3, 17);
            this.lblLayers.Margin = new System.Windows.Forms.Padding(3);
            this.lblLayers.Name = "lblLayers";
            this.lblLayers.Size = new System.Drawing.Size(95, 12);
            this.lblLayers.TabIndex = 0;
            this.lblLayers.Text = "Select Layers :";
            this.lblLayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbLayersList
            // 
            this.cbLayersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayersList.FormattingEnabled = true;
            this.cbLayersList.Location = new System.Drawing.Point(104, 13);
            this.cbLayersList.MinimumSize = new System.Drawing.Size(160, 0);
            this.cbLayersList.Name = "cbLayersList";
            this.cbLayersList.Size = new System.Drawing.Size(160, 20);
            this.cbLayersList.TabIndex = 1;
            this.cbLayersList.SelectedValueChanged += new System.EventHandler(this.LayersListSelectedValueChanged);
            // 
            // lblMatrixCount
            // 
            this.lblMatrixCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMatrixCount.AutoSize = true;
            this.lblMatrixCount.Location = new System.Drawing.Point(270, 17);
            this.lblMatrixCount.Name = "lblMatrixCount";
            this.lblMatrixCount.Size = new System.Drawing.Size(41, 12);
            this.lblMatrixCount.TabIndex = 2;
            this.lblMatrixCount.Text = "label1";
            // 
            // cblVisibleLayers
            // 
            this.cblVisibleLayers.CheckOnClick = true;
            this.cblVisibleLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cblVisibleLayers.FormattingEnabled = true;
            this.cblVisibleLayers.Location = new System.Drawing.Point(90, 45);
            this.cblVisibleLayers.Name = "cblVisibleLayers";
            this.cblVisibleLayers.Size = new System.Drawing.Size(356, 285);
            this.cblVisibleLayers.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.btnCancel);
            this.flowLayoutPanel2.Controls.Add(this.btnOk);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(90, 336);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(474, 44);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(396, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(315, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // DynamicSourceLayerEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 383);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DynamicSourceLayerEditDialog";
            this.Text = "DynamicSourceLayerEditDialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblLayers;
        private System.Windows.Forms.ComboBox cbLayersList;
        private System.Windows.Forms.CheckedListBox cblVisibleLayers;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblMatrixCount;
    }
}