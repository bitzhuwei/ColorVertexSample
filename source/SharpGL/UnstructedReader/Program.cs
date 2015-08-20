using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.DataSource;
using YieldingGeometryModel.loader;

namespace UnstructedReader
{
    class Program
    {
        static void Main(string[] args)
        {
            String dataRoot = @"C:\simprojs\test2\Case15";
            String fractionTagsName = "ListCVFracture.txt";
            String borderTagsName = "ListCVBorder.txt";
            String modelGrid = "Model.grid";

            string gridPathFileName = Path.Combine(dataRoot, modelGrid);

            string fractionTagPathFileName = Path.Combine(dataRoot, fractionTagsName);

            string borderTagPathFileName = Path.Combine(dataRoot, borderTagsName);

            UnstructureGeometryLoader loader = new UnstructureGeometryLoader();

            DateTime start = DateTime.Now;
            UnStructuredGridderSource source = loader.LoadSource(gridPathFileName, fractionTagPathFileName, borderTagPathFileName);
            DateTime stop = DateTime.Now;
            double seconds = (stop.Ticks - start.Ticks)/1000.0d;

            System.Console.WriteLine("loan cost seconds:{0}", seconds);
            System.Console.WriteLine("Triangles:{0}", source.Triangles.Length);
            System.Console.WriteLine("Tetras:{0}", source.Tetras.Length);
            System.Console.WriteLine("Fractions:{0}", source.Fractions.Length);



        }
    }
}
