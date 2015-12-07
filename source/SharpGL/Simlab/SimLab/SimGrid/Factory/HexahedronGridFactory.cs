using SharpGL.SceneGraph;
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
    public class HexahedronGridFactory : GridBufferDataFactory
    {

        protected Vertex MinVertex(Vertex min, Vertex value)
        {
            if (min.X > value.X)
                min.X = value.X;
            if (min.Y > value.Y)
                min.Y = value.Y;
            if (min.Z > value.Z)
                min.Z = value.Z;
            return min;
        }

        protected Vertex MaxVertex(Vertex max, Vertex value)
        {
            if (max.X < value.X)
                max.X = value.X;
            if (max.Y < value.Y)
                max.Y = value.Y;
            if (max.Z < value.Z)
                max.Z = value.Z;
            return max;
        }


        public override MeshBase CreateMesh(GridderSource source)
        {
            HexahedronGridderSource src = (HexahedronGridderSource)source;
            Vertex minVertex = new Vertex();
            Vertex maxVertex = new Vertex();
            bool isSet = false;
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

                    if (!isSet && src.IsActiveBlock(gridIndex))
                    {
                        minVertex = cell[gridIndex].FLT;
                        maxVertex = minVertex;
                        isSet = true;
                    }

                    if (isSet && src.IsActiveBlock(gridIndex))
                    {
                        minVertex = MinVertex(minVertex, cell[gridIndex].FLT);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].FLT);

                        minVertex = MinVertex(minVertex, cell[gridIndex].FRT);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].FRT);

                        minVertex = MinVertex(minVertex, cell[gridIndex].BRT);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].BRT);

                        minVertex = MinVertex(minVertex, cell[gridIndex].BLT);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].BLT);

                        minVertex = MinVertex(minVertex, cell[gridIndex].FLB);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].FLB);

                        minVertex = MinVertex(minVertex, cell[gridIndex].FRB);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].FRB);

                        minVertex = MinVertex(minVertex, cell[gridIndex].BRB);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].BRB);

                        minVertex = MinVertex(minVertex, cell[gridIndex].BLB);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex].BLB);
                    }
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
                    int gto = gridIndex * (6 * 2);

                    //任意网格三角形的首指针
                    TriangleIndex* gridTriangle = first + gto;
                    celloffset = gridIndex * 8;

                    //top
                    gridTriangle[0].dot0 = (uint)(celloffset + 0);
                    gridTriangle[0].dot1 = (uint)(celloffset + 1);
                    gridTriangle[0].dot2 = (uint)(celloffset + 2);

                    gridTriangle[1].dot0 = (uint)(celloffset + 0);
                    gridTriangle[1].dot1 = (uint)(celloffset + 2);
                    gridTriangle[1].dot2 = (uint)(celloffset + 3);

                    //bottom
                    gridTriangle[2].dot0 = (uint)(celloffset + 4);
                    gridTriangle[2].dot1 = (uint)(celloffset + 5);
                    gridTriangle[2].dot2 = (uint)(celloffset + 7);

                    gridTriangle[3].dot0 = (uint)(celloffset + 7);
                    gridTriangle[3].dot1 = (uint)(celloffset + 6);
                    gridTriangle[3].dot2 = (uint)(celloffset + 5);

                    //left
                    gridTriangle[4].dot0 = (uint)(celloffset + 0);
                    gridTriangle[4].dot1 = (uint)(celloffset + 3);
                    gridTriangle[4].dot2 = (uint)(celloffset + 4);

                    gridTriangle[5].dot0 = (uint)(celloffset + 4);
                    gridTriangle[5].dot1 = (uint)(celloffset + 7);
                    gridTriangle[5].dot2 = (uint)(celloffset + 3);

                    //right
                    gridTriangle[6].dot0 = (uint)(celloffset + 1);
                    gridTriangle[6].dot1 = (uint)(celloffset + 2);
                    gridTriangle[6].dot2 = (uint)(celloffset + 5);


                    gridTriangle[7].dot0 = (uint)(celloffset + 5);
                    gridTriangle[7].dot1 = (uint)(celloffset + 6);
                    gridTriangle[7].dot2 = (uint)(celloffset + 2);

                    //front
                    gridTriangle[8].dot0 = (uint)(celloffset + 0);
                    gridTriangle[8].dot1 = (uint)(celloffset + 4);
                    gridTriangle[8].dot2 = (uint)(celloffset + 5);

                    gridTriangle[9].dot0 = (uint)(celloffset + 5);
                    gridTriangle[9].dot1 = (uint)(celloffset + 1);
                    gridTriangle[9].dot2 = (uint)(celloffset + 0);

                    //back
                    gridTriangle[10].dot0 = (uint)(celloffset + 3);
                    gridTriangle[10].dot1 = (uint)(celloffset + 2);
                    gridTriangle[10].dot2 = (uint)(celloffset + 6);

                    gridTriangle[11].dot0 = (uint)(celloffset + 6);
                    gridTriangle[11].dot1 = (uint)(celloffset + 7);
                    gridTriangle[11].dot2 = (uint)(celloffset + 3);
                }
                HexahedronMeshGeometry3D mesh = new HexahedronMeshGeometry3D(positions, triangles);
                mesh.Max = maxVertex;
                mesh.Min = minVertex;
                return mesh;
            }
        }

        public override WireFrameBufferData CreateWireFrame(GridderSource source)
        {
            WireFrameBufferData wireframe = new WireFrameBufferData();
            int lineCount = source.DimenSize * 12;
            unsafe
            {
                int size = lineCount * sizeof(LineIndex);
                wireframe.AllocMem(size);
                LineIndex* cellLines = (LineIndex*)wireframe.Data;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    int index = gridIndex * 12;
                    uint offset = (uint)(gridIndex * 8);

                    //top
                    cellLines[index+0].p0 = offset + 0;
                    cellLines[index+0].p1 = offset + 1;

                    cellLines[index + 1].p0 = offset + 1;
                    cellLines[index + 1].p1 = offset + 2;

                    cellLines[index + 2].p0 = offset + 2;
                    cellLines[index + 2].p1 = offset + 3;


                    cellLines[index + 3].p0 = offset + 3;
                    cellLines[index + 3].p1 = offset + 0;

                    //bottom
                    cellLines[index + 4].p0 = offset + 4;
                    cellLines[index + 4].p1 = offset + 5;

                    cellLines[index + 5].p0 = offset + 5;
                    cellLines[index + 5].p1 = offset + 6;

                    cellLines[index + 6].p0 = offset + 6;
                    cellLines[index + 6].p1 = offset + 7;

                    cellLines[index + 7].p0 = offset + 7;
                    cellLines[index + 7].p1 = offset + 4;

                    //pillar
                    cellLines[index + 8].p0 = offset + 0;
                    cellLines[index + 8].p1 = offset + 4;

                    cellLines[index + 9].p0 = offset + 1;
                    cellLines[index + 9].p1 = offset + 5;

                    cellLines[index + 10].p0 = offset + 2;
                    cellLines[index + 10].p1 = offset + 6;

                    cellLines[index + 11].p0 = offset + 3;
                    cellLines[index + 11].p1 = offset + 7;

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
                    if (!(distance <= 0.0f))
                    {
                        textures[gridIndex] = (value - minValue) / distance;
                        if (textures[gridIndex] < 0.5f)
                        {
                            textures[gridIndex] = 0.5f - (0.5f - textures[gridIndex]) * 0.99f;
                        }
                        else
                        {
                            textures[gridIndex] = (textures[gridIndex] - 0.5f) * 0.99f + 0.5f;
                        }
                    }
                    else
                    {
                        //最小值最大值相等时，显示最小值的颜色
                        textures[gridIndex] = 0.01f;
                    }
                }
                else
                {
                     textures[gridIndex] = 2.0f;
                }
            }

            HexahedronTextureCoordinatesBufferData coordBuffer = new HexahedronTextureCoordinatesBufferData();
            unsafe
            {
                int bufferSize = src.DimenSize * sizeof(HexahedronUVs);
                coordBuffer.AllocMem(bufferSize);

                HexahedronUVs* coords = (HexahedronUVs*)coordBuffer.Data;

                for (int gridIndex = 0; gridIndex < dimenSize; gridIndex++)
                {
                    coords[gridIndex].SetCoord(textures[gridIndex]);
                }
            }
            return coordBuffer;
        }

    }
}
