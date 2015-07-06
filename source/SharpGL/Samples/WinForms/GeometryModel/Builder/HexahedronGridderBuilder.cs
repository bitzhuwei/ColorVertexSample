using GeometryModel.GLPrimitive;
using GeometryModel.Gridder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryModel.Builder
{
    public class HexahedronGridderBuilder
    {
        /// <summary>
        /// according to the index to create
        /// </summary>
        /// <param name="source"></param>
        /// <param name="gridIndexs"></param>
        /// <returns></returns>
        public static HexahedronGridder BuildSliceActiveGridder(HexahedronGridderSource source, int[] gridIndexs)
        {
            int NI = source.NX;
            int NJ = source.NY;
            int NK = source.NZ;

            List<Hexahedron> cells = new List<Hexahedron>();
            Dictionary<int,int> gridCellMap = new Dictionary<int,int>();
            int i, j, k;
            for (int index = 0; index < gridIndexs.Length; index++)
            {
                source.InvertIJK(gridIndexs[index], out i, out j, out k);
                if (source.IsActiveBlock(i, j, k) && source.IsSliceBlock(i, j, k))
                {
                    int indexOfArray = cells.Count;
                    gridCellMap[gridIndexs[index]] = indexOfArray;

                    Hexahedron cell = new Hexahedron();
                    cell.flt = source.PointFLT(i, j, k);
                    cell.frt = source.PointFRT(i, j, k);
                    cell.flb = source.PointFLB(i, j, k);
                    cell.frb = source.PointFRB(i, j, k);
                    cell.blt = source.PointBLT(i, j, k);
                    cell.brt = source.PointBRT(i, j, k);
                    cell.blb = source.PointBLB(i, j, k);
                    cell.brb = source.PointBRB(i, j, k);
                    cells.Add(cell);
                    cell.gridIndex = gridIndexs[index];
                }
            }
            HexahedronGridder gridder = new HexahedronGridder();
            gridder.Cells = cells.ToArray();
            gridder.IJKCellsMap = gridCellMap;
            return gridder;

        }

        public static HexahedronGridder BuildGridder(HexahedronGridderSource source)
        {
            Random random = new Random();

            int NI = source.NX;
            int NJ = source.NY;
            int NK = source.NZ;

            int total = NI * NJ * NK;

            Hexahedron[] cells = new Hexahedron[total];
            Dictionary<int, int> gridCellMap = new Dictionary<int, int>();
            int i, j, k;
            for (int index = 0; index < total; index++)
            {
                source.InvertIJK(index, out i, out j, out k);
                int indexOfArray = index;
                gridCellMap[index] = indexOfArray;

                Hexahedron cell = new Hexahedron();
                cell.flt = source.PointFLT(i, j, k);
                cell.frt = source.PointFRT(i, j, k);
                cell.flb = source.PointFLB(i, j, k);
                cell.frb = source.PointFRB(i, j, k);
                cell.blt = source.PointBLT(i, j, k);
                cell.brt = source.PointBRT(i, j, k);
                cell.blb = source.PointBLB(i, j, k);
                cell.brb = source.PointBRB(i, j, k);
                cell.gridIndex = index;

                // set random color for now
                cell.color = new SharpGL.SceneGraph.GLColor((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

                cells[index] = cell;
            }
            HexahedronGridder gridder = new HexahedronGridder();
            gridder.Cells = cells;
            gridder.IJKCellsMap = gridCellMap;
            return gridder;


        }




    }
}
