using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

            var list = new IntPtr[2];
            {
                IntPtr p = Marshal.AllocHGlobal(model.Positions.Length * sizeof(float));
                Marshal.Copy(model.Positions, 0, p, model.Positions.Length);
                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, p);
                list[0] = p;
            }
            {
                IntPtr p = Marshal.AllocHGlobal(model.Colors.Length * sizeof(float));
                Marshal.Copy(model.Colors, 0, p, model.Colors.Length);
                gl.ColorPointer(3, OpenGL.GL_FLOAT, 0, p);
                list[1] = p;
            }

       
            gl.DrawArrays((uint)model.Mode, 0, model.VertexCount);
        
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);

            Marshal.FreeHGlobal(list[0]);
            Marshal.FreeHGlobal(list[1]);
        }

        public static void Build(ScientificModel model, Vertex minPosition, Vertex maxPosition)
        {
            if (model == null) { return; }

            Random random = new Random();

            for (int i = 0; i < model.VertexCount; i++)
            {
                var x = (maxPosition.X - minPosition.X) * random.NextDouble() + minPosition.X;
                var y = (maxPosition.Y - minPosition.Y) * random.NextDouble() + minPosition.Y;
                var z = (maxPosition.Z - minPosition.Z) * random.NextDouble() + minPosition.Z;

                model.Positions[i * 3 + 0] = (float)x;
                model.Positions[i * 3 + 1] = (float)y;
                model.Positions[i * 3 + 2] = (float)z;

                model.Colors[i * 3 + 0] = (float)random.NextDouble();
                model.Colors[i * 3 + 1] = (float)random.NextDouble();
                model.Colors[i * 3 + 2] = (float)random.NextDouble();
            }
        }
    }
}
