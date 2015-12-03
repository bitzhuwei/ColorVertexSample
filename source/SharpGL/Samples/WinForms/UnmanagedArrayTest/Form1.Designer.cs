namespace UnmanagedArrayTest
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
            this.btnInSubRoutine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInSubRoutine
            // 
            this.btnInSubRoutine.Location = new System.Drawing.Point(12, 12);
            this.btnInSubRoutine.Name = "btnInSubRoutine";
            this.btnInSubRoutine.Size = new System.Drawing.Size(482, 33);
            this.btnInSubRoutine.TabIndex = 0;
            this.btnInSubRoutine.Text = "在子函数中不断使用UnmanagedArray";
            this.btnInSubRoutine.UseVisualStyleBackColor = true;
            this.btnInSubRoutine.Click += new System.EventHandler(this.btnInSubRoutine_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 291);
            this.Controls.Add(this.btnInSubRoutine);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInSubRoutine;
    }
}

