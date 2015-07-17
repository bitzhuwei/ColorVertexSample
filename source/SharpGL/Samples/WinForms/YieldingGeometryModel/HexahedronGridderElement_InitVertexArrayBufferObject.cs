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
                UnmanagedArray indexVisualArray = InitVisualArray();

                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, indexVisualArray.ByteLength, indexVisualArray.Pointer, OpenGL.GL_DYNAMIC_READ);
                gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexVisible);

                indexVisualArray.Dispose();

                this.visualBuffer = ids[0];
            }

            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }

        unsafe private UnmanagedArray InitVisualArray()
        {
            int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron);

            UnmanagedArray visualArray = new UnmanagedArray(arrayLength, sizeof(float));
            float* visuals = (float*)visualArray.Pointer.ToPointer();

            bool signal = true;

            uint gridderElementIndex = 0;
            foreach (Hexahedron hexahedron in source.GetGridderCells())
            {
                // 计算位置信息。
                int vertexIndex = 0;
                foreach (Vertex vertex in hexahedron.GetVertexes())
                {
                    visuals[gridderElementIndex + (vertexIndex++)] = signal ? 1 : 0;

                    // 顺便处理boundingbox.
                    this.boundingBox.Extend(vertex);
                }

                // TODO: 此signal应由具体业务提供。
                signal = (random.NextDouble() > 0.8);

                gridderElementIndex += vertexCountInHexahedron;
            }

            return visualArray;
        }

        Random random = new Random();

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
