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
    public partial class FormPortal : Form
    {
        public FormPortal()
        {
            InitializeComponent();
        }

        private void btnWell_Click(object sender, EventArgs e)
        {
            (new FormWell()).Show();
        }

        private void btnFormHexahedronGridder_Click(object sender, EventArgs e)
        {
            (new FormHexahedronGridderElement()).Show();
        }

        private void btnFormPointSpriteStringElement_Click(object sender, EventArgs e)
        {
            (new FormPointSpriteStringElement()).Show();
        }

        private void btnFormPointSpriteGridderElement_Click(object sender, EventArgs e)
        {
            (new FormPointSpriteGridderElement()).Show();
        }

        private void btnFormUnStructuredGridderElement_Click(object sender, EventArgs e)
        {
            (new FormUnStructuredGridderElement()).Show();
        }

        private void btnFormScientificVisual3DControl_Click(object sender, EventArgs e)
        {
            (new FormScientificVisual3DControl()).Show();
        }

        private void btnFormdfmPointSpriteGridderElement_Click(object sender, EventArgs e)
        {
            var frmSettings = new FormSelectExpectedRadius();
            if(frmSettings.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                (new FormdfmPointSpriteGridderElement(frmSettings.MaxRadius)).Show();
            }
        }

        private void btnFormDynamicUnStructuredGridderElement_Click(object sender, EventArgs e)
        {
            (new FormDynamicUnStructuredGridderElement()).Show();
        }

        private void btnFormZippedHexadronGridderElement_Click(object sender, EventArgs e)
        {
            (new FormZippedHexahedronGridderElement()).Show();
        }

        private void btnFormTexture2dHexahedronGridderElement_Click(object sender, EventArgs e)
        {
            (new FormTexture2dHexahedronGridderElement()).Show();
        }

        private void btnFormOnlyScientificControl_Click(object sender, EventArgs e)
        {
            (new FormOnlyScientificControl()).Show();
        }

        private void btnOnlyOpenGLControl_Click(object sender, EventArgs e)
        {
            (new FormOnlyOpenGLControl()).Show();
        }

        private void btnFormTryToReleaseBufferInOpenGL_Click(object sender, EventArgs e)
        {
            (new FormTryToReleaseBufferInOpenGL()).Show();
        }

        private void btnFormTexture2dHexahedronGridderElement_DrawElements_Click(object sender, EventArgs e)
        {
            (new FormTexture2dHexahedronGridderElement_DrawElements()).Show();
        }
    }
}
