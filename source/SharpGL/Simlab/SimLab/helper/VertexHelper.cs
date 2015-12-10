using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid.helper
{
    public class VertexHelper
    {

        public static Vertex MinVertex(Vertex min, Vertex value)
        {
            if (min.X > value.X)
                min.X = value.X;
            if (min.Y > value.Y)
                min.Y = value.Y;
            if (min.Z > value.Z)
                min.Z = value.Z;
            return min;
        }

        public static Vertex MaxVertex(Vertex max, Vertex value)
        {
            if (max.X < value.X)
                max.X = value.X;
            if (max.Y < value.Y)
                max.Y = value.Y;
            if (max.Z < value.Z)
                max.Z = value.Z;
            return max;
        }

    }
}
