using SimLab.SimGrid;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSource.Factory
{
    public class HexahedronGridFactory:GridBufferDataFactory
    {

        public override MeshGeometry3D CreateMesh(GridderSource source)
        {
             HexahedronGridderSource src = (HexahedronGridderSource)source;
             PositionsBufferData positions = new HexahedronPositionBufferData();
             TriangleIndicesBufferData triangles = new TriangleIndicesBufferData();
             int dimSize = src.DimenSize;
             int I, J, K;
             unsafe
             {
                 int gridMemSize = dimSize * sizeof(Hexahedron);
                 positions.AllocMem(gridMemSize);
                 Hexahedron* cell = (Hexahedron*)positions.Data;
                 for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                 {
                     src.InvertIJK(gridIndex, out I, out J, out K);
                     cell->FLT = src.PointFLT(I, J, K);
                     cell->FRT = src.PointFRT(I, J, K);
                     cell->BRT = src.PointBRT(I, J, K);
                     cell->BLT = src.PointBLT(I, J, K);
                     cell->FLB = src.PointFLB(I, J, K);
                     cell->FRB = src.PointFRB(I, J, K);
                     cell->BRB = src.PointBRB(I, J, K);
                     cell->BLB = src.PointBLB(I, J, K);
                     cell++;
                 }

                 //网格个数*每个六面体的面数*描述每个六面体的三角形个数
                 int triangleCount = dimSize * 6 * 2;
                 int triangleSize = triangleCount * sizeof(Triangle);
                 triangles.AllocMem(triangleSize);

                 int celloffset = 0; //每个网格数描述的点
                 Triangle* first = (Triangle*)triangles.Data;
                 for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                 {
                     //网格三角形的偏移量
                     int gto= gridIndex * (6 * 2);

                     //任意网格三角形的首指针
                     Triangle* gridTriangle = first + gto;
                     celloffset = gridIndex * 8;

                     //top
                     gridTriangle[0].dot0 = celloffset + 0;
                     gridTriangle[0].dot1 = celloffset + 1;
                     gridTriangle[0].dot2 = celloffset + 2;

                     gridTriangle[1].dot0 = celloffset + 0;
                     gridTriangle[1].dot1 = celloffset + 2;
                     gridTriangle[1].dot2 = celloffset + 3;

                     //bottom
                     gridTriangle[2].dot0 = celloffset + 4;
                     gridTriangle[2].dot1 = celloffset + 5;
                     gridTriangle[2].dot2 = celloffset + 7;

                     gridTriangle[3].dot0 = celloffset + 7;
                     gridTriangle[3].dot1 = celloffset + 6;
                     gridTriangle[3].dot2 = celloffset + 5;

                     //left
                     gridTriangle[4].dot0 = celloffset + 0;
                     gridTriangle[4].dot1 = celloffset + 3;
                     gridTriangle[4].dot2 = celloffset + 4;

                     gridTriangle[5].dot0 = celloffset + 4;
                     gridTriangle[5].dot1 = celloffset + 7;
                     gridTriangle[5].dot2 = celloffset + 3;

                     //right
                     gridTriangle[6].dot0 = celloffset + 1;
                     gridTriangle[6].dot1 = celloffset + 2;
                     gridTriangle[6].dot2 = celloffset + 5;


                     gridTriangle[7].dot0 = celloffset + 5;
                     gridTriangle[7].dot1 = celloffset + 6;
                     gridTriangle[7].dot2 = celloffset + 2;

                     //front
                     gridTriangle[8].dot0 = celloffset + 0;
                     gridTriangle[8].dot1 = celloffset + 4;
                     gridTriangle[8].dot2 = celloffset + 5;

                     gridTriangle[9].dot0 = celloffset + 5;
                     gridTriangle[9].dot1 = celloffset + 1;
                     gridTriangle[9].dot2 = celloffset + 0;

                     //back
                     gridTriangle[10].dot0 = celloffset + 3;
                     gridTriangle[10].dot1 = celloffset + 2;
                     gridTriangle[10].dot2 = celloffset + 6;

                     gridTriangle[11].dot0 = celloffset + 6;
                     gridTriangle[11].dot1 = celloffset + 7;
                     gridTriangle[11].dot2 = celloffset + 3;
                 }
                 MeshGeometry3D mesh = new MeshGeometry3D(positions,triangles);
                 return mesh;
             }
        }


        public override TextureCoordinatesBufferData CreateTextureCoordinates(GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
             HexahedronGridderSource src = (HexahedronGridderSource)source;
             int[] resultsVisibles = src.ExpandVisibles(gridIndexes);
             int[] bindVisibles = src.BindCellActive(src.BindVisibles, resultsVisibles);

            int dimenSize = src.DimenSize;
             float[] textures = src.GetDefaultTextureCoords();
             float distance = Math.Abs(maxValue - minValue);
             for (int i = 0; i < gridIndexes.Length; i++)
             {
                 int gridIndex = gridIndexes[i];
                 float value = values[i];
                 if (value < minValue)
                     value = minValue;
                 if (value > maxValue)
                     value = maxValue;

                 if (bindVisibles[gridIndex] > 0)
                 {
                     textures[gridIndex] = (value - minValue) / distance;
                 }
             }

            HexahedronTextureCoordinatesBufferData coordBuffer = new HexahedronTextureCoordinatesBufferData();
            unsafe{
               int bufferSize = src.DimenSize*sizeof(HexhedronTextureCoordinates);
               coordBuffer.AllocMem(bufferSize);
              
                HexhedronTextureCoordinates *coords = (HexhedronTextureCoordinates *)coordBuffer.Data;

               for (int  gridIndex= 0; gridIndex < dimenSize; gridIndex++)
               {
                   coords[gridIndex].SetCoord(textures[gridIndex]);
               }
             }
            return coordBuffer;
        }

    }
}
