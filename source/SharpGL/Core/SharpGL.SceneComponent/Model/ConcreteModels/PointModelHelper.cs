﻿using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    class PointModelHelper
    {
        internal static void Build(PointModel model, int nx, int ny, int nz, float radius, float minValue, float maxValue)
        {
            Random positionRandom = new Random();
            Random colorRandom = new Random();

            Vertex min = new Vertex(), max = new Vertex();
            bool isInit = false;

            unsafe
            {
                float[] positions = model.Positions;
                float[] colors = model.Colors;

                for (long i = 0; i < model.VertexCount; i++)
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

                    positions[i * 3 + 0] = x;
                    positions[i * 3 + 1] = y;
                    positions[i * 3 + 2] = z;

                    //colors[i].red = (byte)colorRandom.Next(0, 256 / 2);// 256 / 2 is max color in byte.
                    //colors[i].green = (byte)colorRandom.Next(0, 256 / 2);
                    //colors[i].blue = (byte)colorRandom.Next(0, 256 / 2);
                    //colors[i].red = (byte)(255 / 2 * ((float)(i % nx) / nx));  //(byte)colorRandom.Next(0, 256 / 2);// 256 / 2 is max color in byte.
                    //colors[i].green = (byte)(255 / 2 * ((float)(i / nx % ny) / ny));//(byte)colorRandom.Next(0, 256 / 2);
                    //colors[i].blue = (byte)(255 / 2 * ((float)(i / nx / ny % nz) / nz));//(byte)colorRandom.Next(0, 256 / 2);
                    colors[i * 3 + 0] = (2 / 2 * ((float)(i % nx) / nx));  //(byte)colorRandom.Next(0, 256 / 2);// 256 / 2 is max color in byte.
                    colors[i * 3 + 1] = (2 / 2 * ((float)(i / nx % ny) / ny));//(byte)colorRandom.Next(0, 256 / 2);
                    colors[i * 3 + 2] = (2 / 2 * ((float)(i / nx / ny % nz) / nz));//(byte)colorRandom.Next(0, 256 / 2);

                }

                model.BoundingBox.Set(min.X, min.Y, min.Z, max.X, max.Y, max.Z);
            }
        }
    }
}