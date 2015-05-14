using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Specify a cuboid that marks a model's edges.
    /// </summary>
    public class BoundingBox : IBoundingBox
    {
        /// <summary>
        /// Maximum position of this cuboid.
        /// </summary>
        private Vertex maxPosition;


        /// <summary>
        /// Minimum position of this cuboid.
        /// </summary>
        private Vertex minPosition;


        /// <summary>
        /// Cuboid's color of its lines.
        /// </summary>
        public GLColor BoxColor { get; set; }

        public BoundingBox()
        {
            BoxColor = new GLColor(1, 1, 1, 1);// white color
        }


        #region IBoundingBox 成员

        /// <summary>
        /// Maximum position of this cuboid.
        /// </summary>
        public Vertex MaxPosition
        {
            get { return maxPosition; }
            set { maxPosition = value; }
        }

        /// <summary>
        /// Minimum position of this cuboid.
        /// </summary>
        public Vertex MinPosition
        {
            get { return minPosition; }
            set { minPosition = value; }
        }

        /// <summary>
        /// Get center position of this cuboid.
        /// </summary>
        /// <param name="x">x position.</param>
        /// <param name="y">y position.</param>
        /// <param name="z">z position.</param>
        public void GetCenter(out float x, out float y, out float z)
        {
            x = (this.MaxPosition.X + this.MinPosition.X) / 2;
            y = (this.MaxPosition.Y + this.MinPosition.Y) / 2;
            z = (this.MaxPosition.Z + this.MinPosition.Z) / 2;
        }

        /// <summary>
        /// Gets the bound dimensions.
        /// </summary>
        /// <param name="x">The x size.</param>
        /// <param name="y">The y size.</param>
        /// <param name="z">The z size.</param>
        public void GetBoundDimensions(out float xSize, out float ySize, out float zSize)
        {
            Vertex diff = this.MaxPosition - this.MinPosition;
            xSize = Math.Abs(diff.X);
            ySize = Math.Abs(diff.Y);
            zSize = Math.Abs(diff.Z);
        }

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="renderMode">The render mode.</param>
        public virtual void Render(OpenGL gl, RenderMode renderMode)
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

        #endregion

        /// <summary>
        /// Make sure the bounding box covers specifed vertex.
        /// </summary>
        /// <param name="vertex"></param>
        internal void Extend(Vertex vertex)
        {
            if (vertex.X < this.minPosition.X) { this.minPosition.X = vertex.X; }
            if (vertex.Y < this.minPosition.Y) { this.minPosition.Y = vertex.Y; }
            if (vertex.Z < this.minPosition.Z) { this.minPosition.Z = vertex.Z; }

            if (vertex.X > this.maxPosition.X) { this.maxPosition.X = vertex.X; }
            if (vertex.Y > this.maxPosition.Y) { this.maxPosition.Y = vertex.Y; }
            if (vertex.Z > this.maxPosition.Z) { this.maxPosition.Z = vertex.Z; }

        }

        internal void Reset()
        {
            this.minPosition.X = 0;
            this.minPosition.Y = 0;
            this.minPosition.Z = 0;

            this.maxPosition.X = 0;
            this.maxPosition.Y = 0;
            this.maxPosition.Z = 0;
        }
    }
}
