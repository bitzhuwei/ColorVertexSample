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
    public partial class UnStructuredGridderElement
    {
        private int fractionsIndexCount;
        private int tetrasIndexCount;

        private void InitVAO(OpenGL gl)
        {
            PreparePositionBufferObject(gl);

            PrepareColorBufferObject(gl);

            PrepareTetrasIndexBufferObject(gl);

            PrepareFractionsIndexBufferObject(gl);

            gl.GenVertexArrays(this.vertexArrayObject.Length, this.vertexArrayObject);

            InitFractions(gl);

            InitTetras(gl);

        }

        private unsafe void PrepareFractionsIndexBufferObject(OpenGL gl)
        {
            var fractions = this.source.Fractions;
            UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(fractions.Length * 3);
            uint* header = (uint*)indexArray.FirstElement();
            //uint* last = (uint*)indexArray.LastElement();
            //uint* tail = (uint*)indexArray.TailAddress();

            int index = 0;
            for (int i = 0; i < fractions.Length; i++)
            {
                var triangleIndexes = fractions[i];
                //indexArray[index++] = (uint)triangleIndexes[0];
                //indexArray[index++] = (uint)triangleIndexes[1];
                //indexArray[index++] = (uint)triangleIndexes[2];
                *(header + index++) = (uint)triangleIndexes[0];
                *(header + index++) = (uint)triangleIndexes[1];
                *(header + index++) = (uint)triangleIndexes[2];
                if (triangleIndexes[3] == -1)
                {
                    Console.WriteLine("something wrong in your mind");
                }
            }

            gl.GenBuffers(1, this.fractionsIndexBufferObjects);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.fractionsIndexBufferObjects[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Header, OpenGL.GL_STATIC_DRAW);

            this.fractionsIndexCount = fractions.Length * 3;

            indexArray.Dispose();
        }

        private unsafe void PrepareTetrasIndexBufferObject(OpenGL gl)
        {
            //List<int> tetraIndexes = new List<int>();
            var triangles = this.source.Triangles;
            var tetras = this.source.Tetras;
            UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(tetras.Length * 4 * 3);
            uint* header = (uint*)indexArray.FirstElement();
            uint* last = (uint*)indexArray.LastElement();
            uint* tail = (uint*)indexArray.TailAddress();

            //int index = 0;
            int length = tetras.Length;
            for (int i = 0; i < length; i++)
            {
                var tetra = tetras[i];
                var tetraIndex = tetra[4];
                //tetraIndexes.Add(tetraIndex);
                for (int j = 0; j < 4; j++)
                {
                    var faceIndex = tetra[j];

                    //if (faceIndex >= triangles.Length)
                    {
                        //Console.WriteLine("something wrong in your mind");
                    }

                    var triangle = triangles[faceIndex];
                    int tmp = tetraIndex * 12 + j * 3;
 
                    if (header + tmp + 2 < tail)//or: if (header + tmp + 2 <= last)
                    {
                        //indexArray[tmp + 0] = (uint)triangle[0];
                        //indexArray[tmp + 1] = (uint)triangle[1];
                        //indexArray[tmp + 2] = (uint)triangle[2];
                        *(header + tmp + 0) = (uint)triangle[0];
                        *(header + tmp + 1) = (uint)triangle[1];
                        *(header + tmp + 2) = (uint)triangle[2];
                    }

                    //if (tetraIndex * 12 + j * 3 + 2 >= indexArray.Length)
                    {
                        //Console.WriteLine("something wrong in your mind");
                    }
                    //if (triangle[3] == -1)
                    {
                        //Console.WriteLine("something wrong in your mind");
                    }
                }
            }
            //tetraIndexes.Sort();
            //for (int i = 0; i < tetraIndexes.Count; i++)
            //{

            //}

            gl.GenBuffers(1, this.tetrasIndexBufferObjects);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.tetrasIndexBufferObjects[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexArray.ByteLength, indexArray.Header, OpenGL.GL_STATIC_DRAW);

            this.tetrasIndexCount = tetras.Length * 4 * 3;

            indexArray.Dispose();
        }

        private unsafe void PrepareColorBufferObject(OpenGL gl)
        {
            var coords = this.source.Coords;
            // coords colors
            // random color for now
            UnmanagedArray<vec4> colorArray = new UnmanagedArray<vec4>(coords.Length);
            vec4* header = (vec4*)colorArray.FirstElement();
            //uint* last = (uint*)colorArray.LastElement();
            //uint* tail = (uint*)colorArray.TailAddress();

            // 根据 每个fraction的三个顶点颜色相同 进行配色
            {
                //var fractions = this.source.Fractions;
                //int coloredCount = 0;
                //for (int i = 0; i < fractions.Length; i++)
                //{
                //    var triangleIndexes = fractions[i];
                //    if (triangleIndexes[3] != -1)
                //    {
                //        vec4 color = new vec4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                //        colorArray[triangleIndexes[0]] = color;
                //        colorArray[triangleIndexes[1]] = color;
                //        colorArray[triangleIndexes[2]] = color;
                //        coloredCount += 3;
                //    }
                //    else
                //    {
                //        Console.WriteLine("asdfasdfafs");
                //    }
                //}
                //if (coloredCount != coords.Length)
                //{
                //    Console.WriteLine("asdfasdfafs");
                //}
            }

            // 根据 每个tetra的4个面的4*3个顶点颜色相同 进行配色
            {
                var triangles = this.source.Triangles;
                var tetras = this.source.Tetras;
                //UnmanagedArray<uint> indexArray = new UnmanagedArray<uint>(tetras.Length * 4 * 3);
                //int index = 0;
                for (int i = 0; i < tetras.Length; i++)
                {
                    var tetra = tetras[i];
                    var tetraIndex = tetra[4];

                    vec4 color = new vec4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

                    for (int j = 0; j < 4; j++)
                    {
                        var faceIndex = tetra[j];

                        if (faceIndex >= triangles.Length)
                        {
                            Console.WriteLine("something wrong in your mind");
                        }

                        var triangle = triangles[faceIndex];
                        //colorArray[triangle[0]] = color;
                        //colorArray[triangle[1]] = color;
                        //colorArray[triangle[2]] = color;
                        *(header + triangle[0]) = color;
                        *(header + triangle[1]) = color;
                        *(header + triangle[2]) = color;
                    }
                }
            }

            {
                gl.GenBuffers(1, this.colorsBufferObject);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorsBufferObject[0]);
                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);
            }
            colorArray.Dispose();
        }

        private unsafe Vertex[] PreparePositionBufferObject(OpenGL gl)
        {
            var coords = this.source.Coords;

            // coords positions
            UnmanagedArray<vec3> positionArray = new UnmanagedArray<vec3>(coords.Length);
            vec3* header = (vec3*)positionArray.FirstElement();
            //uint* last = (uint*)positionArray.LastElement();
            //uint* tail = (uint*)positionArray.TailAddress();

            for (int i = 0; i < this.source.Coords.Length; i++)
            {
                Vertex v = coords[i];
                //positionArray[i] = new vec3(v.X, v.Y, v.Z);
                *(header + i) = new vec3(v.X, v.Y, v.Z);
            }
            {
                gl.GenBuffers(1, this.positionBufferObject);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.positionBufferObject[0]);
                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
            }
            positionArray.Dispose();
            return coords;
        }

        private void InitTetras(OpenGL gl)
        {
            gl.BindVertexArray(this.vertexArrayObject[1]);

            // coords positions
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.positionBufferObject[0]);
                gl.VertexAttribPointer(this.in_PositionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_PositionLocation);
            }

            // coords colors
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorsBufferObject[0]);
                gl.VertexAttribPointer(this.in_ColorLocation, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_ColorLocation);
            }

            // element array for tetras
            {
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.tetrasIndexBufferObjects[0]);
            }

            gl.BindVertexArray(0);
        }

        private void InitFractions(OpenGL gl)//, UnmanagedArray<vec3> positionArray, UnmanagedArray<vec4> colorArray)
        {
            gl.BindVertexArray(this.vertexArrayObject[0]);

            // coords positions
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.positionBufferObject[0]);
                gl.VertexAttribPointer(this.in_PositionLocation, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_PositionLocation);
            }

            // coords colors
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorsBufferObject[0]);
                gl.VertexAttribPointer(this.in_ColorLocation, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(this.in_ColorLocation);
            }

            // element array for fractions
            {
                gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.fractionsIndexBufferObjects[0]);
            }

            gl.BindVertexArray(0);
        }

        static Random random = new Random();
    }
}
