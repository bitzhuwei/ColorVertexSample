using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFormHexahedronGridderElement_Click(object sender, EventArgs e)
        {
            (new FormHexahedronGridderElement()).Show();
        }

        private void btnFormPointGrid_Click(object sender, EventArgs e)
        {
            (new FormPointGrid()).Show();
        }

        private void btnDynamicUnstructoreForm_Click(object sender, EventArgs e)
        {
            (new FormDynamicUnstructureGridSample()).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new FormDynamicUnstructureGridTetrahedronSample()).Show();

        }
    }
}
