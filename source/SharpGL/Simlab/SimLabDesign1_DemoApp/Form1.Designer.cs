namespace SimLabDesign1_DemoApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnHexahedronElement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHexahedronElement
            // 
            this.btnHexahedronElement.Location = new System.Drawing.Point(12, 12);
            this.btnHexahedronElement.Name = "btnHexahedronElement";
            this.btnHexahedronElement.Size = new System.Drawing.Size(536, 65);
            this.btnHexahedronElement.TabIndex = 0;
            this.btnHexahedronElement.Text = "HexahedronElement";
            this.btnHexahedronElement.UseVisualStyleBackColor = true;
            this.btnHexahedronElement.Click += new System.EventHandler(this.btnHexahedronElement_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 354);
            this.Controls.Add(this.btnHexahedronElement);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHexahedronElement;
    }
}

