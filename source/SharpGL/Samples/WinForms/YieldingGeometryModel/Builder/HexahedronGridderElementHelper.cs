using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel.Builder
{
    public static class HexahedronGridderElementHelper
    {
        public static void RandomVisibility(this HexahedronGridderElement element, OpenGL
             gl)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, element.visualBuffer);
            IntPtr visualArray = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);

            unsafe
            {
                int arrayLength = (int)(element.source.DimenSize * HexahedronGridderElement.vertexCountInHexahedron);

                float* visuals = (float*)visualArray.ToPointer();

                bool signal;

                uint gridderElementIndex = 0;
                foreach (Hexahedron hexahedron in element.source.GetGridderCells())
                {
                    // TODO: 此signal应由具体业务提供。
                    signal = (random.NextDouble() > 0.8);

                    // 计算位置信息。
                    int vertexIndex = 0;
                    foreach (Vertex vertex in hexahedron.GetVertexes())
                    {
                        visuals[gridderElementIndex + (vertexIndex++)] = signal ? 1 : 0;
                    }

                    gridderElementIndex += HexahedronGridderElement.vertexCountInHexahedron;
                }
            }

            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);
        }

        static Random random = new Random();
    }
}
