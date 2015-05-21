using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DepthTestWithOrtho
{
    class ModelDemo : SceneElement, IRenderable
    {
        internal List<Vertex> positions = new List<Vertex>();
        internal List<GLColor> colors = new List<GLColor>();
        public ModelDemo(Vertex minPosition, Vertex maxPosition, int pointCount, BeginMode mode)
        {
            this.MinPosition = minPosition;
            this.MaxPosition = maxPosition;
            this.Mode = mode;
            ModelDemoHelper.Build(this, pointCount); 
        }

        #region IRenderable 成员

        void IRenderable.Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            RenderModel(gl);

            RenderBoundingBox(gl);
        }

        private void RenderModel(OpenGL gl)
        {
            gl.Begin(this.Mode);
            for (int i = 0; i < this.positions.Count; i++)
            {
                gl.Color(this.colors[i].R, this.colors[i].G, this.colors[i].B);
                gl.Vertex(this.positions[i].X, this.positions[i].Y, this.positions[i].Z);
            }
            gl.End();
        }

        private void RenderBoundingBox(OpenGL gl)
        {
            gl.Color(1f, 1f, 1f);
            //gl.Color(1.0f, 0, 0);
            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(MinPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MinPosition.Y, MaxPosition.Z);
            gl.End();

            //gl.Color(0, 1.0f, 0);
            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MinPosition.Z);
            gl.Vertex(MaxPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.Vertex(MinPosition.X, MaxPosition.Y, MaxPosition.Z);
            gl.End();

            //gl.Color(0, 0, 1.0f);
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

        public Vertex MinPosition { get; set; }

        public Vertex MaxPosition { get; set; }

        public BeginMode Mode { get; set; }
    }
}
