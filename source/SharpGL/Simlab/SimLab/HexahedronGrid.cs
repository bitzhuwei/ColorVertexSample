using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{
    public class HexahedronGrid:SimLabGrid
    {

        /// <summary>
        /// 初始化点的vao
        /// </summary>
        /// <param name="gridCoords"></param>
        public void Init(GridCoordsBufferData gridCoords)
        {


        }



        public override void SetTextureCoods(BufferData visibles)
        {
               throw new NotImplementedException();
        }

        public override void SetVisibleBuffer(BufferData visibles)
        {
             throw new NotImplementedException();
        }


        public override void SetTexutre(System.Drawing.Bitmap bitmap)
        {
            throw new NotImplementedException();
        }
       
    }
}
