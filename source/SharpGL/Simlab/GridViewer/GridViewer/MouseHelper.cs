using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridViewer
{
    class MouseHelper
    {
        

        public static void WaitIdle(){

           Application.OpenForms[0].Cursor = Cursors.WaitCursor;
           Application.Idle -= OnIdle;
           Application.Idle += OnIdle;
        }

        private static void OnIdle(object sender, EventArgs e)
        {
           Application.OpenForms[0].Cursor = null;
           Application.Idle -= OnIdle;
        }
    }
}
