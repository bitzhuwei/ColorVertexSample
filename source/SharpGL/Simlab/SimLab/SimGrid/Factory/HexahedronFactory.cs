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
                 int gridMemSize = dimSize * sizeof(HexahedronPositions);
                 positions.AllocMem(gridMemSize);
                 HexahedronPositions* cell = (HexahedronPositions*)positions.Data;
                 for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                 {
                     src.InvertIJK(gridIndex, out I, out J, out K);
                     cell[gridIndex].FLT = src.PointFLT(I, J, K);
                     cell[gridIndex].FRT = src.PointFRT(I, J, K);
                     cell[gridIndex].BRT = src.PointBRT(I, J, K);
                     cell[gridIndex].BLT = src.PointBLT(I, J, K);
                     cell[gridIndex].FLB = src.PointFLB(I, J, K);
                     cell[gridIndex].FRB = src.PointFRB(I, J, K);
                     cell[gridIndex].BRB = src.PointBRB(I, J, K);
                     cell[gridIndex].BLB = src.PointBLB(I, J, K);
                 }

                 //网格个数*每个六面体的面数*描述每个六面体的三角形个数
                 int triangleCount = dimSize * 6 * 2;
                 int triangleSize = triangleCount * sizeof(TriangleIndex);
                 triangles.AllocMem(triangleSize);

                 int celloffset = 0; //每个网格数描述的点
                 TriangleIndex* first = (TriangleIndex*)triangles.Data;
                 for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                 {
                     //网格三角形的偏移量
                     int gto= gridIndex * (6 * 2);

                     //任意网格三角形的首指针
                     TriangleIndex* gridTriangle = first + gto;
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

        public override WireFrameBufferData CreateWireFrame(GridderSource source)
        {
            WireFrameBufferData wireframe = new WireFrameBufferData();
            int lineCount = source.DimenSize*12;
            unsafe
            {
                int size = lineCount * sizeof(LineIndex);
                wireframe.AllocMem(size);
                LineIndex* first = (LineIndex *)wireframe.Data;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    LineIndex* cellLines = first + (gridIndex * 12);
                    uint offset = (uint)(gridIndex*8);

                    //top
                    cellLines[0].p0 =offset  + 0;
                    cellLines[0].p1 = offset + 1;

                    cellLines[1].p0 = offset + 1;
                    cellLines[1].p1 = offset + 2;

                    cellLines[2].p0 = offset + 2;
                    cellLines[2].p1 = offset + 3;


                    cellLines[3].p0 = offset + 3;
                    cellLines[3].p1 = offset + 0;

                    //bottomm
                    cellLines[4].p0 = offset + 4;
                    cellLines[4].p1 = offset + 5;

                    cellLines[5].p0 = offset + 5;
                    cellLines[5].p1 = offset + 6;

                    cellLines[6].p0 = offset + 6;
                    cellLines[6].p1 = offset + 7;

                    cellLines[7].p0 = offset + 7;
                    cellLines[7].p1 = offset + 4;

                    //pillar
                    cellLines[8].p0 = offset + 0;
                    cellLines[9].p1 = offset + 4;

                    cellLines[9].p0 = offset + 2;
                    cellLines[9].p1 = offset + 5;

                    cellLines[10].p0 = offset + 3;
                    cellLines[10].p1 = offset + 6;

                    cellLines[11].p0 = offset + 4;
                    cellLines[11].p1 = offset + 7;

                }
                return wireframe;
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
               int bufferSize = src.DimenSize*sizeof(HexahedronUVs);
               coordBuffer.AllocMem(bufferSize);
              
                HexahedronUVs *coords = (HexahedronUVs *)coordBuffer.Data;

               for (int  gridIndex= 0; gridIndex < dimenSize; gridIndex++)
               {
                   coords[gridIndex].SetCoord(textures[gridIndex]);
               }
             }
            return coordBuffer;
        }

    }
}
