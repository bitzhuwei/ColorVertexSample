using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimLab.Dialogs
{
    public partial class ZAxisDistortionEditorDialog : Form
    {
        public ZAxisDistortionEditorDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public float Distortion
        {
            get
            {
                return (float)nudDistortion.Value;
            }
            set
            {
                this.nudDistortion.Value = (decimal)value;
            }
        }
    }
}
