using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Utility;
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
        private int vertexCount;
        private int[] firsts;
        private int[] counts;

        private void InitVertexes(OpenGL gl, UnmanagedArray<Vertex> vertexes, UnmanagedArray<ColorF> colorArray, UnmanagedArray<float> visibles)
        {
            uint[] vao = new uint[1];
            gl.GenVertexArrays(vao.Length, vao);
            gl.BindVertexArray(vao[0]);

            this.vertexArrayObject = vao[0];
            uint[] vboVertex = new uint[1];
            gl.GenBuffers(vboVertex.Length, vboVertex);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVertex[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, vertexes.ByteLength, vertexes.Header, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(ATTRIB_INDEX_POSITION, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_POSITION);
            this.vertexsBufferObject = vboVertex[0];

            this.vertexCount = vertexes.Length;
            this.firsts = new int[vertexes.Length / 8];
            this.counts = new int[vertexes.Length / 8];
            //for (int i = 0; i < this.firsts.Length; i++)
            //{
            //    firsts[i] = i * 8;
            //    if (firsts[i] > this.vertexCount)
            //    {
            //        Console.WriteLine("adsf");
            //    }
            //    this.counts[i] = 8;
            //}

            uint[] vboColor = new uint[1];
            gl.GenBuffers(vboColor.Length, vboColor);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboColor[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_DYNAMIC_DRAW);
            gl.VertexAttribPointer(ATTRIB_INDEX_COLOUR, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_COLOUR);
            this.colorsBufferObject = vboColor[0];

            uint[] vboVisual = new uint[1];
            gl.GenBuffers(vboVisual.Length, vboVisual);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVisual[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, visibles.ByteLength, visibles.Header, OpenGL.GL_DYNAMIC_READ);
            gl.VertexAttribPointer(ATTRIB_INDEX_VISIBLE, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(ATTRIB_INDEX_VISIBLE);
            this.visiblesBufferObject = vboVisual[0];

            gl.BindVertexArray(0);
        }

        public void UpdateColorBuffer(OpenGL gl, UnmanagedArray<ColorF> colors, UnmanagedArray<float> visibles)
        {
            if (this.visiblesBufferObject == 0 || this.colorsBufferObject == 0)
                return;
            gl.MakeCurrent();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.visiblesBufferObject);
            IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorsBufferObject);
            IntPtr destColors = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destColors, colors.Header, (uint)colors.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

        }



        //protected override void InitVertexArrayBufferObject(SharpGL.OpenGL gl, out uint[] vao)
        //{
        //    vao = new uint[1];
        //    gl.GenVertexArrays(1, vao);
        //    gl.BindVertexArray(vao[0]);

        //    //  Create a vertex buffer for the vertex data.
        //    {
        //        UnmanagedArray<float> positionArray = InitPositionArray();

        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
        //        gl.VertexAttribPointer(attributeIndexPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexPosition);

        //        positionArray.Dispose();
        //    }

        //    //  Now do the same for the colour data.
        //    {
        //        UnmanagedArray<float> colorArray = InitColorArray();

        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);
        //        gl.VertexAttribPointer(attributeIndexColour, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexColour);

        //        colorArray.Dispose();
        //    }

        //    // Now do the same for the index's visual signal data.
        //    {
        //        UnmanagedArray<float> visualArray = InitVisualArray();

        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, visualArray.ByteLength, visualArray.Header, OpenGL.GL_DYNAMIC_READ);
        //        gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexVisible);

        //        visualArray.Dispose();

        //        this.visualBuffer = ids[0];
        //    }

        //    //  Unbind the vertex array, we've finished specifying data for it.
        //    gl.BindVertexArray(0);
        //}

        //unsafe private UnmanagedArray<float> InitVisualArray()
        //{
        //    int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron);

        //    UnmanagedArray<float> visualArray = new UnmanagedArray<float>(arrayLength);// new UnmanagedArray(arrayLength, sizeof(float));
        //    float* visuals = (float*)visualArray.Header.ToPointer();

        //    bool signal = true;

        //    uint gridderElementIndex = 0;
        //    foreach (Hexahedron hexahedron in source.GetGridderCells())
        //    {
        //        // 计算位置信息。
        //        int vertexIndex = 0;
        //        foreach (Vertex vertex in hexahedron.GetVertexes())
        //        {
        //            visuals[gridderElementIndex + (vertexIndex++)] = signal ? 1 : 0;
        //        }

        //        // TODO: 此signal应由具体业务提供。
        //        signal = (random.NextDouble() > 0.8);

        //        gridderElementIndex += vertexCountInHexahedron;
        //    }

        //    return visualArray;
        //}

        //Random random = new Random();

        //unsafe private UnmanagedArray<float> InitColorArray()
        //{
        //    int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron * componentCountInVertex);

        //    UnmanagedArray<float> colorArray = new UnmanagedArray<float>(arrayLength);// new UnmanagedArray(arrayLength, sizeof(float));
        //    float* colors = (float*)colorArray.Header.ToPointer();

        //    uint gridderElementIndex = 0;
        //    foreach (Hexahedron hexahedron in source.GetGridderCells())
        //    {
        //        // 计算颜色信息。
        //        GLColor color = hexahedron.color;
        //        for (int vertexIndex = 0; vertexIndex < vertexCountInHexahedron; vertexIndex++)
        //        {
        //            colors[gridderElementIndex + vertexIndex * componentCountInVertex + 0] = color.R;
        //            colors[gridderElementIndex + vertexIndex * componentCountInVertex + 1] = color.G;
        //            colors[gridderElementIndex + vertexIndex * componentCountInVertex + 2] = color.B;
        //        }

        //        gridderElementIndex += vertexCountInHexahedron * componentCountInVertex;
        //    }

        //    return colorArray;
        //}

        //unsafe private UnmanagedArray<float> InitPositionArray()
        //{
        //    int arrayLength = (int)(source.DimenSize * vertexCountInHexahedron * componentCountInVertex);

        //    UnmanagedArray<float> positionArray = new UnmanagedArray<float>(arrayLength);// new UnmanagedArray(arrayLength, sizeof(float));
        //    float* positions = (float*)positionArray.Header.ToPointer();

        //    uint gridderElementIndex = 0;
        //    foreach (Hexahedron hexahedron in source.GetGridderCells())
        //    {
        //        // 计算位置信息。
        //        int vertexIndex = 0;
        //        foreach (Vertex vertex in hexahedron.GetVertexes())
        //        {
        //            positions[gridderElementIndex + (vertexIndex++)] = vertex.X;
        //            positions[gridderElementIndex + (vertexIndex++)] = vertex.Y;
        //            positions[gridderElementIndex + (vertexIndex++)] = vertex.Z;

        //            // 顺便处理boundingbox.
        //            //this.boundingBox.Extend(vertex);
        //        }

        //        gridderElementIndex += vertexCountInHexahedron * componentCountInVertex;
        //    }

        //    return positionArray;
        //}

    }
}
