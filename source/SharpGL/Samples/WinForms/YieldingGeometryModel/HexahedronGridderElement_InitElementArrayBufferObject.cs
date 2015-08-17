using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class HexahedronGridderElement
    {

        //protected void InitElementArrayBufferObject(OpenGL gl, out uint[] ebo, out uint mode, out int indexArrayElementCount)
        //{
        //    {
        //        UnmanagedArray<uint> indexArray = InitHexahedronGridderIndex();
        //        ebo = new uint[1];
        //        gl.GenBuffers(1, ebo);
        //        gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ebo[0]);
        //        gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Header, OpenGL.GL_STATIC_DRAW);

        //        mode = OpenGL.GL_TRIANGLE_STRIP;
        //        indexArrayElementCount = indexArray.Count;

        //        indexArray.Dispose();
        //    }

        //}

        private UnmanagedArray<uint> InitLineGridderIndex()
        {
            const int lineStrip = 24;
            // 用三角形带画六面体的线框，需要24个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexCount = (int)(source.DimenSize * (lineStrip ));

            UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(indexCount); //new UnmanagedArray(indexCount, sizeof(uint));
            //uint* indexes = (uint*)indexArray.Header.ToPointer();

            // 计算索引信息。
            for (int i = 0; i < indexArray.Length / ((lineStrip)); i++)
            {
                // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
                indexArray[i * (lineStrip ) + 00] = (uint)((i * vertexCountInHexahedron) + 0);
                indexArray[i * (lineStrip ) + 01] = (uint)((i * vertexCountInHexahedron) + 1);
                indexArray[i * (lineStrip ) + 02] = (uint)((i * vertexCountInHexahedron) + 1);
                indexArray[i * (lineStrip ) + 03] = (uint)((i * vertexCountInHexahedron) + 3);
                indexArray[i * (lineStrip ) + 04] = (uint)((i * vertexCountInHexahedron) + 3);
                indexArray[i * (lineStrip ) + 05] = (uint)((i * vertexCountInHexahedron) + 2);
                indexArray[i * (lineStrip ) + 06] = (uint)((i * vertexCountInHexahedron) + 2);
                indexArray[i * (lineStrip ) + 07] = (uint)((i * vertexCountInHexahedron) + 0);
                indexArray[i * (lineStrip ) + 08] = (uint)((i * vertexCountInHexahedron) + 4);
                indexArray[i * (lineStrip ) + 09] = (uint)((i * vertexCountInHexahedron) + 5);
                indexArray[i * (lineStrip ) + 10] = (uint)((i * vertexCountInHexahedron) + 5);
                indexArray[i * (lineStrip ) + 11] = (uint)((i * vertexCountInHexahedron) + 7);
                indexArray[i * (lineStrip ) + 12] = (uint)((i * vertexCountInHexahedron) + 7);
                indexArray[i * (lineStrip ) + 13] = (uint)((i * vertexCountInHexahedron) + 6);
                indexArray[i * (lineStrip ) + 14] = (uint)((i * vertexCountInHexahedron) + 6);
                indexArray[i * (lineStrip ) + 15] = (uint)((i * vertexCountInHexahedron) + 4);
                indexArray[i * (lineStrip ) + 16] = (uint)((i * vertexCountInHexahedron) + 0);
                indexArray[i * (lineStrip ) + 17] = (uint)((i * vertexCountInHexahedron) + 4);
                indexArray[i * (lineStrip ) + 18] = (uint)((i * vertexCountInHexahedron) + 1);
                indexArray[i * (lineStrip ) + 19] = (uint)((i * vertexCountInHexahedron) + 5);
                indexArray[i * (lineStrip ) + 20] = (uint)((i * vertexCountInHexahedron) + 3);
                indexArray[i * (lineStrip ) + 21] = (uint)((i * vertexCountInHexahedron) + 7);
                indexArray[i * (lineStrip ) + 22] = (uint)((i * vertexCountInHexahedron) + 2);
                indexArray[i * (lineStrip ) + 23] = (uint)((i * vertexCountInHexahedron) + 6);
                //indexArray[i * (lineStrip ) + 24] = uint.MaxValue;// 截断三角形带的索引值。
            }

            return indexArray;
        }

        //unsafe private UnmanagedArray<uint> InitHexahedronGridderIndex()
        //{
        //    // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
        //    int indexCount = (int)(source.DimenSize * (triangleStrip + 1));

        //    UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(indexCount); //new UnmanagedArray(indexCount, sizeof(uint));
        //    //uint* indexes = (uint*)indexArray.Header.ToPointer();

        //    // 计算索引信息。
        //    for (int i = 0; i < indexArray.Count / ((triangleStrip + 1)); i++)
        //    {
        //        // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
        //        indexArray[i * (triangleStrip + 1) + 00] = (uint)((i * vertexCountInHexahedron) + 0);
        //        //indexes[i * (triangleStrip + 1) + 00] = (uint)((i * vertexCountInHexahedron) + 0);
        //        indexArray[i * (triangleStrip + 1) + 01] = (uint)((i * vertexCountInHexahedron) + 2);
        //        indexArray[i * (triangleStrip + 1) + 02] = (uint)((i * vertexCountInHexahedron) + 4);
        //        indexArray[i * (triangleStrip + 1) + 03] = (uint)((i * vertexCountInHexahedron) + 6);
        //        indexArray[i * (triangleStrip + 1) + 04] = (uint)((i * vertexCountInHexahedron) + 7);
        //        indexArray[i * (triangleStrip + 1) + 05] = (uint)((i * vertexCountInHexahedron) + 2);
        //        indexArray[i * (triangleStrip + 1) + 06] = (uint)((i * vertexCountInHexahedron) + 3);
        //        indexArray[i * (triangleStrip + 1) + 07] = (uint)((i * vertexCountInHexahedron) + 0);
        //        indexArray[i * (triangleStrip + 1) + 08] = (uint)((i * vertexCountInHexahedron) + 1);
        //        indexArray[i * (triangleStrip + 1) + 09] = (uint)((i * vertexCountInHexahedron) + 4);
        //        indexArray[i * (triangleStrip + 1) + 10] = (uint)((i * vertexCountInHexahedron) + 5);
        //        indexArray[i * (triangleStrip + 1) + 11] = (uint)((i * vertexCountInHexahedron) + 7);
        //        indexArray[i * (triangleStrip + 1) + 12] = (uint)((i * vertexCountInHexahedron) + 1);
        //        indexArray[i * (triangleStrip + 1) + 13] = (uint)((i * vertexCountInHexahedron) + 3);
        //        indexArray[i * (triangleStrip + 1) + 14] = uint.MaxValue;// 截断三角形带的索引值。
        //    }

        //    return indexArray;
        //}

    }
}
