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
    public partial class PointSpriteGridderElement
    {

        protected override void InitVertexArrayBufferObject(OpenGL gl, out uint mode, out uint[] vao, out int primitiveCount, TriangleMesh mesh)
        {
            mode = OpenGL.GL_POINTS;

            vao = new uint[1];
            gl.GenVertexArrays(1, vao);
            gl.BindVertexArray(vao[0]);

            primitiveCount = mesh.Vertexes.Count;

            //  Create a vertex buffer for the vertex data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.Vertexes.ByteLength, mesh.Vertexes.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexPosition, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexPosition);
            }

            //  Now do the same for the colour data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.VertexColors.ByteLength, mesh.VertexColors.Header, OpenGL.GL_STATIC_DRAW);
                gl.VertexAttribPointer(attributeIndexColour, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexColour);
            }

            // Now do the same for the index's visual signal data.
            {
                uint[] ids = new uint[1];
                gl.GenBuffers(1, ids);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ids[0]);

                gl.BufferData(OpenGL.GL_ARRAY_BUFFER, mesh.Visibles.ByteLength, mesh.Visibles.Header, OpenGL.GL_DYNAMIC_READ);
                gl.VertexAttribPointer(attributeIndexVisible, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(attributeIndexVisible);

                this.visualBuffer = ids[0];
            }

            //  Unbind the vertex array, we've finished specifying data for it.
            gl.BindVertexArray(0);
        }

    }
}
