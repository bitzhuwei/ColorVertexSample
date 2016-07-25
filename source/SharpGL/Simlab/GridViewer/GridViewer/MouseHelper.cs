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

           Application.Idle += OnIdle;
           Application.UseWaitCursor = true;
           
        }

        private static void OnIdle(object sender, EventArgs e)
        {
           Application.UseWaitCursor = false;
           Application.Idle -= OnIdle;
        }
    }
}
