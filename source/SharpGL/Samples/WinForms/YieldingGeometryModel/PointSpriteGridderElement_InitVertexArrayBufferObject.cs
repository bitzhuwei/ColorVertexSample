using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
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
    public partial class PointSpriteGridderElement
    {
        protected override void InitVertexArrayBufferObject(OpenGL gl, out uint mode, out uint[] vao, out int primitiveCount, PointSpriteMesh mesh)
        {
            mode = OpenGL.GL_POINTS;

            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);

            primitiveCount = mesh.PositionArray.Length;

            //  Create a vertex buffer for the vertex data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.PositionArray.ByteLength, mesh.PositionArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexPosition);
            }

            //  Now do the same for the colour data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                // TODO: mesh.ColorArray may be null!
                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.ColorArray.ByteLength, mesh.ColorArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexColour, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexColour);
                this.colorBuffer = ids[0];
            }

            // Now do the same for the index's visual signal data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.VisibleArray.ByteLength, mesh.VisibleArray.Header, OpenGL.GL_DYNAMIC_READ);
                gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexVisible);

                this.visualBuffer = ids[0];
            }
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.RadiusArray.ByteLength, mesh.RadiusArray.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexRadius, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexRadius);
                this.radiusBuffer = ids[0];
            }

            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }


        public void UpdateColorBuffer(OpenGL gl, UnmanagedArray<ColorF> colors, UnmanagedArray<float> visibles)
        {
            if (this.visualBuffer == 0 || this.colorBuffer == 0)
                return;
            gl.MakeCurrent();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.visualBuffer);
            IntPtr destVisibles = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destVisibles, visibles.Header, (uint)visibles.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.colorBuffer);
            IntPtr destColors = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destColors, colors.Header, (uint)colors.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

        }

        public void UpdateRadiusBuffer(OpenGL gl, UnmanagedArray<float> radius)
        {
            if (this.radiusBuffer == 0)
                return;
            gl.MakeCurrent();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, this.radiusBuffer);
            IntPtr destRadius = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);
            MemoryHelper.CopyMemory(destRadius, radius.Header, (uint)radius.ByteLength);
            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);

        }

    }
}
