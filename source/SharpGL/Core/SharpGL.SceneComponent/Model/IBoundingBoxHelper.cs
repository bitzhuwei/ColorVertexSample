using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    public static class IBoundingBoxHelper
    {
        /// <summary>
        /// Expands the <see cref="IBoundingBox"/>'s values.
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="factor">0 for no expanding.</param>
        public static void Expand(this IBoundingBox boundingBox, float factor = 0.1f)
        {
            if (boundingBox == null) { return; }

            Vertex min = boundingBox.MinPosition;
            Vertex max = boundingBox.MaxPosition;

            if (boundingBox.MaxPosition.X < min.X) { min.X = boundingBox.MaxPosition.X; }
            if (boundingBox.MaxPosition.Y < min.Y) { min.Y = boundingBox.MaxPosition.Y; }
            if (boundingBox.MaxPosition.Z < min.Z) { min.Z = boundingBox.MaxPosition.Z; }

            if (max.X < boundingBox.MinPosition.X) { max.X = boundingBox.MinPosition.X; }
            if (max.Y < boundingBox.MinPosition.Y) { max.Y = boundingBox.MinPosition.Y; }
            if (max.Z < boundingBox.MinPosition.Z) { max.Z = boundingBox.MinPosition.Z; }

            float distance = (float)((max - min).Magnitude() * factor);
            Vertex vector = (max - min);
            vector *= (1 + factor);
            Vertex newMax = min + vector;
            Vertex newMin = max - vector;
            boundingBox.Set(newMin.X, newMin.Y, newMin.Z, newMax.X, newMax.Y, newMax.Z);
        }
    }
}
