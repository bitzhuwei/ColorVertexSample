using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeometryModel;
using GeometryModel.Builder;
using GeometryModel.Gridder;
using GeometryModel.GLPrimitive;
using SharpGL.SceneGraph;
using System.Runtime.InteropServices;

namespace TestGeomertyModel
{
    [TestClass]
    public class TestGeomeryModel
    {
        private static String FormatVertex(Vertex v){
            return String.Format("({0},{1},{2})",v.X,v.Y,v.Z);
        }

        [TestMethod]
        public void TestCatesianModel()
        {

            CatesianGridderSource catesianSource = new CatesianGridderSource();

            catesianSource.NX = 6;
            catesianSource.NY = 22;
            catesianSource.NZ = 85;
            catesianSource.DX = 500;
            catesianSource.DY = 500;
            catesianSource.DZ = 20;

            HexahedronGridder hexaGridder = HexahedronGridderBuilder.BuildGridder(catesianSource);
            int arrayIndex = 0;
            int i,j,k;
            foreach(Hexahedron cell in hexaGridder.Cells)
            {
               arrayIndex++;
               catesianSource.InvertIJK(cell.gridIndex,out i,out j, out k);
               System.Console.WriteLine("[{0},{1},{2}]", i, j, k);
               System.Console.WriteLine(FormatVertex(cell.flt));
               System.Console.WriteLine(FormatVertex(cell.frt));
               System.Console.WriteLine(FormatVertex(cell.flb));
               System.Console.WriteLine(FormatVertex(cell.frt));
               System.Console.WriteLine(FormatVertex(cell.blt));
               System.Console.WriteLine(FormatVertex(cell.brt));
               System.Console.WriteLine(FormatVertex(cell.blb));
               System.Console.WriteLine(FormatVertex(cell.brb));
            }
        }

        [TestMethod]
        public void TestStructSize()
        {
            
            int size = Marshal.SizeOf(typeof(Hexahedron));
            System.Console.WriteLine("sizeof(Hexahedron):"+size);
            System.Console.WriteLine("sizeof(Vertex):" + Marshal.SizeOf(typeof(Vertex)));
            System.Console.WriteLine("sizeof(GLColor):"+Marshal.SizeOf(typeof(GLColor)));
        }
    }
}
