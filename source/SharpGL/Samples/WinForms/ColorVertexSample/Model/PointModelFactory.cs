using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;

namespace ColorVertexSample.Model
{
    class PointModelFactory
    {
        public static PointModel Create(int nx, int ny, int nz, float radius, float minValue, float maxValue)
        {
            int pointCount = nx * ny * nz;
            PointModel model = new PointModel(pointCount);
            Random positionRandom = new Random();
            Random colorRandom = new Random();

            Vertex min = new Vertex(), max = new Vertex();
            bool isInit = false;

            unsafe
            {
                for (long i = 0; i < model.PointCount; i++)
                {
                    float x = minValue + ((float)positionRandom.NextDouble()) * (maxValue - minValue);
                    float y = minValue + ((float)positionRandom.NextDouble()) * (maxValue - minValue);
                    float z = minValue + ((float)positionRandom.NextDouble()) * (maxValue - minValue);
                    if (!isInit)
                    {
                        min = new Vertex(x, y, z);
                        max = new Vertex(x, y, z);
                        isInit = true;
                    }
                    if (x < min.X) min.X = x;
                    if (x > max.X) max.X = x;
                    if (y < min.Y) min.Y = y;
                    if (y > max.Y) max.Y = y;
                    if (z < min.Z) min.Z = z;
                    if (z > max.Z) max.Z = z;

                    Vertex* positions = model.Positions;
                    positions[i].X = x;
                    positions[i].Y = y;
                    positions[i].Z = z;

                    Color* colors = model.Colors;
                    colors[i].red = (byte)colorRandom.Next(0, 256);
                    colors[i].green = (byte)colorRandom.Next(0, 256);
                    colors[i].blue = (byte)colorRandom.Next(0, 256);

                }

                model.translateVector = (max + min) / 2;

                for (long i = 0; i < model.PointCount; i++)
                {
                    Vertex* centers = model.Positions;
                    centers[i].X -= model.translateVector.X;
                    centers[i].Y -= model.translateVector.Y;
                    centers[i].Z -= model.translateVector.Z;
                }

                Vertex location = min - model.translateVector;
                Size3D size = max - min;
                Rect3D rect = new Rect3D(location, size);

                model.Bounds = rect;
            }
            return model;
        }
    }
}
