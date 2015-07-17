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
        protected override void InitVertexArrayBufferObject(SharpGL.OpenGL gl, out uint[] vao)
        {
            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);

            //  Create a vertex buffer for the vertex data.
            {
                UnmanagedArray positionArray = InitPositionArray();

                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Pointer, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexPosition);

                //var positionBuffer = new VertexBuffer();
                //positionBuffer.Create(gl);
                //positionBuffer.Bind(gl);
                ////vertexDataBuffer.SetData(gl, 0, positions, false, 3);
                //positionBuffer.SetData(gl, 0, positionArray.ByteLength, positionArray.Pointer, false, 3, OpenGL.GL_STATIC_DRAW);

                positionArray.Dispose();
            }

            //  Now do the same for the colour data.
            {
                UnmanagedArray colorArray = InitColorArray();

                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Pointer, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexColour, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexColour);

                colorArray.Dispose();
            }

            // Now do the same for the index's visual signal data.
            {
                UnmanagedArray indexVisualArray = InitIndexVisualArray();

                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, indexVisualArray.ByteLength, indexVisualArray.Pointer, OpenGL.GL_DYNAMIC_READ);
                gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_BYTE, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexVisible);

                indexVisualArray.Dispose();

                this.indexVisualBuffer = ids[0];
            }

            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }

        unsafe private UnmanagedArray InitIndexVisualArray()
        {
            // 用三角形带画六面体，需要14个顶点（索引值），为切断三角形带，还需要附加一个。
            int indexCount = (int)(source.DimenSize * (triangleStrip + 1));

            UnmanagedArray indexArray = new UnmanagedArray(indexCount, sizeof(byte));
            byte* indexes = (byte*)indexArray.Pointer.ToPointer();

            bool signal = true;

            // 计算索引信息。
            for (int i = 0; i < indexArray.ElementCount / ((triangleStrip + 1)); i++)
            {
                // 索引值的指定必须配合hexahedron.GetVertexes()的次序。
                indexes[i * (triangleStrip + 1) + 00] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 01] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 02] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 03] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 04] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 05] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 06] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 07] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 08] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 09] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 10] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 11] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 12] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 13] = (byte)(signal ? 1 : 0);
                indexes[i * (triangleStrip + 1) + 14] = (byte)(signal ? 1 : 0);

                signal = !signal;
            }

            return indexArray;
        }

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

    }
}
