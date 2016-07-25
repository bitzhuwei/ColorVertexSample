using SharpGL.SceneGraph;
using SimLab.SimGrid;
using SimLab.SimGrid.Geometry;
using SimLab.VertexBuffers;
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
            PointPositionBuffer positions = new PointPositionBuffer();
            PointRadiusBuffer radiusBuffer = null;
            int dimSize = src.DimenSize;
            // setup positions
            unsafe
            {
                positions.AllocMem(dimSize);
                Vertex* cells = (Vertex*)positions.Data;
                for (int gridIndex = 0; gridIndex < dimSize; gridIndex++)
                {
                    Vertex p = src.Transform * src.Positions[gridIndex];
                    cells[gridIndex] = p;
                }
            }
            radiusBuffer = this.CreateRadiusBufferData(src, src.Radius);
            PointMeshGeometry3D mesh = new PointMeshGeometry3D(positions, radiusBuffer, dimSize);
            mesh.Max = src.TransformedActiveBounds.Max;
            mesh.Min = src.TransformedActiveBounds.Min;
            return mesh;
        }


        public PointRadiusBuffer CreateRadiusBufferData(PointGridderSource src, float[] radius)
        {
            PointRadiusBuffer radiusBuffer = new PointRadiusBuffer();
            unsafe
            {
                int dimenSize = src.DimenSize;
                radiusBuffer.AllocMem(dimenSize);
                float* radiues = (float*)radiusBuffer.Data;
                for (int gridIndex = 0; gridIndex < dimenSize; gridIndex++)
                {
                    radiues[gridIndex] = radius[gridIndex];
                }
            }
            return radiusBuffer;
        }


        public PointRadiusBuffer CreateRadiusBufferData(PointGridderSource src, float radius)
        {

            PointRadiusBuffer radiusBuffer = new PointRadiusBuffer();
            unsafe
            {
                int dimenSize = src.DimenSize;
                radiusBuffer.AllocMem(dimenSize);
                float* radiues = (float*)radiusBuffer.Data;
                for (int gridIndex = 0; gridIndex < dimenSize; gridIndex++)
                {
                    radiues[gridIndex] = radius;
                }
            }
            return radiusBuffer;
        }



        public override TexCoordBuffer CreateTextureCoordinates(GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            PointGridderSource src = (PointGridderSource)source;
            int[] blockVisibles = src.BindResultsVisibles(gridIndexes);
            int dimenSize = src.DimenSize;

            float[] textures = src.GetInvisibleTextureCoords();
            float distance = Math.Abs(maxValue - minValue);
            for (int i = 0; i < gridIndexes.Length; i++)
            {
                int gridIndex = gridIndexes[i];
                int[] mappedBlockIndexes = source.MapBlockIndexes(gridIndex);
                for (int jblockIndex = 0; jblockIndex < mappedBlockIndexes.Length; jblockIndex++)
                {
                    int block = mappedBlockIndexes[jblockIndex];
                    if(block<0||block >=dimenSize)
                       continue;

                    if (blockVisibles[block] > 0)
                    {
                        float value = values[i];
                        if (value < minValue)
                            value = minValue;
                        if (value > maxValue)
                            value = maxValue;

                        if (!(distance <= 0.0f))
                        {
                            textures[block] = (value - minValue) / distance;
                            if (textures[block] < 0.5f)
                            {
                                textures[block] = 0.5f - (0.5f - textures[block]) * 0.99f;
                            }
                            else
                            {
                                textures[block] = (textures[block] - 0.5f) * 0.99f + 0.5f;
                            }
                        }
                        else
                        {
                            //最小值最大值相等时，显示最小值的颜色
                            textures[block] = 0.01f;
                            //textures[gridIndex] = 0;
                        }
                    }
                }//end for
            }

            PointTexCoordBuffer coordBuffer = new PointTexCoordBuffer();
            unsafe
            {
                coordBuffer.AllocMem(src.DimenSize);
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
