using SharpGL.SceneGraph;
using SimLab.GridSource.Factory;
using SimLab.SimGrid.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid.Factory
{
    public  class DynamicUnstructureGridFactory : GridBufferDataFactory
    {



        private unsafe DynamicUnstructureGeometry DoCreateMesh(DynamicUnstructuredGridderSource src)
        {
            MatrixPositionBufferData matrixPositions = null;
            MatrixIndicesBufferData matrixIndicesBuffer = null;
            FracturePositionBufferData fractionPositionsBuffer = null;

            //生成母体
            if (src.ElementFormat == DynamicUnstructuredGridderSource.MATRIX_FORMAT4_TETRAHEDRON)
            {

                matrixPositions = new MatrixPositionBufferData();
                matrixPositions.Shape = MatrixPositionBufferData.SHAPE_TETRAHEDRON;
                int memSize = src.ElementNum * sizeof(TetrahedronPositions);
                matrixPositions.AllocMem(memSize);
                TetrahedronPositions* tets = (TetrahedronPositions*)matrixPositions.Data;
                int[][] matrixIndices = src.Elements;
                Vertex[] positions = src.Nodes;
                for (int i = 0; i < src.ElementNum; i++)
                {
                    tets[i].p1 = positions[matrixIndices[i][0]-1];
                    tets[i].p2 = positions[matrixIndices[i][1]-1];
                    tets[i].p3 = positions[matrixIndices[i][2]-1];
                    tets[i].p4 = positions[matrixIndices[i][3]-1];
                }

                int triangleCount = src.ElementNum * 4;
                matrixIndicesBuffer = new MatrixIndicesBufferData();
                matrixIndicesBuffer.AllocMem(triangleCount * sizeof(TriangleIndex));
                TriangleIndex* triangles = (TriangleIndex*)matrixIndicesBuffer.Data;
                for (int i = 0; i < src.ElementNum; i++)
                {
                    TriangleIndex* tetraTriangles = triangles + (i * 4);
                    uint offset = (uint)(i * 4);
                    tetraTriangles[0].dot0 = offset + 0;
                    tetraTriangles[0].dot1 = offset + 1;
                    tetraTriangles[0].dot2 = offset + 2;

                    tetraTriangles[1].dot0 = offset + 0;
                    tetraTriangles[1].dot1 = offset + 1;
                    tetraTriangles[1].dot2 = offset + 3;


                    tetraTriangles[2].dot0 = offset + 0;
                    tetraTriangles[2].dot1 = offset + 2;
                    tetraTriangles[2].dot2 = offset + 3;

                    tetraTriangles[3].dot0 = offset + 1;
                    tetraTriangles[3].dot1 = offset + 2;
                    tetraTriangles[3].dot2 = offset + 3;
                }
            }
            if (src.ElementFormat == DynamicUnstructuredGridderSource.MATRIX_FORMAT3_TRIANGLE)
            {
                matrixPositions = new MatrixPositionBufferData();
                matrixPositions.Shape = MatrixPositionBufferData.SHAPE_TRIANGLE;
                int memSize = src.ElementNum * sizeof(TrianglePositions);
                matrixPositions.AllocMem(memSize);
                int[][] matrixIndices = src.Elements;
                Vertex[] positions = src.Nodes;
                TrianglePositions* triangles = (TrianglePositions*)matrixPositions.Data;
                for (int i = 0; i < src.ElementNum; i++)
                {
                    triangles[i].P1 = positions[matrixIndices[i][0]-1];
                    triangles[i].P2 = positions[matrixIndices[i][1]-1];
                    triangles[i].P3 = positions[matrixIndices[i][2]-1];
                }
            }

            //生成裂缝
            if (src.FractureFormat == DynamicUnstructuredGridderSource.FRACTURE_FORMAT3_TRIANGLE)
            {
                fractionPositionsBuffer = new FracturePositionBufferData();
                fractionPositionsBuffer.Shape = FracturePositionBufferData.SHAPE_TRIANGLE;
                int triangleCount = src.FractureNum;
                fractionPositionsBuffer.AllocMem(triangleCount * sizeof(TrianglePositions));
                int[][] triangleIndices = src.Fractures;
                Vertex[] positions = src.Nodes;
                TrianglePositions* triangles = (TrianglePositions*)fractionPositionsBuffer.Data;
                for (int i = 0; i < triangleCount; i++)
                {
                    triangles[i].P1 = positions[triangleIndices[i][0]-1];
                    triangles[i].P2 = positions[triangleIndices[i][1]-1];
                    triangles[i].P3 = positions[triangleIndices[i][2]-1];
                }
            }

            if (src.FractureFormat == DynamicUnstructuredGridderSource.FRACTURE_FORMAT2_LINE)
            {
                fractionPositionsBuffer = new FracturePositionBufferData();
                fractionPositionsBuffer.Shape = FracturePositionBufferData.SHAPE_LINE;
                int lineCount = src.FractureNum;
                fractionPositionsBuffer.AllocMem(lineCount * sizeof(LinePositions));
                LinePositions* lines = (LinePositions*)fractionPositionsBuffer.Data;
                Vertex[] positions = src.Nodes;
                int[][] lineIndices = src.Fractures;
                for (int i = 0; i < lineCount; i++)
                {
                    lines[i].P1 = positions[lineIndices[i][0]-1];
                    lines[i].P2 = positions[lineIndices[i][1]-1];
                }
            }

            DynamicUnstructureGeometry geometry = new DynamicUnstructureGeometry(matrixPositions);
            geometry.MatrixIndices = matrixIndicesBuffer;
            geometry.FracturePositions = fractionPositionsBuffer;
            geometry.Min = src.Min;
            geometry.Max = src.Max;
            return geometry;
        }
        /// <summary>
        /// 生成DynamicUnstructureGeometry
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override MeshBase CreateMesh(GridSource.GridderSource source)
        {
            return this.DoCreateMesh((DynamicUnstructuredGridderSource)source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="gridIndexes"></param>
        /// <param name="values"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public override TextureCoordinatesBufferData CreateTextureCoordinates(GridSource.GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            return this.DoCreateMatrixTextureCoordinates((DynamicUnstructuredGridderSource)source, gridIndexes, values, minValue, maxValue);
        }


        public  TextureCoordinatesBufferData CreateFractureTextureCoordinates(DynamicUnstructuredGridderSource src, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            return DoCreateFractureTextureCoordinates(src, gridIndexes, values, minValue, maxValue);
        }

        protected static unsafe TextureCoordinatesBufferData DoCreateFractureTextureCoordinates(DynamicUnstructuredGridderSource src, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            int fractureStartIndex = 0;
            int fractureEndIndex = src.FractureNum - 1;
            int[] invisibles = src.BindResultsAndActiveFractures(gridIndexes);
            float[] textures = new float[src.InvisibleFractureTextures.Length];
            Array.Copy(src.InvisibleFractureTextures, textures, textures.Length);

            for (int mixedIndex = 0; mixedIndex < gridIndexes.Length; mixedIndex++)
            {
                int gridIndex = gridIndexes[mixedIndex];
                if (gridIndex >= fractureStartIndex && gridIndex <= fractureEndIndex)
                {
                    float value = values[mixedIndex];
                    if (value < minValue)
                        value = minValue;
                    if (value > maxValue)
                        value = maxValue;
                    int matrixIndex = gridIndex;
                    if (invisibles[matrixIndex] > 0)
                    {
                        float distance = maxValue - minValue;
                        if (!(distance <= 0.0f))
                        {
                            textures[matrixIndex] = (value - minValue) / distance;
                            if (textures[matrixIndex] < 0.5f)
                            {
                                textures[matrixIndex] = 0.5f - (0.5f - textures[matrixIndex]) * 0.99f;
                            }
                            else
                            {
                                textures[matrixIndex] = (textures[matrixIndex] - 0.5f) * 0.99f + 0.5f;
                            }
                        }
                        else
                        {
                            //最小值最大值相等时，显示最小值的颜色
                            textures[matrixIndex] = 0.01f;
                        }
                    }
                }
            }//end for


            TextureCoordinatesBufferData textureCoordinates = new TextureCoordinatesBufferData();
            int texturesCount = src.FractureNum;
            if (src.FractureFormat == DynamicUnstructuredGridderSource.FRACTURE_FORMAT3_TRIANGLE)
            {
                textureCoordinates.AllocMem(texturesCount * sizeof(TriangleUV));
                TriangleUV* pTextures = (TriangleUV*)textureCoordinates.Data;
                for (int i = 0; i < textures.Length; i++)
                {
                    pTextures[i].SetTextureCoord(textures[i]);
                }
            }
            if (src.FractureFormat == DynamicUnstructuredGridderSource.FRACTURE_FORMAT2_LINE)
            {
                textureCoordinates.AllocMem(texturesCount * sizeof(LineUV));
                LineUV* pTextures = (LineUV*)textureCoordinates.Data;
                for (int i = 0; i < textures.Length; i++)
                {
                    pTextures[i].SetTextureCoord(textures[i]);
                }
            }
            return textureCoordinates;
        }

        /// <summary>
        /// 生成基质的纹理映射坐标
        /// </summary>
        /// <param name="src"></param>
        /// <param name="gridIndexes"></param>
        /// <param name="values"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        protected unsafe TextureCoordinatesBufferData DoCreateMatrixTextureCoordinates(DynamicUnstructuredGridderSource src, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            int matrixStartIndex = src.FractureNum;
            int matrixEndIndex = src.DimenSize - 1;
            int[] invisibles = src.BindResultsAndActiveMatrix(gridIndexes);
            float[] textures = new float[src.InvisibleMatrixTextures.Length];
            Array.Copy(src.InvisibleMatrixTextures, textures, textures.Length);

            for (int mixedIndex = 0; mixedIndex < gridIndexes.Length; mixedIndex++)
            {
                int gridIndex = gridIndexes[mixedIndex];
                if (gridIndex >= matrixStartIndex && gridIndex < src.DimenSize)
                {
                    float value = values[mixedIndex];
                    if (value < minValue)
                        value = minValue;
                    if (value > maxValue)
                        value = maxValue;
                    int matrixIndex = gridIndex - matrixStartIndex;
                    if (invisibles[matrixIndex] > 0)
                    {
                        float distance = maxValue - minValue;
                        if (!(distance <= 0.0f))
                        {
                            textures[matrixIndex] = (value - minValue) / distance;
                            //if (textures[matrixIndex] < 0.5f)
                            //{
                            //    textures[matrixIndex] = 0.5f - (0.5f - textures[matrixIndex]) * 0.99f;
                            //}
                            //else
                            //{
                            //    textures[matrixIndex] = (textures[matrixIndex] - 0.5f) * 0.99f + 0.5f;
                            //}
                        }
                        else
                        {
                            //最小值最大值相等时，显示最小值的颜色
                            //textures[matrixIndex] = 0.01f;
                            textures[gridIndex] = 0;
                        }
                    }
                }
            }//end for


            TextureCoordinatesBufferData textureCoordinates = new TextureCoordinatesBufferData();
            int texturesCount = src.ElementNum;
            if (src.ElementFormat == DynamicUnstructuredGridderSource.MATRIX_FORMAT3_TRIANGLE)
            {
                textureCoordinates.AllocMem(texturesCount * sizeof(TriangleUV));
                TriangleUV* pTextures = (TriangleUV*)textureCoordinates.Data;
                for (int i = 0; i < textures.Length; i++)
                {
                    pTextures[i].SetTextureCoord(textures[i]);
                }
            }
            if (src.ElementFormat == DynamicUnstructuredGridderSource.MATRIX_FORMAT4_TETRAHEDRON)
            {
                textureCoordinates.AllocMem(texturesCount * sizeof(TetrahedronUV));
                TetrahedronUV* pTextures = (TetrahedronUV*)textureCoordinates.Data;
                for (int i = 0; i < textures.Length; i++)
                {
                    pTextures[i].SetTextureCoord(textures[i]);
                }
            }
            return textureCoordinates;
        }

    }
}
