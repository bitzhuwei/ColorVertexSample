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

        protected override void InitElementArrayBufferObject(OpenGL gl, out uint[] ebo, out uint mode, out int indexArrayElementCount)
        {
            UnmanagedArray indexArray = InitIndexArray();
            ebo = new uint[1];
            gl.GenBuffers(1, ebo);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ebo[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Header, OpenGL.GL_STATIC_DRAW);

            mode = OpenGL.GL_TRIANGLE_STRIP;
            indexArrayElementCount = indexArray.Count;

            indexArray.Dispose();
        }

        unsafe private UnmanagedArray InitIndexArray()
        {
            // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexCount = (int)(source.DimenSize * (triangleStrip + 1));

            UnmanagedArray indexArray = new UIntArray(indexCount); //new UnmanagedArray(indexCount, sizeof(uint));
            uint* indexes = (uint*)indexArray.Header.ToPointer();

            // 计算索引信息。
            for (int i = 0; i < indexArray.Count / ((triangleStrip + 1)); i++)
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
