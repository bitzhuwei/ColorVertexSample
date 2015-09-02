using System;
using System.Windows.Forms;

namespace ModernOpenGLSample
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
            //Application.Run(new ModernOpenGLSample.Form1());
            Application.Run(new ModernOpenGLSample._3MySceneControl.FormModernSceneControlDemo());
            Application.Run(new ModernOpenGLSample._2SceneControl.FormModernSceneControlDemo());
            Application.Run(new ModernOpenGLSample._1OpenGLControl.FormModernOpenGLControlDemo());
            Application.Run(new FormModernOpenGLSample());
        }
    }
}
