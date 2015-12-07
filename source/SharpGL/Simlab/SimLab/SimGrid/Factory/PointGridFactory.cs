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
            RadiusBufferData   radiusBuffer = new RadiusBufferData();
            int dimSize = src.DimenSize;
            Random random = new Random();
            // setup positions
            unsafe
            {
                int gridMemSize = dimSize * sizeof(Vertex);
                positions.AllocMem(gridMemSize);
                Vertex* cells = (Vertex*)positions.Data;
                for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                {
                    Vertex p = src.Positions[gridIndex];
                    cells[gridIndex] = p;
                    if (!isSet)
                    {
                        minVertex = p;
                        maxVertex = p;
                        isSet = true;
                    }
                    
                   
                    if (src.IsActiveBlock(gridIndex))
                    {
                        minVertex = MinVertex(minVertex, p);
                        maxVertex = MaxVertex(maxVertex, p);
                    }
                }
            }
            // setup radius
            unsafe
            {
                int gridMemSize = dimSize * sizeof(float);
                radiusBuffer.AllocMem(gridMemSize);
                float* radiues = (float*)radiusBuffer.Data;
                for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                {
                    radiues[gridIndex] = src.Radius[gridIndex];
                }
            }
            PointMeshGeometry3D mesh = new PointMeshGeometry3D(positions, radiusBuffer, dimSize);
            mesh.Max = maxVertex;
            mesh.Min = minVertex;
            return mesh;
        }
        

        public override TextureCoordinatesBufferData CreateTextureCoordinates(GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            PointGridderSource src = (PointGridderSource)source;
            int[] visibles = src.BindResultsVisibles(gridIndexes);
            int dimenSize = src.DimenSize;
            float[] textures = src.GetInvisibleTextureCoords();
            float distance = Math.Abs(maxValue - minValue);
            for (int i = 0; i < gridIndexes.Length; i++)
            {
                int gridIndex = gridIndexes[i];
                if (visibles[gridIndex] > 0)
                {
                    float value = values[i];
                    if (value < minValue)
                        value = minValue;
                    if (value > maxValue)
                        value = maxValue;

                    if (!(distance <= 0.0f))
                    {
                        textures[gridIndex] = (value - minValue) / distance;
                        //if (textures[gridIndex] < 0.5f)
                        //{
                        //    textures[gridIndex] = 0.5f - (0.5f - textures[gridIndex]) * 0.99f;
                        //}
                        //else
                        //{
                        //    textures[gridIndex] = (textures[gridIndex] - 0.5f) * 0.99f + 0.5f;
                        //}
                    }
                    else
                    {
                        //最小值最大值相等时，显示最小值的颜色
                        //textures[gridIndex] = 0.01f;
                        textures[gridIndex] = 0;
                    }
                }
            }

            HexahedronTextureCoordinatesBufferData coordBuffer = new HexahedronTextureCoordinatesBufferData();
            unsafe
            {
                int bufferSize = src.DimenSize * sizeof(float);
                coordBuffer.AllocMem(bufferSize);

                float* coords = (float*)coordBuffer.Data;
                for (int gridIndex = 0; gridIndex < dimenSize; gridIndex++)
                {
                    coords[gridIndex] = textures[gridIndex];
                }
            }
            return coordBuffer;
        }

    }
}
