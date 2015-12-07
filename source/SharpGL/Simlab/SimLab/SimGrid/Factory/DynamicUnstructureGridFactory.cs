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
    public class DynamicUnstructureGridFactory : GridBufferDataFactory
    {



        private unsafe DynamicUnstructureGeometry DoCreateMesh(DynamicUnstructuredGridderSource src)
        {
            MatrixPositionBufferData matrixPositions = null;
            MatrixIndicesBufferData   matrixIndicesBuffer = null;
            FracturePositionBufferData fractionPositionsBuffer = null;

            //生成母体
            if (src.ElementFormat == DynamicUnstructuredGridderSource.MATRIX_FORMAT4_TETRAHEDRON)
            {

                matrixPositions = new MatrixPositionBufferData();
                matrixPositions.Shape = MatrixPositionBufferData.SHAPE_TETRAHEDRON;
                int memSize = src.ElementNum * sizeof(TetrahedronPositions);
                matrixPositions.AllocMem(memSize);
                TetrahedronPositions* tets = (TetrahedronPositions *)matrixPositions.Data;
                int[][] matrixIndices =  src.Elements;
                Vertex[] positions = src.Nodes;
                for (int i = 0; i < src.ElementNum; i++)
                {
                    tets[i].p1 = positions[matrixIndices[i][0]];
                    tets[i].p2 = positions[matrixIndices[i][1]];
                    tets[i].p3 = positions[matrixIndices[i][2]];
                    tets[i].p4 = positions[matrixIndices[i][3]];
                }

                int triangleCount = src.ElementNum*4;
                matrixIndicesBuffer = new MatrixIndicesBufferData();
                matrixIndicesBuffer.AllocMem(triangleCount * sizeof(TriangleIndex));
                TriangleIndex* triangles = (TriangleIndex *)matrixIndicesBuffer.Data;
                for (int i = 0; i < src.ElementNum; i++)
                {
                    TriangleIndex* tetraTriangles = triangles + (i * 4);
                    uint offset  = (uint)(i*4);
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
                int[][] matrixIndices = src.Elements;
                Vertex[] positions = src.Nodes;
                TetrahedronPositions* triangles = (TetrahedronPositions*)matrixPositions.Data;
                for (int i = 0; i < src.ElementNum; i++)
                {
                    triangles[i].p1 = positions[matrixIndices[i][0]];
                    triangles[i].p2 = positions[matrixIndices[i][0]];
                    triangles[i].p3 = positions[matrixIndices[i][0]];
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
                TrianglePositions* triangles = (TrianglePositions *)fractionPositionsBuffer.Data;
                for (int i = 0; i < triangleCount; i++)
                {
                    triangles[i].P1 = positions[triangleIndices[i][0]];
                    triangles[i].P2 = positions[triangleIndices[i][1]];
                    triangles[i].P3 = positions[triangleIndices[i][2]];
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
                int[][] lineIndices =  src.Fractures;
                for (int i = 0; i < lineCount; i++)
                {
                    lines[i].P1 = positions[lineIndices[i][0]];
                    lines[i].P2 = positions[lineIndices[i][1]];
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



            return null;
        }

        public override WireFrameBufferData CreateWireFrame(GridSource.GridderSource source)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
