namespace GridViewer.Dialogs
{
    partial class ColorBarRangeEditDialog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.barChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCaculate = new System.Windows.Forms.Button();
            this.tbLogBase = new System.Windows.Forms.TextBox();
            this.lblBase = new System.Windows.Forms.Label();
            this.cbxUseAuto = new System.Windows.Forms.CheckBox();
            this.cbxUseLog = new System.Windows.Forms.CheckBox();
            this.tbxStep = new System.Windows.Forms.TextBox();
            this.lblStep = new System.Windows.Forms.Label();
            this.nudMaximum = new System.Windows.Forms.NumericUpDown();
            this.lblMaximum = new System.Windows.Forms.Label();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.nudMinimum = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimum)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.62722F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.37278F));
            this.tableLayoutPanel1.Controls.Add(this.barChart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(676, 475);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // barChart
            // 
            chartArea3.Name = "ChartArea1";
            this.barChart.ChartAreas.Add(chartArea3);
            this.tableLayoutPanel1.SetColumnSpan(this.barChart, 2);
            this.barChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.barChart.Legends.Add(legend3);
            this.barChart.Location = new System.Drawing.Point(3, 3);
            this.barChart.Name = "barChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.barChart.Series.Add(series3);
            this.barChart.Size = new System.Drawing.Size(670, 319);
            this.barChart.TabIndex = 0;
            title3.Name = "Hello";
            title3.Text = "Value Distribution";
            this.barChart.Titles.Add(title3);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btnCaculate);
            this.panel1.Controls.Add(this.tbLogBase);
            this.panel1.Controls.Add(this.lblBase);
            this.panel1.Controls.Add(this.cbxUseAuto);
            this.panel1.Controls.Add(this.cbxUseLog);
            this.panel1.Controls.Add(this.tbxStep);
            this.panel1.Controls.Add(this.lblStep);
            this.panel1.Controls.Add(this.nudMaximum);
            this.panel1.Controls.Add(this.lblMaximum);
            this.panel1.Controls.Add(this.lblMinValue);
            this.panel1.Controls.Add(this.nudMinimum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 328);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 87);
            this.panel1.TabIndex = 1;
            // 
            // btnCaculate
            // 
            this.btnCaculate.Location = new System.Drawing.Point(250, 59);
            this.btnCaculate.Name = "btnCaculate";
            this.btnCaculate.Size = new System.Drawing.Size(114, 23);
            this.btnCaculate.TabIndex = 10;
            this.btnCaculate.Text = "Caculate";
            this.btnCaculate.UseVisualStyleBackColor = true;
            this.btnCaculate.Click += new System.EventHandler(this.CaculateStatClick);
            // 
            // tbLogBase
            // 
            this.tbLogBase.Enabled = false;
            this.tbLogBase.Location = new System.Drawing.Point(432, 5);
            this.tbLogBase.Name = "tbLogBase";
            this.tbLogBase.Size = new System.Drawing.Size(100, 21);
            this.tbLogBase.TabIndex = 9;
            this.tbLogBase.Text = "10";
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(370, 9);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(59, 12);
            this.lblBase.TabIndex = 8;
            this.lblBase.Text = "Log base:";
            // 
            // cbxUseAuto
            // 
            this.cbxUseAuto.AutoSize = true;
            this.cbxUseAuto.Location = new System.Drawing.Point(250, 36);
            this.cbxUseAuto.Name = "cbxUseAuto";
            this.cbxUseAuto.Size = new System.Drawing.Size(84, 16);
            this.cbxUseAuto.TabIndex = 7;
            this.cbxUseAuto.Text = "Auto Range";
            this.cbxUseAuto.UseVisualStyleBackColor = true;
            this.cbxUseAuto.CheckedChanged += new System.EventHandler(this.UseAutoCheckedChanged);
            // 
            // cbxUseLog
            // 
            this.cbxUseLog.AutoSize = true;
            this.cbxUseLog.Location = new System.Drawing.Point(250, 7);
            this.cbxUseLog.Name = "cbxUseLog";
            this.cbxUseLog.Size = new System.Drawing.Size(114, 16);
            this.cbxUseLog.TabIndex = 6;
            this.cbxUseLog.Text = "Use Logarithmic";
            this.cbxUseLog.UseVisualStyleBackColor = true;
            this.cbxUseLog.CheckedChanged += new System.EventHandler(this.CheckBoxUseLogCheckedChanged);
            // 
            // tbxStep
            // 
            this.tbxStep.Location = new System.Drawing.Point(57, 61);
            this.tbxStep.Name = "tbxStep";
            this.tbxStep.Size = new System.Drawing.Size(143, 21);
            this.tbxStep.TabIndex = 5;
            this.tbxStep.TextChanged += new System.EventHandler(this.OnStepValueTextChanged);
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(16, 64);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(35, 12);
            this.lblStep.TabIndex = 4;
            this.lblStep.Text = "Step:";
            // 
            // nudMaximum
            // 
            this.nudMaximum.Location = new System.Drawing.Point(57, 34);
            this.nudMaximum.Name = "nudMaximum";
            this.nudMaximum.Size = new System.Drawing.Size(145, 21);
            this.nudMaximum.TabIndex = 3;
            this.nudMaximum.ValueChanged += new System.EventHandler(this.OnMaximumValueChanged);
            // 
            // lblMaximum
            // 
            this.lblMaximum.AutoSize = true;
            this.lblMaximum.Location = new System.Drawing.Point(3, 38);
            this.lblMaximum.Name = "lblMaximum";
            this.lblMaximum.Size = new System.Drawing.Size(53, 12);
            this.lblMaximum.TabIndex = 2;
            this.lblMaximum.Text = "Maximum:";
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Location = new System.Drawing.Point(3, 9);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(53, 12);
            this.lblMinValue.TabIndex = 1;
            this.lblMinValue.Text = "Minimum:";
            // 
            // nudMinimum
            // 
            this.nudMinimum.Location = new System.Drawing.Point(57, 5);
            this.nudMinimum.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMinimum.Name = "nudMinimum";
            this.nudMinimum.Size = new System.Drawing.Size(145, 21);
            this.nudMinimum.TabIndex = 0;
            this.nudMinimum.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudMinimum.ValueChanged += new System.EventHandler(this.OnMinimumValueChanged);
            this.nudMinimum.Validating += new System.ComponentModel.CancelEventHandler(this.OnMinimumValidating);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(352, 421);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 51);
            this.panel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(91, 19);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ColorBarRangeEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 475);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ColorBarRangeEditDialog";
            this.Text = "Color Bar Property Range ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnWindowClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimum)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart barChart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.NumericUpDown nudMinimum;
        private System.Windows.Forms.NumericUpDown nudMaximum;
        private System.Windows.Forms.Label lblMaximum;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.CheckBox cbxUseAuto;
        private System.Windows.Forms.CheckBox cbxUseLog;
        private System.Windows.Forms.TextBox tbxStep;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.TextBox tbLogBase;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCaculate;
        private System.Windows.Forms.ErrorProvider errorProvider;

    }
}