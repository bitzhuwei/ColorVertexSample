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
            Vertex3DArray positions = new Vertex3DArray(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

            //是否可见
            FloatArray visibles = new FloatArray(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

            //半径
            FloatArray radiusArray = new FloatArray(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);

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

                    *positions[cellOffset + 0] = position;

                    float visible = source.IsActiveBlock(i, j, k) == true ? 1.0f : 0;
                    *visibles[cellOffset + 0] = visible;

                    // TODO: 此处应由具体业务决定。
                    *radiusArray[cellOffset + 0] = random.Next(1, 10);
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

        public static ColorFArray FromColors(PointSpriteGridderSource source, int[] gridIndexes, ColorF[] colors, FloatArray visibles)
        {
            ColorFArray colorArray = new ColorFArray(source.DimenSize * PointSpriteGridderElement.vertexCountPerElement);
            ByteArray hasColorArray = new ByteArray(source.DimenSize);
            unsafe
            {

                //是否有颜色

                int i = 0;
                for (i = 0; i < gridIndexes.Length; i++)
                {
                    int gridIndex = gridIndexes[i];
                    *hasColorArray[gridIndex] = 1;
                    int cellOffset = gridIndex * PointSpriteGridderElement.vertexCountPerElement;
                    ColorF cf = colors[i];

                    *colorArray[cellOffset + 0] = cf;
                }
                int j, k;
                for (int gridIndex = 0; gridIndex < source.DimenSize; gridIndex++)
                {
                    int cellOffset = gridIndex * PointSpriteGridderElement.vertexCountPerElement;
                    source.InvertIJK(gridIndex, out i, out j, out k);
                    if (*hasColorArray[gridIndex] == 0 || !source.IsActiveBlock(i, j, k))
                    {
                        *visibles[cellOffset + 0] = 0;
                    }

                }

            }
            hasColorArray.Dispose();
            return colorArray;
        }
    }
}
