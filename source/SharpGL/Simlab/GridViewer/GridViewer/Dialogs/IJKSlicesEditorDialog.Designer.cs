namespace GridViewer.Dialogs
{
    partial class IJKSlicesEditorDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbIAll = new System.Windows.Forms.CheckBox();
            this.nudEveryI = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.clbIList = new System.Windows.Forms.CheckedListBox();
            this.lblInfoI = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbJAll = new System.Windows.Forms.CheckBox();
            this.nudEveryJ = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.clbJList = new System.Windows.Forms.CheckedListBox();
            this.lblInfoJ = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbKAll = new System.Windows.Forms.CheckBox();
            this.nudEveryK = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.clbKList = new System.Windows.Forms.CheckedListBox();
            this.lblInfoK = new System.Windows.Forms.Label();
            this.pnlCommonButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryI)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryJ)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryK)).BeginInit();
            this.pnlCommonButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlCommonButtons, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 525);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 3);
            this.panel1.Size = new System.Drawing.Size(314, 467);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 467);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "I Direction";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.clbIList, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblInfoI, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.962529F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.03747F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(308, 447);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbIAll);
            this.panel4.Controls.Add(this.nudEveryI);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(302, 27);
            this.panel4.TabIndex = 0;
            // 
            // cbIAll
            // 
            this.cbIAll.AutoSize = true;
            this.cbIAll.Location = new System.Drawing.Point(169, 6);
            this.cbIAll.Name = "cbIAll";
            this.cbIAll.Size = new System.Drawing.Size(42, 16);
            this.cbIAll.TabIndex = 3;
            this.cbIAll.Text = "All";
            this.cbIAll.UseVisualStyleBackColor = true;
            this.cbIAll.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
            // 
            // nudEveryI
            // 
            this.nudEveryI.Location = new System.Drawing.Point(87, 4);
            this.nudEveryI.Name = "nudEveryI";
            this.nudEveryI.Size = new System.Drawing.Size(76, 21);
            this.nudEveryI.TabIndex = 1;
            this.nudEveryI.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEveryI.ValueChanged += new System.EventHandler(this.OnEveryValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Every Select";
            // 
            // clbIList
            // 
            this.clbIList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbIList.FormattingEnabled = true;
            this.clbIList.Location = new System.Drawing.Point(3, 36);
            this.clbIList.Name = "clbIList";
            this.clbIList.Size = new System.Drawing.Size(302, 381);
            this.clbIList.TabIndex = 1;
            // 
            // lblInfoI
            // 
            this.lblInfoI.AutoSize = true;
            this.lblInfoI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfoI.Location = new System.Drawing.Point(3, 420);
            this.lblInfoI.Name = "lblInfoI";
            this.lblInfoI.Size = new System.Drawing.Size(302, 27);
            this.lblInfoI.TabIndex = 2;
            this.lblInfoI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(323, 3);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 3);
            this.panel2.Size = new System.Drawing.Size(314, 467);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 467);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "J Direction";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.clbJList, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblInfoJ, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.962529F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.03747F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(308, 447);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbJAll);
            this.panel5.Controls.Add(this.nudEveryJ);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(302, 27);
            this.panel5.TabIndex = 0;
            // 
            // cbJAll
            // 
            this.cbJAll.AutoSize = true;
            this.cbJAll.Location = new System.Drawing.Point(166, 6);
            this.cbJAll.Name = "cbJAll";
            this.cbJAll.Size = new System.Drawing.Size(42, 16);
            this.cbJAll.TabIndex = 4;
            this.cbJAll.Text = "All";
            this.cbJAll.UseVisualStyleBackColor = true;
            this.cbJAll.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
            // 
            // nudEveryJ
            // 
            this.nudEveryJ.Location = new System.Drawing.Point(87, 4);
            this.nudEveryJ.Name = "nudEveryJ";
            this.nudEveryJ.Size = new System.Drawing.Size(73, 21);
            this.nudEveryJ.TabIndex = 1;
            this.nudEveryJ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEveryJ.ValueChanged += new System.EventHandler(this.OnEveryValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Every Select";
            // 
            // clbJList
            // 
            this.clbJList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbJList.FormattingEnabled = true;
            this.clbJList.Location = new System.Drawing.Point(3, 36);
            this.clbJList.Name = "clbJList";
            this.clbJList.Size = new System.Drawing.Size(302, 381);
            this.clbJList.TabIndex = 1;
            // 
            // lblInfoJ
            // 
            this.lblInfoJ.AutoSize = true;
            this.lblInfoJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfoJ.Location = new System.Drawing.Point(3, 420);
            this.lblInfoJ.Name = "lblInfoJ";
            this.lblInfoJ.Size = new System.Drawing.Size(302, 27);
            this.lblInfoJ.TabIndex = 2;
            this.lblInfoJ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(643, 3);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 3);
            this.panel3.Size = new System.Drawing.Size(314, 467);
            this.panel3.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 467);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "K Direction";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.clbKList, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblInfoK, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.962529F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.03747F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(308, 447);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cbKAll);
            this.panel6.Controls.Add(this.nudEveryK);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(302, 27);
            this.panel6.TabIndex = 0;
            // 
            // cbKAll
            // 
            this.cbKAll.AutoSize = true;
            this.cbKAll.Location = new System.Drawing.Point(182, 6);
            this.cbKAll.Name = "cbKAll";
            this.cbKAll.Size = new System.Drawing.Size(42, 16);
            this.cbKAll.TabIndex = 4;
            this.cbKAll.Text = "All";
            this.cbKAll.UseVisualStyleBackColor = true;
            this.cbKAll.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
            // 
            // nudEveryK
            // 
            this.nudEveryK.Location = new System.Drawing.Point(87, 4);
            this.nudEveryK.Name = "nudEveryK";
            this.nudEveryK.Size = new System.Drawing.Size(89, 21);
            this.nudEveryK.TabIndex = 1;
            this.nudEveryK.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEveryK.ValueChanged += new System.EventHandler(this.OnEveryValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Every Select";
            // 
            // clbKList
            // 
            this.clbKList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbKList.FormattingEnabled = true;
            this.clbKList.Location = new System.Drawing.Point(3, 36);
            this.clbKList.Name = "clbKList";
            this.clbKList.Size = new System.Drawing.Size(302, 381);
            this.clbKList.TabIndex = 1;
            // 
            // lblInfoK
            // 
            this.lblInfoK.AutoSize = true;
            this.lblInfoK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfoK.Location = new System.Drawing.Point(3, 420);
            this.lblInfoK.Name = "lblInfoK";
            this.lblInfoK.Size = new System.Drawing.Size(302, 27);
            this.lblInfoK.TabIndex = 2;
            this.lblInfoK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCommonButtons
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlCommonButtons, 2);
            this.pnlCommonButtons.Controls.Add(this.btnCancel);
            this.pnlCommonButtons.Controls.Add(this.btnApply);
            this.pnlCommonButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCommonButtons.Location = new System.Drawing.Point(323, 476);
            this.pnlCommonButtons.Name = "pnlCommonButtons";
            this.pnlCommonButtons.Size = new System.Drawing.Size(634, 46);
            this.pnlCommonButtons.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(529, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CloseClick);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(413, 12);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(97, 25);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ApplyClick);
            // 
            // IJKSlicesEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 525);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "IJKSlicesEditorDialog";
            this.Text = "IJK Slices";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryI)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryJ)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryK)).EndInit();
            this.pnlCommonButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlCommonButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown nudEveryI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbIList;
        private System.Windows.Forms.Label lblInfoI;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.NumericUpDown nudEveryJ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clbJList;
        private System.Windows.Forms.Label lblInfoJ;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown nudEveryK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox clbKList;
        private System.Windows.Forms.Label lblInfoK;
        private System.Windows.Forms.CheckBox cbIAll;
        private System.Windows.Forms.CheckBox cbJAll;
        private System.Windows.Forms.CheckBox cbKAll;
    }
}