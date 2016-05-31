using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridViewer.Dialogs
{
    public partial class ZAxisScaleEditorDialog : Form
    {
        public ZAxisScaleEditorDialog(float value)
        {
            InitializeComponent();

            this.txtZAxisScale.Text = string.Format("{0}", value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            float value = 0;
            if (!float.TryParse(this.txtZAxisScale.Text, out value))
            {
                MessageBox.Show("Invalid number!");
                return;
            }

            this.ZAxisScale = value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public float ZAxisScale { get; set; }
    }
}
