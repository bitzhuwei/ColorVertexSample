using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public class BoundingBox
    {
        public Vertex MaxPosition { get; set; }
        public Vertex MinPosition { get; set; }

        public GLColor BoxColor { get; set; }

        public BoundingBox()
        {
            BoxColor = new GLColor(1, 1, 1, 1);// white color
        }

        public Vertex GetCenter()
        {
            return (this.MaxPosition + this.MinPosition) / 2;
        }

        /// <summary>
        /// Gets the bound dimensions.
        /// </summary>
        /// <param name="x">The x size.</param>
        /// <param name="y">The y size.</param>
        /// <param name="z">The z size.</param>
        public void GetBoundDimensions(out float x, out float y, out float z)
        {
            Vertex diff = this.MaxPosition - this.MinPosition;
            x = Math.Abs(diff.X);
            y = Math.Abs(diff.Y);
            z = Math.Abs(diff.Z);
        }

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="renderMode">The render mode.</param>
        public void Render(OpenGL gl, RenderMode renderMode)
        {
            gl.Color(BoxColor);

            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(MinPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.End();

            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.End();

            gl.Begin(BeginMode.Lines);
            gl.Vertex(MinPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.End();
        }
    }
}
