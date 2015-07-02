using GeometryModel.GLPrimitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryModel.Gridder
{

    /// <summary>
    /// HexaHedron Gridder 
    /// </summary>
    public class HexahedronGridder
    {
      
        /// <summary>
        /// gridder cells
        /// </summary>
        private Hexahedron[] cells;


        /// <summary>
        /// key is the I,J,K index ,Value is the index of cells
        /// </summary>
        private Dictionary<int, int> ijkCellsMap;

        /// <summary>
        /// gridder cells
        /// </summary>
        public Hexahedron[] Cells
        {
            get
            {
                return this.cells;
            }
            set
            {
                this.cells = value;
            }
        }

        public Dictionary<int, int> IJKCellsMap
        {
            get
            {
                return this.ijkCellsMap;
            }
            set
            {
                this.ijkCellsMap = value;
            }
        }



        public Hexahedron IndexOf(int griderIndex)
        {
            int cellIndex;
            cellIndex = this.ijkCellsMap[griderIndex];
            return cells[cellIndex];
        }

   

    }
}
