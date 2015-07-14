using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// A small helper class to load manifest resource files.
    /// </summary>
    public static class ManifestResourceLoader
    {
        /// <summary>
        /// Loads the named manifest resource as a text string.
        /// </summary>
        /// <param name="textFileName">Name of the text file.</param>
        /// <returns>The contents of the manifest resource.</returns>
        public static string LoadTextFile(string textFileName)
        {
            StackTrace stack = new StackTrace();
            StackFrame frame = stack.GetFrame(1);
            MethodBase method = frame.GetMethod();
            Type type = method.ReflectedType;
            Assembly executingAssembly = type.Assembly; //Assembly.GetExecutingAssembly();
            string pathToDots = textFileName.Replace("\\", ".");
            string location = string.Format("{0}.{1}", executingAssembly.GetName().Name, pathToDots);

            using (var stream = executingAssembly.GetManifestResourceStream(location))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
