using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorVertexSample
{
    public partial class FormSelectExpectedRadius : Form
    {

        public float MaxRadius { get; set; }

        public FormSelectExpectedRadius()
        {
            InitializeComponent();

            this.MaxRadius = (float)this.numericUpDown1.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.MaxRadius = (float)this.numericUpDown1.Value;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
