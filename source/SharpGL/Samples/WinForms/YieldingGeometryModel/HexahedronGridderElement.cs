using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public class HexahedronGridderElement : SceneElement, IRenderable
    {

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons. 
        /// </summary>
        /// <param name="source">用于生成网格内所有元素的数据源。</param>
        public HexahedronGridderElement(HexahedronGridderSource source)
        {
            const int vertexCountInHexahedron = 8;// 元素内的顶点数
            const int elementCountInVertex = 3;// 顶点的元素数

            int count = source.DimenSize * vertexCountInHexahedron * elementCountInVertex;

            // 稍后将用InPtr代替float[]
            float[] positions = new float[count];
            float[] colors = new float[count];

            int gridderElementIndex = 0;
            foreach (Hexahedron hexahedron in source.GetGridderCells())
            {
                // 计算位置信息。
                {
                    int vertexIndex = 0;
                    foreach (Vertex vertex in hexahedron.GetVertexes())
                    {
                        positions[gridderElementIndex + (vertexIndex++)] = vertex.X;
                        positions[gridderElementIndex + (vertexIndex++)] = vertex.Y;
                        positions[gridderElementIndex + (vertexIndex++)] = vertex.Z;
                    }
                }
                // 计算颜色信息。
                {
                    GLColor color = hexahedron.color;
                    for (int vertexIndex = 0; vertexIndex < vertexCountInHexahedron; vertexIndex++)
                    {
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 0] = color.R;
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 1] = color.R;
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 2] = color.R;
                    }
                }

                gridderElementIndex += vertexCountInHexahedron * elementCountInVertex;
            }
        }

        #region IRenderable 成员

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
