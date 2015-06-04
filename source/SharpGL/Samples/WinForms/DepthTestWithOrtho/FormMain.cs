using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepthTestWithOrtho
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnOpenGLControl_Click(object sender, EventArgs e)
        {
            (new FormOpenGLControl()).Show();
        }

        private void btnSceneControl_Click(object sender, EventArgs e)
        {
            (new FormSceneControl()).Show();
        }

        private void btnMySceneControl_Click(object sender, EventArgs e)
        {
            (new FormMySceneControl()).Show();
        }

        private void btnScientificVisual3DControl_Click(object sender, EventArgs e)
        {
            (new FormScientificVisual3DControl()).Show();
        }
    }
}
