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
    public class PointGridFactory : GridBufferDataFactory
    {

        public override MeshBase CreateMesh(GridderSource source)
        {
            PointGridderSource src = (PointGridderSource)source;
            Vertex minVertex = new Vertex();
            Vertex maxVertex = new Vertex();
            bool isSet = false;
            PositionsBufferData positions = new PositionsBufferData();
            RadiusBufferData radius = new RadiusBufferData();
            int dimSize = src.DimenSize;
            int I, J, K;
            Random random = new Random();
            // setup positions
            unsafe
            {
                int gridMemSize = dimSize * sizeof(Vertex);
                positions.AllocMem(gridMemSize);
                Vertex* cell = (Vertex*)positions.Data;
                for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                {
                    cell[gridIndex].Set(
                        (float)random.NextDouble() * 1000,
                        (float)random.NextDouble() * 1000,
                        (float)random.NextDouble() * 1000);

                    if (!isSet && src.IsActiveBlock(gridIndex))
                    {
                        minVertex = cell[gridIndex];
                        maxVertex = minVertex;
                        isSet = true;
                    }

                    if (isSet && src.IsActiveBlock(gridIndex))
                    {
                        minVertex = MinVertex(minVertex, cell[gridIndex]);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex]);
                    }
                }
            }
            // setup radius
            unsafe
            {
                int gridMemSize = dimSize * sizeof(Vertex);
                radius.AllocMem(gridMemSize);
                Vertex* cell = (Vertex*)radius.Data;
                for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                {
                    cell[gridIndex].Set(
                        (float)random.NextDouble() * 1000,
                        (float)random.NextDouble() * 1000,
                        (float)random.NextDouble() * 1000);

                    if (!isSet && src.IsActiveBlock(gridIndex))
                    {
                        minVertex = cell[gridIndex];
                        maxVertex = minVertex;
                        isSet = true;
                    }

                    if (isSet && src.IsActiveBlock(gridIndex))
                    {
                        minVertex = MinVertex(minVertex, cell[gridIndex]);
                        maxVertex = MaxVertex(maxVertex, cell[gridIndex]);
                    }
                }
            }
            PointMeshGeometry3D mesh = new PointMeshGeometry3D(positions, radius, dimSize);
            mesh.Max = maxVertex;
            mesh.Min = minVertex;
            return mesh;
        }

        public override WireFrameBufferData CreateWireFrame(GridderSource source)
        {
            return null;
        }


        public override TextureCoordinatesBufferData CreateTextureCoordinates(GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            PointGridderSource src = (PointGridderSource)source;

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
