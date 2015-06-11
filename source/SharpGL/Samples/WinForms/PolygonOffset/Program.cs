using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PolygonOffset
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MessageBox.Show("Use 'A' to switch polygon offset on and off.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SharpGLForm());
        }
    }
}
