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
            this.components = new System.ComponentModel.Container();
            this.btnUnsafeVSSafe = new System.Windows.Forms.Button();
            this.btnInSubRoutine = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnUnsafeVSSafe
            // 
            this.btnUnsafeVSSafe.Location = new System.Drawing.Point(12, 12);
            this.btnUnsafeVSSafe.Name = "btnUnsafeVSSafe";
            this.btnUnsafeVSSafe.Size = new System.Drawing.Size(482, 33);
            this.btnUnsafeVSSafe.TabIndex = 0;
            this.btnUnsafeVSSafe.Text = "对比unsafe和safe方式使用UnmanagedArray<T>的效率";
            this.btnUnsafeVSSafe.UseVisualStyleBackColor = true;
            this.btnUnsafeVSSafe.Click += new System.EventHandler(this.btnUnsafeVSSafe_Click);
            // 
            // btnInSubRoutine
            // 
            this.btnInSubRoutine.Location = new System.Drawing.Point(12, 51);
            this.btnInSubRoutine.Name = "btnInSubRoutine";
            this.btnInSubRoutine.Size = new System.Drawing.Size(482, 33);
            this.btnInSubRoutine.TabIndex = 0;
            this.btnInSubRoutine.Text = "在子函数中不断为UnmanagedArray赋值";
            this.btnInSubRoutine.UseVisualStyleBackColor = true;
            this.btnInSubRoutine.Click += new System.EventHandler(this.btnInSubRoutine_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 291);
            this.Controls.Add(this.btnInSubRoutine);
            this.Controls.Add(this.btnUnsafeVSSafe);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUnsafeVSSafe;
        private System.Windows.Forms.Button btnInSubRoutine;
        private System.Windows.Forms.Timer timer1;
    }
}

