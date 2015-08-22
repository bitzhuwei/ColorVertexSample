using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
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
    public partial class PointSpriteStringElement
    {
        //protected void InitVAO(OpenGL gl, out uint mode, out uint[] vao, out int primitiveCount)
        //{
        //    mode = OpenGL.GL_POINTS;

        //    vao = new uint[1];
        //    gl.GenVertexArrays(1, vao);
        //    gl.BindVertexArray(vao[0]);

        //    //primitiveCount = mesh.PositionArray.Length;
        //    primitiveCount = 1000;
        //    int count = 3;
        //    //  Create a vertex buffer for the vertex data.
        //    {
        //        UnmanagedArray<vec3> positionArray = new UnmanagedArray<vec3>(count * count * count);
        //        for (int i = 0; i < count; i++)
        //        {
        //            for (int j = 0; j < count; j++)
        //            {
        //                for (int k = 0; k < count; k++)
        //                {
        //                    positionArray[i * count * count + j * count + k] = new vec3(i - count / 2, j - count / 2, k - count / 2);
        //                }
        //            }
        //        }
        //        //positionArray[0] = new vec3(0, 0, 0);

        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        //gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.PositionArray.ByteLength, mesh.PositionArray.Header, OpenGL.GL_STATIC_DRAW);
        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
        //        gl.VertexAttribPointer(attributeIndexPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexPosition);

        //        positionArray.Dispose();
        //    }

        //    //  Now do the same for the colour data.
        //    {
        //        UnmanagedArray<vec4> colorArray = new UnmanagedArray<vec4>(count * count * count);
        //        for (int i = 0; i < count * count * count; i++)
        //        {
        //            colorArray[i] = new vec4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        //        }
        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        // TODO: mesh.ColorArray may be null!
        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);
        //        gl.VertexAttribPointer(attributeIndexColour, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexColour);

        //        colorArray.Dispose();
        //    }

        //    // Now do the same for the index's visual signal data.
        //    {
        //        UnmanagedArray<float> visibleArray = new UnmanagedArray<float>(count * count * count);
        //        for (int i = 0; i < count * count * count; i++)
        //        {
        //            visibleArray[i] = 1.0f;
        //        }
        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, visibleArray.ByteLength, visibleArray.Header, OpenGL.GL_DYNAMIC_READ);
        //        gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexVisible);

        //        visibleArray.Dispose();

        //        this.visualBuffer = ids[0];
        //    }
        //    {
        //        UnmanagedArray<float> radiusArray = new UnmanagedArray<float>(count * count * count);
        //        for (int i = 0; i < count * count * count; i++)
        //        {
        //            //radiusArray[i] = (float)random.NextDouble()*100;
        //            radiusArray[i] = this.textureWidth / 10.0f; //100;
        //        }

        //        uint[] ids = new uint[1];
        //        gl.GenBuffers(1, ids);
        //        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

        //        gl.BufferData(OpenGL.GL_ARRAY_BUFFER, radiusArray.ByteLength, radiusArray.Header, OpenGL.GL_STATIC_DRAW);
        //        gl.VertexAttribPointer(attributeIndexRadius, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
        //        gl.EnableVertexAttribArray(attributeIndexRadius);

        //        radiusArray.Dispose();
        //    }

        //    //  Unbind the vertex array, we've finished specifying data for it.
        //    gl.BindVertexArray(0);
        //}
        protected void InitVAO(OpenGL gl, out uint mode, out uint[] vao, out int primitiveCount)
        {
            mode = OpenGL.GL_POINTS;

            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);

            //primitiveCount = mesh.PositionArray.Length;
            int count = 1;
            primitiveCount = count * count * count;
            //  Create a vertex buffer for the vertex data.
            {
                UnmanagedArray<vec3> positionArray = new UnmanagedArray<vec3>(count * count * count);
                positionArray[0] = new vec3(this.position.X, this.position.Y, this.position.Z);

                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                //gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.PositionArray.ByteLength, mesh.PositionArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, positionArray.ByteLength, positionArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexPosition);

                positionArray.Dispose();
            }

            //  Now do the same for the colour data.
            {
                UnmanagedArray<vec4> colorArray = new UnmanagedArray<vec4>(count * count * count);
                for (int i = 0; i < count * count * count; i++)
                {
                    colorArray[i] = new vec4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                }
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                // TODO: mesh.ColorArray may be null!
                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, colorArray.ByteLength, colorArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexColour, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexColour);

                colorArray.Dispose();
            }

            // Now do the same for the index's visual signal data.
            {
                UnmanagedArray<float> visibleArray = new UnmanagedArray<float>(count * count * count);
                for (int i = 0; i < count * count * count; i++)
                {
                    visibleArray[i] = 1.0f;
                }
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, visibleArray.ByteLength, visibleArray.Header, OpenGL.GL_DYNAMIC_READ);
                gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexVisible);

                visibleArray.Dispose();

                this.visualBuffer = ids[0];
            }
            {
                UnmanagedArray<float> radiusArray = new UnmanagedArray<float>(count * count * count);
                for (int i = 0; i < count * count * count; i++)
                {
                    //radiusArray[i] = (float)random.NextDouble()*100;
                    radiusArray[i] = this.textureWidth / 10.0f; //100;
                }

                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, radiusArray.ByteLength, radiusArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexRadius, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexRadius);

                radiusArray.Dispose();
            }

            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }
        Random random = new Random();
    }
}
