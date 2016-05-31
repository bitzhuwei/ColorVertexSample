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

           Application.UseWaitCursor = true;
           Application.Idle -= OnIdle;
           Application.Idle += OnIdle;
        }

        private static void OnIdle(object sender, EventArgs e)
        {
           Application.UseWaitCursor = false;
           Application.Idle -= OnIdle;
        }
    }
}
