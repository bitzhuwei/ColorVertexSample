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
    public partial class DynamicUnStructuredGridderElement
    {

        private uint[] vertexArrayObject = new uint[2];
        private uint[] tetrasPositionBufferObject = new uint[1];
        private uint[] tetrasColorBufferObject = new uint[1];
        private uint[] tetrasIndexBufferObject = new uint[1];

        private uint[] fractionsPositionBufferObject = new uint[1];
        private uint[] fractionsColorBufferObject = new uint[1];

        private int tetrasIndexBufferObjectCount;
        private int fractionsBufferObjectCount;

        /// <summary>
        /// 2 or 3
        /// </summary>
        int vertexCountPerFraction;
        /// <summary>
        /// 3 or 6
        /// </summary>
        int vertexCountPerTetra;

        private void InitVAO(OpenGL gl)
        {
            {
                //int[][] tetras = this.source.Tetras;
                int[][] tetras = this.source.Elements;
                int length = tetras.Length;
                if (this.source.ElementFormat == 3)
                {
                    vertexCountPerTetra = this.source.ElementFormat;
                    this.tetrasIndexBufferObjectCount = length * (vertexCountPerTetra);
                }
                else if (this.source.ElementFormat == 4)
                {
                    vertexCountPerTetra = 6;
                    this.tetrasIndexBufferObjectCount = length * (vertexCountPerTetra + 1);
                }
            }
            {
                //int[][] fractions = this.source.Fractions;
                int[][] fractions = this.source.Fractures;
                int length = fractions.Length;
                vertexCountPerFraction = this.source.FractureFormat;
                this.fractionsBufferObjectCount = length * vertexCountPerFraction;
            }

            // 三角形或四面体（四面体需要index buffer object）
            PrepareTetrasPositionBufferObject(gl);
            PrepareTetrasColorBufferObject(gl);
            PrepareTetrasIndexBufferObject(gl);

            // 线段或三角形
            PrepareFractionsPositionBufferObject(gl);
            PrepareFractionsColorBufferObject(gl);

            gl.GenVertexArrays(this.vertexArrayObject.Length, this.vertexArrayObject);

            InitFractions(gl);

            InitTetras(gl);

        }

        private unsafe void PrepareFractionsColorBufferObject(OpenGL gl)
        {
            //Vertex[] coords = source.Coords;
            //int[][] triangles = this.source.Triangles;
            //int[][] fractions = this.source.Fractions;
            int[][] fractions = this.source.Fractures;
            int length = fractions.Length;
            UnmanagedArray<vec4> colorArray = new UnmanagedArray<vec4>(length * vertexCountPerFraction);
            vec4* header = (vec4*)colorArray.FirstElement();
            //vec4* last = (vec4*)colorArray.LastElement();
            //vec4* tail = (vec4*)colorArray.TailAddress();
            vec4* currentPosition = header;

            for (int i = 0; i < length; i++)
            {
                vec4 color = new vec4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                for (int vertexIndex = 0; vertexIndex < vertexCountPerFraction; vertexIndex++)
                {
                    *(currentPosition + vertexIndex) = color;
                }
                currentPosition += vertexCountPerFraction;
            }

            gl.GenBuffers(1, this.fractionsColorBufferObject);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.fractionsColorBufferObject[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);

            colorArray.Dispose();
        }

        private unsafe void PrepareFractionsPositionBufferObject(OpenGL gl)
        {
            //Vertex[] coords = source.Coords;
            Vertex[] coords = source.Nodes;
            //int[][] triangles = this.source.Triangles;
            //int[][] fractions = this.source.Fractions;
            int[][] fractures = this.source.Fractures;
            int length = fractures.Length;
            UnmanagedArray<vec3> positionArray = new UnmanagedArray<vec3>(length * vertexCountPerFraction);
            vec3* first = (vec3*)positionArray.FirstElement();
            //vec3* last = (vec3*)positionArray.LastElement();
            //vec3* tail = (vec3*)positionArray.TailAddress();
            vec3* currentPosition = first;

            if (vertexCountPerFraction == 2)// 线段 Lines
            {
                for (int i = 0; i < length; i++)
                {
                    var fracture = fractures[i];
                    for (int j = 0; j < vertexCountPerFraction; j++)
                    {
                        int coordIndex = fracture[j];
                        Vertex vertex = coords[coordIndex];
                        *(currentPosition + j) = new vec3(vertex.X, vertex.Y, vertex.Z);
                    }
                    currentPosition += vertexCountPerFraction;
                }
            }
            else if (vertexCountPerFraction == 3)// 三角形 Triangles
            {
                for (int i = 0; i < length; i++)
                {
                    var fracture = fractures[i];
                    for (int j = 0; j < vertexCountPerFraction; j++)
                    {
                        int coordIndex = fracture[j];
                        Vertex vertex = coords[coordIndex];
                        *(currentPosition + j) = new vec3(vertex.X, vertex.Y, vertex.Z);
                    }
                    currentPosition += vertexCountPerFraction;
                }
            }

            gl.GenBuffers(1, this.fractionsPositionBufferObject);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.fractionsPositionBufferObject[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);

            positionArray.Dispose();
        }

        private unsafe void PrepareTetrasIndexBufferObject(OpenGL gl)
        {
            if (vertexCountPerTetra == 3)//三角形 triangles
            {
                // 三角形不需要索引
            }
            else if (vertexCountPerTetra == 6)//四面体 triangle_strip
            {
                //Vertex[] coords = source.Coords;
                Vertex[] coords = source.Nodes;
                //int[][] triangles = this.source.Triangles;
                //int[][] tetras = this.source.Tetras;
                int[][] tetras = this.source.Elements;
                int length = tetras.Length;
                UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(length * (vertexCountPerTetra + 1));
                uint* colorHeader = (uint*)indexArray.FirstElement();
                //uint* colorLast = (uint*)colorArray.LastElement();
                //uint* colorTail = (uint*)colorArray.TailAddress();
                uint* currentPosition = colorHeader;

                for (int i = 0; i < length; i++)
                {
                    for (int vertexIndex = 0; vertexIndex < vertexCountPerTetra; vertexIndex++)
                    {
                        *(currentPosition + vertexIndex) = (uint)(vertexIndex + i * vertexCountPerTetra);
                    }
                    *(currentPosition + vertexCountPerTetra) = uint.MaxValue;
                    currentPosition += (vertexCountPerTetra + 1);
                }
                gl.GenBuffers(1, this.tetrasIndexBufferObject);
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.tetrasIndexBufferObject[0]);
                gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Header, OpenGL.GL_STATIC_DRAW);

                indexArray.Dispose();
            }
        }

        private unsafe void PrepareTetrasColorBufferObject(OpenGL gl)
        {
            //Vertex[] coords = source.Coords;
            Vertex[] coords = source.Nodes;
            //int[][] triangles = this.source.Triangles;
            //int[][] tetras = this.source.Tetras;
            int[][] tetras = this.source.Elements;
            int length = tetras.Length;
            // http://www.cnblogs.com/bitzhuwei/gallery/image/159266.html
            //const int vertexCountPerTetra = 6;
            UnmanagedArray<vec4> colorArray = new UnmanagedArray<vec4>(length * vertexCountPerTetra);
            vec4* headPointer = (vec4*)colorArray.FirstElement();
            //vec4* lastPointer = (vec4*)colorArray.LastElement();
            //vec4* tailPointer = (vec4*)colorArray.TailAddress();
            vec4* currentPosition = headPointer;

            for (int i = 0; i < length; i++)
            {
                vec4 color = new vec4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                for (int vertexIndex = 0; vertexIndex < vertexCountPerTetra; vertexIndex++)
                {
                    *(currentPosition + vertexIndex) = color;
                }
                currentPosition += vertexCountPerTetra;
            }

            gl.GenBuffers(1, this.tetrasColorBufferObject);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.tetrasColorBufferObject[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);

            colorArray.Dispose();
        }

        private unsafe void PrepareTetrasPositionBufferObject(OpenGL gl)
        {
            //Vertex[] coords = source.Coords;
            Vertex[] coords = source.Nodes;
            //int[][] triangles = this.source.Triangles;
            //int[][] tetras = this.source.Tetras;
            int[][] tetras = this.source.Elements;
            int length = tetras.Length;
            // http://www.cnblogs.com/bitzhuwei/gallery/image/159266.html
            UnmanagedArray<vec3> positionArray = new UnmanagedArray<vec3>(length * vertexCountPerTetra);
            vec3* positionHeader = (vec3*)positionArray.FirstElement();
            //vec3* positionLast = (vec3*)positionArray.LastElement();
            //vec3* positionTail = (vec3*)positionArray.TailAddress();
            vec3* currentPosition = positionHeader;

            if (vertexCountPerTetra == 3)//三角形 triangles
            {
                for (int i = 0; i < length; i++)
                {
                    int[] tetra = tetras[i];
                    int vertexIndex = 0;
                    for (int j = 0; j < vertexCountPerTetra; j++)
                    {
                        int coordIndex = tetra[j] - 1;
                        Vertex vertex = coords[coordIndex];
                        *(currentPosition + vertexIndex) = new vec3(vertex.X, vertex.Y, vertex.Z);
                        vertexIndex++;
                    }
                    currentPosition += vertexCountPerTetra;
                }
            }
            else if (vertexCountPerTetra == 6)//四面体 triangle_strip
            {
                for (int i = 0; i < length; i++)
                {
                    // http://www.cnblogs.com/bitzhuwei/gallery/image/159266.html
                    int[] tetra = tetras[i];
                    int vertexIndex = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        int coordIndex = tetra[j] - 1;
                        Vertex coord = coords[coordIndex];
                        *(currentPosition + vertexIndex) = new vec3(coord.X, coord.Y, coord.Z);
                        vertexIndex++;
                    }
                    *(currentPosition + 4) = *(currentPosition + 0);
                    *(currentPosition + 5) = *(currentPosition + 1);
                    currentPosition += vertexCountPerTetra;
                }
            }

            gl.GenBuffers(1, this.tetrasPositionBufferObject);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.tetrasPositionBufferObject[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);

            positionArray.Dispose();
        }

        private void InitTetras(OpenGL gl)
        {
            gl.BindVertexArray(this.vertexArrayObject[1]);

            // positions
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.tetrasPositionBufferObject[0]);
                gl.VertexAttribPointer(this.in_PositionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_PositionLocation);
            }

            // colors
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.tetrasColorBufferObject[0]);
                gl.VertexAttribPointer(this.in_ColorLocation, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_ColorLocation);
            }

            // index
            if (this.vertexCountPerTetra == 3)// 三角形，不需要index
            {
                // nothing to do.
            }
            else if (this.vertexCountPerTetra == 6)// 四面体，需要index
            {
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.tetrasIndexBufferObject[0]);
            }

            gl.BindVertexArray(0);
        }

        private void InitFractions(OpenGL gl)//, UnmanagedArray<vec3> positionArray, UnmanagedArray<vec4> colorArray)
        {
            gl.BindVertexArray(this.vertexArrayObject[0]);

            // positions
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.fractionsPositionBufferObject[0]);
                gl.VertexAttribPointer(this.in_PositionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_PositionLocation);
            }

            // colors
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.fractionsColorBufferObject[0]);
                gl.VertexAttribPointer(this.in_ColorLocation, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_ColorLocation);
            }

            gl.BindVertexArray(0);
        }

        static Random random = new Random();
    }
}
