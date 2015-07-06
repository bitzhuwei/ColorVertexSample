using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVertexSample
{
    public static class VertexHelper
    {
        public static float MinField(this Vertex vertex)
        {
            float min = vertex.X;
            if (vertex.Y < min) { min = vertex.Y; }
            if (vertex.Z < min) { min = vertex.Z; }

            return min;
        }

        public static float MinField(this Vertex[] vertexes)
        {
            if (vertexes == null || vertexes.Length == 0)
            { throw new ArgumentNullException("vertixes"); }

            float min = MinField(vertexes[0]);

            for (int i = 1; i < vertexes.Length; i++)
            {
                float tmp = MinField(vertexes[i]);
                if (tmp < min) { min = tmp; }
            }

            return min;
        }

        public static float MaxField(this Vertex vertex)
        {
            float max = vertex.X;
            if (max < vertex.Y) { max = vertex.Y; }
            if (max < vertex.Z) { max = vertex.Z; }

            return max;
        }

        public static float MaxField(this Vertex[] vertexes)
        {
            if (vertexes == null || vertexes.Length == 0)
            { throw new ArgumentNullException("vertixes"); }

            float max = MaxField(vertexes[0]);

            for (int i = 1; i < vertexes.Length; i++)
            {
                float tmp = MaxField(vertexes[i]);
                if (max < tmp) { max = tmp; }
            }

            return max;
        }
    }
}
