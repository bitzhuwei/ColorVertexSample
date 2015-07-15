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
        /// 元素内的顶点数
        /// </summary>
        const int vertexCountInHexahedron = 8;
        /// <summary>
        /// 顶点的元素数
        /// </summary>
        const int elementCountInVertex = 3;
        /// <summary>
        /// 用三角形带画六面体，需要14个顶点（索引值）
        /// </summary>
        const int triangleStrip = 14;

        /// <summary>
        /// init vertex attributes.
        /// </summary>
        /// <param name="gl"></param>
        unsafe private void InitVertexAttributes(OpenGL gl)
        {
            //UnmanagedArray positionArrayWrapper;
            //UnmanagedArray colorArrayWrapper;
            //UnmanagedArray indexeArrayWrapper;

            InitVAO(gl, positions, colors, this.indexArray);
        }

        /// <summary>
        /// 初始化VAO。
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="positions"></param>
        /// <param name="colors"></param>
        /// <param name="indexArray"></param>
        unsafe private void InitVAO(OpenGL gl, float[] positions, float[] colors, UnmanagedArray indexArray)
        {
            {
                indexDataBuffer = new IndexBuffer();
                indexDataBuffer.Create(gl);
                indexDataBuffer.Bind(gl);
                //indexDataBuffer.SetData(gl, indexArray);
                gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.byteLength, indexArray.pointer, OpenGL.GL_STATIC_DRAW);
                //Marshal.FreeHGlobal(p);
            }

            {
                //  Create the vertex array object.
                vertexBufferArray = new VertexBufferArray();
                vertexBufferArray.Create(gl);
                vertexBufferArray.Bind(gl);

                //  Create a vertex buffer for the vertex data.
                var vertexDataBuffer = new VertexBuffer();
                vertexDataBuffer.Create(gl);
                vertexDataBuffer.Bind(gl);
                // TODO: set data
                vertexDataBuffer.SetData(gl, 0, positions, false, 3);

                //  Now do the same for the colour data.
                var colourDataBuffer = new VertexBuffer();
                colourDataBuffer.Create(gl);
                colourDataBuffer.Bind(gl);
                // TODO: set data
                colourDataBuffer.SetData(gl, 1, colors, false, 3);

                //  Unbind the vertex array, we've finished specifying data for it.
                vertexBufferArray.Unbind(gl);
            }
        }

        /// <summary>
        /// 初始化各项属性数组。
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="colors"></param>
        /// <param name="indexes"></param>
        private void InitArrays(out float[] positions, out float[] colors, out UnmanagedArray indexes)
        {
            int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron * elementCountInVertex);

            // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexLength = (int)(source.DimenSize * (triangleStrip + 1));

            // 稍后将用InPtr代替float[]
            positions = new float[arrayLength];
            colors = new float[arrayLength];
            indexes = new UnmanagedArray(indexLength * sizeof(ushort)); //new ushort[indexLength];
            //var x = new UnmanagedArray(indexLength*sizeof(ushort), ushort);
            //indexesWrapper = new IntPtrWrapper();
            //indexesWrapper.pointer = Marshal.AllocHGlobal(indexLength * sizeof(uint));
            //indexesWrapper.length = indexLength * sizeof(uint);

            uint gridderElementIndex = 0;
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

                        // 顺便处理boundingbox.
                        this.boundingBox.Extend(vertex);
                    }
                }
                // 计算颜色信息。
                {
                    GLColor color = hexahedron.color;
                    for (int vertexIndex = 0; vertexIndex < vertexCountInHexahedron; vertexIndex++)
                    {
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 0] = color.R;
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 1] = color.G;
                        colors[gridderElementIndex + vertexIndex * elementCountInVertex + 2] = color.B;
                    }
                }

                gridderElementIndex += vertexCountInHexahedron * elementCountInVertex;
            }

            // 计算索引信息。
            //for (int i = 0; i < indexLength / (triangleStrip + 1); i++)
            //{
            //    // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
            //    indexes[i * (triangleStrip + 1) + 00] = (ushort)((i * vertexCountInHexahedron) + 0);
            //    indexes[i * (triangleStrip + 1) + 01] = (ushort)((i * vertexCountInHexahedron) + 2);
            //    indexes[i * (triangleStrip + 1) + 02] = (ushort)((i * vertexCountInHexahedron) + 4);
            //    indexes[i * (triangleStrip + 1) + 03] = (ushort)((i * vertexCountInHexahedron) + 6);
            //    indexes[i * (triangleStrip + 1) + 04] = (ushort)((i * vertexCountInHexahedron) + 7);
            //    indexes[i * (triangleStrip + 1) + 05] = (ushort)((i * vertexCountInHexahedron) + 2);
            //    indexes[i * (triangleStrip + 1) + 06] = (ushort)((i * vertexCountInHexahedron) + 3);
            //    indexes[i * (triangleStrip + 1) + 07] = (ushort)((i * vertexCountInHexahedron) + 0);
            //    indexes[i * (triangleStrip + 1) + 08] = (ushort)((i * vertexCountInHexahedron) + 1);
            //    indexes[i * (triangleStrip + 1) + 09] = (ushort)((i * vertexCountInHexahedron) + 4);
            //    indexes[i * (triangleStrip + 1) + 10] = (ushort)((i * vertexCountInHexahedron) + 5);
            //    indexes[i * (triangleStrip + 1) + 11] = (ushort)((i * vertexCountInHexahedron) + 7);
            //    indexes[i * (triangleStrip + 1) + 12] = (ushort)((i * vertexCountInHexahedron) + 1);
            //    indexes[i * (triangleStrip + 1) + 13] = (ushort)((i * vertexCountInHexahedron) + 3);
            //    indexes[i * (triangleStrip + 1) + 14] = ushort.MaxValue;// 截断三角形带的索引值。
            //}
            InitIndexArray(indexes);

            //this.positions = positions;
            //this.colors = colors;
            //this.indexes = indexes;
            //this.indexesWrapper = indexesWrapper;
            //this.indexesWrapper
        }

        private unsafe void InitIndexArray(UnmanagedArray indexArray)
        {
            //ushort* indexes = (ushort*)(indexArray.pointer);
            ushort* indexes = (ushort*)indexArray.pointer.ToPointer();

            // 计算索引信息。
            int elementCount = indexArray.byteLength / sizeof(ushort);
            for (int i = 0; i < elementCount / ((triangleStrip + 1)); i++)
            {
                // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
                indexes[i * (triangleStrip + 1) + 00] = (ushort)((i * vertexCountInHexahedron) + 0);
                indexes[i * (triangleStrip + 1) + 01] = (ushort)((i * vertexCountInHexahedron) + 2);
                indexes[i * (triangleStrip + 1) + 02] = (ushort)((i * vertexCountInHexahedron) + 4);
                indexes[i * (triangleStrip + 1) + 03] = (ushort)((i * vertexCountInHexahedron) + 6);
                indexes[i * (triangleStrip + 1) + 04] = (ushort)((i * vertexCountInHexahedron) + 7);
                indexes[i * (triangleStrip + 1) + 05] = (ushort)((i * vertexCountInHexahedron) + 2);
                indexes[i * (triangleStrip + 1) + 06] = (ushort)((i * vertexCountInHexahedron) + 3);
                indexes[i * (triangleStrip + 1) + 07] = (ushort)((i * vertexCountInHexahedron) + 0);
                indexes[i * (triangleStrip + 1) + 08] = (ushort)((i * vertexCountInHexahedron) + 1);
                indexes[i * (triangleStrip + 1) + 09] = (ushort)((i * vertexCountInHexahedron) + 4);
                indexes[i * (triangleStrip + 1) + 10] = (ushort)((i * vertexCountInHexahedron) + 5);
                indexes[i * (triangleStrip + 1) + 11] = (ushort)((i * vertexCountInHexahedron) + 7);
                indexes[i * (triangleStrip + 1) + 12] = (ushort)((i * vertexCountInHexahedron) + 1);
                indexes[i * (triangleStrip + 1) + 13] = (ushort)((i * vertexCountInHexahedron) + 3);
                indexes[i * (triangleStrip + 1) + 14] = ushort.MaxValue;// 截断三角形带的索引值。
            }
        }

        //private unsafe void InitIndexArray(UnmanagedArray indexArray)
        //{
        //    uint* indexes = (uint*)(indexArray.pointer);

        //    // 计算索引信息。
        //int elementCount = indexArray.byteLength / sizeof(uint);
            //for (int i = 0; i < elementCount / ((triangleStrip + 1)); i++)
        //    {
        //        // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
        //        indexes[i * (triangleStrip + 1) + 00] = (uint)((i * vertexCountInHexahedron) + 0);
        //        indexes[i * (triangleStrip + 1) + 01] = (uint)((i * vertexCountInHexahedron) + 2);
        //        indexes[i * (triangleStrip + 1) + 02] = (uint)((i * vertexCountInHexahedron) + 4);
        //        indexes[i * (triangleStrip + 1) + 03] = (uint)((i * vertexCountInHexahedron) + 6);
        //        indexes[i * (triangleStrip + 1) + 04] = (uint)((i * vertexCountInHexahedron) + 7);
        //        indexes[i * (triangleStrip + 1) + 05] = (uint)((i * vertexCountInHexahedron) + 2);
        //        indexes[i * (triangleStrip + 1) + 06] = (uint)((i * vertexCountInHexahedron) + 3);
        //        indexes[i * (triangleStrip + 1) + 07] = (uint)((i * vertexCountInHexahedron) + 0);
        //        indexes[i * (triangleStrip + 1) + 08] = (uint)((i * vertexCountInHexahedron) + 1);
        //        indexes[i * (triangleStrip + 1) + 09] = (uint)((i * vertexCountInHexahedron) + 4);
        //        indexes[i * (triangleStrip + 1) + 10] = (uint)((i * vertexCountInHexahedron) + 5);
        //        indexes[i * (triangleStrip + 1) + 11] = (uint)((i * vertexCountInHexahedron) + 7);
        //        indexes[i * (triangleStrip + 1) + 12] = (uint)((i * vertexCountInHexahedron) + 1);
        //        indexes[i * (triangleStrip + 1) + 13] = (uint)((i * vertexCountInHexahedron) + 3);
        //        indexes[i * (triangleStrip + 1) + 14] = uint.MaxValue;// 截断三角形带的索引值。
        //    }
        //}
    }
}
