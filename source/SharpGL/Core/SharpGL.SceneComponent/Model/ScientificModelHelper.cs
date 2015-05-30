using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class ScientificModelHelper
    {
        /// <summary>
        /// render with Vertex Array(not VAO)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        public static void RenderVertexArray(this ScientificModel model, OpenGL gl, RenderMode renderMode)
        {
            if (model == null) { return; }
            if (model.VertexCount <= 0) { return; }

            // render with Vertex Array(not VAO)
            gl.Enable(OpenGL.GL_POINT_SPRITE_ARB);

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, model.positions);
            gl.ColorPointer(3, OpenGL.GL_BYTE, 0, model.colors);

            gl.DrawArrays((uint)model.Mode, 0, model.VertexCount);

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);
        }
    }
}
