using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DepthTestWithOrtho
{
    class ModelDemoHelper
    {
        internal static void Build(ModelDemo modelDemo, int pointCount)
        {
            var random = new Random();
            var positions = modelDemo.positions;
            var colors = modelDemo.colors;
            for (int i = 0; i < pointCount; i++)
            {
                var position = new Vertex();
                position.X = (float)random.NextDouble() * 2 - 1;
                position.Y = (float)random.NextDouble() * 2 - 1;
                position.Z = (float)random.NextDouble() * 2 - 1;
                positions.Add(position);
                var color = new GLColor();
                color.R = (float)random.NextDouble();
                color.G = (float)random.NextDouble();
                color.B = (float)random.NextDouble();
                color.A = (float)random.NextDouble();
                colors.Add(color);
            }
        }
    }
}
