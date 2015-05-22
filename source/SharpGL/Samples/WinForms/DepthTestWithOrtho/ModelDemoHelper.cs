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
            var max = modelDemo.MaxPosition;
            var min = modelDemo.MinPosition;
            var positions = modelDemo.positions;
            var colors = modelDemo.colors;
            for (int i = 0; i < pointCount; i++)
            {
                var position = new Vertex();
                //position = (max - min) * (float)random.NextDouble() + min;
                position.X = (max .X- min.X) * (float)random.NextDouble() + min.X;
                position.Y = (max .Y- min.Y) * (float)random.NextDouble() + min.Y;
                position.Z = (max .Z- min.Z) * (float)random.NextDouble() + min.Z;
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
