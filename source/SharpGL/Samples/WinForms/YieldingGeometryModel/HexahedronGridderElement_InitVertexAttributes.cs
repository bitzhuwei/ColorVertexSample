using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class HexahedronGridderElement : SceneElement, IRenderable
    {

        /// <summary>
        /// 元素内的顶点数。（8）
        /// </summary>
        const int vertexCountInHexahedron = 8;
        /// <summary>
        /// 顶点的分量数。（3）
        /// </summary>
        const int componentCountInVertex = 3;
        /// <summary>
        /// 用三角形带画六面体，需要14个顶点（索引值）
        /// </summary>
        const int triangleStrip = 14;

        /// <summary>
        /// 初始化VAO并立即释放内存。
        /// </summary>
        /// <param name="gl"></param>
        private void InitializeVAO(OpenGL gl)
        {
            //  Create the vertex array object.
            vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            //  Create a vertex buffer for the vertex data.
            UnmanagedArray positionArray = InitPositionArray();
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            //vertexDataBuffer.SetData(gl, 0, positions, false, 3);
            vertexDataBuffer.SetData(gl, 0, positionArray.ByteLength, positionArray.Pointer, false, 3, OpenGL.GL_STATIC_DRAW);
            positionArray.Dispose();

            //  Now do the same for the colour data.
            UnmanagedArray colorArray = InitColorArray();
            var colourDataBuffer = new VertexBuffer();
            colourDataBuffer.Create(gl);
            colourDataBuffer.Bind(gl);
            //colourDataBuffer.SetData(gl, 1, colors, false, 3);
            colourDataBuffer.SetData(gl, 1, colorArray.ByteLength, colorArray.Pointer, false, 3, OpenGL.GL_STATIC_DRAW);
            colorArray.Dispose();

            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);

        }

        /// <summary>
        /// 初始化索引并立即释放内存。
        /// </summary>
        /// <param name="gl"></param>
        private void InitializeIndexArray(OpenGL gl)
        {
            UnmanagedArray indexArray = InitIndexArray();
            IndexBuffer indexDataBuffer = new IndexBuffer();
            indexDataBuffer.Create(gl);
            indexDataBuffer.Bind(gl);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Pointer, OpenGL.GL_STATIC_DRAW);

            this.indexArrayElementCount = indexArray.ElementCount;
            this.indexDataBuffer = indexDataBuffer;

            indexArray.Dispose();
        }

        /// <summary>
        /// 计算color数组。
        /// </summary>
        /// <returns></returns>
        unsafe private UnmanagedArray InitColorArray()
        {
            int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron * componentCountInVertex);

            UnmanagedArray colorArray = new UnmanagedArray(arrayLength, sizeof(float));
            float* colors = (float*)colorArray.Pointer.ToPointer();

            uint gridderElementIndex = 0;
            foreach (Hexahedron hexahedron in source.GetGridderCells())
            {
                // 计算颜色信息。
                GLColor color = hexahedron.color;
                for (int vertexIndex = 0; vertexIndex < vertexCountInHexahedron; vertexIndex++)
                {
                    colors[gridderElementIndex + vertexIndex * componentCountInVertex + 0] = color.R;
                    colors[gridderElementIndex + vertexIndex * componentCountInVertex + 1] = color.G;
                    colors[gridderElementIndex + vertexIndex * componentCountInVertex + 2] = color.B;
                }

                gridderElementIndex += vertexCountInHexahedron * componentCountInVertex;
            }

            return colorArray;
        }

        /// <summary>
        /// 计算position数组。
        /// </summary>
        /// <returns></returns>
        unsafe private UnmanagedArray InitPositionArray()
        {
            int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron * componentCountInVertex);

            // 稍后将用InPtr代替float[]
            UnmanagedArray positionArray = new UnmanagedArray(arrayLength, sizeof(float));
            float* positions = (float*)positionArray.Pointer.ToPointer();

            uint gridderElementIndex = 0;
            foreach (Hexahedron hexahedron in source.GetGridderCells())
            {
                // 计算位置信息。
                int vertexIndex = 0;
                foreach (Vertex vertex in hexahedron.GetVertexes())
                {
                    positions[gridderElementIndex + (vertexIndex++)] = vertex.X;
                    positions[gridderElementIndex + (vertexIndex++)] = vertex.Y;
                    positions[gridderElementIndex + (vertexIndex++)] = vertex.Z;

                    // 顺便处理boundingbox.
                    this.boundingBox.Extend(vertex);
                }

                gridderElementIndex += vertexCountInHexahedron * componentCountInVertex;
            }

            return positionArray;
        }

        /// <summary>
        /// 计算index数组。
        /// </summary>
        /// <param name="indexArray"></param>
        private unsafe UnmanagedArray InitIndexArray()
        {
            // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexCount = (int)(source.DimenSize * (triangleStrip + 1));

            UnmanagedArray indexArray = new UnmanagedArray(indexCount, sizeof(uint));
            uint* indexes = (uint*)indexArray.Pointer.ToPointer();

            // 计算索引信息。
            for (int i = 0; i < indexArray.ElementCount / ((triangleStrip + 1)); i++)
            {
                // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
                indexes[i * (triangleStrip + 1) + 00] = (uint)((i * vertexCountInHexahedron) + 0);
                indexes[i * (triangleStrip + 1) + 01] = (uint)((i * vertexCountInHexahedron) + 2);
                indexes[i * (triangleStrip + 1) + 02] = (uint)((i * vertexCountInHexahedron) + 4);
                indexes[i * (triangleStrip + 1) + 03] = (uint)((i * vertexCountInHexahedron) + 6);
                indexes[i * (triangleStrip + 1) + 04] = (uint)((i * vertexCountInHexahedron) + 7);
                indexes[i * (triangleStrip + 1) + 05] = (uint)((i * vertexCountInHexahedron) + 2);
                indexes[i * (triangleStrip + 1) + 06] = (uint)((i * vertexCountInHexahedron) + 3);
                indexes[i * (triangleStrip + 1) + 07] = (uint)((i * vertexCountInHexahedron) + 0);
                indexes[i * (triangleStrip + 1) + 08] = (uint)((i * vertexCountInHexahedron) + 1);
                indexes[i * (triangleStrip + 1) + 09] = (uint)((i * vertexCountInHexahedron) + 4);
                indexes[i * (triangleStrip + 1) + 10] = (uint)((i * vertexCountInHexahedron) + 5);
                indexes[i * (triangleStrip + 1) + 11] = (uint)((i * vertexCountInHexahedron) + 7);
                indexes[i * (triangleStrip + 1) + 12] = (uint)((i * vertexCountInHexahedron) + 1);
                indexes[i * (triangleStrip + 1) + 13] = (uint)((i * vertexCountInHexahedron) + 3);
                indexes[i * (triangleStrip + 1) + 14] = uint.MaxValue;// 截断三角形带的索引值。
            }

            return indexArray;
        }
    }
}
