using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// The axies objects are design time rendered primitives
    /// that show an RGB axies at the origin of the scene.
    /// </summary>
    public class AxisSpy : SceneElement, IRenderable
    {
        public Vertex[] projectedAxisVertexes { get; protected set; }

        const int length = 3;
        private Vertex[] axisVertexes = new Vertex[] { new Vertex(), new Vertex(length, 0, 0), new Vertex(0, length, 0), new Vertex(0, 0, length) };

        /// <summary>
        /// Initializes a new instance of the <see cref="Axies"/> class.
        /// </summary>
        public AxisSpy()
        {
            Name = "Design Time Axies";
            this.projectedAxisVertexes = this.axisVertexes.ToArray();
        }
      
        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="renderMode">The render mode.</param>
        public void Render(OpenGL gl, RenderMode renderMode)
        {
            //  Design time primitives render only in design mode.
            if (renderMode != RenderMode.Design)
                return;

            double[] modelview = new double[16];
            double[] projection = new double[16];
            int[] viewport = new int[4];
            gl.GetDouble(OpenGL.GL_MODELVIEW_MATRIX, modelview);
            gl.GetDouble(OpenGL.GL_PROJECTION_MATRIX, projection);
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
            double[] x = new double[1];
            double[] y = new double[1];
            double[] z = new double[1];
            for (int i = 0; i < this.axisVertexes.Length; i++)
            {
                var vertex = this.axisVertexes[i];
                gl.Project(vertex.X, vertex.Y, vertex.Z,
                    modelview, projection, viewport, x, y, z);
                this.projectedAxisVertexes[i].X = (float)x[0];
                this.projectedAxisVertexes[i].Y = (float)y[0];
                this.projectedAxisVertexes[i].Z = (float)z[0];
            }
        }
    }
}
