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
        /// <summary>
        /// 随机决定此gridder内的各个元素的可见性。
        /// </summary>
        /// <param name="element"></param>
        /// <param name="gl"></param>
        /// <param name="probability">可见度，范围为0 ~ 1，0为全部不可见，1为全部可见。</param>
        public static void RandomVisibility(this HexahedronGridderElement element, OpenGL
             gl, double probability)
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
                    signal = (random.NextDouble() < probability);

                    // 计算visual信息。
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
