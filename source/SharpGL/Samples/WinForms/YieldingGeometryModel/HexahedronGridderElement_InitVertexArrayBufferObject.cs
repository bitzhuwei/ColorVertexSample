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
            gl.BindVertexArray(0);
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
