using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.Builder
{
    public static class PointSpriteGridderElementHelper
    {

        /// <summary>
        /// 随机决定此gridder内的各个元素的可见性。
        /// </summary>
        /// <param name="element"></param>
        /// <param name="gl"></param>
        /// <param name="probability">可见度，范围为0 ~ 1，0为全部不可见，1为全部可见。</param>
        public static void RandomVisibility(this PointSpriteGridderElement element, OpenGL
             gl, double probability)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, element.visualBuffer);
            IntPtr visualArray = gl.MapBuffer(OpenGL.GL_ARRAY_BUFFER, OpenGL.GL_READ_WRITE);

            unsafe
            {
                int arrayLength = (int)(element.Source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

                float* visuals = (float*)visualArray.ToPointer();

                bool signal;

                for (int gridIndex = 0; gridIndex < element.Source.DimenSize; gridIndex++)
                {
                    // TODO: 此signal应由具体业务提供。
                    signal = (random.NextDouble() < probability);

                    // 计算visual信息。
                    visuals[gridIndex + 0] = signal ? 1 : 0;
                }
            }

            gl.UnmapBuffer(OpenGL.GL_ARRAY_BUFFER);
        }

        public static void MinMax(Vertex value, ref Vertex min, ref Vertex max)
        {

            if (value.X < min.X)
                min.X = value.X;
            if (value.Y < min.Y)
                min.Y = value.Y;
            if (value.Z < min.Z)
                min.Z = value.Z;
            if (value.X > max.X)
                max.X = value.X;
            if (value.Y > max.Y)
                max.Y = value.Y;
            if (value.Z > max.Z)
                max.Z = value.Z;
        }

        public static PointSpriteMesh CreateMesh(this PointSpriteGridderSource source)
        {
            if (source.DimenSize <= 0)
                return null;

            //每个圆需要1个位置顶点。
            UnmanagedArray<Vertex> positions = new UnmanagedArray<Vertex>(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

            //是否可见
            UnmanagedArray<float> visibles = new UnmanagedArray<float>(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

            //半径
            UnmanagedArray<float> radiusArray = new UnmanagedArray<float>(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

            Vertex min = new Vertex();
            Vertex max = new Vertex();
            min.X = 0; min.Y = 0; min.Z = 0;
            max.X = 1; max.Y = 1; max.Z = -1;

            unsafe
            {
                int i, j, k;
                bool assigned = false;
                //顺序很重要
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    source.InvertIJK(gridIndex, out i, out j, out k);
                    Vertex position = source.GetPosition(i, j, k);
                    if (!assigned)
                    {
                        assigned = true;
                        min = position;
                        max = position;
                    }
                    MinMax(position, ref min, ref max);

                    int cellOffset = gridIndex * PointSpriteGridderElement.vertexCountPerElement;

                    positions[cellOffset + 0] = position;

                    float visible = source.IsActiveBlock(i, j, k) == true ? 1.0f : 0;
                    visibles[cellOffset + 0] = visible;

                    // TODO: 此处应由具体业务决定。
                    radiusArray[cellOffset + 0] = source.GetRadius(i, j, k);
                }
            }

            PointSpriteMesh mesh = new PointSpriteMesh();
            mesh.PositionArray = positions;
            mesh.VisibleArray = visibles;
            mesh.RadiusArray = radiusArray;
            mesh.Min = min;
            mesh.Max = max;
            return mesh;
        }

        static Random random = new Random();

        public static UnmanagedArray<ColorF> FromColors(PointSpriteGridderSource source, int[] gridIndexes, ColorF[] colors, UnmanagedArray<float> visibles)
        {
            UnmanagedArray<ColorF> colorArray = new UnmanagedArray<ColorF>(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);
            UnmanagedArray<byte> hasColorArray = new UnmanagedArray<byte>(source.DimenSize);
            unsafe
            {

                //是否有颜色

                int i = 0;
                for (i = 0; i < gridIndexes.Length; i++)
                {
                    int gridIndex = gridIndexes[i];
                    hasColorArray[gridIndex] = 1;
                    int cellOffset = gridIndex * PointSpriteGridderElement.vertexCountPerElement;
                    ColorF cf = colors[i];

                    colorArray[cellOffset + 0] = cf;
                }
                int j, k;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    int cellOffset = gridIndex * PointSpriteGridderElement.vertexCountPerElement;
                    source.InvertIJK(gridIndex, out i, out j, out k);
                    if (hasColorArray[gridIndex] == 0 || !source.IsActiveBlock(i, j, k))
                    {
                        visibles[cellOffset + 0] = 0;
                    }

                }

            }
            hasColorArray.Dispose();
            return colorArray;
        }
    }
}
