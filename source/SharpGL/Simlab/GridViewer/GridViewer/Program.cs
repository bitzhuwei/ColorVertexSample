using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow mainWindow = new MainWindow();
            Rectangle rect = WindowSizeHelper.WindowSize(0.8f,0.9f);
            mainWindow.Size = rect.Size;
            mainWindow.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(mainWindow);
        }
    }
}
