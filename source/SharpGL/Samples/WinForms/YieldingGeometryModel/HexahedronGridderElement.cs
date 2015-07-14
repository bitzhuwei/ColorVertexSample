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
        private bool preparedForRendering = false;
        private float[] positions;
        private float[] colors;
        private float[] indexes;

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons. 
        /// </summary>
        /// <param name="source">用于生成网格内所有元素的数据源。</param>
        public HexahedronGridderElement(HexahedronGridderSource source)
        {
            PrepareVertexAttributes(source);
        }

        /// <summary>
        /// 准备各项顶点属性。
        /// </summary>
        /// <param name="source"></param>
        private void PrepareVertexAttributes(HexahedronGridderSource source)
        {
            const int vertexCountInHexahedron = 8;// 元素内的顶点数
            const int elementCountInVertex = 3;// 顶点的元素数
            int arrayLength = source.DimenSize * vertexCountInHexahedron * elementCountInVertex;

            const int triangleStrip = 14;
            // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexLength = source.DimenSize * (triangleStrip + 1);

            // 稍后将用InPtr代替float[]
            float[] positions = new float[arrayLength];
            float[] colors = new float[arrayLength];
            float[] indexes = new float[indexLength];

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

            for (int i = 0; i < indexLength / (triangleStrip + 1); i++)
            {
                indexes[i * (triangleStrip + 1) + 00] = (i * vertexCountInHexahedron) + 0;
                indexes[i * (triangleStrip + 1) + 01] = (i * vertexCountInHexahedron) + 2;
                indexes[i * (triangleStrip + 1) + 02] = (i * vertexCountInHexahedron) + 4;
                indexes[i * (triangleStrip + 1) + 03] = (i * vertexCountInHexahedron) + 6;
                indexes[i * (triangleStrip + 1) + 04] = (i * vertexCountInHexahedron) + 7;
                indexes[i * (triangleStrip + 1) + 05] = (i * vertexCountInHexahedron) + 2;
                indexes[i * (triangleStrip + 1) + 06] = (i * vertexCountInHexahedron) + 3;
                indexes[i * (triangleStrip + 1) + 07] = (i * vertexCountInHexahedron) + 0;
                indexes[i * (triangleStrip + 1) + 08] = (i * vertexCountInHexahedron) + 1;
                indexes[i * (triangleStrip + 1) + 09] = (i * vertexCountInHexahedron) + 4;
                indexes[i * (triangleStrip + 1) + 10] = (i * vertexCountInHexahedron) + 5;
                indexes[i * (triangleStrip + 1) + 11] = (i * vertexCountInHexahedron) + 7;
                indexes[i * (triangleStrip + 1) + 12] = (i * vertexCountInHexahedron) + 1;
                indexes[i * (triangleStrip + 1) + 13] = (i * vertexCountInHexahedron) + 3;
                indexes[i * (triangleStrip + 1) + 14] = int.MaxValue;// 截断三角形带的索引值。
            }

            this.positions = positions;
            this.colors = colors;
            this.indexes = indexes;
        }

        #region IRenderable 成员

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            if (renderMode != RenderMode.Render) { return; }

            if (!preparedForRendering)
            {

            }
            

        }

        #endregion
    }
}
