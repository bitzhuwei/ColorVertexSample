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

        public override MeshBase CreateMesh(GridderSource source)
        {
            HexahedronGridderSource src = (HexahedronGridderSource)source;
            Vertex minVertex = new Vertex();
            Vertex maxVertex = new Vertex();
            bool isSet = false;
            PositionsBufferData positions = new HexahedronPositionBufferData();
            HalfHexahedronIndicesBufferData halfHexahedronIndices = new HalfHexahedronIndicesBufferData();
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
                int halfHexahedronIndexCount = dimSize * 2;
                int memorySizeInBytes = halfHexahedronIndexCount * sizeof(HalfHexahedronIndex);
                halfHexahedronIndices.AllocMem(memorySizeInBytes);

                HalfHexahedronIndex* array = (HalfHexahedronIndex*)halfHexahedronIndices.Data;
                for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                {
                    array[gridIndex * 2].dot0 = (uint)(8 * gridIndex + 6);
                    array[gridIndex * 2].dot1 = (uint)(8 * gridIndex + 2);
                    array[gridIndex * 2].dot2 = (uint)(8 * gridIndex + 7);
                    array[gridIndex * 2].dot3 = (uint)(8 * gridIndex + 3);
                    array[gridIndex * 2].dot4 = (uint)(8 * gridIndex + 4);
                    array[gridIndex * 2].dot5 = (uint)(8 * gridIndex + 0);
                    array[gridIndex * 2].dot6 = (uint)(8 * gridIndex + 5);
                    array[gridIndex * 2].dot7 = (uint)(8 * gridIndex + 1);
                    array[gridIndex * 2].restartIndex = uint.MaxValue;

                    array[gridIndex * 2 + 1].dot0 = (uint)(8 * gridIndex + 3);
                    array[gridIndex * 2 + 1].dot1 = (uint)(8 * gridIndex + 0);
                    array[gridIndex * 2 + 1].dot2 = (uint)(8 * gridIndex + 2);
                    array[gridIndex * 2 + 1].dot3 = (uint)(8 * gridIndex + 1);
                    array[gridIndex * 2 + 1].dot4 = (uint)(8 * gridIndex + 6);
                    array[gridIndex * 2 + 1].dot5 = (uint)(8 * gridIndex + 5);
                    array[gridIndex * 2 + 1].dot6 = (uint)(8 * gridIndex + 7);
                    array[gridIndex * 2 + 1].dot7 = (uint)(8 * gridIndex + 4);
                    array[gridIndex * 2 + 1].restartIndex = uint.MaxValue;
                }

                HexahedronMeshGeometry3D mesh = new HexahedronMeshGeometry3D(positions, halfHexahedronIndices);
                mesh.Max = maxVertex;
                mesh.Min = minVertex;
                return mesh;
            }
        }

        /*
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
                    cellLines[index + 0].p0 = offset + 0;
                    cellLines[index + 0].p1 = offset + 1;

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
        */

        public override TextureCoordinatesBufferData CreateTextureCoordinates(GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            HexahedronGridderSource src = (HexahedronGridderSource)source;
            int[] resultsVisibles = src.ExpandVisibles(gridIndexes);
            int[] bindVisibles = src.BindCellActive(src.BindVisibles, resultsVisibles);

            int dimenSize = src.DimenSize;
            float[] textures = src.GetInvisibleTextureCoords();
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
                        //textures[gridIndex] = 0.0f;
                    }
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
