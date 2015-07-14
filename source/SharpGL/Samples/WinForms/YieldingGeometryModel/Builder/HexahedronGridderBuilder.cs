using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel.Builder
{
    public static class HexahedronGridderBuilder
    {

        /// <summary>
        /// 依次获取网格内的所有六面体。
        /// Get hexahedrons of gridder from specified source successively.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Hexahedron> GetGridderCells(this HexahedronGridderSource source)
        {
            Random random = new Random();

            int NI = source.NX;
            int NJ = source.NY;
            int NK = source.NZ;

            int total = NI * NJ * NK;

            //Hexahedron[] cells = new Hexahedron[total];
            //Dictionary<int, int> gridCellMap = new Dictionary<int, int>();
            for (int index = 0; index < total; index++)
            {
                int i, j, k;
                source.InvertIJK(index, out i, out j, out k);
                //int indexOfArray = index;
                //gridCellMap[index] = indexOfArray;

                Hexahedron cell = new Hexahedron();
                cell.flt = source.PointFLT(i, j, k);
                cell.frt = source.PointFRT(i, j, k);
                cell.flb = source.PointFLB(i, j, k);
                cell.frb = source.PointFRB(i, j, k);
                cell.blt = source.PointBLT(i, j, k);
                cell.brt = source.PointBRT(i, j, k);
                cell.blb = source.PointBLB(i, j, k);
                cell.brb = source.PointBRB(i, j, k);
                //cell.gridIndex = index;

                // set random color for now
                //cell.color = new SharpGL.SceneGraph.GLColor((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                //float r = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //float g = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //float b = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //float a = (float)random.Next(0, int.MaxValue) / (float)int.MaxValue;
                //cell.color = new SharpGL.SceneGraph.GLColor(r, g, b, a);
                byte[] bytes = new byte[4];
                random.NextBytes(bytes);
                cell.color = new SharpGL.SceneGraph.GLColor(
                    (0.0f + bytes[0]) / byte.MaxValue,
                    (0.0f + bytes[1]) / byte.MaxValue,
                    (0.0f + bytes[2]) / byte.MaxValue,
                    (0.0f + bytes[3]) / byte.MaxValue);

                yield return cell;
                //cells[index] = cell;
            }
            //HexahedronGridder gridder = new HexahedronGridder();
            //gridder.Cells = cells;
            //gridder.IJKCellsMap = gridCellMap;
            //return gridder;

        }

    }
}
